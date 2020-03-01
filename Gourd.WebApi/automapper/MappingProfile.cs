using AutoMapper;
using Gourd.Domain.Dto.Market;
using Gourd.Domain.Entity.Market;
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

            #endregion

        }
    }
}
