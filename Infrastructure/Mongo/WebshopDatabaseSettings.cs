namespace Infrastructure.Mongo;

public class WebshopDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string CollectionName { get; set; } = null!;
    
    
    public WebshopDatabaseSettings()
    {
        ConnectionString = "mongodb://localhost:27017";
        DatabaseName = "webshop";
        CollectionName = "orders";
    }

}