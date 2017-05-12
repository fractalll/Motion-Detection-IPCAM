using IpCamLibrary.Database;
using IpCamMonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IpCamMonitor.Controllers
{
    public class JournalController : Controller
    {


        public JournalController()
        {       
        }


        [HttpGet]
        public ActionResult Index()
        {
            JournalViewModel model = new JournalViewModel()
            {
                ListCams = new List<CameraHistory>()
            };

            List<Camera> cams;
            using (JournalDbCobtext db = new JournalDbCobtext())
            {
                cams = db.Cameras.ToList();                
            }

            foreach (var cam in cams)
            {
                model.ListCams.Add(new CameraHistory
                {
                    Title = cam.Title,
                    Data = DataProvider.GetData(DateTime.Parse("2017-05-06"), cam.Id)
                });
            }  

            return View(model);
        }
     

    }
}