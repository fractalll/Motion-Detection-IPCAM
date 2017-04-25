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

        public ActionResult Translation(string id)
        {
            string IpCamCapture = ConfigurationManager.AppSettings["pathToCapture"];
            
            try
            {                
                WinApp.Execute(IpCamCapture, id, false);
                ViewBag.TranslationResult = "Команда " + id + " выполнена";
            }
            catch (Exception ex)
            {
                ViewBag.TranslationResult = "Прозошла ошибка: " + ex.Message;
            }
           
            return View();
        }
    }
}