using System;
using System.Collections.Generic;
using System.Text;

namespace Gourd.Domain.Entity.Channel
{
    public class OutboundRecord:BaseEntity
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public virtual string OrderId { get; set; }

        /// <summary>
        /// 商品编码
        /// </summary>
        public virtual int No { get; set; }

        /// <summary>
        /// 状态(0:出库中,1:已出库)
        /// </summary>
        public virtual int Status { get; set; }

        /// <summary>
        /// 出库时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
    }
}
