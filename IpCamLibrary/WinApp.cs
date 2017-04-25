using System;
using System.Diagnostics;

namespace IpCamLibrary
{
    public static class WinApp
    {
        /// <summary>
        /// Запускет приложение в командной строке Windows
        /// </summary>   
        public static void Execute(string pathToExe, string args = "", bool print = true)
        {
            if (print)
                Console.WriteLine(pathToExe + " " + args);
            try
            {
                Process.Start(pathToExe, args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }            
        }

        public static bool Kill(string name)
        {
            try
            {
                Process[] proc = Process.GetProcessesByName(name);
                if (proc.Length == 0)
                    return false;
                else
                    foreach (var p in proc)
                    {
                        p.Kill();
                        Console.WriteLine(String.Format("[{0}] {1} - has killed", p.Id, p.ProcessName));
                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
    }
}
