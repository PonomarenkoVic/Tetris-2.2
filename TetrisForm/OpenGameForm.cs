using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TetrisInterfaces;

namespace TetrisForm
{
    public partial class OpenGameForm : Form
    {
        public OpenGameForm(DataTable table)
        {
            InitializeComponent();
            SavePointsDGridView.DataSource = table;
        }

        public SavePointChoose PointChose;

        private void SavePointsDGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            int id = (int)SavePointsDGridView.Rows[index].Cells[0].Value;
            int level = (int)SavePointsDGridView.Rows[index].Cells[2].Value;
            int burnL = (int)SavePointsDGridView.Rows[index].Cells[3].Value;
            int score = (int)SavePointsDGridView.Rows[index].Cells[4].Value;
            int idField = (int)SavePointsDGridView.Rows[index].Cells[5].Value;

            PointChose?.Invoke(this, new SavePointEventArg(id, level, burnL, score, idField));
            Close();
        }
    }
}
