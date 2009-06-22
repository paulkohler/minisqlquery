using System;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;
using MiniSqlQuery.Core;

namespace MiniSqlQuery.PlugIns.DatabaseInspector.Commands
{
	public class TruncateTableCommand : GenerateStatementCommandBase
	{
		public TruncateTableCommand()
			: base("Truncate Table")
		{
		}

		public override void Execute()
		{
			string tableName = Services.HostWindow.DatabaseInspector.RightClickedTableName;

			if (tableName != null &&
			    MessageBox.Show("Delete all table data, are you sure?", "Truncate Table Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) ==
			    DialogResult.Yes)
			{
				DbConnection dbConnection;
				DbCommand cmd = null;

				try
				{
					Services.HostWindow.SetPointerState(Cursors.WaitCursor);
					dbConnection = Services.Settings.GetOpenConnection();
					cmd = dbConnection.CreateCommand();
					cmd.CommandText = "DELETE FROM " + tableName;
					cmd.CommandType = CommandType.Text;
					cmd.ExecuteNonQuery();
					Services.PostMessage(SystemMessage.TableTruncated, tableName);
				}
				catch (DbException dbExp)
				{
					Services.HostWindow.DisplaySimpleMessageBox(null, dbExp.Message, "Error");
				}
				catch (InvalidOperationException invalidExp)
				{
					Services.HostWindow.DisplaySimpleMessageBox(null, invalidExp.Message, "Error");
				}
				finally
				{
					if (cmd != null)
					{
						cmd.Dispose();
					}
					Services.HostWindow.SetPointerState(Cursors.Default);
				}
			}
		}
	}
}