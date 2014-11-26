using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

			Assert.That(QueryEditor.AllText, Is.Empty);
			Assert.That(QueryEditor.FileName, Is.Null);
			Assert.That(QueryEditor.Batch, Is.Null);
			Assert.That(QueryEditor.IsDirty, Is.False);
			Assert.That(QueryEditor.SelectedText, Is.Empty);
			Assert.That(QueryEditor.CursorOffset, Is.EqualTo(0));
			Assert.That(QueryEditor.TotalLines, Is.EqualTo(1));
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