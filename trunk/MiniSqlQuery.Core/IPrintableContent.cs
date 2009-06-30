using System;
using System.Drawing.Printing;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// If implemented it signals that the class supports printing of the "contents" of the object.
	/// </summary>
	public interface IPrintableContent
	{
		/// <summary>
		/// The document to print (or null if not supported in the current context).
		/// </summary>
		PrintDocument PrintDocument { get; }
	}
}
