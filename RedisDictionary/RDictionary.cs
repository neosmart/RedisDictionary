using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Collections;
using NeoSmart.Hashing.XXHash;
using System.Linq;

namespace NeoSmart.Redis
{
    /// <summary>
    /// A redis-backed dictionary, mapping <typeparamref name="K"/> to <typeparamref name="V"/>, with serialization provided by <code>BasicSerializer</code> and
    /// <code>BasicBidirectionalSerializer</code> for <typeparamref name="K"/> and <typeparamref name="V"/>, respectively.
    /// </summary>
    /// <typeparam name="K">The type to use as a key</typeparam>
    /// <typeparam name="V">The type to use as a value</typeparam>
    /// <exception cref="SerializerRequiredException">Thrown if type <typeparamref name="K"/> cannot be serialized by <code>BasicSerializer</code>.</exception>
    /// <exception cref="SerializerRequiredException">Thrown if type <typeparamref name="V"/> cannot be serialized by <code>BasicBidirectionalSerializer</code>.</exception>
    public class RDictionary<K, V> : RDictionary<K, V, BasicSerializer<K>, BasicBidirectionalSerializer<V>>
    {
        /// <summary>
        /// Creates a new redis-backed dictionary mapping <code>K</code> to <code>V</code>
        /// </summary>
        /// <param name="name">The unique name for the collection. Instances with the same name share a view of the same data.</param>
        /// <param name="connectionOptions">Optional parameter for specifying the settings used for the underlying redis connection.</param>
        /// <param name="database">The optional numeric id of the numbered database to use in the redis instance. Created if does not already exist.</param>
        public RDictionary(string name, ConfigurationOptions connectionOptions = null, int database = -1)
            : base(name, connectionOptions, database)
        {
        }

        /// <summary>
        /// Creates a new redis-backed dictionary mapping <code>K</code> to <code>V</code>
        /// </summary>
        /// <param name="name">The unique name for the collection. Instances with the same name share a view of the same data.</param>
        /// <param name="connectionString">Optional parameter for specifying the settings used for the underlying redis connection. <seealso cref="StackExchange.Redis.ConfigurationOptions"/></param>
        /// <param name="database">The optional numeric id of the numbered database to use in the redis instance. Created if does not already exist.</param>
        public RDictionary(string name, string connectionString, int database = -1)
            : base(name, connectionString, database)
        {
        }
    }

    //We can only define a single variant that takes only one serializer, either we specify only the key serializer or only the value serializer
#if false
    /// <summary>
    /// A redis-backed dictionary, mapping <typeparamref name="K"/> to <typeparamref name="V"/>, with <typeparamref name="K"/> serialization provided by <typeparamref name="S1"/>
    /// </summary>
    /// <typeparam name="K">The type to use as a key</typeparam>
    /// <typeparam name="V">The type to use as a value</typeparam>
    /// <typeparam name="S1">The <code>NeoSmart.Redis.ISerializer</code> type used to convert <typeparamref name="K"/> to
    /// a <code>string</code>. Required if serialization of <typeparamref name="K"/> is not provided by <code>NeoSmart.Redis.BasicSerializer</code></typeparam>
    /// <exception cref="SerializerRequiredException">Thrown if type <typeparamref name="V"/> cannot be serialized by <code>BasicBidirectionalSerializer</code>.</exception>
    public class RDictionary<K, V, S1> : RDictionary<K, V, S1, BasicBidirectionalSerializer<V>>
        where S1 : ISerializer<K, string>, new()
    {
        /// <summary>
        /// Creates a new redis-backed dictionary mapping <code>K</code> to <code>V</code>
        /// </summary>
        /// <param name="name">The unique name for the collection. Instances with the same name share a view of the same data.</param>
        /// <param name="connectionOptions">Optional parameter for specifying the settings used for the underlying redis connection.</param>
        /// <param name="database">The optional numeric id of the numbered database to use in the redis instance. Created if does not already exist.</param>
        public RDictionary(string name, ConfigurationOptions connectionOptions = null, int database = -1)
            : base(name, connectionOptions, database)
        {
        }

        /// <summary>
        /// Creates a new redis-backed dictionary mapping <code>K</code> to <code>V</code>
        /// </summary>
        /// <param name="name">The unique name for the collection. Instances with the same name share a view of the same data.</param>
        /// <param name="connectionString">Optional parameter for specifying the settings used for the underlying redis connection. <seealso cref="StackExchange.Redis.ConfigurationOptions"/></param>
        /// <param name="database">The optional numeric id of the numbered database to use in the redis instance. Created if does not already exist.</param>
        public RDictionary(string name, string connectionString, int database = -1)
            : base(name, connectionString, database)
        {
        }
    }
#endif

