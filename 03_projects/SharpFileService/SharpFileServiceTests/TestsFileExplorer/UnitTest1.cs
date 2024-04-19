using SharpFileServiceProg.Operations.FileSize;
using SharpFileServiceProg.Service;
using SharpFileServiceTests.Registrations;
using OutBorder01 = SharpSetup21ProgPrivate.AAPublic.OutBorder;

namespace SharpFileServiceTests.SingleClassTests
{
    [TestClass]
    public class UnitTest1
    {
        private readonly IFileService fileService;

        public UnitTest1()
        {
            OutBorder01.GetPreparer("PrivateNotesPreparer").Prepare();
            fileService = MyBorder.Container.Resolve<IFileService>();
        }

        [TestMethod]
        public void Method01()
        {
            // arrange
            //var visitor = fileService.File.GetNewVisitDirectoriesRecursivelyWithParentMemory();
            var gg = new GetFolderSizes();

            // act
            var path = "D:\\03_synch\\01_files_programming\\03_github\\17_projects";
            var gg2 = gg.Do(path);

            var gg4 = new GetSizesByFileExtension();
            var gg5 = gg4.Do(path);
        }
    }
}