namespace ViewpointAPI.Models
{

    public class SecurityDatabaseSettings
    {
        // Should reference to .env file go here?
        //public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string DataCollectionName { get; set; } = null!;

        public string ReferenceCollectionName { get; set; } = null!;

        public string IdsCollectionName { get; set; } = null!;
    }
}

