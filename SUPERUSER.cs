using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;
using CyanMod;
using System.Collections;

public class SUPERUSER : UnityEngine.MonoBehaviour
{
    public static string isLogeds = "";
    List<string> special_list;
    public static string special_ground = "sasha";
    public static string special_air = "levi";
    public static float speed_special = 1f;
    readonly static string config_path = Application.dataPath + "/super_user_config.cfg";
    Rect rect = new Rect(5, 5, 250, 400);
   public static int[] settings;
    bool show_panel = false;
    GUIStyle styleLabel;
    public static float speed_dc = 1f;
    public static string login = "";
    public static string pasword = "";
    bool loggge = false;


    void Update()
    {
        if (!show_panel && SUPERUSER.isLogeds != "Logged")
        {
            if (Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
            {
                show_panel = true;
            }

        }
        if ((Input.GetKeyDown(KeyCode.Alpha0) && SUPERUSER.isLogeds == "Logged") || show_panel && Input.GetKeyDown(KeyCode.Alpha0))
        {
            show_panel = !show_panel;
        }
        if (SUPERUSER.isLogeds == "Logged")
        {
            if (HERO.myHero != null)
            {
                if (HERO.myHero.IsGrounded())
                {
                    if (HERO.myHero.skillId != special_ground)
                    {
                        SetSprite(true);
                    }
                }
                else
                {
                    if (HERO.myHero.skillId != special_air)
                    {
                        SetSprite(false);
                    }
                }
            }
        }
    }
    void SetSprite(bool isGround)
    {
        if (isGround)
        {
            HERO.myHero.skillId = special_ground;
        }
        else
        {
            HERO.myHero.skillId = special_air;
        }
        HERO.myHero.skillIDHUD = HERO.myHero.skillId;
        HERO.myHero.setSkillHUDPosition2();
        foreach (var ui in HERO.myHero.cachedSprites)
        {
            if (ui.Value != null)
            {
                if (ui.Key == "skill_cd_" + HERO.myHero.skillIDHUD)
                {
                    ui.Value.enabled = true;
                }
                else if (ui.Key.StartsWith("skill_cd_") && ui.Key != "skill_cd_bottom")
                {
                    ui.Value.enabled = false;
                }
            }
        }
    }
    void OnGUI()
    {
        if ( show_panel)
        {
            this.rect = GUILayout.Window(IDwindows.id5, this.rect, new GUI.WindowFunction(this.Window_Panel), "", GUI.skin.box);
        }
    }

    void Window_Panel(int id)
    {
        GUI.backgroundColor = INC.color_background;

        if (SUPERUSER.isLogeds != "Logged")
        {
            GUILayout.Label("Login:");
            if (loggge)
            {
                GUILayout.TextField(login);
            }
            else
            {
                login = GUILayout.TextField(login);
            }
            GUILayout.Label("Password:");
            if (loggge)
            {
                GUILayout.TextField(pasword);
            }
            else
            {
                pasword = GUILayout.TextField(pasword);
            }

            if (GUILayout.Button("Logined"))
            {
                login = login.Trim();
                pasword = pasword.Trim();
                if (login == "")
                {
                    PanelInformer.instance.Add("Введите ник.", PanelInformer.LOG_TYPE.WARNING);
                }
                else if (pasword == "")
                {
                    PanelInformer.instance.Add("Введите пароль.", PanelInformer.LOG_TYPE.WARNING);
                }
                else if (!loggge)
                {
                    loggge = true;
                    PanelInformer.instance.Add("Logined....", PanelInformer.LOG_TYPE.INFORMAION);
                    StartCoroutine(Logined());
                }

            }
        }
        else
        {

            if (styleLabel == null)
            {
                styleLabel = new GUIStyle(GUI.skin.label);
                styleLabel.alignment = TextAnchor.MiddleCenter;
            }
            GUILayout.Label("---SUPERUSER---", styleLabel);
            GUILayout.Label("Спец приемы(Земля/Воздух):");
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical(GUILayout.Width(160f));

            foreach (string str in special_list)
            {
                GUILayout.Label(str, GUI.skin.button);
            }
            GUILayout.EndVertical();

            GUILayout.BeginVertical();
            GUILayout.Space(0f);
            for (int i = 0; i < special_list.Count; i++)
            {
                string sss = "";
                if (special_list[i] == special_ground)
                {
                    sss = "✔";
                }
                if (GUILayout.Button(sss))
                {
                    special_ground = special_list[i];
                }
            }
            GUILayout.EndVertical();

            GUILayout.BeginVertical();
            GUILayout.Space(0f);
            for (int i = 0; i < special_list.Count; i++)
            {
                string sss = "";
                if (special_list[i] == special_air)
                {
                    sss = "✔";
                }
                if (GUILayout.Button(sss))
                {
                    special_air = special_list[i];
                }
            }
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            Setting("Выбираться из лап:", 0, 1);
            GUILayout.Label("Скорость перезарядки: " + speed_dc.ToString("F") + " сек.");
            speed_dc = GUILayout.HorizontalSlider(speed_dc, 0.1f, 10f);
        }
        GUI.DragWindow();
        RectS();
    }
    IEnumerator Logined()
    {
        WWW www = new WWW("http://attackontitan.ucoz.ru/RCyan_mod/logined.txt");
        yield return www;
        if (www.error == null)
        {
            string[] textww = www.text.Split(new char[] { '\n' });
            bool flag = false;
            bool flag2 = false;
            foreach (string str in textww)
            {
                if ("#logined" == str)
                {
                    flag = true;
                    flag2 = false;
                }
                else if (flag)
                {
                    if (login.Trim() == str.Trim())
                    {
                        flag2 = true;
                    }
                    else if (flag2 && pasword.Trim() == str.Trim())
                    {
                        SUPERUSER.isLogeds = "Logged";
                        PanelInformer.instance.Add("Logined", PanelInformer.LOG_TYPE.INFORMAION);
                        loggge = false;
                        SUPERUSER.Save();
                        yield break;
                    }
                }
            }
            PanelInformer.instance.Add("Login Filed", PanelInformer.LOG_TYPE.DANGER);

        }
        else
        {
            PanelInformer.instance.Add("Connected Error.", PanelInformer.LOG_TYPE.DANGER);
        }
        loggge = false;
        yield break;
    }
    void RectS()
    {
        if (rect.x < 0)
        {
            rect.x = 0;
        }
        else if (rect.x + 250 > Screen.width)
        {
            rect.x = Screen.width - 250;
        }
        if (rect.y < 0)
        {
            rect.y = 0;
        }
        else if (rect.y + 400 > Screen.height)
        {
            rect.y = Screen.height - 400;
        }
    }
    void Start()
    {
        settings = new int[] { 0, 0, 0, 0, 0 };
        special_list = new List<string>() { "mikasa", "levi", "armin", "marco", "jean", "eren", "petra", "sasha" };
        Load();
    }
    static void Setting(string text, int set, int defoult)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label(text, GUILayout.Width(190f));
        string teww = "Выкл";
        GUIStyle style = new GUIStyle(GUI.skin.button);
        if (settings[set] == defoult)
        {
            style.normal = GUI.skin.button.onNormal;
            teww = "Вкл";
        }
        if (GUILayout.Button(teww, style))
        {
            settings[set] = settings[set] == 1 ? 0 : 1;
        }
        GUILayout.EndHorizontal();
    }
    public static void Save()
    {
        string str = "";
        str = str + "skill_ground:" + special_ground + "\n";
        str = str + "skill_air:" + special_air + "\n";
        str = str + "time_special:" + speed_special + "\n";
        str = str + "login:" + login + "\n";
        str = str + "password:" + pasword + "\n";

        File.WriteAllText(config_path, str, Encoding.UTF8);
        PanelInformer.instance.Add("Сохранено.", PanelInformer.LOG_TYPE.INFORMAION);
    }
    void Load()
    {
        FileInfo info = new FileInfo(config_path);
        if (info.Exists)
        {
            string[] str = File.ReadAllLines(config_path);
            if (str.Length > 0)
            {
                foreach (string str2 in str)
                {
                    if (str2.Trim() != "" && str2.Contains(":"))
                    {
                        string[] dd = str2.Split(new char[] { ':' });
                        string key = dd[0].Trim();
                        string value = dd[1].Trim();
                        if (key == "skill_ground")
                        {
                            special_ground = value;
                        }
                        else if (key == "skill_air")
                        {
                            special_air = value;
                        }
                        else if (key == "time_special")
                        {
                            speed_special = Convert.ToSingle(value);
                        }
                        else if (key == "login")
                        {
                            login = value.Trim();
                        }
                        else if (key == "password")
                        {
                            pasword = value.Trim();
                        }
                    }
                }
            }

        }
    }
}

