using System.Collections.Generic;
using FakeOmmerce.Models;
using Xunit;

namespace FakeOmmerceTests.Domain
{
    public class ProductDTOShould
    {   
        private readonly string STR_ID = "5f7a092a4a88b9e4418cddc6";

        [Fact]
        public void CreateInstanceOfProductDTO()
        {            
                    
            ProductDTO sut = new ProductDTO();

            sut.Id = STR_ID;
            sut.Brand = "Any Brand";
            sut.Categories = new List<string>() {"Any Category"};
            sut.Description = "Any Description";
            sut.Images = null;
            sut.Name = "Any Name";
            sut.Price = 10;

            Assert.IsType<ProductDTO>(sut);
            Assert.Equal(sut.Id, STR_ID);
            Assert.Equal(sut.Brand, "Any Brand");
            Assert.Equal(sut.Categories, new List<string>() {"Any Category"});
            Assert.Equal(sut.Description, "Any Description");
            Assert.Equal(sut.Images, null);
            Assert.Equal(sut.Name, "Any Name");
            Assert.Equal(sut.Price, 10);
        }
    }
}