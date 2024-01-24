using SharpFileServiceProg.Service;
using SharpRepoServiceProg.Service;
using SharpTtsServiceProg.AAPublic;
using SharpTtsServiceProg.Worker;
using SharpVideoServiceProg.AAPublic;

namespace SharpTtsServiceProg.Service
{
    internal class TtsService : ITtsService
    {
        public TtsWorker Tts { get; }
        public RepoTtsWorker RepoTts { get; }

        public TtsService(
            IFileService fileService,
            IRepoService repoService,
            IVideoService videoService)
        {
            Tts = new TtsWorker();
            RepoTts = new RepoTtsWorker(fileService, repoService, videoService);
        }
    }
}
