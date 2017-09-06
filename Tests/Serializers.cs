using NeoSmart.Redis;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    public struct CustomSerializer : IBidirectionalSerialiver<CustomType, RedisValue>, ISerializer<CustomType, string>, ISerializer<int, string>
    {
        public CustomType Deserialize(string s)
        {
            return new CustomType
            {
                Value = int.Parse(s)
            };
        }

        public CustomType Deserialize(RedisValue s)
        {
            var result = new CustomType();
            if (!s.TryParse(out result.Value))
            {
                throw new Exception("Unable to deserialize!");
            }

            return result;
        }

        public string Serialize(CustomType t)
        {
            return t.Value.ToString();
        }

        public string Serialize(int t)
        {
            return t.ToString();
        }

        RedisValue ISerializer<CustomType, RedisValue>.Serialize(CustomType t)
        {
            throw new NotImplementedException();
        }
    }
}
