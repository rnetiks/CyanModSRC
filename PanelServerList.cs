using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CyanMod;

public class PanelServerList : MonoBehaviour
{
    public GameObject label_back;
    public GameObject label_create;
    public GameObject label_name;
    public GameObject label_refresh;
    public static PanelServerList instance;
    void Awake()
    {
        instance = this;
    }
    void OnDestroy()
    {
        instance = null;
    }
    string[] searshing;
    int lengt = 0;
    string[] mapName = new string[] { "none", "track - akina", "The City I", "The Forest", "Colossal Titan", "OutSide", "HouseFight", "CaveFight", "Cage Fighting" };

    string[] types = new string[] { "none", "BOSS_FIGHT_CT", "CAGE_FIGHT", "ENDLESS_TITAN", "KILL_TITAN", "PVP_AHSS", "PVP_CAPTURE", "RACING", "SURVIVE_MODE", "TROST" };
    string[] levels = new string[] { 
        "none", "The Forest", "The Forest II", "The Forest III", "The Forest IV  - LAVA",  "The City", "The City II", "The City III", "Annie", "Annie II", "Colossal Titan", "Colossal Titan II", 
        "House Fight","Cave Fight", "Racing - Akina",  "Outside The Walls", "Trost", "Trost II","Custom", "Custom (No PT)"
     };
    string[] diff = new string[] { "none", "NORMAL", "HARD", "ABNORMAL" };
    string[] day = new string[] { "none", "DAY", "DAWN", "NIGHT" };
    string[] sort = new string[] { "0-9,A-Z", "Z-A,9-0", "Player count", "none" };
    int[] setting;
    MyRoom myRoom;
    float timerupdatelist;
    private int lang = -1;
    string Filter;
    bool[] ShowSet;
    Vector3 posPanel;
    Rect CyanPanelRect;
    Vector2 posCyanPanelList;
    Vector2 posCyanSearhList;
    string password;
    bool ShowPanelCyan;
    List<RoomInfo> RoomList;
    private void OnEnable()
    {
    }

