using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace XingLuRongZiServer
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
        public DbSet<menueditor_info> t_menueditorinfo { get; set; }
        public DbSet<userlog_info> t_userlog_info { get; set; }
        public DbSet<project_info> t_project_info { get; set; }
        public DbSet<rongzi_info> t_rongzi_info { get; set; }
    }
}
