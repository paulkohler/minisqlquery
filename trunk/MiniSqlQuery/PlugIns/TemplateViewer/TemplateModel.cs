using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Template;
using MiniSqlQuery.Properties;

namespace MiniSqlQuery.PlugIns.TemplateViewer
{
	public class TemplateModel
	{
		#region Delegates

		public delegate string GetValueForParameter(string parameter);

		#endregion

		private readonly IApplicationServices _services;
		private ITextFormatter _formatter;

		public TemplateModel(IApplicationServices services, ITextFormatter formatter)
		{
			_services = services;
			_formatter = formatter;
		}

		public string GetTemplatePath()
		{
			string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			path = Path.Combine(path, Resources.TemplatesDirectoryName);

			return path;
		}

		public string[] GetFilesForFolder(string path)
		{
			return Directory.GetFiles(path, "*.mt", SearchOption.TopDirectoryOnly);
		}

		public TreeNode[] CreateNodes()
		{
			string path = GetTemplatePath();
			return CreateNodes(path, GetFilesForFolder(path));
		}

		public TreeNode[] CreateNodes(string rootPath, string[] files)
		{
			List<TreeNode> nodes = new List<TreeNode>();

			foreach (string file in files)
			{
				if (file.StartsWith(rootPath))
				{
					FileInfo fi = new FileInfo(file);

					TreeNode node = new TreeNode(Path.GetFileNameWithoutExtension(fi.FullName));
					node.ImageKey = "script_code";
					node.SelectedImageKey = "script_code";
					node.Tag = fi; // store file info on tag
					node.ToolTipText = fi.FullName;

					nodes.Add(node);
				}
			}

			return nodes.ToArray();
		}

		public TemplateResult ProcessTemplateFile(string filename, GetValueForParameter getValueForParameter)
		{
			string[] lines = File.ReadAllLines(filename);
			string text;
			Dictionary<string, object> items = new Dictionary<string, object>();
			text = PreProcessTemplate(lines, getValueForParameter, items);
			return ProcessTemplate(text, items);
		}

		public string PreProcessTemplate(
			string[] lines,
			GetValueForParameter getValueForParameter,
			Dictionary<string, object> items)
		{
			int i = 0;
			for (; i < lines.Length; i++)
			{
				string line = lines[i];
				if (line.StartsWith("#@")) // process cmd
				{
					if (line.StartsWith("#@get ", StringComparison.CurrentCultureIgnoreCase))
					{
						string name = line.Substring("#@get ".Length);
						string val = getValueForParameter(name);
						items.Add(name, val);
					}
					else if (line.StartsWith("#@set extension ", StringComparison.CurrentCultureIgnoreCase))
					{
						string ext = line.Substring("#@set extension ".Length);
						items.Add("extension", ext);
					}
				}
				else
				{
					break;
				}
			}

			string text = string.Join(Environment.NewLine, lines, i, lines.Length - i);

			return text;
		}

		public TemplateResult ProcessTemplate(string text, Dictionary<string, object> items)
		{
			if (items != null)
			{
				if (_services.HostWindow.DatabaseInspector.DbSchema == null)
				{
					_services.HostWindow.DatabaseInspector.LoadDatabaseDetails();
				}
				TemplateHost data = new TemplateHost {Services = _services, Model = _services.HostWindow.DatabaseInspector.DbSchema};
				items.Add("Host", data);
				items.Add("Model", data.Model);
			}

			TemplateResult result = new TemplateResult();
			result.Text = _formatter.Format(text, items);
			result.Extension = "sql";
			if (items != null && items.ContainsKey("extension"))
			{
				result.Extension = (string) items["extension"];
			}

			return result;
		}
	}
}