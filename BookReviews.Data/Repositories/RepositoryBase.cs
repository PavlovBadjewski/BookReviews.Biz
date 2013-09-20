using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BookReviews.Data.Repositories
{
    public class RepositoryBase
    {
        private static string _connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        private static string _database = ConfigurationManager.AppSettings["DatabaseName"];

        static RepositoryBase()
        {
            try
            {
                Server = new MongoClient(_connectionString).GetServer();
                var settings = new MongoDatabaseSettings()
                {
                    WriteConcern = WriteConcern.Unacknowledged,
                    ReadPreference = ReadPreference.Nearest
                };

                Database = Server.GetDatabase(_database, settings);
                Database.SetProfilingLevel(ProfilingLevel.Slow);
            }
            catch (Exception e)
            {
            }
        }

        public static MongoServer Server { get; set; }
        public static MongoDatabase Database { get; set; }
    }
}