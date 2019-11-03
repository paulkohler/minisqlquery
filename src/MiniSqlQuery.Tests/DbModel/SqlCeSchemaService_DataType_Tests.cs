#region License
// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion
using System;
using System.Collections.Generic;
using System.Data.Common;
using MiniSqlQuery.Core.DbModel;
using NUnit.Framework;

namespace MiniSqlQuery.Tests.DbModel
{
    [TestFixture(Description = "Requires SQLCE DB")]
    [Category("Functional")]
    public class SqlCeSchemaService_DataType_Tests
    {
        #region Setup/Teardown

        [SetUp]
        public void TestSetup()
        {
            _service = new SqlCeSchemaService();
            DbConnection conn = DbProviderFactories.GetFactory(_providerName).CreateConnection();
            conn.ConnectionString = _connStr;
            conn.Open();
            _dbTypes = _service.GetDbTypes(conn);
        }

        #endregion

        private SqlCeSchemaService _service;
        private string _connStr = @"data source=|DataDirectory|\sqlce-test.v4.sdf";
        private string _providerName = "System.Data.SqlServerCe.4.0";
        Dictionary<string, DbModelType> _dbTypes;

        [Test]
        public void There_are_at_least_18_types()
        {
            Assert.That(_dbTypes.Count, Is.GreaterThanOrEqualTo(18));
        }

        [Test]
        public void NVarchar_type()
        {
            DbModelType dbType = _dbTypes["nvarchar"];
            Assert.That(dbType.Name, Is.EqualTo("nvarchar").IgnoreCase);
            Assert.That(dbType.Length, Is.EqualTo(4000));
            Assert.That(dbType.CreateFormat, Is.EqualTo("nvarchar({0})"));
            Assert.That(dbType.LiteralPrefix, Is.EqualTo("N'"));
            Assert.That(dbType.LiteralSuffix, Is.EqualTo("'"));
            Assert.That(dbType.SystemType, Is.EqualTo(typeof(string)));
        }

        [Test]
        public void Money_type()
        {
            DbModelType dbType = _dbTypes["money"];
            Assert.That(dbType.Name, Is.EqualTo("money").IgnoreCase);
            Assert.That(dbType.Summary, Is.EqualTo("money"));
            Assert.That(dbType.SystemType, Is.EqualTo(typeof(decimal)));
        }

        [Test]
        public void Float_type()
        {
            DbModelType dbType = _dbTypes["float"];
            Assert.That(dbType.Name, Is.EqualTo("float").IgnoreCase);
            Assert.That(dbType.Summary, Is.EqualTo("float"));
            Assert.That(dbType.SystemType, Is.EqualTo(typeof(double)));
        }

        [Test]
        [Ignore("run this manually if needed, just a helper")]
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