using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace Kiseleva_PO
{


    public partial class Главная : Form
    {
       

        
        public Главная()
        {
            InitializeComponent();

        }

        public static NpgsqlConnection connection = new NpgsqlConnection("Server=127.0.0.1;User Id=postgres;" +
            "Password=111; Database=po");

        public static DataSet ds = new DataSet();

        public static void Table_Fill(string name,string sql)
        {
            if (ds.Tables[name] != null)
                ds.Tables[name].Clear();
            NpgsqlDataAdapter dat;
            dat = new NpgsqlDataAdapter(sql, connection);
            dat.Fill(ds, name);
            connection.Close();
        }

        public static bool Modification_Execute(string sql)
        {
            NpgsqlCommand com;
            com = new NpgsqlCommand(sql,connection);
            connection.Open();

            try
            {
                com.ExecuteNonQuery();
            }

            catch(NpgsqlException)
            {
                MessageBox.Show("Обновление базы данных не было выполнено из-за не указания обновляемых данных или несоответствия их типов!!!", "Ошибка");

            }

            connection.Close();
            return true;

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (Авторизация.polz != "Админ")
            {
                //организацииToolStripMenuItem.Enabled = false;
                //поставщикиToolStripMenuItem.Enabled = false;
                //подразделенияToolStripMenuItem.Enabled = false;
                //настройкаПаролейToolStripMenuItem.Enabled = false;
                //пОToolStripMenuItem1.Enabled = false;
            }   
        }

        private void menuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutBox1 = new AboutBox1();

            aboutBox1.Show();
        }

        private void настройкаПаролейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            НастройкаПаролей настройка = new НастройкаПаролей();
            настройка.Show();

        }

        private void страницыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Страница страница = new Страница();

            страница.Show();
        }

        private void пользователиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Пользователь пользователь = new Пользователь();

            пользователь.Show();
        }

        private void регистрацииToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Регистрации регистрации = new Регистрации();

            регистрации.Show();
        }
    }
    
}
