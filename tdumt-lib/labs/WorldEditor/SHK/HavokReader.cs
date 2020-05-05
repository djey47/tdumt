using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace TDUModdingLibrary.labs.WorldEditor
{
    internal class HavokReader
    {
        public struct havokConvexMesh
        {
            public string hash;

            public List<Utilitaires.Vertex> vertexList;

            public List<short> faceList;
        }

        private struct havokDataHead
        {
            public int offset;

            public int taille1;

            public int taille2;

            public int taille3;

            public int taille4;
        }

        private struct havokTypesHead
        {
            public int offset;

            public int taille1;

            public int taille2;

            public int taille3;
        }

        private struct havokBase
        {
            public int classNameOffset;

            public int classIndexOffset;

            public int dataIndexOffset;

            public int dataIndexSize;

            public havokDataHead data;

            public havokTypesHead types;
        }

        private Utilitaires pUtilitaires = new Utilitaires();

        public List<havokConvexMesh> ReadHavokRoad(string input)
        {
            //IL_0010: Unknown result type (might be due to invalid IL or missing references)
            //IL_0015: Expected O, but got Unknown
            List<havokConvexMesh> list = new List<havokConvexMesh>();
            Form activeForm = Form.ActiveForm;
            using (BinaryReader binaryReader = new BinaryReader((Stream)File.Open(input, 3, 1, 3)))
            {
                binaryReader.BaseStream.Seek(2L, SeekOrigin.Begin);
                int num = binaryReader.ReadInt32();
                binaryReader.BaseStream.Seek(18L, SeekOrigin.Current);
                binaryReader.ReadInt32();
                binaryReader.BaseStream.Seek(4L, SeekOrigin.Current);
                for (short num2 = 0; num2 < num; num2 = (short)(num2 + 1))
                {
                    havokConvexMesh item = default(havokConvexMesh);
                    item.hash = "CONVEX_" + num2;
                    item.faceList = new List<short>();
                    item.vertexList = new List<Utilitaires.Vertex>();
                    list.Add(item);
                    binaryReader.BaseStream.Seek(32L, SeekOrigin.Current);
                }
                int num3 = Convert.ToInt32(binaryReader.BaseStream.Position) + 40;
                binaryReader.BaseStream.Seek(84L, SeekOrigin.Current);
                havokBase havokBase = default(havokBase);
                havokBase.classNameOffset = binaryReader.ReadInt32() + num3;
                binaryReader.BaseStream.Seek(44L, SeekOrigin.Current);
                havokBase.classIndexOffset = binaryReader.ReadInt32() + num3;
                binaryReader.BaseStream.Seek(44L, SeekOrigin.Current);
                havokBase.dataIndexOffset = binaryReader.ReadInt32() + num3;
                havokBase.dataIndexSize = binaryReader.ReadInt32();
                binaryReader.BaseStream.Seek(40L, SeekOrigin.Current);
                havokBase.data.offset = binaryReader.ReadInt32() + num3;
                havokBase.data.taille1 = binaryReader.ReadInt32();
                havokBase.data.taille2 = binaryReader.ReadInt32() - havokBase.data.taille1;
                havokBase.data.taille3 = binaryReader.ReadInt32() - havokBase.data.taille2;
                binaryReader.BaseStream.Seek(4L, SeekOrigin.Current);
                havokBase.data.taille4 = binaryReader.ReadInt32();
                binaryReader.BaseStream.Seek(24L, SeekOrigin.Current);
                havokBase.types.offset = binaryReader.ReadInt32() + num3;
                havokBase.types.taille1 = binaryReader.ReadInt32();
                havokBase.types.taille2 = binaryReader.ReadInt32() - havokBase.types.taille1;
                havokBase.types.taille3 = binaryReader.ReadInt32() - havokBase.types.taille2;
                binaryReader.BaseStream.Seek(havokBase.data.offset + 12, SeekOrigin.Begin);
                while (binaryReader.ReadInt32() != -1)
                {
                }
                binaryReader.BaseStream.Seek(binaryReader.BaseStream.Position - 4, SeekOrigin.Begin);
                for (int i = 0; i < num; i++)
                {
                    binaryReader.BaseStream.Seek(384L, SeekOrigin.Current);
                    int num4 = binaryReader.ReadInt32();
                    binaryReader.BaseStream.Seek(12L, SeekOrigin.Current);
                    int num5 = binaryReader.ReadInt32();
                    binaryReader.BaseStream.Seek(116L, SeekOrigin.Current);
                    for (int j = 0; j < num4; j++)
                    {
                        Utilitaires.Vertex item2 = default(Utilitaires.Vertex);
                        item2.x = binaryReader.ReadSingle();
                        item2.y = binaryReader.ReadSingle();
                        item2.z = binaryReader.ReadSingle();
                        list[i].vertexList.Add(item2);
                    }
                    while (binaryReader.BaseStream.Position % 16 != 0)
                    {
                        binaryReader.ReadByte();
                    }
                    long position = binaryReader.BaseStream.Position;
                    binaryReader.BaseStream.Seek(position, SeekOrigin.Begin);
                    for (int k = 0; k < num5 * 3; k++)
                    {
                        list[i].faceList.Add(binaryReader.ReadInt16());
                    }
                    while (num5 != 0)
                    {
                        num5--;
                    }
                    while (binaryReader.ReadInt32() != -1)
                    {
                    }
                    binaryReader.BaseStream.Seek(binaryReader.BaseStream.Position - 4, SeekOrigin.Begin);
                }
                return list;
            }
        }
    }
}
