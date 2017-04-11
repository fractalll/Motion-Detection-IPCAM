using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IpCamMonitor.Models;
using IpCamMonitor.Models.HomeModel;
using System.Web.UI;

namespace IpCamMonitor.Controllers
{
    public class HomeController : Controller
    {
        SettingsManager manager = new SettingsManager();


        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult Index()
        {
            manager.LoadConfig();
            List<HomeViewModel> model = manager.SettingsList.Select(x => new HomeViewModel
            {
                Title = x.Title,
                FileName = x.GetImagePath(SlashType.WebSlash)               
            }).ToList();

            return View(model);
        }

        [OutputCache(Duration = 1)]
        public PartialViewResult GetPicture(string filename)
        {            
            return PartialView((object)filename);
        }
    }
}