#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using System;

namespace MiniSqlQuery.Core.Commands
{
	/// <summary>
	/// 	The email author command.
	/// </summary>
	public class EmailAuthorCommand
		: ShowUrlCommand
	{
		/// <summary>
		/// 	Initializes a new instance of the <see cref = "EmailAuthorCommand" /> class.
		/// </summary>
		public EmailAuthorCommand()
			: base("Email the Author", "mailto:paul@viridissoftware.com.au?subject=Mini SQL Query Feedback", ImageResource.email)
		{
		}
	}
}