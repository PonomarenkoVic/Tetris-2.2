using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TetrisInterfaces;

namespace TetrisForm
{
    public partial class SaveOptionsForm : Form
    {
        public SaveOptionsForm()
        {
            InitializeComponent();
        }

        public ConnectionBuild ConnBuiltEvent;

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            if (SNameTextB.Text != "" && DBaseNameTBox.Text != "" && LoginTBox.Text != "" && PassTBox.Text != "")
            {
                SqlConnectionStringBuilder connString = new SqlConnectionStringBuilder()
                {
                    DataSource = SNameTextB.Text,
                    InitialCatalog = DBaseNameTBox.Text,
                    UserID = LoginTBox.Text,
                    Password = PassTBox.Text,
                    Pooling = true
                };
                ConnBuiltEvent?.Invoke(this, new ConnEventArg(connString));
                Close();
            }
        }
    }
}
