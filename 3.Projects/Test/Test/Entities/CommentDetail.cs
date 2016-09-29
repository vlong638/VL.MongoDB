using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace Test.Entities
{
    public class CommentDetail
    {
        public static string CollectionName = "CommentDetail";

        #region Fields
        /// <summary>
        /// 标识列
        /// </summary>
        public ObjectId _id { get; set; }
        /// <summary>
        /// 评论主体Id
        /// </summary>
        public MongoDBRef Parent { get; set; }
        /// <summary>
        ///点赞用户Id列表
        /// </summary>
        public List<long> CommendUserIdList { get; set; } = new List<long>();
        /// <summary>
        /// 相关评论Id
        /// </summary>
        public MongoDBRef RelatedComment { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        #endregion
    }
}
