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

		private readonly IDatabaseInspector _databaseInspector;
		private readonly IApplicationServices _services;
		private ITextFormatter _formatter;

		public TemplateModel(IApplicationServices services, IDatabaseInspector databaseInspector, ITextFormatter formatter)
		{
			_services = services;
			_databaseInspector = databaseInspector;
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
			Dictionary<string, object> items = new Dictionary<string, object>();
			string[] lines = File.ReadAllLines(filename);

			// file ext check
			string ext = Path.GetExtension(filename);
			string templateFilename = Path.GetFileNameWithoutExtension(filename);
			if (ext.ToLower() == ".mt" && templateFilename.Contains("."))
			{
				items["extension"] = Path.GetExtension(templateFilename).Substring(1);
			}

			string text = PreProcessTemplate(lines, getValueForParameter, items);
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
						items["extension"] = line.Substring("#@set extension ".Length);
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
			if (items == null)
			{
				items = new Dictionary<string, object>();
			}

			if (_databaseInspector.DbSchema == null)
			{
				_databaseInspector.LoadDatabaseDetails();
			}

			TemplateResult result;

			using (TemplateHost host = _services.Resolve<TemplateHost>())
			{
				host.Model = _databaseInspector.DbSchema;
				host.Data = _services.Resolve<TemplateData>();

				items.Add("Host", host);
				items.Add("Model", host.Model);
				items.Add("Data", host.Data);

				result = new TemplateResult();
				result.Text = _formatter.Format(text, items);
				result.Extension = "sql";
				if (items.ContainsKey("extension"))
				{
					result.Extension = (string) items["extension"];
				}
			}

			return result;
		}
	}
}