#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System;
using System.Data;

namespace MiniSqlQuery.Core.Commands
{
	public class CloseDatabaseConnectionCommand
		: CommandBase
	{
		public CloseDatabaseConnectionCommand()
			: base("Close Current connection")
		{
			//SmallImage = ImageResource.database_?;
		}

		public override void Execute()
		{
			Settings.CloseConnection();
		}

		public override bool Enabled
		{
			get
			{
				if (Settings.Connection.State == ConnectionState.Closed && 
				    Settings.Connection.State == ConnectionState.Broken)
				{
					return false;
				}
				return true;
			}
		}
	}
}