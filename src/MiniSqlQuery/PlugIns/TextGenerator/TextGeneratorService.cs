using System;
using System.Globalization;
using System.Text;

namespace MiniSqlQuery.PlugIns.TextGenerator
{
    public class TextGeneratorService
    {
        private const string SpaceString = " ";

        public string Process(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            // convert to a class, line 1 is name, rest are props

            var lines = text.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            var sb = new StringBuilder();

            // class name
            sb.Append("public class ");
            sb.AppendLine(ToPascalCase(lines[0]));
            sb.AppendLine("{");

            // properties
            if (lines.Length > 1)
            {
                for (int i = 1; i < lines.Length; i++)
                {
                    string typeName = "string";
                    var propertyName = ToPascalCase(lines[i]);

                    // is it an "id"
                    if (propertyName.EndsWith("id", StringComparison.CurrentCultureIgnoreCase))
                    {
                        typeName = "int";
                    }

                    sb.Append("public virtual ");
                    sb.Append(typeName);
                    sb.Append(" ");
                    sb.Append(propertyName);
                    sb.AppendLine(" { get; set; }");
                }
            }

            sb.AppendLine("}");

            return sb.ToString();
        }

        public string ToPascalCase(string text)
        {
            // if it has spaces, e.g "first name", or maybe its lower, e.g. "foo". Still allow for "MyID" etc
            if (text.Contains(SpaceString) || !char.IsUpper(text[0]))
            {
                return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text).Replace(SpaceString, string.Empty).Trim();
            }
            return text.Trim();
        }
    }
}