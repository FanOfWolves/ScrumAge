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
using ScrumageEngine.Objects.Items.Cards;

namespace ScrumageEngine.Views
{
    /// <summary>
    /// Interaction logic for CardWindow.xaml
    /// </summary>
    public partial class CardWindow : Window
    {
        public CardWindow()
        {
            InitializeComponent();
        }

        public CardWindow(Card card)
        {
            InitializeComponent();

            txtBlockCardInfo.Text = card.ToString() ?? "Invalid Card";
        }
    }
}
