using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectToMySql
{
    public class DdContext : DbContext
    {
        private MySqlConnection conn = null;
        public DdContext()
        {
            conn = new MySqlConnection("server=localhost;user id=root;password=;database=JuDb;port=3306;persist security info=true;Pooling=true;Max Pool Size=5;Min Pool Size=1;Allow Batch=true;");
            conn.Open();
        }

        public DbSet<SW> SWs { get; set; }
    }
}
