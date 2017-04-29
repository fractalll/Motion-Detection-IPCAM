using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
            dc.CycleInterval = 60;
            dc.StartCams(cams[0], cams[6]);

            while (true)
            {
                Thread.Sleep(5000);
            }
        }

        private static void OnDataRecived(object sender, DetectingEventArgs e)
        {
            SaveToConsole(e);
            lock (locker)
            {
                SaveToFile(e);
            }
        }


        static object locker = new object();
        public static void SaveToFile(DetectingEventArgs e)
        {
            using (StreamWriter sr = new StreamWriter("C:\\cam_log.txt", true))
            {
                sr.WriteLine("Source " + e.DataSource +
                                 " Motions " + e.AverageMotions +
                                 " Count " + e.TotalCount +
                                 " Time " + e.TimeStart.Hour + ":" + e.TimeStart.Minute + ":" + e.TimeStart.Second);
            };
        }


        
    





        public static void SaveToConsole(DetectingEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine("Source " + e.DataSource + 
                              " Motions " + e.AverageMotions +
                              " Count " + e.TotalCount + 
                              " Time " + e.TimeStart.Hour + ":" + e.TimeStart.Minute + ":" + e.TimeStart.Second
                             
            );
        }
    }
}
