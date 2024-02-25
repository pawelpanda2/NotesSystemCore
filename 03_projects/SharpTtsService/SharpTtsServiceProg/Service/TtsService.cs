using SharpFileServiceProg.Service;
using SharpRepoServiceProg.Service;
using SharpTtsServiceProg.AAPublic;
using SharpTtsServiceProg.Worker;
using SharpVideoServiceProg.AAPublic;

namespace SharpTtsServiceProg.Service
{
    internal class TtsService : ITtsService
    {
        private IFileService fileService;
        private IRepoService repoService;
        private IVideoService videoService;

        private RepoTtsWorker repoTts;
        public RepoTtsWorker RepoTts
        {
            get
            {
                TryInitialize();
                return repoTts;
            }
        }

        public TtsWorker Tts { get; private set;}

        private bool isInitialized;

        private void TryInitialize()
        {
            Tts = new TtsWorker();
            repoTts = new RepoTtsWorker(fileService, repoService, videoService);
        }

        public TtsService(
            IFileService fileService,
            IRepoService repoService,
            IVideoService videoService)
        {
            this.fileService = fileService;
            this.repoService = repoService;
            this.videoService = videoService;
        }
    }
}
