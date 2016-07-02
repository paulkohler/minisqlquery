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