using System;
using System.Collections.Generic;
using System.Net;
using FakeOmmerce.Errors;
using FakeOmmerce.Models;
using MongoDB.Bson;
using Newtonsoft.Json;
using Xunit;

namespace FakeOmmerceTests.Domain
{
    public class ProductTestShould
    {
        [Fact]
        public void ThrowBadRequestErrorIfPriceIsLowerThanZero()
        {
            Product sut = new Product();            
            
            var act = Assert.Throws<BadRequestException>(() => sut.Price = -10);
            var expect = new {
                statusCode = HttpStatusCode.BadRequest,
                message = "BadRequest price is not valid"
            };            

            var obj1Str = JsonConvert.SerializeObject(expect);
            var obj2Str = JsonConvert.SerializeObject(act.HttpErrorResponse); 

            Assert.Equal("BadRequest price is not valid", act.Message);
            Assert.Equal(obj1Str, obj2Str);
        }

        [Fact]
        public void SetCorrectPriceIfPriceIsZeroOrGreather()
        {
            Product sut = new Product();                        
            sut.Price = 10;

            var act = sut.Price;
            
            Assert.Equal(act, 10);
        }

        [Fact]
        public void CaptalizeNameString()
        {
            Product sut = new Product();
            sut.Name = "any name";

            var act = sut.Name;

            Assert.Equal(act, "Any Name");
        }

        [Fact]
        public void SetCorrectDescription()
        {
            Product sut = new Product();
            sut.Description = "any desc";

            var act = sut.Description;

            Assert.Equal(act, "any desc");
        }

        [Fact]
        public void CaptalizeBrandString()
        {
            Product sut = new Product();
            sut.Brand = "any brand";

            var act = sut.Brand;

            Assert.Equal(act, "Any Brand");
        }


        [Fact]
        public void ValidateImagesUrl()
        {
            Product sut = new Product();
            sut.Images = new HashSet<string>() {
                "https://static.netshoes.com.br/produtos/tenis-nike-revolution-5-masculino/09/HZM-1731-309/HZM-1731-309_zoom1.jpg?ts=1584658433&",
                "https://static.netshoes.com.br/produtos/tenis-nike-revolution-5-masculino/09/HZM-1731-309/HZM-1731-309_zoom2.jpg?ts=1584658433&",
                "https://static.netshoes.com.br/produtos/tenis-nike-revolution-5-masculino/09/HZM-1731-309/HZM-1731-309_zoom3.jpg?ts=1584658433&",
                "https://static.netshoes.com.br/produtos/tenis-nike-revolution-5-masculino/09/HZM-1731-309/HZM-1731-309_zoom4.jpg?ts=1584658433&"
            };

            var act = sut.Images;

            var expect = new HashSet<string>() {
                "https://static.netshoes.com.br/produtos/tenis-nike-revolution-5-masculino/09/HZM-1731-309/HZM-1731-309_zoom1.jpg?ts=1584658433&",
                "https://static.netshoes.com.br/produtos/tenis-nike-revolution-5-masculino/09/HZM-1731-309/HZM-1731-309_zoom2.jpg?ts=1584658433&",
                "https://static.netshoes.com.br/produtos/tenis-nike-revolution-5-masculino/09/HZM-1731-309/HZM-1731-309_zoom3.jpg?ts=1584658433&",
                "https://static.netshoes.com.br/produtos/tenis-nike-revolution-5-masculino/09/HZM-1731-309/HZM-1731-309_zoom4.jpg?ts=1584658433&"
            };

            Assert.Equal(expect, act);            
        }

        [Fact]
        public void ValidateImagesUrlIfIEnumerrableIsEmpty()
        {
            Product sut = new Product();
            sut.Images = new HashSet<string>() {};

            var act = sut.Images;

            var expect = new HashSet<string>() {};

            Assert.Equal(expect, act);
        }

        [Fact]
        public void ThrowsBadRequestExceptionIfSomeLinkIsNotAnImage()
        {
            Product sut = new Product();

            var act = Assert.Throws<BadRequestException>(() => sut.Images = new HashSet<string>() {
                "https://static.netshoes.com.br/produtos/tenis-nike-revolution-5-masculino/09/HZM-1731-309/HZM-1731-309_zoom1.jpg?ts=1584658433&",
                "https://static.netshoes.com.br/produtos/tenis-nike-revolution-5-masculino/09/HZM-1731-309/HZM-1731-309_zoom2.jpg?ts=1584658433&",
                "https://mundipagg.com/"
            });

            var expect = new {
                statusCode = HttpStatusCode.BadRequest,
                message = "BadRequest some image is not valid"
            };            

            var obj1Str = JsonConvert.SerializeObject(expect);
            var obj2Str = JsonConvert.SerializeObject(act.HttpErrorResponse); 

            Assert.Equal("BadRequest some image is not valid", act.Message);
            Assert.Equal(obj1Str, obj2Str);            
        }

