using IntegrationTests.Infrastructure;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Models;
using NUnit.Framework;
using System;

namespace IntegrationTests
{
    public class TKeyTestDocument : TestDoc<Guid>
    {
    }

    [TestFixture]
    public class CRUDTKeyTests : MongoDBTestBase<TKeyTestDocument, Guid>
    {
        public override string GetClassName()
        {
            return "CreateTKeyTests";
        }
    }
}
