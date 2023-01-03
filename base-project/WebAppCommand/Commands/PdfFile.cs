using System.Text;

namespace WebAppCommand.Commands
{
    public class PdfFile<T> //reciever-- dinktopdf html'i pdf'e ceviriyor
    {
        public readonly List<T> _list;
        public string FileName => $"{typeof(T).Name}.pdf";
        public string FileType => "application/octet-stream"; //pdf icin standart

        public MemoryStream Create()
        {
            var type = typeof(T);

            var sb = new StringBuilder();
            //$ degisken kullanmak icin @girinti vermek icin @=`backtick js`
            sb.Append($@"<html>
                                ");
        }

    }
}
