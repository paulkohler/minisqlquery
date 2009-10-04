#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
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