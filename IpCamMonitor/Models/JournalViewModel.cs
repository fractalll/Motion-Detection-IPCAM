using IpCamLibrary.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IpCamMonitor.Models
{
    public class JournalViewModel
    {
        public List<CameraHistory> ListCams { get; set; }
    }

    public class CameraHistory
    {
        public string Title { get; set; }

        public State[] Data {get;set;}
    }



   
}