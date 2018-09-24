using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace ConnectToMySql
{
    public class DcInitializer : DropCreateMySqlDatabaseIfModelChanges<DcContext>
    {
        protected override void Seed(DcContext context)
        {
            //context.SaveChanges();
        }
    }
}
