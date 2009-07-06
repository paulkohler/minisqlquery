using System;
using System.Data;
using System.Text;
using MiniSqlQuery.Core.Commands;

namespace MiniSqlQuery.Commands
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
			Services.Settings.CloseConnection();
		}

		public override bool Enabled
		{
			get
			{
				if (Services.Settings.Connection.State == ConnectionState.Closed && 
					Services.Settings.Connection.State == ConnectionState.Broken)
				{
					return false;
				}
				return true;
			}
		}
	}
}
