#region License

// Copyright 2005-2009 Paul Kohler (https://github.com/paul-kohler-au/minisqlquery). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using NVelocity;
using NVelocity.App;
using NVelocity.Exception;

namespace MiniSqlQuery.Core.Template
{
	/// <summary>The n velocity wrapper.</summary>
	public class NVelocityWrapper : ITextFormatter
	{
		/// <summary>The format.</summary>
		/// <param name="text">The text.</param>
		/// <param name="items">The items.</param>
		/// <returns>The format.</returns>
		/// <exception cref="TemplateException"></exception>
		public string Format(string text, Dictionary<string, object> items)
		{
			try
			{
				VelocityContext velocityContext = new VelocityContext();

				if (items != null)
				{
					foreach (var pair in items)
					{
						velocityContext.Put(pair.Key, pair.Value);
					}
				}

				StringWriter sw = new StringWriter();
				VelocityEngine velocityEngine = new VelocityEngine();
				velocityEngine.Init();

				bool ok = velocityEngine.Evaluate(velocityContext, sw, "ContextTest.CaseInsensitive", text);

				if (!ok)
				{
					throw new TemplateException("Template run error (try adding an extra newline at the end of the file)");
				}

				return sw.ToString();
			}
			catch (ParseErrorException parseErrorException)
			{
				throw new TemplateException(parseErrorException.Message, parseErrorException);
			}
		}
	}
}