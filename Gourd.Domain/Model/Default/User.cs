using System;
using System.Collections.Generic;
using System.Text;

namespace Gourd.Domain.Model.Default
{
    /// <summary>
    /// 用户领域模型(客户关注的东西)
    /// </summary>
    public class User:BaseModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 用户积分
        /// </summary>
        public  decimal Integral { get; set; }

        /// <summary>
        /// 用户资产
        /// </summary>
        public  decimal Assets { get; set; }


        /// <summary>
        /// 最近登录时间
        /// </summary>
        public DateTime LoginTime { get; set; }
    }
}
