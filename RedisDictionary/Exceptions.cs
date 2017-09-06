using System;
using System.Collections.Generic;
using System.Text;

namespace NeoSmart.Redis
{
    /// <summary>
    /// An exception thrown when a type cannot be serialized by the default serializer.
    /// <seealso cref="BasicSerializer{T}"/>
    /// <seealso cref="BasicBidirectionalSerializer{T}"/>
    /// </summary>
    public class SerializerRequiredException: Exception
    {
        /// <summary>
        /// Creates a new <see cref="SerializerRequiredException"/> instance.
        /// </summary>
        /// <param name="t1">The type to serialize from.</param>
        /// <param name="t2">The type to serialie to.</param>
        public SerializerRequiredException(Type t1 ,Type t2)
            : base($"A serializer is required to convert from {t1} to {t2}!")
        {
        }
    }
}
