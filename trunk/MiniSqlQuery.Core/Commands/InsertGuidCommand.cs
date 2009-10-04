#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System;

namespace MiniSqlQuery.Core.Commands
{
	public class InsertGuidCommand : CommandBase
	{
		public InsertGuidCommand()
			: base("Insert GUID")
		{
			//todo SmallImage = ImageResource.;
		}

		public override void Execute()
		{
			IEditor editor = ActiveFormAsEditor;
			if (editor != null)
			{
				editor.InsertText(Guid.NewGuid().ToString());
			}
		}

		public override bool Enabled
		{
			get
			{
				return ActiveFormAsEditor != null;
			}
		}
	}
}