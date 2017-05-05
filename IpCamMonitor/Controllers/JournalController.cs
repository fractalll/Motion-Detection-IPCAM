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
        JournalViewModel model;

        public JournalController()
        {
            using (JournalDbCobtext db = new JournalDbCobtext())
            {
                model = new JournalViewModel();
                model.ItemList = db.Cameras.Select(x => new SelectListItem()
                {
                    Text = x.Title,
                    Value = x.Id.ToString()
                });               
            }

            model.CurrentCamera = model.ItemList.Select(x => new CameraModel()
            {
                Id = Int32.Parse(x.Value),
                Title = x.Text
            }).SingleOrDefault();
        }

        public ActionResult Index()
        {            
            
            return View(model);
        }



    }
}