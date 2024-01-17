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
            SuspendLayout();
            // 
            // button1
            // 
            startButton.Location = new Point(683, 363);
            startButton.Name = "button1";
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
            // dateTimePicker1
            // 
            startEventsDateTimePicker.Location = new Point(106, 231);
            startEventsDateTimePicker.Name = "dateTimePicker1";
            startEventsDateTimePicker.Size = new Size(285, 30);
            startEventsDateTimePicker.TabIndex = 3;
            // 
            // dateTimePicker2
            // 
            EndEventsDateTimePicker.Location = new Point(449, 232);
            EndEventsDateTimePicker.Name = "dateTimePicker2";
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
            // checkBox1
            // 
            openLogsDirectoryAtEndCheckbox.AutoSize = true;
            openLogsDirectoryAtEndCheckbox.Checked = true;
            openLogsDirectoryAtEndCheckbox.CheckState = CheckState.Checked;
            openLogsDirectoryAtEndCheckbox.Location = new Point(390, 375);
            openLogsDirectoryAtEndCheckbox.Name = "checkBox1";
            openLogsDirectoryAtEndCheckbox.Size = new Size(270, 27);
            openLogsDirectoryAtEndCheckbox.TabIndex = 6;
            openLogsDirectoryAtEndCheckbox.Text = "Open output directory on finish";
            openLogsDirectoryAtEndCheckbox.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(47, 186);
            label3.Name = "label3";
            label3.Size = new Size(185, 23);
            label3.TabIndex = 7;
            label3.Text = "Date and time options:";
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
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
    }
}