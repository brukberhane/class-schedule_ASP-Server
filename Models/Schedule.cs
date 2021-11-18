using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HiLCoECS.Models
{
    public class Schedule
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _ID { get; set; }
    }
}