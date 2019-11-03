#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

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