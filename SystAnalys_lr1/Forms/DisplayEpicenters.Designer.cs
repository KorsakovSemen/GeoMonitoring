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
            this.SimulatingTimer = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new MetroFramework.Controls.MetroPanel();
            this.metroPanel4 = new MetroFramework.Controls.MetroPanel();
            this.SettingsButton = new MetroFramework.Controls.MetroButton();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.ParametresLabel = new MetroFramework.Controls.MetroLabel();
            this.metroPanel3 = new MetroFramework.Controls.MetroPanel();
            this.ShowResultLabel = new MetroFramework.Controls.MetroLabel();
            this.TimePastLabel = new MetroFramework.Controls.MetroLabel();
            this.ShowOriginalButton = new MetroFramework.Controls.MetroButton();
            this.AfterButton = new MetroFramework.Controls.MetroButton();
            this.metroPanel2 = new MetroFramework.Controls.MetroPanel();
            this.StartStopLabel = new MetroFramework.Controls.MetroLabel();
            this.SimulationLaabel = new MetroFramework.Controls.MetroLabel();
            this.SimulatingTimerButton = new MetroFramework.Controls.MetroButton();
            this.EZoomBar = new MetroFramework.Controls.MetroTrackBar();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.ERouts = new MetroFramework.Controls.MetroComboBox();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.label2 = new MetroFramework.Controls.MetroLabel();
            this.MapPanel = new MetroFramework.Controls.MetroPanel();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.metroPanel5 = new MetroFramework.Controls.MetroPanel();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.metroPanel4.SuspendLayout();
            this.metroPanel3.SuspendLayout();
            this.metroPanel2.SuspendLayout();
            this.metroPanel1.SuspendLayout();
            this.metroPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // SimulatingTimer
            // 
            this.SimulatingTimer.Interval = 1000;
            this.SimulatingTimer.Tick += new System.EventHandler(this.ModelingTimer_Tick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.metroPanel4);
            this.panel1.Controls.Add(this.metroPanel3);
            this.panel1.Controls.Add(this.metroPanel2);
            this.panel1.Controls.Add(this.EZoomBar);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.HorizontalScrollbarBarColor = true;
            this.panel1.HorizontalScrollbarHighlightOnWheel = false;
            this.panel1.HorizontalScrollbarSize = 6;
            this.panel1.Name = "panel1";
            this.panel1.VerticalScrollbarBarColor = true;
            this.panel1.VerticalScrollbarHighlightOnWheel = false;
            this.panel1.VerticalScrollbarSize = 6;
            // 
            // metroPanel4
            // 
            this.metroPanel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroPanel4.Controls.Add(this.SettingsButton);
            this.metroPanel4.Controls.Add(this.metroLabel2);
            this.metroPanel4.Controls.Add(this.ParametresLabel);
            this.metroPanel4.HorizontalScrollbarBarColor = true;
            this.metroPanel4.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel4.HorizontalScrollbarSize = 10;
            resources.ApplyResources(this.metroPanel4, "metroPanel4");
            this.metroPanel4.Name = "metroPanel4";
            this.metroPanel4.VerticalScrollbarBarColor = true;
            this.metroPanel4.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel4.VerticalScrollbarSize = 10;
            // 
            // SettingsButton
            // 
            resources.ApplyResources(this.SettingsButton, "SettingsButton");
            this.SettingsButton.Name = "SettingsButton";
            this.SettingsButton.UseSelectable = true;
            this.SettingsButton.Click += new System.EventHandler(this.Button11_Click);
            // 
            // metroLabel2
            // 
            resources.ApplyResources(this.metroLabel2, "metroLabel2");
            this.metroLabel2.Name = "metroLabel2";
            // 
            // ParametresLabel
            // 
            resources.ApplyResources(this.ParametresLabel, "ParametresLabel");
            this.ParametresLabel.Name = "ParametresLabel";
            // 
            // metroPanel3
            // 
            this.metroPanel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroPanel3.Controls.Add(this.ShowResultLabel);
            this.metroPanel3.Controls.Add(this.TimePastLabel);
            this.metroPanel3.Controls.Add(this.ShowOriginalButton);
            this.metroPanel3.Controls.Add(this.AfterButton);
            this.metroPanel3.HorizontalScrollbarBarColor = true;
            this.metroPanel3.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel3.HorizontalScrollbarSize = 10;
            resources.ApplyResources(this.metroPanel3, "metroPanel3");
            this.metroPanel3.Name = "metroPanel3";
            this.metroPanel3.VerticalScrollbarBarColor = true;
            this.metroPanel3.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel3.VerticalScrollbarSize = 10;
            // 
            // ShowResultLabel
            // 
            resources.ApplyResources(this.ShowResultLabel, "ShowResultLabel");
            this.ShowResultLabel.Name = "ShowResultLabel";
            // 
            // TimePastLabel
            // 
            resources.ApplyResources(this.TimePastLabel, "TimePastLabel");
            this.TimePastLabel.Name = "TimePastLabel";
            // 
            // ShowOriginalButton
            // 
            resources.ApplyResources(this.ShowOriginalButton, "ShowOriginalButton");
            this.ShowOriginalButton.Name = "ShowOriginalButton";
            this.ShowOriginalButton.UseSelectable = true;
            this.ShowOriginalButton.Click += new System.EventHandler(this.ShowOriginalButton_Click);
            // 
            // AfterButton
            // 
            resources.ApplyResources(this.AfterButton, "AfterButton");
            this.AfterButton.Name = "AfterButton";
            this.AfterButton.UseSelectable = true;
            this.AfterButton.Click += new System.EventHandler(this.metroButton4_Click);
            // 
            // metroPanel2
            // 
            this.metroPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroPanel2.Controls.Add(this.StartStopLabel);
            this.metroPanel2.Controls.Add(this.SimulationLaabel);
            this.metroPanel2.Controls.Add(this.SimulatingTimerButton);
            this.metroPanel2.HorizontalScrollbarBarColor = true;
            this.metroPanel2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel2.HorizontalScrollbarSize = 10;
            resources.ApplyResources(this.metroPanel2, "metroPanel2");
            this.metroPanel2.Name = "metroPanel2";
            this.metroPanel2.VerticalScrollbarBarColor = true;
            this.metroPanel2.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel2.VerticalScrollbarSize = 10;
            // 
            // StartStopLabel
            // 
            resources.ApplyResources(this.StartStopLabel, "StartStopLabel");
            this.StartStopLabel.Name = "StartStopLabel";
            // 
            // SimulationLaabel
            // 
            resources.ApplyResources(this.SimulationLaabel, "SimulationLaabel");
            this.SimulationLaabel.Name = "SimulationLaabel";
            // 
            // SimulatingTimerButton
            // 
            resources.ApplyResources(this.SimulatingTimerButton, "SimulatingTimerButton");
            this.SimulatingTimerButton.Name = "SimulatingTimerButton";
            this.SimulatingTimerButton.UseSelectable = true;
            this.SimulatingTimerButton.Click += new System.EventHandler(this.metroButton2_Click);
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
            // metroPanel5
            // 
            this.metroPanel5.Controls.Add(this.metroLabel6);
            this.metroPanel5.Controls.Add(this.metroLabel5);
            this.metroPanel5.Controls.Add(this.metroLabel4);
            this.metroPanel5.Controls.Add(this.metroLabel3);
            this.metroPanel5.Controls.Add(this.panel4);
            this.metroPanel5.Controls.Add(this.panel3);
            this.metroPanel5.Controls.Add(this.panel2);
            this.metroPanel5.HorizontalScrollbarBarColor = true;
            this.metroPanel5.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel5.HorizontalScrollbarSize = 10;
            resources.ApplyResources(this.metroPanel5, "metroPanel5");
            this.metroPanel5.Name = "metroPanel5";
            this.metroPanel5.VerticalScrollbarBarColor = true;
            this.metroPanel5.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel5.VerticalScrollbarSize = 10;
            // 
            // metroLabel6
            // 
            resources.ApplyResources(this.metroLabel6, "metroLabel6");
            this.metroLabel6.Name = "metroLabel6";
            // 
            // metroLabel5
            // 
            resources.ApplyResources(this.metroLabel5, "metroLabel5");
            this.metroLabel5.Name = "metroLabel5";
            // 
            // metroLabel4
            // 
            resources.ApplyResources(this.metroLabel4, "metroLabel4");
            this.metroLabel4.Name = "metroLabel4";
            // 
            // metroLabel3
            // 
            resources.ApplyResources(this.metroLabel3, "metroLabel3");
            this.metroLabel3.Name = "metroLabel3";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Yellow;
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // DisplayEpicenters
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.metroPanel5);
            this.Controls.Add(this.metroPanel1);
            this.Controls.Add(this.MapPanel);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "DisplayEpicenters";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DisplayEpicenters_FormClosed);
            this.Load += new System.EventHandler(this.DisplayEpicenters_Load);
            this.panel1.ResumeLayout(false);
            this.metroPanel4.ResumeLayout(false);
            this.metroPanel4.PerformLayout();
            this.metroPanel3.ResumeLayout(false);
            this.metroPanel3.PerformLayout();
            this.metroPanel2.ResumeLayout(false);
            this.metroPanel2.PerformLayout();
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            this.metroPanel5.ResumeLayout(false);
            this.metroPanel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer SimulatingTimer;
        private MetroFramework.Controls.MetroPanel panel1;
        private MetroFramework.Controls.MetroButton metroButton1;
        private MetroFramework.Controls.MetroButton SettingsButton;
        private MetroFramework.Controls.MetroLabel label2;
        private MetroFramework.Controls.MetroComboBox ERouts;
        private MetroFramework.Controls.MetroLabel ParametresLabel;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroTrackBar EZoomBar;
        private MetroFramework.Controls.MetroPanel MapPanel;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroButton SimulatingTimerButton;
        private MetroFramework.Controls.MetroLabel SimulationLaabel;
        private MetroFramework.Controls.MetroPanel metroPanel2;
        private MetroFramework.Controls.MetroLabel TimePastLabel;
        private MetroFramework.Controls.MetroButton AfterButton;
        private MetroFramework.Controls.MetroButton ShowOriginalButton;
        private MetroFramework.Controls.MetroPanel metroPanel3;
        private MetroFramework.Controls.MetroLabel ShowResultLabel;
        private MetroFramework.Controls.MetroLabel StartStopLabel;
        private MetroFramework.Controls.MetroPanel metroPanel4;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroPanel metroPanel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private MetroFramework.Controls.MetroLabel metroLabel6;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroLabel metroLabel3;
    }
}