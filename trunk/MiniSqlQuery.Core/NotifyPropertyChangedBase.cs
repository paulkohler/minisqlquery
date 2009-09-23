using System;
using System.ComponentModel;

namespace MiniSqlQuery.Core
{
	public abstract class NotifyPropertyChangedBase : INotifyPropertyChanged
	{
		public virtual event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler changed = PropertyChanged;
			if (changed != null)
			{
				changed(this, new PropertyChangedEventArgs(propertyName));
			}
		}

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