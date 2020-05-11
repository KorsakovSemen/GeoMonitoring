namespace SystAnalys_lr1
{
    partial class Main
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelOpt = new MetroFramework.Controls.MetroPanel();
            this.pollutionOptions = new MetroFramework.Controls.MetroButton();
            this.results = new MetroFramework.Controls.MetroGrid();
            this.Avg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Walkthrough = new MetroFramework.Controls.MetroLabel();
            this.T = new MetroFramework.Controls.MetroLabel();
            this.optimize = new MetroFramework.Controls.MetroButton();
            this.mean = new MetroFramework.Controls.MetroLabel();
            this.optText = new MetroFramework.Controls.MetroTextBox();
            this.speed = new MetroFramework.Controls.MetroTextBox();
            this.panelSettings = new MetroFramework.Controls.MetroPanel();
            this.busOptions = new MetroFramework.Controls.MetroLabel();
            this.selectedLanguage = new MetroFramework.Controls.MetroLabel();
            this.allBusSettings = new MetroFramework.Controls.MetroPanel();
            this.backsideCheck = new MetroFramework.Controls.MetroCheckBox();
            this.stopBuses = new MetroFramework.Controls.MetroButton();
            this.launchBuses = new MetroFramework.Controls.MetroButton();
            this.trackerCheck = new MetroFramework.Controls.MetroCheckBox();
            this.busSize = new MetroFramework.Controls.MetroTextBox();
            this.changeRoute = new MetroFramework.Controls.MetroComboBox();
            this.matrixLabel = new MetroFramework.Controls.MetroLabel();
            this.zoomLabel = new MetroFramework.Controls.MetroLabel();
            this.optimization = new MetroFramework.Controls.MetroLabel();
            this.panelMatrix = new MetroFramework.Controls.MetroPanel();
            this.matrix = new SystAnalys_lr1.Classes.MatrixControl();
            this.zoomBar = new MetroFramework.Controls.MetroTrackBar();
            this.trafficLightLabel = new MetroFramework.Controls.MetroLabel();
            this.theme = new MetroFramework.Controls.MetroLabel();
            this.changeTheme = new MetroFramework.Controls.MetroComboBox();
            this.mainPanel = new MetroFramework.Controls.MetroPanel();
            this.sheet = new System.Windows.Forms.PictureBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.saveButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.createModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openEpicFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addRouteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jSONToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runTrafficLightsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFromToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportTool = new System.Windows.Forms.ToolStripButton();
            this.selectButton = new System.Windows.Forms.ToolStripButton();
            this.drawVertexButton = new System.Windows.Forms.ToolStripButton();
            this.drawEdgeButton = new System.Windows.Forms.ToolStripButton();
            this.selectRoute = new System.Windows.Forms.ToolStripButton();
            this.deleteButton = new System.Windows.Forms.ToolStripButton();
            this.deleteALLButton = new System.Windows.Forms.ToolStripButton();
            this.addBus = new System.Windows.Forms.ToolStripButton();
            this.stopPointButton = new System.Windows.Forms.ToolStripButton();
            this.addTraficLight = new System.Windows.Forms.ToolStripButton();
            this.clearButton = new System.Windows.Forms.ToolStripButton();
            this.deleteBus = new System.Windows.Forms.ToolStripButton();
            this.deleteRoute = new System.Windows.Forms.ToolStripButton();
            this.delAllBusesOnRoute = new System.Windows.Forms.ToolStripButton();
            this.msmMain = new MetroFramework.Components.MetroStyleManager(this.components);
            this.themes = new MetroFramework.Controls.MetroToggle();
            this.config = new MetroFramework.Controls.MetroLabel();
            this.changeLanguage = new MetroFramework.Controls.MetroComboBox();
            this.language = new MetroFramework.Controls.MetroLabel();
            this.hint = new MetroFramework.Controls.MetroLabel();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.panelOpt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.results)).BeginInit();
            this.panelSettings.SuspendLayout();
            this.allBusSettings.SuspendLayout();
            this.panelMatrix.SuspendLayout();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sheet)).BeginInit();
            this.toolStripMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.msmMain)).BeginInit();
            this.SuspendLayout();
            // 
            // panelOpt
            // 
            resources.ApplyResources(this.panelOpt, "panelOpt");
            this.panelOpt.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panelOpt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelOpt.Controls.Add(this.pollutionOptions);
            this.panelOpt.Controls.Add(this.results);
            this.panelOpt.Controls.Add(this.Walkthrough);
            this.panelOpt.Controls.Add(this.T);
            this.panelOpt.Controls.Add(this.optimize);
            this.panelOpt.Controls.Add(this.mean);
            this.panelOpt.Controls.Add(this.optText);
            this.panelOpt.Controls.Add(this.speed);
            this.panelOpt.HorizontalScrollbarBarColor = true;
            this.panelOpt.HorizontalScrollbarHighlightOnWheel = false;
            this.panelOpt.HorizontalScrollbarSize = 8;
            this.panelOpt.Name = "panelOpt";
            this.panelOpt.VerticalScrollbarBarColor = true;
            this.panelOpt.VerticalScrollbarHighlightOnWheel = false;
            this.panelOpt.VerticalScrollbarSize = 8;
            // 
            // pollutionOptions
            // 
            resources.ApplyResources(this.pollutionOptions, "pollutionOptions");
            this.pollutionOptions.Name = "pollutionOptions";
            this.pollutionOptions.UseSelectable = true;
            this.pollutionOptions.Click += new System.EventHandler(this.MetroButton2_Click);
            // 
            // results
            // 
            resources.ApplyResources(this.results, "results");
            this.results.AllowUserToResizeRows = false;
            this.results.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.results.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.results.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.results.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.results.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.results.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.results.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Avg});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.results.DefaultCellStyle = dataGridViewCellStyle5;
            this.results.EnableHeadersVisualStyles = false;
            this.results.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.results.Name = "results";
            this.results.ReadOnly = true;
            this.results.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.results.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.results.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.results.RowTemplate.Height = 24;
            this.results.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // Avg
            // 
            resources.ApplyResources(this.Avg, "Avg");
            this.Avg.Name = "Avg";
            this.Avg.ReadOnly = true;
            // 
            // Walkthrough
            // 
            resources.ApplyResources(this.Walkthrough, "Walkthrough");
            this.Walkthrough.Name = "Walkthrough";
            // 
            // T
            // 
            resources.ApplyResources(this.T, "T");
            this.T.Name = "T";
            // 
            // optimize
            // 
            resources.ApplyResources(this.optimize, "optimize");
            this.optimize.Name = "optimize";
            this.optimize.Style = MetroFramework.MetroColorStyle.Green;
            this.optimize.UseSelectable = true;
            this.optimize.Click += new System.EventHandler(this.Optimize_ClickAsync);
            // 
            // mean
            // 
            resources.ApplyResources(this.mean, "mean");
            this.mean.Name = "mean";
            // 
            // optText
            // 
            resources.ApplyResources(this.optText, "optText");
            // 
            // 
            // 
            this.optText.CustomButton.AccessibleDescription = resources.GetString("resource.AccessibleDescription");
            this.optText.CustomButton.AccessibleName = resources.GetString("resource.AccessibleName");
            this.optText.CustomButton.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("resource.Anchor")));
            this.optText.CustomButton.AutoSize = ((bool)(resources.GetObject("resource.AutoSize")));
            this.optText.CustomButton.AutoSizeMode = ((System.Windows.Forms.AutoSizeMode)(resources.GetObject("resource.AutoSizeMode")));
            this.optText.CustomButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("resource.BackgroundImage")));
            this.optText.CustomButton.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("resource.BackgroundImageLayout")));
            this.optText.CustomButton.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("resource.Dock")));
            this.optText.CustomButton.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("resource.FlatStyle")));
            this.optText.CustomButton.Font = ((System.Drawing.Font)(resources.GetObject("resource.Font")));
            this.optText.CustomButton.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            this.optText.CustomButton.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("resource.ImageAlign")));
            this.optText.CustomButton.ImageIndex = ((int)(resources.GetObject("resource.ImageIndex")));
            this.optText.CustomButton.ImageKey = resources.GetString("resource.ImageKey");
            this.optText.CustomButton.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("resource.ImeMode")));
            this.optText.CustomButton.Location = ((System.Drawing.Point)(resources.GetObject("resource.Location")));
            this.optText.CustomButton.Margin = ((System.Windows.Forms.Padding)(resources.GetObject("resource.Margin")));
            this.optText.CustomButton.MaximumSize = ((System.Drawing.Size)(resources.GetObject("resource.MaximumSize")));
            this.optText.CustomButton.Name = "";
            this.optText.CustomButton.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("resource.RightToLeft")));
            this.optText.CustomButton.Size = ((System.Drawing.Size)(resources.GetObject("resource.Size")));
            this.optText.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.optText.CustomButton.TabIndex = ((int)(resources.GetObject("resource.TabIndex")));
            this.optText.CustomButton.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("resource.TextAlign")));
            this.optText.CustomButton.TextImageRelation = ((System.Windows.Forms.TextImageRelation)(resources.GetObject("resource.TextImageRelation")));
            this.optText.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.optText.CustomButton.UseSelectable = true;
            this.optText.CustomButton.Visible = ((bool)(resources.GetObject("resource.Visible")));
            this.optText.Lines = new string[0];
            this.optText.MaxLength = 9;
            this.optText.Name = "optText";
            this.optText.PasswordChar = '\0';
            this.optText.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.optText.SelectedText = "";
            this.optText.SelectionLength = 0;
            this.optText.SelectionStart = 0;
            this.optText.ShortcutsEnabled = true;
            this.optText.UseSelectable = true;
            this.optText.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.optText.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.optText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OptText_KeyPress);
            // 
            // speed
            // 
            resources.ApplyResources(this.speed, "speed");
            // 
            // 
            // 
            this.speed.CustomButton.AccessibleDescription = resources.GetString("resource.AccessibleDescription1");
            this.speed.CustomButton.AccessibleName = resources.GetString("resource.AccessibleName1");
            this.speed.CustomButton.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("resource.Anchor1")));
            this.speed.CustomButton.AutoSize = ((bool)(resources.GetObject("resource.AutoSize1")));
            this.speed.CustomButton.AutoSizeMode = ((System.Windows.Forms.AutoSizeMode)(resources.GetObject("resource.AutoSizeMode1")));
            this.speed.CustomButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("resource.BackgroundImage1")));
            this.speed.CustomButton.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("resource.BackgroundImageLayout1")));
            this.speed.CustomButton.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("resource.Dock1")));
            this.speed.CustomButton.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("resource.FlatStyle1")));
            this.speed.CustomButton.Font = ((System.Drawing.Font)(resources.GetObject("resource.Font1")));
            this.speed.CustomButton.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image1")));
            this.speed.CustomButton.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("resource.ImageAlign1")));
            this.speed.CustomButton.ImageIndex = ((int)(resources.GetObject("resource.ImageIndex1")));
            this.speed.CustomButton.ImageKey = resources.GetString("resource.ImageKey1");
            this.speed.CustomButton.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("resource.ImeMode1")));
            this.speed.CustomButton.Location = ((System.Drawing.Point)(resources.GetObject("resource.Location1")));
            this.speed.CustomButton.Margin = ((System.Windows.Forms.Padding)(resources.GetObject("resource.Margin1")));
            this.speed.CustomButton.MaximumSize = ((System.Drawing.Size)(resources.GetObject("resource.MaximumSize1")));
            this.speed.CustomButton.Name = "";
            this.speed.CustomButton.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("resource.RightToLeft1")));
            this.speed.CustomButton.Size = ((System.Drawing.Size)(resources.GetObject("resource.Size1")));
            this.speed.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.speed.CustomButton.TabIndex = ((int)(resources.GetObject("resource.TabIndex1")));
            this.speed.CustomButton.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("resource.TextAlign1")));
            this.speed.CustomButton.TextImageRelation = ((System.Windows.Forms.TextImageRelation)(resources.GetObject("resource.TextImageRelation1")));
            this.speed.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.speed.CustomButton.UseSelectable = true;
            this.speed.CustomButton.Visible = ((bool)(resources.GetObject("resource.Visible1")));
            this.speed.Lines = new string[0];
            this.speed.MaxLength = 9;
            this.speed.Name = "speed";
            this.speed.PasswordChar = '\0';
            this.speed.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.speed.SelectedText = "";
            this.speed.SelectionLength = 0;
            this.speed.SelectionStart = 0;
            this.speed.ShortcutsEnabled = true;
            this.speed.UseSelectable = true;
            this.speed.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.speed.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.speed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Speed_KeyPress);
            // 
            // panelSettings
            // 
            resources.ApplyResources(this.panelSettings, "panelSettings");
            this.panelSettings.BackColor = System.Drawing.SystemColors.Menu;
            this.panelSettings.Controls.Add(this.busOptions);
            this.panelSettings.Controls.Add(this.selectedLanguage);
            this.panelSettings.Controls.Add(this.allBusSettings);
            this.panelSettings.Controls.Add(this.changeRoute);
            this.panelSettings.Controls.Add(this.matrixLabel);
            this.panelSettings.Controls.Add(this.zoomLabel);
            this.panelSettings.Controls.Add(this.optimization);
            this.panelSettings.Controls.Add(this.panelMatrix);
            this.panelSettings.Controls.Add(this.panelOpt);
            this.panelSettings.Controls.Add(this.zoomBar);
            this.panelSettings.HorizontalScrollbarBarColor = true;
            this.panelSettings.HorizontalScrollbarHighlightOnWheel = false;
            this.panelSettings.HorizontalScrollbarSize = 8;
            this.panelSettings.Name = "panelSettings";
            this.panelSettings.VerticalScrollbarBarColor = true;
            this.panelSettings.VerticalScrollbarHighlightOnWheel = false;
            this.panelSettings.VerticalScrollbarSize = 8;
            // 
            // busOptions
            // 
            resources.ApplyResources(this.busOptions, "busOptions");
            this.busOptions.Name = "busOptions";
            // 
            // selectedLanguage
            // 
            resources.ApplyResources(this.selectedLanguage, "selectedLanguage");
            this.selectedLanguage.Name = "selectedLanguage";
            // 
            // allBusSettings
            // 
            resources.ApplyResources(this.allBusSettings, "allBusSettings");
            this.allBusSettings.BackColor = System.Drawing.Color.DimGray;
            this.allBusSettings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.allBusSettings.Controls.Add(this.backsideCheck);
            this.allBusSettings.Controls.Add(this.stopBuses);
            this.allBusSettings.Controls.Add(this.launchBuses);
            this.allBusSettings.Controls.Add(this.trackerCheck);
            this.allBusSettings.Controls.Add(this.busSize);
            this.allBusSettings.HorizontalScrollbarBarColor = true;
            this.allBusSettings.HorizontalScrollbarHighlightOnWheel = false;
            this.allBusSettings.HorizontalScrollbarSize = 13;
            this.allBusSettings.Name = "allBusSettings";
            this.allBusSettings.Style = MetroFramework.MetroColorStyle.Blue;
            this.allBusSettings.UseStyleColors = true;
            this.allBusSettings.VerticalScrollbarBarColor = true;
            this.allBusSettings.VerticalScrollbarHighlightOnWheel = false;
            this.allBusSettings.VerticalScrollbarSize = 13;
            // 
            // backsideCheck
            // 
            resources.ApplyResources(this.backsideCheck, "backsideCheck");
            this.backsideCheck.Name = "backsideCheck";
            this.backsideCheck.UseSelectable = true;
            // 
            // stopBuses
            // 
            resources.ApplyResources(this.stopBuses, "stopBuses");
            this.stopBuses.Name = "stopBuses";
            this.stopBuses.UseSelectable = true;
            this.stopBuses.Click += new System.EventHandler(this.StopBuses_Click_1);
            // 
            // launchBuses
            // 
            resources.ApplyResources(this.launchBuses, "launchBuses");
            this.launchBuses.Name = "launchBuses";
            this.launchBuses.UseSelectable = true;
            this.launchBuses.Click += new System.EventHandler(this.LaunchBuses_Click_1);
            // 
            // trackerCheck
            // 
            resources.ApplyResources(this.trackerCheck, "trackerCheck");
            this.trackerCheck.Name = "trackerCheck";
            this.trackerCheck.UseSelectable = true;
            // 
            // busSize
            // 
            resources.ApplyResources(this.busSize, "busSize");
            // 
            // 
            // 
            this.busSize.CustomButton.AccessibleDescription = resources.GetString("resource.AccessibleDescription2");
            this.busSize.CustomButton.AccessibleName = resources.GetString("resource.AccessibleName2");
            this.busSize.CustomButton.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("resource.Anchor2")));
            this.busSize.CustomButton.AutoSize = ((bool)(resources.GetObject("resource.AutoSize2")));
            this.busSize.CustomButton.AutoSizeMode = ((System.Windows.Forms.AutoSizeMode)(resources.GetObject("resource.AutoSizeMode2")));
            this.busSize.CustomButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("resource.BackgroundImage2")));
            this.busSize.CustomButton.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("resource.BackgroundImageLayout2")));
            this.busSize.CustomButton.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("resource.Dock2")));
            this.busSize.CustomButton.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("resource.FlatStyle2")));
            this.busSize.CustomButton.Font = ((System.Drawing.Font)(resources.GetObject("resource.Font2")));
            this.busSize.CustomButton.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image2")));
            this.busSize.CustomButton.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("resource.ImageAlign2")));
            this.busSize.CustomButton.ImageIndex = ((int)(resources.GetObject("resource.ImageIndex2")));
            this.busSize.CustomButton.ImageKey = resources.GetString("resource.ImageKey2");
            this.busSize.CustomButton.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("resource.ImeMode2")));
            this.busSize.CustomButton.Location = ((System.Drawing.Point)(resources.GetObject("resource.Location2")));
            this.busSize.CustomButton.Margin = ((System.Windows.Forms.Padding)(resources.GetObject("resource.Margin2")));
            this.busSize.CustomButton.MaximumSize = ((System.Drawing.Size)(resources.GetObject("resource.MaximumSize2")));
            this.busSize.CustomButton.Name = "";
            this.busSize.CustomButton.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("resource.RightToLeft2")));
            this.busSize.CustomButton.Size = ((System.Drawing.Size)(resources.GetObject("resource.Size2")));
            this.busSize.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.busSize.CustomButton.TabIndex = ((int)(resources.GetObject("resource.TabIndex2")));
            this.busSize.CustomButton.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("resource.TextAlign2")));
            this.busSize.CustomButton.TextImageRelation = ((System.Windows.Forms.TextImageRelation)(resources.GetObject("resource.TextImageRelation2")));
            this.busSize.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.busSize.CustomButton.UseSelectable = true;
            this.busSize.CustomButton.Visible = ((bool)(resources.GetObject("resource.Visible2")));
            this.busSize.Lines = new string[0];
            this.busSize.MaxLength = 9;
            this.busSize.Name = "busSize";
            this.busSize.PasswordChar = '\0';
            this.busSize.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.busSize.SelectedText = "";
            this.busSize.SelectionLength = 0;
            this.busSize.SelectionStart = 0;
            this.busSize.ShortcutsEnabled = true;
            this.busSize.UseSelectable = true;
            this.busSize.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.busSize.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // changeRoute
            // 
            resources.ApplyResources(this.changeRoute, "changeRoute");
            this.changeRoute.FormattingEnabled = true;
            this.changeRoute.Name = "changeRoute";
            this.changeRoute.UseSelectable = true;
            this.changeRoute.SelectedIndexChanged += new System.EventHandler(this.ChangeRoute_SelectedIndexChanged);
            // 
            // matrixLabel
            // 
            resources.ApplyResources(this.matrixLabel, "matrixLabel");
            this.matrixLabel.Name = "matrixLabel";
            // 
            // zoomLabel
            // 
            resources.ApplyResources(this.zoomLabel, "zoomLabel");
            this.zoomLabel.Name = "zoomLabel";
            // 
            // optimization
            // 
            resources.ApplyResources(this.optimization, "optimization");
            this.optimization.Name = "optimization";
            // 
            // panelMatrix
            // 
            resources.ApplyResources(this.panelMatrix, "panelMatrix");
            this.panelMatrix.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panelMatrix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMatrix.Controls.Add(this.matrix);
            this.panelMatrix.HorizontalScrollbarBarColor = true;
            this.panelMatrix.HorizontalScrollbarHighlightOnWheel = false;
            this.panelMatrix.HorizontalScrollbarSize = 8;
            this.panelMatrix.Name = "panelMatrix";
            this.panelMatrix.VerticalScrollbarBarColor = true;
            this.panelMatrix.VerticalScrollbarHighlightOnWheel = false;
            this.panelMatrix.VerticalScrollbarSize = 8;
            // 
            // matrix
            // 
            resources.ApplyResources(this.matrix, "matrix");
            this.matrix.Name = "matrix";
            this.matrix.UseSelectable = true;
            // 
            // zoomBar
            // 
            resources.ApplyResources(this.zoomBar, "zoomBar");
            this.zoomBar.BackColor = System.Drawing.Color.Transparent;
            this.zoomBar.LargeChange = 1;
            this.zoomBar.Maximum = 3;
            this.zoomBar.Minimum = 1;
            this.zoomBar.Name = "zoomBar";
            this.zoomBar.Value = 1;
            this.zoomBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.MetroTrackBar1_ScrollAsync);
            // 
            // trafficLightLabel
            // 
            resources.ApplyResources(this.trafficLightLabel, "trafficLightLabel");
            this.trafficLightLabel.Name = "trafficLightLabel";
            // 
            // theme
            // 
            resources.ApplyResources(this.theme, "theme");
            this.theme.Name = "theme";
            // 
            // changeTheme
            // 
            resources.ApplyResources(this.changeTheme, "changeTheme");
            this.changeTheme.FormattingEnabled = true;
            this.changeTheme.Items.AddRange(new object[] {
            resources.GetString("changeTheme.Items"),
            resources.GetString("changeTheme.Items1"),
            resources.GetString("changeTheme.Items2"),
            resources.GetString("changeTheme.Items3"),
            resources.GetString("changeTheme.Items4"),
            resources.GetString("changeTheme.Items5"),
            resources.GetString("changeTheme.Items6"),
            resources.GetString("changeTheme.Items7"),
            resources.GetString("changeTheme.Items8"),
            resources.GetString("changeTheme.Items9"),
            resources.GetString("changeTheme.Items10"),
            resources.GetString("changeTheme.Items11"),
            resources.GetString("changeTheme.Items12"),
            resources.GetString("changeTheme.Items13"),
            resources.GetString("changeTheme.Items14")});
            this.changeTheme.Name = "changeTheme";
            this.changeTheme.UseSelectable = true;
            this.changeTheme.SelectedIndexChanged += new System.EventHandler(this.ChangeTheme_SelectedIndexChanged);
            // 
            // mainPanel
            // 
            resources.ApplyResources(this.mainPanel, "mainPanel");
            this.mainPanel.Controls.Add(this.sheet);
            this.mainPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mainPanel.HorizontalScrollbar = true;
            this.mainPanel.HorizontalScrollbarBarColor = false;
            this.mainPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.mainPanel.HorizontalScrollbarSize = 10;
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.VerticalScrollbar = true;
            this.mainPanel.VerticalScrollbarBarColor = false;
            this.mainPanel.VerticalScrollbarHighlightOnWheel = false;
            this.mainPanel.VerticalScrollbarSize = 10;
            this.mainPanel.Scroll += new System.Windows.Forms.ScrollEventHandler(this.Panel6_Scroll);
            this.mainPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel6_Paint);
            // 
            // sheet
            // 
            resources.ApplyResources(this.sheet, "sheet");
            this.sheet.BackColor = System.Drawing.Color.White;
            this.sheet.Name = "sheet";
            this.sheet.TabStop = false;
            this.sheet.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Sheet_MouseClick_1);
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // toolStripSeparator2
            // 
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // toolStripMenu
            // 
            resources.ApplyResources(this.toolStripMenu, "toolStripMenu");
            this.toolStripMenu.BackColor = System.Drawing.Color.White;
            this.toolStripMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveButton,
            this.loadButton,
            this.reportTool,
            this.toolStripSeparator1,
            this.selectButton,
            this.drawVertexButton,
            this.drawEdgeButton,
            this.selectRoute,
            this.deleteButton,
            this.deleteALLButton,
            this.addBus,
            this.stopPointButton,
            this.addTraficLight,
            this.toolStripSeparator2,
            this.clearButton,
            this.deleteBus,
            this.deleteRoute,
            this.delAllBusesOnRoute});
            this.toolStripMenu.Name = "toolStripMenu";
            // 
            // saveButton
            // 
            resources.ApplyResources(this.saveButton, "saveButton");
            this.saveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.saveButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createModelToolStripMenuItem,
            this.openEpicFormToolStripMenuItem,
            this.addRouteToolStripMenuItem,
            this.createGridToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveInToolStripMenuItem,
            this.runTrafficLightsToolStripMenuItem});
            this.saveButton.Name = "saveButton";
            // 
            // createModelToolStripMenuItem
            // 
            resources.ApplyResources(this.createModelToolStripMenuItem, "createModelToolStripMenuItem");
            this.createModelToolStripMenuItem.Name = "createModelToolStripMenuItem";
            this.createModelToolStripMenuItem.Click += new System.EventHandler(this.NewModelToolStripMenuItem_Click);
            // 
            // openEpicFormToolStripMenuItem
            // 
            resources.ApplyResources(this.openEpicFormToolStripMenuItem, "openEpicFormToolStripMenuItem");
            this.openEpicFormToolStripMenuItem.Name = "openEpicFormToolStripMenuItem";
            this.openEpicFormToolStripMenuItem.Click += new System.EventHandler(this.OpenEpicFormToolStripMenuItem_Click);
            // 
            // addRouteToolStripMenuItem
            // 
            resources.ApplyResources(this.addRouteToolStripMenuItem, "addRouteToolStripMenuItem");
            this.addRouteToolStripMenuItem.Name = "addRouteToolStripMenuItem";
            this.addRouteToolStripMenuItem.Click += new System.EventHandler(this.AddRouteToolStripMenuItem_Click);
            // 
            // createGridToolStripMenuItem
            // 
            resources.ApplyResources(this.createGridToolStripMenuItem, "createGridToolStripMenuItem");
            this.createGridToolStripMenuItem.Name = "createGridToolStripMenuItem";
            this.createGridToolStripMenuItem.Click += new System.EventHandler(this.CreateGridToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            resources.ApplyResources(this.saveToolStripMenuItem, "saveToolStripMenuItem");
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // saveInToolStripMenuItem
            // 
            resources.ApplyResources(this.saveInToolStripMenuItem, "saveInToolStripMenuItem");
            this.saveInToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.jSONToolStripMenuItem,
            this.xMLToolStripMenuItem});
            this.saveInToolStripMenuItem.Name = "saveInToolStripMenuItem";
            // 
            // jSONToolStripMenuItem
            // 
            resources.ApplyResources(this.jSONToolStripMenuItem, "jSONToolStripMenuItem");
            this.jSONToolStripMenuItem.Name = "jSONToolStripMenuItem";
            this.jSONToolStripMenuItem.Click += new System.EventHandler(this.JSONToolStripMenuItem_Click);
            // 
            // xMLToolStripMenuItem
            // 
            resources.ApplyResources(this.xMLToolStripMenuItem, "xMLToolStripMenuItem");
            this.xMLToolStripMenuItem.Name = "xMLToolStripMenuItem";
            this.xMLToolStripMenuItem.Click += new System.EventHandler(this.XMLToolStripMenuItem_Click);
            // 
            // runTrafficLightsToolStripMenuItem
            // 
            resources.ApplyResources(this.runTrafficLightsToolStripMenuItem, "runTrafficLightsToolStripMenuItem");
            this.runTrafficLightsToolStripMenuItem.Name = "runTrafficLightsToolStripMenuItem";
            this.runTrafficLightsToolStripMenuItem.Click += new System.EventHandler(this.RunTrafficLightsToolStripMenuItem_Click);
            // 
            // loadButton
            // 
            resources.ApplyResources(this.loadButton, "loadButton");
            this.loadButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.loadButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.loadFromToolStripMenuItem});
            this.loadButton.Name = "loadButton";
            // 
            // loadToolStripMenuItem
            // 
            resources.ApplyResources(this.loadToolStripMenuItem, "loadToolStripMenuItem");
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.LoadToolStripMenuItem_Click);
            // 
            // loadFromToolStripMenuItem
            // 
            resources.ApplyResources(this.loadFromToolStripMenuItem, "loadFromToolStripMenuItem");
            this.loadFromToolStripMenuItem.Name = "loadFromToolStripMenuItem";
            this.loadFromToolStripMenuItem.Click += new System.EventHandler(this.LoadFromToolStripMenuItem_Click);
            // 
            // reportTool
            // 
            resources.ApplyResources(this.reportTool, "reportTool");
            this.reportTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.reportTool.Name = "reportTool";
            this.reportTool.Click += new System.EventHandler(this.ReportTool_Click);
            // 
            // selectButton
            // 
            resources.ApplyResources(this.selectButton, "selectButton");
            this.selectButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.selectButton.Image = global::SystAnalys_lr1.Properties.Resources.newcursor;
            this.selectButton.Name = "selectButton";
            this.selectButton.Click += new System.EventHandler(this.SelectButton_Click);
            // 
            // drawVertexButton
            // 
            resources.ApplyResources(this.drawVertexButton, "drawVertexButton");
            this.drawVertexButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.drawVertexButton.Image = global::SystAnalys_lr1.Properties.Resources.circle1;
            this.drawVertexButton.Name = "drawVertexButton";
            this.drawVertexButton.Click += new System.EventHandler(this.DrawVertexButton_Click);
            // 
            // drawEdgeButton
            // 
            resources.ApplyResources(this.drawEdgeButton, "drawEdgeButton");
            this.drawEdgeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.drawEdgeButton.Image = global::SystAnalys_lr1.Properties.Resources.new_line__;
            this.drawEdgeButton.Name = "drawEdgeButton";
            this.drawEdgeButton.Click += new System.EventHandler(this.DrawEdgeButton_Click);
            // 
            // selectRoute
            // 
            resources.ApplyResources(this.selectRoute, "selectRoute");
            this.selectRoute.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.selectRoute.Image = global::SystAnalys_lr1.Properties.Resources.line_chart;
            this.selectRoute.Name = "selectRoute";
            this.selectRoute.Click += new System.EventHandler(this.SelectRoute_Click);
            // 
            // deleteButton
            // 
            resources.ApplyResources(this.deleteButton, "deleteButton");
            this.deleteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteButton.Image = global::SystAnalys_lr1.Properties.Resources.criss_cross;
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // deleteALLButton
            // 
            resources.ApplyResources(this.deleteALLButton, "deleteALLButton");
            this.deleteALLButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteALLButton.Image = global::SystAnalys_lr1.Properties.Resources.rubbish;
            this.deleteALLButton.Name = "deleteALLButton";
            this.deleteALLButton.Click += new System.EventHandler(this.DeleteALLButton_Click);
            // 
            // addBus
            // 
            resources.ApplyResources(this.addBus, "addBus");
            this.addBus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addBus.Image = global::SystAnalys_lr1.Properties.Resources.newbus;
            this.addBus.Name = "addBus";
            this.addBus.Click += new System.EventHandler(this.AddBus_Click);
            // 
            // stopPointButton
            // 
            resources.ApplyResources(this.stopPointButton, "stopPointButton");
            this.stopPointButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.stopPointButton.Image = global::SystAnalys_lr1.Properties.Resources.transportation;
            this.stopPointButton.Name = "stopPointButton";
            this.stopPointButton.Click += new System.EventHandler(this.GridButton_Click);
            // 
            // addTraficLight
            // 
            resources.ApplyResources(this.addTraficLight, "addTraficLight");
            this.addTraficLight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addTraficLight.Image = global::SystAnalys_lr1.Properties.Resources.traffic_light_;
            this.addTraficLight.Name = "addTraficLight";
            this.addTraficLight.Click += new System.EventHandler(this.AddTraficLight_Click);
            // 
            // clearButton
            // 
            resources.ApplyResources(this.clearButton, "clearButton");
            this.clearButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.clearButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.clearButton.Name = "clearButton";
            this.clearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // deleteBus
            // 
            resources.ApplyResources(this.deleteBus, "deleteBus");
            this.deleteBus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteBus.Name = "deleteBus";
            // 
            // deleteRoute
            // 
            resources.ApplyResources(this.deleteRoute, "deleteRoute");
            this.deleteRoute.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteRoute.Name = "deleteRoute";
            // 
            // delAllBusesOnRoute
            // 
            resources.ApplyResources(this.delAllBusesOnRoute, "delAllBusesOnRoute");
            this.delAllBusesOnRoute.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.delAllBusesOnRoute.Name = "delAllBusesOnRoute";
            // 
            // msmMain
            // 
            this.msmMain.Owner = this;
            // 
            // themes
            // 
            resources.ApplyResources(this.themes, "themes");
            this.themes.Name = "themes";
            this.themes.UseSelectable = true;
            this.themes.CheckedChanged += new System.EventHandler(this.Themes_CheckedChanged);
            // 
            // config
            // 
            resources.ApplyResources(this.config, "config");
            this.config.Name = "config";
            // 
            // changeLanguage
            // 
            resources.ApplyResources(this.changeLanguage, "changeLanguage");
            this.changeLanguage.AutoCompleteCustomSource.AddRange(new string[] {
            resources.GetString("changeLanguage.AutoCompleteCustomSource"),
            resources.GetString("changeLanguage.AutoCompleteCustomSource1")});
            this.changeLanguage.FormattingEnabled = true;
            this.changeLanguage.Name = "changeLanguage";
            this.changeLanguage.UseSelectable = true;
            this.changeLanguage.SelectedIndexChanged += new System.EventHandler(this.MetroComboBox1_SelectedIndexChanged);
            // 
            // language
            // 
            resources.ApplyResources(this.language, "language");
            this.language.Name = "language";
            // 
            // hint
            // 
            resources.ApplyResources(this.hint, "hint");
            this.hint.Name = "hint";
            // 
            // timer
            // 
            this.timer.Interval = 20;
            this.timer.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // Main
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.hint);
            this.Controls.Add(this.language);
            this.Controls.Add(this.changeLanguage);
            this.Controls.Add(this.config);
            this.Controls.Add(this.themes);
            this.Controls.Add(this.trafficLightLabel);
            this.Controls.Add(this.theme);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.changeTheme);
            this.Controls.Add(this.panelSettings);
            this.Controls.Add(this.toolStripMenu);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Style = MetroFramework.MetroColorStyle.Default;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.panelOpt.ResumeLayout(false);
            this.panelOpt.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.results)).EndInit();
            this.panelSettings.ResumeLayout(false);
            this.panelSettings.PerformLayout();
            this.allBusSettings.ResumeLayout(false);
            this.allBusSettings.PerformLayout();
            this.panelMatrix.ResumeLayout(false);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sheet)).EndInit();
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.msmMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroPanel panelSettings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton selectButton;
        private System.Windows.Forms.ToolStripButton drawVertexButton;
        private System.Windows.Forms.ToolStripButton drawEdgeButton;
        private System.Windows.Forms.ToolStripButton deleteALLButton;
        private System.Windows.Forms.ToolStripButton addBus;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripDropDownButton saveButton;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createModelToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton loadButton;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadFromToolStripMenuItem;
        private MetroFramework.Controls.MetroButton optimize;
        private MetroFramework.Controls.MetroTextBox optText;
        private System.Windows.Forms.PictureBox sheet;
        private MetroFramework.Controls.MetroLabel mean;
        public MetroFramework.Controls.MetroPanel mainPanel;
        private System.Windows.Forms.ToolStripButton stopPointButton;
        public MetroFramework.Controls.MetroTextBox speed;
        private System.Windows.Forms.ToolStripMenuItem jSONToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton addTraficLight;
        public System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton selectRoute;
        private System.Windows.Forms.ToolStripMenuItem openEpicFormToolStripMenuItem;
        private MetroFramework.Controls.MetroLabel Walkthrough;
        private MetroFramework.Controls.MetroLabel T;
        private System.Windows.Forms.ToolStripMenuItem addRouteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createGridToolStripMenuItem;
        private MetroFramework.Controls.MetroPanel panelMatrix;
        private MetroFramework.Controls.MetroPanel panelOpt;
        private System.Windows.Forms.ToolStripMenuItem runTrafficLightsToolStripMenuItem;
        public MetroFramework.Components.MetroStyleManager msmMain;
        private MetroFramework.Controls.MetroToggle themes;
        private MetroFramework.Controls.MetroComboBox changeTheme;
        private MetroFramework.Controls.MetroLabel theme;
        private MetroFramework.Controls.MetroLabel config;
        private MetroFramework.Controls.MetroLabel language;
        private MetroFramework.Controls.MetroComboBox changeLanguage;
        private MetroFramework.Controls.MetroTrackBar zoomBar;
        private MetroFramework.Controls.MetroLabel hint;
        private MetroFramework.Controls.MetroLabel trafficLightLabel;
        private MetroFramework.Controls.MetroLabel optimization;
        private MetroFramework.Controls.MetroLabel zoomLabel;
        private MetroFramework.Controls.MetroLabel matrixLabel;

        //test
        private MetroFramework.Controls.MetroGrid results;
        private System.Windows.Forms.DataGridViewTextBoxColumn Avg;


        private MetroFramework.Controls.MetroButton pollutionOptions;
        private System.Windows.Forms.ToolStripButton clearButton;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripButton reportTool;
        private System.Windows.Forms.ToolStripButton deleteButton;
        private System.Windows.Forms.ToolStripButton deleteBus;
        private System.Windows.Forms.ToolStripButton deleteRoute;
        private System.Windows.Forms.ToolStripButton delAllBusesOnRoute;
        private Classes.MatrixControl matrix;
        private MetroFramework.Controls.MetroLabel busOptions;
        private MetroFramework.Controls.MetroLabel selectedLanguage;
        private MetroFramework.Controls.MetroPanel allBusSettings;
        private MetroFramework.Controls.MetroCheckBox backsideCheck;
        private MetroFramework.Controls.MetroButton stopBuses;
        private MetroFramework.Controls.MetroButton launchBuses;
        private MetroFramework.Controls.MetroCheckBox trackerCheck;
        private MetroFramework.Controls.MetroTextBox busSize;
        public MetroFramework.Controls.MetroComboBox changeRoute;
    }
}

