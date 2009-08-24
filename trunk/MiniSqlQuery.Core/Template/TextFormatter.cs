using System;

namespace MiniSqlQuery.Core.Template
{
	public interface ITextFormatter
	{
		string Format(string text, object dataSource);
	}
}