using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace MongoDBStarter
{
    class Business
    {
        public async void test()
        {
            await Insert();
            await Update();
            await Retrieve();
            await Delete();
        }
        public async Task Insert()
        {
            Student student = new Student();
            student.Name = "张三";
            student.Age = 21;
            var collection = Session.GetBsonCollection(nameof(Student));
            await collection.InsertOneAsync(student.ToBsonDocument());
        }
        public async Task Update()
        {
            var filterBuilder = Builders<BsonDocument>.Filter;
            var filter = filterBuilder.Eq(nameof(Student.Name), "张三");
            var updateBuilder = Builders<BsonDocument>.Update;
            var update = updateBuilder.Set(nameof(Student.Age), 22);
            var collection = Session.GetBsonCollection(nameof(Student));
            await collection.UpdateOneAsync(filter, update);
        }
        public async Task Retrieve()
        {
            var filterBuilder = Builders<BsonDocument>.Filter;
            var filter = filterBuilder.Eq(nameof(Student.Name), "张三");
            var collection = Session.GetBsonCollection(nameof(Student));
            var bsonDocument = await collection.Find(filter).FirstAsync();
            Student zhangSan = BsonSerializer.Deserialize<Student>(bsonDocument);
        }
        public async Task Delete()
        {
            var filterBuilder = Builders<BsonDocument>.Filter;
            var filter = filterBuilder.Eq(nameof(Student.Name), "张三");
            var collection = Session.GetBsonCollection(nameof(Student));
            var bsonDocument = await collection.DeleteManyAsync(filter);
        }
    }
}
