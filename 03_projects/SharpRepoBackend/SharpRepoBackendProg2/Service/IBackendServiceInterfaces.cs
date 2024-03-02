namespace SharpRepoBackendProg.Service
{
    public partial interface IBackendService
    {
        public enum ApiMethods
        {
            // item
            GetItem,

            // name
            GetName,

            // body
            GetBody,

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
            OpenGoogleDoc,
            RecreateGoogleDoc,
            CreateGoogleDoc,

            // printer
            RunPrinter,

            // ??
            GetAllRepoName,
        }
    }
}