#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using System.Windows.Forms;
using Ninject;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery.Core.Commands
{
    /// <summary>
    /// 	The new query form command.
    /// </summary>
    public class NewQueryFormCommand
        : CommandBase
    {
        /// <summary>
        /// 	Initializes a new instance of the <see cref = "NewQueryFormCommand" /> class.
        /// </summary>
        public NewQueryFormCommand()
            : base("New &Query Window")
        {
            ShortcutKeys = Keys.Control | Keys.N;
            SmallImage = ImageResource.page_white;
        }

        /// <summary>
        /// 	Execute the command.
        /// </summary>
        public override void Execute()
        {
            var editor = Services.Container.Get<IQueryEditor>();
            editor.FileName = null;
            HostWindow.DisplayDockedForm(editor as DockContent);
        }
    }
}