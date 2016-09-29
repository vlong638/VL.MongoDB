using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Test.BLL;
using Test.Constants.Enums;
using Test.DAS;
using Test.Entities;

namespace Test
{
    class Tester
    {
        Business Business { set; get; }

        public Tester()
        {
            var id = new ObjectId();
            var idString = id.ToString();

            Session session = new Session();
            session.Connect();
            //session.ConnectRemote();
            Business = new Business(session);
        }


        #region TestMethods
        public async void test()
        {
            await Insert();
            //await Update();
            //await Retrieve();
            //await Delete();

            //var issue = DateTime.Now.ToString("HHmm");
            //Test.Utilities.Logger.WriteLog("==========={0}期执行开始===========", issue);
            //#region 创建多个评论主体时 ObjectId的赋值
            //////创建足球比赛的评论主体
            ////await Business.CreateCommentBody(sourceType, sourceId);
            //////创建足球比赛的评论主体
            ////await Business.CreateCommentBody(sourceType, sourceId);
            /////// 结论
            /////// 无需赋值,数据库自动产生一个ObjectId
            //#endregion

            ////await SortLimitTest();

            ////常规测试 基础操作
            ////await SingleTest();
            ////量化测试
            ////await MultiTest();

            ////for (int i = 0; i < 100; i++)
            ////{
            ////    await MultiBodyTest();
            ////}

            ////测试Collection内Array的新增效率
            //var bodyId = await SingleCommentBodyCreateTest();
            //await Business.UpdateCommentBodyControlState(bodyId, Constants.Enums.ControlState.Available);
            //var detailId = await SingleCommentDetailCreateTest(bodyId, 1);
            //await SingleCommendCreateTest(detailId, 1);
            //await SingleCommendCreateTest(detailId, 2);
            //await SingleCommendCreateTest(detailId, 3);
            //await MultiCommendCreateTest(detailId, 4, 10000);
            //await SingleCommendCreateTest(detailId, 10001);
            //await SingleCommendCreateTest(detailId, 10002);
            //await SingleCommendCreateTest(detailId, 10003);

            ////直接进行点赞
            //await SingleCommendCreateTest(new ObjectId("55dd7463db9e7f38dc29ff2f"), 1);

            /////测试初次本机获取20条记录时间
            //var bodyId = new ObjectId("55dd7462db9e7f38dc29ff1d");
            //var commentDetails1 = await GetCommentDetailListTest(bodyId);
            /////测试后续本机获取20条记录时间
            //var commentDetails2 = await GetCommentDetailListTest(bodyId);

            #region GetCommentDetailListTest
            //55dd74abdb9e7f27f0958890
            //55dd749fdb9e7f38dc2a25ce
            //55dd7475db9e7f38dc2a25cd
            //55dd7475db9e7f38dc2a25cc
            //55dd7475db9e7f38dc2a25cb
            //55dd7475db9e7f38dc2a25ca
            //55dd7475db9e7f38dc2a25c9
            //55dd7475db9e7f38dc2a25c8
            //55dd7475db9e7f38dc2a25c7 !
            //55dd7475db9e7f38dc2a25c6
            //55dd7475db9e7f38dc2a25c5
            //55dd7475db9e7f38dc2a25c4
            //55dd7475db9e7f38dc2a25c3
            //55dd7475db9e7f38dc2a25c2
            //55dd7475db9e7f38dc2a25c1
            //55dd7475db9e7f38dc2a25c0
            //55dd7475db9e7f38dc2a25bf
            //55dd7475db9e7f38dc2a25be
            //55dd7475db9e7f38dc2a25bd
            //55dd7475db9e7f38dc2a25bc 
            #endregion

            #region GetCommentDetailListAfterCertainCommentDetailTest结果
            //55dd7475db9e7f38dc2a25c6
            //55dd7475db9e7f38dc2a25c5
            //55dd7475db9e7f38dc2a25c4
            //55dd7475db9e7f38dc2a25c3
            //55dd7475db9e7f38dc2a25c2
            //55dd7475db9e7f38dc2a25c1
            //55dd7475db9e7f38dc2a25c0
            //55dd7475db9e7f38dc2a25bf
            //55dd7475db9e7f38dc2a25be
            //55dd7475db9e7f38dc2a25bd
            //55dd7475db9e7f38dc2a25bc
            //55dd7475db9e7f38dc2a25bb
            //55dd7475db9e7f38dc2a25ba
            //55dd7475db9e7f38dc2a25b9
            //55dd7475db9e7f38dc2a25b8
            //55dd7475db9e7f38dc2a25b7
            //55dd7475db9e7f38dc2a25b6
            //55dd7475db9e7f38dc2a25b5
            //55dd7475db9e7f38dc2a25b4
            //55dd7475db9e7f38dc2a25b3 
            #endregion

            /////测试初次本机获取20条后续记录时间
            //var detailId = new ObjectId("55dd7475db9e7f38dc2a25c7");
            //var commentDetails3 = await GetCommentDetailListAfterCertainCommentDetailTest(bodyId, detailId);
            /////测试后续本机获取20条后续记录时间
            //var commentDetails4 = await GetCommentDetailListAfterCertainCommentDetailTest(bodyId, detailId);
            //string ids = "";
            //foreach (var commentDetail in commentDetails3)
            //{
            //    ids += commentDetail._id.ToString() + "\n";
            //}

            //var commentBodyIds = await MultiTest(1, 1, 1, 0, 100);
            //await SingleCommentDetailCreateTest(commentBodyIds[0], 10000);

            //string s = "";
            //while (true)
            //{
            //    if (s == "s")
            //        break;
            //    var commentBodyIds = await MultiTest(1, 1, 1, 0, 100);
            //    await SingleCommentTest(commentBodyIds[0], 10000);
            //}
            //Test.Utilities.Logger.WriteLog("==========={0}期执行结束===========", issue);
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
            var result = await collection.UpdateOneAsync(filter, update);
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

        private async Task MultiBodyTest()
        {
            //单条数据创建测试 首次
            await SingleCommentBodyCreateTest();
            //单条数据创建测试 后续
            await SingleCommentBodyCreateTest();
            //获取一个主体Id
            var bodyId = await SingleCommentBodyCreateTest();
            //评论开关
            await Business.UpdateCommentBodyControlState(bodyId, Constants.Enums.ControlState.Available);
            //单次评论
            await SingleCommentDetailCreateTest(bodyId, 1);
            //单次评论
            await SingleCommentDetailCreateTest(bodyId, 2);
            //多次评论
            await MultiCommentDetailCreateTest(bodyId, 3, 10000);
            //单次评论
            var detailId = await SingleCommentDetailCreateTest(bodyId, 10001);
            //单次评论
            await SingleCommentDetailCreateTest(bodyId, 10002);
            //单次点赞
            await SingleCommendCreateTest(detailId, 1);
            //单次点赞
            await SingleCommendCreateTest(detailId, 2);
            //单次点赞
            await SingleCommendCreateTest(detailId, 3);
            //多次点赞
            await MultiCommendCreateTest(detailId, 4, 10000);
            //单次点赞
            await SingleCommendCreateTest(detailId, 10001);
            //单次点赞
            await SingleCommendCreateTest(detailId, 10002);
            //单次点赞
            await SingleCommendCreateTest(detailId, 10003);
            ///获取20条最新记录时间 初次 远程 无索引
            var commentDetails1 = await GetCommentDetailListTest(bodyId);
            ///获取20条最新记录时间 后续 远程 无索引
            var commentDetails2 = await GetCommentDetailListTest(bodyId);
            string ids1 = "";
            foreach (var commentDetail in commentDetails1)
            {
                ids1 += commentDetail._id.ToString() + "\n";
            }
            ///获取20条后续记录时间 初次 远程 无索引
            var afterDetailId = commentDetails2[8]._id;
            var commentDetails3 = await GetCommentDetailListAfterCertainCommentDetailTest(bodyId, afterDetailId);
            ///获取20条后续记录时间 后续 远程 无索引
            var commentDetails4 = await GetCommentDetailListAfterCertainCommentDetailTest(bodyId, afterDetailId);
            string ids2 = "";
            foreach (var commentDetail in commentDetails3)
            {
                ids2 += commentDetail._id.ToString() + "\n";
            }
        }

        private async Task<List<CommentDetail>> GetCommentDetailListAfterCertainCommentDetailTest(ObjectId bodyId,ObjectId detailId)
        {
            DateTime start = DateTime.Now;
            var commentDetails = await Business.GetFollowingCommentDetailsAfterCertainCommentDetail(bodyId, detailId, 20);
            DateTime end = DateTime.Now;
            TimeSpan span = end - start;
            Test.Utilities.Logger.WriteLog("获取后续评论详情列表-执行结束,耗费时间{0}s{1}ms", span.Seconds, span.Milliseconds);
            var commentDetailsCount = await Business.GetCommentDetailsCount(bodyId);
            Test.Utilities.Logger.WriteLog("共计{0}次评论", commentDetailsCount);
            Test.Utilities.Logger.WriteLog("");
            return commentDetails;
        }
        private async Task<List<CommentDetail>> GetCommentDetailListTest(ObjectId bodyId)
        {
            DateTime start = DateTime.Now;
            var commentDetails = await Business.GetLatestCommentDetails(bodyId, 20);
            DateTime end = DateTime.Now;
            TimeSpan span = end - start;
            Test.Utilities.Logger.WriteLog("获取最新评论详情列表-执行结束,耗费时间{0}s{1}ms", span.Seconds, span.Milliseconds);
            var commentDetailsCount = await Business.GetCommentDetailsCount(bodyId);
            Test.Utilities.Logger.WriteLog("共计{0}次评论", commentDetailsCount);
            Test.Utilities.Logger.WriteLog("");
            return commentDetails;
        }

        private async Task<ObjectId> SingleCommentBodyCreateTest()
        {
            var commentBodyId = ObjectId.GenerateNewId();
            var sourceType = SourceType.FootballMatch;
            var sourceId = 1;

            DateTime start = DateTime.Now;
            await Business.CreateCommentBody(commentBodyId, sourceType, sourceId);
            DateTime end = DateTime.Now;
            TimeSpan span = end - start;
            Test.Utilities.Logger.WriteLog("单次评论主体-执行结束,耗费时间{0}s{1}ms", span.Seconds, span.Milliseconds);
            Test.Utilities.Logger.WriteLog("");
            return commentBodyId;
        }
        private async Task<ObjectId> SingleCommentDetailCreateTest(ObjectId bodyId, long userId)
        {
            var detailId = ObjectId.GenerateNewId();

            DateTime start = DateTime.Now;
            await Business.CreateCommentDetail(bodyId, detailId, userId, "我是一条评论-10000");
            DateTime end = DateTime.Now;
            TimeSpan span = end - start;
            Test.Utilities.Logger.WriteLog("单条评论详情-执行结束,耗费时间{0}s{1}ms", span.Seconds, span.Milliseconds);
            var commentDetailsCount = await Business.GetCommentDetailsCount(bodyId);
            Test.Utilities.Logger.WriteLog("共计{0}次评论", commentDetailsCount);
            Test.Utilities.Logger.WriteLog("");
            return detailId;
        }
        private async Task MultiCommentDetailCreateTest(ObjectId bodyId, long minUserId = 1, long maxUserId = 10000)
        {
            DateTime start = DateTime.Now;
            for (long i = minUserId; i <= maxUserId; i++)
            {
                var detailId = ObjectId.GenerateNewId();
                await Business.CreateCommentDetail(bodyId, detailId, i, string.Format("我是一条来自{0}评论", i));
            }
            DateTime end = DateTime.Now;
            TimeSpan span = end - start;
            Test.Utilities.Logger.WriteLog("多条评论详情-执行结束,耗费时间{0}s{1}ms", span.Seconds, span.Milliseconds);
            var commentDetailsCount = await Business.GetCommentDetailsCount(bodyId);
            Test.Utilities.Logger.WriteLog("共计{0}次评论", commentDetailsCount);
            Test.Utilities.Logger.WriteLog("");
        }
        private async Task SingleCommendCreateTest(ObjectId detailId, long userId = 10000)
        {
            DateTime start = DateTime.Now;
            await Business.OptIn(detailId, userId);
            DateTime end = DateTime.Now;
            TimeSpan span = end - start;
            Test.Utilities.Logger.WriteLog("单次点赞-执行结束,耗费时间{0}s{1}ms", span.Seconds, span.Milliseconds);
            var commentDetail = await Business.GetCommentDetailById(detailId);
            Test.Utilities.Logger.WriteLog("共计{0}次点赞", commentDetail.CommendUserIdList.Count());
            Test.Utilities.Logger.WriteLog("");
        }
        private async Task MultiCommendCreateTest(ObjectId detailId, long minUserId = 1, long maxUserId = 10000)
        {
            DateTime start = DateTime.Now;
            for (long i = minUserId; i <= maxUserId; i++)
            {
                await Business.OptIn(detailId, i);
            }
            DateTime end = DateTime.Now;
            TimeSpan span = end - start;
            Test.Utilities.Logger.WriteLog("多次点赞-执行结束,耗费时间{0}s{1}ms", span.Seconds, span.Milliseconds);
            var commentDetail = await Business.GetCommentDetailById(detailId);
            Test.Utilities.Logger.WriteLog("共计{0}次点赞", commentDetail.CommendUserIdList.Count());
            Test.Utilities.Logger.WriteLog("");
        }
        private async Task SortAndLimitTest()
        {
            var sourceType = SourceType.FootballMatch;
            var sourceId = 1;
            //获取刚才创建的评论主体
            CommentBody body = await Business.GetCommentBodyId(sourceType, sourceId);
            //获取评论列表
            var details = await Business.GetLatestCommentDetails(body._id, 2);
        }
        private async Task SingleTest()
        {
            var sourceType = SourceType.FootballMatch;
            var sourceId = 1;
            var userId1 = 1;
            var userId2 = 2;
            var userId3 = 2;
            //创建足球比赛的评论主体
            await Business.CreateCommentBody(sourceType, sourceId);
            //获取刚才创建的评论主体
            CommentBody body = await Business.GetCommentBodyId(sourceType, sourceId);
            //开启评论功能
            await Business.UpdateCommentBodyControlState(body._id, Constants.Enums.ControlState.Available);
            //检查评论开关
            ControlState controlState = await Business.GetCommentBodyControlState(body._id);
            //评论
            await Business.Comment(body._id, userId1, "我是一条评论");
            await Business.Comment(body._id, userId2, "我还是一条评论");
            await Business.Comment(body._id, userId3, "我又是一条评论");
            //获取评论列表
            var details = await Business.GetLatestCommentDetails(body._id, 2);
            //点赞
            foreach (var detail in details)
            {
                await Business.OptIn(detail._id, userId1);
                await Business.OptIn(detail._id, userId2);
            }
        }
        private async Task<List<ObjectId>> MultiTest(int matchCount = 1, long userminId = 1, long userMaxId = 10000, int commentFrequence = 60, int commendFrequence = 60)
        {
            //统计用信息
            DateTime start = DateTime.Now;
            int matchCreatedCount = 0;
            int commentCreatedCount = 0;
            int commendCreatedCount = 0;

            var sourceType = SourceType.FootballMatch;
            //已创建的Body的Id列表
            List<ObjectId> commentBodyIds = new List<ObjectId>();
            //创建足球比赛的评论主体
            for (int i = 0; i < matchCount; i++)
            {
                var commentBodyId = ObjectId.GenerateNewId();
                await Business.CreateCommentBody(commentBodyId, sourceType, i);
                commentBodyIds.Add(commentBodyId);
                matchCreatedCount++;
            }
            foreach (var commentBodyId in commentBodyIds)
            {
                //开启评论功能
                await Business.UpdateCommentBodyControlState(commentBodyId, Constants.Enums.ControlState.Available);
                //检查评论开关
                ControlState controlState = await Business.GetCommentBodyControlState(commentBodyId);
                //评论
                for (long j = userminId; j <= userMaxId; j++)
                {
                    if (GetRandomNumber(100) > commentFrequence)
                    {
                        await Business.Comment(commentBodyId, j, string.Format("我是一条来自{0}评论", j));
                        commentCreatedCount++;
                    }
                }
                //获取评论列表
                var details = await Business.GetLatestCommentDetails(commentBodyId, 20);
                //点赞
                foreach (var detail in details)
                {
                    for (long j = userminId; j <= userMaxId; j++)
                    {
                        if (GetRandomNumber(100) > commendFrequence)
                        {
                            await Business.OptIn(detail._id, j);
                            commendCreatedCount++;
                        }
                    }
                }
            }
            DateTime end = DateTime.Now;
            TimeSpan span = end - start;
            Test.Utilities.Logger.WriteLog("MultiTest执行结束,耗费时间{0}s{1}ms", span.Seconds, span.Milliseconds);
            Test.Utilities.Logger.WriteLog("共计{0}场赛事评论,{1}条评论,{2}次点赞", matchCreatedCount, commentCreatedCount, commendCreatedCount);
            Test.Utilities.Logger.WriteLog("");
            return commentBodyIds;
        }
        #endregion
        #region Utilities
        private static readonly ThreadLocal<Random> appRandom = new ThreadLocal<Random>(() => new Random());
        public static int GetRandomNumber(int maxNumber)
        {
            return appRandom.Value.Next(maxNumber);
        }
        #endregion
    }
}
