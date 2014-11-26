using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;

namespace MiniSqlQuery.Core.Template
{
	/// <summary>
	/// From an online debate on parsing, original author todo....
	/// </summary>
	public class HenriFormatter : ITextFormatter
	{
		#region ITextFormatter Members

		public string Format(string text, object dataSource)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}

			StringBuilder result = new StringBuilder(text.Length*2);

			using (var reader = new StringReader(text))
			{
				StringBuilder expression = new StringBuilder();
				int @char = -1;

				TemplateParseState templateParseState = TemplateParseState.OutsideExpression;
				do
				{
					switch (templateParseState)
					{
						case TemplateParseState.OutsideExpression:
							@char = reader.Read();
							switch (@char)
							{
								case -1:
									templateParseState = TemplateParseState.End;
									break;
								case '{':
									templateParseState = TemplateParseState.OnOpenBracket;
									break;
								case '}':
									templateParseState = TemplateParseState.OnCloseBracket;
									break;
								default:
									result.Append((char) @char);
									break;
							}
							break;
						case TemplateParseState.OnOpenBracket:
							@char = reader.Read();
							switch (@char)
							{
								case -1:
									throw new FormatException();
								case '{':
									result.Append('{');
									templateParseState = TemplateParseState.OutsideExpression;
									break;
								default:
									expression.Append((char) @char);
									templateParseState = TemplateParseState.InsideExpression;
									break;
							}
							break;
						case TemplateParseState.InsideExpression:
							@char = reader.Read();
							switch (@char)
							{
								case -1:
									throw new FormatException();
								case '}':
									result.Append(ResolveExpression(dataSource, expression.ToString()));
									expression.Length = 0;
									templateParseState = TemplateParseState.OutsideExpression;
									break;
								default:
									expression.Append((char) @char);
									break;
							}
							break;
						case TemplateParseState.OnCloseBracket:
							@char = reader.Read();
							switch (@char)
							{
								case '}':
									result.Append('}');
									templateParseState = TemplateParseState.OutsideExpression;
									break;
								default:
									throw new FormatException();
							}
							break;
						default:
							throw new InvalidOperationException("Invalid state.");
					}
				}
				while (templateParseState != TemplateParseState.End);
			}

			return result.ToString();
		}

		#endregion

		private string ResolveExpression(object source, string expression)
		{
			string format = "";

			int colonIndex = expression.IndexOf(':');
			if (colonIndex > 0)
			{
				format = expression.Substring(colonIndex + 1);
				expression = expression.Substring(0, colonIndex);
			}

			try
			{
				// yes, it uses the databinder
				if (String.IsNullOrEmpty(format))
				{
					return (DataBinder.Eval(source, expression) ?? "").ToString();
				}
				return DataBinder.Eval(source, expression, "{0:" + format + "}") ?? "";
			}
			catch (HttpException exp)
			{
				throw new FormatException(exp.Message);
			}
		}

		#region Nested type: TemplateParseState

		private enum TemplateParseState
		{
			OutsideExpression,
			OnOpenBracket,
			InsideExpression,
			OnCloseBracket,
			End
		}

		#endregion
	}
}