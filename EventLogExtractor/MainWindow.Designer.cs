namespace EventLogExtractor
{
    partial class MainWindow
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
            startButton = new Button();
            OutputWindow = new RichTextBox();
            label1 = new Label();
            startEventsDateTimePicker = new DateTimePicker();
            EndEventsDateTimePicker = new DateTimePicker();
            label2 = new Label();
            openLogsDirectoryAtEndCheckbox = new CheckBox();
            label3 = new Label();
            _15_min_btn = new Button();
            _30_min_btn = new Button();
            _1_h_btn = new Button();
            _3_h_btn = new Button();
            _24_h_btn = new Button();
            _6_h_btn = new Button();
            SuspendLayout();
            // 
            // startButton
            // 
            startButton.Location = new Point(683, 363);
            startButton.Name = "startButton";
            startButton.Size = new Size(95, 49);
            startButton.TabIndex = 0;
            startButton.Text = "Start";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += startButton_Click;
            // 
            // OutputWindow
            // 
            OutputWindow.Location = new Point(31, 29);
            OutputWindow.Name = "OutputWindow";
            OutputWindow.Size = new Size(729, 92);
            OutputWindow.TabIndex = 1;
            OutputWindow.Text = "";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(47, 238);
            label1.Name = "label1";
            label1.Size = new Size(53, 23);
            label1.TabIndex = 2;
            label1.Text = "From:";
            // 
            // startEventsDateTimePicker
            // 
            startEventsDateTimePicker.Location = new Point(106, 231);
            startEventsDateTimePicker.Name = "startEventsDateTimePicker";
            startEventsDateTimePicker.Size = new Size(285, 30);
            startEventsDateTimePicker.TabIndex = 3;
            // 
            // EndEventsDateTimePicker
            // 
            EndEventsDateTimePicker.Location = new Point(449, 232);
            EndEventsDateTimePicker.Name = "EndEventsDateTimePicker";
            EndEventsDateTimePicker.Size = new Size(292, 30);
            EndEventsDateTimePicker.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(412, 237);
            label2.Name = "label2";
            label2.Size = new Size(31, 23);
            label2.TabIndex = 5;
            label2.Text = "To:";
            // 
            // openLogsDirectoryAtEndCheckbox
            // 
            openLogsDirectoryAtEndCheckbox.AutoSize = true;
            openLogsDirectoryAtEndCheckbox.Checked = true;
            openLogsDirectoryAtEndCheckbox.CheckState = CheckState.Checked;
            openLogsDirectoryAtEndCheckbox.Location = new Point(390, 375);
            openLogsDirectoryAtEndCheckbox.Name = "openLogsDirectoryAtEndCheckbox";
            openLogsDirectoryAtEndCheckbox.Size = new Size(270, 27);
            openLogsDirectoryAtEndCheckbox.TabIndex = 6;
            openLogsDirectoryAtEndCheckbox.Text = "Open output directory on finish";
            openLogsDirectoryAtEndCheckbox.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(31, 162);
            label3.Name = "label3";
            label3.Size = new Size(129, 46);
            label3.TabIndex = 7;
            label3.Text = "Date and time\ndefault options:";
            // 
            // _15_min_btn
            // 
            _15_min_btn.Location = new Point(166, 153);
            _15_min_btn.Name = "_15_min_btn";
            _15_min_btn.Size = new Size(90, 55);
            _15_min_btn.TabIndex = 8;
            _15_min_btn.Text = "Last 15 min";
            _15_min_btn.UseVisualStyleBackColor = true;
            _15_min_btn.Click += _15_min_btn_Click;
            // 
            // _30_min_btn
            // 
            _30_min_btn.Location = new Point(275, 153);
            _30_min_btn.Name = "_30_min_btn";
            _30_min_btn.Size = new Size(90, 55);
            _30_min_btn.TabIndex = 9;
            _30_min_btn.Text = "Last 30 min";
            _30_min_btn.UseVisualStyleBackColor = true;
            _30_min_btn.Click += _30_min_btn_Click;
            // 
            // _1_h_btn
            // 
            _1_h_btn.Location = new Point(380, 153);
            _1_h_btn.Name = "_1_h_btn";
            _1_h_btn.Size = new Size(90, 55);
            _1_h_btn.TabIndex = 10;
            _1_h_btn.Text = "Last 1 hour";
            _1_h_btn.UseVisualStyleBackColor = true;
            _1_h_btn.Click += _1_h_btn_Click;
            // 
            // _3_h_btn
            // 
            _3_h_btn.Location = new Point(486, 153);
            _3_h_btn.Name = "_3_h_btn";
            _3_h_btn.Size = new Size(90, 55);
            _3_h_btn.TabIndex = 11;
            _3_h_btn.Text = "Last 3 hours";
            _3_h_btn.UseVisualStyleBackColor = true;
            _3_h_btn.Click += _3_h_btn_Click;
            // 
            // _24_h_btn
            // 
            _24_h_btn.Location = new Point(698, 153);
            _24_h_btn.Name = "_24_h_btn";
            _24_h_btn.Size = new Size(90, 55);
            _24_h_btn.TabIndex = 12;
            _24_h_btn.Text = "Last 24 hours";
            _24_h_btn.UseVisualStyleBackColor = true;
            _24_h_btn.Click += _24_h_btn_Click;
            // 
            // _6_h_btn
            // 
            _6_h_btn.Location = new Point(592, 153);
            _6_h_btn.Name = "_6_h_btn";
            _6_h_btn.Size = new Size(90, 55);
            _6_h_btn.TabIndex = 13;
            _6_h_btn.Text = "Last 6 hours";
            _6_h_btn.UseVisualStyleBackColor = true;
            _6_h_btn.Click += _6_h_btn_Click;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(_6_h_btn);
            Controls.Add(_24_h_btn);
            Controls.Add(_3_h_btn);
            Controls.Add(_1_h_btn);
            Controls.Add(_30_min_btn);
            Controls.Add(_15_min_btn);
            Controls.Add(label3);
            Controls.Add(openLogsDirectoryAtEndCheckbox);
            Controls.Add(label2);
            Controls.Add(EndEventsDateTimePicker);
            Controls.Add(startEventsDateTimePicker);
            Controls.Add(label1);
            Controls.Add(OutputWindow);
            Controls.Add(startButton);
            Name = "MainWindow";
            Text = "Event Viewer Logs Extractor";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button startButton;
        private RichTextBox OutputWindow;
        private Label label1;
        private DateTimePicker startEventsDateTimePicker;
        private DateTimePicker EndEventsDateTimePicker;
        private Label label2;
        private CheckBox openLogsDirectoryAtEndCheckbox;
        private Label label3;
        private Button _15_min_btn;
        private Button _30_min_btn;
        private Button _1_h_btn;
        private Button _3_h_btn;
        private Button _24_h_btn;
        private Button _6_h_btn;
    }
}