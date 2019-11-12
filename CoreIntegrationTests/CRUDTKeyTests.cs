using CoreIntegrationTests.Infrastructure;
using System;

namespace CoreIntegrationTests
{
    public class CoreTKeyTestDocument : TestDoc<Guid>
    {
    }

    public class CRUDTKeyTests : MongoDbTKeyDocumentTestBase<CoreTKeyTestDocument, Guid>
    {
        public CRUDTKeyTests(MongoDbTestFixture<CoreTKeyTestDocument, Guid> fixture) : base(fixture)
        {

        }

        public override string GetClassName()
        {
            return "CreateTKeyTests";
        }
    }
    public class CRUDTKeyTestDefaultRepositorys : DefaultBaseTKeyMongodbRepositoryTestBase<CoreTKeyTestDocument, Guid>
    {
        public CRUDTKeyTestDefaultRepositorys(MongoDbTestFixture<CoreTKeyTestDocument, Guid> fixture) : base(fixture)
        {
        }

        public override string GetClassName()
        {
            return nameof(CRUDTKeyTestDefaultRepositorys);
        }
    }
}
