using System;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// A system wide message event.
	/// </summary>
	public class SystemMessageEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SystemMessageEventArgs"/> class.
		/// </summary>
		/// <param name="message">The system message.</param>
		/// <param name="data">The associated data.</param>
		public SystemMessageEventArgs(SystemMessage message, object data)
		{
			Message = message;
			Data = data;
		}

		/// <summary>
		/// Gets or sets the system message type.
		/// </summary>
		/// <value>The system message type.</value>
		public SystemMessage Message { get; private set; }

		/// <summary>
		/// Gets or sets the data.
		/// </summary>
		/// <value>The data.</value>
		public object Data { get; private set; }
	}
}