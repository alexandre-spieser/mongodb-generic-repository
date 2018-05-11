# MongoDbGenericRepository
An example of generic repository implementation using the MongoDB C# Sharp 2.0 driver (async)

Now available as a nuget package:
https://www.nuget.org/packages/MongoDbGenericRepository/

Covered by 200+ integration tests and counting.

The MongoDbGenericRepository is also used in [AspNetCore.Identity.MongoDbCore](https://github.com/alexandre-spieser/AspNetCore.Identity.MongoDbCore).

# Usage examples

This repository is meant to be inherited from. 

You are responsible for managing its lifetime, it is advised to setup this repository as a singleton.

Here is an example of repository usage, where the TestRepository is implementing 2 custom methods:

```csharp
    public interface ITestRepository : IBaseMongoRepository
    {
        void DropTestCollection<TDocument>();
        void DropTestCollection<TDocument>(string partitionKey);
    }
    
    public class TestRepository : BaseMongoRepository, ITestRepository
    {
        public TestRepository(string connectionString, string databaseName) : base(connectionString, databaseName)
        {
        }

        public void DropTestCollection<TDocument>()
        {
            MongoDbContext.DropCollection<TDocument>();
        }

        public void DropTestCollection<TDocument>(string partitionKey)
        {
            MongoDbContext.DropCollection<TDocument>(partitionKey);
        }
    }
```

## Instantiation

The repository can be instantiated like so:

```csharp
ITestRepository testRepository = new TestRepository(connectionString, "MongoDbTests");
```

If you prefer to reuse the same MongoDb database across your application, you can use the `MongoDatabase` from the MongoDb driver implementing the `IMongoDatabase` interface:

```csharp
var client = new MongoClient(connectionString);
var mongoDbDatabase = Client.GetDatabase(databaseName);
ITestRepository testRepository = new TestRepository(mongoDbDatabase);
```

## Adding documents
To add a document, its class must inherit from the `Document` class,  implement the `IDocument` or `IDocument<TKey>` interface:

```csharp
    public class MyDocument : Document
    {
        public MyDocument()
        {
            Version = 2; // you can bump the version of the document schema if you change it over time
        }
        public string SomeContent { get; set; }
    }
```

The `IDocument` and `IDocument<TKey>` interfaces can be seen below:

```csharp
    /// <summary>
    /// This class represents a basic document that can be stored in MongoDb.
    /// Your document must implement this class in order for the MongoDbRepository to handle them.
    /// </summary>
    public interface IDocument
    {
        Guid Id { get; set; }
        int Version { get; set; }
    }
    
    /// <summary>
    /// This class represents a basic document that can be stored in MongoDb.
    /// Your document must implement this class in order for the MongoDbRepository to handle them.
    /// </summary>
    public interface IDocument<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// The Primary Key, which must be decorated with the [BsonId] attribute 
        /// if you want the MongoDb C# driver to consider it to be the document ID.
        /// </summary>
        [BsonId]
        TKey Id { get; set; }
        /// <summary>
        /// A version number, to indicate the version of the schema.
        /// </summary>
        int Version { get; set; }
    }
```

## Partitioned collections
This repository also allows you to partition your document across multiple collections, this can be useful if you are running a SaaS application and want to keep good performance.

To use partitioned collections, you must define your documents using the PartitionedDocument class, which implements the IPartitionedDocument interface:
```csharp
    public class MyPartitionedDocument : PartitionedDocument
    {
        public MyPartitionedDocument(string myPartitionKey) : base(myPartitionKey)
        {
            Version = 1;
        }
        public string SomeContent { get; set; }
    }
```

This partitioned key will be used as a prefix to your collection name.
The collection name is derived from the name of the type of your document, is set to camel case, and is pluralized using a class taken from Humanizer (https://github.com/Humanizr/Humanizer).

```csharp
var myDoc = new MyPartitionedDocument("myPartitionKey");
_testRepository.AddOne(myDoc);
```

The above code will generate a collection named `myPartitionKey-myPartitionedDocuments`.

## CollectionName Attribute
It is now possible to change the collection name by using the `CollectionName` attribute:

```csharp
    [CollectionName("MyCollectionName")]
    public class MyDocument : Document
    {
        public MyDocument()
        {
            Version = 2;
        }
        public string SomeContent { get; set; }
    }
```
Documents of this type will be inserted into a collection named "MyCollectionName".

Please refer to the IntegrationTests (NET45) and CoreIntegrationTests (netstandard2.0) projects for more usage examples.

## Author
**Alexandre Spieser**

## License
mongodb-generic-repository is under MIT license - http://www.opensource.org/licenses/mit-license.php

The MIT License (MIT)

Copyright (c) 2016-2018 Alexandre Spieser

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

==============================================================================

Inflector (https://github.com/srkirkland/Inflector)
The MIT License (MIT)
Copyright (c) 2013 Scott Kirkland

==============================================================================

Humanizer (https://github.com/Humanizr/Humanizer)
The MIT License (MIT)
Copyright (c) 2012-2014 Mehdi Khalili (http://omar.io)

==============================================================================

## Copyright
Copyright © 2018
