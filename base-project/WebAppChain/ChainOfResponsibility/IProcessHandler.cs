namespace WebAppChain.ChainOfResponsibility
{
    public interface IProcessHandler
    {
        IProcessHandler SetNext(IProcessHandler processHandler);

        Object Handle(Object o); //tipten bagimsiz olmak icin object tanimladik
    }
}
