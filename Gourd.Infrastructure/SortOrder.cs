using System;
using System.Collections.Generic;
using System.Text;

namespace Gourd.Infrastructure
{
    public class SortOrder
    {
        /// <summary>
        /// 排序方式(Asc,Desc)
        /// </summary>
        public OrderType orderType { get; set; }

        /// <summary>
        /// 要排序的属性(对应Model里的属性)
        /// </summary>
        public string value { get; set; }
    }
}
