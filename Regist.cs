using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace order_accounting
{
    public partial class Regist_Form : Form
    {
        private SQLiteConnection conn;
        public Regist_Form()
        {
            InitializeComponent();
            this.Select();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login_text1.Clear();
            Password_Text.Clear();
            textBox1.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Login_text1.Text.Length < 4 || Login_text1.Text.Length > 20)
            {
                MessageBox.Show("Длина логина от 4 до 20 символов");
                return;
            }
            if (Password_Text.Text.Length < 4 || Password_Text.Text.Length > 20)
            {
                MessageBox.Show("Длина пароля от 4 до 20 символов");
                return;
            }
            if (Password_Text.Text != textBox1.Text)
            {
                MessageBox.Show("Пароли не совпадают");
                return;
            }
            try
            {
                string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string connectionString = $"Data Source={userProfile}\\source\\repos\\order accounting\\SibGold.db;Version=3;";
                conn = new SQLiteConnection(connectionString);
                string query1 = "SELECT * FROM Users Where Login = @login";
                conn.Open();
                SQLiteCommand commandShow = new SQLiteCommand(query1, conn);
                commandShow.Parameters.AddWithValue("@login", Login_text1.Text);
                SQLiteDataReader reader = commandShow.ExecuteReader();
                if (reader.Read())
                {
                    MessageBox.Show("Логин занят. Попробуйте другой");
                    return;
                }
                string query = "INSERT INTO Users (Login, Password) VALUES (@login, @Password)";
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@login", Login_text1);
                cmd.Parameters.AddWithValue("@Password", Password_Text);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Пользователь успешно добавлен");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Login_text1_Enter(object sender, EventArgs e)
        {
            if (Login_text1.Text == "Логин")
            {
                Login_text1.Text = "";
                Login_text1.ForeColor = Color.Black;
            }
        }

        private void Login_text1_Leave(object sender, EventArgs e)
        {
            if (Login_text1.Text == "")
            {
                Login_text1.ForeColor = Color.Silver;
                Login_text1.Text = "Логин";
            }
        }

        private void Password_Text_Leave(object sender, EventArgs e)
        {
            if (Password_Text.Text == "")
            {
                Password_Text.ForeColor = Color.Silver;
                Password_Text.Text = "Пароль";
            }
        }

        private void Password_Text_Enter(object sender, EventArgs e)
        {
            if (Password_Text.Text == "Пароль")
            {
                Password_Text.Text = "";
                Password_Text.ForeColor = Color.Black;
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Подтвердите пароль")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.ForeColor = Color.Silver;
                textBox1.Text = "Подтвердите пароль";
            }
        }

        private void Login_text1_TextChanged(object sender, EventArgs e)
        {
            if (Login_text1.Text.Length > 0 &&
                !System.Text.RegularExpressions.Regex.IsMatch(Login_text1.Text.Last().ToString(), @"[a-zA-Z0-9]") && Login_text1.ForeColor == Color.Black)
            {
                Login_text1.Text = Login_text1.Text.Remove(Login_text1.Text.Length - 1);
                Login_text1.SelectionStart = Login_text1.Text.Length; // Устанавливаем курсор в конец
            }
        }

        private void Password_Text_TextChanged(object sender, EventArgs e)
        {
            if (Password_Text.Text.Length > 0 &&
                !System.Text.RegularExpressions.Regex.IsMatch(Password_Text.Text.Last().ToString(), @"[a-zA-Z0-9]") && Password_Text.ForeColor == Color.Black)
            {
                Password_Text.Text = Password_Text.Text.Remove(Password_Text.Text.Length - 1);
                Password_Text.SelectionStart = Password_Text.Text.Length; // Устанавливаем курсор в конец
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0 &&
                !System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text.Last().ToString(), @"[a-zA-Z0-9]") && textBox1.ForeColor == Color.Black)
            {
                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1);
                textBox1.SelectionStart = textBox1.Text.Length; // Устанавливаем курсор в конец
            }
        }
    }
}
