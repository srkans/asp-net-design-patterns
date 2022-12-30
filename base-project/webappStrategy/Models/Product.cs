using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webappStrategy.Models
{
    public class Product
    {
        [BsonId] //mongodb icin
        [Key] //efcore
        [BsonRepresentation(BsonType.ObjectId)] //mongodb id'yi string alabilmek icin
        public string Id { get; set; } //mongodb otomatik belirliyor sql icin guid uretilecek
        public string Name { get; set; }
        [BsonRepresentation(BsonType.Decimal128)]
        [Column(TypeName ="decimal(18,2)")] //toplamda 18 karakter 2si virgulden sonra
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string UserId { get; set; } //id'yi string yapmak guvenli??arastir
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedDate { get; set; }

    }
}
