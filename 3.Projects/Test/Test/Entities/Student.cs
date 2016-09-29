using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Entities
{
    class Student
    {
        public ObjectId _id { set; get; }
        public string Name { set; get; }
        public int Age { set; get; }
        public List<MongoDBRef> Friends { get; set; } = new List<MongoDBRef>();
        public List<int> CommendPersons { get; set; } = new List<int>();
    }
}
