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
    }
}
