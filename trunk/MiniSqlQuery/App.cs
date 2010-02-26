#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System;
using System.Threading;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.DbModel;
using MiniSqlQuery.Core.Forms;
using MiniSqlQuery.Core.Template;
using MiniSqlQuery.PlugIns;
using MiniSqlQuery.PlugIns.ConnectionStringsManager;
using MiniSqlQuery.PlugIns.DatabaseInspector;
using MiniSqlQuery.PlugIns.SearchTools;
using MiniSqlQuery.PlugIns.TemplateViewer;
using MiniSqlQuery.PlugIns.ViewTable;
using MiniSqlQuery.Properties;

namespace MiniSqlQuery
{
	internal static class App
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
#if !DEBUG
			AppDomain.CurrentDomain.UnhandledException += CurrentDomainUnhandledException;
			Application.ThreadException += ApplicationThreadException;
#endif

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			IApplicationServices services = ApplicationServices.Instance;

			ConfigureContainer(services);

			services.LoadPlugIn(new CoreApplicationPlugIn());
			services.LoadPlugIn(new ConnectionStringsManagerLoader());
			services.LoadPlugIn(new DatabaseInspectorLoader());
			services.LoadPlugIn(new ViewTableLoader());
			services.LoadPlugIn(new TemplateViewerLoader());
			services.LoadPlugIn(new SearchToolsLoader());

			if (services.Settings.LoadExternalPlugins)
			{
				IPlugIn[] plugins = PlugInUtility.GetInstances<IPlugIn>(Environment.CurrentDirectory, Settings.Default.PlugInFileFilter);
				Array.Sort(plugins, new PlugInComparer());
				foreach (IPlugIn plugin in plugins)
				{
					services.LoadPlugIn(plugin);
				}
			}

			services.HostWindow.SetArguments(args);
			Application.Run(services.HostWindow.Instance);
		}

		public static void ConfigureContainer(IApplicationServices services)
		{
			// singletons
			services.RegisterSingletonComponent<IApplicationSettings, ApplicationSettings>("ApplicationSettings");
			services.RegisterSingletonComponent<IHostWindow, MainForm>("HostWindow");
			services.RegisterSingletonComponent<IFileEditorResolver, FileEditorResolverService>("FileEditorResolver");

			// components
			services.RegisterComponent<AboutForm>("AboutForm");
			services.RegisterComponent<ITextFindService, BasicTextFindService>("DefaultTextFindService");
			services.RegisterComponent<IQueryEditor, QueryForm>("QueryForm");
			services.RegisterComponent<ISqlWriter, SqlWriter>("DefaultSqlWriter");
			services.RegisterComponent<ITextFormatter, NVelocityWrapper>("TextFormatter");
			services.RegisterComponent<TemplateModel>("TemplateModel");
			services.RegisterComponent<BatchQuerySelectForm>("BatchQuerySelectForm");
		}

// ReSharper disable UnusedMember.Local
		private static void ApplicationThreadException(object sender, ThreadExceptionEventArgs e)
		{
			if (!(e.Exception is ThreadAbortException))
			{
				HandleException(e.Exception);
			}
		}

		private static void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			if (!(e.ExceptionObject is ThreadAbortException))
			{
				HandleException((Exception) e.ExceptionObject);
			}
		}
// ReSharper restore UnusedMember.Local

		private static void HandleException(Exception e)
		{
			ErrorForm errorForm = new ErrorForm();
			errorForm.SetException(e);
			errorForm.ShowDialog();
			errorForm.Dispose();
		}
	}
}