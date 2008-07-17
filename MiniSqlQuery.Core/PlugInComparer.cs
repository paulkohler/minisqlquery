using System;
using System.Collections.Generic;
using System.Text;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// Used for sorting plugins at load time.
	/// </summary>
	public class PlugInComparer : IComparer<IPlugIn>
	{
		/// <summary>
		/// Orders two plugin classes.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		/// <remarks>
		/// This ordering functionality is a simple way of doing what Windsor does well, todo - convert to Windsor container for plugins!
		/// </remarks>
		public int Compare(IPlugIn x, IPlugIn y)
		{
			int result = 0;
			int numX = 0;
			int numY = 0;

			if (x == null && y == null)
			{
				result = 0;
			}
			else if (y == null)
			{
				result = -1;
			}
			else if (x == null)
			{
				result = 1;
			}
			else
			{
				numX = x.RequestedLoadOrder;
				numY = y.RequestedLoadOrder;

				result = numX - numY;
			}

			return result;
		}
	}
}
