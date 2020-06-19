using ScrumageEngine.BoardSpace;
using ScrumageEngine.Exceptions;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ScrumageEngine.Objects.Items;
using ScrumageEngine.Objects.Player;
using static ScrumageEngine.InputLogic.InputHandler;

namespace ScrumageEngine.Windows{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
		private Int32 PlayerCount = 0;
		Game game;
		private Int32 currentPlayerID;
		private List<String> SelectedPawns = new List<String>();

        private String PawnboxForPlacementNode {
            get { return this.NodeComboBox.SelectedItem.ToString(); }
        }

        private String PawnboxForActionNode {
            get { return this.NodeComboBox2.SelectionBoxItem.ToString();  }
        }


		public GameWindow(List<String> playerNames) {
			PlayerCount = playerNames.Count;
			game = new Game(playerNames);
			InitializeComponent();
			InitPlayerTab(playerNames);
			InitComboBox(NodeComboBox, game.GetNodeNames());
			InitComboBox(NodeComboBox2, game.GetNodeNames());
			currentPlayerID = PlayerTabControl.SelectedIndex;
		}
		/// <summary>
		/// Initializes the Node combobox with all nodes on the board(Make sure nodes are added to the Nodes list in Board).
		/// </summary>
		/// <param name="nodes">The list of Nodes on the board.</param>
		void InitComboBox(ComboBox comboBox, List<String> nodes) {
			foreach(String node in nodes) {
				comboBox.Items.Add(node);
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
				//temp.Visibility = Visibility.Visible;
				temp.IsEnabled = true;
				temp.Header = playerNames[i];
				(temp.FindName($"P{i+1}NameValue") as Label).Content = playerNames[i];
				UpdatePawnBox(FindPlayerPawnBox(i+1), game.GetPlayerPawns(i+1));
				// Whatever else needs to be initialized at the start of the game for players goes here.
			}
		}

		/// <summary>
		/// Displays information on the card in the relative location.
		/// </summary>
		/// <param name="sender">The label prompt.</param>
		/// <param name="e">Prompt being clicked.</param>
		private void Label_MouseDoubleClick(Object sender, MouseButtonEventArgs e) {
			MessageBox.Show("Make GUI to display card requirements");
		}

		/// <summary>
		/// Exits the program on "Exit" click.
		/// </summary>
		/// <param name="sender">The Exit button.</param>
		/// <param name="e">The button being pressed.</param>
		private void ExitBtn_Click(Object sender, RoutedEventArgs e) {
			Application.Current.Shutdown();
		}

		private void TestBtn_Click(Object sender, RoutedEventArgs e) {
			// Test for adding a pawn
			/*GivePlayerPawn(game, currentPlayerID); // Gives random pawn
			GivePlayerPawn(game, currentPlayerID, "0"); // 0 = FE, 1 = BE, 2 = FS
			GivePlayerPawn(game, currentPlayerID, "Full Stack"); // Just enter the name of the pawn
			UpdatePawnBox(GetPlayerPawnBoxByID(currentPlayerID), game.GetPlayerPawns(currentPlayerID));*/

			// Test for card(probably a better way of getting the TextBox)
			/*GivePlayerCard("artifact", game.Players[currentPlayerID]);
			GivePlayerCard("agility", game.Players[currentPlayerID]);
			UpdateCardBox((PlayerTabControl.Items[currentPlayerID] as TabItem).FindName($"P{currentPlayerID + 1}ArtifactBox") as TextBox, game.Players[currentPlayerID].FeatureCards[0]);
			UpdateCardBox((PlayerTabControl.Items[currentPlayerID] as TabItem).FindName($"P{currentPlayerID + 1}AgilityBox") as TextBox, game.Players[currentPlayerID].UserStories[0]);*/

			//currentPlayerID++;
			//if(currentPlayerID > PlayerCount - 1) currentPlayerID = 0;

			IncrementPlayer();
			//PlayerTabControl.SelectedIndex = currentPlayerID-1;


			LogInput();
		}

		/// <summary>
		/// Updates the Sprint2 log with the currently contained inputs.
		/// </summary>
		void LogInput() {
			ClearLog();
			foreach(String input in GetRecentInputs())
				SprintLogBox.Items.Add(input);
		}

		/// <summary>
		/// Clears the sprint log.
		/// </summary>
		void ClearLog() {
			SprintLogBox.Items.Clear();
		}

