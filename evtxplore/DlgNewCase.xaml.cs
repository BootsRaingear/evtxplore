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
using System.Windows.Shapes;
using System.Windows.Forms;
using WK.Libraries.BetterFolderBrowserNS;
using System.IO;

namespace evtxplore
{
    /// <summary>
    /// Interaction logic for DlgNewCase.xaml
    /// </summary>
    public partial class DlgNewCase : Window
    {
        public event Action<Project> CaseCreated;

        public DlgNewCase()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var fbd = new BetterFolderBrowser();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                return;

            txtDBFolder.Text = fbd.SelectedFolder;
            // confirm file exists 
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (txtCasenum.Text.Length == 0)
            {
                return;
            }
            if (!Directory.Exists(txtDBFolder.Text))
            {

                return;
            }

            var newCase = new Project(txtCasenum.Text, txtDBFolder.Text, txtNotes.Text);
            CaseCreated(newCase);
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
