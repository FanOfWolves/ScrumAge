using System;
using System.Collections.Generic;
using System.Windows;
using ScrumageEngine.Views;

namespace ScrumageEngine.Windows {
	/// <summary>
	/// Interaction logic for MainMenu.xaml
	/// </summary>
	public partial class MainMenu : Window {
		/// <summary>
		/// Initializes a list of Strings read from the input boxes that allow players to enter their names.
		/// </summary>
		private List<String> playerList = new List<String>();


		/// <summary>
		/// Initialize the MainMenu GUI when called by App
		/// </summary>
		public MainMenu() {
			InitializeComponent();
		}


		/// <summary>
		/// Start the Game or warm player that game cannot be started due to players not being set.
		/// </summary>
		/// <param name="sender">Start Button</param>
		/// <param name="e">Start button being clicked</param>
		private void btnStartGame_Click(object sender, RoutedEventArgs e) {
			Main.Content = new StartView(playerList);
		}


		/// <summary>
		/// Exit the Application
		/// </summary>
		/// <param name="sender">Exit button</param>
		/// <param name="e">Exit button being clicked</param>
		private void btnExitGame_Click(object sender, RoutedEventArgs e) {
			Environment.Exit(0);
		}


		/// <summary>
		/// Calls and displays the global help-menu item
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void btnHelp_Click(object sender, RoutedEventArgs e) {
			new HelpView().Show();
		}


		/// <summary>
		/// Open the options panel to configure the game
		/// This can also be done when the Start Game button is pressed
		/// </summary>
		/// <param name="sender">Options button</param>
		/// <param name="e">Options button being clicked</param>
		private void btnOptions_Click(object sender, RoutedEventArgs e) {
			Main.Content = new OptionsView(ref playerList);
		}
	}
}
