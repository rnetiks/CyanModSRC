using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;

public class Pashalca : UnityEngine.MonoBehaviour
{
   static Texture2D texture;
    float timer;
    Vector2 vector;
    bool max = false;
    void Start()
    {
        if (texture == null)
        {
            texture = cext.loadResTexture("pass_1");
        }
        timer = 0;
        vector = new Vector2(Screen.width, Screen.height);
    }
    void OnGUI()
    {
        GUI.DrawTexture(new Rect(vector.x, vector.y, texture.width, texture.height), texture);
    }
    void Update()
    {
       
        timer += Time.deltaTime;
        if (timer > 0.05f)
        {
            timer = 0;
            if (!max)
            {
                if (vector.x >= Screen.width - texture.width)
                {
                    vector.x = vector.x - 10;
                    vector.y = vector.y - 10;
                }
            }
            else
            {
                if (vector.x <= Screen.width)
                {
                    vector.x = vector.x + 10;
                    vector.y = vector.y + 10;
                }
                else
                {
                    Destroy(base.gameObject);
                }
            }
            if (vector.x <= Screen.width - texture.width && !max)
            {
                max = true;
                vector.x = Screen.width - texture.width;
                vector.y = Screen.height - texture.width;
            }
            
        }
    }
}

