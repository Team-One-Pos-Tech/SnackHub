namespace SnackHub.Configuration;

public record StorageSettings
{
    public MongoDbSettings MongoDb { get; set; }
}