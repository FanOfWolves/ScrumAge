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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ScrumageEngine.Windows;

namespace ScrumageEngine.Views {
	/// <summary>
	/// Interaction logic for StartView.xaml
	/// </summary>
	public partial class StartView : Page {

		/// <summary>
		/// A list to hold the names of the players once entered.
		/// </summary>
		private List<String> list;


		/// <summary>
		/// Initializes the view.
		/// </summary>
		public StartView() {
			InitializeComponent();
		}


		/// <summary>
		/// Initializes the start view with a list of names
		/// </summary>
		/// <param name="list">The list of names already passed</param>
		public StartView(List<String> list) {
			InitializeComponent();

			this.list = list;
			lblTotalPlayers.Content = $"Total Players: {list.Count}";
		}


		/// <summary>
		/// Determines if enough players have been created.
		/// </summary>
		/// <returns>
		///		<c>true</c> if enough; Otherwise <c>false</c>.
		/// </returns>
		private bool CanStartGame() {
			// not enough players
			if(list.Count < 2)
				return false;

			return true;
		}

		/// <summary>
		/// Starts the Game (and validates)
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void btnStart_Click(object sender, RoutedEventArgs e) {
			if(CanStartGame()) {
				GameWindow mw = new GameWindow(list);
				mw.Show();

				try {
					Window.GetWindow(this).Close();
				} catch(Exception exception) {
					Console.WriteLine(exception);
					throw;
				}

			} else {
				MessageBox.Show("Failed to start game.", "ScrumAge");
			}
		}
	}
}
