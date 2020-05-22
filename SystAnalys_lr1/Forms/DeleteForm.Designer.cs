namespace SystAnalys_lr1.Forms
{
    partial class DeleteForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeleteForm));
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.theBuses = new MetroFramework.Controls.MetroRadioButton();
            this.All = new MetroFramework.Controls.MetroRadioButton();
            this.deleteTypes = new MetroFramework.Controls.MetroLabel();
            this.BusStops = new MetroFramework.Controls.MetroRadioButton();
            this.TrafficLights = new MetroFramework.Controls.MetroRadioButton();
            this.VandE = new MetroFramework.Controls.MetroRadioButton();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.metroPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroPanel1
            // 
            resources.ApplyResources(this.metroPanel1, "metroPanel1");
            this.metroPanel1.Controls.Add(this.theBuses);
            this.metroPanel1.Controls.Add(this.All);
            this.metroPanel1.Controls.Add(this.deleteTypes);
            this.metroPanel1.Controls.Add(this.BusStops);
            this.metroPanel1.Controls.Add(this.TrafficLights);
            this.metroPanel1.Controls.Add(this.VandE);
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // theBuses
            // 
            resources.ApplyResources(this.theBuses, "theBuses");
            this.theBuses.Name = "theBuses";
            this.theBuses.UseSelectable = true;
            this.theBuses.CheckedChanged += new System.EventHandler(this.TheBuses_CheckedChanged);
            // 
            // All
            // 
            resources.ApplyResources(this.All, "All");
            this.All.Name = "All";
            this.All.UseSelectable = true;
            this.All.CheckedChanged += new System.EventHandler(this.All_CheckedChanged);
            // 
            // deleteTypes
            // 
            resources.ApplyResources(this.deleteTypes, "deleteTypes");
            this.deleteTypes.Name = "deleteTypes";
            // 
            // BusStops
            // 
            resources.ApplyResources(this.BusStops, "BusStops");
            this.BusStops.Name = "BusStops";
            this.BusStops.UseSelectable = true;
            this.BusStops.CheckedChanged += new System.EventHandler(this.BusStops_CheckedChanged);
            // 
            // TrafficLights
            // 
            resources.ApplyResources(this.TrafficLights, "TrafficLights");
            this.TrafficLights.Name = "TrafficLights";
            this.TrafficLights.UseSelectable = true;
            this.TrafficLights.CheckedChanged += new System.EventHandler(this.TrafficLights_CheckedChanged);
            // 
            // VandE
            // 
            resources.ApplyResources(this.VandE, "VandE");
            this.VandE.Name = "VandE";
            this.VandE.UseSelectable = true;
            this.VandE.CheckedChanged += new System.EventHandler(this.MetroRadioButton1_CheckedChanged);
            // 
            // metroButton1
            // 
            resources.ApplyResources(this.metroButton1, "metroButton1");
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.UseSelectable = true;
            this.metroButton1.Click += new System.EventHandler(this.MetroButton1_Click);
            // 
            // DeleteForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.metroButton1);
            this.Controls.Add(this.metroPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DeleteForm";
            this.Resizable = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DeleteForm_FormClosing);
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroRadioButton BusStops;
        private MetroFramework.Controls.MetroRadioButton TrafficLights;
        private MetroFramework.Controls.MetroButton metroButton1;
        public MetroFramework.Controls.MetroRadioButton VandE;
        public MetroFramework.Controls.MetroRadioButton All;
        public MetroFramework.Controls.MetroRadioButton theBuses;
        public MetroFramework.Controls.MetroLabel deleteTypes;
    }
}