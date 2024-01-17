using System.Diagnostics;
using System.IO.Compression;

namespace EventLogExtractor.ExtractorLogic.Helpers
{
    public class WindowsHelper
    {
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        public static void ClearOutputDirectory()
        {
            DeleteEmptyDirectories(GlobalExtractorOptions.OutputEventViewerLogsDirectory);
        } 
        
        public static void PrepareOutputDirectory()
        {
            var dir = GlobalExtractorOptions.OutputEventViewerLogsDirectory;
            if (Directory.Exists(dir))
            {
                Directory.Delete(dir, true);
                Directory.CreateDirectory(dir);
            }
            else
                Directory.CreateDirectory(dir);       
        }

        public static void OpenOutputDirectory ()
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    Arguments = $"{GlobalExtractorOptions.OutputEventViewerLogsDirectory}",
                    FileName = "explorer.exe"
                };

                Process.Start(startInfo);

            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        public static void DisplayErrorMessage(string message)
        {
            using (var w = new Form() { Size = new Size(0, 0) })
            {
                Task.Delay(TimeSpan.FromSeconds(5)).ContinueWith((t) => w.Close(), TaskScheduler.FromCurrentSynchronizationContext());
                MessageBox.Show(w, $"{message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
        }

        public static void AddEventToCsvFile(string line)
        {
            try
            {
                using (var file = System.IO.File.AppendText(GlobalExtractorOptions.AllEventsCsvPath))
                {
                  //  file.WriteLine(line.Replace("\n"," ").Replace("\r",""));
                    file.WriteLine(line.Replace("\n"," ").Replace("\r",";"));
                 //   file.WriteLine(line);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex,"Exception on adding event to csv");
            }
        }
       public static void ZipFolder(string sourceFolderPath, string zipFilePath)
        {
            if (!Directory.Exists(sourceFolderPath))
            {
                throw new DirectoryNotFoundException($"Directory '{sourceFolderPath}' not exists.");
            }

            try
            {
                ZipFile.CreateFromDirectory(sourceFolderPath, zipFilePath);
            }
            catch (Exception ex)
            {
               _logger.Error(ex, "Exception on compressing file ");
            }
        }

        static void DeleteEmptyDirectories(string directoryPath)
        {
            try
            {
                foreach (string directory in Directory.GetDirectories(directoryPath))
                {
                    DeleteEmptyDirectories(directory);

                    if (Directory.GetFiles(directory).Length == 0 && Directory.GetDirectories(directory).Length == 0)
                    {
                        Directory.Delete(directory);
                        _logger.Info($"Deleted empty directory: {directory}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Error deleting empty directories: {ex.Message}");
            }
        }
    }
}
