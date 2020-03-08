using System;
using System.Collections.Generic;
using System.Text;

namespace Gourd.Domain.Model
{
    public class BaseModel
    {
        /// <summary>
        /// 唯一实例标识
        /// </summary>
        public virtual string Id { get; set; }
    }
}
