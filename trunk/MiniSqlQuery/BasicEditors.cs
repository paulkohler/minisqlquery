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
		public override string FileFilter { get { return "ASPX Files (*.asp;*.aspx;*.asax;*.asmx)|*.asp;*.aspx;*.asax;*.asmx|All Files (*.*)|*.*"; } }
	}

	public class BasicBatchEditor : BasicEditor
	{
		public BasicBatchEditor(IApplicationServices services, IApplicationSettings settings) : base(services, settings)
		{
			SetSyntax("BAT");
		}
		public override string FileFilter { get { return "Batch Files (*.bat;*.cmd)|*.bat;*.cmd|All Files (*.*)|*.*"; } }
	}

	public class BasicBooEditor : BasicEditor
	{
		public BasicBooEditor(IApplicationServices services, IApplicationSettings settings) : base(services, settings)
		{
			SetSyntax("Boo");
		}
		public override string FileFilter { get { return "BOO Files (*.boo)|*.boo|All Files (*.*)|*.*"; } }
	}

	public class BasicCocoEditor : BasicEditor
	{
		public BasicCocoEditor(IApplicationServices services, IApplicationSettings settings) : base(services, settings)
		{
			SetSyntax("Coco");
		}
		public override string FileFilter { get { return "Coco Files (*.atg)|*.atg|All Files (*.*)|*.*"; } }
	}

	public class BasicCPlusPlusEditor : BasicEditor
	{
		public BasicCPlusPlusEditor(IApplicationServices services, IApplicationSettings settings) : base(services, settings)
		{
			SetSyntax("C++.NET");
		}
		public override string FileFilter { get { return "C++ Files (*.cpp;*.cc;*.c;*.h)|*.cpp;*.cc;*.c;*.h|All Files (*.*)|*.*"; } }
	}

	public class BasicCSharpEditor : BasicEditor
	{
		public BasicCSharpEditor(IApplicationServices services, IApplicationSettings settings) : base(services, settings)
		{
			SetSyntax("C#");
		}
		public override string FileFilter { get { return "C# Files (*.cs)|*.cs|All Files (*.*)|*.*"; } }
	}

	public class BasicHtmlEditor : BasicEditor
	{
		public BasicHtmlEditor(IApplicationServices services, IApplicationSettings settings) : base(services, settings)
		{
			SetSyntax("HTML");
		}
		public override string FileFilter { get { return "HTML Files (*.htm*)|*.htm*|All Files (*.*)|*.*"; } }
	}

	public class BasicJavaEditor : BasicEditor
	{
		public BasicJavaEditor(IApplicationServices services, IApplicationSettings settings) : base(services, settings)
		{
			SetSyntax("Java");
		}
		public override string FileFilter { get { return "Java Files (*.java)|*.java|All Files (*.*)|*.*"; } }
	}

	public class BasicJavaScriptEditor : BasicEditor
	{
		public BasicJavaScriptEditor(IApplicationServices services, IApplicationSettings settings) : base(services, settings)
		{
			SetSyntax("JavaScript");
		}
		public override string FileFilter { get { return "JavaScript Files (*.js)|*.js|All Files (*.*)|*.*"; } }
	}

	public class BasicPatchEditor : BasicEditor
	{
		public BasicPatchEditor(IApplicationServices services, IApplicationSettings settings) : base(services, settings)
		{
			SetSyntax("Patch");
		}
		public override string FileFilter { get { return "Patch Files (*.patch;*.diff)|*.patch;*.diff|All Files (*.*)|*.*"; } }
	}

	public class BasicPhpEditor : BasicEditor
	{
		public BasicPhpEditor(IApplicationServices services, IApplicationSettings settings) : base(services, settings)
		{
			SetSyntax("PHP");
		}
		public override string FileFilter { get { return "PHP Files (*.php*)|*.php*|All Files (*.*)|*.*"; } }
	}

	public class BasicTexEditor : BasicEditor
	{
		public BasicTexEditor(IApplicationServices services, IApplicationSettings settings) : base(services, settings)
		{
			SetSyntax("TeX");
		}
		public override string FileFilter { get { return "TeX Files (*.tex)|*.tex|All Files (*.*)|*.*"; } }
	}

	public class BasicVbNetEditor : BasicEditor
	{
		public BasicVbNetEditor(IApplicationServices services, IApplicationSettings settings) : base(services, settings)
		{
			SetSyntax("VBNET");
		}
		public override string FileFilter { get { return "VB.NET Files (*.vb)|*.vb|All Files (*.*)|*.*"; } }
	}

	public class BasicXmlEditor : BasicEditor
	{
		public BasicXmlEditor(IApplicationServices services, IApplicationSettings settings) : base(services, settings)
		{
			SetSyntax("XML");
		}
		public override string FileFilter { get { return "XML Files (*.xml;*.resx)|*.xml;*.resx|All Files (*.*)|*.*"; } }
	}
}