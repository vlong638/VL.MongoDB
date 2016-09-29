using MongoDB.Bson;
using System;
using Test.Constants.Enums;

namespace Test.Entities
{
    public class CommentBody
    {
        public static string CollectionName = "CommentBody";

        #region Fields
        /// <summary>
        /// 标识Id
        /// </summary>
        public ObjectId _id { set; get; }
        /// <summary>
        /// 来源类型
        /// </summary>
        public SourceType SourceType { set; get; }
        /// <summary>
        /// 来源Id
        /// </summary>
        public long SourceId { set; get; }
        /// <summary>
        /// 评论主体状态
        /// 可评论
        /// 已关闭
        /// </summary>
        public ControlState ControlState { set; get; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { set; get; } 
        #endregion
    }
}
