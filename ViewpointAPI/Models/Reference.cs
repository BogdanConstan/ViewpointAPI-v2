using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ViewpointAPI.Models
{
    public class Reference
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("identifier")]
        public string Identifier { get; set; }

        [BsonElement("field")]
        public string Field { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [BsonElement("timestamp")]
        public DateTime Timestamp { get; set; }

        [BsonElement("value")]
        public string Value { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [BsonElement("modified")]
        public DateTime Modified { get; set; }
    }
}
