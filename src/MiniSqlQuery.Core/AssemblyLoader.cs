#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license

#endregion

using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// 	Helper class for loading external plugins.
	/// </summary>
	public class AssemblyLoader
	{
		/// <summary>
		/// 	Search <paramref name = "baseDir" /> for files that match the pattern <paramref name = "searchPattern" />
		/// 	and return an array of instances.
		/// </summary>
		/// <returns>An array of instances of the plugins found.</returns>
		/// <typeparam name = "T">The type (interface or class) to find instances of.</typeparam>
		/// <param name = "baseDir">The search base.</param>
		/// <param name = "searchPattern">Search pattern, e.g. "*.dll".</param>
		public static IEnumerable<T> GetInstances<T>(string baseDir, string searchPattern)
		{
			try
			{
				return Directory.GetFiles(baseDir, searchPattern, SearchOption.TopDirectoryOnly)
					.Where(IsDllOrExe)
					.Select(Assembly.LoadFrom)
					.SelectMany(x => x.GetTypes())
					.Where(IsNewClassOfType<T>)
					.Select(Activator.CreateInstance)
					.Cast<T>();
			}
			catch (TargetInvocationException exp)
			{
				if (exp.InnerException != null)
				{
					throw exp.InnerException;
				}
			}
			return null;
		}
		private static bool IsDllOrExe(string file)
		{
			var ext = Path.GetExtension(file);
			return ext == ".dll" || ext == ".exe";
		}
		private static bool IsNewClassOfType<T>(Type type)
		{
			return typeof(T).IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface;
		}
	}
}