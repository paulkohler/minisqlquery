#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System;
using System.Collections.Generic;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.DbModel;
using NUnit.Framework;

using System.Data.Common;

namespace MiniSqlQuery.Tests.DbModel
{
	[TestFixture(Description = "Requires MS Access DB")]
	[Category("Functional")]
	[Ignore]
	public class MSAccess_DataType_Tests
	{
		#region Setup/Teardown

		[SetUp]
		public void TestSetup()
		{
			_service = new OleDbSchemaService();
			DbConnection conn = DbProviderFactories.GetFactory(_providerName).CreateConnection();
			conn.ConnectionString = _connStr;
			conn.Open();
			_dbTypes = _service.GetDbTypes(conn);
		}

		#endregion

		private OleDbSchemaService _service;
		private string _connStr = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Projects\minisqlquery\trunk\TestDatabases\NorthWindDemo.mdb";
		private string _providerName = "System.Data.OleDb";
		Dictionary<string, DbModelType> _dbTypes;

		[Test]
		public void There_are_at_least_15_types_for_access()
		{
			Assert.That(_dbTypes.Count, Is.GreaterThanOrEqualTo(15));
			/*
Short  (System.Int16)
Long  (System.Int32)
Single  (System.Single)
Double  (System.Double)
Currency  (System.Decimal)
DateTime  (System.DateTime)
Bit  (System.Boolean)
Byte  (System.Byte)
GUID  (System.Guid)
BigBinary  (System.Byte[])
LongBinary  (System.Byte[])
VarBinary  (System.Byte[])
LongText  (System.String)
VarChar  (System.String)
Decimal  (System.Decimal)
			 */
		}

		[Test]
		public void VarChar_type()
		{
			DbModelType dbType = _dbTypes["varchar"];
			Assert.That(dbType.Name, Is.EqualTo("VarChar").IgnoreCase);
			Assert.That(dbType.Length, Is.EqualTo(255));
			Assert.That(dbType.CreateFormat, Is.EqualTo("VarChar({0})"));
			Assert.That(dbType.LiteralPrefix, Is.EqualTo("'"));
			Assert.That(dbType.LiteralSuffix, Is.EqualTo("'"));
		}

		[Test]
		public void VarBinary_type()
		{
			DbModelType dbType = _dbTypes["varbinary"];
			Assert.That(dbType.Name, Is.EqualTo("VarBinary").IgnoreCase);
			Assert.That(dbType.CreateFormat, Is.EqualTo("VarBinary({0})"));
			Assert.That(dbType.LiteralPrefix, Is.EqualTo("0x"));
			Assert.That(dbType.LiteralSuffix, Is.Empty);
		}

		[Test]
		public void LongText_type()
		{
			DbModelType dbType = _dbTypes["longtext"];
			Assert.That(dbType.Name, Is.EqualTo("LongText").IgnoreCase);
			Assert.That(dbType.Summary, Is.EqualTo("LongText"));
			Assert.That(dbType.LiteralPrefix, Is.EqualTo("'"));
			Assert.That(dbType.LiteralSuffix, Is.EqualTo("'"));
		}

		[Test]
		public void DateTime_type()
		{
			DbModelType dbType = _dbTypes["datetime"];
			Assert.That(dbType.Name, Is.EqualTo("DateTime").IgnoreCase);
			Assert.That(dbType.Summary, Is.EqualTo("DateTime"));
			Assert.That(dbType.LiteralPrefix, Is.EqualTo("#"));
			Assert.That(dbType.LiteralSuffix, Is.EqualTo("#"));
			var copy = dbType.Copy();
			copy.Value = new DateTime(2000, 12, 17);
			Assert.That(copy.ToDDLValue(), Is.EqualTo("#17/12/2000 12:00:00 AM#"));
		}

		[Test, Explicit]
		public void Show_all()
		{
			List<DbModelType> ary = new List<DbModelType>(_dbTypes.Values);
			foreach (var dbModelType in ary)
			{
				Console.WriteLine("{0}  ({1})", dbModelType.Summary, dbModelType.SystemType);
			}
		}
	}
}