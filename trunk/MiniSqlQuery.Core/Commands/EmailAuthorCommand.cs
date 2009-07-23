using System;

namespace MiniSqlQuery.Core.Commands
{
	public class EmailAuthorCommand
		: ShowUrlCommand
	{
		public EmailAuthorCommand()
			: base("Email the Author", "mailto:paul@pksoftware.net?subject=Mini SQL Query Feedback", ImageResource.email)
		{
		}
	}
}