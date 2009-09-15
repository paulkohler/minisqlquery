using System;
using System.Windows.Forms;
using MiniSqlQuery.Core;

namespace MiniSqlQuery
{
	public partial class NewFileForm : Form
	{
		private readonly IFileEditorResolver _fileEditorResolver;

		public NewFileForm(IFileEditorResolver fileEditorResolver)
		{
			InitializeComponent();
			_fileEditorResolver = fileEditorResolver;
		}

		public FileEditorDescriptor FileEditorDescriptor 
		{ 
			get { return lstFileTypes.SelectedItem as FileEditorDescriptor; }
		}

		public bool IsValid
		{
			get { return lstFileTypes.SelectedItem != null; }
		}

		private void NewFileForm_Load(object sender, EventArgs e)
		{
			lstFileTypes.DataSource = _fileEditorResolver.GetFileTypes();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			DoOK();
		}

		private void lstFileTypes_SelectedValueChanged(object sender, EventArgs e)
		{
			btnOK.Enabled = IsValid;
		}

		private void lstFileTypes_DoubleClick(object sender, EventArgs e)
		{
			if (IsValid)
			{
				DoOK();
			}
		}

		private void DoOK()
		{
			DialogResult = DialogResult.OK;
			Close();
		}
	}
}
