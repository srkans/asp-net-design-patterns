using System.Drawing;

namespace WebAppAdapter.Services
{
    public class AdvanceImageProcessAdapter :IImageProcess //legacy interface'i implement ediyor
    {
        private readonly IAdvanceImageProcess _advanceImageProcess; //adapte edilecek interface constructor'da tanimlaniyor
                                                                    //bu yuzden DI servislerde tanimlamak gerekiyor 

        public AdvanceImageProcessAdapter(IAdvanceImageProcess advanceImageProcess)
        {
            _advanceImageProcess = advanceImageProcess;
        }

        public void AddWatermark(string text, string filename, Stream imageStream)
        {
            _advanceImageProcess.AddWatermarkImage(imageStream, text,$"wwwroot/watermarks/{filename}",Color.FromArgb(128,255,255,255),Color.FromArgb(0,255,255,255));
        }
    }
}
