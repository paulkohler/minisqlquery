using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery.Commands
{
    /// <summary>
    /// 	Opens a recent file in a list by the <see cref = "Index" />.
    /// 	Makes use of <see cref = "IMostRecentFilesService" /> to calculate the filename.
    /// </summary>
    public class OpenRecentFileCommand
        : CommandBase
    {
        private readonly int _index;
        private IMostRecentFilesService _mostRecentFilesService;

        /// <summary>
        /// 	Initializes a new instance of the <see cref = "OpenRecentFileCommand" /> class.
        /// </summary>
        protected OpenRecentFileCommand(int index)
            : base("MRU")
        {
            _index = index;
        }

        public OpenRecentFileCommand(IMostRecentFilesService mostRecentFilesService, int index)
            : base("MRU")
        {
            _mostRecentFilesService = mostRecentFilesService;
            _index = index;
        }

        public override bool Enabled
        {
            get
            {
                if (MostRecentFilesService.Filenames != null)
                {
                    return MostRecentFilesService.Filenames.Count >= Index;
                }
                return false;
            }
        }

        public int Index
        {
            get { return _index; }
        }

        public IMostRecentFilesService MostRecentFilesService
        {
            get
            {
                if (_mostRecentFilesService == null)
                {
                    _mostRecentFilesService = Services.Resolve<IMostRecentFilesService>();
                    _mostRecentFilesService.MostRecentFilesChanged += MostRecentFilesServiceFilesChanged;
                }
                return _mostRecentFilesService;
            }
        }

        void MostRecentFilesServiceFilesChanged(object sender, MostRecentFilesChangedEventArgs e)
        {
            UpdateName();
        }

        public void UpdateName()
        {
            if (Enabled)
            {
                Name = string.Format("&{0} - {1}", Index, GetFilenameByIndex());
            }
        }

        public override void Execute()
        {
            if (!Enabled)
            {
                return;
            }

            var fileEditorResolver = Services.Resolve<IFileEditorResolver>();
            _mostRecentFilesService = Services.Resolve<IMostRecentFilesService>();
            string fileName = GetFilenameByIndex();

            var editor = fileEditorResolver.ResolveEditorInstance(fileName);
            editor.FileName = fileName;
            editor.LoadFile();
            HostWindow.DisplayDockedForm(editor as DockContent);
        }

        private string GetFilenameByIndex()
        {
            return MostRecentFilesService.Filenames[Index - 1];
        }
    }

    public class OpenRecentFile1Command : OpenRecentFileCommand
    {
        public OpenRecentFile1Command()
            : base(1)
        {
        }
    }

    public class OpenRecentFile2Command : OpenRecentFileCommand
    {
        public OpenRecentFile2Command()
            : base(2)
        {
        }
    }

    public class OpenRecentFile3Command : OpenRecentFileCommand
    {
        public OpenRecentFile3Command()
            : base(3)
        {
        }
    }

    public class OpenRecentFile4Command : OpenRecentFileCommand
    {
        public OpenRecentFile4Command()
            : base(4)
        {
        }
    }

    public class OpenRecentFile5Command : OpenRecentFileCommand
    {
        public OpenRecentFile5Command()
            : base(5)
        {
        }
    }

    public class OpenRecentFile6Command : OpenRecentFileCommand
    {
        public OpenRecentFile6Command()
            : base(6)
        {
        }
    }

    public class OpenRecentFile7Command : OpenRecentFileCommand
    {
        public OpenRecentFile7Command()
            : base(7)
        {
        }
    }

    public class OpenRecentFile8Command : OpenRecentFileCommand
    {
        public OpenRecentFile8Command()
            : base(8)
        {
        }
    }

    public class OpenRecentFile9Command : OpenRecentFileCommand
    {
        public OpenRecentFile9Command()
            : base(9)
        {
        }
    }

    public class OpenRecentFile10Command : OpenRecentFileCommand
    {
        public OpenRecentFile10Command()
            : base(10)
        {
        }
    }
}