using System;
using System.Collections.Generic;
using System.Text;

namespace NeoSmart.Redis
{
    public class SerializerRequiredException: Exception
    {
        public SerializerRequiredException(Type t1 ,Type t2)
            : base($"A serializer is required to convert from {t1} to {t2}!")
        {
        }
    }
}
