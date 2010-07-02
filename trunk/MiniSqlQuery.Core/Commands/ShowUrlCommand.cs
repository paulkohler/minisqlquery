#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.Drawing;

namespace MiniSqlQuery.Core.Commands
{
	/// <summary>The show url command.</summary>
	public class ShowUrlCommand
		: CommandBase
	{
		/// <summary>Initializes a new instance of the <see cref="ShowUrlCommand"/> class.</summary>
		/// <param name="name">The name.</param>
		/// <param name="url">The url.</param>
		/// <param name="image">The image.</param>
		public ShowUrlCommand(string name, string url, Image image)
			: base(name)
		{
			Url = url;
			SmallImage = image;
		}

		/// <summary>Gets or sets Url.</summary>
		/// <value>The url.</value>
		public string Url { get; protected set; }

		/// <summary>The execute.</summary>
		public override void Execute()
		{
			Utility.ShowUrl(Url);
		}
	}
}