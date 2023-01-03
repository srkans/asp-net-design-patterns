using Microsoft.AspNetCore.Mvc;

namespace WebAppCommand.Commands
{
    public interface ITableActionCommand //InterfaceCommand
    {
        IActionResult Execute(); 
    }
}
