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
    public partial class Страница : Form
    {
        public Страница()
        {
            InitializeComponent();
        }

        int n;

       
        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Компьютер_Load(object sender, EventArgs e)
        {
           
           string sql = "SELECT computer.inventory AS \"Инвентарный номер\",computer.type AS" +
                " \"Тип\" " 
                + "FROM computer ";

            Главная.Table_Fill("Компьютер", sql);
            if (Главная.ds.Tables["Компьютер"].Rows.Count > 0)
            {
                n = 0;
                FieldsForm_Fill();
            }
        }

        private void FieldsForm_Clear()
        {
            textBox1.Text = "0";
            comboBox1.Text = "";
            textBox1.Enabled = true;
            textBox1.Focus();
        }

        private void FieldsForm_Fill()
        {
            textBox1.Text = Главная.ds.Tables["Компьютер"].Rows[n]["Инвентарный номер"].ToString();
            comboBox1.Text = Главная.ds.Tables["Компьютер"].Rows[n]["Тип"].ToString();
            textBox1.Enabled = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (n < Главная.ds.Tables["Компьютер"].Rows.Count) n++;
            if (Главная.ds.Tables["Компьютер"].Rows.Count > n)
            {
                FieldsForm_Fill();
            }
            else
            {
                FieldsForm_Clear();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            n = Главная.ds.Tables["Компьютер"].Rows.Count;
            FieldsForm_Clear();
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
            if (Главная.ds.Tables["Компьютер"].Rows.Count > 0)
            {
                n = 0;
                FieldsForm_Fill();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
           

            string sql;
            if (n == Главная.ds.Tables["Компьютер"].Rows.Count)
            {
                sql = "INSERT INTO computer(inventory,type) values(" + textBox1.Text + ",'" + comboBox1.Text + ")";
                if (!Главная.Modification_Execute(sql))
                    return;
                textBox1.Enabled = false;

                Главная.ds.Tables["Компьютер"].Rows.Add(new object[] { textBox1.Text, comboBox1.Text});

            }

            else
            {
                sql = "UPDATE (SELECT * FROM computer) SET type='" + comboBox1.Text + "WHERE inventory=" + textBox1.Text;
                if (!Главная.Modification_Execute(sql))
                    return;

                Главная.ds.Tables["Компьютер"].Rows[n].ItemArray = new object[] { textBox1.Text, comboBox1.Text };

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string message = "Вы точно хотите удалить из картотеки компьютер с инвентарным номером" + textBox1.Text + "?";
            string caption = "Удаление компьютера";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.No) { return; }

            string sql = "DELETE FROM computer WHERE inventory=" + textBox1.Text;
            Главная.Modification_Execute(sql);

            try
            {
                Главная.ds.Tables["Компьютер"].Rows.RemoveAt(n);
            }

            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Удаление не было выполнено из-за указания несуществующего экземпляра!!!", "Ошибка");

            }

            if (Главная.ds.Tables["Компьютер"].Rows.Count > n)
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
