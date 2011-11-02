using System;
using System.Collections.Generic;
using MiniSqlQuery.Commands;
using MiniSqlQuery.Core;
using NUnit.Framework;
using Rhino.Mocks;

// ReSharper disable InconsistentNaming

namespace MiniSqlQuery.Tests.MRU
{
	[TestFixture]
	public class OpenRecentFileCommand_Tests
	{
		private IMostRecentFilesService _service;

		[Test]
		public void If_File_in_slot_1_the_command_is_enabled()
		{
			_service.Expect(s => s.Filenames).Return(new List<string> {"File1"});

			var cmd = new OpenRecentFileCommand(_service, 1);

			Assert.That(cmd.Enabled, Is.True);
			_service.VerifyAllExpectations();
		}

		[Test]
		public void If_no_Files_the_command_is_disabled()
		{
			_service.Expect(s => s.Filenames).Return(new List<string>());

			var cmd = new OpenRecentFileCommand(_service, 1);

			Assert.That(cmd.Enabled, Is.False);
		}

		[SetUp]
		public void TestSetUp()
		{
			_service = MockRepository.GenerateMock<IMostRecentFilesService>();
		}

		[Test]
		public void The_name_is_updated()
		{
			_service.Expect(s => s.Filenames).Return(new List<string> {"File1", "File2"});

			var cmd = new OpenRecentFileCommand(_service, 1);
			cmd.UpdateName();

			Assert.That(cmd.Name, Is.EqualTo("&1 - File1"));
		}
	}
}