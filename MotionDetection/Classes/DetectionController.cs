using IpCamLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        
        Dictionary<string, MotionDetector> detectors = new Dictionary<string, MotionDetector>();
        
        public string[] GetActiveCamSource()
        {
            return detectors.Keys.ToArray();
        }

        /// <summary>        
        /// Запускает распознавание движений с камер в отдельных потоках
        /// </summary>
        /// <param name="source">строки подключения к камерам</param>
        public void StartCams(params string[] source)
        {           
            foreach (var cam in source)
            {
                if (detectors.ContainsKey(cam)) continue;  
                           
                MotionDetector detector = new MotionDetector(cam);
                detector.CycleInterval = cycle;                
                detector.DataRecived += OnControllerDataRecived;                

                new Task(detector.Start).Start();
                detectors[cam] = detector;
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
                if (detectors.ContainsKey(cam))
                {
                    detectors[cam].Stop();
                    detectors.Remove(cam);
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

        private void OnControllerDataRecived(object sender, DetectingEventArgs e)
        {
            DataRecived?.Invoke(this, e);
        }

       




    }
}
