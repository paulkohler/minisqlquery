#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using MiniSqlQuery.Core;
using MiniSqlQuery.PlugIns.TemplateViewer.Commands;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery.PlugIns.TemplateViewer
{
	/// <summary>The template viewer loader.</summary>
	public class TemplateViewerLoader : PluginLoaderBase
	{
		/// <summary>Initializes a new instance of the <see cref="TemplateViewerLoader"/> class.</summary>
		public TemplateViewerLoader()
			: base("Template Viewer", "A Mini SQL Query Plugin for displaying template SQL items.", 50)
		{
		}

		/// <summary>Iinitialize the plug in.</summary>
		public override void InitializePlugIn()
		{
			Services.RegisterEditor<TemplateEditorForm>(new FileEditorDescriptor("Template Editor", "mt-editor", "mt"));
			Services.RegisterComponent<TemplateHost>("TemplateHost");
			Services.RegisterComponent<TemplateData>("TemplateData");

			Services.RegisterComponent<TemplateViewForm>("TemplateViewForm");
			Services.HostWindow.AddPluginCommand<RunTemplateCommand>();
			Services.HostWindow.ShowToolWindow(Services.Resolve<TemplateViewForm>(), DockState.DockLeft);
		}
	}
}