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
        string _connectionString;
        string _transcodeParams;
        string _dst;

        /// <summary> Создает команду для запуска перекодирования с указанием обязательных параметров </summary>
        public VLCCommand(string CamConnectionString, string OutputPort)
        {
            try
            {                
                _transcodeParams = ConfigurationManager.AppSettings["TranscodeParams"];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }                   
            _connectionString = CamConnectionString;            
            _dst = OutputPort;
        }    

        public string GetString()        
        {
            return "-R " + _connectionString + " --sout \"" + _transcodeParams + _dst + "}\"";
        }
    }
}
