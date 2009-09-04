using System;
using System.Collections.Generic;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Template;
using MiniSqlQuery.PlugIns.TemplateViewer;
using Moq;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace MiniSqlQuery.Tests.Templates
{
	[TestFixture]
	public class TemplateModel_tests
	{
		TemplateModel _model;
		IApplicationServices _services;

		[SetUp]
		public void TestSetUp()
		{
			var appServicesMock = new Mock<IApplicationServices>(MockBehavior.Relaxed);
			_services = appServicesMock.Object;
			_model = new TemplateModel(_services, new NVelocityWrapper());
		}

		// todo ParseErrorException
		
		[Test]
		public void ModelData_parameters_are_precessed()
		{
			Dictionary<string, object> items = new Dictionary<string, object>();
			string processedtext = _model.ProcessTemplate("create new ${Host.Date(\"yyyy\")}", items).Text;
			Assert.That(processedtext, Is.EqualTo("create new " + DateTime.Now.Year));
		}
	}
}