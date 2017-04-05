using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace IpCamMonitor.Models
{
    public class Settings
    {
        // id?

        [XmlAttribute("Title")]
        public string Title { get; set; }

        [XmlAttribute("Ip")]
        public string Ip { get; set; }

        [XmlAttribute("Port")]
        public string Port { get; set; }

        [XmlAttribute("Url")]
        public string Url { get; set; }

        [XmlAttribute("Login")]
        public string Login { get; set; }

        [XmlAttribute("Password")]
        public string Pass { get; set; }
        
        [XmlIgnore]
        public int Id { get; set; }

        public string GetImageName()
        {           
            return Global.RootFolder + "capture_img\\" + Ip + ".jpg";
        }                

        public string GetConnectionString()
        {
            if (Ip == null || Login == null || Pass == null)
                return null;
            return "rtsp://" + Login + ":" + Pass + "@" + Ip + ":" + Port + "/" + Url;
        }

        public override string ToString()
        {
            string result = "Title: " + Title + Environment.NewLine;
            result += "Ip: " + Ip + Environment.NewLine;
            result += "Port: " + Port + Environment.NewLine;
            result += "Url: " + Url + Environment.NewLine;
            result += "Login: " + Login + Environment.NewLine;
            result += "Password: " + Pass;            
            return result;
        }
    }
}