namespace FakeOmmerce.Models
{
    using System.Collections.Generic;
    using MongoDB.Driver;
    using System.Linq;

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
                Builders<Product>.Filter.Where(x => x.Name.ToLower().Contains(_name))
                & Builders<Product>.Filter.Gte(x => x.Price, _priceGreatherThen)
                & Builders<Product>.Filter.Lte(x => x.Price, _priceLowerThen)
                & Builders<Product>.Filter.Where(x => x.Brand.ToLower().Contains(_brand));

            if (_categories.Count > 0)
            {
                filter &= Builders<Product>.Filter.AnyIn(x => x.Categories, _categories);
            }                
 
            return filter;
        }
    }
  }