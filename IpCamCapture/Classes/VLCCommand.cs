using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpCamCapture
{
    class VLCCommand
    {

        /// <summary>
        /// Создает команду для запуска перекодирования
        /// </summary>
        public VLCCommand()
        {
            SetDefault();
        }

        /// <summary> Создает команду для запуска перекодирования с указанием обязательных параметров </summary>
        /// <param name="PathToEXE">Путь к исполняемому файлу VLC</param>
        /// <param name="URICamera">URI строка подключения к камере</param>
        /// <param name="IpHost">URI перекодрованной трансляции</param>
        public VLCCommand(string PathToEXE, string URICamera, string Port)
        {
            SetDefault();
            this.PathToEXE = PathToEXE;
            this.URICamera = URICamera;            
            this.Port = Port;
        }        

        //Обязательные параметры
        public string PathToEXE { get; set; }
        public string URICamera { get; set; }       
        public string Port { get; set; }

        // Далее параметры, которые можно оставить дефолтными
        public string vcodec { get; set; }
        public string vb { get; set; }
        public string scale { get; set; }
        public string fps { get; set; }
        public string acodec { get; set; }       
        public string mime { get; set; }
        public string boundary { get; set; }
        public string mux { get; set; }

        /// <summary>
        /// Устанавливает по умолчанию необязательные параметры
        /// </summary>
        public void SetDefault()
        {
            vcodec = "mjpg";
            vb = "2500";
            scale = "1.0";
            fps = "25";
            acodec = "none";
            mime = "multipart/x-mixed-replace";
            boundary = "7b3cc56e5f51db803f790dad720ed50a";
            mux = "mpjpeg";
        }   

        public override string ToString()        
        {
            if (PathToEXE == null || URICamera == null || Port == null)
                return null;

            return "-R " + URICamera
                        + " --sout "
                        + "\"#transcode{"
                        + "vcodec=" + vcodec
                        + ",vb=" + vb
                        + ",scale=" + scale
                        + ",fps=" + fps
                        + ",acodec=" + acodec
                        + "}:standard{access=http{"
                        + "mime=" + mime
                        + "; boundary=" + boundary
                        + "},mux=" + mux
                        + ",dst=:" + Port + "}";                        
        }

        /// <summary>
        /// Выполняет команду в командной строке
        /// </summary>
        public void Execute()
        {
            Console.WriteLine(ToString());            
            System.Diagnostics.Process.Start(PathToEXE, this.ToString());
        }

    }
}
