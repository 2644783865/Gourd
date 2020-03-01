using Gourd.Domain.Dto.Market;
using Gourd.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gourd.Domain.IEvent.Market
{
    public interface IUserDomain
    {
        Task<JsonResponse> CreateUser(UserInfoDto dto);

        Task<JsonResponse> UpdateUser(UserInfoDto dto);

        Task<JsonResponse> GetUser(string id);

        Task<JsonResponse> GetUserList(UserInfoDto dto);

        Task<JsonResponse> DelUser(List<string> Ids);
    }
}
