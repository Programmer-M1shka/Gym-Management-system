using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Management_System.Models
{
    internal class LoggingService
    {
        private readonly string _logFilePath = "gym_system_log.txt";

        public void LogEvent(string eventType, string details)
        {
            string logEntry = $"{DateTime.Now} - {eventType}: {details}";

            using (StreamWriter writer = new StreamWriter(_logFilePath, true))
            {
                writer.WriteLine(logEntry);
            }
        }

        public List<string> ReadLogs()
        {
            if (File.Exists(_logFilePath))
            {
                return File.ReadAllLines(_logFilePath).ToList();
            }
            return new List<string>();
        }
    }
}

