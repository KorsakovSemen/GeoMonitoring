namespace SystAnalys_lr1.Forms
{
    partial class TrafficLightSettings
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
            this.button1 = new MetroFramework.Controls.MetroButton();
            this.traficLightsOptions = new System.Windows.Forms.GroupBox();
            this.label1 = new MetroFramework.Controls.MetroLabel();
            this.textBox1 = new MetroFramework.Controls.MetroTextBox();
            this.customTrafficLights = new MetroFramework.Controls.MetroRadioButton();
            this.twoTrafficLights = new MetroFramework.Controls.MetroRadioButton();
            this.fourTrafficLights = new MetroFramework.Controls.MetroRadioButton();
            this.threeTrafficLights = new MetroFramework.Controls.MetroRadioButton();
            this.traficLightsOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(356, 372);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 98;
            this.button1.Text = "Закончить";
            this.button1.UseSelectable = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // traficLightsOptions
            // 
            this.traficLightsOptions.Controls.Add(this.label1);
            this.traficLightsOptions.Controls.Add(this.textBox1);
            this.traficLightsOptions.Controls.Add(this.customTrafficLights);
            this.traficLightsOptions.Controls.Add(this.twoTrafficLights);
            this.traficLightsOptions.Controls.Add(this.fourTrafficLights);
            this.traficLightsOptions.Controls.Add(this.threeTrafficLights);
            this.traficLightsOptions.Location = new System.Drawing.Point(245, 103);
            this.traficLightsOptions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.traficLightsOptions.Name = "traficLightsOptions";
            this.traficLightsOptions.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.traficLightsOptions.Size = new System.Drawing.Size(341, 186);
            this.traficLightsOptions.TabIndex = 99;
            this.traficLightsOptions.TabStop = false;
            this.traficLightsOptions.Text = "Опции светофоров";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(160, 148);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Количество светофоров";
            // 
            // textBox1
            // 
            // 
            // 
            // 
            this.textBox1.CustomButton.Image = null;
            this.textBox1.CustomButton.Location = new System.Drawing.Point(145, 1);
            this.textBox1.CustomButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox1.CustomButton.Name = "";
            this.textBox1.CustomButton.Size = new System.Drawing.Size(31, 28);
            this.textBox1.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.textBox1.CustomButton.TabIndex = 1;
            this.textBox1.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.textBox1.CustomButton.UseSelectable = true;
            this.textBox1.CustomButton.Visible = false;
            this.textBox1.Enabled = false;
            this.textBox1.Lines = new string[0];
            this.textBox1.Location = new System.Drawing.Point(20, 144);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox1.MaxLength = 32767;
            this.textBox1.Name = "textBox1";
            this.textBox1.PasswordChar = '\0';
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textBox1.SelectedText = "";
            this.textBox1.SelectionLength = 0;
            this.textBox1.SelectionStart = 0;
            this.textBox1.ShortcutsEnabled = true;
            this.textBox1.Size = new System.Drawing.Size(133, 25);
            this.textBox1.TabIndex = 4;
            this.textBox1.UseSelectable = true;
            this.textBox1.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.textBox1.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // customTrafficLights
            // 
            this.customTrafficLights.AutoSize = true;
            this.customTrafficLights.Location = new System.Drawing.Point(20, 114);
            this.customTrafficLights.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.customTrafficLights.Name = "customTrafficLights";
            this.customTrafficLights.Size = new System.Drawing.Size(113, 17);
            this.customTrafficLights.TabIndex = 3;
            this.customTrafficLights.TabStop = true;
            this.customTrafficLights.Text = "Свое значение";
            this.customTrafficLights.UseSelectable = true;
            this.customTrafficLights.CheckedChanged += new System.EventHandler(this.customTrafficLights_CheckedChanged);
            // 
            // twoTrafficLights
            // 
            this.twoTrafficLights.AutoSize = true;
            this.twoTrafficLights.Location = new System.Drawing.Point(20, 28);
            this.twoTrafficLights.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.twoTrafficLights.Name = "twoTrafficLights";
            this.twoTrafficLights.Size = new System.Drawing.Size(100, 17);
            this.twoTrafficLights.TabIndex = 2;
            this.twoTrafficLights.TabStop = true;
            this.twoTrafficLights.Text = "2 светофора";
            this.twoTrafficLights.UseSelectable = true;
            this.twoTrafficLights.CheckedChanged += new System.EventHandler(this.twoTrafficLights_CheckedChanged);
            // 
            // fourTrafficLights
            // 
            this.fourTrafficLights.AutoSize = true;
            this.fourTrafficLights.Location = new System.Drawing.Point(20, 85);
            this.fourTrafficLights.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.fourTrafficLights.Name = "fourTrafficLights";
            this.fourTrafficLights.Size = new System.Drawing.Size(244, 17);
            this.fourTrafficLights.TabIndex = 1;
            this.fourTrafficLights.TabStop = true;
            this.fourTrafficLights.Text = "Обычный перекресток(4 светофора)";
            this.fourTrafficLights.UseSelectable = true;
            this.fourTrafficLights.CheckedChanged += new System.EventHandler(this.fourTrafficLights_CheckedChanged);
            // 
            // threeTrafficLights
            // 
            this.threeTrafficLights.AutoSize = true;
            this.threeTrafficLights.Location = new System.Drawing.Point(20, 57);
            this.threeTrafficLights.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.threeTrafficLights.Name = "threeTrafficLights";
            this.threeTrafficLights.Size = new System.Drawing.Size(263, 17);
            this.threeTrafficLights.TabIndex = 0;
            this.threeTrafficLights.TabStop = true;
            this.threeTrafficLights.Text = "Т-образный перекресток (3 светофора)";
            this.threeTrafficLights.UseSelectable = true;
            this.threeTrafficLights.CheckedChanged += new System.EventHandler(this.threeTrafficLights_CheckedChanged);
            // 
            // TrafficLightSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 464);
            this.Controls.Add(this.traficLightsOptions);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "TrafficLightSettings";
            this.Padding = new System.Windows.Forms.Padding(27, 74, 27, 25);
            this.Text = "TrafficLightSettings";
            this.traficLightsOptions.ResumeLayout(false);
            this.traficLightsOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroButton button1;
        private System.Windows.Forms.GroupBox traficLightsOptions;
        private MetroFramework.Controls.MetroRadioButton fourTrafficLights;
        private MetroFramework.Controls.MetroRadioButton threeTrafficLights;
        private MetroFramework.Controls.MetroRadioButton twoTrafficLights;
        private MetroFramework.Controls.MetroLabel label1;
        private MetroFramework.Controls.MetroTextBox textBox1;
        private MetroFramework.Controls.MetroRadioButton customTrafficLights;
    }
}