        [Fact]
        public void ThrowsCannotValidateExceptionIfSomeLinkIsNotValid()
        {
            Product sut = new Product();

            var act = Assert.Throws<CannotValidateException>(() => sut.Images = new HashSet<string>() {
                "https://static.netshoes.com.br/produtos/tenis-nike-revolution-5-masculino/09/HZM-1731-309/HZM-1731-309_zoom1.jpg?ts=1584658433&",
                "https://static.netshoes.com.br/produtos/tenis-nike-revolution-5-masculino/09/HZM-1731-309/HZM-1731-309_zoom2.jpg?ts=1584658433&",
                "https://anylink.com.br/any_link"
            });

            var expect = new {
                statusCode = HttpStatusCode.BadRequest,
                message = "Cannot validate image url https://anylink.com.br/any_link, please check if it is an valid image url"
            }; 

            var obj1Str = JsonConvert.SerializeObject(expect);
            var obj2Str = JsonConvert.SerializeObject(act.HttpErrorResponse); 

            Assert.Equal("Cannot validate image url https://anylink.com.br/any_link, please check if it is an valid image url", act.Message);
            Assert.Equal(obj1Str, obj2Str);
        }

        [Fact]
        public void ThrowsCannotValidateExceptionIfSomeLinkIsNotALink()
        {
            Product sut = new Product();

            var act = Assert.Throws<CannotValidateException>(() => sut.Images = new HashSet<string>() {
                "https://static.netshoes.com.br/produtos/tenis-nike-revolution-5-masculino/09/HZM-1731-309/HZM-1731-309_zoom1.jpg?ts=1584658433&",
                "https://static.netshoes.com.br/produtos/tenis-nike-revolution-5-masculino/09/HZM-1731-309/HZM-1731-309_zoom2.jpg?ts=1584658433&",
                "Any string"
            });

            var expect = new {
                statusCode = HttpStatusCode.BadRequest,
                message = "Cannot validate image url Any string, please check if it is an valid image url"
            }; 

            var obj1Str = JsonConvert.SerializeObject(expect);
            var obj2Str = JsonConvert.SerializeObject(act.HttpErrorResponse); 

            Assert.Equal("Cannot validate image url Any string, please check if it is an valid image url", act.Message);
            Assert.Equal(obj1Str, obj2Str);
        }

        [Fact]
        public void CapitalizeAllCategories()
        {
            Product sut = new Product();
            sut.Categories = new HashSet<string>() {
                "any cat",
                "any category name",
                "any cat name"
            };

            var expect = new HashSet<string> () {
                "Any Cat",
                "Any Category Name",
                "Any Cat Name"
            };

            var act = sut.Categories;

            Assert.Equal(expect, act);
        }

        [Fact]
        public void ThrowsBadRequestExceptionIfSomeCategoryHasNumericDigit()
        {
            Product sut = new Product();
            var act = Assert.Throws<BadRequestException>(() => sut.Categories = new HashSet<string>() {
                "any cat",
                "any1 cat",
                "any cat",
            });

            var expect = new {
                statusCode = HttpStatusCode.BadRequest,
                message = "BadRequest some category is not valid"
            }; 

            var obj1Str = JsonConvert.SerializeObject(expect);
            var obj2Str = JsonConvert.SerializeObject(act.HttpErrorResponse); 

            Assert.Equal("BadRequest some category is not valid", act.Message);
            Assert.Equal(obj1Str, obj2Str);
        }        

        [Fact]
        public void TrueIfProductsHaveSameName()
        {
            Product sut = new Product() {
                Name = "Any Name"
            };

            Product act = new Product() {
                Name = "Any Name"
            };

            Assert.True(sut.Equals(act));
        }  

        [Fact]
        public void FalseIfProductsHaveSameName()
        {
            Product sut = new Product() {
                Name = "Any Name"
            };

            Product act = new Product() {
                Name = "Any Another Name"
            };

            Assert.False(sut.Equals(act));
        }     

        [Fact]
        public void SameHashCodeOfNameString()
        {
            Product sut = new Product() {
                Name = "Any Name"
            };

            var hash = sut.GetHashCode();

            Assert.Equal("Any Name".GetHashCode(), hash);
        }   

        [Fact]
        public void CreateProductInstance()
        {
            var objId = ObjectId.GenerateNewId();            

            Product sut = new Product(
                objId,
                "Any Name",
                new HashSet<string>() {"https://static.netshoes.com.br/produtos/tenis-nike-revolution-5-masculino/09/HZM-1731-309/HZM-1731-309_zoom1.jpg?ts=1584658433&"},
                new HashSet<string>() {"Any Category"},
                10,
                "Any Brand",
                "Any Description"
            );

            Assert.IsType<Product>(sut);
            Assert.IsAssignableFrom<MongoEntity>(sut);
            Assert.Equal(sut.InternalId, objId);
            Assert.Equal(sut.Name, "Any Name");
            Assert.Equal(sut.Images, new HashSet<string>() {"https://static.netshoes.com.br/produtos/tenis-nike-revolution-5-masculino/09/HZM-1731-309/HZM-1731-309_zoom1.jpg?ts=1584658433&"});
            Assert.Equal(sut.Categories, new HashSet<string>() {"Any Category"});
            Assert.Equal(sut.Price, 10);
            Assert.Equal(sut.Brand, "Any Brand");
            Assert.Equal(sut.Description, "Any Description");
        }

