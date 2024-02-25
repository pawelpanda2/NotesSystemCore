using System.Diagnostics;

namespace SharpButtonActionsProj.Service
{
    public class ButtonActionsMacWorker
    {
        static char space = ' ';
        
        private void AppPathsCorrect()
        {
            // Nova
            // var exePath = "/Applications/Nova.app/Contents/MacOS/Nova";

            // Terminal
            // var exePath = "/System/Applications/Utilities/Terminal.app/Contents/MacOS/Terminal";

            // Finder
            // var exePath = "/System/Library/CoreServices/Finder.app/Contents/MacOS/Finder";
        }

        public void TryOpenFolder(string path)
        {
            //var exePath = "/System/Library/CoreServices/Finder.app/Contents/MacOS/Finder";
            var exePath = @"/System/Applications/Utilities/Terminal.app/Contents/MacOS/Terminal";

            //var p = Process.Start(exePath, path);

            var startInfo = new ProcessStartInfo {
                FileName = exePath,
                Arguments = path,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                UserName = System.Environment.UserName
            };

            var process = Process.Start (startInfo);
        }

        public void TryOpenTerminal(string path)
        {
            try
            {
                var exePath = "/System/Applications/Utilities/Terminal.app/Contents/MacOS/Terminal";
                System.Diagnostics.Process uploadProc = new System.Diagnostics.Process {
                    StartInfo = {
                        FileName = exePath,
                        UseShellExecute = false,
                        CreateNoWindow = false,
                        WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal
                    }
                };
            
                uploadProc.Start();

                //var exePath = "/Applications/Utilities/Terminal.app/contents/macos/terminal";
                //var exePath = @"/Applications/Utilities/Terminal.app/Contents/MacOS/Terminal";
                //Process.Start(exePath);
            }
            catch(Exception ex){

            }            
        }

        public void TryOpenContent(string path)
        {
            try{
                //var exePath = "/applications/textedit.app";
                //var exePath = "/applications/textedit.app/contents/macos/textedit";
                var exePath = "/Applications/Nova.app/Contents/MacOS/Nova";
                var gg = Process.Start(exePath, path);
            }
            catch(Exception ex){
                
            }            
        }

        public void TryOpenConfigFile(string path)
        {
            var contentFilePath = path + "/" + "nazwa.txt";
            var programPath = @"C:\Program Files\Notepad++\notepad++.exe";
            var windowsFormatPath = Path.GetFullPath(contentFilePath);
            Process.Start(programPath, windowsFormatPath);
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
    }
}
