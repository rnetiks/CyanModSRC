using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using UnityEngine;
using CyanMod;
using System.Diagnostics;
public class PanelMain : MonoBehaviour
{
    public static PanelMain instance;
    public static Vector2 oldpos;
    public LoadChanglog loadChange;
    public loadedNews loadNews;
    string connecting;
    int is_Conecting = 0;
    int abl_setingf = 0;

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
        is_Conecting = 0;
    }
    IEnumerator ConnectedS()
    {
        int con = 0;
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            con = con + 1;
            if (NGUITools.GetActive(UIMainReferences.instance.panelMultiROOM))
            {
                NGUITools.SetActive(base.gameObject, false);
            }
            if (con > 1000)
            {
                connecting = INC.la("error_connectings");
                yield break;
            }
            if (is_Conecting != 1)
            {
                yield break;
            }
        }

    }
    int isHow = 0;
    float hight = 35;
    void botom_right()
    {
      
        Rect rr = new Rect(0,Screen.height - 22,180f,22);
        Rect[] rect = new Rect[] { rr, rr, rr, rr };
        string vk = "ВКонтакте";
        for (int s = 0; s < rect.Length; s++)
        {
            GUIStyle style = new GUIStyle(GUI.skin.box);
            style.fontSize = 15;
            style.alignment = TextAnchor.MiddleLeft;
            style.normal.background = GUI.skin.box.onNormal.background;
            
            rect[s].y = rect[s].y - (22 * s);
            if (rect[s].Contains(Event.current.mousePosition))
            {
                style.normal = GUI.skin.box.normal;
                rect[s].x = rect[s].x + 20f;
                if (s == 0)
                {
                    vk = "https://vk.com/cyan_mod";
                }
            }
            if (s == 0)
            {
                if (GUI.Button(rect[s], vk, style))
                {
                    Process.Start("https://vk.com/cyan_mod");
                }
                
            }
            else if (s == 1)
            {
                if (GUI.Button(rect[s], CyanMod.INC.la("Snapshot_Reviewer"), style))
                {
                    Application.LoadLevel("SnapShot");
                }
            }
            else if (s == 2)
            {
                if (GUI.Button(rect[s], CyanMod.INC.la("Custom_Characters"), style))
                {
                    Application.LoadLevel("characterCreation");
                }
            }
            else if (s == 3)
            {
                if (GUI.Button(rect[s], INC.la("Level_Editor"), style))
                {
                    FengGameManagerMKII.settings[64] = 101;
                    Application.LoadLevel(2);
                }
       
            }
        }
           
      
    }
    Vector2 slop;
    void OnGUI()
    {
        if (INC.isLogined)
        {
            GUI.backgroundColor = INC.gui_color;
            if (is_Conecting == 1)
            {
                GUILayout.BeginArea(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 30, 200, 90), GUI.skin.box);
                GUILayout.Label(connecting);
                if (GUILayout.Button(INC.la("close_connectings")))
                {
                    PhotonNetwork.Disconnect();
                    is_Conecting = 0;
                }
                GUILayout.EndArea();
            }
            else if (is_Conecting == 2)
            {
                bo_righrt(false);
            }
            else if (is_Conecting == 0)
            {
                bo_righrt(true);
                GUI.Label(new Rect(Screen.width / 2 - 100f, Screen.height - 22, 200, 22), "Cyan_mod " + UIMainReferences.CyanModVers, new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontSize = 13 });
                botom_right();
                int s = 130;
                GUI.Box(new Rect(0, 0, s * 4, hight), "");

                Rect single = new Rect(s * 0, 0, 130, 30);
                Rect multy = new Rect(s * 1, 0, 130, 30);
                Rect multyS = new Rect(0f, 30f, s * 4, 100f);
                Rect snapshos = new Rect(s * 2, 0, 130, 30);
                Rect snapshosS = new Rect(0, 30f, s * 4, 400f);
                Rect exit = new Rect(s * 3, 0, 130, 30);
                Rect loggin = new Rect(Screen.width - 200, 0, 200, 30);
                Rect logginS = new Rect(Screen.width - 200, 30, 200, 250);


              
                if (multy.Contains(Event.current.mousePosition))
                {
                    isHow = 1;
                }
                else if (snapshos.Contains(Event.current.mousePosition))
                {
                    isHow = 2;
                }
                else if (loggin.Contains(Event.current.mousePosition))
                {
                    isHow = 3;
                }
                if (isHow == 1)
                {
                    hight = 130f;
                    if (!multyS.Contains(Event.current.mousePosition) && !multy.Contains(Event.current.mousePosition))
                    {
                        isHow = 0;
                        hight = 35;
                    }
                    Multy(multyS);
                }
                else if (isHow == 2)
                {
                    hight = 430f;

                    if (!snapshosS.Contains(Event.current.mousePosition) && !snapshos.Contains(Event.current.mousePosition))
                    {
                        isHow = 0;
                        hight = 35;
                    }
                    Abilities(snapshosS);
                }
                else if (isHow == 3)
                {
                    if (!loggin.Contains(Event.current.mousePosition) && !logginS.Contains(Event.current.mousePosition))
                    {
                        isHow = 0;
                    }
                    player_info(logginS);
                }
                GUI.Label(loggin, "<color=#FFFFFF>" + LoginFengKAI.player.name.toHex() + "</color>");
                if (GUI.Button(single, CyanMod.INC.la("btn_single")))
                {
                    NGUITools.SetActive(base.gameObject, false);
                    NGUITools.SetActive(UIMainReferences.instance.panelSingleSet, true);
                }
                if (GUI.Button(multy, CyanMod.INC.la("btn_multiplayer")))
                {
                  
                    CyanMod.INC.Connected();
                    connecting = INC.la("connectings");
                    is_Conecting = 1;
                    base.StartCoroutine(ConnectedS());
                }
                GUI.Button(snapshos, CyanMod.INC.la("ablities_main_m"));

                if (GUI.Button(exit, INC.la("quit")))
                {
                    Application.Quit();
                }
            }
        }
    }
    void player_info(Rect rect)
    {
        GUI.Box(new Rect(rect.x, rect.y - 30f, rect.width, rect.height),"");
        GUILayout.BeginArea(rect);
        slop = GUILayout.BeginScrollView(slop);
        if (FengGameManagerMKII.loginstate == 3)
        {
            GUILayout.Label(INC.la("Guild"));
            LoginFengKAI.player.guildname = GUILayout.TextField(LoginFengKAI.player.guildname, 40);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(INC.la("Set_Guild")))
            {
                base.StartCoroutine(FengGameManagerMKII.instance.setGuildFeng());
            }
            else if (GUILayout.Button(INC.la("Logout")))
            {
                FengGameManagerMKII.loginstate = 0;
                INC.isLogined = false;
            }
            GUILayout.EndHorizontal();
        }
        else
        {
            GUILayout.Label(INC.la("Name"));
            FengGameManagerMKII.settings[363] = GUILayout.TextField((string)FengGameManagerMKII.settings[363], 400);
            GUILayout.Label(INC.la("chatname"));
            FengGameManagerMKII.settings[364] = GUILayout.TextField((string)FengGameManagerMKII.settings[364], 400);
            GUILayout.Label(INC.la("Guild1"));
            FengGameManagerMKII.settings[361] = GUILayout.TextField((string)FengGameManagerMKII.settings[361], 400);
            GUILayout.Label(INC.la("Guild2"));
            FengGameManagerMKII.settings[362] = GUILayout.TextField((string)FengGameManagerMKII.settings[362], 400);
            if (GUILayout.Button(INC.la("save")))
            {
                if (((string)FengGameManagerMKII.settings[363]).Replace(" ", string.Empty).HexDell().StripHex().Length < 3)
                {
                  PanelInformer.instance.Add( INC.la("Name_is_In_thre"), PanelInformer.LOG_TYPE.DANGER);
                    return;
                }
                if (((string)FengGameManagerMKII.settings[363]).Contains("\n"))
                {
                    FengGameManagerMKII.settings[363] = ((string)FengGameManagerMKII.settings[363]).Replace("\n", string.Empty);
                }
                if (((string)FengGameManagerMKII.settings[361]).Contains("\n"))
                {
                    FengGameManagerMKII.settings[361] = ((string)FengGameManagerMKII.settings[361]).Replace("\n", string.Empty);
                }
                if (((string)FengGameManagerMKII.settings[362]).Contains("\n"))
                {
                    FengGameManagerMKII.settings[362] = ((string)FengGameManagerMKII.settings[362]).Replace("\n", string.Empty);
                }
                if (((string)FengGameManagerMKII.settings[364]).Contains("\n"))
                {
                    FengGameManagerMKII.settings[364] = ((string)FengGameManagerMKII.settings[364]).Replace("\n", string.Empty);
                }
                if (UIMainReferences.instance != null)
                {
                    UIMainReferences.instance.panelMain.gameObject.transform.position = INC.vector_mainMenu;
                }
                FengGameManagerMKII.instance.ApplySettings();
                LoginFengKAI.player.name = ((string)FengGameManagerMKII.settings[363]);
                FengGameManagerMKII.instance.saves();
                PanelInformer.instance.Add(INC.la("m_saveds"), PanelInformer.LOG_TYPE.INFORMAION);

            }
        }
        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }
    Vector2[] scrollpos = new Vector2[2];
    void Abilities(Rect rect)
    {
        GUILayout.BeginArea(rect);
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical(GUILayout.Width(150f));
        {
            scrollpos[0] = GUILayout.BeginScrollView(scrollpos[0]);
            if (GUILayout.Button(INC.la("open_folders")))
            {
                abl_setingf = 0;
            }
            if (GUILayout.Button(INC.la("show_connect_list")))
            {
                abl_setingf = 1;
            }
            if (GUILayout.Button(INC.la("news")))
            {
                abl_setingf = 2;
            }
            if (GUILayout.Button(INC.la("changelog")))
            {
                abl_setingf = 3;
            }
            if (GUILayout.Button(INC.la("btn_credits")))
            {
                abl_setingf = 4;
            }
            GUILayout.EndScrollView();
        }
        GUILayout.EndVertical();
        GUILayout.BeginVertical();
        {
            scrollpos[1] = GUILayout.BeginScrollView(scrollpos[1]);
            if (abl_setingf == 0)
            {
                if (GUILayout.Button(INC.la("open_map_script")))
                {
                    DirectoryInfo info = new DirectoryInfo(FengGameManagerMKII.instance.path);
                    if (!info.Exists)
                    {
                        info.Create();
                    }
                    Process.Start(FengGameManagerMKII.instance.path);
                }
                if (GUILayout.Button(INC.la("open_logic_script")))
                {
                    DirectoryInfo info = new DirectoryInfo(FengGameManagerMKII.instance.pathLogic);
                    if (!info.Exists)
                    {
                        info.Create();
                    }
                    Process.Start(FengGameManagerMKII.instance.pathLogic);
                }
                if (GUILayout.Button(INC.la("open_snapshots")))
                {
                    DirectoryInfo info = new DirectoryInfo(BTN_save_snapshot.path);
                    if (!info.Exists)
                    {
                        info.Create();
                    }
                    Process.Start(BTN_save_snapshot.path);
                }
                if (GUILayout.Button(INC.la("open_screenshots")))
                {
                    DirectoryInfo info = new DirectoryInfo(FengGameManagerMKII.ScreenshotsPath);
                    if (!info.Exists)
                    {
                        info.Create();
                    }
                    Process.Start(FengGameManagerMKII.ScreenshotsPath);
                }
            }
            else if (abl_setingf == 1)
            {
                if (INC.ConnectPlList.Count > 0)
                {
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("Hide all"))
                    {
                        foreach (INC.PlayerCheck player in INC.ConnectPlList)
                        {
                            player.show_hash = false;
                            player.show = false;
                        }
                    }
                    if (GUILayout.Button("Saved to File"))
                    {
                        string str44 = "";
                        foreach (INC.PlayerCheck player in INC.ConnectPlList)
                        {
                            str44 = str44 + "Player name:" + player.name + "\n";
                            str44 = str44 + "Time:" + player.time + "\n";
                            str44 = str44 + "Player ID:" + player.ID + "\n";
                            str44 = str44 + "Server:" + player.server_name + "\n";
                            str44 = str44 + "Server ID:" + player.server_id + "\n";
                            str44 = str44 + "Player Table:\n";
                            foreach (var sg in player.hash)
                            {
                                str44 = str44 + "Key:" + sg.Key.ToString() + " Value:" + sg.Value.ToString() + "\n";
                            }
                            str44 = str44 + "------------------------" + "\n";
                        }
                        File.WriteAllText(Application.dataPath + "/Connect_list.txt", str44, System.Text.Encoding.UTF8);
                        UnityEngine.Debug.Log("Connect_list Saved to " + Application.dataPath + "/Connect_list.txt");
                    }
                    GUILayout.EndHorizontal();
                    GUIStyle style = new GUIStyle(GUI.skin.button);
                    style.alignment = TextAnchor.MiddleLeft;
                    scrool = GUILayout.BeginScrollView(scrool);
                    foreach (INC.PlayerCheck player in INC.ConnectPlList)
                    {
                        if (GUILayout.Button(player.time + ": " + player.ishexname, style))
                        {
                            player.show = !player.show;
                        }
                        if (player.show)
                        {
                            string str = "Player:" + player.name + "\n";
                            str = str + "Player ID:" + player.ID + "\n";
                            str = str + "Server:" + player.server_name + "\n";
                            str = str + "Server ID:" + player.server_id;
                            GUILayout.TextField(str, GUI.skin.label);
                            if (GUILayout.Button("Show Player Table", GUILayout.Width(150f)))
                            {
                                player.show_hash = !player.show_hash;
                            }
                            if (player.show_hash)
                            {
                                string str33 = "";
                                foreach (var sg in player.hash)
                                {
                                    str33 = str33 + "Key:" + sg.Key.ToString() + " Value:" + sg.Value.ToString() + "\n";
                                }
                                GUILayout.TextField(str33, GUI.skin.label);
                            }

                        }
                    }
                    GUILayout.EndScrollView();

                }
                else
                {
                    GUILayout.Label("List clean.");
                }
            }
            else if (abl_setingf == 2)
            {
                if (loadNews == null)
                {
                    loadNews = new GameObject().AddComponent<loadedNews>();
                }
                loadNews.Updated();
            }
            else if (abl_setingf == 3)
            {
                if (loadChange == null)
                {
                    loadChange = new GameObject().AddComponent<LoadChanglog>();
                }
                loadChange.Updated();
            }
              else if (abl_setingf == 4)
            {
                GUILayout.Label("遊戲製作：李丰 \n動作師：李江\nThe game was inspired by the anime/manga: Shingeki no kyojin(or Attack On Titan)\nCreated by:Feng Li\nAddtional Animator:Jiang Li\nSpecial Thanks:Aya\n\nCreated Cyan mod: tap1k");
              }
            GUILayout.EndScrollView();
        }
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
    void Multy(Rect rect)
    {
        GUILayout.BeginArea(rect);
        GUIStyle[] style = new GUIStyle[] { new GUIStyle(GUI.skin.button),  new GUIStyle(GUI.skin.button), new GUIStyle( GUI.skin.button )};
        if (UIMainReferences.version == UIMainReferences.fengVersion)
        {
            GUILayout.Label(INC.la("Conn_to_public"));
            style[0].normal = GUI.skin.button.onNormal;
        }
        else if (UIMainReferences.version == FengGameManagerMKII.s[0])
        {
            GUILayout.Label(INC.la("Conn_to_RCprivate"));
            style[1].normal = GUI.skin.button.onNormal;
        }
        else if (UIMainReferences.version == "DontUseThisVersionPlease173")
        {
            GUILayout.Label(INC.la("Conn_to_crypto"));
        }
        else
        {
            GUILayout.Label(INC.la("Conn_to_custom"));
            style[2].normal = GUI.skin.button.onNormal;
        }
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();
        if (GUILayout.Button(INC.la("Public_Server"), style[0]))
        {
            UIMainReferences.version = UIMainReferences.fengVersion;
        }
        GUILayout.EndVertical();
        GUILayout.BeginVertical();
        if (GUILayout.Button(INC.la("RC_Private"), style[1]))
        {
            UIMainReferences.version = FengGameManagerMKII.s[0];
        }
        GUILayout.EndVertical();
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
       
        if (GUILayout.Button(INC.la("Custom"), style[2]))
        {
            UIMainReferences.version = FengGameManagerMKII.privateServerField;
        }
        FengGameManagerMKII.privateServerField = GUILayout.TextField(FengGameManagerMKII.privateServerField, 50, GUILayout.Width(70f));
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
       
        GUILayout.BeginHorizontal();
        if (GUILayout.Button(INC.la("offline_mode_hud")))
        {
            PhotonNetwork.offlineMode = true;
            NGUITools.SetActive(base.gameObject, false);
            NGUITools.SetActive(UIMainReferences.instance.panelMultiSet, true);
        }
        if (GUILayout.Button(INC.la("europe_serv")))
        {
            connecting = INC.la("connectings");
            FengGameManagerMKII.settings[399] = 0;
            CyanMod.INC.Connected();
            is_Conecting = 1;
            base.StartCoroutine(ConnectedS());
        }
        if (GUILayout.Button(INC.la("usa_serv")))
        {
            connecting = INC.la("connectings");
            FengGameManagerMKII.settings[399] = 1;
            CyanMod.INC.Connected();
            is_Conecting = 1;
            base.StartCoroutine(ConnectedS());
        }
        if (GUILayout.Button(INC.la("asia_serv")))
        {
            connecting = INC.la("connectings");
            FengGameManagerMKII.settings[399] = 2;
            CyanMod.INC.Connected();
            is_Conecting = 1;
            base.StartCoroutine(ConnectedS());
        }
        if (GUILayout.Button(INC.la("japan_serv")))
        {
            connecting = INC.la("connectings");
            FengGameManagerMKII.settings[399] = 3;
            CyanMod.INC.Connected();
            is_Conecting = 1;
            base.StartCoroutine(ConnectedS());
        }
        GUILayout.EndHorizontal();

        GUILayout.EndArea();
    }
    bool isOpen = false;
    Vector2 scrPos;
    PanelScore currentScore;
    Vector2[] vector = new Vector2[2];

    int kills;
    int death;
    int total;
    int max_dmg;
    int killed_titan;
    int killed_human;
    void InfoSession(PanelScore cs)
    {
        kills = 0;
        death = 0;
        total = 0;
        max_dmg = 0;
        killed_titan = 0;
        killed_human = 0;
        currentScore = cs;
        if (currentScore.titam_list.Count > 0)
        {
            foreach (PanelScore.Stats stats in currentScore.titam_list)
            {
                if (stats.death)
                {
                    death = death + 1;
                }
                else
                {
                    killed_titan = killed_titan + 1;
                    kills = kills + 1;
                    total = total + stats.damage;
                    if (stats.damage > max_dmg)
                    {
                        max_dmg = stats.damage;
                    }
                }
            }
        }
    }
    void bo_righrt( bool flag)
    {
        if (flag)
        {
            if (!isOpen)
            {
                if (GUI.Button(new Rect(Screen.width - 250, Screen.height - 25f, 250f, 25f), INC.la("open_session")))
                {
                    isOpen = true;
                }
            }
            else
            {
                GUILayout.BeginArea(new Rect(Screen.width - 250, Screen.height - 250f, 250f, 250f), GUI.skin.box);
                if (GUILayout.Button(INC.la("my_session")))
                {
                    isOpen = false;
                }
                scrPos = GUILayout.BeginScrollView(scrPos);
                if (PanelScore.list != null && PanelScore.list.Count > 0)
                {
                    for (int s = 0; s < PanelScore.list.Count; s++)
                    {
                        GUILayout.BeginVertical(GUI.skin.box);
                        PanelScore myS = PanelScore.list[s];
                        string str = "";
                        str = str + "[" + myS.time + "]" + myS.nickname.toHex() + "\n";
                        str = str + "Time:" + ((int)myS.timeSession).ToString() + "\n";
                        str = str + "LVL:" + myS.levelinfo.name + "\n";
                        str = str + "DIFF:" + myS.difficulty + "\n";
                        GUILayout.Label(str);
                        if (GUILayout.Button(INC.la("session_full")))
                        {
                            is_Conecting = 2;
                            InfoSession( myS);
                        }
                        GUILayout.EndVertical();
                    }
                }
                else
                {
                    GUILayout.Label(INC.la("no_session"));
                }
                GUILayout.EndScrollView();
                GUILayout.EndArea();
            }
        }
        else
        {
            GUILayout.BeginArea(new Rect(50, 50, Screen.width - 100f, Screen.height - 100f), GUI.skin.box);
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical(GUILayout.Width(200f));
            GUILayout.Label(INC.la("session_list"));
            vector[0] = GUILayout.BeginScrollView(vector[0]);
            if (PanelScore.list != null && PanelScore.list.Count > 0)
            {
                for (int s = 0; s < PanelScore.list.Count; s++)
                {
                    PanelScore myS = PanelScore.list[s];
                    if (GUILayout.Button("[" + myS.time + "]" + ((int)myS.timeSession).ToString()))
                    {
                        InfoSession( myS);
                    }
                }
            }
            else
            {
                GUILayout.Label(INC.la("no_session"));
            }
            GUILayout.EndScrollView();
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            GUILayout.Label(INC.la("sessionPlayer"));
            if (GUILayout.Button("<color=red>X</color>",GUILayout.Width(30f) ))
            {
                is_Conecting = 0;
            }
            GUILayout.EndHorizontal();
            vector[1] = GUILayout.BeginScrollView(vector[1]);

            if (currentScore != null)
            {
                GUILayout.Label("Time:" + currentScore.time + " Time Session:" + currentScore.timeSession.ToString("F") + "sc. Player name:" + currentScore.nickname.toHex() + "\n" + "Map name:" + currentScore.levelinfo.name + " Difficulty:" + currentScore.difficulty + "\n" + "Kills:" + kills + " Death:" + death + " Max Damage:" + max_dmg + " Total Damage:" + total);
                foreach (PanelScore.Stats stats in currentScore.titam_list)
                {
                    string time = "[" + stats.time + "]" + "{" + stats.sessiontime.ToString("F") + "sc.}";
                    if (stats.death)
                    {
                        GUILayout.Label(time + " DEATH.");
                    }
                    else
                    {
                        if (stats.size > 0)
                        {
                            GUILayout.Label(time + " Damage:" + stats.damage + " Tian name:" + stats.titan_name.toHex() + " Size:" + stats.size.ToString("F"));
                        }
                        else
                        {
                            GUILayout.Label(time + " Player name:" + stats.titan_name.toHex());
                        }
                    }
                }
            }
            else
            {
                GUILayout.Label(INC.la("no_curren_session"));
            }
            GUILayout.EndScrollView();
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
    }
    private void Start()
    {
        is_Conecting = 0;
        oldpos = base.gameObject.transform.position;
        base.gameObject.transform.position = new Vector3(0, 9999, 0);
        PhotonNetwork.offlineMode = false;
    }
    Vector2 scrool;
   
}

