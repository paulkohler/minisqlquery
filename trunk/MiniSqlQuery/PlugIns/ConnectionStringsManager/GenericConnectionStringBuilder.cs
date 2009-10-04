#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Collections;
using System.Data.Common;

namespace MiniSqlQuery.PlugIns.ConnectionStringsManager
{
	[DefaultMember("Item"), DefaultProperty("ConnectionString")]
	public class GenericConnectionStringBuilder : DbConnectionStringBuilder
	{
		private Hashtable _properties;

		public GenericConnectionStringBuilder()
		{
			Initialize(null);
		}

		public GenericConnectionStringBuilder(string connectionString)
		{
			Initialize(connectionString);
		}

		private void Initialize(string cnnString)
		{
			_properties = new Hashtable();
			base.GetProperties(_properties);
			if (!string.IsNullOrEmpty(cnnString))
			{
				base.ConnectionString = cnnString;
			}
		}

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
	}
}