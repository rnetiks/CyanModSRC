using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;

	public class LoadCyanModAnim
	{
        static List<Texture2D> textured;
        static Texture2D noimage;
      static  int textCount = 0;
     static float time = 0f;
        static bool isInit = false;
        static bool isFirstLoad = true;


        static void isUpdated()
        {
           time += FengGameManagerMKII.deltaTime;
           if (time > 0.1)
            {
                time = 0;
                textCount = textCount + 1;
                if(textCount > textured.Count -1)
                {
                    textCount = 0;
                }
            }
        }
        static void Init()
        {
            FileInfo info = new FileInfo(Application.dataPath + "/imageLoaded.png");
            noimage = Color.clear.toTexture1();
            if (info.Exists)
            {
                textured = new List<Texture2D>();
                byte[] bt = info.ReadAllBytes();
                Texture2D textwee = new Texture2D(1, 1);
                textwee.LoadImage(bt);
                textwee.Apply();
                int y = 0;
                for (int i = 0; i < 10; i++)
                {
                    Texture2D text = new Texture2D(120, 120, TextureFormat.ARGB32, false);
                    text.SetPixels(textwee.GetPixels(0, 120 * i, 120, 120));
                    text.Apply();
                    textured.Add(text);
                }
                isInit = true;
            }
            isFirstLoad = false;
        }
        public static Texture2D onlyT
        {
            get
            {
                if (isFirstLoad && !isInit)
                {
                    Init();
                }
                if (isInit)
                {
                    isUpdated();
                    return textured[textCount];
                }
                return noimage;
            }
        }
	}

