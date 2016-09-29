using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Utilities
{
    public static class Tools
    {
        public static FilterDefinitionBuilder<BsonDocument> FilterBuilder { get { return Builders<BsonDocument>.Filter; } }
        public static SortDefinitionBuilder<BsonDocument> SortBuilder { get { return Builders<BsonDocument>.Sort; } }
        public static UpdateDefinitionBuilder<BsonDocument> UpdateBuilder { get { return Builders<BsonDocument>.Update; } }
    }
}
