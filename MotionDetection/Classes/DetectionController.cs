using IpCamLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IpCamMotionDetection
{
    public class DetectionController
    {
        int cycle = 10;
        public int CycleInterval
        {
            get { return cycle; }
            
            set
            {
                if (value > 0)
                    cycle = value;
            }
        }
        
        public event EventHandler<DetectingEventArgs> DataRecived;

        public event EventHandler<DetectingEventArgs> FrameProcessed;

        Dictionary<string, MotionDetector> _activeDetectors = new Dictionary<string, MotionDetector>();
        
        public string[] GetActiveCamSource()
        {   
            return _activeDetectors.Keys.ToArray();
        }

        public static void GCLoop()
        {
            while(true)
            {
                GC.Collect();
                Thread.Sleep(2*1000);
            }
        }

        private void Detector_FrameProcessed(object sender, DetectingEventArgs e)
        {
            //e.DataSource, e.TotalCount
            FrameProcessed?.Invoke(this, e);
        }

        private void OnControllerDataRecived(object sender, DetectingEventArgs e)
        {
            DataRecived?.Invoke(this, e);
        }

        /// <summary>        
        /// Запускает распознавание движений с камер в отдельных потоках
        /// </summary>
        /// <param name="source">строки подключения к камерам</param>
        public void StartCams(params string[] source)
        {           
            foreach (var cam in source)
            {
                if (_activeDetectors.ContainsKey(cam)) continue;  
                           
                MotionDetector detector = new MotionDetector(cam);
                detector.CycleInterval = cycle;                
                detector.DataRecived += OnControllerDataRecived;
                detector.FrameProcessed += Detector_FrameProcessed;

                new Task(detector.Start).Start();
                _activeDetectors[cam] = detector;
               // Thread.Sleep(0);
            }
        }
       
        /// <summary>
        /// Останавливает и уничтожает камеры
        /// </summary>   
        /// <param name="source">строки подключения к камерам</param>
        public void StopCams(params string[] source)
        {
            foreach (var cam in source) 
            {
                if (_activeDetectors.ContainsKey(cam))
                {
                    _activeDetectors[cam].Stop();
                    _activeDetectors.Remove(cam);
                }
            }
        }

        /// <summary>
        /// Перезапускает указанные камеры
        /// </summary>
        /// <param name="source">строки подключения к камерам</param>
        public void RestartCams(params string[] source)
        {
            StopCams(source);
            StartCams(source);
        }

      

       




    }
}
