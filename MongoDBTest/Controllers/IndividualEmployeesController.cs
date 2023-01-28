using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace MongoDBTest.Controllers;

[ApiController]
[Route("/MessageRecords/EmployeeInfo")]
public class IndividualEmployeesController : ControllerBase
{
    private MongoClient Client { get; set; }
    private ILogger<IndividualEmployeesController> _logger { get; set; }
    private IConfiguration _configuration { get; set; }

    public IndividualEmployeesController(ILogger<IndividualEmployeesController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
        Client = new DatabaseConnectionHandler(_configuration).GetMongoClient();
    }

    [HttpGet]
    public string Get(int id)
    {
        var collection = CollectionFetcher.GetMessageCollection(Client);

        var filter = Builders<BsonDocument>.Filter.Eq("employeeId", id);

        var records = collection.Find(filter).ToCursor();


        var employeeMessageRecords = new List<BsonDocument>();
        foreach (var record in records.ToEnumerable())
        {
            employeeMessageRecords.Add(record);
        }


        return JsonConvert.SerializeObject(employeeMessageRecords);
    }
}