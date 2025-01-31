namespace SnackHub.Configuration;

public record MongoDbSettings
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Database { get; set; }
    public bool Isfull { get; set; }
    public string ConnectionString { get; set; }
}