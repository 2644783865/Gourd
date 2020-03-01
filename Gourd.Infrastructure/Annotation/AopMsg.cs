using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gourd.Infrastructure.Annotation
{
    public class AopMsg : IInterceptor
    {
        /// <summary>
        /// 拦截方法 打印被拦截的方法执行前的名称、参数和方法执行后的 返回结果
        /// </summary>
        /// <param name="invocation">包含被拦截方法的信息</param>
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine("方法执行前:拦截{0}类下的方法{1}的参数是{2}",
                invocation.InvocationTarget.GetType(),
                invocation.Method.Name, string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray()));

            //在被拦截的方法执行完毕后 继续执行
            invocation.Proceed();

            Console.WriteLine("方法执行完毕，返回结果：{0}", invocation.ReturnValue);
            Console.WriteLine();
        }
    }
}
