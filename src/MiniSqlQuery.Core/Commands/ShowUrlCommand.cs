#region License

// Copyright 2005-2009 Paul Kohler (https://github.com/paul-kohler-au/minisqlquery). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.Drawing;

namespace MiniSqlQuery.Core.Commands
{
	/// <summary>
	/// 	The show url command.
	/// </summary>
	public class ShowUrlCommand
		: CommandBase
	{
		/// <summary>
		/// 	Initializes a new instance of the <see cref="ShowUrlCommand"/> class.
		/// </summary>
		/// <param name="name">
		/// The name of the command.
		/// </param>
		/// <param name="url">
		/// The url of the link to display in a browser.
		/// </param>
		/// <param name="image">
		/// The image to use from the resources.
		/// </param>
		public ShowUrlCommand(string name, string url, Image image)
			: base(name)
		{
			Url = url;
			SmallImage = image;
		}

		/// <summary>
		/// 	Gets or sets Url.
		/// </summary>
		/// <value>The url.</value>
		public string Url { get; protected set; }

		/// <summary>
		/// 	Execute the command.
		/// </summary>
		public override void Execute()
		{
			Utility.ShowUrl(Url);
		}
	}
}