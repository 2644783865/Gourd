using AutoMapper;
using Gourd.Domain.Dto.Default;
using Gourd.Domain.Entity.Default;
using Gourd.Domain.IFactory.Default;
using Gourd.Domain.Model.Default;
using Gourd.DomainCore.Repository.Default;
using Gourd.Infrastructure;
using Gourd.Infrastructure.config;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Gourd.DomainCore.Factory.Default
{
    public class UserFacory : IUserFacory
    {
        private readonly DBConfig _config;
        private readonly IMapper _mapper;
        public UserFacory(IOptions<DBConfig> config, IMapper mapper)
        {
            _config = config.Value;
            this._mapper = mapper;
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="dto_userLogin"></param>
        /// <returns></returns>
        public async Task<User> CreateUser(UserInfoDto dto)
        {
            //事务
            try
            {
                using (var db = new DefaultDbContext(_config.ConnType, _config.ConnName))
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        //创建用户实体
                        var model = _mapper.Map<UserInfo>(dto);
                        model.CreateTime = DateTime.Now;
                        model.Id = Guid.NewGuid().ToString();
                        using (var md5 = MD5.Create())
                        {
                            var result = md5.ComputeHash(Encoding.UTF8.GetBytes(dto.Pwd));
                            model.Pwd = BitConverter.ToString(result).Replace("-", "");
                        }
                        await db.UserInfo.AddAsync(model);
                        //初始化用户资产
                        UserAccount account = new UserAccount()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Assets = 0,
                            Integral = 0,
                            UserID = model.Id
                        };
                        await db.UserAccount.AddAsync(account);
                        await db.SaveChangesAsync();
                        await transaction.CommitAsync();

                        //用户模型
                        User user = new User() 
                        {
                            Id = model.Id,
                            Assets = account.Assets,
                            Integral = account.Integral,
                            Name = model.Name,
                            LoginTime = DateTime.Now
                        };
                        return user;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
