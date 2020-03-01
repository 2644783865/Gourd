using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Gourd.WebApi.Filters
{
    /// <summary>
    /// swagger请求头
    /// </summary>
    //public class HttpHeaderOperationFilter : IOperationFilter
    //{
    //    public void Apply(Operation operation, OperationFilterContext context)
    //    {
    //        #region 新方法
    //        if (operation.Parameters == null)
    //        {
    //            operation.Parameters = new List<IParameter>();
    //        }

    //        if (context.ApiDescription.TryGetMethodInfo(out MethodInfo methodInfo))
    //        {
    //            if (!methodInfo.CustomAttributes.Any(t => t.AttributeType == typeof(AllowAnonymousAttribute))
    //                    && !(methodInfo.ReflectedType.CustomAttributes.Any(t => t.AttributeType == typeof(AuthorizeAttribute))))
    //            {
    //                operation.Parameters.Add(new NonBodyParameter
    //                {
    //                    Name = "Authorization",
    //                    In = "header",
    //                    Type = "string",
    //                    Required = true,
    //                    Description = "请输入Token，格式为bearer XXX"
    //                });
    //            }
    //        }
    //        #endregion
    //    }
    //}
}
