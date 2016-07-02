#region License

// Copyright 2005-2009 Paul Kohler (https://github.com/paul-kohler-au/minisqlquery). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license

#endregion

using System;
using MiniSqlQuery.Core.Commands;

namespace MiniSqlQuery.PlugIns.TextGenerator.Commands
{
	public class RunTextGeneratorCommand : CommandBase
	{
		public RunTextGeneratorCommand()
			: base("Run the (experimental) text to C# class generator")
		{
		}

		public override void Execute()
		{
			var editor = ActiveFormAsEditor;

			if (editor != null)
			{
				var text = editor.SelectedText;
				if (string.IsNullOrEmpty(text))
				{
					text = editor.AllText;
				}

				var textGeneratorService = new TextGeneratorService();
				var generatedText = textGeneratorService.Process(text);

				// update editor, just put in the code for now...
				editor.InsertText(generatedText);
			}
		}
	}
}