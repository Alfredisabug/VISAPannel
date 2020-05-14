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
using NationalInstruments.Visa;
using Ivi;
using Ivi.Visa;

namespace VISAPannel.Page
{
    /// <summary>
    /// home.xaml 的互動邏輯
    /// </summary>
    public partial class home
    {
        private List<string> InsList = new List<string>();
        private MessageBasedSession mbSession;

        public home()
        {
            InitializeComponent();
        }

        private void GoToAbout(object sender, RoutedEventArgs e)
        {
            WindowHelper.ShowAbout();
        }

        private void RefreshDevice(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("refresh");
            InsList.Clear();
            using (var rm = new ResourceManager())
            {

                try
                {
                    IEnumerable<string> resources = rm.Find("GPIB?*INSTR");
                    foreach (var item in resources)
                    {
                        ParseResult result = rm.Parse(item);
                        switch (result.InterfaceType)
                        {
                            case HardwareInterfaceType.Gpib:
                                InsList.Add(item);
                                break;
                        }
                    }
                    InsListBox.ItemsSource = InsList;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        #region 開啟儀器
        private bool OpenIns()
        {
            using (var rm = new ResourceManager())
            {
                try
                {
                    Console.WriteLine("Select GPIB Addrss = " + InsListBox.SelectedItem.ToString());
                    mbSession = (MessageBasedSession)rm.Open(InsListBox.SelectedItem.ToString());
                    Console.WriteLine("Open success.");
                }
                catch (InvalidCastException)
                {
                    MessageBox.Show("Resource selected must be a message-based session");
                    return false;
                }
                catch (Exception exp)
                {
                    MessageBox.Show("Other exp:" + exp.Message);
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region 關閉儀器
        private bool closeIns()
        {
            mbSession.Dispose();
            return true;
        }
        #endregion

        private void QueryIns(object sender, RoutedEventArgs e)
        {
            if (!OpenIns())
            {
                return;
            }

            try
            {
                string textToWrite = ReplaceCommonEscapeSequences(CMDTextBox.Text);
                mbSession.RawIO.Write(textToWrite);
                ReturnDataTextBox.Text = InsertCommonEscapeSequences(mbSession.RawIO.ReadString());
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }

            closeIns();
        }

        private string ReplaceCommonEscapeSequences(string s)
        {
            return s.Replace("\\n", "\n").Replace("\\r", "\r");
        }

        private string InsertCommonEscapeSequences(string s)
        {
            return s.Replace("\n", "\\n").Replace("\r", "\\r");
        }
    }
}
