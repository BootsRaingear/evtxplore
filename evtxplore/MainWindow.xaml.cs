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
using System.Diagnostics.Eventing.Reader;
using Microsoft.Win32;
using WK.Libraries.BetterFolderBrowserNS;
using evtxread;

namespace evtxplore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool IsCaseOpened = false;
        Project Case;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void miOpenCase_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void miNewCase_Click(object sender, RoutedEventArgs e)
        {
            var dlgNewCase = new DlgNewCase();
            dlgNewCase.CaseCreated += value => Case = value;
            dlgNewCase.ShowDialog();
        }

        private void miExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAddEvidence_Click(object sender, RoutedEventArgs e)
        {
            var fbd = new BetterFolderBrowser();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                return;

            Case.AddEvidence(fbd.SelectedFolder);
        }

        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            // TODO: check that window position is within visible bounds
            if (Properties.Settings.Default.WdwTop >= 0) Top = Properties.Settings.Default.WdwTop;
            if (Properties.Settings.Default.WdwLeft >= 0) Left = Properties.Settings.Default.WdwLeft;
            if (Properties.Settings.Default.WdwWidth >= 0) Width = Properties.Settings.Default.WdwWidth;
            if (Properties.Settings.Default.WdwHeight >= 0) Height = Properties.Settings.Default.WdwHeight;
            if (Properties.Settings.Default.WdwMaximized) WindowState = WindowState.Maximized;            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                Properties.Settings.Default.WdwTop = RestoreBounds.Top;
                Properties.Settings.Default.WdwLeft = RestoreBounds.Left;
                Properties.Settings.Default.WdwHeight = RestoreBounds.Height;
                Properties.Settings.Default.WdwWidth = RestoreBounds.Width;
                Properties.Settings.Default.WdwMaximized = true;
            }
            else
            {
                Properties.Settings.Default.WdwTop = Top;
                Properties.Settings.Default.WdwLeft = Left;
                Properties.Settings.Default.WdwHeight = Height;
                Properties.Settings.Default.WdwWidth = Width;
                Properties.Settings.Default.WdwMaximized = false;
            }

            Properties.Settings.Default.Save();
        }

        private void btnEvtxReadTest_Click(object sender, RoutedEventArgs e)
        {
            EvtxFile file = new EvtxFile(@"c:\temp\Logs\Windows PowerShell.evtx");

        }
    }
}
