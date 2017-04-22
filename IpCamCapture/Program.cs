using IpCamLibrary;
using System;

namespace IpCamCapture
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 0)
            {
                Route(args[0]);
                Exit();
            }

            Console.WriteLine("Welcome to video streaming program.\nPrint 'q' to exit. ");

            while (true)
            {
                Console.WriteLine("Please, input command");
                string cmd = Console.ReadLine();
                Route(cmd);
            }
        }         

        public static void Route(string cmd)
        {
            switch (cmd)
            {
                case "q": Exit(); break;
                case "start": Start(); break;
                default: break;
            }
        }

        public static void Exit()
        {
            Environment.Exit(0);
        }

        public static void Start()
        {
            (new VLCVideoserver()).Start();
        }
    }
}