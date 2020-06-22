using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ScrumageEngine.Views
{
    /// <summary>
    /// Interaction logic for HelpView.xaml
    /// </summary>
    public partial class HelpView : Window
    {
        public HelpView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Open the Rules HTML file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void HelpView_Loaded(object sender, RoutedEventArgs args)
        {
            string curDir = Directory.GetCurrentDirectory();
            this.webRules.Navigate(new Uri(String.Format("file:///{0}/Content/Rules/rules.html", curDir)));
        }
    }
}
