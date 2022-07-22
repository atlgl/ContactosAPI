using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace ContactosAPI.Models
{
    public class Contacto
    {
        [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfNull]
        public ObjectId id { get; set; }
        public String name { get; set; }
        public String phone { get; set; }
               
        public List<String> addressLines { get; set; }



    }
}
