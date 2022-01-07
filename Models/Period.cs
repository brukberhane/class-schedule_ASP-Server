using MongoDB.Bson.Serialization.Attributes;

namespace HiLCoECS.Models
{
  public class Period
  {
    [BsonElement("number")]
    public int Number { get; set; }
    [BsonElement("name")]
    public string? Name { get; set; }
    [BsonElement("code")]
    public string? Code { get; set; }
    [BsonElement("room")]
    public string? Room { get; set; }
    [BsonElement("time")]
    public string? Time { get; set; }
    [BsonElement("lecturer")]
    public string? Lecturer { get; set; }
  }
}