using System;
using System.Data.OleDb;
using System.Windows.Forms;
using Voting.Models;

namespace Voiting
{
    public partial class VotingForm : Form
    {
        private readonly int _voterId;
        private readonly int _electionId;

        public VotingForm(int voterId, int electionId)
        {
            InitializeComponent();
            _voterId = voterId;
            _electionId = electionId;
            LoadCandidates();
        }

        private void LoadCandidates()
        {
            try
            {
                cmbCandidates.Items.Clear();

                using (var conn = DatabaseHelper.GetConnection())
                {
                    var cmd = new OleDbCommand(
                        "SELECT CandidateID, FullName, Party FROM Candidates " +
                        "WHERE ElectionID = @ElectionID AND IsActive = True",
                        conn);

                    cmd.Parameters.AddWithValue("@ElectionID", _electionId);
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbCandidates.Items.Add(new Candidate
                            {
                                CandidateID = Convert.ToInt32(reader["CandidateID"]),
                                FullName = reader["FullName"].ToString(),
                                Party = reader["Party"].ToString()
                            });
                        }
                    }
                }
                cmbCandidates.DisplayMember = "FullName";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки кандидатов: {ex.Message}");
            }
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (cmbCandidates.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите кандидата!");
                return;
            }

            var candidate = (Candidate)cmbCandidates.SelectedItem;

            try
            {
                // Проверка, не голосовал ли уже этот пользователь
                if (HasUserVoted(_voterId, _electionId))
                {
                    MessageBox.Show("Вы уже голосовали в этих выборах!");
                    return;
                }

                // Сохранение голоса
                SaveVote(_voterId, candidate.CandidateID, _electionId);

                MessageBox.Show("Ваш голос успешно сохранен!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении голоса: {ex.Message}");
            }
        }

        private bool HasUserVoted(int voterId, int electionId)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmd = new OleDbCommand(
                    "SELECT COUNT(*) FROM Votes " +
                    "WHERE VoterID = @VoterID AND ElectionID = @ElectionID",
                    conn);

                cmd.Parameters.AddWithValue("@VoterID", voterId);
                cmd.Parameters.AddWithValue("@ElectionID", electionId);
                conn.Open();

                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        private void SaveVote(int voterId, int candidateId, int electionId)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmd = new OleDbCommand(
                    "INSERT INTO Votes (VoterID, CandidateID, ElectionID, Timestamp, isVerified, VerificationNotes) " +
                    "VALUES (@VoterID, @CandidateID, @ElectionID, @Timestamp, @IsVerified, @Notes)",
                    conn);

                cmd.Parameters.AddWithValue("@VoterID", voterId);
                cmd.Parameters.AddWithValue("@CandidateID", candidateId);
                cmd.Parameters.AddWithValue("@ElectionID", electionId);
                cmd.Parameters.AddWithValue("@Timestamp", DateTime.Now);
                cmd.Parameters.AddWithValue("@IsVerified", false);
                cmd.Parameters.AddWithValue("@Notes", DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}