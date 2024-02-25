namespace SharpRepoBackendProg.Service
{
    public partial interface IBackendService
    {
        public enum ApiMethods
        {
            // config
            GetConfig,
            OpenConfig,
            CreateFolder,

            // content
            GetContent,

            OpenContent,
            CreateContent,
            AddContent,

            // folder
            OpenFolder,

            // pdf
            OpenPdf,
            CreatePdf,

            // google doc
            OpenGoogledoc,
            RecreateGoogledoc,
            CreateGoogledoc,

            // printer
            RunPrinter,

            // ??
            GetAllRepoName,
        }
    }
}