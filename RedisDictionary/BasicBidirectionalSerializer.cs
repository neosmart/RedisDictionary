using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using StackExchange.Redis;

namespace NeoSmart.Redis
{
    /// <summary>
    /// Provides serialization to/from <typeparamref name="T"/> and <code>RedisValue</code>.
    /// </summary>
    /// <remarks>Used as the default type serializer for <code>RDictionary</code> when an <code>IBidirectionalSerializer&lt;T, RedisValue&gt;</code> is not supplied.</remarks>
    /// <typeparam name="T">The type to serialize to/from <code>RedisValue</code></typeparam>
    public class BasicBidirectionalSerializer<T> : ISerializer<T, RedisValue>, IDeserializer<RedisValue, T>, IBidirectionalSerialiver<T, RedisValue>
    {
        private static Type[] ImplictTypes = new Type[] { typeof(bool), typeof(bool?), typeof(int), typeof(int?), typeof(long), typeof(long?), typeof(double), typeof(double?), typeof(string), typeof(byte[]) };

        /// <summary>
        /// Creates a new <see cref="BasicBidirectionalSerializer{T}"/> instance
        /// </summary>
        public BasicBidirectionalSerializer()
        {
            if (!ImplictTypes.Contains(typeof(T)))
            {
                throw new SerializerRequiredException(typeof(T), typeof(RedisValue));
            }
        }

        /// <summary>
        /// Convert <paramref name="t"/> from <code>T</code> to <code>RedisValue</code>
        /// </summary>
        /// <param name="t">The value to be serialized.</param>
        /// <returns></returns>
        public RedisValue Serialize(T t)
        {
            return (RedisValue) (dynamic)t;
        }

        /// <summary>
        /// Converts a <code>RedisValue</code> to its equilavent <code>T</code> value.
        /// </summary>
        /// <param name="s">The serialized value to be converted/deserialized.</param>
        /// <returns></returns>
        public T Deserialize(RedisValue s)
        {
            return (T)(dynamic)s;
        }
    }
}
