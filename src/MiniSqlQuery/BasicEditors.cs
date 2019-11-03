#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using System;
using MiniSqlQuery.Core;

namespace MiniSqlQuery
{
	/// <summary>The basic aspx editor.</summary>
	public class BasicAspxEditor : BasicEditor
	{
		/// <summary>Initializes a new instance of the <see cref="BasicAspxEditor"/> class.</summary>
		/// <param name="services">The services.</param>
		/// <param name="settings">The settings.</param>
		public BasicAspxEditor(IApplicationServices services, IApplicationSettings settings) : base(services, settings)
		{
			SetSyntax("ASP/XHTML");
		}

		/// <summary>Gets FileFilter.</summary>
		public override string FileFilter
		{
			get { return "ASPX Files (*.asp;*.aspx;*.asax;*.asmx)|*.asp;*.aspx;*.asax;*.asmx|All Files (*.*)|*.*"; }
		}
	}

	/// <summary>The basic batch editor.</summary>
	public class BasicBatchEditor : BasicEditor
	{
		/// <summary>Initializes a new instance of the <see cref="BasicBatchEditor"/> class.</summary>
		/// <param name="services">The services.</param>
		/// <param name="settings">The settings.</param>
		public BasicBatchEditor(IApplicationServices services, IApplicationSettings settings) : base(services, settings)
		{
			SetSyntax("BAT");
		}

		/// <summary>Gets FileFilter.</summary>
		public override string FileFilter
		{
			get { return "Batch Files (*.bat;*.cmd)|*.bat;*.cmd|All Files (*.*)|*.*"; }
		}
	}

	/// <summary>The basic boo editor.</summary>
	public class BasicBooEditor : BasicEditor
	{
		/// <summary>Initializes a new instance of the <see cref="BasicBooEditor"/> class.</summary>
		/// <param name="services">The services.</param>
		/// <param name="settings">The settings.</param>
		public BasicBooEditor(IApplicationServices services, IApplicationSettings settings) : base(services, settings)
		{
			SetSyntax("Boo");
		}

		/// <summary>Gets FileFilter.</summary>
		public override string FileFilter
		{
			get { return "BOO Files (*.boo)|*.boo|All Files (*.*)|*.*"; }
		}
	}

	/// <summary>The basic coco editor.</summary>
	public class BasicCocoEditor : BasicEditor
	{
		/// <summary>Initializes a new instance of the <see cref="BasicCocoEditor"/> class.</summary>
		/// <param name="services">The services.</param>
		/// <param name="settings">The settings.</param>
		public BasicCocoEditor(IApplicationServices services, IApplicationSettings settings) : base(services, settings)
		{
			SetSyntax("Coco");
		}

		/// <summary>Gets FileFilter.</summary>
		public override string FileFilter
		{
			get { return "Coco Files (*.atg)|*.atg|All Files (*.*)|*.*"; }
		}
	}

	/// <summary>The basic c plus plus editor.</summary>
	public class BasicCPlusPlusEditor : BasicEditor
	{
		/// <summary>Initializes a new instance of the <see cref="BasicCPlusPlusEditor"/> class.</summary>
		/// <param name="services">The services.</param>
		/// <param name="settings">The settings.</param>
		public BasicCPlusPlusEditor(IApplicationServices services, IApplicationSettings settings) : base(services, settings)
		{
			SetSyntax("C++.NET");
		}

		/// <summary>Gets FileFilter.</summary>
		public override string FileFilter
		{
			get { return "C++ Files (*.cpp;*.cc;*.c;*.h)|*.cpp;*.cc;*.c;*.h|All Files (*.*)|*.*"; }
		}
	}

	/// <summary>The basic c sharp editor.</summary>
	public class BasicCSharpEditor : BasicEditor
	{
		/// <summary>Initializes a new instance of the <see cref="BasicCSharpEditor"/> class.</summary>
		/// <param name="services">The services.</param>
		/// <param name="settings">The settings.</param>
		public BasicCSharpEditor(IApplicationServices services, IApplicationSettings settings) : base(services, settings)
		{
			SetSyntax("C#");
		}

		/// <summary>Gets FileFilter.</summary>
		public override string FileFilter
		{
			get { return "C# Files (*.cs)|*.cs|All Files (*.*)|*.*"; }
		}
	}

	/// <summary>The basic html editor.</summary>
	public class BasicHtmlEditor : BasicEditor
	{
		/// <summary>Initializes a new instance of the <see cref="BasicHtmlEditor"/> class.</summary>
		/// <param name="services">The services.</param>
		/// <param name="settings">The settings.</param>
		public BasicHtmlEditor(IApplicationServices services, IApplicationSettings settings) : base(services, settings)
		{
			SetSyntax("HTML");
		}

