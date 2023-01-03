﻿using Microsoft.AspNetCore.Mvc;

namespace WebAppCommand.Commands
{
    public class FileCreateInvoker //Invoker
    {
        private ITableActionCommand _tableActionCommand;
        private List<ITableActionCommand> tableActionCommands = new List<ITableActionCommand>();

        public void SetCommand(ITableActionCommand tableActionCommand)
        {
            _tableActionCommand = tableActionCommand;
        }

        public void AddCommand(ITableActionCommand tableActionCommand)
        {
            tableActionCommands.Add(tableActionCommand);    
        }

        public IActionResult CreateFile()
        {
            return _tableActionCommand.Execute();
        }

        public List<IActionResult> CreateFiles()
        {
            var list = new List<IActionResult>();

            return tableActionCommands.Select(x => x.Execute()).ToList(); 
        }
    }
}
