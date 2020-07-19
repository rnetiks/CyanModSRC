using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Effect : Photon.MonoBehaviour
{
    void Awake()
    {
        toSet_Shadow(base.gameObject.name + "_shadow");
        toSet_text(base.gameObject.name + "_text");
    }
     GameObject gameobj1;
     GameObject gameobj2;
     TextMesh text_one;
     TextMesh text_shadow;
     public string text
     {
         get
         {
            return text_one.text;
         }
         set
         {
             text_one.text = value;
             text_shadow.text = value.StripHex().HexDell();
         }
     }
     public Vector3 localScale
     {
         get
         {
             if (gameobj1 != null)
             {
                 return gameobj1.transform.localScale;
             }
             return Vector3.zero;
         }
         set
         {
             if (gameobj1 != null)
             {
                 gameobj1.transform.localScale = value;
                 gameobj2.transform.localScale = value * 1.1f;
             }
             else
             {
                 Debug.Log("Error. no gm localScale");
             }
         }
     }
     public Color text_color
     {
         set
         {
             text_one.color = value;
         }
     }
     public Color shadow_color
     {
       
         set
         {
             text_shadow.color = value;
         }
     }
     public TextAlignment aligment
     {
       
         set
         {
             text_one.alignment = value;
             text_shadow.alignment = value;
         }
     }
     public TextAnchor achor
     {
         set
         {
             text_one.anchor = value;
             text_shadow.anchor = value;
         }
     }
     public Font font
     {
         set
         {
             text_one.font = value;
             text_shadow.font = value;
         }
     }
     public int font_size
     {
         get
         {
             return text_one.fontSize;
         }
         set
         {
             text_one.fontSize = value;
             text_shadow.fontSize = value;
         }
     }
   
     void toSet_text(string name)
    {
        Font font = FengGameManagerMKII.fotHUD;
        gameobj1 = new GameObject(name);
        text_one = gameobj1.GetComponent<TextMesh>();
        if (text_one == null)
        {
            text_one = gameobj1.AddComponent<TextMesh>();
        }
        MeshRenderer mr = gameobj1.GetComponent<MeshRenderer>();
        if (mr == null)
        {
            mr = gameobj1.AddComponent<MeshRenderer>();
        }
        mr.material = font.material;
        gameobj1.transform.localScale = new Vector3(4.64f, 4.64f);
        text_one.font = font;
        text_one.fontSize = 30;
        text_one.anchor =  TextAnchor.MiddleCenter;
        text_one.alignment = TextAlignment.Center;
        text_one.color = Color.white;
        gameobj1.transform.parent = base.gameObject.transform;
    }
     void toSet_Shadow(string name)
     {
         Font font = FengGameManagerMKII.fotHUD;
         gameobj2 = new GameObject(name);
         text_shadow = gameobj2.GetComponent<TextMesh>();
         if (text_shadow == null)
         {
             text_shadow = gameobj2.AddComponent<TextMesh>();
         }
         MeshRenderer mr = gameobj2.GetComponent<MeshRenderer>();
         if (mr == null)
         {
             mr = gameobj2.AddComponent<MeshRenderer>();
         }
         mr.material = font.material;
         gameobj2.transform.localScale = new Vector3(9f, 9f);
         text_shadow.font = font;
         text_shadow.fontSize = 30;
         text_shadow.anchor = TextAnchor.MiddleCenter;
         text_shadow.alignment = TextAlignment.Center;
         text_shadow.color = Color.black;
         gameobj2.transform.parent = CyanMod.CachingsGM.Find("LabelInfoCenter").transform;
         
   
     }
   
}

