using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Constants.Enums;
using Test.DAS;
using Test.Entities;
using Test.Utilities;

namespace Test.BLL
{
    public class Business
    {
        #region Nested Properties
        private CommentBody CommentBody = new CommentBody();
        private CommentDetail CommentDetail = new CommentDetail();
        private Session _session;
        public Session Session
        {
            get
            {
                return _session;
            }

            set
            {
                _session = value;
            }
        }
        #endregion

        #region Constructors
        public Business(Session session)
        {
            Session = session;
        }
        #endregion

        #region Methods
        #region 原稿
        //    收束主体的定位方式 如发生变化 只需要更改定位相关的方法
        //	进一步 可多主体
        //CreateCommentBody(来源type, 来源Id, 主体名称)
        //GetCommentBodyId(来源type, 来源Id, 主体名称)
        //	UpdateCommentBodyState(主体Id, 控制状态)
        //    更改某场评论的控制状态
        //	GetCommentBodyState(主体Id, 控制状态)
        //    某场评论的
        //	GetPagedComments(主体Id, 页号, 每页大小, 总条数)
        //    支持常规分页化查询
        //	GetLastestComments(主体Id, 条数)
        //    查询最新
        //	GetFollowedComments(主体Id, 分隔点Id, 条数)
        //    向后查阅
        //	Comment(主表Id, 用户Id, 内容)
        //    评论
        //	OptIn(子表Id, 用户Id)
        //    赞
        //	?OptOut(子表Id, 用户Id)
        #endregion
        #region 已实现
        /// <summary>
        /// 创建一场评论
        /// </summary>
        public async Task CreateCommentBody(SourceType sourceType, long sourceId)//(来源type, 来源Id)
        {
            CommentBody body = new CommentBody();
            body.SourceType = sourceType;
            body.SourceId = sourceId;
            body.ControlState = ControlState.Closed;
            body.CreateTime = DateTime.Now;
            var collection = Session.GetBsonCollection(CommentBody.CollectionName);
            Task ta= collection.InsertOneAsync(body.ToBsonDocument());
            ta.Wait();
            //collection.InsertOneAsync()
        }
        /// <summary>
        /// 获取某场评论主体的Id
        /// </summary>
        /// <returns></returns>
        public async Task<CommentBody> GetCommentBodyId(SourceType sourceType, long sourceId)//(来源type, 来源Id)
        {
            var filterBuilder = Builders<BsonDocument>.Filter;
            var filter = filterBuilder.And(filterBuilder.Eq(nameof(CommentBody.SourceType), (int)sourceType), filterBuilder.Eq(nameof(CommentBody.SourceId), sourceId));
            var collection = Session.GetBsonCollection(CommentBody.CollectionName);
            var bsonDocument = await collection.Find(filter).FirstOrDefaultAsync();
            if (bsonDocument == null)
            {
                Test.Utilities.Logger.WriteLog("GetCommentBodyId-未获取到匹配的评论主体{0}错误数据标识--sourceType:{1},sourceId:{2}", System.Environment.NewLine, sourceType, sourceId);
            }
            return BsonSerializer.Deserialize<CommentBody>(bsonDocument);
        }
        /// <summary>
        /// 评论主体的可评论开关
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UpdateCommentBodyControlState(ObjectId bodyId, ControlState controlState)//(主体Id, 控制状态)
        {
            var filter = Builders<BsonDocument>.Filter.Eq(nameof(CommentBody._id), bodyId);
            var update = Builders<BsonDocument>.Update.Set(nameof(CommentBody.ControlState), (int)ControlState.Available);
            //var updateOptions = new UpdateOptions();
            var collection = Session.GetBsonCollection(CommentBody.CollectionName);
            var result = await collection.UpdateOneAsync( filter, update);
            if (result.MatchedCount == 0)
            {
                Test.Utilities.Logger.WriteLog("UpdateCommentBodyControlState-未获取到匹配的数据{0}错误数据标识-bodyId:{1},controlState:{2}", System.Environment.NewLine, bodyId, controlState);
            }
            else if (result.MatchedCount != result.ModifiedCount)
            {
                Test.Utilities.Logger.WriteLog("UpdateCommentBodyControlState-匹配与更新数不一致{0}错误数据标识--bodyId:{1},controlState:{2}", System.Environment.NewLine, bodyId, controlState);
            }
            return result.ModifiedCount > 0;
        }
        /// <summary>
        /// 检索评论主体的可评论状态
        /// </summary>
        /// <param name="bodyId"></param>
        /// <returns></returns>
        public async Task<ControlState> GetCommentBodyControlState(ObjectId bodyId)//(主体Id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq(nameof(CommentBody._id), bodyId);
            var collection = Session.GetBsonCollection(CommentBody.CollectionName);
            var bsonDocument = await collection.Find(filter).FirstOrDefaultAsync();
            if (bsonDocument==null)
            {
                Test.Utilities.Logger.WriteLog("GetCommentBodyControlState-未获取到匹配的评论主体{0}错误数据标识--bodyId:{1}", System.Environment.NewLine, bodyId);
            }
            return BsonSerializer.Deserialize<CommentBody>(bsonDocument).ControlState;
        }
        /// <summary>
        /// 评论
        /// </summary>
        /// <returns>false:评论主体不可评论</returns>
        public async Task<bool> Comment(ObjectId bodyId, long userId, string content, ObjectId? relatedCommentId = null)//(主表Id, 用户Id, 内容)
        {
            var result = await GetCommentBodyControlState(bodyId);
            if (result == ControlState.Closed)
                return false;

            CommentDetail detail = new CommentDetail();
            detail.Parent = new MongoDBRef(CommentBody.CollectionName, bodyId);
            detail.UserId = userId;
            detail.Content = content;
            detail.CreateTime = DateTime.Now;
            if (relatedCommentId.HasValue)
            {
                detail.RelatedComment = new MongoDBRef(CommentDetail.CollectionName, relatedCommentId);
            }
            var collection = Session.GetBsonCollection(CommentDetail.CollectionName);
            await collection.InsertOneAsync(detail.ToBsonDocument());
            return true;
        }
        /// <summary>
        /// 获取最新的评论列表
        /// </summary>
        /// <param name="bodyId">评论主体Id</param>
        /// <param name="count">评论条数</param>
        /// <returns></returns>
        public async Task<List<CommentDetail>> GetLatestCommentDetails(ObjectId bodyId,int count)//(主体Id, 条数)
        {
            var filter = Builders<BsonDocument>.Filter.Eq(nameof(CommentDetail.Parent), new MongoDBRef(CommentBody.CollectionName, bodyId));
            var sort = Builders<BsonDocument>.Sort.Descending(nameof(CommentDetail.CreateTime));
            List<CommentDetail> commentDetails = new List<CommentDetail>();
            var collection = Session.GetBsonCollection(CommentDetail.CollectionName);
            var bsonDocuments = await collection.Find(filter).Sort(sort).Limit(count).ToListAsync();
            foreach (var item in bsonDocuments)
            {
                commentDetails.Add(BsonSerializer.Deserialize<CommentDetail>(item));
            }
            return commentDetails;
        }
        /// <summary>
        /// 根据某一条评论往前取往期的评论列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<CommentDetail>> GetFollowingCommentDetailsAfterCertainCommentDetail(ObjectId bodyId,ObjectId detailId,int count)//(主体Id, 分隔点Id, 条数)
        {
            var createTime =await GetCommentDetailCreateTime(detailId);
            var filterBuilder = Tools.FilterBuilder;
            var filter = filterBuilder.And(filterBuilder.Eq(nameof(CommentDetail.Parent), new MongoDBRef(CommentBody.CollectionName, bodyId)),filterBuilder.Lt(nameof(CommentDetail.CreateTime), createTime));
            var sort = Tools.SortBuilder.Descending(nameof(CommentDetail.CreateTime));
            var collection = Session.GetBsonCollection(CommentDetail.CollectionName);
            var bsonDocuments = await collection.Find(filter).Sort(sort).Limit(count).ToListAsync();
            List<CommentDetail> commentDetails = new List<CommentDetail>();
            foreach (var bsonDocument in bsonDocuments)
            {
                commentDetails.Add(BsonSerializer.Deserialize<CommentDetail>(bsonDocument));
            }
            return commentDetails;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="detailId"></param>
        /// <returns></returns>
        private async Task<DateTime> GetCommentDetailCreateTime(ObjectId detailId)
        {
            var filter = Builders<BsonDocument>.Filter.Eq(nameof(CommentDetail._id), detailId);
            var collection = Session.GetBsonCollection(CommentDetail.CollectionName);
            var bsonDocument = await collection.Find(filter).FirstAsync();
            return DateTime.Parse(bsonDocument.GetElement(nameof(CommentDetail.CreateTime)).Value.ToString());
        }
        /// <summary>
        /// 赞
        /// </summary>
        /// <returns></returns>
        public async Task OptIn(ObjectId commentId,long userId)//(子表Id, 用户Id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq(nameof(CommentDetail._id), commentId);
            var update = Builders<BsonDocument>.Update.AddToSet(nameof(CommentDetail.CommendUserIdList), userId);
            //var updateOptions = new UpdateOptions();
            var collection = Session.GetBsonCollection(CommentDetail.CollectionName);
            var result = await collection.UpdateOneAsync(filter, update);
            if (result.MatchedCount==0)
            {
                Test.Utilities.Logger.WriteLog("OptIn-未获取到匹配的数据{0}错误数据标识-commentId:{1},userId:{2}", System.Environment.NewLine, commentId, userId);
            }
            else if (result.MatchedCount!=result.ModifiedCount)
            {
                Test.Utilities.Logger.WriteLog("OptIn-匹配与更新数不一致{0}错误数据标识-commentId:{1},userId:{2}", System.Environment.NewLine, commentId, userId);
            }
        }
        #endregion
        #region 本版实现功能
        #region 分页数据
        //public List<Comment> GetPagedComments()//(主体Id, 页号, 每页大小, 总条数)
        //{
        //    throw new NotImplementedException();
        //}
        #endregion
        #endregion
        #region 测试
        /// <summary>
        /// 创建一场评论
        /// </summary>
        public async Task CreateCommentBody(ObjectId commentBodyId,SourceType sourceType, long sourceId)//(来源type, 来源Id)
        {
            CommentBody body = new CommentBody();
            body._id = commentBodyId;
            body.SourceType = sourceType;
            body.SourceId = sourceId;
            body.ControlState = ControlState.Closed;
            body.CreateTime = DateTime.Now;
            var collection = Session.GetBsonCollection(CommentBody.CollectionName);
            await collection.InsertOneAsync(body.ToBsonDocument());
        }
        public async Task<long> GetCommentDetailsCount(ObjectId commentBodyId)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("Parent", new MongoDBRef(CommentBody.CollectionName, commentBodyId));
            var collection = Session.Database.GetCollection<BsonDocument>(CommentDetail.CollectionName);
            return await collection.Find(filter).CountAsync();
        }
        /// <summary>
        /// 获取某条评论
        /// </summary>
        /// <returns></returns>
        public async Task<CommentDetail> GetCommentDetailById(ObjectId commentDetailId)
        {
            var filterBuilder = Builders<BsonDocument>.Filter;
            var filter = filterBuilder.Eq("_id", commentDetailId);
            var collection = Session.GetBsonCollection(CommentDetail.CollectionName);
            var bsonDocument = await collection.Find(filter).FirstAsync();
            return BsonSerializer.Deserialize<CommentDetail>(bsonDocument);
        }
        /// <summary>
        /// 评论
        /// </summary>
        /// <returns>false:评论主体不可评论</returns>
        public async Task<bool> CreateCommentDetail(ObjectId bodyId, ObjectId detailId, long userId, string content, ObjectId? relatedCommentId = null)//(主表Id, 用户Id, 内容)
        {
            var result = await GetCommentBodyControlState(bodyId);
            if (result == ControlState.Closed)
                return false;

            CommentDetail detail = new CommentDetail();
            detail._id = detailId;
            detail.Parent = new MongoDBRef(CommentBody.CollectionName, bodyId);
            detail.UserId = userId;
            detail.Content = content;
            detail.CreateTime = DateTime.Now;
            if (relatedCommentId.HasValue)
            {
                detail.RelatedComment = new MongoDBRef(CommentDetail.CollectionName, relatedCommentId);
            }
            var collection = Session.GetBsonCollection(CommentDetail.CollectionName);
            await collection.InsertOneAsync(detail.ToBsonDocument());
            return true;
        }
        public void CreateTemp()//(来源type, 来源Id)
        {
            Session.InsertTempObject();
        }
        //public void MultiCreateTemp()//(来源type, 来源Id)
        //{
        //    Session.MultiInsert();
        //}
        #endregion
        #region 本版不予实现
        ///// <summary>
        ///// 取消赞 
        ///// </summary>
        ///// <returns></returns>
        //public bool OptOut()//(子表Id, 用户Id)
        //{
        //    throw new NotImplementedException();
        //} 
        #endregion
        #endregion
    }
}
