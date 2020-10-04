using System.Net;
using FakeOmmerce.Errors;
using FakeOmmerce.Models;
using MongoDB.Bson;
using Newtonsoft.Json;
using Xunit;

namespace FakeOmmerceTests.Domain
{
    public class MongoEntityTestShould
    {         
        private readonly string STR_ID = "5f7a092a4a88b9e4418cddc6";

        [Fact]
        public void ValidateStringIdAndParseToObjId()
        {
                    
            MongoEntity sut = new MongoEntity(STR_ID);

            var act = sut.InternalId;
            var expect = ObjectId.Parse(STR_ID);

            Assert.Equal(STR_ID, act.ToString());
            Assert.Equal(STR_ID, sut.Id);
            Assert.Equal(expect, act);
        }

        [Fact]
        public void ValidateSetStringIdAndParseToObjId()
        {
            MongoEntity sut = new MongoEntity();
            sut.Id = STR_ID;

            var act = sut.InternalId;
            var expect = ObjectId.Parse(STR_ID);

            Assert.Equal(STR_ID, act.ToString());
            Assert.Equal(STR_ID, sut.Id);
            Assert.Equal(expect, act);            
        }

        [Fact]
        public void ShouldThrowBadRequestExceptionIfStringIdIsNotAnValidObjId()
        {
            MongoEntity sut = new MongoEntity();

            var act = Assert.Throws<BadRequestException>(() => sut.Id = "AnyId");
            
            var expect = new {
                statusCode = HttpStatusCode.BadRequest,
                message = "BadRequest id: AnyId is not valid"
            };            

            var obj1Str = JsonConvert.SerializeObject(expect);
            var obj2Str = JsonConvert.SerializeObject(act.HttpErrorResponse); 

            Assert.Equal("BadRequest id: AnyId is not valid", act.Message);
            Assert.Equal(obj1Str, obj2Str);
        }

        [Fact]
        public void TrueIfValidObjIdIsPassed()
        {                      
            Assert.True(MongoEntity.IsValidObjId(STR_ID));
        }

        [Fact]
        public void FlaseIfValidObjIdIsPassed()
        {                      
            Assert.False(MongoEntity.IsValidObjId("AnyId"));
        }

        [Fact]
        public void ConvertObjIdIfIsValid()
        {
            var act = MongoEntity.ValidateAndParseObjId(STR_ID);

            Assert.IsType<ObjectId>(act);
        }

        [Fact]
        public void ThrowErrorAndNotConvertIfObjIdIsNotValid()
        {
            var act = Assert.Throws<BadRequestException>(() => MongoEntity.ValidateAndParseObjId("AnyId"));

            var expect = new {
                statusCode = HttpStatusCode.BadRequest,
                message = "BadRequest id: AnyId is not valid"
            };            

            var obj1Str = JsonConvert.SerializeObject(expect);
            var obj2Str = JsonConvert.SerializeObject(act.HttpErrorResponse); 

            Assert.Equal("BadRequest id: AnyId is not valid", act.Message);
            Assert.Equal(obj1Str, obj2Str);
        }
    }
}