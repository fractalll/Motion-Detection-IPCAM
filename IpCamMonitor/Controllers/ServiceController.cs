using IpCamLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IpCamMonitor.Controllers
{
    [Authorize(Roles = "Domain Admins")]
    public class ServiceController : Controller
    {
        // GET: Service
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StartTranslation()
        {
            string path = ConfigurationManager.AppSettings["pathToCapture"];
            ViewBag.ServiceMsg = "Трансляция успешно запущена";
            try
            {
                WinApp.Execute(path, "start");
            }
            catch (Exception ex)
            {
                ViewBag.ServiceMsg = "При зауске трансляции прозошла ошибка: " + ex.Message;
            }
           
            return View();
        }
    }
}