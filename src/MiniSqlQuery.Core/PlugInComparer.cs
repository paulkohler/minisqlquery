#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE

#endregion

using System;
using System.Collections.Generic;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// 	Used for sorting plugins at load time, a very simple ordering system for the plugins.
	/// </summary>
	public class PlugInComparer : IComparer<IPlugIn>
	{
		/// <summary>
		/// 	Orders two plugin classes.
		/// </summary>
		/// <param name = "x">The left side object.</param>
		/// <param name = "y">The right side object.</param>
		/// <returns>The compare result.</returns>
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