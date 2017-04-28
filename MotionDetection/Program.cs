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
            CameraManager manager = CameraManager.getInstance();

            string _XML = ConfigurationManager.AppSettings["PathToSettingsXML"];
            manager.LoadCamsFromConfig(_XML);

            manager.StartCapture();

            /*
            string cam = "http://10.210.50.50:8881/mjpg/video.mjpg"; 
            MotionDetector md = new MotionDetector(cam);           
            md.CycleInterval = 1;
            md.DataRecived += Md_DataRecived;



            Thread t1 = new Thread(md.Start);
            t1.Start();

            while (true)
            {
                string cmd = Console.ReadLine();              
            }*/
        }

       
        private static void Md_DataRecived(object sender, DetectingEventArgs e)
        {
            
            Console.WriteLine(e.AverageMotions+" " + DateTime.Now);
        }
    }
}
