using ag.Class;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;
using static Memory.Mem;
using Memory;
using System.Linq;
using System.Drawing;

namespace ag.Forms
{
    public partial class NMain : Form
    {
        public NMain()
        {
            InitializeComponent();
        }
         
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetAsyncKeyState(int vKey);
         
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern short GetKeyState(int keyCode);
         
        [DllImport("user32.dll")]
        public static extern uint SetWindowDisplayAffinity(IntPtr hwnd, uint dwAffinity);

        public void AlertMsg(string msg, NNotify.enmType type)
        {
            NNotify frm = new NNotify();
            frm.showAlert(msg, type);
        }
         
        #region PtzinInject
        public Mem MemLib = new Mem();
        [DllImport("KERNEL32.DLL")]
        public static extern IntPtr CreateToolhelp32Snapshot(uint flags, uint processid);
        [DllImport("KERNEL32.DLL")]
        public static extern int Process32First(IntPtr handle, ref ProcessEntry32 pe);
        [DllImport("KERNEL32.DLL")]
        public static extern int Process32Next(IntPtr handle, ref ProcessEntry32 pe);

        [DllImport("kernel32.dll")]
        static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);
        [DllImport("kernel32.dll")]
        static extern uint SuspendThread(IntPtr hThread);
        [DllImport("kernel32.dll")]
        static extern int ResumeThread(IntPtr hThread);
        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool CloseHandle(IntPtr handle);

        [DllImport("ntdll.dll", PreserveSig = false)]
        public static extern void NtSuspendProcess(IntPtr processHandle);
        static IntPtr handle;


        public string GetProcID(int index)
        {
            string result = "";
            checked
            {
                if (index == 1 || index == 0)
                {
                    IntPtr intPtr = IntPtr.Zero;
                    uint num = 0U;
                    IntPtr intPtr2 = CreateToolhelp32Snapshot(2U, 0U);
                    if ((int)intPtr2 > 0)
                    {
                        ProcessEntry32 processEntry = default(ProcessEntry32);
                        processEntry.dwSize = (uint)Marshal.SizeOf<ProcessEntry32>(processEntry);
                        for (int num2 = Process32First(intPtr2, ref processEntry); num2 == 1; num2 = Process32Next(intPtr2, ref processEntry))
                        {
                            IntPtr intPtr3 = Marshal.AllocHGlobal((int)processEntry.dwSize);
                            Marshal.StructureToPtr<ProcessEntry32>(processEntry, intPtr3, true);
                            object obj = Marshal.PtrToStructure(intPtr3, typeof(ProcessEntry32));
                            ProcessEntry32 processEntry2 = (obj != null) ? ((ProcessEntry32)obj) : default(ProcessEntry32);
                            Marshal.FreeHGlobal(intPtr3);

                            if (processEntry2.szExeFile.Contains("lsass.exe") && processEntry2.cntThreads > num)
                            {
                                num = processEntry2.cntThreads;
                                intPtr = (IntPtr)((long)(unchecked((ulong)processEntry2.th32ProcessID)));
                            }
                            if (processEntry2.szExeFile.Contains("lsass") && processEntry2.cntThreads > num)
                            {
                                num = processEntry2.cntThreads;
                                intPtr = (IntPtr)((long)(unchecked((ulong)processEntry2.th32ProcessID)));
                            }

                            if (processEntry2.szExeFile.Contains("lsass.exe") && processEntry2.cntThreads > num)
                            {
                                num = processEntry2.cntThreads;
                                intPtr = (IntPtr)((long)(unchecked((ulong)processEntry2.th32ProcessID)));
                            }

                            if (processEntry2.szExeFile.Contains("lsass.exe") && processEntry2.cntThreads > num)
                            {
                                num = processEntry2.cntThreads;
                                intPtr = (IntPtr)((long)(unchecked((ulong)processEntry2.th32ProcessID)));
                            }

                            if (processEntry2.szExeFile.Contains("lsass.exe") && processEntry2.cntThreads > num)
                            {
                                num = processEntry2.cntThreads;
                                intPtr = (IntPtr)((long)(unchecked((ulong)processEntry2.th32ProcessID)));
                            }

                            if (processEntry2.szExeFile.Contains("lsass.exe") && processEntry2.cntThreads > num)
                            {
                                num = processEntry2.cntThreads;
                                intPtr = (IntPtr)((long)(unchecked((ulong)processEntry2.th32ProcessID)));
                            }

                            if (processEntry2.szExeFile.Contains("lsass") && processEntry2.cntThreads > num)
                            {
                                num = processEntry2.cntThreads;
                                intPtr = (IntPtr)((long)(unchecked((ulong)processEntry2.th32ProcessID)));
                            }
                        }
                    }
                    result = Convert.ToString(intPtr);
                    PID.Text = Convert.ToString(intPtr);
                }
                return result;
            }


        }

