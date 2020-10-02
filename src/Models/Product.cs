namespace FakeOmmerce.Models
{
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.Linq;
  using System.Net;
  using MongoDB.Bson;
  using MongoDB.Bson.Serialization.Attributes;

  public class Product
  {

    private ObjectId id;
    private string name;    
    private List<string> images;    
    private List<string> categories;
    private double price;
    private string brand;
    private string description;

    // [BsonId]
    // [BsonRepresentation(BsonType.ObjectId)]
    // [BsonElement("id")]
    public ObjectId Id { get => id; set => id = value; }

    // [BsonElement("name")]
    
    public string Name { get => name; set => name = value.ToUpper(); }

    // [BsonElement("images")]
    public List<string> Images { get => images; set => images = value; }

    // [BsonElement("price")]
    [Required]
    public double Price { get => price; set => price = value; }

    // [BsonElement("brand")]
    [Required]
    public string Brand { get => brand; set => brand = value; }

    // [BsonElement("description")]
    public string Description { get => description; set => description = value; }

    // [BsonElement("categories")]
    [Required]
    public List<string> Categories { 
      get => categories; 
      set 
      {
        var result = isValidCategories(value);
        if (!result)
        {
            throw new System.Exception("Algoasdasdasd");
        }
        System.Console.WriteLine(result);
        categories = value;
      }
    }   

    public Product()
    { }

    public Product(ObjectId id, string name, List<string> images, List<string> categories, double price, string brand, string description)
    {
      // this.name = name;
      // this.images = images;
      // this.categories = categories;
      // this.price = price;
      // this.brand = brand;
      // this.description = description;

      Id = id;
      Name = name;
      Images = images;
      Categories = categories;
      Price = price;
      Brand = brand;
      Description = description;
    }

    public override bool Equals(object obj)
    {
      var product = (Product)obj;
      return this.Name == product.Name;
    }

    private bool isValidCategories(List<string> categoryName)
    {
      foreach (var item in categoryName)
      {
        System.Console.WriteLine(item);
        if (item.Any(char.IsDigit))
        {
          //throw new System.Exception($@"{item} is not an valid category name");
          return false;
        }
      }
      return true;
    }
    private bool isValidImages(List<string> imagesUrls)
    {
      foreach (var url in imagesUrls)
      {
        var request = HttpWebRequest.Create(url);
        string contentType = "";

        if (request != null)
        {
          var response = request.GetResponse();

          if (response != null)
          {
            contentType = response.ContentType;
          }
        }

        if (contentType.Contains("image"))
        {
          continue;
        }
        else
        {
          throw new System.Exception($@"{url} is not an image");
        }
      }

      return true;
    }
    private string capitalizeStrings(string str)
    {
      char[] array = str.ToCharArray();
      // Handle the first letter in the string.
      if (array.Length >= 1)
      {
        if (char.IsLower(array[0]))
        {
          array[0] = char.ToUpper(array[0]);
        }
      }

      // Scan through the letters, checking for spaces.
      // ... Uppercase the lowercase letters following spaces.
      for (int i = 1; i < array.Length; i++)
      {
        if (array[i - 1] == ' ')
        {
          if (char.IsLower(array[i]))
          {
            array[i] = char.ToUpper(array[i]);
          }
        }
      }
      return new string(array);
    }
  }
}