    public void showTxt()
    {
       
            this.label_name.GetComponent<UILabel>().text = INC.la("server_name");
            this.label_refresh.GetComponent<UILabel>().text = INC.la("btn_refresh");
            this.label_back.GetComponent<UILabel>().text = INC.la("btn_back");
            this.label_create.GetComponent<UILabel>().text =INC.la("btn_create_game");
            UILabel[] gm = base.gameObject.GetComponentsInChildren<UILabel>();
            foreach (UILabel lab in gm)
            {

                lab.color = INC.color_UI;
            }
        
    }
    bool AddFilter(string text)
    {
        if (text.Trim() == string.Empty)
        {
            return false;
        }
        text = text.ToLower().Trim();
        searshing = searshing.EmptyD();

        int serahLenght = searshing.Length;
        if (serahLenght > 0)
        {
            if (searshing.Contains(text))
            {
                return false;
            }
            string strw = string.Empty;
            if (serahLenght >= 3)
            {
                serahLenght = 2;
            }
            for (int i = 0; i < serahLenght; i++)
            {
                string lll = searshing[i];
                if (lll != string.Empty)
                {
                    strw = lll + "," + strw;
                }
            }
            FengGameManagerMKII.settings[265] = text + "," + strw;
            PrefersCyan.SetString("stringSearhList", (string)FengGameManagerMKII.settings[265]);
            PrefersCyan.Save();
        }
        else
        {

            FengGameManagerMKII.settings[265] = text + ",";
            PrefersCyan.SetString("stringSearhList", (string)FengGameManagerMKII.settings[265]);
            PrefersCyan.Save();
            return true;
        }


        return true;
    }
    bool ShowPanel()
    {
        base.gameObject.transform.localPosition = new Vector3(0, 9999, 0);

        FengGameManagerMKII.settings[267] = 1;
        PanelMultiJoin.Enabeled = false;
        ShowPanelCyan = true;
        PrefersCyan.Save();
        return true;
    }
    bool HidePanel()
    {
        base.gameObject.transform.localPosition = posPanel;
        FengGameManagerMKII.settings[267] = 0;
        PanelMultiJoin.Enabeled = true;
        ShowPanelCyan = false;
        PrefersCyan.Save();
        return true;
    }
    void Start()
    {
        searshing = (((string)FengGameManagerMKII.settings[265]).Split(new char[] { ',' })).EmptyD();
        ShowSet = new bool[] { false, false, false, false, false, false };
        setting = new int[] { 0, 0, 0, 0, 0, 0 };
        RoomList = new List<RoomInfo>();
        PanelInformer.instance.Add(INC.la("chage_serv_plc"), PanelInformer.LOG_TYPE.INFORMAION);
        password = string.Empty;
        myRoom = new MyRoom();
        timerupdatelist = 3f;
        Filter = string.Empty;
        posPanel = base.gameObject.transform.localPosition;
        if ((int)FengGameManagerMKII.settings[267] == 1)
        {
            ShowPanel();
        }
        else
        {
            HidePanel();
        }
        CyanPanelRect = new Rect(Screen.width / 2 - 440, Screen.height / 2 - 250, 880, 500);
        if (UIMainReferences.instance != null || NGUITools.GetActive(UIMainReferences.instance.panelMain))
        {
            NGUITools.SetActive(UIMainReferences.instance.panelMain, false);
        }
    }
    bool settingSearh(int Bool, int sett, string label, string[] param)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label(label);
        if (GUILayout.Button(param[setting[sett]], new GUIStyle(GUI.skin.button) { alignment = TextAnchor.MiddleLeft }))
        {
            ShowSet[Bool] = !ShowSet[Bool];
        }
        GUILayout.EndHorizontal();
        if (ShowSet[Bool])
        {
            int sss = setting[sett];
            setting[sett] = GUILayout.SelectionGrid(setting[sett], param, 1);
            if (sss != setting[sett])
            {
                ShowSet[Bool] = false;
                UpdList();
            }
        }
        return true;
    }
    bool toServer(RoomInfo room)
    {
        if (room == null)
        {
            PanelInformer.instance.Add(INC.la("no_changed_room_plc"), PanelInformer.LOG_TYPE.INFORMAION);
            return false;
        }
        if (room.Password != string.Empty && room.Password == password)
        {
            AddFilter(Filter);
            PhotonNetwork.JoinRoom(room.name);
            conected = true;
            FengGameManagerMKII.instance.saves();
            string strw = string.Empty;
            return true;
        }
        else if (room.Password != string.Empty && room.Password != password)
        {
            PanelInformer.instance.Add(INC.la("pass_no_correct_plc"), PanelInformer.LOG_TYPE.WARNING);
            return false;
        }
        else if (room.Password == string.Empty)
        {
            AddFilter(Filter);
            conected = true;
            PhotonNetwork.JoinRoom(room.name);

            FengGameManagerMKII.instance.saves();
        }
        return false;
    }
    void SearhSettings()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label(INC.la("filter_cyan_ser_list"));
        if (GUILayout.Button("UPD", GUILayout.Width(34f)))
        {
            UpdList();
            PanelInformer.instance.Add(INC.la("server_list_upd"), PanelInformer.LOG_TYPE.INFORMAION);
        }
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        string s = Filter;
        Filter = GUILayout.TextField(Filter);
        if (s != Filter)
        {
            UpdList();
        }
        if (GUILayout.Button("x", GUILayout.Width(30f)))
        {
            Filter = "";
            UpdList();
        }
        GUILayout.EndHorizontal();
        if (searshing.Length > 0)
        {
            GUILayout.BeginHorizontal();

            foreach (string ssd in searshing)
            {
                if (GUILayout.Button(ssd, new GUIStyle(GUI.skin.button) { alignment = TextAnchor.MiddleLeft }, GUILayout.Width(70f)))
                {
                    Filter = ssd;
                    UpdList();
                }
            }

            GUILayout.EndHorizontal();
        }
        posCyanSearhList = GUILayout.BeginScrollView(posCyanSearhList);

        GUICyan.OnToogleCyan(INC.la("show_pass_csl"), 275, 1, 0, 50f);
        GUICyan.OnToogleCyan(INC.la("show_full_csl"), 276, 1, 0, 50f);
        if (INC.ServerPrivated != "")
        {
            int i = (int)FengGameManagerMKII.settings[368];
            GUICyan.OnToogleCyan(INC.la("private_server"), 368, 1, 0, 50f);
            if (i != (int)FengGameManagerMKII.settings[368])
            {
                INC.Connected();
            }
        }
        if (GUILayout.Button(INC.la("random_server_ps")))
        {
            RoomInfo[] rm = PhotonNetwork.GetRoomList();
            if (rm.Length > 0)
            {
                List<RoomInfo> list = new List<RoomInfo>();
                foreach (RoomInfo info in rm)
                {
                    if (info.playerCount < info.maxPlayers && info.Password == "")
                    {
                        list.Add(info);
                    }
                }
                conected = true;
                PhotonNetwork.JoinRoom(list[UnityEngine.Random.Range(0, list.Count)].name);
            }
        }
        GUILayout.Label(INC.la("sorting_csl"));
        FengGameManagerMKII.settings[272] = GUILayout.SelectionGrid((int)FengGameManagerMKII.settings[272], sort, 2);


        settingSearh(4, 4, INC.la("diff_s_csl"), diff);
        settingSearh(3, 3, INC.la("day_time_s_csl"), day);
        settingSearh(1, 1, INC.la("level_name_csl"), levels);
        settingSearh(2, 2, INC.la("map_name_csl"), mapName);
        settingSearh(0, 0, INC.la("game_mode_csl"), types);




        GUILayout.EndScrollView();
    }
    void ServerS(RoomInfo room, int count, GUIStyle style)
    {
        GUIStyle style32 = new GUIStyle(GUI.skin.button);
        style32.alignment = TextAnchor.MiddleLeft;
        style32.normal.background = Coltext.transp;
        style32.hover.background = Coltext.transp;
        style32.active.background = Coltext.transp;
        style32.focused.background = Coltext.transp;
        style32.onNormal.background = Coltext.transp;
        style32.onHover.background = Coltext.transp;
        style32.onFocused.background = Coltext.transp;
        style32.onActive.background = Coltext.transp;
        GUILayout.BeginHorizontal(style);
        if (GUILayout.Button(room.Password == string.Empty ? string.Empty : "[PWD]", style32, GUILayout.Width(50f)))
        {
            myRoom.Set(room, count);
        }
        if (GUILayout.Button(room.RoomName.toHex(), style32, GUILayout.Width(200f)))
        {
            myRoom.Set(room, count);
        }
        if (GUILayout.Button(room.MapName, style32, GUILayout.Width(130f)))
        {
            myRoom.Set(room, count);
        }
        if (GUILayout.Button(room.Difficulty, style32, GUILayout.Width(80f)))
        {
            myRoom.Set(room, count);
        }

        if (GUILayout.Button(room.DayTime, style32, GUILayout.Width(50f)))
        {
            myRoom.Set(room, count);
        }
        if (GUILayout.Button(room.playerCount + "/" + room.maxPlayers, style32, GUILayout.Width(40f)))
        {
            myRoom.Set(room, count);
        }
        GUILayout.EndHorizontal();
    }
    void connect(string but, int i)
    {
        string name = but;
        if (i == (int)FengGameManagerMKII.settings[399])
        {
            name = "[" + name + "]";
        }
        if (GUILayout.Button(name))
        {
            FengGameManagerMKII.settings[399] = i;
            CyanMod.INC.Connected();
            RoomList.Clear();
        }
    }
    WitServer witeS;
    void ListServers()
    {
        GUIStyle style = new GUIStyle(GUI.skin.button);
        style.fixedHeight = 26f;
        RoomInfo roomch = null;
        GUILayout.BeginHorizontal();
        string[] listcon = new string[] { INC.la("ui_europe"), INC.la("ui_usa"), INC.la("ui_asia"), INC.la("ui_japan") };
        for (int i = 0; i < listcon.Length; i++)
        {
            connect(listcon[i], i);
        }
        GUILayout.EndHorizontal();
        posCyanPanelList = GUILayout.BeginScrollView(posCyanPanelList);
        for (int i = 0; i < RoomList.Count; i++)
        {
            RoomInfo rOom = RoomList[i];
            if (myRoom.ID == rOom.IDRoom && myRoom.roomMap == rOom.MapName && myRoom.roomName == rOom.RoomName && myRoom.diff == rOom.Difficulty)
            {
                style.normal = GUI.skin.button.onNormal;
                myRoom.myPos = i;
                roomch = rOom;
            }
            else
            {
                style.normal = GUI.skin.button.normal;
            }
            ServerS(rOom, i, style);
        }
        GUILayout.EndScrollView();
        GUILayout.BeginVertical(GUI.skin.box);
        if (roomch != null)
        {
            GUILayout.Label(INC.la("name_server_psl") + roomch.RoomName.toHex());
            GUILayout.Label(INC.la("map_psl") + roomch.MapName + " " + INC.la("diff_psl") + roomch.Difficulty + " " + INC.la("daytime_psl") + roomch.DayTime + " " + INC.la("player_psl") + roomch.playerCount + "/" + roomch.maxPlayers);
            GUILayout.BeginHorizontal();

            if (roomch.maxPlayers > roomch.playerCount)
            {
                if (GUILayout.Button(INC.la("connect_to_plc"), GUILayout.Width(130f), GUILayout.Height(30f)))
                {
                    if (witeS != null)
                    {
                        Destroy(witeS);
                    }
                    toServer(roomch);
                }
            }
            else
            {
                if (GUILayout.Button(INC.la("wait_server"), GUILayout.Width(130f), GUILayout.Height(30f)))
                {
                    if (roomch.Password == "" || roomch.Password == password)
                    {
                        if (witeS != null)
                        {
                            Destroy(witeS);
                        }
                        witeS = new GameObject().AddComponent<WitServer>();
                        witeS.to_wait(roomch);
                    }
                    else
                    {
                        PanelInformer.instance.Add(INC.la("pass_no_correct_plc"), PanelInformer.LOG_TYPE.INFORMAION);
                    }
                   
                }
            }
            if (roomch.Password != string.Empty)
            {
                GUILayout.Label(INC.la("password_plc"), GUILayout.Width(130f));
                password = GUILayout.TextField(password, GUILayout.Width(150f));
            }


            GUILayout.EndHorizontal();

        }
        GUILayout.EndVertical();

    }
    public bool conected = false;
    void OnGUI()
    {
        GUI.backgroundColor = INC.gui_color;
        if (conected)
        {
            GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), INC.la("conected_to_fas"), new GUIStyle(GUI.skin.box) { alignment = TextAnchor.MiddleCenter });
            return;
        }
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
            GUILayout.BeginArea(CyanPanelRect, GUI.skin.box);
            GUILayout.BeginHorizontal();
            GUILayout.Label(INC.la("update_list_cpl") + (3f - timerupdatelist).ToString("F"), new GUIStyle(GUI.skin.label) { alignment = TextAnchor.UpperLeft }, GUILayout.Width(230f));
            GUILayout.Label(INC.la("server_list_cyan_panel"), new GUIStyle(GUI.skin.label) { alignment = TextAnchor.UpperCenter });

            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(230f));
            SearhSettings();
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            ListServers();
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(INC.la("create_game_multi"), GUILayout.Width(150f), GUILayout.Height(30f)))
            {
                NGUITools.SetActive(base.gameObject, false);
                NGUITools.SetActive(UIMainReferences.instance.panelMultiSet, true);
            }
            GUILayout.Label("");
            if (GUILayout.Button(INC.la("back"), GUILayout.Width(150f), GUILayout.Height(30f)))
            {
                PhotonNetwork.Disconnect();
                NGUITools.SetActive(base.gameObject, false);
                NGUITools.SetActive(UIMainReferences.instance.panelMain, true);

                PhotonNetwork.Disconnect();
            }
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
    }
    void UpdList()
    {
        RoomList = new List<RoomInfo>();
        foreach (RoomInfo roon in PhotonNetwork.GetRoomList())
        {
            LevelInfo level = LevelInfo.getInfo(roon.MapName);
            if ((types[setting[0]] == "none" || level.type.ToString() == types[setting[0]]))
            {
                if (levels[setting[1]] == "none" || level.name == levels[setting[1]])
                {
                    if (mapName[setting[2]] == "none" || level.mapName == mapName[setting[2]])
                    {
                        if ((day[setting[3]] == "none" || roon.DayTime == day[setting[3]]))
                        {
                            if ((diff[setting[4]] == "none" || roon.Difficulty == diff[setting[4]]))
                            {
                                if ((int)FengGameManagerMKII.settings[275] == 0 || roon.Password == string.Empty)
                                {
                                    if ((int)FengGameManagerMKII.settings[276] == 0 || roon.playerCount != roon.maxPlayers)
                                    {
                                        if (roon.name.ToLower().Contains(Filter.ToLower()))
                                        {
                                            this.RoomList.Add(roon);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        if (RoomList.Count > 1)
        {
            int num = (int)FengGameManagerMKII.settings[272];
            if (num == 0)
            {
                this.RoomList = new List<RoomInfo>(this.RoomList.OrderBy<RoomInfo, RoomInfo>(x => x, new FunctionComparer<RoomInfo>((x, y) => string.Compare(x.name.Split(new char[] { '`' })[0].NullFix().StripHex().Trim(), y.name.Split(new char[] { '`' })[0].NullFix().StripHex().Trim()))));
            }
            else if (num == 1)
            {
                this.RoomList = new List<RoomInfo>(this.RoomList.OrderByDescending<RoomInfo, RoomInfo>(x => x, new FunctionComparer<RoomInfo>((x, y) => string.Compare(x.name.Split(new char[] { '`' })[0].NullFix().StripHex().Trim(), y.name.Split(new char[] { '`' })[0].NullFix().StripHex().Trim()))));
            }
            else if (num == 2)
            {
                this.RoomList = new List<RoomInfo>((from room in this.RoomList orderby room.playerCount descending select room).ToArray<RoomInfo>());
            }
        }
        if (lengt != searshing.Length)
        {
            searshing = (((string)FengGameManagerMKII.settings[265]).Split(new char[] { ',' })).EmptyD();
            lengt = searshing.Length;
        }
    }
    void Update()
    {
        this.showTxt();
        if (ShowPanelCyan)
        {
            timerupdatelist += (float)Time.deltaTime;
            if (timerupdatelist > 3f)
            {
                timerupdatelist = 0;
                UpdList();
            }

        }
    }
    void LateUpdate()
    {
        if (NGUITools.GetActive(UIMainReferences.instance.panelMain.gameObject))
        {
            NGUITools.SetActive(UIMainReferences.instance.panelMain, false);
        }
        if (ShowPanelCyan && RoomList.Count > 1)
        {
            if (myRoom.myPos > RoomList.Count - 1)
            {
                posCyanPanelList.y = 999f;
                myRoom.myPos = RoomList.Count - 1;
                myRoom.Set(RoomList[myRoom.myPos], myRoom.myPos);
            }
            else if (myRoom.myPos < 0)
            {
                posCyanPanelList.y = 0f;
                myRoom.myPos = 0;
                myRoom.Set(RoomList[myRoom.myPos], myRoom.myPos);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                if (myRoom.myPos != RoomList.Count - 1)
                {
                    myRoom.myPos++;
                    posCyanPanelList.y = myRoom.myPos * 30;
                    myRoom.Set(RoomList[myRoom.myPos], myRoom.myPos);
                }
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                if (myRoom.myPos != 0)
                {

                    myRoom.myPos--;
                    posCyanPanelList.y = myRoom.myPos * 30;
                    myRoom.Set(RoomList[myRoom.myPos], myRoom.myPos);
                }
            }
            if (Input.GetKey(KeyCode.Return))
            {
                toServer(RoomList[myRoom.myPos]);
            }
        }
    }
    public class MyRoom
    {
        public int myPos;
        public int ID;
        public string roomName;
        public string roomMap;
        public string diff;
        public MyRoom()
        {
            myPos = 0;
            ID = 0;
            roomName = string.Empty;
            roomMap = string.Empty;
            diff = string.Empty;
        }
        public void Set(RoomInfo room, int position)
        {
            myPos = position;
            ID = room.IDRoom;
            roomName = room.RoomName.Resize(15);
            roomMap = room.MapName;
            diff = room.Difficulty;
        }
    }

}

