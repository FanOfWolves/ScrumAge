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
using ScrumageEngine.Views;

namespace ScrumageEngine.Windows
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {

        private List<string> playerList = new List<string>();
        public MainMenu()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Start the Game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartGame_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new StartView(playerList);
        }

        /// <summary>
        /// Exit the Application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExitGame_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// Call <Global>Help Menu</Global>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            new HelpView().Show();
        }

        /// <summary>
        /// Open the options panel to configure the game
        /// This can also be done when the Start Game button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOptions_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new OptionsView(ref playerList);
        }
    }
}
