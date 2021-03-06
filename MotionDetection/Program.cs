﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IpCamLibrary;
using IpCamLibrary.Database;
using System.Windows.Forms;

namespace IpCamMotionDetection
{
    class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
    
        }           

        public static void InitCameras()
        {
            SettingsManager sm;
            try
            {
                string path = ConfigurationManager.AppSettings["PathToSettingsXML"];
                sm = new SettingsManager(path);
                sm.LoadConfig();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            using (JournalDbCobtext db = new JournalDbCobtext())
            {
                //  *** Нужно не удалять, а обновлять в случае необходимости ***                         

                foreach (var set in sm.SettingsList)
                {
                    db.Cameras.Add(new Camera()
                    {
                        Title = set.Title,
                        Source = "http://" + sm.Ip_vlc + ":" + set.Port_vlc
                    });
                }
                db.SaveChanges();

                Console.WriteLine("Cameras updated");
            }
        }



    }    
}
