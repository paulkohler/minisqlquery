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
	/// 	The notify property changed base class.
	/// </summary>
	public abstract class NotifyPropertyChangedBase : INotifyPropertyChanged
	{
		/// <summary>
		/// 	The property changed event, fired when a propety is modified.
		/// </summary>
		public virtual event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// 	The on property changed method (by property name).
		/// </summary>
		/// <param name = "propertyName">The property name that has been modified.</param>
		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler changed = PropertyChanged;
			if (changed != null)
			{
				changed(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		/// <summary>
		/// 	The on property changed method.
		/// </summary>
		/// <param name = "e">The events.</param>
		protected void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			PropertyChangedEventHandler changed = PropertyChanged;
			if (changed != null)
			{
				changed(this, e);
			}
		}
	}
}