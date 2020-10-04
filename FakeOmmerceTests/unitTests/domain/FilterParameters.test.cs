using System.Collections.Generic;
using System.Net;
using FakeOmmerce.Errors;
using FakeOmmerce.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using Xunit;

namespace FakeOmmerceTests.Domain
{
    public class FilterParametersShould
    {   
        [Fact]
        public void CreateInstanceOfFilterParameters()
        {
                    
            FilterParameters sut = new FilterParameters(
                new List<string>() {"AnyCategory"},
                "AnyBrand",
                "AnyBrand",
                10,
                100
            );

            FilterParameters sut2 = new FilterParameters();

            Assert.IsType<FilterParameters>(sut);
            Assert.IsType<FilterParameters>(sut2);
        }

        [Fact]
        public void CreateFilters()
        {
            FilterParameters sut = new FilterParameters(
                new List<string>() {"AnyCategory"},
                "AnyBrand",
                "AnyName",
                10,
                100
            );     

            var act = sut.makeFilters();

            var expect = 
                Builders<Product>.Filter.Where(p => p.Name.ToLower().Contains("AnyName"))
                & Builders<Product>.Filter.Gte(p => p.Price, 10)
                & Builders<Product>.Filter.Lte(p => p.Price, 100)
                & Builders<Product>.Filter.Where(p => p.Brand.ToLower().Contains("AnyBrand"))
                & Builders<Product>.Filter.AnyIn(p => p.Categories, new List<string>() {"AnyCategory"});                
            
            Assert.Equal(expect.ToJson(), act.ToJson());
        }


    }
}