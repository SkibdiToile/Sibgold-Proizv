using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using CsvHelper;
using System.Globalization;
using System.Data.SQLite;
using System.Drawing.Configuration;
using System.Data.SqlClient;

namespace order_accounting
{
    public partial class MainForm : Form
    {
        string log;
        private LoginForm logForm;
        public MainForm(LoginForm logForm,string login)
        {
            InitializeComponent();
            this.logForm = logForm;
            log = login;
            label1.Text = log;
            this.Select();

        }
        public void close_filter()
        {
            textBox1.Visible = false;
            textBox2.Visible = false;
            pictureBox2.Visible = false;
        }
        private SQLiteConnection sqliteConnection;
        private bool isButtonClose = false;
        private void button2_Click(object sender, EventArgs e)
        {
            isButtonClose = true;
            string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string connectionString = $"Data Source={userProfile}\\source\\repos\\order accounting\\SibGold.db;Version=3;";
            using (sqliteConnection = new SQLiteConnection(connectionString))
            {
                string query = "UPDATE Users SET remember_me = 0 Where remember_me = 1";
                sqliteConnection.Open();
                SQLiteCommand cmd = new SQLiteCommand(query, sqliteConnection);
                cmd.ExecuteNonQuery();
            }
            this.Close();
            logForm.Show();
            logForm.ClearFields();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            var dutyForm = new Duty(this);
            dutyForm.LoadData(null); // Загрузка данные
            openChildForm(dutyForm);
            textBox1.Visible = true;
            textBox2.Visible = false;
            pictureBox2.Visible = true;
        }
         Form activeForm = null;
        public void openChildForm(Form childForm) // Дочерняя форма
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            Child_panel.Controls.Add(childForm);
            Child_panel.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void button1_Click(object sender, EventArgs e) //Сборка папок
        {
            CombinateForm combinateForm = new CombinateForm();
            combinateForm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
                {
                    folderDialog.Description = "Выберите папку с CSV файлами";

                    if (folderDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Получаем путь к выбранной папке
                        string selectedPath = folderDialog.SelectedPath;

                        // Поиск всех CSV файлов в папке и подкаталогах
                        var csvFiles = Directory.GetFiles(selectedPath, "*.csv", SearchOption.AllDirectories);

                        if (csvFiles.Length == 0)
                        {
                            MessageBox.Show("CSV файлы не найдены.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                        string connectionString = $"Data Source={userProfile}\\source\\repos\\order accounting\\SibGold.db;Version=3;";

                        using (SQLiteConnection sqliteConnection = new SQLiteConnection(connectionString))
                        {
                            sqliteConnection.Open();

                            foreach (var filePath in csvFiles)
                            {
                                using (var reader = new StreamReader(filePath))
                                {
                                    var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                                    var records = csv.GetRecords<dynamic>();

                                    foreach (var record in records)
                                    {
                                        string checkQuery = "SELECT COUNT(1) FROM Duty WHERE File_name = @File_name";
                                        using (SQLiteCommand checkCmd = new SQLiteCommand(checkQuery, sqliteConnection))
                                        {
                                            checkCmd.Parameters.AddWithValue("@File_name", record.filename);
                                            long exists = (long)checkCmd.ExecuteScalar();

                                            if (exists > 0)
                                            {
                                                continue;
                                            }
                                        }

                                        string insertQuery = "INSERT INTO Duty (File_name, spec_tech, Start_duty, End_duty, all_buckets, average_buckets, number_of_cars) " +
                                                             "VALUES (@file_name, @Spec_tech, @start_duty, @end_duty, @All_buckets, @Average_buckets, @Number_of_cars)";
                                        using (SQLiteCommand insertCmd = new SQLiteCommand(insertQuery, sqliteConnection))
                                        {
                                            insertCmd.Parameters.AddWithValue("@file_name", record.filename);
                                            insertCmd.Parameters.AddWithValue("@Spec_tech", record.machine_number);
                                            insertCmd.Parameters.AddWithValue("@start_duty", $"{record.start_file_data} {record.start_file_time}");
                                            insertCmd.Parameters.AddWithValue("@end_duty", $"{record.end_file_data} {record.end_file_time}");
                                            insertCmd.Parameters.AddWithValue("@All_buckets", record.buckets);
                                            insertCmd.Parameters.AddWithValue("@Average_buckets", record.average_buckets);
                                            insertCmd.Parameters.AddWithValue("@Number_of_cars", record.cars);
                                            insertCmd.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }
                            sqliteConnection.Close();
                        }
                        MessageBox.Show("Все данные успешно загружены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e) // Отправка на сервер
        {
            try
            {
                string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string sourceDirectory = $@"C:\Users\{userProfile}\Videos\Combinated";

                // Кол-во серверов
                int serverCount = GetServerCount();
                if (serverCount == -10321)
                    return;
                if (serverCount <= 0)
                {
                    MessageBox.Show("Количество серверов должно быть больше нуля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string[] serverDirectories = new string[serverCount];
                string rootDirectory;

                using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
                {
                    folderDialog.Description = "Выберите корневую папку для серверов";

                    if (folderDialog.ShowDialog() == DialogResult.OK)
                    {
                        rootDirectory = folderDialog.SelectedPath;
                    }
                    else
                    {
                        return;
                    }
                }

                for (int i = 0; i < serverCount; i++)
                {
                    serverDirectories[i] =  Path.Combine(rootDirectory, $"Server {i + 1}");
                }

                using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
                {
                    folderDialog.Description = "Выберите папку";

                    if (folderDialog.ShowDialog() == DialogResult.OK)
                    {
                        string selectedPath = folderDialog.SelectedPath;
                        sourceDirectory = selectedPath;

                        var subdirectories = Directory.GetDirectories(sourceDirectory);
                        var subFiles = Directory.GetFiles(sourceDirectory);

                        foreach (var serverDir in serverDirectories)
                        {
                            if (!Directory.Exists(serverDir))
                            {
                                Directory.CreateDirectory(serverDir);
                            }
                        }

                        if (subdirectories.Length == 0)
                        {
                            if (subFiles.Length == 0)
                            {
                                CopyDirectory(sourceDirectory, serverDirectories[0]);
                                return;
                            }
                            MessageBox.Show("Папка не содержит файлов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        int serverIndex = 0;
                        foreach (var folder in subdirectories)
                        {
                            string folderName = Path.GetFileName(folder);
                            string destinationPath = Path.Combine(serverDirectories[serverIndex], folderName);
                            CopyDirectory(folder, destinationPath);
                            serverIndex = (serverIndex + 1) % serverDirectories.Length;
                        }
                        MessageBox.Show("Файлы отправлены на серверную обработку", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

        private int GetServerCount()
        {
            using (Form inputForm = new Form())
            {
                inputForm.Text = "Введите количество серверов";
                inputForm.Width = 300;
                inputForm.Height = 150;
                inputForm.StartPosition = FormStartPosition.CenterScreen;
                Label label = new Label() { Left = 25, Top = 25, Text = "Кол-во серверов:" };
                TextBox textBox = new TextBox() { Left = 150, Top = 18, Width = 100 };
                Button confirmButton = new Button() { Text = "OK", Left = 100, Width = 80, Top = 60, DialogResult = DialogResult.OK };

                inputForm.Controls.Add(label);
                inputForm.Controls.Add(textBox);
                inputForm.Controls.Add(confirmButton);
                inputForm.AcceptButton = confirmButton;

                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    if (int.TryParse(textBox.Text, out int serverCount))
                    {
                        return serverCount;
                    }
                    else
                    {
                        MessageBox.Show("Введите корректное число.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    return -10321;
                }
            }
            return 0;
        }

        static void CopyDirectory(string sourceDir, string destinationDir)
        {
            if (!Directory.Exists(destinationDir))
            {
                Directory.CreateDirectory(destinationDir);
            }

            foreach (string file in Directory.GetFiles(sourceDir))
            {
                string destFile = Path.Combine(destinationDir, Path.GetFileName(file));
                File.Copy(file, destFile, true);
            }

            foreach (string dir in Directory.GetDirectories(sourceDir))
            {
                string destDir = Path.Combine(destinationDir, Path.GetFileName(dir));
                CopyDirectory(dir, destDir);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Regist_Form regist = new Regist_Form();
            regist.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var fileform = new Files_form();
            fileform.LoadData(null);
            openChildForm(fileform);
            textBox2.Visible = true;
            pictureBox2.Visible = true;
            textBox1.Visible = false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }

        private void MainForm_Deactivate(object sender, EventArgs e)
        {
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            try
            {
                string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string connectionString = $"Data Source={userProfile}\\source\\repos\\order accounting\\SibGold.db;Version=3;";
                sqliteConnection = new SQLiteConnection(connectionString);
                sqliteConnection.Open();
                string query = "SELECT * FROM Users Where Login = @login";
                SQLiteCommand sQLiteCommand = new SQLiteCommand(query, sqliteConnection);
                sQLiteCommand.Parameters.AddWithValue("@login", log);
                using (SQLiteDataReader reader = sQLiteCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string adm = reader["Admin_rules"].ToString();
                        if (adm == "1")
                            button6.Visible = true;
                    }
                }
                sqliteConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var dutyForm = new Duty(this);
            dutyForm.LoadData(textBox1.Text); // Загрузка данные
            openChildForm(dutyForm);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            var fileform = new Files_form();
            fileform.LoadData(textBox2.Text);
            openChildForm(fileform);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !isButtonClose)
            {
                Application.Exit();
            }
            else
            {
                e.Cancel = false;
            }
        }
    }
}