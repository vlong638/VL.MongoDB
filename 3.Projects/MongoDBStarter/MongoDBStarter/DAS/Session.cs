using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBStarter
{
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

        static Session()
        {
            Connect();
        }

        #region CommonMethods
        public static void Connect()
        {
            _client = new MongoClient("mongodb://localhost");//参数可以指定不同的服务
            _database = _client.GetDatabase("test");//test为服务端默认创建的数据库

        }
        public static void ConnectRemote()
        {
            _client = new MongoClient("mongodb://10.12.8.197");//参数可以指定不同的服务
            _database = _client.GetDatabase("test");//test为服务端默认创建的数据库

        }
        public static IMongoCollection<BsonDocument> GetBsonCollection(string collectionName, MongoCollectionSettings settings = null)
        {
            return Database.GetCollection<BsonDocument>(collectionName, settings);
        }
        #endregion
    }
}
