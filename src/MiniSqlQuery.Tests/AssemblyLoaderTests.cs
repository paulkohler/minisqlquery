using MiniSqlQuery.Core;
using NUnit.Framework;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MiniSqlQuery.Tests
{
	[TestFixture(Author = "Robert Snyder", TestOf =typeof(AssemblyLoader))]
	public class AssemblyLoaderTests
	{
		[Test]
		public void Test_Loading_Plugins()
		{
			var location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			var plugingInstances = AssemblyLoader.GetInstances<IPlugIn>(location, "MiniSqlQuery.*").ToList();
			Assert.That(plugingInstances, Is.Not.Null.And.Count.GreaterThan(0));
		}
	}
}
