using System;
using System.Diagnostics;
using System.IO;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;
using WeifenLuo.WinFormsUI.Docking;
using MiniSqlQuery.ExternalTools.Plugin.Properties;

namespace MiniSqlQuery.ExternalTools.Plugin.Commands
{
	public class RunExportSqlCeCommand : CommandBase
	{
		public RunExportSqlCeCommand()
			: base("Run 'Export SQL CE' Tool")
		{
			SmallImage = Resources.data_out.ToBitmap();
		}

		public override void Execute()
		{
			string file = Path.GetTempFileName() + ".sql";
			string arguments = string.Format("\"{0}\" \"{1}\"", Settings.ConnectionDefinition.ConnectionString, file);

			Process tool = new Process();
			tool.StartInfo.FileName = "ExportSqlCE.exe";
			tool.StartInfo.Arguments = arguments;
			tool.StartInfo.UseShellExecute = false;
			tool.StartInfo.RedirectStandardOutput = true;

			if (tool.Start())
			{
				string output = tool.StandardOutput.ReadToEnd();

				if (File.Exists(file))
				{
					IEditor editor = Services.Resolve<IFileEditorResolver>().ResolveEditorInstance(file);
					editor.FileName = file;
					editor.LoadFile();
					HostWindow.DisplayDockedForm(editor as DockContent);
				}
				else
				{
					output = "Error generating the output file." + Environment.NewLine + output;
				}

				if (!string.IsNullOrEmpty(output))
				{
					HostWindow.DisplaySimpleMessageBox(null, output, "Export SQL CE Output");
				}
			}
		}
	}
}