using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WeeBas.Grammar;

namespace WeeBas
{
    public class Vm
    {
        private readonly TextReader reader;
        private readonly TextWriter writer;

        private readonly SortedList<int, Line> program = new SortedList<int, Line>();

        private readonly IDictionary<string, int> variables = new Dictionary<string, int>();

        private int programCounter;
        private bool running;

        private readonly Stack<int> subStack = new Stack<int>();
        private readonly Func<bool> stopFunc;

        public Vm(TextReader reader, TextWriter writer, Func<bool> stopFunc = null)
        {
            this.reader = reader;
            this.writer = writer;
            this.stopFunc = stopFunc ?? (() => false);
        }

        public void Start()
        {
            writer.WriteLine("WeeBas v1\n");
            Ready();
            ReadLines(reader);
        }

        private void ReadLines(TextReader textReader)
        {
            var readLine = default(string);
            while ((readLine = textReader.ReadLine()) != null)
            {
                var trimmedLine = readLine.Trim();

                if (!trimmedLine.Any())
                {
                    continue;
                }

                var input = new Input(trimmedLine);
                var line = Line.Parse(input, writer);
                if (line == null)
                {
                    continue;
                }
                if (line.Number != null)
                {
                    var lineNumber = Evaluate(line.Number);
                    program.Add(lineNumber, line);
                }
                else
                {
                    Execute(line.Statement.Command);
                    Ready();
                }
            }
        }

        private void Execute(ICommand command)
        {
            command.ExecuteIn(this);
        }

        private static int Evaluate(Number n)
        {
            return int.Parse(string.Join("", n.Digits.Select(d => d.Body)));
        }

        public int this[string variableName]
        {
            get
            {
                var result = 0;
                return variables.TryGetValue(variableName.ToLowerInvariant(), out result) ? result : 0;
            }
            set { variables[variableName.ToLowerInvariant()] = value; }
        }

        public void Goto(int lineNumber)
        {
            var indexOfKey = program.IndexOfKey(lineNumber);
            if (indexOfKey == -1)
            {
                End();
                writer.WriteLine(MessageFormatter.Error($"no such line {lineNumber}", LineNumberOfProgramCounter()));
            }
            else
            {
                programCounter = indexOfKey;
                if (!running)
                {
                    Run();
                }
            }
        }

        private int? LineNumberOfProgramCounter()
        {
            return programCounter < program.Count ? program.Keys[programCounter] : default(int?);
        }

        public void Run()
        {
            if (running)
            {
                programCounter = 0;
            }
            running = true;

            while (programCounter < program.Count && running && !stopFunc())
            {
                var line = program.Values[programCounter];
                programCounter += 1;
                line.Statement.Command.ExecuteIn(this);
            }

            End();
        }

        public void Gosub(int lineNumber)
        {
            subStack.Push(programCounter);
            Goto(lineNumber);
        }

        public void Return()
        {
            programCounter = subStack.Pop();
        }

        public void Clear()
        {
            End();
            program.Clear();
        }

        public void List()
        {
            var textWriter = writer;
            WriteProgram(textWriter);
        }

        private void WriteProgram(TextWriter textWriter)
        {
            foreach (var line in program.Values)
            {
                textWriter.WriteLine(line.ToString());
            }
        }

        public void End()
        {
            programCounter = 0;
            running = false;
        }

        private void Ready()
        {
            writer.WriteLine("ready");
        }

        public void Write(string s, bool newLine = true)
        {
            if (newLine)
            {
                writer.WriteLine(s);
            }
            else
            {
                writer.Write(s);
            }
        }

        public string ReadLine()
        {
            return reader.ReadLine();
        }

        public void Save(string filepath)
        {
            try
            {
                using (var streamWriter = new StreamWriter(filepath))
                {
                    WriteProgram(streamWriter);
                }
            }
            catch (Exception ex)
            {
                writer.WriteLine(MessageFormatter.Error(ex.Message, null));
            }
        }

        public void Load(string filepath)
        {
            Clear();

            try
            {
                using (var streamReader = new StreamReader(filepath))
                {
                    ReadLines(streamReader);
                }
            }
            catch (Exception ex)
            {
                writer.WriteLine(MessageFormatter.Error(ex.Message, null));
            }
        }

        private readonly Random random = new Random();

        public int Random(int upperLimit)
        {
            return random.Next(upperLimit);
        }
    }
}