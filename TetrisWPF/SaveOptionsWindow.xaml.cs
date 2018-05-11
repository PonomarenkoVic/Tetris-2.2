using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using TetrisInterfaces;

namespace TetrisWPF
{
    /// <summary>
    /// Interaction logic for SaveOptionsWindow.xaml
    /// </summary>
    public partial class SaveOptionsWindow : Window
    {
        public SaveOptionsWindow()
        {
            InitializeComponent();
        }

        public ConnectionBuild ConnBuiltEvent;

        private void ApplyButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (Server.Text != "" && Database.Text != "" && Login.Text != "" && Pass.Password != "")
            {
                SqlConnectionStringBuilder connString = new SqlConnectionStringBuilder()
                {
                    DataSource = Server.Text,
                    InitialCatalog = Database.Text,
                    UserID = Login.Text,
                    Password = Pass.Password,
                    Pooling = true
                };
                ConnBuiltEvent?.Invoke(this, new ConnEventArg(connString));
                Close();
            }
        }
    }
}
