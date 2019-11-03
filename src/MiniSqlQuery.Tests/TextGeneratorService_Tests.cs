using MiniSqlQuery.PlugIns.TextGenerator;
using NUnit.Framework;


// ReSharper disable InconsistentNaming

namespace MiniSqlQuery.Tests
{
    [TestFixture]
    public class TextGeneratorService_Tests
    {
        [Test]
        public void Simple_text_gen()
        {
            var gen = new TextGeneratorService();
            string result = gen.Process("ClassName\r\n Foo\r\n Bar");
            Assert.That(result, Is.EqualTo(@"public class ClassName
{
public virtual string Foo { get; set; }
public virtual string Bar { get; set; }
}
"));
        }

        [Test]
        public void Simple_text_gen_converts_to_PascalCase()
        {
            var gen = new TextGeneratorService();
            string result = gen.Process("Some other class\r\n Ba da bing");
            Assert.That(result, Is.EqualTo(@"public class SomeOtherClass
{
public virtual string BaDaBing { get; set; }
}
"));
        }

        [Test]
        public void if_a_property_ends_with_Id_its_an_int()
        {
            var gen = new TextGeneratorService();
            string result = gen.Process("Some other class\r\n My Id");
            Assert.That(result, Is.EqualTo(@"public class SomeOtherClass
{
public virtual int MyId { get; set; }
}
"));
        }
    }
}