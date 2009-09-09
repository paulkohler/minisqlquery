using System;
using MiniSqlQuery.Core;

namespace MiniSqlQuery.PlugIns.TemplateViewer
{
	public interface ITemplateEditor : IPerformTask
	{
		void RunTemplate();
	}
}