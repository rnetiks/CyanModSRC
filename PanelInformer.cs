using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;
using System.IO;



public class PanelInformer : UnityEngine.MonoBehaviour
{
    public static PanelInformer instance;
    GUIStyle[] Style;
    List<INF> ListLoged;
    public static List<string> logtype = new List<string>();
    public void Add(string text, LOG_TYPE log)
    {
        while (ListLoged.Count > 10)
        {
            ListLoged.Remove(ListLoged[0]);
        }
        INF inf = new INF(text, log);
        ListLoged.Add(inf );
        logtype.Add("[" + inf.date_time + "][" + inf.log_type + "]" + inf.message);
        while (logtype.Count > 150)
        {
            logtype.Remove(logtype[0]);
        }
    }
    void Awake()
    {
        DontDestroyOnLoad(base.gameObject);
        instance = this;
        ListLoged = new List<INF>();
    }
    void OnDestroy()
    {
        instance = null;
    }
    GUIStyle tex(LOG_TYPE loged)
    {
        if (loged == LOG_TYPE.INFORMAION)
        {
            return Style[0];
        }
        else if (loged == LOG_TYPE.WARNING)
        {
            return Style[1];
        }
        else
        {
            return Style[2];
        }
        
    }
    void OnGUI()
    {
        if (Style == null)
        {
            Texture2D[] texture = new Texture2D[3];
            string[] str = new string[] { "Ok.png", "Warning.png", "Danger.png" };
            for (int s = 0; s < str.Length; s++)
            {
                texture[s] = cext.loadImage(Application.dataPath + "/Style/" + str[s]);
            }
            Style = new GUIStyle[3];
            Style[0] = new GUIStyle();
            Style[0].normal.background = texture[0];
            Style[1] = new GUIStyle();
            Style[1].normal.background = texture[1];
            Style[2] = new GUIStyle();
            Style[2].normal.background = texture[2];

        }
        if (ListLoged.Count > 0)
        {
            for (int s = 0; s < ListLoged.Count; s++)
            {
                Rect rect = new Rect(Screen.width - 300f, 0f, 300f, 50f);
                INF inf = ListLoged[s];
                inf.Lifetime -= FengGameManagerMKII.deltaTime;
                if (inf.Lifetime < 1)
                {
                    inf.upd_time += FengGameManagerMKII.deltaTime;
                    if (inf.upd_time > 0.1)
                    {
                        inf.upd_time = 0;
                        if (inf.transparament > 0)
                        {
                            inf.transparament = inf.transparament - 0.1f;
                        }
                    }
                }
                if (inf.Lifetime < 0)
                {
                    ListLoged.Remove(inf);
                    return;
                }
                GUI.depth = -999;
                Color color = new Color(1f, 1f, 1f, inf.transparament);
                GUI.backgroundColor = color;
                rect.y = (50 * s);
                GUI.Box(rect, "", tex(inf.log_type));
                GUIStyle style = new GUIStyle(GUI.skin.label);
                style.alignment = TextAnchor.UpperLeft;
                style.normal.textColor = color;
                rect.x = rect.x + 35f;
                rect.y = rect.y + 5f;
                rect.width = 260f;
                rect.height = 50;
                GUI.Label(rect, "<size=15>" + inf.message + "</size>", style);
                rect.y = rect.y + 25f;
                rect.x = rect.x + 200f;
                GUI.Label(rect, "<size=12>" + inf.date_time + "</size>", style);
            }
        }
    }
    public class INF
    {
        public float Lifetime = 6f;
        public float upd_time = 0f;
        public float transparament = 1f;
        public string message;
        public LOG_TYPE log_type;
        public string date_time;
        public INF(string mes, LOG_TYPE log)
        {
            message = mes;
            log_type = log;
            date_time = DateTime.Now.ToString("HH:mm:ss");
        }

    }
    public enum LOG_TYPE
    {
        INFORMAION,
        WARNING,
        DANGER
        
    }
}

