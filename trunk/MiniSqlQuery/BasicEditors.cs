using System;
using MiniSqlQuery.Core;

namespace MiniSqlQuery
{
	public class BasicAspxEditor : BasicEditor
	{
		public BasicAspxEditor(IApplicationServices services) : base(services)
		{
			SetSyntax("ASP/XHTML");
		}
	}

	public class BasicBatchEditor : BasicEditor
	{
		public BasicBatchEditor(IApplicationServices services) : base(services)
		{
			SetSyntax("BAT");
		}
	}

	public class BasicBooEditor : BasicEditor
	{
		public BasicBooEditor(IApplicationServices services) : base(services)
		{
			SetSyntax("Boo");
		}
	}

	public class BasicCocoEditor : BasicEditor
	{
		public BasicCocoEditor(IApplicationServices services) : base(services)
		{
			SetSyntax("Coco");
		}
	}

	public class BasicCPlusPlusEditor : BasicEditor
	{
		public BasicCPlusPlusEditor(IApplicationServices services) : base(services)
		{
			SetSyntax("C++.NET");
		}
	}

	public class BasicCSharpEditor : BasicEditor
	{
		public BasicCSharpEditor(IApplicationServices services) : base(services)
		{
			SetSyntax("C#");
		}
	}

	public class BasicHtmlEditor : BasicEditor
	{
		public BasicHtmlEditor(IApplicationServices services) : base(services)
		{
			SetSyntax("HTML");
		}
	}

	public class BasicJavaEditor : BasicEditor
	{
		public BasicJavaEditor(IApplicationServices services) : base(services)
		{
			SetSyntax("Java");
		}
	}

	public class BasicJavaScriptEditor : BasicEditor
	{
		public BasicJavaScriptEditor(IApplicationServices services) : base(services)
		{
			SetSyntax("JavaScript");
		}
	}

	public class BasicPatchEditor : BasicEditor
	{
		public BasicPatchEditor(IApplicationServices services) : base(services)
		{
			SetSyntax("Patch");
		}
	}

	public class BasicPhpEditor : BasicEditor
	{
		public BasicPhpEditor(IApplicationServices services) : base(services)
		{
			SetSyntax("PHP");
		}
	}

	public class BasicTexEditor : BasicEditor
	{
		public BasicTexEditor(IApplicationServices services) : base(services)
		{
			SetSyntax("TeX");
		}
	}

	public class BasicVbNetEditor : BasicEditor
	{
		public BasicVbNetEditor(IApplicationServices services) : base(services)
		{
			SetSyntax("VBNET");
		}
	}

	public class BasicXmlEditor : BasicEditor
	{
		public BasicXmlEditor(IApplicationServices services) : base(services)
		{
			SetSyntax("XML");
		}
	}
}