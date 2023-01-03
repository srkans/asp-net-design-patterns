using Microsoft.AspNetCore.Mvc;

namespace WebAppCommand.Commands
{
    public class CreateExcelTableActionCommand<T> : ITableActionCommand //command
    {
        private readonly ExcelFile<T> _excelFile; //receiver

        public CreateExcelTableActionCommand(ExcelFile<T> excelFile)
        {
            _excelFile = excelFile;
        }

        public IActionResult Execute()
        {
            var excelMemoryStream = _excelFile.Create();

            return new FileContentResult(excelMemoryStream.ToArray(), _excelFile.FileType) { FileDownloadName = _excelFile.FileName }; //download edilebilir bir dosya donerken
        }
    }
}
