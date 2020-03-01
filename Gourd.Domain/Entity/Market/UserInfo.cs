using System;
using System.Collections.Generic;
using System.Text;

namespace Gourd.Domain.Entity.Market
{
    /// <summary>
    /// 用户实体
    /// </summary>
    public class UserInfo:BaseEntity
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public virtual string Pwd { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public virtual int Status { get; set; }
    }
}
