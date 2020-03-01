using Gourd.Infrastructure;
using Gourd.WebApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gourd.WebApi.Filters
{
    public class ResultFilterAttribute : Attribute, IResultFilter
    {
        private readonly ILogger<ResultFilterAttribute> logger;
 
         public ResultFilterAttribute(ILoggerFactory loggerFactory)
         {
             logger = loggerFactory.CreateLogger<ResultFilterAttribute>();
         }
 
         public void OnResultExecuted(ResultExecutedContext context)
         {

         }
 
         public void OnResultExecuting(ResultExecutingContext context)
         {
            if (!context.ModelState.IsValid)
            {
                JsonResponse response = new JsonResponse();
                var errorList = new List<ShowError>();
                var errorFieldsAndMsgs = context.ModelState.Where(m => m.Value.Errors.Any()) .Select(x => new { x.Key, x.Value.Errors });
                foreach (var item in errorFieldsAndMsgs)
                {
                    //获取键
                    var fieldKey = item.Key;
                    //获取键对应的错误信息
                    var fieldErrors = item.Errors
                        .Select(e => new ShowError(fieldKey, e.ErrorMessage));
                    errorList.AddRange(fieldErrors);
                }
                response.data = errorList;
                response.status = -1;
                response.code = StatusCodeDefine.RequestedRangeNotSatisfiable;
                response.msg = errorList.FirstOrDefault().Message;
                context.Result = new JsonResult(response);
            }
        }
    }
}
