using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace ConnectToMySql
{
    public abstract class MySqlDatabaseIntializer<TContext> : IDatabaseInitializer<TContext> where TContext : DbContext
    {
        public abstract void InitializeDatabase(TContext context);
        protected void CreateMySqlDatabase(TContext context)
        {
            try
            {
                context.Database.Create();
                return;
            }
            catch (Exception ex)
            {

            }

            using (var connection =((MySqlConnection)context.Database.Connection).Clone())
            {
                using (var command = connection.CreateCommand())
                {
                    
                }
            }
        }
    }
}
