using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IpCamLibrary
{   
    public class Settings
    {       
        [XmlAttribute("Title")]
        public string Title { get; set; }

        [XmlAttribute("Ip")]
        public string Ip { get; set; }

        [XmlAttribute("Port")]
        public string Port { get; set; }

        [XmlAttribute("Url")]
        public string Url_ipcam { get; set; }

        [XmlAttribute("Login")]
        public string Login { get; set; }

        [XmlAttribute("Password")]
        public string Pass { get; set; }

        [XmlAttribute("VLC_stream_Port")]
        public string Port_vlcstream { get; set; }

        [XmlIgnore]
        public int Id { get; set; }


                                 
        public string GetConnectionString()
        {
            if (Ip == null || Login == null || Pass == null)
                return null;
            return "rtsp://" + Login + ":" + Pass + "@" + Ip + ":" + Port + "/" + Url_ipcam;
        }

        public override string ToString()
        {
            string result = "Title: " + Title + Environment.NewLine;
            result += "Ip: " + Ip + Environment.NewLine;
            result += "Port: " + Port + Environment.NewLine;
            result += "Url: " + Url_ipcam + Environment.NewLine;
            result += "Login: " + Login + Environment.NewLine;
            result += "Password: " + Pass;
            return result;
        }
    }
}
