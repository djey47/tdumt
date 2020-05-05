using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace TDUModdingLibrary.labs.WorldEditor
{
    internal class DAE_Reader
    {
        public struct sGEOMDAE
        {
            public string hashcode;

            public float LOD;

            public List<sPRIMDAE> sPRIMList;
        }

        public struct sPRIMDAE
        {
            public List<Utilitaires.Vertex> vertexList;

            public ushort[] faceList;

            public List<List<Utilitaires.UVCoord>> vertexPaintList;

            public uint[] normalList;

            public short matIndice;

            public string materialString;

            public bool bWater;
        }

        public struct sINSTDAE
        {
            public string hash;

            public string parentHash;

            public float[,] pivot;

            public List<string> textures;

            public List<sSubMesh> subMeshList;
        }

        public struct sSubMesh
        {
            public string sGEOMDATE_Hashcode;

            public float LOD;
        }

        private string parentHash;

        private XmlNamespaceManager nsmgr;

        private _2DM_Writer p2DMWriter = new _2DM_Writer();

        private _3DG_Object_Writer p3DGWriter = new _3DG_Object_Writer();

        private _3DD_Writer p3DDWriter = new _3DD_Writer();

        private _2DM_Terrain_Writer p2DMTerrainWriter = new _2DM_Terrain_Writer();

        private _3DG_Terrain_Writer p3DGTerrainWriter = new _3DG_Terrain_Writer();

        private _3DD_Terrain_Writer p3DDTerrainWriter = new _3DD_Terrain_Writer();

        private HavokWriter pSHKWriter = new HavokWriter();

        private void readNode(XmlNode node, List<sINSTDAE> instList, string parentHash, sINSTDAE inst)
        {
            Form activeForm = Form.ActiveForm;
            XmlNode xmlNode = node.SelectSingleNode("colladans:matrix", nsmgr);
            XmlNodeList xmlNodeList = node.SelectNodes("colladans:node", nsmgr);
            XmlNodeList xmlNodeList2 = node.SelectNodes("colladans:instance_geometry", nsmgr);
            float result = -1f;
            float result2 = -1f;
            float.TryParse(node.Attributes["name"].Value.Split('_')[node.Attributes["name"].Value.Split('_').Length - 1], out result);
            float.TryParse(inst.hash.Split('_')[inst.hash.Split('_').Length - 1], out result2);
            bool flag = result != -1f && result != result2 && result > 1000f && node.Attributes["name"].Value.Split('_').Length >= 3;
            string a = node.Attributes["name"].Value.Replace("_ncl1", "").Replace("-lib", "").Split('_')[0];
            string text = inst.hash.Split('_')[0];
            bool flag2 = (a == text && a == "NETWORK" && text == "NETWORK") || (a == text && a == "BLOCK" && text == "BLOCK");
            if ((xmlNodeList.Count != 0 && !flag2) || (flag && !flag2))
            {
                inst = default(sINSTDAE);
                inst.hash = node.Attributes["name"].Value.Replace("_ncl1", "").Replace("-lib", "");
                inst.parentHash = parentHash;
                inst.subMeshList = new List<sSubMesh>();
                inst.textures = new List<string>();
            }
            if (xmlNode != null)
            {
                inst.pivot = new float[4, 4];
                string[] array = xmlNode.InnerText.Split(' ');
                int num = 0;
                int num2 = 0;
                while (num < 16)
                {
                    float[,] pivot = inst.pivot;
                    int num3 = num2;
                    float num4 = Convert.ToSingle(array[num], new CultureInfo("en-US"));
                    pivot[0, num3] = num4;
                    float[,] pivot2 = inst.pivot;
                    int num5 = num2;
                    float num6 = Convert.ToSingle(array[num + 1], new CultureInfo("en-US"));
                    pivot2[1, num5] = num6;
                    float[,] pivot3 = inst.pivot;
                    int num7 = num2;
                    float num8 = Convert.ToSingle(array[num + 2], new CultureInfo("en-US"));
                    pivot3[2, num7] = num8;
                    float[,] pivot4 = inst.pivot;
                    int num9 = num2;
                    float num10 = Convert.ToSingle(array[num + 3], new CultureInfo("en-US"));
                    pivot4[3, num9] = num10;
                    num += 4;
                    num2++;
                }
            }
            if (xmlNodeList2.Count != 0)
            {
                foreach (XmlNode item2 in xmlNodeList2)
                {
                    sSubMesh item = default(sSubMesh);
                    item.sGEOMDATE_Hashcode = item2.Attributes["url"].Value.Replace("#", "");
                    float result3 = 0f;
                    string arg = null;
                    string[] array2 = item2.Attributes["url"].Value.Replace("#", "").Replace("_ncl1", "").Replace("-lib", "")
                        .Split('_');
                    if (array2.Length >= 2 && float.TryParse(array2[array2.Length - 1], out result3) && result3 >= 1000f)
                    {
                        for (int j = 0; j < array2.Length - 2; j++)
                        {
                            arg = arg + array2[j] + '_';
                        }
                    }
                    else if (array2.Length == 2 && float.TryParse(array2[array2.Length - 1], out result3))
                    {
                        if (result3 < 1000f)
                        {
                            result3 = 0f;
                        }
                        arg = array2[0];
                    }
                    else
                    {
                        arg = item2.Attributes["url"].Value.Replace("#", "");
                    }
                    item.LOD = result3;
                    inst.subMeshList.Add(item);
                    XmlNode xmlNode3 = item2.SelectSingleNode("colladans:bind_material", nsmgr);
                    if (xmlNode3 != null)
                    {
                        XmlNode xmlNode4 = xmlNode3.SelectSingleNode("colladans:technique_common", nsmgr);
                        if (xmlNode4 != null)
                        {
                            XmlNode xmlNode5 = xmlNode4.SelectSingleNode("colladans:instance_material", nsmgr);
                            if (xmlNode5 != null)
                            {
                                inst.textures.Add(xmlNode5.Attributes["symbol"].Value);
                            }
                        }
                    }
                }
            }
            if (xmlNodeList.Count != 0)
            {
                foreach (XmlNode item3 in xmlNodeList)
                {
                    readNode(item3, instList, inst.hash, inst);
                }
            }
            if (!instList.Exists((sINSTDAE i) => i.hash == inst.hash))
            {
                instList.Add(inst);
            }
        }

        private List<sINSTDAE> getHiearchy(string input)
        {
            List<sINSTDAE> list = new List<sINSTDAE>();
            Form activeForm = Form.ActiveForm;
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(input);
            XmlNode documentElement = xmlDocument.DocumentElement;
            nsmgr = new XmlNamespaceManager(xmlDocument.NameTable);
            nsmgr.AddNamespace("colladans", "http://www.collada.org/2005/11/COLLADASchema");
            XmlNode xmlNode = documentElement.SelectSingleNode("colladans:library_visual_scenes", nsmgr);
            XmlNode xmlNode2 = xmlNode.SelectSingleNode("colladans:visual_scene", nsmgr);
            XmlNode xmlNode3 = xmlNode2.SelectSingleNode("colladans:node", nsmgr);
            foreach (XmlNode item in xmlNode3)
            {
                if (item.NodeType == XmlNodeType.Element && item.Name == "node")
                {
                    switch (item.Attributes["name"].Value)
                    {
                        case "DYNAMIC":
                            {
                                sINSTDAE sINSTDAE = default(sINSTDAE);
                                sINSTDAE.subMeshList = new List<sSubMesh>();
                                sINSTDAE.textures = new List<string>();
                                sINSTDAE.parentHash = parentHash;
                                sINSTDAE.hash = "DYNAMIC";
                                parentHash = sINSTDAE.hash;
                                XmlNode xmlNode6 = item.SelectSingleNode("colladans:matrix", nsmgr);
                                if (xmlNode6 != null)
                                {
                                    sINSTDAE.pivot = new float[4, 4];
                                    string[] array = xmlNode6.InnerText.Split(' ');
                                    int num = 0;
                                    int num2 = 0;
                                    while (num < 16)
                                    {
                                        float[,] pivot = sINSTDAE.pivot;
                                        int num3 = num2;
                                        float num4 = Convert.ToSingle(array[num], new CultureInfo("en-US"));
                                        pivot[0, num3] = num4;
                                        float[,] pivot2 = sINSTDAE.pivot;
                                        int num5 = num2;
                                        float num6 = Convert.ToSingle(array[num + 1], new CultureInfo("en-US"));
                                        pivot2[1, num5] = num6;
                                        float[,] pivot3 = sINSTDAE.pivot;
                                        int num7 = num2;
                                        float num8 = Convert.ToSingle(array[num + 2], new CultureInfo("en-US"));
                                        pivot3[2, num7] = num8;
                                        float[,] pivot4 = sINSTDAE.pivot;
                                        int num9 = num2;
                                        float num10 = Convert.ToSingle(array[num + 3], new CultureInfo("en-US"));
                                        pivot4[3, num9] = num10;
                                        num += 4;
                                        num2++;
                                    }
                                }
                                list.Add(sINSTDAE);
                                XmlNodeList xmlNodeList2 = item.SelectNodes("colladans:node", nsmgr);
                                foreach (XmlNode item2 in xmlNodeList2)
                                {
                                    readNode(item2, list, parentHash, sINSTDAE);
                                }
                                break;
                            }
                        case "HAVOK":
                            foreach (XmlNode item3 in item)
                            {
                                if (item3.NodeType == XmlNodeType.Element && item3.Name == "node")
                                {
                                    switch (item3.Attributes["name"].Value)
                                    {
                                        case "ROAD":
                                            {
                                                XmlNodeList xmlNodeList = item3.SelectNodes("colladans:node", nsmgr);
                                                foreach (XmlNode item4 in xmlNodeList)
                                                {
                                                    XmlNode xmlNode7 = item4;
                                                }
                                                break;
                                            }
                                    }
                                }
                            }
                            break;
                    }
                }
            }
            return list;
        }

        public DAE_Reader(string input, string output, string sectorname, bool skip2DMObj = false, bool hmap = false, bool obj = false, bool havokr = false, string blend1 = null, string blend2 = null)
        {
            //IL_1532: Unknown result type (might be due to invalid IL or missing references)
            new Utilitaires();
            Form1 pForm = Form.ActiveForm as Form1;
            if (obj && !File.Exists("NvTriStripper.exe"))
            {
                pForm.Logger("NvTriStripper.exe not found! Make sure the .exe is in the same folder as TDU World Editor", 3);
            }
            else
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(input);
                string srcFolder = Path.GetDirectoryName(input) + '\\';
                List<_2DM_Common.sMAT> list = new List<_2DM_Common.sMAT>();
                List<_2DM_Common.sLAYER> list2 = new List<_2DM_Common.sLAYER>();
                List<sGEOMDAE> list3 = new List<sGEOMDAE>();
                List<float> list4 = new List<float>();
                List<HavokReader.havokConvexMesh> list5 = new List<HavokReader.havokConvexMesh>();
                List<sINSTDAE> hiearchy = getHiearchy(input);
                using (XmlReader xmlReader = XmlReader.Create(input))
                {
                    xmlReader.MoveToContent();
                    XmlReader exml;
                    while (xmlReader.Read())
                    {
                        if (xmlReader.IsStartElement())
                        {
                            switch (xmlReader.Name)
                            {
                                case "library_images":
                                    if (obj)
                                    {
                                        XmlReader xmlReader3 = xmlReader.ReadSubtree();
                                        xmlReader3.ReadToDescendant("image");
                                        do
                                        {
                                            _2DM_Common.sLAYER item7 = new _2DM_Common.sLAYER
                                            {
                                                parameter = xmlReader3["name"]
                                            };
                                            xmlReader3.ReadToFollowing("init_from");
                                            string[] array11 = xmlReader3.ReadString().Split('\\');
                                            item7.texture = Path.GetFileNameWithoutExtension(array11[array11.Length - 1]);
                                            list2.Add(item7);
                                        } while (xmlReader3.ReadToFollowing("image"));
                                    }
                                    break;
                                case "library_effects":
                                    if (obj)
                                    {
                                        exml = xmlReader.ReadSubtree();
                                        exml.ReadToDescendant("effect");
                                        do
                                        {
                                            XmlReader xmlReader4 = exml.ReadSubtree();
                                            if (!(exml["name"] == "TERRAIN"))
                                            {
                                                _2DM_Common.sMAT item8 = new _2DM_Common.sMAT
                                                {
                                                    layerList = new List<_2DM_Common.sLAYER>(),
                                                    hash = exml["name"]
                                                };
                                                xmlReader4.ReadToDescendant("phong");
                                                while (xmlReader4.Read())
                                                {
                                                    if (xmlReader4.ReadToDescendant("texture") && xmlReader4.NodeType != XmlNodeType.EndElement)
                                                    {
                                                        _2DM_Common.sLAYER item9 = default(_2DM_Common.sLAYER);
                                                        exml["texture"].Replace("-image", "").Replace("_SAMPLE", "");
                                                        item9 = list2.Find((_2DM_Common.sLAYER i) => i.parameter == exml["texture"].Replace("-image", "").Replace("_SAMPLE", ""));
                                                        if (item9.parameter == null)
                                                        {
                                                            continue;
                                                        }
                                                        if (item9.parameter.Split('_').Length != 0)
                                                        {
                                                            item9.parameter = item9.parameter.Split('_')[item9.parameter.Split('_').Length - 1];
                                                        }
                                                        item8.layerList.Add(item9);
                                                    }
                                                    if (xmlReader4.ReadToDescendant("color"))
                                                    {
                                                        string[] array12 = xmlReader4.ReadElementString().Split(' ');
                                                        if (xmlReader4.Name.Equals("diffuse"))
                                                        {
                                                            item8.diffuse[0] = Convert.ToSingle(array12[0], new CultureInfo("en-US"));
                                                            item8.diffuse[1] = Convert.ToSingle(array12[1], new CultureInfo("en-US"));
                                                            item8.diffuse[2] = Convert.ToSingle(array12[2], new CultureInfo("en-US"));
                                                        }
                                                        else if (xmlReader4.Name.Equals("ambient"))
                                                        {
                                                            item8.ambient[0] = Convert.ToSingle(array12[0], new CultureInfo("en-US"));
                                                            item8.ambient[1] = Convert.ToSingle(array12[1], new CultureInfo("en-US"));
                                                            item8.ambient[2] = Convert.ToSingle(array12[2], new CultureInfo("en-US"));
                                                        }
                                                        else if (xmlReader4.Name.Equals("specular"))
                                                        {
                                                            item8.specular[0] = Convert.ToSingle(array12[0], new CultureInfo("en-US"));
                                                            item8.specular[1] = Convert.ToSingle(array12[1], new CultureInfo("en-US"));
                                                            item8.specular[2] = Convert.ToSingle(array12[2], new CultureInfo("en-US"));
                                                        }
                                                    }
                                                }
                                                list.Add(item8);
                                            }
                                        } while (exml.ReadToFollowing("effect"));
                                    }
                                    break;
                                case "library_geometries":
                                    {
                                        XmlReader gxml = xmlReader.ReadSubtree();
                                        gxml.ReadToDescendant("geometry");
                                        do
                                        {
                                            HavokReader.havokConvexMesh item = default(HavokReader.havokConvexMesh);
                                            sPRIMDAE item2 = default(sPRIMDAE);
                                            XmlReader xmlReader2 = gxml.ReadSubtree();
                                            float result = 0f;
                                            string geomParent = null;
                                            bool flag = false;
                                            string[] array = gxml["name"].Replace("Mesh", "").Replace("_ncl1", "").Split('_');
                                            if (array.Length >= 2 && float.TryParse(array[array.Length - 1], out result) && result >= 1000f)
                                            {
                                                short result2;
                                                int num = (!short.TryParse(array[array.Length - 2], out result2)) ? (array.Length - 1) : (array.Length - 2);
                                                if (!item2.bWater)
                                                {
                                                    for (int j = 0; j < num; j++)
                                                    {
                                                        if (flag)
                                                        {
                                                            geomParent += '_';
                                                        }
                                                        geomParent += array[j];
                                                        if (!flag)
                                                        {
                                                            flag = true;
                                                        }
                                                    }
                                                }
                                            }
                                            else if (array[0] != "CONVEX" && array.Length == 2 && float.TryParse(array[array.Length - 1], out result))
                                            {
                                                if (result < 1000f)
                                                {
                                                    result = 0f;
                                                }
                                                geomParent = array[0];
                                            }
                                            else
                                            {
                                                geomParent = gxml["name"].Replace("Mesh", "").Replace("_ncl1", "");
                                            }
                                            int num2 = list3.FindIndex((sGEOMDAE i) => i.hashcode == geomParent);
                                            xmlReader2.ReadToDescendant("mesh");
                                            while (xmlReader2.Read())
                                            {
                                                if (xmlReader2.Name.Equals("source") && xmlReader2.NodeType != XmlNodeType.EndElement)
                                                {
                                                    string text = xmlReader2["id"].Split('_')[xmlReader2["id"].Split('_').Length - 1];
                                                    bool flag2 = false;
                                                    if (((IEnumerable<char>)text).Contains('-'))
                                                    {
                                                        text = text.Split('-')[text.Split('-').Length - 1];
                                                    }
                                                    switch (text)
                                                    {
                                                        case "POSITION":
                                                        case "P":
                                                            if (geomParent != "HMAP" && geomParent.Split('_')[0] != "CONVEX" && obj)
                                                            {
                                                                xmlReader2.ReadToFollowing("float_array");
                                                                string[] array3 = xmlReader2.ReadString().Split(' ', '\n');
                                                                item2.vertexList = new List<Utilitaires.Vertex>();
                                                                uint num4 = 0u;
                                                                while (num4 < array3.Count())
                                                                {
                                                                    if (array3[num4] == string.Empty)
                                                                    {
                                                                        num4++;
                                                                    }
                                                                    else
                                                                    {
                                                                        Utilitaires.Vertex item4 = new Utilitaires.Vertex
                                                                        {
                                                                            x = Convert.ToSingle(array3[num4], new CultureInfo("en-US")),
                                                                            y = Convert.ToSingle(array3[num4 + 1], new CultureInfo("en-US")),
                                                                            z = Convert.ToSingle(array3[num4 + 2], new CultureInfo("en-US"))
                                                                        };
                                                                        num4 += 3;
                                                                        item2.vertexList.Add(item4);
                                                                    }
                                                                }
                                                                item2.vertexPaintList = new List<List<Utilitaires.UVCoord>>();
                                                            }
                                                            else if (geomParent == "HMAP" && hmap)
                                                            {
                                                                xmlReader2.ReadToFollowing("float_array");
                                                                string[] array4 = xmlReader2.ReadString().Split(' ', '\n');
                                                                uint num5 = 0u;
                                                                while (num5 < array4.Count())
                                                                {
                                                                    if (array4[num5] == string.Empty)
                                                                    {
                                                                        num5++;
                                                                    }
                                                                    else
                                                                    {
                                                                        list4.Add(Convert.ToSingle(array4[num5 + 1], new CultureInfo("en-US")));
                                                                        num5 += 3;
                                                                    }
                                                                }
                                                            }
                                                            else if (geomParent.Split('_')[0] == "CONVEX" && havokr)
                                                            {
                                                                item.hash = geomParent;
                                                                item.vertexList = new List<Utilitaires.Vertex>();
                                                                xmlReader2.ReadToFollowing("float_array");
                                                                string[] array5 = xmlReader2.ReadString().Split(' ', '\n');
                                                                uint num6 = 0u;
                                                                while (num6 < array5.Count())
                                                                {
                                                                    if (array5[num6] == string.Empty)
                                                                    {
                                                                        num6++;
                                                                    }
                                                                    else
                                                                    {
                                                                        Utilitaires.Vertex item5 = new Utilitaires.Vertex
                                                                        {
                                                                            x = Convert.ToSingle(array5[num6], new CultureInfo("en-US")),
                                                                            y = Convert.ToSingle(array5[num6 + 1], new CultureInfo("en-US")),
                                                                            z = Convert.ToSingle(array5[num6 + 2], new CultureInfo("en-US"))
                                                                        };
                                                                        item.vertexList.Add(item5);
                                                                        num6 += 3;
                                                                    }
                                                                }
                                                            }
                                                            break;
                                                        case "Normal0":
                                                        case "NORMAL":
                                                            if (geomParent != "HMAP" && geomParent.Split('_')[0] != "CONVEX" && obj)
                                                            {
                                                                xmlReader2.ReadToFollowing("float_array");
                                                                string[] array6 = xmlReader2.ReadString().Split(' ', '\n');
                                                                item2.normalList = new uint[(array6.Length - 1) / 3];
                                                                int num7 = 0;
                                                                int num8 = 0;
                                                                while (num8 < array6.Length - 1)
                                                                {
                                                                    if (array6[num8] == string.Empty)
                                                                    {
                                                                        flag2 = true;
                                                                        num7--;
                                                                        num8 -= 2;
                                                                    }
                                                                    else
                                                                    {
                                                                        float num9 = Convert.ToSingle(array6[num8], new CultureInfo("en-US"));
                                                                        float num10 = Convert.ToSingle(array6[num8 + 1], new CultureInfo("en-US"));
                                                                        float num11 = Convert.ToSingle(array6[num8 + 2], new CultureInfo("en-US"));
                                                                        num9 = ((num9 < -1f) ? (-1f) : ((num9 > 1f) ? 1f : num9));
                                                                        num10 = ((num10 < -1f) ? (-1f) : ((num10 > 1f) ? 1f : num10));
                                                                        num11 = ((num11 < -1f) ? (-1f) : ((num11 > 1f) ? 1f : num11));
                                                                        uint num12 = (uint)((num9 < 0f) ? (512f + 511f * (num9 + 1f)) : (511f * num9));
                                                                        uint num13 = (uint)((num10 < 0f) ? (512f + 511f * (num10 + 1f)) : (511f * num10));
                                                                        uint num14 = (uint)((num11 < 0f) ? (512f + 511f * (num11 + 1f)) : (511f * num11));
                                                                        item2.normalList[num7] = (((num12 & 0x3FF) + 50) | (((num13 & 0x3FF) << 10) + 50) | (((num14 & 0x3FF) << 20) + 50));
                                                                    }
                                                                    num8 += 3;
                                                                    num7++;
                                                                }
                                                                if (flag2)
                                                                {
                                                                    Array.Resize(ref item2.normalList, num7);
                                                                }
                                                            }
                                                            break;
                                                        case "UV0":
                                                        case "0":
                                                        case "UV1":
                                                        case "1":
                                                        case "UV2":
                                                        case "2":
                                                        case "UV3":
                                                        case "3":
                                                            if (geomParent != "HMAP" && geomParent.Split('_')[0] != "CONVEX" && obj)
                                                            {
                                                                xmlReader2.ReadToFollowing("float_array");
                                                                string[] array2 = xmlReader2.ReadString().Split(' ', '\n');
                                                                List<Utilitaires.UVCoord> list6 = new List<Utilitaires.UVCoord>();
                                                                uint num3 = 0u;
                                                                while (num3 < array2.Count())
                                                                {
                                                                    if (array2[num3] == string.Empty)
                                                                    {
                                                                        num3++;
                                                                    }
                                                                    else
                                                                    {
                                                                        Utilitaires.UVCoord item3 = new Utilitaires.UVCoord
                                                                        {
                                                                            u = Convert.ToSingle(array2[num3], new CultureInfo("en-US")),
                                                                            v = Convert.ToSingle(array2[num3 + 1], new CultureInfo("en-US"))
                                                                        };
                                                                        num3 += 2;
                                                                        list6.Add(item3);
                                                                    }
                                                                }
                                                                item2.vertexPaintList.Add(list6);
                                                            }
                                                            break;
                                                    }
                                                }
                                                else if (xmlReader2.Name.Equals("triangles") && xmlReader2.NodeType != XmlNodeType.EndElement)
                                                {
                                                    item2.matIndice = Convert.ToInt16(list.FindIndex((_2DM_Common.sMAT i) => i.hash == gxml["material"]));
                                                    item2.materialString = gxml["material"];
                                                    if (xmlReader2.ReadToDescendant("input"))
                                                    {
                                                        short num15 = 1;
                                                        if (geomParent != "HMAP" && geomParent.Split('_')[0] != "CONVEX" && obj)
                                                        {
                                                            while (xmlReader2.Read())
                                                            {
                                                                if (xmlReader2.Name.Equals("input") && xmlReader2.NodeType != XmlNodeType.EndElement)
                                                                {
                                                                    num15 = (short)(num15 + 1);
                                                                }
                                                                else if (xmlReader2.Name.Equals("p") && xmlReader2.NodeType != XmlNodeType.EndElement)
                                                                {
                                                                    string[] array7 = xmlReader2.ReadString().Split(' ', '\n');
                                                                    item2.faceList = new ushort[(array7.Length - 1) / num15];
                                                                    int num16 = 0;
                                                                    for (int k = 0; k < item2.faceList.Length; k++)
                                                                    {
                                                                        if (array7[num16] == string.Empty)
                                                                        {
                                                                            k--;
                                                                            num16 -= num15 - 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            try
                                                                            {
                                                                                item2.faceList[k] = Convert.ToUInt16(array7[num16], new CultureInfo("en-US"));
                                                                            }
                                                                            catch
                                                                            {
                                                                                pForm.Logger("mesh has too many faces! Try to split it.", 3);
                                                                                break;
                                                                            }
                                                                        }
                                                                        num16 += num15;
                                                                    }
                                                                    Process process = new Process();
                                                                    process.StartInfo.FileName = "NvTriStripper.exe";
                                                                    process.StartInfo.UseShellExecute = false;
                                                                    process.StartInfo.RedirectStandardInput = true;
                                                                    process.StartInfo.RedirectStandardOutput = true;
                                                                    process.Start();
                                                                    StreamWriter standardInput = process.StandardInput;
                                                                    StreamReader standardOutput = process.StandardOutput;
                                                                    ushort[] faceList = item2.faceList;
                                                                    foreach (ushort value in faceList)
                                                                    {
                                                                        standardInput.WriteLine(value);
                                                                    }
                                                                    standardInput.WriteLine(-1);
                                                                    int num17 = Convert.ToInt32(standardOutput.ReadLine(), new CultureInfo("en-US"));
                                                                    string[] array8 = standardOutput.ReadLine().Split(' ');
                                                                    int num18 = -1;
                                                                    item2.faceList = new ushort[num17];
                                                                    string[] array9 = array8;
                                                                    foreach (string text2 in array9)
                                                                    {
                                                                        if (!(text2 == string.Empty))
                                                                        {
                                                                            item2.faceList[++num18] = Convert.ToUInt16(text2, new CultureInfo("en-US"));
                                                                        }
                                                                    }
                                                                    if (!process.HasExited)
                                                                    {
                                                                        try
                                                                        {
                                                                            process.Kill();
                                                                        }
                                                                        catch (Exception ex)
                                                                        {
                                                                            pForm.Logger(ex.Message, 3);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        else if (geomParent.Split('_')[0] == "CONVEX" && havokr)
                                                        {
                                                            while (xmlReader2.Read())
                                                            {
                                                                if (xmlReader2.Name.Equals("input") && xmlReader2.NodeType != XmlNodeType.EndElement)
                                                                {
                                                                    num15 = (short)(num15 + 1);
                                                                }
                                                                else if (xmlReader2.Name.Equals("p") && xmlReader2.NodeType != XmlNodeType.EndElement)
                                                                {
                                                                    string[] array10 = xmlReader2.ReadString().Split(' ', '\n');
                                                                    item.faceList = new List<short>();
                                                                    int num19 = 0;
                                                                    while (num19 < array10.Length - 1)
                                                                    {
                                                                        if (array10[num19] == string.Empty)
                                                                        {
                                                                            num19++;
                                                                        }
                                                                        else
                                                                        {
                                                                            item.faceList.Add(Convert.ToInt16(array10[num19], new CultureInfo("en-US")));
                                                                            num19 += num15;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            if (num2 == -1)
                                            {
                                                if (geomParent != "HMAP" && geomParent.Split('_')[0] != "CONVEX")
                                                {
                                                    sGEOMDAE item6 = default(sGEOMDAE);
                                                    item6.hashcode = geomParent;
                                                    item6.LOD = result;
                                                    item6.sPRIMList = new List<sPRIMDAE>();
                                                    item6.sPRIMList.Add(item2);
                                                    list3.Add(item6);
                                                }
                                                else if (geomParent.Split('_')[0] == "CONVEX")
                                                {
                                                    list5.Add(item);
                                                    item = new HavokReader.havokConvexMesh
                                                    {
                                                        hash = geomParent
                                                    };
                                                }
                                            }
                                            else
                                            {
                                                list3[num2].sPRIMList.Add(item2);
                                            }
                                        } while (gxml.ReadToFollowing("geometry"));
                                        break;
                                    }
                                case "scene":
                                case "asset":
                                case "library_materials":
                                case "library_visual_scenes":
                                    xmlReader.Skip();
                                    break;
                            }
                        }
                    }
                }
                pForm.Logger(input + " read. Now building files...", 1);
                if (!Directory.Exists(output))
                {
                    Directory.CreateDirectory(output);
                }
                if (obj)
                {
                    if (!skip2DMObj)
                    {
                        p2DMWriter.Write2DM(output + fileNameWithoutExtension + "-O.2DM", list, srcFolder);
                    }
                    Dictionary<string, List<string>> materialCatalog = p3DDWriter.Write3DD(output + fileNameWithoutExtension + "-O.3DD", hiearchy, list3);
                    p3DGWriter.WriteObject(output + fileNameWithoutExtension + "-O.3DG", list3, materialCatalog);
                    pForm.Logger("Successful Objects export!", 1);
                }
                if (hmap)
                {
                    p2DMTerrainWriter.Write2DM(output + fileNameWithoutExtension + ".2DM", sectorname.ToLower(), blend1, blend2);
                    p3DDTerrainWriter.Write3DD(output + fileNameWithoutExtension + ".3DD", sectorname.ToLower(), list4);
                    p3DGTerrainWriter.WriteHMAP(list4, output + fileNameWithoutExtension + ".3DG");
                    pForm.Logger("Successful Hmap export!", 1);
                }
                if (havokr)
                {
                    pSHKWriter.HavokRoad(output + fileNameWithoutExtension + "-R.SHK", list5, sectorname.ToLower());
                    pForm.Logger("Successful SHK Roads export!", 1);
                }
                pForm.Invoke((MethodInvoker)delegate {
                    pForm.button1.Enabled = true;
                });
            }
        }
    }
}
