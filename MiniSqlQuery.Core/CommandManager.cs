using System;
using System.Collections.Generic;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// Stores instances of commands by type.
	/// </summary>
	public class CommandManager
	{
		private static readonly Dictionary<Type, ICommand> _commandCache = new Dictionary<Type, ICommand>();

		/// <summary>
		/// Gets the command instance by <paramref name="commandTypeName"/>.
		/// </summary>
		/// <param name="commandTypeName">Name of the command, e.g. "OpenFileCommand".</param>
		/// <returns>The first command by that name or null if not found.</returns>
		public static ICommand GetCommandInstance(string commandTypeName)
		{
			foreach (Type cmdType in _commandCache.Keys)
			{
				if (cmdType.Name == commandTypeName)
				{
					return _commandCache[cmdType];
				}
			}
			return null;
		}

		/// <summary>
		/// Gets command instance by it's partial name, e.g. "OpenFile".
		/// </summary>
		/// <param name="commandName">Name partial of the command.</param>
		/// <returns>The first command by that name or null if not found.</returns>
		public static ICommand GetCommandInstanceByPartialName(string commandName)
		{
			string cmdName = commandName + "Command";

			foreach (Type cmdType in _commandCache.Keys)
			{
				if (cmdType.Name.EndsWith(commandName) || cmdType.Name.EndsWith(cmdName))
				{
					return _commandCache[cmdType];
				}
			}
			return null;
		}

		/// <summary>
		/// Gets or creates an instance of a command by type.
		/// </summary>
		/// <typeparam name="TCommand">The type of command to get or create.</typeparam>
		/// <returns>An instance of <typeparamref name="TCommand"/>.</returns>
		public static ICommand GetCommandInstance<TCommand>() where TCommand : ICommand, new()
		{
			ICommand cmd;

			if (_commandCache.ContainsKey(typeof (TCommand)))
			{
				cmd = _commandCache[typeof (TCommand)];
			}
			else
			{
				cmd = new TCommand();
				cmd.Services = ApplicationServices.Instance;
				_commandCache[typeof (TCommand)] = cmd;
			}

			return cmd;
		}
	}
}