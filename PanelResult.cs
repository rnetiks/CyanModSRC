using System;
using UnityEngine;
using CyanMod;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class PanelResult : MonoBehaviour
{
    public GameObject label_quit;
    bool ShowPanelCyan = true;
    int my_formils;
    Vector3 posPanel;
   Rect CyanPanelRect;
   List<Mystats> inf_players;
   List<my_formuls> formuls;
   int isSorting;
   Vector2 scrollPos;
   bool is_show_form = false;
   bool isSorti = false;
   string TITAL;
    private void OnEnable()
    {
    }
    public void showTxt()
    {
            this.label_quit.GetComponent<UILabel>().text = CyanMod.INC.la("btn_quit");
            UILabel[] gm = base.gameObject.GetComponentsInChildren<UILabel>();
            foreach (UILabel lab in gm)
            {

                lab.color = INC.color_UI;
            }
        
    }
    void Update()
    {
        this.showTxt();
    }
    void to_sort()
    {
        if (isSorting == 0)
        {
            isSorti = !isSorti;
            if (isSorti)
            {
                inf_players = new List<Mystats>((from room in inf_players orderby room.ID descending select room).ToArray<Mystats>());
            }
            else
            {
                inf_players = new List<Mystats>((from room in inf_players orderby room.ID ascending select room).ToArray<Mystats>());
            }
        }
        else if (isSorting == 1)
        {
            isSorti = !isSorti;
            if (isSorti)
            {
                inf_players = new List<Mystats>(inf_players.OrderBy<Mystats, Mystats>(x => x, new FunctionComparer<Mystats>((x, y) => string.Compare(x.player_name.HexDell().Trim(), y.player_name.HexDell().Trim()))));
            }
            else
            {
                inf_players = new List<Mystats>(inf_players.OrderByDescending<Mystats, Mystats>(x => x, new FunctionComparer<Mystats>((x, y) => string.Compare(x.player_name.HexDell().Trim(), y.player_name.HexDell().Trim()))));
            }

        }
        else if (isSorting == 2)
        {
            isSorti = !isSorti;
            if (isSorti)
            {
                inf_players = new List<Mystats>((from room in inf_players orderby room.kills descending select room).ToArray<Mystats>());
            }
            else
            {
                inf_players = new List<Mystats>((from room in inf_players orderby room.kills ascending select room).ToArray<Mystats>());
            }
        }
        else if (isSorting == 3)
        {
            isSorti = !isSorti;
            if (isSorti)
            {
                inf_players = new List<Mystats>((from room in inf_players orderby room.death descending select room).ToArray<Mystats>());
            }
            else
            {
                inf_players = new List<Mystats>((from room in inf_players orderby room.death ascending select room).ToArray<Mystats>());
            }
        }
        else if (isSorting == 4)
        {
            isSorti = !isSorti;
            if (isSorti)
            {
                inf_players = new List<Mystats>((from room in inf_players orderby room.max_dmg descending select room).ToArray<Mystats>());
            }
            else
            {
                inf_players = new List<Mystats>((from room in inf_players orderby room.max_dmg ascending select room).ToArray<Mystats>());
            }
        }
        else if (isSorting == 5)
        {
            isSorti = !isSorti;
            if (isSorti)
            {
                inf_players = new List<Mystats>((from room in inf_players orderby room.total descending select room).ToArray<Mystats>());
            }
            else
            {
                inf_players = new List<Mystats>((from room in inf_players orderby room.total ascending select room).ToArray<Mystats>());
            }
        }
        else if (isSorting == 6)
        {
            isSorti = !isSorti;
            if (isSorti)
            {
                inf_players = new List<Mystats>((from room in inf_players orderby room.formuls descending select room).ToArray<Mystats>());
            }
            else
            {
                inf_players = new List<Mystats>((from room in inf_players orderby room.formuls ascending select room).ToArray<Mystats>());
            }
        }
    }
    void content(int num, int sotr, string text, string tital,bool islocal,float width = 0f)
    {
        if (width != 0)
        {
            GUILayout.BeginVertical(GUILayout.Width(width));
        }
        else
        {
            GUILayout.BeginVertical();
            GUILayout.Space(0f);
        }
        if (num == 0)
        {
            if (GUILayout.Button((isSorting == sotr ? (isSorti ? "▼" : "▲") : "") + tital, GUILayout.Height(23f)))
            {
                if (isSorting != sotr)
                {
                    isSorti = false;
                }
                isSorting = sotr;
                to_sort();
            }
        }
        string str = text;
        if (islocal)
        {
            str = "<b>" + str + "</b>";
        }
        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.alignment = TextAnchor.MiddleCenter;
        GUILayout.Label(str, style);
        GUILayout.EndVertical();
    }
    void OnGUI()
    {
        if (!ShowPanelCyan)
        {
            GUI.backgroundColor = INC.gui_color;
            if (GUI.Button(new Rect(5, 5, 140, 22), INC.la("to_cyan_panel_server_list")))
            {
                ShowPanel();
            }
        }
        else
        {
            GUI.backgroundColor = INC.gui_color;
            if (GUI.Button(new Rect(5, 5, 140, 22), INC.la("to_defoult_panel_server_list")))
            {
                HidePanel();
            }
            GUILayout.BeginArea(CyanPanelRect, GUI.skin.box);
            GUIStyle style_tital = new GUIStyle(GUI.skin.label);
            style_tital.fontSize = GUI.skin.label.fontSize + 3;
            GUILayout.Label(TITAL, style_tital);
            scrollPos = GUILayout.BeginScrollView(scrollPos);
            for (int i = 0; i < inf_players.Count; i++)
            {
                Mystats player = inf_players[i];
                GUILayout.BeginHorizontal();
                content(i, 0, player.ID.ToString(), "ID:", player.local, 50f);
                content(i, 1, player.player_name, INC.la("nick_name_player"), player.local, 200f);
                content(i, 2, player.kills.ToString(), INC.la("kills_player_pr"), player.local);
                content(i, 3, player.death.ToString(), INC.la("death_player_pr"), player.local);
                content(i, 4, player.max_dmg.ToString(), INC.la("max_d_player_pr"), player.local);
                content(i, 5, player.total.ToString(), INC.la("total_d_player_pr"), player.local);
                content(i, 6, (player.formuls = cext.calculate_formuls(my_formils, player.kills, player.death, player.max_dmg, player.total)).ToString("F"), "F:" + my_formils.ToString() + " " + INC.la("formuls_d_player_pr"), player.local);
                GUILayout.EndHorizontal();
            }
            GUILayout.EndScrollView();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button(INC.la("quit"), GUILayout.Width(120f), GUILayout.Height(30f)))
            {
                PhotonNetwork.Disconnect();
                Screen.lockCursor = false;
                Screen.showCursor = true;
                IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.STOP;
                FengGameManagerMKII.instance.gameStart = false;
                 FengGameManagerMKII.instance.MenuOn = false;
                UnityEngine.Object.Destroy(CyanMod.CachingsGM.Find("MultiplayerManager"));
                Application.LoadLevel("menu");
            }
            if (!is_show_form)
            {
                if (GUILayout.Button("Formula: " + my_formils + " >>", GUILayout.Width(130f), GUILayout.Height(30f)))
                {
                    is_show_form = true;
                }
            }
            else
            {
                if (GUILayout.Button("<<", GUILayout.Width(30f), GUILayout.Height(30f)))
                {
                    is_show_form = false;
                }
                if (formuls == null)
                {
                    formuls = new List<my_formuls>();
                    formuls.Add(new my_formuls("F1", "F-0"));
                    formuls.Add(new my_formuls("F2", "F-1"));
                    formuls.Add(new my_formuls("F3", "F-2"));
                    formuls.Add(new my_formuls("F4", "F-3"));
                }

                Vector2 vector = new Vector2(CyanPanelRect.x + 20f, CyanPanelRect.y + 300f);
                if (GUILayout.Button(formuls[0].name_formuls, GUILayout.Width(40f), GUILayout.Height(30f)))
                {
                    my_formils = 0;
                    is_show_form = false;
                }
                ToolTip.tip(vector, formuls[0].textured);

                if (GUILayout.Button(formuls[1].name_formuls, GUILayout.Width(40f), GUILayout.Height(30f)))
                {
                    my_formils = 1;
                    is_show_form = false;
                }
                ToolTip.tip( vector, formuls[1].textured);

                if (GUILayout.Button(formuls[2].name_formuls, GUILayout.Width(40f), GUILayout.Height(30f)))
                {
                    my_formils = 2;
                    is_show_form = false;
                }
                ToolTip.tip( vector, formuls[2].textured);

                if (GUILayout.Button(formuls[3].name_formuls, GUILayout.Width(40f), GUILayout.Height(30f)))
                {
                    my_formils = 3;
                    is_show_form = false;
                }

                ToolTip.tip( vector, formuls[3].textured);

            }
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
    }

    bool ShowPanel()
    {
        base.gameObject.transform.localPosition = new Vector3(0, 9999, 0);
        ShowPanelCyan = true;
        PrefersCyan.SetInt("int_result_cyan_panel",1);
        PrefersCyan.Save();
        return true;
    }
    bool HidePanel()
    {
        base.gameObject.transform.localPosition = posPanel;
        ShowPanelCyan = false;
        PrefersCyan.SetInt("int_result_cyan_panel", 0);
        PrefersCyan.Save();
        return true;
    }
    void Start()
    {
        TITAL = FengGameManagerMKII.level;
        TITAL = TITAL + " " + IN_GAME_MAIN_CAMERA.difficulty.ToString();
        TITAL = TITAL + " " + IN_GAME_MAIN_CAMERA.dayLight;
        TITAL = TITAL + "\n" + FengGameManagerMKII.instance.tital_text();
        posPanel = base.gameObject.transform.localPosition;
        ShowPanelCyan = (PrefersCyan.GetInt("int_result_cyan_panel", 1) == 1);
        CyanPanelRect = new Rect(Screen.width / 2 - 440, Screen.height / 2 - 250, 880, 500);
        my_formils = PrefersCyan.GetInt("int_my_formuls", 1);
        inf_players = new List<Mystats>();
        foreach (PhotonPlayer player in PhotonNetwork.playerList)
        {
            inf_players.Add(new Mystats(player.kills, player.deaths, player.max_dmg, player.total_dmg, player.ishexname, player.ID,player.isLocal));
        }
        isSorting = 1;
        to_sort();
        if (ShowPanelCyan)
        {
            ShowPanel();
        }
        else
        {
            HidePanel();
        }
    }
    public class my_formuls
    {
        public string name_formuls;
        public Texture2D textured;
        public my_formuls(string path_name, string name)
        {
            name_formuls = name;
            textured = cext.loadResTexture(path_name);
        }
    }
    public class Mystats
    {
        public readonly int kills;
        public readonly int death;
        public readonly int max_dmg;
        public readonly int total;
        public readonly string player_name;
        public readonly int ID;
        public readonly bool local;
        public float formuls;
        public Mystats(int k, int d, int m, int t, string n, int id,bool l)
        {
            kills = k;
            death = d;
            max_dmg = m;
            total = t;
            player_name = n;
            ID = id;
            local = l;
            formuls =0f;
        }
    }
}

