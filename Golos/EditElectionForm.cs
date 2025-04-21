using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Voiting
{
    public partial class EditElectionForm : Form
    {
        private readonly string _connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Mi\Documents\Database312.mdb;";
        private readonly int _currentUserId;
        private readonly int? _electionId;

        public EditElectionForm(int currentUserId, int? electionId = null)
        {
            InitializeComponent();
            _currentUserId = currentUserId;
            _electionId = electionId;

            ConfigureForm();
            if (_electionId.HasValue) LoadElectionData();
        }

        private void ConfigureForm()
        {
            // Настройка минимальных дат
            dtpStartDate.MinDate = DateTime.Today;
            dtpEndDate.MinDate = DateTime.Today.AddDays(1);

            // Настройка заголовка формы
            Text = _electionId.HasValue ? "Редактирование выборов" : "Создание новых выборов";
        }

        private void LoadElectionData()
        {
            try
            {
                using (var conn = new OleDbConnection(_connectionString))
                using (var cmd = new OleDbCommand(
                    "SELECT Title, StartDate, EndDate, Status FROM Elections WHERE ElectionID = @ElectionID",
                    conn))
                {
                    cmd.Parameters.AddWithValue("@ElectionID", _electionId.Value);
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtTitle.Text = reader["Title"].ToString();
                            dtpStartDate.Value = Convert.ToDateTime(reader["StartDate"]);
                            dtpEndDate.Value = Convert.ToDateTime(reader["EndDate"]);
                            cmbStatus.SelectedItem = reader["Status"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных выборов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateData()) return;

            try
            {
                if (_electionId.HasValue)
                {
                    UpdateElection();
                }
                else
                {
                    CreateElection();
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateData()
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Введите название выборов!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTitle.Focus();
                return false;
            }

            if (dtpEndDate.Value <= dtpStartDate.Value)
            {
                MessageBox.Show("Дата окончания должна быть позже даты начала!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpEndDate.Focus();
                return false;
            }

            return true;
        }

        private void CreateElection()
        {
            using (var conn = new OleDbConnection(_connectionString))
            using (var cmd = new OleDbCommand(
                "INSERT INTO Elections (Title, StartDate, EndDate, Status, CreatedByUserID) " +
                "VALUES (@Title, @StartDate, @EndDate, @Status, @CreatedByUserID)",
                conn))
            {
                cmd.Parameters.AddWithValue("@Title", txtTitle.Text.Trim());
                cmd.Parameters.AddWithValue("@StartDate", dtpStartDate.Value);
                cmd.Parameters.AddWithValue("@EndDate", dtpEndDate.Value);
                cmd.Parameters.AddWithValue("@Status", "Planned"); // Статус по умолчанию
                cmd.Parameters.AddWithValue("@CreatedByUserID", _currentUserId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void UpdateElection()
        {
            using (var conn = new OleDbConnection(_connectionString))
            using (var cmd = new OleDbCommand(
                "UPDATE Elections SET " +
                "Title = @Title, " +
                "StartDate = @StartDate, " +
                "EndDate = @EndDate, " +
                "Status = @Status " +
                "WHERE ElectionID = @ElectionID",
                conn))
            {
                cmd.Parameters.AddWithValue("@Title", txtTitle.Text.Trim());
                cmd.Parameters.AddWithValue("@StartDate", dtpStartDate.Value);
                cmd.Parameters.AddWithValue("@EndDate", dtpEndDate.Value);
                cmd.Parameters.AddWithValue("@Status", cmbStatus.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@ElectionID", _electionId.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}