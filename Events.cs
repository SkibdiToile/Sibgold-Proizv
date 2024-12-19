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
using System.Data.SqlClient;

namespace order_accounting
{
    public partial class Events : Form
    {
        private SQLiteConnection sqliteConnection;
        public Events(string id_duty)
        {
            InitializeComponent();
            LoadData(id_duty);
        }
        private void LoadData(string id_duty)
        {
            string connectionString = "Data Source=C:\\Users\\Павел\\source\\repos\\order accounting\\SibGold.db;Version=3;";
            sqliteConnection = new SQLiteConnection(connectionString);

            try
            {
                sqliteConnection.Open();
                string query = "SELECT * FROM Events WHERE duty = @id_duty";
                SQLiteCommand commandShow = new SQLiteCommand(query, sqliteConnection);
                commandShow.Parameters.AddWithValue("@id_duty", id_duty);
                SQLiteDataReader reader = commandShow.ExecuteReader();

                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString());
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName == "Delete") // Удаление
            {
                if (MessageBox.Show("Вы хотите удалить запись?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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

                        string delete = "DELETE FROM `Events` WHERE `Id_event` = @id";
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
        }
    }
}
