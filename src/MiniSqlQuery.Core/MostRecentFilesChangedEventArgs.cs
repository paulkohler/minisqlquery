using System;
using System.Collections.Generic;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// 	A system wide message event.
	/// </summary>
	public class MostRecentFilesChangedEventArgs : EventArgs
	{
		public MostRecentFilesChangedEventArgs(ICollection<string> filenames)
		{
			Filenames = filenames;
		}

		public ICollection<string> Filenames { get; private set; }
	}
}