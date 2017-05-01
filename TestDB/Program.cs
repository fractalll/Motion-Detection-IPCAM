using IpCamLibrary.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IpCamLibrary;
using System.Threading;

namespace TestDB
{
    class Program
    {
        static void Main(string[] args)
        {
            //InitCameras();
            using (JournalDbCobtext db = new JournalDbCobtext())
            {
                db.Logs.Add(new Log()
                {
                    AverageMotions = 151,
                    TimeStart = DateTime.Now,
                    TimeFinish = DateTime.Now.AddSeconds(1),
                    TotalCount = 5,
                    Camera = new Camera()
                    {
                        Source = "source",
                        Title = "impossible"
                    }
                });

                db.SaveChanges();           
            
            }
            
            Console.ReadKey();
        }


        public static void InitCameras()
        {
            using (JournalDbCobtext db = new JournalDbCobtext())
            {
                string path = "C:\\inetpub\\wwwroot\\m\\config.xml";
                SettingsManager sm = new SettingsManager(path);
                sm.LoadConfig();

                foreach (var set in sm.SettingsList)
                {
                    db.Cameras.Add(new Camera()
                    {
                        Title = set.Title,
                        Source = "htpp://" + sm.Ip_vlc + ":" + set.Port_vlc
                    });
                }


                db.SaveChanges();
                Console.WriteLine("Cameras updated");
            }
        }
    }
}
