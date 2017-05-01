using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Xml.Serialization;

namespace IpCamLibrary
{
    [Serializable]
    public class SettingsManager
    {

        string _filename;

        public SettingsManager()
        {            
            _filename = HostingEnvironment.MapPath("~/config.xml");
            SettingsList = new List<Settings>();
        }
        
        public SettingsManager(string localPath)
        {
            _filename = localPath;
            SettingsList = new List<Settings>();
        }                      

        [XmlElement(ElementName = "Ip_VLC_Stream")]
        public string Ip_vlc { get; set; }


        [XmlArray("Camera_List"), XmlArrayItem("Camera")]
        public List<Settings> SettingsList { get; set; }
        

        /// <summary> Сериализует настройки в XML файл конфигурации </summary>
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
                
        
        void SettingsFrom(SettingsManager sm)
        {
            SettingsList = sm.SettingsList;           
            Ip_vlc = sm.Ip_vlc;
        }


        /// <summary> Десериализует настройки из XML файла конфигурации </summary>
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

                SettingsFrom(sm);               

                int i = 0;
                foreach (var cam in SettingsList)
                    cam.Id = i++;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("При получении файла конфигурации произошла следующая ошибка: " + ex.Message);
            }
        }


        public static string[] GetVLCConnectionStrings(string pathToConfig)
        {
            SettingsManager sm = new SettingsManager(pathToConfig);
            try
            {
                sm.LoadConfig();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            List<string> arr = new List<string>();
            foreach (Settings set in sm.SettingsList)
                arr.Add("http://" + sm.Ip_vlc + ":" + set.Port_vlc);

            return arr.ToArray();
        }
    }
}
