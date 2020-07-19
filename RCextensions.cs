using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using ExitGames.Client.Photon;
using Photon;
using UnityEngine;
using System.Linq;
using System.Text;
using System.Globalization;
using CyanMod;

public static class cext
{
    public static Regex sizeMatch = new Regex("(([[])(\\w{6,6})([]]))", RegexOptions.IgnoreCase);
    public static Regex hex_code = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
    public static Regex sizeMatchCyanMod = new Regex(@"(<( *)size=( *)(([3-9]\d[^\D]*|\d{3,}[^\D]*).*?))( *>)", RegexOptions.IgnoreCase);

    public static void GUIHelp_Info_CN(string text)
    {
        GUI.backgroundColor = Color.white;
        GUIStyle styles = new GUIStyle(GUI.skin.label);
        styles.normal.background = Coltext.graytext;
        styles.normal.textColor = Color.white;
        GUILayout.BeginHorizontal();
        GUILayout.TextField(text, GUILayout.Width(200f));
        GUILayout.Label(text, styles, GUILayout.Width(180f));
        GUILayout.EndHorizontal();
    }
    
    public static string Resize(this string text,int size)
    {
        return sizeMatchCyanMod.Replace(text, "<size="+size.ToString() + ">");
    }
    public static void Add<T>(ref T[] source, T value)
    {
        T[] localArray = new T[source.Length + 1];
        for (int i = 0; i < source.Length; i++)
        {
            localArray[i] = source[i];
        }
        localArray[localArray.Length - 1] = value;
        source = localArray;
    }
    public static string hex_allov(this string text)
    {
        Match match = hex_code.Match(text.Trim());
        int rrr = 0;
        while (match.Success)
        {
            rrr++;
            match = match.NextMatch();
        }
        if (rrr % 2 == 0)
        {
            return text;
        }
        else
        {
            return text = hex_code.Replace(text, string.Empty);
        }

    }
    public static string[] EmptyD(this string[] texts)
    {
        int num1 = texts.Length;
        if (num1 > 0)
        {
            int num2 = 0;
            for (int i = 0; i < num1; i++)
            {
                if (texts[i].Replace(" ", "") != string.Empty)
                {
                    num2++;
                }
            }
            if (num2 > 0)
            {
                string[] str2 = new string[num2];
                num2 = 0;
                for (int i = 0; i < num1; i++)
                {
                    if (texts[i].Replace(" ", "") != string.Empty)
                    {
                        str2[num2] = texts[i];
                        num2++;
                    }
                }
                return str2;
            }
            else
            {
                return new string[0];
            }
        }
        return texts;
    }
    public static string StripHex(this string text)
    {
        text = text.Replace("[-]", string.Empty);
        return text = sizeMatch.Replace(text, string.Empty);

    }
    public static string toHex(this string text)
    {

        if (text != string.Empty && text.Contains("]") && text.Contains("["))
        {
            text = text.Replace("[-]", string.Empty);
            Match match = sizeMatch.Match(text);
            int rrr = 0;
            while (match.Success)
            {
                string text23 = "<color=#" + match.Value.Replace("[", string.Empty).Replace("]", string.Empty) + ">";
                text = text.Remove(match.Index + rrr, 8);
                text = text.Insert(match.Index + rrr, text23) + "</color>";
                rrr += 7;
                match = match.NextMatch();
            }
        }
        return text;
    }
    public static bool isInt(this string text)
    {
        int num;
        if (text != string.Empty && int.TryParse(text, out num))
        {
            return true;
        }
        return false;
    }
    public static string NullFix(this string nullorempty)
    {
        if (nullorempty == null)
        {
            return string.Empty;
        }
        return nullorempty;
    }
    public static Rect maxrect(this Rect rect)
    {
        if ((rect.x + rect.width) > Screen.width)
        {
            rect.x = Screen.width - rect.width;
        }
        if ((rect.y + rect.height) > Screen.height)
        {
            rect.y = Screen.height - rect.height;
        }
        if (rect.y < 0)
        {
            rect.y = 0;
        }
        if (rect.x < 0)
        {
            rect.x = 0;
        }
        return rect;
    }
    public static Texture2D toTexture1(this Color color)
    {

        Texture2D texture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
        texture.SetPixel(0, 0, color);
        texture.Apply();
        return texture;
    }
    public static string HexDell(this string value)
    {
        return value = Regex.Replace(value, "<[^>]+>", string.Empty);
    }
    public static bool isLowestID(this PhotonPlayer player)
    {
        foreach (PhotonPlayer player2 in PhotonNetwork.playerList)
        {
            if (player2.ID < player.ID)
            {
                return false;
            }
        }
        return true;
    }
    public static Texture2D loadimage(WWW link, bool mipmap, int size)
    {
        Texture2D tex = new Texture2D(4, 4, TextureFormat.DXT1, mipmap);
        if (link.size >= size)
        {
            return tex;
        }
        Texture2D texture = link.texture;
        int width = texture.width;
        int height = texture.height;
        int num3 = 0;
        if ((width < 4) || ((width & (width - 1)) != 0))
        {
            num3 = 4;
            width = Math.Min(width, 0x3ff);
            while (num3 < width)
            {
                num3 *= 2;
            }
        }
        else if ((height < 4) || ((height & (height - 1)) != 0))
        {
            num3 = 4;
            height = Math.Min(height, 0x3ff);
            while (num3 < height)
            {
                num3 *= 2;
            }
        }
        if (num3 == 0)
        {
            if (mipmap)
            {
                try
                {
                    link.LoadImageIntoTexture(tex);
                }
                catch
                {
                    tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
                    link.LoadImageIntoTexture(tex);
                }
                return tex;
            }
            link.LoadImageIntoTexture(tex);
            return tex;
        }
        if (num3 < 4)
        {
            return tex;
        }
        Texture2D textured3 = new Texture2D(4, 4, TextureFormat.DXT1, false);
        link.LoadImageIntoTexture(textured3);
        if (mipmap)
        {
            try
            {
                textured3.Resize(num3, num3, TextureFormat.DXT1, mipmap);
                goto Label_00FE;
            }
            catch
            {
                textured3.Resize(num3, num3, TextureFormat.DXT1, false);
                goto Label_00FE;
            }
        }
        textured3.Resize(num3, num3, TextureFormat.DXT1, mipmap);
    Label_00FE:
        textured3.Apply();
        return textured3;
    }
    public static void RemoveAt<T>(ref T[] source, int index)
    {
        if (source.Length == 1)
        {
            source = new T[0];
        }
        else if (source.Length > 1)
        {
            T[] localArray = new T[source.Length - 1];
            int num = 0;
            int num2 = 0;
            while (num < source.Length)
            {
                if (num != index)
                {
                    localArray[num2] = source[num];
                    num2++;
                }
                num++;
            }
            source = localArray;
        }
    }
    public static bool returnBoolFromObject(object obj)
    {
        return (((obj != null) && (obj is bool)) && ((bool)obj));
    }
    public static float returnFloatFromObject(object obj)
    {
        if ((obj != null) && (obj is float))
        {
            return (float)obj;
        }
        return 0f;
    }
    public static int returnIntFromObject(object obj)
    {
        if ((obj != null) && (obj is int))
        {
            return (int)obj;
        }
        return 0;
    }
    public static string isString(this object obj)
    {
        if (obj != null)
        {
            string str = obj as string;
            if (str != null)
            {
                return str;
            }
        }
        return string.Empty;
    }
    public static string Cript(this string text)
    {
        return new SimpleAES().Decrypt(text);
    }
    public static void mess(string text)
    {
        object[] parameters = new object[] { InRoomChat.StyleMes(text), string.Empty };
        FengGameManagerMKII.instance.photonView.RPC("Chat", PhotonTargets.All, parameters);
    }
    public static void mess(string text, PhotonPlayer player)
    {
        object[] parameters = new object[] { InRoomChat.StyleMes(text), string.Empty };
        FengGameManagerMKII.instance.photonView.RPC("Chat", player, parameters);
    }
 
