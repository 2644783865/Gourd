using Gourd.Application.ViewModel.Market;
using Gourd.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gourd.Application.IService.Market
{
    public interface IUserService
    {
        Task<JsonResponse> CreateUser(CreateUserRequest request);

        Task<JsonResponse> UpdateUser(UpdateUserRequest request);

        Task<JsonResponse> GetUser(string id);

        Task<JsonResponse> GetUserList(GetUserListRequest request);

        Task<JsonResponse> DelUser(List<string> Ids);
    }
}
