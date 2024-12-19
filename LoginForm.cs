using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace order_accounting
{
    public partial class LoginForm : Form
    {
        private SQLiteConnection sqliteConnetion;
        public LoginForm()
        {
            InitializeComponent();
            this.Select();
        }
        public void ClearFields()
        {
            Login_text1.Text = "Логин";
            Password_Text.Text = "Пароль";
            Login_text1.ForeColor = Color.Silver;
            Password_Text.ForeColor = Color.Silver;
            Password_Text.UseSystemPasswordChar = false;
            checkBox2.Checked = false;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            bool RememberFlag = false;
            string userlog = "";
            string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string connectionString = $"Data Source={userProfile}\\source\\repos\\order accounting\\SibGold.db;Version=3;";
            using (SQLiteConnection SqlConn = new SQLiteConnection(connectionString))
            {
                SqlConn.Open();
                string query = "SELECT Login FROM Users Where remember_me = 1";
                using (SQLiteCommand cmd = new SQLiteCommand(query, SqlConn))
                {
                    cmd.Parameters.AddWithValue("@login", Login_text1.Text);
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            RememberFlag = true;
                            userlog = reader.GetString(0);
                        }
                    }
                }
            }
            if (RememberFlag == true)
            {
                RememberFlag = false;
                this.Hide();
                MainForm mainForm = new MainForm(this, userlog);
                mainForm.ShowDialog();
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
                Login_text1.Text = "Логин";
                Login_text1.ForeColor = Color.Silver;
            }
        }

        private void Password_Text_Enter(object sender, EventArgs e)
        {
            if (Password_Text.Text == "Пароль")
            {
                Password_Text.UseSystemPasswordChar = true;
                Password_Text.Text = "";
                Password_Text.ForeColor = Color.Black;
            }
        }

        private void Password_Text_Leave(object sender, EventArgs e)
        {
            if (Password_Text.Text == "")
            {
                Password_Text.UseSystemPasswordChar = false;
                Password_Text.Text = "Пароль";
                Password_Text.ForeColor = Color.Silver;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login_text1.Clear();
            Password_Text.Clear();
                Login_text1.Text = "Логин";
                Login_text1.ForeColor = Color.Silver;
                Password_Text.UseSystemPasswordChar = false;
                Password_Text.Text = "Пароль";
                Password_Text.ForeColor = Color.Silver;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (Password_Text.ForeColor == Color.Silver && Password_Text.Text == "Пароль")
                return;
                if (checkBox1.Checked == true)
                {
                    Password_Text.UseSystemPasswordChar = false;
                }
                else
                {
                    Password_Text.UseSystemPasswordChar = true;
                }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            User authUser = null;
            using (ApplicationContext cont = new ApplicationContext())
            {
                authUser = cont.Users.Where(b => b.Login == Login_text1.Text && b.Password == Password_Text.Text).FirstOrDefault();
            }

            if (authUser != null)
            {
                if (checkBox2.Checked == true)
                {
                    string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                    string connectionString = $"Data Source={userProfile}\\source\\repos\\order accounting\\SibGold.db;Version=3;";
                    using (SQLiteConnection SqlConn = new SQLiteConnection(connectionString))
                    {
                        SqlConn.Open();
                        string query = "UPDATE Users SET remember_me = 1 WHERE Login = @login";
                        using (SQLiteCommand cmd = new SQLiteCommand(query, SqlConn))
                        {
                            cmd.Parameters.AddWithValue("@login", Login_text1.Text);
                            cmd.ExecuteNonQuery();
                        }
                        SqlConn.Close();
                    }
                }
                MainForm mainForm = new MainForm(this,Login_text1.Text);
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Пользователь не найден");
            }
        }

        private void Login_text1_TextChanged(object sender, EventArgs e)
        {
            if (Login_text1.Text.Length > 0 && Login_text1.Text.Last() == ' ')
            {
                Login_text1.Text = Login_text1.Text.Remove(Login_text1.Text.Length - 1);
                Login_text1.SelectionStart = Login_text1.Text.Length;
            }
        }

        private void Password_Text_TextChanged(object sender, EventArgs e)
        {
            if (Password_Text.Text.Length > 0 && Password_Text.Text.Last() == ' ')
            {
                Password_Text.Text = Password_Text.Text.Remove(Password_Text.Text.Length - 1);
                Password_Text.SelectionStart = Password_Text.Text.Length;
            }
        }

        private void LoginForm_Activated(object sender, EventArgs e)
        {
            
        }
    }
}