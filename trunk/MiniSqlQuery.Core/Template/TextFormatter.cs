using System;
using System.Collections.Generic;

namespace MiniSqlQuery.Core.Template
{
	public interface ITextFormatter
	{
		string Format(string text, Dictionary<string, object> items);
	}
}