using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Collections;
using NeoSmart.Hashing.XXHash;
using System.Linq;

namespace NeoSmart.Redis
{
    public class RDictionary<K, V> : RDictionary<K, V, BasicSerializer<K>, BasicBidirectionalSerializer<V>>
    {
        public RDictionary(string name, ConfigurationOptions connectionOptions = null, int database = -1)
            : base(name, connectionOptions, database)
        { }
    }

    //We can only define a single variant that takes only one serializer, either we specify only the key serializer or only the value serializer
#if false
    public class RDictionary<K, V, S1> : RDictionary<K, V, S1, BasicBidirectionalSerializer<V>>
        where S1 : ISerializer<K, string>, new()
    {
        public RDictionary(string name, ConfigurationOptions connectionOptions = null, int database = -1)
            : base(name, connectionOptions, database)
        { }
    }
#endif

    public class RDictionary<K, V, S2> : RDictionary<K, V, BasicSerializer<K>, S2>
        where S2 : IBidirectionalSerialiver<V, RedisValue>, new()
    {
        public RDictionary(string name, ConfigurationOptions connectionOptions = null, int database = -1)
            : base(name, connectionOptions, database)
        { }
    }

    /// <summary>
    /// A redis-backed dictionary
    /// </summary>
    public class RDictionary<K, V, S1, S2> : IDictionary<K, V>, IDisposable
        where S1: ISerializer<K, string>, new()
        where S2: IBidirectionalSerialiver<V, RedisValue>, new()
    {
        ConnectionMultiplexer _redis = null;
        Func<dynamic, string> _kserializer;
        Func<dynamic, RedisValue> _vserializer;
        Func<dynamic, V> _vdeserializer;

        uint _hash;
        IDatabase _db;

        public RDictionary(string name, ConfigurationOptions connectionOptions = null, int database = -1)
        {
            var options = connectionOptions ?? ConfigurationOptions.Parse("localhost");
            _redis = ConnectionMultiplexer.Connect(options);
            _hash = XXHash32.Hash(name);
            _db = _redis.GetDatabase(database);

            try
            {
                var s1 = new S1();
                var s2 = new S2();

                _kserializer = k => s1.Serialize(k);
                _vserializer = v => s2.Serialize(v);
                _vdeserializer = v => s2.Deserialize(v);
            }
            catch (System.Reflection.TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        public RDictionary(string name, string connectionString, int database = -1)
            : this(name, ConfigurationOptions.Parse(connectionString), database)
        {
        }

        private string CreateKey(K key) => $"{_hash}-{_kserializer(key)}";

        private void Set(K key, V value)
        {
            _db.StringSet(CreateKey(key), _vserializer(value));
        }

        private V Get(K key)
        {
            return _vdeserializer(_db.StringGet(CreateKey(key)));
        }

        public V this[K key] { get => Get(key); set => Set(key, value); }

        public ICollection<K> Keys => throw new NotImplementedException();

        public ICollection<V> Values => throw new NotImplementedException();

        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => false;

        public void Add(K key, V value)
        {
            Set(key, value);
        }

        public void Add(KeyValuePair<K, V> item)
        {
            Set(item.Key, item.Value);
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<K, V> item)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(K key)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (_redis != null)
            {
                _redis.Dispose();
                _redis = null;
                _db = null;
            }
        }

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(K key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<K, V> item)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(K key, out V value)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
