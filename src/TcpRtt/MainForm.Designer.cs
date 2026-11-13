namespace TcpRtt
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            settingsGroup = new GroupBox();
            settingsTable = new TableLayoutPanel();
            calculationMethodComboBox = new ComboBox();
            calculationMethodLabel = new Label();
            minRttNumeric = new NumericUpDown();
            minRttLabel = new Label();
            portLabel = new Label();
            ipMaskLabel = new Label();
            networkInterfaceLabel = new Label();
            networkComboBox = new ComboBox();
            ipMaskTextBox = new TextBox();
            portNumeric = new NumericUpDown();
            overlayButton = new Button();
            clearButton = new Button();
            startButton = new Button();
            dataGroup = new GroupBox();
            dataTable = new TableLayoutPanel();
            maxValueLabel = new Label();
            maxDescLabel = new Label();
            avgValueLabel = new Label();
            latestValueLabel = new Label();
            avgDescLabel = new Label();
            latestDescLabel = new Label();
            pingPlot = new ScottPlot.WinForms.FormsPlot();
            timeoutDescLabel = new Label();
            timeoutValueLabel = new Label();
            uiTimer = new System.Windows.Forms.Timer(components);
            toolTip = new ToolTip(components);
            settingsGroup.SuspendLayout();
            settingsTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)minRttNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)portNumeric).BeginInit();
            dataGroup.SuspendLayout();
            dataTable.SuspendLayout();
            SuspendLayout();
            //
            // settingsGroup
            //
            settingsGroup.Controls.Add(settingsTable);
            settingsGroup.Dock = DockStyle.Top;
            settingsGroup.Font = new Font("Segoe UI", 12F);
            settingsGroup.Location = new Point(8, 8);
            settingsGroup.Name = "settingsGroup";
            settingsGroup.Padding = new Padding(8);
            settingsGroup.Size = new Size(468, 246);
            settingsGroup.TabIndex = 0;
            settingsGroup.TabStop = false;
            settingsGroup.Text = "Settings";
            //
            // settingsTable
            //
            settingsTable.ColumnCount = 3;
            settingsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            settingsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            settingsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            settingsTable.Controls.Add(calculationMethodComboBox, 0, 5);
            settingsTable.Controls.Add(calculationMethodLabel, 0, 4);
            settingsTable.Controls.Add(minRttNumeric, 2, 1);
            settingsTable.Controls.Add(minRttLabel, 2, 0);
            settingsTable.Controls.Add(portLabel, 1, 0);
            settingsTable.Controls.Add(ipMaskLabel, 0, 0);
            settingsTable.Controls.Add(networkInterfaceLabel, 0, 2);
            settingsTable.Controls.Add(networkComboBox, 0, 3);
            settingsTable.Controls.Add(ipMaskTextBox, 0, 1);
            settingsTable.Controls.Add(portNumeric, 1, 1);
            settingsTable.Controls.Add(overlayButton, 2, 6);
            settingsTable.Controls.Add(clearButton, 1, 6);
            settingsTable.Controls.Add(startButton, 0, 6);
            settingsTable.Dock = DockStyle.Fill;
            settingsTable.Location = new Point(8, 30);
            settingsTable.Name = "settingsTable";
            settingsTable.RowCount = 7;
            settingsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            settingsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            settingsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            settingsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            settingsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            settingsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            settingsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            settingsTable.Size = new Size(452, 208);
            settingsTable.TabIndex = 0;
            //
            // calculationMethodComboBox
            //
            calculationMethodComboBox.BackColor = SystemColors.ControlLight;
            settingsTable.SetColumnSpan(calculationMethodComboBox, 3);
            calculationMethodComboBox.Dock = DockStyle.Fill;
            calculationMethodComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            calculationMethodComboBox.ForeColor = SystemColors.ControlText;
            calculationMethodComboBox.FormattingEnabled = true;
            calculationMethodComboBox.Location = new Point(4, 136);
            calculationMethodComboBox.Margin = new Padding(4, 0, 4, 0);
            calculationMethodComboBox.Name = "calculationMethodComboBox";
            calculationMethodComboBox.Size = new Size(444, 29);
            calculationMethodComboBox.TabIndex = 12;
            //
            // calculationMethodLabel
            //
            calculationMethodLabel.AutoSize = true;
            settingsTable.SetColumnSpan(calculationMethodLabel, 3);
            calculationMethodLabel.Dock = DockStyle.Fill;
            calculationMethodLabel.Location = new Point(0, 112);
            calculationMethodLabel.Margin = new Padding(0);
            calculationMethodLabel.Name = "calculationMethodLabel";
            calculationMethodLabel.Size = new Size(452, 24);
            calculationMethodLabel.TabIndex = 11;
            calculationMethodLabel.Text = "Calculation Method";
            calculationMethodLabel.TextAlign = ContentAlignment.BottomLeft;
            //
            // minRttNumeric
            //
            minRttNumeric.BackColor = SystemColors.ControlLight;
            minRttNumeric.BorderStyle = BorderStyle.FixedSingle;
            minRttNumeric.DecimalPlaces = 1;
            minRttNumeric.Dock = DockStyle.Fill;
            minRttNumeric.ForeColor = SystemColors.ControlText;
            minRttNumeric.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            minRttNumeric.Location = new Point(304, 24);
            minRttNumeric.Margin = new Padding(4, 0, 4, 0);
            minRttNumeric.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            minRttNumeric.Name = "minRttNumeric";
            minRttNumeric.Size = new Size(144, 29);
            minRttNumeric.TabIndex = 9;
            toolTip.SetToolTip(minRttNumeric, "Samples below this value (ms) are treated as noise and ignored.");
            minRttNumeric.Value = new decimal(new int[] { 1, 0, 0, 65536 });
            //
            // minRttLabel
            //
            minRttLabel.AutoSize = true;
            minRttLabel.Dock = DockStyle.Fill;
            minRttLabel.Location = new Point(300, 0);
            minRttLabel.Margin = new Padding(0);
            minRttLabel.Name = "minRttLabel";
            minRttLabel.Size = new Size(152, 24);
            minRttLabel.TabIndex = 8;
            minRttLabel.Text = "Min RTT (ms)";
            minRttLabel.TextAlign = ContentAlignment.BottomLeft;
            //
            // portLabel
            //
            portLabel.AutoSize = true;
            portLabel.Dock = DockStyle.Fill;
            portLabel.Location = new Point(150, 0);
            portLabel.Margin = new Padding(0);
            portLabel.Name = "portLabel";
            portLabel.Size = new Size(150, 24);
            portLabel.TabIndex = 1;
            portLabel.Text = "Port";
            portLabel.TextAlign = ContentAlignment.BottomLeft;
            //
            // ipMaskLabel
            //
            ipMaskLabel.AutoSize = true;
            ipMaskLabel.Dock = DockStyle.Fill;
            ipMaskLabel.Location = new Point(0, 0);
            ipMaskLabel.Margin = new Padding(0);
            ipMaskLabel.Name = "ipMaskLabel";
            ipMaskLabel.Size = new Size(150, 24);
            ipMaskLabel.TabIndex = 0;
            ipMaskLabel.Text = "Server IP/CIDR";
            ipMaskLabel.TextAlign = ContentAlignment.BottomLeft;
            //
            // networkInterfaceLabel
            //
            networkInterfaceLabel.AutoSize = true;
            settingsTable.SetColumnSpan(networkInterfaceLabel, 3);
            networkInterfaceLabel.Dock = DockStyle.Fill;
            networkInterfaceLabel.Location = new Point(0, 56);
            networkInterfaceLabel.Margin = new Padding(0);
            networkInterfaceLabel.Name = "networkInterfaceLabel";
            networkInterfaceLabel.Size = new Size(452, 24);
            networkInterfaceLabel.TabIndex = 2;
            networkInterfaceLabel.Text = "Network Interface";
            networkInterfaceLabel.TextAlign = ContentAlignment.BottomLeft;
            //
            // networkComboBox
            //
            networkComboBox.BackColor = SystemColors.ControlLight;
            settingsTable.SetColumnSpan(networkComboBox, 3);
            networkComboBox.Dock = DockStyle.Fill;
            networkComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            networkComboBox.ForeColor = SystemColors.ControlText;
            networkComboBox.FormattingEnabled = true;
            networkComboBox.Location = new Point(4, 80);
            networkComboBox.Margin = new Padding(4, 0, 4, 0);
            networkComboBox.Name = "networkComboBox";
            networkComboBox.Size = new Size(444, 29);
            networkComboBox.TabIndex = 3;
            //
            // ipMaskTextBox
            //
            ipMaskTextBox.BackColor = SystemColors.ControlLight;
            ipMaskTextBox.BorderStyle = BorderStyle.FixedSingle;
            ipMaskTextBox.Dock = DockStyle.Fill;
            ipMaskTextBox.ForeColor = SystemColors.ControlText;
            ipMaskTextBox.Location = new Point(4, 24);
            ipMaskTextBox.Margin = new Padding(4, 0, 4, 0);
            ipMaskTextBox.Name = "ipMaskTextBox";
            ipMaskTextBox.PlaceholderText = "192.168.1.0/24";
            ipMaskTextBox.Size = new Size(142, 29);
            ipMaskTextBox.TabIndex = 4;
            toolTip.SetToolTip(ipMaskTextBox, "Server address or network, e.g. 203.0.113.10 or 203.0.113.0/24");
            //
            // portNumeric
            //
            portNumeric.BackColor = SystemColors.ControlLight;
            portNumeric.BorderStyle = BorderStyle.FixedSingle;
            portNumeric.Dock = DockStyle.Fill;
            portNumeric.ForeColor = SystemColors.ControlText;
            portNumeric.Location = new Point(154, 24);
            portNumeric.Margin = new Padding(4, 0, 4, 0);
            portNumeric.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            portNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            portNumeric.Name = "portNumeric";
            portNumeric.Size = new Size(142, 29);
            portNumeric.TabIndex = 5;
            portNumeric.Value = new decimal(new int[] { 8889, 0, 0, 0 });
            //
            // overlayButton
            //
            overlayButton.BackColor = SystemColors.ControlLight;
            overlayButton.Dock = DockStyle.Fill;
            overlayButton.Font = new Font("Segoe UI", 12F);
            overlayButton.ForeColor = SystemColors.ControlText;
            overlayButton.Location = new Point(303, 171);
            overlayButton.Name = "overlayButton";
            overlayButton.Size = new Size(146, 34);
            overlayButton.TabIndex = 10;
            overlayButton.Text = "Overlay";
            toolTip.SetToolTip(overlayButton, "Borderless always-on-top mode. Drag the window to move it.");
            overlayButton.UseVisualStyleBackColor = false;
            overlayButton.Click += overlayButton_Click;
            //
            // clearButton
            //
            clearButton.BackColor = SystemColors.ControlLight;
            clearButton.Dock = DockStyle.Fill;
            clearButton.Font = new Font("Segoe UI", 12F);
            clearButton.ForeColor = SystemColors.ControlText;
            clearButton.Location = new Point(154, 172);
            clearButton.Margin = new Padding(4);
            clearButton.Name = "clearButton";
            clearButton.Size = new Size(142, 32);
            clearButton.TabIndex = 7;
            clearButton.Text = "Clear";
            clearButton.UseVisualStyleBackColor = false;
            clearButton.Click += clearButton_Click;
            //
            // startButton
            //
            startButton.BackColor = SystemColors.ControlLight;
            startButton.Dock = DockStyle.Fill;
            startButton.ForeColor = SystemColors.ControlText;
            startButton.Location = new Point(4, 172);
            startButton.Margin = new Padding(4);
            startButton.Name = "startButton";
            startButton.Size = new Size(142, 32);
            startButton.TabIndex = 6;
            startButton.Text = "Start";
            startButton.UseVisualStyleBackColor = false;
            startButton.Click += startButton_Click;
            //
            // dataGroup
            //
            dataGroup.Controls.Add(dataTable);
            dataGroup.Dock = DockStyle.Fill;
            dataGroup.Font = new Font("Segoe UI", 12F);
            dataGroup.Location = new Point(8, 254);
            dataGroup.Name = "dataGroup";
            dataGroup.Padding = new Padding(8);
            dataGroup.Size = new Size(468, 354);
            dataGroup.TabIndex = 1;
            dataGroup.TabStop = false;
            dataGroup.Text = "Data";
            //
            // dataTable
            //
            dataTable.ColumnCount = 4;
            dataTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            dataTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            dataTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            dataTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            dataTable.Controls.Add(maxValueLabel, 2, 1);
            dataTable.Controls.Add(maxDescLabel, 2, 0);
            dataTable.Controls.Add(avgValueLabel, 1, 1);
            dataTable.Controls.Add(latestValueLabel, 0, 1);
            dataTable.Controls.Add(avgDescLabel, 1, 0);
            dataTable.Controls.Add(latestDescLabel, 0, 0);
            dataTable.Controls.Add(pingPlot, 0, 2);
            dataTable.Controls.Add(timeoutDescLabel, 3, 0);
            dataTable.Controls.Add(timeoutValueLabel, 3, 1);
            dataTable.Dock = DockStyle.Fill;
            dataTable.Location = new Point(8, 30);
            dataTable.Name = "dataTable";
            dataTable.RowCount = 3;
            dataTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            dataTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            dataTable.RowStyles.Add(new RowStyle());
            dataTable.Size = new Size(452, 316);
            dataTable.TabIndex = 0;
            //
            // maxValueLabel
            //
            maxValueLabel.AutoSize = true;
            maxValueLabel.Dock = DockStyle.Fill;
            maxValueLabel.Location = new Point(226, 24);
            maxValueLabel.Margin = new Padding(0);
            maxValueLabel.Name = "maxValueLabel";
            maxValueLabel.Size = new Size(113, 24);
            maxValueLabel.TabIndex = 8;
            maxValueLabel.TextAlign = ContentAlignment.BottomLeft;
            //
            // maxDescLabel
            //
            maxDescLabel.AutoSize = true;
            maxDescLabel.Dock = DockStyle.Fill;
            maxDescLabel.Location = new Point(226, 0);
            maxDescLabel.Margin = new Padding(0);
            maxDescLabel.Name = "maxDescLabel";
            maxDescLabel.Size = new Size(113, 24);
            maxDescLabel.TabIndex = 7;
            maxDescLabel.Text = "Max";
            maxDescLabel.TextAlign = ContentAlignment.BottomLeft;
            //
            // avgValueLabel
            //
            avgValueLabel.AutoSize = true;
            avgValueLabel.Dock = DockStyle.Fill;
            avgValueLabel.Location = new Point(113, 24);
            avgValueLabel.Margin = new Padding(0);
            avgValueLabel.Name = "avgValueLabel";
            avgValueLabel.Size = new Size(113, 24);
            avgValueLabel.TabIndex = 4;
            avgValueLabel.TextAlign = ContentAlignment.BottomLeft;
            //
            // latestValueLabel
            //
            latestValueLabel.AutoSize = true;
            latestValueLabel.Dock = DockStyle.Fill;
            latestValueLabel.Location = new Point(0, 24);
            latestValueLabel.Margin = new Padding(0);
            latestValueLabel.Name = "latestValueLabel";
            latestValueLabel.Size = new Size(113, 24);
            latestValueLabel.TabIndex = 3;
            latestValueLabel.TextAlign = ContentAlignment.BottomLeft;
            //
            // avgDescLabel
            //
            avgDescLabel.AutoSize = true;
            avgDescLabel.Dock = DockStyle.Fill;
            avgDescLabel.Location = new Point(113, 0);
            avgDescLabel.Margin = new Padding(0);
            avgDescLabel.Name = "avgDescLabel";
            avgDescLabel.Size = new Size(113, 24);
            avgDescLabel.TabIndex = 1;
            avgDescLabel.Text = "Avg";
            avgDescLabel.TextAlign = ContentAlignment.BottomLeft;
            //
            // latestDescLabel
            //
            latestDescLabel.AutoSize = true;
            latestDescLabel.Dock = DockStyle.Fill;
            latestDescLabel.Location = new Point(0, 0);
            latestDescLabel.Margin = new Padding(0);
            latestDescLabel.Name = "latestDescLabel";
            latestDescLabel.Size = new Size(113, 24);
            latestDescLabel.TabIndex = 0;
            latestDescLabel.Text = "Latest";
            latestDescLabel.TextAlign = ContentAlignment.BottomLeft;
            //
            // pingPlot
            //
            dataTable.SetColumnSpan(pingPlot, 4);
            pingPlot.Dock = DockStyle.Fill;
            pingPlot.Location = new Point(3, 51);
            pingPlot.Name = "pingPlot";
            pingPlot.Size = new Size(446, 262);
            pingPlot.TabIndex = 6;
            //
            // timeoutDescLabel
            //
            timeoutDescLabel.AutoSize = true;
            timeoutDescLabel.Dock = DockStyle.Fill;
            timeoutDescLabel.Location = new Point(339, 0);
            timeoutDescLabel.Margin = new Padding(0);
            timeoutDescLabel.Name = "timeoutDescLabel";
            timeoutDescLabel.Size = new Size(113, 24);
            timeoutDescLabel.TabIndex = 2;
            timeoutDescLabel.Text = "Timeout %";
            timeoutDescLabel.TextAlign = ContentAlignment.BottomLeft;
            toolTip.SetToolTip(timeoutDescLabel, "Share of sent segments not acknowledged within 1 second.");
            //
            // timeoutValueLabel
            //
            timeoutValueLabel.AutoSize = true;
            timeoutValueLabel.Dock = DockStyle.Fill;
            timeoutValueLabel.Location = new Point(339, 24);
            timeoutValueLabel.Margin = new Padding(0);
            timeoutValueLabel.Name = "timeoutValueLabel";
            timeoutValueLabel.Size = new Size(113, 24);
            timeoutValueLabel.TabIndex = 5;
            timeoutValueLabel.TextAlign = ContentAlignment.BottomLeft;
            //
            // uiTimer
            //
            uiTimer.Interval = 100;
            uiTimer.Tick += uiTimer_Tick;
            //
            // MainForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(484, 616);
            Controls.Add(dataGroup);
            Controls.Add(settingsGroup);
            ForeColor = SystemColors.ControlText;
            MinimumSize = new Size(500, 655);
            Name = "MainForm";
            Padding = new Padding(8);
            Text = "TCP RTT";
            FormClosing += MainForm_FormClosing;
            settingsGroup.ResumeLayout(false);
            settingsTable.ResumeLayout(false);
            settingsTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)minRttNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)portNumeric).EndInit();
            dataGroup.ResumeLayout(false);
            dataTable.ResumeLayout(false);
            dataTable.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox settingsGroup;
        private TableLayoutPanel settingsTable;
        private GroupBox dataGroup;
        private TableLayoutPanel dataTable;
        private Label portLabel;
        private Label ipMaskLabel;
        private Label networkInterfaceLabel;
        private ComboBox networkComboBox;
        private TextBox ipMaskTextBox;
        private NumericUpDown portNumeric;
        private Button startButton;
        private Label timeoutValueLabel;
        private Label avgValueLabel;
        private Label latestValueLabel;
        private Label timeoutDescLabel;
        private Label avgDescLabel;
        private Label latestDescLabel;
        private ScottPlot.WinForms.FormsPlot pingPlot;
        private Label maxValueLabel;
        private Label maxDescLabel;
        private Button clearButton;
        private NumericUpDown minRttNumeric;
        private Label minRttLabel;
        private Button overlayButton;
        private ComboBox calculationMethodComboBox;
        private Label calculationMethodLabel;
        private System.Windows.Forms.Timer uiTimer;
        private ToolTip toolTip;
    }
}
