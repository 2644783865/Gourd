using System;
using System.Collections.Generic;
using System.Text;

namespace Gourd.Domain.Dto.Market
{
    /// <summary>
    /// 登录实体
    /// </summary>
    public class UserLoginDto:BaseDto
    {
        /// <summary>
        /// 登录时间
        /// </summary>
        public virtual DateTime LoginTime { get; set; }

        /// <summary>
        /// 登录IP
        /// </summary>
        public virtual string LoginIP { get; set; }

        /// <summary>
        /// 登录终端(0:PC,1:H5,2:Android,3:IOS)
        /// </summary>
        public virtual int LoginType { get; set; }
    }
}
