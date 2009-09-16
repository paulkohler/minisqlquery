using System;
using System.Collections.Generic;
using System.IO;
using NVelocity;
using NVelocity.App;
using NVelocity.Exception;
using NVelocity.Runtime.Log;

namespace MiniSqlQuery.Core.Template
{
	public class NVelocityWrapper : ITextFormatter
	{
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

				Boolean ok = velocityEngine.Evaluate(velocityContext, sw, "ContextTest.CaseInsensitive", text);

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