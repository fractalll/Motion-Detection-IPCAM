using Emgu.CV;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace IpCamMotionDetection
{
    public class Camera 
    {
        Capture _capture;        
        string _connectionString;
        public int CycleInterval { get; set; }

        public Camera(string ConnectionString, int cycleInterval = 10)
        {         
            _connectionString = ConnectionString;
            CycleInterval = cycleInterval;
            Console.WriteLine("Create camera " + _connectionString);
        }
   
        public void StartCapture()
        {            
            MotionDetector md = new MotionDetector(_connectionString, CycleInterval);           
            md.DataRecived += OnDataRecived;

            Thread t1 = new Thread(md.Start);
            t1.Start();           
        }

        private void OnDataRecived(object sender, DetectingEventArgs e)
        {
            Console.WriteLine(e.DataSource + " " + e.TimeStart + " " + e.TimeFinish + " " + e.AverageMotions);
        }

           /*    
               if (!_capture.Grab())
                   throw new Exception("Невозможно захватить изображение!");               
           }*/

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
