namespace ViewpointAPI.Models
{

    public class SecurityDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string DataCollectionName { get; set; } = null!;

        public string ReferenceCollectionName { get; set; } = null!;

        public string IdsCollectionName { get; set; } = null!;
    }
}

