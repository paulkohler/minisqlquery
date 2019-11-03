#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE

#endregion

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