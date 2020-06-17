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

        /// <summary>
        /// Creates a new instance of OptionsViews
        /// </summary>
        public OptionsView()
        {
            InitializeComponent();
            Loaded += OnLoad;
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

        }

        /// <summary>
        /// Called from UI to indicate our panel is loaded
        /// </summary>
        private void OnLoad(object sender, RoutedEventArgs e)
        {
            // Build player options
            for (int i = 1; i <= MAX_PLAYERS; i++)
            {
                cbPlayerList.Items.Add(i);
            }
        }

    }
}
