using Xunit;

namespace Techtalk.FM.Test.Utils
{
    /// <summary>
    /// This class exists to guarantee that all tests will use the same test database and server.
    /// </summary>
    [CollectionDefinition("TestServerCollection")]
    public class TestServerCollection : ICollectionFixture<TestServerFixture> { }
}
