using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HiLCoECS.Models
{
  public class Schedule
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? _ID { get; set; }
    [BsonElement("batchId")]
    public string? BatchId { get; set; }

    [BsonElement("shift")]
    public string? Shift { get; set; }
    [BsonElement("program")]
    public string? Program { get; set; }
    [BsonElement("days")]
    public List<Day>? Days { get; set; }
  }
}