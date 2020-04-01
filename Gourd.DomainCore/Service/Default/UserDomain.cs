using AutoMapper;
using Gourd.Domain.Dto.Default;
using Gourd.Domain.Entity.Default;
using Gourd.Domain.IEvent;
using Gourd.Domain.IEvent.Default;
using Gourd.Domain.IFactory.Default;
using Gourd.Domain.IRepository;
using Gourd.Domain.IService.Default;
using Gourd.Domain.Model.Default;
using Gourd.DomainCore.Event;
using Gourd.DomainCore.Event.Default;
using Gourd.DomainCore.Repository.Default;
using Gourd.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Gourd.DomainCore.Service.Default
{
    public class UserDomain :IUserDomain
    {
        private readonly IBaseRepository<UserInfo> _userInfoRepository;  //用户实体(聚合根)
        private readonly IBaseRepository<UserAccount> _userAccountRepository; //用户资产实体
        private readonly IUserEventHandler _userEventHandler;

        private readonly IMapper _mapper;
        private readonly IUserFacory _userFacory;

        public UserDomain(IUserEventHandler userEventHandler,IBaseRepository<UserInfo> userInfoRepository, IUserFacory userFacory, IMapper mapper)
        {
            this._userInfoRepository = userInfoRepository;
            this._mapper = mapper;
            this._userFacory = userFacory;
            this._userEventHandler = userEventHandler;
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<JsonResponse> CreateUser(UserInfoDto dto)
        {
            JsonResponse response = new JsonResponse();
            try
            {
                var model = await _userFacory.CreateUser(dto);
                response.data = model;
                response.total = 1;
                response.msg = "操作成功";
                response.code = StatusCodeDefine.Success;
            }
            catch (Exception ex)
            {
                response.msg = ex.Message;
                response.code = StatusCodeDefine.ServerError;
                response.status = -1;
            }
            return response;
        }

        public async Task<JsonResponse> Login(UserInfoDto dto)
        {
            JsonResponse response = new JsonResponse();
            try
            {
                var pwd = "";
                using (var md5 = MD5.Create()) 
                {
                    var result = md5.ComputeHash(Encoding.UTF8.GetBytes(dto.Pwd));
                     pwd = BitConverter.ToString(result).Replace("-", "");
                }

                var model = await _userInfoRepository.GetAsync(m => m.Account == dto.Account && m.Pwd == pwd);
                if(model == null)
                {
                    response.total = 0;
                    response.msg = "账号或密码错误";
                    response.code = StatusCodeDefine.RequestedRangeNotSatisfiable;
                    return response;
                }
                response.data = model;
                response.total = 1;
                response.msg = "操作成功";
                response.code = StatusCodeDefine.Success;
                //绑定事件
                _userEventHandler.BaseDomainEvent += _userEventHandler.UserAccount_Login_Handler;
                _userEventHandler.BaseDomainEvent += _userEventHandler.UserLogin_Login_Handler;
                UserDto userDto = new UserDto()
                {
                    Id = model.Id,
                    userAccountDto = new UserAccountDto() { Integral = 5 },
                    userLoginDto = new UserLoginDto() 
                    { 
                        Id = Guid.NewGuid().ToString(),
                        UserID = model.Id,
                        LoginIP = "",
                        LoginTime = DateTime.Now,
                        LoginType = 0
                    }
                };
                await _userEventHandler.RunAsync(userDto);
            }
            catch (Exception ex) 
            {
                response.msg = ex.Message;
                response.code = StatusCodeDefine.ServerError;
                response.status = -1;
            }
            return response;
        }


        public async Task<JsonResponse> DelUser(List<string> Ids)
        {
            JsonResponse response = new JsonResponse();
            try
            {
                await _userInfoRepository.DeleteAsync(Ids);
                response.msg = "操作成功";
                response.code = StatusCodeDefine.Success;
            }
            catch (Exception ex)
            {
                response.msg = ex.Message;
                response.code = StatusCodeDefine.ServerError;
                response.status = -1;
            }
            return response;
        }

        public async Task<JsonResponse> GetUser(string id)
        {
            JsonResponse response = new JsonResponse();
            try
            {
               
                var model = await _userInfoRepository.GetAsync(id);
                response.data = model;
                if (model != null)
                {
                    response.total = 1;
                }
                response.msg = "操作成功";
                response.code = StatusCodeDefine.Success;
            }
            catch (Exception ex)
            {
                response.msg = ex.Message;
                response.code = StatusCodeDefine.ServerError;
                response.status = -1;
            }
            return response;
        }

        public async Task<JsonResponse> GetUserList(UserInfoDto dto)
        {
            JsonResponse response = new JsonResponse();
            try
            {
                List<Expression<Func<UserInfo, bool>>> expression = new List<Expression<Func<UserInfo, bool>>>();
                if (!string.IsNullOrEmpty(dto.Name))
                {
                    expression.Add(m => m.Name == dto.Name);
                }
                if (!string.IsNullOrEmpty(dto.Pwd))
                {
                    expression.Add(m => m.Pwd == dto.Pwd);
                }
                if (dto.Status.HasValue)
                {
                    expression.Add(m => m.Status == dto.Status.Value);
                }
                var res = await _userInfoRepository.GetListAsync(expression, dto.pageIndex, dto.pageSize, dto.sortOrder);
                response.data = res.Item1;
                response.total = res.Item2;
                response.msg = "操作成功";
                response.code = StatusCodeDefine.Success;
            }
            catch (Exception ex)
            {
                response.msg = ex.Message;
                response.code = StatusCodeDefine.ServerError;
                response.status = -1;
            }
            return response;
        }

        public async Task<JsonResponse> UpdateUser(UserInfoDto dto)
        {
            JsonResponse response = new JsonResponse();
            try
            {
                var model = _mapper.Map<UserInfo>(dto);
                await _userInfoRepository.UpdateAsync(model);
                response.data = model;
                response.total = 1;
                response.msg = "操作成功";
                response.code = StatusCodeDefine.Success;
            }
            catch (Exception ex)
            {
                response.msg = ex.Message;
                response.code = StatusCodeDefine.ServerError;
                response.status = -1;
            }
            return response;
        }
    }
}
