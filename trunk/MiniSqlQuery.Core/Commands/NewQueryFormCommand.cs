#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery.Core.Commands
{
	/// <summary>
	/// 	The new query form command.
	/// </summary>
	public class NewQueryFormCommand
		: CommandBase
	{
		/// <summary>
		/// 	Initializes a new instance of the <see cref = "NewQueryFormCommand" /> class.
		/// </summary>
		public NewQueryFormCommand()
			: base("New &Query Window")
		{
			ShortcutKeys = Keys.Control | Keys.N;
			SmallImage = ImageResource.page_white;
		}

		/// <summary>
		/// 	Execute the command.
		/// </summary>
		public override void Execute()
		{
			var editor = Services.Container.Resolve<IQueryEditor>();
			editor.FileName = null;
			HostWindow.DisplayDockedForm(editor as DockContent);
		}
	}
}