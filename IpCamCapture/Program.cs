using System;

namespace IpCamCapture
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Connecting to ip-Cams. Please, wait. ");

            CameraManager manager = CameraManager.getInstance();
            manager.StartCapture();

            Console.WriteLine("Grabbing images ...");
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