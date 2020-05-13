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
using VISAPannel.Common;

namespace VISAPannel.Page
{
    /// <summary>
    /// home.xaml 的互動邏輯
    /// </summary>
    public partial class home
    {
        public home()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.ShowAbout();
        }
    }
}
