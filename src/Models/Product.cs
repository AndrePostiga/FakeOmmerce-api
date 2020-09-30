namespace FakeOmmerce.Models
{
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;  
  using MongoDB.Bson;
  using MongoDB.Bson.Serialization.Attributes;

  public class Product
  {

    // private ObjectId id;
    // private string name;    
    // private List<string> images;    
    // private List<string> categories;
    // private double price;
    // private string brand;
    // private string description;

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("id")]
    public string Id { get; set; }

    [BsonElement("name")]
    [Required]
    [MaxLength(60)]
    [MinLength(3)]   
    public string Name { get; set; }

    [BsonElement("images")]
    public List<string> Images { get; set; }
    
    [BsonElement("categories")]
    public HashSet<string> Categories { get; set; }

    [BsonElement("price")]
    [Required]
    [Range(0.1, int.MaxValue, ErrorMessage = "Prime must be higher than 0.0")]
    public double Price { get; set; }

    [Required]
    [BsonElement("brand")]
    [Required]
    public string Brand { get; set; }

    [BsonElement("description")]
    public string Description { get; set; }

    [BsonElement("categories")]
    [Required]
    public List<string> Categories { get; set; }   

    public Product()
    { }

    public Product(string name, List<string> images, List<string> categories, double price, string brand, string description)
    {
      Name = name;
      Images = images;
      Categories = categories;
      Price = price;
      Brand = brand;
      Description = description;
    }
  }
}