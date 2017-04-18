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
         
        public VLCVideoserver()
        {
            string _XML = ConfigurationManager.AppSettings["PathToSettingsXML"];
            _manager = new SettingsManager(_XML);
            _manager.LoadConfig();
        }  

        public void Start()
        {
            foreach (var cam in _manager.SettingsList)
                (new VLCCommand(cam.GetConnectionString(), cam.Port_vlc)).Execute();
        }

    }
}
