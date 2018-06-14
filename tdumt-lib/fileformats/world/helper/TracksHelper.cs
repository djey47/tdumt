using System.Collections.ObjectModel;
using System.IO;
using DjeFramework1.Common.Support.Traces;
using TDUModdingLibrary.support.constants;

namespace TDUModdingLibrary.fileformats.world.helper
{
    /// <summary>
    /// Helper class to manage custom tracks
    /// </summary>
    public class TracksHelper
    {
        /// <summary>
        /// Returns all default challenges from specified reference folder
        /// </summary>
        /// <param name="referenceFolder"></param>
        public static Collection<DFE> LoadDefaultChallenges(string referenceFolder)
        {
            Collection<DFE> returnedChallenges = new Collection<DFE>();

            // Get all files in specified DFE folder
            DirectoryInfo dirInfo = new DirectoryInfo(referenceFolder);

            if (!dirInfo.Exists)
                Directory.CreateDirectory(referenceFolder);

            string[] returnedTracks = Directory.GetFiles(referenceFolder, LibraryConstants.FILTER_DFE);

            foreach (string anotherFile in returnedTracks)
            {
                DFE dfeFile = TduFile.GetFile(anotherFile) as DFE;

                if (dfeFile == null)
                    Log.Warning("DFE file failed to load: " + anotherFile);
                else
                    returnedChallenges.Add(dfeFile);
            }

            // Log message
            Log.Info("Original challenges found and loaded: " + returnedChallenges.Count);

            return returnedChallenges;
        }

    }
}
