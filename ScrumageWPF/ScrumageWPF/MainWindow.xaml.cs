using ScrumageEngine.MapSpace;
using System;
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
using static ScrumageEngine.InputLogic.InputHandler;
using System.Collections.ObjectModel;
using ScrumageEngine.Objects.Items;

namespace ScrumageWPF {

	/*
	 * No Data Outside of GUI gets manipulated in GUI. InputHandlers will manipulate all data in backend! Think of the GUI as the client and the Game class
	 * that needs to be created as the "server".
	 */


	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		Board board = new Board(); // This is likely to be a Game class instead of board, in which the Game class will have a board(?) Discuss in design.
		public static ObservableCollection<String> testBind { get; set; } = new ObservableCollection<String>();
		public MainWindow() {
			InitializeComponent();
		}

		// Example function to show how the GUI deals with InputHandler. No data is being manipulated directly by the GUI.
		private void player1GivePawnBtn_Click(object sender, RoutedEventArgs e) {
			// Test Comment for thing
			HandleInput("add pawn", board.p1, board);
			UpdatePlayer1Pawns();
		}
		// Example function to show how to update the GUI after data has been changed in the "server".
		void UpdatePlayer1Pawns() {
			player1PawnBox.Items.Clear();
			foreach(object p in board.p1.Pawns) {
				player1PawnBox.Items.Add(p);
			}
		}
	}
}
