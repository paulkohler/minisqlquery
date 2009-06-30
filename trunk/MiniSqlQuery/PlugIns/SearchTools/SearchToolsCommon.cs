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
