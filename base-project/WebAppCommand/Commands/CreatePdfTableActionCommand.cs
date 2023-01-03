using Microsoft.AspNetCore.Mvc;

namespace WebAppCommand.Commands
{
    public class CreatePdfTableActionCommand<T> : ITableActionCommand //command
    {
        private readonly PdfFile<T> _pdfFile; //reciever

        public CreatePdfTableActionCommand(PdfFile<T> pdfFile)
        {
            _pdfFile = pdfFile;
        }

        public IActionResult Execute()
        {
            var pdfMemoryStream = _pdfFile.Create();

            return new FileContentResult(pdfMemoryStream.ToArray(), _pdfFile.FileType) { FileDownloadName = _pdfFile.FileName }; //download edilebilir bir dosya donerken
        }
    }
}
