using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace IpCamMonitor.Models
{
    public class SettingsManager
    {
        string _filename = Global.RootFolder + "config.xml"; 

        [XmlArray("Camera_List"), XmlArrayItem("Camera")]
        public List<Settings> SettingsList { get; set; }
                
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

                int i = 0;
                foreach (var cam in SettingsList)
                    cam.Id = i++;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("При чтении файла конфигурации произошла следующая ошибка: " + ex.Message);
            }           
        }


    }
}