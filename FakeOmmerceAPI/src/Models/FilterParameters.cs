namespace FakeOmmerce.Models
{
    using System.Collections.Generic;
    using MongoDB.Driver;
    using System.Linq;
    using MongoDB.Bson;

    public class FilterParameters
    {
        private List<string> _categories;
        private double _priceGreatherThen;
        private double _priceLowerThen;
        private string _brand;
        private string _name;

        public FilterParameters(List<string> categories, string brand, string name, double priceGreatherThen, double priceLowerThen)
        {
            _categories = categories;
            _priceGreatherThen = priceGreatherThen;
            _priceLowerThen = priceLowerThen;
            _brand = brand;
            _name = name;
        }

        public FilterParameters()
        { }

        public FilterDefinition<Product> makeFilters() {           

            var filter = 
                Builders<Product>.Filter.Where(p => p.Name.ToLower().Contains(_name))
                & Builders<Product>.Filter.Gte(p => p.Price, _priceGreatherThen)
                & Builders<Product>.Filter.Lte(p => p.Price, _priceLowerThen)
                & Builders<Product>.Filter.Where(p => p.Brand.ToLower().Contains(_brand));

            if (_categories.Count > 0)
            {
                filter &= Builders<Product>.Filter.AnyIn(p => p.Categories, _categories);                       
            }                
 
            return filter;
        }
    }
  }