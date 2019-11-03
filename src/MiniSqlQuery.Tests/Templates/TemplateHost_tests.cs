#region License
// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion
using MiniSqlQuery.Core;
using MiniSqlQuery.PlugIns.TemplateViewer;
using NUnit.Framework;

using Rhino.Mocks;

namespace MiniSqlQuery.Tests.Templates
{
    [TestFixture]
    public class TemplateHost_tests
    {
        private TemplateHost _host;
        IApplicationServices _services;
        IDatabaseInspector _databaseInspector;

        [SetUp]
        public void TestSetUp()
        {
            _services = MockRepository.GenerateStub<IApplicationServices>();
            _databaseInspector = MockRepository.GenerateStub<IDatabaseInspector>();
            _host = new TemplateHost(_services, _databaseInspector);
        }

        [Test]
        public void UserName()
        {
            Assert.That(_host.UserName, Is.Not.Null);
        }

        [Test]
        public void ToPascalCase()
        {
            Assert.That(_host.ToPascalCase(null), Is.EqualTo(""));
            Assert.That(_host.ToPascalCase("name"), Is.EqualTo("Name"));
            Assert.That(_host.ToPascalCase("foo baa"), Is.EqualTo("FooBaa"));
            Assert.That(_host.ToPascalCase("user_id"), Is.EqualTo("UserId"));
            Assert.That(_host.ToPascalCase("firstName"), Is.EqualTo("FirstName"));
            Assert.That(_host.ToPascalCase("FirstName"), Is.EqualTo("FirstName"));
            Assert.That(_host.ToPascalCase("id"), Is.EqualTo("Id"));
            Assert.That(_host.ToPascalCase("ID"), Is.EqualTo("ID"));
        }

        [Test]
        public void ToCamelCase()
        {
            Assert.That(_host.ToCamelCase(null), Is.EqualTo(""));
            Assert.That(_host.ToCamelCase("name"), Is.EqualTo("name"));
            Assert.That(_host.ToCamelCase("Foo"), Is.EqualTo("foo"));
            Assert.That(_host.ToCamelCase("bat man"), Is.EqualTo("batMan"));
            Assert.That(_host.ToCamelCase("bat_man"), Is.EqualTo("batMan"));
            Assert.That(_host.ToCamelCase("XYZCodeThing"), Is.EqualTo("xyzCodeThing"));
            Assert.That(_host.ToCamelCase("XYZ Code Thing"), Is.EqualTo("xyzCodeThing"));
            Assert.That(_host.ToCamelCase("user_name"), Is.EqualTo("userName"));
            Assert.That(_host.ToCamelCase("id"), Is.EqualTo("id"));
        }
    }
}