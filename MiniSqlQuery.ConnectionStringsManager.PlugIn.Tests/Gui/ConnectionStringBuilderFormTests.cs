using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Windows.Forms;
using NUnit.Framework.SyntaxHelpers;
using NUnit.Extensions.Forms;

namespace MiniSqlQuery.ConnectionStringsManager.PlugIn.Tests.Gui
{
	[TestFixture]
	public class ConnectionStringBuilderFormTests : ConnectionStringBuilderFormTester
	{
		[Test]
		public void Basic_init_tests()
		{
			ShowDefaultForm();

			Assert.That(ConnectionName, Is.Empty);
			Assert.That(ProvidersComboBox.Properties.SelectedItem.ToString(), Is.EqualTo("System.Data.SqlClient"));
			Assert.That(_form.ConnectionString, Is.Empty);

			OkButton.Click();

			Assert.That(_form.DialogResult, Is.EqualTo(DialogResult.OK));
		}

		[Test]
		public void Open_with_values_and_select_oledb_as_the_provider()
		{
			_form = new ConnectionStringBuilderForm("Access Connection", "System.Data.SqlClient", "");
			_form.Show();
			Application.DoEvents();

			ProvidersComboBox.Select("System.Data.OleDb");
			// back handed way of doing it but not sure how to handle a property grid effectively!
			_form.DbConnectionStringBuilderInstance.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\sample.mdb";
			OkButton.Click();

			Assert.That(_form.DialogResult, Is.EqualTo(DialogResult.OK));

			Assert.That(_form.ProviderName, Is.EqualTo("System.Data.OleDb"));
			//Assert.That(_form.ConnectionName, Is.EqualTo("Access Connection"));
			//Assert.That(_form.ConnectionString, Is.EqualTo(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\sample.mdb"));
		}

		[Test]
		public void Pressing_cancel_sets_DialogResult_accordingly()
		{
			ShowDefaultForm();

			ProvidersComboBox.Select("System.Data.OleDb");
			// back handed way of doing it but not sure how to handle a property grid effectively!
			_form.DbConnectionStringBuilderInstance.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\sample.mdb";
			OkButton.Click();

			Assert.That(_form.DialogResult, Is.EqualTo(DialogResult.OK));

			Assert.That(_form.ProviderName, Is.EqualTo("System.Data.OleDb"));
			//Assert.That(_form.ConnectionName, Is.EqualTo("Access Connection"));
			//Assert.That(_form.ConnectionString, Is.EqualTo(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\sample.mdb"));
		}
	}
}
