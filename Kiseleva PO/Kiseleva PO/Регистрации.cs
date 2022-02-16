using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kiseleva_PO
{
    public partial class Регистрации : Form
    {
        public Регистрации()
        {
            InitializeComponent();
        }

        private void ПО_Load(object sender, EventArgs e)
        {
            string sql = "SELECT nomer AS \"Номер\", name AS \"Название\"," +
               "\"Date\" AS \"Дата установки\"," +
               " firm AS \"Фирма производитель\"," +
               " CONCAT_WS (' ',computer.inventory, computer.type) AS" +
               " \"Инвентарный номер компьютера\"" +
               " FROM ((po LEFT JOIN computer ON computer.inventory=po.inventory_comp))";
              


            Главная.Table_Fill("Установки", sql);

            dataGridView1.DataSource = Главная.ds.Tables["Установки"];
            dataGridView1.BackgroundColor = SystemColors.Control;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        private void ПО_Activated(object sender, EventArgs e)
        {
            dataGridView1.AutoResizeColumns();
            dataGridView1.CurrentCell = null;
        }

        public static int n = -1;

        private void button1_Click(object sender, EventArgs e)
        {
            n = dataGridView1.Rows.Count;

            int num;

            if (n > 0)

                num = Convert.ToInt32(dataGridView1.Rows[n - 1].Cells["Номер"].Value) + 1;

            else num = 1;

            string sql = "INSERT INTO  nomer  values (" + num + ")";

            Главная.Modification_Execute(sql);

            Главная.ds.Tables["Установки"].Rows.Add(new object[] { num, null, null, null, null });
            dataGridView1.CurrentCell = null;


            Добавить1 добавить1 = new Добавить1();
            добавить1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string msg;

            try
            {
                msg = "Вы точно хотите удалить документ с номером" + " " + dataGridView1.Rows[n].Cells["Номер"].Value + "?";

            }

            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Не указана удаляемая запись таблицы", "Ошибка");
                return;
            }

            string caption = "Удаление документа";

            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(msg, caption, buttons);
            if (result == DialogResult.No)
            {
                return;
            }

            string sql = " DELETE FROM po WHERE nomer=" + dataGridView1.Rows[n].Cells["Номер"].Value;
            Главная.Modification_Execute(sql);
            Главная.ds.Tables["Установки"].Rows.RemoveAt(n);
            dataGridView1.AutoResizeColumns();
            dataGridView1.CurrentCell = null;
            n = -1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Добавить1 добавить1 = new Добавить1();
            добавить1.Show();
        }

        private void ПО_FormClosed(object sender, FormClosedEventArgs e)
        {
            Регистрации.n = -1;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            n = dataGridView1.CurrentRow.Index;

            for (int i=0; i<Главная.ds.Tables["ПО"].Rows.Count; i++)
            {
                if (Convert.ToInt32(Главная.ds.Tables["ПО"].Rows[i]["Номер"]) ==
                    Convert.ToInt32(Главная.ds.Tables["Установки"].Rows[n]["Номер"]))

                    Регистрации.n = i;
            }
        }
    }
}
