using System;
using System.Collections.Concurrent;
using MongoDB.Bson.Serialization;

namespace MongoDbGenericRepository.Internals
{
    /// <summary>
    /// An <see cref="IBsonSerializationProvider"/> that can handle multiple serializer registration calls.
    /// </summary>
    internal class RepositorySerializationProvider : IBsonSerializationProvider
    {
        private static volatile RepositorySerializationProvider _instance;
        private static readonly object LockObject = new object();

        private readonly ConcurrentDictionary<Type, IBsonSerializer> _cache;

        private RepositorySerializationProvider()
        {
            _cache = new ConcurrentDictionary<Type, IBsonSerializer>();
        }

        public static RepositorySerializationProvider Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (LockObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new RepositorySerializationProvider();
                            BsonSerializer.RegisterSerializationProvider(_instance);
                        }
                    }
                }

                return _instance;
            }
        }

        /// <inheritdoc />
        public IBsonSerializer GetSerializer(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return _cache.TryGetValue(type, out var serializer) ? serializer : null;
        }

        internal void RegisterSerializer<T>(IBsonSerializer<T> serializer) =>
            RegisterSerializer(typeof(T), serializer);

        internal void RegisterSerializer(Type type, IBsonSerializer serializer) =>
            _cache.TryAdd(type, serializer);
    }
}