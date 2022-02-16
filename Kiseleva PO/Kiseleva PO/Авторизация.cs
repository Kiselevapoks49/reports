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
    public partial class Авторизация : Form
    {
        public Авторизация()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                textBox1.UseSystemPasswordChar = false;
            else
                textBox1.UseSystemPasswordChar = true;
        }

        private void Авторизация_Load(object sender, EventArgs e)
        {
            string sql;

            sql = "SELECT * FROM usser ORDER BY login";

            Главная.Table_Fill("Пользователь", sql);

            for ( int i =0; i < Главная.ds.Tables["Пользователь"].Rows.Count; i++)
            {
                comboBox1.Items.Add(Главная.ds.Tables["Пользователь"].Rows[i]["login"]);
            }

            textBox1.UseSystemPasswordChar = true;
        }

        public static string polz = "";
        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == Главная.ds.Tables["Пользователь"].Rows[comboBox1.SelectedIndex]["password"].ToString())
            {
                if (comboBox1.Text == "Администратор")
                    polz = "Админ";

                Hide();

                Главная главная = new Главная();
                главная.ShowDialog();
                Close();

            }

            else
            {
                MessageBox.Show("Неправильный пароль", "Ошибка");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Пользователь пользователь = new Пользователь();
            пользователь.Show();
        }
    }
}