    public static string name(this PhotonPlayer player)
    {
        return player.name2.StripHex().Replace("\n", string.Empty);
    }
    public static UnityEngine.Object searhObj(this string obj)
    {
        UnityEngine.Object[] sss = Resources.FindObjectsOfTypeAll<UnityEngine.Object>();
        foreach (UnityEngine.Object objr in sss)
        {
            if (objr.name == obj)
            {
                return objr;
            }
        }
        return null;
    }
    public static GUIStyle colortext(this Color clr)
    {
        GUIStyle fsf = new GUIStyle(GUI.skin.box);
        fsf.normal.textColor = clr;
        return fsf;
    }
    public static byte[] ReadAllBytes(this System.IO.FileInfo fileinfo)
    {
        if (fileinfo == null)
        {
            throw new System.ArgumentNullException("fileinfo");
        }
        byte[] array;
        using (System.IO.FileStream fileStream = fileinfo.Open(System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read))
        {
            int num = 0;
            long length = fileStream.Length;
            if (length > 2147483647L)
            {
                throw new System.IO.IOException("IO.IO_FileTooLong2GB");
            }
            int i = (int)length;
            array = new byte[i];
            while (i > 0)
            {
                int num2 = fileStream.Read(array, num, i);
                if (num2 == 0)
                {
                    throw new System.IO.EndOfStreamException("IO.EOF_ReadBeyondEOF");
                }
                num += num2;
                i -= num2;
            }
        }
        return array;
    }
    public static string HexConverter(this Color c)
    {
        byte rByte = (byte)(c.r * 256f);
        byte gByte = (byte)(c.g * 256f);
        byte bByte = (byte)(c.b * 256f);
        return rByte.ToString("X2") + gByte.ToString("X2") + bByte.ToString("X2");
    }
    public static bool SetSkybox(string up, string down, string left, string right, string front, string back, bool flag)
    {

        Material mat = Camera.main.GetComponent<Skybox>().material;
        string[] dfgfdg = new string[] { up, down, left, right, front, back };
        if (flag)
        {
            for (int i = 0; i < dfgfdg.Length; i++)
            {
                string str = dfgfdg[i];
                Texture2D texture2D = new Texture2D(1, 1, TextureFormat.RGBA32, false);
                string file = Application.dataPath + "/Skybox/" + str;
                FileInfo up_info = new FileInfo(file);
                if (!up_info.Exists)
                {
                    UnityEngine.Debug.LogError("Cyan_mod|File Not Found. File:" + file);
                    return false;
                }
                else
                {
                    texture2D.LoadImage(up_info.ReadAllBytes());
                    if (i == 0)
                    {
                        mat.SetTexture("_UpTex", texture2D);
                    }
                    else if (i == 1)
                    {
                        mat.SetTexture("_DownTex", texture2D);
                    }
                    else if (i == 2)
                    {
                        mat.SetTexture("_LeftTex", texture2D);
                    }
                    else if (i == 3)
                    {
                        mat.SetTexture("_RightTex", texture2D);
                    }
                    else if (i == 5)
                    {
                        mat.SetTexture("_BackTex", texture2D);
                    }
                    else if (i == 4)
                    {
                        mat.SetTexture("_FrontTex", texture2D);
                    }
                }
            }
        }
        else
        {

            for (int i = 0; i < dfgfdg.Length; i++)
            {
                string str = dfgfdg[i];
                Texture2D texture2D = new Texture2D(1, 1, TextureFormat.DXT5, false);
                texture2D = (Texture2D)Statics.CMassets.Load(str);
                if (i == 0)
                {
                    mat.SetTexture("_UpTex", texture2D);
                }
                else if (i == 1)
                {
                    mat.SetTexture("_DownTex", texture2D);
                }
                else if (i == 2)
                {
                    mat.SetTexture("_LeftTex", texture2D);
                }
                else if (i == 3)
                {
                    mat.SetTexture("_RightTex", texture2D);
                }
                else if (i == 5)
                {
                    mat.SetTexture("_BackTex", texture2D);
                }
                else if (i == 4)
                {
                    mat.SetTexture("_FrontTex", texture2D);
                }
            }
        }
        Camera.main.GetComponent<Skybox>().material = mat;
        return true;
    }
    public static string returnStringFromObject(object obj)
    {
        if (obj != null)
        {
            string str = obj as string;
            if (str != null)
            {
                return str;
            }
        }
        return string.Empty;
    }
    public static Font font;
    public static TextMesh LabelObjext(TITAN titan)
    {
        if (font == null)
        {
            font = (Font)Resources.FindObjectsOfTypeAll(typeof(Font))[0];
        }
        GameObject obj = new GameObject("info_TITANs");
        float x = 1f;
        if (titan.myLevel < 1f)
        {
            x = 1f / titan.myLevel;
        }
        obj.transform.localScale = new Vector3(x, x, x);
        obj.transform.parent = titan.gameObject.transform;
        obj.transform.localPosition = Vector3.zero;
        obj.AddComponent<MeshRenderer>().material = font.material;
       TextMesh tm = obj.AddComponent<TextMesh>();
       tm.font = font;
       tm.fontSize = 0;
       tm.anchor = TextAnchor.MiddleCenter;
       tm.text = "size:" + titan.myLevel.ToString("F") + "\n" + "speed:"+ titan.speed.ToString("F") + "\n" + titan.abnormalType;
       return tm;
    }
    static float xs = 0f;
    static float ys = 0f;
    static float zs = 0f;
    static string[] localpos = new string[] { "0", "0", "0" };
    public static void pos_to_GUI_Slider(GameObject v, float width, float l, float r)
    {

        GUILayout.Label("x:" + xs.ToString("F"));
        xs = GUILayout.HorizontalSlider(xs, l, r, GUILayout.Width(width));
        GUILayout.Label("y:" + ys.ToString("F"));
        ys = GUILayout.HorizontalSlider(ys, l, r, GUILayout.Width(width));
        GUILayout.Label("z:" + zs.ToString("F"));
        zs = GUILayout.HorizontalSlider(zs, l, r, GUILayout.Width(width));
      
        GUILayout.BeginHorizontal();
        localpos[0] = GUILayout.TextField(localpos[0]);
        localpos[1] = GUILayout.TextField(localpos[1]);
        localpos[2] = GUILayout.TextField(localpos[2]);
        if (GUILayout.Button(">>"))
        {
            xs = Convert.ToSingle(localpos[0]);
            ys = Convert.ToSingle(localpos[1]);
            zs = Convert.ToSingle(localpos[2]);
        }
       
        GUILayout.EndHorizontal();
        Transform sd = HERO.myHero.transform.Find("Amarture/Controller_Body/hip/spine/chest/neck");
        v.transform.localPosition = new Vector3(sd.localPosition.x - xs, sd.localPosition.y + ys, sd.localPosition.z + zs);
    }
    public static string[] isSplit(this string text, char ch)
    {
        Regex hex_code = new Regex(ch.ToString());
        Match match = hex_code.Match(text);
        List<string> stt = new List<string>();
        List<int> pos = new List<int>();
        int a = 0;
        while (match.Success)
        {
            if (!pos.Contains(a))
            {
                string str = string.Empty;
                for (int i = a; i < match.Index; i++)
                {
                    str = str + text[i];
                }
                if (!stt.Contains(str))
                {
                    stt.Add(str.Replace(ch.ToString(), ""));
                }
                pos.Add(a);

            }
            else
            {
                break;
            }

            a = match.Index;
            match = match.NextMatch();
        }
        return stt.ToArray();
    }
    public static Color color_toGUI(Color col,string text = "",bool isAlpha = true)
    {
        float r = col.r;
        float g = col.g;
        float b = col.b;
        float a = col.a;
        if (text != "")
        {
            GUILayout.Label(text);
        }
        GUILayout.BeginHorizontal();
        GUILayout.Label("R:" + r.ToString("F"), GUILayout.Width(40f));
        r = GUILayout.HorizontalSlider(r,0f,0.99f);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("G:" + g.ToString("F"), GUILayout.Width(40f));
        g = GUILayout.HorizontalSlider(g, 0f, 0.99f);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("B:" + b.ToString("F"), GUILayout.Width(40f));
        b = GUILayout.HorizontalSlider(b, 0f, 0.99f);
        GUILayout.EndHorizontal();
        if (isAlpha)
        {
            GUILayout.BeginHorizontal();

            GUILayout.Label("A:" + a.ToString("F"), GUILayout.Width(40f));
            a = GUILayout.HorizontalSlider(a, 0f, 1f);

            GUILayout.EndHorizontal();
        }
        return new Color(r, g, b, a);
    }
    public static Color color_toGUI(Color col, GUIStyle style, string text = "", bool isAlpha = true)
    {
        float r = col.r;
        float g = col.g;
        float b = col.b;
        float a = col.a;
        if (text != "")
        {
            GUILayout.Label(text,style);
        }
        GUILayout.BeginHorizontal();
        GUILayout.Label("R:" + r.ToString("F"), GUILayout.Width(40f));
        r = GUILayout.HorizontalSlider(r, 0f, 0.99f);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("G:" + g.ToString("F"), GUILayout.Width(40f));
        g = GUILayout.HorizontalSlider(g, 0f, 0.99f);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("B:" + b.ToString("F"), GUILayout.Width(40f));
        b = GUILayout.HorizontalSlider(b, 0f, 0.99f);
        GUILayout.EndHorizontal();
        if (isAlpha)
        {
            GUILayout.BeginHorizontal();

            GUILayout.Label("A:" + a.ToString("F"), GUILayout.Width(40f));
            a = GUILayout.HorizontalSlider(a, 0f, 1f);

            GUILayout.EndHorizontal();
        }
        return new Color(r, g, b, a);
    }
    public static int int_slider_toGUI(int num,float min,float max, string text = "")
    {
        float s = (float)num;
        if (text != "")
        {
            GUILayout.Label(text);
        }
        GUILayout.BeginHorizontal();
        GUILayout.Label(s.ToString(), GUILayout.Width(40f));
        s = GUILayout.HorizontalSlider(s, min, max);
        GUILayout.EndHorizontal();

        return (int)s;
    }
    public static FontStyle font_style_toGUI(FontStyle fs, string text = "")
    {
        if (text != "")
        {
            GUILayout.Label(text);
        }
        int s = (fs == FontStyle.Bold ? 1 : fs == FontStyle.BoldAndItalic ? 2 : fs == FontStyle.Italic ? 3 : 0);
        s = GUILayout.SelectionGrid(s, new string[] { "no", "<i>i</i>", "<b>b</b>", "<i><b>i,b</b></i>" }, 4);
        return s == 1 ? FontStyle.Bold : s == 2 ? FontStyle.BoldAndItalic : s == 3 ? FontStyle.Italic : FontStyle.Normal;
    }
    public static TextAnchor anchor__toGUI(TextAnchor fs, string text = "")
    {
        if (text != "")
        {
            GUILayout.Label(text);
        }
        int s = 0;
        switch(fs)
        {
            case TextAnchor.UpperLeft: s=  0;
                break;
            case TextAnchor.UpperCenter: s = 1;
                break;
            case TextAnchor.UpperRight: s = 2;
                break;
            case TextAnchor.MiddleLeft: s = 3;
                break;
            case TextAnchor.MiddleCenter: s = 4;
                break;
            case TextAnchor.MiddleRight: s = 5;
                break;
            case TextAnchor.LowerLeft: s = 6;
                break;
            case TextAnchor.LowerCenter: s = 7;
                break;
            case TextAnchor.LowerRight: s = 8;
                break;
                
        }
        string[] str = new string[]{"ULeft","UCenter","URight" ,"MLeft","MCenter","MRight","LLeft","LCenter","LRight"};
        s = GUILayout.SelectionGrid(s,str,3);
        TextAnchor ta = TextAnchor.MiddleCenter;
        switch (s)
        {
            case 0: ta = TextAnchor.UpperLeft;
                break;
            case 1: ta = TextAnchor.UpperCenter;
                break;
            case 2: ta = TextAnchor.UpperRight;
                break;
            case 3: ta = TextAnchor.MiddleLeft;
                break;
            case 4: ta = TextAnchor.MiddleCenter;
                break;
            case 5: ta = TextAnchor.MiddleRight;
                break;
            case 6: ta = TextAnchor.LowerLeft;
                break;
            case 7: ta = TextAnchor.LowerCenter;
                break;
            case 8: ta = TextAnchor.LowerRight;
                break;
        }
        return ta;
    }
    public static ImagePosition imagePosition__toGUI(ImagePosition fs, string text = "")
    {
        int s = 0;
        switch(fs)
        {
            case ImagePosition.ImageAbove: s = 0;
                break;
            case ImagePosition.ImageLeft: s = 1;
                break;
            case ImagePosition.ImageOnly: s = 2;
                break;
            case ImagePosition.TextOnly: s = 3;
                break;
          
                
        }
        string[] str = new string[] { "ImageAbove", "ImageLeft", "ImageOnly", "TextOnly" };
        s = GUILayout.SelectionGrid(s,str,4);
        ImagePosition ta = ImagePosition.ImageAbove;
        switch (s)
        {
            case 0: ta = ImagePosition.ImageAbove;
                break;
            case 1: ta = ImagePosition.ImageLeft;
                break;
            case 2: ta = ImagePosition.ImageOnly;
                break;
            case 3: ta = ImagePosition.TextOnly;
                break;
           
        }
        return ta;
    }
    static float x = 0f;
    static float y = 0f;
    static float z = 0f;
    public static void rot_to_GUI_Slider(GameObject v, float width)
    {
      
        GUILayout.Label("x:" + x.ToString("F"));
        x = GUILayout.HorizontalSlider(x,0,360, GUILayout.Width(width));
        GUILayout.Label("y:" + y.ToString("F"));
        y = GUILayout.HorizontalSlider(y, 0,360, GUILayout.Width(width));
        GUILayout.Label("z:" + z.ToString("F"));
        z = GUILayout.HorizontalSlider(z, 0,360, GUILayout.Width(width));
        foreach (HERO h in FengGameManagerMKII.instance.heroes)
        {
            v.transform.rotation = h.transform.Find("Amarture/Controller_Body/hip/spine/chest/neck").rotation * Quaternion.Euler(x, y, z);
        }
      
    }
    public static Color hexToColor(this string hex)
    {
        byte r = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
        return (Color)new Color32(r, g, b, 0xff);
    }
    public static GameObject CreateLabel(GameObject parent, GameObject toClone, Vector3 position, Quaternion rotation, string name, string text, int fontsize, int lineWidth = 130)
    {
        GameObject prefab = (GameObject)UnityEngine.Object.Instantiate(toClone);
        GameObject obj3 = NGUITools.AddChild(parent, prefab);
        obj3.name = name;
        obj3.transform.localPosition = position;
        obj3.transform.localRotation = rotation;
        obj3.AddComponent<UILabel>();
        obj3.GetComponent<UILabel>().text = text;
        obj3.GetComponent<UILabel>().font.dynamicFontSize = fontsize;
        obj3.GetComponent<UILabel>().lineWidth = lineWidth;
        return obj3;
    }
    public static float calculate_formuls(int f, int kills, int death, int max_dmg, int total_dmg)
    {
        if (f == 0)
        {
            return (float)((double)(800 * kills + max_dmg) * (double)(10 - Math.Sqrt((double)death)) + (double)(100 * max_dmg)) / 10000;
        }
        else if (f == 1)
        {
            return (float)100 * kills - total_dmg;
        }
        else if (f == 2)
        {
            return (float)((double)(120 * kills + total_dmg) * (double)(10000 + max_dmg)) / (5 * 100000 * (20 + death));
        }
        else 
        {
            return (float)Math.Sqrt((double)((kills * (double)(total_dmg / 100) + max_dmg ) / (1 + death)));
        }
    }
    public static Texture2D loadResTexture(string name)
    {
        Texture2D texture = (Texture2D)Statics.CMassets.Load(name);
        if (texture != null)
        {
            return texture;
        }
        return null;
    }
    public static GameObject to_parent(GameObject gm, GameObject parrent,bool parrentPos = false,bool parrentRot = false)
    {
        if (parrentPos)
        {
            gm.transform.position = parrent.transform.position;
        }
        if (parrentRot)
        {
            gm.transform.rotation = parrent.transform.rotation;
        }
        gm.transform.parent = parrent.transform;
        return gm;
    }
    public static Texture2D loadImage ( string path )
    {
        FileInfo info = new FileInfo(path);
        if ( info.Exists )
        {
            byte[] sss = info.ReadAllBytes();
            Texture2D text = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            text.LoadImage(sss);
            text.Apply();
            return text;
        }
        return Coltext.transp;
    }
}


public class Example
{
    public void SSS()
    {
        GUIStyle style = new GUIStyle(GUI.skin.box);
        style.normal.background = LoadTextureSkin(Application.dataPath + "/skin_normal.png");
        if(GUILayout.Button("Example_button",style))
        {

        }
    }
   
    public Texture2D LoadTextureSkin(string path)
    {
        FileInfo info = new FileInfo(path);
        if(info.Exists)
        {
            byte[] sss = info.ReadAllBytes();
            Texture2D text = new Texture2D(1,1,TextureFormat.ARGB32,false);
            text.LoadImage(sss);
            text.Apply();
            return text;
        }
        return null;
    }
}