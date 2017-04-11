using Emgu.CV;
using IpCamCapture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test_Capture_Console
{
    class Program
    {
        static void Main(string[] args)
        {           
            CameraManager manager = CameraManager.getInstance();
            manager.StartCapture();

            while (true)
            { }
        }



    }
}