        [Fact]
        public void CreateProductInstanceByDto()
        {            
            ProductDTO dto = new ProductDTO() {
                Brand = "Any Brand",
                Categories = new List<string>() {"Any Category"},
                Description = "Any Description",
                Images = new List<string>() {"https://static.netshoes.com.br/produtos/tenis-nike-revolution-5-masculino/09/HZM-1731-309/HZM-1731-309_zoom1.jpg?ts=1584658433&"},
                Name = "Any Name",
                Price = 10
            };                      

            Product sut = new Product(dto);

            Assert.IsType<Product>(sut);      
            Assert.IsAssignableFrom<MongoEntity>(sut);      
            Assert.Equal(sut.Name, "Any Name");
            Assert.Equal(sut.Images, new HashSet<string>() {"https://static.netshoes.com.br/produtos/tenis-nike-revolution-5-masculino/09/HZM-1731-309/HZM-1731-309_zoom1.jpg?ts=1584658433&"});
            Assert.Equal(sut.Categories, new HashSet<string>() {"Any Category"});
            Assert.Equal(sut.Price, 10);
            Assert.Equal(sut.Brand, "Any Brand");
            Assert.Equal(sut.Description, "Any Description");
        }

        [Fact]
        public void CreateProductInstanceByDtoWithNullImages()
        {            
            ProductDTO dto = new ProductDTO() {
                Brand = "Any Brand",
                Categories = new List<string>() {"Any Category"},
                Description = "Any Description",
                Images = null,
                Name = "Any Name",
                Price = 10
            };                      

            Product sut = new Product(dto);

            Assert.IsType<Product>(sut); 
            Assert.IsAssignableFrom<MongoEntity>(sut);           
            Assert.Equal(sut.Name, "Any Name");
            Assert.Equal(sut.Images, new HashSet<string>());
            Assert.Equal(sut.Categories, new HashSet<string>() {"Any Category"});
            Assert.Equal(sut.Price, 10);
            Assert.Equal(sut.Brand, "Any Brand");
            Assert.Equal(sut.Description, "Any Description");
        }

        [Fact]
        public void CreateProductInstanceByDtoAndId()
        {    
            var objId = ObjectId.GenerateNewId();

            ProductDTO dto = new ProductDTO() {
                Brand = "Any Brand",
                Categories = new List<string>() {"Any Category"},
                Description = "Any Description",
                Images = new List<string>() {"https://static.netshoes.com.br/produtos/tenis-nike-revolution-5-masculino/09/HZM-1731-309/HZM-1731-309_zoom1.jpg?ts=1584658433&"},
                Name = "Any Name",
                Price = 10
            };                      

            Product sut = new Product(objId, dto);

            Assert.IsType<Product>(sut);
            Assert.IsAssignableFrom<MongoEntity>(sut);
            Assert.Equal(sut.InternalId, objId);
            Assert.Equal(sut.Name, "Any Name");
            Assert.Equal(sut.Images, new HashSet<string>() {"https://static.netshoes.com.br/produtos/tenis-nike-revolution-5-masculino/09/HZM-1731-309/HZM-1731-309_zoom1.jpg?ts=1584658433&"});
            Assert.Equal(sut.Categories, new HashSet<string>() {"Any Category"});
            Assert.Equal(sut.Price, 10);
            Assert.Equal(sut.Brand, "Any Brand");
            Assert.Equal(sut.Description, "Any Description");
        }

        [Fact]
        public void CreateProductInstanceByDtoAndIdWithNullImages()
        {    
            var objId = ObjectId.GenerateNewId();

            ProductDTO dto = new ProductDTO() {
                Brand = "Any Brand",
                Categories = new List<string>() {"Any Category"},
                Description = "Any Description",
                Images = new List<string>(),
                Name = "Any Name",
                Price = 10
            };                      

            Product sut = new Product(objId, dto);

            Assert.IsType<Product>(sut);
            Assert.IsAssignableFrom<MongoEntity>(sut);
            Assert.Equal(sut.InternalId, objId);
            Assert.Equal(sut.Name, "Any Name");
            Assert.Equal(sut.Images, new HashSet<string>());
            Assert.Equal(sut.Categories, new HashSet<string>() {"Any Category"});
            Assert.Equal(sut.Price, 10);
            Assert.Equal(sut.Brand, "Any Brand");
            Assert.Equal(sut.Description, "Any Description");
        }
    }    
}
