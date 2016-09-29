using System;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Test.DAS
{
    public class SessionHelper
    {
        Session Session { set; get; }

        public SessionHelper(Session session)
        {
            Session = session;
        }

        int _uniqueId = -1;
        int UniqueId
        {
            set
            {
                if (_uniqueId == -1)
                {
                    _uniqueId = Session.FindMaxIndex();
                }
                _uniqueId++;
            }
            get
            {
                if (_uniqueId == -1)
                {
                    _uniqueId = Session.FindMaxIndex();
                }
                return _uniqueId;
            }
        }
        static object IdLocker = new object();
        public int GetNewId()
        {
            lock(IdLocker)
            {
                return UniqueId++;
            }
        }
    }
    public class Session
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;
        public static IMongoDatabase Database
        {
            get
            {
                return _database;
            }
        }

        public Session()
        {
        }

        #region CommonMethods
        public void Connect()
        {
            //_client = new MongoClient("mongodb://localhost");//参数可以指定不同的服务

            MongoClientSettings settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress("localhost");
            settings.Credentials = new[] { MongoCredential.CreateCredential("MsgBoard", "test", "pw_test") };
            _client = new MongoClient(settings);//参数可以指定不同的服务
            _database = _client.GetDatabase("MsgBoard");//test为服务端默认创建的数据库

        }
        public void ConnectRemote()
        {
            _client = new MongoClient("mongodb://10.12.8.197");//参数可以指定不同的服务
            _database = _client.GetDatabase("test");//test为服务端默认创建的数据库

        }
        public static IMongoCollection<BsonDocument> GetBsonCollection(string collectionName, MongoCollectionSettings settings = null)
        {
            return Database.GetCollection<BsonDocument>(collectionName, settings);
        }
        

        //public async Task<BsonDocument> FindOne(string collectionName, FilterDefinition<BsonDocument> filter)
        //{
        //    var collection = _database.GetCollection<BsonDocument>(collectionName);
        //    return await collection.Find(filter).FirstOrDefaultAsync();
        //}
        //public async Task<IEnumerable<BsonDocument>> FindList(string collectionName, FilterDefinition<BsonDocument> filter, SortDefinition<BsonDocument> sort = null, int limit = 0)
        //{
        //    var collection = _database.GetCollection<BsonDocument>(collectionName);
        //    var result = collection.Find(filter);
        //    if (sort != null)
        //    {
        //        result = result.Sort(sort);
        //    }
        //    if (limit > 0)
        //    {
        //        result = result.Limit(limit); ;
        //    }
        //    return await result.ToListAsync();
        //}
        //public async Task Insert(string collectionName, BsonDocument document)
        //{
        //    var collection = _database.GetCollection<BsonDocument>(collectionName);
        //    await collection.InsertOneAsync(document);
        //}
        //public async Task<UpdateResult> Update(string collectionName, FilterDefinition<BsonDocument> filter,UpdateDefinition<BsonDocument> update,UpdateOptions updateOptions = null)
        //{
        //    var collection = _database.GetCollection<BsonDocument>(collectionName);
        //    var result =await collection.UpdateOneAsync(filter, update, updateOptions);
        //    return result;
        //}
        #endregion
        #region Template
        public async void InsertTempObject()
        {
            var document = new BsonDocument
            {
                { "address",new BsonDocument
                    {
                        {"street","萍水西街" },
                        {"zipCode","10075" },
                        {"building","萍水西街" },
                        {"coord",new BsonArray { 73.95,40.77} }
                    }
                },
                { "borough","Manhattan"},
                { "cuisine","Italian"},
                { "grades",new BsonArray
                    {
                        new BsonDocument
                        {
                            { "date",new DateTime(2015,8,20,13,36,0,DateTimeKind.Utc)},
                            { "grade","A"},
                            { "score",9}
                        },
                        new BsonDocument
                        {
                            { "date",new DateTime(2015,8,21,13,36,0,DateTimeKind.Utc)},
                            { "grade","B"},
                            { "score",7}
                        }
                    }
                },
                { "name","Vella"},
                { "restaurant_id","41704620"}//xx_id字段如果未被设置,系统会默认增加一个对象Id
            };
            var collection = _database.GetCollection<BsonDocument>("restaurants");
            await collection.InsertOneAsync(document);
        } 
        public async void MultiInsert(int id)
        {
            var document = new BsonDocument
            {
                { "address",new BsonDocument
                    {
                        {"street","萍水西街"+id },
                        {"zipCode","10075" },
                        {"building","萍水西街" },
                        {"coord",new BsonArray { 73.95,40.77} }
                    }
                },
                { "borough","Manhattan"},
                { "cuisine","Italian"},
                { "grades",new BsonArray
                    {
                        new BsonDocument
                        {
                            { "date",new DateTime(2015,8,20,13,36,0,DateTimeKind.Utc)},
                            { "grade","A"},
                            { "score",9}
                        },
                        new BsonDocument
                        {
                            { "date",new DateTime(2015,8,21,13,36,0,DateTimeKind.Utc)},
                            { "grade","B"},
                            { "score",7}
                        }
                    }
                },
                { "name","Vella"+id},
                { "restaurant_id",id}//xx_id字段如果未被设置,系统会默认增加一个对象Id
            };
            var collection = _database.GetCollection<BsonDocument>("restaurants");
            await collection.InsertOneAsync(document);
        }

        public async void FindEach()
        {
            var collection = _database.GetCollection<BsonDocument>("restaurants");
            var filter = new BsonDocument();
            var count = 0;
            using (var cursor = await collection.FindAsync(filter))
            {
                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    foreach (var document in batch)
                    {
                        //process document
                        count++;
                    }
                }
            }

            Console.ReadLine();
        }

        public async void Find()
        {
            var collection = _database.GetCollection<BsonDocument>("restaurants");
            var filter = Builders<BsonDocument>.Filter.Eq("borough", "Manhattan");
            var result = await collection.Find(filter).ToListAsync();

            Console.ReadLine();
        }
        public int FindMaxIndex()
        {
            var collection = _database.GetCollection<BsonDocument>("restaurants");
            var filter = new BsonDocument();
            var results = collection.Find(filter);

            var sort = Builders<BsonDocument>.Sort.Descending("restaurant_id");
            var result = collection.Find(filter).Sort(sort).Limit(1);
            BsonDocument document = result.ToBsonDocument();
            BsonElement element = document.GetElement("restaurant_id");
            int value = Convert.ToInt32(element.Value);
            return value;
        }

        //public async void Push()
        //{
        //    var collection = _database.GetCollection<BsonDocument>("restaurants");

        //}
        #endregion
    }
}
