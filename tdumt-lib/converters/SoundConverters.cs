using TDUModdingLibrary.fileformats.sound;

namespace TDUModdingLibrary.converters
{
    class SoundConverters
    {
        /// <summary>
        /// Convertit un fichier XMB_WAV (illisible) en fichier WAV
        /// </summary>
        /// <param name="xmbWavFile">Fichier XMB_WAV à manipuler</param>
        /// <param name="newWavFileName">Nom du fichier WAV à produire</param>
        public static void XMB_WAVToWAV(XMB_WAV xmbWavFile, string newWavFileName)
        { }

        /// <summary>
        /// Extrait la partie XMB d'un fichier XMB_WAV
        /// </summary>
        /// <param name="xmbWavFile">Fichier XMB_WAV à manipuler</param>
        /// <param name="newXmbFileName">Nom du fichier XMB à extraire</param>
        public static void XMB_WAVToXMB(XMB_WAV xmbWavFile, string newXmbFileName)
        { }
    }
}