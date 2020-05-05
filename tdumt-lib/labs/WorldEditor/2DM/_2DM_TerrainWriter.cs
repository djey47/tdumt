using System.IO;

namespace TDUModdingLibrary.labs.WorldEditor
{
    internal class _2DM_TerrainWriter
    {
        private TerrainPieces pTerrainPieces = new TerrainPieces();

        private Utilitaires pUtilitaires = new Utilitaires();

        public void Write2DM(string output, string sector, string blend1, string blend2)
        {
            //IL_0004: Unknown result type (might be due to invalid IL or missing references)
            //IL_0009: Expected O, but got Unknown
            using (BinaryWriter binaryWriter = new BinaryWriter((Stream)File.Open(output, 2, 2, 3)))
            {
                binaryWriter.Write(pTerrainPieces.terrain2DMhead);
                binaryWriter.Write(pUtilitaires.hexify(sector + "-c"));
                binaryWriter.Write(pTerrainPieces.terrain2DMhead2);
                binaryWriter.Write(pUtilitaires.hexify(sector + "-s"));
                binaryWriter.Write(pTerrainPieces.terrain2DMhead2);
                binaryWriter.Write(pUtilitaires.hexify(blend1));
                binaryWriter.Write(pTerrainPieces.terrain2DMhead2);
                binaryWriter.Write(pUtilitaires.hexify(sector + "-a"));
                binaryWriter.Write(pTerrainPieces.terrain2DMhead2);
                binaryWriter.Write(pUtilitaires.hexify(blend2));
                binaryWriter.Write(pTerrainPieces.terrain2DMmid2);
                binaryWriter.Write(pUtilitaires.hexify(sector));
                binaryWriter.Write(pTerrainPieces.terrain2DMbottom);
            }
        }
    }
}
