using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace RESTAPI.Models
{
    public class Product
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string? ProductName { get; set; }
        public int CategoryId { get; set; }
    }
}
