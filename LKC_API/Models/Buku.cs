using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LKC_API.Models
{
    public class Buku
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int Id { get; set; }

        [BsonElement("nama_buku")]
        public string NamaBuku { get; set; }

        [BsonElement("image_cover_buku")]
        public string ImageCoverBuku { get; set; }


        [BsonElement("author")]
        public string AuthorBy { get; set; }


        [BsonElement("tahun")]
        public int TahunTerbit{ get; set; }

        [BsonElement("description")]
        public string Description { get; set; }
    }
}
