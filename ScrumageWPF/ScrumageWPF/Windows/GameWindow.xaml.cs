using ScrumageEngine.BoardSpace;
using ScrumageEngine.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ScrumageEngine.Objects.Items;
using ScrumageEngine.Objects.Items.Cards;
using ScrumageEngine.Objects.Player;
using ScrumageEngine.Views;
using static ScrumageEngine.InputLogic.InputHandler;

namespace ScrumageEngine.Windows {
	/// <summary>
	/// Interaction logic for GameWindow.xaml
	/// </summary>
	public partial class GameWindow : Window {
		#region Fields
		/// <summary>
		/// Link to the main Game class where all logic is done and information is altered/stored. Allows GUI to update view based on data in this class.
		/// </summary>
		Game game;

		/// <summary>
		/// The ID of the current player controlling the board.
		/// </summary>
		private Int32 currentPlayerID;

		/// <summary>
		/// A list of the pawns that are currently selected in the player's pawn box.
		/// </summary>
		private List<String> SelectedPawns = new List<String>();

		/// <summary>
		/// returns the combobox choice in the Placement Phase panel
		/// </summary>
		private String PawnboxForPlacementNode {
			get { return NodeComboBox.SelectedItem.ToString(); }
		}

		/// <summary>
		/// returns the combobox choice in the Action Phase panel
		/// </summary>
		private String PawnboxForActionNode {
			get { return this.NodeComboBox2.SelectionBoxItem.ToString(); }
		}

		/// <summary>
		/// Tracks the current phase from the Game class and updates GUI when needed
		/// </summary>
		private int currentPhaseIndex;
		#endregion

		#region Constructor
		/// <summary>
		/// Initializes the game as well as the game GUI.
		/// </summary>
		/// <param name="playerNames">The list of entered names from the main menu.</param>
		public GameWindow(List<String> playerNames) {
			game = new Game(playerNames);
			InitializeComponent();
			InitPlayerTab(playerNames);
			InitComboBox(NodeComboBox, game.GetNodeNames());
			InitComboBox(NodeComboBox2, game.GetNodeNames());
			UpdateResourceLabels();
			currentPlayerID = game.currentPlayerIndex+1;
			currentPhaseIndex = game.phase - 1;
		}
		#endregion