		/// <summary>Gets FileFilter.</summary>
		public override string FileFilter
		{
			get { return "HTML Files (*.htm*)|*.htm*|All Files (*.*)|*.*"; }
		}
	}

	/// <summary>The basic java editor.</summary>
	public class BasicJavaEditor : BasicEditor
	{
		/// <summary>Initializes a new instance of the <see cref="BasicJavaEditor"/> class.</summary>
		/// <param name="services">The services.</param>
		/// <param name="settings">The settings.</param>
		public BasicJavaEditor(IApplicationServices services, IApplicationSettings settings) : base(services, settings)
		{
			SetSyntax("Java");
		}

		/// <summary>Gets FileFilter.</summary>
		public override string FileFilter
		{
			get { return "Java Files (*.java)|*.java|All Files (*.*)|*.*"; }
		}
	}

	/// <summary>The basic java script editor.</summary>
	public class BasicJavaScriptEditor : BasicEditor
	{
		/// <summary>Initializes a new instance of the <see cref="BasicJavaScriptEditor"/> class.</summary>
		/// <param name="services">The services.</param>
		/// <param name="settings">The settings.</param>
		public BasicJavaScriptEditor(IApplicationServices services, IApplicationSettings settings) : base(services, settings)
		{
			SetSyntax("JavaScript");
		}

		/// <summary>Gets FileFilter.</summary>
		public override string FileFilter
		{
			get { return "JavaScript Files (*.js)|*.js|All Files (*.*)|*.*"; }
		}
	}

	/// <summary>The basic patch editor.</summary>
	public class BasicPatchEditor : BasicEditor
	{
		/// <summary>Initializes a new instance of the <see cref="BasicPatchEditor"/> class.</summary>
		/// <param name="services">The services.</param>
		/// <param name="settings">The settings.</param>
		public BasicPatchEditor(IApplicationServices services, IApplicationSettings settings) : base(services, settings)
		{
			SetSyntax("Patch");
		}

		/// <summary>Gets FileFilter.</summary>
		public override string FileFilter
		{
			get { return "Patch Files (*.patch;*.diff)|*.patch;*.diff|All Files (*.*)|*.*"; }
		}
	}

	/// <summary>The basic php editor.</summary>
	public class BasicPhpEditor : BasicEditor
	{
		/// <summary>Initializes a new instance of the <see cref="BasicPhpEditor"/> class.</summary>
		/// <param name="services">The services.</param>
		/// <param name="settings">The settings.</param>
		public BasicPhpEditor(IApplicationServices services, IApplicationSettings settings) : base(services, settings)
		{
			SetSyntax("PHP");
		}

		/// <summary>Gets FileFilter.</summary>
		public override string FileFilter
		{
			get { return "PHP Files (*.php*)|*.php*|All Files (*.*)|*.*"; }
		}
	}

	/// <summary>The basic tex editor.</summary>
	public class BasicTexEditor : BasicEditor
	{
		/// <summary>Initializes a new instance of the <see cref="BasicTexEditor"/> class.</summary>
		/// <param name="services">The services.</param>
		/// <param name="settings">The settings.</param>
		public BasicTexEditor(IApplicationServices services, IApplicationSettings settings) : base(services, settings)
		{
			SetSyntax("TeX");
		}

		/// <summary>Gets FileFilter.</summary>
		public override string FileFilter
		{
			get { return "TeX Files (*.tex)|*.tex|All Files (*.*)|*.*"; }
		}
	}

	/// <summary>The basic vb net editor.</summary>
	public class BasicVbNetEditor : BasicEditor
	{
		/// <summary>Initializes a new instance of the <see cref="BasicVbNetEditor"/> class.</summary>
		/// <param name="services">The services.</param>
		/// <param name="settings">The settings.</param>
		public BasicVbNetEditor(IApplicationServices services, IApplicationSettings settings) : base(services, settings)
		{
			SetSyntax("VBNET");
		}

		/// <summary>Gets FileFilter.</summary>
		public override string FileFilter
		{
			get { return "VB.NET Files (*.vb)|*.vb|All Files (*.*)|*.*"; }
		}
	}

	/// <summary>The basic xml editor.</summary>
	public class BasicXmlEditor : BasicEditor
	{
		/// <summary>Initializes a new instance of the <see cref="BasicXmlEditor"/> class.</summary>
		/// <param name="services">The services.</param>
		/// <param name="settings">The settings.</param>
		public BasicXmlEditor(IApplicationServices services, IApplicationSettings settings) : base(services, settings)
		{
			SetSyntax("XML");
		}

		/// <summary>Gets FileFilter.</summary>
		public override string FileFilter
		{
			get { return "XML Files (*.xml;*.resx)|*.xml;*.resx|All Files (*.*)|*.*"; }
		}
	}
}