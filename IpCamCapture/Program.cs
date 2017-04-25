using IpCamLibrary;
using System;
using System.Collections;
using System.Collections.Generic;

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
            Console.WriteLine("Please, input command");


            while (true)
            {
                Console.Write("> ");
                string cmd = Console.ReadLine();
                Route(cmd);
            }
        }         

        public static void Route(string cmd)
        {
            cmd = cmd.ToLower();
            switch (cmd)
            {               
                case "start": Start(); break;
                case "kill": Kill(); break;
                case "restart": Restart(); break;
                case "help": Help(); break;
                case "": break;
                case "q": Exit(); break;
                default: Console.WriteLine("Unknow command. Print help to get command list"); break;
            }
        }

        private static void Help()
        {
            Console.WriteLine(string.Format("{0} : {1}", "start", "Запускает процессы VLC"));
            Console.WriteLine(string.Format("{0} : {1}", "kill", "Завершает все запущенные процессы VLC"));
            Console.WriteLine(string.Format("{0} : {1}", "restart", "Перезапускает все процессы VLC"));
            Console.WriteLine(string.Format("{0} : {1}", "q", "Завершает приложение"));
        }

        public static void Exit()
        {
            Environment.Exit(0);
        }

        public static void Restart()
        {
            Kill();
            Start();
        }

        public static void Kill()
        {
            try
            {
                if (!WinApp.Kill("vlc"))
                    Console.WriteLine("Vlc process not found");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void Start()
        {
            (new VLCVideoserver()).Start();
        }
    }
}