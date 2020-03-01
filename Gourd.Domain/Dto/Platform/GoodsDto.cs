using System;
using System.Collections.Generic;
using System.Text;

namespace Gourd.Domain.Dto.Platform
{
    /// <summary>
    /// 商品
    /// </summary>
    public class GoodsDto : BaseDto
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
