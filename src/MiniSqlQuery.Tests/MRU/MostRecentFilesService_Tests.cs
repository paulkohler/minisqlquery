#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using MiniSqlQuery.Core;
using NUnit.Framework;

// ReSharper disable InconsistentNaming

namespace MiniSqlQuery.Tests.MRU
{
	[TestFixture]
	public class MostRecentFilesService_Tests
	{
		private MostRecentFilesService _service;

		[Test]
		public void List_does_not_exceed_maximum_entries()
		{
			// set up the max entries
			int i = 1;
			for (; i <= _service.MaxCommands; i++)
			{
				_service.Register(i.ToString());
			}

			// add 1 extra
			i++;
			_service.Register(i.ToString());

			Assert.That(_service.Filenames.Count, Is.EqualTo(_service.MaxCommands));
		}

		[Test]
		public void No_filenames_at_start()
		{
			Assert.That(_service.Filenames.Count, Is.EqualTo(0));
		}

		[Test]
		public void Register_adds_new_files()
		{
			_service.Register("1");
			_service.Register("2");
			Assert.That(_service.Filenames.Count, Is.EqualTo(2));
		}

		[Test]
		public void Register_adds_only_new_files()
		{
			_service.Register("1");
			_service.Register("2");
			_service.Register("2");
			_service.Register("2");
			Assert.That(_service.Filenames.Count, Is.EqualTo(2));
		}

		[Test]
		public void Register_takes_off_relevent_file()
		{
			_service.Register("1");
			_service.Register("2");
			_service.Register("3");
			Assert.That(_service.Filenames.Count, Is.EqualTo(3));

			_service.Remove("2");
			Assert.That(_service.Filenames.Count, Is.EqualTo(2));
			Assert.That(_service.Filenames[0], Is.EqualTo("3"));
			Assert.That(_service.Filenames[1], Is.EqualTo("1"));
		}

		[SetUp]
		public void TestSetUp()
		{
			_service = new MostRecentFilesService();
		}
	}
}