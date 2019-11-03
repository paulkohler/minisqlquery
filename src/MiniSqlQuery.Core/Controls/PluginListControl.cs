#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using System;
using System.Windows.Forms;

namespace MiniSqlQuery.Core.Controls
{
	/// <summary>A simple control to display plugin details.</summary>
	public partial class PluginListControl : UserControl
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PluginListControl"/> class.
		/// </summary>
		public PluginListControl()
		{
			InitializeComponent();
		}

		/// <summary>Sets the data source to the <paramref name="plugins"/>.</summary>
		/// <param name="plugins">The plugins to display.</param>
		public void SetDataSource(IPlugIn[] plugins)
		{
			foreach (IPlugIn plugin in plugins)
			{
				ListViewItem item = new ListViewItem(new[]
				                                     	{
				                                     		plugin.PluginName, plugin.PluginDescription, plugin.GetType().Assembly.FullName
				                                     	});
				listView1.Items.Add(item);
			}
		}
	}
}