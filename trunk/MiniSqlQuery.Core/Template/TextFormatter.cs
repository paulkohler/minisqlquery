using System;
using System.Collections.Generic;
using System.IO;

namespace MiniSqlQuery.Core.Template
{
	public interface ITextFormatter
	{
		string Format(string text, Dictionary<string, object> items);
	}
}