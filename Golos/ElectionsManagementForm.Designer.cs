using System.Drawing;
using System.Windows.Forms;

namespace Voiting
{
    partial class ElectionsManagementForm
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dgvElections;
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
            this.dgvElections = new System.Windows.Forms.DataGridView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvElections)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvElections
            // 
            this.dgvElections.ColumnHeadersHeight = 34;
            this.dgvElections.Location = new System.Drawing.Point(20, 20);
            this.dgvElections.Name = "dgvElections";
            this.dgvElections.RowHeadersWidth = 62;
            this.dgvElections.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvElections.Size = new System.Drawing.Size(600, 300);
            this.dgvElections.TabIndex = 0;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(20, 340);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 30);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(140, 340);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(100, 30);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "Изменить";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(260, 340);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 30);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Удалить";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // ElectionsManagementForm
            // 
            this.ClientSize = new System.Drawing.Size(640, 400);
            this.Controls.Add(this.dgvElections);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Name = "ElectionsManagementForm";
            this.Text = "Управление выборами";
            ((System.ComponentModel.ISupportInitialize)(this.dgvElections)).EndInit();
            this.ResumeLayout(false);

        }
    }
}