#region License

// Copyright 2005-2009 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery.Commands
{
    /// <summary>
    /// 	The set the name global "file name" command.
    /// </summary>
    /// <remarks>
    /// example:
    /// 
    /// MiniSqlQuery /cmd:SetGlobalFilename /cmd:OpenGlobalFile /cmd:ExecuteTask /cmd:ExitApplication
    /// </remarks>
    public class SetGlobalFilenameCommand : CommandBase
    {
        public static string Filename { get; set; }

        /// <summary>
        /// 	Initializes a new instance of the <see cref = "SetGlobalFilenameCommand" /> class.
        /// </summary>
        public SetGlobalFilenameCommand()
            : base("Set the global filename")
        {
        }

        /// <summary>
        /// 	Execute the command.
        /// </summary>
        public override void Execute()
        {
            Filename = Services.GetEnvironmentVariable("minisqlqueryfile");
        }
    }

    /// <summary>The open file command.</summary>
    public class OpenGlobalFileCommand
        : CommandBase
    {
        /// <summary>Initializes a new instance of the <see cref="OpenGlobalFileCommand"/> class.</summary>
        public OpenGlobalFileCommand()
            : base("Open File")
        {
        }

        /// <summary>Execute the command.</summary>
        public override void Execute()
        {
            if (!String.IsNullOrEmpty(SetGlobalFilenameCommand.Filename))
            {
                IFileEditorResolver resolver = Services.Resolve<IFileEditorResolver>();
                var fileName = SetGlobalFilenameCommand.Filename;
                IEditor editor = resolver.ResolveEditorInstance(fileName);
                editor.FileName = fileName;
                editor.LoadFile();
                HostWindow.DisplayDockedForm(editor as DockContent);

                Services.Resolve<IMostRecentFilesService>().Register(fileName);
            }
        }
    }
}