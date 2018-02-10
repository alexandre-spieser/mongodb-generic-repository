using IntegrationTests.Infrastructure;
using NUnit.Framework;

namespace IntegrationTests
{
    public class TestDocument : TestDoc
    {
    }

    [TestFixture]
    public class CRUDTests : MongoDbDocumentTestBase<TestDocument>
    {
        public override string GetClassName()
        {
            return "CRUDTests";
        }
    }
}
