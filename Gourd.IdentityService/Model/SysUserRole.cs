using System;
using System.Collections.Generic;
using System.Text;

namespace Gourd.IdentityService
{
    /// <summary>
    /// 用户角色实体类
    /// </summary>
    [Serializable]
    public class SysUserRole
    {
        public string Id { get; set; }
        //用户或者部门ID
        public string UserId { get; set; }
        //角色ID
        public string RoleId { get; set; }
    }
}
