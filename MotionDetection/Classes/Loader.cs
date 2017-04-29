using IpCamLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpCamMotionDetection
{
    class Loader
    {
        public static string[] GetCamsUri(string pathToConfig)
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