    /// <summary>
    /// A redis-backed dictionary, mapping <typeparamref name="K"/> to <typeparamref name="V"/>, with <typeparamref name="V"/> serialization provided by <typeparamref name="S2"/>
    /// </summary>
    /// <typeparam name="K">The type to use as a key</typeparam>
    /// <typeparam name="V">The type to use as a value</typeparam>
    /// <typeparam name="S2">The <code>NeoSmart.Redis.ISerializer</code> type used to serialize <typeparamref name="V"/> as a <code>RedisValue</code>.
    /// Required if serialization of <typeparamref name="V"/> is not provided by <code>NeoSmart.Redis.BasicBidirectionalSerializer</code></typeparam>
    /// <exception cref="SerializerRequiredException">Thrown if type <typeparamref name="K"/> cannot be serialized by <code>BasicSerializer</code>.</exception>
    public class RDictionary<K, V, S2> : RDictionary<K, V, BasicSerializer<K>, S2>
        where S2 : IBidirectionalSerialiver<V, RedisValue>, new()
    {
        /// <summary>
        /// Creates a new redis-backed dictionary mapping <code>K</code> to <code>V</code>
        /// </summary>
        /// <param name="name">The unique name for the collection. Instances with the same name share a view of the same data.</param>
        /// <param name="connectionOptions">Optional parameter for specifying the settings used for the underlying redis connection.</param>
        /// <param name="database">The optional numeric id of the numbered database to use in the redis instance. Created if does not already exist.</param>
        public RDictionary(string name, ConfigurationOptions connectionOptions = null, int database = -1)
            : base(name, connectionOptions, database)
        {
        }

        /// <summary>
        /// Creates a new redis-backed dictionary mapping <code>K</code> to <code>V</code>
        /// </summary>
        /// <param name="name">The unique name for the collection. Instances with the same name share a view of the same data.</param>
        /// <param name="connectionString">Optional parameter for specifying the settings used for the underlying redis connection. <seealso cref="StackExchange.Redis.ConfigurationOptions"/></param>
        /// <param name="database">The optional numeric id of the numbered database to use in the redis instance. Created if does not already exist.</param>
        public RDictionary(string name, string connectionString, int database = -1)
            : base(name, connectionString, database)
        {
        }
    }

    /// <summary>
    /// A redis-backed dictionary, mapping <typeparamref name="K"/> to <typeparamref name="V"/>, with <typeparamref name="K"/> serialized to a <code>System.String</code> via an instance of
    /// <typeparamref name="S1"/> and <typeparamref name="V"/> serialized to a <code>RedisValue</code> via an instance of <typeparamref name="S2"/>.
    /// </summary>
    /// <typeparam name="K">The type to use as a key</typeparam>
    /// <typeparam name="V">The type to use as a value</typeparam>
    /// <typeparam name="S1">The <code>NeoSmart.Redis.ISerializer</code> type used to convert <typeparamref name="K"/> to
    /// a <code>string</code>. Required if serialization of <typeparamref name="K"/> is not provided by <code>NeoSmart.Redis.BasicSerializer</code></typeparam>
    /// <typeparam name="S2">The <code>NeoSmart.Redis.ISerializer</code> type used to serialize <typeparamref name="V"/> as a <code>RedisValue</code>.
    /// Required if serialization of <typeparamref name="V"/> is not provided by <code>NeoSmart.Redis.BasicBidirectionalSerializer</code></typeparam>
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

        /// <summary>
        /// Creates a new redis-backed dictionary mapping <code>K</code> to <code>V</code>
        /// </summary>
        /// <param name="name">The unique name for the collection. Instances with the same name share a view of the same data.</param>
        /// <param name="connectionOptions">Optional parameter for specifying the settings used for the underlying redis connection.</param>
        /// <param name="database">The optional numeric id of the numbered database to use in the redis instance. Created if does not already exist.</param>
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

        /// <summary>
        /// Creates a new redis-backed dictionary mapping <code>K</code> to <code>V</code>
        /// </summary>
        /// <param name="name">The unique name for the collection. Instances with the same name share a view of the same data.</param>
        /// <param name="connectionString">Optional parameter for specifying the settings used for the underlying redis connection. <seealso cref="StackExchange.Redis.ConfigurationOptions"/></param>
        /// <param name="database">The optional numeric id of the numbered database to use in the redis instance. Created if does not already exist.</param>
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

        /// <summary></summary>
        public V this[K key] { get => Get(key); set => Set(key, value); }

        /// <summary></summary>
        public ICollection<K> Keys => throw new NotImplementedException();

        /// <summary></summary>
        public ICollection<V> Values => throw new NotImplementedException();

        /// <summary></summary>
        public int Count => throw new NotImplementedException();

        /// <summary></summary>
        public bool IsReadOnly => false;

        /// <summary></summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(K key, V value)
        {
            Set(key, value);
        }

        /// <summary></summary>
        /// <param name="item"></param>
        public void Add(KeyValuePair<K, V> item)
        {
            Set(item.Key, item.Value);
        }

        /// <summary>
        /// Removes all keys belonging to this collection from the redis backend.
        /// </summary>
        public void Clear()
        {
            throw new NotImplementedException();
        }

        /// <summary></summary>
        public bool Contains(KeyValuePair<K, V> item)
        {
            throw new NotImplementedException();
        }

        /// <summary></summary>
        public bool ContainsKey(K key)
        {
            throw new NotImplementedException();
        }

        /// <summary></summary>
        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Closes the underlying redis connection and frees resources.
        /// </summary>
        public void Dispose()
        {
            if (_redis != null)
            {
                _redis.Dispose();
                _redis = null;
                _db = null;
            }
        }

        /// <summary></summary>
        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        /// <summary></summary>
        public bool Remove(K key)
        {
            throw new NotImplementedException();
        }

        /// <summary></summary>
        public bool Remove(KeyValuePair<K, V> item)
        {
            throw new NotImplementedException();
        }

        /// <summary></summary>
        public bool TryGetValue(K key, out V value)
        {
            throw new NotImplementedException();
        }

        /// <summary></summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
