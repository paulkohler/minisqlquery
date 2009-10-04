#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// Intended as a window level task such as executing a query (like applying ICommand to a window).
	/// </summary>
	public interface IPerformTask
	{
		/// <summary>
		/// Executes the current task.
		/// </summary>
		void ExecuteTask();

		/// <summary>
		/// Cancels the current task.
		/// </summary>
		void CancelTask();

		/// <summary>
		/// True if a task is being executed.
		/// </summary>
		bool IsBusy { get; }
	}
}