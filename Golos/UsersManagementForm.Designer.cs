using System.Windows.Forms;

namespace Voiting
{
    partial class UsersManagementForm
    {
        private System.ComponentModel.IContainer components = null;

        private DataGridView dataGridView1;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();

            // dataGridView1
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode =
                System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(20, 20);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(750, 300);

            // btnAdd
            this.btnAdd.Location = new System.Drawing.Point(20, 340);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 30);
            this.btnAdd.Text = "Добавить";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // btnEdit
            this.btnEdit.Location = new System.Drawing.Point(140, 340);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(100, 30);
            this.btnEdit.Text = "Изменить";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);

            // btnDelete
            this.btnDelete.Location = new System.Drawing.Point(260, 340);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 30);
            this.btnDelete.Text = "Удалить";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // UsersManagementForm
            this.ClientSize = new System.Drawing.Size(800, 400);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Name = "UsersManagementForm";
            this.Text = "Управление пользователями";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
        }

            private void ConfigureDataGridViewColumns()
        {
            dataGridView1.Columns.Clear();

            // Колонка ID
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "colUserID",
                DataPropertyName = "UserID",
                HeaderText = "ID",
                Width = 50,
                ReadOnly = true
            });

            // Колонка Логин
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "colLogin",
                DataPropertyName = "Login",
                HeaderText = "Логин",
                Width = 120
            });

            // Колонка Роль
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "colRole",
                DataPropertyName = "Role",
                HeaderText = "Роль",
                Width = 80
            });

            // Колонка ФИО
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "colFullName",
                DataPropertyName = "FullName",
                HeaderText = "ФИО",
                Width = 180
            });

            // Колонка Контакты
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "colContact",
                DataPropertyName = "ContactInfo",
                HeaderText = "Контакты",
                Width = 150
            });

            // Колонка Активен (чекбокс)
            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                Name = "colIsActive",
                DataPropertyName = "isActive",
                HeaderText = "Активен",
                Width = 60
            });

            dataGridView1.AutoGenerateColumns = false;
        }
    }
    }
