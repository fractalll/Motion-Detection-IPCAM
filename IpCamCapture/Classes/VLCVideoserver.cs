using IpCamLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace IpCamCapture
{
    class VLCVideoserver
    {
        SettingsManager _manager;
        string pathToVLC;
        
        public VLCVideoserver()
        {
            LoadConfigs();          
        }  

        void LoadConfigs()
        {
            try
            {
                pathToVLC = ConfigurationManager.AppSettings["PathToVLCexe"];
                string _XML = ConfigurationManager.AppSettings["PathToSettingsXML"];
                _manager = new SettingsManager(_XML);
                _manager.LoadConfig();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }           
        }

        public void Start()
        {
            foreach (var cam in _manager.SettingsList)
            {
                var cmd = new VLCCommand(cam.GetConnectionString(), cam.Port_vlc);            
                WinApp.Execute(pathToVLC, cmd.GetString()); 
            }
        }

    }
}
