using IpCamLibrary;
using System;

namespace IpCamCapture
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
               
                Console.WriteLine("• ▌ ▄ ·.                ▐ ▄     ▪      ▄▄▄▄▄              ▄▄▄  ");
                Console.WriteLine("·██ ▐███▪    ▪         •█▌▐█    ██     •██      ▪         ▀▄ █·");
                Console.WriteLine("▐█ ▌▐▌▐█·     ▄█▀▄     ▐█▐▐▌    ▐█·     ▐█.▪     ▄█▀▄     ▐▀▀▄ ");
                Console.WriteLine("██ ██▌▐█▌    ▐█▌.▐▌    ██▐█▌    ▐█▌     ▐█▌·    ▐█▌.▐▌    ▐█•█▌");
                Console.WriteLine("▀▀  █▪▀▀▀     ▀█▄▀▪    ▀▀ █▪    ▀▀▀     ▀▀▀      ▀█▄▀▪    .▀  ▀");

                Console.WriteLine("Welcome to video streaming program.\nPrint 'q' to exit. ");
                for (int i = 0; i < 120; i++)
                {
                    Console.Write("#");
                }

                while (true)
                {
                    Console.WriteLine("Please, input command");
                    string cmd = Console.ReadLine();
                    switch (cmd)
                    {
                        case "q": Exit(); break;
                        case "start": Start(); break;
                        //case "q": Exit(); break;
                        default:
                            break;
                    }
                }
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