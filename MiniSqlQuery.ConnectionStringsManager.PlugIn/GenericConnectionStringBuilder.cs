using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Collections;
using System.Data.Common;

namespace MiniSqlQuery.ConnectionStringsManager.PlugIn
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
