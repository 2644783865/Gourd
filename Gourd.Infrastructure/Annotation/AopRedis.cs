using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gourd.Infrastructure.Annotation
{
    public class AopRedis : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var method = invocation.MethodInvocationTarget ?? invocation.Method;
            var param = method.GetParameters();
            if (param.Length > 0) 
            {
                for (int i = 0; i < param.Length; i++)
                {
                    if (param[i].Name.ToLower() == "key") 
                    {
                        //修改redis的主键,头部加上{v2_}标识
                        invocation.Arguments[i] = $"v2_{invocation.Arguments[i]}";
                    }
                }
            }
          invocation.Proceed();
        }
    }
}
