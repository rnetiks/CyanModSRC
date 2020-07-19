using Photon;
using System;
using System.Globalization;
using UnityEngine;

internal class ChatFade : Photon.MonoBehaviour
{
    public static ChatFade instance = new ChatFade();

    public string clearCodes(string str)
    {
        while (str.Contains("[-]"))
        {
            str = str.Replace("[-]", "");
        }
        while (str.Contains("["))
        {
            int index = str.IndexOf("[");
            str = str.Remove(index, 8);
        }
        return str;
    }

    public string colorToHex(Color32 color)
    {
        return (color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2"));
    }

    public Color hexToColor(string hex)
    {
        byte r = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
        return (Color) new Color32(r, g, b, 0xff);
    }
}

