using System;
using System.Collections.Generic;
using System.Text;

namespace Gourd.IdentityService
{
    /// <summary>
    /// 角色实体类
    /// </summary>
    [Serializable]
    public class SysRole
    {
        public string Id { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Memo { get; set; }
    }
}