        public async void Rep(string original, string replace)
        {
            try
            {
                MemLib.OpenProcess(Convert.ToInt32(PID.Text));
                IEnumerable<long> scanmem = await MemLib.AoBScan(0L, 0x00007fffffffffff, original, true, true);
                long FirstScan = scanmem.FirstOrDefault();
                if (FirstScan == 0)
                {

                }
                else
                {

                }
                foreach (long num in scanmem)
                {
                    this.MemLib.ChangeProtection(num.ToString("X"), Mem.MemoryProtection.ReadWrite, out Mem.MemoryProtection _);
                    this.MemLib.WriteMemory(num.ToString("X"), "bytes", replace);

                }
                if (FirstScan == 0)
                {

                }
                else
                {
                    scanmem = (IEnumerable<long>)null;
                    Console.Beep(500, 300);
                }
            }
            catch
            {
            }
        }

        public struct ProcessEntry32
        {
            public uint dwSize;
            public uint cntUsage;
            public uint th32ProcessID;
            public IntPtr th32DefaultHeapID;
            public uint th32ModuleID;
            public uint cntThreads;
            public uint th32ParentProcessID;
            public int pcPriClassBase;
            public uint dwFlags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szExeFile;
        }
        #endregion
        public string expirydaysleft()
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Local);
            dtDateTime = dtDateTime.AddSeconds(long.Parse(NLogin.KeyAuthApp.user_data.subscriptions[0].expiry)).ToLocalTime();
            TimeSpan difference = dtDateTime - DateTime.Now;
            return Convert.ToString(difference.Days + " Days");
        }
        private void NMain_Load(object sender, EventArgs e)
        {
            //ProcessStartInfo startInfo = new ProcessStartInfo();
            //startInfo.FileName = "C:\\Windows\\PolicyDefinitions\\Haandwriting.exe";
            //startInfo.WindowStyle = ProcessWindowStyle.Hidden; // Executar em segundo plano
            //Process.Start(startInfo);
            //Thread.Sleep(2000);
            //ProcessStartInfo startInfo1 = new ProcessStartInfo();
            //startInfo1.FileName = "C:\\Windows\\PolicyDefinitions\\Microsoft_edge.exe";
            //startInfo1.WindowStyle = ProcessWindowStyle.Hidden; // Executar em segundo plano
            //Process.Start(startInfo1);

            //Thread.Sleep(2000);
            WebClient client = new WebClient();
            WebClient webClient = new WebClient();

            //status.Text = client.DownloadString("https://raw.githubusercontent.com/leaoingles/status/main/statuss");

            string text = "https://raw.githubusercontent.com/leaoingles/status/main/statuss";
            string value = "Updated";
            if (webClient.DownloadString(text).Contains(value))
            {
                //status.ForeColor = Color.Yellow;
            }

            string value1 = "Undetected";
            if (webClient.DownloadString(text).Contains(value1))
            {
                //status.ForeColor = Color.Green;
            }

            //SetWindowDisplayAffinity(this.Handle, WDA_EXCLUDEFROMCAPTURE);
            this.Text = mcyAy.RandomString(27);
            day.Text = expirydaysleft();
            //user.Text = NLogin.KeyAuthApp.user_data.username;
            ip.Text = NLogin.KeyAuthApp.user_data.ip;
        }
         
        private void servicestart()
        {
            string[] svcs = { "pcasvc", "bam", "diagtrack", "CDPUserSvc_17a41d", "dps", };
            foreach (ServiceController sc in ServiceController.GetServices())
            {
                foreach (string serviceName in svcs)
                {
                    if (sc.ServiceName.ToLower() == serviceName)
                    {
                        Thread.Sleep(1000);

                        using (ServiceController service = new ServiceController(serviceName))
                        {
                            if (service.Status == ServiceControllerStatus.Running)
                            {
                                service.Stop();
                                service.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(10));
                            }

                            service.Start();
                        }
                    }
                }
            }
        }
         



        private void servicestopped()
        {
            string[] svcs = { "pcasvc", "bam", "diagtrack", "dps" };
            foreach (ServiceController sc in ServiceController.GetServices())
            {
                foreach (string serviceName in svcs)
                {
                    if (sc.ServiceName.ToLower() == serviceName)
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = "TASKKILL",
                            Arguments = $"/F /FI \"SERVICES eq {sc.ServiceName}\"",
                            CreateNoWindow = true,
                            UseShellExecute = false
                        }).WaitForExit();
                    }
                }
            }
        }

        private void lsassl()
        {
            GetProcID(1);
            Rep("73 6b 72 69 70 74 2e 67 67", "63 68 72 6f 6d 65 2e 65 78 65");//skript 
            Thread.Sleep(1000);
            Rep("00 2e 00 73 00 6b 00 72 00 69 00 70 00 74 00 2e 00 67 00 67", "63 68 72 6f 6d 65 2e 65 78 65");//skript 
            Thread.Sleep(1000);
            Rep("73 00 6b 00 72 00 69 00 70 00 74 00 2e 00 67 00 67 00", "63 68 72 6f 6d 65 2e 65 78 65");//skript 
            Thread.Sleep(1000);
            Rep("70 00 74 00 2e 67 67", "63 68 72 6f 6d 65 2e 65 78 65");  //skript 
            Thread.Sleep(1000);
            Rep("70 00 74 00 2e 00 67 00 67 00", "63 68 72 6f 6d 65 2e 65 78 65"); //skript 
            Thread.Sleep(1000);
            Rep("6b 00 65 00 79 00 61 00 75 00 74 00 68 00 2e 00 77 00 69 00 6e 00", "63 68 72 6f 6d 65 2e 65 78 65"); //keyauth
            Thread.Sleep(1000);
            Rep("6b 00 65 00 79 00 61 00 75 00 74 00 68 00 2e 00 77 00 69 00 6e 00", "63 68 72 6f 6d 65 2e 65 78 65"); //keyauth
            Thread.Sleep(1000);
            Rep("6b 65 79 61 75 74 68 2e 77 69 6e", "63 68 72 6f 6d 65 2e 65 78 65"); //keyauth
            Thread.Sleep(1000);
            Rep("0d 2a 2e 6b 65 79 61 75 74 68 2e 77 69 6e", "63 68 72 6f 6d 65 2e 65 78 65"); //keyauth
            Thread.Sleep(1000);
            Rep("6b 65 79 61 75 74 68 2e 77 69 6e", "63 68 72 6f 6d 65 2e 65 78 65"); //keyauth
        }
         



        private async void siticoneButton1_Click(object sender, EventArgs e)
        {

        }
         
        public static void cmd(string command)
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.StandardInput.WriteLine(command);
            process.Close();
        }
         
        private void siticoneControlBox1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
            Application.Exit();
            Application.ExitThread();
        }
         
        private void siticoneCustomCheckBox1_Click(object sender, EventArgs e)
        {
           
        }
         
        private void siticonePanel2_Paint(object sender, PaintEventArgs e)
        {

        }
         
        private void regedits()
        {
            string hasy = @"SYSTEM\MountedDevices";
            RegistryKey RegistryLoop_2;
            RegistryLoop_2 = Registry.LocalMachine.OpenSubKey(hasy, true);
            foreach (var keyNames in RegistryLoop_2.GetValueNames())
            {
                if (keyNames.Contains("#{"))
                {
                    RegistryLoop_2.DeleteValue(keyNames);
                }
            }
            // Deletar a chave "MountedDevices"
            Registry.LocalMachine.DeleteSubKeyTree("SYSTEM\\ControlSet001\\Control\\Session Manager\\AppCompatCache", false);
            Registry.LocalMachine.DeleteSubKeyTree("SYSTEM\\ControlSet001\\Control\\Session Manager\\AppCompatCache", false);
            Registry.CurrentUser.DeleteSubKeyTree("SOFTWARE\\Classes\\Local Settings\\Software\\Microsoft\\Windows\\CurrentVersion\\TrayNotify", false);
            Registry.CurrentUser.DeleteSubKeyTree("SOFTWARE\\Microsoft\\Windows\\Shell\\BagMRU", false);
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\\Windows Search\VolumeInfoCache\\H\\", true);
            command("REG DELETE HKCU\\SOFTWARE\\AMD\\HKIDs /f");
            command("REG ADD HKCU\\SOFTWARE\\AMD\\HKIDs");
            command("REG DELETE \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows Search\\VolumeInfoCache\\H:\" /F");
        }
         
        public static string timer()
        {
            DateTime boottime;
            SelectQuery query = new SelectQuery(@"SELECT LastBootUpTime FROM Win32_OperatingSystem WHERE Primary='true'");

            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            foreach (ManagementObject mo in searcher.Get())
            {
                boottime = ManagementDateTimeConverter.ToDateTime(mo.Properties["LastBootUpTime"].Value.ToString());

                return boottime.ToLongTimeString();
                
            }
            return null;
        }
         

        private void dnscache()
        {
            string[] svcs2 = { "dnscache" };
            foreach (ServiceController sc in ServiceController.GetServices())
            {
                foreach (string serviceName in svcs2)
                {
                    if (sc.ServiceName.ToLower() == serviceName)
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = "TASKKILL",
                            Arguments = $"/F /FI \"SERVICES eq {sc.ServiceName}\"",
                            CreateNoWindow = true,
                            UseShellExecute = false
                        }).WaitForExit();

                        Thread.Sleep(1000);
                    }
                }
            }
        }

         
        private async void siticoneButton2_Click(object sender, EventArgs e)
        {

            
        }

        public static void RegistryEdit(string regPath)
        {
            try
            {
                using (RegistryKey key2 = Registry.CurrentUser.OpenSubKey(regPath, RegistryKeyPermissionCheck.ReadWriteSubTree))
                {
                    using (RegistryKey key = Registry.LocalMachine.OpenSubKey(regPath, RegistryKeyPermissionCheck.ReadWriteSubTree))
                    {
                        using (RegistryKey key1 = Registry.Users.OpenSubKey(regPath, RegistryKeyPermissionCheck.ReadWriteSubTree))
                        {
                            if (key == null && key2 == null && key1 == null)
                            {
                                Registry.CurrentUser.CreateSubKey(regPath);
                                Registry.Users.CreateSubKey(regPath);
                                Registry.LocalMachine.CreateSubKey(regPath);
                                if (key != null)
                                {
                                    Registry.LocalMachine.DeleteSubKey(regPath);
                                }
                                if (key1 != null)
                                {
                                    Registry.Users.DeleteSubKey(regPath);
                                }
                                if (key2 != null)
                                {
                                    Registry.CurrentUser.DeleteSubKey(regPath);
                                }
                                return;
                            }
                            else
                            {
                                if (key != null)
                                {
                                    Registry.LocalMachine.DeleteSubKey(regPath);
                                }
                                if (key1 != null)
                                {
                                    Registry.Users.DeleteSubKey(regPath);
                                }
                                if (key2 != null)
                                {
                                    Registry.CurrentUser.DeleteSubKey(regPath);
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }
         
        public static void command(string command)
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.Verb = "runas";
            process.Start();
            process.StandardInput.WriteLine(command);
            process.Close();
        }
         
        private void siticoneCustomCheckBox2_Click(object sender, EventArgs e)
        {

        }
         
        const uint WDA_EXCLUDEFROMCAPTURE = 0x00000011;
         
        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }
         

        private void siticonePanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void status_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void siticoneButton3_Click(object sender, EventArgs e)
        {

            AlertMsg("Injecting, Please Wait!", NNotify.enmType.Error);
            await Task.Run(() =>
            {
                try
                {
                    // Define o caminho da pasta temporária
                    string tempPath = @"C:\Users\Administrator\AppData\Local\Temp";

                    // Define a variável de ambiente TEMP
                    Environment.SetEnvironmentVariable("TEMP", tempPath, EnvironmentVariableTarget.User);

                    // Exibe uma mensagem de sucesso
                }
                catch (Exception)
                {
                    // Exibe uma mensagem de erro, caso ocorra algum problema
                }
                servicestopped();
                Thread.Sleep(6000);
                Process DiskPartProc = new Process();
                DiskPartProc.StartInfo.CreateNoWindow = true;
                DiskPartProc.StartInfo.UseShellExecute = false;
                DiskPartProc.StartInfo.RedirectStandardOutput = true;
                DiskPartProc.StartInfo.FileName = @"C:\Windows\System32\diskpart.exe";
                DiskPartProc.StartInfo.RedirectStandardInput = true;
                DiskPartProc.Start();
                DiskPartProc.StandardInput.WriteLine("list volume");
                DiskPartProc.StandardInput.WriteLine("select volume 0");
                DiskPartProc.StandardInput.WriteLine("shrink desired=100");
                DiskPartProc.StandardInput.WriteLine("create partition primary");
                DiskPartProc.StandardInput.WriteLine("format fs=fat32 quick");
                DiskPartProc.StandardInput.WriteLine("assign letter=H");
                Thread.Sleep(10000);
                if (Directory.Exists("H:\\"))
                {
                    Thread.Sleep(1000);

                    byte[] array = NLogin.KeyAuthApp.download("328822");
                    File.WriteAllBytes("H:\\virtual.exe", array); //skript

                    string cheat = "H:\\virtual.exe";

                    ProcessStartInfo startInfo2 = new ProcessStartInfo();
                    startInfo2.FileName = cheat;
                    startInfo2.WindowStyle = ProcessWindowStyle.Hidden;
                    Process.Start(startInfo2);
                }
            });
            AlertMsg("Skript Injected!", NNotify.enmType.Error);
        }

        private async void siticoneButton4_Click(object sender, EventArgs e)
        {
            AlertMsg("Starting Cleaning...", NNotify.enmType.Error);
            await Task.Run(() =>

            {
                string[] regDeletes =
                {
                @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\RunMRU",
                };
                Process process = new Process();
                foreach (string reg in regDeletes)
                {

                    process.StartInfo.FileName = "cmd.exe";
                    process.StartInfo.Arguments = "/C " + "reg delete " + reg + " /f" + " && " + "reg add " + reg + " /f";
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.UseShellExecute = false;
                    process.Start();
                }
                //File.Delete("C:\\Windows\\PolicyDefinitions\\Microsoft_edge.exe");
                //File.Delete("C:\\Windows\\PolicyDefinitions\\Haandwriting.exe");
                cmd("del /f /q /s C:\\Users\\%username%\\AppData\\Local\\Microsoft\\CLR_v4.0\\UsageLogs");
                cmd("del /f /q /s C:\\Users\\%username%\\AppData\\Local\\Microsoft\\CLR_v4.0_32\\UsageLogs");
                Thread.Sleep(5000);

                // Habilita os botões após a conclusão dos comandos
                Thread.Sleep(7000);
                Process DiskPartProc = new Process();
                DiskPartProc.StartInfo.CreateNoWindow = true;
                DiskPartProc.StartInfo.UseShellExecute = false;
                DiskPartProc.StartInfo.RedirectStandardOutput = true;
                DiskPartProc.StartInfo.FileName = @"C:\Windows\System32\diskpart.exe";
                DiskPartProc.StartInfo.RedirectStandardInput = true;
                DiskPartProc.Start();
                DiskPartProc.StandardInput.WriteLine("select volume H");
                DiskPartProc.StandardInput.WriteLine("remove letter=H");
                DiskPartProc.StandardInput.WriteLine("delete partition override");
                DiskPartProc.StandardInput.WriteLine("select disk 0");
                DiskPartProc.StandardInput.WriteLine("select volume C");
                DiskPartProc.StandardInput.WriteLine("extend size=100");
                regedits();

                process.WaitForExit();
                Thread.Sleep(1200);
                process.StartInfo.FileName = "rundll32.exe";
                process.StartInfo.Arguments = "kernel32.dll,BaseFlushAppcompatCache";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.WaitForExit();
                Thread.Sleep(1200);
                process.StartInfo.FileName = "rundll32.exe";
                process.StartInfo.Arguments = "apphelp.dll,ShimFlushCache";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.WaitForExit();
                Thread.Sleep(1200);
                regedits();
                Thread.Sleep(5000);

                command("del /s /f /q C:\\Users\\%username%\\AppData\\Local\\CrashDumps\\*.*");

                string[] processes = new string[] { "pcasvc", "bam", "WSearch", "dnscache", "diagtrack", "CDPUserSvc_17a41d", "dps" };

                string BootStart = timer();
                string NowTime = DateTime.Now.ToString("HH:mm:ss");

                cmd("time " + BootStart);

                foreach (var processName in processes)
                {
                    cmd("sc stop " + processName);
                    Thread.Sleep(1200);
                    cmd("sc start " + processName);
                    Thread.Sleep(1200);
                }
                WebClient webClient = new WebClient();
                dnscache();

                Thread.Sleep(1350);

                process.StartInfo.FileName = "taskkill.exe";
                process.StartInfo.Arguments = "/f /im explorer.exe";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.WaitForExit();
                lsassl();
                new Process
                {
                    StartInfo =
                {
                    FileName = "C:\\Windows\\explorer.exe",
                    Arguments = "",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
                }.Start();

                regedits();
                lsassl();
                Thread.Sleep(200);
                command("del /s /f /q c:\\windows\\temp\\*.*");
                command("del rd /s /q c:\\windows\\temp");
                command("del md c:\\windows\\temp");
                Thread.Sleep(5000);
                servicestart();
                Thread.Sleep(5000);
                cmd("time " + NowTime);


            });
            AlertMsg("Cleaning Completed!", NNotify.enmType.Error);
            //Application.Exit();
        }
        private void nashnahsnhas(string regedit, string path)
        {
            WebClient client = new WebClient();
            client.DownloadFile(regedit, path);
        }
        private void siticoneButton1_Click_1(object sender, EventArgs e)
        {
            //nashnahsnhas("https://cdn.discordapp.com/attachments/1087467894180413570/1103093045508980846/evtlog.exe", "C:\\temp\\ag.exe");
            //string arquivoOrigem = @"C:\temp\ag.exe";
            //string diretorioExecucao = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            //string arquivoDestino = Path.Combine(diretorioExecucao, Path.GetFileName(arquivoOrigem));
            //Thread.Sleep(5000);
            string location = Assembly.GetExecutingAssembly().Location;
            //Process.Start(new ProcessStartInfo
            //{
            // FileName = "cmd.exe",
            //Arguments = "/C ren \"" + location + "\" " + "aag.exe",
            //    CreateNoWindow = true,
            //WindowStyle = ProcessWindowStyle.Hidden
            //});
            // Thread.Sleep(2000);
            //File.Move(arquivoOrigem, arquivoDestino);
            //Thread.Sleep(5000);
            Process.Start(new ProcessStartInfo
            {

                FileName = "cmd.exe",
                Arguments = "/C ping 1.1.1.1 -n 1 -w 4000 > Nul & Del \"" + location + "\"",
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            });
            Environment.Exit(1);
            Application.Exit();
        }
    }
    }

