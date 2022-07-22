namespace ContactosAPI.Models
{
    public class ContactsDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string ContactsCollectionName { get; set; } = null!;
    }
}
