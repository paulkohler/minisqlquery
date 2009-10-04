#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Template;

namespace MiniSqlQuery.PlugIns.TemplateViewer
{
	public class TemplateModel
	{
		public const string Extension = "extension";

		#region Delegates

		public delegate string GetValueForParameter(string parameter);

		#endregion

		private readonly IApplicationServices _services;
		private readonly ITextFormatter _formatter;
		private readonly TemplateData _templateData;

		public TemplateModel(IApplicationServices services, ITextFormatter formatter, TemplateData templateData)
		{
			_services = services;
			_formatter = formatter;
			_templateData = templateData;
		}

		public string GetTemplatePath()
		{
			string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			path = Path.Combine(path, TemplateResources.TemplatesDirectoryName);

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

			items[Extension] = InferExtensionFromFilename(filename, items);

			string text = PreProcessTemplate(lines, getValueForParameter, items);
			return ProcessTemplate(text, items);
		}

		public string InferExtensionFromFilename(string filename, Dictionary<string, object> items)
		{
			string ext = Path.GetExtension(filename);
			string templateFilename = Path.GetFileNameWithoutExtension(filename);
			if (ext.ToLower() == ".mt" && templateFilename.Contains("."))
			{
				return Path.GetExtension(templateFilename).Substring(1);
			}

			// default
			return "sql";
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
						items[Extension] = line.Substring("#@set extension ".Length);
					}
					else if (line.StartsWith("#@import-plugin ", StringComparison.CurrentCultureIgnoreCase))
					{
						string pluginKeyName = line.Substring("#@import-plugin ".Length);
						items[pluginKeyName.Replace(".", "_")] = _services.Resolve<IPlugIn>(pluginKeyName);
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

			TemplateResult result;

			using (TemplateHost host = _services.Resolve<TemplateHost>())
			{
				host.Data = _templateData;

				items.Add("Host", host);
				items.Add("Data", host.Data);

				result = new TemplateResult();
				result.Text = _formatter.Format(text, items);
				result.Extension = "sql";
				if (items.ContainsKey(Extension))
				{
					result.Extension = (string) items[Extension];
				}
			}

			return result;
		}
	}
}