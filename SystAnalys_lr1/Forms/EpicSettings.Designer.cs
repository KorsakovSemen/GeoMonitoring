namespace SystAnalys_lr1.Forms
{
    partial class EpicSettings
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new MetroFramework.Controls.MetroTextBox();
            this.radioCustom = new MetroFramework.Controls.MetroRadioButton();
            this.radioEpicRandom = new MetroFramework.Controls.MetroRadioButton();
            this.label12 = new MetroFramework.Controls.MetroLabel();
            this.radioEpicBig = new MetroFramework.Controls.MetroRadioButton();
            this.radioEpicMedium = new MetroFramework.Controls.MetroRadioButton();
            this.radioEpicSmall = new MetroFramework.Controls.MetroRadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new MetroFramework.Controls.MetroLabel();
            this.left = new MetroFramework.Controls.MetroCheckBox();
            this.right = new MetroFramework.Controls.MetroCheckBox();
            this.down = new MetroFramework.Controls.MetroCheckBox();
            this.up = new MetroFramework.Controls.MetroCheckBox();
            this.button1 = new MetroFramework.Controls.MetroButton();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.radioCustom);
            this.groupBox2.Controls.Add(this.radioEpicRandom);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.radioEpicBig);
            this.groupBox2.Controls.Add(this.radioEpicMedium);
            this.groupBox2.Controls.Add(this.radioEpicSmall);
            this.groupBox2.Location = new System.Drawing.Point(70, 78);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(265, 138);
            this.groupBox2.TabIndex = 93;
            this.groupBox2.TabStop = false;
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // textBox1
            // 
            // 
            // 
            // 
            this.textBox1.CustomButton.Image = null;
            this.textBox1.CustomButton.Location = new System.Drawing.Point(62, 2);
            this.textBox1.CustomButton.Name = "";
            this.textBox1.CustomButton.Size = new System.Drawing.Size(11, 12);
            this.textBox1.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.textBox1.CustomButton.TabIndex = 1;
            this.textBox1.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.textBox1.CustomButton.UseSelectable = true;
            this.textBox1.CustomButton.Visible = false;
            this.textBox1.Enabled = false;
            this.textBox1.Lines = new string[0];
            this.textBox1.Location = new System.Drawing.Point(126, 54);
            this.textBox1.MaxLength = 9;
            this.textBox1.Name = "textBox1";
            this.textBox1.PasswordChar = '\0';
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textBox1.SelectedText = "";
            this.textBox1.SelectionLength = 0;
            this.textBox1.SelectionStart = 0;
            this.textBox1.ShortcutsEnabled = true;
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 6;
            this.textBox1.UseSelectable = true;
            this.textBox1.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.textBox1.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // radioCustom
            // 
            this.radioCustom.AutoSize = true;
            this.radioCustom.Location = new System.Drawing.Point(126, 31);
            this.radioCustom.Name = "radioCustom";
            this.radioCustom.Size = new System.Drawing.Size(107, 15);
            this.radioCustom.TabIndex = 5;
            this.radioCustom.TabStop = true;
            this.radioCustom.Text = "Свой параметр";
            this.radioCustom.UseSelectable = true;
            this.radioCustom.CheckedChanged += new System.EventHandler(this.radioCustom_CheckedChanged);
            // 
            // radioEpicRandom
            // 
            this.radioEpicRandom.AutoSize = true;
            this.radioEpicRandom.Location = new System.Drawing.Point(6, 100);
            this.radioEpicRandom.Name = "radioEpicRandom";
            this.radioEpicRandom.Size = new System.Drawing.Size(87, 15);
            this.radioEpicRandom.TabIndex = 4;
            this.radioEpicRandom.Text = "Случайный";
            this.radioEpicRandom.UseSelectable = true;
            this.radioEpicRandom.CheckedChanged += new System.EventHandler(this.radioEpicRandom_CheckedChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(55, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(123, 19);
            this.label12.TabIndex = 3;
            this.label12.Text = "Размер эпицентра";
            // 
            // radioEpicBig
            // 
            this.radioEpicBig.AutoSize = true;
            this.radioEpicBig.Location = new System.Drawing.Point(6, 77);
            this.radioEpicBig.Name = "radioEpicBig";
            this.radioEpicBig.Size = new System.Drawing.Size(75, 15);
            this.radioEpicBig.TabIndex = 2;
            this.radioEpicBig.Text = "Большой";
            this.radioEpicBig.UseSelectable = true;
            this.radioEpicBig.CheckedChanged += new System.EventHandler(this.radioEpicBig_CheckedChanged);
            // 
            // radioEpicMedium
            // 
            this.radioEpicMedium.AutoSize = true;
            this.radioEpicMedium.Checked = true;
            this.radioEpicMedium.Location = new System.Drawing.Point(6, 54);
            this.radioEpicMedium.Name = "radioEpicMedium";
            this.radioEpicMedium.Size = new System.Drawing.Size(71, 15);
            this.radioEpicMedium.TabIndex = 1;
            this.radioEpicMedium.TabStop = true;
            this.radioEpicMedium.Text = "Средний";
            this.radioEpicMedium.UseSelectable = true;
            this.radioEpicMedium.CheckedChanged += new System.EventHandler(this.radioEpicMedium_CheckedChanged);
            // 
            // radioEpicSmall
            // 
            this.radioEpicSmall.AutoSize = true;
            this.radioEpicSmall.Location = new System.Drawing.Point(6, 31);
            this.radioEpicSmall.Name = "radioEpicSmall";
            this.radioEpicSmall.Size = new System.Drawing.Size(86, 15);
            this.radioEpicSmall.TabIndex = 0;
            this.radioEpicSmall.Text = "Маленький";
            this.radioEpicSmall.UseSelectable = true;
            this.radioEpicSmall.CheckedChanged += new System.EventHandler(this.radioEpicSmall_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.left);
            this.groupBox1.Controls.Add(this.right);
            this.groupBox1.Controls.Add(this.down);
            this.groupBox1.Controls.Add(this.up);
            this.groupBox1.Location = new System.Drawing.Point(70, 222);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(265, 128);
            this.groupBox1.TabIndex = 94;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 19);
            this.label1.TabIndex = 4;
            this.label1.Text = "Направление расширения";
            // 
            // left
            // 
            this.left.AutoSize = true;
            this.left.Location = new System.Drawing.Point(16, 105);
            this.left.Name = "left";
            this.left.Size = new System.Drawing.Size(56, 15);
            this.left.TabIndex = 3;
            this.left.Text = "Влево";
            this.left.UseSelectable = true;
            // 
            // right
            // 
            this.right.AutoSize = true;
            this.right.Location = new System.Drawing.Point(16, 82);
            this.right.Name = "right";
            this.right.Size = new System.Drawing.Size(63, 15);
            this.right.TabIndex = 2;
            this.right.Text = "Вправо";
            this.right.UseSelectable = true;
            // 
            // down
            // 
            this.down.AutoSize = true;
            this.down.Location = new System.Drawing.Point(16, 59);
            this.down.Name = "down";
            this.down.Size = new System.Drawing.Size(49, 15);
            this.down.TabIndex = 1;
            this.down.Text = "Вниз";
            this.down.UseSelectable = true;
            // 
            // up
            // 
            this.up.AutoSize = true;
            this.up.Location = new System.Drawing.Point(16, 36);
            this.up.Name = "up";
            this.up.Size = new System.Drawing.Size(54, 15);
            this.up.TabIndex = 0;
            this.up.Text = "Вверх";
            this.up.UseSelectable = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(173, 356);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 95;
            this.button1.Text = "Set";
            this.button1.UseSelectable = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // EpicSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 423);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(402, 423);
            this.MinimizeBox = false;
            this.Name = "EpicSettings";
            this.Text = "Epic settings";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private MetroFramework.Controls.MetroTextBox textBox1;
        private MetroFramework.Controls.MetroRadioButton radioCustom;
        private MetroFramework.Controls.MetroRadioButton radioEpicRandom;
        private MetroFramework.Controls.MetroLabel label12;
        private MetroFramework.Controls.MetroRadioButton radioEpicBig;
        private MetroFramework.Controls.MetroRadioButton radioEpicMedium;
        private MetroFramework.Controls.MetroRadioButton radioEpicSmall;
        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroLabel label1;
        private MetroFramework.Controls.MetroCheckBox left;
        private MetroFramework.Controls.MetroCheckBox right;
        private MetroFramework.Controls.MetroCheckBox down;
        private MetroFramework.Controls.MetroCheckBox up;
        private MetroFramework.Controls.MetroButton button1;
    }
}