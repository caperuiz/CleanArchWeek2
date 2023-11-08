using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace BasketService.API
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;

        public MongoDBContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["MongoDB:ConnectionString"]);
            _database = client.GetDatabase(configuration["MongoDB:DatabaseName"]);
        }

        public IMongoCollection<MyModel> MyCollection => _database.GetCollection<MyModel>("your-collection-name");
    }

    public class MyModel
    {
        public ObjectId Id { get; set; }
        public string Message { get; set; }
    }

}
