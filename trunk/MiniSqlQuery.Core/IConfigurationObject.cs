using System;
using System.ComponentModel;

namespace MiniSqlQuery.Core
{
	public interface IConfigurationObject : INotifyPropertyChanged
	{
		bool IsDirty { get; }
		object Settings { get; }
		string Name { get; }
		void Save();
	}
}