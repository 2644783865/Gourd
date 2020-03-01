using System;
using System.Collections.Generic;
using System.Text;

namespace Gourd.Domain.Entity.Platform
{
    /// <summary>
    /// 商品分类
    /// </summary>
    public class GoodsType:BaseEntity
    {
        /// <summary>
        /// 商品分类名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 商品分类父级编号
        /// </summary>
        public virtual string ParentId { get; set; }

        /// <summary>
        /// 层级
        /// </summary>
        public virtual int Level { get; set; }
    }
}
