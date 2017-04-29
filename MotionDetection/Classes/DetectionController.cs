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

        public void StartCams(params string[] cams)
        {
            for (int i = 0; i < cams.Length; i++)
            {
                MotionDetector detector = new MotionDetector(cams[i]);
                detector.CycleInterval = cycle;                
                detector.DataRecived += Detector_DataRecived;

                new Task(detector.Start).Start();
            }
        }

        private void Detector_DataRecived(object sender, DetectingEventArgs e)
        {
            DataRecived?.Invoke(this, e);
        }

        public static string[] LoadCamsFromConfig(string pathToConfig)
        {
            SettingsManager sm = new SettingsManager(pathToConfig);
            try
            {
                sm.LoadConfig();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            List<string> arr = new List<string>();
            foreach (Settings set in sm.SettingsList)            
                arr.Add("http://" + sm.Ip_vlc + ":" + set.Port_vlc);

            return arr.ToArray();
        }




    }
}
