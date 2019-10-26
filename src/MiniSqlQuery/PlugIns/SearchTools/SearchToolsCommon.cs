#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using System;
using System.Collections.Generic;
using MiniSqlQuery.Core;

namespace MiniSqlQuery.PlugIns.SearchTools
{
	/// <summary>The search tools common.</summary>
	public class SearchToolsCommon
	{
		/// <summary>The _find text requests.</summary>
		private static readonly Dictionary<int, FindTextRequest> _findTextRequests = new Dictionary<int, FindTextRequest>();

		/// <summary>Gets FindReplaceTextRequests.</summary>
		public static Dictionary<int, FindTextRequest> FindReplaceTextRequests
		{
			get { return _findTextRequests; }
		}
	}
}