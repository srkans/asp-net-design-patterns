using System.IO.Compression;

namespace WebAppChain.ChainOfResponsibility
{
    public class ZipFileProcessHandler<T> : ProcessHandler
    {
        public override object Handle(object o)
        {
            var excelMS = o as MemoryStream;

            excelMS.Position = 0; // icerisinde byte array tutuyor yazdirmadan once baslangic indeksini 0 olarak ayarlamak gerekiyor.

            using (var zipStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(zipStream,ZipArchiveMode.Create,true)) //zipstream gonderecek olsaydım ,true
                {
                    var zipFile = archive.CreateEntry($"{typeof(T).Name}.xlsx");

                    using(var zipEntry = zipFile.Open())
                    {
                        excelMS.CopyTo(zipEntry);   
                    }
                }

                return base.Handle(zipStream); //
            }

           
        }
    }
}
