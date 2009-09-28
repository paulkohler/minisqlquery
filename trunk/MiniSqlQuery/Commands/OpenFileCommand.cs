using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MiniSqlQuery.Core.Commands;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery.Commands
{
	public class OpenFileCommand
        : CommandBase
    {
		public OpenFileCommand()
            : base("&Open File")
        {
            ShortcutKeys = Keys.Control | Keys.O;
			SmallImage = ImageResource.folder_page;
        }

        public override void Execute()
        {
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
			//openFileDialog.Filter = Settings.DefaultFileFilter;
			openFileDialog.Filter = "SQL Files (*.sql)|*.sql|" +
				"Mini SQL Template Files (*.mt)|*.mt|" +
				"ASPX Files (*.asp;*.aspx;*.asax;*.asmx)|*.asp;*.aspx;*.asax;*.asmx|" +
				"Batch Files (*.bat;*.cmd)|*.bat;*.cmd|" +
				"BOO Files (*.boo)|*.boo|" +
				"Coco Files (*.atg)|*.atg|" +
				"C++ Files (*.cpp;*.cc;*.c;*.h)|*.cpp;*.cc;*.c;*.h|" +
				"C# Files (*.cs)|*.cs|" +
				"HTML Files (*.htm*)|*.htm*|" +
				"Java Files (*.java)|*.java|" +
				"JavaScript Files (*.js)|*.js|" +
				"Patch Files (*.patch;*.diff)|*.patch;*.diff|" +
				"PHP Files (*.php*)|*.php*|" +
				"TeX Files (*.tex)|*.tex|" +
				"VB.NET Files (*.vb)|*.vb|" +
				"XML Files (*.xml;*.resx)|*.xml;*.resx|" + 
				"All Files (*.*)|*.*"; ;
			openFileDialog.CheckFileExists = true;
			if (openFileDialog.ShowDialog(HostWindow.Instance) == DialogResult.OK)
			{
				//todo: check for file exist file in open windows;

				IFileEditorResolver resolver = Services.Resolve<IFileEditorResolver>();
				IEditor editor = resolver.ResolveEditorInstance(openFileDialog.FileName);
				editor.FileName = openFileDialog.FileName;
				editor.LoadFile();
				HostWindow.DisplayDockedForm(editor as DockContent);
			}

        }
		/*
"SQL Files (*.sql)|*.sql|All Files (*.*)|*.*|" + 
"Mini SQL Template Files (*.mt)|*.mt|All Files (*.*)|*.*|" + 
"ASPX Files (*.asp;*.aspx;*.asax;*.asmx)|*.asp;*.aspx;*.asax;*.asmx|All Files (*.*)|*.*|" + 
"Batch Files (*.bat;*.cmd)|*.bat;*.cmd|All Files (*.*)|*.*|" + 
"BOO Files (*.boo)|*.boo|All Files (*.*)|*.*|" + 
"Coco Files (*.atg)|*.atg|All Files (*.*)|*.*|" + 
"C++ Files (*.cpp;*.cc;*.c;*.h)|*.cpp;*.cc;*.c;*.h|All Files (*.*)|*.*|" + 
"C# Files (*.cs)|*.cs|All Files (*.*)|*.*|" + 
"HTML Files (*.htm*)|*.htm*|All Files (*.*)|*.*|" + 
"Java Files (*.java)|*.java|All Files (*.*)|*.*|" + 
"JavaScript Files (*.js)|*.js|All Files (*.*)|*.*|" + 
"Patch Files (*.patch;*.diff)|*.patch;*.diff|All Files (*.*)|*.*|" + 
"PHP Files (*.php*)|*.php*|All Files (*.*)|*.*|" + 
"TeX Files (*.tex)|*.tex|All Files (*.*)|*.*|" + 
"VB.NET Files (*.vb)|*.vb|All Files (*.*)|*.*|" + 
"XML Files (*.xml;*.resx)|*.xml;*.resx|All Files (*.*)|*.*";
*/

	}
}
