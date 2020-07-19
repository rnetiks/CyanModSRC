using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using CyanMod;


public class Grafics_Cyan_mod : UnityEngine.MonoBehaviour
{
    public List<float[]> positional;
    public string name_graf = "";
    public static Texture2D background;
    Texture2D pixels;
    public static Texture2D back_label;
    public Color color_set = new Color(0f, 1f, 0f, 1f);
    public Rect rect = new Rect(0, 0, 20, 20);
    public float value = 0;
    float timer = 0;
    float plus = 0;
    float max = 0;
    public float isMaximal = 0;
    public float isMaximal2 = 0;
    public bool isValue2 = false;
    public float value2 = 0f;
    public Color color_set2 = Color.yellow;
    public string name_graf2 = "";
    public int winID = 0;
    public bool stoped = true;

    void Awake()
    {
        DontDestroyOnLoad(base.gameObject);
    }
    void Start()
    {
        positional = new List<float[]>();
        positional.Add(new float[] { 0f, 0f });
        if (back_label == null)
        {
            back_label = (Texture2D)Statics.CMassets.Load("back_grafical");
        }
        if (background == null)
        {
            background = (Texture2D)Statics.CMassets.Load("grafical");
            background.wrapMode = TextureWrapMode.Repeat;
        }
        pixels = new Texture2D(200, 50, TextureFormat.ARGB32, false);
        pixels.SetPixels(0, 0, 200, 50, background.GetPixels());
        pixels.Apply();
    }
    void LateUpdate()
    {
        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE || IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER))
        {
            if (stoped)
            {
                timer += Time.deltaTime;
                if (timer > 0.5f)
                {
                    timer = 0;
                    positional.Add(new float[] { value, value2 });
                    if (positional.Count > 200)
                    {
                        positional.Remove(positional[0]);
                    }
                    Destroy(pixels);
                    pixels = new Texture2D(200, 50, TextureFormat.ARGB32, false);
                    pixels.SetPixels(0, 0, 200, 50, background.GetPixels());
                    foreach (float[] fl in positional)
                    {
                        float mmax = fl.Max();
                        if (max < mmax)
                        {
                            max = mmax;
                        }
                    }
                    plus = 49 / max;

                    int s = 0;
                    int v = 0;
                    for (int i = 0; i < positional.Count; i++)
                    {
                        int ps = (int)(positional[i][0] * plus);
                        pixels.SetPixel(i, ps, color_set);
                        if (ps > s && i != 0)
                        {
                            for (int d = s; d < ps; d++)
                            {
                                pixels.SetPixel(i, d, color_set);
                            }
                        }
                        else if (ps < s && i != 0)
                        {
                            for (int d = s; d > ps; d--)
                            {
                                pixels.SetPixel(i, d, color_set);
                            }
                        }
                        s = ps;
                        if (isValue2)
                        {
                            int cs = (int)(positional[i][1] * plus);
                            pixels.SetPixel(i, cs, color_set2);
                            if (cs > v && i != 0)
                            {
                                for (int d = v; d < cs; d++)
                                {
                                    pixels.SetPixel(i, d, color_set2);
                                }
                            }
                            else if (cs < v && i != 0)
                            {
                                for (int d = v; d > cs; d--)
                                {
                                    pixels.SetPixel(i, d, color_set2);
                                }
                            }
                            v = cs;
                        }
                    }
                    pixels.Apply();
                }
            }
        }
        else
        {
            Destroy(base.gameObject);
        }
    }
    public void OnGUI()
    {

        rect = GUILayout.Window(winID, rect, window, "", new GUIStyle());
        rect = rect.maxrect();
    }
    GUIStyle style_label;
    void window(int winID)
    {
        if (style_label == null)
        {
            style_label = new GUIStyle(GUI.skin.label); ;
            style_label.normal.background = back_label;
            style_label.border.top = 9;
            style_label.border.bottom = 9;
            style_label.border.left = 9;
            style_label.border.right = 9;
            style_label.padding.top = 0;
            style_label.padding.bottom = 0;
            style_label.padding.left = 0;
            style_label.padding.right = 0;
            style_label.margin.top = 0;
            style_label.margin.bottom = 0;
            style_label.margin.left = 0;
            style_label.margin.right = 0;
            style_label.fontSize = 12;
            style_label.wordWrap = false;
            style_label.stretchWidth = false;
        }
        style_label.normal.textColor = color_set;
        GUILayout.Label(name_graf + " " + value.ToString("F") + "/" + (isMaximal / 2).ToString("F") + "/" + isMaximal.ToString("F"), style_label);
        if (isValue2)
        {
            style_label.normal.textColor = color_set2;
            GUILayout.Label(name_graf2 + " " + value2.ToString("F") + "/" + (isMaximal2 / 2).ToString("F") + "/" + isMaximal2.ToString("F"), style_label);
        }
        GUILayout.BeginHorizontal();
        GUILayout.Label(pixels);
        style_label.normal.textColor = color_set;
        GUILayout.Label("-" + max.ToString("F") + "\n-" + (max / 2).ToString("F") + "\n-0", style_label);
        GUILayout.EndHorizontal();
        GUI.DragWindow();
    }
   public void clean()
    {
        positional = new List<float[]>();
        if (pixels != null)
        {
            Destroy(pixels);
        }
        pixels = new Texture2D(200, 50, TextureFormat.ARGB32, false);
        pixels.SetPixels(0, 0, 200, 50, background.GetPixels());
        pixels.Apply();
        max = 0;
        timer = 0;
    }
}

