#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion


namespace MiniSqlQuery.Core.DbModel
{
    /// <summary>The db model view.</summary>
    public class DbModelView : DbModelTable
    {
        /// <summary>Initializes a new instance of the <see cref="DbModelView"/> class.</summary>
        public DbModelView()
        {
            ObjectType = ObjectTypes.View;
        }
    }
}