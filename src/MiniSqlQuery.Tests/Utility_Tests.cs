using MiniSqlQuery.Core;
using NUnit.Framework;


namespace MiniSqlQuery.Tests
{
    [TestFixture]
    public class Utility_Tests
    {
        [Test]
        public void RenderSafeSchemaObjectName()
        {
            Assert.That(Utility.RenderSafeSchemaObjectName(null, "table"), Is.EqualTo("[table]"));
            Assert.That(Utility.RenderSafeSchemaObjectName("", "table"), Is.EqualTo("[table]"));
            Assert.That(Utility.RenderSafeSchemaObjectName("dbo", "table"), Is.EqualTo("[dbo].[table]"));
            Assert.That(Utility.RenderSafeSchemaObjectName("dbo", "table name"), Is.EqualTo("[dbo].[table name]"));
            Assert.That(Utility.RenderSafeSchemaObjectName("some schema", "table name"), Is.EqualTo("[some schema].[table name]"));
            Assert.That(Utility.RenderSafeSchemaObjectName("rights", "Right"), Is.EqualTo("[rights].[Right]"));
        }
    }
}