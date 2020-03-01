using System;
using System.Collections.Generic;
using System.Text;

namespace Gourd.Domain.Dto.Channel
{
    /// <summary>
    /// 库存表
    /// </summary>
    public class InventoryDto:BaseDto
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
