using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using TestDrive_WorldEditor.Properties;

namespace TDUModdingLibrary.labs.WorldEditor
{
    internal class DAE_Writer
    {
        private Utilitaires pUtilitaires = new Utilitaires();

        private TerrainPieces pTerrainPieces = new TerrainPieces();

        private List<int> pushedIndex;

        private List<_2DM_Common.sMAT> matList;

        private List<_3DD_Common.sINST> TDUscene;

        private List<_3DG_Common.sGEOM> sGEOMList;

        private List<Utilitaires.Vertex> hmapGEO;

        private List<HavokReader.havokConvexMesh> shkRoad;

        private XmlWriterSettings settings = new XmlWriterSettings
        {
            Indent = true,
            IndentChars = "\t",
            NewLineChars = "\r\n",
            NewLineHandling = NewLineHandling.Replace
        };

        private string getPivotMatrix(float[,] pivot)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < 4; i++)
            {
                stringBuilder.Append(Convert.ToString(pivot[0, i], new CultureInfo("en-US")) + ' ' + Convert.ToString(pivot[1, i], new CultureInfo("en-US")) + ' ' + Convert.ToString(pivot[2, i], new CultureInfo("en-US")) + ' ' + Convert.ToString(pivot[3, i], new CultureInfo("en-US")) + ' ');
            }
            return stringBuilder.ToString();
        }

        private string getUVMAP(string input, _3DG_Common.sPRIM PRIM, int index)
        {
            //IL_000c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0011: Expected O, but got Unknown
            StringBuilder stringBuilder = new StringBuilder();
            short num = 1;
            using (BinaryReader binaryReader = new BinaryReader((Stream)File.Open(input, 3, 1, 3)))
            {
                binaryReader.BaseStream.Seek(PRIM.vertexOffset, SeekOrigin.Begin);
                binaryReader.BaseStream.Seek(PRIM.vertexCount * 3 * 4, SeekOrigin.Current);
                if (PRIM.flag2 == 1 && PRIM.flag3 == 1)
                {
                    num = 2;
                }
                binaryReader.BaseStream.Seek(PRIM.vertexCount * 4 * num + PRIM.vertexCount * 4 * 2 * index, SeekOrigin.Current);
                for (int i = 0; i < PRIM.vertexCount; i++)
                {
                    stringBuilder.Append(Convert.ToString(binaryReader.ReadSingle(), new CultureInfo("en-US")) + ' ' + Convert.ToString(0f - binaryReader.ReadSingle(), new CultureInfo("en-US")) + ' ');
                }
            }
            return stringBuilder.ToString();
        }

        private string getVertexArr(string input, int oVertices, int cVertices)
        {
            //IL_000a: Unknown result type (might be due to invalid IL or missing references)
            //IL_000f: Expected O, but got Unknown
            StringBuilder stringBuilder = new StringBuilder();
            using (BinaryReader binaryReader = new BinaryReader((Stream)File.Open(input, 3, 1, 3)))
            {
                binaryReader.BaseStream.Seek(oVertices, SeekOrigin.Begin);
                for (int i = 0; i < cVertices; i++)
                {
                    stringBuilder.Append(Convert.ToString(binaryReader.ReadSingle(), new CultureInfo("en-US")) + ' ' + Convert.ToString(binaryReader.ReadSingle(), new CultureInfo("en-US")) + " " + Convert.ToString(binaryReader.ReadSingle(), new CultureInfo("en-US")) + ' ');
                }
            }
            return stringBuilder.ToString();
        }

        private void getFaceArr(string input, XmlWriter xml, int oFaces, int cFaces, int cUVMap)
        {
            //IL_0010: Unknown result type (might be due to invalid IL or missing references)
            //IL_0015: Expected O, but got Unknown
            short num = -1;
            short num2 = -1;
            StringBuilder stringBuilder = new StringBuilder();
            using (BinaryReader binaryReader = new BinaryReader((Stream)File.Open(input, 3, 1, 3)))
            {
                binaryReader.BaseStream.Seek(oFaces, SeekOrigin.Begin);
                xml.WriteStartElement("p");
                for (int i = 0; i < cFaces - 2; i++)
                {
                    short value = binaryReader.ReadInt16();
                    short num3 = binaryReader.ReadInt16();
                    short num4 = binaryReader.ReadInt16();
                    binaryReader.BaseStream.Seek(oFaces + 2, SeekOrigin.Begin);
                    num = binaryReader.ReadInt16();
                    num2 = binaryReader.ReadInt16();
                    binaryReader.ReadInt16();
                    if (num3 == num4 && num == num2 && num3 == num)
                    {
                        binaryReader.BaseStream.Seek(oFaces += 4, SeekOrigin.Begin);
                        i++;
                    }
                    else
                    {
                        stringBuilder.Append(Convert.ToString(value, new CultureInfo("en-US")) + ' ' + Convert.ToString(value, new CultureInfo("en-US")) + ' ');
                        for (byte b = 0; b < cUVMap; b = (byte)(b + 1))
                        {
                            stringBuilder.Append(Convert.ToString(value, new CultureInfo("en-US")) + ' ');
                        }
                        stringBuilder.Append(Convert.ToString(num3, new CultureInfo("en-US")) + ' ' + Convert.ToString(num3, new CultureInfo("en-US")) + ' ');
                        for (byte b2 = 0; b2 < cUVMap; b2 = (byte)(b2 + 1))
                        {
                            stringBuilder.Append(Convert.ToString(num3, new CultureInfo("en-US")) + ' ');
                        }
                        stringBuilder.Append(Convert.ToString(num4, new CultureInfo("en-US")) + ' ' + Convert.ToString(num4, new CultureInfo("en-US")) + ' ');
                        for (byte b3 = 0; b3 < cUVMap; b3 = (byte)(b3 + 1))
                        {
                            stringBuilder.Append(Convert.ToString(num4, new CultureInfo("en-US")) + ' ');
                        }
                        binaryReader.BaseStream.Seek(oFaces += 2, SeekOrigin.Begin);
                    }
                }
                xml.WriteString(stringBuilder.ToString());
                xml.WriteEndElement();
            }
        }

        private string getNormals(string input, int oNormal, int cNormal)
        {
            //IL_000a: Unknown result type (might be due to invalid IL or missing references)
            //IL_000f: Expected O, but got Unknown
            StringBuilder stringBuilder = new StringBuilder();
            using (BinaryReader binaryReader = new BinaryReader((Stream)File.Open(input, 3, 1, 3)))
            {
                binaryReader.BaseStream.Seek(oNormal, SeekOrigin.Begin);
                for (int i = 0; i < cNormal; i++)
                {
                    uint num = binaryReader.ReadUInt32();
                    long num2 = num & 0x3FF;
                    long num3 = (num & 0xFFC00) >> 10;
                    long num4 = (num & 0x3FF00000) >> 20;
                    stringBuilder.Append(Convert.ToString((float)((num2 > 511) ? (num2 - 1023) : num2) / 511f, new CultureInfo("en-US")) + ' ' + Convert.ToString((float)((num3 > 511) ? (num3 - 1023) : num3) / 511f, new CultureInfo("en-US")) + ' ' + Convert.ToString((float)((num4 > 511) ? (num4 - 1023) : num4) / 511f, new CultureInfo("en-US")) + ' ');
                }
            }
            return stringBuilder.ToString();
        }

        private string matchHash(string Hash, List<_3DG_Common.sGEOM> geometry)
        {
            for (int i = 0; i < geometry.Count; i++)
            {
                if (Hash.Equals(geometry[i].hashcode))
                {
                    return pUtilitaires.stringify(geometry[i].hashcode);
                }
            }
            return null;
        }

        private void writeNode(_3DD_Common.sINST Instance, int index, XmlWriter xml)
        {
            short index2 = 0;
            List<short> list = new List<short>();
            xml.WriteStartElement("node");
            xml.WriteAttributeString("id", pUtilitaires.stringify(Instance.hash));
            xml.WriteAttributeString("name", pUtilitaires.stringify(Instance.hash));
            xml.WriteAttributeString("type", "NODE");
            xml.WriteStartElement("matrix");
            xml.WriteAttributeString("sid", "transform");
            xml.WriteString(getPivotMatrix(Instance.pivot));
            xml.WriteEndElement();
            if (Instance.LODList != null)
            {
                for (int i = 0; i < Instance.LODList.Count; i++)
                {
                    for (short num = 0; num < sGEOMList.Count; num = (short)(num + 1))
                    {
                        if (sGEOMList[num].hashcode == Instance.LODList[i].objectHash && !list.Contains(num))
                        {
                            index2 = num;
                            list.Add(num);
                            break;
                        }
                    }
                    string str = pUtilitaires.stringify(sGEOMList[index2].hashcode);
                    xml.WriteStartElement("node");
                    xml.WriteAttributeString("id", str + ((Instance.LODList[i].LOD != 0f) ? ('_' + Convert.ToString(Instance.LODList[i].LOD)) : string.Empty));
                    xml.WriteAttributeString("name", str + ((Instance.LODList[i].LOD != 0f) ? ('_' + Convert.ToString(Instance.LODList[i].LOD)) : string.Empty));
                    xml.WriteAttributeString("type", "NODE");
                    for (int j = 0; j < Instance.LODList[i].unknownInt; j++)
                    {
                        string text = pUtilitaires.stringify(sGEOMList[index2].hashcode) + '_' + j;
                        if (Instance.LODList[i].LOD != 0f)
                        {
                            text = text + '_' + Convert.ToString(Instance.LODList[i].LOD);
                        }
                        xml.WriteStartElement("instance_geometry");
                        xml.WriteAttributeString("url", '#' + text);
                        if (sGEOMList[index2].sPRIMList[j].matIndice != -1)
                        {
                            xml.WriteStartElement("bind_material");
                            xml.WriteStartElement("technique_common");
                            xml.WriteStartElement("instance_material");
                            xml.WriteAttributeString("symbol", pUtilitaires.stringify(Instance.textures[sGEOMList[index2].sPRIMList[j].matIndice]));
                            xml.WriteAttributeString("target", '#' + pUtilitaires.stringify(Instance.textures[sGEOMList[index2].sPRIMList[j].matIndice]));
                            xml.WriteEndElement();
                            xml.WriteEndElement();
                            xml.WriteEndElement();
                        }
                        xml.WriteEndElement();
                    }
                    xml.WriteEndElement();
                }
            }
            for (int k = index; k < TDUscene.Count; k++)
            {
                if (Instance.hash == TDUscene[k].parentHash)
                {
                    pushedIndex.Add(k);
                    writeNode(TDUscene[k], k, xml);
                }
            }
            xml.WriteEndElement();
        }

        public DAE_Writer(string sector, string input, string output, bool hmap = false, bool obj = false, bool havokr = false, bool havokw = false)
        {
            //IL_0104: Unknown result type (might be due to invalid IL or missing references)
            //IL_0109: Expected O, but got Unknown
            //IL_2432: Unknown result type (might be due to invalid IL or missing references)
            //IL_2437: Expected O, but got Unknown
            //IL_275e: Unknown result type (might be due to invalid IL or missing references)
            //IL_2763: Expected O, but got Unknown
            Form1 pForm = Form.ActiveForm as Form1;
            _3DG_Common _3DG_Common = new _3DG_Common();
            _2DM_Reader _2DM_Reader = new _2DM_Reader();
            _3DD_Reader _3DD_Reader = new _3DD_Reader();
            _3DG_Terrain_Reader _3DG_Terrain_Reader = new _3DG_Terrain_Reader();
            HavokReader havokReader = new HavokReader();
            if (obj)
            {
                if (!File.Exists(input + sector + "-O.2DM"))
                {
                    pForm.Logger("object material file " + input + sector + "-O.2DM not found!", 3);
                    return;
                }
                if (!File.Exists(input + sector + "-O.3DD"))
                {
                    pForm.Logger("object scene file " + input + sector + "-O.3DD not found!", 3);
                    return;
                }
                FileInfo val = new FileInfo(input + sector + "-O.3DD");
                if (val.get_Length() > 48)
                {
                    matList = _2DM_Reader.Read2DM(input + sector + "-O.2DM");
                    TDUscene = _3DD_Reader.Read3DD(input + sector + "-O.3DD");
                    sGEOMList = _3DG_Common.ReadHeader(32, input + sector + "-O.3DG");
                }
                else
                {
                    pForm.Logger("trying to import a corrupted/empty object scene; skipping element...", 2);
                    obj = false;
                }
            }
            if (hmap)
            {
                if (!File.Exists(input + sector + ".2DM"))
                {
                    pForm.Logger("hmap material file " + input + sector + ".2DM not found!", 3);
                    return;
                }
                if (!File.Exists(input + sector + ".3DD"))
                {
                    pForm.Logger("hmap scene file " + input + sector + ".3DD not found!", 3);
                    return;
                }
                if (!File.Exists("hmap_faces.txt") || !File.Exists("hmap_uvmap.txt"))
                {
                    pForm.Logger("hardcoded hmap ressources not found! Make sure hmap_faces.txt and hmap_uvmap.txt are present in editor folder!", 3);
                    return;
                }
                hmapGEO = _3DG_Terrain_Reader.ReadHMAP(input + sector + ".3DG");
            }
            if (havokr)
            {
                shkRoad = havokReader.ReadHavokRoad(input + sector + "-R.shk");
            }
            using (XmlWriter xmlWriter = XmlWriter.Create(output, settings))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("COLLADA", "http://www.collada.org/2005/11/COLLADASchema");
                xmlWriter.WriteAttributeString("version", null, "1.4.1");
                xmlWriter.WriteStartElement("asset");
                xmlWriter.WriteStartElement("contributor");
                xmlWriter.WriteElementString("authoring_tool", "Test Drive Unlimited World Editor");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteElementString("created", DateTime.Now.ToString("yyyy-m-dTH:mm:ss"));
                xmlWriter.WriteStartElement("unit");
                xmlWriter.WriteAttributeString("name", "meter");
                xmlWriter.WriteAttributeString("meter", "1");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteElementString("up_axis", "Y_UP");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("library_images");
                if (obj)
                {
                    foreach (_2DM_Common.sMAT mat in matList)
                    {
                        _2DM_Common.sMAT current = mat;
                        foreach (_2DM_Common.sLAYER layer in current.layerList)
                        {
                            _2DM_Common.sLAYER current2 = layer;
                            if (current2.texture != null && !(current2.texture == string.Empty) && (File.Exists(input + current2.texture + ".DDS") || File.Exists(Settings.Default.commonworld + current2.texture + ".DDS")))
                            {
                                xmlWriter.WriteStartElement("image");
                                xmlWriter.WriteAttributeString("id", pUtilitaires.stringify(current.hash) + '_' + current2.parameter);
                                xmlWriter.WriteAttributeString("name", pUtilitaires.stringify(current.hash) + '_' + current2.parameter);
                                if (File.Exists(input + current2.texture + ".DDS"))
                                {
                                    xmlWriter.WriteElementString("init_from", "file://" + input + current2.texture + ".DDS");
                                }
                                else if (File.Exists(Settings.Default.commonworld + current2.texture + ".DDS"))
                                {
                                    xmlWriter.WriteElementString("init_from", "file://" + Settings.Default.commonworld + current2.texture + ".DDS");
                                }
                                else
                                {
                                    pForm.Logger("WARNING : texture " + current2.texture + " not found ", 2);
                                }
                                xmlWriter.WriteEndElement();
                            }
                        }
                    }
                }
                if (hmap)
                {
                    if (File.Exists(input + sector + "-c.DDS"))
                    {
                        xmlWriter.WriteStartElement("image");
                        xmlWriter.WriteAttributeString("id", sector + "-c");
                        xmlWriter.WriteAttributeString("name", sector + "-c");
                        xmlWriter.WriteElementString("init_from", "file://" + input + sector + "-c.DDS");
                        xmlWriter.WriteEndElement();
                    }
                    if (File.Exists(input + sector + "-a.DDS"))
                    {
                        xmlWriter.WriteStartElement("image");
                        xmlWriter.WriteAttributeString("id", sector + "-a");
                        xmlWriter.WriteAttributeString("name", sector + "-a");
                        xmlWriter.WriteElementString("init_from", "file://" + input + sector + "-a.DDS");
                        xmlWriter.WriteEndElement();
                    }
                    if (File.Exists(input + sector + "-s.DDS"))
                    {
                        xmlWriter.WriteStartElement("image");
                        xmlWriter.WriteAttributeString("id", sector + "-s");
                        xmlWriter.WriteAttributeString("name", sector + "-s");
                        xmlWriter.WriteElementString("init_from", "file://" + input + sector + "-s.DDS");
                        xmlWriter.WriteEndElement();
                    }
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("library_effects");
                if (obj)
                {
                    foreach (_2DM_Common.sMAT mat2 in matList)
                    {
                        _2DM_Common.sMAT current3 = mat2;
                        xmlWriter.WriteStartElement("effect");
                        xmlWriter.WriteAttributeString("id", pUtilitaires.stringify(current3.hash) + "_E");
                        xmlWriter.WriteAttributeString("name", pUtilitaires.stringify(current3.hash));
                        xmlWriter.WriteStartElement("profile_COMMON");
                        xmlWriter.WriteStartElement("technique");
                        xmlWriter.WriteAttributeString("sid", "standard");
                        xmlWriter.WriteStartElement("phong");
                        xmlWriter.WriteStartElement("ambient");
                        xmlWriter.WriteStartElement("color");
                        xmlWriter.WriteAttributeString("sid", "ambient");
                        xmlWriter.WriteString(Convert.ToString(current3.ambient[0] * 255f, new CultureInfo("en-US")) + ' ' + Convert.ToString(current3.ambient[1] * 255f, new CultureInfo("en-US")) + ' ' + Convert.ToString(current3.ambient[2] * 255f, new CultureInfo("en-US")));
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("diffuse");
                        xmlWriter.WriteStartElement("color");
                        xmlWriter.WriteAttributeString("sid", "diffuse");
                        xmlWriter.WriteString(Convert.ToString(current3.diffuse[0] * 255f, new CultureInfo("en-US")) + ' ' + Convert.ToString(current3.diffuse[1] * 255f, new CultureInfo("en-US")) + ' ' + Convert.ToString(current3.diffuse[2] * 255f, new CultureInfo("en-US")));
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("specular");
                        xmlWriter.WriteStartElement("color");
                        xmlWriter.WriteAttributeString("sid", "specular");
                        xmlWriter.WriteString(Convert.ToString(current3.specular[0] * 255f, new CultureInfo("en-US")) + ' ' + Convert.ToString(current3.specular[1] * 255f, new CultureInfo("en-US")) + ' ' + Convert.ToString(current3.specular[2] * 255f, new CultureInfo("en-US")));
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndElement();
                        short num = -1;
                        foreach (_2DM_Common.sLAYER layer2 in current3.layerList)
                        {
                            _2DM_Common.sLAYER current4 = layer2;
                            switch (current4.parameter)
                            {
                                case "COLOR":
                                    xmlWriter.WriteStartElement("diffuse");
                                    goto IL_0b0e;
                                case "DETAIL":
                                    xmlWriter.WriteStartElement("ambient");
                                    goto IL_0b0e;
                                case "SHADOW":
                                    xmlWriter.WriteStartElement("specular");
                                    goto IL_0b0e;
                                case "ENVMAP":
                                case "REFLECTION":
                                    xmlWriter.WriteStartElement("reflective");
                                    goto IL_0b0e;
                                case "MARKS":
                                case "GLOSS":
                                case "DETAIL2":
                                case "NORMALMAP0":
                                    xmlWriter.WriteStartElement("emission");
                                    goto IL_0b0e;
                                case "NORMALMAP1":
                                case "MARKS2":
                                    {
                                        xmlWriter.WriteStartElement("transparent");
                                        xmlWriter.WriteAttributeString("opaque", "RGB_ZERO");
                                        goto IL_0b0e;
                                    }
                                IL_0b0e:
                                    xmlWriter.WriteStartElement("texture");
                                    xmlWriter.WriteAttributeString("texture", pUtilitaires.stringify(current3.hash) + '_' + current4.parameter);
                                    xmlWriter.WriteAttributeString("texcoord", "CHANNEL" + (num = (short)(num + 1)));
                                    xmlWriter.WriteStartElement("extra");
                                    xmlWriter.WriteStartElement("technique");
                                    xmlWriter.WriteAttributeString("profile", "MAYA");
                                    xmlWriter.WriteStartElement("wrapU");
                                    xmlWriter.WriteAttributeString("sid", "wrapU0");
                                    xmlWriter.WriteString("TRUE");
                                    xmlWriter.WriteEndElement();
                                    xmlWriter.WriteStartElement("wrapV");
                                    xmlWriter.WriteAttributeString("sid", "wrapV0");
                                    xmlWriter.WriteString("TRUE");
                                    xmlWriter.WriteEndElement();
                                    xmlWriter.WriteStartElement("blend_mode");
                                    xmlWriter.WriteString("ADD");
                                    xmlWriter.WriteEndElement();
                                    xmlWriter.WriteEndElement();
                                    xmlWriter.WriteEndElement();
                                    xmlWriter.WriteEndElement();
                                    xmlWriter.WriteEndElement();
                                    break;
                            }
                        }
                        xmlWriter.WriteStartElement("transparent");
                        xmlWriter.WriteAttributeString("opaque", "RGB_ZERO");
                        xmlWriter.WriteStartElement("color");
                        xmlWriter.WriteAttributeString("sid", "transparent");
                        xmlWriter.WriteString("1.0 1.0 1.0 1.0");
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("transparency");
                        xmlWriter.WriteStartElement("float");
                        xmlWriter.WriteAttributeString("sid", "transparency");
                        xmlWriter.WriteString("0.0");
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("shininess");
                        xmlWriter.WriteStartElement("float");
                        xmlWriter.WriteAttributeString("sid", "shininess");
                        xmlWriter.WriteString("50.0");
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("reflectivity");
                        xmlWriter.WriteStartElement("float");
                        xmlWriter.WriteString("1");
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndElement();
                    }
                }
                if (hmap)
                {
                    xmlWriter.WriteStartElement("effect");
                    xmlWriter.WriteAttributeString("id", "TERRAIN_E");
                    xmlWriter.WriteAttributeString("name", "TERRAIN");
                    xmlWriter.WriteStartElement("profile_COMMON");
                    xmlWriter.WriteStartElement("technique");
                    xmlWriter.WriteAttributeString("sid", "standard");
                    xmlWriter.WriteStartElement("phong");
                    xmlWriter.WriteStartElement("ambient");
                    xmlWriter.WriteStartElement("color");
                    xmlWriter.WriteAttributeString("sid", "ambient");
                    xmlWriter.WriteString("255 255 255");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("diffuse");
                    xmlWriter.WriteStartElement("color");
                    xmlWriter.WriteAttributeString("sid", "diffuse");
                    xmlWriter.WriteString("255 255 255");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("specular");
                    xmlWriter.WriteStartElement("color");
                    xmlWriter.WriteAttributeString("sid", "specular");
                    xmlWriter.WriteString("255 255 255");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                    if (File.Exists(input + sector + "-c.DDS"))
                    {
                        xmlWriter.WriteStartElement("diffuse");
                        xmlWriter.WriteStartElement("texture");
                        xmlWriter.WriteAttributeString("texture", sector + "-c");
                        xmlWriter.WriteAttributeString("texcoord", "CHANNEL0");
                        xmlWriter.WriteStartElement("extra");
                        xmlWriter.WriteStartElement("technique");
                        xmlWriter.WriteAttributeString("profile", "MAYA");
                        xmlWriter.WriteStartElement("wrapU");
                        xmlWriter.WriteAttributeString("sid", "wrapU0");
                        xmlWriter.WriteString("TRUE");
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("wrapV");
                        xmlWriter.WriteAttributeString("sid", "wrapV0");
                        xmlWriter.WriteString("TRUE");
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("blend_mode");
                        xmlWriter.WriteString("ADD");
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndElement();
                    }
                    if (File.Exists(input + sector + "-a.DDS"))
                    {
                        xmlWriter.WriteStartElement("ambient");
                        xmlWriter.WriteStartElement("texture");
                        xmlWriter.WriteAttributeString("texture", sector + "-a");
                        xmlWriter.WriteAttributeString("texcoord", "CHANNEL0");
                        xmlWriter.WriteStartElement("extra");
                        xmlWriter.WriteStartElement("technique");
                        xmlWriter.WriteAttributeString("profile", "MAYA");
                        xmlWriter.WriteStartElement("wrapU");
                        xmlWriter.WriteAttributeString("sid", "wrapU0");
                        xmlWriter.WriteString("TRUE");
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("wrapV");
                        xmlWriter.WriteAttributeString("sid", "wrapV0");
                        xmlWriter.WriteString("TRUE");
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("blend_mode");
                        xmlWriter.WriteString("ADD");
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndElement();
                    }
                    if (File.Exists(input + sector + "-s.DDS"))
                    {
                        xmlWriter.WriteStartElement("specular");
                        xmlWriter.WriteStartElement("texture");
                        xmlWriter.WriteAttributeString("texture", sector + "-s");
                        xmlWriter.WriteAttributeString("texcoord", "CHANNEL0");
                        xmlWriter.WriteStartElement("extra");
                        xmlWriter.WriteStartElement("technique");
                        xmlWriter.WriteAttributeString("profile", "MAYA");
                        xmlWriter.WriteStartElement("wrapU");
                        xmlWriter.WriteAttributeString("sid", "wrapU0");
                        xmlWriter.WriteString("TRUE");
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("wrapV");
                        xmlWriter.WriteAttributeString("sid", "wrapV0");
                        xmlWriter.WriteString("TRUE");
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("blend_mode");
                        xmlWriter.WriteString("ADD");
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndElement();
                    }
                    xmlWriter.WriteStartElement("transparent");
                    xmlWriter.WriteAttributeString("opaque", "RGB_ZERO");
                    xmlWriter.WriteStartElement("color");
                    xmlWriter.WriteAttributeString("sid", "transparent");
                    xmlWriter.WriteString("1.0 1.0 1.0 1.0");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("transparency");
                    xmlWriter.WriteStartElement("float");
                    xmlWriter.WriteAttributeString("sid", "transparency");
                    xmlWriter.WriteString("0.0");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("shininess");
                    xmlWriter.WriteStartElement("float");
                    xmlWriter.WriteAttributeString("sid", "shininess");
                    xmlWriter.WriteString("50.0");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("reflectivity");
                    xmlWriter.WriteStartElement("float");
                    xmlWriter.WriteString("1");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("library_materials");
                if (obj)
                {
                    foreach (_2DM_Common.sMAT mat3 in matList)
                    {
                        _2DM_Common.sMAT current5 = mat3;
                        xmlWriter.WriteStartElement("material");
                        xmlWriter.WriteAttributeString("id", pUtilitaires.stringify(current5.hash));
                        xmlWriter.WriteAttributeString("name", pUtilitaires.stringify(current5.hash));
                        xmlWriter.WriteStartElement("instance_effect");
                        xmlWriter.WriteAttributeString("url", '#' + pUtilitaires.stringify(current5.hash) + "_E");
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndElement();
                    }
                }
                if (hmap)
                {
                    xmlWriter.WriteStartElement("material");
                    xmlWriter.WriteAttributeString("id", "TERRAIN");
                    xmlWriter.WriteAttributeString("name", "TERRAIN");
                    xmlWriter.WriteStartElement("instance_effect");
                    xmlWriter.WriteAttributeString("url", "#TERRAIN_E");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteEndElement();
                short index = 0;
                List<short> list = new List<short>();
                xmlWriter.WriteStartElement("library_geometries");
                if (obj)
                {
                    for (int i = 0; i < TDUscene.Count; i++)
                    {
                        if (TDUscene[i].LODList != null && TDUscene[i].LODList.Count != 0)
                        {
                            for (int j = 0; j < TDUscene[i].LODList.Count; j++)
                            {
                                for (short num2 = 0; num2 < sGEOMList.Count; num2 = (short)(num2 + 1))
                                {
                                    if (sGEOMList[num2].hashcode == TDUscene[i].LODList[j].objectHash && !list.Contains(num2))
                                    {
                                        index = num2;
                                        list.Add(num2);
                                        break;
                                    }
                                }
                                for (int k = 0; k < sGEOMList[index].sPRIMList.Count; k++)
                                {
                                    string text = pUtilitaires.stringify(sGEOMList[index].hashcode) + '_' + k;
                                    if (TDUscene[i].LODList[j].LOD != 0f)
                                    {
                                        text = text + '_' + Convert.ToString(TDUscene[i].LODList[j].LOD);
                                    }
                                    xmlWriter.WriteStartElement("geometry");
                                    xmlWriter.WriteAttributeString("id", text);
                                    xmlWriter.WriteAttributeString("name", text);
                                    xmlWriter.WriteStartElement("mesh");
                                    xmlWriter.WriteStartElement("source");
                                    xmlWriter.WriteAttributeString("id", text + "_P");
                                    xmlWriter.WriteStartElement("float_array");
                                    xmlWriter.WriteAttributeString("id", text + "_PA");
                                    xmlWriter.WriteAttributeString("count", Convert.ToString(sGEOMList[index].sPRIMList[k].vertexCount * 3));
                                    xmlWriter.WriteString(getVertexArr(input + sector + "-O.3DG", sGEOMList[index].sPRIMList[k].vertexOffset, sGEOMList[index].sPRIMList[k].vertexCount));
                                    xmlWriter.WriteEndElement();
                                    xmlWriter.WriteStartElement("technique_common");
                                    xmlWriter.WriteStartElement("accessor");
                                    xmlWriter.WriteAttributeString("source", '#' + text + "_PA");
                                    xmlWriter.WriteAttributeString("count", Convert.ToString(sGEOMList[index].sPRIMList[k].vertexCount));
                                    xmlWriter.WriteAttributeString("stride", "3");
                                    xmlWriter.WriteStartElement("param");
                                    xmlWriter.WriteAttributeString("name", "X");
                                    xmlWriter.WriteAttributeString("type", "float");
                                    xmlWriter.WriteEndElement();
                                    xmlWriter.WriteStartElement("param");
                                    xmlWriter.WriteAttributeString("name", "Y");
                                    xmlWriter.WriteAttributeString("type", "float");
                                    xmlWriter.WriteEndElement();
                                    xmlWriter.WriteStartElement("param");
                                    xmlWriter.WriteAttributeString("name", "Z");
                                    xmlWriter.WriteAttributeString("type", "float");
                                    xmlWriter.WriteEndElement();
                                    xmlWriter.WriteEndElement();
                                    xmlWriter.WriteEndElement();
                                    xmlWriter.WriteEndElement();
                                    xmlWriter.WriteStartElement("source");
                                    xmlWriter.WriteAttributeString("id", text + "_NORMAL");
                                    xmlWriter.WriteStartElement("float_array");
                                    xmlWriter.WriteAttributeString("id", text + "_NORMALA");
                                    xmlWriter.WriteAttributeString("count", Convert.ToString(sGEOMList[index].sPRIMList[k].vertexCount * 2));
                                    xmlWriter.WriteString(getNormals(input + sector + "-O.3DG", sGEOMList[index].sPRIMList[k].vertexOffset + sGEOMList[index].sPRIMList[k].vertexCount * 3 * 4, sGEOMList[index].sPRIMList[k].vertexCount));
                                    xmlWriter.WriteEndElement();
                                    xmlWriter.WriteStartElement("technique_common");
                                    xmlWriter.WriteStartElement("accessor");
                                    xmlWriter.WriteAttributeString("source", '#' + text + "__NORMALA");
                                    xmlWriter.WriteAttributeString("count", Convert.ToString(sGEOMList[index].sPRIMList[k].vertexCount));
                                    xmlWriter.WriteAttributeString("stride", "3");
                                    xmlWriter.WriteStartElement("param");
                                    xmlWriter.WriteAttributeString("name", "X");
                                    xmlWriter.WriteAttributeString("type", "float");
                                    xmlWriter.WriteEndElement();
                                    xmlWriter.WriteStartElement("param");
                                    xmlWriter.WriteAttributeString("name", "Y");
                                    xmlWriter.WriteAttributeString("type", "float");
                                    xmlWriter.WriteEndElement();
                                    xmlWriter.WriteStartElement("param");
                                    xmlWriter.WriteAttributeString("name", "Z");
                                    xmlWriter.WriteAttributeString("type", "float");
                                    xmlWriter.WriteEndElement();
                                    xmlWriter.WriteEndElement();
                                    xmlWriter.WriteEndElement();
                                    xmlWriter.WriteEndElement();
                                    for (int l = 0; l < sGEOMList[index].sPRIMList[k].UVmapCount; l++)
                                    {
                                        xmlWriter.WriteStartElement("source");
                                        xmlWriter.WriteAttributeString("id", text + "_UV_" + l);
                                        xmlWriter.WriteStartElement("float_array");
                                        xmlWriter.WriteAttributeString("id", text + "_UVA_" + l);
                                        xmlWriter.WriteAttributeString("count", Convert.ToString(sGEOMList[index].sPRIMList[k].vertexCount * 2));
                                        xmlWriter.WriteString(getUVMAP(input + sector + "-O.3DG", sGEOMList[index].sPRIMList[k], l));
                                        xmlWriter.WriteEndElement();
                                        xmlWriter.WriteStartElement("technique_common");
                                        xmlWriter.WriteStartElement("accessor");
                                        xmlWriter.WriteAttributeString("source", '#' + text + "_UVA_" + l);
                                        xmlWriter.WriteAttributeString("count", Convert.ToString(sGEOMList[index].sPRIMList[k].vertexCount));
                                        xmlWriter.WriteAttributeString("stride", "2");
                                        xmlWriter.WriteStartElement("param");
                                        xmlWriter.WriteAttributeString("name", "S");
                                        xmlWriter.WriteAttributeString("type", "float");
                                        xmlWriter.WriteEndElement();
                                        xmlWriter.WriteStartElement("param");
                                        xmlWriter.WriteAttributeString("name", "T");
                                        xmlWriter.WriteAttributeString("type", "float");
                                        xmlWriter.WriteEndElement();
                                        xmlWriter.WriteEndElement();
                                        xmlWriter.WriteEndElement();
                                        xmlWriter.WriteEndElement();
                                    }
                                    xmlWriter.WriteStartElement("vertices");
                                    xmlWriter.WriteAttributeString("id", text + "_V");
                                    xmlWriter.WriteStartElement("input");
                                    xmlWriter.WriteAttributeString("semantic", "POSITION");
                                    xmlWriter.WriteAttributeString("source", '#' + text + "_P");
                                    xmlWriter.WriteEndElement();
                                    xmlWriter.WriteEndElement();
                                    xmlWriter.WriteStartElement("triangles");
                                    xmlWriter.WriteAttributeString("count", Convert.ToString(sGEOMList[index].sPRIMList[k].vertexCount / 3));
                                    if (TDUscene[i].textures != null && sGEOMList[index].sPRIMList[k].matIndice != -1)
                                    {
                                        xmlWriter.WriteAttributeString("material", pUtilitaires.stringify(TDUscene[i].textures[sGEOMList[index].sPRIMList[k].matIndice]));
                                    }
                                    xmlWriter.WriteStartElement("input");
                                    xmlWriter.WriteAttributeString("semantic", "VERTEX");
                                    xmlWriter.WriteAttributeString("source", '#' + text + "_V");
                                    xmlWriter.WriteAttributeString("offset", "0");
                                    xmlWriter.WriteEndElement();
                                    xmlWriter.WriteStartElement("input");
                                    xmlWriter.WriteAttributeString("semantic", "NORMAL");
                                    xmlWriter.WriteAttributeString("source", '#' + text + "_NORMAL");
                                    xmlWriter.WriteAttributeString("offset", "1");
                                    xmlWriter.WriteEndElement();
                                    for (short num3 = 0; num3 < sGEOMList[index].sPRIMList[k].UVmapCount; num3 = (short)(num3 + 1))
                                    {
                                        xmlWriter.WriteStartElement("input");
                                        xmlWriter.WriteAttributeString("semantic", "TEXCOORD");
                                        xmlWriter.WriteAttributeString("source", '#' + text + "_UV_" + num3);
                                        xmlWriter.WriteAttributeString("offset", Convert.ToString(num3 + 2));
                                        xmlWriter.WriteAttributeString("set", Convert.ToString(num3));
                                        xmlWriter.WriteEndElement();
                                    }
                                    getFaceArr(input + sector + "-O.3DG", xmlWriter, sGEOMList[index].sPRIMList[k].faceOffset, sGEOMList[index].sPRIMList[k].faceCount, sGEOMList[index].sPRIMList[k].UVmapCount);
                                    xmlWriter.WriteEndElement();
                                    xmlWriter.WriteEndElement();
                                    xmlWriter.WriteEndElement();
                                }
                            }
                        }
                    }
                }
                List<Utilitaires.Vertex>.Enumerator enumerator6;
                if (hmap)
                {
                    xmlWriter.WriteStartElement("geometry");
                    xmlWriter.WriteAttributeString("id", "HMAP");
                    xmlWriter.WriteAttributeString("name", "HMAP");
                    xmlWriter.WriteStartElement("mesh");
                    xmlWriter.WriteStartElement("source");
                    xmlWriter.WriteAttributeString("id", "HMAP_P");
                    xmlWriter.WriteStartElement("float_array");
                    xmlWriter.WriteAttributeString("id", "HMAP_PA");
                    xmlWriter.WriteAttributeString("count", Convert.ToString(hmapGEO.Count * 3, new CultureInfo("en-US")));
                    StringBuilder stringBuilder = new StringBuilder();
                    enumerator6 = hmapGEO.GetEnumerator();
                    try
                    {
                        while (enumerator6.MoveNext())
                        {
                            Utilitaires.Vertex current6 = enumerator6.Current;
                            stringBuilder.Append(Convert.ToString(current6.x, new CultureInfo("en-US")) + ' ' + Convert.ToString(current6.y, new CultureInfo("en-US")) + ' ' + Convert.ToString(current6.z, new CultureInfo("en-US")) + ' ');
                        }
                    }
                    finally
                    {
                        ((IDisposable)enumerator6).Dispose();
                    }
                    xmlWriter.WriteString(stringBuilder.ToString());
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("technique_common");
                    xmlWriter.WriteStartElement("accessor");
                    xmlWriter.WriteAttributeString("source", "#HMAP_P");
                    xmlWriter.WriteAttributeString("count", Convert.ToString(hmapGEO.Count, new CultureInfo("en-US")));
                    xmlWriter.WriteAttributeString("stride", "3");
                    xmlWriter.WriteStartElement("param");
                    xmlWriter.WriteAttributeString("name", "X");
                    xmlWriter.WriteAttributeString("type", "float");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("param");
                    xmlWriter.WriteAttributeString("name", "Y");
                    xmlWriter.WriteAttributeString("type", "float");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("param");
                    xmlWriter.WriteAttributeString("name", "Z");
                    xmlWriter.WriteAttributeString("type", "float");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("source");
                    xmlWriter.WriteAttributeString("id", "HMAP_NORMAL");
                    xmlWriter.WriteStartElement("float_array");
                    xmlWriter.WriteAttributeString("id", "HMAP_NORMALA");
                    xmlWriter.WriteAttributeString("count", "393216");
                    StringBuilder stringBuilder2 = new StringBuilder();
                    for (int m = 0; m < 131072; m++)
                    {
                        stringBuilder2.Append("0 1 0 ");
                    }
                    xmlWriter.WriteString(stringBuilder2.ToString());
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("technique_common");
                    xmlWriter.WriteStartElement("accessor");
                    xmlWriter.WriteAttributeString("source", "#HMAP_NORMALA");
                    xmlWriter.WriteAttributeString("count", "131072");
                    xmlWriter.WriteAttributeString("stride", "3");
                    xmlWriter.WriteStartElement("param");
                    xmlWriter.WriteAttributeString("name", "X");
                    xmlWriter.WriteAttributeString("type", "float");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("param");
                    xmlWriter.WriteAttributeString("name", "Y");
                    xmlWriter.WriteAttributeString("type", "float");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("param");
                    xmlWriter.WriteAttributeString("name", "Z");
                    xmlWriter.WriteAttributeString("type", "float");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("source");
                    xmlWriter.WriteAttributeString("id", "HMAP_UV");
                    xmlWriter.WriteStartElement("float_array");
                    xmlWriter.WriteAttributeString("id", "HMAP_UVA");
                    xmlWriter.WriteAttributeString("count", "786432");
                    StringBuilder stringBuilder3 = new StringBuilder();
                    int num4 = 0;
                    float num5 = 0f;
                    using (StreamReader streamReader = new StreamReader((Stream)File.Open("hmap_uvmap.txt", 3)))
                    {
                        string[] array = streamReader.ReadLine().Split(' ');
                        foreach (string text2 in array)
                        {
                            if (!(text2 == string.Empty))
                            {
                                num5 = Convert.ToSingle(text2, new CultureInfo("en-US"));
                                if (++num4 % 2 == 0)
                                {
                                    stringBuilder3.Append(Convert.ToString(0f - num5, new CultureInfo("en-US")) + ' ');
                                }
                                else
                                {
                                    stringBuilder3.Append(Convert.ToString(text2, (IFormatProvider)new CultureInfo("en-US")) + ' ');
                                }
                            }
                        }
                    }
                    xmlWriter.WriteString(stringBuilder3.ToString());
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("technique_common");
                    xmlWriter.WriteStartElement("accessor");
                    xmlWriter.WriteAttributeString("source", "#HMAP_UVA");
                    xmlWriter.WriteAttributeString("count", "393216");
                    xmlWriter.WriteAttributeString("stride", "2");
                    xmlWriter.WriteStartElement("param");
                    xmlWriter.WriteAttributeString("name", "S");
                    xmlWriter.WriteAttributeString("type", "float");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("param");
                    xmlWriter.WriteAttributeString("name", "T");
                    xmlWriter.WriteAttributeString("type", "float");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("vertices");
                    xmlWriter.WriteAttributeString("id", "HMAP_V");
                    xmlWriter.WriteStartElement("input");
                    xmlWriter.WriteAttributeString("semantic", "POSITION");
                    xmlWriter.WriteAttributeString("source", "#HMAP_P");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("triangles");
                    xmlWriter.WriteAttributeString("count", "131072");
                    xmlWriter.WriteAttributeString("material", "TERRAIN");
                    xmlWriter.WriteStartElement("input");
                    xmlWriter.WriteAttributeString("semantic", "VERTEX");
                    xmlWriter.WriteAttributeString("source", "#HMAP_V");
                    xmlWriter.WriteAttributeString("offset", "0");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("input");
                    xmlWriter.WriteAttributeString("semantic", "NORMAL");
                    xmlWriter.WriteAttributeString("source", "#HMAP_NORMAL");
                    xmlWriter.WriteAttributeString("offset", "1");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("input");
                    xmlWriter.WriteAttributeString("semantic", "TEXCOORD");
                    xmlWriter.WriteAttributeString("source", "#HMAP_UV");
                    xmlWriter.WriteAttributeString("offset", "2");
                    xmlWriter.WriteAttributeString("set", "0");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("p");
                    using (StreamReader streamReader2 = new StreamReader((Stream)File.Open("hmap_faces.txt", 3)))
                    {
                        xmlWriter.WriteString(streamReader2.ReadLine());
                    }
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                }
                if (havokr)
                {
                    for (int num6 = 0; num6 < shkRoad.Count; num6++)
                    {
                        string hash = shkRoad[num6].hash;
                        xmlWriter.WriteStartElement("geometry");
                        xmlWriter.WriteAttributeString("id", hash);
                        xmlWriter.WriteAttributeString("name", hash);
                        xmlWriter.WriteStartElement("mesh");
                        xmlWriter.WriteStartElement("source");
                        xmlWriter.WriteAttributeString("id", hash + "_P");
                        xmlWriter.WriteStartElement("float_array");
                        xmlWriter.WriteAttributeString("id", hash + "_PA");
                        xmlWriter.WriteAttributeString("count", Convert.ToString(shkRoad[num6].vertexList.Count * 3));
                        StringBuilder stringBuilder4 = new StringBuilder();
                        enumerator6 = shkRoad[num6].vertexList.GetEnumerator();
                        try
                        {
                            while (enumerator6.MoveNext())
                            {
                                Utilitaires.Vertex current7 = enumerator6.Current;
                                stringBuilder4.Append(Convert.ToString(current7.x, new CultureInfo("en-US")) + ' ' + Convert.ToString(current7.y, new CultureInfo("en-US")) + ' ' + Convert.ToString(current7.z, new CultureInfo("en-US")) + ' ');
                            }
                        }
                        finally
                        {
                            ((IDisposable)enumerator6).Dispose();
                        }
                        xmlWriter.WriteString(stringBuilder4.ToString());
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("technique_common");
                        xmlWriter.WriteStartElement("accessor");
                        xmlWriter.WriteAttributeString("source", '#' + hash + "_PA");
                        xmlWriter.WriteAttributeString("count", Convert.ToString(shkRoad[num6].vertexList.Count));
                        xmlWriter.WriteAttributeString("stride", "3");
                        xmlWriter.WriteStartElement("param");
                        xmlWriter.WriteAttributeString("name", "X");
                        xmlWriter.WriteAttributeString("type", "float");
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("param");
                        xmlWriter.WriteAttributeString("name", "Y");
                        xmlWriter.WriteAttributeString("type", "float");
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("param");
                        xmlWriter.WriteAttributeString("name", "Z");
                        xmlWriter.WriteAttributeString("type", "float");
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("vertices");
                        xmlWriter.WriteAttributeString("id", hash + "_V");
                        xmlWriter.WriteStartElement("input");
                        xmlWriter.WriteAttributeString("semantic", "POSITION");
                        xmlWriter.WriteAttributeString("source", '#' + hash + "_P");
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("triangles");
                        xmlWriter.WriteAttributeString("count", Convert.ToString(shkRoad[num6].faceList.Count / 3));
                        xmlWriter.WriteStartElement("input");
                        xmlWriter.WriteAttributeString("semantic", "VERTEX");
                        xmlWriter.WriteAttributeString("source", '#' + hash + "_V");
                        xmlWriter.WriteAttributeString("offset", "0");
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("p");
                        StringBuilder stringBuilder5 = new StringBuilder();
                        for (int num7 = 0; num7 < shkRoad[num6].faceList.Count; num7++)
                        {
                            stringBuilder5.Append(Convert.ToString(shkRoad[num6].faceList[num7], new CultureInfo("en-US")) + ' ');
                        }
                        xmlWriter.WriteString(stringBuilder5.ToString());
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndElement();
                    }
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("library_visual_scenes");
                xmlWriter.WriteStartElement("visual_scene");
                xmlWriter.WriteAttributeString("id", "Scene");
                xmlWriter.WriteAttributeString("name", "Scene");
                xmlWriter.WriteStartElement("node");
                xmlWriter.WriteAttributeString("id", sector);
                xmlWriter.WriteAttributeString("name", sector);
                xmlWriter.WriteAttributeString("type", "NODE");
                if (obj)
                {
                    xmlWriter.WriteStartElement("node");
                    xmlWriter.WriteAttributeString("id", "DYNAMIC");
                    xmlWriter.WriteAttributeString("name", "DYNAMIC");
                    xmlWriter.WriteAttributeString("type", "NODE");
                    pushedIndex = new List<int>();
                    for (int num8 = 0; num8 < TDUscene.Count; num8++)
                    {
                        if (!pushedIndex.Contains(num8) && !TDUscene[num8].isRoot)
                        {
                            writeNode(TDUscene[num8], num8, xmlWriter);
                            pushedIndex.Add(num8);
                        }
                    }
                    xmlWriter.WriteEndElement();
                }
                if (hmap)
                {
                    xmlWriter.WriteStartElement("node");
                    xmlWriter.WriteAttributeString("id", "HMAP");
                    xmlWriter.WriteAttributeString("name", "HMAP");
                    xmlWriter.WriteAttributeString("type", "NODE");
                    xmlWriter.WriteStartElement("matrix");
                    xmlWriter.WriteAttributeString("sid", "transform");
                    xmlWriter.WriteString("-4.37114e-8 0 -1 0 3.55271e-15 1 0 0 1 -3.55271e-15 -4.37114e-8 0 0 0 0 1");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("instance_geometry");
                    xmlWriter.WriteAttributeString("url", "#HMAP");
                    xmlWriter.WriteStartElement("bind_material");
                    xmlWriter.WriteStartElement("technique_common");
                    xmlWriter.WriteStartElement("instance_material");
                    xmlWriter.WriteAttributeString("symbol", "TERRAIN");
                    xmlWriter.WriteAttributeString("target", "#TERRAIN");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                }
                if (havokr || havokw)
                {
                    xmlWriter.WriteStartElement("node");
                    xmlWriter.WriteAttributeString("id", "HAVOK");
                    xmlWriter.WriteAttributeString("name", "HAVOK");
                    xmlWriter.WriteAttributeString("type", "HAVOK");
                    if (havokr)
                    {
                        xmlWriter.WriteStartElement("node");
                        xmlWriter.WriteAttributeString("id", "ROAD");
                        xmlWriter.WriteAttributeString("name", "ROAD");
                        xmlWriter.WriteAttributeString("type", "NODE");
                        xmlWriter.WriteStartElement("matrix");
                        xmlWriter.WriteAttributeString("sid", "transform");
                        xmlWriter.WriteString("1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1");
                        xmlWriter.WriteEndElement();
                        for (int num9 = 0; num9 < shkRoad.Count; num9++)
                        {
                            xmlWriter.WriteStartElement("node");
                            xmlWriter.WriteAttributeString("id", shkRoad[num9].hash);
                            xmlWriter.WriteAttributeString("name", shkRoad[num9].hash);
                            xmlWriter.WriteAttributeString("type", "NODE");
                            xmlWriter.WriteStartElement("instance_geometry");
                            xmlWriter.WriteAttributeString("url", '#' + shkRoad[num9].hash);
                            xmlWriter.WriteEndElement();
                            xmlWriter.WriteEndElement();
                        }
                        xmlWriter.WriteEndElement();
                    }
                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("scene");
                xmlWriter.WriteStartElement("instance_visual_scene");
                xmlWriter.WriteAttributeString("url", "#Scene");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
                pForm.Logger("Successful import! (" + (hmap ? " HMap " : string.Empty) + (obj ? " Objects " : string.Empty) + (havokr ? " SHK Roads " : string.Empty) + ")", 1);
                pForm.Invoke((MethodInvoker)delegate {
                    pForm.button3.Enabled = true;
                });
            }
        }
    }
}
