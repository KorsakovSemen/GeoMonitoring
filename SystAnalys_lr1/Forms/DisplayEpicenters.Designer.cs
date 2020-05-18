namespace SystAnalys_lr1
{
    partial class DisplayEpicenters
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisplayEpicenters));
            this.ModelingTimer = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new MetroFramework.Controls.MetroPanel();
            this.metroButton2 = new MetroFramework.Controls.MetroButton();
            this.EZoomBar = new MetroFramework.Controls.MetroTrackBar();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.button11 = new MetroFramework.Controls.MetroButton();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.ERouts = new MetroFramework.Controls.MetroComboBox();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.label2 = new MetroFramework.Controls.MetroLabel();
            this.MapPanel = new MetroFramework.Controls.MetroPanel();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.panel1.SuspendLayout();
            this.metroPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ModelingTimer
            // 
            this.ModelingTimer.Interval = 1000;
            this.ModelingTimer.Tick += new System.EventHandler(this.ModelingTimer_Tick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.metroButton2);
            this.panel1.Controls.Add(this.EZoomBar);
            this.panel1.Controls.Add(this.metroLabel2);
            this.panel1.Controls.Add(this.button11);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.HorizontalScrollbarBarColor = true;
            this.panel1.HorizontalScrollbarHighlightOnWheel = false;
            this.panel1.HorizontalScrollbarSize = 6;
            this.panel1.Name = "panel1";
            this.panel1.VerticalScrollbarBarColor = true;
            this.panel1.VerticalScrollbarHighlightOnWheel = false;
            this.panel1.VerticalScrollbarSize = 6;
            // 
            // metroButton2
            // 
            resources.ApplyResources(this.metroButton2, "metroButton2");
            this.metroButton2.Name = "metroButton2";
            this.metroButton2.UseSelectable = true;
            this.metroButton2.Click += new System.EventHandler(this.metroButton2_Click);
            // 
            // EZoomBar
            // 
            this.EZoomBar.BackColor = System.Drawing.Color.Transparent;
            this.EZoomBar.LargeChange = 1;
            resources.ApplyResources(this.EZoomBar, "EZoomBar");
            this.EZoomBar.Maximum = 3;
            this.EZoomBar.Minimum = 1;
            this.EZoomBar.Name = "EZoomBar";
            this.EZoomBar.Value = 1;
            this.EZoomBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.EZoomBar_Scroll);
            // 
            // metroLabel2
            // 
            resources.ApplyResources(this.metroLabel2, "metroLabel2");
            this.metroLabel2.Name = "metroLabel2";
            // 
            // button11
            // 
            resources.ApplyResources(this.button11, "button11");
            this.button11.Name = "button11";
            this.button11.UseSelectable = true;
            this.button11.Click += new System.EventHandler(this.Button11_Click);
            // 
            // metroLabel1
            // 
            resources.ApplyResources(this.metroLabel1, "metroLabel1");
            this.metroLabel1.Name = "metroLabel1";
            // 
            // ERouts
            // 
            this.ERouts.FormattingEnabled = true;
            resources.ApplyResources(this.ERouts, "ERouts");
            this.ERouts.Name = "ERouts";
            this.ERouts.UseSelectable = true;
            // 
            // metroButton1
            // 
            resources.ApplyResources(this.metroButton1, "metroButton1");
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.UseSelectable = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // MapPanel
            // 
            this.MapPanel.HorizontalScrollbarBarColor = true;
            this.MapPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.MapPanel.HorizontalScrollbarSize = 10;
            resources.ApplyResources(this.MapPanel, "MapPanel");
            this.MapPanel.Name = "MapPanel";
            this.MapPanel.VerticalScrollbarBarColor = true;
            this.MapPanel.VerticalScrollbarHighlightOnWheel = false;
            this.MapPanel.VerticalScrollbarSize = 10;
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.label2);
            this.metroPanel1.Controls.Add(this.metroButton1);
            this.metroPanel1.Controls.Add(this.ERouts);
            this.metroPanel1.Controls.Add(this.metroLabel1);
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            resources.ApplyResources(this.metroPanel1, "metroPanel1");
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // DisplayEpicenters
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.metroPanel1);
            this.Controls.Add(this.MapPanel);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "DisplayEpicenters";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DisplayEpicenters_FormClosed);
            this.Load += new System.EventHandler(this.DisplayEpicenters_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer ModelingTimer;
        private MetroFramework.Controls.MetroPanel panel1;
        private MetroFramework.Controls.MetroButton metroButton1;
        private MetroFramework.Controls.MetroButton button11;
        private MetroFramework.Controls.MetroLabel label2;
        private MetroFramework.Controls.MetroComboBox ERouts;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroTrackBar EZoomBar;
        private MetroFramework.Controls.MetroPanel MapPanel;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroButton metroButton2;
    }
}