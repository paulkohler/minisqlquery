using System;
using System.Drawing;

namespace MiniSqlQuery.Core.Commands
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