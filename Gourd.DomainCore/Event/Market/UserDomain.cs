using AutoMapper;
using Gourd.Domain.Dto.Market;
using Gourd.Domain.Entity.Market;
using Gourd.Domain.IEvent.Market;
using Gourd.Domain.IFactory.Market;
using Gourd.Domain.IRepository;
using Gourd.DomainCore.Repository.Market;
using Gourd.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Gourd.DomainCore.Event.Market
{
    public class UserDomain :IUserDomain
    {
        private readonly IBaseRepository<UserInfo> dao;
        private readonly IMapper _mapper;
        private readonly IUserFacory _userFacory;

        public UserDomain(IBaseRepository<UserInfo> dao, IUserFacory userFacory, IMapper mapper)
        {
            this.dao = dao;
            this._mapper = mapper;
            this._userFacory = userFacory;
        }

        public async Task<JsonResponse> CreateUser(UserInfoDto dto)
        {
            JsonResponse response = new JsonResponse();
            try
            {
                var model = _mapper.Map<UserInfo>(dto);
                model.CreateTime = DateTime.Now;
                model.Id = Guid.NewGuid().ToString();
                model = await dao.AddAsync(model);
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

        public async Task<JsonResponse> DelUser(List<string> Ids)
        {
            JsonResponse response = new JsonResponse();
            try
            {
                await dao.DeleteAsync(Ids);
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
               
                var model = await dao.GetAsync(id);
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
                var res = await dao.GetListAsync(expression, dto.pageIndex, dto.pageSize, dto.sortOrder);
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
                await dao.UpdateAsync(model);
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
