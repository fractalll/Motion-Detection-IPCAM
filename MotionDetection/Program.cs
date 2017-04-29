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
            
            
                                    
            MotionDetector md = new MotionDetector(cams[6]);           
            md.CycleInterval = 5;
            md.DataRecived += Md_DataRecived;

            Thread t1 = new Thread(md.Start);
            t1.Start();

            while (true)
            {
                string cmd = Console.ReadLine();              
            }
        }
        
       
        private static void Md_DataRecived(object sender, DetectingEventArgs e)
        {
            SaveToDatabase(e);           
        }

        public static void SaveToDatabase(DetectingEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine("Motions " + e.AverageMotions +
                              " Time Start " + e.TimeStart +
                              " Source " + e.DataSource
            );
        }
    }
}
