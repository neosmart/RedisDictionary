using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using StackExchange.Redis;

namespace NeoSmart.Redis
{
    public class BasicBidirectionalSerializer<T> : ISerializer<T, RedisValue>, IDeserializer<RedisValue, T>, IBidirectionalSerialiver<T, RedisValue>
    {
        private Func<dynamic, string> _serializer;
        private Func<string, dynamic> _deserializer;
        private static Type[] ImplictTypes = new Type[] { typeof(bool), typeof(bool?), typeof(int), typeof(int?), typeof(long), typeof(long?), typeof(double), typeof(double?), typeof(string), typeof(byte[]) };

        public BasicBidirectionalSerializer()
        {
            if (!ImplictTypes.Contains(typeof(T)))
            {
                throw new SerializerRequiredException(typeof(T), typeof(RedisValue));
            }
        }

        public RedisValue Serialize(T t)
        {
            return (RedisValue) (dynamic)t;
        }

        public T Deserialize(RedisValue s)
        {
            return (T)(dynamic)s;
        }
    }
}
