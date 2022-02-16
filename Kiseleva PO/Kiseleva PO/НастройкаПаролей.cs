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
    public partial class НастройкаПаролей : Form
    {
        public НастройкаПаролей()
        {
            InitializeComponent();
        }

        private void НастройкаПаролей_Load(object sender, EventArgs e)
        {
            string sql;

            sql = "SELECT * FROM usser ORDER BY login";

            Главная.Table_Fill("Пользователь", sql);

            for (int i=0; i < Главная.ds.Tables["Пользователь"].Rows.Count; i++)
            {
                comboBox1.Items.Add(Главная.ds.Tables["Пользователь"].Rows[i]["login"]);
            }

            textBox1.UseSystemPasswordChar= true;
            textBox2.UseSystemPasswordChar= true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == textBox2.Text)
            {
                string sql = "UPDATE usser SET password ='" + textBox1.Text +
                    "'WHERE login='" + comboBox1.Text + "'";

                Главная.Modification_Execute(sql);

                Close();
            }
            else
            {
                MessageBox.Show("Неверное подтверждение пароля");
            }
        }
    }
}
