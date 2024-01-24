using SharpFileServiceProg.Service;
using SharpRepoServiceProg.Service;
using SharpVideoServiceProg.AAPublic;
using System.Globalization;
using System.Speech.Synthesis;

namespace SharpTtsServiceProg.Worker
{
    public class RepoTtsWorker : MethodRunner
    {
        private readonly TtsWorker ttsWorker;
        private readonly IFileService fileService;
        private readonly IRepoService repoService;
        private readonly IVideoService videoService;

        private string fileName;

        public RepoTtsWorker(
            IFileService fileService,
            IRepoService repoService,
            IVideoService videoService)
            : base()
        {
            this.fileService = fileService;
            this.repoService = repoService;
            this.videoService = videoService;
            ttsWorker = new TtsWorker();
            fileName = "lista";
        }

        public override async Task RunMethodAsync(string methodName, params object[] args)
        {
            if (methodName == "Resume")
            {
                await Resume();
                return;
            }

            if (methodName == "Pause")
            {
                await Pause();
                return;
            }

            if (methodName == "Stop")
            {
                await Stop();
                return;
            }

            var adrTuple = (args[0].ToString(), args[1].ToString());

            if (methodName == "PlStartNew")
            {
                await PlStartNew(adrTuple);
                return;
            }

            if (methodName == "EnStartNew")
            {
                await EnStartNew(adrTuple);
                return;
            }

            if (methodName == "PlSaveAudio")
            {
                await PlSaveAudio(adrTuple);
                return;
            }

            if (methodName == "EnSaveAudio")
            {
                await EnSaveAudio(adrTuple);
                return;
            }

            if (methodName == "VoiceToVideo")
            {
                await VoiceToVideo(adrTuple);
                return;
            }
        }

        public async Task Stop()
        {
            await ttsWorker.Stop();
        }

        public async Task PlStartNew((string Repo, string Loca) adrTuple)
        {
            var text = GetText(adrTuple);
            await ttsWorker.StartNew(text, new CultureInfo("pl-PL"));
        }

        public async Task EnStartNew((string Repo, string Loca) adrTuple)
        {
            var text = GetText(adrTuple);
            await ttsWorker.StartNew(text, new CultureInfo("en-GB"));
        }

        public async Task Pause()
        {
            await ttsWorker.Pause();
        }

        public async Task<string> PlSaveAudio((string Repo, string Loca) adrTuple)
        {
            var culture = new CultureInfo("pl-PL");
            var builder = GetBuilder(adrTuple, culture);
            var elemPath = repoService.Methods.GetElemPath(adrTuple);

            await ttsWorker.SaveAudioFile(elemPath, fileName, builder);
            return elemPath;
        }

        public async Task<string> EnSaveAudio((string Repo, string Loca) adrTuple)
        {
            var culture = new CultureInfo("en-GB");
            var builder = GetBuilder(adrTuple, culture);
            var elemPath = repoService.Methods.GetElemPath(adrTuple);

            await ttsWorker.SaveAudioFile(elemPath, fileName, builder);
            return elemPath;
        }

        public async Task VoiceToVideo((string Repo, string Loca) adrTuple)
        {
            var folderPath = repoService.Methods.GetElemPath(adrTuple);
            //var folderPath = await PlSaveAudio(adrTuple);
            var audioFilePath = folderPath + "/" + fileName + ".wav";
            var imageFilePath = "Output/background.png";
            var videoFilePath = folderPath + "/" + fileName + ".mp4";
            await videoService.PosterWithAudio(imageFilePath, audioFilePath, videoFilePath);
        }

        public async Task Resume()
        {
            await ttsWorker.Resume();
        }

        private string GetText((string Repo, string Loca) adrTuple)
        {
            var text = repoService.Methods.GetText3(adrTuple);
            text = text.Replace("//", "");
            return text;
        }

        private PromptBuilder GetBuilder((string Repo, string Loca) adrTuple, CultureInfo culture)
        {
            var text = repoService.Methods.GetText3(adrTuple);
            var elements = fileService.Header.Select2.GetElements(text);
            var builder = new PromptBuilder();
            builder.Culture = culture;

            foreach (var elem in elements)
            {
                if (elem.Type == "Header")
                {
                    builder.AppendBreak();
                    builder.AppendText(elem.Text);
                    builder.AppendBreak();
                }

                if (elem.Type != "Header")
                {
                    builder.AppendText(elem.Text);
                }
            }

            return builder;
        }
    }
}
