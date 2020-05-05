using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TDUModdingLibrary.labs.WorldEditor
{
    internal class _2DM_Common
    {
        public struct sMAT
        {
            public string hash;

            public string shader;

            public List<sLAYER> layerList;

            public List<sUNKNOWN> unkList;

            public byte alpha;

            public byte[] saturation;

            public float[] reflectionLayerScale;

            public float[] diffuse;

            public float[] specular;

            public float[] ambient;
        }

        public struct sUNKNOWN
        {
            public int id;

            public int unk1;

            public int unk2;

            public int unk3;
        }

        public struct sLAYER
        {
            public string parameter;

            public string texture;

            public string animationHash;

            public byte flag1;

            public byte flag2;

            public byte flag3;

            public byte flag4;
        }

        public enum ShaderEnum
        {
            NONE,
            DEFAULT,
            ASPHALT,
            ASPHALT_2,
            ASPHALT_ARROWS,
            ASPHALT_ARROWS_2,
            ASPHALT_COLORMAP,
            ASPHALT_COLORMAP_2,
            ASPHALT_COLORMAP_3,
            ASPHALT_DECAL,
            ASPHALT_DECAL_2,
            ASPHALT_DECAL_3,
            ASPHALT_TUNNEL,
            ASPHALT_TUNNEL_ARROWS,
            BORD_BLANC,
            COLOR_BLEND,
            COLOR_BLEND_2,
            ECUME,
            ECUME_2,
            ECUME_3,
            ECUME_4,
            HEIGHTMAP,
            ILLUMINATION,
            LAC,
            OBJECT_ALPHA,
            OBJECT_COLOR,
            OBJECT_COLOR_2,
            OBJECT_COLOR_3,
            OBJECT_COLOR_4,
            OBJECT_COLOR_5,
            OBJECT_COLOR_REFLECTIVE,
            OBJECT_DETAIL,
            OBJECT_DETAIL_2,
            OBJECT_DETAIL_3,
            OBJECT_DETAIL_4,
            OBJECT_DETAIL_5,
            OBJECT_DETAIL_6,
            OBJECT_GLOSS,
            OBJECT_GLOSS_2,
            OBJECT_GLOSS_3,
            OBJECT_GLOSS_4,
            OBJECT_GLOSS_5,
            OBJECT_GLOSS_6,
            OBJECT_GLOSS_7,
            OBJECT_GLOSS_8,
            OBJECT_GLOSS_9,
            OBJECT_ILLUMINATED,
            OBJECT_SHADOW,
            VAGUE,
            VAGUE_2,
            VAGUE_3,
            VEGETATION_COLOR,
            VEGETATION_COLOR_2,
            VEGETATION_NORMAL,
            VEGETATION_IMPOSTOR
        }

        public Dictionary<string, string> hashDico;

        public Dictionary<string, byte[]> hashDicoBin;

        private bool isIni;

        public string ShaderConfCatalog(byte[] data)
        {
            MaterialPieces materialPieces = new MaterialPieces();
            if (data.SequenceEqual(materialPieces.SHADER_DEFAULT))
            {
                return "DEFAULT";
            }
            if (data.SequenceEqual(materialPieces.SHADER_NOTFOUND))
            {
                return "ERROR";
            }
            if (data.SequenceEqual(materialPieces.SHADER_VAGUE))
            {
                return "VAGUE";
            }
            if (data.SequenceEqual(materialPieces.SHADER_VEGETATION_COLOR))
            {
                return "VEGETATION_COLOR";
            }
            if (data.SequenceEqual(materialPieces.SHADER_VEGETATION_COLOR2))
            {
                return "VEGETATION_COLOR_2";
            }
            if (data.SequenceEqual(materialPieces.SHADER_VEGETATION_NORMAL))
            {
                return "VEGETATION_NORMAL";
            }
            if (data.SequenceEqual(materialPieces.SHADER_VEGETATION_IMPOSTEUR))
            {
                return "VEGETATION_IMPOSTOR";
            }
            if (data.SequenceEqual(materialPieces.SHADER_VAGUE2))
            {
                return "VAGUE_2";
            }
            if (data.SequenceEqual(materialPieces.SHADER_VAGUE3))
            {
                return "VAGUE_3";
            }
            if (data.SequenceEqual(materialPieces.SHADER_LAC))
            {
                return "LAC";
            }
            if (data.SequenceEqual(materialPieces.SHADER_BORD_BLANC))
            {
                return "BORD_BLANC";
            }
            if (data.SequenceEqual(materialPieces.SHADER_ECUME))
            {
                return "ECUME";
            }
            if (data.SequenceEqual(materialPieces.SHADER_ECUME2))
            {
                return "ECUME_2";
            }
            if (data.SequenceEqual(materialPieces.SHADER_COLOR_BLEND2))
            {
                return "COLOR_BLEND_2";
            }
            if (data.SequenceEqual(materialPieces.SHADER_ECUME3))
            {
                return "ECUME_3";
            }
            if (data.SequenceEqual(materialPieces.SHADER_ECUME4))
            {
                return "ECUME_4";
            }
            if (data.SequenceEqual(materialPieces.SHADER_ASPHALT_TUNNEL_ARROWS))
            {
                return "ASPHALT_TUNNEL_ARROWS";
            }
            if (data.SequenceEqual(materialPieces.SHADER_ASPHALT))
            {
                return "ASPHALT";
            }
            if (data.SequenceEqual(materialPieces.SHADER_ASPHALT2))
            {
                return "ASPHALT_2";
            }
            if (data.SequenceEqual(materialPieces.SHADER_ASPHALT_ARROWS))
            {
                return "ASPHALT_ARROWS";
            }
            if (data.SequenceEqual(materialPieces.SHADER_ASPHALT_ARROWS2))
            {
                return "ASPHALT_ARROWS_2";
            }
            if (data.SequenceEqual(materialPieces.SHADER_ASPHALT_CROSSROAD))
            {
                return "ASPHALT_CROSSROAD";
            }
            if (data.SequenceEqual(materialPieces.SHADER_ASPHALT_CROSSROAD2))
            {
                return "ASPHALT_CROSSROAD_2";
            }
            if (data.SequenceEqual(materialPieces.SHADER_ASPHALT_COLORMAP3))
            {
                return "ASPHALT_COLORMAP_3";
            }
            if (data.SequenceEqual(materialPieces.SHADER_ASPHALT_TUNNEL))
            {
                return "ASPHALT_TUNNEL";
            }
            if (data.SequenceEqual(materialPieces.SHADER_OBJECT_ALPHA))
            {
                return "COLORMAP_ALPHA";
            }
            if (data.SequenceEqual(materialPieces.SHADER_OBJECT_GLOSS))
            {
                return "COLORMAP_GLOSS";
            }
            if (data.SequenceEqual(materialPieces.SHADER_OBJECT_GLOSS2))
            {
                return "COLORMAP_GLOSS_2";
            }
            if (data.SequenceEqual(materialPieces.SHADER_OBJECT_GLOSS3))
            {
                return "COLORMAP_GLOSS_3";
            }
            if (data.SequenceEqual(materialPieces.SHADER_OBJECT_GLOSS4))
            {
                return "COLORMAP_GLOSS_4";
            }
            if (data.SequenceEqual(materialPieces.SHADER_OBJECT_GLOSS5))
            {
                return "COLORMAP_GLOSS_5";
            }
            if (data.SequenceEqual(materialPieces.SHADER_OBJECT_GLOSS6))
            {
                return "COLORMAP_GLOSS_6";
            }
            if (data.SequenceEqual(materialPieces.SHADER_OBJECT_GLOSS7))
            {
                return "COLORMAP_GLOSS_7";
            }
            if (data.SequenceEqual(materialPieces.SHADER_OBJECT_GLOSS8))
            {
                return "COLORMAP_GLOSS_8";
            }
            if (data.SequenceEqual(materialPieces.SHADER_OBJECT_GLOSS9))
            {
                return "COLORMAP_GLOSS_9";
            }
            if (data.SequenceEqual(materialPieces.SHADER_OBJECT_DETAIL))
            {
                return "COLORMAP_DETAIL";
            }
            if (data.SequenceEqual(materialPieces.SHADER_OBJECT_DETAIL2))
            {
                return "COLORMAP_DETAIL_2";
            }
            if (data.SequenceEqual(materialPieces.SHADER_OBJECT_DETAIL3))
            {
                return "COLORMAP_DETAIL_3";
            }
            if (data.SequenceEqual(materialPieces.SHADER_OBJECT_DETAIL4))
            {
                return "COLORMAP_DETAIL_4";
            }
            if (data.SequenceEqual(materialPieces.SHADER_OBJECT_DETAIL5))
            {
                return "COLORMAP_DETAIL_5";
            }
            if (data.SequenceEqual(materialPieces.SHADER_OBJECT_COLOR))
            {
                return "COLORMAP_COLOR";
            }
            if (data.SequenceEqual(materialPieces.SHADER_OBJECT_COLOR2))
            {
                return "COLORMAP_COLOR_2";
            }
            if (data.SequenceEqual(materialPieces.SHADER_OBJECT_DETAIL6))
            {
                return "COLORMAP_DETAIL_6";
            }
            if (data.SequenceEqual(materialPieces.SHADER_ASPHALT_DECAL))
            {
                return "ASPHALT_DECAL";
            }
            if (data.SequenceEqual(materialPieces.SHADER_ASPHALT_DECAL2))
            {
                return "ASPHALT_DECAL_2";
            }
            if (data.SequenceEqual(materialPieces.SHADER_ASPHALT_DECAL3))
            {
                return "ASPHALT_DECAL_3";
            }
            if (data.SequenceEqual(materialPieces.SHADER_OBJECT_COLOR3))
            {
                return "COLORMAP_COLOR_3";
            }
            if (data.SequenceEqual(materialPieces.SHADER_OBJECT_COLOR5))
            {
                return "COLORMAP_COLOR_5";
            }
            if (data.SequenceEqual(materialPieces.SHADER_COLOR_BLEND))
            {
                return "COLOR_BLEND";
            }
            if (data.SequenceEqual(materialPieces.SHADER_OBJECT_SHADOW))
            {
                return "COLORMAP_SHADOW";
            }
            if (data.SequenceEqual(materialPieces.SHADER_OBJECT_ILLUMINATED))
            {
                return "COLORMAP_ILLUMINATED";
            }
            if (data.SequenceEqual(materialPieces.SHADER_OBJECT_COLORR))
            {
                return "COLORMAP_COLOR_R";
            }
            if (data.SequenceEqual(materialPieces.SHADER_ILLUMINATION))
            {
                return "ILLUMINATION";
            }
            if (data.SequenceEqual(materialPieces.SHADER_HEIGHTMAP_IMPOSTOR))
            {
                return "HMAP";
            }
            return "???";
        }

        public byte[] getShaderData(ShaderEnum hashcode)
        {
            MaterialPieces materialPieces = new MaterialPieces();
            switch (hashcode)
            {
                case ShaderEnum.DEFAULT:
                    return materialPieces.SHADER_DEFAULT;
                case ShaderEnum.NONE:
                    return materialPieces.SHADER_NOTFOUND;
                case ShaderEnum.ILLUMINATION:
                    return materialPieces.SHADER_ILLUMINATION;
                case ShaderEnum.HEIGHTMAP:
                    return materialPieces.SHADER_HEIGHTMAP_IMPOSTOR;
                case ShaderEnum.VAGUE:
                    return materialPieces.SHADER_VAGUE;
                case ShaderEnum.VAGUE_2:
                    return materialPieces.SHADER_VAGUE2;
                case ShaderEnum.VAGUE_3:
                    return materialPieces.SHADER_VAGUE3;
                case ShaderEnum.BORD_BLANC:
                    return materialPieces.SHADER_BORD_BLANC;
                case ShaderEnum.LAC:
                    return materialPieces.SHADER_LAC;
                case ShaderEnum.ECUME:
                    return materialPieces.SHADER_ECUME;
                case ShaderEnum.ECUME_2:
                    return materialPieces.SHADER_ECUME2;
                case ShaderEnum.ECUME_3:
                    return materialPieces.SHADER_ECUME3;
                case ShaderEnum.ECUME_4:
                    return materialPieces.SHADER_ECUME4;
                case ShaderEnum.ASPHALT:
                    return materialPieces.SHADER_ASPHALT;
                case ShaderEnum.ASPHALT_2:
                    return materialPieces.SHADER_ASPHALT2;
                case ShaderEnum.ASPHALT_ARROWS:
                    return materialPieces.SHADER_ASPHALT_ARROWS;
                case ShaderEnum.ASPHALT_ARROWS_2:
                    return materialPieces.SHADER_ASPHALT_ARROWS2;
                case ShaderEnum.ASPHALT_COLORMAP:
                    return materialPieces.SHADER_ASPHALT_CROSSROAD;
                case ShaderEnum.ASPHALT_COLORMAP_2:
                    return materialPieces.SHADER_ASPHALT_CROSSROAD2;
                case ShaderEnum.ASPHALT_COLORMAP_3:
                    return materialPieces.SHADER_ASPHALT_COLORMAP3;
                case ShaderEnum.ASPHALT_DECAL:
                    return materialPieces.SHADER_ASPHALT_DECAL;
                case ShaderEnum.ASPHALT_DECAL_2:
                    return materialPieces.SHADER_ASPHALT_DECAL2;
                case ShaderEnum.ASPHALT_DECAL_3:
                    return materialPieces.SHADER_ASPHALT_DECAL3;
                case ShaderEnum.ASPHALT_TUNNEL:
                    return materialPieces.SHADER_ASPHALT_TUNNEL;
                case ShaderEnum.ASPHALT_TUNNEL_ARROWS:
                    return materialPieces.SHADER_ASPHALT_TUNNEL_ARROWS;
                case ShaderEnum.OBJECT_COLOR:
                    return materialPieces.SHADER_OBJECT_COLOR;
                case ShaderEnum.OBJECT_COLOR_2:
                    return materialPieces.SHADER_OBJECT_COLOR2;
                case ShaderEnum.OBJECT_COLOR_3:
                    return materialPieces.SHADER_OBJECT_COLOR3;
                case ShaderEnum.OBJECT_COLOR_5:
                    return materialPieces.SHADER_OBJECT_COLOR5;
                case ShaderEnum.OBJECT_COLOR_REFLECTIVE:
                    return materialPieces.SHADER_OBJECT_COLORR;
                case ShaderEnum.OBJECT_GLOSS:
                    return materialPieces.SHADER_OBJECT_GLOSS;
                case ShaderEnum.OBJECT_GLOSS_2:
                    return materialPieces.SHADER_OBJECT_GLOSS2;
                case ShaderEnum.OBJECT_GLOSS_3:
                    return materialPieces.SHADER_OBJECT_GLOSS3;
                case ShaderEnum.OBJECT_GLOSS_4:
                    return materialPieces.SHADER_OBJECT_GLOSS4;
                case ShaderEnum.OBJECT_GLOSS_5:
                    return materialPieces.SHADER_OBJECT_GLOSS5;
                case ShaderEnum.OBJECT_GLOSS_6:
                    return materialPieces.SHADER_OBJECT_GLOSS6;
                case ShaderEnum.OBJECT_GLOSS_7:
                    return materialPieces.SHADER_OBJECT_GLOSS7;
                case ShaderEnum.OBJECT_GLOSS_8:
                    return materialPieces.SHADER_OBJECT_GLOSS8;
                case ShaderEnum.OBJECT_GLOSS_9:
                    return materialPieces.SHADER_OBJECT_GLOSS9;
                case ShaderEnum.OBJECT_DETAIL:
                    return materialPieces.SHADER_OBJECT_DETAIL;
                case ShaderEnum.OBJECT_DETAIL_2:
                    return materialPieces.SHADER_OBJECT_DETAIL2;
                case ShaderEnum.OBJECT_DETAIL_3:
                    return materialPieces.SHADER_OBJECT_DETAIL3;
                case ShaderEnum.OBJECT_DETAIL_4:
                    return materialPieces.SHADER_OBJECT_DETAIL4;
                case ShaderEnum.OBJECT_DETAIL_5:
                    return materialPieces.SHADER_OBJECT_DETAIL5;
                case ShaderEnum.OBJECT_DETAIL_6:
                    return materialPieces.SHADER_OBJECT_DETAIL6;
                case ShaderEnum.OBJECT_ALPHA:
                    return materialPieces.SHADER_OBJECT_ALPHA;
                case ShaderEnum.OBJECT_SHADOW:
                    return materialPieces.SHADER_OBJECT_SHADOW;
                case ShaderEnum.OBJECT_ILLUMINATED:
                    return materialPieces.SHADER_OBJECT_ILLUMINATED;
                case ShaderEnum.COLOR_BLEND:
                    return materialPieces.SHADER_COLOR_BLEND;
                case ShaderEnum.COLOR_BLEND_2:
                    return materialPieces.SHADER_COLOR_BLEND2;
                case ShaderEnum.VEGETATION_COLOR:
                    return materialPieces.SHADER_VEGETATION_COLOR;
                case ShaderEnum.VEGETATION_COLOR_2:
                    return materialPieces.SHADER_VEGETATION_COLOR2;
                case ShaderEnum.VEGETATION_IMPOSTOR:
                    return materialPieces.SHADER_VEGETATION_IMPOSTEUR;
                case ShaderEnum.VEGETATION_NORMAL:
                    return materialPieces.SHADER_VEGETATION_NORMAL;
                default:
                    return materialPieces.SHADER_NOTFOUND;
            }
        }

        public ShaderEnum ShaderCatalogImport(string hashcode)
        {
            switch (hashcode)
            {
                case "DEFAULT":
                    return ShaderEnum.DEFAULT;
                case "ERROR":
                    return ShaderEnum.NONE;
                case "VAGUE":
                    return ShaderEnum.VAGUE;
                case "VAGUE_2":
                    return ShaderEnum.VAGUE_2;
                case "VAGUE_3":
                    return ShaderEnum.VAGUE_3;
                case "BORD_BLANC":
                    return ShaderEnum.BORD_BLANC;
                case "ECUME":
                    return ShaderEnum.ECUME;
                case "LAC":
                    return ShaderEnum.LAC;
                case "ECUME_2":
                    return ShaderEnum.ECUME_2;
                case "ECUME_3":
                    return ShaderEnum.ECUME_3;
                case "ECUME_4":
                    return ShaderEnum.ECUME_4;
                case "ASPHALT":
                    return ShaderEnum.ASPHALT;
                case "ASPHALT_TUNNEL":
                    return ShaderEnum.ASPHALT_TUNNEL;
                case "ASPHALT_2":
                    return ShaderEnum.ASPHALT_2;
                case "ASPHALT_TUNNEL_ARROWS":
                    return ShaderEnum.ASPHALT_TUNNEL_ARROWS;
                case "ASPHALT_ARROWS":
                    return ShaderEnum.ASPHALT_ARROWS;
                case "ASPHALT_ARROWS_2":
                    return ShaderEnum.ASPHALT_ARROWS_2;
                case "ASPHALT_CROSSROAD":
                    return ShaderEnum.ASPHALT_COLORMAP;
                case "ASPHALT_CROSSROAD_2":
                    return ShaderEnum.ASPHALT_COLORMAP_2;
                case "ASPHALT_COLORMAP_3":
                    return ShaderEnum.ASPHALT_COLORMAP_3;
                case "COLORMAP_ALPHA":
                    return ShaderEnum.OBJECT_ALPHA;
                case "COLORMAP_COLOR":
                    return ShaderEnum.OBJECT_COLOR;
                case "ASPHALT_DECAL":
                    return ShaderEnum.ASPHALT_DECAL;
                case "ASPHALT_DECAL_2":
                    return ShaderEnum.ASPHALT_DECAL_2;
                case "ASPHALT_DECAL_3":
                    return ShaderEnum.ASPHALT_DECAL_3;
                case "COLORMAP_COLOR_2":
                    return ShaderEnum.OBJECT_COLOR_2;
                case "COLORMAP_COLOR_3":
                    return ShaderEnum.OBJECT_COLOR_3;
                case "COLORMAP_COLOR_5":
                    return ShaderEnum.OBJECT_COLOR_5;
                case "COLORMAP_COLOR_R":
                    return ShaderEnum.OBJECT_COLOR_REFLECTIVE;
                case "COLORMAP_GLOSS":
                    return ShaderEnum.OBJECT_GLOSS;
                case "COLORMAP_GLOSS_2":
                    return ShaderEnum.OBJECT_GLOSS_2;
                case "COLORMAP_GLOSS_3":
                    return ShaderEnum.OBJECT_GLOSS_3;
                case "COLORMAP_GLOSS_4":
                    return ShaderEnum.OBJECT_GLOSS_4;
                case "COLORMAP_GLOSS_5":
                    return ShaderEnum.OBJECT_GLOSS_5;
                case "COLORMAP_GLOSS_6":
                    return ShaderEnum.OBJECT_GLOSS_6;
                case "COLORMAP_GLOSS_7":
                    return ShaderEnum.OBJECT_GLOSS_7;
                case "COLORMAP_GLOSS_8":
                    return ShaderEnum.OBJECT_GLOSS_8;
                case "COLORMAP_GLOSS_9":
                    return ShaderEnum.OBJECT_GLOSS_9;
                case "COLORMAP_DETAIL":
                    return ShaderEnum.OBJECT_DETAIL;
                case "COLORMAP_DETAIL_2":
                    return ShaderEnum.OBJECT_DETAIL_2;
                case "COLORMAP_DETAIL_3":
                    return ShaderEnum.OBJECT_DETAIL_3;
                case "COLORMAP_DETAIL_4":
                    return ShaderEnum.OBJECT_DETAIL_4;
                case "COLORMAP_DETAIL_5":
                    return ShaderEnum.OBJECT_DETAIL_5;
                case "COLORMAP_DETAIL_6":
                    return ShaderEnum.OBJECT_DETAIL_6;
                case "COLORMAP_SHADOW":
                    return ShaderEnum.OBJECT_SHADOW;
                case "COLORMAP_ILLUMINATED":
                    return ShaderEnum.OBJECT_ILLUMINATED;
                case "COLOR_BLEND":
                    return ShaderEnum.COLOR_BLEND;
                case "COLOR_BLEND_2":
                    return ShaderEnum.COLOR_BLEND_2;
                case "ILLUMINATION":
                    return ShaderEnum.ILLUMINATION;
                case "HMAP":
                    return ShaderEnum.HEIGHTMAP;
                case "VEGETATION_COLOR":
                    return ShaderEnum.VEGETATION_COLOR;
                case "VEGETATION_COLOR_2":
                    return ShaderEnum.OBJECT_COLOR_4;
                case "VEGETATION_IMPOSTOR":
                    return ShaderEnum.VEGETATION_IMPOSTOR;
                case "VEGETATION_NORMAL":
                    return ShaderEnum.VEGETATION_NORMAL;
                default:
                    return ShaderEnum.NONE;
            }
        }

        public void SetupDico()
        {
            hashDico = new Dictionary<string, string>();
            hashDicoBin = new Dictionary<string, byte[]>();
            string text = "TextureDictionary.txt";
            if (!File.Exists(text))
            {
                MessageBox.Show(null, "Dictionnary cannot be found! File Hash will be alterated!", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                using (StreamReader streamReader = new StreamReader(text))
                {
                    string text2;
                    while ((text2 = streamReader.ReadLine()) != null)
                    {
                        string[] array = text2.Split('#');
                        if (array.Length == 2 && !hashDico.ContainsKey(array[0]) && !hashDicoBin.ContainsKey(array[1]))
                        {
                            hashDico.Add(array[0], array[1]);
                            byte[] array2 = new byte[8];
                            string[] array3 = array[0].Split('-');
                            short num = 0;
                            if (array3.Length != 1)
                            {
                                string[] array4 = array3;
                                foreach (string value in array4)
                                {
                                    if (num == 8)
                                    {
                                        break;
                                    }
                                    array2[num] = Convert.ToByte(value, 16);
                                    num = (short)(num + 1);
                                }
                            }
                            hashDicoBin.Add(array[1], array2);
                        }
                    }
                }
            }
        }

        public void DicoPop(string ActiveFolder)
        {
            if (!isIni)
            {
                hashDico = new Dictionary<string, string>();
                hashDicoBin = new Dictionary<string, byte[]>();
                string[] files = Directory.GetFiles(ActiveFolder, "*.DDS");
                string[] array = files;
                foreach (string path in array)
                {
                    string text = BitConverter.ToString(Encoding.get_ASCII().GetBytes(Path.GetFileNameWithoutExtension(path).ToUpper()));
                    StringBuilder stringBuilder = new StringBuilder();
                    string[] array2 = text.Split('-');
                    for (int j = 0; j < array2.Length; j++)
                    {
                        uint num = Convert.ToUInt32(array2[j], 16);
                        if (num >= 0 && num < 127)
                        {
                            stringBuilder.Append(Convert.ToChar(num));
                        }
                        else
                        {
                            stringBuilder.Append('#' + array2[j] + '#');
                        }
                    }
                    stringBuilder = stringBuilder.Replace("\0", string.Empty);
                    char[] array3 = stringBuilder.ToString().ToCharArray();
                    byte[] array4 = new byte[8];
                    short num2 = 0;
                    short num3 = 0;
                    short num4 = 0;
                    char[] array5 = array3;
                    foreach (char c in array5)
                    {
                        if (c == '#')
                        {
                            num4 = (short)(num4 + 1);
                        }
                    }
                    if (array3.Length - num4 / 2 * 4 > 8)
                    {
                        int num5 = 8;
                        int num6 = 0;
                        while (array3.Length - (num2 + num6) > 8)
                        {
                            if (array3.Length - (num2 + num6) >= 8 && num3 == 8)
                            {
                                num3 = 0;
                                num6 += num2;
                                num2 = 0;
                            }
                            if (array3[num2] == '#')
                            {
                                array4[num3] = Convert.ToByte(stringBuilder.ToString().Substring(num2 = (short)(num2 + 1), 2), 16);
                                num3 = (short)(num3 + 1);
                                num2 = (short)(num2 + 3);
                            }
                            else
                            {
                                array4[num3] = Convert.ToByte(Convert.ToUInt32(array3[num2]) + Convert.ToUInt32(array3[num5]));
                                num5++;
                                num2 = (short)(num2 + 1);
                                num3 = (short)(num3 + 1);
                            }
                        }
                        int num7 = array3.Length;
                        if (array3.Length > 8 && num4 == 0)
                        {
                            num7 = 8;
                        }
                        while (num2 < num7)
                        {
                            if (array3[num2] == '#')
                            {
                                array4[num3] = Convert.ToByte(stringBuilder.ToString().Substring(num2 = (short)(num2 + 1), 2), 16);
                                num3 = (short)(num3 + 1);
                                num2 = (short)(num2 + 2);
                            }
                            else
                            {
                                array4[num3] = Convert.ToByte(stringBuilder.ToString()[num2]);
                                num3 = (short)(num3 + 1);
                            }
                            num2 = (short)(num2 + 1);
                        }
                        text = BitConverter.ToString(array4);
                        if (!hashDico.ContainsKey(text))
                        {
                            hashDico.Add(text, Path.GetFileNameWithoutExtension(path));
                            if (!hashDicoBin.ContainsKey(Path.GetFileNameWithoutExtension(path)))
                            {
                                hashDicoBin.Add(Path.GetFileNameWithoutExtension(path), array4);
                            }
                        }
                    }
                }
                SetupDico();
                isIni = true;
            }
        }
    }
}
