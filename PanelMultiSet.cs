using System;
using UnityEngine;
using CyanMod;
using System.Linq;
using System.Collections.Generic;

public class PanelMultiSet : MonoBehaviour
{
    public GameObject label_ab;
    public GameObject label_BACK;
    public GameObject label_choose_map;
    public GameObject label_difficulty;
    public GameObject label_game_time;
    public GameObject label_hard;
    public GameObject label_max_player;
    public GameObject label_max_time;
    public GameObject label_normal;
    public GameObject label_server_name;
    public GameObject label_START;
    public static Vector3 pos_gm;
    string[] color = new string[] { "00FFFF", "00FF00", "FF0000", "FF00FF", "FFFF00", "FFFFFF", "000000" };
    bool isSetings = false;
    List<LevelInfo> levels;
    LevelInfo current_lvl;
    Rect CyanPanelRect;
    public static PanelMultiSet instance;
    void Awake()
    {
        instance = this;
    }
    void OnDestroy()
    {
        instance = null;
    }
    private void OnEnable()
    {
    }
    Vector2 pos;
    public void showTxt()
    {
            this.label_START.GetComponent<UILabel>().text = INC.la("btn_start");
            this.label_BACK.GetComponent<UILabel>().text =INC.la("btn_back");
            this.label_choose_map.GetComponent<UILabel>().text =INC.la("choose_map");
            this.label_server_name.GetComponent<UILabel>().text =INC.la("server_name");
            this.label_max_player.GetComponent<UILabel>().text = INC.la("max_player");
            this.label_max_time.GetComponent<UILabel>().text =INC.la("max_Time");
            this.label_game_time.GetComponent<UILabel>().text = INC.la("game_time");
            this.label_difficulty.GetComponent<UILabel>().text =INC.la("difficulty");
            this.label_normal.GetComponent<UILabel>().text = INC.la("ui_normal");
            this.label_hard.GetComponent<UILabel>().text = INC.la("ui_hard");
            this.label_ab.GetComponent<UILabel>().text = INC.la("ui_abnormal");
            UILabel[] gm = base.gameObject.GetComponentsInChildren<UILabel>();
            foreach (UILabel lab in gm)
            {
                lab.color = INC.color_UI;
            }
    }
    Vector2 rectPos_2;
    void setting_serv()
    {
        rectPos_2 = GUILayout.BeginScrollView(rectPos_2);
        GUILayout.Box(INC.la("titans_settings"));
        GUILayout.Label(INC.la("ms_only"));
        float labelwidth = 50;
        float textFiled = 80;
        GUICyan.OnToogleCyan(INC.la("custom_titan_number"), 203, 1, 0, labelwidth);
        if ((int)FengGameManagerMKII.settings[203] == 1)
        {
            GUICyan.OnTextFileCyan(INC.la("amount_integer"), 204, textFiled);
        }
        GUICyan.OnToogleCyan(INC.la("custom_titan_spawns"), 210, 1, 0, labelwidth);
        if ((int)FengGameManagerMKII.settings[210] == 1)
        {
            float t_normal = 0f;
            if (Single.TryParse((string)FengGameManagerMKII.settings[211], out t_normal))
            {
                t_normal = Convert.ToSingle((string)FengGameManagerMKII.settings[211]);
            }
            float t_aberrant = 0f;
            if (Single.TryParse((string)FengGameManagerMKII.settings[212], out t_aberrant))
            {
                t_aberrant = Convert.ToSingle((string)FengGameManagerMKII.settings[212]);
            }
            float t_jumper = 0f;
            if (Single.TryParse((string)FengGameManagerMKII.settings[213], out t_jumper))
            {
                t_jumper = Convert.ToSingle((string)FengGameManagerMKII.settings[213]);
            }
            float t_crawler = 0f;
            if (Single.TryParse((string)FengGameManagerMKII.settings[214], out t_crawler))
            {
                t_crawler = Convert.ToSingle((string)FengGameManagerMKII.settings[214]);
            }
            float t_punk = 0f;
            if (Single.TryParse((string)FengGameManagerMKII.settings[215], out t_punk))
            {
                t_punk = Convert.ToSingle((string)FengGameManagerMKII.settings[215]);
            }

            GUILayout.BeginHorizontal();
            GUILayout.Label(INC.la("t_nornal") + " " + t_normal.ToString() + "%.", GUILayout.Width(labelwidth));
            if (GUILayout.Button("X"))
            {
                t_normal = 0;
            }
            GUILayout.EndHorizontal();
            t_normal = GUILayout.HorizontalSlider(t_normal, 0,FengGameManagerMKII.instance.titanspawners(0));

            GUILayout.BeginHorizontal();
            GUILayout.Label(INC.la("t_abnormal") + " " + t_aberrant.ToString() + "%.", GUILayout.Width(labelwidth));
            if (GUILayout.Button("X"))
            {
                t_aberrant = 0;
            }
            GUILayout.EndHorizontal();
            t_aberrant = GUILayout.HorizontalSlider(t_aberrant, 0, FengGameManagerMKII.instance.titanspawners(1));

            GUILayout.BeginHorizontal();
            GUILayout.Label(INC.la("t_jumper") + " " + t_jumper.ToString() + "%.", GUILayout.Width(labelwidth));
            if (GUILayout.Button("X"))
            {
                t_jumper = 0;
            }
            GUILayout.EndHorizontal();
            t_jumper = GUILayout.HorizontalSlider(t_jumper, 0, FengGameManagerMKII.instance.titanspawners(2));

            GUILayout.BeginHorizontal();
            GUILayout.Label(INC.la("t_crawler") + " " + t_crawler.ToString() + "%.", GUILayout.Width(labelwidth));
            if (GUILayout.Button("X"))
            {
                t_crawler = 0;
            }
            GUILayout.EndHorizontal();
            t_crawler = GUILayout.HorizontalSlider(t_crawler, 0, FengGameManagerMKII.instance.titanspawners(3));

            GUILayout.BeginHorizontal();
            GUILayout.Label(INC.la("t_punk") + " " + t_punk.ToString() + "%.", GUILayout.Width(labelwidth));
            if (GUILayout.Button("X"))
            {
                t_punk = 0;
            }
            GUILayout.EndHorizontal();
            t_punk = GUILayout.HorizontalSlider(t_punk, 0, FengGameManagerMKII.instance.titanspawners(4));


            FengGameManagerMKII.settings[211] = Math.Round(t_normal).ToString();
            FengGameManagerMKII.settings[212] = Math.Round(t_aberrant).ToString();
            FengGameManagerMKII.settings[213] = Math.Round(t_jumper).ToString();
            FengGameManagerMKII.settings[214] = Math.Round(t_crawler).ToString();
            FengGameManagerMKII.settings[215] = Math.Round(t_punk).ToString();
        }
        GUICyan.OnToogleCyan(INC.la("titan_size_mode"), 207, 1, 0, labelwidth);
        if ((int)FengGameManagerMKII.settings[207] == 1)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(INC.la("minimum"));
            FengGameManagerMKII.settings[208] = GUILayout.TextField((string)FengGameManagerMKII.settings[208], GUILayout.Width(60f));
            GUILayout.Label(INC.la("maximum"));
            FengGameManagerMKII.settings[209] = GUILayout.TextField((string)FengGameManagerMKII.settings[209], GUILayout.Width(60f));
            GUILayout.EndHorizontal();
        }

