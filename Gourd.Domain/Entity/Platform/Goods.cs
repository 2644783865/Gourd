using System;
using System.Collections.Generic;
using System.Text;

namespace Gourd.Domain.Entity.Platform
{
    /// <summary>
    /// 商品
    /// </summary>
    public class Goods:BaseEntity
    {
        /// <summary>
        /// 商品编码
        /// </summary>
        public virtual string No { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 商品分类编号
        /// </summary>
        public virtual string GoodsTypeId { get; set; }
    }
}
