using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpCamLibrary.Database
{   
    public class JournalDbCobtext : DbContext
    {
        public JournalDbCobtext() : base("DbConnection")
        { }

        public DbSet<Log> Logs { get; set; }

        public DbSet<Camera> Cameras { get; set; }
    }
}
