﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data.SqlClient;

namespace order_accounting
{
    public partial class Duty : Form
    {
        private SQLiteConnection sqliteConnection;
        public Duty(MainForm main)
        {
            InitializeComponent();
            mainForm = main;
        }
        private MainForm mainForm;
        public void LoadData(string filter)
            {
            string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string connectionString = $"Data Source={userProfile}\\source\\repos\\order accounting\\SibGold.db;Version=3;";
            sqliteConnection = new SQLiteConnection(connectionString);

         
                try
                {
                    sqliteConnection.Open();
                    string query = "SELECT * FROM Duty Where File_name LIKE @filter OR spec_tech LIKE @filter OR Start_duty LIKE @filter OR End_duty LIKE @filter";
                    SQLiteCommand commandShow = new SQLiteCommand(query, sqliteConnection);
                commandShow.Parameters.AddWithValue("@filter", '%' +filter + '%');
                    SQLiteDataReader reader = commandShow.ExecuteReader();
                
                    while (reader.Read())
                    {
                    dataGridView1.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), reader[9].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    sqliteConnection.Close();
                }
            }

        public void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName == "Delete") // Удаление
            {
                if (MessageBox.Show("Вы хотите удалить запись о смене?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        string id = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                        if (string.IsNullOrEmpty(id))
                        {
                            MessageBox.Show("Id записи не найден.");
                            return;
                        }

                        sqliteConnection.Open();

                        string delete = "DELETE FROM `Duty` WHERE `Id_duty` = @id";
                        using (SQLiteCommand commandDelete = new SQLiteCommand(delete, sqliteConnection))
                        {
                            commandDelete.Parameters.AddWithValue("@id", id);

                            int rowsAffected = commandDelete.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Запись удалена");
                                dataGridView1.Rows.RemoveAt(e.RowIndex); // Удаляем строку из DataGridView
                            }
                            else
                            {
                                MessageBox.Show("Запись не найдена в базе данных.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}");
                    }
                    finally
                    {
                        sqliteConnection.Close();
                    }
                }
            }
            if (colName == "opt") //Подробности смены, просмотр событий
            {
                string id_duty = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                if (string.IsNullOrEmpty(id_duty))
                {
                    MessageBox.Show("Id записи не найден.");
                    return;
                }
                Form detailsForm = new Events(id_duty);
                mainForm.openChildForm(detailsForm);
                mainForm.close_filter();
            }
        }
    }
}