		#region Initializers
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
				temp.IsEnabled = true;
				temp.Header = playerNames[i];
				(temp.FindName($"P{i + 1}NameValue") as Label).Content = playerNames[i];
				UpdatePawnBox(FindPlayerPawnBox(i + 1), game.GetPlayerPawns(i + 1));
				// Whatever else needs to be initialized at the start of the game for players goes here.
			}
		}
		#endregion

		#region Log Functions
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
		#endregion

		#region Event Handlers
		/// <summary>
		/// Player clicks this button after pawns and a destination node are selected to move the pawns.
		/// </summary>
		/// <param name="sender">"Move" button in Phase 1 panel.</param>
		/// <param name="e">The button being pressed.</param>
		private void MovePawnBtn_Click(Object sender, RoutedEventArgs e) {
			SelectedPawns.Clear();
			Boolean phaseEnd = false;
			foreach(String p in FindPlayerPawnBox(currentPlayerID).SelectedItems) {
				SelectedPawns.Add(p);
			}
			try {
				phaseEnd = MovePawn(game, SelectedPawns, currentPlayerID, PawnboxForPlacementNode);

				UpdatePawnBox(FindNodePawnBoxPhase1(), game.GetNodePawns(PawnboxForPlacementNode));
				UpdatePawnBox(FindPlayerPawnBox(currentPlayerID), game.GetPlayerPawns(currentPlayerID));
				UpdateResourceLabels();
				IncrementPlayer();
			} catch(MovePawnException _exception) {
				MessageBox.Show(_exception.Message);
			}

			if(phaseEnd) { 
				MessageBox.Show("Phase 1 has completed!");
				IncrementPhase();
				ClearInputs();
				ClearLog();
			}
			LogInput();
		}


		/// <summary>
		/// Rolls dice on click. Count specified by combobox
		/// </summary>
		/// <param name="sender">Dice button</param>
		/// <param name="e">Dice button being pressed</param>
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
			Boolean phaseDone = ActivateNode(game, currentPlayerID, PawnboxForActionNode);
			UpdatePawnBox(FindNodePawnBoxPhase2(), game.GetNodePawns(PawnboxForActionNode));
			UpdatePlayerInformation(this.currentPlayerID);
			UpdateResourceLabels();
			IncrementPlayer();
			if(phaseDone) { 
				MessageBox.Show("Phase 2 Done");
				//IncrementPhase(); When phase 3 is ready
				ClearInputs();
				ClearLog();
			}
			LogInput();
		}


		/// <summary>
		/// Displays information on the card in the relative location.
		/// </summary>
		/// <param name="sender">The label prompt.</param>
		/// <param name="e">Prompt being clicked.</param>
		private void Label_MouseDoubleClick(Object sender, MouseButtonEventArgs e) {
			var pickedNode = (CardNode)game.GetNodeByName((String)(sender as Label).Tag);
			MessageBox.Show(pickedNode.TopCard().ToString());
		}


		/// <summary>
		/// Exits the program on "Exit" click.
		/// </summary>
		/// <param name="sender">The Exit button.</param>
		/// <param name="e">The button being pressed.</param>
		private void ExitBtn_Click(Object sender, RoutedEventArgs e) {
			Application.Current.Shutdown();
		}
		#endregion

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
			return FindName($"{this.NodeComboBox.SelectedItem.ToString().Replace(" ", "")}Box") as ListBox;
		}

		/// <summary>
		/// Finds the currently selected node pawn-box from the action phase combo-box
		/// </summary>
		/// <returns>the pawn-box for the selected node</returns>
		private ListBox FindNodePawnBoxPhase2() {
			return FindName($"{this.NodeComboBox2.SelectedItem.ToString().Replace(" ", "")}Box") as ListBox;
		}


		/// <summary>
		/// Gets the Player Score Label for a player given the player ID.
		/// </summary>
		/// <param name="playerIdP">The player ID</param>
		/// <returns>The Score Label belonging to that player</returns>
		private Label FindPlayerScoreLabel(Int32 playerIdP) {
			return FindName($"P{playerIdP}ScoreValue") as Label;
		}


		/// <summary>
		/// Gets the Player Budget Label for a player given the player ID.
		/// </summary>
		/// <param name="playerIdP">The player ID</param>
		/// <returns>The Budget Label belonging to that player</returns>
		private Label FindPlayerBudgetLabel(Int32 playerIdP) {
			return FindName($"P{playerIdP}BudgetValue") as Label;
		}


		/// <summary>
		/// Gets the Player Funds Label for a player given the player ID.
		/// </summary>
		/// <param name="playerIdP">The player ID</param>
		/// <returns>The Funds Label belonging to that player</returns>
		private Label FindPlayerFundsLabel(Int32 playerIdp) {
			return FindName($"P{playerIdp}FundsValue") as Label;
		}
		#endregion

		#region Update Player Display
		/// <summary>
		/// Updates the player's information display.
		/// </summary>
		/// <param name="playerIdP">The player identifier.</param>
		private void UpdatePlayerInformation(Int32 playerIdP) {

			// Update inventory display
			UpdatePlayerResourceDisplay(FindResourceBox(playerIdP), this.game.GetPlayerResources(playerIdP));
			UpdatePawnBox(FindPlayerPawnBox(playerIdP), this.game.GetPlayerPawns(playerIdP));

			var currentPlayer = this.game.GetPlayerByID(playerIdP);

			// Update Cards
			UpdatePlayerCardsDisplay(playerIdP, currentPlayer.Agility, currentPlayer.Artifacts);
			//	update budget
			UpdatePlayerBudgetDisplay(FindPlayerBudgetLabel(playerIdP), currentPlayer.Budget);
			//	update score
			UpdatePlayerScoreDisplay(FindPlayerScoreLabel(playerIdP), currentPlayer.FeaturePoints);
			//	update funds
			UpdatePlayerFundsDisplay(FindPlayerFundsLabel(playerIdP), currentPlayer.Funds);
		}

		/// <summary>
		/// Updates all the card boxes, as well as the labels for the player.
		/// </summary>
		private void UpdatePlayerCardsDisplay(Int32 playerId, List<Card> agilityCards, List<Card> artifactCards)
		{
			var artifactLabel = FindName($"P{playerId}ArtifactsCountLabel") as Label;

			if (artifactLabel != null)
			{
				artifactLabel.Content = artifactCards.Count;
			}

			var agilityLabel = FindName($"P{playerId}AgilityCountLabel") as Label;
			if (agilityLabel != null)
			{
				agilityLabel.Content = agilityCards.Count;
			}

			var artifactBox = FindName($"P{playerId}ArtifactBox") as ListBox;

			if (artifactBox != null)
			{
				artifactBox.Items.Clear();
				foreach (var card in artifactCards)
				{
					artifactBox.Items.Add(card.GetName());
				}
			}

			var agilityBox = FindName($"P{playerId}AgilityBox") as ListBox;
			if (agilityBox != null)
			{
				agilityBox.Items.Clear();
				foreach(var card in agilityCards)
				{
					agilityBox.Items.Add(card.GetName());
				}
			}

		}


		/// <summary>
		/// Updates the player's Funds label.
		/// </summary>
		/// <param name="playerLabelp"></param>
		/// <param name="playerFundsp"></param>
		private void UpdatePlayerFundsDisplay(Label playerLabelp, Int32 playerFundsp) {
			playerLabelp.Content = playerFundsp;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="playerLabelp"></param>
		/// <param name="playerBudgetp"></param>
		private void UpdatePlayerBudgetDisplay(Label playerLabelp, Int32 playerBudgetp) {
			playerLabelp.Content = playerBudgetp;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="playerLabelp"></param>
		/// <param name="playerScorep"></param>
		private void UpdatePlayerScoreDisplay(Label playerLabelp, Int32 playerScorep) {
			playerLabelp.Content = playerScorep;
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
		/// Updates a specified card display.
		/// </summary>
		/// <param name="cardBox">The display to update.</param>
		/// <param name="card">The new card to be displayed.</param>
		private void UpdateCardBox(TextBox cardBox, String cardString) {
			// Get the card from Game class, not directly accessed.
			///cardBox.Text = card.ToString();
		}


		/// <summary>
		/// Updates the current player id to the next player
		/// </summary>
		private void IncrementPlayer() {
			currentPlayerID = game.currentPlayerIndex + 1;
			PlayerTabControl.SelectedIndex = game.currentPlayerIndex;
		}


		/// <summary>
		/// Updates the current phase tab to the correct phase
		/// </summary>
		private void IncrementPhase() {
			currentPhaseIndex = game.phase - 1;
			PhaseTabControl.SelectedIndex = currentPhaseIndex;
		}
		#endregion

		/// <summary>
		/// Creates the HelpWindow
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HelpBtn_Click(object sender, RoutedEventArgs e)
		{
			new HelpView().Show();
		}


		/// <summary>
		/// Updates the 4 resource node remaining spots labels as nodes enter or leave
		/// </summary>
		private void UpdateResourceLabels() {
			Int32[] spots = game.GetResourceNodeSpots();
			R1SpotValueLabel.Content = spots[0];
			R2SpotValueLabel.Content = spots[1];
			R3SpotValueLabel.Content = spots[2];
			R4SpotValueLabel.Content = spots[3];
		}

		private void TestBtn_Click(Object sender, RoutedEventArgs e)
		{
            foreach(Player p in game.GetAllPlayers()) {
				p.AddToCards(new ArtifactCard("Test Artifact 1", new Int32[] { 0,0,0,0}));
				p.AddToCards(new ArtifactCard("Test Artifact 2", new Int32[] { 0,0,0,0}));
				p.AddToCards(new ArtifactCard("Test Artifact 3", new Int32[] { 0,0,0,0}));
				p.AddToCards(new ArtifactCard("Test Artifact 4", new Int32[] { 0,0,0,0}));
				p.AddToCards(new ArtifactCard("Test Artifact 5", new Int32[] { 0,0,0,0}));
				p.AddToCards(new ArtifactCard("Test Artifact 6", new Int32[] { 0,0,0,0}));

				p.AddToCards(new AgilityCard("Test Agility 1", new Int32[] { 0, 0, 0, 0 }));
				p.AddToCards(new AgilityCard("Test Agility 2", new Int32[] { 0, 0, 0, 0 }));
				p.AddToCards(new AgilityCard("Test Agility 3", new Int32[] { 0, 0, 0, 0 }));
				p.AddToCards(new AgilityCard("Test Agility 4", new Int32[] { 0, 0, 0, 0 }));
				p.AddToCards(new AgilityCard("Test Agility 5", new Int32[] { 0, 0, 0, 0 }));
				p.AddToCards(new AgilityCard("Test Agility 6", new Int32[] { 0, 0, 0, 0 }));
				UpdatePlayerInformation(p.PlayerID);
				Debug.WriteLine($"Finished giving test cards to {p.PlayerID}");

			}
		}

		private void OpenCardWindow_Click(Object sender, RoutedEventArgs e)
		{
			if (sender == null)
				return;


			var item = (sender as ListBox).SelectedItem;

            var cardWindow = new CardWindow();
            cardWindow.Show();

        }

	}

}
