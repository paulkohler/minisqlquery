#region License

// Copyright 2005-2009 Paul Kohler (https://github.com/paul-kohler-au/minisqlquery). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.Windows.Forms;
using MiniSqlQuery.Core;

namespace MiniSqlQuery
{
	/// <summary>The new file form.</summary>
	public partial class NewFileForm : Form
	{
		/// <summary>The _file editor resolver.</summary>
		private readonly IFileEditorResolver _fileEditorResolver;

		/// <summary>Initializes a new instance of the <see cref="NewFileForm"/> class.</summary>
		/// <param name="fileEditorResolver">The file editor resolver.</param>
		public NewFileForm(IFileEditorResolver fileEditorResolver)
		{
			InitializeComponent();
			_fileEditorResolver = fileEditorResolver;
		}

		/// <summary>Gets FileEditorDescriptor.</summary>
		public FileEditorDescriptor FileEditorDescriptor
		{
			get { return lstFileTypes.SelectedItem as FileEditorDescriptor; }
		}

		/// <summary>Gets a value indicating whether IsValid.</summary>
		public bool IsValid
		{
			get { return lstFileTypes.SelectedItem != null; }
		}

		/// <summary>The do ok.</summary>
		private void DoOK()
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		/// <summary>The new file form_ load.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void NewFileForm_Load(object sender, EventArgs e)
		{
			lstFileTypes.DataSource = _fileEditorResolver.GetFileTypes();
		}

		/// <summary>The btn o k_ click.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void btnOK_Click(object sender, EventArgs e)
		{
			DoOK();
		}

		/// <summary>The lst file types_ double click.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void lstFileTypes_DoubleClick(object sender, EventArgs e)
		{
			if (IsValid)
			{
				DoOK();
			}
		}

		/// <summary>The lst file types_ selected value changed.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void lstFileTypes_SelectedValueChanged(object sender, EventArgs e)
		{
			btnOK.Enabled = IsValid;
		}
	}
}