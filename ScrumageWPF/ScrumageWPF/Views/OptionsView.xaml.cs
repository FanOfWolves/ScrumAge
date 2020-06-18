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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScrumageEngine.Views
{
    /// <summary>
    /// Interaction logic for OptionsView.xaml
    /// </summary>
    public partial class OptionsView : Page
    {

        private List<string> _nameList;

        private int MAX_PLAYERS = 4;
        private int CURRENT_PLAYERS = 2;

        /// <summary>
        /// Creates a new instance of OptionsViews
        /// </summary>
        public OptionsView()
        {
            InitializeComponent();
            Loaded += OnLoad;
        }


        /// <summary>
        /// Clears a TextBox's text when Focused
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearButtonPreview(object sender, RoutedEventArgs e)
        {
            TextBox box = sender as TextBox;
            box.Text = "";
            box.GotFocus -= ClearButtonPreview;
        }

        /// <summary>
        /// Creates a new options view, with namesList
        /// </summary>
        /// <param name="namesList"></param>

        public OptionsView(ref List<string> namesList)
        {
            InitializeComponent();

            _nameList = namesList;
            Loaded += OnLoad;

            // We already have some names, so let's build them
            if (namesList.Count >= 2)
            {
                BuildPlayerBoxes(namesList);
            }

        }

        /// <summary>
        /// Called from UI to indicate our panel is loaded
        /// </summary>
        private void OnLoad(object sender, RoutedEventArgs e)
        {
            // Build player options
            for (int i = 2; i <= MAX_PLAYERS; i++)
            {
                cbPlayerList.Items.Add(i);
            }
        }


        private void cbPlayerList_ValueChanged(object sender, EventArgs e)
        {
            BuildPlayerBoxes(Convert.ToInt16(cbPlayerList.SelectedValue));
        }


        /// <summary>
        /// Builds the player name boxes
        /// </summary>
        private void BuildPlayerBoxes(int amount = 2)
        {
            // clear any previous
            playerButtons.Children.Clear();

            CURRENT_PLAYERS = amount;

            for (int i = 0; i < CURRENT_PLAYERS; i++)
            {
                TextBox txtBox = new TextBox();
                txtBox.FontSize = 16;
                txtBox.Text = $"Player {i + 1}";
                txtBox.Margin = new Thickness(0, 5, 0, 0);
                txtBox.GotFocus += ClearButtonPreview;
                playerButtons.Children.Add(txtBox);
            }
        }


        /// <summary>
        /// Builds the playerlist from <list type="string">List</list>
        /// </summary>
        /// <param name="list"></param>
        private void BuildPlayerBoxes(List<string> list)
        {
            // clear any previous
            playerButtons.Children.Clear();
            foreach (var playerName in list)
            {
                TextBox txtBox = new TextBox();
                txtBox.FontSize = 16;
                txtBox.Text = playerName;
                txtBox.Margin = new Thickness(0, 5, 0, 0);
                txtBox.GotFocus += ClearButtonPreview;
                playerButtons.Children.Add(txtBox);
                CURRENT_PLAYERS++;
            }
        }

        /// <summary>
        /// Checks for any invalid textboxes
        /// </summary>
        /// <returns>true valid, false invalid.</returns>
        private bool Validate()
        {
            var children = playerButtons.Children;

            // Not enough players selected
            if (children.Count < 2)
                return false;

            foreach (TextBox child in children)
            {
                if (String.IsNullOrEmpty(child.Text))
                {
                    return false;
                }
            }

            return true;
        }


        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (Validate())
            {
                // Clear any previous players from list
                _nameList.Clear();
                foreach (TextBox child in playerButtons.Children)
                {
                    _nameList.Add(child.Text);
                }

                MessageBox.Show("Player list saved!", "ScrumAge");
            }
            else
            {
                MessageBox.Show("Invalid player list.", "ScrumAge");
            }
        }
    }
}
