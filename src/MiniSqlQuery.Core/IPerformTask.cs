#region License

// Copyright 2005-2009 Paul Kohler (https://github.com/paul-kohler-au/minisqlquery). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license

#endregion

using System;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// 	Intended as a window level task such as executing a query (like applying ICommand to a window).
	/// </summary>
	public interface IPerformTask
	{
		/// <summary>
		/// 	Gets a value indicating whether this instance is busy.
		/// </summary>
		/// <value>True if busy.</value>
		bool IsBusy { get; }

		/// <summary>
		/// 	Cancels the current task.
		/// </summary>
		void CancelTask();

		/// <summary>
		/// 	Executes the current task.
		/// </summary>
		void ExecuteTask();
	}
}