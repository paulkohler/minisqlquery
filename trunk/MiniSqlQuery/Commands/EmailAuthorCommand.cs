using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;

namespace MiniSqlQuery.Commands
{
	public class EmailAuthorCommand
		: CommandBase
	{
		public EmailAuthorCommand()
			: base("Email the Author")
		{
			SmallImage = ImageResource.email;
		}

		public override void Execute()
		{
			Utility.ShowUrl("mailto:paul@pksoftware.net?subject=Mini SQL Query Feedback");
		}

	}
}