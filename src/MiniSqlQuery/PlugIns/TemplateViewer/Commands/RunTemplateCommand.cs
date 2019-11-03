#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;

namespace MiniSqlQuery.PlugIns.TemplateViewer.Commands
{
    /// <summary>The run template command.</summary>
    public class RunTemplateCommand
        : CommandBase
    {
        /// <summary>Initializes a new instance of the <see cref="RunTemplateCommand"/> class.</summary>
        public RunTemplateCommand()
            : base("Run Template")
        {
            SmallImage = ImageResource.script_code;
        }

        /// <summary>Gets a value indicating whether Enabled.</summary>
        public override bool Enabled
        {
            get { return HostWindow.ActiveChildForm is ITemplateEditor; }
        }

        /// <summary>Execute the command.</summary>
        public override void Execute()
        {
            ITemplateEditor templateEditor = HostWindow.ActiveChildForm as ITemplateEditor;

            if (templateEditor != null)
            {
                templateEditor.RunTemplate();
            }
        }
    }
}