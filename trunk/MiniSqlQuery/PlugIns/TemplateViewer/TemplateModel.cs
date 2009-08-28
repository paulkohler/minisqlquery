using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Template;
using MiniSqlQuery.Properties;

namespace MiniSqlQuery.PlugIns.TemplateViewer
{
	public class TemplateModel
	{
		private readonly IApplicationServices _services;
		private ITextFormatter _formatter;

		public delegate string GetValueForParameter(string parameter);

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
			return Directory.GetFiles(path, Settings.Default.SqlFileFilter, SearchOption.TopDirectoryOnly);
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

		public string ProcessTemplateFile(string filename, GetValueForParameter getValueForParameter)
		{
			string[] lines = File.ReadAllLines(filename);
			Dictionary<string, object> items = new Dictionary<string, object>();

			int i = 0;
			for (; i < lines.Length; i++)
			{
				string line = lines[i];
				if (line.StartsWith("#@")) // process cmd
				{
					if (line.StartsWith("#@get "))
					{
						string name = line.Substring("#@get ".Length);
						string val = getValueForParameter(name);
						items.Add(name, val);
					}
				}
				else
				{
					break;
				}
			}

			string text = string.Join(Environment.NewLine, lines, i, lines.Length - i);

			return ProcessTemplate(text, items);
		}

		public string ProcessTemplate(string text, Dictionary<string, object> items)
		{
			if (items != null)
			{
				ModelData data = new ModelData { Services = _services };
				items.Add("Host", data);
				if (_services.HostWindow.DatabaseInspector.DbSchema == null)
				{
					_services.HostWindow.DatabaseInspector.LoadDatabaseDetails();
				}
				items.Add("Model", _services.HostWindow.DatabaseInspector.DbSchema);
			}
			return _formatter.Format(text, items);
		}

		#region Nested type: ModelData

		public class ModelData
		{
			public IApplicationServices Services { get; set; }

			public string Date(string format)
			{
				return DateTime.Now.ToString(format);
			}

			public DateTime CurrentDateTime
			{
				get { return DateTime.Now; }
			}

			public string MachineName
			{
				get { return Environment.MachineName; }
			}

			public string UserName
			{
				get { return Environment.UserName; }
			}
		}

		#endregion
	}
}