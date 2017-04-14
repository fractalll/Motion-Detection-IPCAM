using IpCamLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpCamCapture
{
    public class CameraManager
    {
        private static CameraManager instance;
        public static CameraManager getInstance()
        {
            if (instance == null)
                instance = new CameraManager();
            return instance;
        }

        private CameraManager()
        {
            UpdateCamsFromConfig();
        }

        List<Camera> _camList;

        public void UpdateCamsFromConfig()
        {
            SettingsManager sm = new SettingsManager();
            try
            {
                sm.LoadConfig();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            _camList = new List<Camera>();

            foreach (Settings set in sm.SettingsList)
            {
                string _connection = set.GetConnectionString();
                _camList.Add(new Camera(_connection));
            }
        }

        public void StartCapture()
        {
            if (_camList == null || _camList.Count == 0)
                return;
            foreach (var cam in _camList)
                cam.StartCapture();                
        }

        public void StopCapture()
        {
            if (_camList == null || _camList.Count == 0) return;
            foreach (var cam in _camList)
                cam.StopCapture();
        }
    }
}