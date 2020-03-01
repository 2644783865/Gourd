using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gourd.Infrastructure.Annotation
{
    /// <summary>
    /// 隐藏swagger接口特性标识
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class HiddenApiAttribute : System.Attribute
    {
    }
}
