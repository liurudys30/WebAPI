using LKC_API.Configurations;
using LKC_API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;


namespace LKC_API.Services
{
    public class LKCService
    {
        private readonly IMongoCollection<Pinjaman> _pinjamanCollection;
        private readonly IMongoCollection<Buku> _bukuCollection;
        private readonly IMongoCollection<Peminjam> _peminjamCollection;

        public LKCService(IOptions<LKCDatabaseSettings> lkcDatabaseSettings)
        {
            var mongoClient = new MongoClient(lkcDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(lkcDatabaseSettings.Value.DatabaseName);

            _pinjamanCollection = mongoDatabase.GetCollection<Pinjaman>(lkcDatabaseSettings.Value.PinjamanCollectionName);
            _bukuCollection = mongoDatabase.GetCollection<Buku>(lkcDatabaseSettings.Value.BukuCollectionName);
            _peminjamCollection = mongoDatabase.GetCollection<Peminjam>(lkcDatabaseSettings.Value.PeminjamCollectionName);
        }

        public async Task<List<Pinjaman>> GetPinjamanAsync() =>
            await _pinjamanCollection.Find(_ => true).ToListAsync();

        public async Task<Pinjaman?> GetPinjamanAsync(string id) =>
            await _pinjamanCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreatePinjamanAsync(Pinjaman newPinjaman) =>
            await _pinjamanCollection.InsertOneAsync(newPinjaman);

        public async Task UpdatePinjamanAsync(string id, Pinjaman updatedPinjaman) =>
            await _pinjamanCollection.ReplaceOneAsync(x => x.Id == id, updatedPinjaman);

        public async Task RemovePinjamanAsync(string id) =>
            await _pinjamanCollection.DeleteOneAsync(x => x.Id == id);

        // Similarly, you can add methods for Buku and Peminjam collections
        //public async Task<List<Buku>> GetBukuAsync() =>
        //           await _bukuCollection.Find(_ => true).ToListAsync();

        public async Task<List<Buku>> GetBukuAsync(string txtsearch)
        {
            /*
            if (string.IsNullOrEmpty(txtsearch))
            {
                return await _bukuCollection.Find(_ => true).ToListAsync();
            }

            var searchTerms = txtsearch.Split(',');
            var filters = new List<FilterDefinition<Buku>>();
            var yearRegex = new Regex(@"^\d{4}$"); // Regular expression to check for a 4-digit year

            for (int i = 0; i < searchTerms.Length; i++)
            {
                var term = searchTerms[i];
                if (string.IsNullOrEmpty(term)) continue;

                if (yearRegex.IsMatch(term))
                {
                    // Jika term cocok dengan format tahun, filter berdasarkan TahunTerbit sebagai int
                    if (int.TryParse(term, out int year))
                    {
                        filters.Add(Builders<Buku>.Filter.Eq("tahun", year));
                    }
                }
                else if (i == 0)
                {
                    // Asumsikan term pertama adalah NamaBuku jika bukan tahun
                    filters.Add(Builders<Buku>.Filter.Regex("nama_buku", new MongoDB.Bson.BsonRegularExpression(term, "i")));
                }
                else if (i == 1)
                {
                    // Asumsikan term kedua adalah AuthorBy jika bukan tahun
                    filters.Add(Builders<Buku>.Filter.Regex("author", new MongoDB.Bson.BsonRegularExpression(term, "i")));
                }
            }

            if (filters.Count == 0)
            {
                return await _bukuCollection.Find(_ => true).ToListAsync();
            }

            var combinedFilter = Builders<Buku>.Filter.Or(filters);

            // Debug: Print the filter
            var filterJson = combinedFilter.Render(BsonSerializer.SerializerRegistry.GetSerializer<Buku>(), BsonSerializer.SerializerRegistry).ToJson();
            Console.WriteLine($"Generated Filter: {filterJson}");

            var result = await _bukuCollection.Find(combinedFilter).ToListAsync();

            Console.WriteLine($"Found {result.Count} items");

            return result;
            */
            if (string.IsNullOrEmpty(txtsearch))
            {
                return await _bukuCollection.Find(_ => true).ToListAsync();
            }

            var searchTerms = txtsearch.Split(',');

            var filters = new List<FilterDefinition<Buku>>();
            foreach (var term in searchTerms)
            {
                var filter = Builders<Buku>.Filter.Or(
                    Builders<Buku>.Filter.Regex("nama_buku", new BsonRegularExpression(term, "i")),
                    Builders<Buku>.Filter.Regex("author", new BsonRegularExpression(term, "i")),
                    Builders<Buku>.Filter.Where(b => b.TahunTerbit.ToString().Contains(term))
                );
                filters.Add(filter);
            }

            var combinedFilter = Builders<Buku>.Filter.And(filters);

            return await _bukuCollection.Find(combinedFilter).ToListAsync();
        }
    
    }
  
}
