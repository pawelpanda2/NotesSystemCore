using SharpButtonActionsProj.Service;
namespace ButtonActionsCoreProj
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new ButtonActionsMacWorker();


            //var path01 = "/Users/pawelfluder/Dropbox/0fc7da8d-3466-4964-a24c-dfc0d0fef87c/Notki";
            var path01 = "/Users";
            service.TryOpenFolder(path01);

            var path = "/Users/pawelfluder/Dropbox/0fc7da8d-3466-4964-a24c-dfc0d0fef87c/Notki/nazwa.txt";

            //https://help.panic.com/nova/cli-tool/
            service.TryOpenContent(path);

            service.TryOpenTerminal("/");


            
            

            //var buttonActionService = new ButtonActionsService();
        }
    }
}
