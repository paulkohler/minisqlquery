#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license

#endregion

using System;
using System.Collections.Generic;

namespace MiniSqlQuery.Core
{
	public class MostRecentFilesService : IMostRecentFilesService
	{
		private readonly List<string> _filenames;

		public MostRecentFilesService()
		{
			_filenames = new List<string>();
		}

		public event EventHandler<MostRecentFilesChangedEventArgs> MostRecentFilesChanged;

		public IList<string> Filenames
		{
			get { return _filenames; }
		}

		public void OnMostRecentFilesChanged(MostRecentFilesChangedEventArgs e)
		{
			var handler = MostRecentFilesChanged;
			if (handler != null)
			{
				handler(this, e);
			}
		}

		public void Register(string filename)
		{
			if (!_filenames.Contains(filename))
			{
				_filenames.Insert(0, filename);
			}
			else
			{
				// move to top of list
				if (_filenames.Count > 1)
				{
					_filenames.Remove(filename);
					_filenames.Insert(0, filename);
				}
			}

			// enure the list is capped
			while (_filenames.Count > MaxCommands)
			{
				_filenames.RemoveAt(_filenames.Count - 1);
			}

			NotifyListenersOfChange();
		}

		public void Remove(string filename)
		{
			if (_filenames.Contains(filename))
			{
				_filenames.Remove(filename);
			}
			NotifyListenersOfChange();
		}

		public int MaxCommands
		{
			get { return 10; }
		}

		protected void NotifyListenersOfChange()
		{
			OnMostRecentFilesChanged(new MostRecentFilesChangedEventArgs(_filenames));
		}
	}
}