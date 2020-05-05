using System;
using System.IO;
using System.Text;

namespace TDUModdingLibrary.labs.WorldEditor
{
    internal class Utilitaires
    {
        public struct Vertex
        {
            public float x;

            public float y;

            public float z;
        }

        public struct UVCoord
        {
            public float u;

            public float v;
        }

        private _2DM_Common p2DM_Common = new _2DM_Common();

        public void HavokPadding(BinaryWriter stream, byte var)
        {
            while (stream.BaseStream.Position % 16 != 0)
            {
                stream.Write(var);
            }
        }

        public byte[] hexify(string txtStr)
        {
            p2DM_Common.SetupDico();
            if (txtStr == null || txtStr.Length == 0)
            {
                return new byte[8];
            }
            if (p2DM_Common.hashDicoBin.ContainsKey(txtStr))
            {
                return p2DM_Common.hashDicoBin[txtStr];
            }
            char[] array = txtStr.ToUpper().ToCharArray();
            byte[] array2 = new byte[8];
            short num = 0;
            short num2 = 0;
            short num3 = 0;
            char[] array3 = array;
            foreach (char c in array3)
            {
                if (c == '%')
                {
                    num3 = (short)(num3 + 1);
                }
            }
            if (array.Length - num3 / 2 * 4 > 8)
            {
                int num4 = 8;
                int num5 = 0;
                while (array.Length - (num + num5) > 8)
                {
                    if (array.Length - (num + num5) >= 8 && num2 == 8)
                    {
                        num2 = 0;
                        num5 += num;
                        num = 0;
                    }
                    if (array[num] == '%')
                    {
                        array2[num2] = Convert.ToByte(txtStr.Substring(num = (short)(num + 1), 2), 16);
                        num2 = (short)(num2 + 1);
                        num = (short)(num + 3);
                    }
                    else
                    {
                        array2[num2] = Convert.ToByte(Convert.ToUInt32(array[num]) + Convert.ToUInt32(array[num4]));
                        num4++;
                        num = (short)(num + 1);
                        num2 = (short)(num2 + 1);
                    }
                }
                int num6 = array.Length;
                if (array.Length > 8 && num3 == 0)
                {
                    num6 = 8;
                }
                while (num < num6)
                {
                    if (array[num] == '%')
                    {
                        array2[num2] = Convert.ToByte(txtStr.Substring(num = (short)(num + 1), 2), 16);
                        num2 = (short)(num2 + 1);
                        num = (short)(num + 2);
                    }
                    else
                    {
                        array2[num2] = Convert.ToByte(txtStr[num]);
                        num2 = (short)(num2 + 1);
                    }
                    num = (short)(num + 1);
                }
            }
            else
            {
                int num7 = array.Length;
                if (array.Length > 8 && num3 == 0)
                {
                    num7 = 8;
                }
                while (num < num7)
                {
                    if (array[num] == '%')
                    {
                        array2[num2] = Convert.ToByte(txtStr.Substring(num = (short)(num + 1), 2), 16);
                        num2 = (short)(num2 + 1);
                        num = (short)(num + 2);
                    }
                    else
                    {
                        if (num2 == 8)
                        {
                            break;
                        }
                        array2[num2] = Convert.ToByte(txtStr[num]);
                        num2 = (short)(num2 + 1);
                    }
                    num = (short)(num + 1);
                }
            }
            while (num2 != 8)
            {
                array2[num2] = 0;
                num2 = (short)(num2 + 1);
            }
            return array2;
        }

        public string stringify(string hexStr)
        {
            if (hexStr == null)
            {
                return null;
            }
            short num = 0;
            p2DM_Common.SetupDico();
            if (p2DM_Common.hashDico.ContainsKey(hexStr))
            {
                return p2DM_Common.hashDico[hexStr];
            }
            foreach (char c in hexStr)
            {
                if (c == '-')
                {
                    num = (short)(num + 1);
                }
            }
            if (num == 0 || hexStr.Contains("sector") || hexStr.Contains("-i"))
            {
                return hexStr;
            }
            StringBuilder stringBuilder = new StringBuilder();
            string[] array = hexStr.Split('-');
            for (int j = 0; j < array.Length; j++)
            {
                if (Convert.ToUInt32(array[j], 16) != 0)
                {
                    uint num2 = Convert.ToUInt32(array[j], 16);
                    if (num2 >= 20 && num2 < 127)
                    {
                        stringBuilder.Append(Convert.ToChar(num2));
                    }
                    else
                    {
                        stringBuilder.Append('%' + array[j] + '%');
                    }
                }
            }
            return stringBuilder.ToString().Replace("\0", string.Empty).Replace("%00%", string.Empty);
        }
    }
}
