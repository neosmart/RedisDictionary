using System;
using System.Collections.Generic;
using System.Text;

namespace NeoSmart.Redis
{
    public interface ISerializer<T, S>
    {
        S Serialize(T t);
    }

    public  interface IDeserializer<S, T>
    {
        T Deserialize(S s);
    }

    public interface IBidirectionalSerialiver<T, S> : ISerializer<T, S>, IDeserializer<S, T>
    {
    }
}
