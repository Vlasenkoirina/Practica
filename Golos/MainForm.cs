using System;
using System.Windows.Forms;

namespace Voiting
{
    public partial class MainForm : Form
    {
        private readonly int _userId;
        private readonly string _fullName;
        private readonly string _role;

        public MainForm(int userId, string fullName, string role)
        {
            InitializeComponent();
            _userId = userId;
            _fullName = fullName;
            _role = role;

            ConfigureUI();
            SetupEventHandlers();
        }

        private void ConfigureUI()
        {
            Text = $"Система голосования | {_fullName} ({_role})";
            lblWelcome.Text = $"Добро пожаловать, {_fullName}!";

            // Настройка видимости кнопок по роли
            btnManageElections.Visible = (_role == "Admin");
            btnManageUsers.Visible = (_role == "Admin");
            btnVote.Visible = (_role == "User");
        }

        private void SetupEventHandlers()
        {
            btnManageElections.Click += BtnManageElections_Click;
            btnManageUsers.Click += BtnManageUsers_Click;
            btnVote.Click += BtnVote_Click;
        }

        private void BtnManageElections_Click(object sender, EventArgs e)
        {
            try
            {
                // Передаем currentUserId в конструктор
                using (var electionsForm = new ElectionsManagementForm(_userId))
                {
                    electionsForm.StartPosition = FormStartPosition.CenterParent;
                    electionsForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка открытия формы: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnManageUsers_Click(object sender, EventArgs e)
        {
            var usersForm = new UsersManagementForm();
            usersForm.ShowDialog();
        }

        private void BtnVote_Click(object sender, EventArgs e)
        {
            // Предположим, что у вас есть способ получить electionId
            int currentElectionId = GetCurrentElectionId();
            var votingForm = new VotingForm(_userId, currentElectionId);
            votingForm.ShowDialog();
        }

        private int GetCurrentElectionId()
        {
            // Реализуйте логику получения ID текущих выборов
            // Например, можно сделать запрос к БД или использовать статическое значение
            return 1; // Временная заглушка
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите выйти из системы?",
                "Подтверждение выхода",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}