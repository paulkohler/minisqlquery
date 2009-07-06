using System;
using System.Collections.Generic;
using System.Text;
using MiniSqlQuery.Core.Commands;
using System.Windows.Forms;
using MiniSqlQuery.Core;

namespace MiniSqlQuery.Commands
{
	public class ExecuteQueryCommand
		: CommandBase
	{
		public ExecuteQueryCommand()
			: base("&Execute Query")
		{
			ShortcutKeys = Keys.F5;
			SmallImage = ImageResource.lightning;
		}

		public override void Execute()
		{
			if (!Enabled)
			{
				return;
			}

			IQueryEditor editor = ActiveFormAsEditor;
			if (editor != null)
			{
				editor.ExecuteQuery();
			}
		}

		public override bool Enabled
		{
			get
			{
				IQueryEditor editor = ActiveFormAsEditor;
				if (editor != null)
				{
					return !editor.IsBusy;
				}
				return false;
			}
		}
	}
}
