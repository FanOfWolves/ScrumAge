﻿using ScrumageEngine.BoardSpace;
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

namespace ScrumageEngine {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		private int PlayerCount = 0;
		Game game;
		private int currentPlayerID;
		private List<Pawn> SelectedPawns = new List<Pawn>();
		public MainWindow(List<String> playerNames) {
			PlayerCount = playerNames.Count;
			game = new Game(playerNames);
			InitializeComponent();
			InitPlayerTab(playerNames);
			InitLocationComboBox(game.board.GetAllNodes());
			currentPlayerID = PlayerTabControl.SelectedIndex;
		}
		void InitLocationComboBox(List<Node> nodes) {
			foreach(Node node in nodes) {
				NodeComboBox.Items.Add(node.NodeName);
			}
		}
		void InitPlayerTab(List<String> playerNames) {
			TabItem temp = null;
			for(int i = 0; i < playerNames.Count; i++) {
				temp = PlayerTabControl.Items[i] as TabItem;
				temp.Visibility = Visibility.Visible;
				temp.IsEnabled = true;
				temp.Header = playerNames[i];
				(temp.FindName($"P{i + 1}NameValue") as Label).Content = playerNames[i];
			}
			
		}
		private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
			MessageBox.Show("Make GUI to display card requirements");
		}

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
			LogInput();
		}

		void LogInput() {
			ClearLog();
			foreach(String input in GetRecentInputs())
				SprintLogBox.Items.Add(input);
		}
		void ClearLog() {
			SprintLogBox.Items.Clear();
		}
		private void UpdateDieBoxes(List<Die> dice) {
			TextBox tempBox = null;
			for(int i = 0; i < 7; i++) {
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

		private void UpdatePawnBox(ListBox pawnBox, List<Pawn> pawns) {
			pawnBox.Items.Clear();
			foreach(Pawn p in pawns) {
				pawnBox.Items.Add(p);
			}
		}
		private void UpdateCardBox(TextBox cardBox, Card card) {
			cardBox.Text = card.ToString();
		}

		private ListBox GetPlayerPawnBoxByID(int id) {
			switch(id) {
				case 0: return P1PawnBox;
				case 1: return P2PawnBox;
				case 2: return P3PawnBox;
				case 3: return P4PawnBox;
				default: return P1PawnBox;
			}
		}
		private void PlayerTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			currentPlayerID = (sender as TabControl).SelectedIndex;
		}

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
			game.board.dice = RollDice(int.Parse(DiceCountCombo.SelectedItem.ToString()));
			UpdateDieBoxes(game.board.dice);
			LogInput();
		}
	}
}
