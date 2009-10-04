#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System;
using System.Collections.Generic;
using System.Text;
using MiniSqlQuery.Core;

namespace MiniSqlQuery.PlugIns.SearchTools
{
	public class SearchToolsCommon
	{
		private static readonly Dictionary<int, FindTextRequest> _findTextRequests = new Dictionary<int, FindTextRequest>();

		public static Dictionary<int, FindTextRequest> FindReplaceTextRequests
		{
			get
			{
				return _findTextRequests;
			}
		}
	}
}
