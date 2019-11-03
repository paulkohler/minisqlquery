using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery.ExternalTools.Plugin.Commands
{
    public class RunExportSqlCeCommandBase : CommandBase
    {
        public RunExportSqlCeCommandBase(string name)
            : base(name)
        {
        }

        protected void RunExportSqlCe(string fileName)
        {
            string file = Path.GetTempFileName() + ".sql";
            string conn = Settings.ConnectionDefinition.ConnectionString.Replace(@"""", @"\""");
            string arguments = string.Format("\"{0}\" \"{1}\"", conn, file);

            var tool = new Process();
            tool.StartInfo.FileName = fileName;
            tool.StartInfo.Arguments = arguments;
            tool.StartInfo.UseShellExecute = false;
            tool.StartInfo.RedirectStandardOutput = true;
            tool.StartInfo.RedirectStandardError = true;

            if (tool.Start())
            {
                string output = tool.StandardOutput.ReadToEnd();
                string err = tool.StandardError.ReadToEnd();

                if (!string.IsNullOrEmpty(err))
                {
                    output = "ERROR:" + Environment.NewLine + err + Environment.NewLine + output;
                }

                if (File.Exists(file))
                {
                    IEditor editor = Services.Resolve<IFileEditorResolver>().ResolveEditorInstance(file);
                    editor.FileName = file;
                    editor.LoadFile();
                    HostWindow.DisplayDockedForm(editor as DockContent);
                }
                else
                {
                    var sb = new StringBuilder();
                    sb.AppendLine("Error generating the output file.");
                    sb.AppendLine("Process Info:");
                    sb.AppendFormat("  File Name: {0}", tool.StartInfo.FileName);
                    sb.AppendLine();
                    sb.AppendFormat("  Arguments: {0}", tool.StartInfo.Arguments);
                    sb.AppendLine();
                    sb.AppendLine(output);
                    output = sb.ToString();
                }

                if (!string.IsNullOrEmpty(output))
                {
                    HostWindow.DisplaySimpleMessageBox(null, output, fileName + " Output");
                }
            }
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}