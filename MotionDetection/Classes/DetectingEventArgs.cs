using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpCamMotionDetection
{
    public class DetectingEventArgs : EventArgs
    {
        public double AverageMotions { get; set; }

        public double AveragePixels { get; set; }

        public int TotalCount { get; set; }

        public DateTime TimeStart { get; set; }

        public DateTime TimeFinish { get; set; }

        public string DataSource { get; set; }
    }

    public static class Ext
    {
        public static void PrintConsole(this DetectingEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine("Source " + e.DataSource +
                              " Motions " + e.AverageMotions +
                              " Count " + e.TotalCount +
                              " Time " + e.TimeStart.Hour + ":" + e.TimeStart.Minute + ":" + e.TimeStart.Second
            );
        }
    }
}
