using System;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
using Voting;

namespace Voiting
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                // Тестовое подключение к базе
                TestDatabaseConnection();

                // Запуск формы авторизации
                Application.Run(new LoginForm());
            }
            catch (Exception ex)
            {
                ShowFatalError(ex);
            }
        }

        static void TestDatabaseConnection()
        {
            using (var connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Mi\Documents\Database312.mdb;Persist Security Info=False;"))
            {
                try
                {
                    connection.Open();
                    MessageBox.Show("Подключение к базе данных успешно!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка подключения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        static void ShowFatalError(Exception ex)
        {
            string errorDetails = $"Ошибка: {ex.Message}\n\n";

            if (ex.InnerException != null)
                errorDetails += $"InnerException: {ex.InnerException.Message}\n";

            errorDetails += "Проверьте:\n" +
                           "1. Файл базы данных (.accdb) существует и не заблокирован\n" +
                           "2. Установлен Microsoft Access Database Engine\n" +
                           "3. Строка подключения в App.config верна";

            MessageBox.Show(errorDetails, "Фатальная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
