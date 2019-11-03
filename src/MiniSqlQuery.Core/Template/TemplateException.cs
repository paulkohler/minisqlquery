#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using System;
using System.Runtime.Serialization;

namespace MiniSqlQuery.Core.Template
{
	/// <summary>The template exception.</summary>
	[Serializable]
	public class TemplateException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="TemplateException"/> class.</summary>
		public TemplateException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="TemplateException"/> class.</summary>
		/// <param name="message">The message.</param>
		public TemplateException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="TemplateException"/> class.</summary>
		/// <param name="message">The message.</param>
		/// <param name="inner">The inner.</param>
		public TemplateException(string message, Exception inner) : base(message, inner)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="TemplateException"/> class.</summary>
		/// <param name="info">The info.</param>
		/// <param name="context">The context.</param>
		protected TemplateException(
			SerializationInfo info, 
			StreamingContext context) : base(info, context)
		{
		}
	}
}