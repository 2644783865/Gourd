using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Gourd.Infrastructure
{
    public enum StatusCodeDefine
    {
        /// <summary>
        /// 处理成功
        /// </summary>
        [Description("处理成功")]
        Success = 200,


        /// <summary>
        /// 未授权
        /// </summary>
        [Description("未授权")]
        Unauthorized = 401,


        /// <summary>
        /// 客户端请求的范围无效
        /// </summary>
        [Description("客户端请求的范围无效")]
        RequestedRangeNotSatisfiable = 416,

        /// <summary>
        /// 服务错误
        /// </summary>
        [Description("服务错误")]
        ServerError = 500,
    }
}
