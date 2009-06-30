using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using Moq;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace MiniSqlQuery.Tests.Gui
{
	[TestFixture]
	public class Basic_GUI_tests : QueryFormTester
	{

		[Test]
		public void Check_all_new_query_from_defaults()
		{
			ShowBasicForm();

			Assert.That(queryEditor.AllText, Is.Empty);
			Assert.That(queryEditor.FileName, Is.Null);
			Assert.That(queryEditor.Batch, Is.Null);
			Assert.That(queryEditor.IsDirty, Is.False);
			Assert.That(queryEditor.SelectedText, Is.Empty);
			Assert.That(queryEditor.CursorOffset, Is.EqualTo(0));
			Assert.That(queryEditor.TotalLines, Is.EqualTo(1));
		}

	}

	//static class App
	//{
	//    /// <summary>
	//    /// The main entry point for the application.
	//    /// </summary>
	//    [STAThread]
	//    static void Main(string[] args)
	//    {
	//        Basic_GUI_tests t = new Basic_GUI_tests();
	//        t.Setup();
	//        t.t1();
	//    }
	//}
}
