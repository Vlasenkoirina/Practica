using System.Reflection.Emit;
using System.Windows.Forms;


namespace Voiting
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblWelcome;
        private Button btnManageElections;
        private Button btnManageUsers;
        private Button btnVote;

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
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btnManageElections = new System.Windows.Forms.Button();
            this.btnManageUsers = new System.Windows.Forms.Button();
            this.btnVote = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lblWelcome
            this.lblWelcome.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.Location = new System.Drawing.Point(0, 0);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(800, 60);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblWelcome.BackColor = System.Drawing.Color.LightSteelBlue;

            // btnManageElections
            this.btnManageElections.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnManageElections.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnManageElections.ForeColor = System.Drawing.Color.White;
            this.btnManageElections.BackColor = System.Drawing.Color.SteelBlue;
            this.btnManageElections.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManageElections.FlatAppearance.BorderSize = 0;
            this.btnManageElections.Location = new System.Drawing.Point(0, 60);
            this.btnManageElections.Margin = new System.Windows.Forms.Padding(10);
            this.btnManageElections.Name = "btnManageElections";
            this.btnManageElections.Size = new System.Drawing.Size(800, 50);
            this.btnManageElections.TabIndex = 1;
            this.btnManageElections.Text = "🗳️ Управление выборами";

            // btnManageUsers
            this.btnManageUsers.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnManageUsers.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnManageUsers.ForeColor = System.Drawing.Color.White;
            this.btnManageUsers.BackColor = System.Drawing.Color.Teal;
            this.btnManageUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManageUsers.FlatAppearance.BorderSize = 0;
            this.btnManageUsers.Location = new System.Drawing.Point(0, 110);
            this.btnManageUsers.Margin = new System.Windows.Forms.Padding(10);
            this.btnManageUsers.Name = "btnManageUsers";
            this.btnManageUsers.Size = new System.Drawing.Size(800, 50);
            this.btnManageUsers.TabIndex = 2;
            this.btnManageUsers.Text = "👥 Управление пользователями";

            // btnVote
            this.btnVote.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnVote.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnVote.ForeColor = System.Drawing.Color.White;
            this.btnVote.BackColor = System.Drawing.Color.ForestGreen;
            this.btnVote.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVote.FlatAppearance.BorderSize = 0;
            this.btnVote.Location = new System.Drawing.Point(0, 160);
            this.btnVote.Margin = new System.Windows.Forms.Padding(10);
            this.btnVote.Name = "btnVote";
            this.btnVote.Size = new System.Drawing.Size(800, 60);
            this.btnVote.TabIndex = 3;
            this.btnVote.Text = "✅ ПРОГОЛОСОВАТЬ";

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.btnVote);
            this.Controls.Add(this.btnManageUsers);
            this.Controls.Add(this.btnManageElections);
            this.Controls.Add(this.lblWelcome);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Система голосования";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResumeLayout(false);
        }
    }
}