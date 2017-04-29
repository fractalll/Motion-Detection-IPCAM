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
            string config_XML = ConfigurationManager.AppSettings["PathToSettingsXML"];
            string[] cams = DetectionController.LoadCamsFromConfig(config_XML);

            DetectionController dc = new DetectionController();
            dc.DataRecived += OnDataRecived;
            dc.CycleInterval = 5;
            dc.StartCams(cams[0], cams[6]);
                        
            while (true)
            {
                Thread.Sleep(5000);                
            }
        }
      
        private static void OnDataRecived(object sender, DetectingEventArgs e)
        {
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
