using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using NUnit.Extensions.Forms;
using Rhino.Mocks;

namespace MiniSqlQuery.Tests.Gui
{
	/// <summary>
	/// Query Form Tester class for use with NUnitForms.
	/// Inherit from this class in standard unit tests.
	/// </summary>
	public class QueryFormTester : NUnitFormTest
	{
		protected IApplicationServices StubAppServices;
		protected IApplicationSettings StubAppSettings;
		protected IHostWindow StubHostWindow;
		protected IQueryEditor QueryEditor;
		protected QueryForm QueryForm;

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
			StubAppServices = MockRepository.GenerateStub<IApplicationServices>();
		}

		/// <summary>
		/// Sets up an empty <see cref="IApplicationSettings"/> mock object.
		/// </summary>
		public virtual void SetUpApplicationSettingsMock()
		{
			StubAppSettings = MockRepository.GenerateStub<IApplicationSettings>();
		}

		/// <summary>
		/// Sets up a basic <see cref="IHostWindow"/> mock assigning it to <see cref="ApplicationServices"/>.
		/// </summary>
		/// <remarks>
		/// The SetStaus method just takes any parameters.
		/// </remarks>
		public virtual void SetUpHostWindowMock()
		{
			StubHostWindow = MockRepository.GenerateStub<IHostWindow>();
			StubAppServices.Expect(services => services.HostWindow).Return(StubHostWindow).Repeat.Any();
		}

		/// <summary>
		/// Displays an basic query editor form with the relevent dependency injection applied.
		/// </summary>
		public virtual void ShowBasicForm()
		{
			QueryForm = new QueryForm(StubAppServices);
			QueryEditor = QueryForm; // for reference by interface 

			QueryForm.Show();
			Application.DoEvents(); // ensure fully displayed
		}
	}
}
