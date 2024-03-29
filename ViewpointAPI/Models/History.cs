﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ViewpointAPI.Models 
{
    public class History
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonElement("identifier")]
        public string Identifier { get; set; }

        [BsonElement("field")]
        public string Field { get; set; }

        [BsonElement("timestamp")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime Timestamp { get; set; }

        [BsonElement("value")]
        public double Value { get; set; }

        [BsonElement("modified")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime Modified { get; set; }
    }
}



