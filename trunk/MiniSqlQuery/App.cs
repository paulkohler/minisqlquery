using System;
using System.Threading;
using System.Windows.Forms;
using Castle.Core;
using MiniSqlQuery.Core;
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

			// singletons
			ApplicationServices.Instance.Container.AddComponentWithLifestyle<IApplicationSettings, ApplicationSettings>(
				"ApplicationSettings", LifestyleType.Singleton);
			ApplicationServices.Instance.Container.AddComponentWithLifestyle<IHostWindow, MainForm>(
				"HostWindow", LifestyleType.Singleton);

			// components
            ApplicationServices.Instance.Container.AddComponentWithLifestyle<AboutForm>(
                "AboutForm", LifestyleType.Transient);
			ApplicationServices.Instance.Container.AddComponentWithLifestyle<ITextFindService, BasicTextFindService>(
				"DefaultTextFindService", LifestyleType.Transient);
			ApplicationServices.Instance.Container.AddComponentWithLifestyle<IQueryEditor, QueryForm>(
				"QueryForm", LifestyleType.Transient);

            ApplicationServices.Instance.LoadPlugIn(new CoreApplicationPlugIn());
            ApplicationServices.Instance.LoadPlugIn(new ConnectionStringsManagerLoader());
            ApplicationServices.Instance.LoadPlugIn(new DatabaseInspectorLoader());
            ApplicationServices.Instance.LoadPlugIn(new ViewTableLoader());
            ApplicationServices.Instance.LoadPlugIn(new TemplateViewerLoader());
            ApplicationServices.Instance.LoadPlugIn(new SearchToolsLoader());

            IPlugIn[] plugins = PlugInUtility.GetInstances<IPlugIn>(Environment.CurrentDirectory, Settings.Default.PlugInFileFilter);
            Array.Sort(plugins, new PlugInComparer());
            foreach (IPlugIn plugin in plugins)
            {
                ApplicationServices.Instance.LoadPlugIn(plugin);
            }

            ApplicationServices.Instance.HostWindow.SetArguements(args);
            Application.Run(ApplicationServices.Instance.HostWindow.Instance);
		}

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

		private static void HandleException(Exception e)
		{
			ErrorForm errorForm = new ErrorForm();
			errorForm.SetException(e);
			errorForm.ShowDialog();
			errorForm.Dispose();
		}
	}
}