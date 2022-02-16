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
    public partial class Добавить1 : Form
    {
        public Добавить1()
        {
            InitializeComponent();
        }

        private void Добавить1_Load(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = Главная.ds.Tables["Установки"].Rows[ПО.n]["Номер"].ToString();
                textBox2.Text = Главная.ds.Tables["Установки"].Rows[ПО.n]["Название"].ToString();
                if (Главная.ds.Tables["Установки"].Rows[ПО.n]["Дата установки"] != DBNull.Value)
                    dateTimePicker3.Value = Convert.ToDateTime(Главная.ds.Tables["Установки"].Rows[ПО.n]["Дата"]);
                textBox3.Text = Главная.ds.Tables["Установки"].Rows[ПО.n]["Фирма производитель"].ToString();
                comboBox3.Text = Главная.ds.Tables["Установки"].Rows[ПО.n]["Инвентарный номер компьютера"].ToString();
               
            }

            catch
            {
                MessageBox.Show("Не указана редактируемая запись таблицы!!!", "Ошибка");
                this.Close();
                return;
            }

            textBox1.Enabled = false;

            string sql = " SELECT inventory," +
                " CONCAT_WS (' ',computer.inventory, computer.type) AS \"Инвентарный номер компьютера\"" +
                " FROM computer";

            Главная.Table_Fill("Компьютер", sql);

            comboBox3.Items.Clear();
            for (int i = 0; i < Главная.ds.Tables["Компьютер"].Rows.Count; i++)
            {
                comboBox3.Items.Add(Главная.ds.Tables["Компьютер"].Rows[i]["Инвентарный номер компьютера"].ToString());

            }

            sql = " SELECT name," +
               " CONCAT_WS (' ',postavshik.kod, postavshik.name) AS \"Поставщик\"" +
               " FROM postavshik";

            Главная.Table_Fill("Поставщик", sql);

            comboBox2.Items.Clear();
            for (int i = 0; i < Главная.ds.Tables["Поставщик"].Rows.Count; i++)
            {
                comboBox2.Items.Add(Главная.ds.Tables["Поставщик"].Rows[i]["Поставщик"].ToString());

            }


        }

        int n;
        private void button4_Click(object sender, EventArgs e)
        {
            string inventory = null;
           

            for (int i = 0; i < Главная.ds.Tables["Компьютер"].Rows.Count; i++)
            {
                if (Главная.ds.Tables["Компьютер"].Rows[i]["Инвентарный номер компьютера"].ToString() == comboBox3.Text)
                    inventory = Главная.ds.Tables["Компьютер"].Rows[i]["inventory"].ToString();

            }

            
            string sql = " UPDATE po SET \"Date\"=" + dateTimePicker3.Value +
                "', inventory_comp=" + inventory + ",name=" + textBox2.Text +
                ",firm=" + textBox3.Text +
                " WHERE  \"Number\"=" + textBox1.Text;

            if (!Главная.Modification_Execute(sql))
                return;

            Главная.ds.Tables["Установки"].Rows[ПО.n].ItemArray = new object[] {textBox1.Text,
              textBox2.Text, dateTimePicker3.Value,textBox3.Text , comboBox3.Text};

            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string message = "Вы точно хотите удалить документ с кодом" + textBox1.Text + "?";
            string caption = "Удаление документа";

            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, caption, buttons);

            if (result == DialogResult.No)
            {
                return;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Страница компьютер = new Страница();
            компьютер.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "UPDATE po " +
                "' name='"+textBox2.Text+
                 " SET \"Date\"=" + dateTimePicker3.Value +
                 "'firm='" + textBox3.Text + "'inventory='" + comboBox3.Text +
                  "'WHERE \"Number\"='" + textBox1.Text;

            Главная.ds.Tables["Установки"].Rows.Add(new object[] { textBox1.Text, textBox2.Text, dateTimePicker3.Value, textBox3.Text, comboBox3.Text });
        }
    }

}

 