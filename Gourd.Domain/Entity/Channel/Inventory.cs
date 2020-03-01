using System;
using System.Collections.Generic;
using System.Text;

namespace Gourd.Domain.Entity.Channel
{
    /// <summary>
    /// 库存表
    /// </summary>
    public class Inventory:BaseEntity
    {
        /// <summary>
        /// 商品编码
        /// </summary>
        public virtual string No { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        public virtual int Count { get; set; }
    }
}
