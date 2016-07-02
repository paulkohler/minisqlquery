#region License

// Copyright 2005-2009 Paul Kohler (https://github.com/paul-kohler-au/minisqlquery). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;
using MiniSqlQuery.Core;

namespace MiniSqlQuery.PlugIns.DatabaseInspector.Commands
{
	/// <summary>The truncate table command.</summary>
	public class TruncateTableCommand : GenerateStatementCommandBase
	{
		/// <summary>Initializes a new instance of the <see cref="TruncateTableCommand"/> class.</summary>
		public TruncateTableCommand()
			: base("Truncate Table")
		{
			SmallImage = ImageResource.table_delete;
		}

		/// <summary>Execute the command.</summary>
		public override void Execute()
		{
			IHostWindow hostWindow = Services.HostWindow;
			string tableName = hostWindow.DatabaseInspector.RightClickedTableName;

			string caption = string.Format("Truncate '{0}' Table Confirmation", tableName);
			string msg = string.Format("Delete all '{0}' data, are you sure?", tableName);
			if (tableName != null && MessageBox.Show(msg, caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				DbConnection dbConnection;
				DbCommand cmd = null;

				try
				{
					hostWindow.SetPointerState(Cursors.WaitCursor);
					dbConnection = Settings.GetOpenConnection();
					cmd = dbConnection.CreateCommand();
					cmd.CommandText = "DELETE FROM " + tableName;
					cmd.CommandType = CommandType.Text;
					cmd.ExecuteNonQuery();
					Services.PostMessage(SystemMessage.TableTruncated, tableName);
				}
				catch (DbException dbExp)
				{
					hostWindow.DisplaySimpleMessageBox(null, dbExp.Message, "Error");
				}
				catch (InvalidOperationException invalidExp)
				{
					hostWindow.DisplaySimpleMessageBox(null, invalidExp.Message, "Error");
				}
				finally
				{
					if (cmd != null)
					{
						cmd.Dispose();
					}

					hostWindow.SetPointerState(Cursors.Default);
				}
			}
		}
	}
}