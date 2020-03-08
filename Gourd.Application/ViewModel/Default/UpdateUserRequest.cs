using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Gourd.Application.ViewModel.Default
{
    public class UpdateUserRequest
    {
        [Required(ErrorMessage = "用户标识不能为空")]
        public string id { get; set; }

        [Required(ErrorMessage = "用户名不能为空")]
        public string name { get; set; }

        [Required(ErrorMessage = "密码不能为空")]
        public string pwd { get; set; }
    }
}
