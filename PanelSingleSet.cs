using System;
using UnityEngine;
using CyanMod;
using System.Collections.Generic;

public class PanelSingleSet : MonoBehaviour
{
    public GameObject label_ab;
    public GameObject label_BACK;
    public GameObject label_camera;
    public GameObject label_choose_character;
    public GameObject label_choose_map;
    public GameObject label_difficulty;
    public GameObject label_hard;
    public GameObject label_normal;
    public GameObject label_original;
    public GameObject label_START;
    public GameObject label_tps;
    public GameObject label_wow;
    bool ShowPanelCyan;
    Vector3 posPanel;
    Vector2 scroolPos;
    static LevelInfo curentLVL;
    List<LevelInfo> lvels;
    Vector2 scroolPos1;
   
    public static PanelSingleSet instance;
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
    bool ShowPanel()
    {
        base.gameObject.transform.localPosition = new Vector3(0, 9999, 0);
        FengGameManagerMKII.settings[365] = 0;
        ShowPanelCyan = true;
        PrefersCyan.Save();
        return true;
    }
    bool HidePanel()
    {
        base.gameObject.transform.localPosition = posPanel;
        FengGameManagerMKII.settings[365] = 1;
        ShowPanelCyan = false;
        PrefersCyan.Save();
        return true;
    }
    public void Saving()
    {
        FengGameManagerMKII.settings[410] = curentLVL.name;
                IN_GAME_MAIN_CAMERA.dayLight = (DayLight)(int)FengGameManagerMKII.settings[409];
        FengGameManagerMKII.settings[411] = (int)IN_GAME_MAIN_CAMERA.difficulty;
        FengGameManagerMKII.settings[412] = CheckBoxCostume.costumeSet;
    }
    void Start()
    {
        this.showTxt();
        CheckBoxCostume.costumeSet = (int)FengGameManagerMKII.settings[412];
        int num = !CyanMod.CachingsGM.Find("CheckboxHard").GetComponent<UICheckbox>().isChecked ? (!CyanMod.CachingsGM.Find("CheckboxAbnormal").GetComponent<UICheckbox>().isChecked ? 0 : 2) : 1;
       
        IN_GAME_MAIN_CAMERA.difficulty = (DIFFICULTY)FengGameManagerMKII.settings[411];
        if (IN_GAME_MAIN_CAMERA.difficulty == DIFFICULTY.ABNORMAL)
        {
            CyanMod.CachingsGM.Find("CheckboxAbnormal").GetComponent<UICheckbox>().isChecked = true;
        }
        else if (IN_GAME_MAIN_CAMERA.difficulty == DIFFICULTY.HARD)
        {
            CyanMod.CachingsGM.Find("CheckboxHard").GetComponent<UICheckbox>().isChecked = true;
        }
        else if (IN_GAME_MAIN_CAMERA.difficulty == DIFFICULTY.NORMAL)
        {
            CyanMod.CachingsGM.Find("CheckboxNormal").GetComponent<UICheckbox>().isChecked = true;
        }
        HeroStat.initDATA();
        CyanMod.CachingsGM.Find("PopupListCharacter").GetComponent<UIPopupList>().selection = IN_GAME_MAIN_CAMERA.singleCharacter;
        LevelInfo.initData2();
        lvels = new List<LevelInfo>();
        foreach (LevelInfo info in LevelInfo.levels)
        {
            if (info != null && info.name.StartsWith("[S]"))
            {
                lvels.Add(info);
            }
        }
        if (curentLVL == null)
        {
            curentLVL = LevelInfo.getInfo((string)FengGameManagerMKII.settings[410]);
        }
        GameObject poplist = CyanMod.CachingsGM.Find("PopupListMap");
        if (poplist != null)
        {
            UIPopupList pl = poplist.GetComponent<UIPopupList>();
            if (pl != null && !pl.items.Contains("[S]The Forest IV - LAVA"))
            {
                pl.items.Add("[S]The Forest IV - LAVA");
            }
            pl.selection = curentLVL.name;
        }
        posPanel = base.gameObject.transform.localPosition;
        if ((int)FengGameManagerMKII.settings[365] == 0)
        {
            ShowPanel();
        }
        else
        {
            HidePanel();
        }
    }
    void OnGUI()
    {
        GUI.backgroundColor = INC.gui_color;
        if (!ShowPanelCyan)
        {
            if (GUI.Button(new Rect(5, 5, 140, 22), INC.la("to_cyan_panel_server_list")))
            {
                ShowPanel();
            }
        }
        else
        {
            if (GUI.Button(new Rect(5, 5, 140, 22), INC.la("to_defoult_panel_server_list")))
            {
                HidePanel();
            }
            GUILayout.BeginArea(new Rect(Screen.width / 2 - 440, Screen.height / 2 - 250, 880, 500), GUI.skin.box);
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical(GUILayout.Width(250f));
            GUILayout.Label(INC.la("choose_map"));
            scroolPos = GUILayout.BeginScrollView(scroolPos);
            GUIStyle style = new GUIStyle(GUI.skin.button);
            style.alignment = TextAnchor.MiddleLeft;
            foreach (LevelInfo info in lvels)
            {
                if (curentLVL == info)
                {
                    style.normal = GUI.skin.button.onNormal;
                }
                else
                {
                    style.normal = GUI.skin.button.normal;
                }
                if (GUILayout.Button(info.name, style))
                {
                    curentLVL = info;
                }
            }
            GUILayout.EndScrollView();
            GUILayout.EndVertical();
            GUILayout.BeginVertical(GUILayout.Width(120));
            GUILayout.Label(INC.la("camera_type"));
            int ss = (int)IN_GAME_MAIN_CAMERA.cameraMode;
            ss = GUILayout.SelectionGrid(ss, new string[] { "ORIGINAL", "WOW", "TPS", "oldTPS" }, 1);
            IN_GAME_MAIN_CAMERA.cameraMode = (CAMERA_TYPE)ss;
            GUILayout.Label(INC.la("day_time_multy"));
            GUIStyle style4 = new GUIStyle(GUI.skin.button);

            string[] ssf = new string[] { INC.la("day_multy"), INC.la("dawn_multy"), INC.la("night_multy") };
            for (int i = 0; i < 3; i++)
            {
                string ssn = ssf[i];
                if ((int)FengGameManagerMKII.settings[409] == i)
                {
                    style4.normal = GUI.skin.button.onNormal;
                }
                else
                {
                    style4.normal = GUI.skin.button.normal;
                }
                if (GUILayout.Button(ssn, style4))
                {
                    FengGameManagerMKII.settings[409] = i;
                }
            }

            GUILayout.Label( CyanMod.INC.la("ui_difficulty"));
            IN_GAME_MAIN_CAMERA.difficulty = (DIFFICULTY)GUILayout.SelectionGrid((int)IN_GAME_MAIN_CAMERA.difficulty, new string[] { CyanMod.INC.la("ui_normal"), CyanMod.INC.la("ui_hard"), CyanMod.INC.la("ui_abnormal") }, 1);
            GUILayout.EndVertical();
            GUILayout.Label("", GUILayout.Width(100f));
            GUILayout.BeginVertical();
            if (!IN_GAME_MAIN_CAMERA.singleCharacter.StartsWith("SET") && !IN_GAME_MAIN_CAMERA.singleCharacter.StartsWith("AHSS"))
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(INC.la("type_cos"));
                string[] cos = new string[] { "Cos 1", "Cos 2", "Cos 3" };
                GUIStyle style3 = new GUIStyle(GUI.skin.button);
                for (int s = 0; s < cos.Length; s++)
                {
                    string de = cos[s];
                    if (CheckBoxCostume.costumeSet - 1 == s)
                    {
                        style3.normal = GUI.skin.button.onNormal;
                    }
                    else
                    {
                        style3.normal = GUI.skin.button.normal;
                    }
                    if (GUILayout.Button(de, style3))
                    {
                        CheckBoxCostume.costumeSet = s + 1;
                    }
                }

                GUILayout.EndHorizontal();
            }
            GUIStyle style2 = new GUIStyle(GUI.skin.button);
            scroolPos1 = GUILayout.BeginScrollView(scroolPos1);
            foreach (var sre in HeroStat.stats)
            {
                if (sre.Key != "CUSTOM_DEFAULT")
                {
                    if (IN_GAME_MAIN_CAMERA.singleCharacter == sre.Key.ToUpper())
                    {
                        style2.normal = GUI.skin.button.onNormal;
                    }
                    else
                    {
                        style2.normal = GUI.skin.button.normal;
                    }
                    if (GUILayout.Button(sre.Key.ToUpper(), style2))
                    {
                        IN_GAME_MAIN_CAMERA.singleCharacter = sre.Key.ToUpper();
                    }
                }
            }
            GUILayout.EndScrollView();
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button(INC.la("btn_start"), GUILayout.Width(120f), GUILayout.Height(35f)))
            {
              
                Saving();
                if (IN_GAME_MAIN_CAMERA.singleCharacter.StartsWith("SET") || IN_GAME_MAIN_CAMERA.singleCharacter.StartsWith("AHSS"))
                {
                    CheckBoxCostume.costumeSet = 1;
                }
                IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.SINGLE;
                if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.TPS || IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.oldTPS)
                {
                    Screen.lockCursor = true;
                }
                Screen.showCursor = false;
                FengGameManagerMKII.level = curentLVL.name;
                FengGameManagerMKII.instance.saves();
                Application.LoadLevel(curentLVL.mapName);
            }

            GUILayout.Label("");
            if (GUILayout.Button(INC.la("btn_back"), GUILayout.Width(120f), GUILayout.Height(35f)))
            {
                NGUITools.SetActive(base.gameObject, false);
                NGUITools.SetActive(UIMainReferences.instance.panelMain.gameObject, true);
            }
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
    }
    public void showTxt()
    {
        this.label_START.GetComponent<UILabel>().text = INC.la("btn_start");
        this.label_BACK.GetComponent<UILabel>().text = INC.la("btn_back");
        this.label_camera.GetComponent<UILabel>().text = INC.la("camera_type");
        this.label_original.GetComponent<UILabel>().text = INC.la("camera_original");
        this.label_wow.GetComponent<UILabel>().text = INC.la("camera_wow");
        this.label_tps.GetComponent<UILabel>().text =INC.la("camera_tps");
        this.label_choose_character.GetComponent<UILabel>().text = INC.la("choose_character");
        this.label_difficulty.GetComponent<UILabel>().text =INC.la("difficulty");
        this.label_choose_map.GetComponent<UILabel>().text =INC.la("choose_map");
        this.label_normal.GetComponent<UILabel>().text = INC.la("ui_normal");
        this.label_hard.GetComponent<UILabel>().text = INC.la("ui_hard");
        this.label_ab.GetComponent<UILabel>().text = INC.la("ui_abnormal");
        UILabel[] gm = base.gameObject.GetComponentsInChildren<UILabel>();
        foreach (UILabel lab in gm)
        {
            lab.color = CyanMod.INC.color_UI;
        }
    }
    private void Update()
    {
        
    }
}

