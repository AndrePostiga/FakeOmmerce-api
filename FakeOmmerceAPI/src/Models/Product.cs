namespace FakeOmmerce.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Net;
    using FakeOmmerce.Errors;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Product : MongoEntity
    {        
        private string name;
        private HashSet<string> images;
        private HashSet<string> categories;
        private double price;
        private string brand;
        private string description;
		
        public string Name { get => name; set => name = capitalizeStrings(value); }        
		
        public double Price { 
			get => price; 
			set
			{
				if (value < 0)
				{
					throw new BadRequestException($@"price");
				}

				price = value;
			}
		}
		
        public string Brand { get => brand; set => brand = capitalizeStrings(value); }

        public string Description { get => description; set => description = value; }

        public HashSet<string> Images 
		{ 
			get => images; 
			set
			{
				if (!isValidImages(value))
                {
                    throw new BadRequestException($@"some image");
                }

				images = value;
			}
			
		}
	
        public HashSet<string> Categories
        {
            get => categories;
            set
            {
                if (!isValidCategories(value))
                {
                    throw new BadRequestException($@"some category");
                }

                var transformedHashSet = new HashSet<string>();
                foreach (var element in value)
                {
                    transformedHashSet.Add(capitalizeStrings(element));
                }

                categories = transformedHashSet;
            }
        }

        public Product() : base()
        { }

        public Product(
            ObjectId id, 
            string name, 
            HashSet<string> images, 
            HashSet<string> categories, 
            double price, 
            string brand, 
            string description
        )   : base(id)            
        {
            this.name = name;
            this.images = images;
            this.categories = categories;
            this.price = price;
            this.brand = brand;
            this.description = description;
        }

        public Product(ProductDTO dto) : base(ObjectId.GenerateNewId())
        {
            Name = dto.Name;
            Brand = dto.Brand;
            Categories = new HashSet<string>(dto.Categories);
            Description = dto.Description;
            Images = dto.Images != null ? new HashSet<string>(dto.Images) : new HashSet<string>();
            Price = dto.Price;
        }

        public Product(ObjectId id, ProductDTO dto) : base (id)
        {         
            Name = dto.Name;
            Brand = dto.Brand;
            Categories = new HashSet<string>(dto.Categories);
            Description = dto.Description;
            Images = dto.Images != null ? new HashSet<string>(dto.Images) : new HashSet<string>();
            Price = dto.Price;
        }

        
        private bool isValidCategories(HashSet<string> categoryName)
        {
            foreach (var item in categoryName)
            {                
                if (item.Any(char.IsDigit))
                {
                    return false;
                }
            }
            return true;
        }
        private bool isValidImages(HashSet<string> imagesUrls)
        {            
            if (imagesUrls.Count == 0)
            {
                return true;
            }

            foreach (var url in imagesUrls)
            {
                var request = HttpWebRequest.Create(url);
                string contentType = "";

                if (request != null)
                {					
					try
					{
						var response = request.GetResponse();
						if (response != null)
						{
							contentType = response.ContentType;
						}
					}
					catch (System.Exception)
					{
						throw new CannotValidateException($@"image url {url}, please check if it is an valid image url");
					}        
                }

                if (contentType.Contains("image"))
                {
                    continue;
                }
                else
                {
                    return false;
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
        
        public override bool Equals(object obj)
        {
            var product = (Product)obj;
            return this.Name == product.Name;
        }
        public override int GetHashCode()
        {
            return name.GetHashCode();
        }
    }
}