using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using CyanMod;
using System.IO;
public class ScreenShots : UnityEngine.MonoBehaviour
{
    public static string path = Directory.GetCurrentDirectory() + @"\Screenshots\";
    Rect rect;
    float timer;
    float timer2;
    GUIStyle style;
    float t1;
    void Start()
    {
        DateTime now = DateTime.Now;
        string str2 = "screen_" + now.ToString().Replace("/", "_").Replace(" ", "_").Replace(":", "_") + ".png";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        Application.CaptureScreenshot(path + str2);
        rect = new Rect(Screen.width, 0f, 150, 35);
        timer = 0f;
        timer2 = 0f;
        t1 = Screen.width - 150f;
    }
    void OnGUI()
    {
        if (t1 < rect.x)
        {
            timer += FengGameManagerMKII.deltaTime;
            if (timer > 0.1)
            {
                timer = 0;
                rect.x = rect.x - 15f;
            }
        }
        else
        {
             timer2 += FengGameManagerMKII.deltaTime;
             if (timer2 > 1.2f)
             {
                 Destroy(base.gameObject);
                 return;
             }
        }
        if (style == null)
        {
            style = new GUIStyle();
            style.normal.background = Coltext.graytext;
        }
        GUI.depth= -200;
        GUILayout.BeginArea(rect,style);
        GUILayout.BeginHorizontal();
        GUILayout.Label(LoadCyanModAnim.onlyT, GUILayout.Width(25f));
        GUILayout.Label("<color=cyan><size=16>Sreenshot</size></color>");
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
}

