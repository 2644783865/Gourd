using Gourd.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gourd.Domain.Dto.Market
{
    /// <summary>
    /// 用户实体
    /// </summary>
    public class UserInfoDto : BaseDto
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


        public virtual int? Status { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public virtual int?  pageIndex { get; set; }

        /// <summary>
        /// 条数
        /// </summary>
        public virtual int? pageSize { get; set; }

        /// <summary>
        /// 排序方式
        /// </summary>
        public virtual List<SortOrder> sortOrder { get; set; }
    }
}
