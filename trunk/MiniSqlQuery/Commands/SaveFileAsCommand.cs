using System;
using System.Windows.Forms;
using MiniSqlQuery.Core.Commands;
using MiniSqlQuery.Core;

namespace MiniSqlQuery.Commands
{
	public class SaveFileAsCommand
        : CommandBase
    {
		public SaveFileAsCommand()
            : base("Save File &As...")
        {
        }

        public override void Execute()
        {
			IQueryEditor queryForm = Services.HostWindow.Instance.ActiveMdiChild as IQueryEditor;
			if (queryForm != null)
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
				saveFileDialog.Filter = Services.Settings.DefaultFileFilter;
				if (saveFileDialog.ShowDialog(Services.HostWindow.Instance) == DialogResult.OK)
				{
					// what if this filename covers an existing open window?
					queryForm.FileName = saveFileDialog.FileName;
					queryForm.SaveFile();
				}
			}
        }
    }
}
