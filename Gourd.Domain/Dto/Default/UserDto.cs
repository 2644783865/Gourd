using System;
using System.Collections.Generic;
using System.Text;

namespace Gourd.Domain.Dto.Default
{
    /// <summary>
    /// 用户领域模型数据传输模型
    /// </summary>
    public class UserDto:BaseDto
    {
        /// <summary>
        /// 聚合根
        /// </summary>
        public UserInfoDto userInfoDto { get; set; }

        /// <summary>
        /// 用户账号实体
        /// </summary>
        public UserAccountDto userAccountDto { get; set; }

        /// <summary>
        /// 用户登录记录实体
        /// </summary>
        public UserLoginDto userLoginDto { get; set; }
    }
}
