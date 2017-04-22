using Emgu.CV;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpCamCapture
{
    public class Camera 
    {
        Capture _capture;        
        string _connectionString;

        public Camera(string ConnectionString)
        {         
            _connectionString = ConnectionString;            
        }
   
        public void StartCapture()
        {
            try
            {
                _capture = new Capture(_connectionString);
            }
            catch { }

            if (_capture != null)
            {
                _capture.ImageGrabbed += ReciveNewFrame;

                if (!_capture.Grab())
                    throw new Exception("Невозможно захватить изображение!");

               // _capture.Start();
            }
        }

        public void StopCapture()
        {
            if (_capture != null)
                _capture.Stop();
        }

        private void ReciveNewFrame(object sender, EventArgs e)
        {
            GC.Collect();
            Mat image = new Mat();
            _capture.Retrieve(image);            
        }
    }
}
