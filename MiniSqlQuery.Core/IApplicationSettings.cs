using System;
using System.Data.Common;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// An interface for the application settings.
	/// </summary>
	public interface IApplicationSettings
	{
		/// <summary>
		/// Fired when the list of connection definitions is modified.
		/// </summary>
		/// <seealso cref="SetConnectionDefinitions"/>
		/// <seealso cref="GetConnectionDefinitions"/>
		event EventHandler ConnectionDefinitionsChanged;

		/// <summary>
		/// Fired when the database connection (provider and/or connection string) are modified.
		/// </summary>
		/// <seealso cref="ResetConnection"/>
		event EventHandler DatabaseConnectionReset;

		/// <summary>
		/// A reference to the current connection definiton class.
		/// </summary>
		ConnectionDefinition ConnectionDefinition { get; set; }

		/// <summary>
        /// Gets an instance of <see cref="DbProviderFactory"/> depending on the value of <see cref="ConnectionDefinition"/>.
		/// </summary>
		DbProviderFactory ProviderFactory { get; }

		/// <summary>
		/// Gets an instance of <see cref="DbConnection"/> depending on the value of <see cref="ConnectionDefinition"/>.
		/// </summary>
		DbConnection Connection { get; }

		/// <summary>
		/// Gets an array of the current connection definitions for this user.
		/// </summary>
		/// <returns>An array of connection definitions.</returns>
		ConnectionDefinition[] GetConnectionDefinitions();

		/// <summary>
        /// Resets the list of connection definitions that are stored in the user profile.
		/// </summary>
        /// <param name="connections">The array of connection definitions.</param>
		void SetConnectionDefinitions(ConnectionDefinition[] connections);

		/// <summary>
		/// Resets the connection details firing the <see cref="DatabaseConnectionReset"/> event.
		/// </summary>
		/// <seealso cref="DatabaseConnectionReset"/>
		void ResetConnection();

		/// <summary>
		/// Helper method to get an open connection.
		/// </summary>
		/// <returns>A <see cref="DbConnection"/> object.</returns>
		DbConnection GetOpenConnection();

		/// <summary>
		/// The default filter string for dialog boxes.
		/// </summary>
		string DefaultFileFilter { get; }
	}
}
