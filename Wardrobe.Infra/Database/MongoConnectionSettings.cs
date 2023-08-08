namespace Wardrobe.Infra.Database;

public class MongoConnectionSettings
{
    public required string ConnectionString { get; set; }
    public required string DatabaseName { get; set; }
    public required string CollectionName { get; set; }
}