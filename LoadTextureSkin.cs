using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;
using CyanMod;

public class LoadTextureSkin : UnityEngine.MonoBehaviour
{
    public string url = "";
    Texture2D texture;
    WWW www;
    float s = 1;
    string message = "";
    void OnDestroy()
    {
        Destroy(texture);
    }
    void Start()
    {
        if (url.ToLower().EndsWith("png") || url.ToLower().EndsWith("jpg") || url.ToLower().EndsWith("jpeg"))
        {
            StartCoroutine(loadTexture(url));
        }
        else
        {
            message = "ERROR: Invalid URL";
        }
    }
    void LateUpdate()
    {
        if (www != null)
        {
            if (www.error != null)
            {
                message = "ERROR Load Texture \n" + www.error;
                return;
            }
            if (www.isDone)
            {
                message = "Download complate!";
            }
            else
            {
                message = "Downloading... " + (int)(www.progress * 100) + "%.";
            }
        }
    }
    public void show(float width)
    {
        try
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(INC.la("close"), GUILayout.Width(100f)))
            {
                Destroy(FengGameManagerMKII.instance.texture_skin_view);
            }
            if (texture != null)
            {
                if (texture.height > width || texture.width > width)
                {
                    float r = width * (s + 0.2f);
                    if (texture.width > r)
                    {
                        if (GUILayout.Button("O+", GUILayout.Width(30f)))
                        {
                            s = s + 0.2f;
                            if (s > 4)
                            {
                                s = 4;
                            }
                        }
                    }
                    float h = width * (s - 0.2f);
                    if (texture.width > h && s - 0.2f > 1)
                    {
                        if (GUILayout.Button("O-", GUILayout.Width(30f)))
                        {
                            s = s - 0.2f;
                            if (s < 1)
                            {
                                s = 1;
                            }
                        }
                    }
                }
            }
            GUILayout.EndHorizontal();
            if (!www.isDone)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(LoadCyanModAnim.onlyT, new GUIStyle(), GUILayout.Width(30), GUILayout.Height(30));
                GUILayout.Label(message);
                GUILayout.EndHorizontal();
            }
            else
            {
                GUILayout.Label(message);
            }
            if (texture != null)
            {
                GUIStyle style = new GUIStyle();
                if (texture.height > width || texture.width > width)
                {
                    style.fixedHeight = width * s;
                    style.fixedWidth = width * s;
                }
                GUILayout.Box(texture, style);
            }
        }
        catch
        {
            GUILayout.Label("Error.");
        }
     
    }
    IEnumerator loadTexture(string uri)
    {
        www = new WWW(uri);
        yield return www;
        if (www.error != null)
        {
            yield break;
        }
        else if (www.isDone)
        {
            texture = www.texture;
            yield break;
        }
    }
}

