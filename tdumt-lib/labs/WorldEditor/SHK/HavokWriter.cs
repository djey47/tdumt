using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TDUModdingLibrary.labs.WorldEditor
{
    internal class HavokWriter
    {
        private const float shiftMagic = 0.04999f;

        private const float magicVertex = -0.123456791f;

        private const int fileInfoSize = 240;

        private const int classnamesSize = 96;

        private const int classindexSize = 288;

        private const int typesSize1 = 9088;

        private const int typesSize2 = 11120;

        private const int typesSize3 = 11744;

        private const int MOPP_ALLOC = 800;

        private Utilitaires pUtilitaires = new Utilitaires();

        private HavokPieces HavokP = new HavokPieces();

        public void HavokRoad(string output, List<HavokReader.havokConvexMesh> convexList, string sector)
        {
            //IL_0095: Unknown result type (might be due to invalid IL or missing references)
            //IL_009a: Expected O, but got Unknown
            Form1 form = Form.ActiveForm as Form1;
            string[] array = sector.Split('-');
            int result = -1;
            int result2 = -1;
            int result3 = -1;
            int result4 = -1;
            int.TryParse(array[1], out result);
            int.TryParse(array[2], out result2);
            int.TryParse(array[3], out result3);
            int.TryParse(array[4], out result4);
            if (result == -1 || result2 == -1 || result3 == -1 || result4 == -1)
            {
                form.Logger("in order to export StaticHavoK Collisions, a valid sector name must be specificied (e.g. Sector-X-X-X-X)", 3);
            }
            else
            {
                if (File.Exists(output))
                {
                    File.Copy(output, output + ".BAK", true);
                }
                using (BinaryWriter binaryWriter = new BinaryWriter((Stream)File.Open(output, 2, 3, 3)))
                {
                    binaryWriter.Write((short)514);
                    binaryWriter.Write((short)convexList.Count);
                    binaryWriter.Write(0);
                    binaryWriter.Write(0);
                    pUtilitaires.HavokPadding(binaryWriter, 205);
                    binaryWriter.Write(14829735431805717965uL);
                    binaryWriter.Write(0);
                    binaryWriter.Write(3452816845u);
                    for (int i = 0; i < convexList.Count; i++)
                    {
                        binaryWriter.Write(pUtilitaires.hexify(convexList[i].hash));
                        binaryWriter.Write(i);
                        binaryWriter.Write(3);
                        binaryWriter.Write(3452816845u);
                        pUtilitaires.HavokPadding(binaryWriter, 0);
                    }
                    int num = Convert.ToInt32(binaryWriter.BaseStream.Position);
                    binaryWriter.Write(HavokP.HavokRoadHeader);
                    Convert.ToInt32(binaryWriter.BaseStream.Position);
                    long num2 = -1L;
                    long num3 = -1L;
                    long num4 = -1L;
                    long num5 = -1L;
                    long[] array2 = new long[4];
                    List<long> list = new List<long>();
                    List<long> list2 = new List<long>();
                    List<long> list3 = new List<long>();
                    List<long> list4 = new List<long>();
                    List<long> list5 = new List<long>();
                    List<long> list6 = new List<long>();
                    List<long> list7 = new List<long>();
                    List<long> list8 = new List<long>();
                    List<long> list9 = new List<long>();
                    List<long> list10 = new List<long>();
                    binaryWriter.Write(Encoding.get_ASCII().GetBytes("__classnames__"));
                    pUtilitaires.HavokPadding(binaryWriter, 0);
                    binaryWriter.Write(4278190080u);
                    binaryWriter.Write(HavokP.HavokRoadHeader.Length + 240);
                    binaryWriter.Write(96);
                    binaryWriter.Write(96);
                    binaryWriter.Write(96);
                    binaryWriter.Write(96);
                    binaryWriter.Write(96);
                    binaryWriter.Write(uint.MaxValue);
                    binaryWriter.Write(Encoding.get_ASCII().GetBytes("__classindex__"));
                    pUtilitaires.HavokPadding(binaryWriter, 0);
                    binaryWriter.Write(4278190080u);
                    binaryWriter.Write(HavokP.HavokRoadHeader.Length + 240 + 96);
                    binaryWriter.Write(288);
                    binaryWriter.Write(288);
                    binaryWriter.Write(288);
                    binaryWriter.Write(288);
                    binaryWriter.Write(288);
                    binaryWriter.Write(uint.MaxValue);
                    binaryWriter.Write(Encoding.get_ASCII().GetBytes("__dataindex__"));
                    pUtilitaires.HavokPadding(binaryWriter, 0);
                    binaryWriter.Write(4278190080u);
                    binaryWriter.Write(HavokP.HavokRoadHeader.Length + 240 + 96 + 288);
                    num2 = binaryWriter.BaseStream.Position;
                    binaryWriter.Write(0);
                    binaryWriter.Write(0);
                    binaryWriter.Write(0);
                    binaryWriter.Write(0);
                    binaryWriter.Write(0);
                    binaryWriter.Write(uint.MaxValue);
                    binaryWriter.Write(Encoding.get_ASCII().GetBytes("__data__"));
                    pUtilitaires.HavokPadding(binaryWriter, 0);
                    binaryWriter.Write(4278190080u);
                    num4 = binaryWriter.BaseStream.Position;
                    binaryWriter.Write(0);
                    binaryWriter.Write(0);
                    binaryWriter.Write(0);
                    binaryWriter.Write(0);
                    binaryWriter.Write(uint.MaxValue);
                    binaryWriter.Write(0);
                    binaryWriter.Write(uint.MaxValue);
                    binaryWriter.Write(Encoding.get_ASCII().GetBytes("__types__"));
                    pUtilitaires.HavokPadding(binaryWriter, 0);
                    binaryWriter.Write(HavokP.HavokRoadDebutBlocHead);
                    num5 = binaryWriter.BaseStream.Position;
                    binaryWriter.Write(0);
                    binaryWriter.Write(9088);
                    binaryWriter.Write(11120);
                    binaryWriter.Write(11744);
                    binaryWriter.Write(uint.MaxValue);
                    binaryWriter.Write(11744);
                    binaryWriter.Write(uint.MaxValue);
                    binaryWriter.Write(HavokP.HavokRoadClassName);
                    binaryWriter.Write(HavokP.HavokRoadClassIndex);
                    long position = binaryWriter.BaseStream.Position;
                    binaryWriter.Write(HavokP.HavokRoadDataB1C1);
                    for (int j = 0; j < convexList.Count; j++)
                    {
                        if (j != 0)
                        {
                            binaryWriter.Write(3);
                        }
                        list.Add(binaryWriter.BaseStream.Position);
                        binaryWriter.Write(0);
                        binaryWriter.Write(18);
                        binaryWriter.Write(3);
                        binaryWriter.Write(0);
                        binaryWriter.Write(10);
                        binaryWriter.Write(3);
                        binaryWriter.Write(0);
                        binaryWriter.Write(1);
                        binaryWriter.Write(3);
                        binaryWriter.Write(0);
                        binaryWriter.Write(0);
                        if (j == 0)
                        {
                            binaryWriter.Write(4);
                            binaryWriter.Write(2016);
                            binaryWriter.Write(uint.MaxValue);
                            binaryWriter.Write(3);
                        }
                        else
                        {
                            binaryWriter.Write(3);
                        }
                        list3.Add(binaryWriter.BaseStream.Position);
                        binaryWriter.Write(0);
                        binaryWriter.Write(8);
                        binaryWriter.Write(3);
                        list5.Add(binaryWriter.BaseStream.Position);
                        binaryWriter.Write(0);
                        binaryWriter.Write(12);
                        if (j < 1)
                        {
                            binaryWriter.Write(4);
                        }
                        if (j == 0)
                        {
                            binaryWriter.Write(7232);
                            binaryWriter.Write(uint.MaxValue);
                        }
                    }
                    binaryWriter.Write(4);
                    binaryWriter.Write(8496);
                    pUtilitaires.HavokPadding(binaryWriter, byte.MaxValue);
                    num3 = binaryWriter.BaseStream.Position - position;
                    long position2 = binaryWriter.BaseStream.Position;
                    for (short num6 = 0; num6 < 12; num6 = (short)(num6 + 1))
                    {
                        binaryWriter.Write((byte)0);
                    }
                    binaryWriter.Write(convexList.Count);
                    binaryWriter.Write((short)convexList.Count);
                    binaryWriter.Write(HavokP.HavokMeshOpeningStorage);
                    for (short num7 = 0; num7 < convexList.Count; num7 = (short)(num7 + 1))
                    {
                        binaryWriter.Write(0);
                    }
                    pUtilitaires.HavokPadding(binaryWriter, 0);
                    int num8 = 6;
                    for (int k = 0; k < convexList.Count; k++)
                    {
                        list2.Add(binaryWriter.BaseStream.Position - position2);
                        binaryWriter.Write(HavokP.HavokMeshHeaderC1);
                        binaryWriter.Write(++num8);
                        binaryWriter.Write(HavokP.HavokMeshHeaderC2);
                        binaryWriter.Write(Convert.ToInt16(k + 1));
                        binaryWriter.Write(HavokP.HavokMeshHeaderC3);
                        binaryWriter.Write(0L);
                        binaryWriter.Write(HavokP.HavokMeshHeaderC3B);
                        binaryWriter.Write(k);
                        binaryWriter.Write(0);
                        binaryWriter.Write(HavokP.HavokMeshHeaderC4);
                        binaryWriter.Write(convexList[k].vertexList.Count);
                        binaryWriter.Write(0);
                        binaryWriter.Write((short)257);
                        binaryWriter.Write((short)0);
                        binaryWriter.Write(6);
                        binaryWriter.Write(convexList[k].faceList.Count / 3);
                        binaryWriter.Write(0);
                        binaryWriter.Write(1);
                        binaryWriter.Write(0);
                        binaryWriter.Write(HavokP.HavokMeshHeaderC5);
                        binaryWriter.Write(convexList[k].vertexList.Count * 3);
                        binaryWriter.Write(Convert.ToInt16(convexList[k].vertexList.Count * 3));
                        binaryWriter.Write((byte)0);
                        binaryWriter.Write((byte)192);
                        binaryWriter.Write(0);
                        binaryWriter.Write(convexList[k].faceList.Count);
                        binaryWriter.Write(Convert.ToInt16(convexList[k].faceList.Count));
                        binaryWriter.Write((byte)0);
                        binaryWriter.Write((byte)192);
                        binaryWriter.Write(0L);
                        binaryWriter.Write((short)0);
                        binaryWriter.Write((byte)0);
                        binaryWriter.Write((byte)192);
                        binaryWriter.Write(0);
                        binaryWriter.Write(convexList[k].faceList.Count / 3);
                        binaryWriter.Write(Convert.ToInt16(convexList[k].faceList.Count / 3));
                        binaryWriter.Write(HavokP.HavokMeshHeaderC6);
                        for (int l = 0; l < convexList[k].vertexList.Count; l++)
                        {
                            binaryWriter.Write(convexList[k].vertexList[l].x);
                            binaryWriter.Write(convexList[k].vertexList[l].y);
                            binaryWriter.Write(convexList[k].vertexList[l].z);
                        }
                        pUtilitaires.HavokPadding(binaryWriter, 0);
                        list9.Add(binaryWriter.BaseStream.Position - position2);
                        for (int m = 0; m < convexList[k].faceList.Count; m++)
                        {
                            binaryWriter.Write(convexList[k].faceList[m]);
                        }
                        pUtilitaires.HavokPadding(binaryWriter, 0);
                        list10.Add(binaryWriter.BaseStream.Position - position2);
                        for (int n = 0; n < convexList[k].faceList.Count / 3; n++)
                        {
                            binaryWriter.Write((byte)3);
                        }
                        pUtilitaires.HavokPadding(binaryWriter, 0);
                        binaryWriter.Write(HavokP.HavokMeshMoppOpening);
                        list4.Add(binaryWriter.BaseStream.Position - position2);
                        binaryWriter.Write((byte)0);
                        pUtilitaires.HavokPadding(binaryWriter, 0);
                        binaryWriter.Write(convexList[k].vertexList.Min((Utilitaires.Vertex y) => y.x) - 0.04999f);
                        binaryWriter.Write(convexList[k].vertexList.Min((Utilitaires.Vertex y) => y.y) - 0.04999f);
                        binaryWriter.Write(convexList[k].vertexList.Min((Utilitaires.Vertex y) => y.z) - 0.04999f);
                        binaryWriter.Write(16581375f / (convexList[k].vertexList.Max((Utilitaires.Vertex y) => y.z) - convexList[k].vertexList.Min((Utilitaires.Vertex y) => y.z)));
                        binaryWriter.Write(0);
                        list8.Add(binaryWriter.BaseStream.Position);
                        binaryWriter.Write(0);
                        binaryWriter.Write((short)0);
                        binaryWriter.Write((byte)0);
                        binaryWriter.Write((byte)192);
                        binaryWriter.Write(0);
                        float num27 = convexList[k].vertexList.Max((Utilitaires.Vertex y) => y.x) / 255f;
                        float num28 = convexList[k].vertexList.Max((Utilitaires.Vertex y) => y.y) / 255f;
                        float num29 = convexList[k].vertexList.Max((Utilitaires.Vertex y) => y.z) / 255f;
                        long position3 = binaryWriter.BaseStream.Position;
                        for (int num9 = 0; num9 < convexList[k].faceList.Count / 3; num9++)
                        {
                            binaryWriter.Write((byte)21);
                            binaryWriter.Write(byte.MaxValue);
                            binaryWriter.Write((byte)0);
                            if (num9 > 255)
                            {
                                binaryWriter.Write((byte)3);
                                binaryWriter.Write((byte)81);
                                binaryWriter.Write(Convert.ToByte(num9 / 255));
                                binaryWriter.Write(Convert.ToByte(num9 - 255 * (num9 / 255)));
                            }
                            else
                            {
                                binaryWriter.Write((byte)2);
                                binaryWriter.Write((byte)80);
                                binaryWriter.Write(Convert.ToByte(num9));
                            }
                        }
                        binaryWriter.Write((byte)48);
                        list7.Add(binaryWriter.BaseStream.Position - position3);
                        pUtilitaires.HavokPadding(binaryWriter, 0);
                        list6.Add(binaryWriter.BaseStream.Position - position2);
                        binaryWriter.Write(HavokP.HavokMeshMoppClosing);
                        float num10 = (result4 % 2 == 0) ? ((float)(result3 * 768)) : ((float)(result3 * 768 + 384));
                        float num11 = (float)(result4 * 768);
                        binaryWriter.Write(num10);
                        binaryWriter.Write(0.0002001f);
                        binaryWriter.Write(num11);
                        binaryWriter.Write(1f);
                        binaryWriter.Write(num10);
                        binaryWriter.Write(0.0002001f);
                        binaryWriter.Write(num11);
                        binaryWriter.Write(0);
                        binaryWriter.Write(num10);
                        binaryWriter.Write(0.0002001f);
                        binaryWriter.Write(num11);
                        binaryWriter.Write(0);
                        binaryWriter.Write(0L);
                        binaryWriter.Write(0);
                        binaryWriter.Write(1f);
                        binaryWriter.Write(0L);
                        binaryWriter.Write(0);
                        binaryWriter.Write(1f);
                        for (int num12 = 0; num12 < 32; num12++)
                        {
                            binaryWriter.Write((byte)0);
                        }
                        binaryWriter.Write((float)(int)convexList[k].vertexList.Max((Utilitaires.Vertex y) => y.x) + 400f);
                        binaryWriter.Write(HavokP.HavokMeshEnding);
                    }
                    array2[0] = binaryWriter.BaseStream.Position;
                    binaryWriter.Write(8);
                    binaryWriter.Write(64);
                    for (int num13 = 0; num13 < convexList.Count; num13++)
                    {
                        binaryWriter.Write((int)list2[num13] + 68);
                        binaryWriter.Write((int)list2[num13] + 208);
                        binaryWriter.Write((int)list2[num13] + 356);
                        binaryWriter.Write((int)list2[num13] + 400);
                        binaryWriter.Write((int)list2[num13] + 384);
                        binaryWriter.Write((int)list2[num13] + 448);
                        binaryWriter.Write((int)list2[num13] + 464);
                        binaryWriter.Write((int)list2[num13] + 544);
                        binaryWriter.Write((int)list2[num13] + 476);
                        binaryWriter.Write((int)list9[num13]);
                        binaryWriter.Write((int)list2[num13] + 500);
                        binaryWriter.Write((int)list10[num13]);
                        binaryWriter.Write((int)list2[num13] + 512);
                        binaryWriter.Write((int)list4[num13] - 16);
                        binaryWriter.Write((int)list4[num13] + 32);
                        binaryWriter.Write((int)list4[num13] + 48);
                    }
                    pUtilitaires.HavokPadding(binaryWriter, byte.MaxValue);
                    new List<long>();
                    new List<long>();
                    array2[1] = binaryWriter.BaseStream.Position;
                    int num14 = 64;
                    binaryWriter.Write(num14);
                    for (int num15 = 0; num15 < convexList.Count; num15++)
                    {
                        binaryWriter.Write(3);
                        binaryWriter.Write((int)list2[num15]);
                        if (num15 != convexList.Count - 1)
                        {
                            binaryWriter.Write(num14 += 4);
                        }
                    }
                    for (int num16 = 0; num16 < convexList.Count; num16++)
                    {
                        binaryWriter.Write((int)list2[num16] + 20);
                        binaryWriter.Write(3);
                        binaryWriter.Write((int)(list2[num16] + 288));
                        binaryWriter.Write((int)(list2[num16] + 80));
                        binaryWriter.Write(3);
                        binaryWriter.Write((int)list6[num16]);
                        binaryWriter.Write((int)(list2[num16] + 300));
                        binaryWriter.Write(3);
                        binaryWriter.Write((int)(list2[num16] + 320));
                        binaryWriter.Write((int)(list2[num16] + 304));
                        binaryWriter.Write(3);
                        binaryWriter.Write((int)list4[num16]);
                        binaryWriter.Write((int)(list2[num16] + 448));
                        binaryWriter.Write(3);
                        binaryWriter.Write((int)(list2[num16] + 464));
                    }
                    pUtilitaires.HavokPadding(binaryWriter, byte.MaxValue);
                    array2[2] = binaryWriter.BaseStream.Position;
                    binaryWriter.Write(0L);
                    binaryWriter.Write(30);
                    for (int num17 = 0; num17 < convexList.Count; num17++)
                    {
                        binaryWriter.Write((int)list2[num17]);
                        binaryWriter.Write(0);
                        binaryWriter.Write(64);
                        binaryWriter.Write((int)list2[num17] + 288);
                        binaryWriter.Write(0);
                        binaryWriter.Write(46);
                        binaryWriter.Write((int)(list2[num17] + 320));
                        binaryWriter.Write(0);
                        binaryWriter.Write(76);
                        binaryWriter.Write((int)list4[num17]);
                        binaryWriter.Write(0);
                        binaryWriter.Write(19);
                        binaryWriter.Write((int)list6[num17]);
                        binaryWriter.Write(0L);
                    }
                    pUtilitaires.HavokPadding(binaryWriter, byte.MaxValue);
                    array2[3] = binaryWriter.BaseStream.Position;
                    binaryWriter.Write(HavokP.HavokRoadFooter);
                    int num18 = Convert.ToInt32(binaryWriter.BaseStream.Position);
                    binaryWriter.Seek(24, SeekOrigin.Begin);
                    binaryWriter.Write(num18 - num);
                    binaryWriter.Seek((int)num2, SeekOrigin.Begin);
                    binaryWriter.Write((int)num3);
                    binaryWriter.Write((int)num3);
                    binaryWriter.Write((int)num3);
                    binaryWriter.Write((int)num3);
                    binaryWriter.Write((int)num3);
                    int num19 = (int)(num3 + (HavokP.HavokRoadHeader.Length + 240 + 96 + 288));
                    binaryWriter.Seek((int)num4, SeekOrigin.Begin);
                    binaryWriter.Write(num19);
                    binaryWriter.Write((int)(array2[0] - num - num19));
                    binaryWriter.Write((int)(array2[1] - num - num19));
                    binaryWriter.Write((int)(array2[2] - num - num19));
                    binaryWriter.Write(uint.MaxValue);
                    binaryWriter.Write((int)(array2[3] - num - num19));
                    binaryWriter.Seek((int)num5, SeekOrigin.Begin);
                    binaryWriter.Write((int)(array2[3] - num));
                    for (int num20 = 0; num20 < list.Count; num20++)
                    {
                        binaryWriter.Seek((int)list[num20], SeekOrigin.Begin);
                        binaryWriter.Write((int)list2[num20]);
                        binaryWriter.Seek((int)list[num20] + 12, SeekOrigin.Begin);
                        binaryWriter.Write((int)(list2[num20] + 288));
                        binaryWriter.Seek((int)list[num20] + 24, SeekOrigin.Begin);
                        binaryWriter.Write((int)(list2[num20] + 288 + 32));
                        binaryWriter.Seek((int)list[num20] + 36, SeekOrigin.Begin);
                        binaryWriter.Write((int)(list2[num20] + 288 + 32 + 144));
                    }
                    for (int num21 = 0; num21 < list3.Count; num21++)
                    {
                        binaryWriter.Seek((int)list3[num21], SeekOrigin.Begin);
                        binaryWriter.Write((int)list4[num21]);
                    }
                    for (int num22 = 0; num22 < list5.Count; num22++)
                    {
                        binaryWriter.Seek((int)list5[num22], SeekOrigin.Begin);
                        long num23;
                        do
                        {
                            List<long> list11;
                            int index;
                            (list11 = list6)[index = num22] = (num23 = list11[index]) + 1;
                        } while (num23 % 16 != 0);
                        BinaryWriter binaryWriter2 = binaryWriter;
                        List<long> list12;
                        int index2;
                        long num25 = (list12 = list6)[index2 = num22] = list12[index2] - 1;
                        binaryWriter2.Write((int)num25);
                    }
                    for (int num26 = 0; num26 < list8.Count; num26++)
                    {
                        binaryWriter.Seek((int)list8[num26], SeekOrigin.Begin);
                        binaryWriter.Write((int)list7[num26]);
                        binaryWriter.Write((short)list7[num26]);
                    }
                }
            }
        }
    }
}
