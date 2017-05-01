using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpCamLibrary.Database
{
    public class Log
    {
        public int Id { get; set; }

        public double AverageMotions { get; set; }

        public int TotalCount { get; set; }

        public DateTime TimeStart { get; set; }

        public DateTime TimeFinish { get; set; }

        public Camera Camera{ get; set; }
    }
}
