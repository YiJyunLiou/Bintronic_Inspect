namespace Bintronic_Inspect
{
    partial class formCountdownSetting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rdbCloseCountdown = new System.Windows.Forms.RadioButton();
            this.rdbOpenCountdown = new System.Windows.Forms.RadioButton();
            this.lblCountdownS = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.nbudCountdownSeconds = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nbudCountdownSeconds)).BeginInit();
            this.SuspendLayout();
            // 
            // rdbCloseCountdown
            // 
            this.rdbCloseCountdown.AutoSize = true;
            this.rdbCloseCountdown.Font = new System.Drawing.Font("微軟正黑體", 18F);
            this.rdbCloseCountdown.Location = new System.Drawing.Point(64, 38);
            this.rdbCloseCountdown.Name = "rdbCloseCountdown";
            this.rdbCloseCountdown.Size = new System.Drawing.Size(151, 34);
            this.rdbCloseCountdown.TabIndex = 0;
            this.rdbCloseCountdown.TabStop = true;
            this.rdbCloseCountdown.Text = "不啟用倒數";
            this.rdbCloseCountdown.UseVisualStyleBackColor = true;
            // 
            // rdbOpenCountdown
            // 
            this.rdbOpenCountdown.AutoSize = true;
            this.rdbOpenCountdown.Font = new System.Drawing.Font("微軟正黑體", 18F);
            this.rdbOpenCountdown.Location = new System.Drawing.Point(64, 94);
            this.rdbOpenCountdown.Name = "rdbOpenCountdown";
            this.rdbOpenCountdown.Size = new System.Drawing.Size(79, 34);
            this.rdbOpenCountdown.TabIndex = 1;
            this.rdbOpenCountdown.TabStop = true;
            this.rdbOpenCountdown.Text = "倒數";
            this.rdbOpenCountdown.UseVisualStyleBackColor = true;
            // 
            // lblCountdownS
            // 
            this.lblCountdownS.AutoSize = true;
            this.lblCountdownS.Font = new System.Drawing.Font("微軟正黑體", 18F);
            this.lblCountdownS.Location = new System.Drawing.Point(256, 98);
            this.lblCountdownS.Name = "lblCountdownS";
            this.lblCountdownS.Size = new System.Drawing.Size(37, 30);
            this.lblCountdownS.TabIndex = 3;
            this.lblCountdownS.Text = "秒";
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackColor = System.Drawing.Color.Green;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnConfirm.ForeColor = System.Drawing.Color.White;
            this.btnConfirm.Location = new System.Drawing.Point(197, 162);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(142, 39);
            this.btnConfirm.TabIndex = 4;
            this.btnConfirm.Text = "確認";
            this.btnConfirm.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(60)))), ((int)(((byte)(0)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(33, 162);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(142, 39);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // nbudCountdownSeconds
            // 
            this.nbudCountdownSeconds.Font = new System.Drawing.Font("微軟正黑體", 18F);
            this.nbudCountdownSeconds.Location = new System.Drawing.Point(149, 89);
            this.nbudCountdownSeconds.Name = "nbudCountdownSeconds";
            this.nbudCountdownSeconds.Size = new System.Drawing.Size(100, 39);
            this.nbudCountdownSeconds.TabIndex = 6;
            // 
            // formCountdownSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 228);
            this.Controls.Add(this.nbudCountdownSeconds);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.lblCountdownS);
            this.Controls.Add(this.rdbOpenCountdown);
            this.Controls.Add(this.rdbCloseCountdown);
            this.Name = "formCountdownSetting";
            this.Text = "倒數設定";
            ((System.ComponentModel.ISupportInitialize)(this.nbudCountdownSeconds)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdbCloseCountdown;
        private System.Windows.Forms.RadioButton rdbOpenCountdown;
        private System.Windows.Forms.Label lblCountdownS;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.NumericUpDown nbudCountdownSeconds;
    }
}