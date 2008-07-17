using System;
using System.Collections.Generic;
using System.Text;
using MiniSqlQuery.Core.Commands;
using MiniSqlQuery.Core;
using System.Windows.Forms;
using System.Data;
using MiniSqlQuery.Properties;

namespace MiniSqlQuery.Commands
{
	public class SaveResultsAsDataSetCommand
		: CommandBase
	{
		public SaveResultsAsDataSetCommand()
			: base("Save Results as DataSet XML...")
		{
			SmallImage = ImageResource.table_save;
		}

		public override void Execute()
		{
			if (Services.HostWindow.ActiveChildForm is IQueryEditor)
			{
				IQueryEditor editor = (IQueryEditor)Services.HostWindow.ActiveChildForm;
				DataSet ds = editor.DataSet;
				
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.Title = "Save Results as DataSet XML";
				saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
				saveFileDialog.Filter = Settings.Default.XmlFileDialogFilter;

				if (saveFileDialog.ShowDialog(Services.HostWindow.Instance) == DialogResult.OK)
				{
					ds.WriteXml(saveFileDialog.FileName, XmlWriteMode.WriteSchema);

					editor.SetStatus(string.Format("Saved reults to file: '{0}'", saveFileDialog.FileName));
				}

				saveFileDialog.Dispose();
			}
			else
			{
				Services.HostWindow.DisplaySimpleMessageBox(null, "No reults to save as a 'DataSet'.", "Save Results as DataSet XML Error");
			}
		}
	}
}
