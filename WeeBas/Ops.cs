using System.Collections.Generic;

namespace WeeBas
{
    public static class Ops
    {
        public delegate int IntOp(int i0, int i1);

        public delegate bool RelOp(int i0, int i1);

        public static readonly IDictionary<string, RelOp> RelOps = new Dictionary<string, RelOp>
        {
            {">", (i0, i1) => i0 > i1},
            {"<", (i0, i1) => i0 < i1},
            {">=", (i0, i1) => i0 >= i1},
            {"<=", (i0, i1) => i0 <= i1},
            {"<>", (i0, i1) => i0 != i1},
            {"=", (i0, i1) => i0 == i1},
        };

        public static readonly IDictionary<string, IntOp> IntOps = new Dictionary<string, IntOp>
        {
            {"+", (i0, i1) => i0 + i1},
            {"-", (i0, i1) => i0 - i1},
            {"*", (i0, i1) => i0*i1},
            {"/", (i0, i1) => i0/i1},
        };
    }
}