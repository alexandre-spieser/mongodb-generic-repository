using CoreIntegrationTests.Infrastructure;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;
using MongoDbGenericRepository.Models;
using System;

namespace CoreIntegrationTests
{
    #region Guid Type 

    [CollectionName("TestingCNameAttrPartTKey")]
    public class CoreTKeyPartitionedCollectionNameDoc : TestDoc<Guid>, IPartitionedDocument
    {
        public CoreTKeyPartitionedCollectionNameDoc()
        {
            PartitionKey = "CoreTestPartitionKey";
        }

        public string PartitionKey { get; set; }
    }

    public class CRUDTKeyPartitionedCollectionNameAttributeTests : MongoDbTKeyDocumentTestBase<CoreTKeyPartitionedCollectionNameDoc, Guid>
    {
        public CRUDTKeyPartitionedCollectionNameAttributeTests(MongoDbTestFixture<CoreTKeyPartitionedCollectionNameDoc, Guid> fixture) : base(fixture)
        {

        }

        public override string GetClassName()
        {
            return "CoreCRUDTKeyPartitionedCollectionNameAttributeTests";
        }
    }
    public class
        CRUDTKeyPartitionedCollectionNameAttributeTestDefaultRepositorys : DefaultBaseTKeyMongodbRepositoryTestBase<
            CoreTKeyPartitionedCollectionNameDoc, Guid>
    {
        public CRUDTKeyPartitionedCollectionNameAttributeTestDefaultRepositorys(
            MongoDbTestFixture<CoreTKeyPartitionedCollectionNameDoc, Guid> fixture) : base(fixture)
        {
        }

        public override string GetClassName()
        {
            return nameof(CRUDTKeyPartitionedCollectionNameAttributeTestDefaultRepositorys);
        }
    }
    #endregion Guid Type


    #region ObjectId Type

    [CollectionName("TestingCNameAttrPartObjectId")]
    public class CoreObjectIdPartitionedCollectionNameDoc : TestDoc<ObjectId>, IPartitionedDocument
    {
        public CoreObjectIdPartitionedCollectionNameDoc()
        {
            PartitionKey = "CoreTestPartitionKeyObjectId";
        }

        public string PartitionKey { get; set; }
    }

    public class CRUDObjectIdPartitionedCollectionNameAttributeTests : MongoDbTKeyDocumentTestBase<CoreObjectIdPartitionedCollectionNameDoc, ObjectId>
    {
        public CRUDObjectIdPartitionedCollectionNameAttributeTests(MongoDbTestFixture<CoreObjectIdPartitionedCollectionNameDoc, ObjectId> fixture) : base(fixture)
        {

        }

        public override string GetClassName()
        {
            return "CoreCRUDTKeyPartitionedCollectionNameAttributeTests";
        }
    }
    public class CRUDObjectIdPartitionedCollectionNameAttributeTestDefaultRepositorys :
        DefaultBaseTKeyMongodbRepositoryTestBase<CoreObjectIdPartitionedCollectionNameDoc, ObjectId>
    {
        public CRUDObjectIdPartitionedCollectionNameAttributeTestDefaultRepositorys(
            MongoDbTestFixture<CoreObjectIdPartitionedCollectionNameDoc, ObjectId> fixture) : base(fixture)
        {
        }

        public override string GetClassName()
        {
            return nameof(CRUDObjectIdPartitionedCollectionNameAttributeTestDefaultRepositorys);
        }
    }

    #endregion ObjectId Type
}
