#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;

namespace MiniSqlQuery.Core.Commands
{
	/// <summary>The insert guid command.</summary>
	public class InsertGuidCommand : CommandBase
	{
		/// <summary>Initializes a new instance of the <see cref="InsertGuidCommand"/> class.</summary>
		public InsertGuidCommand()
			: base("Insert GUID")
		{
			// todo SmallImage = ImageResource.;
		}

		/// <summary>Gets a value indicating whether Enabled.</summary>
		/// <value>The enabled.</value>
		public override bool Enabled
		{
			get { return ActiveFormAsEditor != null; }
		}

		/// <summary>The execute.</summary>
		public override void Execute()
		{
			IEditor editor = ActiveFormAsEditor;
			if (editor != null)
			{
				editor.InsertText(Guid.NewGuid().ToString());
			}
		}
	}
}