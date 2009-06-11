using System;
using System.IO;
using System.Text;

namespace MiniSqlQuery.Core
{
	public class StringWriterWithEncoding : StringWriter
	{
		private readonly Encoding _encoding;

		public StringWriterWithEncoding(Encoding encoding)
		{
			_encoding = encoding;
		}

		public override Encoding Encoding
		{
			get { return _encoding; }
		}
	}
}