using System;
using System.IO;
using System.Text;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// A <see cref="StringWriter"/> that allows the setting of the <see cref="Encoding"/>.
	/// </summary>
	public class StringWriterWithEncoding : StringWriter
	{
		private readonly Encoding _encoding;

		/// <summary>
		/// Initializes a new instance of the <see cref="StringWriterWithEncoding"/> class.
		/// </summary>
		/// <param name="encoding">The encoding to use, e.g. Encoding.UTF8.</param>
		public StringWriterWithEncoding(Encoding encoding)
		{
			_encoding = encoding;
		}

		/// <summary>
		/// Gets the <see cref="T:System.Text.Encoding"/> in which the output is written.
		/// </summary>
		/// <value>The encoding type.</value>
		/// <returns>
		/// The Encoding in which the output is written.
		/// </returns>
		public override Encoding Encoding
		{
			get { return _encoding; }
		}
	}
}