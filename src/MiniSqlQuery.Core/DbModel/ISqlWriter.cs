#region License

// Copyright 2005-2009 Paul Kohler (https://github.com/paul-kohler-au/minisqlquery). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license

#endregion

using System;
using System.IO;

namespace MiniSqlQuery.Core.DbModel
{
	/// <summary>An SQL Writer interface.</summary>
	public interface ISqlWriter
	{
		/// <summary>Gets or sets a value indicating whether IncludeComments.</summary>
		/// <value>The include comments.</value>
		bool IncludeComments { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether to include read-only columns in the export SQL.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if including read-only columns in the export; otherwise, <c>false</c>.
		/// </value>
		bool IncludeReadOnlyColumnsInExport { get; set; }

		/// <summary>Gets or sets a value indicating whether InsertLineBreaksBetweenColumns.</summary>
		/// <value>The insert line breaks between columns.</value>
		bool InsertLineBreaksBetweenColumns { get; set; }

		/// <summary>The write create.</summary>
		/// <param name="writer">The writer.</param>
		/// <param name="column">The column.</param>
		void WriteCreate(TextWriter writer, DbModelColumn column);

		/// <summary>The write delete.</summary>
		/// <param name="writer">The writer.</param>
		/// <param name="tableOrView">The table or view.</param>
		void WriteDelete(TextWriter writer, DbModelTable tableOrView);

		/// <summary>The write insert.</summary>
		/// <param name="writer">The writer.</param>
		/// <param name="tableOrView">The table or view.</param>
		void WriteInsert(TextWriter writer, DbModelTable tableOrView);

		/// <summary>The write select.</summary>
		/// <param name="writer">The writer.</param>
		/// <param name="tableOrView">The table or view.</param>
		void WriteSelect(TextWriter writer, DbModelTable tableOrView);

		/// <summary>The write select count.</summary>
		/// <param name="writer">The writer.</param>
		/// <param name="tableOrView">The table or view.</param>
		void WriteSelectCount(TextWriter writer, DbModelTable tableOrView);

		/// <summary>The write summary.</summary>
		/// <param name="writer">The writer.</param>
		/// <param name="column">The column.</param>
		void WriteSummary(TextWriter writer, DbModelColumn column);

		/// <summary>The write update.</summary>
		/// <param name="writer">The writer.</param>
		/// <param name="tableOrView">The table or view.</param>
		void WriteUpdate(TextWriter writer, DbModelTable tableOrView);
	}
}