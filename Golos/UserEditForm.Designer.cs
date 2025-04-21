using System.Windows.Forms;
using System;
using System.Drawing;

namespace Voiting
{
    partial class UserEditForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblLogin;
        private TextBox txtLogin;
        private Label lblPassword;
        private TextBox txtPassword;
        private Label lblRole;
        private ComboBox cmbRole;
        private Label lblFullName;
        private TextBox txtFullName;
        private Label lblContact;
        private TextBox txtContact;
        private CheckBox chkIsActive;
        private Button btnSave;
        private Button btnCancel;

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
            this.lblLogin = new Label();
            this.txtLogin = new TextBox();
            this.lblPassword = new Label();
            this.txtPassword = new TextBox();
            this.lblRole = new Label();
            this.cmbRole = new ComboBox();
            this.lblFullName = new Label();
            this.txtFullName = new TextBox();
            this.lblContact = new Label();
            this.txtContact = new TextBox();
            this.chkIsActive = new CheckBox();
            this.btnSave = new Button();
            this.btnCancel = new Button();
            this.SuspendLayout();

            // lblLogin
            this.lblLogin.AutoSize = true;
            this.lblLogin.Location = new System.Drawing.Point(20, 20);
            this.lblLogin.Text = "Логин:";

            // txtLogin
            this.txtLogin.Location = new System.Drawing.Point(120, 17);
            this.txtLogin.Size = new System.Drawing.Size(200, 20);

            // lblPassword
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(20, 50);
            this.lblPassword.Text = "Пароль:";

            // txtPassword
            this.txtPassword.Location = new System.Drawing.Point(120, 47);
            this.txtPassword.Size = new System.Drawing.Size(200, 20);

            // lblRole
            this.lblRole.AutoSize = true;
            this.lblRole.Location = new System.Drawing.Point(20, 80);
            this.lblRole.Text = "Роль:";

            // cmbRole
            this.cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbRole.FormattingEnabled = true;
            this.cmbRole.Location = new System.Drawing.Point(120, 77);
            this.cmbRole.Size = new System.Drawing.Size(200, 21);

            // lblFullName
            this.lblFullName.AutoSize = true;
            this.lblFullName.Location = new System.Drawing.Point(20, 110);
            this.lblFullName.Text = "ФИО:";

            // txtFullName
            this.txtFullName.Location = new System.Drawing.Point(120, 107);
            this.txtFullName.Size = new System.Drawing.Size(200, 20);

            // lblContact
            this.lblContact.AutoSize = true;
            this.lblContact.Location = new System.Drawing.Point(20, 140);
            this.lblContact.Text = "Контакты:";

            // txtContact
            this.txtContact.Location = new System.Drawing.Point(120, 137);
            this.txtContact.Size = new System.Drawing.Size(200, 20);

            // chkIsActive
            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Location = new System.Drawing.Point(120, 167);
            this.chkIsActive.Text = "Активен";
            this.chkIsActive.Checked = true;

            // btnSave
            this.btnSave.Location = new System.Drawing.Point(120, 200);
            this.btnSave.Size = new System.Drawing.Size(90, 30);
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new EventHandler(this.BtnSave_Click);

            // btnCancel
            this.btnCancel.Location = new System.Drawing.Point(230, 200);
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new EventHandler(this.BtnCancel_Click);

            // UserEditForm
            this.ClientSize = new System.Drawing.Size(350, 250);
            this.Controls.AddRange(new Control[] {
                lblLogin, txtLogin,
                lblPassword, txtPassword,
                lblRole, cmbRole,
                lblFullName, txtFullName,
                lblContact, txtContact,
                chkIsActive,
                btnSave, btnCancel
            });
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ResumeLayout(true);
            this.PerformLayout();
        }
    }
}