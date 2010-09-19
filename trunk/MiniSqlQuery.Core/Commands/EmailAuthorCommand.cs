#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
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
			: base("Email the Author", "mailto:paul@pksoftware.net?subject=Mini SQL Query Feedback", ImageResource.email)
		{
		}
	}
}