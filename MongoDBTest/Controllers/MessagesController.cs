using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace MongoDBTest.Controllers;

[ApiController]
[Route("/MessageRecords")]
public class MessagesController : ControllerBase
{
    private readonly IConfiguration _configuration;

    private readonly ILogger<MessagesController> _logger;

    private MongoClient Client { get; }

    public MessagesController(ILogger<MessagesController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;

        Client = new DatabaseConnectionHandler(_configuration).GetMongoClient();
    }

    [HttpGet(Name = "GetRecords")]
    public List<string> Get()
    {
        var collection = CollectionFetcher.GetMessageCollection(Client);

        var all = collection.Find(FilterDefinition<BsonDocument>.Empty).ToList();

        List<string> serializedList = new();

        foreach (var document in all)
        {
            serializedList.Add(document.ToJson());
        }

        return serializedList;
    }


    [HttpDelete(Name = "DeleteAllRecords")]
    public string Delete()
    {
        var db = Client.GetDatabase("messages");
        var collection = db.GetCollection<BsonDocument>("data");

        var allFilter = Builders<BsonDocument>.Filter.Empty;

        collection.DeleteMany(allFilter);


        return JsonConvert.SerializeObject(new { Message = "All records successfully deleted" });
    }

    [HttpPost(Name = "PostNewRecord")]
    public string Post(int employeeId, string textContents, string addressee)
    {
        var db = Client.GetDatabase("messages");
        var collection = db.GetCollection<BsonDocument>("data");


        collection.InsertOne(new BsonDocument()
        {
            { "employeeId", employeeId },
            { "textContents", textContents },
            { "addressee", addressee }
        });


        return JsonConvert.SerializeObject(new { Message = "Message has been added!" });
    }
}