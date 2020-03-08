using Gourd.Domain.Dto.Default;
using Gourd.Domain.Model.Default;
using Gourd.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gourd.Domain.IFactory.Default
{
    public interface IUserFacory
    {
        Task<User> CreateUser(UserInfoDto dto);
    }
}
