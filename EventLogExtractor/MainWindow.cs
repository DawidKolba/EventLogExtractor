using EventLogExtractor.ExtractorLogic;
using EventLogExtractor.ExtractorLogic.Helpers;
using System.Text;


namespace EventLogExtractor
{
    public partial class MainWindow : Form
    {
        public delegate void ReceivedMessageEventHandler(ExtractorState state);
        private static readonly object ThisLock = new object();

        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public MainWindow()
        {
            InitializeComponent();

            startEventsDateTimePicker.Format = DateTimePickerFormat.Custom;
            startEventsDateTimePicker.CustomFormat = GlobalExtractorOptions.DateTimeFormat;
            startEventsDateTimePicker.Value = DateTime.Now.AddDays(-1);

            EndEventsDateTimePicker.Format = DateTimePickerFormat.Custom;
            EndEventsDateTimePicker.CustomFormat = GlobalExtractorOptions.DateTimeFormat;
            EndEventsDateTimePicker.Value = DateTime.Now;

            try
            {
                this.OutputWindow.Text = "Please use the controls below to set the start date and time, and the end date and time.\n" +
                    "The application will retrieve the logs from the specified period.";
                this.Text = $"Event Viewer Logs Extractor \t \t \t \t  \t \t Version: {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}";
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Cannot get version");
            }
        }

        public void HandleOnReceivedMessage(ExtractorState state)
        {
            try
            {
                UpdateCurrentState(state);
                _logger.Info(state.CurrentWorkingLogsName);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Problem during trying to handle event");
            }
        }

        private void InvokeIfRequired(Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }

        private void ChangeButtonState(bool enabled, Button button)
        {
            InvokeIfRequired(button, () => button.Enabled = enabled);
        }

        private bool CheckCheckboxState(CheckBox checkBox)
        {
            bool output = false;
            InvokeIfRequired(checkBox, () => output = checkBox.Checked);
            return output;
        }

        private void UpdateCurrentState(ExtractorState state)
        {
            try
            {
                var builder = new StringBuilder();
                if (state.CurrentWorkingState == PossibleExtractorStates.Done)
                {
                    builder.AppendLine($"All logs collected");
                    ChangeButtonState(true, this.startButton);

                    lock (ThisLock)
                    {
                        if (CheckCheckboxState(this.openLogsDirectoryAtEndCheckbox))
                            WindowsHelper.OpenOutputDirectory();
                    }
                }

                else
                {
                    builder.AppendLine($"Current progress: {state.PercentageCompleted}%");
                    builder.AppendLine($"Processing: {state.CurrentWorkingLogsName}");
                }

                string text = builder.ToString();

                if (this.OutputWindow.InvokeRequired)
                    this.OutputWindow.BeginInvoke((MethodInvoker)delegate () { this.OutputWindow.Text = text; });
                else
                    this.OutputWindow.Text = text;

            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Exception on update current state");
            }
        }

        private bool CheckForm()
        {
            try
            {
                if (startEventsDateTimePicker.Value > EndEventsDateTimePicker.Value)
                {
                    string msg = $"Start checking if date and time are greater than ending";
                    _logger.Error(msg);
                    this.OutputWindow.Text = msg;
                    WindowsHelper.DisplayErrorMessage(msg);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Exception on checking form");
                string msg = $"Cannot continue:\n{ex}";
                this.OutputWindow.Text = msg;
                WindowsHelper.DisplayErrorMessage(msg);
                return false;
            }
        }

        private async Task StartLogExtractionAsync()
        {
            using (EventViewerExtractor cmdProcess = new EventViewerExtractor())
            {
                cmdProcess.OnStateChangedMessage += new ReceivedMessageEventHandler(HandleOnReceivedMessage);
                await cmdProcess.GetLogsAsync(startEventsDateTimePicker.Value, EndEventsDateTimePicker.Value);
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (!CheckForm())
                return;

            startButton.Enabled = false;
            OutputWindow.Text = "Preparing to get logs from Event Viewer. Please wait...";
            Task.Run(async () => await StartLogExtractionAsync());
        }

        private void _15_min_btn_Click(object sender, EventArgs e)
        {
            startEventsDateTimePicker.Value = DateTime.Now.AddMinutes(-15);
            EndEventsDateTimePicker.Value = DateTime.Now;
        }

        private void _30_min_btn_Click(object sender, EventArgs e)
        {
            startEventsDateTimePicker.Value = DateTime.Now.AddMinutes(-30);
            EndEventsDateTimePicker.Value = DateTime.Now;
        }

        private void _1_h_btn_Click(object sender, EventArgs e)
        {
            startEventsDateTimePicker.Value = DateTime.Now.AddHours(-1);
            EndEventsDateTimePicker.Value = DateTime.Now;
        }

        private void _3_h_btn_Click(object sender, EventArgs e)
        {
            startEventsDateTimePicker.Value = DateTime.Now.AddHours(-3);
            EndEventsDateTimePicker.Value = DateTime.Now;
        }

        private void _6_h_btn_Click(object sender, EventArgs e)
        {
            startEventsDateTimePicker.Value = DateTime.Now.AddHours(-6);
            EndEventsDateTimePicker.Value = DateTime.Now;
        }

        private void _24_h_btn_Click(object sender, EventArgs e)
        {
            startEventsDateTimePicker.Value = DateTime.Now.AddHours(-24);
            EndEventsDateTimePicker.Value = DateTime.Now;
        }
    }
}