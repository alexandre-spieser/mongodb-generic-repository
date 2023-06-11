using MongoDB.Bson.Serialization.Conventions;
using System.Threading;

namespace CoreIntegrationTests.Infrastructure
{
    internal static class MongoDbConfig
    {
        private static bool _initialized;
        private static object _initializationLock = new();
        private static object _initializationTarget;

        public static void EnsureConfigured()
        {
            EnsureConfiguredImpl();
        }

        private static void EnsureConfiguredImpl()
        {
            LazyInitializer.EnsureInitialized(ref _initializationTarget, ref _initialized, ref _initializationLock, () =>
            {
                Configure();
                return null;
            });
        }

        private static void Configure()
        {
            RegisterConventions();
        }

        private static void RegisterConventions()
        {
            var pack = new ConventionPack
            {
                new IgnoreIfNullConvention(false),
                new CamelCaseElementNameConvention(),
            };
        }

    }
}
