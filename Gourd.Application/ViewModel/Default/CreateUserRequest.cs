using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Gourd.Application.ViewModel.Default
{
    public class CreateUserRequest
    {
        [Required(ErrorMessage = "用户名不能为空")]
        public string name { get; set; }

        [Required(ErrorMessage = "账号不能为空")]
        public string account { get; set; }

        [Required(ErrorMessage = "密码不能为空")]
        public string pwd { get; set; }
    }
}
