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

namespace IpCamMotionDetection
{
    class Program
    {
        static void Main(string[] args)
        {
            try 
            {
                //InitCameras();
            }
            catch(Exception ex)
            {
                Console.WriteLine("При инициализации приложения произошла ошибка: " + ex.Message);
                Console.ReadKey();
                return;
            }
            
            string [] sources;
            using (JournalDbCobtext db = new JournalDbCobtext())
            {
                sources = db.Cameras.Where(x => (x.Title.Contains("1-4") || x.Title.Contains("414000"))).Select(x => x.Source).ToArray();
            }

            new Task(DetectionController.GCLoop).Start(); 

            DetectionController dc = new DetectionController();
            dc.DataRecived += OnDataRecived;
            dc.CycleInterval = 60;
            dc.StartCams(sources);

            while (true)
            {               
                var key = Console.ReadKey(false);
                if (key.KeyChar == 'q')
                    Environment.Exit(0);
            }
        }
              

        private static void OnDataRecived(object sender, DetectingEventArgs e)
        {            
            e.PrintConsole();
            lock (locker)
            {
                SaveToDatabase(e);
            }
        }

        static object locker = new object();
        public static void SaveToDatabase(DetectingEventArgs e)
        {
            using (JournalDbCobtext db = new JournalDbCobtext())
            {
                db.Logs.Add(new Log()
                {
                    AverageMotions = e.AverageMotions,
                    TimeStart = e.TimeStart,
                    TimeFinish = e.TimeFinish,
                    TotalCount = e.TotalCount,
                    Camera = db.Cameras.Where(x => x.Source == e.DataSource).SingleOrDefault()      
                });

                db.SaveChanges();               
            }
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
