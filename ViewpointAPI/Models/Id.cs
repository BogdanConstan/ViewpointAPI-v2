using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ViewpointAPI.Models
{
    [BsonIgnoreExtraElements]
    public class Id : SecurityData
    {
        //define variables for this collection here

        [BsonElement("identifier")]
        public string Identifier { get; set; } = string.Empty; // User input: Human readable security name. 

        [BsonElement("id_bb_global")]
        public string Id_bb_global { get; set; } = string.Empty; // Bloomberg global security identifier. 
    }
}
