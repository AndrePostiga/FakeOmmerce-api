namespace FakeOmmerce.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Net;
    using FakeOmmerce.Errors;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Product
    {

        private ObjectId id;
        private string name;
        private HashSet<string> images;
        private HashSet<string> categories;
        private double price;
        private string brand;
        private string description;

        public ObjectId Id { get => id; set => id = value; }

		
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

        public Product()
        { }

        public Product(ObjectId id, string name, HashSet<string> images, HashSet<string> categories, double price, string brand, string description)
        {
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
        private bool isValidCategories(HashSet<string> categoryName)
        {
            foreach (var item in categoryName)
            {
                System.Console.WriteLine(item);
                if (item.Any(char.IsDigit))
                {
                    return false;
                }
            }
            return true;
        }
        private bool isValidImages(HashSet<string> imagesUrls)
        {
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
    }
}