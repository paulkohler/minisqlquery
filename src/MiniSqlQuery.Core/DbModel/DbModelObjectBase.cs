#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE

#endregion

using System;

namespace MiniSqlQuery.Core.DbModel
{
    /// <summary>The db model object base.</summary>
    public class DbModelObjectBase : IDbModelNamedObject
    {
        private string _fullName;

        /// <summary>
        /// Gets the full name of the object which may include the <see cref="IDbModelNamedObject.Schema"/> for example.
        /// </summary>
        /// <value>The full name.</value>
        public virtual string FullName
        {
            get
            {
                if (_fullName == null)
                {
                    _fullName = Utility.RenderSafeSchemaObjectName(Schema, Name);
                }

                return _fullName;
            }
        }

        /// <summary>Gets or sets name of the database object.</summary>
        /// <value>The name of the object.</value>
        public virtual string Name { get; set; }

        /// <summary>Gets or sets ObjectType.</summary>
        /// <value>The object type.</value>
        public virtual string ObjectType { get; set; }

        /// <summary>Gets or sets Schema.</summary>
        /// <value>The schema.</value>
        public virtual string Schema { get; set; }
    }
}