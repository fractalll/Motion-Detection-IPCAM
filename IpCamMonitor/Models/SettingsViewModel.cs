using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IpCamMonitor.Models
{
   
    public class SettingsViewModel
    {       
        public IEnumerable<SelectListItem> ItemList { get; set; }
        public Settings CurrentSettings { get; set; }
    }
}