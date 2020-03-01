using Gourd.InitQ.Abstractions;
using System;
using System.Reflection;

namespace Gourd.InitQ.Internal
{
    public class ConsumerExecutorDescriptor
    {
        public MethodInfo MethodInfo { get; set; }

        public TypeInfo ImplTypeInfo { get; set; }

        public TopicAttribute Attribute { get; set; }
    }
}
