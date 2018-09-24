using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace ConnectToMySql
{
    public class DropCreateMySqlDatabaseIfModelChanges<TContext> : MySqlDatabaseIntializer<TContext> where TContext : DbContext
    {
        protected virtual void Seed(TContext context) { }

        public override void InitializeDatabase(TContext context)
        {
            bool needsNewDb = false;
            if (context.Database.Exists())
            {
                if (!context.Database.CompatibleWithModel(false))
                {
                    context.Database.Delete();
                    needsNewDb = true;
                }
            }
            else
            {
                needsNewDb = true;
            }
            if (needsNewDb)
            {
                CreateMySqlDatabase(context);
                Seed(context);
            }
        }
    }
}
