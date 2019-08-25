using Xunit;

namespace Techtalk.FM.Test.Utils
{
    /// <summary>
    /// This base Controller Test receives the Server Fixture and use the same database for all tests created
    /// </summary>
    [Collection("TestServerCollection")]
    public class BaseControllerTest
    {
        #region "  Properties  "

        /// <summary>
        /// The Server Fixture with current test database and server
        /// </summary>
        public readonly TestServerFixture Fixture;

        #endregion

        #region "  Constructors  "

        public BaseControllerTest(TestServerFixture fixture)
        {
            Fixture = fixture;
        }

        #endregion
    }
}
