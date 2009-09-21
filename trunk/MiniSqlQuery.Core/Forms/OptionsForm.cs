using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MiniSqlQuery.Core.Forms
{
	public partial class OptionsForm : Form
	{
		private readonly IApplicationServices _applicationServices;
		PropertyGrid propertyGrid;

		public OptionsForm(IApplicationServices applicationServices)
		{
			_applicationServices = applicationServices;
			InitializeComponent();
		}

		private void OptionsForm_Load(object sender, EventArgs e)
		{
			propertyGrid = new PropertyGrid();
			propertyGrid.Dock = DockStyle.Fill;
			groupBox1.Controls.Add(propertyGrid);

			// todo - options wrapper & provider interface
			propertyGrid.SelectedObject = _applicationServices.Settings;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			// apply
		}
	}
}
