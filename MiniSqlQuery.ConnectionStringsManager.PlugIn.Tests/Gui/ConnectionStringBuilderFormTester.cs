using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Extensions.Forms;
using System.Windows.Forms;

namespace MiniSqlQuery.ConnectionStringsManager.PlugIn.Tests.Gui
{
	public class ConnectionStringBuilderFormTester : NUnitFormTest
	{
		protected ConnectionStringBuilderForm _form;

		public TextBoxTester ConnectionName
		{
			get
			{
				return new TextBoxTester("txtConnectionName");
			}
		}

		public ComboBoxTester ProvidersComboBox
		{
			get
			{
				return new ComboBoxTester("cboProvider");
			}
		}

		public ToolStripButtonTester OkButton
		{
			get
			{
				return new ToolStripButtonTester("toolStripButtonOk");
			}
		}

		public ToolStripButtonTester CancelButton
		{
			get
			{
				return new ToolStripButtonTester("toolStripButtonCancel");
			}
		}


		protected virtual void ShowDefaultForm()
		{
			_form = new ConnectionStringBuilderForm();
			_form.Show();
			Application.DoEvents();
		}

	}
}
