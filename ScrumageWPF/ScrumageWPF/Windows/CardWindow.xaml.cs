using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using ScrumageEngine.Objects.Items.Cards;

namespace ScrumageEngine.Views
{
    /// <summary>
    /// Interaction logic for CardWindow.xaml
    /// </summary>
    public partial class CardWindow : Window
    {
        /// <summary>
        /// Creates a new CardWindow
        /// </summary>
        public CardWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Creates a new CardWindow
        /// </summary>
        /// <param name="card"></param>
        public CardWindow(Card card)
        {
            InitializeComponent();

            txtBlockCardInfo.Text = card.ToString() ?? "Invalid Card";
        }
    }
}
