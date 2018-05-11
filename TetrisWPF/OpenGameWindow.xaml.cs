using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for OpenGameWindow.xaml
    /// </summary>
    public partial class OpenGameWindow : Window
    {
        public OpenGameWindow(DataTable table)
        {
            InitializeComponent();
            Table.ItemsSource = table.DefaultView;
            _table = table;
        }

        public SavePointChoose PointChose;

        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            int index = Table.SelectedIndex;
            int id = (int)_table.Rows[index][0];
            int level = (int)_table.Rows[index][2];
            int burnL = (int)_table.Rows[index][3];
            int score = (int)_table.Rows[index][4];
            int idField = (int)_table.Rows[index][5];

            PointChose?.Invoke(this, new SavePointEventArg(id, level, burnL, score, idField));
            Close();

        }

        private readonly DataTable _table;
    }
}
