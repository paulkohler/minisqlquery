using System;
using System.Collections.Generic;
using System.Collections;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// Interface for viewing table and view data.
	/// </summary>
	public interface IViewTable : IPerformTask, IQueryBatchProvider, INavigatableDocument
	{
		string TableName { get; set; }
		string Text { get; set; }
		bool AutoReload { get; }
	}
}