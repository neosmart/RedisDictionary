using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Collections;
using NeoSmart.Hashing.XXHash;
using System.Linq;

namespace NeoSmart.Redis
{
    /// <summary>
    /// A redis-backed dictionary
    /// </summary>
    public class RDictionary<K, V> : IDictionary<K, V>, IDisposable
    {
        ConnectionMultiplexer _redis = null;
        Func<dynamic, string> _kserializer;
        Func<dynamic, RedisValue> _vserializer;
        Func<dynamic, V> _vdeserializer;

        uint _hash;
        IDatabase _db;

        public RDictionary(string name, ConfigurationOptions connectionOptions = null, int database = -1, Serializer<K, string> keySerializer = null, Serializer<V, RedisValue> valueSerializer = null)
        {
            var options = connectionOptions ?? ConfigurationOptions.Parse("localhost");
            _redis = ConnectionMultiplexer.Connect(options);
            _hash = XXHash32.Hash(name);
            _db = _redis.GetDatabase(database);

            SetKeySerializer(keySerializer);
            SetValueSerializer(valueSerializer);
        }

        public RDictionary(string name, string connectionString, int database = -1)
            : this(name, ConfigurationOptions.Parse(connectionString), database)
        {
        }

        private void SetKeySerializer(Serializer<K, string> serializer)
        {
            if (serializer != null)
            {
                _kserializer = k => serializer.Serialize(k);
                return;
            }

            if (typeof(K) == typeof(string))
            {
                _kserializer = k => k;
            }
            else
            {
                throw new SerializerRequiredException(typeof(K), typeof(string));
            }
        }

        private void SetValueSerializer(Serializer<V, RedisValue> serializer)
        {
            if (serializer != null)
            {
                _vserializer = v => serializer.Serialize(v);
                return;
            }

            var implictTypes = new Type[] { typeof(bool), typeof(bool?), typeof(int), typeof(int?), typeof(long), typeof(long?), typeof(double), typeof(double?), typeof(string), typeof(byte[]) };
            if (implictTypes.Contains(typeof(V)))
            {
                _vserializer = v => v;
            }
            else
            {
                throw new SerializerRequiredException(typeof(V), typeof(RedisValue));
            }

            if (implictTypes.Contains(typeof(V)))
            {
                _vdeserializer = v => v;
            }
            else
            {
                throw new SerializerRequiredException(typeof(RedisValue), typeof(V));
            }
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
