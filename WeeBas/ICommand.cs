namespace WeeBas
{
    public interface ICommand
    {
        void ExecuteIn(Vm vm);
    }
}