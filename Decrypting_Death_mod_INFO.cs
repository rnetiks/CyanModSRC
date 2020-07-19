using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

	public class Decrypting_Death_mod_INFO : UnityEngine.MonoBehaviour
	{
        string text_in;
        string text_out;
        List<string> log;
        Vector2 scrolPos;
        Vector2 scrolPos1;
        Vector2 scrolPos2;
        void AddLog(string text)
        {
            log.Add("[" + DateTime.Now.ToString("HH:mm:ss") + "]" + text);
            if (log.Count > 50)
            {
                log.Remove(log[0]);
            }
        }
        void Start()
        {
            text_in = "";
            text_out = "";
            log = new List<string>();
        }
        void OnGUI()
        {
            GUI.backgroundColor = Color.black;
            GUILayout.BeginArea(new Rect(Screen.width / 2 - 300f, Screen.height / 2 - 200f, 600, 400), GUI.skin.box);
            GUILayout.BeginHorizontal();

            GUILayout.BeginVertical(GUILayout.Width(200f));
            scrolPos = GUILayout.BeginScrollView(scrolPos);
            text_in = GUILayout.TextArea(text_in);
            GUILayout.EndScrollView();
            if (GUILayout.Button("Обработать"))
            {
                Decompil(text_in);
            }
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            scrolPos1 = GUILayout.BeginScrollView(scrolPos1, GUILayout.Height(300f));
            GUILayout.TextArea(text_out);
            GUILayout.EndScrollView();
            scrolPos2 = GUILayout.BeginScrollView(scrolPos2);
            foreach (string text in log)
            {
                GUILayout.Label(text);
            }
            GUILayout.EndScrollView();
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
        void Decompil(string text)
        {
         
            try
            {
                if (text.Trim() == "")
                {
                    AddLog("Ничего не введено.");
                    return;
                }
                text_out = "";
                string[] iteratorVariable1 = Encoding.UTF8.GetString(new SimpleAES()[new byte[] { 
                230, 0x6b, 0xb6, 0x5f, 0x57, 0xfd, 0, 0xad, 70, 0x7c, 0x2f, 0xb6, 0x1a, 0x16, 0xa9, 0xf3, 
                0xf9, 0x30, 3, 0xb6, 0x86, 170, 0x6a, 0x2e, 0xd0, 0x10, 0x77, 0x23, 90, 0xbf, 0x81, 3, 
                0xea, 0xe8, 0x65, 0x8a, 0x7c, 0x88, 0x87, 0x54, 0xe0, 0x9f, 0x5f, 0x3e, 0x4b, 0x27, 0x72, 0x30, 
                0x20, 0xf4, 0x89, 0x8a, 0x97, 40, 0xf5, 1, 0x9c, 0x2f, 0xdf, 6, 0xc1, 0xbf, 0x2a, 0x8d, 
                0xd8, 0x9c, 0x2a, 0x5d, 0xeb, 0x5e, 12, 40, 0xe1, 0x5f, 0x86, 0x17, 0xa2, 180, 0xf4, 0x5b, 
                0xdf, 0x72, 0x9d, 0xf8, 5, 0xc4, 0x98, 0x76, 0x65, 0x86, 0x4c, 0x35, 0x87, 0xd0, 0xd7, 0x9c, 
                0xd3, 0xd3, 0x1f, 0x3f, 0x51, 0x12, 0xe9, 0x57, 0x79, 0x60, 0xe2, 0xfc, 210, 0xc6, 0x49, 0xe2, 
                0x63, 0x15, 0x42, 0xf8, 0xda, 0x38, 0xd0, 0xa7, 0x6c, 0x26, 0xad, 0x60, 90, 0x2d, 0x2f, 250, 
                160, 0x91, 0x9c, 0x20, 60, 0xf5, 0xa9, 0x85, 0x56, 0x77, 0x93, 0x8b, 0x94, 0x2e, 0x63, 0xbd, 
                0x3d, 0x65, 0xe3, 0xc9, 0x4b, 0x13, 180, 0x20, 30, 0x6b, 0xb2, 0xbf, 0xa9, 0x83, 0x89, 0x87, 
                130, 0xdd, 0xbd, 0xc2, 0xc1, 4, 0x68, 0xb6, 0xbd, 0xa3, 0x60, 0xdf, 0x9f, 0x4b, 0x43, 0xa3, 
                0x65, 0xb3, 0x13, 0x39, 0x87, 0x24, 0x8d, 240, 0x42, 0x2c, 0x4e, 0xf5, 0x8d, 0x6c, 0x29, 0x88, 
                0xaf, 0x7c, 0x94, 0x6b, 0xc3, 0x95, 0xcb, 0x38, 3, 150, 0x7a, 0x8d, 11, 0x4d, 0xd3, 0xf4
             }]).Split(new char[] { ',' });
                foreach (string srre in iteratorVariable1)
                {
                    text_out = text_out + srre + "\n";
                }
                AddLog("Успешно обработано. Строк " + iteratorVariable1.Length.ToString());
            }
            catch (Exception e)
            {
                AddLog("Ошибка:" + e.ToString());
            }
        }
	}

