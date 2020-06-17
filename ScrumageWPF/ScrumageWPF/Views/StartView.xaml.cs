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
using ScrumageEngine.Windows;

namespace ScrumageEngine.Views
{
    /// <summary>
    /// Interaction logic for StartView.xaml
    /// </summary>
    public partial class StartView : Page
    {
        // Used to pass along to the Game class
        private List<string> list;

        public StartView()
        {
            InitializeComponent();
        }

        public StartView(List<string> list)
        {
            InitializeComponent();

            this.list = list;
            lblTotalPlayers.Content = $"Total Players: {list.Count}";
        }

        private bool CanStartGame()
        {
            // not enough players
            if (list.Count < 2)
                return false;

            return true;
        }

        /// <summary>
        /// Starts the Game (and validates)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (CanStartGame())
            {
                GameWindow mw = new GameWindow(list);
                mw.Show();

                try
                {
                    Window.GetWindow(this).Close();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }

            }
            else
            {
                MessageBox.Show("Failed to start game.", "ScrumAge");
            }
        }
    }
}
