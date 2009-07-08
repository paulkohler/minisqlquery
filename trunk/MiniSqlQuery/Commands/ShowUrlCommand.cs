using System;
using System.Drawing;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;

namespace MiniSqlQuery.Commands
{
	public class ShowUrlCommand
		: CommandBase
	{
		public ShowUrlCommand(string name, string url, Image image)
			: base(name)
		{
			Url = url;
			SmallImage = image;
		}

		public string Url { get; protected set; }

		public override void Execute()
		{
			Utility.ShowUrl(Url);
		}
	}
}