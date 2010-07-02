#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;

namespace MiniSqlQuery.PlugIns.SearchTools.Commands
{
	/// <summary>The show find text form command.</summary>
	public class ShowFindTextFormCommand : CommandBase
	{
		/// <summary>Initializes a new instance of the <see cref="ShowFindTextFormCommand"/> class.</summary>
		public ShowFindTextFormCommand()
			: base("&Find Text...")
		{
			SmallImage = ImageResource.find;
			ShortcutKeys = Keys.Control | Keys.F;
		}


		/// <summary>Gets a value indicating whether Enabled.</summary>
		public override bool Enabled
		{
			get { return HostWindow.ActiveChildForm is IFindReplaceProvider; }
		}

		/// <summary>Gets FindReplaceWindow.</summary>
		public IFindReplaceWindow FindReplaceWindow { get; private set; }

		/// <summary>Execute the command.</summary>
		public override void Execute()
		{
			if (!Enabled)
			{
				return;
			}

			// if the window is an editor, grab the highlighted text
			IFindReplaceProvider findReplaceProvider = HostWindow.ActiveChildForm as IFindReplaceProvider;

			if (FindReplaceWindow == null || FindReplaceWindow.IsDisposed)
			{
				FindReplaceWindow = new FindReplaceForm(Services);
			}

			if (findReplaceProvider is IEditor)
			{
				FindReplaceWindow.FindString = ((IEditor)findReplaceProvider).SelectedText;
			}

			FindReplaceWindow.TopMost = true;

			if (!FindReplaceWindow.Visible)
			{
				FindReplaceWindow.Show(HostWindow.Instance);
			}
		}
	}
}