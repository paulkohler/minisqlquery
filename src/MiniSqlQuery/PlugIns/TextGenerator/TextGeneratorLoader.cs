#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using System;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.PlugIns.TextGenerator.Commands;

namespace MiniSqlQuery.PlugIns.TextGenerator
{
	public class TextGeneratorLoader : PluginLoaderBase
	{
		public TextGeneratorLoader()
			: base(
				"Text Generator Tools", 
				"A Mini SQL Query Plugin for generating test from... text :-)", 
				21)
		{
		}

		public override void InitializePlugIn()
		{
			IHostWindow hostWindow = Services.HostWindow;
			hostWindow.AddPluginCommand<RunTextGeneratorCommand>();
		}
	}
}