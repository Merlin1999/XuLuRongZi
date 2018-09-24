using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace WpfApp1
{
    public partial class excellentmcoinEntities : DbContext
    {
        public excellentmcoinEntities()
            : base("name= xinglurongziEntities")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
        public DbSet<user_info> t_userinfo { get; set; }
    }
}
