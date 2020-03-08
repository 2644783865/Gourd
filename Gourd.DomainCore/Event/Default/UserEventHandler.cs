using AutoMapper;
using Gourd.Domain.Dto.Default;
using Gourd.Domain.Entity.Default;
using Gourd.Domain.Model;
using Gourd.Domain.Model.Default;
using Gourd.DomainCore.Repository.Default;
using Gourd.Infrastructure.config;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Gourd.Domain.IEvent.Default;

namespace Gourd.DomainCore.Event.Default
{
    /// <summary>
    /// 用户领域事件
    /// </summary>
    public class UserEventHandler:BaseEventHandler<UserDto>, IUserEventHandler
    {
        private readonly DBConfig _config;
        private readonly IMapper _mapper;


        public event  EventHandler<UserDto> TestEvent;

        public UserEventHandler(IOptions<DBConfig> config, IMapper mapper)
        {
            _config = config.Value;
            this._mapper = mapper;
        }

        /// <summary>
        /// 用户登录成功加分事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UserAccount_Login_Handler(object sender, UserDto e)
        {
            //事务
            try
            {
                Task.Run(async() => 
                {
                    using (var db = new DefaultDbContext(_config.ConnType, _config.ConnName))
                    {
                        using (var transaction = db.Database.BeginTransaction())
                        {
                            var entity = await db.UserAccount.AsQueryable().FirstOrDefaultAsync(m => m.UserID == e.Id);
                            entity.Integral += e.userAccountDto.Integral;
                            db.UserAccount.Update(entity);
                            await db.SaveChangesAsync();
                            await transaction.CommitAsync();
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                //TODO 记录错误日志
            }
        }
    


        /// <summary>
        /// 用户登录记录事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public void UserLogin_Login_Handler(object sender, UserDto e)
        {
            //事务
            try
            {
                Task.Run(async () =>
                {
                    using (var db = new DefaultDbContext(_config.ConnType, _config.ConnName))
                    {
                        using (var transaction = db.Database.BeginTransaction())
                        {
                            var entity = _mapper.Map<UserLogin>(e.userLoginDto);
                            await db.AddAsync(entity);
                            await db.SaveChangesAsync();
                            await transaction.CommitAsync();
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                //TODO 记录错误日志
            }
        }
    }
}
