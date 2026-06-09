namespace TcpPingMonitorNet10
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
            settingGroup = new GroupBox();
            tableLayoutPanel = new TableLayoutPanel();
            calculationMethodComboBox = new ComboBox();
            calculationMethodLabel = new Label();
            thresholdTextBox = new TextBox();
            thresholdLabel = new Label();
            portLabel = new Label();
            IpMaskLabel = new Label();
            NetworkInterfaceLabel = new Label();
            networkComboBox = new ComboBox();
            ipMaskTextBox = new TextBox();
            portTextBox = new TextBox();
            openOverlayButton = new Button();
            clearButton = new Button();
            startButton = new Button();
            dataGroup = new GroupBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            peaksValueLabel = new Label();
            peaksDescLabel = new Label();
            avgValueLabel = new Label();
            latestValueLabel = new Label();
            avgDescLabel = new Label();
            latestDescLabel = new Label();
            pingPlot = new ScottPlot.WinForms.FormsPlot();
            packetLossDescLabel = new Label();
            packetLossValueLabel = new Label();
            settingGroup.SuspendLayout();
            tableLayoutPanel.SuspendLayout();
            dataGroup.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // settingGroup
            // 
            settingGroup.Controls.Add(tableLayoutPanel);
            settingGroup.Dock = DockStyle.Top;
            settingGroup.Font = new Font("Segoe UI", 12F);
            settingGroup.Location = new Point(8, 8);
            settingGroup.Name = "settingGroup";
            settingGroup.Padding = new Padding(8);
            settingGroup.Size = new Size(468, 246);
            settingGroup.TabIndex = 0;
            settingGroup.TabStop = false;
            settingGroup.Text = "Settings";
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 3;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel.Controls.Add(calculationMethodComboBox, 0, 5);
            tableLayoutPanel.Controls.Add(calculationMethodLabel, 0, 4);
            tableLayoutPanel.Controls.Add(thresholdTextBox, 2, 1);
            tableLayoutPanel.Controls.Add(thresholdLabel, 2, 0);
            tableLayoutPanel.Controls.Add(portLabel, 1, 0);
            tableLayoutPanel.Controls.Add(IpMaskLabel, 0, 0);
            tableLayoutPanel.Controls.Add(NetworkInterfaceLabel, 0, 2);
            tableLayoutPanel.Controls.Add(networkComboBox, 0, 3);
            tableLayoutPanel.Controls.Add(ipMaskTextBox, 0, 1);
            tableLayoutPanel.Controls.Add(portTextBox, 1, 1);
            tableLayoutPanel.Controls.Add(openOverlayButton, 2, 6);
            tableLayoutPanel.Controls.Add(clearButton, 1, 6);
            tableLayoutPanel.Controls.Add(startButton, 0, 6);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(8, 30);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 7;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel.Size = new Size(452, 208);
            tableLayoutPanel.TabIndex = 0;
            // 
            // calculationMethodComboBox
            // 
            calculationMethodComboBox.BackColor = SystemColors.ControlLight;
            tableLayoutPanel.SetColumnSpan(calculationMethodComboBox, 3);
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
            tableLayoutPanel.SetColumnSpan(calculationMethodLabel, 3);
            calculationMethodLabel.Dock = DockStyle.Fill;
            calculationMethodLabel.Location = new Point(0, 112);
            calculationMethodLabel.Margin = new Padding(0);
            calculationMethodLabel.Name = "calculationMethodLabel";
            calculationMethodLabel.Size = new Size(452, 24);
            calculationMethodLabel.TabIndex = 11;
            calculationMethodLabel.Text = "Calculation Method";
            calculationMethodLabel.TextAlign = ContentAlignment.BottomLeft;
            // 
            // thresholdTextBox
            // 
            thresholdTextBox.BackColor = SystemColors.ControlLight;
            thresholdTextBox.BorderStyle = BorderStyle.FixedSingle;
            thresholdTextBox.Dock = DockStyle.Fill;
            thresholdTextBox.ForeColor = SystemColors.ControlText;
            thresholdTextBox.Location = new Point(304, 24);
            thresholdTextBox.Margin = new Padding(4, 0, 4, 0);
            thresholdTextBox.Name = "thresholdTextBox";
            thresholdTextBox.Size = new Size(144, 29);
            thresholdTextBox.TabIndex = 9;
            thresholdTextBox.Text = "0.1";
            thresholdTextBox.KeyPress += thresholdTextBox_KeyPress;
            thresholdTextBox.Leave += thresholdTextBox_Leave;
            // 
            // thresholdLabel
            // 
            thresholdLabel.AutoSize = true;
            thresholdLabel.Dock = DockStyle.Fill;
            thresholdLabel.Location = new Point(300, 0);
            thresholdLabel.Margin = new Padding(0);
            thresholdLabel.Name = "thresholdLabel";
            thresholdLabel.Size = new Size(152, 24);
            thresholdLabel.TabIndex = 8;
            thresholdLabel.Text = "Threshold";
            thresholdLabel.TextAlign = ContentAlignment.BottomLeft;
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
            // IpMaskLabel
            // 
            IpMaskLabel.AutoSize = true;
            IpMaskLabel.Dock = DockStyle.Fill;
            IpMaskLabel.Location = new Point(0, 0);
            IpMaskLabel.Margin = new Padding(0);
            IpMaskLabel.Name = "IpMaskLabel";
            IpMaskLabel.Size = new Size(150, 24);
            IpMaskLabel.TabIndex = 0;
            IpMaskLabel.Text = "IP Mask";
            IpMaskLabel.TextAlign = ContentAlignment.BottomLeft;
            // 
            // NetworkInterfaceLabel
            // 
            NetworkInterfaceLabel.AutoSize = true;
            tableLayoutPanel.SetColumnSpan(NetworkInterfaceLabel, 3);
            NetworkInterfaceLabel.Dock = DockStyle.Fill;
            NetworkInterfaceLabel.Location = new Point(0, 56);
            NetworkInterfaceLabel.Margin = new Padding(0);
            NetworkInterfaceLabel.Name = "NetworkInterfaceLabel";
            NetworkInterfaceLabel.Size = new Size(452, 24);
            NetworkInterfaceLabel.TabIndex = 2;
            NetworkInterfaceLabel.Text = "Network Interface";
            NetworkInterfaceLabel.TextAlign = ContentAlignment.BottomLeft;
            // 
            // networkComboBox
            // 
            networkComboBox.BackColor = SystemColors.ControlLight;
            tableLayoutPanel.SetColumnSpan(networkComboBox, 3);
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
            ipMaskTextBox.PlaceholderText = "127.0.0.0/24";
            ipMaskTextBox.Size = new Size(142, 29);
            ipMaskTextBox.TabIndex = 4;
            ipMaskTextBox.Text = "123.253.172.0/24";
            // 
            // portTextBox
            // 
            portTextBox.BackColor = SystemColors.ControlLight;
            portTextBox.BorderStyle = BorderStyle.FixedSingle;
            portTextBox.Dock = DockStyle.Fill;
            portTextBox.ForeColor = SystemColors.ControlText;
            portTextBox.Location = new Point(154, 24);
            portTextBox.Margin = new Padding(4, 0, 4, 0);
            portTextBox.Name = "portTextBox";
            portTextBox.PlaceholderText = "3000";
            portTextBox.Size = new Size(142, 29);
            portTextBox.TabIndex = 5;
            portTextBox.Text = "8889";
            // 
            // openOverlayButton
            // 
            openOverlayButton.BackColor = SystemColors.ControlLight;
            openOverlayButton.Dock = DockStyle.Fill;
            openOverlayButton.Font = new Font("Segoe UI", 12F);
            openOverlayButton.ForeColor = SystemColors.ControlText;
            openOverlayButton.Location = new Point(303, 171);
            openOverlayButton.Name = "openOverlayButton";
            openOverlayButton.Size = new Size(146, 34);
            openOverlayButton.TabIndex = 10;
            openOverlayButton.Text = "Overlay";
            openOverlayButton.UseVisualStyleBackColor = false;
            openOverlayButton.Click += openOverlayButton_Click;
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
            dataGroup.Controls.Add(tableLayoutPanel2);
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
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 4;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.Controls.Add(peaksValueLabel, 2, 1);
            tableLayoutPanel2.Controls.Add(peaksDescLabel, 2, 0);
            tableLayoutPanel2.Controls.Add(avgValueLabel, 1, 1);
            tableLayoutPanel2.Controls.Add(latestValueLabel, 0, 1);
            tableLayoutPanel2.Controls.Add(avgDescLabel, 1, 0);
            tableLayoutPanel2.Controls.Add(latestDescLabel, 0, 0);
            tableLayoutPanel2.Controls.Add(pingPlot, 0, 2);
            tableLayoutPanel2.Controls.Add(packetLossDescLabel, 3, 0);
            tableLayoutPanel2.Controls.Add(packetLossValueLabel, 3, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(8, 30);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(452, 316);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // peaksValueLabel
            // 
            peaksValueLabel.AutoSize = true;
            peaksValueLabel.Dock = DockStyle.Fill;
            peaksValueLabel.Location = new Point(226, 24);
            peaksValueLabel.Margin = new Padding(0);
            peaksValueLabel.Name = "peaksValueLabel";
            peaksValueLabel.Size = new Size(113, 24);
            peaksValueLabel.TabIndex = 8;
            peaksValueLabel.TextAlign = ContentAlignment.BottomLeft;
            // 
            // peaksDescLabel
            // 
            peaksDescLabel.AutoSize = true;
            peaksDescLabel.Dock = DockStyle.Fill;
            peaksDescLabel.Location = new Point(226, 0);
            peaksDescLabel.Margin = new Padding(0);
            peaksDescLabel.Name = "peaksDescLabel";
            peaksDescLabel.Size = new Size(113, 24);
            peaksDescLabel.TabIndex = 7;
            peaksDescLabel.Text = "Max";
            peaksDescLabel.TextAlign = ContentAlignment.BottomLeft;
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
            tableLayoutPanel2.SetColumnSpan(pingPlot, 4);
            pingPlot.Dock = DockStyle.Fill;
            pingPlot.Location = new Point(3, 51);
            pingPlot.Name = "pingPlot";
            pingPlot.Size = new Size(446, 262);
            pingPlot.TabIndex = 6;
            // 
            // packetLossDescLabel
            // 
            packetLossDescLabel.AutoSize = true;
            packetLossDescLabel.Dock = DockStyle.Fill;
            packetLossDescLabel.Location = new Point(339, 0);
            packetLossDescLabel.Margin = new Padding(0);
            packetLossDescLabel.Name = "packetLossDescLabel";
            packetLossDescLabel.Size = new Size(113, 24);
            packetLossDescLabel.TabIndex = 2;
            packetLossDescLabel.Text = "Timeout %";
            packetLossDescLabel.TextAlign = ContentAlignment.BottomLeft;
            // 
            // packetLossValueLabel
            // 
            packetLossValueLabel.AutoSize = true;
            packetLossValueLabel.Dock = DockStyle.Fill;
            packetLossValueLabel.Location = new Point(339, 24);
            packetLossValueLabel.Margin = new Padding(0);
            packetLossValueLabel.Name = "packetLossValueLabel";
            packetLossValueLabel.Size = new Size(113, 24);
            packetLossValueLabel.TabIndex = 5;
            packetLossValueLabel.TextAlign = ContentAlignment.BottomLeft;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(484, 616);
            Controls.Add(dataGroup);
            Controls.Add(settingGroup);
            ForeColor = SystemColors.ControlText;
            MinimumSize = new Size(500, 655);
            Name = "MainForm";
            Padding = new Padding(8);
            Text = "TCP RTT";
            FormClosing += MainForm_FormClosing;
            settingGroup.ResumeLayout(false);
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            dataGroup.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox settingGroup;
        private TableLayoutPanel tableLayoutPanel;
        private GroupBox dataGroup;
        private TableLayoutPanel tableLayoutPanel2;
        private Label portLabel;
        private Label IpMaskLabel;
        private Label NetworkInterfaceLabel;
        private ComboBox networkComboBox;
        private TextBox ipMaskTextBox;
        private TextBox portTextBox;
        private Button startButton;
        private Label packetLossValueLabel;
        private Label avgValueLabel;
        private Label latestValueLabel;
        private Label packetLossDescLabel;
        private Label avgDescLabel;
        private Label latestDescLabel;
        private ScottPlot.WinForms.FormsPlot pingPlot;
        private Label peaksValueLabel;
        private Label peaksDescLabel;
        private Button clearButton;
        private TextBox thresholdTextBox;
        private Label thresholdLabel;
        private Button openOverlayButton;
        private ComboBox calculationMethodComboBox;
        private Label calculationMethodLabel;
    }
}
