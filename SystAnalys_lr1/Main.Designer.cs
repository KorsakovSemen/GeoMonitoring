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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.loadButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFromToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportTool = new System.Windows.Forms.ToolStripButton();
            this.clearButton = new System.Windows.Forms.ToolStripButton();
            this.selectButton = new System.Windows.Forms.ToolStripButton();
            this.drawVertexButton = new System.Windows.Forms.ToolStripButton();
            this.drawEdgeButton = new System.Windows.Forms.ToolStripButton();
            this.selectRoute = new System.Windows.Forms.ToolStripButton();
            this.deleteButton = new System.Windows.Forms.ToolStripButton();
            this.deleteALLButton = new System.Windows.Forms.ToolStripButton();
            this.addBus = new System.Windows.Forms.ToolStripButton();
            this.stopPointButton = new System.Windows.Forms.ToolStripButton();
            this.addTraficLight = new System.Windows.Forms.ToolStripButton();
            this.msmMain = new MetroFramework.Components.MetroStyleManager(this.components);
            this.config = new MetroFramework.Controls.MetroLabel();
            this.changeLanguage = new MetroFramework.Controls.MetroComboBox();
            this.language = new MetroFramework.Controls.MetroLabel();
            this.hint = new MetroFramework.Controls.MetroLabel();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.saved = new MetroFramework.Controls.MetroLabel();
            this.loadingSpinner = new MetroFramework.Controls.MetroProgressSpinner();
            this.themes = new MetroFramework.Controls.MetroToggle();
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
            this.panelOpt.HorizontalScrollbarSize = 13;
            resources.ApplyResources(this.panelOpt, "panelOpt");
            this.panelOpt.Name = "panelOpt";
            this.panelOpt.VerticalScrollbarBarColor = true;
            this.panelOpt.VerticalScrollbarHighlightOnWheel = false;
            this.panelOpt.VerticalScrollbarSize = 15;
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
            this.results.AllowUserToResizeRows = false;
            this.results.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.results.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.results.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.results.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.results.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.results.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.results.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Avg});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.results.DefaultCellStyle = dataGridViewCellStyle2;
            this.results.EnableHeadersVisualStyles = false;
            resources.ApplyResources(this.results, "results");
            this.results.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.results.Name = "results";
            this.results.ReadOnly = true;
            this.results.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.results.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
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
            // 
            // 
            // 
            this.optText.CustomButton.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            this.optText.CustomButton.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("resource.ImeMode")));
            this.optText.CustomButton.Location = ((System.Drawing.Point)(resources.GetObject("resource.Location")));
            this.optText.CustomButton.Margin = ((System.Windows.Forms.Padding)(resources.GetObject("resource.Margin")));
            this.optText.CustomButton.Name = "";
            this.optText.CustomButton.Size = ((System.Drawing.Size)(resources.GetObject("resource.Size")));
            this.optText.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.optText.CustomButton.TabIndex = ((int)(resources.GetObject("resource.TabIndex")));
            this.optText.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.optText.CustomButton.UseSelectable = true;
            this.optText.CustomButton.Visible = ((bool)(resources.GetObject("resource.Visible")));
            this.optText.Lines = new string[0];
            resources.ApplyResources(this.optText, "optText");
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
            // 
            // 
            // 
            this.speed.CustomButton.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image1")));
            this.speed.CustomButton.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("resource.ImeMode1")));
            this.speed.CustomButton.Location = ((System.Drawing.Point)(resources.GetObject("resource.Location1")));
            this.speed.CustomButton.Margin = ((System.Windows.Forms.Padding)(resources.GetObject("resource.Margin1")));
            this.speed.CustomButton.Name = "";
            this.speed.CustomButton.Size = ((System.Drawing.Size)(resources.GetObject("resource.Size1")));
            this.speed.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.speed.CustomButton.TabIndex = ((int)(resources.GetObject("resource.TabIndex1")));
            this.speed.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.speed.CustomButton.UseSelectable = true;
            this.speed.CustomButton.Visible = ((bool)(resources.GetObject("resource.Visible1")));
            this.speed.Lines = new string[0];
            resources.ApplyResources(this.speed, "speed");
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
            resources.ApplyResources(this.panelSettings, "panelSettings");
            this.panelSettings.HorizontalScrollbarBarColor = true;
            this.panelSettings.HorizontalScrollbarHighlightOnWheel = false;
            this.panelSettings.HorizontalScrollbarSize = 13;
            this.panelSettings.Name = "panelSettings";
            this.panelSettings.VerticalScrollbarBarColor = true;
            this.panelSettings.VerticalScrollbarHighlightOnWheel = false;
            this.panelSettings.VerticalScrollbarSize = 15;
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
            this.allBusSettings.BackColor = System.Drawing.Color.DimGray;
            this.allBusSettings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.allBusSettings.Controls.Add(this.backsideCheck);
            this.allBusSettings.Controls.Add(this.stopBuses);
            this.allBusSettings.Controls.Add(this.launchBuses);
            this.allBusSettings.Controls.Add(this.trackerCheck);
            this.allBusSettings.HorizontalScrollbarBarColor = true;
            this.allBusSettings.HorizontalScrollbarHighlightOnWheel = false;
            this.allBusSettings.HorizontalScrollbarSize = 22;
            resources.ApplyResources(this.allBusSettings, "allBusSettings");
            this.allBusSettings.Name = "allBusSettings";
            this.allBusSettings.Style = MetroFramework.MetroColorStyle.Blue;
            this.allBusSettings.UseStyleColors = true;
            this.allBusSettings.VerticalScrollbarBarColor = true;
            this.allBusSettings.VerticalScrollbarHighlightOnWheel = false;
            this.allBusSettings.VerticalScrollbarSize = 23;
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
            // changeRoute
            // 
            this.changeRoute.FormattingEnabled = true;
            resources.ApplyResources(this.changeRoute, "changeRoute");
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
            this.panelMatrix.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panelMatrix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMatrix.Controls.Add(this.matrix);
            this.panelMatrix.HorizontalScrollbarBarColor = true;
            this.panelMatrix.HorizontalScrollbarHighlightOnWheel = false;
            this.panelMatrix.HorizontalScrollbarSize = 13;
            resources.ApplyResources(this.panelMatrix, "panelMatrix");
            this.panelMatrix.Name = "panelMatrix";
            this.panelMatrix.VerticalScrollbarBarColor = true;
            this.panelMatrix.VerticalScrollbarHighlightOnWheel = false;
            this.panelMatrix.VerticalScrollbarSize = 15;
            // 
            // matrix
            // 
            resources.ApplyResources(this.matrix, "matrix");
            this.matrix.Name = "matrix";
            this.matrix.UseSelectable = true;
            // 
            // zoomBar
            // 
            this.zoomBar.BackColor = System.Drawing.Color.Transparent;
            this.zoomBar.LargeChange = 1;
            resources.ApplyResources(this.zoomBar, "zoomBar");
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
            this.changeTheme.FormattingEnabled = true;
            resources.ApplyResources(this.changeTheme, "changeTheme");
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
            this.mainPanel.HorizontalScrollbarSize = 17;
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.VerticalScrollbar = true;
            this.mainPanel.VerticalScrollbarBarColor = false;
            this.mainPanel.VerticalScrollbarHighlightOnWheel = false;
            this.mainPanel.VerticalScrollbarSize = 17;
            this.mainPanel.Scroll += new System.Windows.Forms.ScrollEventHandler(this.Panel6_Scroll);
            this.mainPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel6_Paint);
            // 
            // sheet
            // 
            this.sheet.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.sheet, "sheet");
            this.sheet.Name = "sheet";
            this.sheet.TabStop = false;
            this.sheet.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Sheet_MouseClick_1);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.BackColor = System.Drawing.Color.White;
            this.toolStripMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveButton,
            this.loadButton,
            this.reportTool,
            this.clearButton,
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
            this.toolStripSeparator2});
            resources.ApplyResources(this.toolStripMenu, "toolStripMenu");
            this.toolStripMenu.Name = "toolStripMenu";
            // 
            // saveButton
            // 
            this.saveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.saveButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createModelToolStripMenuItem,
            this.openEpicFormToolStripMenuItem,
            this.addRouteToolStripMenuItem,
            this.createGridToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveInToolStripMenuItem});
            resources.ApplyResources(this.saveButton, "saveButton");
            this.saveButton.Name = "saveButton";
            // 
            // createModelToolStripMenuItem
            // 
            this.createModelToolStripMenuItem.Name = "createModelToolStripMenuItem";
            resources.ApplyResources(this.createModelToolStripMenuItem, "createModelToolStripMenuItem");
            this.createModelToolStripMenuItem.Click += new System.EventHandler(this.NewModelToolStripMenuItem_Click);
            // 
            // openEpicFormToolStripMenuItem
            // 
            this.openEpicFormToolStripMenuItem.Name = "openEpicFormToolStripMenuItem";
            resources.ApplyResources(this.openEpicFormToolStripMenuItem, "openEpicFormToolStripMenuItem");
            this.openEpicFormToolStripMenuItem.Click += new System.EventHandler(this.OpenEpicFormToolStripMenuItem_Click);
            // 
            // addRouteToolStripMenuItem
            // 
            this.addRouteToolStripMenuItem.Name = "addRouteToolStripMenuItem";
            resources.ApplyResources(this.addRouteToolStripMenuItem, "addRouteToolStripMenuItem");
            this.addRouteToolStripMenuItem.Click += new System.EventHandler(this.AddRouteToolStripMenuItem_Click);
            // 
            // createGridToolStripMenuItem
            // 
            this.createGridToolStripMenuItem.Name = "createGridToolStripMenuItem";
            resources.ApplyResources(this.createGridToolStripMenuItem, "createGridToolStripMenuItem");
            this.createGridToolStripMenuItem.Click += new System.EventHandler(this.CreateGridToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            resources.ApplyResources(this.saveToolStripMenuItem, "saveToolStripMenuItem");
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // saveInToolStripMenuItem
            // 
            this.saveInToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.jSONToolStripMenuItem,
            this.xMLToolStripMenuItem});
            this.saveInToolStripMenuItem.Name = "saveInToolStripMenuItem";
            resources.ApplyResources(this.saveInToolStripMenuItem, "saveInToolStripMenuItem");
            // 
            // jSONToolStripMenuItem
            // 
            this.jSONToolStripMenuItem.Name = "jSONToolStripMenuItem";
            resources.ApplyResources(this.jSONToolStripMenuItem, "jSONToolStripMenuItem");
            this.jSONToolStripMenuItem.Click += new System.EventHandler(this.JSONToolStripMenuItem_Click);
            // 
            // xMLToolStripMenuItem
            // 
            this.xMLToolStripMenuItem.Name = "xMLToolStripMenuItem";
            resources.ApplyResources(this.xMLToolStripMenuItem, "xMLToolStripMenuItem");
            this.xMLToolStripMenuItem.Click += new System.EventHandler(this.XMLToolStripMenuItem_Click);
            // 
            // loadButton
            // 
            this.loadButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.loadButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.loadFromToolStripMenuItem});
            resources.ApplyResources(this.loadButton, "loadButton");
            this.loadButton.Name = "loadButton";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            resources.ApplyResources(this.loadToolStripMenuItem, "loadToolStripMenuItem");
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.LoadToolStripMenuItem_Click);
            // 
            // loadFromToolStripMenuItem
            // 
            this.loadFromToolStripMenuItem.Name = "loadFromToolStripMenuItem";
            resources.ApplyResources(this.loadFromToolStripMenuItem, "loadFromToolStripMenuItem");
            this.loadFromToolStripMenuItem.Click += new System.EventHandler(this.LoadFromToolStripMenuItem_Click);
            // 
            // reportTool
            // 
            this.reportTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.reportTool, "reportTool");
            this.reportTool.Name = "reportTool";
            this.reportTool.Click += new System.EventHandler(this.ReportTool_Click);
            // 
            // clearButton
            // 
            this.clearButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.clearButton, "clearButton");
            this.clearButton.Name = "clearButton";
            this.clearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // selectButton
            // 
            this.selectButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.selectButton.Image = global::SystAnalys_lr1.Properties.Resources.newcursor;
            this.selectButton.Name = "selectButton";
            resources.ApplyResources(this.selectButton, "selectButton");
            this.selectButton.Click += new System.EventHandler(this.SelectButton_Click);
            // 
            // drawVertexButton
            // 
            this.drawVertexButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.drawVertexButton.Image = global::SystAnalys_lr1.Properties.Resources.CIRCLE_WT;
            this.drawVertexButton.Name = "drawVertexButton";
            resources.ApplyResources(this.drawVertexButton, "drawVertexButton");
            this.drawVertexButton.Click += new System.EventHandler(this.DrawVertexButton_Click);
            // 
            // drawEdgeButton
            // 
            this.drawEdgeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.drawEdgeButton.Image = global::SystAnalys_lr1.Properties.Resources.new_line__;
            this.drawEdgeButton.Name = "drawEdgeButton";
            resources.ApplyResources(this.drawEdgeButton, "drawEdgeButton");
            this.drawEdgeButton.Click += new System.EventHandler(this.DrawEdgeButton_Click);
            // 
            // selectRoute
            // 
            this.selectRoute.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.selectRoute.Image = global::SystAnalys_lr1.Properties.Resources.line_chart;
            this.selectRoute.Name = "selectRoute";
            resources.ApplyResources(this.selectRoute, "selectRoute");
            this.selectRoute.Click += new System.EventHandler(this.SelectRoute_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteButton.Image = global::SystAnalys_lr1.Properties.Resources.criss_cross;
            this.deleteButton.Name = "deleteButton";
            resources.ApplyResources(this.deleteButton, "deleteButton");
            this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // deleteALLButton
            // 
            this.deleteALLButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteALLButton.Image = global::SystAnalys_lr1.Properties.Resources.DELETE_ALL_ICON;
            this.deleteALLButton.Name = "deleteALLButton";
            resources.ApplyResources(this.deleteALLButton, "deleteALLButton");
            this.deleteALLButton.Click += new System.EventHandler(this.DeleteALLButton_Click);
            // 
            // addBus
            // 
            this.addBus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addBus.Image = global::SystAnalys_lr1.Properties.Resources.newbus;
            this.addBus.Name = "addBus";
            resources.ApplyResources(this.addBus, "addBus");
            this.addBus.Click += new System.EventHandler(this.AddBus_Click);
            // 
            // stopPointButton
            // 
            this.stopPointButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.stopPointButton.Image = global::SystAnalys_lr1.Properties.Resources.transportation;
            this.stopPointButton.Name = "stopPointButton";
            resources.ApplyResources(this.stopPointButton, "stopPointButton");
            this.stopPointButton.Click += new System.EventHandler(this.GridButton_Click);
            // 
            // addTraficLight
            // 
            this.addTraficLight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addTraficLight.Image = global::SystAnalys_lr1.Properties.Resources.traffic_light_;
            this.addTraficLight.Name = "addTraficLight";
            resources.ApplyResources(this.addTraficLight, "addTraficLight");
            this.addTraficLight.Click += new System.EventHandler(this.AddTraficLight_Click);
            // 
            // msmMain
            // 
            this.msmMain.Owner = this;
            // 
            // config
            // 
            resources.ApplyResources(this.config, "config");
            this.config.Name = "config";
            // 
            // changeLanguage
            // 
            this.changeLanguage.AutoCompleteCustomSource.AddRange(new string[] {
            resources.GetString("changeLanguage.AutoCompleteCustomSource"),
            resources.GetString("changeLanguage.AutoCompleteCustomSource1")});
            this.changeLanguage.FormattingEnabled = true;
            resources.ApplyResources(this.changeLanguage, "changeLanguage");
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
            this.timer.Interval = 40;
            this.timer.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // saved
            // 
            resources.ApplyResources(this.saved, "saved");
            this.saved.Name = "saved";
            // 
            // loadingSpinner
            // 
            resources.ApplyResources(this.loadingSpinner, "loadingSpinner");
            this.loadingSpinner.Maximum = 100;
            this.loadingSpinner.Name = "loadingSpinner";
            this.loadingSpinner.Speed = 3F;
            this.loadingSpinner.UseSelectable = true;
            this.loadingSpinner.Value = 20;
            // 
            // themes
            // 
            resources.ApplyResources(this.themes, "themes");
            this.themes.Name = "themes";
            this.themes.UseSelectable = true;
            this.themes.CheckedChanged += new System.EventHandler(this.Themes_CheckedChanged);
            // 
            // Main
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.loadingSpinner);
            this.Controls.Add(this.saved);
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
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Main_KeyDown);
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
        public MetroFramework.Components.MetroStyleManager msmMain;
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
        private Classes.MatrixControl matrix;
        private MetroFramework.Controls.MetroLabel busOptions;
        private MetroFramework.Controls.MetroLabel selectedLanguage;
        private MetroFramework.Controls.MetroPanel allBusSettings;
        private MetroFramework.Controls.MetroCheckBox backsideCheck;
        private MetroFramework.Controls.MetroButton stopBuses;
        private MetroFramework.Controls.MetroButton launchBuses;
        private MetroFramework.Controls.MetroCheckBox trackerCheck;
        public MetroFramework.Controls.MetroComboBox changeRoute;
        private MetroFramework.Controls.MetroLabel saved;
        private MetroFramework.Controls.MetroProgressSpinner loadingSpinner;
        private MetroFramework.Controls.MetroToggle themes;
    }
}

