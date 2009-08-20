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
		public int Compare(IPlugIn x, IPlugIn y)
		{
			int result;

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
				int numX = x.RequestedLoadOrder;
				int numY = y.RequestedLoadOrder;

				result = numX - numY;
			}

			return result;
		}
	}
}
