using Gourd.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Linq;
using Gourd.Infrastructure.Annotation;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Gourd.WebApi.Filters
{
    public class ActionFilterAttribute : Attribute, IActionFilter
    {

        private readonly ILogger<ActionFilterAttribute> logger;
        private readonly IMemoryCache _Cache;


        private readonly Token _Token;
        public ActionFilterAttribute(ILoggerFactory loggerFactory, IMemoryCache memoryCache
        )
        {
            logger = loggerFactory.CreateLogger<ActionFilterAttribute>();
            _Cache = memoryCache;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var request = context.HttpContext.Request;
            var parms = "";
            // post 请求方式获取请求参数方式
            try
            {
                if (request.Method.ToLower().Equals("post"))
                {
                    request.Body.Seek(0, SeekOrigin.Begin);
                    using (var reader = new StreamReader(request.Body, Encoding.UTF8))
                    {
                        parms = reader.ReadToEnd();
                    }
                    request.Body.Seek(0, SeekOrigin.Begin);
                }
            }
            catch (Exception e)
            {


            }
            //请求的唯一ID
            var requestId = context.ActionDescriptor.Id;


            #region 记录日志(所有的请求)
            MonitorLog MonLog = new MonitorLog();
            MonLog.RequestID = requestId;
            MonLog.UID = context.HttpContext.User.Identity.Name;
            MonLog.IP = context.HttpContext?.Connection.RemoteIpAddress.ToString();
            MonLog.ExecuteStartTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff", DateTimeFormatInfo.InvariantInfo));
            MonLog.ControllerName = context.RouteData.Values["controller"] as string;
            MonLog.ActionName = context.RouteData.Values["action"] as string;
            MonLog.QueryCollections = context.HttpContext.Request.QueryString;//Url 参数
            MonLog.BodyCollections = parms; //Form参数
            _Cache.Set(requestId, MonLog, TimeSpan.FromSeconds(60));
            #endregion

            #region 根据注解允许匿名访问
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            //controller
            var controllerAttributes = actionDescriptor.MethodInfo.DeclaringType.GetCustomAttributes(typeof(AlwaysAccessibleAttribute), true);
            if (controllerAttributes != null && controllerAttributes.Length > 0)
            {
                return;
            }
            //action
            var actionAttributes = actionDescriptor.MethodInfo.GetCustomAttributes(typeof(AlwaysAccessibleAttribute), true);
            if (actionAttributes != null && actionAttributes.Length > 0)
            {
                return;
            }
            #endregion
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var requestId = context.ActionDescriptor.Id;
            var MonLog = _Cache.Get<MonitorLog>(requestId);
            if (MonLog != null)
            {
                MonLog.ExecuteEndTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff", DateTimeFormatInfo.InvariantInfo));
                var mm = (MonLog.ExecuteEndTime - MonLog.ExecuteStartTime);
                MonLog.TimeConsuming = Convert.ToDecimal((MonLog.ExecuteEndTime - MonLog.ExecuteStartTime).TotalSeconds);
                logger.LogInformation(MonLog.GetLoginfo());
                //插入表
            }
        }
    }
}
