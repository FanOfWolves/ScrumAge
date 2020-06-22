﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
		/// Call <Global>Help Menu</Global>
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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
