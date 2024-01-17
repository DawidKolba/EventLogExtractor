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
            startEventsDateTimePicker.CustomFormat = "yyyy-MM-dd HH:mm";
            startEventsDateTimePicker.Value = DateTime.Now.AddDays(-1);

            EndEventsDateTimePicker.Format = DateTimePickerFormat.Custom;
            EndEventsDateTimePicker.CustomFormat = "yyyy-MM-dd HH:mm";
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
                _logger.Error(ex, "Problem during trying to handle event", ex);
            }
        }

        bool ChceckCheckboxState(CheckBox checkBox)
        {
            bool output = false;
            if (checkBox.InvokeRequired)
                checkBox.Invoke((MethodInvoker)delegate () { output = checkBox.Checked; });
            else
                output = checkBox.Checked;

            return output;
        }

        void UpdateCurrentState(ExtractorState state)
        {
            try
            {
                var builder = new StringBuilder();
                if (state.CurrentWorkingState == PossibleExtractorStates.Done)
                {
                    builder.AppendLine($"All logs grabbed");
                    changeButtonState(true, this.startButton);

                    lock (ThisLock)
                    {
                        if (ChceckCheckboxState(this.openLogsDirectoryAtEndCheckbox))
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

        private void changeButtonState(bool enabled, Button button)
        {
            if (button.InvokeRequired)
                button.BeginInvoke((MethodInvoker)delegate () { button.Enabled = enabled; });
            else
                button.Enabled = enabled;
        }

        bool CheckForm()
        {
            try
            {
                if (startEventsDateTimePicker.Value > EndEventsDateTimePicker.Value)
                {
                    string msg = $"Starting date and time is greater than ending";
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


        private void startButton_Click(object sender, EventArgs e)
        {
            if (!CheckForm())
                return;

            startButton.Enabled = false;
            OutputWindow.Text = "Preparing to grabb logs from Event Viewer. Please wait...";
            Task.Factory.StartNew(() =>
            {
                using (EventViewerExtractor cmdProcess = new EventViewerExtractor())
                {
                    cmdProcess.OnStateChangedMessage += new ReceivedMessageEventHandler(HandleOnReceivedMessage);
                    cmdProcess.GetLogs(startEventsDateTimePicker.Value, EndEventsDateTimePicker.Value);
                }
            });
        }

    }
}