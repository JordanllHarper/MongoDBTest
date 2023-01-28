using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDBTest;

public class CollectionFetcher
{

    public static IMongoCollection<BsonDocument>? GetMessageCollection(MongoClient client)
    {
        var dbWanted = client.GetDatabase("messages");
        var collection = dbWanted.GetCollection<BsonDocument>("data");
        return collection;
    }

}