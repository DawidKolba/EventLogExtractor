# EventLogExtractor

EventLogExtractor is a Windows application developed using .NET 6.0, designed to efficiently extract logs from the Windows Event Log within a specified time range. This tool is especially useful for users who need to retrieve and analyze system logs over custom time periods.

## Key Features

- **Custom Time Range Selection**: Users can specify a start and end time for extracting logs, providing flexibility for various use cases.
- **Quick Time Interval Selection**: Includes convenient buttons for common intervals such as the last 15 minutes, 30 minutes, 1 hour, etc.
- **Comprehensive Log Extraction**: Capable of extracting both standard logs (System, Application, Security) and custom logs based on provider names.
- **Automatic Log Compression**: Extracted logs are automatically compressed into an archive for efficient storage and handling.
- **User-Friendly Interface**: The application offers a straightforward and intuitive user interface, displaying real-time progress updates during the extraction process.

## Getting Started

1. Clone the repository to your local machine.
2. Ensure you have .NET 6.0 installed.
3. Build and run the application using a compatible .NET environment.
4. Use the interface to set the desired time range for log extraction.
5. Click 'Start' to initiate the log retrieval process. Extracted logs will be saved in a designated directory.

## Dependencies

- .NET 6.0
- Windows Forms for the user interface
- NLog for internal logging

## Usage Scenarios

This tool is ideal for system administrators, IT professionals, and anyone who needs to periodically review or archive system logs. Whether for troubleshooting, monitoring, or compliance purposes, EventLogExtractor offers a convenient way to access and manage Windows event logs.

## Note

This project is developed as a part of my professional portfolio. Feel free to explore the code, and suggestions for improvements or collaborations are always welcome.
