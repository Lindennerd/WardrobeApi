namespace Wardrobe.Infra.Database;

public class MongoConnectionSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string CollectionName { get; set; }
}