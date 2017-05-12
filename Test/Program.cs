using IpCamLibrary.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = DataProvider.GetData(DateTime.Now, 8);

        }
    }
}
