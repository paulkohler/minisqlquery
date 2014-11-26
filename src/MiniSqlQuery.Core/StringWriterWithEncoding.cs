#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license

#endregion

using System;
using System.IO;
using System.Text;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// 	A <see cref = "StringWriter" /> that allows the setting of the <see cref = "Encoding" />.
	/// </summary>
	public class StringWriterWithEncoding : StringWriter
	{
		/// <summary>
		/// 	The _encoding.
		/// </summary>
		private readonly Encoding _encoding;

		/// <summary>
		/// 	Initializes a new instance of the <see cref = "StringWriterWithEncoding" /> class.
		/// </summary>
		/// <param name = "encoding">The encoding to use, e.g. Encoding.UTF8.</param>
		public StringWriterWithEncoding(Encoding encoding)
		{
			_encoding = encoding;
		}

		/// <summary>
		/// 	Gets the <see cref = "T:System.Text.Encoding" /> in which the output is written.
		/// </summary>
		/// <value>The encoding type.</value>
		/// <returns>
		/// 	The Encoding in which the output is written.
		/// </returns>
		public override Encoding Encoding
		{
			get { return _encoding; }
		}
	}
}