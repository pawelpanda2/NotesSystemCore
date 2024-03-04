using SharpFileServiceProg.Service;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SharpButtonActionsProg.Workers
{
    public class MacWorker
    {
        private readonly IFileService fileService;
        private char space = ' ';

        private string osaFileName = "osaScript.scpt";
        private string osaFilePath;

        public MacWorker(IFileService fileService)
        {
            this.fileService = fileService;
            osaFilePath = GetBinFilePath(osaFileName);
        }

        private void PrepareOpenFolder(string filePath)
        {
            var fileName = "OpenFolder.scpt";
            var dict = new Dictionary<string, string>()
            {
                { "[[folderPath]]", filePath }
            };

            ReplaceScript(fileName, dict);
        }

        private void ReplaceScript(
            string fileName,
            Dictionary<string, string> dict)
        {
            var path = "OsaScripts." + fileName;

            var script = fileService.Credentials
                .GetEmbeddedResource(GetAssembly().GetName(), path);

            foreach (var item in dict)
            {
                script = script.Replace(item.Key, item.Value);
            }

            ReplaceBinFile(osaFileName, script);
        }

        private void AppPathsCorrect()
        {
            // Nova
            // var exePath = "/Applications/Nova.app/Contents/MacOS/Nova";

            // Terminal
            // var exePath = "/System/Applications/Utilities/Terminal.app/Contents/MacOS/Terminal";

            // Finder
            // var exePath = "/System/Library/CoreServices/Finder.app/Contents/MacOS/Finder";
        }

        private Assembly GetAssembly()
        {
            return Assembly.GetAssembly(this.GetType());
        }

        private string ReplaceBinFile(string fileName, string content)
        {
            var codeBase = GetAssembly().CodeBase;
            var binFolder = Path.GetDirectoryName(codeBase).Replace("file:\\", "");

            var filePath = binFolder + "/" + fileName;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            File.WriteAllText(filePath, content);
            return filePath;
        }

        private string GetBinFilePath(string fileName)
        {
            var codeBase = GetAssembly().CodeBase;
            var binFolder = Path.GetDirectoryName(codeBase);
            var filePath = binFolder + "/" + fileName;
            return filePath;
        }

        public void TryOpenFolder(string path)
        {
            if (!IsMyOsSystem()) { return; }

            PrepareOpenFolder(path);

            var scriptPath = GetBinFilePath(osaFilePath);
            RunOsaScript(scriptPath, path);
        }

        public void TryOpenTerminal(string path)
        {
            if (!IsMyOsSystem()) { return; }

            try
            {
                var exePath = "/System/Applications/Utilities/Terminal.app/Contents/MacOS/Terminal";
                Process uploadProc = new Process
                {
                    StartInfo = {
                        FileName = exePath,
                        UseShellExecute = false,
                        CreateNoWindow = false,
                        WindowStyle = ProcessWindowStyle.Normal
                    }
                };

                uploadProc.Start();

                //var exePath = "/Applications/Utilities/Terminal.app/contents/macos/terminal";
                //var exePath = @"/Applications/Utilities/Terminal.app/Contents/MacOS/Terminal";
                //Process.Start(exePath);
            }
            catch (Exception ex)
            {

            }
        }

        public void TryOpenFile(string path)
        {
            if (!IsMyOsSystem()) { return; }

            try
            {
                RunOsaScript("OpenFile.sh", path);

                //var exePath = "/applications/textedit.app";
                //var exePath = "/applications/textedit.app/contents/macos/textedit";
                //var exePath = "/Applications/Nova.app/Contents/MacOS/Nova";
                //var gg = Process.Start(exePath, path);
            }
            catch (Exception ex)
            {

            }
        }

        public void RunOsaScript(string scriptPath, string arguments = null)
        {
            if (!IsMyOsSystem()) { return; }

            //string test = $" -c \"osascript -e \' tell application \\\"Terminal\\\" to do script \\\"echo hello\\\" \' \"";
            
            string test = $" -c \"osascript {scriptPath}\"";
            
            test = new string(test.Where(c => !char.IsControl(c)).ToArray());
            Console.WriteLine(test);
            var startInfo = new ProcessStartInfo
            {
                UseShellExecute = true,
                WindowStyle = ProcessWindowStyle.Normal,
                FileName = "/bin/bash",
                Arguments = test,
                CreateNoWindow = false,
            };
            var process = new Process()
            {
                StartInfo = startInfo,
            };
            process.StartInfo = startInfo;
            var s1 = process.Start();
        }

        public void RunScript1(string scriptPath, string arguments = null)
        {
            if (!IsMyOsSystem()) { return; }

            var appPath = "/System/Applications/Utilities/Terminal.app/Contents/MacOS/Terminal";

            var s1 = Process.Start(appPath, "open .");

            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = "osascript",
                Arguments = $"-e 'tell application \"Terminal\" to activate' -e 'tell application \"Terminal\" to do script \"sh {scriptPath} {arguments}\"'",

                UseShellExecute = true,
                CreateNoWindow = false,
                Verb = "runas",
                RedirectStandardOutput = false,
                RedirectStandardInput = false,
            };
            Process process = new Process()
            {
                StartInfo = startInfo,
            };
            process.Start();
        }

        public void Run(string[] args)
        {
            //var fileName = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
            //var fileName = @"C:\Program Files\Mozilla Firefox\firefox.exe";
            //var arguments = "D:\02_Xampp\htdocs\Notki\01\02\lista.txt";

            var fileName = @"C:\Program Files\Notepad++\notepad++.exe";

            //var arguments = @"https://facebook.com";
            //var arguments = "https://www.google.com";

            string arguments = string.Empty;


            var argsList = args.Any() ? args.ToList() : new List<string>();


            if (argsList.Count == 0)
            {
                var currentDirectory = Directory.GetCurrentDirectory();
                Process.Start("explorer.exe", currentDirectory);
            }
            else if (argsList.Count == 1)
            {
                arguments = args[0];

                if (Directory.Exists(arguments))
                {
                    Process.Start("explorer.exe", arguments);
                }
                else
                {
                    Process process = new Process();
                    process.StartInfo = new ProcessStartInfo()
                    {
                        WindowStyle = ProcessWindowStyle.Hidden,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true,
                        StandardOutputEncoding = System.Text.Encoding.UTF8,
                        FileName = fileName,
                        Arguments = arguments,
                    };
                    process.Start();
                }
            }
        }

        private bool IsMyOsSystem()
        {
            var result = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
            return result;
        }
    }
}
