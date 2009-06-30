namespace MiniSqlQuery.Core
{
	/// <summary>
	/// A text finding serice interface. A window can implement this interface and
	/// it will allow searching of its text.
	/// </summary>
	public interface ITextFindService
	{
		/// <summary>
		/// Finds the next string of text depending on the contrnts of the <paramref name="request"/>.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns>A find request with position updated.</returns>
		FindTextRequest FindNext(FindTextRequest request);

		//todo - set type?
	}
}