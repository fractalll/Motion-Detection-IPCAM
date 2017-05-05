using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IpCamMonitor.Models
{
    public class JournalViewModel
    {       
        public List<SelectListItem> ItemList { get; set; }
        public CameraModel CurrentCamera { get; set; }
    }

    public class CameraModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}