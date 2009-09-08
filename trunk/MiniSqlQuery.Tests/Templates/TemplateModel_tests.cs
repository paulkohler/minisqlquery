using System;
using System.Collections.Generic;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Template;
using MiniSqlQuery.PlugIns.TemplateViewer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

// ReSharper disable InconsistentNaming

namespace MiniSqlQuery.Tests.Templates
{
	[TestFixture]
	public class TemplateModel_tests
	{
		TemplateModel _model;
		IApplicationServices _services;
		IDatabaseInspector _databaseInspector;

		[SetUp]
		public void TestSetUp()
		{
			_services = MockRepository.GenerateStub<IApplicationServices>();
			_databaseInspector = MockRepository.GenerateStub<IDatabaseInspector>();
			_model = new TemplateModel(_services, _databaseInspector, new NVelocityWrapper());
		}

		// todo ParseErrorException
		
		[Test]
		public void ModelData_parameters_are_precessed()
		{
			Dictionary<string, object> items = new Dictionary<string, object>();
			string processedtext = _model.ProcessTemplate("create new ${Host.Date(\"yyyy\")}", items).Text;
			Assert.That(processedtext, Is.EqualTo("create new " + DateTime.Now.Year));
		}
		
		[Test]
		public void If_a_file_extension_is_set_this_defaults_the_TemplateResult()
		{
			var result = _model.ProcessTemplateFile(@"Templates\foo.cs.mt", null);
			Assert.That(result.Extension, Is.EqualTo("cs"));
		}
		
		[Test]
		public void If_no_file_extension_is_set_this_defaults_the_TemplateResult_to_SQL()
		{
			var result = _model.ProcessTemplateFile(@"Templates\bar.mt", null);
			Assert.That(result.Extension, Is.EqualTo("sql"));
		}
	}
}