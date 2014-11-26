#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.Windows.Forms;

namespace MiniSqlQuery.Core.Forms
{
	/// <summary>Used as a modal dialog, a <see cref="QueryBatch"/> is supplied and the used can 
	/// select one of the result sets. <see cref="DialogResult"/> is set to <see cref="DialogResult.OK"/> on an OK exit.</summary>
	public partial class BatchQuerySelectForm : Form
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BatchQuerySelectForm"/> class.
		/// </summary>
		public BatchQuerySelectForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Gets a refernce to the selected query.
		/// </summary>
		/// <value>The selected query.</value>
		public Query SelectedQuery
		{
			get { return batchQuerySelectControl1.SelectedQuery; }
		}

		/// <summary>Fills the list with the named batch results.</summary>
		/// <param name="batch">The batch.</param>
		public void Fill(QueryBatch batch)
		{
			batchQuerySelectControl1.Fill(batch);
		}

		/// <summary>The btn ok_ click.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void btnOk_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}
	}
}