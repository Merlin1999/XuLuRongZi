using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectToMySql
{
    public class DcContext : DbContext
    {
        private DbConnection dc = null;
        public DcContext():base("name=DcDataBase")
        {
            dc = Database.Connection;
            Database.SetInitializer<DcContext>(new DcInitializer());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<SW> SWs { get; set; }
    }
}
