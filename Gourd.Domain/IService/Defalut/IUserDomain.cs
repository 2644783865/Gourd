using Gourd.Domain.Dto.Default;
using Gourd.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gourd.Domain.IService.Default
{
    public interface IUserDomain
    {
        Task<JsonResponse> CreateUser(UserInfoDto dto);

        Task<JsonResponse> UpdateUser(UserInfoDto dto);

        Task<JsonResponse> GetUser(string id);

        Task<JsonResponse> Login(UserInfoDto dto);

        Task<JsonResponse> GetUserList(UserInfoDto dto);

        Task<JsonResponse> DelUser(List<string> Ids);
    }
}
