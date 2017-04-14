using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IpCamLibrary
{
    [Serializable]
    public class SettingsManager
    {
        string _filename = "C:\\IPCamMonitorUtil\\config.xml";

        [XmlElement(ElementName = "Exe_VLC")]
        public string PathVLC_exe { get; set; }

        [XmlElement(ElementName = "Ip_VLC_Stream")]
        public string Ip_vlcstream { get; set; }

        [XmlArray("Camera_List"), XmlArrayItem("Camera")]
        public List<Settings> SettingsList { get; set; }
        
        public void Init()
        {      
            PathVLC_exe = "C:\\Program Files (x86)\\VideoLAN\\VLC\\vlc.exe";
            Ip_vlcstream = "10.210.50.50";            
        }


        public SettingsManager()
        {
            SettingsList = new List<Settings>();
        }

        /// <summary>
        /// Сериализует настройки в XML файл конфигурации
        /// </summary>
        public void SaveConfig()
        {
            try
            {
                if (File.Exists(_filename))  //лишнее?
                    File.Delete(_filename);

                using (FileStream fs = new FileStream(_filename, FileMode.Create))
                {
                    XmlSerializer xser = new XmlSerializer(typeof(SettingsManager));
                    xser.Serialize(fs, this);
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("При сохранении файла конфигурации произошла следующая ошибка: " + ex.Message);
            }
        }

        /// <summary>
        /// Десериализует настройки из XML файла конфигурации
        /// </summary>
        public void LoadConfig()
        {
            try
            {
                SettingsManager sm = null;

                using (FileStream fs = new FileStream(_filename, FileMode.Open))
                {
                    XmlSerializer xser = new XmlSerializer(typeof(SettingsManager));
                    sm = (SettingsManager)xser.Deserialize(fs);
                    fs.Close();
                }

                SettingsList = sm.SettingsList;
                PathVLC_exe = sm.PathVLC_exe;
                Ip_vlcstream = sm.Ip_vlcstream;

                int i = 0;
                foreach (var cam in SettingsList)
                    cam.Id = i++;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("При получении файла конфигурации произошла следующая ошибка: " + ex.Message);
            }
        }


    }
}
