using System;
using System.Collections.Generic;
using System.Text;

namespace NeoSmart.Redis
{
    /// <summary>
    /// An interface for converting from <typeparamref name="T"/> to <typeparamref name="S"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="S"></typeparam>
    public interface ISerializer<T, S>
    {
        /// <summary>
        /// Serialize an instance of <typeparamref name="T"/> to an instance of <typeparamref name="S"/>
        /// </summary>
        /// <param name="t"></param>
        /// <returns><paramref name="t"/> serialized to type <typeparamref name="S"/></returns>
        S Serialize(T t);
    }

    /// <summary>
    /// An interface for converting back from <typeparamref name="S"/> to <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="S"></typeparam>
    /// <typeparam name="T"></typeparam>
    public  interface IDeserializer<S, T>
    {
        /// <summary>
        /// Serialize an instance of <typeparamref name="T"/> to an instance of <typeparamref name="S"/>
        /// </summary>
        /// <param name="s"></param>
        /// <returns><paramref name="s"/> deserialized to type <typeparamref name="T"/></returns>
        T Deserialize(S s);
    }

    /// <summary>
    /// An interface for bidirectional (serialize/deserialize) conversion between <typeparamref name="T"/> and <typeparamref name="S"/>.
    /// </summary>
    /// <typeparam name="T"/>
    /// <typeparam name="S"/>
    public interface IBidirectionalSerialiver<T, S> : ISerializer<T, S>, IDeserializer<S, T>
    {
    }
}
