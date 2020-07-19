using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using CyanMod;
using System.IO;

	public class TexturesBackgrounds : UnityEngine.MonoBehaviour
	{
      static Texture2D backgroundGeneralF;
      static Texture2D backgroundRebindsF;
      static Texture2D backgroundSkinsF;
      static Texture2D backgroundCustomF;
      static Texture2D backgroundOtherF;
      static Texture2D backgroundF;
      static List<UIPanel> uipanels = new List<UIPanel>();

      public static void Init()
      {
          DirectoryInfo dir = new DirectoryInfo(Application.dataPath + "/Style");
          if (dir.Exists)
          {
              FileInfo info1111111111111111111111 = new FileInfo(dir.FullName + "/background.png");
              if (info1111111111111111111111.Exists)
              {
                  Texture2D texture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
                  texture.LoadImage(info1111111111111111111111.ReadAllBytes());
                  texture.Apply();
                  backgroundF = texture;
                  if (UIDrawCalls.instance != null)
                  {
                      UIDrawCall uidraw = UIDrawCalls.instance.GetComponent<UIDrawCall>();
                      if (uidraw != null)
                      {
                          uidraw.material.mainTexture = (Texture)backgroundF;
                      }
                  }
              }

              FileInfo info111111111111111111111 = new FileInfo(dir.FullName + "/background_other.png");
              if (info111111111111111111111.Exists)
              {
                  Texture2D texture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
                  texture.LoadImage(info111111111111111111111.ReadAllBytes());
                  texture.Apply();
                  backgroundOtherF = texture;
              }

              FileInfo info11111111111111111111 = new FileInfo(dir.FullName + "/background_custum.png");
              if (info11111111111111111111.Exists)
              {
                  Texture2D texture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
                  texture.LoadImage(info11111111111111111111.ReadAllBytes());
                  texture.Apply();
                  backgroundCustomF = texture;
              }

              FileInfo info1111111111111111111 = new FileInfo(dir.FullName + "/background_skins.png");
              if (info1111111111111111111.Exists)
              {
                  Texture2D texture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
                  texture.LoadImage(info1111111111111111111.ReadAllBytes());
                  texture.Apply();
                  backgroundSkinsF = texture;
              }

              FileInfo info111111111111111111 = new FileInfo(dir.FullName + "/background_rebinds.png");
              if (info111111111111111111.Exists)
              {
                  Texture2D texture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
                  texture.LoadImage(info111111111111111111.ReadAllBytes());
                  texture.Apply();
                  backgroundRebindsF = texture;
              }

              FileInfo info11111111111111111 = new FileInfo(dir.FullName + "/background_general.png");
              if (info11111111111111111.Exists)
              {
                  Texture2D texture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
                  texture.LoadImage(info11111111111111111.ReadAllBytes());
                  texture.Apply();
                  backgroundGeneralF = texture;
              }

            
          }
          else
          {
              dir.Create();
              Debug.Log("Create directory " + dir.FullName);
          }
      }
      public static void Clean()
      {
          if (backgroundGeneralF != null)
          {
              Destroy(backgroundGeneralF);
          }
          if (backgroundRebindsF != null)
          {
              Destroy(backgroundRebindsF);
          }
          if (backgroundSkinsF != null)
          {
              Destroy(backgroundSkinsF);
          }
          if (backgroundCustomF != null)
          {
              Destroy(backgroundCustomF);
          }
          if (backgroundOtherF != null)
          {
              Destroy(backgroundOtherF);
          }
          if (backgroundF != null)
          {
              Destroy(backgroundF);
          }
      }
        public static Texture2D back_general
        {
            get
            {
                if (backgroundGeneralF != null)
                {
                    return backgroundGeneralF;
                }
                return Coltext.transp;
            }
        }
        public static Texture2D back_rebinds
        {
            get
            {
                if (backgroundRebindsF != null)
                {
                    return backgroundRebindsF;
                }
                return Coltext.transp;
            }
        }
        public static Texture2D back_skins
        {
            get
            {
                if (backgroundSkinsF != null)
                {
                    return backgroundSkinsF;
                }
                return Coltext.transp;
            }
        }
        public static Texture2D back_custom
        {
            get
            {
                if (backgroundCustomF != null)
                {
                    return backgroundCustomF;
                }
                return Coltext.transp;
            }
        }
        public static Texture2D back_other
        {
            get
            {
                if (backgroundOtherF != null)
                {
                    return backgroundOtherF;
                }
                return Coltext.transp;
            }
        }
        public static Texture2D background
        {
            get
            {
                if (backgroundF != null)
                {
                    return backgroundF;
                }
                return null;
            }
        }
	}

