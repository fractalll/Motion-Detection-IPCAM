﻿using IpCamLibrary;
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

        public VLCVideoserver()
        {
            set_manager = new SettingsManager();
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
                VLCCommand cmd = new VLCCommand(set_manager.PathVLC_exe, cam.GetConnectionString(), cam.Port_vlcstream);
                cmd.Execute();
            }
        }

    }
}
