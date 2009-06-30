using System;
using System.Collections.Generic;
using System.Text;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// A "document" that supports reporting the position of it's cursor
	/// </summary>
	public interface ISupportCursorOffset
	{
		/// <summary>
		/// Gets the cursor offset.
		/// </summary>
		/// <value>The cursor offset.</value>
		int CursorOffset { get; }
	}
}
