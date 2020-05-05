using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TDUModdingLibrary.labs.WorldEditor
{
    internal class _2DM_Reader
    {
        private _2DM_Common p2DM_Common = new _2DM_Common();

        private Utilitaires pUtilitaires = new Utilitaires();

        public List<_2DM_Common.sMAT> Read2DM(string input)
        {
            //IL_0029: Unknown result type (might be due to invalid IL or missing references)
            //IL_002e: Expected O, but got Unknown
            int num = 0;
            int num2 = 0;
            long num3 = 0L;
            long num4 = 0L;
            short num5 = 0;
            List<_2DM_Common.sMAT> list = new List<_2DM_Common.sMAT>();
            p2DM_Common.DicoPop(Path.GetDirectoryName(input));
            using (BinaryReader binaryReader = new BinaryReader((Stream)File.Open(input, 3, 1, 1)))
            {
                if (!isHMAP(binaryReader, Path.GetFileNameWithoutExtension(input).ToLower()))
                {
                    if (binaryReader.BaseStream.Length == 0)
                    {
                        MessageBox.Show("ERROR: empty file!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                    binaryReader.BaseStream.Seek(0L, SeekOrigin.Begin);
                    while (binaryReader.ReadUInt32() != 1096040781)
                    {
                    }
                    binaryReader.BaseStream.Seek(24L, SeekOrigin.Current);
                    num2 = (binaryReader.ReadInt32() - 16) / 16;
                    for (short num6 = 0; num6 < num2; num6 = (short)(num6 + 1))
                    {
                        _2DM_Common.sMAT item = default(_2DM_Common.sMAT);
                        List<_2DM_Common.sUNKNOWN> list2 = new List<_2DM_Common.sUNKNOWN>();
                        item.reflectionLayerScale = new float[2];
                        item.saturation = new byte[2];
                        num3 = binaryReader.BaseStream.Position;
                        item.hash = BitConverter.ToString(binaryReader.ReadBytes(8));
                        binaryReader.BaseStream.Seek(binaryReader.ReadInt32() + 39, SeekOrigin.Begin);
                        item.alpha = binaryReader.ReadByte();
                        item.saturation[0] = binaryReader.ReadByte();
                        item.saturation[1] = binaryReader.ReadByte();
                        binaryReader.BaseStream.Seek(118L, SeekOrigin.Current);
                        item.ambient = new float[4];
                        item.ambient[0] = binaryReader.ReadSingle();
                        item.ambient[1] = binaryReader.ReadSingle();
                        item.ambient[2] = binaryReader.ReadSingle();
                        item.ambient[3] = binaryReader.ReadSingle();
                        item.diffuse = new float[4];
                        item.diffuse[0] = binaryReader.ReadSingle();
                        item.diffuse[1] = binaryReader.ReadSingle();
                        item.diffuse[2] = binaryReader.ReadSingle();
                        item.diffuse[3] = binaryReader.ReadSingle();
                        item.specular = new float[4];
                        item.specular[0] = binaryReader.ReadSingle();
                        item.specular[1] = binaryReader.ReadSingle();
                        item.specular[2] = binaryReader.ReadSingle();
                        item.specular[3] = binaryReader.ReadSingle();
                        binaryReader.BaseStream.Seek(16L, SeekOrigin.Current);
                        binaryReader.BaseStream.Seek(binaryReader.ReadInt32() + 16, SeekOrigin.Begin);
                        item.shader = p2DM_Common.ShaderConfCatalog(binaryReader.ReadBytes(64));
                        int num7 = binaryReader.ReadInt32();
                        int num8 = binaryReader.ReadInt32();
                        if (num7 != 0)
                        {
                            if (num8 == 0)
                            {
                                binaryReader.BaseStream.Seek(36L, SeekOrigin.Current);
                                num8 = binaryReader.ReadInt32();
                            }
                            else
                            {
                                binaryReader.BaseStream.Seek(8L, SeekOrigin.Current);
                                for (int i = 0; i < num7; i++)
                                {
                                    _2DM_Common.sUNKNOWN item2 = default(_2DM_Common.sUNKNOWN);
                                    item2.id = binaryReader.ReadInt32();
                                    item2.unk1 = binaryReader.ReadInt32();
                                    item2.unk2 = binaryReader.ReadInt32();
                                    item2.unk3 = binaryReader.ReadInt32();
                                    list2.Add(item2);
                                }
                            }
                            item.reflectionLayerScale[0] = binaryReader.ReadSingle();
                            item.reflectionLayerScale[1] = binaryReader.ReadSingle();
                        }
                        binaryReader.BaseStream.Seek(num8 + 28, SeekOrigin.Begin);
                        num5 = binaryReader.ReadInt16();
                        binaryReader.BaseStream.Seek(2L, SeekOrigin.Current);
                        List<_2DM_Common.sLAYER> list3 = new List<_2DM_Common.sLAYER>();
                        for (short num9 = 0; num9 < num5; num9 = (short)(num9 + 1))
                        {
                            _2DM_Common.sLAYER item3 = default(_2DM_Common.sLAYER);
                            item3.parameter = pUtilitaires.stringify(BitConverter.ToString(binaryReader.ReadBytes(8)));
                            item3.texture = pUtilitaires.stringify(BitConverter.ToString(binaryReader.ReadBytes(8)));
                            binaryReader.BaseStream.Seek(8L, SeekOrigin.Current);
                            item3.flag1 = binaryReader.ReadByte();
                            item3.flag2 = binaryReader.ReadByte();
                            item3.flag3 = binaryReader.ReadByte();
                            item3.flag4 = binaryReader.ReadByte();
                            num = binaryReader.ReadInt32();
                            if (num != 0)
                            {
                                num4 = binaryReader.BaseStream.Position;
                                binaryReader.BaseStream.Seek(num + 20, SeekOrigin.Begin);
                                binaryReader.BaseStream.Seek(binaryReader.ReadInt32(), SeekOrigin.Begin);
                                item3.animationHash = BitConverter.ToString(binaryReader.ReadBytes(8));
                                binaryReader.BaseStream.Seek(num4, SeekOrigin.Begin);
                            }
                            list3.Add(item3);
                        }
                        item.layerList = list3;
                        item.unkList = list2;
                        binaryReader.BaseStream.Seek(num3 += 16, SeekOrigin.Begin);
                        list.Add(item);
                    }
                    return list;
                }
                MessageBox.Show("ERROR: heightmap material file can't be edited with this tool! Use the texture blending option during scene export instead.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private bool isHMAP(BinaryReader tduReader, string sector)
        {
            int num = 0;
            while (tduReader.ReadInt32() != 1213415752)
            {
            }
            tduReader.BaseStream.Seek(4L, SeekOrigin.Current);
            num = (tduReader.ReadInt32() - 16) / 16;
            if (num != 1)
            {
                return false;
            }
            tduReader.BaseStream.Seek(4L, SeekOrigin.Current);
            byte[] first = tduReader.ReadBytes(8);
            byte[] second = pUtilitaires.hexify(sector);
            if (first.SequenceEqual(second))
            {
                return true;
            }
            return false;
        }
    }
}
