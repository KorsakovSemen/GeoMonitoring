namespace SystAnalys_lr1.Forms
{
    partial class AddGrid
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
            this.up = new MetroFramework.Controls.MetroTextBox();
            this.left = new MetroFramework.Controls.MetroTextBox();
            this.button1 = new MetroFramework.Controls.MetroButton();
            this.down = new MetroFramework.Controls.MetroTextBox();
            this.right = new MetroFramework.Controls.MetroTextBox();
            this.label1 = new MetroFramework.Controls.MetroLabel();
            this.label2 = new MetroFramework.Controls.MetroLabel();
            this.label3 = new MetroFramework.Controls.MetroLabel();
            this.label4 = new MetroFramework.Controls.MetroLabel();
            this.label5 = new MetroFramework.Controls.MetroLabel();
            this.h = new MetroFramework.Controls.MetroTextBox();
            this.label6 = new MetroFramework.Controls.MetroLabel();
            this.w = new MetroFramework.Controls.MetroTextBox();
            this.SuspendLayout();
            // 
            // up
            // 
            this.up.Location = new System.Drawing.Point(148, 67);
            this.up.Name = "up";
            this.up.Size = new System.Drawing.Size(69, 20);
            this.up.TabIndex = 0;
            // 
            // left
            // 
            this.left.Location = new System.Drawing.Point(148, 103);
            this.left.Name = "left";
            this.left.Size = new System.Drawing.Size(69, 20);
            this.left.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(209, 195);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Создать";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // down
            // 
            this.down.Location = new System.Drawing.Point(278, 103);
            this.down.Name = "down";
            this.down.Size = new System.Drawing.Size(69, 20);
            this.down.TabIndex = 4;
            // 
            // right
            // 
            this.right.Location = new System.Drawing.Point(278, 67);
            this.right.Name = "right";
            this.right.Size = new System.Drawing.Size(69, 20);
            this.right.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(63, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Отступ сверху";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(66, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Отступ слева";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(362, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Отступ справа";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(362, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Отступ снизу";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(47, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Высота квадрата";
            // 
            // h
            // 
            this.h.Location = new System.Drawing.Point(278, 138);
            this.h.Name = "h";
            this.h.Size = new System.Drawing.Size(69, 20);
            this.h.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(362, 141);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Ширина квадрата";
            // 
            // w
            // 
            this.w.Location = new System.Drawing.Point(148, 138);
            this.w.Name = "w";
            this.w.Size = new System.Drawing.Size(69, 20);
            this.w.TabIndex = 11;
            // 
            // AddGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 281);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.w);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.h);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.down);
            this.Controls.Add(this.right);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.left);
            this.Controls.Add(this.up);
            this.Name = "AddGrid";
            this.Text = "AddGrid";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox up;
        private MetroFramework.Controls.MetroTextBox left;
        private MetroFramework.Controls.MetroButton button1;
        private MetroFramework.Controls.MetroTextBox down;
        private MetroFramework.Controls.MetroTextBox right;
        private MetroFramework.Controls.MetroLabel label1;
        private MetroFramework.Controls.MetroLabel label2;
        private MetroFramework.Controls.MetroLabel label3;
        private MetroFramework.Controls.MetroLabel label4;
        private MetroFramework.Controls.MetroLabel label5;
        private MetroFramework.Controls.MetroTextBox h;
        private MetroFramework.Controls.MetroLabel label6;
        private MetroFramework.Controls.MetroTextBox w;
    }
}