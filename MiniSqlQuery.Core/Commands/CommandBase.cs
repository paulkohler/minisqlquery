using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.Design;
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
		private Keys _shortcutKeys;
		private IApplicationServices _services;

		/// <summary>
		/// Initializes a new instance of the <see cref="CommandBase"/> class.
		/// The default value for <see cref="Enabled"/> is true, and <see cref="ShortcutKeys"/> is Keys.None.
		/// </summary>
		/// <param name="name">The name.</param>
		public CommandBase(string name)
		{
			_name = name;
			_shortcutKeys = Keys.None;
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="ICommand"/> is enabled.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c> (the default is true).</value>
		public virtual bool Enabled
		{
			get
			{
				return true;
			}
		}

		/// <summary>
		/// Gets the "small image" associated with this control (for use on buttons or menu items).
		/// Use null (or Nothing in Visual Basic) if there is no image.
		/// </summary>
		/// <value>The small image representing this command (the default is null).</value>
		public virtual Image SmallImage
		{
			get
			{
				return _smallImage;
			}
			protected set
			{
				_smallImage = value;
			}
		}

		/// <summary>
		/// Gets the menu shortcut keys for this command (e.g. Keys.F5).
		/// </summary>
		/// <value>The shortcut keys for this command (the default is Keys.None).</value>
		public virtual Keys ShortcutKeys
		{
			get
			{
				return _shortcutKeys;
			}
			protected set
			{
				_shortcutKeys = value;
			}
		}

		/// <summary>
		/// The name of the command, used in menus and buttons.
		/// </summary>
		/// <value>The name of the command.</value>
		public virtual string Name
		{
			get
			{
				return _name;
			}
			protected set
			{
				_name = value;
			}
		}

		/// <summary>
		/// A reference to the application services to allow access to the other components.
		/// </summary>
		/// <value>A reference to the <see cref="IApplicationServices"/> instance.</value>
		public IApplicationServices Services
		{
			get
			{
				return _services;
			}
			set
			{
				_services = value;
			}
		}

		/// <summary>
		/// Attempts to convert the current host windows active form to <see cref="IQueryEditor"/>.
		/// </summary>
		/// <value>A reference to the active query editor window, otherwise null.</value>
		protected IQueryEditor ActiveFormAsEditor
		{
			get
			{
				return Services.HostWindow.ActiveChildForm as IQueryEditor;
			}
		}

		/// <summary>
		/// Executes the command based on the current settings (abstract).
		/// </summary>
		/// <remarks>
		/// If a commands <see cref="Enabled"/> value is false, a call to <see cref="Execute"/> should have no effect
		/// (and not throw an exception).
		/// </remarks>
		public abstract void Execute();

	}
}
