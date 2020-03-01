using Gourd.InitQ.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gourd.InitQ.Attributes
{
    public class SubscribeAttribute: TopicAttribute
    {
        public SubscribeAttribute(string name) : base(name) 
        {

        }
    }
}
