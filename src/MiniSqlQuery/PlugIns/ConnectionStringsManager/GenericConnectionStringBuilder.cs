#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Reflection;

namespace MiniSqlQuery.PlugIns.ConnectionStringsManager
{
    /// <summary>The generic connection string builder.</summary>
    [DefaultMember("Item"), DefaultProperty("ConnectionString")]
    public class GenericConnectionStringBuilder : DbConnectionStringBuilder
    {
        /// <summary>The _properties.</summary>
        private Hashtable _properties;

        /// <summary>Initializes a new instance of the <see cref="GenericConnectionStringBuilder"/> class.</summary>
        public GenericConnectionStringBuilder()
        {
            Initialize(null);
        }

        /// <summary>Initializes a new instance of the <see cref="GenericConnectionStringBuilder"/> class.</summary>
        /// <param name="connectionString">The connection string.</param>
        public GenericConnectionStringBuilder(string connectionString)
        {
            Initialize(connectionString);
        }

        /// <summary>The try get value.</summary>
        /// <param name="keyword">The keyword.</param>
        /// <param name="value">The value.</param>
        /// <returns>The try get value.</returns>
        public override bool TryGetValue(string keyword, out object value)
        {
            bool success = base.TryGetValue(keyword, out value);
            if (_properties.ContainsKey(keyword))
            {
                PropertyDescriptor descriptor = _properties[keyword] as PropertyDescriptor;
                if (descriptor == null)
                {
                    return success;
                }

                if (success)
                {
                    value = TypeDescriptor.GetConverter(descriptor.PropertyType).ConvertFrom(value);
                    return success;
                }

                DefaultValueAttribute attribute = descriptor.Attributes[typeof(DefaultValueAttribute)] as DefaultValueAttribute;
                if (attribute != null)
                {
                    value = attribute.Value;
                    success = true;
                }
            }

            return success;
        }

        /// <summary>The initialize.</summary>
        /// <param name="cnnString">The cnn string.</param>
        private void Initialize(string cnnString)
        {
            _properties = new Hashtable();
            this.GetProperties(_properties);
            if (!string.IsNullOrEmpty(cnnString))
            {
                ConnectionString = cnnString;
            }
        }
    }
}