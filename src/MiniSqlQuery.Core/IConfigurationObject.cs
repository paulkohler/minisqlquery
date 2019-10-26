#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE

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