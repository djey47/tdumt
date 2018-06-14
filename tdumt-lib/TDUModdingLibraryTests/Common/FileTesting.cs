using System.IO;
using System.Reflection;

namespace TDUModdingLibraryTests.Common
{
    public class FileTesting
    {
        private static readonly Assembly CurrentAssembly = Assembly.GetExecutingAssembly();

        public static string CreateTempDirectory()
        {
            var path = Path.Combine(Path.GetTempPath(), "tdumt-lib-tests");
            Directory.CreateDirectory(path);
            return path;
        }

        public static string CreateFileFromResource(string resourcePath, string targetFile)
        {
            using (
                Stream stream =
                    CurrentAssembly.GetManifestResourceStream(resourcePath))
            {
                if (stream == null)
                {
                    return null;
                }

                var fileStream = new FileStream(targetFile, FileMode.Create, FileAccess.Write);
                stream.CopyTo(fileStream);
                fileStream.Dispose();

                return targetFile;
            }
        }
    }
}
