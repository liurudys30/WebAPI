namespace LKC_API.Configurations
{
    public class LKCDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string PinjamanCollectionName { get; set; } = null!;
        public string BukuCollectionName { get; set; } = null!;
        public string PeminjamCollectionName { get; set; } = null!;
    }
}
