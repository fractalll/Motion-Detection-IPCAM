using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Emgu.CV;                  //
using Emgu.CV.CvEnum;           // usual Emgu Cv imports
using Emgu.CV.Structure;        //
using Emgu.CV.UI;               


namespace IpCamMonitor.Models.Camera_Services
{
    public class Camera
    {
        Capture _capture;
        string _outputImagePath;
        string _connectionString;   // "rtsp://admin:Law@9aEZ@10.210.52.1:554/g711v1";

        public Camera(string ConnectionString, string OutputImagePath)
        {            
            _connectionString = ConnectionString;
            _outputImagePath = OutputImagePath;
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
                    
                _capture.Start();
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
           
            image.Save(_outputImagePath);
        }
    }
}