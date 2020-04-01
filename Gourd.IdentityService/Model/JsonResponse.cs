using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gourd.IdentityService.Model
{
    /// <summary>
    /// 返回的Json模型
    /// </summary>
    public class JsonResponse
    {
        private int _status = 0;
        /// <summary>
        /// 状态 -1失败,0成功
        /// </summary>
        public int status
        {
            get { return _status; }
            set { _status = value; }
        }

        public string msg { get; set; }//消息


        public string code { get; set; } //状态码

        public object data { get; set; }//数据

        public int total { get; set; }//总条数
    }
}
