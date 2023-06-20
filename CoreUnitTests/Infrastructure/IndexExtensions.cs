using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDbGenericRepository.Models;

namespace CoreUnitTests.Infrastructure;

public static class IndexExtensions
{
    public static bool EqualToJson<TDocument>(this IndexKeysDefinition<TDocument> keys, string json)
    {
        var indexModelRendered = RenderIndexModelKeys(keys);
        return indexModelRendered.Equals(json, System.StringComparison.Ordinal);
    }

    public static bool EqualTo(this IndexCreationOptions x, CreateIndexOptions y) =>
        x.Unique == y.Unique &&
        x.TextIndexVersion == y.TextIndexVersion &&
        x.SphereIndexVersion == y.SphereIndexVersion &&
        x.Sparse == y.Sparse &&
        x.Name == y.Name &&
        x.Min == y.Min &&
        x.Max == y.Max &&
        x.LanguageOverride == y.LanguageOverride &&
        x.ExpireAfter == y.ExpireAfter &&
        x.DefaultLanguage == y.DefaultLanguage &&
        x.Bits == y.Bits &&
        x.Background == y.Background &&
        x.Version == y.Version;

    public static bool EqualTo(this CreateIndexOptions x,  IndexCreationOptions y) =>
        x.Unique == y.Unique &&
        x.TextIndexVersion == y.TextIndexVersion &&
        x.SphereIndexVersion == y.SphereIndexVersion &&
        x.Sparse == y.Sparse &&
        x.Name == y.Name &&
        x.Min == y.Min &&
        x.Max == y.Max &&
        x.LanguageOverride == y.LanguageOverride &&
        x.ExpireAfter == y.ExpireAfter &&
        x.DefaultLanguage == y.DefaultLanguage &&
        x.Bits == y.Bits &&
        x.Background == y.Background &&
        x.Version == y.Version;

    private static string RenderIndexModelKeys<TDocument>(IndexKeysDefinition<TDocument> keys)
    {
        var indexModelRendered = keys.Render(
            BsonSerializer.SerializerRegistry.GetSerializer<TDocument>(),
            BsonSerializer.SerializerRegistry);

        var result = indexModelRendered.ToString();
        return result.Replace(" ", "");
    }


}
