#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System;
using System.Runtime.Serialization;

namespace MiniSqlQuery.Core.Template
{
	[Serializable]
	public class TemplateException : Exception
	{
		public TemplateException()
		{
		}

		public TemplateException(string message) : base(message)
		{
		}

		public TemplateException(string message, Exception inner) : base(message, inner)
		{
		}

		protected TemplateException(
			SerializationInfo info,
			StreamingContext context) : base(info, context)
		{
		}
	}
}