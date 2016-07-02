#region License

// Copyright 2005-2009 Paul Kohler (https://github.com/paul-kohler-au/minisqlquery). All rights reserved.
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
	/// <summary>The template model.</summary>
	public class TemplateModel
	{
		/// <summary>The extension.</summary>
		public const string Extension = "extension";

		/// <summary>The _formatter.</summary>
		private readonly ITextFormatter _formatter;

		/// <summary>The _services.</summary>
		private readonly IApplicationServices _services;

		/// <summary>Initializes a new instance of the <see cref="TemplateModel"/> class.</summary>
		/// <param name="services">The services.</param>
		/// <param name="formatter">The formatter.</param>
		public TemplateModel(IApplicationServices services, ITextFormatter formatter)
		{
			_services = services;
			_formatter = formatter;
		}

		/// <summary>The get value for parameter.</summary>
		/// <param name="parameter">The parameter.</param>
		public delegate string GetValueForParameter(string parameter);

		/// <summary>The create nodes.</summary>
		/// <returns></returns>
		public TreeNode[] CreateNodes()
		{
			string path = GetTemplatePath();
			return CreateNodes(path, GetFilesForFolder(path));
		}

		/// <summary>The create nodes.</summary>
		/// <param name="rootPath">The root path.</param>
		/// <param name="files">The files.</param>
		/// <returns></returns>
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

		/// <summary>The get files for folder.</summary>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		public string[] GetFilesForFolder(string path)
		{
			return Directory.GetFiles(path, "*.mt", SearchOption.TopDirectoryOnly);
		}

		/// <summary>The get template path.</summary>
		/// <returns>The get template path.</returns>
		public string GetTemplatePath()
		{
			string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			path = Path.Combine(path, TemplateResources.TemplatesDirectoryName);

			return path;
		}

		/// <summary>The infer extension from filename.</summary>
		/// <param name="filename">The filename.</param>
		/// <param name="items">The items.</param>
		/// <returns>The infer extension from filename.</returns>
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

		/// <summary>The pre process template.</summary>
		/// <param name="lines">The lines.</param>
		/// <param name="getValueForParameter">The get value for parameter.</param>
		/// <param name="items">The items.</param>
		/// <returns>The pre process template.</returns>
		public string PreProcessTemplate(
			string[] lines, 
			GetValueForParameter getValueForParameter, 
			Dictionary<string, object> items)
		{
			int i = 0;
			for (; i < lines.Length; i++)
			{
				string line = lines[i];
				if (line.StartsWith("#@"))
				{
// process cmd
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

		/// <summary>The process template.</summary>
		/// <param name="text">The text.</param>
		/// <param name="items">The items.</param>
		/// <returns></returns>
		public TemplateResult ProcessTemplate(string text, Dictionary<string, object> items)
		{
			if (items == null)
			{
				items = new Dictionary<string, object>();
			}

			TemplateResult result;

			using (TemplateHost host = _services.Resolve<TemplateHost>())
			{
				items.Add("Host", host);
				items.Add("Data", host.Data);

				result = new TemplateResult();
				result.Text = _formatter.Format(text, items);
				result.Extension = "sql";
				if (items.ContainsKey(Extension))
				{
					result.Extension = (string)items[Extension];
				}
			}

			return result;
		}

		/// <summary>The process template file.</summary>
		/// <param name="filename">The filename.</param>
		/// <param name="getValueForParameter">The get value for parameter.</param>
		/// <returns></returns>
		public TemplateResult ProcessTemplateFile(string filename, GetValueForParameter getValueForParameter)
		{
			Dictionary<string, object> items = new Dictionary<string, object>();
			string[] lines = File.ReadAllLines(filename);

			items[Extension] = InferExtensionFromFilename(filename, items);

			string text = PreProcessTemplate(lines, getValueForParameter, items);
			return ProcessTemplate(text, items);
		}
	}
}