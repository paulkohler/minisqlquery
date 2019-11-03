#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE

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
using MiniSqlQuery.PlugIns.TextGenerator;
using MiniSqlQuery.PlugIns.ViewTable;
using MiniSqlQuery.Properties;

namespace MiniSqlQuery
{
    /// <summary>
    /// 	The application entry point.
    /// </summary>
    internal static class App
    {
        /// <summary>
        /// 	The configure container.
        /// </summary>
        /// <param name = "services">The services.</param>
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

        /// <summary>
        /// 	The application thread exception.
        /// </summary>
        /// <param name = "sender">The sender.</param>
        /// <param name = "e">The e.</param>
        private static void ApplicationThreadException(object sender, ThreadExceptionEventArgs e)
        {
            if (!(e.Exception is ThreadAbortException))
            {
                HandleException(e.Exception);
            }
        }

        /// <summary>
        /// 	The current domain unhandled exception.
        /// </summary>
        /// <param name = "sender">The sender.</param>
        /// <param name = "e">The e.</param>
        private static void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (!(e.ExceptionObject is ThreadAbortException))
            {
                HandleException((Exception)e.ExceptionObject);
            }
        }

        /// <summary>
        /// 	The handle exception.
        /// </summary>
        /// <param name = "e">The e.</param>
        private static void HandleException(Exception e)
        {
            ErrorForm errorForm = new ErrorForm();
            errorForm.SetException(e);
            errorForm.ShowDialog();
            errorForm.Dispose();
        }

        /// <summary>
        /// 	The main entry point for the application.
        /// </summary>
        /// <param name = "args">The args.</param>
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
            services.LoadPlugIn(new TextGeneratorLoader());

            if (services.Settings.LoadExternalPlugins)
            {
                var plugins = PlugInUtility.GetInstances<IPlugIn>(Environment.CurrentDirectory, Settings.Default.PlugInFileFilter);
                Array.Sort(plugins, new PlugInComparer());
                foreach (var plugin in plugins)
                {
                    services.LoadPlugIn(plugin);
                }
            }

            services.HostWindow.SetArguments(args);
            Application.Run(services.HostWindow.Instance);
        }
    }
}