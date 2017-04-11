using System.Collections.Generic;
using System.Web.Mvc;
using IpCamLibrary;

namespace IpCamMonitor.Models.SettingsModel
{
    public class SettingsViewModel
    {       
        public IEnumerable<SelectListItem> ItemList { get; set; }
        public Settings CurrentSettings { get; set; }
    }
}