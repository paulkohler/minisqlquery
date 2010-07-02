#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.ComponentModel;

namespace MiniSqlQuery.Core
{
	/// <summary>The notify property changed base.</summary>
	public abstract class NotifyPropertyChangedBase : INotifyPropertyChanged
	{
		/// <summary>The property changed.</summary>
		public virtual event PropertyChangedEventHandler PropertyChanged;

		/// <summary>The on property changed.</summary>
		/// <param name="propertyName">The property name.</param>
		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler changed = PropertyChanged;
			if (changed != null)
			{
				changed(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		/// <summary>The on property changed.</summary>
		/// <param name="e">The events.</param>
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