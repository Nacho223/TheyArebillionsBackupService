using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace FuckTheyAreBillions
{
    public partial class Service1 : ServiceBase
    {
        private static string[] savegames;
        private static FilesValues[] mySaves;
        private static readonly string path = Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"), "Documents\\My Games\\They Are Billions\\Saves\\");
        private static readonly string myPath = Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"), "Documents\\My Games\\Fuck Numantian Games\\Saves");

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Timer aTimer = new Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 60000;
            aTimer.Enabled = true;
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
            Run();
        }

        protected override void OnStop()
        {
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_STOPPED;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (!Directory.Exists(myPath))
            {
                Directory.CreateDirectory(myPath);
            }
            Run();
        }

        private static void Run()
        {
            if (Directory.Exists(path))
            {
                GetWritten();

                if (savegames == null)
                {
                    savegames = Directory.GetFiles(path);
                }
                string[] temp = Directory.GetFiles(path);
                if (temp.Length != savegames.Length && temp.Length != 0)
                {
                    Copy(temp);
                }
                else
                {
                    if (CheckFiles(temp))
                    {
                        Copy(temp);
                    }
                }
            }
        }
        private static void Copy(string[] saves)
        {
            string dataBackup = string.Empty;
            List<string> filesbackup = new List<string>();
            if (File.Exists(myPath + @"\SaveInfo.txt"))
            {
                int i = File.ReadAllText(myPath + @"\SaveInfo.txt").Length;
                if (i != 0)
                {
                    File.WriteAllText(myPath + @"\SaveInfo.txt", string.Empty);
                }
            }
            else
            {
                File.Create(myPath + @"\SaveInfo.txt");
            }
            string folderName = "\\SaveBackup " + DateTime.Now.ToString();
            folderName = folderName.Replace(":", string.Empty);
            string folder = myPath + folderName;

            folder = folder.Replace("/", string.Empty);
            Directory.CreateDirectory(folder);
            foreach (string s in saves)
            {
                string filename = s.Replace(path, string.Empty);
                File.Copy(s, folder + @"\" + filename);
                filesbackup.Add(filename);
                filesbackup.Add(File.GetLastWriteTime(s).ToString());
            }
            dataBackup = string.Join("|", filesbackup);
            File.WriteAllText(myPath + @"\SaveInfo.txt", dataBackup);
        }

        private static void GetWritten()
        {
            if (File.Exists(myPath + @"\SaveInfo.txt"))
            {
                int i = File.ReadAllText(myPath + @"\SaveInfo.txt").Length;
                if (i != 0)
                {
                    string s = File.ReadAllText(myPath + @"\SaveInfo.txt");
                    string[] allFiles = s.Split('|');
                    mySaves = new FilesValues[allFiles.Length / 2];
                    int counter = 0;
                    for (int j = 0; j < allFiles.Length; j++)
                    {
                        if (j % 2 == 0)
                        {
                            mySaves[counter] = new FilesValues();
                            mySaves[counter].filename = allFiles[j];
                            mySaves[counter].lastmodified = DateTime.Parse(allFiles[j + 1]);
                            counter++;
                        }
                    }

                }
            }
            else
            {
                File.Create(myPath + @"\SaveInfo.txt");
            }
        }
        private static bool CheckFiles(string[] saves)
        {
            bool canCopy = false;
            if (mySaves != null)
            {
                foreach (string s in saves)
                {
                    string r = s.Replace(path, string.Empty);
                    var t = mySaves.Where(a => a.filename == r).Select(a => a.lastmodified);
                    string date = t.ElementAt(0).ToString();
                    string datecompare = File.GetLastWriteTime(s).ToString();
                    if (datecompare != date)
                    {
                        canCopy = true;
                    }
                }

                return canCopy;
            }
            else
            {
                return true;
            }
        }

        public class FilesValues
        {
            public string filename { get; set; }
            public DateTime lastmodified { get; set; }
        }

        public enum ServiceState
        {
            SERVICE_STOPPED = 0x00000001,
            SERVICE_START_PENDING = 0x00000002,
            SERVICE_STOP_PENDING = 0x00000003,
            SERVICE_RUNNING = 0x00000004,
            SERVICE_CONTINUE_PENDING = 0x00000005,
            SERVICE_PAUSE_PENDING = 0x00000006,
            SERVICE_PAUSED = 0x00000007,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ServiceStatus
        {
            public int dwServiceType;
            public ServiceState dwCurrentState;
            public int dwControlsAccepted;
            public int dwWin32ExitCode;
            public int dwServiceSpecificExitCode;
            public int dwCheckPoint;
            public int dwWaitHint;
        };
    }
}
