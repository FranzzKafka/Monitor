using System;
using Monitor;

string ProcessName;
string MaximumLifetimeMinutes;
string MonitoringFrequencyMinutes;
ProcessesHandler processesHandler = new ProcessesHandler();

Console.WriteLine("Hello! To find and kill the desired process, input the name of the process and press enter. Press ctrl+c buttons to exit the program anytime");
ProcessName = Console.ReadLine();
Console.WriteLine("Input maximum allowed process time in minutes and press enter");
MaximumLifetimeMinutes = Console.ReadLine();
Console.WriteLine("Input frequency of checking for the presence of a process in minutes and press enter");
MonitoringFrequencyMinutes = Console.ReadLine();
processesHandler.MonitorProcess(ProcessName, MaximumLifetimeMinutes, MonitoringFrequencyMinutes);