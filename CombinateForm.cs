using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace order_accounting
{
    public partial class CombinateForm : Form
    {
        public CombinateForm()
        {
            InitializeComponent();
        }
        List<string> sourceFolders = new List<string>();
        List<string> reFilesNames = new List<string>();
        List<string> rePathFiles = new List<string>();
        List<string> reCombinedFolderPath = new List<string>();
        int papk = 1;
        private SQLiteConnection sqliteConnection;

        private void button1_Click(object sender, EventArgs e)
        {
            string destinationFolder = "";
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Выберите каталог, куда собрать все файлы";

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    destinationFolder = folderDialog.SelectedPath;
                }
                else
                {
                    return;
                }
            }
            if (sourceFolders.Count == 0)
            {
                MessageBox.Show("Пожалуйста, выберите папку", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (string sourcefolder in sourceFolders)
            {
                try
                {
                    if (!Directory.Exists(destinationFolder))
                    {
                        Directory.CreateDirectory(destinationFolder);
                    }
                    string NameCopyFolder = "";
                    ProcessDirectoriesRecursively(sourcefolder, destinationFolder, "Неопознанный борт");
                    if (rePathFiles.Count > 0)
                    {
                        string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                        string connectionString = $"Data Source={userProfile}\\source\\repos\\order accounting\\SibGold.db;Version=3;";
                        sqliteConnection = new SQLiteConnection(connectionString);
                        if (MessageBox.Show("Обнаружены дублированные файлы, перезаписать их?", "Дублирование", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            sqliteConnection.Open();
                            string query = "INSERT INTO FILES (Name, From_path) VALUES (@fileName, @filePath)";
                            // Запись дублей в базу данных и перезапись файлов
                            for (int i = 0; i < reFilesNames.Count; i++)
                            {
                                string fileName = reFilesNames[i];
                                string filePath = rePathFiles[i];
                                string folderName = reCombinedFolderPath[i];
                                string destinationFile = Path.Combine(destinationFolder, folderName, fileName);
                                // Перезапись файла
                                File.Copy(filePath, destinationFile, true);
                                // Запись в базу данных
                                using (SQLiteCommand command = new SQLiteCommand(query, sqliteConnection))
                                {
                                    command.Parameters.AddWithValue("@fileName", fileName);
                                    command.Parameters.AddWithValue("@filePath", filePath);
                                    command.ExecuteNonQuery();
                                }
                            }
                            sqliteConnection.Close();
                        }
                    }
                    if (Directory.EnumerateDirectories(destinationFolder).Count() == 0)
                    {
                        MessageBox.Show($"Обработка завершена. Подходящих файлов не обнаружено");
                        return;
                    }
                    MessageBox.Show($"Обработка завершена. Все файлы находятся в папке: {destinationFolder}", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ProcessDirectoriesRecursively(string currentDir, string destinationDir, string folderName)
        {
            try
            {
                string[] subDirectories = Directory.GetDirectories(currentDir, "*", SearchOption.AllDirectories);

                foreach (string subDirectory in subDirectories)
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(folderName, @".*\d{3}.*"))    // Маска для имени папки (3 цифры)
                    {
                        folderName = Path.GetFileName(subDirectory);
                    }
                    string nameCopyFolder = folderName; // Сохраняем имя папки, которое прошло маску
                    string combinedFolderPath = Path.Combine(destinationDir, nameCopyFolder);

                    // Проверяем наличие MOV файлов в текущей папке
                    string[] movFiles = Directory.GetFiles(subDirectory, "*.MOV", SearchOption.TopDirectoryOnly);
                    if (movFiles.Length > 0)
                    {
                        if (!Directory.Exists(combinedFolderPath))
                        {
                            Directory.CreateDirectory(combinedFolderPath);
                        }
                        string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                        string connectionString = $"Data Source={userProfile}\\source\\repos\\order accounting\\SibGold.db;Version=3;";
                        sqliteConnection = new SQLiteConnection(connectionString);
                        foreach (string movFile in movFiles)
                        {
                            string fileName = Path.GetFileName(movFile);
                            string destinationFile = Path.Combine(combinedFolderPath, fileName);

                            if (!File.Exists(destinationFile))
                            {
                                File.Copy(movFile, destinationFile);
                                sqliteConnection.Open();
                                string query = "INSERT INTO FILES (Name, From_path) VALUES (@movFile, @Path)";
                                using (SQLiteCommand command = new SQLiteCommand(query, sqliteConnection))
                                {
                                    command.Parameters.AddWithValue("@movFile", fileName);
                                    command.Parameters.AddWithValue("@Path", movFile);
                                    command.ExecuteNonQuery();
                                }
                                sqliteConnection.Close();
                            }
                            else
                            {
                                reFilesNames.Add(fileName);
                                rePathFiles.Add(movFile);
                                reCombinedFolderPath.Add(combinedFolderPath);
                            }
                        }
                    }
                    ProcessDirectoriesRecursively(subDirectory, destinationDir, folderName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка обработки папки {currentDir}: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void button2_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Выберите папку";

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = folderDialog.SelectedPath;
                    sourceFolders.Add(selectedPath);
                    listBox1.Items.Add(papk + ". " + selectedPath);
                    papk++;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            papk = 1;
            sourceFolders.Clear();
            listBox1.Items.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}