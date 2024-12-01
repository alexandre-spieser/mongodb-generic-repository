namespace CoreUnitTests.Infrastructure;

using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

public static class FilterDefinitionExtensions
{
    public static string RenderToJson<TDocument>(this FilterDefinition<TDocument> filter)
        => filter.Render(new RenderArgs<TDocument>(BsonSerializer.SerializerRegistry.GetSerializer<TDocument>(), BsonSerializer.SerializerRegistry)).ToJson();

    public static bool EquivalentTo<TDocument>(this FilterDefinition<TDocument> filter, FilterDefinition<TDocument> other)
        => filter.RenderToJson() == other.RenderToJson();
}
