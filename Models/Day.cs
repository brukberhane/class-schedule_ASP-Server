using MongoDB.Bson.Serialization.Attributes;

namespace HiLCoECS.Models
{
  public class Day
  {
    [BsonElement("dow")]
    public string? DoW { get; set; }
    [BsonElement("periods")]
    public List<Period>? Periods { get; set; }
  }
}