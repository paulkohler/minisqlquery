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
using MiniSqlQuery.Properties;

namespace MiniSqlQuery
{
	/// <summary>The about form.</summary>
	internal partial class AboutForm : Form
	{
		/// <summary>The _services.</summary>
		private readonly IApplicationServices _services;

		/// <summary>Initializes a new instance of the <see cref="AboutForm"/> class.</summary>
		/// <param name="services">The services.</param>
		public AboutForm(IApplicationServices services) : this()
		{
			_services = services;
		}

		/// <summary>Initializes a new instance of the <see cref="AboutForm"/> class.</summary>
		public AboutForm()
		{
			InitializeComponent();
			pic.Image = ImageResource.ApplicationIcon;
			Text = String.Format("About {0}", AssemblyTitle);
			labelProductName.Text = AssemblyTitle;
			labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
			labelCopyright.Text = AssemblyCopyright;
			textBoxDescription.Text = AssemblyDescription;
		}

		/// <summary>Gets AssemblyCompany.</summary>
		public string AssemblyCompany
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
				if (attributes.Length == 0)
				{
					return string.Empty;
				}

				return ((AssemblyCompanyAttribute)attributes[0]).Company;
			}
		}

		/// <summary>Gets AssemblyCopyright.</summary>
		public string AssemblyCopyright
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
				if (attributes.Length == 0)
				{
					return string.Empty;
				}

				return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
			}
		}

		/// <summary>Gets AssemblyDescription.</summary>
		public string AssemblyDescription
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), 
				                                                                          false);
				if (attributes.Length == 0)
				{
					return string.Empty;
				}

				return ((AssemblyDescriptionAttribute)attributes[0]).Description;
			}
		}

		/// <summary>Gets AssemblyProduct.</summary>
		public string AssemblyProduct
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
				if (attributes.Length == 0)
				{
					return string.Empty;
				}

				return ((AssemblyProductAttribute)attributes[0]).Product;
			}
		}

		/// <summary>Gets AssemblyTitle.</summary>
		public string AssemblyTitle
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
				if (attributes.Length > 0)
				{
					AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
					if (titleAttribute.Title != string.Empty)
					{
						return titleAttribute.Title;
					}
				}

				return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
			}
		}

		/// <summary>Gets AssemblyVersion.</summary>
		public string AssemblyVersion
		{
			get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
		}

		/// <summary>The about form_ load.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void AboutForm_Load(object sender, EventArgs e)
		{
			List<IPlugIn> plugins = new List<IPlugIn>(_services.Plugins.Values);
			pluginList.SetDataSource(plugins.ToArray());
			webBrowser1.DocumentText = Resources.ReadMe;
			txtLicense.Text = Resources.LicenseMiniSqlQuery;
		}

		/// <summary>The label version_ click.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void labelVersion_Click(object sender, EventArgs e)
		{
		}

		/// <summary>The ok button_ click.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void okButton_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}