namespace FakeOmmerce.Models
{
    using System.Text.Json.Serialization;
    using FakeOmmerce.Errors;
    using MongoDB.Bson;

    public class MongoEntity
    {        
        private ObjectId internalId;
        private string id;

        [JsonIgnore]
        public ObjectId InternalId
        {
            get => internalId;
            set
            {
                internalId = value;
                id = value.ToString();
            }
        }

        public string Id
        {
            get => id;
            set
            {
                if (!MongoEntity.IsValidObjId(value))
                {
                    throw new BadRequestException($@"id: {value}");
                }
                InternalId = ObjectId.Parse(value);
                id = value;
            }
        }

        public MongoEntity(string id)
        {
            Id = id;
        }

        public MongoEntity(ObjectId id)
        {
            InternalId = id;
        }

        public MongoEntity()
        { }

        public static bool IsValidObjId(string id)
        {
            bool isValid = ObjectId.TryParse(id, out ObjectId objId);
            if (!isValid)
            {
                return false;
            }

            return true;
        }

        public static ObjectId ValidateAndParseObjId(string id)
        {
            if (!MongoEntity.IsValidObjId(id))
            {
                throw new BadRequestException($@"id: {id}");
            }

            return ObjectId.Parse(id);
        }
    }
}