using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace IpCamMotionDetection
{
    class Program
    {
        static void Main(string[] args)
        {  
            /*
            CameraManager manager = CameraManager.getInstance();

            string _XML = ConfigurationManager.AppSettings["PathToSettingsXML"];
            manager.LoadCamsFromConfig(_XML);
            manager.StartAll();
            */

            string config_XML = ConfigurationManager.AppSettings["PathToSettingsXML"];
            string[] cams = Loader.GetCamsUri(config_XML);


            string[] usedcams = new string[2] { cams[0], cams[6] };

            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < usedcams.Length; i++)
            {
                MotionDetector detector = new MotionDetector(usedcams[i]);
                detector.CycleInterval = 1;
                detector.DataRecived += OnDataRecived;
                threads.Add(new Thread(detector.Start));
            }

            foreach (var t in threads)                      
                t.Start();
              
            while (true)
            {
                Thread.Sleep(5000);                
            }
        }
        public static bool isRecived = false;
        private static void OnDataRecived(object sender, DetectingEventArgs e)
        {
            isRecived = true;
            SaveToDatabase(e);           
        }

        public static void SaveToDatabase(DetectingEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine("Motions " + e.AverageMotions +
                              " Count " + e.TotalCount + 
                              " Time " + e.TimeStart.Hour + ":" + e.TimeStart.Minute + ":" + e.TimeStart.Second +
                              " Source " + e.DataSource
            );
        }
    }
}
