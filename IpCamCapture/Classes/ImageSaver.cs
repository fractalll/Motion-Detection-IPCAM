using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IpCamCapture
{
    class ImageSaver
    {
        Mat image;
        string _outputImagePath;

        public ImageSaver(string OutputImagePath, DateTime TimeOfStart)
        {
            image = new Mat();
            _outputImagePath = OutputImagePath;

            int millesecondsToStart = TimeOfStart.Subtract(DateTime.Now).Milliseconds;
            Console.WriteLine(TimeOfStart + ":"+millesecondsToStart);

            Timer timer = new Timer(new TimerCallback(Save), image, 0, 2000);
                    
        }
        
        public void SetImage(Mat image)
        {
            this.image = image;            
        }

        void Save(object img)
        {
            Console.WriteLine("save : " + _outputImagePath);
            var image = (Mat)img;
            try
            {
                image.Save(_outputImagePath);
                Console.WriteLine(_outputImagePath + ":" + DateTime.Now);
            }
            catch
            {
                Console.WriteLine(_outputImagePath + ":" + DateTime.Now+" FAIL");
            }
            
        }





    }
}
