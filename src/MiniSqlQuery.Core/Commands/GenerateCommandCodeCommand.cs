#region License

// Copyright 2005-2009 Paul Kohler (https://github.com/paul-kohler-au/minisqlquery). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using Ninject;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery.Core.Commands
{
	/// <summary>
	/// 	The generate command code command.
	/// </summary>
	public class GenerateCommandCodeCommand
		: CommandBase
	{
		/// <summary>
		/// 	Initializes a new instance of the <see cref = "GenerateCommandCodeCommand" /> class.
		/// </summary>
		public GenerateCommandCodeCommand()
			: base("Generate Command Code")
		{
			SmallImage = ImageResource.cog;
		}

		/// <summary>
		/// 	Execute the command.
		/// </summary>
		public override void Execute()
		{
			string template =
				@"    public class $name$Command
        : CommandBase
    {
        public $name$Command()
            : base(""$desc$"")
        {
            //ShortcutKeys = Keys.Control | Keys.?;
			//SmallImage = ImageResource.?;
		}

        public override void Execute()
        {
			
        }
    }";

			string code = template
				.Replace("$name$", "OI")
				.Replace("$desc$", "a thing");

			var editor = Services.Container.Get<IQueryEditor>();
			editor.AllText = code;
			editor.SetSyntax("C#");

			HostWindow.DisplayDockedForm(editor as DockContent);
		}
	}
}