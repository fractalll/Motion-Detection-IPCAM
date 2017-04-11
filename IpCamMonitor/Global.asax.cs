using IpCamMonitor.Models.Camera_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace IpCamMonitor
{
    public class MvcApplication : System.Web.HttpApplication
    {
        CameraManager cam_manager = CameraManager.getInstance();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);           
        }        

    }
}
