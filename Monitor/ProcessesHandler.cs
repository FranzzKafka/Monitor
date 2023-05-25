using System;
using System.Diagnostics;

namespace Monitor
{
    public class ProcessesHandler
    {
        public Process RequiredProcess = null;

        /// <summary>
        /// Method that monitors the system for a process that is running for more than a certain amount of time with a certain frequency.
        /// If it lasts longer than a certain time, the method is killed.
        /// </summary>
        /// <param name="processName">Name of the running process</param>
        /// <param name="maximumLifetimeMinutes">The maximum time a process can run before it terminates</param>
        /// <param name="monitoringFrequencyMinutes">Process check frequency</param>
        public void MonitorProcess(string processName, string maximumLifetimeMinutes, string monitoringFrequencyMinutes)
        {
            int minutes;
            int milisecondsInMinute = 60000;
            
            var timer = new System.Diagnostics.Stopwatch();
            int.TryParse(monitoringFrequencyMinutes, out minutes);
            timer.Start();
            while (true)
            {
                if ((int)timer.ElapsedMilliseconds >= minutes * milisecondsInMinute)
                {
                    if (IsProcessKilled(processName, maximumLifetimeMinutes))
                        Environment.Exit(0);
                    else
                    {
                        timer.Restart();
                        continue;
                    }
                }
            }
        }

        /// <summary>
        /// Method that checks for the presence of a process in the system.
        /// If process exists, return true.
        /// </summary>
        /// <param name="processName">Name of the running process</param>
        public bool IsProcessExists(string processName)
        {
            Process[] processesList = Process.GetProcesses();

            foreach (Process myProcess in processesList)
            {
                if (myProcess.ProcessName == processName)
                {
                    RequiredProcess = myProcess;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Method that kills a process and returns true, if it works longer than the specified time.
        /// </summary>
        /// <param name="process">Process we want to kill</param>
        /// <param name="maximumLifetimeMinutes">The maximum time a process can run before it terminates</param>
        public bool IsProcessKilled(string processName, string maximumLifetimeMinutes)
        {
            int minutes;

            int.TryParse(maximumLifetimeMinutes, out minutes);
            if (IsProcessExists(processName) == false)
            {
                return false;
            }
            else if ((DateTime.Now - RequiredProcess.StartTime).TotalMinutes > minutes && IsProcessExists(processName) != false)
            {
                RequiredProcess.Kill();
                Logger.log.Information($"Process {processName} has been killed");
                return true;
            }
            else
                return false;
        }
    }
}