using IpCamLibrary;
using System;

namespace IpCamCapture
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Connecting to ip-Cams. Please, wait. ");
            
            /*
            SettingsManager sm = new SettingsManager();
            sm.LoadConfig();
            sm.Init();
            sm.SaveConfig();
            */

            VLCVideoserver video_server = new VLCVideoserver();
            video_server.Start();

            //CameraManager manager = CameraManager.getInstance();
            //manager.StartCapture();
                      
            Console.WriteLine("\nPress Ctrl+C to close application");

            Console.CancelKeyPress += (sender, e) =>
            {
                Console.WriteLine("Exit...");
                Environment.Exit(0);
            };

           

            while (true)
            {
               
            }
        }
    }
}