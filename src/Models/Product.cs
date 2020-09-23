namespace FakeOmmerce.Models
{
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using MongoDB.Bson;
  using MongoDB.Bson.Serialization.Attributes;

  public class Product
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("name")]
    [Required]
    [MaxLength(60)]
    [MinLength(3)]   
    public string Name { get; set;}

    [BsonElement("images")]
    public List<string> Images { get; set; }
    
    [BsonElement("categories")]
    public List<string> Categories { get; set; }

    [BsonElement("price")]
    [Required]
    [Range(0.1, int.MaxValue, ErrorMessage = "Prime must be higher than 0.0")]
    public double Price { get; set; }

    [Required]
    [BsonElement("brand")]
    public string Brand { get; set; }
  }
}