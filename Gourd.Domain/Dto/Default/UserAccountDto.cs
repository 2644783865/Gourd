using System;
using System.Collections.Generic;
using System.Text;

namespace Gourd.Domain.Dto.Default
{
    /// <summary>
    /// 用户账号
    /// </summary>
    public class UserAccountDto:BaseDto
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public virtual string UserID { get; set; }

        /// <summary>
        /// 用户积分
        /// </summary>
        public virtual decimal Integral { get; set; }

        /// <summary>
        /// 用户资产
        /// </summary>
        public virtual decimal Assets { get; set; }
    }
}
