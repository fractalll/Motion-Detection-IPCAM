using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpCamLibrary.Database
{
    public enum State { NoData, Yes, No }

    public class DataProvider
    {
        public static State[] GetData(DateTime date, int camId)
        {
            //10 minutes
            //08:00 - 20:00
            
            List<Log> logs;
            using (JournalDbCobtext db = new JournalDbCobtext())
            {
                logs = db.Logs.Where(x => x.Camera.Id == camId 
                    && DbFunctions.TruncateTime(x.TimeStart) == date.Date
                    && x.TimeStart.Hour >= 8 
                    && x.TimeFinish.Hour <= 20
                ).ToList();
            }
            
            State[] data = new State[72];
           
            DateTime start = date.Date.AddHours(8);
            for (int i = 0; i < data.Length; i++)
            {
                var datablock = logs.Where(x => x.TimeStart > start.Add(new TimeSpan(0, 10 * i, 0))
                    && x.TimeFinish < start.Add(new TimeSpan(0, 10 * (i + 1), 0))
                ).ToList();

                if (datablock.Count == 0)
                    data[i] = State.NoData;
                else
                    data[i] = AnalyseMotions(datablock.Average(x => x.AverageMotions));
            }

            return data;
        }    

        private static State AnalyseMotions(double count)
        {
            if (count < 400) 
                return State.No;
            return State.Yes; 
        }
            
    }
}
