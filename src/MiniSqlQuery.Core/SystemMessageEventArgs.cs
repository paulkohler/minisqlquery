#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE

#endregion

using System;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// 	A system wide message event.
	/// </summary>
	public class SystemMessageEventArgs : EventArgs
	{
		/// <summary>
		/// 	Initializes a new instance of the <see cref = "SystemMessageEventArgs" /> class.
		/// </summary>
		/// <param name = "message">The system message.</param>
		/// <param name = "data">The associated data.</param>
		public SystemMessageEventArgs(SystemMessage message, object data)
		{
			Message = message;
			Data = data;
		}

		/// <summary>
		/// 	Gets the data for the event.
		/// </summary>
		/// <value>The event data.</value>
		public object Data { get; private set; }

		/// <summary>
		/// 	Gets the system message type.
		/// </summary>
		/// <value>The system message type.</value>
		public SystemMessage Message { get; private set; }
	}
}