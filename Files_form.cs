using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace order_accounting
{
    public partial class Files_form : Form
    {
        private SQLiteConnection sqliteConnection;
        public Files_form()
        {
            InitializeComponent();
        }
        public void LoadData(string filter)
        {
            string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string connectionString = $"Data Source={userProfile}\\source\\repos\\order accounting\\SibGold.db;Version=3;";
            sqliteConnection = new SQLiteConnection(connectionString);


            try
            {
                sqliteConnection.Open();
                string query = "SELECT * FROM Files Where Name like @filter OR Status like @filter OR Who_process like @filter OR From_path like @filter";
                SQLiteCommand commandShow = new SQLiteCommand(query, sqliteConnection);
                commandShow.Parameters.AddWithValue("@filter", '%' + filter + '%');
                SQLiteDataReader reader = commandShow.ExecuteReader();
                int Number = 0;
                while (reader.Read())
                {
                    Number++;
                    dataGridView1.Rows.Add(Number, reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString());
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
    }
}
