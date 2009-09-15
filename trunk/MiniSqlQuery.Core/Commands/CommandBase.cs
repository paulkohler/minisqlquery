using System;
using System.Drawing;
using System.Windows.Forms;

namespace MiniSqlQuery.Core.Commands
{
	/// <summary>
	/// A basic implementation of the <see cref="ICommand"/> interface.
	/// Represents a "command", typically a user action such as saving a file or executing a query.
	/// Inheritors must implement the abstract method <see cref="Execute"/>.
	/// </summary>
	public abstract class CommandBase : ICommand
	{
		private string _name;
		private Image _smallImage;
		IHostWindow _hostWindow;

		/// <summary>
		/// Initializes a new instance of the <see cref="CommandBase"/> class.
		/// The default value for <see cref="Enabled"/> is true, and <see cref="ShortcutKeys"/> is Keys.None.
		/// </summary>
		/// <param name="name">The name.</param>
		public CommandBase(string name)
		{
			_name = name;
			ShortcutKeys = Keys.None;
		}

		/// <summary>
		/// Attempts to convert the current host windows active form to <see cref="IQueryEditor"/>.
		/// </summary>
		/// <value>A reference to the active query editor window, otherwise null.</value>
		protected IQueryEditor ActiveFormAsEditor
		{
			get { return Services.HostWindow.ActiveChildForm as IQueryEditor; }
		}

		/// <summary>
		/// Gets a reference to the host window.
		/// </summary>
		/// <value>The host window.</value>
		protected IHostWindow HostWindow
		{
			get
			{
				// just resolve once
				if (_hostWindow == null)
				{
					_hostWindow = Services.HostWindow;
				}
				return _hostWindow;
			}
		}

		#region ICommand Members

		/// <summary>
		/// Gets a value indicating whether this <see cref="ICommand"/> is enabled.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c> (the default is true).</value>
		public virtual bool Enabled
		{
			get { return true; }
		}

		/// <summary>
		/// Gets the "small image" associated with this control (for use on buttons or menu items).
		/// Use null (or Nothing in Visual Basic) if there is no image.
		/// </summary>
		/// <value>The small image representing this command (the default is null).</value>
		public virtual Image SmallImage
		{
			get { return _smallImage; }
			protected set { _smallImage = value; }
		}

		/// <summary>
		/// Gets the menu shortcut keys for this command (e.g. Keys.F5).
		/// </summary>
		/// <value>The shortcut keys for this command (the default is Keys.None).</value>
		public virtual Keys ShortcutKeys { get; protected set; }

		/// <summary>
		/// Gets the shortcut key text to be displayed as help.
		/// </summary>
		/// <value>The shortcut keys text.</value>
		public string ShortcutKeysText { get; protected set; }

		/// <summary>
		/// The name of the command, used in menus and buttons.
		/// </summary>
		/// <value>The name of the command.</value>
		public virtual string Name
		{
			get { return _name; }
			protected set { _name = value; }
		}

		/// <summary>
		/// A reference to the application services to allow access to the other components.
		/// </summary>
		/// <value>A reference to the <see cref="IApplicationServices"/> instance.</value>
		public IApplicationServices Services { get; set; }

		/// <summary>
		/// Gets a reference to the application settings.
		/// </summary>
		/// <value>The application settings.</value>
		public IApplicationSettings Settings { get; set; }

		/// <summary>
		/// Executes the command based on the current settings (abstract).
		/// </summary>
		/// <remarks>
		/// If a commands <see cref="Enabled"/> value is false, a call to <see cref="Execute"/> should have no effect
		/// (and not throw an exception).
		/// </remarks>
		public abstract void Execute();

		#endregion
	}
}