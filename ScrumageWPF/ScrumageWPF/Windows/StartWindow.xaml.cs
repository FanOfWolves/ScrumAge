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

namespace ScrumageEngine.Windows {
	/// <summary>
	/// Interaction logic for StartWindow.xaml
	/// </summary>
	public partial class StartWindow : Window {
		private int playerCount = 0;
		private List<String> Names = new List<String>();
		public StartWindow() {
			InitializeComponent();
		}

		private void PlayerCountCombo_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			playerCount = int.Parse(PlayerCountCombo.SelectedItem.ToString());
			P1NameBox.Visibility = Visibility.Visible;
			for(int i = 0; i < 4; i++) {
				if(i < playerCount) {
					(FindName($"P{i + 1}Label") as Label).Visibility = Visibility.Visible;
					(FindName($"P{i + 1}NameBox") as TextBox).Visibility = Visibility.Visible;
				} else {
					(FindName($"P{i + 1}Label") as Label).Visibility = Visibility.Hidden;
					(FindName($"P{i + 1}NameBox") as TextBox).Visibility = Visibility.Hidden;
					(FindName($"P{i + 1}NameBox") as TextBox).Text = "";
				}
			}
		}

		private void ToGameBtn_Click(object sender, RoutedEventArgs e) {
			if(playerCount != 0) {
				if(GetNames()) {
					GameWindow mw = new GameWindow(Names);
					mw.Show();
					this.Close();
				}
			} else {
				MessageBox.Show("Please select how many players!");
			}
		}

		private Boolean GetNames() {
			TextBox temp = new TextBox();
			Boolean retBool = true;
			for(int i = 0; i < 4; i++) {
				if(i < playerCount && (temp = (FindName($"P{i + 1}NameBox") as TextBox)).Text != "") {
					Names.Add(temp.Text);
				} else if(i < playerCount && (temp = (FindName($"P{i + 1}NameBox") as TextBox)).Text == "") {
					MessageBox.Show($"Missing name {i + 1}");
					Names.Clear();
					return false;
				}
			}
			return retBool;
		}
	}
}
