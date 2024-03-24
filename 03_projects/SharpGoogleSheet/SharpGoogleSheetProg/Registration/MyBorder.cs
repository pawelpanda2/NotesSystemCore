using SharpContainerProg.AAPublic;

namespace SharpGoogleSheetProg.Registration
{
    internal static class MyBorder
    {
        private static IContainer container = new Registration().Start();
        public static IContainer Container => container;
    }
}