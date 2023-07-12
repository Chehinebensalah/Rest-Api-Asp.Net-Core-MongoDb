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
        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; }
        //this property will not be stored if you pass a null val to it
        //make sure to make it null when passing todb
        [BsonIgnoreIfNull]
        public string CategoryName { get; set; }
    }
}
