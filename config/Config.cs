using MongoDB.Driver;
using  System.Collections.Generic;
using System;
using MongoDB.Bson;


namespace config.Config 
{
    public class AppConfig 
    {
        public string MONGO_DB_URL;
        MongoClient mongoDbClient;

        public AppConfig() 
        {
            var env = DotNetEnv.Env.Load();
            MONGO_DB_URL = DotNetEnv.Env.GetString("MONGODB_URL");
            GetMongoClient();
        }

        public MongoClient GetMongoClient()
        {

            MongoClient dbClient = new MongoClient(MONGO_DB_URL);
            mongoDbClient = dbClient;

            // Logs display current DBs
            foreach ( var db in GetDbList()) {
                Console.WriteLine(db);
            }
            return dbClient;
        }
        public List<MongoDB.Bson.BsonDocument> GetDbList() => mongoDbClient.ListDatabases().ToList();

        public IMongoCollection<MongoDB.Bson.BsonDocument> GetCollection(string dbName, string collectionName) 
        {
            var db = mongoDbClient.GetDatabase(dbName);
            var collection = db.GetCollection<BsonDocument>(collectionName);
            
            return collection;
        }
    }
}