using System.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace SecretCommunicator.SecretCommunicatorDAL
{
    class AccessDatabase
    {
        private static string connectionString = "";
        private static string databaseName = "";
        internal static MongoDatabase ConnectToDatabase()
        {
            if(ConfigurationManager.AppSettings["IS_ON_APPHARBOR"]=="Yes")
            {
                connectionString = ConfigurationManager.AppSettings["MongoConnectionString"];
                databaseName = ConfigurationManager.AppSettings["MongoDatabaseName"];
            }
            var server = MongoServer.Create(connectionString);
            var database = server.GetDatabase(databaseName);
            return database;
        }
    }
}
