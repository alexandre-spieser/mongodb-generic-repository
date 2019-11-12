﻿using CoreIntegrationTests.Infrastructure;
using MongoDbGenericRepository.Attributes;
using MongoDbGenericRepository.Models;
using System;

namespace CoreIntegrationTests
{
    [CollectionName("CoreTestingCNameAttrPart")]
    public class CorePartitionedCollectionNameDoc : TestDoc, IPartitionedDocument
    {
        public CorePartitionedCollectionNameDoc()
        {
            PartitionKey = "CoreTestPartitionKeyCollectionName";
        }

        public string PartitionKey { get; set; }
    }

    public class CRUDPartitionedCollectionNameAttributeTests : MongoDbDocumentTestBase<CorePartitionedCollectionNameDoc>
    {
        public CRUDPartitionedCollectionNameAttributeTests(MongoDbTestFixture<CorePartitionedCollectionNameDoc, Guid> fixture) : base(fixture)
        {

        }

        public override string GetClassName()
        {
            return "CoreCRUDPartitionedCollectionNameAttributeTests";
        }
    }
    public class CRUDPartitionedCollectionNameAttributeTestDefaultRepositorys : DefaultBaseMongodbRepositoryTestBase<
            CorePartitionedCollectionNameDoc>
    {
        public CRUDPartitionedCollectionNameAttributeTestDefaultRepositorys(
            MongoDbTestFixture<CorePartitionedCollectionNameDoc, Guid> fixture) : base(fixture)
        {
        }

        public override string GetClassName()
        {
            return nameof(CRUDPartitionedCollectionNameAttributeTestDefaultRepositorys);
        }
    }

}