		/// <summary>
		/// Updates the dice display.
		/// </summary>
		/// <param name="dice">The list of dice that were just rolled.</param>
		private void UpdateDieBoxes(List<String> dice) {
			TextBox tempBox = null;
			for(Int32 i = 0; i < 7; i++) {
				if(i < dice.Count) {
					tempBox = FindName($"DieBox{i + 1}") as TextBox;
					tempBox.Visibility = Visibility.Visible;
					tempBox.Text = dice[i];
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
		private void UpdatePawnBox(ListBox pawnBox, List<String> pawns) {
			pawnBox.Items.Clear();
			foreach(String p in pawns) {
				pawnBox.Items.Add(p);
			}
		}

		/// <summary>
		/// Udates a specified card display.
		/// </summary>
		/// <param name="cardBox">The display to update.</param>
		/// <param name="card">The new card to be displayed.</param>
/*		private void UpdateCardBox(TextBox cardBox, Card card) {
			cardBox.Text = card.ToString();
		}*/

		

		// Maybe not keep this?
		private void PlayerTabControl_SelectionChanged(Object sender, SelectionChangedEventArgs e) {
			currentPlayerID = (sender as TabControl).SelectedIndex + 1;
			CurrentPlayerIDLabel.Content = currentPlayerID;
		}


		private void IncrementPlayer() {
			if (++currentPlayerID > PlayerCount) {
				currentPlayerID = 1;
			}
			PlayerTabControl.SelectedIndex = currentPlayerID - 1;
		}

		/// <summary>
		/// Player clicks this button after pawns and a destination node are selected to move the pawns.
		/// </summary>
		/// <param name="sender">"Move" button in Phase 1 panel.</param>
		/// <param name="e">The button being pressed.</param>
		private void MovePawnBtn_Click(Object sender, RoutedEventArgs e) {
			SelectedPawns.Clear();
			Boolean phaseEnd = false;
			foreach(String p in FindPlayerPawnBox(this.currentPlayerID).SelectedItems) {
				SelectedPawns.Add(p);
			}
			try {
				phaseEnd = MovePawn(game, SelectedPawns, currentPlayerID, PawnboxForPlacementNode);

				UpdatePawnBox(FindNodePawnBoxPhase1(), game.GetNodePawns(PawnboxForPlacementNode));
				UpdatePawnBox(FindPlayerPawnBox(currentPlayerID), game.GetPlayerPawns(currentPlayerID));
				IncrementPlayer();
			}
			catch(MovePawnException _exception) {
				MessageBox.Show(_exception.Message);
			}

			if (phaseEnd) MessageBox.Show("Phase 1 has completed!");
			LogInput();
		}


		private void TestDiceBtn_Click(Object sender, RoutedEventArgs e) {
			RollDice(game, Int32.Parse(DiceCountCombo.SelectedItem.ToString()));
			UpdateDieBoxes(game.ShowDice());
			LogInput();
		}

		/// <summary>
		/// Handles the Click event of the NodeActionBtn control. For when the current Player
		/// wants to activate a Node on the Board
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void NodeActionBtn_Click(Object sender, RoutedEventArgs e) {
			ActivateNode(game, currentPlayerID, PawnboxForActionNode);
			UpdatePawnBox(FindNodePawnBoxPhase2(), game.GetNodePawns(PawnboxForActionNode));
			UpdatePlayerInformation(this.currentPlayerID);
			LogInput();
		}


		#region Find Component Helper Methods		
		/// <summary>
		/// Finds a player's resource box.
		/// </summary>
		/// <param name="playerIdP">The player identifier</param>
		/// <returns>the resource box belonging to this player</returns>
		private ListBox FindResourceBox(Int32 playerIdP) {
            return FindName($"P{playerIdP}ResourceBox") as ListBox;
        }

		/// <summary>
		/// Finds the player's pawn box.
		/// </summary>
		/// <param name="playerIdP">The player identifier.</param>
		/// <returns>the pawn box belonging to this player</returns>
		private ListBox FindPlayerPawnBox(Int32 playerIdP) {
            return FindName($"P{playerIdP}PawnBox") as ListBox;
        }

		/// <summary>
		/// Finds the currently selected node pawn-box from the placement phase combo-box.
		/// </summary>
		/// <returns>the pawn-box for the selected node</returns>
		private ListBox FindNodePawnBoxPhase1() {
            return FindName($"{this.NodeComboBox.SelectedItem.ToString().Replace(" ","")}Box") as ListBox;
        }

		/// <summary>
		/// Finds the currently selected node pawn-box from the action phase combo-box
		/// </summary>
		/// <returns>the pawn-box for the selected ndoe</returns>
		private ListBox FindNodePawnBoxPhase2() {
			return FindName($"{this.NodeComboBox2.SelectedItem.ToString().Replace(" ","")}Box") as ListBox;
        }
		#endregion

		#region Update Player Display
        /// <summary>
		/// Updates the player's information display.
		/// </summary>
		/// <param name="playerIdP">The player identifier</param>
		private void UpdatePlayerInformation(Int32 playerIdP) {

            // Update inventory display
            UpdatePlayerResourceDisplay(FindResourceBox(playerIdP), this.game.GetPlayerResources(playerIdP));
            UpdatePawnBox(FindPlayerPawnBox(playerIdP), this.game.GetPlayerPawns(playerIdP));
			//TODO: Update Cards

            // Update stats
            //	update budget
            //	update score
            //	update funds
        }

		/// <summary>
		/// Updates the player resource box display.
		/// </summary>
		/// <param name="resourceBox">The resource box to be updated</param>
		/// <param name="playerResourcesP">The player's resources</param>
		private void UpdatePlayerResourceDisplay(ListBox resourceBox, ResourceContainer playerResourcesP) {
            resourceBox.Items.Clear();
            Resource[] _resources = playerResourcesP.GetResourceTypes();
            foreach(Resource _type in _resources) {
                String _lineItem = $"{_type.Name}   {playerResourcesP[_type]}";
                resourceBox.Items.Add(_lineItem);
            }
        }


		#endregion


	}
}
