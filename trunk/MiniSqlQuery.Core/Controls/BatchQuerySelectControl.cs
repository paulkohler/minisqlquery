#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System;
using System.Windows.Forms;

namespace MiniSqlQuery.Core.Controls
{
	/// <summary>
	/// A batch query selection control is used for displaying multiple result sets and allows
	/// the user to select one (e.g. for exports etc).
	/// </summary>
	public partial class BatchQuerySelectControl : UserControl
	{
		private QueryBatch _batch;
		private Query _selectedQuery;

		/// <summary>
		/// Initializes a new instance of the <see cref="BatchQuerySelectControl"/> class.
		/// </summary>
		public BatchQuerySelectControl()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Gets the selected query.
		/// </summary>
		/// <value>The selected query.</value>
		public Query SelectedQuery
		{
			get { return _selectedQuery; }
		}

		/// <summary>
		/// Fills the list with the batch result sets.
		/// </summary>
		/// <param name="batch">The query batch.</param>
		public void Fill(QueryBatch batch)
		{
			_batch = batch;
			lstBatches.Items.Clear();
			if (batch == null)
			{
				return;
			}

			for (int setIndex = 0; setIndex < batch.Queries.Count; setIndex++)
			{
				var query = batch.Queries[setIndex];
				if (query.Result != null && query.Result.Tables.Count > 0)
				{
					string setName = string.Format("Result Set {0} ({1} tables)", setIndex, query.Result.Tables.Count);
					lstBatches.Items.Add(setName);
				}
			}
			lstBatches.SelectedIndex = 0;
		}

		private void lstBatches_SelectedIndexChanged(object sender, EventArgs e)
		{
			_selectedQuery = _batch.Queries[lstBatches.SelectedIndex];
		}
	}
}