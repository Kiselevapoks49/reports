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
    public partial class Пользователь : Form
    {
        public Пользователь()
        {
            InitializeComponent();
        }

        int n;
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Поставщик_Load(object sender, EventArgs e)
        {
            //string sql = "SELECT postavshik.kod AS \"Код\",postavshik.name AS" +
              //  " \"Название организации\" , postavshik.adress AS " +
              //  "\"Адрес\", postavshik.phone_1 AS " +
               // "\"Телефон 1\" , postavshik.phone_2 AS"+
                //"\"Телефон 2\" , postavshik.web_adress AS \"Адрес сайта\""
                //+ "FROM postavshik ";

           // Главная.Table_Fill("Поставщик", sql);
           // if (Главная.ds.Tables["Поставщик"].Rows.Count > 0)
            {
                n = 0;
                FieldsForm_Fill();
            }
        }

        private void FieldsForm_Clear()
        {
            textBox1.Text = "0";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox1.Enabled = true;
            textBox1.Focus();
        }

        private void FieldsForm_Fill()
        {
            textBox1.Text = Главная.ds.Tables["Пользователь"].Rows[n]["Код"].ToString();
            textBox2.Text = Главная.ds.Tables["Пользователь"].Rows[n]["Фамилия"].ToString();
            textBox3.Text = Главная.ds.Tables["Пользователь"].Rows[n]["Имя"].ToString();
            textBox4.Text = Главная.ds.Tables["Пользователь"].Rows[n]["Пароль"].ToString();
            textBox1.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            n = Главная.ds.Tables["Пользователь"].Rows.Count;
            FieldsForm_Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (n < Главная.ds.Tables["Пользователь"].Rows.Count) n++;
            if (Главная.ds.Tables["Пользователь"].Rows.Count > n)
            {
                FieldsForm_Fill();
            }
            else
            {
                FieldsForm_Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (n > 0)
            {
                n--;
                FieldsForm_Fill();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Главная.ds.Tables["Пользователь"].Rows.Count > 0)
            {
                n = 0;
                FieldsForm_Fill();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sql;
            if (n == Главная.ds.Tables["Пользователь"].Rows.Count)
            {
                sql = "INSERT INTO polzovatel(kod,name,fam,password) values(" + textBox1.Text + ",'" + textBox2.Text + "','" + textBox3.Text + "','"  + textBox4.Text+")";
                if (!Главная.Modification_Execute(sql))
                    return;
                textBox1.Enabled = false;

                Главная.ds.Tables["Пользователь"].Rows.Add(new object[] { textBox1.Text, textBox2.Text, textBox3.Text,textBox4.Text});

            }

            else
            {
                sql = "UPDATE (SELECT * FROM polzovatel) SET name='" + textBox2.Text + "'fam='" + textBox3.Text + "'password='" + textBox4.Text + "WHERE kod=" + textBox1.Text;
                if (!Главная.Modification_Execute(sql))
                    return;

                Главная.ds.Tables["Пользователь"].Rows[n].ItemArray = new object[] { textBox1.Text, textBox2.Text, textBox3.Text,  textBox4.Text };

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string message = "Вы точно хотите удалить из картотеки пользователя с кодом" + textBox1.Text + "?";
            string caption = "Удаление пользователя";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.No) { return; }

            string sql = "DELETE FROM polzovatel WHERE kod=" + textBox1.Text;
            Главная.Modification_Execute(sql);

            try
            {
                Главная.ds.Tables["Пользователь"].Rows.RemoveAt(n);
            }

            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Удаление не было выполнено из-за указания несуществующего экземпляра!!!", "Ошибка");

            }

            if (Главная.ds.Tables["Пользователь"].Rows.Count > n)
            {
                FieldsForm_Fill();
            }

            else
            {
                FieldsForm_Clear();
            }
        }
    }
    
}
