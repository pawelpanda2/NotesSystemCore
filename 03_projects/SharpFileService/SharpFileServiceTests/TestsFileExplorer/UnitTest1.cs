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
            //var gg = new GetFolderSizes();

            // act
            //var path = "D:\\03_synch\\01_files_programming\\03_github\\17_projects";
            //var path2 = "/Users/pawelfluder/03_synch/01_files_programming/03_github/";
            var path3 = "/Users/pawelfluder/03_synch/01_files_programming/03_github/NotesSystemCore";
            //var gg2 = gg.Do(path3);

            var gg4 = new GetSizesByFileExtension2();
            var gg5 = gg4.Do(path3);
        }
    }
}