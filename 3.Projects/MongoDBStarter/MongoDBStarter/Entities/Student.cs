using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace MongoDBStarter
{
    class Student
    {
        public ObjectId _id { set; get; }
        public string Name { set; get; }
        public int Age { set; get; }
        public List<MongoDBRef> Friends { get; set; } = new List<MongoDBRef>();
    }
}
