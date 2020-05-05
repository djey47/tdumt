using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TDUModdingLibrary.labs.WorldEditor
{
    internal class _2DM_Writer
    {
        public void Write2DM(string output, List<_2DM_Common.sMAT> materialList, string srcFolder)
        {
            //IL_008b: Unknown result type (might be due to invalid IL or missing references)
            //IL_0090: Expected O, but got Unknown
            Utilitaires utilitaires = new Utilitaires();
            _2DM_Common _2DM_Common = new _2DM_Common();
            MaterialPieces materialPieces = new MaterialPieces();
            Form activeForm = Form.ActiveForm;
            _2DM_Common.DicoPop(Path.GetDirectoryName(srcFolder));
            List<int> list = new List<int>();
            List<int> list2 = new List<int>();
            List<int> list3 = new List<int>();
            List<int> list4 = new List<int>();
            List<int> list5 = new List<int>();
            List<int> list6 = new List<int>();
            List<int> list7 = new List<int>();
            List<int> list8 = new List<int>();
            List<int> list9 = new List<int>();
            List<int> list10 = new List<int>();
            long num = 0L;
            if (File.Exists(output))
            {
                File.Copy(output, output + ".BAK", true);
            }
            using (BinaryWriter binaryWriter = new BinaryWriter((Stream)File.Open(output, 2, 3, 3)))
            {
                binaryWriter.Write(2L);
                num = binaryWriter.BaseStream.Position;
                binaryWriter.Write(0);
                binaryWriter.Write(1296314926);
                binaryWriter.Write(1096368460L);
                binaryWriter.Write(materialList.Count * 288 + 16);
                binaryWriter.Write(16);
                foreach (_2DM_Common.sMAT material in materialList)
                {
                    _2DM_Common.sMAT current = material;
                    list6.Add(Convert.ToInt32(binaryWriter.BaseStream.Position));
                    binaryWriter.Write(777601356L);
                    binaryWriter.Write(288);
                    binaryWriter.Write(288);
                    binaryWriter.Write(materialPieces.LAYBLANK);
                    binaryWriter.Write((short)current.layerList.Count);
                    binaryWriter.Write((ushort)52685);
                    foreach (_2DM_Common.sLAYER layer in current.layerList)
                    {
                        _2DM_Common.sLAYER current2 = layer;
                        binaryWriter.Write(utilitaires.hexify(current2.parameter));
                        binaryWriter.Write(utilitaires.hexify(current2.texture));
                        binaryWriter.Write(0L);
                        binaryWriter.Write((byte)1);
                        binaryWriter.Write((byte)2);
                        binaryWriter.Write((byte)1);
                        binaryWriter.Write((byte)0);
                        binaryWriter.Write(0);
                    }
                    for (int i = current.layerList.Count; i < 8; i++)
                    {
                        binaryWriter.Write(0L);
                        binaryWriter.Write(0L);
                        binaryWriter.Write(0L);
                        binaryWriter.Write(0L);
                    }
                }
                binaryWriter.Write(1095909712L);
                binaryWriter.Write(16 + 160 * materialList.Count);
                binaryWriter.Write(16);
                for (int j = 0; j < materialList.Count; j++)
                {
                    list7.Add(Convert.ToInt32(binaryWriter.BaseStream.Position));
                    binaryWriter.Write(777142608L);
                    binaryWriter.Write(160);
                    binaryWriter.Write(160);
                    binaryWriter.Write(_2DM_Common.getShaderData(_2DM_Common.ShaderEnum.DEFAULT));
                    binaryWriter.Write(0);
                    list4.Add(Convert.ToInt32(binaryWriter.BaseStream.Position));
                    binaryWriter.Write(list6[j]);
                    binaryWriter.Write(0L);
                }
                binaryWriter.Write(1096040781L);
                binaryWriter.Write(240 * materialList.Count() + 16 * materialList.Count() + 32);
                binaryWriter.Write(16);
                binaryWriter.Write(1213415752L);
                binaryWriter.Write(16 * materialList.Count() + 16);
                binaryWriter.Write(16 * materialList.Count() + 16);
                foreach (_2DM_Common.sMAT material2 in materialList)
                {
                    _2DM_Common.sMAT current3 = material2;
                    binaryWriter.Write(utilitaires.hexify(current3.hash));
                    list2.Add(Convert.ToInt32(binaryWriter.BaseStream.Position));
                    binaryWriter.Write(0L);
                }
                int num2 = -1;
                foreach (_2DM_Common.sMAT material3 in materialList)
                {
                    _2DM_Common.sMAT current4 = material3;
                    list8.Add(Convert.ToInt32(binaryWriter.BaseStream.Position));
                    binaryWriter.Write(777273677L);
                    binaryWriter.Write(240);
                    binaryWriter.Write(240);
                    binaryWriter.Write(materialPieces.MAT_ALPHA);
                    binaryWriter.Write(byte.MaxValue);
                    binaryWriter.Write(byte.MaxValue);
                    binaryWriter.Write(byte.MaxValue);
                    for (int k = 0; k < 118; k++)
                    {
                        binaryWriter.Write((byte)0);
                    }
                    if (current4.ambient != null)
                    {
                        binaryWriter.Write(current4.ambient[0]);
                        binaryWriter.Write(current4.ambient[1]);
                        binaryWriter.Write(current4.ambient[2]);
                        binaryWriter.Write(1f);
                        binaryWriter.Write(current4.diffuse[0]);
                        binaryWriter.Write(current4.diffuse[1]);
                        binaryWriter.Write(current4.diffuse[2]);
                        binaryWriter.Write(1f);
                        binaryWriter.Write(current4.specular[0]);
                        binaryWriter.Write(current4.specular[1]);
                        binaryWriter.Write(current4.specular[2]);
                        binaryWriter.Write(1f);
                    }
                    else
                    {
                        binaryWriter.Write(0f);
                        binaryWriter.Write(0f);
                        binaryWriter.Write(0f);
                        binaryWriter.Write(1f);
                        binaryWriter.Write(0f);
                        binaryWriter.Write(0f);
                        binaryWriter.Write(0f);
                        binaryWriter.Write(1f);
                        binaryWriter.Write(0f);
                        binaryWriter.Write(0f);
                        binaryWriter.Write(0f);
                        binaryWriter.Write(1f);
                    }
                    binaryWriter.Write(0f);
                    binaryWriter.Write(0f);
                    binaryWriter.Write(0f);
                    binaryWriter.Write(1f);
                    list5.Add(Convert.ToInt32(binaryWriter.BaseStream.Position));
                    binaryWriter.Write(list7[++num2]);
                    binaryWriter.Write(materialPieces.LAYBLANK);
                }
                long position = binaryWriter.BaseStream.Position;
                for (int l = 0; l < list2.Count; l++)
                {
                    binaryWriter.BaseStream.Seek(list2[l], SeekOrigin.Begin);
                    binaryWriter.Write(list8[l]);
                }
                binaryWriter.BaseStream.Seek(Convert.ToInt32(position), SeekOrigin.Begin);
                binaryWriter.Write(1095783501L);
                binaryWriter.Write(16);
                binaryWriter.Write(16);
                binaryWriter.Write(1094800981L);
                binaryWriter.Write(16);
                binaryWriter.Write(16);
                position = binaryWriter.BaseStream.Position;
                for (int m = 0; m < list3.Count; m++)
                {
                    binaryWriter.BaseStream.Seek(list3[m], SeekOrigin.Begin);
                    binaryWriter.Write(list10[m]);
                }
                int num3 = 16;
                for (num3 += 4 * materialList.Count() * 3; num3 % 16 != 0; num3++)
                {
                }
                binaryWriter.BaseStream.Seek(Convert.ToInt32(position), SeekOrigin.Begin);
                binaryWriter.Write(1279346002L);
                binaryWriter.Write(num3);
                binaryWriter.Write(num3);
                foreach (int item in list)
                {
                    binaryWriter.Write(item);
                }
                foreach (int item2 in list2)
                {
                    binaryWriter.Write(item2);
                }
                foreach (int item3 in list3)
                {
                    binaryWriter.Write(item3);
                }
                foreach (int item4 in list4)
                {
                    binaryWriter.Write(item4);
                }
                foreach (int item5 in list5)
                {
                    binaryWriter.Write(item5);
                }
                while (binaryWriter.BaseStream.Position % 16 != 0)
                {
                    binaryWriter.Write((byte)0);
                }
                for (int n = 0; n < list3.Count; n++)
                {
                    binaryWriter.BaseStream.Seek(list[n], SeekOrigin.Begin);
                    binaryWriter.Write(list9[n]);
                }
                int value = Convert.ToInt32(binaryWriter.BaseStream.Position);
                binaryWriter.BaseStream.Seek(num, SeekOrigin.Begin);
                binaryWriter.Write(value);
            }
        }
    }
}
