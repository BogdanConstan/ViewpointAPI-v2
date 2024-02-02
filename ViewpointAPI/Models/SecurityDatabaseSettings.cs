namespace ViewpointAPI.Models;

public class SecurityDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string SecuritiesCollectionName { get; set; } = null!;
}
