namespace Voiting
{
    partial class VotingForm
    {
        private System.ComponentModel.IContainer components = null;

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
            this.cmbCandidates = new System.Windows.Forms.ComboBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.lblInstruction = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // cmbCandidates
            this.cmbCandidates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCandidates.FormattingEnabled = true;
            this.cmbCandidates.Location = new System.Drawing.Point(20, 50);
            this.cmbCandidates.Name = "cmbCandidates";
            this.cmbCandidates.Size = new System.Drawing.Size(250, 28);
            this.cmbCandidates.TabIndex = 1;
            this.cmbCandidates.DisplayMember = "FullName";

            // btnSubmit
            this.btnSubmit.Location = new System.Drawing.Point(20, 90);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(250, 48);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "Проголосовать";
            this.btnSubmit.Click += new System.EventHandler(this.BtnSubmit_Click);

            // lblInstruction
            this.lblInstruction.AutoSize = true;
            this.lblInstruction.Location = new System.Drawing.Point(20, 20);
            this.lblInstruction.Name = "lblInstruction";
            this.lblInstruction.Size = new System.Drawing.Size(180, 20);
            this.lblInstruction.TabIndex = 0;
            this.lblInstruction.Text = "Выберите кандидата:";

            // VotingForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 150);
            this.Controls.Add(this.lblInstruction);
            this.Controls.Add(this.cmbCandidates);
            this.Controls.Add(this.btnSubmit);
            this.Name = "VotingForm";
            this.Text = "Голосование";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ComboBox cmbCandidates;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label lblInstruction;
    }
}