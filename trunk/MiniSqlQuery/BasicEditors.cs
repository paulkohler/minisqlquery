using System;
using MiniSqlQuery.Core;

namespace MiniSqlQuery
{
	public class BasicAspxEditor : BasicEditor
	{
		public BasicAspxEditor(IApplicationServices services, IApplicationSettings settings) : base(services, settings)
		{
			SetSyntax("ASP/XHTML");
		}
	}

	public class BasicBatchEditor : BasicEditor
	{
		public BasicBatchEditor(IApplicationServices services, IApplicationSettings settings) : base(services, settings)
		{
			SetSyntax("BAT");
		}
	}

	public class BasicBooEditor : BasicEditor
	{
		public BasicBooEditor(IApplicationServices services, IApplicationSettings settings) : base(services, settings)
		{
			SetSyntax("Boo");
		}
	}

	public class BasicCocoEditor : BasicEditor
	{
		public BasicCocoEditor(IApplicationServices services, IApplicationSettings settings) : base(services, settings)
		{
			SetSyntax("Coco");
		}
	}

	public class BasicCPlusPlusEditor : BasicEditor
	{
		public BasicCPlusPlusEditor(IApplicationServices services, IApplicationSettings settings) : base(services, settings)
		{
			SetSyntax("C++.NET");
		}
	}

	public class BasicCSharpEditor : BasicEditor
	{
		public BasicCSharpEditor(IApplicationServices services, IApplicationSettings settings) : base(services, settings)
		{
			SetSyntax("C#");
		}
	}

	public class BasicHtmlEditor : BasicEditor
	{
		public BasicHtmlEditor(IApplicationServices services, IApplicationSettings settings) : base(services, settings)
		{
			SetSyntax("HTML");
		}
	}

	public class BasicJavaEditor : BasicEditor
	{
		public BasicJavaEditor(IApplicationServices services, IApplicationSettings settings) : base(services, settings)
		{
			SetSyntax("Java");
		}
	}

	public class BasicJavaScriptEditor : BasicEditor
	{
		public BasicJavaScriptEditor(IApplicationServices services, IApplicationSettings settings) : base(services, settings)
		{
			SetSyntax("JavaScript");
		}
	}

	public class BasicPatchEditor : BasicEditor
	{
		public BasicPatchEditor(IApplicationServices services, IApplicationSettings settings) : base(services, settings)
		{
			SetSyntax("Patch");
		}
	}

	public class BasicPhpEditor : BasicEditor
	{
		public BasicPhpEditor(IApplicationServices services, IApplicationSettings settings) : base(services, settings)
		{
			SetSyntax("PHP");
		}
	}

	public class BasicTexEditor : BasicEditor
	{
		public BasicTexEditor(IApplicationServices services, IApplicationSettings settings) : base(services, settings)
		{
			SetSyntax("TeX");
		}
	}

	public class BasicVbNetEditor : BasicEditor
	{
		public BasicVbNetEditor(IApplicationServices services, IApplicationSettings settings) : base(services, settings)
		{
			SetSyntax("VBNET");
		}
	}

	public class BasicXmlEditor : BasicEditor
	{
		public BasicXmlEditor(IApplicationServices services, IApplicationSettings settings) : base(services, settings)
		{
			SetSyntax("XML");
		}
	}
}