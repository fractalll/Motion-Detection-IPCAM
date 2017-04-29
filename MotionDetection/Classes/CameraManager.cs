using IpCamLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpCamMotionDetection
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
        }

        List<Camera> _camList;

        public List<Camera> GetCams()
        {
            return new List<Camera>(_camList);
        }


        /// <summary>
        /// Устанавливает интервал сбора данных в секундах
        /// </summary>
        public void SetCaptureInterval(int seconds)
        {
            if (seconds < 1 || _camList == null) return;
            foreach (var cam in _camList)
                cam.CycleInterval = seconds;
        }
        
        public void LoadCamsFromConfig(string pathToConfig)
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

            _camList = new List<Camera>();

            foreach (Settings set in sm.SettingsList)
            {
                string connectionString = "http://" + sm.Ip_vlc + ":" + set.Port_vlc;

                _camList.Add(new Camera(connectionString));
            }
        }

        public void StartAll()
        {
            if (_camList == null || _camList.Count == 0)
                return;
            foreach (var cam in _camList)
            {
                cam.StartCapture();
                break;
            }
                          
        }

        public void Start1()
        {
            _camList[0].StartCapture();
        }

        public void StopAll()
        {
            if (_camList == null || _camList.Count == 0) return;
            foreach (var cam in _camList)
                cam.StopCapture();
        }
    }
}