        GUILayout.Label(INC.la("menu_titan_health"));
        string[] texts = new string[] { INC.la("off"), INC.la("fixed"), INC.la("scaled") };
        FengGameManagerMKII.settings[197] = GUILayout.SelectionGrid((int)FengGameManagerMKII.settings[197], texts, 2);

        if ((int)FengGameManagerMKII.settings[197] != 0)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(INC.la("minimum"));
            FengGameManagerMKII.settings[198] = GUILayout.TextField((string)FengGameManagerMKII.settings[198], GUILayout.Width(60f));
            GUILayout.Label(INC.la("maximum"));
            FengGameManagerMKII.settings[199] = GUILayout.TextField((string)FengGameManagerMKII.settings[199], GUILayout.Width(60f));
            GUILayout.EndHorizontal();
        }

        GUICyan.OnToogleCyan(INC.la("titan_damage_mode"), 205, 1, 0, labelwidth);
        if ((int)FengGameManagerMKII.settings[205] != 0)
        {
            GUICyan.OnTextFileCyan(INC.la("amount_integer"), 206, textFiled);
        }
        GUICyan.OnToogleCyan(INC.la("men_titan_explode"), 195, 1, 0, labelwidth);
        if ((int)FengGameManagerMKII.settings[195] != 0)
        {
            GUICyan.OnTextFileCyan(INC.la("radius_int"), 196, textFiled);
        }
        GUICyan.OnToogleCyan(INC.la("disable_rock_throwing"), 194, 1, 0, labelwidth);
        GUILayout.Box(INC.la("pvp_settings"));
        GUICyan.OnToogleCyan(INC.la("point_mode"), 226, 1, 0, labelwidth);
        if ((int)FengGameManagerMKII.settings[226] != 0)
        {
            GUICyan.OnTextFileCyan(INC.la("max_points_int"), 227, textFiled);
        }
        GUICyan.OnToogleCyan(INC.la("pvp_bomb_mode"), 192, 1, 0, labelwidth);
        GUILayout.Label(INC.la("team_mode"));
        string[] texts12 = new string[] { INC.la("off"), INC.la("no_sort"), INC.la("size_lock"), INC.la("skill_lock") };
        FengGameManagerMKII.settings[193] = GUILayout.SelectionGrid((int)FengGameManagerMKII.settings[193], texts12, 2);
        GUICyan.OnToogleCyan(INC.la("infection_mode"), 200, 1, 0, labelwidth);
        if ((int)FengGameManagerMKII.settings[200] != 0)
        {
            GUICyan.OnTextFileCyan(INC.la("starting_titans_int"), 201, textFiled);
        }
        GUICyan.OnToogleCyan(INC.la("friendly_mode"), 219, 1, 0, labelwidth);
        GUILayout.Label(INC.la("sword_ahss_pvp"));
        string[] texts121 = new string[] { INC.la("off"), INC.la("teams"), INC.la("ffa") };
        FengGameManagerMKII.settings[220] = GUILayout.SelectionGrid((int)FengGameManagerMKII.settings[220], texts121, 3);
        GUICyan.OnToogleCyan(INC.la("no_ahss_air_reloading"), 228, 1, 0, labelwidth);
        GUICyan.OnToogleCyan(INC.la("cannons_kill_humans"), 261, 1, 0, labelwidth);
        GUILayout.Box(INC.la("other_settings"));
        GUILayout.Label(INC.la("message_of_the_day"));
        FengGameManagerMKII.settings[337] = GUILayout.TextField((string)FengGameManagerMKII.settings[337]);
        GUICyan.OnToogleCyan(INC.la("custom_titans_wave"), 217, 1, 0, labelwidth);
        if ((int)FengGameManagerMKII.settings[217] != 0)
        {
            GUICyan.OnTextFileCyan(INC.la("amount_integer"), 218, textFiled);
        }
        GUICyan.OnToogleCyan(INC.la("maximum_waves"), 221, 1, 0, labelwidth);
        if ((int)FengGameManagerMKII.settings[221] != 0)
        {
            GUICyan.OnTextFileCyan(INC.la("amount_integer"), 222, textFiled);
        }
        GUICyan.OnToogleCyan(INC.la("punks_every_5_waves"), 229, 1, 0, labelwidth);
        GUICyan.OnToogleCyan(INC.la("global_minimap_isable"), 235, 1, 0, labelwidth);
        GUICyan.OnToogleCyan(INC.la("endless_respawn"), 223, 1, 0, labelwidth);
        if ((int)FengGameManagerMKII.settings[223] != 0)
        {
            GUICyan.OnTextFileCyan(INC.la("respawn_time_integer"), 224, textFiled);
        }
        GUICyan.OnToogleCyan(INC.la("kick_eren_titan"), 202, 1, 0, labelwidth);
        GUICyan.OnToogleCyan(INC.la("allow_horses"), 216, 1, 0, labelwidth);
        GUIStyle colored = new GUIStyle(GUI.skin.box);
        Color texture = new Color((float)FengGameManagerMKII.settings[246], (float)FengGameManagerMKII.settings[247], (float)FengGameManagerMKII.settings[248], (float)FengGameManagerMKII.settings[249]);
        colored.normal.textColor = texture;
        GUILayout.Box(INC.la("bomb_mode_m"), colored);
        GUILayout.BeginHorizontal();
        GUILayout.Label("R:", GUILayout.Width(30f));
        FengGameManagerMKII.settings[246] = GUILayout.HorizontalSlider((float)FengGameManagerMKII.settings[246], 0f, 1f);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("G:", GUILayout.Width(30f));
        FengGameManagerMKII.settings[247] = GUILayout.HorizontalSlider((float)FengGameManagerMKII.settings[247], 0f, 1f);
        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal();
        GUILayout.Label("B:", GUILayout.Width(30f));
        FengGameManagerMKII.settings[248] = GUILayout.HorizontalSlider((float)FengGameManagerMKII.settings[248], 0f, 1f);
        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal();
        GUILayout.Label("A:", GUILayout.Width(30f));
        FengGameManagerMKII.settings[249] = GUILayout.HorizontalSlider((float)FengGameManagerMKII.settings[249], 0.3f, 1f);
        GUILayout.EndHorizontal();

        int num31 = 20 - (int)FengGameManagerMKII.settings[250] - (int)FengGameManagerMKII.settings[251] - (int)FengGameManagerMKII.settings[252] - (int)FengGameManagerMKII.settings[253];

        GUILayout.BeginHorizontal();
        GUILayout.Label(INC.la("bomb_radius"), GUILayout.Width(120f));
        GUILayout.Label(((int)FengGameManagerMKII.settings[250]).ToString());
        if (GUILayout.Button("-"))
        {
            if ((int)FengGameManagerMKII.settings[250] > 0)
            {
                FengGameManagerMKII.settings[250] = (int)FengGameManagerMKII.settings[250] - 1;
            }
        }
        else if (GUILayout.Button("+") && (int)FengGameManagerMKII.settings[250] < 10 && num31 > 0)
        {
            FengGameManagerMKII.settings[250] = (int)FengGameManagerMKII.settings[250] + 1;
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label(INC.la("bomb_range"), GUILayout.Width(120f));
        GUILayout.Label(((int)FengGameManagerMKII.settings[251]).ToString());
        if (GUILayout.Button("-"))
        {
            if ((int)FengGameManagerMKII.settings[251] > 0)
            {
                FengGameManagerMKII.settings[251] = (int)FengGameManagerMKII.settings[251] - 1;
            }
        }
        else if (GUILayout.Button("+") && (int)FengGameManagerMKII.settings[251] < 10 && num31 > 0)
        {
            FengGameManagerMKII.settings[251] = (int)FengGameManagerMKII.settings[251] + 1;
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label(INC.la("bomb_speed"), GUILayout.Width(120f));
        GUILayout.Label(((int)FengGameManagerMKII.settings[252]).ToString());
        if (GUILayout.Button("-"))
        {
            if ((int)FengGameManagerMKII.settings[252] > 0)
            {
                FengGameManagerMKII.settings[252] = (int)FengGameManagerMKII.settings[252] - 1;
            }
        }
        else if (GUILayout.Button("+") && (int)FengGameManagerMKII.settings[252] < 10 && num31 > 0)
        {
            FengGameManagerMKII.settings[252] = (int)FengGameManagerMKII.settings[252] + 1;
        }
        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal();
        GUILayout.Label(INC.la("bomb_cd"), GUILayout.Width(120f));
        GUILayout.Label(((int)FengGameManagerMKII.settings[253]).ToString());
        if (GUILayout.Button("-"))
        {
            if ((int)FengGameManagerMKII.settings[253] > 0)
            {
                FengGameManagerMKII.settings[253] = (int)FengGameManagerMKII.settings[253] - 1;
            }
        }
        else if (GUILayout.Button("+") && (int)FengGameManagerMKII.settings[253] < 10 && num31 > 0)
        {
            FengGameManagerMKII.settings[253] = (int)FengGameManagerMKII.settings[253] + 1;
        }
        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal();
        GUILayout.Label(INC.la("unused_points"), GUILayout.Width(120f));
        GUILayout.Label(num31.ToString());
        GUILayout.EndHorizontal();
        GUILayout.EndScrollView();
    }
    void panel_gui()
    {
        GUILayout.BeginArea(CyanPanelRect, GUI.skin.box);
        GUILayout.BeginHorizontal();

        GUILayout.BeginVertical( GUILayout.Width(260f));
        GUIStyle style = new GUIStyle(GUI.skin.button);
        GUILayout.Label(INC.la("lvls_multy"));
        pos = GUILayout.BeginScrollView(pos);
        foreach (LevelInfo lvl in levels)
        {
            if (!lvl.name.StartsWith("[S]") && lvl.name != "Cage Fighting")
            {
                string str =  lvl.name;
                if (current_lvl == lvl)
                {
                    style.normal = GUI.skin.button.onNormal;
                    str = "[ " + str + " ]";
                }
                else
                {
                    style.normal = GUI.skin.button.normal;
                }
                if (GUILayout.Button(str, style))
                {
                    current_lvl = lvl;
                    FengGameManagerMKII.settings[344] = lvl.name;
                }
            }
        }
        GUILayout.EndScrollView();
        GUILayout.EndVertical();

        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical( GUILayout.Width(200f));
        GUILayout.Label(INC.la("diff_multy"));
        GUIStyle style5 = new GUIStyle(GUI.skin.button);
     
        string[] sss = new string[] { INC.la("normal_diff_multy"), INC.la("hard_diff_multy"), INC.la("abnormal_diff_multy") };
        for (int i = 0; i < sss.Length; i++)
        {
            string ssn = sss[i];
            if ((int)FengGameManagerMKII.settings[345] == i)
            {
                style5.normal = GUI.skin.button.onNormal;
                ssn = "[ " + ssn + " ]";
            }
            else
            {
                style5.normal = GUI.skin.button.normal;
            }
            if (GUILayout.Button(ssn, style5))
            {
                FengGameManagerMKII.settings[345] = i;
            }
        }
        
        GUILayout.Label(INC.la("day_time_multy"));
        GUIStyle style4 = new GUIStyle(GUI.skin.button);
       
        string[] ssf = new string[] { INC.la("day_multy"), INC.la("dawn_multy"), INC.la("night_multy") };
        for (int i = 0; i < ssf.Length; i++)
        {
            string ssn = ssf[i];
            if( (int)FengGameManagerMKII.settings[346] == i)
            {
                style4.normal = GUI.skin.button.onNormal;
                ssn = "[ " + ssn + " ]"; 
            }
            else
            {
                style4.normal = GUI.skin.button.normal;
            }
            if (GUILayout.Button(ssn, style4))
            {
                FengGameManagerMKII.settings[346] = i;
            }
        }
        GUILayout.EndVertical();
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        if (!isSetings)
        {
            GUILayout.Label(INC.la("name_multy"),GUILayout.Width(200f));
            if (GUILayout.Button(INC.la("settings_multy")))
            {
                isSetings = true;
            }
        }
        else
        {
            if (GUILayout.Button(INC.la("back"),GUILayout.Width(120f)))
            {
                isSetings = false;
            }
        }
        GUILayout.EndHorizontal();
        if (!isSetings)
        {
            FengGameManagerMKII.settings[347] = GUILayout.TextField((string)FengGameManagerMKII.settings[347]);
            GUILayout.Label(INC.la("password_multy"));
            FengGameManagerMKII.settings[348] = GUILayout.TextField((string)FengGameManagerMKII.settings[348]);
            GUILayout.Label(INC.la("max_time_multy"));
            FengGameManagerMKII.settings[349] = GUILayout.TextField((string)FengGameManagerMKII.settings[349]);
            GUILayout.Label(INC.la("max_player_multy"));
            FengGameManagerMKII.settings[350] = GUILayout.TextField((string)FengGameManagerMKII.settings[350]);
            GUILayout.Label(INC.la("color_multy"));
            FengGameManagerMKII.settings[351] = GUILayout.TextField((string)FengGameManagerMKII.settings[351]);
            GUILayout.BeginHorizontal();
            for (int i = 0; i < color.Length; i++)
            {
                string col = color[i];
                if (GUILayout.Button("<color=#" + col + "><b>o</b></color>", GUILayout.Width(30f)))
                {
                    FengGameManagerMKII.settings[351] = "[" + col + "]";
                }
            }
            GUILayout.EndHorizontal();
        }
        else
        {
            setting_serv();
        }
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
        GUIStyle styl2 = new GUIStyle(GUI.skin.label);
        styl2.fontSize = GUI.skin.label.fontSize + 3;
        GUILayout.Label(INC.la("map_multy") + current_lvl.name + "\n" + INC.la("game_mode_multy") + current_lvl.type + "\n" + INC.la("desc_multy") + current_lvl.desc, styl2);
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button(INC.la("creat_multy"), GUILayout.Width(140f), GUILayout.Height(30f)))
        {
            string room_name = (string)FengGameManagerMKII.settings[347];
            if (room_name.Trim() == string.Empty)
            {
                room_name = "Cyan_mod_Server";
            }
            room_name = FengGameManagerMKII.settings[351] + room_name;
            string day_time = "day";
            if ((int)FengGameManagerMKII.settings[346] == 1)
            {
                day_time = "dawn";
            }
            else if ((int)FengGameManagerMKII.settings[346] == 2)
            {
                day_time = "night";
            }
            string diff = "normal";
            if((int)FengGameManagerMKII.settings[345] == 1)
            {
                diff = "hard";
            }
            else if((int)FengGameManagerMKII.settings[345] == 2)
            {
                diff = "abnormal";
            }
            int max_time = 10;
            if (int.TryParse((string)FengGameManagerMKII.settings[349], out max_time))
            {
                max_time = Convert.ToInt32((string)FengGameManagerMKII.settings[349]);
            }
            int max_players = 10;
            if (int.TryParse((string)FengGameManagerMKII.settings[350], out max_players))
            {
                max_players = Convert.ToInt32((string)FengGameManagerMKII.settings[350]);
            }
            string unencrypted = (string)FengGameManagerMKII.settings[348];
            if (unencrypted.Trim() != "")
            {
                unencrypted = new SimpleAES().Encrypt(unencrypted);
            }
            PhotonNetwork.CreateRoom(room_name + "`" + current_lvl.name + "`" + diff + "`" + max_time + "`" + day_time + "`" + unencrypted + "`" + UnityEngine.Random.Range(0, 0xc350) , true , true , max_players);
            FengGameManagerMKII.instance.saves();
        }
        GUILayout.Label("");
        if (GUILayout.Button(INC.la("back"), GUILayout.Width(140f), GUILayout.Height(30f)))
        {
            NGUITools.SetActive(base.gameObject, false);
            NGUITools.SetActive(UIMainReferences.instance.panelMain, true);
            PhotonNetwork.offlineMode = false;
            PhotonNetwork.Disconnect();
        }
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
    void OnGUI()
    {
        GUI.backgroundColor = INC.gui_color;
        if ((int)FengGameManagerMKII.settings[343] == 1)
        {
            if (GUI.Button(new Rect(5, 5, 140, 22), INC.la("to_defoult_panel_server_list")))
            {
                base.gameObject.transform.localPosition = pos_gm;
                FengGameManagerMKII.settings[343] = 0;
                FengGameManagerMKII.instance.saves();
            }
            panel_gui();
        }
        else
        {
            if (GUI.Button(new Rect(5, 5, 140, 22), INC.la("to_cyan_panel_server_list")))
            {
                base.gameObject.transform.localPosition = new Vector3(0, 9999, 0);
                FengGameManagerMKII.settings[343] = 1;
                FengGameManagerMKII.instance.saves();
            }
        }
    }
    private void Update()
    {
        this.showTxt();
    }
    private void Start()
    {
            pos_gm = base.gameObject.transform.localPosition;
        if ((int)FengGameManagerMKII.settings[343] == 1)
        {
            base.gameObject.transform.localPosition = new Vector3(0, 9999, 0);
        }
        CyanPanelRect = new Rect(Screen.width / 2 - 440, Screen.height / 2 - 250, 880, 500);
        current_lvl = LevelInfo.getInfo((string)FengGameManagerMKII.settings[344]);
        if (current_lvl == null)
        {
            current_lvl = LevelInfo.levels[0];
        }
        levels = LevelInfo.levels.ToList();
        this.levels = new List<LevelInfo>(this.levels.OrderBy<LevelInfo, LevelInfo>(x => x, new FunctionComparer<LevelInfo>((x, y) => string.Compare(x.name, y.name))));
    }
}

