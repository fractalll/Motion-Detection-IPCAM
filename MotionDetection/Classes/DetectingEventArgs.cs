using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpCamMotionDetection
{
    class DetectingEventArgs : EventArgs
    {
        public double AverageMotions { get; set; }

        public double AveragePixels { get; set; }

        public int TotalCount { get; set; }

        public DateTime TimeStart { get; set; }

        public DateTime TimeFinish { get; set; }

        public string DataSource { get; set; }
    }
}
