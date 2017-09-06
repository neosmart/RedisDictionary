using System;
using System.Collections.Generic;
using System.Text;

namespace NeoSmart.Redis
{
    public class Serializer<T,S>
    {
        private Func<T, S> _serializer;
        private Func<S, T> _deserializer;

        public Serializer(Func<T, S> serializer, Func<S, T> deserializer = null)
        {
            _serializer = serializer;
            _deserializer = deserializer;
        }

        public S Serialize(T t)
        {
            return _serializer(t);
        }

        public T Deserialize(S s)
        {
            return _deserializer(s);
        }
    }
}
