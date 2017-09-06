using System;
using System.Collections.Generic;
using System.Text;

namespace NeoSmart.Redis
{
    /// <summary>
    /// The default <code>ISerializer</code> implementation used by <see cref="RDictionary{K, V}"/> for key serialization.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BasicSerializer<T> : ISerializer<T, string>
    {
        private Func<dynamic, string> _serializer;

        /// <summary>
        /// Creates a new <see cref="BasicSerializer{T}"/> instance.
        /// </summary>
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

        /// <summary>
        /// Serializes <paramref name="t"/> to a <code>string</code>.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public string Serialize(T t)
        {
            return _serializer(t);
        }
    }
}
