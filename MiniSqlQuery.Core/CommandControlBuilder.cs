using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// Helper class for building controls out of <see cref="ICommand"/> objects.
	/// </summary>
	public class CommandControlBuilder
	{
		public static ToolStripButton CreateToolStripButton<TCommand>() where TCommand : ICommand, new()
		{
			ToolStripButton button = new ToolStripButton();
			ICommand cmd = CommandManager.GetCommandInstance<TCommand>();

			button.DisplayStyle = ToolStripItemDisplayStyle.Image;
			button.Image = cmd.SmallImage;
			button.ImageTransparentColor = Color.Magenta;
			button.Name = cmd.GetType().Name + "ToolStripButton";
			button.Tag = cmd;
			button.Text = cmd.Name;
			button.Click += new EventHandler(CommandItemClick);

			return button;
		}

		public static ToolStripMenuItem CreateToolStripMenuItem<TCommand>() where TCommand : ICommand, new()
		{
			ToolStripMenuItem menuItem = new ToolStripMenuItem();
			ICommand cmd = CommandManager.GetCommandInstance<TCommand>();

			menuItem.Name = cmd.GetType().Name + "ToolStripMenuItem";
			menuItem.Text = cmd.Name;
			menuItem.Tag = cmd;
			menuItem.ShortcutKeys = cmd.ShortcutKeys;
			menuItem.Image = cmd.SmallImage;
			menuItem.Click += new EventHandler(CommandItemClick);

			return menuItem;
		}

		public static LinkLabel CreateLinkLabel<TCommand>() where TCommand : ICommand, new()
		{
			LinkLabel linkLabel = new LinkLabel();
			ICommand cmd = CommandManager.GetCommandInstance<TCommand>();

			linkLabel.AutoSize = true;
			linkLabel.Name = cmd.GetType().Name + "LinkLabel";
			linkLabel.TabStop = true;
			linkLabel.Text = cmd.Name.Replace("&", string.Empty);
			linkLabel.Tag = cmd;
			linkLabel.Padding = new Padding(4);
			linkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(LinkLabelLinkClicked);

			return linkLabel;
		}

		static void LinkLabelLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Control linkLabel = sender as Control;

			if (linkLabel != null)
			{
				ICommand cmd = linkLabel.Tag as ICommand;

				if (cmd != null)
				{
					cmd.Execute();
				}
			}
		}

		public static ToolStripSeparator CreateToolStripMenuItemSeperator()
		{
			return new ToolStripSeparator();
		}

		public static void CommandItemClick(object sender, EventArgs e)
		{
			ToolStripItem item = sender as ToolStripItem;

			if (item != null)
			{
				ICommand cmd = item.Tag as ICommand;

				if (cmd != null)
				{
					cmd.Execute();
				}
			}
		}

		public static void TopLevelMenuDropDownOpening(object sender, EventArgs e)
		{
			ToolStripMenuItem menuItem = sender as ToolStripMenuItem;

			if (menuItem != null)
			{
				foreach (ToolStripItem item in menuItem.DropDownItems)
				{
					ICommand cmd = item.Tag as ICommand;

					if (cmd != null)
					{
						item.Enabled = cmd.Enabled;
					}
				}
			}
		}

		public static void MonitorMenuItemsOpeningForEnabling(ToolStrip menuStrip)
		{
			if (menuStrip is ContextMenuStrip || menuStrip is MenuStrip)
			{
				foreach (ToolStripItem item in menuStrip.Items)
				{
					ToolStripMenuItem topLevelMenu = item as ToolStripMenuItem;
					if (topLevelMenu != null)
					{
						topLevelMenu.DropDownOpening += new EventHandler(CommandControlBuilder.TopLevelMenuDropDownOpening);
					}
				}
			}
		}
	}
}
