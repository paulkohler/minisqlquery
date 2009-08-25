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

		public string ProcessTemplateFile(string filename)
		{
			return ProcessTemplate(File.ReadAllText(filename));
		}

		public string ProcessTemplate(string text)
		{
			ModelData data = new ModelData { Services = _services };
			return _formatter.Format(text, data);
		}

		#region Nested type: ModelData

		public class ModelData
		{
			public IApplicationServices Services { get; set; }

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