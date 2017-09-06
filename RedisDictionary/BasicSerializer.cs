using System;
using System.Collections.Generic;
using System.Text;

namespace NeoSmart.Redis
{
    public class BasicSerializer<T> : ISerializer<T, string>
    {
        private Func<dynamic, string> _serializer;
        private Func<string, dynamic> _deserializer;

        public BasicSerializer()
        {
            if (typeof(T) == typeof(string))
            {
                _serializer = t => t;
            }
            else if (typeof(T) == typeof(int))
            {
                _serializer = t => t.ToString();
            }
            else
            {
                throw new SerializerRequiredException(typeof(T), typeof(string));
            }
        }

        public string Serialize(T t)
        {
            return _serializer(t);
        }
    }
}
