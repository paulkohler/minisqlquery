#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion


namespace MiniSqlQuery.PlugIns.TemplateViewer
{
    /// <summary>The template result.</summary>
    public class TemplateResult
    {
        /// <summary>Gets or sets Extension.</summary>
        public string Extension { get; set; }

        /// <summary>
        /// Converts the <see cref="Extension"/> to an Editor "Syntax Name" such as "C#".
        /// </summary>
        public string SyntaxName
        {
            get
            {
                string ext = Extension ?? string.Empty;
                switch (ext.ToLower())
                {
                    case "bat":
                    case "boo":
                    case "coco":
                    case "java":
                    case "patch":
                    case "php":
                    case "tex":
                    case "xml":
                    case "sql":
                    case "txt":
                        return Extension;

                    case "asp":
                    case "aspx":
                    case "htm":
                    case "html":
                        return "ASP/XHTML";

                    case "vb":
                        return "VBNET";

                    case "js":
                        return "JavaScript";

                    case "cpp":
                    case "cxx":
                        return "C++.NET";

                    case "cs":
                        return "C#";

                    default:
                        return string.Empty;
                }
            }
        }

        /// <summary>Gets or sets Text.</summary>
        public string Text { get; set; }
    }
}