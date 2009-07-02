using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using MiniSqlQuery.Core;

namespace MiniSqlQuery
{
	internal partial class AboutForm : Form
	{
		private IApplicationServices _services;

		public AboutForm(IApplicationServices services) : this()
		{
			_services = services;
		}

		public AboutForm()
		{
			InitializeComponent();
			this.Text = String.Format("About {0}", AssemblyTitle);
			this.labelProductName.Text = AssemblyTitle;
			this.labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
			this.labelCopyright.Text = AssemblyCopyright;
			this.textBoxDescription.Text = AssemblyDescription;
		}

		#region Assembly Attribute Accessors

		public string AssemblyTitle
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyTitleAttribute), false);
				if (attributes.Length > 0)
				{
					AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute) attributes[0];
					if (titleAttribute.Title != "")
					{
						return titleAttribute.Title;
					}
				}
				return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
			}
		}

		public string AssemblyVersion
		{
			get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
		}

		public string AssemblyDescription
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyDescriptionAttribute),
				                                                                          false);
				if (attributes.Length == 0)
				{
					return "";
				}
				return ((AssemblyDescriptionAttribute) attributes[0]).Description;
			}
		}

		public string AssemblyProduct
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyProductAttribute), false);
				if (attributes.Length == 0)
				{
					return "";
				}
				return ((AssemblyProductAttribute) attributes[0]).Product;
			}
		}

		public string AssemblyCopyright
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyCopyrightAttribute), false);
				if (attributes.Length == 0)
				{
					return "";
				}
				return ((AssemblyCopyrightAttribute) attributes[0]).Copyright;
			}
		}

		public string AssemblyCompany
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyCompanyAttribute), false);
				if (attributes.Length == 0)
				{
					return "";
				}
				return ((AssemblyCompanyAttribute) attributes[0]).Company;
			}
		}

		#endregion

		private void okButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void labelVersion_Click(object sender, EventArgs e)
		{
		}

		private void AboutForm_Load(object sender, EventArgs e)
		{
			List<IPlugIn> plugins = new List<IPlugIn>(_services.Plugins.Values);
			pluginList.SetDataSource(plugins.ToArray());
		}

        private void linkLabelIcons_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Utility.ShowUrl("http://www.famfamfam.com/");
        }

        private void linkLabelTextEditor_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Utility.ShowUrl("http://www.icsharpcode.net/OpenSource/SD/");
        }

        private void linkLabelDockPanel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Utility.ShowUrl("http://www.codeproject.com/KB/miscctrl/DockManager.aspx");
        }
	}
}