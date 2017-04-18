using IpCamLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpCamCapture
{
    class VLCVideoserver
    {
        SettingsManager set_manager;
        string pathToSettingsXml;

        public VLCVideoserver()
        {
            pathToSettingsXml = "C:\\inetpub\\wwwroot\\IpCamMonitor\\config.xml";
            set_manager = new SettingsManager(pathToSettingsXml); 
            LoadSettings();
        }      

        public void LoadSettings()
        {
            set_manager.LoadConfig();
        }

        public void Start()
        {
            foreach(var cam in set_manager.SettingsList)
            {
                VLCCommand cmd = new VLCCommand(set_manager.PathVLCexe, cam.GetConnectionString(), cam.Port_vlc);
                cmd.Execute();
            }
        }

    }
}
