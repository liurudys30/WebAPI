using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LKC_API.Models
{
    public class Pinjaman
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("id_peminjam")]
        public int IdPeminjam { get; set; }

        [BsonElement("id_buku")]
        public int IdBuku { get; set; }

        [BsonElement("tanggal_peminjaman")]
        public DateTime TanggalPeminjaman { get; set; }

        [BsonElement("tanggal_pengembalian")]
        public DateTime TanggalPengembalian { get; set; }
    }
}
