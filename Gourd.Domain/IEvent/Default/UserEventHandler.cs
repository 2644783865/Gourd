using Gourd.Domain.Dto.Default;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gourd.Domain.IEvent.Default
{
    public interface IUserEventHandler: IBaseEventHandler<UserDto>
    {
        void UserAccount_Login_Handler(object sender, UserDto e);

        void UserLogin_Login_Handler(object sender, UserDto e);
    }
}
