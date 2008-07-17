using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.Design;
using System.Reflection;
using System.IO;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// Helper class for loading external plugins.
	/// </summary>
	public class PlugInUtility
	{
		/// <summary>
		/// Search <paramref name="baseDir"/> for files that match the pattern <paramref name="searchPattern"/>
		/// and return an array of instances.
		/// </summary>
		/// <typeparam name="T">The type (interface or class) to find instances of.</typeparam>
		/// <param name="baseDir">The search base.</param>
		/// <param name="searchPattern">Search pattern, e.g. "*.dll".</param>
		/// <remarks>
		/// This is a simple way of doing what Windsor does well, todo - convert to Windsor container for plugins!
		/// </remarks>
		public static T[] GetInstances<T>(string baseDir, string searchPattern)
		{
			List<T> tmpInstances = new List<T>();
			string[] files = null;
			Assembly pluginAssembly = null;
			Type[] assemblyTypes = null;
			Type instanceType = null;

			try
			{
				// perform the file search
				files = Directory.GetFiles(baseDir, searchPattern, SearchOption.TopDirectoryOnly);

				// load each asembly and inspect for instances of T
				foreach (string file in files)
				{
					pluginAssembly = Assembly.LoadFrom(file);
					assemblyTypes = pluginAssembly.GetTypes();

					// check each assembly to se it it implements the interface T
					foreach (Type assemblyType in assemblyTypes)
					{
						instanceType = assemblyType.GetInterface(typeof(T).FullName);
						if (instanceType != null)
						{
							// this instance type matches T, create an instance of the class and add it to the list
							tmpInstances.Add((T)Activator.CreateInstance(assemblyType));
						}
					}
				}
			}
			catch (TargetInvocationException exp)
			{
				if (exp.InnerException != null)
				{
					throw exp.InnerException;
				}
			}

			return tmpInstances.ToArray();
		}

	}
}
