using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using MiniSqlQuery.TemplateViewer.PlugIn.Properties;

namespace MiniSqlQuery.TemplateViewer.PlugIn
{
	public class TemplateModel
	{
		public TemplateModel()
		{
		}

		public string GetTemplatePath()
		{
			string path = null;

			path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			path = Path.Combine(path, Properties.Resources.TemplatesDirectoryName);

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

		public string ProcessTemplate(string filename)
		{
			string template = File.ReadAllText(filename);
			// todo - params etc
			return template;
		}

	}
}
