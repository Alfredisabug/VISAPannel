using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VISAPannel.Page;
using VISAPannel.Common;

namespace VISAPannel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var homePage = new home();
            var aboutPage = new about();

            // 註冊事件
            WindowHelper.ShowPageHome = () =>
            {
                frame.Navigate(homePage);
            };

            WindowHelper.ShowAbout = () =>
            {
                frame.Navigate(aboutPage);
            };

            frame.Navigate(homePage);
        }
    }
}
