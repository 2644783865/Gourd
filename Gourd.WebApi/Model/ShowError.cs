using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gourd.WebApi.Model
{
    public class ShowError
    {
        public ShowError(string key, string message)
        {
            Key = key;
            Message = message;
        }
        public string Key { get; set; }
        public string Message { get; set; }
    }
}
