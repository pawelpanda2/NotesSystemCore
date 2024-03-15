using SharpConfigProg.Service;
using SharpNotesMigrationProg.Service;
using SharpNotesMigrationTests.Repetition;
using SharpRepoServiceProg.Service;
using Unity;
using Public01 = SharpSetupProg21Private.AAPublic;

namespace SharpNotesMigrationTests
{
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
            // WpfNotesSystemPrivate01.Repetition
            var registration = new Public01.Registration();
            registration.Start();
            var configService = MyBorder.Container.Resolve<IConfigService>();
            configService.Prepare();
            var repoService = MyBorder.Container.Resolve<IRepoService>();
            repoService.Initialize(configService.GetRepoSearchPaths());
        }

        [TestMethod]
        public void TestMethod1()
        {
            // arrange
            var migrationService = MyBorder.Container.Resolve<IMigrationService>();

            // act
            migrationService.Migrate(typeof(IMigrationService.IMigrator03));
        }

        [TestMethod]
        public void TestMethod2()
        {
            // arrange
            var migrator03 = MyBorder.Container.Resolve<IMigrationService.IMigrator03>();
            var repo = "Notki";
            var loca = "";

            // act
            migrator03.MigrateOneAddress((repo, loca));

            // assert
            var beforeAfter = migrator03.Changes;
        }

        [TestMethod]
        public void MigrateOneFolderRecoursively()
        {
            // arrange 1
            var repoName = "System";
            var loca = "";
            var agree = true;
            
            // arrange 2
            var migrator03 = MyBorder.Container.Resolve<IMigrationService.IMigrator03>();
            migrator03.SetAgree(agree);
            var repoServer = MyBorder.Container.Resolve<IRepoService>();

            var address = (repoName, loca);
            var folderPath = repoServer.Methods.GetElemPath((repoName, loca));

            // act
            migrator03.MigrateOneFolderRecourively(address);

            // print
            var beforeAfter = migrator03.Changes;
            beforeAfter.ForEach((x) =>
            {
                Console.WriteLine(x.Item1);
                Console.WriteLine(x.Item2);
                Console.WriteLine(x.Item3);
                Console.WriteLine(x.Item4);
                Console.WriteLine();
            });
        }

        [TestMethod]
        public void MigrateOneRepo()
        {
            // arrange
            var migrator03 = MyBorder.Container.Resolve<IMigrationService.IMigrator03>();
            migrator03.SetAgree(true);
            var repoServer = MyBorder.Container.Resolve<IRepoService>();
            var repoName = "Notki";
            var address = (repoName, "");
            //var repoName = "02_appData";
            //var repoPath = repoServer.Methods.GetRepoPath(repoName);

            // act
            migrator03.MigrateOneRepo(address);

            // assert
            
            var beforeAfter = migrator03.Changes;
            beforeAfter.ForEach((x) =>
            {
                Console.WriteLine(x.Item1);
                Console.WriteLine(x.Item2);
                Console.WriteLine(x.Item3);
                Console.WriteLine(x.Item4);
                Console.WriteLine();
            });
        }
    }
}