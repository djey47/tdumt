using System;
using TDUModdingLibrary.fileformats;
using TDUModdingLibrary.fileformats.graphics;

namespace TDUModdingToolsTest.tests.gfx
{
    class TexturesTests
    {
        public static void Read2DBFileTest(string fileName)
        {
            _2DB textureFile = (_2DB)TduFile.GetFile(fileName);

            if (textureFile == null)
            {
                throw new Exception();
            }
        }

        public static void Get2DBHeaderAsBytesTest(string fileName)
        {
            _2DB textureFile = (_2DB) TduFile.GetFile(fileName);
            _2DB.TextureHeader header;
            byte[] headerBytes = null;

            if (textureFile == null)
                throw new Exception();

            header = (_2DB.TextureHeader)textureFile.Header;
            headerBytes = textureFile.HeaderData;
        }

        public static void GetDDSHeaderAsBytesTest(string fileName)
        {
            DDS textureFile = (DDS)TduFile.GetFile(fileName);
            DDS.TextureHeader header;
            byte[] headerBytes = null;

            if (textureFile == null)
            {
                throw new Exception();
            }

            header = (DDS.TextureHeader)textureFile.Header;
            headerBytes = textureFile.HeaderData;
        }
    }
}
