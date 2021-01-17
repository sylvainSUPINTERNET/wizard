using System;
using System.Threading.Tasks;
using models.Profiles;
using MongoDB.Bson;
using MongoDB.Driver;

namespace services.ProfileServices 
{
    class ProfileServices 
    {
        public ProfileServices()
        {

        }

        public void insertOne(IMongoCollection<MongoDB.Bson.BsonDocument> collection,
                                                  BsonDocument document) {
            collection.InsertOneAsync(document).Wait();
        }
    }
}