using System;
using System.Collections.Generic;
using System.Text;

namespace Gourd.Infrastructure.config
{
    public class RedisConfig
    {
        public string Ip { get; set; }

        public int Port { get; set; }

        public string Password { get; set; }
    }
}
