using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpCamCapture
{
    class VLCCommand
    {
        string _pathToEXE;
        string _connectionString;
        string _transcodeParams;
        string _dst;

        /// <summary> Создает команду для запуска перекодирования с указанием обязательных параметров </summary>
        public VLCCommand(string CamConnectionString, string OutputPort)
        {
            try
            {
                _pathToEXE = ConfigurationManager.AppSettings["PathToVLCexe"];
                _transcodeParams = ConfigurationManager.AppSettings["TranscodeParams"];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }                   
            _connectionString = CamConnectionString;            
            _dst = OutputPort;
        }    

        public override string ToString()        
        {
            return "-R " + _connectionString + " --sout \"" + _transcodeParams + _dst + "}\"";
        }

        /// <summary>
        /// Выполняет команду в командной строке
        /// </summary>
        public void Execute()
        {
            Console.WriteLine(ToString());            
            System.Diagnostics.Process.Start(_pathToEXE, this.ToString());
        }

    }
}
