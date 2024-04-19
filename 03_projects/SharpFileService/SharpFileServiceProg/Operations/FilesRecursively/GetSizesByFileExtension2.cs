namespace SharpFileServiceProg.Operations.FileSize
{
    public class GetSizesByFileExtension2
    {
        private VisitDirectoriesRecursively rvd;
        private Action<FileInfo> fileAction;
        private Action<DirectoryInfo> folderAction;
        long generalSize;
        long tempSize;
        private Dictionary<string, Dictionary<string, long>> dictionarySize;

        public GetSizesByFileExtension2()
        {
            rvd = new VisitDirectoriesRecursively();
            InitializeActions();
        }

        public Dictionary<string, Dictionary<string, long>> Do
            (string path, string[] typesToCount = null)
        {
            dictionarySize = new Dictionary<string, Dictionary<string, long>>();
            rvd.Visit(path, fileAction, folderAction);
            return dictionarySize;
        }

        private void InitializeActions()
        {
            fileAction = new Action<FileInfo>((fileInfo) =>
            {
                var extension = fileInfo.Extension;

                if (!dictionarySize.ContainsKey(extension))
                {
                    var tmp2 = new Dictionary<string, long>();
                    dictionarySize.Add(extension, tmp2);
                }
                
                var tmp = dictionarySize[extension];
                tmp.Add(fileInfo.FullName, fileInfo.Length);
            });

            folderAction = new Action<DirectoryInfo>((directionryInfo) =>
            {
            });
        }
    }
}