#region License

// Copyright 2005-2009 Paul Kohler (https://github.com/paul-kohler-au/minisqlquery). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license

#endregion

using System;
using System.ComponentModel;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// 	A configuration object for use with the options form.
	/// </summary>
	public interface IConfigurationObject : INotifyPropertyChanged
	{
		/// <summary>
		/// 	Gets a value indicating whether the settings are dirty.
		/// </summary>
		/// <value>The is dirty.</value>
		bool IsDirty { get; }

		/// <summary>
		/// 	Gets the Name of the settings.
		/// </summary>
		/// <value>The name of the settings.</value>
		string Name { get; }

		/// <summary>
		/// 	Gets a Settings object.
		/// </summary>
		/// <value>The settings.</value>
		object Settings { get; }

		/// <summary>
		/// 	Saves the settings.
		/// </summary>
		void Save();
	}
}