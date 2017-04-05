
using IpCamMonitor.Models;
using IpCamMonitor.Models.SettingsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IpCamMonitor.Controllers
{
    public class SettingsController : Controller
    {
        SettingsManager settingsManager;
        SettingsViewModel model;


        public SettingsController()
        {
            settingsManager = new SettingsManager();
            model = new SettingsViewModel();
            UpdateModelFromXML();
            SwichCurrentSettings(0);             
        }               

        /// <summary>
        /// Обновить модель из XML файла
        /// </summary>
        /// <param name="id">Id настроек</param>
        private void UpdateModelFromXML()
        {
            settingsManager.LoadConfig();                        
            
            model.ItemList = settingsManager.CamList.Select(x => new SelectListItem
            {
                Text = x.Title,
                Value = x.Id.ToString()
            });
        }

        /// <summary>
        /// Перключает настройки, используя модель
        /// </summary>        
        private void SwichCurrentSettings(SettingsViewModel svm)
        {            
            model.CurrentSettings = settingsManager.CamList.Where(x => x.Id == svm.CurrentSettings.Id).Single();     
        }

        /// <summary>
        /// Переключает настройки, используя id 
        /// </summary>        
        private void SwichCurrentSettings(int id)
        {
            // BUG: при отсутсвии нужного id, если файл пус, например, или вручную вбит адрес
            model.CurrentSettings = settingsManager.CamList.Where(x => x.Id == id).Single();
        }

        [HttpGet]
        public ActionResult Index()
        {            
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(SettingsViewModel svm)
        {
            SwichCurrentSettings(svm);
            return View(model);
        }
               
        [HttpPost]
        public ActionResult SaveSettings(SettingsViewModel svm)
        {
            try
            {
                for (int i = 0; i < settingsManager.CamList.Count; i++)
                    if (settingsManager.CamList[i].Id == svm.CurrentSettings.Id)
                        settingsManager.CamList[i] = svm.CurrentSettings;

                settingsManager.SaveConfig();
                UpdateModelFromXML();

                SwichCurrentSettings(svm);
                return View("Index", model);
            }
            catch(Exception ex)
            {
                return View("Index", model);
            }            
        }

        public ActionResult DeleteCamera(int id)
        {
            settingsManager.CamList.RemoveAll((pred) => pred.Id == id);

            settingsManager.SaveConfig();
            UpdateModelFromXML();

            SwichCurrentSettings(0);
            return View("Index", model);
        }

        [HttpGet]
        public ActionResult AddCamera()
        {
            return View();    
        }

       [HttpPost]
       public ActionResult AddCamera(Settings set)
       {
            settingsManager.CamList.Add(set);
            settingsManager.SaveConfig();
            UpdateModelFromXML();

            SwichCurrentSettings(set.Id);
            return View("Index", model);
        }
    }
}