using System.Diagnostics;
using System.IO;
using System.Threading;
using NUnit.Framework;

namespace Monitor
{
    [TestFixture]
    public class UnitTests
    {
        string ProccesName = "Monitor";
        string NotExistedProcessName = "asdasdf";
        string MaximumAllowableProcessLifetimeMin = "1";
        int MillisecondsInOneMinute = 60000;

        ProcessesHandler Handler = new ProcessesHandler();

        [Test]
        [Description("Сheck that the method IsProcessExists returns true, if the process exists in the system")]
        public void IsProcessExistsExistentProcessTrueTest()
        {
            bool expectedResult = true;
            
            Process.Start(ProccesName);
            bool actualResult = Handler.IsProcessExists(ProccesName);
            UnitTestsHelpers.KillProcess(ProccesName);
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        [Description("Сheck that the method IsProcessExists returns false, if the process  not exists in the system")]
        public void IsProcessExistsNonExistentProcessFalseTest()
        {
            bool expectedResult = false;
            
            bool actualResult = Handler.IsProcessExists(NotExistedProcessName);
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        [Description("Check that the method IsProcessKilled returns true if the process exists more than the specified time")]
        public void IsProcessKilledMoreThanSpecifiedTimeProcessTrueTest()
        {
            bool expectedResult = true;
            
            Process.Start(ProccesName);
            Thread.Sleep(MillisecondsInOneMinute);
            bool actualResult = Handler.IsProcessKilled(ProccesName, MaximumAllowableProcessLifetimeMin);
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        [Description("Check that the method IsProcessKilled returns false if the process does not exist")]
        public void IsProcessKilledNonExistentProcessFalseTest()
        {
            bool expectedResult = false;
            
            Thread.Sleep(MillisecondsInOneMinute);
            bool actualResult = Handler.IsProcessKilled(NotExistedProcessName, MaximumAllowableProcessLifetimeMin);
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }
        
        [Test]
        [Description("Check that the method IsProcessKilled returns false if the process exists less than specified time")]
        public void IsProcessKilledExistsLessThanSpecifiedTimetProcessFalseTest()
        {
            bool expectedResult = false;
            
            Process.Start(ProccesName);
            bool actualResult = Handler.IsProcessKilled(NotExistedProcessName, MaximumAllowableProcessLifetimeMin);
            UnitTestsHelpers.KillProcess(ProccesName);
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        [Description("Check that the method IsProcessKilled kills the process")]
        public void IsProcessKilledTerminatesProcessTest()
        {
            int expectedResult = 0;
            
            Process.Start(ProccesName);
            Thread.Sleep(MillisecondsInOneMinute);
            Handler.IsProcessKilled(ProccesName, MaximumAllowableProcessLifetimeMin);
            Process[] processesArr = Process.GetProcessesByName(ProccesName);
            int actualResult = processesArr.Length;
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        [Description("Check that method IsProcessKilled saves log.txt file if process has been killed")]
        public void IsProcessKilledSafesLogFileTest()
        {
            string expectedLogFilePath = @"C:\Users\e.sarkisyan\RiderProjects\Monitor\Monitor\bin\Debug\net6.0\log-20230525.txt";
            bool expectedResult = true;
            
            Process.Start(ProccesName);
            Thread.Sleep(MillisecondsInOneMinute);
            Handler.IsProcessKilled(ProccesName, MaximumAllowableProcessLifetimeMin);
            bool actualResult = File.Exists(expectedLogFilePath);
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }
    }

    public static class UnitTestsHelpers
    {
        public static void KillProcess(string processName)
        {
            Process[] processesArr = Process.GetProcessesByName(processName);
            processesArr[0].Kill();
        }
    }
}