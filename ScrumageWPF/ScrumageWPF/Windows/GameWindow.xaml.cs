using ScrumageEngine.BoardSpace;
using ScrumageEngine.Objects.Items;
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

namespace ScrumageEngine.Windows{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
		private int PlayerCount = 0;
		Game game;
		private int currentPlayerID;
		private List<Pawn> SelectedPawns = new List<Pawn>();
		public GameWindow(List<String> playerNames) {
			PlayerCount = playerNames.Count;
			game = new Game(playerNames);
			InitializeComponent();
			InitPlayerTab(playerNames);
			InitLocationComboBox(game.board.GetAllNodes());
			currentPlayerID = PlayerTabControl.SelectedIndex;
		}
		/// <summary>
		/// Initializes the Node combobox with all nodes on the board(Make sure nodes are added to the Nodes list in Board).
		/// </summary>
		/// <param name="nodes">The list of Nodes on the board.</param>
		void InitLocationComboBox(List<Node> nodes) {
			foreach(Node node in nodes) {
				NodeComboBox.Items.Add(node.NodeName);
			}
		}

		/// <summary>
		/// Initializes the Player tabs on start of the game.
		/// </summary>
		/// <param name="playerNames">The names of the players collected in the first GUI.</param>
		void InitPlayerTab(List<String> playerNames) {
			TabItem temp = null;
			for(Int32 i = 0; i < playerNames.Count; i++) {
				temp = PlayerTabControl.Items[i] as TabItem;
				temp.Visibility = Visibility.Visible;
				temp.IsEnabled = true;
				temp.Header = playerNames[i];
				(temp.FindName($"P{i + 1}NameValue") as Label).Content = playerNames[i];
				UpdatePawnBox(GetPlayerPawnBoxByID(i), game.Players[i].Pawns);
				// Whatever else needs to be initialized at the start of the game for players goes here.
			}
		}

		/// <summary>
		/// Displays information on the card in the relative location.
		/// </summary>
		/// <param name="sender">The label prompt.</param>
		/// <param name="e">Prompt being clicked.</param>
		private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
			MessageBox.Show("Make GUI to display card requirements");
		}

		/// <summary>
		/// Exits the program on "Exit" click.
		/// </summary>
		/// <param name="sender">The Exit button.</param>
		/// <param name="e">The button being pressed.</param>
		private void ExitBtn_Click(object sender, RoutedEventArgs e) {
			Application.Current.Shutdown();
		}

		private void TestBtn_Click(object sender, RoutedEventArgs e) {
			// Test for adding a pawn
			GivePlayerPawn(game.Players[currentPlayerID], game); // Gives random pawn
			GivePlayerPawn(game.Players[currentPlayerID], game, "0"); // 0 = FE, 1 = BE, 2 = FS
			GivePlayerPawn(game.Players[currentPlayerID], game, "Full Stack"); // Just enter the name of the pawn
			UpdatePawnBox(GetPlayerPawnBoxByID(currentPlayerID), game.Players[currentPlayerID].Pawns);

			// Test for card(probably a better way of getting the TextBox)
			/*GivePlayerCard("artifact", game.Players[currentPlayerID]);
			GivePlayerCard("agility", game.Players[currentPlayerID]);
			UpdateCardBox((PlayerTabControl.Items[currentPlayerID] as TabItem).FindName($"P{currentPlayerID + 1}ArtifactBox") as TextBox, game.Players[currentPlayerID].FeatureCards[0]);
			UpdateCardBox((PlayerTabControl.Items[currentPlayerID] as TabItem).FindName($"P{currentPlayerID + 1}AgilityBox") as TextBox, game.Players[currentPlayerID].UserStories[0]);*/

			//currentPlayerID++;
			//if(currentPlayerID > PlayerCount - 1) currentPlayerID = 0;
			LogInput();
		}

		/// <summary>
		/// Updates the SprInt32 log with the currently contained inputs.
		/// </summary>
		void LogInput() {
			ClearLog();
			foreach(String input in GetRecentInputs())
				SprintLogBox.Items.Add(input);
		}

		/// <summary>
		/// Clears the sprInt32 log.
		/// </summary>
		void ClearLog() {
			SprintLogBox.Items.Clear();
		}

		/// <summary>
		/// Updates the dice display.
		/// </summary>
		/// <param name="dice">The list of dice that were just rolled.</param>
		private void UpdateDieBoxes(List<Die> dice) {
			TextBox tempBox = null;
			for(Int32 i = 0; i < 7; i++) {
				if(i < dice.Count) {
					tempBox = FindName($"DieBox{i + 1}") as TextBox;
					tempBox.Visibility = Visibility.Visible;
					tempBox.Text = dice[i].DrawDie();
				} else {
					tempBox = FindName($"DieBox{i + 1}") as TextBox;
					tempBox.Visibility = Visibility.Hidden;
				}
			}
		}

		/// <summary>
		/// Updates a pawn ListBox.
		/// </summary>
		/// <param name="pawnBox">The ListBox to be updated.</param>
		/// <param name="pawns">The pawns inside the location the ListBox represents.</param>
		private void UpdatePawnBox(ListBox pawnBox, List<Pawn> pawns) {
			pawnBox.Items.Clear();
			foreach(Pawn p in pawns) {
				pawnBox.Items.Add(p);
			}
		}

		/// <summary>
		/// Udates a specified card display.
		/// </summary>
		/// <param name="cardBox">The display to update.</param>
		/// <param name="card">The new card to be displayed.</param>
		private void UpdateCardBox(TextBox cardBox, Card card) {
			cardBox.Text = card.ToString();
		}

		/// <summary>
		/// Returns the player's pawn List Box by the Player's ID
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		private ListBox GetPlayerPawnBoxByID(Int32 id) {
			switch(id) {
				case 0: return P1PawnBox;
				case 1: return P2PawnBox;
				case 2: return P3PawnBox;
				case 3: return P4PawnBox;
				default: return P1PawnBox;
			}
		}

		// Maybe not keep this?
		private void PlayerTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			currentPlayerID = (sender as TabControl).SelectedIndex;
		}

		/// <summary>
		/// Player clicks this button after pawns and a destination node are selected to move the pawns.
		/// </summary>
		/// <param name="sender">"Move" button in Phase 1 panel.</param>
		/// <param name="e">The button being pressed.</param>
		private void MovePawnBtn_Click(object sender, RoutedEventArgs e) {
			SelectedPawns.Clear();
			foreach(Pawn p in GetPlayerPawnBoxByID(currentPlayerID).SelectedItems) {
				SelectedPawns.Add(p);
			}
			MovePawn(SelectedPawns, game.Players[currentPlayerID], game.board.GetNodeByName(NodeComboBox.SelectedItem.ToString()));
			UpdatePawnBox(FindName($"{NodeComboBox.SelectedItem.ToString().Replace(" ", "")}Box") as ListBox, game.board.GetNodeByName(NodeComboBox.SelectedItem.ToString()).Pawns);
			UpdatePawnBox(GetPlayerPawnBoxByID(currentPlayerID), game.Players[currentPlayerID].Pawns);
			LogInput();
		}


		private void TestDiceBtn_Click(object sender, RoutedEventArgs e) {
			RollDice(Int32.Parse(DiceCountCombo.SelectedItem.ToString()), game);
			UpdateDieBoxes(game.board.dice);
			LogInput();
		}
	}
}
