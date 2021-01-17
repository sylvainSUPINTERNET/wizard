using MongoDB.Driver;
using  System.Collections.Generic;
using System;
using MongoDB.Bson;


namespace config.DbClient
{
    public class DbClient
    {
        public string MONGO_DB_URL;
        public string DB_NAME;

        public MongoClient DbConnection;

        public DbClient() 
        {
            var env = DotNetEnv.Env.Load();
            MONGO_DB_URL = DotNetEnv.Env.GetString("MONGODB_URL");
            DB_NAME = DotNetEnv.Env.GetString("DB_NAME");

            GetMongoClient();
        }

        public MongoClient GetMongoClient()
        {

            MongoClient Client = new MongoClient(MONGO_DB_URL);
            DbConnection = Client;

            // Logs display current DBs
            foreach ( var db in GetDbList()) {
                Console.WriteLine(db);
            }
            return DbConnection;
        }
        public List<MongoDB.Bson.BsonDocument> GetDbList() => DbConnection.ListDatabases().ToList();

        public IMongoCollection<MongoDB.Bson.BsonDocument> GetCollection(string collectionName) 
        {
            var db = DbConnection.GetDatabase(DB_NAME);
            var collection = db.GetCollection<BsonDocument>(collectionName);

            return collection;
        }
        
    }
}