using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using Moq;
using NUnit.Extensions.Forms;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace MiniSqlQuery.Tests.Gui
{
	/// <summary>
	/// Query Form Tester class for use with NUnitForms.
	/// Inherit from this class in standard unit tests.
	/// </summary>
	public class QueryFormTester : NUnitFormTest
	{
		protected IApplicationServices appServices;
		protected IApplicationSettings appSettings;
		protected IHostWindow hostWindow;
		protected QueryForm queryForm;
		protected IQueryEditor queryEditor;
		protected Mock<IApplicationServices> appServicesMock;
		protected Mock<IApplicationSettings> appSettingsMock;
		protected Mock<IHostWindow> hostWindowMock;

		public QueryFormTester()
		{
		}

		/// <summary>
		/// Sets up various mock object required for the form to run (does not cover query execution).
		/// </summary>
		public override void Setup()
		{
			SetUpApplicationServicesMock();
			SetUpApplicationSettingsMock();
			SetUpHostWindowMock();
			base.Setup();
		}

		/// <summary>
		/// Sets up an empty <see cref="IApplicationServices"/> mock object.
		/// </summary>
		public virtual void SetUpApplicationServicesMock()
		{
			appServicesMock = new Mock<IApplicationServices>(MockBehavior.Relaxed);
			appServices = appServicesMock.Object;
		}

		/// <summary>
		/// Sets up an empty <see cref="IApplicationSettings"/> mock object.
		/// </summary>
		public virtual void SetUpApplicationSettingsMock()
		{
			appSettingsMock = new Mock<IApplicationSettings>(MockBehavior.Relaxed);
			appSettings = appSettingsMock.Object;
		}

		/// <summary>
		/// Sets up a basic <see cref="IHostWindow"/> mock assigning it to <see cref="ApplicationServices"/>.
		/// </summary>
		/// <remarks>
		/// The SetStaus method just takes any parameters.
		/// </remarks>
		public virtual void SetUpHostWindowMock()
		{
			hostWindowMock = new Mock<IHostWindow>(MockBehavior.Relaxed);
			hostWindowMock.Expect(x => x.SetStatus(It.IsAny<Form>(), It.IsAny<string>()));
			hostWindow = hostWindowMock.Object;

			appServicesMock.Expect(s => s.HostWindow).Returns(hostWindow);

			//ApplicationServices.Instance.HostWindow = hostWindow;
		}

		/// <summary>
		/// Displays an basic query editor form with the relevent dependency injection applied.
		/// </summary>
		public virtual void ShowBasicForm()
		{
			queryForm = new QueryForm(appServices);
			queryEditor = queryForm; // for reference by interface 

			queryForm.Show();
			Application.DoEvents(); // ensure fully displayed
		}

	}
}
