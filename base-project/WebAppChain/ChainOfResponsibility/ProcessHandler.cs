namespace WebAppChain.ChainOfResponsibility
{
    public abstract class ProcessHandler : IProcessHandler
    {
        private IProcessHandler nextProcessHandler;

        public virtual object Handle(object o)
        {
            if (nextProcessHandler != null) 
            { 
                return nextProcessHandler.Handle(o);
            }
            else
            {
                return null;
            }
           
        }

        public IProcessHandler SetNext(IProcessHandler processHandler)
        {
            nextProcessHandler = processHandler;
            return nextProcessHandler;
        }
    }
}
