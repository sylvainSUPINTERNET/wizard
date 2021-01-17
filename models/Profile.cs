using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;


namespace models.Profiles {
    class Profiles {

        [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        public ObjectId Id {get; set;}

        public Profiles (string UserIdValue, string UserNameValue) {
            UserId = UserIdValue;
            Username = UserNameValue;
        }
    
        public string UserId { get; set;}
        public string Username { get; }

        public BsonDocument toBsonDocument() {
            // help here: https://www.mongodb.com/blog/post/quick-start-c-sharp-and-mongodb--creating-documents
            return new BsonDocument {
                {
                "userId", UserId
                },
                {
                 "username", Username
                }
            };  
        }
    }
}