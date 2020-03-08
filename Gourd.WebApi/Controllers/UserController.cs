using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gourd.Application.IService.Default;
using Gourd.Application.ViewModel.Default;
using Gourd.Domain.Entity.Default;
using Gourd.Domain.IRepository.Default;
using Gourd.DomainCore.Repository.Default;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Gourd.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
      
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> CreateUser([FromBody ]CreateUserRequest request)
        {
            var response = await _userService.CreateUser(request);
            return new JsonResult(response);
        }

        /// <summary>
        /// 查询单个用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> GetUser(string id)
        {
            var response = await _userService.GetUser(id);
            return new JsonResult(response);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> Login([FromBody] LoginRequest request)
        {
            var response = await _userService.Login(request);
            return new JsonResult(response);
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> UpdateUser([FromBody]UpdateUserRequest request)
        {
            var response = await _userService.UpdateUser(request);
            return new JsonResult(response);
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> GetUserList([FromBody]GetUserListRequest request)
        {
            var response = await _userService.GetUserList(request);
            return new JsonResult(response);
        }

        /// <summary>
        /// 批量删除用户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> DelUser([FromBody]List<string> ids,string token)
        {
            if(token != "tibos666")
            {
                return new JsonResult("口令不对,是不允许删除滴!");
            }
            var response = await _userService.DelUser(ids);
            return new JsonResult(response);
        }
    }
}
