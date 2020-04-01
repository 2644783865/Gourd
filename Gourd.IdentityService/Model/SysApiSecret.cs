using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gourd.IdentityService.Model
{
    public class SysApiSecret:ApiSecret
    {
        /// <summary>
        /// 资源名称
        /// </summary>
        public string ApiResourceName { get; set; }
    }
}
