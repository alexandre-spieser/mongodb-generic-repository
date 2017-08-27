using MongoDbGenericRepository.Models;
using NUnit.Framework;

namespace IntegrationTests
{
    public class ReadTestsDocument : Document
    {
        public ReadTestsDocument()
        {
            Version = 2;
        }
        public string SomeContent { get; set; }
    }

    [TestFixture]
    public class ReadTests
    {
    }
}
