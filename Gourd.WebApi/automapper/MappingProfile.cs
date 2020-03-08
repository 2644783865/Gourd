using AutoMapper;
using Gourd.Domain.Dto.Default;
using Gourd.Domain.Entity.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Gourd.WebApi.automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Model -> Dto
            CreateMap<UserInfo, UserInfoDto>();
            CreateMap<UserAccount, UserAccountDto>();
            CreateMap<UserLogin, UserLoginDto>();
            #endregion

        }
    }
}
