using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LKC_API.Models
{
    public class Peminjam
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("nama_peminjam")]
        public string NamaPeminjam { get; set; }

        [BsonElement("binusian")]
        public string Binusian { get; set; }
    }
}
