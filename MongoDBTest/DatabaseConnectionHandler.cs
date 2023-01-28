using MongoDB.Driver;

namespace MongoDBTest;

public class DatabaseConnectionHandler
{

    private string ConnectionString { get; }
    
    public DatabaseConnectionHandler(IConfiguration configuration)
    {
        ConnectionString = configuration["ConnectionString"];
    }



    public MongoClient GetMongoClient()
    {
        return new MongoClient(ConnectionString);
    }
    
}