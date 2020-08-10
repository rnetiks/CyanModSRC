using System;
using System.Diagnostics;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CyanMod;
using UnityEngine;
using System.Security.AccessControl;
using System.Security.Principal;
using ExitGames.Client.Photon;
using UnityEngine.UI;


public class FengGameManagerMKII : Photon.MonoBehaviour
{
    public Dictionary<int, CannonValues> allowedToCannon;
    public static List<int> antiLeaveList;
    public string[] f_u_c_k = new string[] { "хуй", "бля", "пизд", "шлюх", "пид", "хуи", "ебан", "ебл", "сучка", "охуе", "хуе", "хуё" };
    public static readonly string applicationId = "f1f6195c-df4a-40f9-bae5-4744c32901ef";
    public Dictionary<string, Texture2D> assetCacheTextures;
    public static ExitGames.Client.Photon.Hashtable banHash;
    public static bool blazeit = false;
    public static ExitGames.Client.Photon.Hashtable boolVariables { get; set; }
    public GameObject checkpoint;
    public ArrayList cT { get; set; }
    static string currentLevel;
    static string currentScript;
    static string currentScriptLogic;
    private float currentSpeed;
    static bool customLevelLoaded;
    public int cyanKills;
    public float distanceSlider;
    private bool endRacing;
    private ArrayList eT { get; set; }
    public static ExitGames.Client.Photon.Hashtable floatVariables { get; set; }
    public ArrayList fT { get; set; }
    private float gameEndCD;
    private float gameEndTotalCDtime = 9f;
    public bool gameStart;
    private bool gameTimesUp;
    public List<GameObject> groundList;
    public ArrayList heroes { get; set; }
    public static ExitGames.Client.Photon.Hashtable heroHash { get; set; }
    private int highestwave = 1;
    private ArrayList hooks;
    private int humanScore;
    public static List<int> ignoreList { get; set; }
    public static ExitGames.Client.Photon.Hashtable imatitan;
    public static InputManagerRC inputRC { get; set; }
    public static FengGameManagerMKII instance { get; set; }
    public static ExitGames.Client.Photon.Hashtable intVariables { get; set; }
    public static bool isAssetLoaded;
    public bool isFirstLoad;
    private bool isLosing;
    public bool isPlayer1Winning;
    public bool isPlayer2Winning;
    public bool isRecompiling;
    public bool isRestarting;
    public bool isSpawning;
    public bool isUnloading;
    private bool isWinning;
    public bool justSuicide;
    private ArrayList kicklist;
    private ArrayList killInfoGO = new ArrayList();
    public static bool LAN;
    public static string level = string.Empty;
    public List<string[]> levelCache;
    public static ExitGames.Client.Photon.Hashtable[] linkHash;
    private string localRacingResult;
    public static bool logicLoaded;
    public static int loginstate;
    public int magentaKills;
    public IN_GAME_MAIN_CAMERA mainCamera; //off
    public static bool masterRC;
    public int maxPlayers;
    private float maxSpeed;
    private string myLastHero;
    private string myLastRespawnTag = "playerRespawn";
    public float myRespawnTime;
    public bool needChooseSide;
    public static bool noRestart;
    public static string oldScript;
    public static string oldScriptLogic;
    public static bool OnPrivateServer;
    public static string passwordField;
    public float pauseWaitTime;
    public string playerList;
    Vector2 Resolution = new Vector2(1280, 600);
    public List<Vector3> playerSpawnsC;
    public List<Vector3> playerSpawnsM;
    public List<PhotonPlayer> playersRPC;
    public static ExitGames.Client.Photon.Hashtable playerVariables;
    public Dictionary<string, int[]> PreservedPlayerKDR;
    public static string PrivateServerAuthPass;
    public static string privateServerField;
    public int PVPhumanScore;
    private int PVPhumanScoreMax = 200;
    public int PVPtitanScore;
    private int PVPtitanScoreMax = 200;
    public List<GameObject> racingDoors;
    private ArrayList racingResult;
    public Vector3 racingSpawnPoint;
    public bool racingSpawnPointSet;
    public static AssetBundle RCassets;
    public static ExitGames.Client.Photon.Hashtable RCEvents;
    public static ExitGames.Client.Photon.Hashtable RCRegions;
    public static ExitGames.Client.Photon.Hashtable RCRegionTriggers;
    public static ExitGames.Client.Photon.Hashtable RCVariableNames;
    public List<float> restartCount;
    public bool restartingBomb;
    public bool restartingEren;
    public bool restartingHorse;
    public bool restartingMC;
    public bool restartingTitan;
    public float retryTime;
    public float roundTime;
    public static string[] s;
    public Vector2 scroll;
    public Vector2 scroll2;
    public GameObject selectedObj;
    public static object[] settings { get; set; }
    private int single_kills;
    private int single_maxDamage;
    private int single_totalDamage;
    public static Material skyMaterial;
    private bool startRacing;
    public static ExitGames.Client.Photon.Hashtable stringVariables { get; set; }
    private int[] teamScores;
    private int teamWinner;
    public Texture2D textureBackgroundBlack;
    public Texture2D textureBackgroundBlue;
    public int time = 600;
    private float timeElapse;
    private float timeTotalServer;
    public List<TITAN> titans { get; set; }
    private int titanScore;
    public List<TitanSpawner> titanSpawners;
    public List<Vector3> titanSpawns;
    public static ExitGames.Client.Photon.Hashtable titanVariables { get; set; }
    public float transparencySlider;
    private GameObject ui;
    public UIReferArray uiT;
    static int cuality_settings = 0;

    public float updateTime;
    public static string usernameField;
    public int wave = 1;
    PhotonPlayer selplayer;
    List<PhotonPlayer> AddPl;
    TextMesh LabelInfoCenter;
    TextMesh LabelInfoTopCenter;
    TextMesh LabelInfoTopLeft;
    TextMesh LabelInfoTopRight;
    TextMesh LabelNetworkStatus;
    public TextMesh LabelInfoBottomRight;
    bool LabelCenter;
    bool LabelTopCenter;
    bool LabelTopLeft;
    bool LabelTopRight;
    static double fpsfps;
    double fpstime;
    double fpsaccum;
    int fpsframes;
    double fpsinterval;
    bool showFPS;
    bool isGoRestart;
    int timerGoRestart;
    public void GoRestarting(int num2 = 777)
    {
        int num = 3;
        if (num2 != 777)
        {
            num = num2;
        }
        else
        {
            if ((!int.TryParse((string)FengGameManagerMKII.settings[302], out num) || (num > 10)) || (num < 0))
            {
                FengGameManagerMKII.settings[302] = num.ToString();
            }
        }
        if (num > 10)
        {
            InRoomChat.instance.addLINE(INC.la("max_timer_for_go_restart"));
            num = 10;
        }
        else if (num < 0)
        {
            InRoomChat.instance.addLINE(INC.la("min_timer_for_go_restart"));
            num = 0;
        }
        if (!isGoRestart)
        {
            timerGoRestart = num;
            isGoRestart = true;
            dds = 0.0;
            return;
        }
        else
        {
            isGoRestart = false;
            cext.mess(INC.la("stoped_timer_go_restart"));
        }
    }
    double dds;
    public List<GameObject> alltitans;
    public List<GameObject> allheroes;
    public List<KillInfoComponent> killinfo;
    public PhotonPlayer playergrab_skin;
    public static List<string> dis_text;
    public int number_grabed_skin = 0;
    public bool Famel_Titan_mode_survive;
    public Conference confer;
   public static string[] bbfunc = new string[] { "no", "<i>i</i>", "<b>b</b>", "<i><b>i,b</b></i>" };
    bool[] show_set = new bool[] { false, false, false, false };
    public Vector2[] vectors2;
    public bool LavaMode;
  
    public static Font fotHUD;


    bool showUILabel_panel = false;
    bool showSetNamePror = false;
    public string text_pause;
    PhotonStatsGui statusGUI;
    public static Texture2D nya_texture;
    public bool isPlayng;
    public float sizeImage;
    int skinsott = 0;
    public LoadTextureSkin texture_skin_view;
    int playered = 0;
    string AddOnPlayerBanlist;
    int to_read_banlist = -1;
    int info_heshtable = 0;
    int ban_per_temp = 0;
    Vector2 scrolPos332;
    string pm_message = "";
    int countspanelcustom = 0;
    string script_name = "MapScript_" + UnityEngine.Random.Range(1000, 99999);
    string logic_script_name = "LogicScript_" + UnityEngine.Random.Range(1000, 99999);
    List<FileInfo> fils;
    List<FileInfo> logic;
    public string path = Application.dataPath + "/MapScripts/";
    public string pathLogic = Application.dataPath + "/LogicScripts/";
    bool is_file_exist = false;
    string filtermapScript = "";
    string filterlogicScript = "";
    int size_font_scripts = 0;
    static Texture2D[] textures_character;
    string addnickName;
    int tosettings = 0;
    bool testing = false;
    List<string> Nanimation;
    List<AnimN> myanim;
    List<Color> palitra;
    AnimN toR = null;
    AnimN.T currents;
    float timeUPD232 = 0f;
    int counnnts = 0;
    string cccurent = string.Empty;
    bool basic_version_cm;
    List<int> command = null;
    Color col_for_anim = Color.black;
    public int mymenu = 0;
    Vector2 scroolPos;
    string code = "";
    string add_diss_text = "";
    string info_diss_text = "";
    int chatting = 0;
    Conference.cnf current_cnf;
    int c_conf = 0;
    string name_conf = "CONF-" + ((int)UnityEngine.Random.Range(999, 99999)).ToString();
    string text_chat_menu = "";
    bool auto_scroll_chat_on_menu;
    GUIStyle styled_sets;
    GUIStyleState styled_statesed;
    GUIStyleState styled_stateBack;
    int to_settings_sts = 0;
    FilesHelper.MyFiles my_File;
    Vector2 scrollPos1;
    Vector2 scrollPos2;
    Vector2 scrollPos3;
    public static List<Font> font_list;
    string theme_name = "My Theme";
    public List<FileInfo> style_list;
    List<FileInfo> list_screen;
    int snap = 0;
    Vector2 scroolPos999;
    Vector2 scroolPos998;
    public static Texture2D textured_snap;
    bool full = false;
    int local_snapshots = 0;
    Texture2D texture;
    int current_panel = 0;
    static string kills_f = "0";
    static string death_f = "0";
    static string max_dmg_f = "0";
    static string total_f = "0";
    static string result_f = "";
    Texture2D texture_formils;
    public static LevelInfo lvlInfo;
    public static Grafics_Cyan_mod grafic;
    public static Grafics_Cyan_mod grafic_damage_in_min;
    int kills_last = 0;
    int damage_last = 0;
    float timer_kills_min;
    float kills_s;
    float damage_s;
    List<string> chat_Cyan = new List<string>();
    public List<PhotonPlayer> Listpaused = new List<PhotonPlayer>();
    public static Cyan_mod_bot my_bot;
    GameObject doorRacing;
    public static List<Grafics_Cyan_mod> list_grafix = new List<Grafics_Cyan_mod>();
    long last_longT;
    public static float deltaTime = 0f;
    string current_set = "";
    public bool isUpdateFT = false;
    public float qualitySlider = 0f;
    GameObject LabelScore;
    UILabel LabelScoreUI;
    public bool MenuOn = false;
    public static string ScreenshotsPath = Directory.GetCurrentDirectory() + @"\Screenshots\";

    void Awake()
    {
        instance = this;
        if (INC.onFirst)
        {
            GameObject fdfs = new GameObject("Log_Console_Cyan_mod");
            fdfs.AddComponent<DebugConsoleCyan>();
            INC.onFirst = false;
        }
        vectors2 = new Vector2[27];
        for (int i = 0; i < vectors2.Length; i++)
        {
            vectors2[i] = Vector2.zero;
        }
    }
    bool isNotDead
    {
        get
        {
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
            {
                foreach (HERO hero in heroes)
                {
                    if (hero.photonView.isMine)
                    {
                        return true;
                    }
                }
            }
            else if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                if (heroes.Count > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
    void OnDestroy()
    {
        instance = null;
        lvlInfo = null;
    }
    [RPC]
    void FriendSkin(string[] skins, PhotonMessageInfo info)
    {
        CyanSkin nskin = new CyanSkin();
        nskin.name = checkNameskin();
        nskin.horse = skins[0];
        nskin.hair = skins[1];
        nskin.eyes = skins[2];
        nskin.glass = skins[3];
        nskin.face = skins[4];
        nskin.skin = skins[5];
        nskin.costume = skins[6];
        nskin.logo_and_cape = skins[7];
        nskin.dmg_right = skins[8];
        nskin.dmg_left = skins[9];
        nskin.gas = skins[10];
        nskin.hoodie = skins[11];
        nskin.weapon_trail = skins[12];
        INC.cSkins.Add(nskin);
        InRoomChat.instance.addLINE(INC.la("added_new_skin") + info.sender.id + INC.la("and_seved_new_skin") + nskin.name);
    }

    public void addCT(COLOSSAL_TITAN titan)
    {
        this.cT.Add(titan);
        alltitans.Add(titan.gameObject);
    }
 
    public void addET(TITAN_EREN hero)
    {
        this.eT.Add(hero);
        allheroes.Add(hero.gameObject);
    }

    public void addFT(FEMALE_TITAN titan)
    {
        this.fT.Add(titan);
        alltitans.Add(titan.gameObject);
    }

    public void addHero(HERO hero)
    {
        this.heroes.Add(hero);
        allheroes.Add(hero.gameObject);
    }

    public void addHook(Bullet h)
    {
        this.hooks.Add(h);
    }

    public void addTime(float time)
    {
        this.timeTotalServer -= time;
    }

    public void addTitan(TITAN titan)
    {
        this.titans.Add(titan);
        alltitans.Add(titan.gameObject);
    }

    private void cache()
    {
        ClothFactory.ClearClothCache();
       
        this.playersRPC.Clear();
        this.titanSpawners.Clear();
        this.groundList.Clear();
        this.PreservedPlayerKDR = new Dictionary<string, int[]>();
        noRestart = false;
        skyMaterial = null;
        this.isSpawning = false;
        this.retryTime = 0f;
        logicLoaded = false;
        customLevelLoaded = true;
        this.isUnloading = false;
        this.isRecompiling = false;
        Time.timeScale = 1f;
        this.pauseWaitTime = 0f;
        this.isRestarting = false;
        if (PhotonNetwork.isMasterClient)
        {
            base.StartCoroutine(this.WaitAndResetRestarts());
        }
        if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)
        {
            this.roundTime = 0f;
            if (level.StartsWith("Custom"))
            {
                customLevelLoaded = false;
            }
            if (PhotonNetwork.isMasterClient)
            {
                if (this.isFirstLoad)
                {
                    this.setGameSettings(this.checkGameGUI());
                    ChageSetingInfo();
                }
                if (RCSettings.endlessMode > 0)
                {
                    base.StartCoroutine(this.respawnE((float)RCSettings.endlessMode));
                }
            }
            if (((int)settings[0xf4]) == 1)
            {
                InRoomChat.instance.addLINE("(" + this.roundTime.ToString("F2") + ") Round Start.");
            }
        }
        this.isFirstLoad = false;
        this.RecompilePlayerList(0.5f);
    }
   
    void Gskin(PhotonPlayer player, string text)
    {
        string str32 = text.HexDell();
        if (playergrab_skin != null && number_grabed_skin != 0 && str32 == number_grabed_skin.ToString() && playergrab_skin == player)
        {
            CyanSkin skin = new CyanSkin(player.skin);
            skin.name = checkNameskin();
            INC.cSkins.Add(skin);
            playergrab_skin = null;
            number_grabed_skin = 0;
            InRoomChat.instance.addLINE(INC.la("info_frab_add") + skin.name);
            cext.mess(INC.la("grabed_on_skin"), player);
        }
        else if (playergrab_skin != null && playergrab_skin == player && str32.ToUpper().StartsWith("N"))
        {
            InRoomChat.instance.addLINE(INC.la("info_frab_abord"));
            cext.mess(INC.la("you_abordrd_s"), player);
            playergrab_skin = null;
            number_grabed_skin = 0;
        }
    }
    void check_text(PhotonPlayer player, string text)
    {
        if ((int)FengGameManagerMKII.settings[339] != 0 && dis_text.Count > 0 && PhotonNetwork.isMasterClient && !player.isLocal)
        {
            foreach (string stext21 in dis_text)
            {
                if (stext21.Length > 2 && text.Trim().ToLower().Contains(stext21.ToLower()))
                {
                    StartCoroutine(diss_Text_iter(player, stext21));
                    player.LockPlayer = true;
                }
            }
        }
    }
    public string censure(string text)
    {
        for (int s = 0; s < f_u_c_k.Length; s++)
        {
            string stext21 = f_u_c_k[s];
            string str2 = "";
            if (stext21.Length > 2)
            {
                for (int i = 0; i < stext21.Length; i++)
                {
                    str2 = str2 + "*";
                }
                text = Regex.Replace(text, stext21, str2, RegexOptions.IgnoreCase);
            }
        }
        return text;
    }
    string chat_text_spam = "";
    string chat_name_spam = "";
    int last_player_spam = -1;
    float chat_time_spam = 0f;
    int chat_count_repire = 0;
    bool CheckSpam(string text, string namepl, int playerID)
    {
        if (text == chat_text_spam && namepl == chat_name_spam && playerID == last_player_spam && (Time.time - chat_time_spam) <= 0.01f)
        {
            chat_count_repire = chat_count_repire + 1;
            if (chat_count_repire > 4)
            {
                if (PhotonNetwork.isMasterClient)
                {
                    kickPlayerRC(PhotonPlayer.Find(playerID), false, INC.la("spammed_chat"));
                }
                return true;
            }
            return false;
        }
        else
        {
            chat_count_repire = 0;
            chat_text_spam = text;
            chat_name_spam = namepl;
            last_player_spam = playerID;
            chat_time_spam = Time.time;
            return false;
        }
      

    }
    [RPC]
    private void Chat(string content, string sender, PhotonMessageInfo info)
    {
        PhotonPlayer player = info.sender;
        if (!player.isLocal && !player.isMasterClient &&  CheckSpam(content, sender, player.ID))
        {
            return;
        }
        if (!player.LockPlayer)
        {
            string text = content;
            if (Crestiki_noliki.instance != null)
            {
                Crestiki_noliki.instance.Checked(text, player);
            }
            if ((int)FengGameManagerMKII.settings[342] == 1)
            {
                content = censure(content);
            }
            InRoomChat.instance.addLINE(content.Resize(30), sender.Resize(15), player);
            check_text(player, content + sender);
            Gskin(player, content);
            if ((int)FengGameManagerMKII.settings[392] == 1)
            {
                if (my_bot == null)
                {
                    my_bot = new GameObject().AddComponent<Cyan_mod_bot>();
                }
                if (!sender.StartsWith(Cyan_mod_bot.myName))
                {
                    my_bot.Command(text.HexDell(), player);
                }
            }
            if (InRoomChat.IsPausedFlayer && Time.timeScale <= 0.1f)
            {
                if (text.HexDell().ToLower().StartsWith("ready"))
                {
                    Listed(player);
                }
            }
        }
    }

    public void Listed(PhotonPlayer player)
    {
        if (!player.pausedFlayer)
        {
            player.pausedFlayer = true;
            int s = 0;
            foreach (PhotonPlayer pl in PhotonNetwork.playerList)
            {
                if (pl.pausedFlayer)
                {
                    s = s + 1;
                }
            }
            cext.mess("Unpaused " + s + "/" + (PhotonNetwork.playerList.Length - 1));
            if (s >= PhotonNetwork.playerList.Length - 1)
            {
                FengGameManagerMKII.instance.photonView.RPC("pauseRPC", PhotonTargets.All, new object[] { false });
            }
        }
    }
    public void FlayerPauseRecompil()
    {
       Listpaused = new List<PhotonPlayer>();
       foreach (PhotonPlayer pl in PhotonNetwork.playerList)
       {
           if (pl != null)
           {
               pl.pausedFlayer = false;
           }
       }
       FengGameManagerMKII.instance.photonView.RPC("pauseRPC", PhotonTargets.All, new object[] { true });
       cext.mess(INC.la("golosovaie_unpause"));

    }

    IEnumerator diss_Text_iter(PhotonPlayer player, string text)
    {
        cext.mess(INC.la("dis_to_pl") + text, player);
        InRoomChat.instance.addLINE(player.iscleanname + player.id + INC.la("informer_to_dis_t") + text);
        yield return new WaitForSeconds(3f);
        kickPlayerRC(player, false, INC.la("dis_t_pl") + text);
        yield break;
    }
    [RPC]
    private void ChatPM(string sender, string content, PhotonMessageInfo info)
    {
        PhotonPlayer player = info.sender;
        if (!player.isLocal && !player.isMasterClient && CheckSpam(content, sender, player.ID))
        {
            return;
        }
        if (!player.LockPlayer)
        {
            if ((int)FengGameManagerMKII.settings[342] == 1)
            {
                content = censure(content);
            }
            InRoomChat.instance.addLINE(content.Resize(30), "[FROM]" + sender.Resize(15), player);
            check_text(player, content + sender);
        }
    }

    [RPC]
    public void PrivteCyanRPC(string content, string sender, PhotonMessageInfo info)
    {
        if (chat_Cyan.Count > 30)
        {
            chat_Cyan.Remove(chat_Cyan[0]);
        }
        chat_Cyan.Add(info.sender.id + sender.HexDell() + ":" + content.HexDell());
    }
    public ExitGames.Client.Photon.Hashtable checkGameGUI()
    {
        int num;
        int num2;
        PhotonPlayer player;
        int num3;
        float num4;
        float num5;
        ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
        if (Famel_Titan_mode_survive)
        {
            hashtable.Add("annie_mode", 1);
        }

        if (((int)settings[200]) > 0)
        {
            settings[0xc0] = 0;
            settings[0xc1] = 0;
            settings[0xe2] = 0;
            settings[220] = 0;
            num = 1;
            if ((!int.TryParse((string)settings[0xc9], out num) || (num > PhotonNetwork.countOfPlayers)) || (num < 0))
            {
                settings[0xc9] = "1";
            }
            hashtable.Add("infection", num);
            if (RCSettings.infectionMode != num)
            {
                imatitan.Clear();
                for (num2 = 0; num2 < PhotonNetwork.playerList.Length; num2++)
                {
                    player = PhotonNetwork.playerList[num2];
                    player.isTitan = 1;
                }
                int length = PhotonNetwork.playerList.Length;
                num3 = num;
                for (num2 = 0; num2 < PhotonNetwork.playerList.Length; num2++)
                {
                    PhotonPlayer player2 = PhotonNetwork.playerList[num2];
                    if ((length > 0) && (UnityEngine.Random.Range((float)0f, (float)1f) <= (((float)num3) / ((float)length))))
                    {
                        player2.isTitan = 2;
                        imatitan.Add(player2.ID, 2);
                        num3--;
                    }
                    length--;
                }
            }
        }
        if (((int)settings[0xc0]) > 0)
        {
            hashtable.Add("bomb", (int)settings[0xc0]);
        }
        if (((int)settings[0xeb]) > 0)
        {
            hashtable.Add("globalDisableMinimap", (int)settings[0xeb]);
        }
        if (((int)settings[0xc1]) > 0)
        {
            hashtable.Add("team", (int)settings[0xc1]);
            if (RCSettings.teamMode != ((int)settings[0xc1]))
            {
                num3 = 1;
                for (num2 = 0; num2 < PhotonNetwork.playerList.Length; num2++)
                {
                    player = PhotonNetwork.playerList[num2];
                    switch (num3)
                    {
                        case 1:
                            base.photonView.RPC("setTeamRPC", player, new object[] { 1 });
                            num3 = 2;
                            break;

                        case 2:
                            base.photonView.RPC("setTeamRPC", player, new object[] { 2 });
                            num3 = 1;
                            break;
                    }
                }
            }
        }
        if (((int)settings[0xe2]) > 0)
        {
            num = 50;
            if ((!int.TryParse((string)settings[0xe3], out num) || (num > 0x3e8)) || (num < 0))
            {
                settings[0xe3] = "50";
            }
            hashtable.Add("point", num);
        }
        if (((int)settings[0xc2]) > 0)
        {
            hashtable.Add("rock", (int)settings[0xc2]);
        }
        if (((int)settings[0xc3]) > 0)
        {
            num = 30;
            if ((!int.TryParse((string)settings[0xc4], out num) || (num > 100)) || (num < 0))
            {
                settings[0xc4] = "30";
            }
            hashtable.Add("explode", num);
        }
        if (((int)settings[0xc5]) > 0)
        {
            int result = 100;
            int num8 = 200;
            if ((!int.TryParse((string)settings[0xc6], out result) || (result > 0x186a0)) || (result < 0))
            {
                settings[0xc6] = "100";
            }
            if ((!int.TryParse((string)settings[0xc7], out num8) || (num8 > 0x186a0)) || (num8 < 0))
            {
                settings[0xc7] = "200";
            }
            hashtable.Add("healthMode", (int)settings[0xc5]);
            hashtable.Add("healthLower", result);
            hashtable.Add("healthUpper", num8);
        }
        if (((int)settings[0xca]) > 0)
        {
            hashtable.Add("eren", (int)settings[0xca]);
        }
        if (((int)settings[0xcb]) > 0)
        {
            num = 1;
            if ((!int.TryParse((string)settings[0xcc], out num) || (num > 50)) || (num < 0))
            {
                settings[0xcc] = "1";
            }
            hashtable.Add("titanc", num);
        }
        if (((int)settings[0xcd]) > 0)
        {
            num = 0x3e8;
            if ((!int.TryParse((string)settings[0xce], out num) || (num > 0x186a0)) || (num < 0))
            {
                settings[0xce] = "1000";
            }
            hashtable.Add("damage", num);
        }
        if (((int)settings[0xcf]) > 0)
        {
            num4 = 1f;
            num5 = 3f;
            if ((!float.TryParse((string)settings[0xd0], out num4) || (num4 > 100f)) || (num4 < 0f))
            {
                settings[0xd0] = "1.0";
            }
            if ((!float.TryParse((string)settings[0xd1], out num5) || (num5 > 100f)) || (num5 < 0f))
            {
                settings[0xd1] = "3.0";
            }
            hashtable.Add("sizeMode", (int)settings[0xcf]);
            hashtable.Add("sizeLower", num4);
            hashtable.Add("sizeUpper", num5);
        }
        if (((int)settings[210]) > 0)
        {
            num4 = 20f;
            num5 = 20f;
            float num9 = 20f;
            float num10 = 20f;
            float num11 = 20f;
            if (!float.TryParse((string)settings[0xd3], out num4) || (num4 < 0f))
            {
                settings[0xd3] = "20.0";
            }
            if (!float.TryParse((string)settings[0xd4], out num5) || (num5 < 0f))
            {
                settings[0xd4] = "20.0";
            }
            if (!float.TryParse((string)settings[0xd5], out num9) || (num9 < 0f))
            {
                settings[0xd5] = "20.0";
            }
            if (!float.TryParse((string)settings[0xd6], out num10) || (num10 < 0f))
            {
                settings[0xd6] = "20.0";
            }
            if (!float.TryParse((string)settings[0xd7], out num11) || (num11 < 0f))
            {
                settings[0xd7] = "20.0";
            }
            if (((((num4 + num5) + num9) + num10) + num11) > 100f)
            {
                settings[0xd3] = "20.0";
                settings[0xd4] = "20.0";
                settings[0xd5] = "20.0";
                settings[0xd6] = "20.0";
                settings[0xd7] = "20.0";
                num4 = 20f;
                num5 = 20f;
                num9 = 20f;
                num10 = 20f;
                num11 = 20f;
            }
            hashtable.Add("spawnMode", (int)settings[210]);
            hashtable.Add("nRate", num4);
            hashtable.Add("aRate", num5);
            hashtable.Add("jRate", num9);
            hashtable.Add("cRate", num10);
            hashtable.Add("pRate", num11);
        }
        if (((int)settings[0xd8]) > 0)
        {
            hashtable.Add("horse", (int)settings[0xd8]);
        }
        if (((int)settings[0xd9]) > 0)
        {
            num = 1;
            if (!int.TryParse((string)settings[0xda], out num) || (num > 50))
            {
                settings[0xda] = "1";
            }
            hashtable.Add("waveModeOn", (int)settings[0xd9]);
            hashtable.Add("waveModeNum", num);
        }
        if (((int)settings[0xdb]) > 0)
        {
            hashtable.Add("friendly", (int)settings[0xdb]);
        }
        if (((int)settings[220]) > 0)
        {
            hashtable.Add("pvp", (int)settings[220]);
        }
        if (((int)settings[0xdd]) > 0)
        {
            num = 20;
            if ((!int.TryParse((string)settings[0xde], out num) || (num > 0xf4240)) || (num < 0))
            {
                settings[0xde] = "20";
            }
            hashtable.Add("maxwave", num);
        }
        if (((int)settings[0xdf]) > 0)
        {
            num = 5;
            if ((!int.TryParse((string)settings[0xe0], out num) || (num > 0xf4240)) || (num < 5))
            {
                settings[0xe0] = "5";
            }
            hashtable.Add("endless", num);
        }
        if (((string)settings[0xe1]) != string.Empty)
        {
            hashtable.Add("motd", (string)settings[0xe1]);
        }
        if (((int)settings[0xe4]) > 0)
        {
            hashtable.Add("ahssReload", (int)settings[0xe4]);
        }
        if (((int)settings[0xe5]) > 0)
        {
            hashtable.Add("punkWaves", (int)settings[0xe5]);
        }
        if (((int)settings[0x105]) > 0)
        {
            hashtable.Add("deadlycannons", (int)settings[0x105]);
        }
        if (RCSettings.racingStatic > 0)
        {
            hashtable.Add("asoracing", 1);
        }
        return hashtable;
    }

    public bool checkIsTitanAllDie()
    {
        foreach (GameObject obj2 in alltitans)
        {
            TITAN tit = obj2.GetComponent<TITAN>();
            if ((tit != null) && !tit.hasDie)
            {
                return false;
            }
            FEMALE_TITAN ft = obj2.GetComponent<FEMALE_TITAN>();
            if (ft != null && !ft.hasDie)
            {
                return false;
            }
        }
        return true;
    }

    public void checkPVPpts()
    {
        if (this.PVPtitanScore >= this.PVPtitanScoreMax)
        {
            this.PVPtitanScore = this.PVPtitanScoreMax;
            this.gameLose2();
        }
        else if (this.PVPhumanScore >= this.PVPhumanScoreMax)
        {
            this.PVPhumanScore = this.PVPhumanScoreMax;
            this.gameWin2();
        }
    }

    [RPC]
    private void clearlevel(string[] link, int gametype, PhotonMessageInfo info)
    {
        if (info.sender.isMasterClient)
        {
            if (gametype == 0)
            {
                IN_GAME_MAIN_CAMERA.gamemode = GAMEMODE.KILL_TITAN;
            }
            else if (gametype == 1)
            {
                IN_GAME_MAIN_CAMERA.gamemode = GAMEMODE.SURVIVE_MODE;
            }
            else if (gametype == 2)
            {
                IN_GAME_MAIN_CAMERA.gamemode = GAMEMODE.PVP_AHSS;
            }
            else if (gametype == 3)
            {
                IN_GAME_MAIN_CAMERA.gamemode = GAMEMODE.RACING;
            }
            else if (gametype == 4)
            {
                IN_GAME_MAIN_CAMERA.gamemode = GAMEMODE.None;
            }
            if (info.sender.isMasterClient && (link.Length > 6))
            {
                base.StartCoroutine(this.clearlevelE(link));
            }
        }
    }

    private IEnumerator clearlevelE(string[] skybox)
    {
        string key = skybox[6];
        bool mipmap = true;
        bool iteratorVariable2 = false;
        if (((int)settings[0x3f]) == 1)
        {
            mipmap = false;
        }
        if ((((skybox[0] != string.Empty) || (skybox[1] != string.Empty)) || ((skybox[2] != string.Empty) || (skybox[3] != string.Empty))) || ((skybox[4] != string.Empty) || (skybox[5] != string.Empty)))
        {
            string iteratorVariable3 = string.Join(",", skybox);
            if (!linkHash[1].ContainsKey(iteratorVariable3))
            {
                iteratorVariable2 = true;
                Material material = Camera.main.GetComponent<Skybox>().material;
                string url = skybox[0];
                string iteratorVariable6 = skybox[1];
                string iteratorVariable7 = skybox[2];
                string iteratorVariable8 = skybox[3];
                string iteratorVariable9 = skybox[4];
                string iteratorVariable10 = skybox[5];
                if ((url.EndsWith(".jpg") || url.EndsWith(".png")) || url.EndsWith(".jpeg"))
                {
                    WWW link = new WWW(url);
                    yield return link;
                    Texture2D texture = cext.loadimage(link, mipmap, 0x7a120);
                    link.Dispose();
                    material.SetTexture("_FrontTex", texture);
                }
                if ((iteratorVariable6.EndsWith(".jpg") || iteratorVariable6.EndsWith(".png")) || iteratorVariable6.EndsWith(".jpeg"))
                {
                    WWW iteratorVariable13 = new WWW(iteratorVariable6);
                    yield return iteratorVariable13;
                    Texture2D iteratorVariable14 = cext.loadimage(iteratorVariable13, mipmap, 0x7a120);
                    iteratorVariable13.Dispose();
                    material.SetTexture("_BackTex", iteratorVariable14);
                }
                if ((iteratorVariable7.EndsWith(".jpg") || iteratorVariable7.EndsWith(".png")) || iteratorVariable7.EndsWith(".jpeg"))
                {
                    WWW iteratorVariable15 = new WWW(iteratorVariable7);
                    yield return iteratorVariable15;
                    Texture2D iteratorVariable16 = cext.loadimage(iteratorVariable15, mipmap, 0x7a120);
                    iteratorVariable15.Dispose();
                    material.SetTexture("_LeftTex", iteratorVariable16);
                }
                if ((iteratorVariable8.EndsWith(".jpg") || iteratorVariable8.EndsWith(".png")) || iteratorVariable8.EndsWith(".jpeg"))
                {
                    WWW iteratorVariable17 = new WWW(iteratorVariable8);
                    yield return iteratorVariable17;
                    Texture2D iteratorVariable18 = cext.loadimage(iteratorVariable17, mipmap, 0x7a120);
                    iteratorVariable17.Dispose();
                    material.SetTexture("_RightTex", iteratorVariable18);
                }
                if ((iteratorVariable9.EndsWith(".jpg") || iteratorVariable9.EndsWith(".png")) || iteratorVariable9.EndsWith(".jpeg"))
                {
                    WWW iteratorVariable19 = new WWW(iteratorVariable9);
                    yield return iteratorVariable19;
                    Texture2D iteratorVariable20 = cext.loadimage(iteratorVariable19, mipmap, 0x7a120);
                    iteratorVariable19.Dispose();
                    material.SetTexture("_UpTex", iteratorVariable20);
                }
                if ((iteratorVariable10.EndsWith(".jpg") || iteratorVariable10.EndsWith(".png")) || iteratorVariable10.EndsWith(".jpeg"))
                {
                    WWW iteratorVariable21 = new WWW(iteratorVariable10);
                    yield return iteratorVariable21;
                    Texture2D iteratorVariable22 = cext.loadimage(iteratorVariable21, mipmap, 0x7a120);
                    iteratorVariable21.Dispose();
                    material.SetTexture("_DownTex", iteratorVariable22);
                }
                Camera.main.GetComponent<Skybox>().material = material;
                linkHash[1].Add(iteratorVariable3, material);
                skyMaterial = material;
            }
            else
            {
                Camera.main.GetComponent<Skybox>().material = (Material)linkHash[1][iteratorVariable3];
                skyMaterial = (Material)linkHash[1][iteratorVariable3];
            }
        }
        if ((key.EndsWith(".jpg") || key.EndsWith(".png")) || key.EndsWith(".jpeg"))
        {
            foreach (GameObject iteratorVariable23 in this.groundList)
            {
                if ((iteratorVariable23 != null) && (iteratorVariable23.renderer != null))
                {
                    foreach (Renderer iteratorVariable24 in iteratorVariable23.GetComponentsInChildren<Renderer>())
                    {
                        if (!linkHash[0].ContainsKey(key))
                        {
                            WWW iteratorVariable25 = new WWW(key);
                            yield return iteratorVariable25;
                            Texture2D iteratorVariable26 = cext.loadimage(iteratorVariable25, mipmap, 0x30d40);
                            iteratorVariable25.Dispose();
                            if (!linkHash[0].ContainsKey(key))
                            {
                                iteratorVariable2 = true;
                                iteratorVariable24.material.mainTexture = iteratorVariable26;
                                linkHash[0].Add(key, iteratorVariable24.material);
                                iteratorVariable24.material = (Material)linkHash[0][key];
                            }
                            else
                            {
                                iteratorVariable24.material = (Material)linkHash[0][key];
                            }
                        }
                        else
                        {
                            iteratorVariable24.material = (Material)linkHash[0][key];
                        }
                    }
                }
            }
        }
        else if (key.ToLower() == "transparent")
        {
            foreach (GameObject obj2 in this.groundList)
            {
                if ((obj2 != null) && (obj2.renderer != null))
                {
                    foreach (Renderer renderer in obj2.GetComponentsInChildren<Renderer>())
                    {
                        renderer.enabled = false;
                    }
                }
            }
        }
        if (iteratorVariable2)
        {
            this.unloadAssets();
        }
        yield break;
    }

    public void compileScript(string str)
    {
        int num;
        string[] strArray = str.Replace(" ", string.Empty).Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
        int num2 = 0;
        int num3 = 0;
        bool flag = false;
        for (num = 0; num < strArray.Length; num++)
        {
            if (strArray[num] == "{")
            {
                num2++;
            }
            else if (strArray[num] == "}")
            {
                num3++;
            }
            else
            {
                int num4 = 0;
                int num5 = 0;
                int num6 = 0;
                foreach (char ch in strArray[num])
                {
                    switch (ch)
                    {
                        case '(':
                            num4++;
                            break;

                        case ')':
                            num5++;
                            break;

                        case '"':
                            num6++;
                            break;
                    }
                }
                if (num4 != num5)
                {
                    InRoomChat.instance.addLINE("Script Error: Parentheses not equal! (line " + ((num + 1)).ToString() + ")");
                    flag = true;
                }
                if ((num6 % 2) != 0)
                {
                    InRoomChat.instance.addLINE("Script Error: Quotations not equal! (line " + ((num + 1)).ToString() + ")");
                    flag = true;
                }
            }
        }
        if (num2 != num3)
        {
            InRoomChat.instance.addLINE("Script Error: Bracket count not equivalent!");
            flag = true;
        }
        if (!flag)
        {
            try
            {
                int num8;
                num = 0;
                while (num < strArray.Length)
                {
                    if (strArray[num].StartsWith("On") && (strArray[num + 1] == "{"))
                    {
                        int key = num;
                        num8 = num + 2;
                        int num10 = 0;
                        for (int i = num + 2; i < strArray.Length; i++)
                        {
                            if (strArray[i] == "{")
                            {
                                num10++;
                            }
                            if (strArray[i] == "}")
                            {
                                if (num10 > 0)
                                {
                                    num10--;
                                }
                                else
                                {
                                    num8 = i - 1;
                                    i = strArray.Length;
                                }
                            }
                        }
                        hashtable.Add(key, num8);
                        num = num8;
                    }
                    num++;
                }
                foreach (int num12 in hashtable.Keys)
                {
                    int num13;
                    int num14;
                    string str2;
                    string str3;
                    RegionTrigger trigger;
                    string str4 = strArray[num12];
                    num8 = (int)hashtable[num12];
                    string[] stringArray = new string[(num8 - num12) + 1];
                    int index = 0;
                    for (num = num12; num <= num8; num++)
                    {
                        stringArray[index] = strArray[num];
                        index++;
                    }
                    RCEvent event2 = this.parseBlock(stringArray, 0, 0, null);
                    if (str4.StartsWith("OnPlayerEnterRegion"))
                    {
                        num13 = str4.IndexOf('[');
                        num14 = str4.IndexOf(']');
                        str2 = str4.Substring(num13 + 2, (num14 - num13) - 3);
                        num13 = str4.IndexOf('(');
                        num14 = str4.IndexOf(')');
                        str3 = str4.Substring(num13 + 2, (num14 - num13) - 3);
                        if (RCRegionTriggers.ContainsKey(str2))
                        {
                            trigger = (RegionTrigger)RCRegionTriggers[str2];
                            trigger.playerEventEnter = event2;
                            trigger.myName = str2;
                            RCRegionTriggers[str2] = trigger;
                        }
                        else
                        {
                            trigger = new RegionTrigger
                            {
                                playerEventEnter = event2,
                                myName = str2
                            };
                            RCRegionTriggers.Add(str2, trigger);
                        }
                        RCVariableNames.Add("OnPlayerEnterRegion[" + str2 + "]", str3);
                    }
                    else if (str4.StartsWith("OnPlayerLeaveRegion"))
                    {
                        num13 = str4.IndexOf('[');
                        num14 = str4.IndexOf(']');
                        str2 = str4.Substring(num13 + 2, (num14 - num13) - 3);
                        num13 = str4.IndexOf('(');
                        num14 = str4.IndexOf(')');
                        str3 = str4.Substring(num13 + 2, (num14 - num13) - 3);
                        if (RCRegionTriggers.ContainsKey(str2))
                        {
                            trigger = (RegionTrigger)RCRegionTriggers[str2];
                            trigger.playerEventExit = event2;
                            trigger.myName = str2;
                            RCRegionTriggers[str2] = trigger;
                        }
                        else
                        {
                            trigger = new RegionTrigger
                            {
                                playerEventExit = event2,
                                myName = str2
                            };
                            RCRegionTriggers.Add(str2, trigger);
                        }
                        RCVariableNames.Add("OnPlayerExitRegion[" + str2 + "]", str3);
                    }
                    else if (str4.StartsWith("OnTitanEnterRegion"))
                    {
                        num13 = str4.IndexOf('[');
                        num14 = str4.IndexOf(']');
                        str2 = str4.Substring(num13 + 2, (num14 - num13) - 3);
                        num13 = str4.IndexOf('(');
                        num14 = str4.IndexOf(')');
                        str3 = str4.Substring(num13 + 2, (num14 - num13) - 3);
                        if (RCRegionTriggers.ContainsKey(str2))
                        {
                            trigger = (RegionTrigger)RCRegionTriggers[str2];
                            trigger.titanEventEnter = event2;
                            trigger.myName = str2;
                            RCRegionTriggers[str2] = trigger;
                        }
                        else
                        {
                            trigger = new RegionTrigger
                            {
                                titanEventEnter = event2,
                                myName = str2
                            };
                            RCRegionTriggers.Add(str2, trigger);
                        }
                        RCVariableNames.Add("OnTitanEnterRegion[" + str2 + "]", str3);
                    }
                    else if (str4.StartsWith("OnTitanLeaveRegion"))
                    {
                        num13 = str4.IndexOf('[');
                        num14 = str4.IndexOf(']');
                        str2 = str4.Substring(num13 + 2, (num14 - num13) - 3);
                        num13 = str4.IndexOf('(');
                        num14 = str4.IndexOf(')');
                        str3 = str4.Substring(num13 + 2, (num14 - num13) - 3);
                        if (RCRegionTriggers.ContainsKey(str2))
                        {
                            trigger = (RegionTrigger)RCRegionTriggers[str2];
                            trigger.titanEventExit = event2;
                            trigger.myName = str2;
                            RCRegionTriggers[str2] = trigger;
                        }
                        else
                        {
                            trigger = new RegionTrigger
                            {
                                titanEventExit = event2,
                                myName = str2
                            };
                            RCRegionTriggers.Add(str2, trigger);
                        }
                        RCVariableNames.Add("OnTitanExitRegion[" + str2 + "]", str3);
                    }
                    else if (str4.StartsWith("OnFirstLoad()"))
                    {
                        RCEvents.Add("OnFirstLoad", event2);
                    }
                    else if (str4.StartsWith("OnRoundStart()"))
                    {
                        RCEvents.Add("OnRoundStart", event2);
                    }
                    else if (str4.StartsWith("OnUpdate()"))
                    {
                        RCEvents.Add("OnUpdate", event2);
                    }
                    else
                    {
                        string[] strArray3;
                        if (str4.StartsWith("OnTitanDie"))
                        {
                            num13 = str4.IndexOf('(');
                            num14 = str4.LastIndexOf(')');
                            strArray3 = str4.Substring(num13 + 1, (num14 - num13) - 1).Split(new char[] { ',' });
                            strArray3[0] = strArray3[0].Substring(1, strArray3[0].Length - 2);
                            strArray3[1] = strArray3[1].Substring(1, strArray3[1].Length - 2);
                            RCVariableNames.Add("OnTitanDie", strArray3);
                            RCEvents.Add("OnTitanDie", event2);
                        }
                        else if (str4.StartsWith("OnPlayerDieByTitan"))
                        {
                            RCEvents.Add("OnPlayerDieByTitan", event2);
                            num13 = str4.IndexOf('(');
                            num14 = str4.LastIndexOf(')');
                            strArray3 = str4.Substring(num13 + 1, (num14 - num13) - 1).Split(new char[] { ',' });
                            strArray3[0] = strArray3[0].Substring(1, strArray3[0].Length - 2);
                            strArray3[1] = strArray3[1].Substring(1, strArray3[1].Length - 2);
                            RCVariableNames.Add("OnPlayerDieByTitan", strArray3);
                        }
                        else if (str4.StartsWith("OnPlayerDieByPlayer"))
                        {
                            RCEvents.Add("OnPlayerDieByPlayer", event2);
                            num13 = str4.IndexOf('(');
                            num14 = str4.LastIndexOf(')');
                            strArray3 = str4.Substring(num13 + 1, (num14 - num13) - 1).Split(new char[] { ',' });
                            strArray3[0] = strArray3[0].Substring(1, strArray3[0].Length - 2);
                            strArray3[1] = strArray3[1].Substring(1, strArray3[1].Length - 2);
                            RCVariableNames.Add("OnPlayerDieByPlayer", strArray3);
                        }
                        else if (str4.StartsWith("OnChatInput"))
                        {
                            RCEvents.Add("OnChatInput", event2);
                            num13 = str4.IndexOf('(');
                            num14 = str4.LastIndexOf(')');
                            str3 = str4.Substring(num13 + 1, (num14 - num13) - 1);
                            RCVariableNames.Add("OnChatInput", str3.Substring(1, str3.Length - 2));
                        }
                    }
                }
            }
            catch (UnityException exception)
            {
                InRoomChat.instance.addLINE(exception.Message);
            }
        }
    }

    public int conditionType(string str)
    {
        if (!str.StartsWith("Int"))
        {
            if (str.StartsWith("Bool"))
            {
                return 1;
            }
            if (str.StartsWith("String"))
            {
                return 2;
            }
            if (str.StartsWith("Float"))
            {
                return 3;
            }
            if (str.StartsWith("Titan"))
            {
                return 5;
            }
            if (str.StartsWith("Player"))
            {
                return 4;
            }
        }
        return 0;
    }

    float calculate_overal(int a, int b, Grafics_Cyan_mod grafic)
    {
        if (a != b && grafic != null)
        {
            int s = (b - a);
            if (s > 0)
            {
                grafic.value = grafic.value + s;
            }
            return grafic.value / 120;
        }
        return 0;
    }
    void update_grafix()
    {
        if (grafic != null || grafic_damage_in_min != null)
        {
            if (grafic != null)
            {
                FengGameManagerMKII.settings[358] = new Vector2(grafic.rect.x, grafic.rect.y);
            }
            if (grafic_damage_in_min != null)
            {
                FengGameManagerMKII.settings[359] = new Vector2(grafic_damage_in_min.rect.x, grafic_damage_in_min.rect.y);
            }
            bool isddd = grafic.stoped = grafic_damage_in_min.stoped = isNotDead;
            if (isddd)
            {
                float s = calculate_overal(kills_last, single_kills, grafic);
                if (s > 0f)
                {
                    kills_s = s;
                    kills_last = single_kills;
                }
                float b = calculate_overal(damage_last, single_totalDamage, grafic_damage_in_min);
                if (b > 0f)
                {
                    damage_s = b;
                    damage_last = single_totalDamage;
                }

                timer_kills_min += Time.deltaTime;
                if (timer_kills_min >= 0.5f)
                {
                    if (grafic != null && kills_s > 0)
                    {
                        timer_kills_min = 0;
                        grafic.value = grafic.value - kills_s;
                        if (grafic.value < 0)
                        {
                            grafic.value = 0;
                            kills_s = 0;
                        }
                        if (grafic.isMaximal < grafic.value)
                        {
                            grafic.isMaximal = grafic.value;
                        }
                        grafic.value2 = single_kills / (timeTotalServer / 60);
                        if (grafic.isMaximal2 < grafic.value2)
                        {
                            grafic.isMaximal2 = grafic.value2;
                        }
                    }
                    if (grafic_damage_in_min != null && damage_s > 0)
                    {
                        grafic_damage_in_min.value = grafic_damage_in_min.value - damage_s;
                        if (grafic_damage_in_min.value < 0)
                        {
                            grafic_damage_in_min.value = 0;
                            damage_s = 0;
                        }
                        if (grafic_damage_in_min.isMaximal < grafic_damage_in_min.value)
                        {
                            grafic_damage_in_min.isMaximal = grafic_damage_in_min.value;
                        }

                        grafic_damage_in_min.value2 = single_totalDamage / (timeTotalServer / 60);
                        if (grafic_damage_in_min.isMaximal2 < grafic_damage_in_min.value2)
                        {
                            grafic_damage_in_min.isMaximal2 = grafic_damage_in_min.value2;
                        }
                    }
                }
            }
        }
    }
    GameObject findDoorRacing
    {
        get
        {
            if (doorRacing != null)
            {
                return doorRacing;
            }
            return doorRacing = GameObject.Find("door");
        }
    }
    
    private void core2()
    {

        if ((int)FengGameManagerMKII.settings[64] >= 100)
        {
            this.coreeditor();
        }
        else
        {
            if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE && this.needChooseSide)
            {
                if (FengGameManagerMKII.inputRC.isInputCustomKeyDown(InputCode.custom_flare1))
                {
                    if (PanelSetHeroCustom.activet)
                    {
                        Screen.lockCursor = true;
                        Screen.showCursor = true;
                        NGUITools.SetActive(uiT.panels[0], true);
                        NGUITools.SetActive(uiT.panels[1], false);
                        NGUITools.SetActive(uiT.panels[2], false);
                        if (PanelSetHeroCustom.instance == null)
                        {
                            base.gameObject.AddComponent<PanelSetHeroCustom>();
                        }
                        PanelSetHeroCustom.instance.enabled = false;
                        IN_GAME_MAIN_CAMERA.instance.Smov.disable = false;
                        IN_GAME_MAIN_CAMERA.instance.mouselook.disable = false;
                    }
                    else
                    {
                        Screen.lockCursor = false;
                        Screen.showCursor = true;
                        NGUITools.SetActive(uiT.panels[0], false);
                        NGUITools.SetActive(uiT.panels[1], false);
                        NGUITools.SetActive(uiT.panels[2], false);
                        if (PanelSetHeroCustom.instance == null)
                        {
                            base.gameObject.AddComponent<PanelSetHeroCustom>();
                        }
                        PanelSetHeroCustom.instance.enabled = true;
                        IN_GAME_MAIN_CAMERA.instance.Smov.disable = true;
                        IN_GAME_MAIN_CAMERA.instance.mouselook.disable = false;
                    }
                }
                if (FengGameManagerMKII.inputRC.isInputCustomKeyDown(InputCode.custom_pause) && ! FengGameManagerMKII.instance.MenuOn)
                {
                    Screen.showCursor = true;
                    Screen.lockCursor = false;
                    IN_GAME_MAIN_CAMERA.instance.Smov.disable = true;
                    IN_GAME_MAIN_CAMERA.instance.mouselook.disable = true;
                     FengGameManagerMKII.instance.MenuOn = true;
                }
            }
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE || IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
            {
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                {
                    IN_GAME_MAIN_CAMERA camm23 = Camera.main.GetComponent<IN_GAME_MAIN_CAMERA>();
                    this.coreadd();
                    if (IN_GAME_MAIN_CAMERA.instance != null && IN_GAME_MAIN_CAMERA.gamemode != GAMEMODE.RACING && camm23.gameOver && !this.needChooseSide && (int)FengGameManagerMKII.settings[245] == 0)
                    {
                        if (LabelCenter)
                        {
                            this.ShowHUDInfoCenter(
                        LangCore.lang.press + " <color=#00FF00>" + (string)FengGameManagerMKII.settings[426] + "</color> " + LangCore.lang.next_player + "\n" +
                          LangCore.lang.press + " <color=#00FF00>" + (string)FengGameManagerMKII.settings[427] + "</color> " + LangCore.lang.prev_player + "\n" +
                          LangCore.lang.press + " <color=#00FF00>" + (string)FengGameManagerMKII.settings[421] + "</color> " + LangCore.lang.to_spectator + "\n\n\n\n");

                        }

                        if (lvlInfo.respawnMode == RespawnMode.DEATHMATCH || RCSettings.endlessMode > 0 || ((RCSettings.bombMode == 1 || RCSettings.pvpMode > 0) && RCSettings.pointMode > 0))
                        {
                            this.myRespawnTime += Time.deltaTime;
                            int num = 5;
                            if ((PhotonNetwork.player.isTitan) == 2)
                            {
                                num = 10;
                            }
                            if (RCSettings.endlessMode > 0)
                            {
                                num = RCSettings.endlessMode;
                            }
                            if (LabelCenter)
                            {
                                this.ShowHUDInfoCenterADD(LangCore.lang.respawn_in + (num - (int)this.myRespawnTime).ToString() + LangCore.lang.second);
                            }
                            if (this.myRespawnTime > (float)num)
                            {
                                this.myRespawnTime = 0f;
                                camm23.gameOver = false;
                                if ((PhotonNetwork.player.isTitan) == 2)
                                {
                                    this.SpawnNonAITitan(this.myLastHero, "titanRespawn");
                                }
                                else
                                {
                                    base.StartCoroutine(this.WaitAndRespawn1(0.1f, this.myLastRespawnTag));
                                }
                                camm23.gameOver = false;
                                if (LabelCenter)
                                {
                                    this.ShowHUDInfoCenter(string.Empty);
                                }
                            }
                        }
                    }
                }
                else if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                {
                    if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.RACING)
                    {
                        if (!this.isLosing)
                        {
                            if (IN_GAME_MAIN_CAMERA.instance != null)
                            {
                                this.currentSpeed = IN_GAME_MAIN_CAMERA.instance.main_objectR.velocity.magnitude;
                            }
                            this.maxSpeed = Mathf.Max(this.maxSpeed, this.currentSpeed);
                            if (LabelTopLeft)
                            {
                                this.ShowHUDInfoTopLeft(LangCore.lang.current_speed + (int)this.currentSpeed + "\n" + LangCore.lang.max_sped + this.maxSpeed);

                            }
                        }
                    }
                    else
                    {
                        if (LabelTopLeft)
                        {
                            this.ShowHUDInfoTopLeft(LangCore.lang.kills + this.single_kills + "\n" + LangCore.lang.max_damage + this.single_maxDamage + "\n" + LangCore.lang.total_dmg + this.single_totalDamage);
                        }
                        update_grafix();
                    }
                }
                if (this.isLosing && IN_GAME_MAIN_CAMERA.gamemode != GAMEMODE.RACING)
                {
                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                    {
                        if (LabelCenter)
                        {
                            if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)
                            {

                                this.ShowHUDInfoCenter(LangCore.lang.survive + this.wave + LangCore.lang.wave + "\n" + LangCore.lang.press + " <color=#00FF00>" + (string)FengGameManagerMKII.settings[438] + "</color> " + LangCore.lang.to_rest + "\n\n\n");

                            }
                            else
                            {
                                this.ShowHUDInfoCenter(LangCore.lang.humanity_fail + "\n" + LangCore.lang.press + " <color=#00FF00>" + (string)FengGameManagerMKII.settings[438] + "</color> " + LangCore.lang.to_rest + "\n\n\n");
                            }
                        }
                    }
                    else
                    {
                        if (LabelCenter)
                        {
                            if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)
                            {
                                this.ShowHUDInfoCenter(LangCore.lang.survive + this.wave + LangCore.lang.wave + "\n" + LangCore.lang.game_rest_in + (int)this.gameEndCD + LangCore.lang.second + "\n\n");
                            }
                            else
                            {
                                this.ShowHUDInfoCenter(LangCore.lang.humanity_fail + "\n" + LangCore.lang.game_rest_in + (int)this.gameEndCD + LangCore.lang.second + "\n\n");
                            }
                        }
                        if (this.gameEndCD <= 0f)
                        {
                            this.gameEndCD = 0f;
                            if (PhotonNetwork.isMasterClient)
                            {
                                this.restartRC();
                            }

                            this.ShowHUDInfoCenter(string.Empty);
                        }
                        else
                        {
                            this.gameEndCD -= Time.deltaTime;
                        }
                    }
                }
                if (this.isWinning)
                {
                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                    {
                        if (LabelCenter)
                        {
                            if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.RACING)
                            {
                                this.ShowHUDInfoCenter(((float)((int)(this.timeTotalServer * 10f)) * 0.1f - 5f).ToString() + LangCore.lang.second + "\n" + LangCore.lang.press + " <color=#00FF00>" + (string)FengGameManagerMKII.settings[438] + "</color> " + LangCore.lang.to_rest + "\n\n\n");
                            }
                            else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)
                            {
                                this.ShowHUDInfoCenter(LangCore.lang.survive_all_waves + "\n" + LangCore.lang.press + " <color=#00FF00>" + (string)FengGameManagerMKII.settings[438] + "</color> " + LangCore.lang.to_rest + "\n\n\n");
                            }
                            else
                            {
                                this.ShowHUDInfoCenter(LangCore.lang.human_win + "\n" + LangCore.lang.press + " <color=#00FF00>" + (string)FengGameManagerMKII.settings[438] + "</color> " + LangCore.lang.to_rest + "\n\n\n");
                            }
                        }
                    }
                    else
                    {
                        if (LabelCenter)
                        {
                            if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.RACING)
                            {
                                this.ShowHUDInfoCenter(this.localRacingResult.toHex() + "\n\n" + LangCore.lang.game_rest_in + (int)this.gameEndCD + LangCore.lang.second);
                            }
                            else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)
                            {
                                this.ShowHUDInfoCenter(LangCore.lang.survive_all_waves + "\n" + LangCore.lang.game_rest_in + (int)this.gameEndCD + LangCore.lang.second + "\n\n");
                            }
                            else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_AHSS)
                            {
                                if (RCSettings.pvpMode == 0 && RCSettings.bombMode == 0)
                                {
                                    this.ShowHUDInfoCenter(LangCore.lang.team + this.teamWinner + LangCore.lang.win + "\n" + LangCore.lang.game_rest_in + (int)this.gameEndCD + LangCore.lang.second + "\n\n");
                                }
                                else
                                {
                                    this.ShowHUDInfoCenter(LangCore.lang.round_end + "\n" + LangCore.lang.game_rest_in + (int)this.gameEndCD + LangCore.lang.second + "\n\n");
                                }
                            }
                            else
                            {
                                this.ShowHUDInfoCenter(LangCore.lang.human_win + "\n" + LangCore.lang.game_rest_in + (int)this.gameEndCD + LangCore.lang.second + "\n\n");
                            }
                        }
                        if (this.gameEndCD <= 0f)
                        {
                            this.gameEndCD = 0f;
                            if (PhotonNetwork.isMasterClient)
                            {
                                this.restartRC();
                            }
                            this.ShowHUDInfoCenter(string.Empty);
                        }
                        else
                        {
                            this.gameEndCD -= Time.deltaTime;
                        }
                    }
                }
                this.timeElapse += Time.deltaTime;
                this.roundTime += Time.deltaTime;
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                {
                    if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.RACING)
                    {
                        if (!this.isWinning)
                        {
                            this.timeTotalServer += Time.deltaTime;
                        }
                    }
                    else if (!this.isLosing && !this.isWinning)
                    {
                        this.timeTotalServer += Time.deltaTime;
                    }
                }
                else
                {
                    this.timeTotalServer += Time.deltaTime;
                }
                if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.RACING)
                {
                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                    {
                        if (LabelTopCenter)
                        {
                            if (!this.isWinning)
                            {
                                this.ShowHUDInfoTopCenter(LangCore.lang.time + ((float)((int)(this.timeTotalServer * 10f)) * 0.1f - 5f));
                            }
                        }
                        if (this.timeTotalServer < 5f)
                        {
                            if (LabelCenter)
                            {
                                this.ShowHUDInfoCenter(LangCore.lang.race_start_in + (int)(5f - this.timeTotalServer) + LangCore.lang.second);
                            }
                        }
                        else if (!this.startRacing)
                        {
                            if (LabelCenter)
                            {
                                this.ShowHUDInfoCenter(string.Empty);
                            }
                            this.startRacing = true;
                            this.endRacing = false;
                            GameObject obj = findDoorRacing;
                            if (obj != null)
                            {
                                obj.SetActive(false);
                            }
                        }
                    }
                    else
                    {
                        if (LabelTopCenter)
                        {
                            this.ShowHUDInfoTopCenter(LangCore.lang.time + ((this.roundTime >= 20f) ? ((float)((int)(this.roundTime * 10f)) * 0.1f - 20f).ToString() : LangCore.lang.wating));
                        }
                        if (this.roundTime < 20f)
                        {
                            if (LabelCenter)
                            {
                                this.ShowHUDInfoCenter(LangCore.lang.race_start_in + (int)(20f - this.roundTime) + ((!(this.localRacingResult == string.Empty)) ? ("\n" + LangCore.lang.last_round + "\n" + this.localRacingResult.toHex().Resize(15)) : "\n\n"));
                            }
                        }
                        else if (!this.startRacing)
                        {
                            this.ShowHUDInfoCenter(string.Empty);
                            this.startRacing = true;
                            this.endRacing = false;
                            findDoorRacing.SetActive(false);
                            if (this.racingDoors != null && FengGameManagerMKII.customLevelLoaded)
                            {
                                foreach (GameObject current in this.racingDoors)
                                {
                                    current.SetActive(false);
                                }
                                this.racingDoors = null;
                            }
                        }
                        else if (this.racingDoors != null && FengGameManagerMKII.customLevelLoaded)
                        {
                            foreach (GameObject current in this.racingDoors)
                            {
                                current.SetActive(false);
                            }
                            this.racingDoors = null;
                        }
                    }
                    if (IN_GAME_MAIN_CAMERA.instance.gameOver && !this.needChooseSide && FengGameManagerMKII.customLevelLoaded)
                    {
                        this.myRespawnTime += Time.deltaTime;
                        if (this.myRespawnTime > 1.5f)
                        {
                            this.myRespawnTime = 0f;
                            IN_GAME_MAIN_CAMERA.instance.gameOver = false;
                            if (this.checkpoint != null)
                            {
                                base.StartCoroutine(this.WaitAndRespawn2(0.1f, this.checkpoint));
                            }
                            else
                            {
                                base.StartCoroutine(this.WaitAndRespawn1(0.1f, this.myLastRespawnTag));
                            }
                            IN_GAME_MAIN_CAMERA.instance.gameOver = false;
                            if (LabelCenter)
                            {
                                this.ShowHUDInfoCenter(string.Empty);
                            }
                        }
                    }
                }
                if (this.timeElapse > 1f)
                {
                    this.timeElapse -= 1f;
                    string text = string.Empty;
                    if (LabelTopCenter)
                    {
                        if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.ENDLESS_TITAN)
                        {
                            text = text + LangCore.lang.time + (this.time - (int)this.timeTotalServer).ToString();
                        }
                        else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.KILL_TITAN || IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.None)
                        {
                            text = LangCore.lang.titan_left + alltitans.Count + " " + LangCore.lang.time;
                            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                            {
                                text += ((int)this.timeTotalServer).ToString();
                            }
                            else
                            {
                                text += (this.time - (int)this.timeTotalServer).ToString();
                            }
                        }
                        else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)
                        {
                            text = LangCore.lang.titan_left + alltitans.Count + " " + LangCore.lang.current_wave + this.wave + ((RCSettings.AnnieSurvive != 0) ? "\nAnnie Survive" : "");
                        }
                        else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.BOSS_FIGHT_CT)
                        {

                            text = LangCore.lang.time + (this.time - (int)this.timeTotalServer).ToString() + "\nDefeat the Colossal Titan.\nPrevent abnormal titan from running to the north gate";
                        }
                        else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE)
                        {
                            string text2 = "| ";
                            for (int i = 0; i < PVPcheckPoint.chkPts.Count; i++)
                            {
                                text2 = text2 + (PVPcheckPoint.chkPts[i] as PVPcheckPoint).getStateString() + " ";
                            }
                            text2 += "|";
                            text = string.Concat(new object[]
					{
						this.PVPtitanScoreMax - this.PVPtitanScore,
						"  ",
						text2,
						"  ",
						this.PVPhumanScoreMax - this.PVPhumanScore,
						"\n"
					}) + LangCore.lang.time + (this.time - (int)this.timeTotalServer).ToString();
                        }
                        if (RCSettings.teamMode > 0)
                        {
                            text = string.Concat(new string[]
					{
						text,
						"\n<color=#00FFFF>Cyan:",
						Convert.ToString(this.cyanKills),
						"</color>       <color=#FF00FF>Magenta:",
						Convert.ToString(this.magentaKills),
						"</color>"
					});
                        }
                        this.ShowHUDInfoTopCenter(text);
                    }
                    if (LabelTopRight)
                    {
                        text = string.Empty;
                        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                        {
                            if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)
                            {
                                text = LangCore.lang.time;
                                text += ((int)this.timeTotalServer).ToString();
                            }
                        }
                        else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.ENDLESS_TITAN)
                        {
                            text = LangCore.lang.human + this.humanScore + " " + LangCore.lang.titan_left + this.titanScore;
                        }
                        else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.KILL_TITAN || IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.BOSS_FIGHT_CT || IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE)
                        {
                            text = LangCore.lang.human + this.humanScore + " " + LangCore.lang.titan_left + this.titanScore;
                        }
                        else if (IN_GAME_MAIN_CAMERA.gamemode != GAMEMODE.CAGE_FIGHT)
                        {
                            if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)
                            {
                                text = LangCore.lang.time;
                                text += (this.time - (int)this.timeTotalServer).ToString();
                            }
                            else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_AHSS)
                            {
                                for (int j = 0; j < this.teamScores.Length; j++)
                                {
                                    text = text + ((j == 0) ? string.Empty : " : ") + LangCore.lang.team + (j + 1) + " " + this.teamScores[j];
                                }
                                text = text + "\n" + LangCore.lang.time + (this.time - (int)this.timeTotalServer).ToString();
                            }
                        }
                        this.ShowHUDInfoTopRight(text);
                    }
                    if (LabelTopRight)
                    {
                        string text4 = IN_GAME_MAIN_CAMERA.difficulty.ToString();
                        if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.CAGE_FIGHT)
                        {
                            this.ShowHUDInfoTopRightMAPNAME((int)this.roundTime + "s\n" + FengGameManagerMKII.level + " : " + text4);
                        }
                        else
                        {
                            this.ShowHUDInfoTopRightMAPNAME("\n" + FengGameManagerMKII.level + " : " + text4);
                        }
                    }
                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                    {
                        if (LabelTopRight)
                        {
                            this.ShowHUDInfoTopRightMAPNAME("\n" + PhotonNetwork.room.RoomName_to_core + " (" + Convert.ToString(PhotonNetwork.room.playerCount) + "/" + Convert.ToString(PhotonNetwork.room.maxPlayers) + ")" + "\n" + LangCore.lang.camera_hud + IN_GAME_MAIN_CAMERA.cameraMode.ToString() + "\n" + LangCore.lang.room + (PhotonNetwork.room.visible ? LangCore.lang.open : LangCore.lang.closed));
                        }
                        if (LabelTopCenter)
                        {
                            if (this.needChooseSide)
                            {
                                this.ShowHUDInfoTopCenterADD("\n\n" + LangCore.lang.entered_1);
                            }
                        }
                    }
                }
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && this.killinfo.Count > 0 && this.killinfo[0] == null)
                {
                    this.killinfo.RemoveAt(0);
                }
                if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE && PhotonNetwork.isMasterClient && this.timeTotalServer > (float)this.time)
                {
                    IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.STOP;
                    this.gameStart = false;
                    Screen.lockCursor = false;
                    Screen.showCursor = true;
                    string text6 = string.Empty;
                    string text7 = string.Empty;
                    string text8 = string.Empty;
                    string text9 = string.Empty;
                    string text10 = string.Empty;
                    PhotonPlayer[] array2 = PhotonNetwork.playerList;
                    for (int k = 0; k < array2.Length; k++)
                    {
                        PhotonPlayer photonPlayer = array2[k];
                        if (photonPlayer != null)
                        {
                            text6 = text6 + photonPlayer.name2 + "\n";
                            text7 = text7 + photonPlayer.kills + "\n";
                            text8 = text8 + photonPlayer.deaths + "\n";
                            text9 = text9 + photonPlayer.max_dmg + "\n";
                            text10 = text10 + photonPlayer.total_dmg + "\n";
                        }
                    }
                    object[] parameters = new object[]
				{
					text6,
					text7,
					text8,
					text9,
					text10,
					tital_text()
				};
                    base.photonView.RPC("showResult", PhotonTargets.AllBuffered, parameters);
                }
            }
        }
    }
    public string tital_text()
    {
        string text11;
        if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_AHSS)
        {
            text11 = string.Empty;
            for (int l = 0; l < this.teamScores.Length; l++)
            {
                text11 += ((l == 0) ? string.Concat(new object[]
						{
							"Team",
							l + 1,
							" ",
							this.teamScores[l],
							" "
						}) : " : ");
            }
        }
        else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)
        {
            text11 = "Highest Wave : " + this.highestwave;
        }
        else
        {
            object[] args4 = new object[]
					{
						"Humanity ",
						this.humanScore,
						" : Titan ",
						this.titanScore
					};
            text11 = string.Concat(args4);
        }
        return text11;
    }
    private void coreadd()
    {
        if (PhotonNetwork.isMasterClient)
        {
            this.OnUpdate();
            if (customLevelLoaded)
            {
                for (int i = 0; i < this.titanSpawners.Count; i++)
                {
                    TitanSpawner item = this.titanSpawners[i];
                    item.time -= Time.deltaTime;
                    if ((item.time <= 0f) && ((this.titans.Count + this.fT.Count) < Math.Min(RCSettings.titanCap, 80)))
                    {
                        string name = item.name;
                        if (name == "spawnAnnie")
                        {
                            PhotonNetwork.Instantiate("FEMALE_TITAN", item.location, new Quaternion(0f, 0f, 0f, 1f), 0);
                        }
                        else
                        {
                            GameObject obj2 = PhotonNetwork.Instantiate("TITAN_VER3.1", item.location, new Quaternion(0f, 0f, 0f, 1f), 0);
                            if (name == "spawnAbnormal")
                            {
                                obj2.GetComponent<TITAN>().setAbnormalType2(AbnormalType.TYPE_I, false);
                            }
                            else if (name == "spawnJumper")
                            {
                                obj2.GetComponent<TITAN>().setAbnormalType2(AbnormalType.TYPE_JUMPER, false);
                            }
                            else if (name == "spawnCrawler")
                            {
                                obj2.GetComponent<TITAN>().setAbnormalType2(AbnormalType.TYPE_CRAWLER, true);
                            }
                            else if (name == "spawnPunk")
                            {
                                obj2.GetComponent<TITAN>().setAbnormalType2(AbnormalType.TYPE_PUNK, false);
                            }
                        }
                        if (item.endless)
                        {
                            item.time = item.delay;
                        }
                        else
                        {
                            this.titanSpawners.Remove(item);
                        }
                    }
                }
            }
        }
        if (Time.timeScale <= 0.1f)
        {
            if (this.pauseWaitTime <= 3f)
            {
                this.pauseWaitTime -= Time.deltaTime * 1000000f;
                if (this.pauseWaitTime <= 1f)
                {
                    Camera.main.farClipPlane = 1500f;
                }
                if (this.pauseWaitTime <= 0f)
                {
                    this.pauseWaitTime = 0f;
                    Time.timeScale = 1f;
                }
            }
        }
    }

    private void coreeditor()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            GUI.FocusControl(null);
        }
        if (this.selectedObj != null)
        {
            float num = 0.2f;
            if (inputRC.isInputLevel(InputCodeRC.levelSlow))
            {
                num = 0.04f;
            }
            else if (inputRC.isInputLevel(InputCodeRC.levelFast))
            {
                num = 0.6f;
            }
            if (inputRC.isInputLevel(InputCodeRC.levelForward))
            {
                Transform transform = this.selectedObj.transform;
                transform.position += (Vector3)(num * new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z));
            }
            else if (inputRC.isInputLevel(InputCodeRC.levelBack))
            {
                Transform transform9 = this.selectedObj.transform;
                transform9.position -= (Vector3)(num * new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z));
            }
            if (inputRC.isInputLevel(InputCodeRC.levelLeft))
            {
                Transform transform10 = this.selectedObj.transform;
                transform10.position -= (Vector3)(num * new Vector3(Camera.main.transform.right.x, 0f, Camera.main.transform.right.z));
            }
            else if (inputRC.isInputLevel(InputCodeRC.levelRight))
            {
                Transform transform11 = this.selectedObj.transform;
                transform11.position += (Vector3)(num * new Vector3(Camera.main.transform.right.x, 0f, Camera.main.transform.right.z));
            }
            if (inputRC.isInputLevel(InputCodeRC.levelDown))
            {
                Transform transform12 = this.selectedObj.transform;
                transform12.position -= (Vector3)(Vector3.up * num);
            }
            else if (inputRC.isInputLevel(InputCodeRC.levelUp))
            {
                Transform transform13 = this.selectedObj.transform;
                transform13.position += (Vector3)(Vector3.up * num);
            }
            if (!this.selectedObj.name.StartsWith("misc,region"))
            {
                if (inputRC.isInputLevel(InputCodeRC.levelRRight))
                {
                    this.selectedObj.transform.Rotate((Vector3)(Vector3.up * num));
                }
                else if (inputRC.isInputLevel(InputCodeRC.levelRLeft))
                {
                    this.selectedObj.transform.Rotate((Vector3)(Vector3.down * num));
                }
                if (inputRC.isInputLevel(InputCodeRC.levelRCCW))
                {
                    this.selectedObj.transform.Rotate((Vector3)(Vector3.forward * num));
                }
                else if (inputRC.isInputLevel(InputCodeRC.levelRCW))
                {
                    this.selectedObj.transform.Rotate((Vector3)(Vector3.back * num));
                }
                if (inputRC.isInputLevel(InputCodeRC.levelRBack))
                {
                    this.selectedObj.transform.Rotate((Vector3)(Vector3.left * num));
                }
                else if (inputRC.isInputLevel(InputCodeRC.levelRForward))
                {
                    this.selectedObj.transform.Rotate((Vector3)(Vector3.right * num));
                }
            }
            if (inputRC.isInputLevel(InputCodeRC.levelPlace))
            {
                linkHash[3].Add(this.selectedObj.GetInstanceID(), this.selectedObj.name + "," + Convert.ToString(this.selectedObj.transform.position.x) + "," + Convert.ToString(this.selectedObj.transform.position.y) + "," + Convert.ToString(this.selectedObj.transform.position.z) + "," + Convert.ToString(this.selectedObj.transform.rotation.x) + "," + Convert.ToString(this.selectedObj.transform.rotation.y) + "," + Convert.ToString(this.selectedObj.transform.rotation.z) + "," + Convert.ToString(this.selectedObj.transform.rotation.w));
                this.selectedObj = null;
                IN_GAME_MAIN_CAMERA.instance.mouselook.enabled = true;
                Screen.lockCursor = true;
            }
            if (inputRC.isInputLevel(InputCodeRC.levelDelete))
            {
                UnityEngine.Object.Destroy(this.selectedObj);
                this.selectedObj = null;
                IN_GAME_MAIN_CAMERA.instance.mouselook.enabled = true;
                Screen.lockCursor = true;
                linkHash[3].Remove(this.selectedObj.GetInstanceID());
            }
        }
        else
        {
            if (Screen.lockCursor)
            {
                float num2 = 100f;
                if (inputRC.isInputLevel(InputCodeRC.levelSlow))
                {
                    num2 = 20f;
                }
                else if (inputRC.isInputLevel(InputCodeRC.levelFast))
                {
                    num2 = 400f;
                }
                Transform transform7 = Camera.main.transform;
                if (inputRC.isInputLevel(InputCodeRC.levelForward))
                {
                    transform7.position += (Vector3)((transform7.forward * num2) * Time.deltaTime);
                }
                else if (inputRC.isInputLevel(InputCodeRC.levelBack))
                {
                    transform7.position -= (Vector3)((transform7.forward * num2) * Time.deltaTime);
                }
                if (inputRC.isInputLevel(InputCodeRC.levelLeft))
                {
                    transform7.position -= (Vector3)((transform7.right * num2) * Time.deltaTime);
                }
                else if (inputRC.isInputLevel(InputCodeRC.levelRight))
                {
                    transform7.position += (Vector3)((transform7.right * num2) * Time.deltaTime);
                }
                if (inputRC.isInputLevel(InputCodeRC.levelUp))
                {
                    transform7.position += (Vector3)((transform7.up * num2) * Time.deltaTime);
                }
                else if (inputRC.isInputLevel(InputCodeRC.levelDown))
                {
                    transform7.position -= (Vector3)((transform7.up * num2) * Time.deltaTime);
                }
            }
            if (inputRC.isInputLevelDown(InputCodeRC.levelCursor))
            {
                if (Screen.lockCursor)
                {
                    IN_GAME_MAIN_CAMERA.instance.mouselook.enabled = false;
                    Screen.lockCursor = false;
                }
                else
                {
                    IN_GAME_MAIN_CAMERA.instance.mouselook.enabled = true;
                    Screen.lockCursor = true;
                }
            }
            if (((Input.GetKeyDown(KeyCode.Mouse0) && !Screen.lockCursor) && (GUIUtility.hotControl == 0)) && !(((Input.mousePosition.x <= 300f) || (Input.mousePosition.x >= (Screen.width - 300f))) ? ((Screen.height - Input.mousePosition.y) <= 600f) : false))
            {
                RaycastHit hitInfo = new RaycastHit();
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
                {
                    Transform transform8 = hitInfo.transform;
                    if (((transform8.gameObject.name.StartsWith("custom") || transform8.gameObject.name.StartsWith("base")) || (transform8.gameObject.name.StartsWith("racing") || transform8.gameObject.name.StartsWith("photon"))) || (transform8.gameObject.name.StartsWith("spawnpoint") || transform8.gameObject.name.StartsWith("misc")))
                    {
                        this.selectedObj = transform8.gameObject;
                        IN_GAME_MAIN_CAMERA.instance.mouselook.enabled = false;
                        Screen.lockCursor = true;
                        linkHash[3].Remove(this.selectedObj.GetInstanceID());
                    }
                    else if ((transform8.parent.gameObject.name.StartsWith("custom") || transform8.parent.gameObject.name.StartsWith("base")) || (transform8.parent.gameObject.name.StartsWith("racing") || transform8.parent.gameObject.name.StartsWith("photon")))
                    {
                        this.selectedObj = transform8.parent.gameObject;
                        IN_GAME_MAIN_CAMERA.instance.mouselook.enabled = false;
                        Screen.lockCursor = true;
                        linkHash[3].Remove(this.selectedObj.GetInstanceID());
                    }
                }
            }
        }
    }

    private IEnumerator customlevelcache()
    {
        int iteratorVariable0 = 0;
        while (true)
        {
            if (iteratorVariable0 >= this.levelCache.Count)
            {
                yield break;
            }
            this.customlevelclientE(this.levelCache[iteratorVariable0], false);
            yield return new WaitForEndOfFrame();
            iteratorVariable0++;
        }
    }

    private void customlevelclientE(string[] content, bool renewHash)
    {
        int num;
        string[] strArray;
        bool flag = false;
        bool flag2 = false;
        if (content[content.Length - 1].StartsWith("a"))
        {
            flag = true;
        }
        else if (content[content.Length - 1].StartsWith("z"))
        {
            flag2 = true;
            customLevelLoaded = true;
            this.spawnPlayerCustomMap();
            Minimap.TryRecaptureInstance();
            this.unloadAssets();
       
        }
        if (renewHash)
        {
            if (flag)
            {
                currentLevel = string.Empty;
                this.levelCache.Clear();
                this.titanSpawns.Clear();
                this.playerSpawnsC.Clear();
                this.playerSpawnsM.Clear();
                for (num = 0; num < content.Length; num++)
                {
                    strArray = content[num].Split(new char[] { ',' });
                    if (strArray[0] == "titan")
                    {
                        this.titanSpawns.Add(new Vector3(Convert.ToSingle(strArray[1]), Convert.ToSingle(strArray[2]), Convert.ToSingle(strArray[3])));
                    }
                    else if (strArray[0] == "playerC")
                    {
                        this.playerSpawnsC.Add(new Vector3(Convert.ToSingle(strArray[1]), Convert.ToSingle(strArray[2]), Convert.ToSingle(strArray[3])));
                    }
                    else if (strArray[0] == "playerM")
                    {
                        this.playerSpawnsM.Add(new Vector3(Convert.ToSingle(strArray[1]), Convert.ToSingle(strArray[2]), Convert.ToSingle(strArray[3])));
                    }
                }
                this.spawnPlayerCustomMap();
            }
            currentLevel = currentLevel + content[content.Length - 1];
            this.levelCache.Add(content);
            PhotonNetwork.player.currentLevel = currentLevel;
        }
        if (!flag && !flag2)
        {
            for (num = 0; num < content.Length; num++)
            {
                float num2;
                GameObject obj2;
                float num3;
                float num4;
                float num5;
                float num6;
                Color color;
                Mesh mesh;
                Color[] colorArray;
                int num7;
                strArray = content[num].Split(new char[] { ',' });
                if (strArray[0].StartsWith("custom"))
                {
                    num2 = 1f;
                    obj2 = null;
                    obj2 = (GameObject)UnityEngine.Object.Instantiate((GameObject)RCassets.Load(strArray[1]), new Vector3(Convert.ToSingle(strArray[12]), Convert.ToSingle(strArray[13]), Convert.ToSingle(strArray[14])), new Quaternion(Convert.ToSingle(strArray[15]), Convert.ToSingle(strArray[0x10]), Convert.ToSingle(strArray[0x11]), Convert.ToSingle(strArray[0x12])));
                    if (strArray[2] != "default")
                    {
                        if (strArray[2].StartsWith("transparent"))
                        {
                            if (float.TryParse(strArray[2].Substring(11), out num3))
                            {
                                num2 = num3;
                            }
                            foreach (Renderer renderer in obj2.GetComponentsInChildren<Renderer>())
                            {
                                renderer.material = (Material)RCassets.Load("transparent");
                                if ((Convert.ToSingle(strArray[10]) != 1f) || (Convert.ToSingle(strArray[11]) != 1f))
                                {
                                    renderer.material.mainTextureScale = new Vector2(renderer.material.mainTextureScale.x * Convert.ToSingle(strArray[10]), renderer.material.mainTextureScale.y * Convert.ToSingle(strArray[11]));
                                }
                            }
                        }
                        else
                        {
                            foreach (Renderer renderer2 in obj2.GetComponentsInChildren<Renderer>())
                            {
                                renderer2.material = (Material)RCassets.Load(strArray[2]);
                                if ((Convert.ToSingle(strArray[10]) != 1f) || (Convert.ToSingle(strArray[11]) != 1f))
                                {
                                    renderer2.material.mainTextureScale = new Vector2(renderer2.material.mainTextureScale.x * Convert.ToSingle(strArray[10]), renderer2.material.mainTextureScale.y * Convert.ToSingle(strArray[11]));
                                }
                            }
                        }
                    }
                    num4 = obj2.transform.localScale.x * Convert.ToSingle(strArray[3]);
                    num4 -= 0.001f;
                    num5 = obj2.transform.localScale.y * Convert.ToSingle(strArray[4]);
                    num6 = obj2.transform.localScale.z * Convert.ToSingle(strArray[5]);
                    obj2.transform.localScale = new Vector3(num4, num5, num6);
                    if (strArray[6] != "0")
                    {
                        color = new Color(Convert.ToSingle(strArray[7]), Convert.ToSingle(strArray[8]), Convert.ToSingle(strArray[9]), num2);
                        foreach (MeshFilter filter in obj2.GetComponentsInChildren<MeshFilter>())
                        {
                            mesh = filter.mesh;
                            colorArray = new Color[mesh.vertexCount];
                            num7 = 0;
                            while (num7 < mesh.vertexCount)
                            {
                                colorArray[num7] = color;
                                num7++;
                            }
                            mesh.colors = colorArray;
                        }
                    }
                }
                else if (strArray[0].StartsWith("base"))
                {
                    if (strArray.Length < 15)
                    {
                        UnityEngine.Object.Instantiate(Resources.Load(strArray[1]), new Vector3(Convert.ToSingle(strArray[2]), Convert.ToSingle(strArray[3]), Convert.ToSingle(strArray[4])), new Quaternion(Convert.ToSingle(strArray[5]), Convert.ToSingle(strArray[6]), Convert.ToSingle(strArray[7]), Convert.ToSingle(strArray[8])));
                    }
                    else
                    {
                        num2 = 1f;
                        obj2 = null;
                        obj2 = (GameObject)UnityEngine.Object.Instantiate((GameObject)Resources.Load(strArray[1]), new Vector3(Convert.ToSingle(strArray[12]), Convert.ToSingle(strArray[13]), Convert.ToSingle(strArray[14])), new Quaternion(Convert.ToSingle(strArray[15]), Convert.ToSingle(strArray[0x10]), Convert.ToSingle(strArray[0x11]), Convert.ToSingle(strArray[0x12])));
                        if (strArray[2] != "default")
                        {
                            if (strArray[2].StartsWith("transparent"))
                            {
                                if (float.TryParse(strArray[2].Substring(11), out num3))
                                {
                                    num2 = num3;
                                }
                                foreach (Renderer renderer3 in obj2.GetComponentsInChildren<Renderer>())
                                {
                                    renderer3.material = (Material)RCassets.Load("transparent");
                                    if ((Convert.ToSingle(strArray[10]) != 1f) || (Convert.ToSingle(strArray[11]) != 1f))
                                    {
                                        renderer3.material.mainTextureScale = new Vector2(renderer3.material.mainTextureScale.x * Convert.ToSingle(strArray[10]), renderer3.material.mainTextureScale.y * Convert.ToSingle(strArray[11]));
                                    }
                                }
                            }
                            else
                            {
                                foreach (Renderer renderer4 in obj2.GetComponentsInChildren<Renderer>())
                                {
                                    if (!renderer4.name.Contains("Particle System") || !obj2.name.Contains("aot_supply"))
                                    {
                                        renderer4.material = (Material)RCassets.Load(strArray[2]);
                                        if ((Convert.ToSingle(strArray[10]) != 1f) || (Convert.ToSingle(strArray[11]) != 1f))
                                        {
                                            renderer4.material.mainTextureScale = new Vector2(renderer4.material.mainTextureScale.x * Convert.ToSingle(strArray[10]), renderer4.material.mainTextureScale.y * Convert.ToSingle(strArray[11]));
                                        }
                                    }
                                }
                            }
                        }
                        num4 = obj2.transform.localScale.x * Convert.ToSingle(strArray[3]);
                        num4 -= 0.001f;
                        num5 = obj2.transform.localScale.y * Convert.ToSingle(strArray[4]);
                        num6 = obj2.transform.localScale.z * Convert.ToSingle(strArray[5]);
                        obj2.transform.localScale = new Vector3(num4, num5, num6);
                        if (strArray[6] != "0")
                        {
                            color = new Color(Convert.ToSingle(strArray[7]), Convert.ToSingle(strArray[8]), Convert.ToSingle(strArray[9]), num2);
                            foreach (MeshFilter filter2 in obj2.GetComponentsInChildren<MeshFilter>())
                            {
                                mesh = filter2.mesh;
                                colorArray = new Color[mesh.vertexCount];
                                for (num7 = 0; num7 < mesh.vertexCount; num7++)
                                {
                                    colorArray[num7] = color;
                                }
                                mesh.colors = colorArray;
                            }
                        }
                    }
                }
                else if (strArray[0].StartsWith("misc"))
                {
                    if (strArray[1].StartsWith("barrier"))
                    {
                        obj2 = null;
                        obj2 = (GameObject)UnityEngine.Object.Instantiate((GameObject)RCassets.Load(strArray[1]), new Vector3(Convert.ToSingle(strArray[5]), Convert.ToSingle(strArray[6]), Convert.ToSingle(strArray[7])), new Quaternion(Convert.ToSingle(strArray[8]), Convert.ToSingle(strArray[9]), Convert.ToSingle(strArray[10]), Convert.ToSingle(strArray[11])));
                        num4 = obj2.transform.localScale.x * Convert.ToSingle(strArray[2]);
                        num4 -= 0.001f;
                        num5 = obj2.transform.localScale.y * Convert.ToSingle(strArray[3]);
                        num6 = obj2.transform.localScale.z * Convert.ToSingle(strArray[4]);
                        obj2.transform.localScale = new Vector3(num4, num5, num6);
                    }
                    else if (strArray[1].StartsWith("racingStart"))
                    {
                        obj2 = null;
                        obj2 = (GameObject)UnityEngine.Object.Instantiate((GameObject)RCassets.Load(strArray[1]), new Vector3(Convert.ToSingle(strArray[5]), Convert.ToSingle(strArray[6]), Convert.ToSingle(strArray[7])), new Quaternion(Convert.ToSingle(strArray[8]), Convert.ToSingle(strArray[9]), Convert.ToSingle(strArray[10]), Convert.ToSingle(strArray[11])));
                        num4 = obj2.transform.localScale.x * Convert.ToSingle(strArray[2]);
                        num4 -= 0.001f;
                        num5 = obj2.transform.localScale.y * Convert.ToSingle(strArray[3]);
                        num6 = obj2.transform.localScale.z * Convert.ToSingle(strArray[4]);
                        obj2.transform.localScale = new Vector3(num4, num5, num6);
                        if (this.racingDoors != null)
                        {
                            this.racingDoors.Add(obj2);
                        }
                    }
                    else if (strArray[1].StartsWith("racingEnd"))
                    {
                        obj2 = null;
                        obj2 = (GameObject)UnityEngine.Object.Instantiate((GameObject)RCassets.Load(strArray[1]), new Vector3(Convert.ToSingle(strArray[5]), Convert.ToSingle(strArray[6]), Convert.ToSingle(strArray[7])), new Quaternion(Convert.ToSingle(strArray[8]), Convert.ToSingle(strArray[9]), Convert.ToSingle(strArray[10]), Convert.ToSingle(strArray[11])));
                        num4 = obj2.transform.localScale.x * Convert.ToSingle(strArray[2]);
                        num4 -= 0.001f;
                        num5 = obj2.transform.localScale.y * Convert.ToSingle(strArray[3]);
                        num6 = obj2.transform.localScale.z * Convert.ToSingle(strArray[4]);
                        obj2.transform.localScale = new Vector3(num4, num5, num6);
                        obj2.AddComponent<LevelTriggerRacingEnd>();
                    }
                    else if (strArray[1].StartsWith("region") && PhotonNetwork.isMasterClient)
                    {
                        Vector3 loc = new Vector3(Convert.ToSingle(strArray[6]), Convert.ToSingle(strArray[7]), Convert.ToSingle(strArray[8]));
                        RCRegion region = new RCRegion(loc, Convert.ToSingle(strArray[3]), Convert.ToSingle(strArray[4]), Convert.ToSingle(strArray[5]));
                        string key = strArray[2];
                        if (RCRegionTriggers.ContainsKey(key))
                        {
                            GameObject obj3 = (GameObject)UnityEngine.Object.Instantiate((GameObject)RCassets.Load("region"));
                            obj3.transform.position = loc;
                            obj3.AddComponent<RegionTrigger>();
                            obj3.GetComponent<RegionTrigger>().CopyTrigger((RegionTrigger)RCRegionTriggers[key]);
                            num4 = obj3.transform.localScale.x * Convert.ToSingle(strArray[3]);
                            num4 -= 0.001f;
                            num5 = obj3.transform.localScale.y * Convert.ToSingle(strArray[4]);
                            num6 = obj3.transform.localScale.z * Convert.ToSingle(strArray[5]);
                            obj3.transform.localScale = new Vector3(num4, num5, num6);
                            region.myBox = obj3;
                        }
                        RCRegions.Add(key, region);
                    }
                }
                else if (strArray[0].StartsWith("racing"))
                {
                    if (strArray[1].StartsWith("start"))
                    {
                        obj2 = null;
                        obj2 = (GameObject)UnityEngine.Object.Instantiate((GameObject)RCassets.Load(strArray[1]), new Vector3(Convert.ToSingle(strArray[5]), Convert.ToSingle(strArray[6]), Convert.ToSingle(strArray[7])), new Quaternion(Convert.ToSingle(strArray[8]), Convert.ToSingle(strArray[9]), Convert.ToSingle(strArray[10]), Convert.ToSingle(strArray[11])));
                        num4 = obj2.transform.localScale.x * Convert.ToSingle(strArray[2]);
                        num4 -= 0.001f;
                        num5 = obj2.transform.localScale.y * Convert.ToSingle(strArray[3]);
                        num6 = obj2.transform.localScale.z * Convert.ToSingle(strArray[4]);
                        obj2.transform.localScale = new Vector3(num4, num5, num6);
                        if (this.racingDoors != null)
                        {
                            this.racingDoors.Add(obj2);
                        }
                    }
                    else if (strArray[1].StartsWith("end"))
                    {
                        obj2 = null;
                        obj2 = (GameObject)UnityEngine.Object.Instantiate((GameObject)RCassets.Load(strArray[1]), new Vector3(Convert.ToSingle(strArray[5]), Convert.ToSingle(strArray[6]), Convert.ToSingle(strArray[7])), new Quaternion(Convert.ToSingle(strArray[8]), Convert.ToSingle(strArray[9]), Convert.ToSingle(strArray[10]), Convert.ToSingle(strArray[11])));
                        num4 = obj2.transform.localScale.x * Convert.ToSingle(strArray[2]);
                        num4 -= 0.001f;
                        num5 = obj2.transform.localScale.y * Convert.ToSingle(strArray[3]);
                        num6 = obj2.transform.localScale.z * Convert.ToSingle(strArray[4]);
                        obj2.transform.localScale = new Vector3(num4, num5, num6);
                        obj2.GetComponentInChildren<Collider>().gameObject.AddComponent<LevelTriggerRacingEnd>();
                    }
                    else if (strArray[1].StartsWith("kill"))
                    {
                        obj2 = null;
                        obj2 = (GameObject)UnityEngine.Object.Instantiate((GameObject)RCassets.Load(strArray[1]), new Vector3(Convert.ToSingle(strArray[5]), Convert.ToSingle(strArray[6]), Convert.ToSingle(strArray[7])), new Quaternion(Convert.ToSingle(strArray[8]), Convert.ToSingle(strArray[9]), Convert.ToSingle(strArray[10]), Convert.ToSingle(strArray[11])));
                        num4 = obj2.transform.localScale.x * Convert.ToSingle(strArray[2]);
                        num4 -= 0.001f;
                        num5 = obj2.transform.localScale.y * Convert.ToSingle(strArray[3]);
                        num6 = obj2.transform.localScale.z * Convert.ToSingle(strArray[4]);
                        obj2.transform.localScale = new Vector3(num4, num5, num6);
                        obj2.GetComponentInChildren<Collider>().gameObject.AddComponent<RacingKillTrigger>();
                    }
                    else if (strArray[1].StartsWith("checkpoint"))
                    {
                        obj2 = null;
                        obj2 = (GameObject)UnityEngine.Object.Instantiate((GameObject)RCassets.Load(strArray[1]), new Vector3(Convert.ToSingle(strArray[5]), Convert.ToSingle(strArray[6]), Convert.ToSingle(strArray[7])), new Quaternion(Convert.ToSingle(strArray[8]), Convert.ToSingle(strArray[9]), Convert.ToSingle(strArray[10]), Convert.ToSingle(strArray[11])));
                        num4 = obj2.transform.localScale.x * Convert.ToSingle(strArray[2]);
                        num4 -= 0.001f;
                        num5 = obj2.transform.localScale.y * Convert.ToSingle(strArray[3]);
                        num6 = obj2.transform.localScale.z * Convert.ToSingle(strArray[4]);
                        obj2.transform.localScale = new Vector3(num4, num5, num6);
                        obj2.GetComponentInChildren<Collider>().gameObject.AddComponent<RacingCheckpointTrigger>();
                    }
                }
                else if (strArray[0].StartsWith("map"))
                {
                    if (strArray[1].StartsWith("disablebounds"))
                    {
                        UnityEngine.Object.Destroy(CyanMod.CachingsGM.Find("gameobjectOutSide"));
                        UnityEngine.Object.Instantiate(RCassets.Load("outside"));
                    }
                }
                else if (PhotonNetwork.isMasterClient && strArray[0].StartsWith("photon"))
                {
                    if (strArray[1].StartsWith("Cannon"))
                    {
                        if (strArray.Length > 15)
                        {
                            GameObject go = PhotonNetwork.Instantiate("RCAsset/" + strArray[1] + "Prop", new Vector3(Convert.ToSingle(strArray[12]), Convert.ToSingle(strArray[13]), Convert.ToSingle(strArray[14])), new Quaternion(Convert.ToSingle(strArray[15]), Convert.ToSingle(strArray[0x10]), Convert.ToSingle(strArray[0x11]), Convert.ToSingle(strArray[0x12])), 0);
                            go.GetComponent<CannonPropRegion>().settings = content[num];
                            go.GetPhotonView().RPC("SetSize", PhotonTargets.AllBuffered, new object[] { content[num] });
                        }
                        else
                        {
                            PhotonNetwork.Instantiate("RCAsset/" + strArray[1] + "Prop", new Vector3(Convert.ToSingle(strArray[2]), Convert.ToSingle(strArray[3]), Convert.ToSingle(strArray[4])), new Quaternion(Convert.ToSingle(strArray[5]), Convert.ToSingle(strArray[6]), Convert.ToSingle(strArray[7]), Convert.ToSingle(strArray[8])), 0).GetComponent<CannonPropRegion>().settings = content[num];
                        }
                    }
                    else
                    {
                        TitanSpawner item = new TitanSpawner();
                        num4 = 30f;
                        if (float.TryParse(strArray[2], out num3))
                        {
                            num4 = Mathf.Max(Convert.ToSingle(strArray[2]), 1f);
                        }
                        item.time = num4;
                        item.delay = num4;
                        item.name = strArray[1];
                        if (strArray[3] == "1")
                        {
                            item.endless = true;
                        }
                        else
                        {
                            item.endless = false;
                        }
                        item.location = new Vector3(Convert.ToSingle(strArray[4]), Convert.ToSingle(strArray[5]), Convert.ToSingle(strArray[6]));
                        this.titanSpawners.Add(item);
                    }
                }
            }
        }
    }

    private IEnumerator customlevelE(List<PhotonPlayer> players)
    {
        string[] iteratorVariable0;
        if (currentLevel == string.Empty)
        {
            iteratorVariable0 = new string[] { "loadempty" };
            foreach (PhotonPlayer player2 in players)
            {
                this.photonView.RPC("customlevelRPC", player2, new object[] { iteratorVariable0 });
            }
            customLevelLoaded = true;
        }
        else
        {
            for (int i = 0; i < this.levelCache.Count; i++)
            {
                foreach (PhotonPlayer player in players)
                {
                    if (((currentLevel != string.Empty)) && (player.currentLevel == currentLevel))
                    {
                        if (i == 0)
                        {
                            iteratorVariable0 = new string[] { "loadcached" };
                            this.photonView.RPC("customlevelRPC", player, new object[] { iteratorVariable0 });
                        }
                    }
                    else
                    {
                        this.photonView.RPC("customlevelRPC", player, new object[] { this.levelCache[i] });
                    }
                }
                if (i > 0)
                {
                    yield return new WaitForSeconds(0.75f);
                }
                else
                {
                    yield return new WaitForSeconds(0.25f);
                }
            }
        }
        yield break;
    }

    [RPC]
    private void customlevelRPC(string[] content, PhotonMessageInfo info)
    {
        if (info.sender.isMasterClient)
        {
            if ((content.Length == 1) && (content[0] == "loadcached"))
            {
                base.StartCoroutine(this.customlevelcache());
            }
            else if ((content.Length == 1) && (content[0] == "loadempty"))
            {
                currentLevel = string.Empty;
                this.levelCache.Clear();
                this.titanSpawns.Clear();
                this.playerSpawnsC.Clear();
                this.playerSpawnsM.Clear();
                PhotonNetwork.player.currentLevel = currentLevel;
                customLevelLoaded = true;
                this.spawnPlayerCustomMap();
            }
            else
            {
                this.customlevelclientE(content, true);
            }
        }
    }

    public void debugChat(string str)
    {
        InRoomChat.instance.addLINE(str);
    }

    public void DestroyAllExistingCloths()
    {
        Cloth[] clothArray = UnityEngine.Object.FindObjectsOfType<Cloth>();
        if (clothArray.Length > 0)
        {
            for (int i = 0; i < clothArray.Length; i++)
            {
                ClothFactory.DisposeObject(clothArray[i].gameObject);
            }
        }
    }

    private void endGameInfectionRC()
    {
        int num;
        imatitan.Clear();
        for (num = 0; num < PhotonNetwork.playerList.Length; num++)
        {
            PhotonPlayer player = PhotonNetwork.playerList[num];
            player.isTitan = 1;
        }
        int length = PhotonNetwork.playerList.Length;
        int infectionMode = RCSettings.infectionMode;
        for (num = 0; num < PhotonNetwork.playerList.Length; num++)
        {
            PhotonPlayer player2 = PhotonNetwork.playerList[num];
            if ((length > 0) && (UnityEngine.Random.Range((float)0f, (float)1f) <= (((float)infectionMode) / ((float)length))))
            {
                player2.isTitan = 2;
                imatitan.Add(player2.ID, 2);
                infectionMode--;
            }
            length--;
        }
        this.gameEndCD = 0f;
    }

    private void endGameRC()
    {
        if (RCSettings.pointMode > 0)
        {
            foreach (PhotonPlayer player in PhotonNetwork.playerList)
            {
                player.kills = 0;
                player.deaths = 0;
                player.max_dmg = 0;
                player.total_dmg = 0;
            }
        }
        this.gameEndCD = 0f;
    }

  public void EnableSprite(bool flag)
  {
      foreach (GameObject obj2 in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
      {
          UISprite sprite = obj2.GetComponent<UISprite>();
          if ((sprite != null) && obj2.activeInHierarchy)
          {
              string name = obj2.name;
              if (name.Contains("blade") || name.Contains("bullet") || name.Contains("gas") || name.Contains("flare") || name.Contains("skill_cd"))
              {
                sprite.enabled = flag;
              }
          }
      }
  }
    public void EnterSpecMode(bool enter)
    {
        if (enter)
        {
            EnableSprite(false);
            foreach (HERO hero in heroes)
            {
                if (hero.photonView.isMine)
                {
                    PhotonNetwork.Destroy(hero.photonView);
                }
            }
            if (((PhotonNetwork.player.isTitan) == 2) && !(PhotonNetwork.player.dead))
            {
                foreach (TITAN titan in titans)
                {
                    if (titan.photonView.isMine)
                    {
                        PhotonNetwork.Destroy(titan.photonView);
                    }
                }
            }
            NGUITools.SetActive(uiT.panels[1], false);
            NGUITools.SetActive(uiT.panels[2], false);
            NGUITools.SetActive(uiT.panels[3], false);
            needChooseSide = false;
            Camera.main.GetComponent<IN_GAME_MAIN_CAMERA>().enabled = true;
            if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.ORIGINAL)
            {
                Screen.lockCursor = false;
                Screen.showCursor = false;
            }
            GameObject obj4 = GameObject.FindGameObjectWithTag("Player");
            if ((obj4 != null) && (obj4.GetComponent<HERO>() != null))
            {
                IN_GAME_MAIN_CAMERA.instance.setMainObject(obj4, true, false);
            }
            else
            {
                IN_GAME_MAIN_CAMERA.instance.setMainObject(null, true, false);
            }
            IN_GAME_MAIN_CAMERA.instance.setSpectorMode(false);
            IN_GAME_MAIN_CAMERA.instance.gameOver = true;
            base.StartCoroutine(this.reloadSky());
        }
        else
        {
            if (CyanMod.CachingsGM.Find("cross1") != null)
            {
                CyanMod.CachingsGM.Find("cross1").transform.localPosition = (Vector3)(Vector3.up * 5000f);
            }
            NGUITools.SetActive(uiT.panels[1], false);
            NGUITools.SetActive(uiT.panels[2], false);
            NGUITools.SetActive(uiT.panels[3], false);
            instance.needChooseSide = true;
            IN_GAME_MAIN_CAMERA.instance.setMainObject(null, true, false);
            IN_GAME_MAIN_CAMERA.instance.setSpectorMode(true);
            IN_GAME_MAIN_CAMERA.instance.gameOver = true;
        }
    }

    public void gameLose()
    {
        if (!this.isWinning && !this.isLosing)
        {
            this.isLosing = true;
            this.titanScore++;
            this.gameEndCD = this.gameEndTotalCDtime;
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
            {
                object[] parameters = new object[] { this.titanScore };
                base.photonView.RPC("netGameLose", PhotonTargets.Others, parameters);
            }
        }
    }

    public void gameLose2()
    {
        if (!this.isWinning && !this.isLosing)
        {
            this.isLosing = true;
            this.titanScore++;
            this.gameEndCD = this.gameEndTotalCDtime;
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
            {
                object[] parameters = new object[] { this.titanScore };
                base.photonView.RPC("netGameLose", PhotonTargets.Others, parameters);
                if (((int)settings[0xf4]) == 1)
                {
                    InRoomChat.instance.addLINE("(" + this.roundTime.ToString("F2") + ") Round ended (game lose).");
                }
            }
        }
    }

    public void gameWin()
    {
        if (!this.isLosing && !this.isWinning)
        {
            this.isWinning = true;
            this.humanScore++;
            if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.RACING)
            {
                this.gameEndCD = 20f;
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                {
                    object[] parameters = new object[] { 0 };
                    base.photonView.RPC("netGameWin", PhotonTargets.Others, parameters);
                }
            }
            else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_AHSS)
            {
                this.gameEndCD = this.gameEndTotalCDtime;
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                {
                    object[] objArray2 = new object[] { this.teamWinner };
                    base.photonView.RPC("netGameWin", PhotonTargets.Others, objArray2);
                }
                this.teamScores[this.teamWinner - 1]++;
            }
            else
            {
                this.gameEndCD = this.gameEndTotalCDtime;
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                {
                    object[] objArray3 = new object[] { this.humanScore };
                    base.photonView.RPC("netGameWin", PhotonTargets.Others, objArray3);
                }
            }
        }
    }

    public void gameWin2()
    {
        if (!this.isLosing && !this.isWinning)
        {
            this.isWinning = true;
            this.humanScore++;
            if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.RACING)
            {
                if (RCSettings.racingStatic == 1)
                {
                    this.gameEndCD = 1000f;
                }
                else
                {
                    this.gameEndCD = 20f;
                }
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                {
                    object[] parameters = new object[] { 0 };
                    base.photonView.RPC("netGameWin", PhotonTargets.Others, parameters);
                    if (((int)settings[0xf4]) == 1)
                    {
                        InRoomChat.instance.addLINE("(" + this.roundTime.ToString("F2") + ") Round ended (game win).");
                    }
                }
            }
            else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_AHSS)
            {
                this.gameEndCD = this.gameEndTotalCDtime;
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                {
                    object[] objArray2 = new object[] { this.teamWinner };
                    base.photonView.RPC("netGameWin", PhotonTargets.Others, objArray2);
                    if (((int)settings[0xf4]) == 1)
                    {
                        InRoomChat.instance.addLINE("(" + this.roundTime.ToString("F2") + ") Round ended (game win).");
                    }
                }
                this.teamScores[this.teamWinner - 1]++;
            }
            else
            {
                this.gameEndCD = this.gameEndTotalCDtime;
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                {
                    object[] objArray3 = new object[] { this.humanScore };
                    base.photonView.RPC("netGameWin", PhotonTargets.Others, objArray3);
                    if (((int)settings[0xf4]) == 1)
                    {
                        InRoomChat.instance.addLINE("(" + this.roundTime.ToString("F2") + ")Round ended (game win).");
                    }
                }
            }
        }
    }

    [RPC]
    private void getRacingResult(string player, float time)
    {
        RacingResult result = new RacingResult
        {
            name = player,
            time = time
        };
        this.racingResult.Add(result);
        this.refreshRacingResult2();
    }


    private string hairtype(int lol)
    {
        if (lol < 0)
        {
            return "Random";
        }
        return ("Male " + lol);
    }

    [RPC]
    private void ignorePlayer(int ID, PhotonMessageInfo info)
    {
        if (info.sender.isMasterClient)
        {
            PhotonPlayer player = PhotonPlayer.Find(ID);
            if ((player != null))
            {
                if (ignoreList.Contains(ID))
                {
                    ignoreList.Remove(ID);
                }
                ignoreList.Add(ID);
                RaiseEventOptions options = new RaiseEventOptions { TargetActors = new int[] { ID } };
                PhotonNetwork.RaiseEvent(0xfe, null, true, options);
            }
        }
        this.RecompilePlayerList(0.1f);
    }

    [RPC]
    private void ignorePlayerArray(int[] IDS, PhotonMessageInfo info)
    {
        if (info.sender.isMasterClient)
        {
            for (int i = 0; i < IDS.Length; i++)
            {
                int iD = IDS[i];
                PhotonPlayer player = PhotonPlayer.Find(iD);
                if ((player != null))
                {
                    if (ignoreList.Contains(iD))
                    {
                        ignoreList.Remove(iD);
                    }
                    ignoreList.Add(iD);
                    RaiseEventOptions options = new RaiseEventOptions
                    {
                        TargetActors = new int[] { iD }
                    };
                    PhotonNetwork.RaiseEvent(0xfe, null, true, options);
                }
            }
        }
        this.RecompilePlayerList(0.1f);
    }

    public static GameObject InstantiateCustomAsset(string key)
    {
        key = key.Substring(8);
        return (GameObject)RCassets.Load(key);
    }

    public bool isPlayerAllDead()
    {
        int num = 0;
        int num2 = 0;
        foreach (PhotonPlayer player in PhotonNetwork.playerList)
        {
            if (((int)player.isTitan) == 1)
            {
                num++;
                if ((bool)player.dead)
                {
                    num2++;
                }
            }
        }
        return (num == num2);
    }

    public bool isPlayerAllDead2()
    {
        int num = 0;
        int num2 = 0;
        foreach (PhotonPlayer player in PhotonNetwork.playerList)
        {
            if ((player.isTitan) == 1)
            {
                num++;
                if ((player.dead))
                {
                    num2++;
                }
            }
        }
        return (num == num2);
    }

    public bool isTeamAllDead(int team)
    {
        int num = 0;
        int num2 = 0;
        foreach (PhotonPlayer player in PhotonNetwork.playerList)
        {
            if ((((int)player.isTitan) == 1) && (((int)player.team) == team))
            {
                num++;
                if ((bool)player.dead)
                {
                    num2++;
                }
            }
        }
        return (num == num2);
    }

    public bool isTeamAllDead2(int team)
    {
        int num = 0;
        int num2 = 0;
        foreach (PhotonPlayer player in PhotonNetwork.playerList)
        {
            if ((player.isTitan == 1) && (player.team == team))
            {
                num++;
                if ((player.dead))
                {
                    num2++;
                }
            }
        }
        return (num == num2);
    }

    public void justRecompileThePlayerList()
    {

    }

    private void kickPhotonPlayer(string name)
    {
        UnityEngine.Debug.Log("KICK " + name + "!!!");
        foreach (PhotonPlayer player in PhotonNetwork.playerList)
        {
            if ((player.ID.ToString() == name) && !player.isMasterClient)
            {
                PhotonNetwork.CloseConnection(player);
                break;
            }
        }
    }

    private void kickPlayer(string kickPlayer, string kicker)
    {
        KickState state;
        bool flag = false;
        for (int i = 0; i < this.kicklist.Count; i++)
        {
            if (((KickState)this.kicklist[i]).name == kickPlayer)
            {
                state = (KickState)this.kicklist[i];
                state.addKicker(kicker);
                this.tryKick(state);
                flag = true;
                break;
            }
        }
        if (!flag)
        {
            state = new KickState();
            state.init(kickPlayer);
            state.addKicker(kicker);
            this.kicklist.Add(state);
            this.tryKick(state);
        }
    }

    public void kickPlayerRC(PhotonPlayer player, bool ban, string reason)
    {
        string nameplayer = player.name2;
        if (OnPrivateServer)
        {
            ServerCloseConnection(player, ban, nameplayer);
            PhotonNetwork.DestroyPlayerObjects(player);
            PhotonNetwork.CloseConnection(player);

        }
        else
        {
            PhotonNetwork.DestroyPlayerObjects(player);
            PhotonNetwork.CloseConnection(player);
            base.photonView.RPC("ignorePlayer", PhotonTargets.All, new object[] { player.ID });
            if (ignoreList.Contains(player.ID))
            {
                ignoreList.Remove(player.ID);
            }
            ignoreList.Add(player.ID);
            RaiseEventOptions options = new RaiseEventOptions
            {
                TargetActors = new int[] { player.ID }
            };
            PhotonNetwork.RaiseEvent(0xfe, null, true, options);
            if (ban && !banHash.ContainsKey(player.ID) && nameplayer != string.Empty)
            {
                banHash.Add(player.ID, nameplayer);
            }
            nameplayer = player.iscleanname + " ";
            if ((name).Length > 20)
            {
                name = name.Substring(0, 16) + "...";
            }
            else
            {
                nameplayer = player.ishexname;
            }
            if (reason != string.Empty)
            {
                InRoomChat.instance.addLINE((ban ? INC.la("auto_banned") : INC.la("auto_kicked")) + player.id + nameplayer + ". " + INC.la("player_reason") + reason);
            }
            PhotonNetwork.CloseConnection(player);
            PhotonNetwork.DestroyPlayerObjects(player);
            this.RecompilePlayerList(0.1f);
        }
    }
  
    [RPC]
    private void labelRPC(int setting, PhotonMessageInfo info)
    {
        if (PhotonView.Find(setting) != null)
        {
            PhotonPlayer owner = PhotonView.Find(setting).owner;
            if (owner == info.sender)
            {

                GameObject gameObject = PhotonView.Find(setting).gameObject;
                if (gameObject != null)
                {
                    HERO component = gameObject.GetComponent<HERO>();
                    if (component != null && component.myNetWorkName != null)
                    {
                        string str = owner.guildName;
                        string str2 = owner.ishexname;
                        if (str != string.Empty)
                        {
                            component.myNetWorkNameT.text = (str + "\n<color=#FFFF00>" + str2 + "</color>");
                        }
                        else
                        {
                            component.myNetWorkNameT.text = str2;
                        }
                    }
                }
            }
        }
    }

    void ComplateSettings()
    {
        if(QualitySettings.lodBias != (float)FengGameManagerMKII.settings[413])
        {
            QualitySettings.lodBias = (float)FengGameManagerMKII.settings[413];
        }
         if(QualitySettings.shadowCascades != (int)FengGameManagerMKII.settings[414])
        {
            QualitySettings.shadowCascades = (int)FengGameManagerMKII.settings[414];
        }
         if(QualitySettings.shadowDistance != (float)FengGameManagerMKII.settings[415])
        {
            QualitySettings.shadowDistance = (float)FengGameManagerMKII.settings[415];
        }
         if ((int)FengGameManagerMKII.settings[416] != (RenderSettings.fog ? 1 : 0) && lvlInfo.fog_mode)
        {
            RenderSettings.fog = ((int)FengGameManagerMKII.settings[416] == 1);
        }
          if( Camera.main.farClipPlane != (float)FengGameManagerMKII.settings[417])
        {
             Camera.main.farClipPlane = (float)FengGameManagerMKII.settings[417];
            
                 RenderSettings.fogStartDistance = (Camera.main.farClipPlane / 7) - Camera.main.farClipPlane;
                 RenderSettings.fogEndDistance = Camera.main.farClipPlane;
        }
    }
    private void LateUpdate()
    {
        if (this.gameStart)
        {
            if (panelScore != null)
            {
                panelScore.timeSession += Time.deltaTime;
            }
            ComplateSettings();
            foreach (HERO hero in this.heroes)
            {
                if (hero != null)
                {
                    hero.lateUpdate2();
                }
            }
            foreach (TITAN_EREN titan_eren in this.eT)
            {
                if (titan_eren != null)
                {
                    titan_eren.lateUpdate();
                }
            }
            foreach (TITAN titan in this.titans)
            {
                if (titan != null)
                {
                    titan.lateUpdate2();
                }
            }
            foreach (FEMALE_TITAN female_titan in this.fT)
            {
                if (female_titan != null)
                {
                    female_titan.lateUpdate2();
                }
            }
            this.core2();

        }
        CustomButtonCyan();
    }

    public void loadconfig()
    {
        int num;
        int num2;
        cuality_settings = PrefersCyan.GetInt("int_cuality_settings", 5);
        object[] objArray = new object[450];
        objArray[0] = PrefersCyan.GetInt("inthuman", 1);
        objArray[1] = PrefersCyan.GetInt("inttitan", 1);
        objArray[2] = PrefersCyan.GetInt("intlevel", 1);

        objArray[3] = "";
        objArray[4] = "";
        objArray[5] = "";
        objArray[6] = "";
        objArray[7] = "";
        objArray[8] = "";
        objArray[9] = "";
        objArray[10] = "";
        objArray[11] = "";
        objArray[12] = "";
        objArray[13] = "";
        objArray[14] = "";

        objArray[15] = PrefersCyan.GetInt("intgasenable", 0);
        objArray[16] = PrefersCyan.GetInt("inttitantype1", -1);
        objArray[17] = PrefersCyan.GetInt("inttitantype2", -1);
        objArray[18] = PrefersCyan.GetInt("inttitantype3", -1);
        objArray[19] = PrefersCyan.GetInt("inttitantype4", -1);
        objArray[20] = PrefersCyan.GetInt("inttitantype5", -1);
        objArray[21] = PrefersCyan.GetString("stringtitanhair1", string.Empty);
        objArray[22] = PrefersCyan.GetString("stringtitanhair2", string.Empty);
        objArray[23] = PrefersCyan.GetString("stringtitanhair3", string.Empty);
        objArray[24] = PrefersCyan.GetString("stringtitanhair4", string.Empty);
        objArray[25] = PrefersCyan.GetString("stringtitanhair5", string.Empty);
        objArray[26] = PrefersCyan.GetString("stringtitaneye1", string.Empty);
        objArray[27] = PrefersCyan.GetString("stringtitaneye2", string.Empty);
        objArray[28] = PrefersCyan.GetString("stringtitaneye3", string.Empty);
        objArray[29] = PrefersCyan.GetString("stringtitaneye4", string.Empty);
        objArray[30] = PrefersCyan.GetString("stringtitaneye5", string.Empty);
        objArray[31] = 0;
        objArray[32] = PrefersCyan.GetInt("inttitanR", 0);
        objArray[33] = PrefersCyan.GetString("stringtree1", "http://i.imgur.com/QhvQaOY.png");
        objArray[34] = PrefersCyan.GetString("stringtree2", "http://i.imgur.com/QhvQaOY.png");
        objArray[35] = PrefersCyan.GetString("stringtree3", "http://i.imgur.com/k08IX81.png");
        objArray[36] = PrefersCyan.GetString("stringtree4", "http://i.imgur.com/k08IX81.png");
        objArray[37] = PrefersCyan.GetString("stringtree5", "http://i.imgur.com/JQPNchU.png");
        objArray[38] = PrefersCyan.GetString("stringtree6", "http://i.imgur.com/JQPNchU.png");
        objArray[39] = PrefersCyan.GetString("stringtree7", "http://i.imgur.com/IZdYWv4.png");
        objArray[40] = PrefersCyan.GetString("stringtree8", "http://i.imgur.com/IZdYWv4.png");
        objArray[41] = PrefersCyan.GetString("stringleaf1", "http://i.imgur.com/oFGV5oL.png");
        objArray[42] = PrefersCyan.GetString("stringleaf2", "http://i.imgur.com/oFGV5oL.png");
        objArray[43] = PrefersCyan.GetString("stringleaf3", "http://i.imgur.com/mKzawrQ.png");
        objArray[44] = PrefersCyan.GetString("stringleaf4", "http://i.imgur.com/mKzawrQ.png");
        objArray[45] = PrefersCyan.GetString("stringleaf5", "http://i.imgur.com/Ymzavsi.png");
        objArray[46] = PrefersCyan.GetString("stringleaf6", "http://i.imgur.com/Ymzavsi.png");
        objArray[47] = PrefersCyan.GetString("stringleaf7", "http://i.imgur.com/oQfD1So.png");
        objArray[48] = PrefersCyan.GetString("stringleaf8", "http://i.imgur.com/oQfD1So.png");
        objArray[49] = PrefersCyan.GetString("stringforestG", "http://i.imgur.com/IsDTn7x.png");
        objArray[50] = PrefersCyan.GetInt("intforestR", 0);
        objArray[51] = PrefersCyan.GetString("stringhouse1", "http://i.imgur.com/wuy77R8.png");
        objArray[52] = PrefersCyan.GetString("stringhouse2", "http://i.imgur.com/wuy77R8.png");
        objArray[53] = PrefersCyan.GetString("stringhouse3", "http://i.imgur.com/wuy77R8.png");
        objArray[54] = PrefersCyan.GetString("stringhouse4", "http://i.imgur.com/wuy77R8.png");
        objArray[55] = PrefersCyan.GetString("stringhouse5", "http://i.imgur.com/wuy77R8.png");
        objArray[56] = PrefersCyan.GetString("stringhouse6", "http://i.imgur.com/wuy77R8.png");
        objArray[57] = PrefersCyan.GetString("stringhouse7", "http://i.imgur.com/wuy77R8.png");
        objArray[58] = PrefersCyan.GetString("stringhouse8", "http://i.imgur.com/wuy77R8.png");
        objArray[59] = PrefersCyan.GetString("stringcityG", "http://i.imgur.com/Mr9ZXip.png");
        objArray[60] = PrefersCyan.GetString("stringcityW", "http://i.imgur.com/Tm7XfQP.png");
        objArray[61] = PrefersCyan.GetString("stringcityH", "http://i.imgur.com/Q3YXkNM.png");
        objArray[62] = PrefersCyan.GetInt("intskinQ", 0);
        objArray[63] = PrefersCyan.GetInt("intskinQL", 1);
        objArray[64] = 0;
        objArray[65] = PrefersCyan.GetString("stringeren", string.Empty);
        objArray[66] = PrefersCyan.GetString("stringannie", string.Empty);
        objArray[67] = PrefersCyan.GetString("stringcolossal", string.Empty);
        objArray[68] = 100;
        objArray[69] = "default";
        objArray[70] = "1";
        objArray[71] = "1";
        objArray[72] = "1";
        objArray[73] = 1f;
        objArray[74] = 1f;
        objArray[75] = 1f;
        objArray[76] = 0;
        objArray[77] = string.Empty;
        objArray[78] = 0;
        objArray[79] = "1.0";
        objArray[80] = "1.0";
        objArray[81] = 0;
        objArray[82] = PrefersCyan.GetString("stringcnumber", "1");
        objArray[83] = "30";
        objArray[84] = 0;
        objArray[85] = PrefersCyan.GetString("stringcmax", "20");
        objArray[86] = PrefersCyan.GetString("stringtitanbody1", string.Empty);
        objArray[87] = PrefersCyan.GetString("stringtitanbody2", string.Empty);
        objArray[88] = PrefersCyan.GetString("stringtitanbody3", string.Empty);
        objArray[89] = PrefersCyan.GetString("stringtitanbody4", string.Empty);
        objArray[90] = PrefersCyan.GetString("stringtitanbody5", string.Empty);
        objArray[91] = 0;
        objArray[92] = PrefersCyan.GetInt("inttraildisable", 0);
        objArray[93] = PrefersCyan.GetInt("intwind", 0);
        objArray[94] = "";
        objArray[95] = PrefersCyan.GetString("stringsnapshot", "0");
        objArray[96] = "";
        objArray[97] = PrefersCyan.GetInt("intreel", 0);
        objArray[98] = PrefersCyan.GetString("stringreelin", "LeftControl");
        objArray[99] = PrefersCyan.GetString("stringreelout", "LeftAlt");
        objArray[100] = 0;
        objArray[101] = PrefersCyan.GetString("stringtforward", "W");
        objArray[102] = PrefersCyan.GetString("stringtback", "S");
        objArray[103] = PrefersCyan.GetString("stringtleft", "A");
        objArray[104] = PrefersCyan.GetString("stringtright", "D");
        objArray[105] = PrefersCyan.GetString("stringtwalk", "LeftShift");
        objArray[106] = PrefersCyan.GetString("stringtjump", "Space");
        objArray[107] = PrefersCyan.GetString("stringtpunch", "Q");
        objArray[108] = PrefersCyan.GetString("stringtslam", "E");
        objArray[109] = PrefersCyan.GetString("stringtgrabfront", "Alpha1");
        objArray[110] = PrefersCyan.GetString("stringtgrabback", "Alpha3");
        objArray[111] = PrefersCyan.GetString("stringtgrabnape", "Mouse1");
        objArray[112] = PrefersCyan.GetString("stringtantiae", "Mouse0");
        objArray[113] = PrefersCyan.GetString("stringtbite", "Alpha2");
        objArray[114] = PrefersCyan.GetString("stringtcover", "Z");
        objArray[115] = PrefersCyan.GetString("stringtsit", "X");
        objArray[116] = PrefersCyan.GetInt("intreel2", 0);
        objArray[117] = PrefersCyan.GetString("stringlforward", "W");
        objArray[118] = PrefersCyan.GetString("stringlback", "S");
        objArray[119] = PrefersCyan.GetString("stringlleft", "A");
        objArray[120] = PrefersCyan.GetString("stringlright", "D");
        objArray[121] = PrefersCyan.GetString("stringlup", "Mouse1");
        objArray[122] = PrefersCyan.GetString("stringldown", "Mouse0");
        objArray[123] = PrefersCyan.GetString("stringlcursor", "X");
        objArray[124] = PrefersCyan.GetString("stringlplace", "Space");
        objArray[125] = PrefersCyan.GetString("stringldel", "Backspace");
        objArray[126] = PrefersCyan.GetString("stringlslow", "LeftShift");
        objArray[127] = PrefersCyan.GetString("stringlrforward", "R");
        objArray[128] = PrefersCyan.GetString("stringlrback", "F");
        objArray[129] = PrefersCyan.GetString("stringlrleft", "Q");
        objArray[130] = PrefersCyan.GetString("stringlrright", "E");
        objArray[131] = PrefersCyan.GetString("stringlrccw", "Z");
        objArray[132] = PrefersCyan.GetString("stringlrcw", "C");
        objArray[133] = 0;

        objArray[161] = PrefersCyan.GetString("stringlfast", "LeftControl");
        objArray[162] = PrefersCyan.GetString("stringcustomGround", string.Empty);
        objArray[163] = PrefersCyan.GetString("stringforestskyfront", string.Empty);
        objArray[164] = PrefersCyan.GetString("stringforestskyback", string.Empty);
        objArray[165] = PrefersCyan.GetString("stringforestskyleft", string.Empty);
        objArray[166] = PrefersCyan.GetString("stringforestskyright", string.Empty);
        objArray[167] = PrefersCyan.GetString("stringforestskyup", string.Empty);
        objArray[168] = PrefersCyan.GetString("stringforestskydown", string.Empty);
        objArray[169] = PrefersCyan.GetString("stringcityskyfront", string.Empty);
        objArray[170] = PrefersCyan.GetString("stringcityskyback", string.Empty);
        objArray[171] = PrefersCyan.GetString("stringcityskyleft", string.Empty);
        objArray[172] = PrefersCyan.GetString("stringcityskyright", string.Empty);
        objArray[173] = PrefersCyan.GetString("stringcityskyup", string.Empty);
        objArray[174] = PrefersCyan.GetString("stringcityskydown", string.Empty);
        objArray[175] = PrefersCyan.GetString("stringcustomskyfront", string.Empty);
        objArray[176] = PrefersCyan.GetString("stringcustomskyback", string.Empty);
        objArray[177] = PrefersCyan.GetString("stringcustomskyleft", string.Empty);
        objArray[178] = PrefersCyan.GetString("stringcustomskyright", string.Empty);
        objArray[179] = PrefersCyan.GetString("stringcustomskyup", string.Empty);
        objArray[180] = PrefersCyan.GetString("stringcustomskydown", string.Empty);
        objArray[181] = PrefersCyan.GetInt("intdashenable", 0);
        objArray[182] = PrefersCyan.GetString("stringdashkey", "RightControl");
        objArray[183] = PrefersCyan.GetInt("intvsync", 0);
        objArray[184] = PrefersCyan.GetString("stringfpscap", "0");
        objArray[185] = 0;
        objArray[186] = 0;
        objArray[187] = 0;
        objArray[188] = 0;
        objArray[189] = PrefersCyan.GetInt("intspeedometer", 0);
        objArray[190] = 0;
        objArray[191] = string.Empty;
        objArray[192] = PrefersCyan.GetInt("intbombMode", 0);
        objArray[193] = PrefersCyan.GetInt("intteamMode", 0);
        objArray[194] = PrefersCyan.GetInt("introckThrow", 0);
        objArray[195] = PrefersCyan.GetInt("intexplodeModeOn", 0);
        objArray[196] = PrefersCyan.GetString("stringexplodeModeNum", "30");
        objArray[197] = PrefersCyan.GetInt("inthealthMode", 0);
        objArray[198] = PrefersCyan.GetString("stringhealthLower", "100");
        objArray[199] = PrefersCyan.GetString("stringhealthUpper", "200");
        objArray[200] = PrefersCyan.GetInt("intinfectionModeOn", 0);
        objArray[201] = PrefersCyan.GetString("stringinfectionModeNum", "1");
        objArray[202] = PrefersCyan.GetInt("intbanEren", 0);
        objArray[203] = PrefersCyan.GetInt("intmoreTitanOn", 0);
        objArray[204] = PrefersCyan.GetString("stringmoreTitanNum", "1");
        objArray[205] = PrefersCyan.GetInt("intdamageModeOn", 0);
        objArray[206] = PrefersCyan.GetString("stringdamageModeNum", "1000");
        objArray[207] = PrefersCyan.GetInt("intsizeMode", 0);
        objArray[208] = PrefersCyan.GetString("stringsizeLower", "1.0");
        objArray[209] = PrefersCyan.GetString("stringsizeUpper", "3.0");
        objArray[210] = PrefersCyan.GetInt("intspawnModeOn", 0);
        objArray[211] = PrefersCyan.GetString("stringnRate", "20.0");
        objArray[212] = PrefersCyan.GetString("stringaRate", "20.0");
        objArray[213] = PrefersCyan.GetString("stringjRate", "20.0");
        objArray[214] = PrefersCyan.GetString("stringcRate", "20.0");
        objArray[215] = PrefersCyan.GetString("stringpRate", "20.0");
        objArray[216] = PrefersCyan.GetInt("inthorseMode", 0);
        objArray[217] = PrefersCyan.GetInt("intwaveModeOn", 0);
        objArray[218] = PrefersCyan.GetString("stringwaveModeNum", "1");
        objArray[219] = PrefersCyan.GetInt("intfriendlyMode", 0);
        objArray[220] = PrefersCyan.GetInt("intpvpMode", 0);
        objArray[221] = PrefersCyan.GetInt("intmaxWaveOn", 0);
        objArray[222] = PrefersCyan.GetString("stringmaxWaveNum", "20");
        objArray[223] = PrefersCyan.GetInt("intendlessModeOn", 0);
        objArray[224] = PrefersCyan.GetString("stringendlessModeNum", "10");
        objArray[225] = "";
        objArray[226] = PrefersCyan.GetInt("intpointModeOn", 0);
        objArray[227] = PrefersCyan.GetString("stringpointModeNum", "50");
        objArray[228] = PrefersCyan.GetInt("intahssReload", 0);
        objArray[229] = PrefersCyan.GetInt("intpunkWaves", 0);
        objArray[230] = 0;
        objArray[231] = PrefersCyan.GetInt("intmapOn", 0);
        objArray[232] = PrefersCyan.GetString("stringmapMaximize", "Tab");
        objArray[233] = PrefersCyan.GetString("stringmapToggle", "M");
        objArray[234] = PrefersCyan.GetString("stringmapReset", "K");
        objArray[235] = PrefersCyan.GetInt("intglobalDisableMinimap", 0);
        objArray[236] = PrefersCyan.GetString("stringchatRebind", "None");
        objArray[237] = PrefersCyan.GetString("stringhforward", "W");
        objArray[238] = PrefersCyan.GetString("stringhback", "S");
        objArray[239] = PrefersCyan.GetString("stringhleft", "A");
        objArray[240] = PrefersCyan.GetString("stringhright", "D");
        objArray[241] = PrefersCyan.GetString("stringhwalk", "LeftShift");
        objArray[242] = PrefersCyan.GetString("stringhjump", "Q");
        objArray[243] = PrefersCyan.GetString("stringhmount", "LeftControl");
        objArray[244] = PrefersCyan.GetInt("intchatfeed", 0);
        objArray[245] = 0;
        objArray[246] = PrefersCyan.GetFloat("floatbombR", 1f);
        objArray[247] = PrefersCyan.GetFloat("floatbombG", 1f);
        objArray[248] = PrefersCyan.GetFloat("floatbombB", 1f);
        objArray[249] = PrefersCyan.GetFloat("floatbombA", 1f);
        objArray[250] = PrefersCyan.GetInt("intbombRadius", 5);
        objArray[251] = PrefersCyan.GetInt("intbombRange", 5);
        objArray[252] = PrefersCyan.GetInt("intbombSpeed", 5);
        objArray[253] = PrefersCyan.GetInt("intbombCD", 5);
        objArray[254] = PrefersCyan.GetString("stringcannonUp", "W");
        objArray[255] = PrefersCyan.GetString("stringcannonDown", "S");
        objArray[256] = PrefersCyan.GetString("stringcannonLeft", "A");
        objArray[257] = PrefersCyan.GetString("stringcannonRight", "D");
        objArray[258] = PrefersCyan.GetString("stringcannonFire", "Q");
        objArray[259] = PrefersCyan.GetString("stringcannonMount", "G");
        objArray[260] = PrefersCyan.GetString("stringcannonSlow", "LeftShift");
        objArray[261] = PrefersCyan.GetInt("intdeadlyCannon", 0);
        objArray[262] = PrefersCyan.GetString("stringliveCam", "Y");
        objArray[263] = 0;
        objArray[264] = PrefersCyan.GetInt("intOnStyleSetChat", 0);
        objArray[265] = PrefersCyan.GetString("stringSearhList", "");
        objArray[266] = PrefersCyan.GetString("stringMySignInChat", "");
        objArray[267] = PrefersCyan.GetInt("intCyanPanelServerList", 1);
        objArray[268] = PrefersCyan.GetInt("intBBStyleMyMess", 0);
        objArray[269] = PrefersCyan.GetFloat("floatSnapshotsQuality", 0f);
        objArray[270] = PrefersCyan.GetString("stringFont", "Arial");
        objArray[271] = PrefersCyan.GetColor("color_MyColorInChat", new Color(0.99f, 0.99f, 0.99f, 1f));
        objArray[272] = PrefersCyan.GetInt("intSortingServerList", 0);
        objArray[273] = PrefersCyan.GetString("stringNameMySkin", "");
        objArray[274] = PrefersCyan.GetInt("intOnBackgroundChat", 0);
        objArray[275] = PrefersCyan.GetInt("intNoPasswordRoom", 0);
        objArray[276] = PrefersCyan.GetInt("intFullRoom", 0);
        objArray[277] = PrefersCyan.GetInt("intAutoKickVivid", 0);
        objArray[278] = PrefersCyan.GetInt("intAutoKickHyper", 0);
        objArray[279] = PrefersCyan.GetInt("intAutoKickTokyo", 0);
        objArray[280] = PrefersCyan.GetInt("intFPSCount", 1);
        objArray[281] = PrefersCyan.GetInt("intBodyLean", 0);
        objArray[282] = PrefersCyan.GetInt("intGasTrail", 0);
        objArray[283] = PrefersCyan.GetInt("intAllGasTrail", 0);
        objArray[284] = PrefersCyan.GetInt("intShowSlashEffect", 0);
        objArray[285] = PrefersCyan.GetInt("intShowTitanSmokeEffect", 0);
        objArray[286] = PrefersCyan.GetInt("intNapeMelonEffect", 0);
        objArray[287] = PrefersCyan.GetInt("intBloodEffect", 0);
        objArray[288] = PrefersCyan.GetInt("intSmokeTitan_Spawn", 0);
        objArray[289] = PrefersCyan.GetInt("intShowAIM", 0);
        objArray[290] = PrefersCyan.GetInt("intShowSprits", 0);
        objArray[291] = PrefersCyan.GetInt("intShowAllNetworkName", 0);
        objArray[292] = PrefersCyan.GetInt("intShowChat", 0);
        objArray[293] = PrefersCyan.GetInt("intShowLabelCenter", 0);
        objArray[294] = PrefersCyan.GetInt("intShowLabelTopCenter", 0);
        objArray[295] = PrefersCyan.GetInt("intShowLabelTopLeft", 0);
        objArray[296] = PrefersCyan.GetInt("intFirstLogin", 0);
        objArray[297] = PrefersCyan.GetInt("intAutoKickGuest", 0);
        objArray[298] = PrefersCyan.GetInt("intAutoKickShowGuest", 0);
        objArray[299] = PrefersCyan.GetInt("intAutoKickNoName", 0);
        objArray[300] = PrefersCyan.GetInt("intShowAnimationNick", 0);
        objArray[301] = PrefersCyan.GetString("stringScriptAnimation", "");
        objArray[302] = PrefersCyan.GetString("stringTimerGoRestart", "3");
        objArray[303] = PrefersCyan.GetInt("intShowLabelTopRight", 0);
        objArray[304] = PrefersCyan.GetString("stringNumMessage_0", "Alpha4");
        objArray[305] = PrefersCyan.GetString("stringNumMessage_1", "Alpha5");
        objArray[306] = PrefersCyan.GetString("stringNumMessage_2", "Alpha6");
        objArray[307] = PrefersCyan.GetString("stringNumMessage_3", "Alpha7");
        objArray[308] = PrefersCyan.GetString("stringNumMessage_4", "Alpha8");
        objArray[309] = PrefersCyan.GetString("stringNumMessage_5", "Alpha9");
        objArray[310] = PrefersCyan.GetString("stringNumMessage_6", "Alpha0");
        objArray[311] = PrefersCyan.GetString("stringGeneralButton", "LeftControl");
        objArray[312] = PrefersCyan.GetString("stringNumSpeedRestart", "G");
        objArray[313] = PrefersCyan.GetString("stringMess_0", "");
        objArray[314] = PrefersCyan.GetString("stringMess_1", "");
        objArray[315] = PrefersCyan.GetString("stringMess_2", "");
        objArray[316] = PrefersCyan.GetString("stringMess_3", "");
        objArray[317] = PrefersCyan.GetString("stringMess_4", "");
        objArray[318] = PrefersCyan.GetString("stringMess_5", "");
        objArray[319] = PrefersCyan.GetString("stringMess_6", "");
        objArray[320] = PrefersCyan.GetInt("intEnableSS", 0);
        objArray[321] = PrefersCyan.GetInt("intcameraTilt", 0);
        objArray[322] = PrefersCyan.GetInt("intinvertMouseY", 1);
        objArray[323] = PrefersCyan.GetInt("intshowSSInGame", 1);
        objArray[324] = PrefersCyan.GetInt("intShowOriginalCamera", 0);
        objArray[325] = PrefersCyan.GetInt("intShowTPSCamera", 0);
        objArray[326] = PrefersCyan.GetInt("intShowWOWCamera", 0);
        objArray[327] = PrefersCyan.GetInt("intShowoldTPSCamera", 0);
        objArray[328] = PrefersCyan.GetInt("intShowMyNetworkName", 0);
        objArray[329] = PrefersCyan.GetInt("intInfoToChatGoRest", 0);
        objArray[330] = PrefersCyan.GetInt("intAudio_On", 0);
        objArray[331] = PrefersCyan.GetInt("intSpawnTitan_FT", 0);
        objArray[332] = PrefersCyan.GetString("stringArmor_FT", "0");
        objArray[333] = PrefersCyan.GetInt("intShowArmor_FT", 0);
        objArray[334] = PrefersCyan.GetInt("intShowConnectPlayers", 1);
        objArray[335] = PrefersCyan.GetInt("intKickTitanPlayer", 0);
        objArray[336] = PrefersCyan.GetInt("intShowStylish", 0);
        objArray[337] = PrefersCyan.GetString("stringMessageOfTheDay", "");
        objArray[338] = PrefersCyan.GetInt("intDisShowChat", 0);//Отключено
        objArray[339] = PrefersCyan.GetInt("intDisTextOnChat", 0);
        objArray[340] = PrefersCyan.GetInt("intShowBackgrounds", 0);
        objArray[341] = PrefersCyan.GetInt("intShowInfoTitans", 0);
        objArray[342] = PrefersCyan.GetInt("intCenzureFilter", 1);
        objArray[343] = PrefersCyan.GetInt("int_panel_creat_game_cm", 1);
        objArray[344] = PrefersCyan.GetString("stringSaving_lvl_multy", "The Forest III");
        objArray[345] = PrefersCyan.GetInt("int_diff_multy", 0);
        objArray[346] = PrefersCyan.GetInt("int_day_time_multy", 0);
        objArray[351] = PrefersCyan.GetString("string_color_multy", "[08FFFF]");
        objArray[349] = PrefersCyan.GetString("string_max_time_multy", "10");
        objArray[350] = PrefersCyan.GetString("string_max_player_multy", "10");
        objArray[348] = PrefersCyan.GetString("string_pass_multy", "");
        objArray[347] = PrefersCyan.GetString("string_name_multy", "Cyan_mod Server");
        objArray[352] = PrefersCyan.GetFloat("float_size_chat", 0f);
        objArray[353] = PrefersCyan.GetColor("color_background_chat", new Color(0, 0, 0, 0.3f));
        objArray[354] = PrefersCyan.GetInt("int_show_kills_in_min", 0);
        objArray[355] = PrefersCyan.GetInt("int_show_damage_in_min", 0);
        objArray[356] = PrefersCyan.GetColor("color_sprite_1", new Color(0f, 1f, 1f, 1f));
        objArray[357] = PrefersCyan.GetColor("color_sprite_2", new Color(0f, 0f, 0f, 0.5f));
        objArray[358] = PrefersCyan.GetVector2("vector2_pos_grafix_kills", new Vector2(0, 0));
        objArray[359] = PrefersCyan.GetVector2("vector2_pos_grafix_damage", new Vector2(0, 80));
        objArray[360] = PrefersCyan.GetInt("int_infinite_wp", 0);
        objArray[361] = PrefersCyan.GetString("string_guild_name_1", "");
        objArray[362] = PrefersCyan.GetString("string_guild_name_2", "");
        objArray[363] = PrefersCyan.GetString("string_nick", "GUEST" + UnityEngine.Random.Range(999, 99999));
        objArray[364] = PrefersCyan.GetString("string_chat_name", "");
        objArray[365] = PrefersCyan.GetInt("int_panel_single_set", 0);
        objArray[366] = null; ///Свободный слот.
        objArray[367] = null; ///Свободный слот.
        objArray[368] = PrefersCyan.GetInt("int_private_server", 0);
        objArray[369] = PrefersCyan.GetColor("color_system_message", new Color(0f, 0.99f, 0f, 0.99f));
        objArray[370] = PrefersCyan.GetString("string_screenshot", "O");
        objArray[371] = PrefersCyan.GetString("string_debug_console", "F1");
        objArray[372] = PrefersCyan.GetString("string_object_list", "F2");

        objArray[373] = PrefersCyan.GetString("string_first_num", "");
        objArray[374] = PrefersCyan.GetString("string_ending_num", "");
        objArray[375] = PrefersCyan.GetInt("int_style_system_mess", 0);
        objArray[376] = PrefersCyan.GetInt("int_clean_hex", 0);

        objArray[377] = PrefersCyan.GetString("string_char_i_love", "F7");
        objArray[378] = PrefersCyan.GetString("string_char_what", "F8");
        objArray[379] = PrefersCyan.GetString("string_char_class", "F9");
        objArray[380] = PrefersCyan.GetString("string_char_class_1", "F10");
        objArray[381] = PrefersCyan.GetString("string_char_danger", "F11");
        objArray[382] = PrefersCyan.GetString("string_char_fuck", "F12");
        objArray[383] = PrefersCyan.GetInt("int_time_in_chat", 0);
        objArray[384] = PrefersCyan.GetInt("int_conection", 0);
        objArray[385] = PrefersCyan.GetInt("int_diss_objects", 1);
        objArray[386] = PrefersCyan.GetInt("int_part_obj_0", 0);
        objArray[387] = PrefersCyan.GetInt("int_part_obj_1", 0);
        objArray[388] = PrefersCyan.GetInt("int_part_obj_2", 0);
        objArray[389] = PrefersCyan.GetColor("color_part_obj_0", new Color(0f, 0f, 0f, 1f));
        objArray[390] = PrefersCyan.GetColor("color_part_obj_1", new Color(0f, 0f, 0f, 1f));
        objArray[391] = PrefersCyan.GetColor("color_part_obj_2", new Color(0f, 0f, 0f, 1f));
        objArray[392] = PrefersCyan.GetInt("int_cyan_bot", 0);
        objArray[393] = PrefersCyan.GetInt("int_prop_anti_spam", 0);
        objArray[394] = PrefersCyan.GetString("string_custom_w_urll", "http://i.imgur.com/c7966MU.png");
        objArray[395] = PrefersCyan.GetString("string_custom_w_urlr", "http://i.imgur.com/c7966MU.png");
        objArray[396] = PrefersCyan.GetInt("int_custom_w_pr", 0);
        objArray[397] = PrefersCyan.GetColor("color_custom_w_color", new Color(0f, 0f, 0f, 1f));
        objArray[398] = PrefersCyan.GetInt("int_auto_spam_stats", 1);
        objArray[399] = PrefersCyan.GetInt("int_current_server", 0);
        objArray[400] = PrefersCyan.GetInt("int_grafix_blur", 0);
        objArray[401] = PrefersCyan.GetInt("int_grafix_shad_project", 0);
        objArray[402] = PrefersCyan.GetInt("int_aniso_filter", 0);
        objArray[403] = PrefersCyan.GetInt("int_blend_weinth", 0);
        objArray[404] = PrefersCyan.GetString("string_button_focus_p", "F3");
        objArray[405] = PrefersCyan.GetString("string_sid_down_titan", "H");
        objArray[406] = PrefersCyan.GetString("string_laugh_titan", "J");
        objArray[407] = PrefersCyan.GetString("string_attack_titan_d", "L");
        objArray[408] = PrefersCyan.GetString("string_attack_titan_f", "I");
        objArray[410] = PrefersCyan.GetString("string_single_mapname", "[S]Forest Survive(no crawler)");
        IN_GAME_MAIN_CAMERA.singleCharacter = PrefersCyan.GetString("string_single_hero", "MIKASA");
        objArray[409] = PrefersCyan.GetInt("int_single_daytime", 0);
        objArray[411] = PrefersCyan.GetInt("int_single_diffuculty", 1);
        objArray[412] = PrefersCyan.GetInt("int_costume_value", 1);
        objArray[413] = PrefersCyan.GetFloat("float_grafix_lod", 0.2f);
        objArray[414] = PrefersCyan.GetInt("int_grafix_shadow", 0);
        objArray[415] = PrefersCyan.GetFloat("float_grafix_shadow_distance", 0);
        objArray[416] = PrefersCyan.GetInt("int_fog_mode", 0);
        objArray[417] = PrefersCyan.GetFloat("float_view_distance", 1600);
        objArray[418] = PrefersCyan.GetInt("int_fov_enable", 0);
        objArray[419] = PrefersCyan.GetFloat("float_fixed_fov", 50);

        objArray[420] = PrefersCyan.GetString("string_custom_attack0", "Mouse0");
        objArray[421] = PrefersCyan.GetString("string_custom_attack1", "Mouse1");
        objArray[422] = PrefersCyan.GetString("string_custom_bothRope", "Space"); 
        objArray[423] = PrefersCyan.GetString("string_custom_camera", "C");
        objArray[424] = PrefersCyan.GetString("string_custom_dodge", "LeftControl");
        objArray[425] = PrefersCyan.GetString("string_custom_down", "S");
        objArray[426] = PrefersCyan.GetString("string_custom_flare1", "Alpha1");
        objArray[427] = PrefersCyan.GetString("string_custom_flare2", "Alpha2");
        objArray[428] = PrefersCyan.GetString("string_custom_flare3", "Alpha3");
        objArray[430] = PrefersCyan.GetString("string_custom_focus", "F");
        objArray[431] = PrefersCyan.GetString("string_custom_fullscreen", "Backspace");
        objArray[432] = PrefersCyan.GetString("string_custom_hideCursor", "X");
        objArray[433] = PrefersCyan.GetString("string_custom_jump", "LeftShift");
        objArray[434] = PrefersCyan.GetString("string_custom_left", "A");
        objArray[435] = PrefersCyan.GetString("string_custom_leftRope", "Q");
        objArray[436] = PrefersCyan.GetString("string_custom_pause", "P");
        objArray[437] = PrefersCyan.GetString("string_custom_reload", "R");
        objArray[438] = PrefersCyan.GetString("string_custom_restart", "T");
        objArray[439] = PrefersCyan.GetString("string_custom_right", "D");
        objArray[440] = PrefersCyan.GetString("string_custom_rightRope", "E");
        objArray[441] = PrefersCyan.GetString("string_custom_salute", "N");
        objArray[442] = PrefersCyan.GetString("string_custom_up", "W");
        objArray[443] = PrefersCyan.GetInt("int_hook_kill_enable", 0);

        Nanimation = new List<string>();
        foreach (string str332 in ((string)objArray[301]).Split(new char[] { ',' }))
        {
            string dtr = str332.Trim();
            if (dtr != string.Empty)
            {
                Nanimation.Add(dtr);
            }
        }
      
        inputRC = new InputManagerRC();
        inputRC.setInputCustom(InputCode.custom_attack0, (string)objArray[420]);
        inputRC.setInputCustom(InputCode.custom_attack1, (string)objArray[421]);
        inputRC.setInputCustom(InputCode.custom_bothRope, (string)objArray[422]);
        inputRC.setInputCustom(InputCode.custom_camera, (string)objArray[423]);
        inputRC.setInputCustom(InputCode.custom_dodge, (string)objArray[424]);
        inputRC.setInputCustom(InputCode.custom_down, (string)objArray[425]);
        inputRC.setInputCustom(InputCode.custom_flare1, (string)objArray[426]);
        inputRC.setInputCustom(InputCode.custom_flare2, (string)objArray[427]);
        inputRC.setInputCustom(InputCode.custom_flare3, (string)objArray[428]);
        inputRC.setInputCustom(InputCode.custom_focus, (string)objArray[430]);
        inputRC.setInputCustom(InputCode.custom_fullscreen, (string)objArray[431]);
        inputRC.setInputCustom(InputCode.custom_hideCursor, (string)objArray[432]);
        inputRC.setInputCustom(InputCode.custom_jump, (string)objArray[433]);
        inputRC.setInputCustom(InputCode.custom_left, (string)objArray[434]);
        inputRC.setInputCustom(InputCode.custom_leftRope, (string)objArray[435]);
        inputRC.setInputCustom(InputCode.custom_pause, (string)objArray[436]);
        inputRC.setInputCustom(InputCode.custom_reload, (string)objArray[437]);
        inputRC.setInputCustom(InputCode.custom_restart, (string)objArray[438]);
        inputRC.setInputCustom(InputCode.custom_right, (string)objArray[439]);
        inputRC.setInputCustom(InputCode.custom_rightRope, (string)objArray[440]);
        inputRC.setInputCustom(InputCode.custom_salute, (string)objArray[441]);
        inputRC.setInputCustom(InputCode.custom_up, (string)objArray[442]);

        inputRC.setInputCyan(InputCode.message_0, (string)objArray[304]);
        inputRC.setInputCyan(InputCode.message_1, (string)objArray[305]);
        inputRC.setInputCyan(InputCode.message_2, (string)objArray[306]);
        inputRC.setInputCyan(InputCode.message_3, (string)objArray[307]);
        inputRC.setInputCyan(InputCode.message_4, (string)objArray[308]);
        inputRC.setInputCyan(InputCode.message_5, (string)objArray[309]);
        inputRC.setInputCyan(InputCode.message_6, (string)objArray[310]);
        inputRC.setInputCyan(InputCode.buttonX, (string)objArray[311]);
        inputRC.setInputCyan(InputCode.speed_rest, (string)objArray[312]);
        inputRC.setInputCyan(InputCode.screenshot, (string)objArray[370]);
        inputRC.setInputCyan(InputCode.debug_console, (string)objArray[371]);
        inputRC.setInputCyan(InputCode.objects_list, (string)objArray[372]);
        inputRC.setInputCyan(InputCode.Focus_player, (string)objArray[404]);

      
     

        for (int s = 0; s < 6; s++)
        {
            int num999 = 12 + s;
            inputRC.setInputCyan(num999, (string)objArray[377 + s]);
        }

        inputRC.setInputHuman(InputCodeRC.reelin, (string)objArray[98]);
        inputRC.setInputHuman(InputCodeRC.reelout, (string)objArray[99]);
        inputRC.setInputHuman(InputCodeRC.dash, (string)objArray[182]);
        inputRC.setInputHuman(InputCodeRC.mapMaximize, (string)objArray[232]);
        inputRC.setInputHuman(InputCodeRC.mapToggle, (string)objArray[233]);
        inputRC.setInputHuman(InputCodeRC.mapReset, (string)objArray[234]);
        inputRC.setInputHuman(InputCodeRC.chat, (string)objArray[236]);
        inputRC.setInputHuman(InputCodeRC.liveCam, (string)objArray[262]);

        if (!Enum.IsDefined(typeof(KeyCode), (string)objArray[232]))
        {
            objArray[232] = "None";
        }
        if (!Enum.IsDefined(typeof(KeyCode), (string)objArray[233]))
        {
            objArray[233] = "None";
        }
        if (!Enum.IsDefined(typeof(KeyCode), (string)objArray[234]))
        {
            objArray[234] = "None";
        }
        for (num = 0; num < 15; num++)
        {
                inputRC.setInputTitan(num, (string)objArray[101 + num]);
        }
        inputRC.setInputTitan(InputCodeRC.titanSitDown, (string)objArray[405]);
        inputRC.setInputTitan(InputCodeRC.titanlaugh, (string)objArray[406]);
        inputRC.setInputTitan(InputCodeRC.titanAttackDown, (string)objArray[407]);
        inputRC.setInputTitan(InputCodeRC.titanAttackFace, (string)objArray[408]);
        for (num = 0; num < 16; num++)
        {
            inputRC.setInputLevel(num, (string)objArray[117 + num]);
        }
        for (num = 0; num < 7; num++)
        {
            inputRC.setInputHorse(num, (string)objArray[237 + num]);
        }
        for (num = 0; num < 7; num++)
        {
            inputRC.setInputCannon(num, (string)objArray[254 + num]);
        }
        inputRC.setInputLevel(InputCodeRC.levelFast, (string)objArray[161]);
        Application.targetFrameRate = -1;
        if (int.TryParse((string)objArray[184], out num2) && (num2 > 0))
        {
            Application.targetFrameRate = num2;
        }
        QualitySettings.vSyncCount = 0;
        if (((int)objArray[183]) == 1)
        {
            QualitySettings.vSyncCount = 1;
        }


        linkHash = new ExitGames.Client.Photon.Hashtable[] { new ExitGames.Client.Photon.Hashtable(), new ExitGames.Client.Photon.Hashtable(), new ExitGames.Client.Photon.Hashtable(), new ExitGames.Client.Photon.Hashtable(), new ExitGames.Client.Photon.Hashtable() };
        settings = objArray;
        this.scroll = Vector2.zero;
        this.scroll2 = Vector2.zero;
        this.distanceSlider = PrefersCyan.GetFloat("floatcameraDistance", 1f);
        if (distanceSlider > 1f)
        {
            distanceSlider = 1f;
        }
        AudioListener.volume = PrefersCyan.GetFloat("floatvol", 1f);
        IN_GAME_MAIN_CAMERA.sensitivityMulti = PrefersCyan.GetFloat("floatMouseSensitivity", 0.22f);
        IN_GAME_MAIN_CAMERA.cameraTilt = (int)FengGameManagerMKII.settings[321];
        IN_GAME_MAIN_CAMERA.invertY = (int)FengGameManagerMKII.settings[322];
        IN_GAME_MAIN_CAMERA.cameraMode = (CAMERA_TYPE)PrefersCyan.GetInt("int_camera_type", 0);
        this.transparencySlider = 1f;
        IN_GAME_MAIN_CAMERA.cameraDistance = 0.3f + this.distanceSlider;

        QualitySettings.masterTextureLimit = PrefersCyan.GetInt("intskinQ", 0);
    }

    private void loadskin()
    {
        GameObject[] objArray;
        int num;
        GameObject obj2;
        if (((int)settings[0x40]) >= 100)
        {
            string[] strArray = new string[] { "Flare", "LabelInfoBottomRight", "LabelNetworkStatus", "skill_cd_bottom", "GasUI" };
            objArray = (GameObject[])UnityEngine.Object.FindObjectsOfType(typeof(GameObject));
            for (num = 0; num < objArray.Length; num++)
            {
                obj2 = objArray[num];
                if ((obj2.name.Contains("TREE") || obj2.name.Contains("aot_supply")) || obj2.name.Contains("gameobjectOutSide"))
                {
                    UnityEngine.Object.Destroy(obj2);
                }
            }
            CyanMod.CachingsGM.Find("Cube_001").renderer.material.mainTexture = ((Material)RCassets.Load("grass")).mainTexture;
            UnityEngine.Object.Instantiate(RCassets.Load("spawnPlayer"), new Vector3(-10f, 1f, -10f), new Quaternion(0f, 0f, 0f, 1f));
            for (num = 0; num < strArray.Length; num++)
            {
                string name = strArray[num];
                GameObject obj3 = CyanMod.CachingsGM.Find(name);
                if (obj3 != null)
                {
                    UnityEngine.Object.Destroy(obj3);
                }
            }
            IN_GAME_MAIN_CAMERA.instance.Smov.disable = true;
        }
        else
        {
            GameObject obj4;
            string[] strArray2;
            int num2;
            InstantiateTracker.instance.Dispose();
            if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && PhotonNetwork.isMasterClient)
            {
                this.updateTime = 1f;
                if (oldScriptLogic != currentScriptLogic)
                {
                    intVariables.Clear();
                    boolVariables.Clear();
                    stringVariables.Clear();
                    floatVariables.Clear();
                    RCEvents.Clear();
                    RCVariableNames.Clear();
                    playerVariables.Clear();
                    titanVariables.Clear();
                    RCRegionTriggers.Clear();
                    oldScriptLogic = currentScriptLogic;
                    this.compileScript(currentScriptLogic);
                    if (RCEvents.ContainsKey("OnFirstLoad"))
                    {
                        ((RCEvent)RCEvents["OnFirstLoad"]).checkEvent();
                    }
                }
                if (RCEvents.ContainsKey("OnRoundStart"))
                {
                    ((RCEvent)RCEvents["OnRoundStart"]).checkEvent();
                }
                base.photonView.RPC("setMasterRC", PhotonTargets.All, new object[0]);
            }
            logicLoaded = true;
            this.racingSpawnPoint = new Vector3(0f, 0f, 0f);
            this.racingSpawnPointSet = false;
            this.racingDoors = new List<GameObject>();
            this.allowedToCannon = new Dictionary<int, CannonValues>();
            if ((!level.StartsWith("Custom") && (((int)settings[2]) == 1)) && ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE) || PhotonNetwork.isMasterClient))
            {
                obj4 = CyanMod.CachingsGM.Find("aot_supply");
                if ((obj4 != null) && (Minimap.instance != null))
                {
                    Minimap.instance.TrackGameObjectOnMinimap(obj4, Color.white, false, true, Minimap.IconStyle.SUPPLY);
                }
                string url = string.Empty;
                string str3 = string.Empty;
                string n = string.Empty;
                strArray2 = new string[] { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty };
                if (lvlInfo.mapName.Contains("City"))
                {
                    for (num = 0x33; num < 0x3b; num++)
                    {
                        url = url + ((string)settings[num]) + ",";
                    }
                    url.TrimEnd(new char[] { ',' });
                    num2 = 0;
                    while (num2 < 250)
                    {
                        n = n + Convert.ToString((int)UnityEngine.Random.Range((float)0f, (float)8f));
                        num2++;
                    }
                    str3 = ((string)settings[0x3b]) + "," + ((string)settings[60]) + "," + ((string)settings[0x3d]);
                    for (num = 0; num < 6; num++)
                    {
                        strArray2[num] = (string)settings[num + 0xa9];
                    }
                }
                else if (lvlInfo.mapName.Contains("Forest"))
                {
                    for (int i = 0x21; i < 0x29; i++)
                    {
                        url = url + ((string)settings[i]) + ",";
                    }
                    url.TrimEnd(new char[] { ',' });
                    for (int j = 0x29; j < 0x31; j++)
                    {
                        str3 = str3 + ((string)settings[j]) + ",";
                    }
                    str3 = str3 + ((string)settings[0x31]);
                    for (int k = 0; k < 150; k++)
                    {
                        string str5 = Convert.ToString((int)UnityEngine.Random.Range((float)0f, (float)8f));
                        n = n + str5;
                        if (((int)settings[50]) == 0)
                        {
                            n = n + str5;
                        }
                        else
                        {
                            n = n + Convert.ToString((int)UnityEngine.Random.Range((float)0f, (float)8f));
                        }
                    }
                    for (num = 0; num < 6; num++)
                    {
                        strArray2[num] = (string)settings[num + 0xa3];
                    }
                }
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                {
                    base.StartCoroutine(this.loadskinE(n, url, str3, strArray2));
                }
                else if (PhotonNetwork.isMasterClient)
                {
                    base.photonView.RPC("loadskinRPC", PhotonTargets.AllBuffered, new object[] { n, url, str3, strArray2 });
                }
            }
            else if (level.StartsWith("Custom") && (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE))
            {
                GameObject[] objArray2 = GameObject.FindGameObjectsWithTag("playerRespawn");
                for (num = 0; num < objArray2.Length; num++)
                {
                    obj4 = objArray2[num];
                    obj4.transform.position = new Vector3(UnityEngine.Random.Range((float)-5f, (float)5f), 0f, UnityEngine.Random.Range((float)-5f, (float)5f));
                }
                objArray = (GameObject[])UnityEngine.Object.FindObjectsOfType(typeof(GameObject));
                for (num = 0; num < objArray.Length; num++)
                {
                    obj2 = objArray[num];
                    if (obj2.name.Contains("TREE") || obj2.name.Contains("aot_supply"))
                    {
                        UnityEngine.Object.Destroy(obj2);
                    }
                    else if (((obj2.name == "Cube_001") && (obj2.transform.parent.gameObject.tag != "player")) && (obj2.renderer != null))
                    {
                        this.groundList.Add(obj2);
                        obj2.renderer.material.mainTexture = ((Material)RCassets.Load("grass")).mainTexture;
                    }
                }
                if (PhotonNetwork.isMasterClient)
                {
                    int num6;
                    strArray2 = new string[] { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty };
                    for (num = 0; num < 6; num++)
                    {
                        strArray2[num] = (string)settings[num + 0xaf];
                    }
                    strArray2[6] = (string)settings[0xa2];
                    if (int.TryParse((string)settings[0x55], out num6))
                    {
                        RCSettings.titanCap = num6;
                    }
                    else
                    {
                        RCSettings.titanCap = 0;
                        settings[0x55] = "0";
                    }
                    RCSettings.titanCap = Math.Min(50, RCSettings.titanCap);
                    base.photonView.RPC("clearlevel", PhotonTargets.AllBuffered, new object[] { strArray2, RCSettings.gameType });
                    RCRegions.Clear();
                    if (oldScript != currentScript)
                    {

                        this.levelCache.Clear();
                        this.titanSpawns.Clear();
                        this.playerSpawnsC.Clear();
                        this.playerSpawnsM.Clear();
                        this.titanSpawners.Clear();
                        currentLevel = string.Empty;
                        if (currentScript == string.Empty)
                        {
                            PhotonNetwork.player.currentLevel = currentLevel;
                            oldScript = currentScript;
                        }
                        else
                        {
                            string[] strArray3 = Regex.Replace(currentScript, @"\s+", "").Replace("\r\n", "").Replace("\n", "").Replace("\r", "").Split(new char[] { ';' });
                            for (num = 0; num < (Mathf.FloorToInt((float)((strArray3.Length - 1) / 100)) + 1); num++)
                            {
                                string[] strArray4;
                                int num7;
                                string[] strArray5;
                                string str6;
                                if (num < Mathf.FloorToInt((float)(strArray3.Length / 100)))
                                {
                                    strArray4 = new string[0x65];
                                    num7 = 0;
                                    num2 = 100 * num;
                                    while (num2 < ((100 * num) + 100))
                                    {
                                        if (strArray3[num2].StartsWith("spawnpoint"))
                                        {
                                            strArray5 = strArray3[num2].Split(new char[] { ',' });
                                            if (strArray5[1] == "titan")
                                            {
                                                this.titanSpawns.Add(new Vector3(Convert.ToSingle(strArray5[2]), Convert.ToSingle(strArray5[3]), Convert.ToSingle(strArray5[4])));
                                            }
                                            else if (strArray5[1] == "playerC")
                                            {
                                                this.playerSpawnsC.Add(new Vector3(Convert.ToSingle(strArray5[2]), Convert.ToSingle(strArray5[3]), Convert.ToSingle(strArray5[4])));
                                            }
                                            else if (strArray5[1] == "playerM")
                                            {
                                                this.playerSpawnsM.Add(new Vector3(Convert.ToSingle(strArray5[2]), Convert.ToSingle(strArray5[3]), Convert.ToSingle(strArray5[4])));
                                            }
                                        }
                                        strArray4[num7] = strArray3[num2];
                                        num7++;
                                        num2++;
                                    }
                                    str6 = UnityEngine.Random.Range(0x2710, 0x1869f).ToString();
                                    strArray4[100] = str6;
                                    currentLevel = currentLevel + str6;
                                    this.levelCache.Add(strArray4);
                                }
                                else
                                {
                                    strArray4 = new string[(strArray3.Length % 100) + 1];
                                    num7 = 0;
                                    for (num2 = 100 * num; num2 < ((100 * num) + (strArray3.Length % 100)); num2++)
                                    {
                                        if (strArray3[num2].StartsWith("spawnpoint"))
                                        {
                                            strArray5 = strArray3[num2].Split(new char[] { ',' });
                                            if (strArray5[1] == "titan")
                                            {
                                                this.titanSpawns.Add(new Vector3(Convert.ToSingle(strArray5[2]), Convert.ToSingle(strArray5[3]), Convert.ToSingle(strArray5[4])));
                                            }
                                            else if (strArray5[1] == "playerC")
                                            {
                                                this.playerSpawnsC.Add(new Vector3(Convert.ToSingle(strArray5[2]), Convert.ToSingle(strArray5[3]), Convert.ToSingle(strArray5[4])));
                                            }
                                            else if (strArray5[1] == "playerM")
                                            {
                                                this.playerSpawnsM.Add(new Vector3(Convert.ToSingle(strArray5[2]), Convert.ToSingle(strArray5[3]), Convert.ToSingle(strArray5[4])));
                                            }
                                        }
                                        strArray4[num7] = strArray3[num2];
                                        num7++;
                                    }
                                    str6 = UnityEngine.Random.Range(0x2710, 0x1869f).ToString();
                                    strArray4[strArray3.Length % 100] = str6;
                                    currentLevel = currentLevel + str6;
                                    this.levelCache.Add(strArray4);
                                }
                            }
                            List<string> list = new List<string>();
                            foreach (Vector3 vector in this.titanSpawns)
                            {
                                list.Add("titan," + vector.x.ToString() + "," + vector.y.ToString() + "," + vector.z.ToString());
                            }
                            foreach (Vector3 vector2 in this.playerSpawnsC)
                            {
                                list.Add("playerC," + vector2.x.ToString() + "," + vector2.y.ToString() + "," + vector2.z.ToString());
                            }
                            foreach (Vector3 vector3 in this.playerSpawnsM)
                            {
                                list.Add("playerM," + vector3.x.ToString() + "," + vector3.y.ToString() + "," + vector3.z.ToString());
                            }
                            string item = "a" + UnityEngine.Random.Range(0x2710, 0x1869f).ToString();
                            list.Add(item);
                            currentLevel = item + currentLevel;
                            this.levelCache.Insert(0, list.ToArray());
                            string str8 = "z" + UnityEngine.Random.Range(0x2710, 0x1869f).ToString();
                            this.levelCache.Add(new string[] { str8 });
                            currentLevel = currentLevel + str8;
                            PhotonNetwork.player.currentLevel = currentLevel;
                            oldScript = currentScript;
                        }
                    }
                    for (num = 0; num < PhotonNetwork.playerList.Length; num++)
                    {
                        PhotonPlayer player = PhotonNetwork.playerList[num];
                        if (!player.isMasterClient)
                        {
                            this.playersRPC.Add(player);
                        }
                    }
                    base.StartCoroutine(this.customlevelE(this.playersRPC));
                    base.StartCoroutine(this.customlevelcache());
                }
            }
        }
    }

    private IEnumerator loadskinE(string n, string url, string url2, string[] skybox)
    {
        bool mipmap = true;
        bool iteratorVariable1 = false;
        if (((int)settings[0x3f]) == 1)
        {
            mipmap = false;
        }
        if ((skybox.Length > 5) && ((((skybox[0] != string.Empty) || (skybox[1] != string.Empty)) || ((skybox[2] != string.Empty) || (skybox[3] != string.Empty))) || ((skybox[4] != string.Empty) || (skybox[5] != string.Empty))))
        {
            string key = string.Join(",", skybox);
            if (!linkHash[1].ContainsKey(key))
            {
                iteratorVariable1 = true;
                Material material = Camera.main.GetComponent<Skybox>().material;
                string iteratorVariable4 = skybox[0];
                string iteratorVariable5 = skybox[1];
                string iteratorVariable6 = skybox[2];
                string iteratorVariable7 = skybox[3];
                string iteratorVariable8 = skybox[4];
                string iteratorVariable9 = skybox[5];
                if ((iteratorVariable4.EndsWith(".jpg") || iteratorVariable4.EndsWith(".png")) || iteratorVariable4.EndsWith(".jpeg"))
                {
                    WWW link = new WWW(iteratorVariable4);
                    yield return link;
                    if (link.error == null)
                    {
                        Texture2D texture = cext.loadimage(link, mipmap, 0x7a120);
                        link.Dispose();
                        texture.wrapMode = TextureWrapMode.Clamp;
                        material.SetTexture("_FrontTex", texture);
                    }
                    else
                    {
                        PanelInformer.instance.Add("Error load skins " + base.name + ":" + link.url, PanelInformer.LOG_TYPE.DANGER);
                    }
                }
                if ((iteratorVariable5.EndsWith(".jpg") || iteratorVariable5.EndsWith(".png")) || iteratorVariable5.EndsWith(".jpeg"))
                {
                    WWW iteratorVariable12 = new WWW(iteratorVariable5);
                    yield return iteratorVariable12;
                    if (iteratorVariable12.error == null)
                    {
                        Texture2D iteratorVariable13 = cext.loadimage(iteratorVariable12, mipmap, 0x7a120);
                        iteratorVariable12.Dispose();
                        iteratorVariable13.wrapMode = TextureWrapMode.Clamp;
                        material.SetTexture("_BackTex", iteratorVariable13);
                    }
                    else
                    {
                        PanelInformer.instance.Add("Error load skins " + base.name + ":" + iteratorVariable12.url, PanelInformer.LOG_TYPE.DANGER);
                    }
                }
                if ((iteratorVariable6.EndsWith(".jpg") || iteratorVariable6.EndsWith(".png")) || iteratorVariable6.EndsWith(".jpeg"))
                {
                    WWW iteratorVariable14 = new WWW(iteratorVariable6);
                    yield return iteratorVariable14;
                    if (iteratorVariable14.error == null)
                    {
                        Texture2D iteratorVariable15 = cext.loadimage(iteratorVariable14, mipmap, 0x7a120);
                        iteratorVariable14.Dispose();
                        iteratorVariable15.wrapMode = TextureWrapMode.Clamp;
                        material.SetTexture("_LeftTex", iteratorVariable15);
                    }
                    else
                    {
                        PanelInformer.instance.Add("Error load skins " + base.name + ":" + iteratorVariable14.url, PanelInformer.LOG_TYPE.DANGER);
                    }
                }
                if ((iteratorVariable7.EndsWith(".jpg") || iteratorVariable7.EndsWith(".png")) || iteratorVariable7.EndsWith(".jpeg"))
                {
                    WWW iteratorVariable16 = new WWW(iteratorVariable7);
                    yield return iteratorVariable16;
                    if (iteratorVariable16.error == null)
                    {
                        Texture2D iteratorVariable17 = cext.loadimage(iteratorVariable16, mipmap, 0x7a120);
                        iteratorVariable16.Dispose();
                        iteratorVariable17.wrapMode = TextureWrapMode.Clamp;
                        material.SetTexture("_RightTex", iteratorVariable17);
                    }
                    else
                    {
                        PanelInformer.instance.Add("Error load skins " + base.name + ":" + iteratorVariable16.url, PanelInformer.LOG_TYPE.DANGER);
                    }
                }
                if ((iteratorVariable8.EndsWith(".jpg") || iteratorVariable8.EndsWith(".png")) || iteratorVariable8.EndsWith(".jpeg"))
                {
                    WWW iteratorVariable18 = new WWW(iteratorVariable8);
                    yield return iteratorVariable18;
                    if (iteratorVariable18.error == null)
                    {
                        Texture2D iteratorVariable19 = cext.loadimage(iteratorVariable18, mipmap, 0x7a120);
                        iteratorVariable18.Dispose();
                        iteratorVariable19.wrapMode = TextureWrapMode.Clamp;
                        material.SetTexture("_UpTex", iteratorVariable19);
                    }
                    else
                    {
                        PanelInformer.instance.Add("Error load skins " + base.name + ":" + iteratorVariable18.url, PanelInformer.LOG_TYPE.DANGER);
                    }
                }
                if ((iteratorVariable9.EndsWith(".jpg") || iteratorVariable9.EndsWith(".png")) || iteratorVariable9.EndsWith(".jpeg"))
                {
                    WWW iteratorVariable20 = new WWW(iteratorVariable9);
                    yield return iteratorVariable20;
                    if (iteratorVariable20.error == null)
                    {
                        Texture2D iteratorVariable21 = cext.loadimage(iteratorVariable20, mipmap, 0x7a120);
                        iteratorVariable20.Dispose();
                        iteratorVariable21.wrapMode = TextureWrapMode.Clamp;
                        material.SetTexture("_DownTex", iteratorVariable21);
                    }
                    else
                    {
                        PanelInformer.instance.Add("Error load skins " + base.name + ":" + iteratorVariable20.url, PanelInformer.LOG_TYPE.DANGER);
                    }
                }
                Camera.main.GetComponent<Skybox>().material = material;
                skyMaterial = material;
                linkHash[1].Add(key, material);
            }
            else
            {
                Camera.main.GetComponent<Skybox>().material = (Material)linkHash[1][key];
                skyMaterial = (Material)linkHash[1][key];
            }
        }
        if (lvlInfo.mapName.Contains("Forest"))
        {
            string[] iteratorVariable22 = url.Split(new char[] { ',' });
            string[] iteratorVariable23 = url2.Split(new char[] { ',' });
            int startIndex = 0;
            object[] iteratorVariable25 = UnityEngine.Object.FindObjectsOfType(typeof(GameObject));
            foreach (GameObject iteratorVariable26 in iteratorVariable25)
            {
                if (iteratorVariable26 != null)
                {
                    if (iteratorVariable26.name.Contains("TREE") && (n.Length > (startIndex + 1)))
                    {
                        int iteratorVariable28;
                        int iteratorVariable27;
                        string s = n.Substring(startIndex, 1);
                        string iteratorVariable30 = n.Substring(startIndex + 1, 1);
                        if ((((int.TryParse(s, out iteratorVariable28) && int.TryParse(iteratorVariable30, out iteratorVariable27)) && ((iteratorVariable28 >= 0) && (iteratorVariable28 < 8))) && (((iteratorVariable27 >= 0) && (iteratorVariable27 < 8)) && ((iteratorVariable22.Length >= 8) && (iteratorVariable23.Length >= 8)))) && ((iteratorVariable22[iteratorVariable28] != null) && (iteratorVariable23[iteratorVariable27] != null)))
                        {
                            string iteratorVariable31 = iteratorVariable22[iteratorVariable28];
                            string iteratorVariable32 = iteratorVariable23[iteratorVariable27];
                            foreach (Renderer iteratorVariable33 in iteratorVariable26.GetComponentsInChildren<Renderer>())
                            {
                                if (iteratorVariable33.name.Contains(FengGameManagerMKII.s[0x16]))
                                {
                                    if ((iteratorVariable31.EndsWith(".jpg") || iteratorVariable31.EndsWith(".png")) || iteratorVariable31.EndsWith(".jpeg"))
                                    {
                                        if (!linkHash[2].ContainsKey(iteratorVariable31))
                                        {
                                            WWW iteratorVariable34 = new WWW(iteratorVariable31);
                                            yield return iteratorVariable34;
                                            if (iteratorVariable34.error == null)
                                            {
                                                Texture2D iteratorVariable35 = cext.loadimage(iteratorVariable34, mipmap, 0xf4240);
                                                iteratorVariable34.Dispose();
                                                if (!linkHash[2].ContainsKey(iteratorVariable31))
                                                {
                                                    iteratorVariable1 = true;
                                                    iteratorVariable33.material.mainTexture = iteratorVariable35;
                                                    linkHash[2].Add(iteratorVariable31, iteratorVariable33.material);
                                                    iteratorVariable33.material = (Material)linkHash[2][iteratorVariable31];
                                                }
                                                else
                                                {
                                                    iteratorVariable33.material = (Material)linkHash[2][iteratorVariable31];
                                                }
                                            }
                                            else
                                            {
                                                PanelInformer.instance.Add("Error load skins " + base.name + ":" + iteratorVariable34.url, PanelInformer.LOG_TYPE.DANGER);
                                            }
                                        }
                                        else
                                        {
                                            iteratorVariable33.material = (Material)linkHash[2][iteratorVariable31];
                                        }
                                    }
                                }
                                else if (iteratorVariable33.name.Contains(FengGameManagerMKII.s[0x17]))
                                {
                                    if ((iteratorVariable32.EndsWith(".jpg") || iteratorVariable32.EndsWith(".png")) || iteratorVariable32.EndsWith(".jpeg"))
                                    {
                                        if (!linkHash[0].ContainsKey(iteratorVariable32))
                                        {
                                            WWW iteratorVariable36 = new WWW(iteratorVariable32);
                                            yield return iteratorVariable36;
                                            if (iteratorVariable36.error == null)
                                            {
                                                Texture2D iteratorVariable37 = cext.loadimage(iteratorVariable36, mipmap, 0x30d40);
                                                iteratorVariable36.Dispose();
                                                if (!linkHash[0].ContainsKey(iteratorVariable32))
                                                {
                                                    iteratorVariable1 = true;
                                                    iteratorVariable33.material.mainTexture = iteratorVariable37;
                                                    linkHash[0].Add(iteratorVariable32, iteratorVariable33.material);
                                                    iteratorVariable33.material = (Material)linkHash[0][iteratorVariable32];
                                                }
                                                else
                                                {
                                                    iteratorVariable33.material = (Material)linkHash[0][iteratorVariable32];
                                                }
                                            }
                                            else
                                            {
                                                PanelInformer.instance.Add("Error load skins " + base.name + ":" + iteratorVariable36.url, PanelInformer.LOG_TYPE.DANGER);
                                            }
                                        }
                                        else
                                        {
                                            iteratorVariable33.material = (Material)linkHash[0][iteratorVariable32];
                                        }
                                    }
                                    else if (iteratorVariable32.ToLower() == "transparent")
                                    {
                                        iteratorVariable33.enabled = false;
                                    }
                                }
                            }
                        }
                        startIndex += 2;
                    }
                    else if ((iteratorVariable26.name.Contains("Cube_001") && (iteratorVariable26.transform.parent.gameObject.tag != "Player")) && ((iteratorVariable23.Length > 8) && (iteratorVariable23[8] != null)))
                    {
                        string iteratorVariable38 = iteratorVariable23[8];
                        if ((iteratorVariable38.EndsWith(".jpg") || iteratorVariable38.EndsWith(".png")) || iteratorVariable38.EndsWith(".jpeg"))
                        {
                            foreach (Renderer iteratorVariable39 in iteratorVariable26.GetComponentsInChildren<Renderer>())
                            {
                                if (!linkHash[0].ContainsKey(iteratorVariable38))
                                {
                                    WWW iteratorVariable40 = new WWW(iteratorVariable38);
                                    yield return iteratorVariable40;
                                    if (iteratorVariable40.error == null)
                                    {
                                        Texture2D iteratorVariable41 = cext.loadimage(iteratorVariable40, mipmap, 0x30d40);
                                        iteratorVariable40.Dispose();
                                        if (!linkHash[0].ContainsKey(iteratorVariable38))
                                        {
                                            iteratorVariable1 = true;
                                            iteratorVariable39.material.mainTexture = iteratorVariable41;
                                            linkHash[0].Add(iteratorVariable38, iteratorVariable39.material);
                                            iteratorVariable39.material = (Material)linkHash[0][iteratorVariable38];
                                        }
                                        else
                                        {
                                            iteratorVariable39.material = (Material)linkHash[0][iteratorVariable38];
                                        }
                                    }
                                    else
                                    {
                                        PanelInformer.instance.Add("Error load skins " + base.name + ":" + iteratorVariable40.url, PanelInformer.LOG_TYPE.DANGER);
                                    }
                                }
                                else
                                {
                                    iteratorVariable39.material = (Material)linkHash[0][iteratorVariable38];
                                }
                            }
                        }
                        else if (iteratorVariable38.ToLower() == "transparent")
                        {
                            foreach (Renderer renderer in iteratorVariable26.GetComponentsInChildren<Renderer>())
                            {
                                renderer.enabled = false;
                            }
                        }
                    }
                }
            }
        }
        else if (lvlInfo.mapName.Contains("City"))
        {
            string[] iteratorVariable42 = url.Split(new char[] { ',' });
            string[] iteratorVariable43 = url2.Split(new char[] { ',' });
            string text1 = iteratorVariable43[2];
            int iteratorVariable44 = 0;
            object[] iteratorVariable45 = UnityEngine.Object.FindObjectsOfType(typeof(GameObject));
            foreach (GameObject iteratorVariable46 in iteratorVariable45)
            {
                if (((iteratorVariable46 != null) && iteratorVariable46.name.Contains("Cube_")) && (iteratorVariable46.transform.parent.gameObject.tag != "Player"))
                {
                    if (iteratorVariable46.name.EndsWith("001"))
                    {
                        if ((iteratorVariable43.Length > 0) && (iteratorVariable43[0] != null))
                        {
                            string iteratorVariable47 = iteratorVariable43[0];
                            if ((iteratorVariable47.EndsWith(".jpg") || iteratorVariable47.EndsWith(".png")) || iteratorVariable47.EndsWith(".jpeg"))
                            {
                                foreach (Renderer iteratorVariable48 in iteratorVariable46.GetComponentsInChildren<Renderer>())
                                {
                                    if (!linkHash[0].ContainsKey(iteratorVariable47))
                                    {
                                        WWW iteratorVariable49 = new WWW(iteratorVariable47);
                                        yield return iteratorVariable49;
                                        if (iteratorVariable49.error == null)
                                        {
                                            Texture2D iteratorVariable50 = cext.loadimage(iteratorVariable49, mipmap, 0x30d40);
                                            iteratorVariable49.Dispose();
                                            if (!linkHash[0].ContainsKey(iteratorVariable47))
                                            {
                                                iteratorVariable1 = true;
                                                iteratorVariable48.material.mainTexture = iteratorVariable50;
                                                linkHash[0].Add(iteratorVariable47, iteratorVariable48.material);
                                                iteratorVariable48.material = (Material)linkHash[0][iteratorVariable47];
                                            }
                                            else
                                            {
                                                iteratorVariable48.material = (Material)linkHash[0][iteratorVariable47];
                                            }
                                        }
                                        else
                                        {
                                            PanelInformer.instance.Add("Error load skins " + base.name + ":" + iteratorVariable49.url, PanelInformer.LOG_TYPE.DANGER);
                                        }
                                    }
                                    else
                                    {
                                        iteratorVariable48.material = (Material)linkHash[0][iteratorVariable47];
                                    }
                                }
                            }
                            else if (iteratorVariable47.ToLower() == "transparent")
                            {
                                foreach (Renderer renderer2 in iteratorVariable46.GetComponentsInChildren<Renderer>())
                                {
                                    renderer2.enabled = false;
                                }
                            }
                        }
                    }
                    else if (((iteratorVariable46.name.EndsWith("006") || iteratorVariable46.name.EndsWith("007")) || (iteratorVariable46.name.EndsWith("015") || iteratorVariable46.name.EndsWith("000"))) || ((iteratorVariable46.name.EndsWith("002") && (iteratorVariable46.transform.position.x == 0f)) && ((iteratorVariable46.transform.position.y == 0f) && (iteratorVariable46.transform.position.z == 0f))))
                    {
                        if ((iteratorVariable43.Length > 0) && (iteratorVariable43[1] != null))
                        {
                            string iteratorVariable51 = iteratorVariable43[1];
                            if ((iteratorVariable51.EndsWith(".jpg") || iteratorVariable51.EndsWith(".png")) || iteratorVariable51.EndsWith(".jpeg"))
                            {
                                foreach (Renderer iteratorVariable52 in iteratorVariable46.GetComponentsInChildren<Renderer>())
                                {
                                    if (!linkHash[0].ContainsKey(iteratorVariable51))
                                    {
                                        WWW iteratorVariable53 = new WWW(iteratorVariable51);
                                        yield return iteratorVariable53;
                                        if (iteratorVariable53.error == null)
                                        {
                                            Texture2D iteratorVariable54 = cext.loadimage(iteratorVariable53, mipmap, 0x30d40);
                                            iteratorVariable53.Dispose();
                                            if (!linkHash[0].ContainsKey(iteratorVariable51))
                                            {
                                                iteratorVariable1 = true;
                                                iteratorVariable52.material.mainTexture = iteratorVariable54;
                                                linkHash[0].Add(iteratorVariable51, iteratorVariable52.material);
                                                iteratorVariable52.material = (Material)linkHash[0][iteratorVariable51];
                                            }
                                            else
                                            {
                                                iteratorVariable52.material = (Material)linkHash[0][iteratorVariable51];
                                            }
                                        }
                                        else
                                        {
                                            PanelInformer.instance.Add("Error load skins " + base.name + ":" + iteratorVariable53.url, PanelInformer.LOG_TYPE.DANGER);
                                        }
                                    }
                                    else
                                    {
                                        iteratorVariable52.material = (Material)linkHash[0][iteratorVariable51];
                                    }
                                }
                            }
                        }
                    }
                    else if ((iteratorVariable46.name.EndsWith("005") || iteratorVariable46.name.EndsWith("003")) || ((iteratorVariable46.name.EndsWith("002") && (((iteratorVariable46.transform.position.x != 0f) || (iteratorVariable46.transform.position.y != 0f)) || (iteratorVariable46.transform.position.z != 0f))) && (n.Length > iteratorVariable44)))
                    {
                        int iteratorVariable55;
                        string iteratorVariable56 = n.Substring(iteratorVariable44, 1);
                        if (((int.TryParse(iteratorVariable56, out iteratorVariable55) && (iteratorVariable55 >= 0)) && ((iteratorVariable55 < 8) && (iteratorVariable42.Length >= 8))) && (iteratorVariable42[iteratorVariable55] != null))
                        {
                            string iteratorVariable57 = iteratorVariable42[iteratorVariable55];
                            if ((iteratorVariable57.EndsWith(".jpg") || iteratorVariable57.EndsWith(".png")) || iteratorVariable57.EndsWith(".jpeg"))
                            {
                                foreach (Renderer iteratorVariable58 in iteratorVariable46.GetComponentsInChildren<Renderer>())
                                {
                                    if (!linkHash[2].ContainsKey(iteratorVariable57))
                                    {
                                        WWW iteratorVariable59 = new WWW(iteratorVariable57);
                                        yield return iteratorVariable59;
                                        if (iteratorVariable59.error == null)
                                        {
                                            Texture2D iteratorVariable60 = cext.loadimage(iteratorVariable59, mipmap, 0xf4240);
                                            iteratorVariable59.Dispose();
                                            if (!linkHash[2].ContainsKey(iteratorVariable57))
                                            {
                                                iteratorVariable1 = true;
                                                iteratorVariable58.material.mainTexture = iteratorVariable60;
                                                linkHash[2].Add(iteratorVariable57, iteratorVariable58.material);
                                                iteratorVariable58.material = (Material)linkHash[2][iteratorVariable57];
                                            }
                                            else
                                            {
                                                iteratorVariable58.material = (Material)linkHash[2][iteratorVariable57];
                                            }
                                        }
                                        else
                                        {
                                            PanelInformer.instance.Add("Error load skins " + base.name + ":" + iteratorVariable59.url, PanelInformer.LOG_TYPE.DANGER);
                                        }
                                    }
                                    else
                                    {
                                        iteratorVariable58.material = (Material)linkHash[2][iteratorVariable57];
                                    }
                                }
                            }
                        }
                        iteratorVariable44++;
                    }
                    else if ((iteratorVariable46.name.EndsWith("019") || iteratorVariable46.name.EndsWith("020")) && ((iteratorVariable43.Length > 2) && (iteratorVariable43[2] != null)))
                    {
                        string iteratorVariable61 = iteratorVariable43[2];
                        if ((iteratorVariable61.EndsWith(".jpg") || iteratorVariable61.EndsWith(".png")) || iteratorVariable61.EndsWith(".jpeg"))
                        {
                            foreach (Renderer iteratorVariable62 in iteratorVariable46.GetComponentsInChildren<Renderer>())
                            {
                                if (!linkHash[2].ContainsKey(iteratorVariable61))
                                {
                                    WWW iteratorVariable63 = new WWW(iteratorVariable61);
                                    yield return iteratorVariable63;
                                    if (iteratorVariable63.error == null)
                                    {
                                        Texture2D iteratorVariable64 = cext.loadimage(iteratorVariable63, mipmap, 0xf4240);
                                        iteratorVariable63.Dispose();
                                        if (!linkHash[2].ContainsKey(iteratorVariable61))
                                        {
                                            iteratorVariable1 = true;
                                            iteratorVariable62.material.mainTexture = iteratorVariable64;
                                            linkHash[2].Add(iteratorVariable61, iteratorVariable62.material);
                                            iteratorVariable62.material = (Material)linkHash[2][iteratorVariable61];
                                        }
                                        else
                                        {
                                            iteratorVariable62.material = (Material)linkHash[2][iteratorVariable61];
                                        }
                                    }
                                    else
                                    {
                                        PanelInformer.instance.Add("Error load skins " + base.name + ":" + iteratorVariable63.url, PanelInformer.LOG_TYPE.DANGER);
                                    }
                                }
                                else
                                {
                                    iteratorVariable62.material = (Material)linkHash[2][iteratorVariable61];
                                }
                            }
                        }
                    }
                }
            }
        }
        Minimap.TryRecaptureInstance();
        if (iteratorVariable1)
        {
            this.unloadAssets();
        }
        yield break;
    }

    [RPC]
    private void loadskinRPC(string n, string url, string url2, string[] skybox, PhotonMessageInfo info)
    {
        if ((((int)settings[2]) == 1) && info.sender.isMasterClient)
        {
            base.StartCoroutine(this.loadskinE(n, url, url2, skybox));
        }
    }

    public IEnumerator loginFeng()
    {
        WWW iteratorVariable0;
        WWWForm form = new WWWForm();
        form.AddField("userid", usernameField);
        form.AddField("password", passwordField);
        if (Application.isWebPlayer)
        {
            iteratorVariable0 = new WWW("http://aotskins.com/version/getinfo.php", form);
        }
        else
        {
            iteratorVariable0 = new WWW("http://fenglee.com/game/aog/require_user_info.php", form);
        }
        yield return iteratorVariable0;
        if ((iteratorVariable0.error == null) && !iteratorVariable0.text.Contains("Error,please sign in again."))
        {
            char[] separator = new char[] { '|' };
            string[] strArray = iteratorVariable0.text.Split(separator);
            LoginFengKAI.player.name = usernameField;
            LoginFengKAI.player.guildname = strArray[0];
            loginstate = 3;
        }
        else
        {
            loginstate = 2;
        }
        yield break;
    }

    private string mastertexturetype(int lol)
    {
        if (lol == 0)
        {
            return "High";
        }
        if (lol == 1)
        {
            return "Med";
        }
        return "Low";
    }

    public void multiplayerRacingFinsih()
    {
        float time = this.roundTime - 20f;
        if (PhotonNetwork.isMasterClient)
        {
            this.getRacingResult(LoginFengKAI.player.name, time);
        }
        else
        {
            object[] parameters = new object[] { LoginFengKAI.player.name, time };
            base.photonView.RPC("getRacingResult", PhotonTargets.MasterClient, parameters);
        }
        this.gameWin2();
    }

    [RPC]
    private void netGameLose(int score, PhotonMessageInfo info)
    {
        this.isLosing = true;
        this.titanScore = score;
        this.gameEndCD = this.gameEndTotalCDtime;
        if (((int)settings[0xf4]) == 1)
        {
            InRoomChat.instance.addLINE("(" + this.roundTime.ToString("F2") + ")Round ended (game lose).");
        }
        if (((info.sender != PhotonNetwork.masterClient) && !info.sender.isLocal) && PhotonNetwork.isMasterClient)
        {
            InRoomChat.instance.addLINE("Round end sent from Player " + info.sender.ID.ToString());
        }
    }

    [RPC]
    private void netGameWin(int score, PhotonMessageInfo info)
    {
        this.humanScore = score;
        this.isWinning = true;
        if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_AHSS)
        {
            this.teamWinner = score;
            this.teamScores[this.teamWinner - 1]++;
            this.gameEndCD = this.gameEndTotalCDtime;
        }
        else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.RACING)
        {
            if (RCSettings.racingStatic == 1)
            {
                this.gameEndCD = 1000f;
            }
            else
            {
                this.gameEndCD = 20f;
            }
        }
        else
        {
            this.gameEndCD = this.gameEndTotalCDtime;
        }
        if (((int)settings[0xf4]) == 1)
        {
            InRoomChat.instance.addLINE("(" + this.roundTime.ToString("F2") + ") Round ended (game win).");
        }
        if ((info.sender != PhotonNetwork.masterClient) && !info.sender.isLocal)
        {
            InRoomChat.instance.addLINE("Round end sent from Player " + info.sender.ID.ToString());
        }
    }

    [RPC]
    private void netRefreshRacingResult(string tmp)
    {
        this.localRacingResult = tmp;
    }
  
    [RPC]
    public void netShowDamage(int speed)
    {
        if ((int)FengGameManagerMKII.settings[336] == 0)
        {
            StylishComponent stylec = StylishComponent.instance;
            if (stylec != null)
            {
                stylec.Style(speed);
            }
        }
        if (LabelScore == null)
        {
            LabelScore = CyanMod.CachingsGM.Find("LabelScore");
            LabelScoreUI = LabelScore.GetComponent<UILabel>();
        }
        if (LabelScore != null)
        {
            LabelScoreUI.text = speed.ToString();
            LabelScore.transform.localScale = Vector3.zero;
            speed = (int)(speed * 0.1f);
            speed = Mathf.Max(40, speed);
            speed = Mathf.Min(150, speed);
            iTween.Stop(LabelScore);
            object[] args = new object[] { "x", speed, "y", speed, "z", speed, "easetype", iTween.EaseType.easeOutElastic, "time", 1f };
            iTween.ScaleTo(LabelScore, iTween.Hash(args));
            object[] objArray2 = new object[] { "x", 0, "y", 0, "z", 0, "easetype", iTween.EaseType.easeInBounce, "time", 0.5f, "delay", 2f };
            iTween.ScaleTo(LabelScore, iTween.Hash(objArray2));
        }
    }

    public void NOTSpawnNonAITitan(string id)
    {
        this.myLastHero = id.ToUpper();
        PhotonNetwork.player.dead = true;
        PhotonNetwork.player.isTitan = 2;

        if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.TPS || IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.oldTPS)
        {
            Screen.lockCursor = true;
        }
        else
        {
            Screen.lockCursor = false;
        }
        Screen.showCursor = true;
        this.ShowHUDInfoCenter(INC.la("has_started60sec"));
        IN_GAME_MAIN_CAMERA.instance.enabled = true;
        IN_GAME_MAIN_CAMERA.instance.setMainObject(null, true, false);
        IN_GAME_MAIN_CAMERA.instance.setSpectorMode(true);
        IN_GAME_MAIN_CAMERA.instance.gameOver = true;
    }

    public void NOTSpawnNonAITitanRC(string id)
    {
        this.myLastHero = id.ToUpper();
        PhotonNetwork.player.dead = true;
        PhotonNetwork.player.isTitan = 2;
        if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.TPS || IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.oldTPS)
        {
            Screen.lockCursor = true;
        }
        else
        {
            Screen.lockCursor = false;
        }
        Screen.showCursor = true;
        this.ShowHUDInfoCenter("Syncing spawn locations...");
        IN_GAME_MAIN_CAMERA.instance.enabled = true;
        IN_GAME_MAIN_CAMERA.instance.setMainObject(null, true, false);
        IN_GAME_MAIN_CAMERA.instance.setSpectorMode(true);
        IN_GAME_MAIN_CAMERA.instance.gameOver = true;
    }

    public void NOTSpawnPlayer(string id)
    {
        this.myLastHero = id.ToUpper();
        PhotonNetwork.player.dead = true;
        PhotonNetwork.player.isTitan = 1;
        if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.TPS || IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.oldTPS)
        {
            Screen.lockCursor = true;
        }
        else
        {
            Screen.lockCursor = false;
        }
        Screen.showCursor = false;
        this.ShowHUDInfoCenter(INC.la("has_started60sec"));
        IN_GAME_MAIN_CAMERA.instance.enabled = true;
        IN_GAME_MAIN_CAMERA.instance.setMainObject(null, true, false);
        IN_GAME_MAIN_CAMERA.instance.setSpectorMode(true);
        IN_GAME_MAIN_CAMERA.instance.gameOver = true;
    }

    public void NOTSpawnPlayerRC(string id)
    {
        this.myLastHero = id.ToUpper();
        PhotonNetwork.player.dead = true;
        PhotonNetwork.player.isTitan = 1;
        if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.TPS || IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.oldTPS)
        {
            Screen.lockCursor = true;
        }
        else
        {
            Screen.lockCursor = false;
        }
        Screen.showCursor = false;
        IN_GAME_MAIN_CAMERA.instance.enabled = true;
        IN_GAME_MAIN_CAMERA.instance.setMainObject(null, true, false);
        IN_GAME_MAIN_CAMERA.instance.setSpectorMode(true);
        IN_GAME_MAIN_CAMERA.instance.gameOver = true;
    }

    public void OnConnectedToMaster()
    {
        UnityEngine.Debug.Log("OnConnectedToMaster");
    }

    public void OnConnectedToPhoton()
    {
        UnityEngine.Debug.Log("OnConnectedToPhoton");
    }

    public void OnConnectionFail(DisconnectCause cause)
    {
        UnityEngine.Debug.Log("OnConnectionFail : " + cause.ToString());
        Screen.lockCursor = false;
        Screen.showCursor = true;
        IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.STOP;
        this.gameStart = false;
        NGUITools.SetActive(uiT.panels[0], false);
        NGUITools.SetActive(uiT.panels[1], false);
        NGUITools.SetActive(uiT.panels[2], false);
        NGUITools.SetActive(uiT.panels[3], false);
        NGUITools.SetActive(uiT.panels[4], true);
        CyanMod.CachingsGM.Find("LabelDisconnectInfo").GetComponent<UILabel>().text = "OnConnectionFail : " + cause.ToString();
    }

    public void OnCreatedRoom()
    {
        this.kicklist = new ArrayList();
        this.racingResult = new ArrayList();
        this.teamScores = new int[2];
        UnityEngine.Debug.Log("OnCreatedRoom");
    }

    public void OnCustomAuthenticationFailed()
    {
        UnityEngine.Debug.Log("OnCustomAuthenticationFailed");
    }

    public void OnDisconnectedFromPhoton()
    {
        UnityEngine.Debug.Log("OnDisconnectedFromPhoton");
        Screen.lockCursor = false;
        Screen.showCursor = true;
    }
    bool find_FTlive()
    {
        int i = 0;
        foreach (FEMALE_TITAN ft in fT)
        {
            if (!ft.hasDie)
            {
                i++;
            }
        }
        return i == 0;
    }
   
    public void isCheckingsFT()
    {
        if (isUpdateFT && find_FTlive())
        {
            isUpdateFT = false;
            wave = wave + 1;
            foreach (PhotonPlayer player in PhotonNetwork.playerList)
            {
                if ((player.isTitan) != 2)
                {
                    base.photonView.RPC("respawnHeroInNewRound", player, new object[0]);
                }
            }
            if (RCSettings.maxWave > 0 ? wave > RCSettings.maxWave : wave > 20)
            {
                this.gameWin2();
                return;
            }
            else
            {
                FamelTitanRandomSpawn(this.wave + 1);
                cext.mess(INC.la("map_wave") + wave + "/" + ((RCSettings.maxWave != 0) ? RCSettings.maxWave.ToString() : "20"));
            }
        }
    }
    [RPC]
    public void oneTitanDown(string name1, bool onPlayerLeave)
    {
        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE) || (PhotonNetwork.isMasterClient && (RCSettings.AnnieSurvive == 0)))
        {
            if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE)
            {
                if (name1 != string.Empty)
                {
                    if (name1 == "Titan")
                    {
                        this.PVPhumanScore++;
                    }
                    else if (name1 == "Aberrant")
                    {
                        this.PVPhumanScore += 2;
                    }
                    else if (name1 == "Jumper")
                    {
                        this.PVPhumanScore += 3;
                    }
                    else if (name1 == "Crawler")
                    {
                        this.PVPhumanScore += 4;
                    }
                    else if (name1 == "Female Titan")
                    {
                        this.PVPhumanScore += 10;
                    }
                    else
                    {
                        this.PVPhumanScore += 3;
                    }
                }
                this.checkPVPpts();
                object[] parameters = new object[] { this.PVPhumanScore, this.PVPtitanScore };
                base.photonView.RPC("refreshPVPStatus", PhotonTargets.Others, parameters);
            }
            else if (IN_GAME_MAIN_CAMERA.gamemode != GAMEMODE.CAGE_FIGHT)
            {
                if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.KILL_TITAN)
                {
                    if (this.checkIsTitanAllDie())
                    {

                        this.gameWin2();
                        IN_GAME_MAIN_CAMERA.instance.gameOver = true;

                    }
                }
                else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)
                {
                    if (this.checkIsTitanAllDie())
                    {

                        this.wave++;
                        if (!(((lvlInfo.respawnMode == RespawnMode.NEWROUND) || (level.StartsWith("Custom") && (RCSettings.gameType == 1))) ? (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.MULTIPLAYER) : true))
                        {
                            foreach (PhotonPlayer player in PhotonNetwork.playerList)
                            {
                                if ((player.isTitan) != 2)
                                {
                                    base.photonView.RPC("respawnHeroInNewRound", player, new object[0]);
                                }
                            }
                        }
                        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                        {
                            cext.mess(INC.la("map_wave") + this.wave + "/" + ((RCSettings.maxWave != 0) ? RCSettings.maxWave.ToString() : "20"));
                        }
                        if (this.wave > this.highestwave)
                        {
                            this.highestwave = this.wave;
                        }
                        if (PhotonNetwork.isMasterClient)
                        {
                            this.RequireStatus();
                        }
                        if (!(((RCSettings.maxWave != 0) || (this.wave <= 20)) ? ((RCSettings.maxWave <= 0) || (this.wave <= RCSettings.maxWave)) : false))
                        {
                            this.gameWin2();
                        }
                        else
                        {
                            int abnormal = 90;
                            if (IN_GAME_MAIN_CAMERA.difficulty == DIFFICULTY.HARD)
                            {
                                abnormal = 70;
                            }
                            if (!lvlInfo.punk)
                            {
                                this.spawnTitanCustom("titanRespawn", abnormal, this.wave + 2, false);
                            }
                            else if (this.wave == 5)
                            {
                                this.spawnTitanCustom("titanRespawn", abnormal, 1, true);
                            }
                            else if (this.wave == 10)
                            {
                                this.spawnTitanCustom("titanRespawn", abnormal, 2, true);
                            }
                            else if (this.wave == 15)
                            {
                                this.spawnTitanCustom("titanRespawn", abnormal, 3, true);
                            }
                            else if (this.wave == 20)
                            {
                                this.spawnTitanCustom("titanRespawn", abnormal, 4, true);
                            }
                            else
                            {
                                this.spawnTitanCustom("titanRespawn", abnormal, this.wave + 2, false);
                            }
                        }

                    }
                }
                else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.ENDLESS_TITAN)
                {

                    if (!onPlayerLeave)
                    {
                        this.humanScore++;
                        int num2 = 90;
                        if (IN_GAME_MAIN_CAMERA.difficulty == DIFFICULTY.HARD)
                        {
                            num2 = 70;
                        }
                        this.spawnTitanCustom("titanRespawn", num2, 1, false);
                    }

                }
                else
                {
                    int enemyNumber = lvlInfo.enemyNumber;
                }
            }
        }
    }

    public void OnFailedToConnectToPhoton()
    {
        UnityEngine.Debug.Log("OnFailedToConnectToPhoton");
    }

    public string fps
    {
        get
        {
            return showFPS ? " FPS:" + Mathf.Round((float)fpsfps).ToString() : string.Empty;
        }
    }
    public void PauseLabel(string text)
    {
        if (LabelInfoBottomRight != null )
        {
            LabelInfoBottomRight.text = text;
        }
    }
   
    public TextMesh to_text_mesh(GameObject gm, int font_size, TextAnchor anchor, TextAlignment aligment)
    {
        if (gm != null)
        {
            TextMesh tm = gm.GetComponent<TextMesh>();
            if (tm == null)
            {
                tm = gm.AddComponent<TextMesh>();
            }
            MeshRenderer mr = gm.GetComponent<MeshRenderer>();
            if (mr == null)
            {
                mr = gm.AddComponent<MeshRenderer>();
            }
            mr.material = fotHUD.material;
            gm.transform.localScale = new Vector3(4.9f, 4.9f);
            tm.font = fotHUD;
            tm.fontSize = font_size;
            tm.anchor = anchor;
            tm.alignment = aligment;
            tm.color = INC.color_UI;
            UILabel ui = gm.GetComponent<UILabel>();
            if (ui != null)
            {
                ui.enabled = false;
            }
            return tm;
        }
        return null;
    }
  
    public void SearhLabel()
    {
        if (INC.isLogined)
        {
            if (fotHUD == null)
            {
                fotHUD = (Font)Statics.CMassets.Load("tahoma");
            }
            LabelInfoBottomRight = to_text_mesh(CyanMod.CachingsGM.Find("LabelInfoBottomRight"), 32, TextAnchor.LowerRight,TextAlignment.Right);
            LabelInfoCenter = to_text_mesh(CyanMod.CachingsGM.Find("LabelInfoCenter"), 32, TextAnchor.MiddleCenter,  TextAlignment.Center);
            LabelInfoTopCenter = to_text_mesh(CyanMod.CachingsGM.Find("LabelInfoTopCenter"), 32, TextAnchor.UpperCenter, TextAlignment.Center);
            LabelInfoTopLeft = to_text_mesh(CyanMod.CachingsGM.Find("LabelInfoTopLeft"), 28, TextAnchor.UpperLeft, TextAlignment.Left);
            LabelInfoTopRight = to_text_mesh(CyanMod.CachingsGM.Find("LabelInfoTopRight"), 28, TextAnchor.UpperRight, TextAlignment.Right);
            LabelNetworkStatus = to_text_mesh(CyanMod.CachingsGM.Find("LabelNetworkStatus"), 32, TextAnchor.UpperLeft, TextAlignment.Left);
            if ( LabelTopRight)
            {
                text_pause = INC.la("pause_button") + (string)FengGameManagerMKII.settings[436] + " ";
                PauseLabel(text_pause);
            }
            else
            {
                PauseLabel("");
            }
        }
    }
    public void ApplySettings()
    {
        if (INC.isLoadedCofig)
        {
            QualitySettings.SetQualityLevel(cuality_settings);
            if ((int)FengGameManagerMKII.settings[336] == 1)
            {
                if (StylishComponent.instance != null)
                {
                    Destroy(StylishComponent.instance.gameObject);
                }
            }
            IN_GAME_MAIN_CAMERA.invertY = (int)FengGameManagerMKII.settings[322];
            IN_GAME_MAIN_CAMERA.cameraTilt = (int)FengGameManagerMKII.settings[321];
            IN_GAME_MAIN_CAMERA.cameraDistance = 0.3f + distanceSlider;
            Camera.main.GetComponent<TiltShift>().enabled = ((int)FengGameManagerMKII.settings[400]) == 1;
            bool isSingle = IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE;
            bool ismulty = IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER;
            QualitySettings.blendWeights = (((int)FengGameManagerMKII.settings[403]) == 0 ? BlendWeights.OneBone : ((int)FengGameManagerMKII.settings[403]) == 1 ? BlendWeights.TwoBones : BlendWeights.FourBones);
            QualitySettings.anisotropicFiltering = (((int)FengGameManagerMKII.settings[402]) == 0 ? AnisotropicFiltering.Disable : ((int)FengGameManagerMKII.settings[402]) == 1 ? AnisotropicFiltering.Enable : AnisotropicFiltering.ForceEnable);
            QualitySettings.shadowProjection = (int)FengGameManagerMKII.settings[401] == 1 ? ShadowProjection.CloseFit : ShadowProjection.StableFit;
           
            showFPS = (int)FengGameManagerMKII.settings[280] == 1;
            if (isSingle || ismulty)
            {
                InRoomChat.IsVisible = (int)FengGameManagerMKII.settings[292] == 0;
                bool flag_1 = (int)FengGameManagerMKII.settings[281] == 0;
                bool aim = ((int)FengGameManagerMKII.settings[289] == 0);

                bool flag_7 = ((int)FengGameManagerMKII.settings[285] == 0);
                LabelCenter = (int)FengGameManagerMKII.settings[293] == 0;
                LabelTopCenter = (int)FengGameManagerMKII.settings[294] == 0;
                LabelTopLeft = (int)FengGameManagerMKII.settings[295] == 0;
                LabelTopRight = (int)FengGameManagerMKII.settings[303] == 0;
                foreach (HERO hero in heroes)
                {
                    if (isSingle || hero.photonView.isMine)
                    {
                        hero.gastrail = (int)FengGameManagerMKII.settings[282] == 0;
                        if (ismulty && (int)FengGameManagerMKII.settings[328] == 1 && hero.myNetWorkName != null)
                        {
                            hero.name_remove();
                        }
                        hero.aim = aim;
                        if ((int)FengGameManagerMKII.settings[290] == 1)
                        {
                            hero.showSprites(false);
                        }
                        else
                        {
                            hero.showSprites(true);
                            hero.ColorSprite();
                        }
                        if (!aim && hero.crossL1 != null)
                        {

                            Destroy(hero.crossL1);
                            Destroy(hero.crossL2);
                            Destroy(hero.crossR1);
                            Destroy(hero.crossR2);
                        }
                    }
                    else
                    {
                        if ((int)FengGameManagerMKII.settings[291] == 1)
                        {
                            hero.name_remove();
                        }

                    }
                }
            }

            SearhLabel();
        }
    }
   
    bool checkingFile(string path, string filesize)
    {
        FileInfo file1 = new FileInfo(path);
        if (file1.Exists)
        {
            if (filesize != file1.Length.ToString())
            {
                return true;
            }
            return false;
        }
        return true;
    }
  
    public void OnGUI()
    {
        if (isPlayng)
        {
            GUI.DrawTexture(new Rect(((Screen.width / 2) - (sizeImage / 2)), ((Screen.height / 2) - (sizeImage / 2)), nya_texture.width - sizeImage, nya_texture.height - sizeImage), nya_texture);
        }
        if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.STOP)
        {
            if ((int)FengGameManagerMKII.settings[64] >= 100)
            {
                float num11 = (float)Screen.width - 300f;
                GUI.backgroundColor = INC.gui_color;
                bool flag = false;
                bool flag2 = false;
                GUI.Box(new Rect(5f, 5f, 295f, 590f), string.Empty);
                GUI.Box(new Rect(num11, 5f, 295f, 590f), string.Empty);
                if (GUI.Button(new Rect(10f, 10f, 60f, 25f), "Script", "box"))
                {
                    FengGameManagerMKII.settings[68] = 100;
                }
                if (GUI.Button(new Rect(75f, 10f, 65f, 25f), "Controls", "box"))
                {
                    FengGameManagerMKII.settings[68] = 101;
                }
                if (GUI.Button(new Rect(210f, 10f, 80f, 25f), "Full Screen", "box"))
                {
                    Screen.fullScreen = !Screen.fullScreen;
                    if (Screen.fullScreen)
                    {
                        Screen.SetResolution(960, 600, false);
                    }
                    else
                    {
                        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
                    }
                }
                if ((int)FengGameManagerMKII.settings[68] == 100 || (int)FengGameManagerMKII.settings[68] == 102)
                {
                    GUI.Label(new Rect(115f, 40f, 100f, 20f), "Level Script:", "Label");
                    GUI.Label(new Rect(115f, 115f, 100f, 20f), "Import Data", "Label");
                    GUI.Label(new Rect(12f, 535f, 280f, 60f), "Warning: your current level will be lost if you quit or import data. Make sure to save the level to a text document.", "Label");
                    FengGameManagerMKII.settings[77] = GUI.TextField(new Rect(10f, 140f, 285f, 350f), (string)FengGameManagerMKII.settings[77]);
                    if (GUI.Button(new Rect(35f, 500f, 60f, 30f), "Apply"))
                    {
                        UnityEngine.Object[] array = UnityEngine.Object.FindObjectsOfType(typeof(GameObject));
                        for (int i = 0; i < array.Length; i++)
                        {
                            GameObject gameObject = (GameObject)array[i];
                            if (gameObject.name.StartsWith("custom") || gameObject.name.StartsWith("base") || gameObject.name.StartsWith("photon") || gameObject.name.StartsWith("spawnpoint") || gameObject.name.StartsWith("misc") || gameObject.name.StartsWith("racing"))
                            {
                                UnityEngine.Object.Destroy(gameObject);
                            }
                        }
                        FengGameManagerMKII.linkHash[3].Clear();
                        FengGameManagerMKII.settings[186] = 0;
                        string[] array2 = Regex.Replace((string)FengGameManagerMKII.settings[77], "\\s+", "").Replace("\r\n", "").Replace("\n", "").Replace("\r", "").Split(new char[]
					{
						';'
					});
                        for (int j = 0; j < array2.Length; j++)
                        {
                            string[] array3 = array2[j].Split(new char[]
						{
							','
						});
                            if (array3[0].StartsWith("custom") || array3[0].StartsWith("base") || array3[0].StartsWith("photon") || array3[0].StartsWith("spawnpoint") || array3[0].StartsWith("misc") || array3[0].StartsWith("racing"))
                            {
                                GameObject gameObject2 = null;
                                if (array3[0].StartsWith("custom"))
                                {
                                    gameObject2 = (GameObject)UnityEngine.Object.Instantiate((GameObject)FengGameManagerMKII.RCassets.Load(array3[1]), new Vector3(Convert.ToSingle(array3[12]), Convert.ToSingle(array3[13]), Convert.ToSingle(array3[14])), new Quaternion(Convert.ToSingle(array3[15]), Convert.ToSingle(array3[16]), Convert.ToSingle(array3[17]), Convert.ToSingle(array3[18])));
                                }
                                else if (array3[0].StartsWith("photon"))
                                {
                                    if (array3[1].StartsWith("Cannon"))
                                    {
                                        if (array3.Length < 15)
                                        {
                                            gameObject2 = (GameObject)UnityEngine.Object.Instantiate((GameObject)FengGameManagerMKII.RCassets.Load(array3[1] + "Prop"), new Vector3(Convert.ToSingle(array3[2]), Convert.ToSingle(array3[3]), Convert.ToSingle(array3[4])), new Quaternion(Convert.ToSingle(array3[5]), Convert.ToSingle(array3[6]), Convert.ToSingle(array3[7]), Convert.ToSingle(array3[8])));
                                        }
                                        else
                                        {
                                            gameObject2 = (GameObject)UnityEngine.Object.Instantiate((GameObject)FengGameManagerMKII.RCassets.Load(array3[1] + "Prop"), new Vector3(Convert.ToSingle(array3[12]), Convert.ToSingle(array3[13]), Convert.ToSingle(array3[14])), new Quaternion(Convert.ToSingle(array3[15]), Convert.ToSingle(array3[16]), Convert.ToSingle(array3[17]), Convert.ToSingle(array3[18])));
                                        }
                                    }
                                    else
                                    {
                                        gameObject2 = (GameObject)UnityEngine.Object.Instantiate((GameObject)FengGameManagerMKII.RCassets.Load(array3[1]), new Vector3(Convert.ToSingle(array3[4]), Convert.ToSingle(array3[5]), Convert.ToSingle(array3[6])), new Quaternion(Convert.ToSingle(array3[7]), Convert.ToSingle(array3[8]), Convert.ToSingle(array3[9]), Convert.ToSingle(array3[10])));
                                    }
                                }
                                else if (array3[0].StartsWith("spawnpoint"))
                                {
                                    gameObject2 = (GameObject)UnityEngine.Object.Instantiate((GameObject)FengGameManagerMKII.RCassets.Load(array3[1]), new Vector3(Convert.ToSingle(array3[2]), Convert.ToSingle(array3[3]), Convert.ToSingle(array3[4])), new Quaternion(Convert.ToSingle(array3[5]), Convert.ToSingle(array3[6]), Convert.ToSingle(array3[7]), Convert.ToSingle(array3[8])));
                                }
                                else if (array3[0].StartsWith("base"))
                                {
                                    if (array3.Length < 15)
                                    {
                                        gameObject2 = (GameObject)UnityEngine.Object.Instantiate((GameObject)Resources.Load(array3[1]), new Vector3(Convert.ToSingle(array3[2]), Convert.ToSingle(array3[3]), Convert.ToSingle(array3[4])), new Quaternion(Convert.ToSingle(array3[5]), Convert.ToSingle(array3[6]), Convert.ToSingle(array3[7]), Convert.ToSingle(array3[8])));
                                    }
                                    else
                                    {
                                        gameObject2 = (GameObject)UnityEngine.Object.Instantiate((GameObject)Resources.Load(array3[1]), new Vector3(Convert.ToSingle(array3[12]), Convert.ToSingle(array3[13]), Convert.ToSingle(array3[14])), new Quaternion(Convert.ToSingle(array3[15]), Convert.ToSingle(array3[16]), Convert.ToSingle(array3[17]), Convert.ToSingle(array3[18])));
                                    }
                                }
                                else if (array3[0].StartsWith("misc"))
                                {
                                    if (array3[1].StartsWith("barrier"))
                                    {
                                        gameObject2 = (GameObject)UnityEngine.Object.Instantiate((GameObject)FengGameManagerMKII.RCassets.Load("barrierEditor"), new Vector3(Convert.ToSingle(array3[5]), Convert.ToSingle(array3[6]), Convert.ToSingle(array3[7])), new Quaternion(Convert.ToSingle(array3[8]), Convert.ToSingle(array3[9]), Convert.ToSingle(array3[10]), Convert.ToSingle(array3[11])));
                                    }
                                    else if (array3[1].StartsWith("region"))
                                    {
                                        gameObject2 = (GameObject)UnityEngine.Object.Instantiate((GameObject)FengGameManagerMKII.RCassets.Load("regionEditor"));
                                        gameObject2.transform.position = new Vector3(Convert.ToSingle(array3[6]), Convert.ToSingle(array3[7]), Convert.ToSingle(array3[8]));
                                        GameObject gameObject3 = (GameObject)UnityEngine.Object.Instantiate(Cach.UI_LabelNameOverHead != null ? Cach.UI_LabelNameOverHead : Cach.UI_LabelNameOverHead = (GameObject)Cach.UI_LabelNameOverHead != null ? Cach.UI_LabelNameOverHead : Cach.UI_LabelNameOverHead = (GameObject)Resources.Load("UI/LabelNameOverHead"));
                                        gameObject3.name = "RegionLabel";
                                        gameObject3.transform.parent = gameObject2.transform;
                                        float y = 1f;
                                        if (Convert.ToSingle(array3[4]) > 100f)
                                        {
                                            y = 0.8f;
                                        }
                                        else if (Convert.ToSingle(array3[4]) > 1000f)
                                        {
                                            y = 0.5f;
                                        }
                                        gameObject3.transform.localPosition = new Vector3(0f, y, 0f);
                                        gameObject3.transform.localScale = new Vector3(5f / Convert.ToSingle(array3[3]), 5f / Convert.ToSingle(array3[4]), 5f / Convert.ToSingle(array3[5]));
                                        gameObject3.GetComponent<UILabel>().text = array3[2];
                                        gameObject2.AddComponent<RCRegionLabel>();
                                        gameObject2.GetComponent<RCRegionLabel>().myLabel = gameObject3;
                                    }
                                    else if (array3[1].StartsWith("racingStart"))
                                    {
                                        gameObject2 = (GameObject)UnityEngine.Object.Instantiate((GameObject)FengGameManagerMKII.RCassets.Load("racingStart"), new Vector3(Convert.ToSingle(array3[5]), Convert.ToSingle(array3[6]), Convert.ToSingle(array3[7])), new Quaternion(Convert.ToSingle(array3[8]), Convert.ToSingle(array3[9]), Convert.ToSingle(array3[10]), Convert.ToSingle(array3[11])));
                                    }
                                    else if (array3[1].StartsWith("racingEnd"))
                                    {
                                        gameObject2 = (GameObject)UnityEngine.Object.Instantiate((GameObject)FengGameManagerMKII.RCassets.Load("racingEnd"), new Vector3(Convert.ToSingle(array3[5]), Convert.ToSingle(array3[6]), Convert.ToSingle(array3[7])), new Quaternion(Convert.ToSingle(array3[8]), Convert.ToSingle(array3[9]), Convert.ToSingle(array3[10]), Convert.ToSingle(array3[11])));
                                    }
                                }
                                else if (array3[0].StartsWith("racing"))
                                {
                                    gameObject2 = (GameObject)UnityEngine.Object.Instantiate((GameObject)FengGameManagerMKII.RCassets.Load(array3[1]), new Vector3(Convert.ToSingle(array3[5]), Convert.ToSingle(array3[6]), Convert.ToSingle(array3[7])), new Quaternion(Convert.ToSingle(array3[8]), Convert.ToSingle(array3[9]), Convert.ToSingle(array3[10]), Convert.ToSingle(array3[11])));
                                }
                                if (array3[2] != "default" && (array3[0].StartsWith("custom") || (array3[0].StartsWith("base") && array3.Length > 15) || (array3[0].StartsWith("photon") && array3.Length > 15)))
                                {
                                    Renderer[] componentsInChildren = gameObject2.GetComponentsInChildren<Renderer>();
                                    for (int i = 0; i < componentsInChildren.Length; i++)
                                    {
                                        Renderer renderer = componentsInChildren[i];
                                        if (!renderer.name.Contains("Particle System") || !gameObject2.name.Contains("aot_supply"))
                                        {
                                            renderer.material = (Material)FengGameManagerMKII.RCassets.Load(array3[2]);
                                            renderer.material.mainTextureScale = new Vector2(renderer.material.mainTextureScale.x * Convert.ToSingle(array3[10]), renderer.material.mainTextureScale.y * Convert.ToSingle(array3[11]));
                                        }
                                    }
                                }
                                if (array3[0].StartsWith("custom") || (array3[0].StartsWith("base") && array3.Length > 15) || (array3[0].StartsWith("photon") && array3.Length > 15))
                                {
                                    float num12 = gameObject2.transform.localScale.x * Convert.ToSingle(array3[3]);
                                    num12 -= 0.001f;
                                    float y2 = gameObject2.transform.localScale.y * Convert.ToSingle(array3[4]);
                                    float z = gameObject2.transform.localScale.z * Convert.ToSingle(array3[5]);
                                    gameObject2.transform.localScale = new Vector3(num12, y2, z);
                                    if (array3[6] != "0")
                                    {
                                        Color color = new Color(Convert.ToSingle(array3[7]), Convert.ToSingle(array3[8]), Convert.ToSingle(array3[9]), 1f);
                                        MeshFilter[] componentsInChildren2 = gameObject2.GetComponentsInChildren<MeshFilter>();
                                        for (int i = 0; i < componentsInChildren2.Length; i++)
                                        {
                                            MeshFilter meshFilter = componentsInChildren2[i];
                                            Mesh mesh = meshFilter.mesh;
                                            Color[] array4 = new Color[mesh.vertexCount];
                                            for (int k = 0; k < mesh.vertexCount; k++)
                                            {
                                                array4[k] = color;
                                            }
                                            mesh.colors = array4;
                                        }
                                    }
                                    gameObject2.name = string.Concat(new string[]
								{
									array3[0],
									",",
									array3[1],
									",",
									array3[2],
									",",
									array3[3],
									",",
									array3[4],
									",",
									array3[5],
									",",
									array3[6],
									",",
									array3[7],
									",",
									array3[8],
									",",
									array3[9],
									",",
									array3[10],
									",",
									array3[11]
								});
                                }
                                else if (array3[0].StartsWith("misc"))
                                {
                                    if (array3[1].StartsWith("barrier") || array3[1].StartsWith("racing"))
                                    {
                                        float num12 = gameObject2.transform.localScale.x * Convert.ToSingle(array3[2]);
                                        num12 -= 0.001f;
                                        float y2 = gameObject2.transform.localScale.y * Convert.ToSingle(array3[3]);
                                        float z = gameObject2.transform.localScale.z * Convert.ToSingle(array3[4]);
                                        gameObject2.transform.localScale = new Vector3(num12, y2, z);
                                        gameObject2.name = string.Concat(new string[]
									{
										array3[0],
										",",
										array3[1],
										",",
										array3[2],
										",",
										array3[3],
										",",
										array3[4]
									});
                                    }
                                    else if (array3[1].StartsWith("region"))
                                    {
                                        float num12 = gameObject2.transform.localScale.x * Convert.ToSingle(array3[3]);
                                        num12 -= 0.001f;
                                        float y2 = gameObject2.transform.localScale.y * Convert.ToSingle(array3[4]);
                                        float z = gameObject2.transform.localScale.z * Convert.ToSingle(array3[5]);
                                        gameObject2.transform.localScale = new Vector3(num12, y2, z);
                                        gameObject2.name = string.Concat(new string[]
									{
										array3[0],
										",",
										array3[1],
										",",
										array3[2],
										",",
										array3[3],
										",",
										array3[4],
										",",
										array3[5]
									});
                                    }
                                }
                                else if (array3[0].StartsWith("racing"))
                                {
                                    float num12 = gameObject2.transform.localScale.x * Convert.ToSingle(array3[2]);
                                    num12 -= 0.001f;
                                    float y2 = gameObject2.transform.localScale.y * Convert.ToSingle(array3[3]);
                                    float z = gameObject2.transform.localScale.z * Convert.ToSingle(array3[4]);
                                    gameObject2.transform.localScale = new Vector3(num12, y2, z);
                                    gameObject2.name = string.Concat(new string[]
								{
									array3[0],
									",",
									array3[1],
									",",
									array3[2],
									",",
									array3[3],
									",",
									array3[4]
								});
                                }
                                else if (array3[0].StartsWith("photon") && !array3[1].StartsWith("Cannon"))
                                {
                                    gameObject2.name = string.Concat(new string[]
								{
									array3[0],
									",",
									array3[1],
									",",
									array3[2],
									",",
									array3[3]
								});
                                }
                                else
                                {
                                    gameObject2.name = array3[0] + "," + array3[1];
                                }
                                FengGameManagerMKII.linkHash[3].Add(gameObject2.GetInstanceID(), array2[j]);
                            }
                            else if (array3[0].StartsWith("map") && array3[1].StartsWith("disablebounds"))
                            {
                                FengGameManagerMKII.settings[186] = 1;
                                if (!FengGameManagerMKII.linkHash[3].ContainsKey("mapbounds"))
                                {
                                    FengGameManagerMKII.linkHash[3].Add("mapbounds", "map,disablebounds");
                                }
                            }
                        }
                        this.unloadAssets();
                        FengGameManagerMKII.settings[77] = string.Empty;
                    }
                    else if (GUI.Button(new Rect(205f, 500f, 60f, 30f), "Exit"))
                    {
                       
                        Screen.lockCursor = false;
                        Screen.showCursor = true;
                        IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.STOP;
                         FengGameManagerMKII.instance.MenuOn = false;
                        UnityEngine.Object.Destroy(base.gameObject);
                        Application.LoadLevel("menu");
                    }
                    else if (GUI.Button(new Rect(15f, 70f, 115f, 30f), "Copy to Clipboard"))
                    {
                        string text2 = string.Empty;
                        int num13 = 0;
                        using (Dictionary<object, object>.ValueCollection.Enumerator enumerator = FengGameManagerMKII.linkHash[3].Values.GetEnumerator())
                        {
                            while (enumerator.MoveNext())
                            {
                                string str = (string)enumerator.Current;
                                num13++;
                                text2 = text2 + str + ";\n";
                            }
                        }
                        TextEditor textEditor = new TextEditor();
                        textEditor.content = new GUIContent(text2);
                        textEditor.SelectAll();
                        textEditor.Copy();
                    }
                    else if (GUI.Button(new Rect(175f, 70f, 115f, 30f), "View Script"))
                    {
                        FengGameManagerMKII.settings[68] = 102;
                    }
                    if ((int)FengGameManagerMKII.settings[68] == 102)
                    {
                        string text2 = string.Empty;
                        int num13 = 0;
                        using (Dictionary<object, object>.ValueCollection.Enumerator enumerator = FengGameManagerMKII.linkHash[3].Values.GetEnumerator())
                        {
                            while (enumerator.MoveNext())
                            {
                                string str = (string)enumerator.Current;
                                num13++;
                                text2 = text2 + str + ";\n";
                            }
                        }
                        float num14 = (float)(Screen.width / 2) - 110.5f;
                        float num15 = (float)(Screen.height / 2) - 250f;
                        GUI.DrawTexture(new Rect(num14 + 2f, num15 + 2f, 217f, 496f), this.textureBackgroundBlue);
                        GUI.Box(new Rect(num14, num15, 221f, 500f), string.Empty);
                        if (GUI.Button(new Rect(num14 + 10f, num15 + 460f, 60f, 30f), "Copy"))
                        {
                            TextEditor textEditor = new TextEditor();
                            textEditor.content = new GUIContent(text2);
                            textEditor.SelectAll();
                            textEditor.Copy();
                        }
                        else if (GUI.Button(new Rect(num14 + 151f, num15 + 460f, 60f, 30f), "Done"))
                        {
                            FengGameManagerMKII.settings[68] = 100;
                        }
                        GUI.TextArea(new Rect(num14 + 5f, num15 + 5f, 211f, 415f), text2);
                        GUI.Label(new Rect(num14 + 10f, num15 + 430f, 150f, 20f), "Object Count: " + Convert.ToString(num13), "Label");
                    }
                }
                else if ((int)FengGameManagerMKII.settings[68] == 101)
                {
                    GUI.Label(new Rect(92f, 50f, 180f, 20f), "Level Editor Rebinds:", "Label");
                    GUI.Label(new Rect(12f, 80f, 145f, 20f), "Forward:", "Label");
                    GUI.Label(new Rect(12f, 105f, 145f, 20f), "Back:", "Label");
                    GUI.Label(new Rect(12f, 130f, 145f, 20f), "Left:", "Label");
                    GUI.Label(new Rect(12f, 155f, 145f, 20f), "Right:", "Label");
                    GUI.Label(new Rect(12f, 180f, 145f, 20f), "Up:", "Label");
                    GUI.Label(new Rect(12f, 205f, 145f, 20f), "Down:", "Label");
                    GUI.Label(new Rect(12f, 230f, 145f, 20f), "Toggle Cursor:", "Label");
                    GUI.Label(new Rect(12f, 255f, 145f, 20f), "Place Object:", "Label");
                    GUI.Label(new Rect(12f, 280f, 145f, 20f), "Delete Object:", "Label");
                    GUI.Label(new Rect(12f, 305f, 145f, 20f), "Movement-Slow:", "Label");
                    GUI.Label(new Rect(12f, 330f, 145f, 20f), "Rotate Forward:", "Label");
                    GUI.Label(new Rect(12f, 355f, 145f, 20f), "Rotate Backward:", "Label");
                    GUI.Label(new Rect(12f, 380f, 145f, 20f), "Rotate Left:", "Label");
                    GUI.Label(new Rect(12f, 405f, 145f, 20f), "Rotate Right:", "Label");
                    GUI.Label(new Rect(12f, 430f, 145f, 20f), "Rotate CCW:", "Label");
                    GUI.Label(new Rect(12f, 455f, 145f, 20f), "Rotate CW:", "Label");
                    GUI.Label(new Rect(12f, 480f, 145f, 20f), "Movement-Speedup:", "Label");
                    for (int j = 0; j < 17; j++)
                    {
                        float top = 80f + 25f * (float)j;
                        int num16 = 117 + j;
                        if (j == 16)
                        {
                            num16 = 161;
                        }
                        if (GUI.Button(new Rect(135f, top, 60f, 20f), (string)FengGameManagerMKII.settings[num16], "box"))
                        {
                            FengGameManagerMKII.settings[num16] = "waiting...";
                            FengGameManagerMKII.settings[100] = num16;
                        }
                    }
                    if ((int)FengGameManagerMKII.settings[100] != 0)
                    {
                        Event current = Event.current;
                        bool flag3 = false;
                        string text3 = "waiting...";
                        if (current.type == EventType.KeyDown && current.keyCode != KeyCode.None)
                        {
                            flag3 = true;
                            text3 = current.keyCode.ToString();
                        }
                        else if (Input.GetKey(KeyCode.LeftShift))
                        {
                            flag3 = true;
                            text3 = KeyCode.LeftShift.ToString();
                        }
                        else if (Input.GetKey(KeyCode.RightShift))
                        {
                            flag3 = true;
                            text3 = KeyCode.RightShift.ToString();
                        }
                        else if (Input.GetAxis("Mouse ScrollWheel") != 0f)
                        {
                            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                            {
                                flag3 = true;
                                text3 = "Scroll Up";
                            }
                            else
                            {
                                flag3 = true;
                                text3 = "Scroll Down";
                            }
                        }
                        else
                        {
                            for (int j = 0; j < 7; j++)
                            {
                                if (Input.GetKeyDown(KeyCode.Mouse0 + j))
                                {
                                    flag3 = true;
                                    text3 = "Mouse" + Convert.ToString(j);
                                }
                            }
                        }
                        if (flag3)
                        {
                            for (int j = 0; j < 17; j++)
                            {
                                int num16 = 117 + j;
                                if (j == 16)
                                {
                                    num16 = 161;
                                }
                                if ((int)FengGameManagerMKII.settings[100] == num16)
                                {
                                    FengGameManagerMKII.settings[num16] = text3;
                                    FengGameManagerMKII.settings[100] = 0;
                                    FengGameManagerMKII.inputRC.setInputLevel(j, text3);
                                }
                            }
                        }
                    }
                    if (GUI.Button(new Rect(100f, 515f, 110f, 30f), "Save Controls"))
                    {
                        PrefersCyan.SetString("stringlforward", (string)FengGameManagerMKII.settings[117]);
                        PrefersCyan.SetString("stringlback", (string)FengGameManagerMKII.settings[118]);
                        PrefersCyan.SetString("stringlleft", (string)FengGameManagerMKII.settings[119]);
                        PrefersCyan.SetString("stringlright", (string)FengGameManagerMKII.settings[120]);
                        PrefersCyan.SetString("stringlup", (string)FengGameManagerMKII.settings[121]);
                        PrefersCyan.SetString("stringldown", (string)FengGameManagerMKII.settings[122]);
                        PrefersCyan.SetString("stringlcursor", (string)FengGameManagerMKII.settings[123]);
                        PrefersCyan.SetString("stringlplace", (string)FengGameManagerMKII.settings[124]);
                        PrefersCyan.SetString("stringldel", (string)FengGameManagerMKII.settings[125]);
                        PrefersCyan.SetString("stringlslow", (string)FengGameManagerMKII.settings[126]);
                        PrefersCyan.SetString("stringlrforward", (string)FengGameManagerMKII.settings[127]);
                        PrefersCyan.SetString("stringlrback", (string)FengGameManagerMKII.settings[128]);
                        PrefersCyan.SetString("stringlrleft", (string)FengGameManagerMKII.settings[129]);
                        PrefersCyan.SetString("stringlrright", (string)FengGameManagerMKII.settings[130]);
                        PrefersCyan.SetString("stringlrccw", (string)FengGameManagerMKII.settings[131]);
                        PrefersCyan.SetString("stringlrcw", (string)FengGameManagerMKII.settings[132]);
                        PrefersCyan.SetString("stringlfast", (string)FengGameManagerMKII.settings[161]);
                    }
                }
                if ((int)FengGameManagerMKII.settings[64] != 105 && (int)FengGameManagerMKII.settings[64] != 106)
                {
                    GUI.Label(new Rect(num11 + 13f, 445f, 125f, 20f), "Scale Multipliers:", "Label");
                    GUI.Label(new Rect(num11 + 13f, 470f, 50f, 22f), "Length:", "Label");
                    FengGameManagerMKII.settings[72] = GUI.TextField(new Rect(num11 + 58f, 470f, 40f, 20f), (string)FengGameManagerMKII.settings[72]);
                    GUI.Label(new Rect(num11 + 13f, 495f, 50f, 20f), "Width:", "Label");
                    FengGameManagerMKII.settings[70] = GUI.TextField(new Rect(num11 + 58f, 495f, 40f, 20f), (string)FengGameManagerMKII.settings[70]);
                    GUI.Label(new Rect(num11 + 13f, 520f, 50f, 22f), "Height:", "Label");
                    FengGameManagerMKII.settings[71] = GUI.TextField(new Rect(num11 + 58f, 520f, 40f, 20f), (string)FengGameManagerMKII.settings[71]);
                    if ((int)FengGameManagerMKII.settings[64] <= 106)
                    {
                        GUI.Label(new Rect(num11 + 155f, 554f, 50f, 22f), "Tiling:", "Label");
                        FengGameManagerMKII.settings[79] = GUI.TextField(new Rect(num11 + 200f, 554f, 40f, 20f), (string)FengGameManagerMKII.settings[79]);
                        FengGameManagerMKII.settings[80] = GUI.TextField(new Rect(num11 + 245f, 554f, 40f, 20f), (string)FengGameManagerMKII.settings[80]);
                        GUI.Label(new Rect(num11 + 219f, 570f, 10f, 22f), "x:", "Label");
                        GUI.Label(new Rect(num11 + 264f, 570f, 10f, 22f), "y:", "Label");
                        GUI.Label(new Rect(num11 + 155f, 445f, 50f, 20f), "Color:", "Label");
                        GUI.Label(new Rect(num11 + 155f, 470f, 10f, 20f), "R:", "Label");
                        GUI.Label(new Rect(num11 + 155f, 495f, 10f, 20f), "G:", "Label");
                        GUI.Label(new Rect(num11 + 155f, 520f, 10f, 20f), "B:", "Label");
                        FengGameManagerMKII.settings[73] = GUI.HorizontalSlider(new Rect(num11 + 170f, 475f, 100f, 20f), (float)FengGameManagerMKII.settings[73], 0f, 1f);
                        FengGameManagerMKII.settings[74] = GUI.HorizontalSlider(new Rect(num11 + 170f, 500f, 100f, 20f), (float)FengGameManagerMKII.settings[74], 0f, 1f);
                        FengGameManagerMKII.settings[75] = GUI.HorizontalSlider(new Rect(num11 + 170f, 525f, 100f, 20f), (float)FengGameManagerMKII.settings[75], 0f, 1f);
                        GUI.Label(new Rect(num11 + 13f, 554f, 57f, 22f), "Material:", "Label");
                        if (GUI.Button(new Rect(num11 + 66f, 554f, 60f, 20f), (string)FengGameManagerMKII.settings[69]))
                        {
                            FengGameManagerMKII.settings[78] = 1;
                        }
                        if ((int)FengGameManagerMKII.settings[78] == 1)
                        {
                            string[] item = new string[]
						{
							"bark",
							"bark2",
							"bark3",
							"bark4"
						};
                            string[] item2 = new string[]
						{
							"wood1",
							"wood2",
							"wood3",
							"wood4"
						};
                            string[] item3 = new string[]
						{
							"grass",
							"grass2",
							"grass3",
							"grass4"
						};
                            string[] item4 = new string[]
						{
							"brick1",
							"brick2",
							"brick3",
							"brick4"
						};
                            string[] item5 = new string[]
						{
							"metal1",
							"metal2",
							"metal3",
							"metal4"
						};
                            string[] item6 = new string[]
						{
							"rock1",
							"rock2",
							"rock3"
						};
                            string[] item7 = new string[]
						{
							"stone1",
							"stone2",
							"stone3",
							"stone4",
							"stone5",
							"stone6",
							"stone7",
							"stone8",
							"stone9",
							"stone10"
						};
                            string[] item8 = new string[]
						{
							"earth1",
							"earth2",
							"ice1",
							"lava1",
							"crystal1",
							"crystal2",
							"empty"
						};
                            List<string[]> list = new List<string[]>
						{
							item,
							item2,
							item3,
							item4,
							item5,
							item6,
							item7,
							item8
						};
                            string[] array5 = new string[]
						{
							"bark",
							"wood",
							"grass",
							"brick",
							"metal",
							"rock",
							"stone",
							"misc",
							"transparent"
						};
                            int num17 = 78;
                            int num18 = 69;
                            float num14 = (float)(Screen.width / 2) - 110.5f;
                            float num15 = (float)(Screen.height / 2) - 220f;
                            int num19 = (int)FengGameManagerMKII.settings[185];
                            float num20 = 10f + 104f * (float)(list[num19].Length / 3 + 1);
                            num20 = Math.Max(num20, 280f);
                            GUI.DrawTexture(new Rect(num14 + 2f, num15 + 2f, 208f, 446f), this.textureBackgroundBlue);
                            GUI.Box(new Rect(num14, num15, 212f, 450f), string.Empty);
                            for (int j = 0; j < list.Count; j++)
                            {
                                int num21 = j / 3;
                                int num22 = j % 3;
                                if (GUI.Button(new Rect(num14 + 5f + 69f * (float)num22, num15 + 5f + (float)(30 * num21), 64f, 25f), array5[j], "box"))
                                {
                                    FengGameManagerMKII.settings[185] = j;
                                }
                            }
                            this.scroll2 = GUI.BeginScrollView(new Rect(num14, num15 + 110f, 225f, 290f), this.scroll2, new Rect(num14, num15 + 110f, 212f, num20), true, true);
                            if (num19 != 8)
                            {
                                for (int j = 0; j < list[num19].Length; j++)
                                {
                                    int num21 = j / 3;
                                    int num22 = j % 3;
                                    GUI.DrawTexture(new Rect(num14 + 5f + 69f * (float)num22, num15 + 115f + 104f * (float)num21, 64f, 64f), this.RCLoadTexture("p" + list[num19][j]));
                                    if (GUI.Button(new Rect(num14 + 5f + 69f * (float)num22, num15 + 184f + 104f * (float)num21, 64f, 30f), list[num19][j]))
                                    {
                                        FengGameManagerMKII.settings[num18] = list[num19][j];
                                        FengGameManagerMKII.settings[num17] = 0;
                                    }
                                }
                            }
                            GUI.EndScrollView();
                            if (GUI.Button(new Rect(num14 + 24f, num15 + 410f, 70f, 30f), "Default"))
                            {
                                FengGameManagerMKII.settings[num18] = "default";
                                FengGameManagerMKII.settings[num17] = 0;
                            }
                            else if (GUI.Button(new Rect(num14 + 118f, num15 + 410f, 70f, 30f), "Done"))
                            {
                                FengGameManagerMKII.settings[num17] = 0;
                            }
                        }
                        bool flag4 = false;
                        if ((int)FengGameManagerMKII.settings[76] == 1)
                        {
                            flag4 = true;
                            Texture2D texture2D = new Texture2D(1, 1, TextureFormat.ARGB32, false);
                            texture2D.SetPixel(0, 0, new Color((float)FengGameManagerMKII.settings[73], (float)FengGameManagerMKII.settings[74], (float)FengGameManagerMKII.settings[75], 1f));
                            texture2D.Apply();
                            GUI.DrawTexture(new Rect(num11 + 235f, 445f, 30f, 20f), texture2D, ScaleMode.StretchToFill);
                            UnityEngine.Object.Destroy(texture2D);
                        }
                        bool flag5 = GUI.Toggle(new Rect(num11 + 193f, 445f, 40f, 20f), flag4, "On");
                        if (flag4 != flag5)
                        {
                            if (flag5)
                            {
                                FengGameManagerMKII.settings[76] = 1;
                            }
                            else
                            {
                                FengGameManagerMKII.settings[76] = 0;
                            }
                        }
                    }
                }
                if (GUI.Button(new Rect(num11 + 5f, 10f, 60f, 25f), "General", "box"))
                {
                    FengGameManagerMKII.settings[64] = 101;
                }
                else if (GUI.Button(new Rect(num11 + 70f, 10f, 70f, 25f), "Geometry", "box"))
                {
                    FengGameManagerMKII.settings[64] = 102;
                }
                else if (GUI.Button(new Rect(num11 + 145f, 10f, 65f, 25f), "Buildings", "box"))
                {
                    FengGameManagerMKII.settings[64] = 103;
                }
                else if (GUI.Button(new Rect(num11 + 215f, 10f, 50f, 25f), "Nature", "box"))
                {
                    FengGameManagerMKII.settings[64] = 104;
                }
                else if (GUI.Button(new Rect(num11 + 5f, 45f, 70f, 25f), "Spawners", "box"))
                {
                    FengGameManagerMKII.settings[64] = 105;
                }
                else if (GUI.Button(new Rect(num11 + 80f, 45f, 70f, 25f), "Racing", "box"))
                {
                    FengGameManagerMKII.settings[64] = 108;
                }
                else if (GUI.Button(new Rect(num11 + 155f, 45f, 40f, 25f), "Misc", "box"))
                {
                    FengGameManagerMKII.settings[64] = 107;
                }
                else if (GUI.Button(new Rect(num11 + 200f, 45f, 70f, 25f), "Credits", "box"))
                {
                    FengGameManagerMKII.settings[64] = 106;
                }
                if ((int)FengGameManagerMKII.settings[64] == 101)
                {
                    this.scroll = GUI.BeginScrollView(new Rect(num11, 80f, 305f, 350f), this.scroll, new Rect(num11, 80f, 300f, 470f), true, true);
                    GUI.Label(new Rect(num11 + 100f, 80f, 120f, 20f), "General Objects:", "Label");
                    GUI.Label(new Rect(num11 + 108f, 245f, 120f, 20f), "Spawn Points:", "Label");
                    GUI.Label(new Rect(num11 + 7f, 415f, 290f, 60f), "* The above titan spawn points apply only to randomly spawned titans specified by the Random Titan #.", "Label");
                    GUI.Label(new Rect(num11 + 7f, 470f, 290f, 60f), "* If team mode is disabled both cyan and magenta spawn points will be randomly chosen for players.", "Label");
                    GUI.DrawTexture(new Rect(num11 + 27f, 110f, 64f, 64f), this.RCLoadTexture("psupply"));
                    GUI.DrawTexture(new Rect(num11 + 118f, 110f, 64f, 64f), this.RCLoadTexture("pcannonwall"));
                    GUI.DrawTexture(new Rect(num11 + 209f, 110f, 64f, 64f), this.RCLoadTexture("pcannonground"));
                    GUI.DrawTexture(new Rect(num11 + 27f, 275f, 64f, 64f), this.RCLoadTexture("pspawnt"));
                    GUI.DrawTexture(new Rect(num11 + 118f, 275f, 64f, 64f), this.RCLoadTexture("pspawnplayerC"));
                    GUI.DrawTexture(new Rect(num11 + 209f, 275f, 64f, 64f), this.RCLoadTexture("pspawnplayerM"));
                    if (GUI.Button(new Rect(num11 + 27f, 179f, 64f, 60f), "Supply"))
                    {
                        flag = true;
                        GameObject original = (GameObject)Resources.Load("aot_supply");
                        this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(original);
                        this.selectedObj.name = "base,aot_supply";
                    }
                    else if (GUI.Button(new Rect(num11 + 118f, 179f, 64f, 60f), "Cannon \nWall"))
                    {
                        flag = true;
                        GameObject original = (GameObject)FengGameManagerMKII.RCassets.Load("CannonWallProp");
                        this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(original);
                        this.selectedObj.name = "photon,CannonWall";
                    }
                    else if (GUI.Button(new Rect(num11 + 209f, 179f, 64f, 60f), "Cannon\n Ground"))
                    {
                        flag = true;
                        GameObject original = (GameObject)FengGameManagerMKII.RCassets.Load("CannonGroundProp");
                        this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(original);
                        this.selectedObj.name = "photon,CannonGround";
                    }
                    else if (GUI.Button(new Rect(num11 + 27f, 344f, 64f, 60f), "Titan"))
                    {
                        flag = true;
                        flag2 = true;
                        GameObject original = (GameObject)FengGameManagerMKII.RCassets.Load("titan");
                        this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(original);
                        this.selectedObj.name = "spawnpoint,titan";
                    }
                    else if (GUI.Button(new Rect(num11 + 118f, 344f, 64f, 60f), "Player \nCyan"))
                    {
                        flag = true;
                        flag2 = true;
                        GameObject original = (GameObject)FengGameManagerMKII.RCassets.Load("playerC");
                        this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(original);
                        this.selectedObj.name = "spawnpoint,playerC";
                    }
                    else if (GUI.Button(new Rect(num11 + 209f, 344f, 64f, 60f), "Player \nMagenta"))
                    {
                        flag = true;
                        flag2 = true;
                        GameObject original = (GameObject)FengGameManagerMKII.RCassets.Load("playerM");
                        this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(original);
                        this.selectedObj.name = "spawnpoint,playerM";
                    }
                    GUI.EndScrollView();
                }
                else if ((int)FengGameManagerMKII.settings[64] == 107)
                {
                    GUI.DrawTexture(new Rect(num11 + 30f, 90f, 64f, 64f), this.RCLoadTexture("pbarrier"));
                    GUI.DrawTexture(new Rect(num11 + 30f, 199f, 64f, 64f), this.RCLoadTexture("pregion"));
                    GUI.Label(new Rect(num11 + 110f, 243f, 200f, 22f), "Region Name:", "Label");
                    GUI.Label(new Rect(num11 + 110f, 179f, 200f, 22f), "Disable Map Bounds:", "Label");
                    bool flag6 = false;
                    if ((int)FengGameManagerMKII.settings[186] == 1)
                    {
                        flag6 = true;
                        if (!FengGameManagerMKII.linkHash[3].ContainsKey("mapbounds"))
                        {
                            FengGameManagerMKII.linkHash[3].Add("mapbounds", "map,disablebounds");
                        }
                    }
                    else if (FengGameManagerMKII.linkHash[3].ContainsKey("mapbounds"))
                    {
                        FengGameManagerMKII.linkHash[3].Remove("mapbounds");
                    }
                    if (GUI.Button(new Rect(num11 + 30f, 159f, 64f, 30f), "Barrier"))
                    {
                        flag = true;
                        flag2 = true;
                        GameObject original2 = (GameObject)FengGameManagerMKII.RCassets.Load("barrierEditor");
                        this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(original2);
                        this.selectedObj.name = "misc,barrier";
                    }
                    else if (GUI.Button(new Rect(num11 + 30f, 268f, 64f, 30f), "Region"))
                    {
                        if ((string)FengGameManagerMKII.settings[191] == string.Empty)
                        {
                            FengGameManagerMKII.settings[191] = "Region" + UnityEngine.Random.Range(10000, 99999).ToString();
                        }
                        flag = true;
                        flag2 = true;
                        GameObject original2 = (GameObject)FengGameManagerMKII.RCassets.Load("regionEditor");
                        this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(original2);
                        GameObject gameObject3 = (GameObject)UnityEngine.Object.Instantiate(Cach.UI_LabelNameOverHead != null ? Cach.UI_LabelNameOverHead : Cach.UI_LabelNameOverHead = (GameObject)Cach.UI_LabelNameOverHead != null ? Cach.UI_LabelNameOverHead : Cach.UI_LabelNameOverHead = (GameObject)Resources.Load("UI/LabelNameOverHead"));
                        gameObject3.name = "RegionLabel";
                        float num23;
                        if (!float.TryParse((string)FengGameManagerMKII.settings[71], out num23))
                        {
                            FengGameManagerMKII.settings[71] = "1";
                        }
                        if (!float.TryParse((string)FengGameManagerMKII.settings[70], out num23))
                        {
                            FengGameManagerMKII.settings[70] = "1";
                        }
                        if (!float.TryParse((string)FengGameManagerMKII.settings[72], out num23))
                        {
                            FengGameManagerMKII.settings[72] = "1";
                        }
                        gameObject3.transform.parent = this.selectedObj.transform;
                        float y = 1f;
                        if (Convert.ToSingle((string)FengGameManagerMKII.settings[71]) > 100f)
                        {
                            y = 0.8f;
                        }
                        else if (Convert.ToSingle((string)FengGameManagerMKII.settings[71]) > 1000f)
                        {
                            y = 0.5f;
                        }
                        gameObject3.transform.localPosition = new Vector3(0f, y, 0f);
                        gameObject3.transform.localScale = new Vector3(5f / Convert.ToSingle((string)FengGameManagerMKII.settings[70]), 5f / Convert.ToSingle((string)FengGameManagerMKII.settings[71]), 5f / Convert.ToSingle((string)FengGameManagerMKII.settings[72]));
                        gameObject3.GetComponent<UILabel>().text = (string)FengGameManagerMKII.settings[191];
                        this.selectedObj.AddComponent<RCRegionLabel>();
                        this.selectedObj.GetComponent<RCRegionLabel>().myLabel = gameObject3;
                        this.selectedObj.name = "misc,region," + (string)FengGameManagerMKII.settings[191];
                    }
                    FengGameManagerMKII.settings[191] = GUI.TextField(new Rect(num11 + 200f, 243f, 75f, 20f), (string)FengGameManagerMKII.settings[191]);
                    bool flag7;
                    if ((flag7 = GUI.Toggle(new Rect(num11 + 240f, 179f, 40f, 20f), flag6, "On")) != flag6)
                    {
                        if (flag7)
                        {
                            FengGameManagerMKII.settings[186] = 1;
                        }
                        else
                        {
                            FengGameManagerMKII.settings[186] = 0;
                        }
                    }
                }
                else if ((int)FengGameManagerMKII.settings[64] == 105)
                {
                    GUI.Label(new Rect(num11 + 95f, 85f, 130f, 20f), "Custom Spawners:", "Label");
                    GUI.DrawTexture(new Rect(num11 + 7.8f, 110f, 64f, 64f), this.RCLoadTexture("ptitan"));
                    GUI.DrawTexture(new Rect(num11 + 79.6f, 110f, 64f, 64f), this.RCLoadTexture("pabnormal"));
                    GUI.DrawTexture(new Rect(num11 + 151.4f, 110f, 64f, 64f), this.RCLoadTexture("pjumper"));
                    GUI.DrawTexture(new Rect(num11 + 223.2f, 110f, 64f, 64f), this.RCLoadTexture("pcrawler"));
                    GUI.DrawTexture(new Rect(num11 + 7.8f, 224f, 64f, 64f), this.RCLoadTexture("ppunk"));
                    GUI.DrawTexture(new Rect(num11 + 79.6f, 224f, 64f, 64f), this.RCLoadTexture("pannie"));
                    if (GUI.Button(new Rect(num11 + 7.8f, 179f, 64f, 30f), "Titan"))
                    {
                        float num24;
                        if (!float.TryParse((string)FengGameManagerMKII.settings[83], out num24))
                        {
                            FengGameManagerMKII.settings[83] = "30";
                        }
                        flag = true;
                        flag2 = true;
                        GameObject original3 = (GameObject)FengGameManagerMKII.RCassets.Load("spawnTitan");
                        this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(original3);
                        this.selectedObj.name = "photon,spawnTitan," + (string)FengGameManagerMKII.settings[83] + "," + ((int)FengGameManagerMKII.settings[84]).ToString();
                    }
                    else if (GUI.Button(new Rect(num11 + 79.6f, 179f, 64f, 30f), "Aberrant"))
                    {
                        float num24;
                        if (!float.TryParse((string)FengGameManagerMKII.settings[83], out num24))
                        {
                            FengGameManagerMKII.settings[83] = "30";
                        }
                        flag = true;
                        flag2 = true;
                        GameObject original3 = (GameObject)FengGameManagerMKII.RCassets.Load("spawnAbnormal");
                        this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(original3);
                        this.selectedObj.name = "photon,spawnAbnormal," + (string)FengGameManagerMKII.settings[83] + "," + ((int)FengGameManagerMKII.settings[84]).ToString();
                    }
                    else if (GUI.Button(new Rect(num11 + 151.4f, 179f, 64f, 30f), "Jumper"))
                    {
                        float num24;
                        if (!float.TryParse((string)FengGameManagerMKII.settings[83], out num24))
                        {
                            FengGameManagerMKII.settings[83] = "30";
                        }
                        flag = true;
                        flag2 = true;
                        GameObject original3 = (GameObject)FengGameManagerMKII.RCassets.Load("spawnJumper");
                        this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(original3);
                        this.selectedObj.name = "photon,spawnJumper," + (string)FengGameManagerMKII.settings[83] + "," + ((int)FengGameManagerMKII.settings[84]).ToString();
                    }
                    else if (GUI.Button(new Rect(num11 + 223.2f, 179f, 64f, 30f), "Crawler"))
                    {
                        float num24;
                        if (!float.TryParse((string)FengGameManagerMKII.settings[83], out num24))
                        {
                            FengGameManagerMKII.settings[83] = "30";
                        }
                        flag = true;
                        flag2 = true;
                        GameObject original3 = (GameObject)FengGameManagerMKII.RCassets.Load("spawnCrawler");
                        this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(original3);
                        this.selectedObj.name = "photon,spawnCrawler," + (string)FengGameManagerMKII.settings[83] + "," + ((int)FengGameManagerMKII.settings[84]).ToString();
                    }
                    else if (GUI.Button(new Rect(num11 + 7.8f, 293f, 64f, 30f), "Punk"))
                    {
                        float num24;
                        if (!float.TryParse((string)FengGameManagerMKII.settings[83], out num24))
                        {
                            FengGameManagerMKII.settings[83] = "30";
                        }
                        flag = true;
                        flag2 = true;
                        GameObject original3 = (GameObject)FengGameManagerMKII.RCassets.Load("spawnPunk");
                        this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(original3);
                        this.selectedObj.name = "photon,spawnPunk," + (string)FengGameManagerMKII.settings[83] + "," + ((int)FengGameManagerMKII.settings[84]).ToString();
                    }
                    else if (GUI.Button(new Rect(num11 + 79.6f, 293f, 64f, 30f), "Annie"))
                    {
                        float num24;
                        if (!float.TryParse((string)FengGameManagerMKII.settings[83], out num24))
                        {
                            FengGameManagerMKII.settings[83] = "30";
                        }
                        flag = true;
                        flag2 = true;
                        GameObject original3 = (GameObject)FengGameManagerMKII.RCassets.Load("spawnAnnie");
                        this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(original3);
                        this.selectedObj.name = "photon,spawnAnnie," + (string)FengGameManagerMKII.settings[83] + "," + ((int)FengGameManagerMKII.settings[84]).ToString();
                    }
                    GUI.Label(new Rect(num11 + 7f, 379f, 140f, 22f), "Spawn Timer:", "Label");
                    FengGameManagerMKII.settings[83] = GUI.TextField(new Rect(num11 + 100f, 379f, 50f, 20f), (string)FengGameManagerMKII.settings[83]);
                    GUI.Label(new Rect(num11 + 7f, 356f, 140f, 22f), "Endless spawn:", "Label");
                    GUI.Label(new Rect(num11 + 7f, 405f, 290f, 80f), "* The above settings apply only to the next placed spawner. You can have unique spawn times and settings for each individual titan spawner.", "Label");
                    bool flag8 = false;
                    if ((int)FengGameManagerMKII.settings[84] == 1)
                    {
                        flag8 = true;
                    }
                    bool flag9 = GUI.Toggle(new Rect(num11 + 100f, 356f, 40f, 20f), flag8, "On");
                    if (flag8 != flag9)
                    {
                        if (flag9)
                        {
                            FengGameManagerMKII.settings[84] = 1;
                        }
                        else
                        {
                            FengGameManagerMKII.settings[84] = 0;
                        }
                    }
                }
                else if ((int)FengGameManagerMKII.settings[64] == 102)
                {
                    string[] array6 = new string[]
				{
					"cuboid",
					"plane",
					"sphere",
					"cylinder",
					"capsule",
					"pyramid",
					"cone",
					"prism",
					"arc90",
					"arc180",
					"torus",
					"tube"
				};
                    for (int j = 0; j < array6.Length; j++)
                    {
                        int num22 = j % 4;
                        int num21 = j / 4;
                        GUI.DrawTexture(new Rect(num11 + 7.8f + 71.8f * (float)num22, 90f + 114f * (float)num21, 64f, 64f), this.RCLoadTexture("p" + array6[j]));
                        if (GUI.Button(new Rect(num11 + 7.8f + 71.8f * (float)num22, 159f + 114f * (float)num21, 64f, 30f), array6[j]))
                        {
                            flag = true;
                            GameObject original2 = (GameObject)FengGameManagerMKII.RCassets.Load(array6[j]);
                            this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(original2);
                            this.selectedObj.name = "custom," + array6[j];
                        }
                    }
                }
                else if ((int)FengGameManagerMKII.settings[64] == 103)
                {
                    List<string> list2 = new List<string>
				{
					"arch1",
					"house1"
				};
                    string[] array6 = new string[]
				{
					"tower1",
					"tower2",
					"tower3",
					"tower4",
					"tower5",
					"house1",
					"house2",
					"house3",
					"house4",
					"house5",
					"house6",
					"house7",
					"house8",
					"house9",
					"house10",
					"house11",
					"house12",
					"house13",
					"house14",
					"pillar1",
					"pillar2",
					"village1",
					"village2",
					"windmill1",
					"arch1",
					"canal1",
					"castle1",
					"church1",
					"cannon1",
					"statue1",
					"statue2",
					"wagon1",
					"elevator1",
					"bridge1",
					"dummy1",
					"spike1",
					"wall1",
					"wall2",
					"wall3",
					"wall4",
					"arena1",
					"arena2",
					"arena3",
					"arena4"
				};
                    float num20 = 110f + 114f * (float)((array6.Length - 1) / 4);
                    this.scroll = GUI.BeginScrollView(new Rect(num11, 90f, 303f, 350f), this.scroll, new Rect(num11, 90f, 300f, num20), true, true);
                    for (int j = 0; j < array6.Length; j++)
                    {
                        int num22 = j % 4;
                        int num21 = j / 4;
                        GUI.DrawTexture(new Rect(num11 + 7.8f + 71.8f * (float)num22, 90f + 114f * (float)num21, 64f, 64f), this.RCLoadTexture("p" + array6[j]));
                        if (GUI.Button(new Rect(num11 + 7.8f + 71.8f * (float)num22, 159f + 114f * (float)num21, 64f, 30f), array6[j]))
                        {
                            flag = true;
                            GameObject original4 = (GameObject)FengGameManagerMKII.RCassets.Load(array6[j]);
                            this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(original4);
                            if (list2.Contains(array6[j]))
                            {
                                this.selectedObj.name = "customb," + array6[j];
                            }
                            else
                            {
                                this.selectedObj.name = "custom," + array6[j];
                            }
                        }
                    }
                    GUI.EndScrollView();
                }
                else if ((int)FengGameManagerMKII.settings[64] == 104)
                {
                    List<string> list2 = new List<string>
				{
					"tree0"
				};
                    string[] array6 = new string[]
				{
					"leaf0",
					"leaf1",
					"leaf2",
					"field1",
					"field2",
					"tree0",
					"tree1",
					"tree2",
					"tree3",
					"tree4",
					"tree5",
					"tree6",
					"tree7",
					"log1",
					"log2",
					"trunk1",
					"boulder1",
					"boulder2",
					"boulder3",
					"boulder4",
					"boulder5",
					"cave1",
					"cave2"
				};
                    float num20 = 110f + 114f * (float)((array6.Length - 1) / 4);
                    this.scroll = GUI.BeginScrollView(new Rect(num11, 90f, 303f, 350f), this.scroll, new Rect(num11, 90f, 300f, num20), true, true);
                    for (int j = 0; j < array6.Length; j++)
                    {
                        int num22 = j % 4;
                        int num21 = j / 4;
                        GUI.DrawTexture(new Rect(num11 + 7.8f + 71.8f * (float)num22, 90f + 114f * (float)num21, 64f, 64f), this.RCLoadTexture("p" + array6[j]));
                        if (GUI.Button(new Rect(num11 + 7.8f + 71.8f * (float)num22, 159f + 114f * (float)num21, 64f, 30f), array6[j]))
                        {
                            flag = true;
                            GameObject original4 = (GameObject)FengGameManagerMKII.RCassets.Load(array6[j]);
                            this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(original4);
                            if (list2.Contains(array6[j]))
                            {
                                this.selectedObj.name = "customb," + array6[j];
                            }
                            else
                            {
                                this.selectedObj.name = "custom," + array6[j];
                            }
                        }
                    }
                    GUI.EndScrollView();
                }
                else if ((int)FengGameManagerMKII.settings[64] == 108)
                {
                    string[] array7 = new string[]
				{
					"Cuboid",
					"Plane",
					"Sphere",
					"Cylinder",
					"Capsule",
					"Pyramid",
					"Cone",
					"Prism",
					"Arc90",
					"Arc180",
					"Torus",
					"Tube"
				};
                    string[] array6 = new string[12];
                    for (int j = 0; j < array6.Length; j++)
                    {
                        array6[j] = "start" + array7[j];
                    }
                    float num20 = 110f + 114f * (float)((array6.Length - 1) / 4);
                    num20 *= 4f;
                    num20 += 200f;
                    this.scroll = GUI.BeginScrollView(new Rect(num11, 90f, 303f, 350f), this.scroll, new Rect(num11, 90f, 300f, num20), true, true);
                    GUI.Label(new Rect(num11 + 90f, 90f, 200f, 22f), "Racing Start Barrier");
                    int num25 = 125;
                    for (int j = 0; j < array6.Length; j++)
                    {
                        int num22 = j % 4;
                        int num21 = j / 4;
                        GUI.DrawTexture(new Rect(num11 + 7.8f + 71.8f * (float)num22, (float)num25 + 114f * (float)num21, 64f, 64f), this.RCLoadTexture("p" + array6[j]));
                        if (GUI.Button(new Rect(num11 + 7.8f + 71.8f * (float)num22, (float)num25 + 69f + 114f * (float)num21, 64f, 30f), array7[j]))
                        {
                            flag = true;
                            flag2 = true;
                            GameObject original4 = (GameObject)FengGameManagerMKII.RCassets.Load(array6[j]);
                            this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(original4);
                            this.selectedObj.name = "racing," + array6[j];
                        }
                    }
                    num25 += 114 * (array6.Length / 4) + 10;
                    GUI.Label(new Rect(num11 + 93f, (float)num25, 200f, 22f), "Racing End Trigger");
                    num25 += 35;
                    for (int j = 0; j < array6.Length; j++)
                    {
                        array6[j] = "end" + array7[j];
                    }
                    for (int j = 0; j < array6.Length; j++)
                    {
                        int num22 = j % 4;
                        int num21 = j / 4;
                        GUI.DrawTexture(new Rect(num11 + 7.8f + 71.8f * (float)num22, (float)num25 + 114f * (float)num21, 64f, 64f), this.RCLoadTexture("p" + array6[j]));
                        if (GUI.Button(new Rect(num11 + 7.8f + 71.8f * (float)num22, (float)num25 + 69f + 114f * (float)num21, 64f, 30f), array7[j]))
                        {
                            flag = true;
                            flag2 = true;
                            GameObject original4 = (GameObject)FengGameManagerMKII.RCassets.Load(array6[j]);
                            this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(original4);
                            this.selectedObj.name = "racing," + array6[j];
                        }
                    }
                    num25 += 114 * (array6.Length / 4) + 10;
                    GUI.Label(new Rect(num11 + 113f, (float)num25, 200f, 22f), "Kill Trigger");
                    num25 += 35;
                    for (int j = 0; j < array6.Length; j++)
                    {
                        array6[j] = "kill" + array7[j];
                    }
                    for (int j = 0; j < array6.Length; j++)
                    {
                        int num22 = j % 4;
                        int num21 = j / 4;
                        GUI.DrawTexture(new Rect(num11 + 7.8f + 71.8f * (float)num22, (float)num25 + 114f * (float)num21, 64f, 64f), this.RCLoadTexture("p" + array6[j]));
                        if (GUI.Button(new Rect(num11 + 7.8f + 71.8f * (float)num22, (float)num25 + 69f + 114f * (float)num21, 64f, 30f), array7[j]))
                        {
                            flag = true;
                            flag2 = true;
                            GameObject original4 = (GameObject)FengGameManagerMKII.RCassets.Load(array6[j]);
                            this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(original4);
                            this.selectedObj.name = "racing," + array6[j];
                        }
                    }
                    num25 += 114 * (array6.Length / 4) + 10;
                    GUI.Label(new Rect(num11 + 95f, (float)num25, 200f, 22f), "Checkpoint Trigger");
                    num25 += 35;
                    for (int j = 0; j < array6.Length; j++)
                    {
                        array6[j] = "checkpoint" + array7[j];
                    }
                    for (int j = 0; j < array6.Length; j++)
                    {
                        int num22 = j % 4;
                        int num21 = j / 4;
                        GUI.DrawTexture(new Rect(num11 + 7.8f + 71.8f * (float)num22, (float)num25 + 114f * (float)num21, 64f, 64f), this.RCLoadTexture("p" + array6[j]));
                        if (GUI.Button(new Rect(num11 + 7.8f + 71.8f * (float)num22, (float)num25 + 69f + 114f * (float)num21, 64f, 30f), array7[j]))
                        {
                            flag = true;
                            flag2 = true;
                            GameObject original4 = (GameObject)FengGameManagerMKII.RCassets.Load(array6[j]);
                            this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(original4);
                            this.selectedObj.name = "racing," + array6[j];
                        }
                    }
                    GUI.EndScrollView();
                }
                else if ((int)FengGameManagerMKII.settings[64] == 106)
                {
                    GUI.Label(new Rect(num11 + 10f, 80f, 200f, 22f), "- Tree 2 designed by Ken P.", "Label");
                    GUI.Label(new Rect(num11 + 10f, 105f, 250f, 22f), "- Tower 2, House 5 designed by Matthew Santos", "Label");
                    GUI.Label(new Rect(num11 + 10f, 130f, 200f, 22f), "- Cannon retextured by Mika", "Label");
                    GUI.Label(new Rect(num11 + 10f, 155f, 200f, 22f), "- Arena 1,2,3 & 4 created by Gun", "Label");
                    GUI.Label(new Rect(num11 + 10f, 180f, 250f, 22f), "- Cannon Wall/Ground textured by Bellfox", "Label");
                    GUI.Label(new Rect(num11 + 10f, 205f, 250f, 120f), "- House 7 - 14, Statue1, Statue2, Wagon1, Wall 1, Wall 2, Wall 3, Wall 4, CannonWall, CannonGround, Tower5, Bridge1, Dummy1, Spike1 created by meecube", "Label");
                }
                if (flag && this.selectedObj != null)
                {
                    float num23;
                    if (!float.TryParse((string)FengGameManagerMKII.settings[70], out num23))
                    {
                        FengGameManagerMKII.settings[70] = "1";
                    }
                    if (!float.TryParse((string)FengGameManagerMKII.settings[71], out num23))
                    {
                        FengGameManagerMKII.settings[71] = "1";
                    }
                    if (!float.TryParse((string)FengGameManagerMKII.settings[72], out num23))
                    {
                        FengGameManagerMKII.settings[72] = "1";
                    }
                    if (!float.TryParse((string)FengGameManagerMKII.settings[79], out num23))
                    {
                        FengGameManagerMKII.settings[79] = "1";
                    }
                    if (!float.TryParse((string)FengGameManagerMKII.settings[80], out num23))
                    {
                        FengGameManagerMKII.settings[80] = "1";
                    }
                    if (!flag2)
                    {
                        float a = 1f;
                        if ((string)FengGameManagerMKII.settings[69] != "default")
                        {
                            if (((string)FengGameManagerMKII.settings[69]).StartsWith("transparent"))
                            {
                                float num26;
                                if (float.TryParse(((string)FengGameManagerMKII.settings[69]).Substring(11), out num26))
                                {
                                    a = num26;
                                }
                                Renderer[] componentsInChildren = this.selectedObj.GetComponentsInChildren<Renderer>();
                                for (int i = 0; i < componentsInChildren.Length; i++)
                                {
                                    Renderer renderer2 = componentsInChildren[i];
                                    renderer2.material = (Material)FengGameManagerMKII.RCassets.Load("transparent");
                                    renderer2.material.mainTextureScale = new Vector2(renderer2.material.mainTextureScale.x * Convert.ToSingle((string)FengGameManagerMKII.settings[79]), renderer2.material.mainTextureScale.y * Convert.ToSingle((string)FengGameManagerMKII.settings[80]));
                                }
                            }
                            else
                            {
                                Renderer[] componentsInChildren = this.selectedObj.GetComponentsInChildren<Renderer>();
                                for (int i = 0; i < componentsInChildren.Length; i++)
                                {
                                    Renderer renderer2 = componentsInChildren[i];
                                    if (!renderer2.name.Contains("Particle System") || !this.selectedObj.name.Contains("aot_supply"))
                                    {
                                        renderer2.material = (Material)FengGameManagerMKII.RCassets.Load((string)FengGameManagerMKII.settings[69]);
                                        renderer2.material.mainTextureScale = new Vector2(renderer2.material.mainTextureScale.x * Convert.ToSingle((string)FengGameManagerMKII.settings[79]), renderer2.material.mainTextureScale.y * Convert.ToSingle((string)FengGameManagerMKII.settings[80]));
                                    }
                                }
                            }
                        }
                        float num27 = 1f;
                        MeshFilter[] componentsInChildren2 = this.selectedObj.GetComponentsInChildren<MeshFilter>();
                        for (int i = 0; i < componentsInChildren2.Length; i++)
                        {
                            MeshFilter meshFilter = componentsInChildren2[i];
                            if (this.selectedObj.name.StartsWith("customb"))
                            {
                                if (num27 < meshFilter.mesh.bounds.size.y)
                                {
                                    num27 = meshFilter.mesh.bounds.size.y;
                                }
                            }
                            else if (num27 < meshFilter.mesh.bounds.size.z)
                            {
                                num27 = meshFilter.mesh.bounds.size.z;
                            }
                        }
                        float num28 = this.selectedObj.transform.localScale.x * Convert.ToSingle((string)FengGameManagerMKII.settings[70]);
                        num28 -= 0.001f;
                        float y3 = this.selectedObj.transform.localScale.y * Convert.ToSingle((string)FengGameManagerMKII.settings[71]);
                        float z2 = this.selectedObj.transform.localScale.z * Convert.ToSingle((string)FengGameManagerMKII.settings[72]);
                        this.selectedObj.transform.localScale = new Vector3(num28, y3, z2);
                        if ((int)FengGameManagerMKII.settings[76] == 1)
                        {
                            Color color = new Color((float)FengGameManagerMKII.settings[73], (float)FengGameManagerMKII.settings[74], (float)FengGameManagerMKII.settings[75], a);
                            componentsInChildren2 = this.selectedObj.GetComponentsInChildren<MeshFilter>();
                            for (int i = 0; i < componentsInChildren2.Length; i++)
                            {
                                MeshFilter meshFilter = componentsInChildren2[i];
                                Mesh mesh = meshFilter.mesh;
                                Color[] array4 = new Color[mesh.vertexCount];
                                for (int k = 0; k < mesh.vertexCount; k++)
                                {
                                    array4[k] = color;
                                }
                                mesh.colors = array4;
                            }
                        }
                        float num29 = this.selectedObj.transform.localScale.z;
                        if (this.selectedObj.name.Contains("boulder2") || this.selectedObj.name.Contains("boulder3") || this.selectedObj.name.Contains("field2"))
                        {
                            num29 *= 0.01f;
                        }
                        float num30 = 10f + num29 * num27 * 1.2f / 2f;
                        this.selectedObj.transform.position = new Vector3(Camera.main.transform.position.x + Camera.main.transform.forward.x * num30, Camera.main.transform.position.y + Camera.main.transform.forward.y * 10f, Camera.main.transform.position.z + Camera.main.transform.forward.z * num30);
                        this.selectedObj.transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
                        string text4 = this.selectedObj.name;
                        this.selectedObj.name = string.Concat(new string[]
					{
						text4,
						",",
						(string)FengGameManagerMKII.settings[69],
						",",
						(string)FengGameManagerMKII.settings[70],
						",",
						(string)FengGameManagerMKII.settings[71],
						",",
						(string)FengGameManagerMKII.settings[72],
						",",
						FengGameManagerMKII.settings[76].ToString(),
						",",
						((float)FengGameManagerMKII.settings[73]).ToString(),
						",",
						((float)FengGameManagerMKII.settings[74]).ToString(),
						",",
						((float)FengGameManagerMKII.settings[75]).ToString(),
						",",
						(string)FengGameManagerMKII.settings[79],
						",",
						(string)FengGameManagerMKII.settings[80]
					});
                        this.unloadAssetsEditor();
                    }
                    else if (this.selectedObj.name.StartsWith("misc"))
                    {
                        if (this.selectedObj.name.Contains("barrier") || this.selectedObj.name.Contains("region") || this.selectedObj.name.Contains("racing"))
                        {
                            float num27 = 1f;
                            float num28 = this.selectedObj.transform.localScale.x * Convert.ToSingle((string)FengGameManagerMKII.settings[70]);
                            num28 -= 0.001f;
                            float y3 = this.selectedObj.transform.localScale.y * Convert.ToSingle((string)FengGameManagerMKII.settings[71]);
                            float z2 = this.selectedObj.transform.localScale.z * Convert.ToSingle((string)FengGameManagerMKII.settings[72]);
                            this.selectedObj.transform.localScale = new Vector3(num28, y3, z2);
                            float num29 = this.selectedObj.transform.localScale.z;
                            float num30 = 10f + num29 * num27 * 1.2f / 2f;
                            this.selectedObj.transform.position = new Vector3(Camera.main.transform.position.x + Camera.main.transform.forward.x * num30, Camera.main.transform.position.y + Camera.main.transform.forward.y * 10f, Camera.main.transform.position.z + Camera.main.transform.forward.z * num30);
                            if (!this.selectedObj.name.Contains("region"))
                            {
                                this.selectedObj.transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
                            }
                            string text4 = this.selectedObj.name;
                            this.selectedObj.name = string.Concat(new string[]
						{
							text4,
							",",
							(string)FengGameManagerMKII.settings[70],
							",",
							(string)FengGameManagerMKII.settings[71],
							",",
							(string)FengGameManagerMKII.settings[72]
						});
                        }
                    }
                    else if (this.selectedObj.name.StartsWith("racing"))
                    {
                        float num27 = 1f;
                        float num28 = this.selectedObj.transform.localScale.x * Convert.ToSingle((string)FengGameManagerMKII.settings[70]);
                        num28 -= 0.001f;
                        float y3 = this.selectedObj.transform.localScale.y * Convert.ToSingle((string)FengGameManagerMKII.settings[71]);
                        float z2 = this.selectedObj.transform.localScale.z * Convert.ToSingle((string)FengGameManagerMKII.settings[72]);
                        this.selectedObj.transform.localScale = new Vector3(num28, y3, z2);
                        float num29 = this.selectedObj.transform.localScale.z;
                        float num30 = 10f + num29 * num27 * 1.2f / 2f;
                        this.selectedObj.transform.position = new Vector3(Camera.main.transform.position.x + Camera.main.transform.forward.x * num30, Camera.main.transform.position.y + Camera.main.transform.forward.y * 10f, Camera.main.transform.position.z + Camera.main.transform.forward.z * num30);
                        this.selectedObj.transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
                        string text4 = this.selectedObj.name;
                        this.selectedObj.name = string.Concat(new string[]
					{
						text4,
						",",
						(string)FengGameManagerMKII.settings[70],
						",",
						(string)FengGameManagerMKII.settings[71],
						",",
						(string)FengGameManagerMKII.settings[72]
					});
                    }
                    else
                    {
                        this.selectedObj.transform.position = new Vector3(Camera.main.transform.position.x + Camera.main.transform.forward.x * 10f, Camera.main.transform.position.y + Camera.main.transform.forward.y * 10f, Camera.main.transform.position.z + Camera.main.transform.forward.z * 10f);
                        this.selectedObj.transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
                    }
                    Screen.lockCursor = true;
                    GUI.FocusControl(null);
                }
            }
            else if (FengGameManagerMKII.instance.MenuOn)
            {
                this.Menu();
            }
            else if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
            {
                if (Time.timeScale <= 0.1f)
                {
                    GUILayout.BeginArea(new Rect(Screen.width / 2f - 100 ,Screen.height / 2f - 50,200,100),GUI.skin.box);
                    GUIStyle styleLabel = new GUIStyle(GUI.skin.label);
                    styleLabel.alignment = TextAnchor.MiddleCenter;
                     if (this.pauseWaitTime <= 3f)
                    {
                        GUILayout.Label( INC.la("unpaused_in") + "\n" +this.pauseWaitTime.ToString("F1") );
                    }
                    else
                    {
                        GUILayout.Label(INC.la("game_paused"));
                        if (PhotonNetwork.isMasterClient)
                        {
                            GUILayout.BeginHorizontal();
                            GUILayout.Label("/unpause");
                            if (GUILayout.Button("Unpause",GUILayout.Width(80f)))
                            {
                                base.photonView.RPC("pauseRPC", PhotonTargets.All, new object[] { false });
                                cext.mess(INC.la("in_paused_game"));
                            }
                            GUILayout.EndHorizontal();
                        }
                    }
                    GUILayout.EndArea();
                }
                else if (!FengGameManagerMKII.logicLoaded || !FengGameManagerMKII.customLevelLoaded)
                {
                    int length = (PhotonNetwork.player.currentLevel).Length;
                    int length2 = (PhotonNetwork.masterClient.currentLevel).Length;
                    float num7 = (float)Screen.width / 2f;
                    float num8 = (float)Screen.height / 2f;
                    GUI.backgroundColor = INC.gui_color;
                    GUI.Box(new Rect(num7 - 100, num8 - 65, 200, 130),INC.la("loading_cust_lvl") + "\n" + length.ToString() + "/" + length2.ToString());
                    float sss = (length * 100 / length2) * 2;

                    GUIStyle style = new GUIStyle(GUI.skin.label);
                    style.normal.background = Coltext.graytext;
                    GUI.Label(new Rect(num7 - 100, num8 - 5, 200, 25), "", style);
                    style.normal.background = Coltext.cyantext;
                    GUI.Label(new Rect(num7 - 100, num8 - 5, sss, 25), "", style);
                    this.retryTime += Time.deltaTime;
                    Screen.lockCursor = false;
                    Screen.showCursor = true;
                    if (GUI.Button(new Rect(num7 - 35f, num8 + 30f, 70f, 30f), INC.la("quit")))
                    {
                        PhotonNetwork.Disconnect();
                        Screen.lockCursor = false;
                        Screen.showCursor = true;
                        IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.STOP;
                        FengGameManagerMKII.instance.gameStart = false;
                         FengGameManagerMKII.instance.MenuOn = false;
                        this.DestroyAllExistingCloths();
                        UnityEngine.Object.Destroy(base.gameObject);
                        Application.LoadLevel("menu");
                    }
                }
            }
        }
    }
    public IEnumerator Playing()
    {
        int zero = 0;
        sizeImage = 30f;
        while (isPlayng)
        {
            yield return new WaitForSeconds(0.05f);
            if (sizeImage < 250f)
            {
                sizeImage = sizeImage + 8f;
            }
            else
            {
                zero = zero + 1;
                if (zero > 100)
                {
                    isPlayng = false;
                    sizeImage = 30f;
                    yield break;
                }
            }
            yield return null;
        }
        yield break;
    }
    void buttons()
    {
        GUIStyle buttons = new GUIStyle(GUI.skin.button);
        buttons.fixedHeight = 35f;
        string[] list32 = new string[] { INC.la("general_b"), INC.la("rebinds_b"), INC.la("skins_b"), INC.la("custom_b"), INC.la("abilities_b"), INC.la("players_b") };

        GUILayout.BeginArea(new Rect(Screen.width / 2 - 490, Screen.height / 2 - 255, 120, 400));
        for (int i = 0; i < list32.Length; i++)
        {
            if (i == mymenu)
            {
                buttons.normal = GUI.skin.button.onNormal;
        
            }
            else
            {
                buttons.normal = GUI.skin.button.normal;
            }
            if (GUILayout.Button(list32[i], buttons))
            {
                mymenu = i;
            }
        }
        GUILayout.EndArea();
    }
    public string Checkmod(PhotonPlayer player)
    {
        ExitGames.Client.Photon.Hashtable hash = player.customProperties;
        string Name = (hash[PhotonPlayerProperty.name].isString());
        if (player.SLB)
        {
            return "SLB";
        }
        if (player.CM)
        {
            return "Cyan_mod v." + player.version;
        }
        if (player.RS)
        {
            return "DEATH/RedSkies mod " + player.versionRS;
        }
        if (hash != null && hash.Count > 0)
        {
            if (hash["CyanModNew"] != null && hash["CyanMod"] != null)
            {
                return "Cyan_mod v." + (hash["CyanModNew"].isString());
            }
            else if ((hash["CyanMod"] != null))
            {
                return "Cyan_mod(old)";
            }
            if (Name.StartsWith("[6c1dcb]"))
            {
                return "Galaxy";
            }
            if (Name.StartsWith("Remus") || Name.StartsWith("[77c2a7]") || Name.StartsWith("Kurome"))
            {
                return "KUROI SORA/AkaNime";
            }
            if (Name.StartsWith("[FF0000]Vivid-Assassin") || Name.StartsWith("[ffd700]Vivid-Assassin") || Name.StartsWith("[00ff00]Tokyo Ghoul"))
            {
                return "Parrot's mod";
            }
            if (Name.StartsWith("[ffd700]Hyper-MegaCannon"))
            {
                return "Hyper-MegaCannon mod";
            }
            if (hash["KageNoKishi"] != null)
            {
                return "KageNoKishi mod<color=red>{Danger!}</color>";
            }
            if (hash["DT"] != null)
            {
                return "DT/Arche mod";
            }
            if (hash["JakesMod"] != null)
            {
                return "JakesMod";
            }
            if (hash["Alpha_X"] != null)
            {
                return "Alpha_X mod";
            }
            if (hash["Robbie'sMod"] != null)
            {
                return "Robbie's Mod";
            }
            if (hash["not null"] != null)
            {
                return "ЕС mod";
            }
            if (hash["Destroy"] != null)
            {
                return "Destroy mod";
            }
            if (hash["Alpha"] != null)
            {
                return "Alpha mod";
            }
            if (hash.ContainsKey("INS"))
            {
                return "INSANE mod";
            }
            if (hash["SRC"] != null)
            {
                return "SRC mod";
            }
            if (hash.ContainsKey("Raoh"))
            {
                return "RaohMod";
            }
            if (hash["BRC"] != null)
            {
                return "BRC mod";
            }
            if (hash["BRM"] != null)
            {
                return "BRM mod";
            }
            if (hash["woahWtf"] != null)
            {
                return "woahWtf mod";
            }
            if (hash["OhSoShO"] != null)
            {
                return "OhSoShO mod <color=red>{Danger!}</color>";
            }
            if (hash["Nathan"] != null)
            {
                return "Nathan mod";
            }
            if (hash["Arch"] != null)
            {
                return "Arch mod";
            }
            if (hash["NRC"] != null)
            {
                return "NRC mod";
            }
            if (hash["Doge"] != null)
            {
                return "Doge mod";
            }
            if (hash["GHOST"] != null)
            {
                return "GHOST mod";
            }
            if (hash["CMod_CModVersion"] != null)
            {
                byte[] Property = (byte[])hash["CMod_CModVersion"];
                BuildType build = BuildType.Other; ;
                string dsf = string.Empty;
                if (Property.Length == 3)
                {
                    if (Enum.GetValues(typeof(BuildType)).Cast<byte>().Contains(Property[0]))
                    {
                        build = (BuildType)Property[0];
                    }
                    dsf = Property[1].ToString();
                    dsf = dsf + "." + Property[2].ToString();
                }
                return "CMod v." + dsf + " " + "(" + build.ToString() + ")";
            }
            if (hash["RCteams"] != null || hash["Rage"] != null)
            {
                return "Rage mod";
            }
            if (hash["DEATH"] != null || hash["redskies"] != null || hash["REDSKIES"] != null || hash["REDSKIESV2"] != null)
            {
                return "DEATH/RedSkies mod";
            }
        }
        if (player.RC)
        {
            return "RC mod";
        }
        else if (hash["RCteam"] != null)
        {
            return "Anti-Cheat";
        }
        return "Vanilla/Other";
    }
  
    void skins_mototd()
    {
        GUIStyle stylebott43 = new GUIStyle(GUI.skin.button);

        GUILayout.BeginArea(new Rect(0,0, 400, 450));
        GUILayout.Space(1f);
        GUILayout.BeginHorizontal();
        GUILayout.Space(8f);
        string[] listskinswwe = new string[] { INC.la("human_s"), INC.la("titan_s"), INC.la("lvl_s") };
        for (int i = 0; i < listskinswwe.Length; i++)
        {
            if (skinsott == i)
            {
                stylebott43.normal = GUI.skin.button.onNormal;
            }
            else
            {
                stylebott43.normal = GUI.skin.button.normal;
            }
            if (GUILayout.Button(listskinswwe[i], stylebott43))
            {
                skinsott = i;
            }
        }
        GUILayout.EndHorizontal();
        if (skinsott == 0)
        {
            humanskins();
        }
        else
        {
            vectors2[10] = GUILayout.BeginScrollView(vectors2[10]);
            if (skinsott == 1)
            {
                titanskin();
            }
            else if (skinsott == 2)
            {
                levelskin();
            }
            GUILayout.EndScrollView();
        }
        GUILayout.EndArea();
    }
   
    void newpanelskins()
    {
        GUILayout.BeginArea(new Rect(405, 0, 400, 450));
        GUILayout.Space(5f);
        vectors2[11] = GUILayout.BeginScrollView(vectors2[11]);
        GUILayout.Box(INC.la("new_panel_skins"));
        if (texture_skin_view != null)
        {
            texture_skin_view.show(370);
        }
        else
        {
            GUICyan.OnToogleCyan(INC.la("show_object_hero"), 385, 1, 0, 60);
            if ((int)FengGameManagerMKII.settings[385] == 1)
            {
                string[] str = new string[] { "none", "neko", "rabbit", "horn" };
                FengGameManagerMKII.settings[386] = GUILayout.SelectionGrid((int)FengGameManagerMKII.settings[386], str, 4);
                if ((int)FengGameManagerMKII.settings[386] != 0)
                {
                  
                    GUIStyle style = new GUIStyle(GUI.skin.label);
                    style.normal.textColor = (Color)FengGameManagerMKII.settings[389];
                    FengGameManagerMKII.settings[389] = cext.color_toGUI((Color)FengGameManagerMKII.settings[389],style, INC.la("colored_object_p0"));
                }
                string[] str2 = new string[] { "none", "devil", "angel", "cat", "wings", "wings22", "wings23", "wings24", "wings25", "wings26", "wings27", "wings28", "wings29", "wings30","custom" };
                FengGameManagerMKII.settings[387] = GUILayout.SelectionGrid((int)FengGameManagerMKII.settings[387], str2, 4);
                if ((int)FengGameManagerMKII.settings[387] != 0 && (int)FengGameManagerMKII.settings[387] < 4)
                {
                    GUIStyle style = new GUIStyle(GUI.skin.label);
                    style.normal.textColor = (Color)FengGameManagerMKII.settings[390];
                    FengGameManagerMKII.settings[390] = cext.color_toGUI((Color)FengGameManagerMKII.settings[390],style, INC.la("colored_object_p0"));
                }
                if ((int)FengGameManagerMKII.settings[387] == 14)
                {
                    FengGameManagerMKII.settings[387] = 13;
                    GUILayout.Label(INC.la("left_url_wings"));
                    FengGameManagerMKII.settings[394] = GUILayout.TextField((string)FengGameManagerMKII.settings[394]);
                    GUILayout.Label(INC.la("right_url_wings"));
                    FengGameManagerMKII.settings[395] = GUILayout.TextField((string)FengGameManagerMKII.settings[395]);
                    GUICyan.OnToogleCyan(INC.la("advs_wings"), 396, 1, 0, 50f);
                    if ((int)FengGameManagerMKII.settings[396] == 1)
                    {
                        GUIStyle stylew = new GUIStyle(GUI.skin.label);
                        stylew.normal.textColor = (Color)FengGameManagerMKII.settings[397];
                        FengGameManagerMKII.settings[397] = cext.color_toGUI((Color)FengGameManagerMKII.settings[397], stylew,INC.la("advs_colored"),false);
                    }
                }
                string[] str3 = new string[] { "none", "arch_Objects", "bat_1", "bat_2", "Butterfly", "Dove_Objects", "Heart_3", "Heart_2", "Heart_1", "Skull_2", "Skull_1"};
                FengGameManagerMKII.settings[388] = GUILayout.SelectionGrid((int)FengGameManagerMKII.settings[388], str3, 4);
                if ((int)FengGameManagerMKII.settings[388] != 0)
                {
                    GUIStyle style = new GUIStyle(GUI.skin.label);
                    style.normal.textColor = (Color)FengGameManagerMKII.settings[391];
                    FengGameManagerMKII.settings[391] = cext.color_toGUI((Color)FengGameManagerMKII.settings[391], style, INC.la("colored_object_p0"));
                }
            }
        }
        GUILayout.EndScrollView();

        GUILayout.EndArea();
    }

    public static CyanSkin myCyanSkin;
    public string checkNameskin()
    {
        int num34243 = 1;
        string namew = "Set_" + num34243.ToString();
        List<string> list_name = new List<string>();
        List<CyanSkin> nlist = new List<CyanSkin>(INC.cSkins.OrderBy<CyanSkin, CyanSkin>(x => x, new FunctionComparer<CyanSkin>((x, y) => string.Compare(x.name, y.name))));
        foreach (CyanSkin cs in nlist)
        {
            list_name.Add(cs.name);
        }
        while (list_name.Contains(namew))
        {
            num34243++;
            namew = "Set_" + num34243.ToString(); ;
        }
        return namew;
    }
    string onFirstskin()
    {
        bool flag = true;
        string dfdsf = string.Empty;
        foreach (CyanSkin skin in INC.cSkins)
        {
            if (flag)
            {
                dfdsf = skin.name;
                flag = false;
            }
        }
        return dfdsf;

    }
  
    void humanskins()
    {
        GUIStyle stylewwss = new GUIStyle(GUI.skin.button);
        stylewwss.alignment = TextAnchor.MiddleLeft;

        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();
        if (GUILayout.Button("sort"))
        {
            INC.cSkins = new List<CyanSkin>(INC.cSkins.OrderBy<CyanSkin, CyanSkin>(x => x, new FunctionComparer<CyanSkin>((x, y) => string.Compare(x.name.Trim(), y.name.Trim()))));
        }
        if (GUILayout.Button("<b>+</b>"))
        {
            CyanSkin skin = new CyanSkin();
            skin.name = checkNameskin();
            INC.cSkins.Add(skin);
            FengGameManagerMKII.myCyanSkin = skin;
            FengGameManagerMKII.settings[273] = skin.name;
        }
        vectors2[15] = GUILayout.BeginScrollView(vectors2[15], GUILayout.Width(80f));
        foreach (CyanSkin skin in INC.cSkins)
        {
            if (skin == FengGameManagerMKII.myCyanSkin)
            {
                stylewwss.normal = GUI.skin.button.onNormal;
             
            }
            else
            {
                stylewwss.normal = GUI.skin.button.normal; 
               
            }
            if (GUILayout.Button(skin.name, stylewwss))
            {
                FengGameManagerMKII.myCyanSkin = skin;
                FengGameManagerMKII.settings[273] = skin.name;
            }
        }

        GUILayout.EndScrollView();
        GUILayout.EndVertical();
        GUILayout.BeginVertical();
        vectors2[14] = GUILayout.BeginScrollView(vectors2[14], GUILayout.Height(410), GUILayout.Width(300));
        if (FengGameManagerMKII.myCyanSkin != null)
        {
            float labelwidth2 = 275;
            GUICyan.OnToogleCyan(INC.la("human_skin_mode_on"), 0, 1, 0, 50);

            GUILayout.BeginHorizontal();
            if (!FengGameManagerMKII.myCyanSkin.isRead)
            {
                if (INC.cSkins.Count > 1)
                {
                    if (GUILayout.Button(INC.la("dell_skin")))
                    {
                        for (int d = 0; d < INC.cSkins.Count; d++)
                        {
                            CyanSkin sskin = INC.cSkins[d];
                            if (sskin == FengGameManagerMKII.myCyanSkin)
                            {
                                INC.cSkins.Remove(sskin);
                                FengGameManagerMKII.myCyanSkin = INC.cSkins[d - 1];
                                FengGameManagerMKII.settings[273] = FengGameManagerMKII.myCyanSkin.name;
                            }
                        }
                    }
                    if (GUILayout.Button("✍"))
                    {
                        FengGameManagerMKII.myCyanSkin.isRead = true;
                    }
                }
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                {
                    if (GUILayout.Button(INC.la("send_skin")))
                    {
                        mymenu = 11;
                    }
                }
            }
            else
            {
                FengGameManagerMKII.myCyanSkin.name = GUILayout.TextField(FengGameManagerMKII.myCyanSkin.name);
                if (GUILayout.Button("ok", GUILayout.Width(40f)))
                {
                    FengGameManagerMKII.myCyanSkin.isRead = false;
                }
            }
            GUILayout.EndHorizontal();

            button_view_texture(INC.la("s_horse"), myCyanSkin.horse);
            myCyanSkin.horse = GUILayout.TextField(myCyanSkin.horse, GUILayout.Width(labelwidth2));
            button_view_texture(INC.la("s_hair_model"), myCyanSkin.hair);
            myCyanSkin.hair = GUILayout.TextField(myCyanSkin.hair, GUILayout.Width(labelwidth2));
            button_view_texture(INC.la("s_eyes"), myCyanSkin.eyes);
            myCyanSkin.eyes = GUILayout.TextField(myCyanSkin.eyes, GUILayout.Width(labelwidth2));
            button_view_texture(INC.la("s_glass"), myCyanSkin.glass);
            myCyanSkin.glass = GUILayout.TextField(myCyanSkin.glass, GUILayout.Width(labelwidth2));
            button_view_texture(INC.la("s_face"), myCyanSkin.face);
            myCyanSkin.face = GUILayout.TextField(myCyanSkin.face, GUILayout.Width(labelwidth2));
            button_view_texture(INC.la("s_skin"), myCyanSkin.skin);
            myCyanSkin.skin = GUILayout.TextField(myCyanSkin.skin, GUILayout.Width(labelwidth2));
            button_view_texture(INC.la("s_hoodie_model"), myCyanSkin.hoodie);
            myCyanSkin.hoodie = GUILayout.TextField(myCyanSkin.hoodie, GUILayout.Width(labelwidth2));
            button_view_texture(INC.la("s_costume_model"), myCyanSkin.costume);
            myCyanSkin.costume = GUILayout.TextField(myCyanSkin.costume, GUILayout.Width(labelwidth2));
            button_view_texture(INC.la("s_logo_and_cape"), myCyanSkin.logo_and_cape);
            myCyanSkin.logo_and_cape = GUILayout.TextField(myCyanSkin.logo_and_cape, GUILayout.Width(labelwidth2));
            button_view_texture(INC.la("s_3dmg_model_right_gun"), myCyanSkin.dmg_right);
            myCyanSkin.dmg_right = GUILayout.TextField(myCyanSkin.dmg_right, GUILayout.Width(labelwidth2));
            button_view_texture(INC.la("s_3dmg_model_left_gun"), myCyanSkin.dmg_left);
            myCyanSkin.dmg_left = GUILayout.TextField(myCyanSkin.dmg_left, GUILayout.Width(labelwidth2));
            button_view_texture(INC.la("s_gas"), myCyanSkin.gas);
            myCyanSkin.gas = GUILayout.TextField(myCyanSkin.gas, GUILayout.Width(labelwidth2));
            button_view_texture(INC.la("s_weapon_trail"), myCyanSkin.weapon_trail);
            myCyanSkin.weapon_trail = GUILayout.TextField(myCyanSkin.weapon_trail, GUILayout.Width(labelwidth2));
        }
        GUILayout.EndScrollView();
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
    }
    void button_view_texture(string label, string url)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label(label);
        if (url.Trim() != string.Empty)
        {
            if (GUILayout.Button(">.>", GUILayout.Width(40f)))
            {
                if (texture_skin_view != null)
                {
                    Destroy(texture_skin_view);
                }
                texture_skin_view = new GameObject().AddComponent<LoadTextureSkin>();
                texture_skin_view.url = url;
            }
        }
        GUILayout.EndHorizontal();
    }
    void titanskin()
    {
        float labelwidth = 50;
        GUICyan.OnToogleCyan(INC.la("titan_skin_mode"), 1, 1, 0, labelwidth);
        GUICyan.OnToogleCyan(INC.la("randomized_pairs"), 32, 1, 0, labelwidth);
        GUILayout.Label(INC.la("titan_hair"));
        GUILayout.BeginHorizontal();
        FengGameManagerMKII.settings[21] = GUILayout.TextField((string)FengGameManagerMKII.settings[21]);
        if (GUILayout.Button(this.hairtype((int)FengGameManagerMKII.settings[16])))
        {
            int num33 = 16;
            int num34 = (int)FengGameManagerMKII.settings[16];
            if (num34 >= 9)
            {
                num34 = -1;
            }
            else
            {
                num34++;
            }
            FengGameManagerMKII.settings[num33] = num34;
        }

        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        FengGameManagerMKII.settings[22] = GUILayout.TextField((string)FengGameManagerMKII.settings[22]);
        if (GUILayout.Button(this.hairtype((int)FengGameManagerMKII.settings[17])))
        {
            int num33 = 17;
            int num34 = (int)FengGameManagerMKII.settings[17];
            if (num34 >= 9)
            {
                num34 = -1;
            }
            else
            {
                num34++;
            }
            FengGameManagerMKII.settings[num33] = num34;
        }

        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        FengGameManagerMKII.settings[23] = GUILayout.TextField((string)FengGameManagerMKII.settings[23]);
        if (GUILayout.Button(this.hairtype((int)FengGameManagerMKII.settings[18])))
        {
            int num33 = 18;
            int num34 = (int)FengGameManagerMKII.settings[18];
            if (num34 >= 9)
            {
                num34 = -1;
            }
            else
            {
                num34++;
            }
            FengGameManagerMKII.settings[num33] = num34;
        }
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        FengGameManagerMKII.settings[24] = GUILayout.TextField((string)FengGameManagerMKII.settings[24]);
        if (GUILayout.Button(this.hairtype((int)FengGameManagerMKII.settings[19])))
        {
            int num33 = 19;
            int num34 = (int)FengGameManagerMKII.settings[19];
            if (num34 >= 9)
            {
                num34 = -1;
            }
            else
            {
                num34++;
            }
            FengGameManagerMKII.settings[num33] = num34;
        }
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        FengGameManagerMKII.settings[25] = GUILayout.TextField((string)FengGameManagerMKII.settings[25]);
        if (GUILayout.Button(this.hairtype((int)FengGameManagerMKII.settings[20])))
        {
            int num33 = 20;
            int num34 = (int)FengGameManagerMKII.settings[20];
            if (num34 >= 9)
            {
                num34 = -1;
            }
            else
            {
                num34++;
            }
            FengGameManagerMKII.settings[num33] = num34;
        }
        GUILayout.EndHorizontal();
        GUILayout.Label(INC.la("titan_eye"));
        FengGameManagerMKII.settings[26] = GUILayout.TextField((string)FengGameManagerMKII.settings[26]);
        FengGameManagerMKII.settings[27] = GUILayout.TextField((string)FengGameManagerMKII.settings[27]);
        FengGameManagerMKII.settings[28] = GUILayout.TextField((string)FengGameManagerMKII.settings[28]);
        FengGameManagerMKII.settings[29] = GUILayout.TextField((string)FengGameManagerMKII.settings[29]);
        FengGameManagerMKII.settings[30] = GUILayout.TextField((string)FengGameManagerMKII.settings[30]);
        GUILayout.Label(INC.la("titan_body"));
        FengGameManagerMKII.settings[86] = GUILayout.TextField((string)FengGameManagerMKII.settings[86]);
        FengGameManagerMKII.settings[87] = GUILayout.TextField((string)FengGameManagerMKII.settings[87]);
        FengGameManagerMKII.settings[88] = GUILayout.TextField((string)FengGameManagerMKII.settings[88]);
        FengGameManagerMKII.settings[89] = GUILayout.TextField((string)FengGameManagerMKII.settings[89]);
        FengGameManagerMKII.settings[90] = GUILayout.TextField((string)FengGameManagerMKII.settings[90]);
        GUILayout.Label(INC.la("eren"));
        FengGameManagerMKII.settings[65] = GUILayout.TextField((string)FengGameManagerMKII.settings[65]);
        GUILayout.Label(INC.la("annie"));
        FengGameManagerMKII.settings[66] = GUILayout.TextField((string)FengGameManagerMKII.settings[66]);
        GUILayout.Label(INC.la("colossal"));
        FengGameManagerMKII.settings[67] = GUILayout.TextField((string)FengGameManagerMKII.settings[67]);
    }
    void levelskin()
    {
        float labelwidth = 50;
        GUICyan.OnToogleCyan(INC.la("level_skin_mode"), 2, 1, 0, labelwidth);
        GUILayout.BeginHorizontal();
        string[] str43 = new string[] { INC.la("forest_skins"), INC.la("city_skins") };
        FengGameManagerMKII.settings[188] = GUILayout.SelectionGrid((int)FengGameManagerMKII.settings[188], str43, 2);
        GUILayout.EndHorizontal();

        GUICyan.OnToogleCyan(INC.la("randomized_pairs"), 50, 1, 0, labelwidth);
        if ((int)FengGameManagerMKII.settings[188] == 0)
        {
            GUILayout.Label(INC.la("ground"));
            FengGameManagerMKII.settings[49] = GUILayout.TextField((string)FengGameManagerMKII.settings[49]);
            GUILayout.Label(INC.la("forest_trunks"));
            FengGameManagerMKII.settings[33] = GUILayout.TextField((string)FengGameManagerMKII.settings[33]);
            FengGameManagerMKII.settings[34] = GUILayout.TextField((string)FengGameManagerMKII.settings[34]);
            FengGameManagerMKII.settings[35] = GUILayout.TextField((string)FengGameManagerMKII.settings[35]);
            FengGameManagerMKII.settings[36] = GUILayout.TextField((string)FengGameManagerMKII.settings[36]);
            FengGameManagerMKII.settings[37] = GUILayout.TextField((string)FengGameManagerMKII.settings[37]);
            FengGameManagerMKII.settings[38] = GUILayout.TextField((string)FengGameManagerMKII.settings[38]);
            FengGameManagerMKII.settings[39] = GUILayout.TextField((string)FengGameManagerMKII.settings[39]);
            FengGameManagerMKII.settings[40] = GUILayout.TextField((string)FengGameManagerMKII.settings[40]);
            GUILayout.Label(INC.la("forest_leaves"));
            FengGameManagerMKII.settings[41] = GUILayout.TextField((string)FengGameManagerMKII.settings[41]);
            FengGameManagerMKII.settings[42] = GUILayout.TextField((string)FengGameManagerMKII.settings[42]);
            FengGameManagerMKII.settings[43] = GUILayout.TextField((string)FengGameManagerMKII.settings[43]);
            FengGameManagerMKII.settings[44] = GUILayout.TextField((string)FengGameManagerMKII.settings[44]);
            FengGameManagerMKII.settings[45] = GUILayout.TextField((string)FengGameManagerMKII.settings[45]);
            FengGameManagerMKII.settings[46] = GUILayout.TextField((string)FengGameManagerMKII.settings[46]);
            FengGameManagerMKII.settings[47] = GUILayout.TextField((string)FengGameManagerMKII.settings[47]);
            FengGameManagerMKII.settings[48] = GUILayout.TextField((string)FengGameManagerMKII.settings[48]);
            GUILayout.Label(INC.la("skybox_front"));
            FengGameManagerMKII.settings[163] = GUILayout.TextField((string)FengGameManagerMKII.settings[163]);
            GUILayout.Label(INC.la("skybox_back"));
            FengGameManagerMKII.settings[164] = GUILayout.TextField((string)FengGameManagerMKII.settings[164]);
            GUILayout.Label(INC.la("skybox_left"));
            FengGameManagerMKII.settings[165] = GUILayout.TextField((string)FengGameManagerMKII.settings[165]);
            GUILayout.Label(INC.la("skybox_right"));
            FengGameManagerMKII.settings[166] = GUILayout.TextField((string)FengGameManagerMKII.settings[166]);
            GUILayout.Label(INC.la("skybox_up"));
            FengGameManagerMKII.settings[167] = GUILayout.TextField((string)FengGameManagerMKII.settings[167]);
            GUILayout.Label(INC.la("skybox_down"));
            FengGameManagerMKII.settings[168] = GUILayout.TextField((string)FengGameManagerMKII.settings[168]);
        }
        if ((int)FengGameManagerMKII.settings[188] == 1)
        {
            GUILayout.Label(INC.la("ground"));
            FengGameManagerMKII.settings[59] = GUILayout.TextField((string)FengGameManagerMKII.settings[59]);
            GUILayout.Label(INC.la("wall"));
            FengGameManagerMKII.settings[60] = GUILayout.TextField((string)FengGameManagerMKII.settings[60]);
            GUILayout.Label(INC.la("gate"));
            FengGameManagerMKII.settings[61] = GUILayout.TextField((string)FengGameManagerMKII.settings[61]);
            GUILayout.Label(INC.la("houses"));
            FengGameManagerMKII.settings[51] = GUILayout.TextField((string)FengGameManagerMKII.settings[51]);
            FengGameManagerMKII.settings[52] = GUILayout.TextField((string)FengGameManagerMKII.settings[52]);
            FengGameManagerMKII.settings[53] = GUILayout.TextField((string)FengGameManagerMKII.settings[53]);
            FengGameManagerMKII.settings[54] = GUILayout.TextField((string)FengGameManagerMKII.settings[54]);
            FengGameManagerMKII.settings[55] = GUILayout.TextField((string)FengGameManagerMKII.settings[55]);
            FengGameManagerMKII.settings[56] = GUILayout.TextField((string)FengGameManagerMKII.settings[56]);
            FengGameManagerMKII.settings[57] = GUILayout.TextField((string)FengGameManagerMKII.settings[57]);
            FengGameManagerMKII.settings[58] = GUILayout.TextField((string)FengGameManagerMKII.settings[58]);
            GUILayout.Label(INC.la("skybox_front"));
            FengGameManagerMKII.settings[169] = GUILayout.TextField((string)FengGameManagerMKII.settings[169]);
            GUILayout.Label(INC.la("skybox_back"));
            FengGameManagerMKII.settings[170] = GUILayout.TextField((string)FengGameManagerMKII.settings[170]);
            GUILayout.Label(INC.la("skybox_left"));
            FengGameManagerMKII.settings[171] = GUILayout.TextField((string)FengGameManagerMKII.settings[171]);
            GUILayout.Label(INC.la("skybox_right"));
            FengGameManagerMKII.settings[172] = GUILayout.TextField((string)FengGameManagerMKII.settings[172]);
            GUILayout.Label(INC.la("skybox_up"));
            FengGameManagerMKII.settings[173] = GUILayout.TextField((string)FengGameManagerMKII.settings[173]);
            GUILayout.Label(INC.la("skybox_down"));
            FengGameManagerMKII.settings[174] = GUILayout.TextField((string)FengGameManagerMKII.settings[174]);
        }
    }
    bool advenced_setting_grafix = false;
    void grafics()
    {
        float labelwidth = 50;
        float textFiled = 80f;
        GUILayout.BeginArea(new Rect(0, 0, 280, 450));
        GUILayout.Space(5f);
        vectors2[5] = GUILayout.BeginScrollView(vectors2[5]);

        GUILayout.Box(INC.la("graphics_set"));
        GUILayout.Label(INC.la("resolution"));
        GUILayout.BeginHorizontal();
        GUILayout.Label("W:",GUILayout.Width(25f));  Resolution.x = Convert.ToSingle(GUILayout.TextField(Resolution.x.ToString(), GUILayout.Width(50f)));
        GUILayout.Label("H:", GUILayout.Width(25f)); Resolution.y = Convert.ToSingle(GUILayout.TextField(Resolution.y.ToString(), GUILayout.Width(50f)));
        if (GUILayout.Button(INC.la("chage_resolution")))
        {
            if ((int)Resolution.x < 900)
            {
                Resolution.x = 900;
            }
            if ((int)Resolution.x < 600)
            {
                Resolution.x = 600;
            }
            Screen.SetResolution((int)Resolution.x, (int)Resolution.y, Screen.fullScreen);
        }
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label(INC.la("is_window"));
        GUIStyle stylss = new GUIStyle(GUI.skin.button);
        string sr332 = INC.la("Off");
        if (!Screen.fullScreen)
        {
            stylss.normal = GUI.skin.button.onNormal;
            sr332 = INC.la("On");
        }
        if (GUILayout.Button(sr332, stylss, GUILayout.Width(labelwidth)))
        {
            Screen.fullScreen = !Screen.fullScreen;
            IN_GAME_MAIN_CAMERA.instance.needSetHUD = true;
        }
        GUILayout.EndHorizontal();
       
        GUICyan.OnToogleCyan(INC.la("dis_custom_gas"), 15, 1, 0, labelwidth);
        GUICyan.OnToogleCyan(INC.la("dis_weapon_trail"), 92, 1, 0, labelwidth);
        if ((int)FengGameManagerMKII.settings[92] == 0)
        {
            GUICyan.OnToogleCyan(INC.la("infinite_wp"), 360, 1, 0, labelwidth);
        }
        GUICyan.OnToogleCyan(INC.la("dis_wind_effect"), 93, 1, 0, labelwidth);
        GUICyan.OnToogleCyan(INC.la("enable_vsync"), 183, 1, 0, labelwidth);
        GUICyan.OnToogleCyan(INC.la("fog_mode"), 416, 1, 0, labelwidth);
        GUICyan.OnTextFileCyan(INC.la("fps_cap_0"), 184, textFiled);
        GUILayout.Label(INC.la("fixed_fov_mode") + ((int)FengGameManagerMKII.settings[418] != 0 ? "[" + ((float)FengGameManagerMKII.settings[419]).ToString("F") + "]" : ""));
        FengGameManagerMKII.settings[418] = GUILayout.SelectionGrid((int)FengGameManagerMKII.settings[418],new string[]{INC.la("off"),INC.la("fixed_fov"),INC.la("vol_fov")},3);
        if ((int)FengGameManagerMKII.settings[418] != 0)
        {
            FengGameManagerMKII.settings[419] = GUILayout.HorizontalSlider((float)FengGameManagerMKII.settings[419], 40, 140);
        }
        GUILayout.Label(INC.la("overall_quality") + "[" + cuality_settings + "]");

        int setting_q_local = cuality_settings;
        cuality_settings = (int)GUILayout.HorizontalSlider((float)cuality_settings, 0, 5);
        if (setting_q_local != cuality_settings)
        {
            QualitySettings.SetQualityLevel(cuality_settings);
            if (cuality_settings >= 5)
            {
              
                QualitySettings.masterTextureLimit = 0;
                FengGameManagerMKII.settings[403] = 2;//blend_weights
                FengGameManagerMKII.settings[402] = 2;//anisotropic_filter
                FengGameManagerMKII.settings[401] = 0;//shadow_projection
                FengGameManagerMKII.settings[414] = 4;//shadow_quality
                FengGameManagerMKII.settings[415] = 2000f; //shadow_disance
                FengGameManagerMKII.settings[413] = 120f; //Lod_lvl
                FengGameManagerMKII.settings[417] = 2000f; //distance
                FengGameManagerMKII.settings[400] = 1;//blur
            }
            else if (cuality_settings >= 4)
            {
                FengGameManagerMKII.settings[400] = 0;//blur
                QualitySettings.masterTextureLimit = 0;
                FengGameManagerMKII.settings[403] = 2;//blend_weights
                FengGameManagerMKII.settings[402] = 2;//anisotropic_filter
                FengGameManagerMKII.settings[401] = 0;//shadow_projection
                FengGameManagerMKII.settings[414] = 4;//shadow_quality
                FengGameManagerMKII.settings[415] = 1500f; //shadow_disance
                FengGameManagerMKII.settings[413] = 90f; //Lod_lvl
                FengGameManagerMKII.settings[417] = 1600f; //distance
            }
            else if (cuality_settings >= 3)
            {
                FengGameManagerMKII.settings[400] = 0;//blur
                QualitySettings.masterTextureLimit = 0;
                FengGameManagerMKII.settings[403] = 1;//blend_weights
                FengGameManagerMKII.settings[402] = 1;//anisotropic_filter
                FengGameManagerMKII.settings[401] = 0;//shadow_projection
                FengGameManagerMKII.settings[414] = 3;//shadow_quality
                FengGameManagerMKII.settings[415] = 600f; //shadow_disance
                FengGameManagerMKII.settings[413] = 60f; //Lod_lvl
                FengGameManagerMKII.settings[417] = 1500f; //distance
            }
            else if (cuality_settings >= 2)
            {
                FengGameManagerMKII.settings[400] = 0;//blur
                QualitySettings.masterTextureLimit = 1;
                FengGameManagerMKII.settings[403] = 1;//blend_weights
                FengGameManagerMKII.settings[402] = 1;//anisotropic_filter
                FengGameManagerMKII.settings[401] = 0;//shadow_projection
                FengGameManagerMKII.settings[414] = 2;//shadow_quality
                FengGameManagerMKII.settings[415] = 300f; //shadow_disance
                FengGameManagerMKII.settings[413] = 30f; //Lod_lvl
                FengGameManagerMKII.settings[417] = 1500f; //distance
            }
            else
            {
                FengGameManagerMKII.settings[400] = 0;//blur
                QualitySettings.masterTextureLimit = 2;
                FengGameManagerMKII.settings[403] = 0;//blend_weights
                FengGameManagerMKII.settings[402] = 0;//anisotropic_filter
                FengGameManagerMKII.settings[401] = 0;//shadow_projection
                FengGameManagerMKII.settings[414] = 1;//shadow_quality
                FengGameManagerMKII.settings[415] = 0f; //shadow_disance
                FengGameManagerMKII.settings[413] = 0f; //Lod_lvl
                FengGameManagerMKII.settings[417] = 1000f; //distance
            }
        }
      

        if (GUILayout.Button(INC.la("advenced_setings")))
        {
            advenced_setting_grafix = !advenced_setting_grafix;
        }
        if (advenced_setting_grafix)
        {
            GUILayout.Label(INC.la("texture_quality"));
            int sss = QualitySettings.masterTextureLimit;
            string[] str232 = new string[] { INC.la("high_texture"), INC.la("medium_texture"), INC.la("low_texture") };
            QualitySettings.masterTextureLimit = GUILayout.SelectionGrid(QualitySettings.masterTextureLimit, str232, 3);
            if (sss != QualitySettings.masterTextureLimit)
            {
                linkHash[0].Clear();
                linkHash[1].Clear();
                linkHash[2].Clear();

            }

            GUILayout.Label(INC.la("blend_weights"));
            FengGameManagerMKII.settings[403] = GUILayout.SelectionGrid((int)FengGameManagerMKII.settings[403], new string[] { "0", "2", "4" }, 3);
            GUILayout.Label(INC.la("anisotropic_filter"));
            FengGameManagerMKII.settings[402] = GUILayout.SelectionGrid((int)FengGameManagerMKII.settings[402], new string[] { INC.la("dis_niso"), INC.la("enabl_niso"), INC.la("force_enabl_niso") }, 3);
            GUILayout.Label(INC.la("shadow_projection"));
            FengGameManagerMKII.settings[401] = GUILayout.SelectionGrid((int)FengGameManagerMKII.settings[401], new string[] { "Stable Fit", "Close Fit" }, 2);
            GUILayout.Label(INC.la("shadow_quality") + " [" + (int)FengGameManagerMKII.settings[414] + "]");
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("1"))
            {
                FengGameManagerMKII.settings[414] = 0;
            }
            if (GUILayout.Button("2"))
            {
                FengGameManagerMKII.settings[414] = 2;
            }
            if (GUILayout.Button("3"))
            {
                FengGameManagerMKII.settings[414] = 3;
            }
            if (GUILayout.Button("4"))
            {
                FengGameManagerMKII.settings[414] = 4;
            }
            GUILayout.EndHorizontal();
            GUILayout.Label(INC.la("shadow_distance") + " [" + ((int)(float)FengGameManagerMKII.settings[415]) + "]");
            FengGameManagerMKII.settings[415] = GUILayout.HorizontalSlider((float)FengGameManagerMKII.settings[415], 0, 10000);
            GUILayout.Label(INC.la("lod_lvl") + " [" + ((int)((float)FengGameManagerMKII.settings[413] * 100)) + "]");
            FengGameManagerMKII.settings[413] = GUILayout.HorizontalSlider((float)FengGameManagerMKII.settings[413], 0.2f, 2f);
            GUICyan.OnToogleCyan(INC.la("grafix_blur"), 400, 1, 0, labelwidth);
            GUILayout.Label(INC.la("distance_view") + " [" + ((int)(float)FengGameManagerMKII.settings[417]).ToString() + "]");
            FengGameManagerMKII.settings[417] = GUILayout.HorizontalSlider((float)FengGameManagerMKII.settings[417], 20, 10000);
            GUICyan.OnToogleCyan(INC.la("disable_mipmapping"), 63, 1, 0, labelwidth);
            GUILayout.Label(INC.la("mipmapping_info"));
        }
        GUILayout.Box(INC.la("snapshots_settings"));
        GUICyan.OnToogleCyan(INC.la("enable_snapshots"), 320, 1, 0, labelwidth);
        if ((int)FengGameManagerMKII.settings[320] == 1)
        {
            GUILayout.Label(INC.la("quality_snapshots") + " +" + ((float)FengGameManagerMKII.settings[269] * 100f).ToString("F") + "%.");
            FengGameManagerMKII.settings[269] = GUILayout.HorizontalSlider((float)FengGameManagerMKII.settings[269], 0f, 5f);
            GUICyan.OnToogleCyan(INC.la("show_in_game"), 323, 1, 0, labelwidth);
            GUICyan.OnTextFileCyan(INC.la("snapshot_min_d"), 95, textFiled);
        }
        GUILayout.Box(INC.la("other_settings"));
        GUICyan.OnToogleCyan(INC.la("show_all_sounds"), 330, 1, 0, labelwidth);
        if ((int)FengGameManagerMKII.settings[330] == 1)
        {
            GUILayout.Label(INC.la("all_sound_dis_info"));
        }
        bool flag21 = (int)FengGameManagerMKII.settings[330] == 0;
        GUILayout.Label(INC.la("volume") + " [" + Math.Round((AudioListener.volume * 100)).ToString() + "]" + (flag21 ? "" : INC.la("blocked_volume")));
        if (flag21)
        {
            AudioListener.volume = GUILayout.HorizontalSlider(AudioListener.volume, 0f, 1f);
        }
        else
        {
            AudioListener.volume = 0f;
        }
        GUILayout.Label(INC.la("mouse_speed") + " [" + ((IN_GAME_MAIN_CAMERA.sensitivityMulti * 100)).ToString("F") + "]");
        IN_GAME_MAIN_CAMERA.sensitivityMulti = GUILayout.HorizontalSlider(IN_GAME_MAIN_CAMERA.sensitivityMulti, 0.1f, 1f);
        GUILayout.Label(INC.la("camera_dist") + " [" + Math.Round((this.distanceSlider * 100)).ToString() + "]");

        this.distanceSlider = GUILayout.HorizontalSlider(this.distanceSlider, 0f, 1f);
        GUICyan.OnToogleCyan(INC.la("camera_tilt"), 321, 1, 0, labelwidth);
        GUICyan.OnToogleCyan(INC.la("invert_mouse"), 322, -1, 1, labelwidth);
        GUILayout.Label(INC.la("speedometer"));
        string[] texts = new string[] { INC.la("off"), INC.la("speed"), INC.la("damage") };
        FengGameManagerMKII.settings[189] = GUILayout.SelectionGrid((int)FengGameManagerMKII.settings[189], texts, 3);
        GUICyan.OnToogleCyan(INC.la("minimap"), 231, 1, 0, labelwidth);
        GUICyan.OnToogleCyan(INC.la("game_feed"), 244, 1, 0, labelwidth);
        GUILayout.EndScrollView();
        GUILayout.EndArea();
      
        Application.targetFrameRate = -1;
        int num35;
        if (int.TryParse((string)FengGameManagerMKII.settings[184], out num35) && num35 > 0)
        {
            Application.targetFrameRate = num35;
        }
        IN_GAME_MAIN_CAMERA.cameraDistance = 0.3f + this.distanceSlider;
    }
    void titansettings()
    {
        float labelwidth = 50;
        float textFiled = 80;
        GUILayout.BeginArea(new Rect(280, 0, 280, 450));
        GUILayout.Space(5f);
        vectors2[6] = GUILayout.BeginScrollView(vectors2[6]);
        GUILayout.Box(INC.la("titans_settings"));
        GUILayout.Label(INC.la("ms_only"));

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
            GUILayout.Label(INC.la("t_nornal") + " " + t_normal.ToString() + "%.");
            if (GUILayout.Button("X",GUILayout.Width(40f)))
            {
                t_normal = 0;
            }
            GUILayout.EndHorizontal();
            t_normal = GUILayout.HorizontalSlider(t_normal, 0, titanspawners(0));

            GUILayout.BeginHorizontal();
            GUILayout.Label(INC.la("t_abnormal") + " " + t_aberrant.ToString() + "%.");
            if (GUILayout.Button("X", GUILayout.Width(40f)))
            {
                t_aberrant = 0;
            }
            GUILayout.EndHorizontal();
            t_aberrant = GUILayout.HorizontalSlider(t_aberrant, 0, titanspawners(1));

            GUILayout.BeginHorizontal();
            GUILayout.Label(INC.la("t_jumper") + " " + t_jumper.ToString() + "%.");
            if (GUILayout.Button("X", GUILayout.Width(40f)))
            {
                t_jumper = 0;
            }
            GUILayout.EndHorizontal();
            t_jumper = GUILayout.HorizontalSlider(t_jumper, 0, titanspawners(2));

            GUILayout.BeginHorizontal();
            GUILayout.Label(INC.la("t_crawler") + " " + t_crawler.ToString() + "%.");
            if (GUILayout.Button("X", GUILayout.Width(40f)))
            {
                t_crawler = 0;
            }
            GUILayout.EndHorizontal();
            t_crawler = GUILayout.HorizontalSlider(t_crawler, 0, titanspawners(3));

            GUILayout.BeginHorizontal();
            GUILayout.Label(INC.la("t_punk") + " " + t_punk.ToString() + "%.");
            if (GUILayout.Button("X", GUILayout.Width(40f)))
            {
                t_punk = 0;
            }
            GUILayout.EndHorizontal();
            t_punk = GUILayout.HorizontalSlider(t_punk, 0, titanspawners(4));


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
            GUILayout.Label(INC.la("minimum_integer"));
            FengGameManagerMKII.settings[198] = GUILayout.TextField((string)FengGameManagerMKII.settings[198], GUILayout.Width(60f));
            GUILayout.Label(INC.la("maximum_integer"));
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
        FengGameManagerMKII.settings[337] = GUILayout.TextField((string)FengGameManagerMKII.settings[337], GUILayout.Width(288 - 35f));
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
        GUILayout.EndArea();
    }
 
   public GUIStyle styleButtonH
    {
        get
        {
            GUIStyle styelBut = new GUIStyle();
            styelBut.normal.textColor = GUI.skin.box.normal.textColor;
            styelBut.alignment = TextAnchor.MiddleCenter;
            return styelBut;
        }
    }
    void newsettings()
    {
        float labelwidth = 50;
        float textfiledwidth = 80f;
        
        GUILayout.BeginArea(new Rect(560, 0, 280, 450));
        GUILayout.Space(5f);
        vectors2[7] = GUILayout.BeginScrollView(vectors2[7]);
        GUILayout.Box(INC.la("new_settings_general"));
        if (GUILayout.Button(INC.la("b_settings_anim_nick")))
        {
            mymenu = 6;
        }
        if (GUILayout.Button(INC.la("b_show_list_all_commands")))
        {
            mymenu = 7;
        }
        if (GUILayout.Button(INC.la("chat_on_menu")))
        {
            mymenu = 13;
        }
        if (GUILayout.Button(INC.la("settings_styled")))
        {
            mymenu = 15;
        }
        if (GUILayout.Button(INC.la("snapshots_screens")))
        {
            mymenu = 16;
        }
        if (GUILayout.Button("Kaomoji"))
        {
            mymenu = 17;
        }
        if (GUILayout.Button("Cyan_mod Bot Setting"))
        {
            mymenu = 19;
        }
        GUILayout.BeginVertical(GUI.skin.box); //change nicknamr
        {
            if (GUILayout.Button(INC.la("chagme_nickname"), styleButtonH))
            {
                showSetNamePror = !showSetNamePror;
            }
            if (showSetNamePror)
            {
                GUILayout.Label(INC.la("Name"));
                FengGameManagerMKII.settings[363] = GUILayout.TextField((string)FengGameManagerMKII.settings[363]);
                GUILayout.BeginHorizontal();
                if (GUILayout.Button(INC.la("chage")))
                {
                    PhotonNetwork.player.name2 = (string)FengGameManagerMKII.settings[363];
                }
                if (GUILayout.Button(INC.la("defoultes")))
                {
                    FengGameManagerMKII.settings[363] = PrefersCyan.GetString("string_nick", "GUEST" + UnityEngine.Random.Range(999, 99999));
                    PhotonNetwork.player.name2 = (string)FengGameManagerMKII.settings[363];
                }
                GUILayout.EndHorizontal();
                GUILayout.Label(INC.la("chatname"));
                FengGameManagerMKII.settings[364] = GUILayout.TextField((string)FengGameManagerMKII.settings[364]);

                if (GUILayout.Button(INC.la("defoultes")))
                {
                    FengGameManagerMKII.settings[364] = PrefersCyan.GetString("string_chat_name", ""); ;
                }

                GUILayout.Label(INC.la("Guild1"));
                FengGameManagerMKII.settings[361] = GUILayout.TextField((string)FengGameManagerMKII.settings[361]);
                GUILayout.Label(INC.la("Guild2"));
                FengGameManagerMKII.settings[362] = GUILayout.TextField((string)FengGameManagerMKII.settings[362]);
                GUILayout.BeginHorizontal();
                if (GUILayout.Button(INC.la("chage")))
                {
                    string str3 = "";
                    string g1trimg = ((string)FengGameManagerMKII.settings[361]).Trim();
                    string g1trimg2 = ((string)FengGameManagerMKII.settings[362]).Trim();
                    if (g1trimg != string.Empty && g1trimg2 != string.Empty)
                    {
                        str3 = g1trimg + "\n" + g1trimg2;
                    }
                    else if (g1trimg != string.Empty)
                    {
                        str3 = g1trimg;
                    }
                    else if (g1trimg2 != string.Empty)
                    {
                        str3 = g1trimg2;
                    }
                    PhotonNetwork.player.guildName = str3;
                }
                if (GUILayout.Button(INC.la("defoultes")))
                {
                    FengGameManagerMKII.settings[361] = PrefersCyan.GetString("string_guild_name_1", "");
                    FengGameManagerMKII.settings[362] = PrefersCyan.GetString("string_guild_name_2", "");
                    string str3 = "";
                    string g1trimg = ((string)FengGameManagerMKII.settings[361]).Trim();
                    string g1trimg2 = ((string)FengGameManagerMKII.settings[362]).Trim();
                    if (g1trimg != string.Empty && g1trimg2 != string.Empty)
                    {
                        str3 = g1trimg + "\n" + g1trimg2;
                    }
                    else if (g1trimg != string.Empty)
                    {
                        str3 = g1trimg;
                    }
                    else if (g1trimg2 != string.Empty)
                    {
                        str3 = g1trimg2;
                    }
                    PhotonNetwork.player.guildName = str3;
                }
                GUILayout.EndHorizontal();
            }
        }
        GUILayout.EndVertical();//change nicknamr end
        GUILayout.BeginVertical(GUI.skin.box); //settingsUI
        {
            if (GUILayout.Button(INC.la("settings_ui_panels"), styleButtonH))
            {
                showUILabel_panel = !showUILabel_panel;
            }
            if (showUILabel_panel)
            {
                GUICyan.OnToogleCyan(INC.la("show_label_center"), 293, 1, 0, labelwidth);
                GUICyan.OnToogleCyan(INC.la("show_label_top_center"), 294, 1, 0, labelwidth);
                GUICyan.OnToogleCyan(INC.la("show_label_top_left"), 295, 1, 0, labelwidth);
                GUICyan.OnToogleCyan(INC.la("show_label_top_right"), 303, 1, 0, labelwidth);
                GUIStyle sss = new GUIStyle(GUI.skin.label);
                sss.normal.textColor = INC.color_UI;
                INC.color_UI = cext.color_toGUI(INC.color_UI, sss, INC.la("colors_uilabel"));
            }
        }
        GUILayout.EndVertical();//settingsUI end

        GUILayout.BeginVertical(GUI.skin.box); //autokick
        {
            if (GUILayout.Button(INC.la("auto_kickeds"), styleButtonH))
            {
                show_set[0] = !show_set[0];
            }
            if (show_set[0])
            {

                GUICyan.OnToogleCyan(INC.la("autokicked_ex_stats"),398,1,0,labelwidth);
                GUICyan.OnToogleCyan(INC.la("autokicked_guests"), 297, 1, 0, labelwidth);
                if ((int)FengGameManagerMKII.settings[297] == 1)
                {
                    GUICyan.OnToogleCyan(INC.la("autokicked_show_guests"), 298, 1, 0, labelwidth);
                }
                GUICyan.OnToogleCyan(INC.la("autokiked_titan-player"), 335, 1, 0, labelwidth);
                GUICyan.OnToogleCyan(INC.la("autokicked_noname"), 299, 1, 0, labelwidth);
                GUICyan.OnToogleCyan(INC.la("autokicked_vivid"), 277, 1, 0, labelwidth);
                GUICyan.OnToogleCyan(INC.la("autokicked_hyper"), 278, 1, 0, labelwidth);
                GUICyan.OnToogleCyan(INC.la("autokicked_tokyo"), 279, 1, 0, labelwidth);
            }
        }
        GUILayout.EndVertical();//autokick end
        GUILayout.BeginVertical(GUI.skin.box); //chat settings
        {
         
            if (GUILayout.Button(INC.la("chat_settings"), styleButtonH))
            {
                show_set[1] = !show_set[1];
            }
            if (show_set[1])
            {
                GUICyan.OnToogleCyan(INC.la("show_chat_m"), 292, 1, 0, labelwidth);
                GUICyan.OnToogleCyan(INC.la("time_in_chat"), 383, 1, 0, labelwidth);
                GUICyan.OnToogleCyan(INC.la("clean_hex"), 376, 1, 0, labelwidth);
                GUICyan.OnToogleCyan(INC.la("filter_bads"), 342, 1, 0, labelwidth);
                GUICyan.OnToogleCyan(INC.la("show_connect_info_pl"), 334, 1, 0, labelwidth);
                GUICyan.OnToogleCyan(INC.la("show_background_chat"), 274, 1, 0, labelwidth);
                if ((int)FengGameManagerMKII.settings[274] == 1)
                {
                    GUIStyle st = new GUIStyle(GUI.skin.label);
                    st.normal.textColor = (Color)FengGameManagerMKII.settings[353];
                    GUILayout.Label(INC.la("color_background"), st);
                    if (InRoomChat.Background.normal.background != null)
                    {
                        Destroy(InRoomChat.Background.normal.background);
                    }
                    FengGameManagerMKII.settings[353] = cext.color_toGUI((Color)FengGameManagerMKII.settings[353]);
                    InRoomChat.Background.normal.background = ((Color)FengGameManagerMKII.settings[353]).toTexture1();
                }
                GUILayout.Label(INC.la("sytem_mess_styles"));
                FengGameManagerMKII.settings[375] = GUILayout.SelectionGrid((int)FengGameManagerMKII.settings[375], bbfunc, 4);

                GUIStyle ssst = new GUIStyle(GUI.skin.label);
                ssst.normal.textColor = (Color)FengGameManagerMKII.settings[369];
                FengGameManagerMKII.settings[369] = cext.color_toGUI((Color)FengGameManagerMKII.settings[369], ssst, INC.la("sytem_color_mess"), false);

                float sss = (float)FengGameManagerMKII.settings[352];
                GUILayout.Label(INC.la("size_chat") + ((float)FengGameManagerMKII.settings[352]).ToString("F"));
                FengGameManagerMKII.settings[352] = GUILayout.HorizontalSlider((float)FengGameManagerMKII.settings[352], 0, 100f);
                if (sss != (float)FengGameManagerMKII.settings[352])
                {
                    InRoomChat.instance.setPosition();
                }
            }
        }
        GUILayout.EndVertical();//end chat settings
        GUILayout.BeginVertical(GUI.skin.box); //chat style my mess
        {
            if (GUILayout.Button(INC.la("my_show_style_in_chat"), styleButtonH))
            {
                show_set[2] = !show_set[2];
            }
            if (show_set[2])
            {
                GUICyan.OnToogleCyan(INC.la("my_style_in_chat"), 264, 1, 0, labelwidth);
                if ((int)FengGameManagerMKII.settings[264] == 1)
                {
                    FengGameManagerMKII.settings[268] = GUILayout.SelectionGrid((int)FengGameManagerMKII.settings[268], bbfunc, 4);
                    GUIStyle style33 = new GUIStyle(GUI.skin.label);
                    style33.normal.textColor = (Color)FengGameManagerMKII.settings[271];
                    GUILayout.Label(INC.la("color_my_message"), style33);
                    FengGameManagerMKII.settings[271] = cext.color_toGUI((Color)FengGameManagerMKII.settings[271], "", false);
                    GUICyan.OnTextFileCyan(INC.la("sign_in_chat"), 266, textfiledwidth);
                    GUICyan.OnTextFileCyan(INC.la("first_in_chat"), 373, textfiledwidth);
                    GUICyan.OnTextFileCyan(INC.la("end_in_chat"), 374, textfiledwidth);
                }
                else
                {
                    GUILayout.Label(INC.la("help_in_chat_style"));
                }
            }
        }
        GUILayout.EndVertical();//chat style my mess
        GUILayout.BeginVertical(GUI.skin.box); //sprite settings
        {
            if (GUILayout.Button(INC.la("sprite_settings"), styleButtonH))
            {
                show_set[3] = !show_set[3];
            }
            if (show_set[3])
            {
                GUICyan.OnToogleCyan(INC.la("show_sprites_player"), 290, 1, 0, labelwidth);
                if ((int)FengGameManagerMKII.settings[290] == 0)
                {
                    GUIStyle style1 = new GUIStyle(GUI.skin.label);
                    style1.normal.textColor = (Color)FengGameManagerMKII.settings[356];
                    GUILayout.Label(INC.la("color_sprite_1"), style1);
                    FengGameManagerMKII.settings[356] = cext.color_toGUI((Color)FengGameManagerMKII.settings[356]);
                    style1.normal.textColor = (Color)FengGameManagerMKII.settings[357];
                    GUILayout.Label(INC.la("color_sprite_2"), style1);
                    FengGameManagerMKII.settings[357] = cext.color_toGUI((Color)FengGameManagerMKII.settings[357]);
                }
            }
        }
        GUILayout.EndVertical();//end sprite_setting
        statusGUI.newSettin(labelwidth);
        GUICyan.OnToogleCyan("Test_Kill_Hook", 443, 1, 0, labelwidth);
        GUICyan.OnToogleCyan(INC.la("prop_anti_spam"), 393, 1, 0, labelwidth);
        int num1 = (int)FengGameManagerMKII.settings[355];
        int num2 = (int)FengGameManagerMKII.settings[354];
        GUICyan.OnToogleCyan(INC.la("show_grafic_total_m"), 355, 1, 0, labelwidth);
        GUICyan.OnToogleCyan(INC.la("show_grafic_kills_m"), 354, 1, 0, labelwidth);
        if ((num1 != (int)FengGameManagerMKII.settings[355] || num2 != (int)FengGameManagerMKII.settings[354]) && IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
        {
            ChageGrafix(1);
        }

        GUICyan.OnToogleCyan(INC.la("show_info_titans"), 341, 1, 0, labelwidth);
        GUICyan.OnToogleCyan(INC.la("dis_stylish_p"), 336, 1, 0, labelwidth);
        GUICyan.OnToogleCyan(INC.la("dis_spawn_15_t_ft"), 331, 1, 0, labelwidth);
        GUICyan.OnTextFileCyan(INC.la("max_damage_ft"), 332, textfiledwidth);
        GUICyan.OnToogleCyan(INC.la("show_damage_label_ft"), 333, 1, 0, labelwidth);

        GUICyan.OnToogleCyan(INC.la("OnFPSCount"), 280, 1, 0, labelwidth);
        GUICyan.OnToogleCyan(INC.la("body_lean"), 281, 1, 0, labelwidth);
        GUICyan.OnToogleCyan(INC.la("disable_gas"), 282, 1, 0, labelwidth);
        GUICyan.OnToogleCyan(INC.la("disable_all_gas"), 283, 1, 0, labelwidth);
        GUICyan.OnToogleCyan(INC.la("show_slash_effect"), 284, 1, 0, labelwidth);
        GUICyan.OnToogleCyan(INC.la("show_titan_smoke_effect"), 285, 1, 0, labelwidth);
        GUICyan.OnToogleCyan(INC.la("show_titan_nape"), 286, 1, 0, labelwidth);
        GUICyan.OnToogleCyan(INC.la("show_blood_effect"), 287, 1, 0, labelwidth);
        GUICyan.OnToogleCyan(INC.la("show_spawn_titan_effect"), 288, 1, 0, labelwidth);
        GUICyan.OnToogleCyan(INC.la("show_aim_player"), 289, 1, 0, labelwidth);

        GUICyan.OnToogleCyan(INC.la("show_my_plyer_name"), 328, 1, 0, labelwidth);
        GUICyan.OnToogleCyan(INC.la("show_all_player_name"), 291, 1, 0, labelwidth);
      
        GUICyan.OnToogleCyan(INC.la("show_orig_camera"), 324, 1, 0, labelwidth);
        GUICyan.OnToogleCyan(INC.la("show_tps_camera"), 325, 1, 0, labelwidth);
        GUICyan.OnToogleCyan(INC.la("show_wow_camera"), 326, 1, 0, labelwidth);
        GUICyan.OnToogleCyan(INC.la("show_old_tps_camera"), 327, 1, 0, labelwidth);
       
        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }

    string playerStats()
    {
        int gas = (selplayer.statGAS);
        int bla = (selplayer.statBLA);
        int acl = (selplayer.statACL);
        int spd = (selplayer.statSPD);
        return "ACL:" + acl.ToString() + " BLA:" + bla.ToString() + " SPD:" + spd.ToString() + " GAS:" + gas.ToString() + " Total:" + (gas + bla + acl + spd).ToString();
    }
    public void CheckPropertyL(PhotonPlayer player, string key)
    {
        switch (key)
        {
            case "CyanMod": return;
            case "CyanModNew": return;
            case "version": return;
            case "beard_texture_id": return;
            case "body_texture": return;
            case "cape": return;
            case "character": return;
            case "costumeId": return;
            case "currentLevel": return;
            case "customBool": return;
            case "customFloat": return;
            case "customInt": return;
            case "customString": return;
            case "dead": return;
            case "deaths": return;
            case "division": return;
            case "eye_texture_id": return;
            case "glass_texture_id": return;
            case "guildName": return;
            case "hair_color1": return;
            case "hair_color2": return;
            case "hair_color3": return;
            case "hairInfo": return;
            case "heroCostumeId": return;
            case "isTitan": return;
            case "kills": return;
            case "max_dmg": return;
            case "name": return;
            case "part_chest_1_object_mesh": return;
            case "part_chest_1_object_texture": return;
            case "part_chest_object_mesh": return;
            case "part_chest_object_texture": return;
            case "part_chest_skinned_cloth_mesh": return;
            case "part_chest_skinned_cloth_texture": return;
            case "RCBombA": return;
            case "RCBombB": return;
            case "RCBombG": return;
            case "RCBombR": return;
            case "RCBombRadius": return;
            case "RCteam": return;
            case "sex": return;
            case "skin_color": return;
            case "statACL": return;
            case "statBLA": return;
            case "statGAS": return;
            case "statSKILL": return;
            case "statSPD": return;
            case "team": return;
            case "total_dmg": return;
            case "uniform_type": return;
            case "sender": return;
        }
        player.Property = key;
    }
    void infoPlayers()
    {
        if (to_Anya.instance != null)
        {
            string[] dfsfsd = new string[] { INC.la("information_player"), INC.la("banlist_payl"), INC.la("hashtable_player"), "тыц" };
            playered = GUILayout.SelectionGrid(playered, dfsfsd, 5);
        }
        else
        {
            string[] dfsfsd = new string[] { INC.la("information_player"), INC.la("banlist_payl"), INC.la("hashtable_player"),"Event" };
            playered = GUILayout.SelectionGrid(playered, dfsfsd, 4);
        }
        if (playered == 3)
        {
            GUILayout.Box("Events");
            scrolPos332 = GUILayout.BeginScrollView(scrolPos332);
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical(GUILayout.Width(150f));
            foreach (var s in selplayer.eve.codelist)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Code:" + s.Key);
                GUILayout.Label("Re:" + s.Value);
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();
            GUILayout.BeginVertical(GUILayout.Width(300f));
            foreach (var s in selplayer.eve.rpclist)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("RPC:" + s.Key);
                GUILayout.Label("Re:" + s.Value,GUILayout.Width(80f));
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUILayout.EndScrollView();
            PhotonNetwork.networkingPeer.isInformationEvent = GUILayout.Toggle(PhotonNetwork.networkingPeer.isInformationEvent, "Enable");
        }
        if (playered == 2)
        {
            GUILayout.Box(INC.la("hashtable_m_player"));

            if (selplayer.customProperties.Count == 0)
            {
                GUILayout.Label(INC.la("no_customprop_player"));
            }
            else
            {
                info_heshtable = GUILayout.SelectionGrid(info_heshtable, new string[] { INC.la("to_basic_hash"), INC.la("to_copy_hash") }, 2, GUILayout.Width(400f));
                vectors2[0] = GUILayout.BeginScrollView(vectors2[0]);
                if (info_heshtable == 0)
                {
                    foreach (var nameplayer in selplayer.customProperties)
                    {
                        GUILayout.BeginHorizontal(GUI.skin.box);
                        GUILayout.BeginVertical(GUILayout.Width(170f));
                        {
                            GUILayout.Label(nameplayer.Key.ToString());
                        }
                        GUILayout.EndVertical();
                        GUILayout.BeginVertical();
                        {
                            GUILayout.Label(nameplayer.Value.ToString());
                        }
                        GUILayout.EndVertical();
                        GUILayout.EndHorizontal();
                    }
                }
                else if (info_heshtable == 1)
                {
                    string strerd = INC.la("kays_and_val_cp") + "\n";
                    foreach (var nameplayer in selplayer.customProperties)
                    {
                        strerd = strerd + nameplayer.Key + ":" + nameplayer.Value + "\n";
                    }
                    GUILayout.TextField(strerd);
                }
                GUILayout.EndScrollView();

            }
        }
      
        if (playered == 1)
        {

        ban_per_temp = GUILayout.SelectionGrid(ban_per_temp,new string[]{INC.la("ban_perm_player"),INC.la("ban_temp_player")},2);
              if (ban_per_temp == 1)
        {
            GUILayout.Box(INC.la("ban_temp_m_player"));
            if (PhotonNetwork.player.isMasterClient)
            {
                if (banHash.Count == 0)
                {
                    GUILayout.Label(INC.la("no_players_in_t_banlist"));
                }
                else
                {
                    vectors2[0] = GUILayout.BeginScrollView(vectors2[0], GUILayout.Height(300f));
                    foreach (var nameplayer in banHash)
                    {
                        GUILayout.BeginHorizontal(GUI.skin.box);
                        if (GUILayout.Button("x", GUILayout.Width(40f)))
                        {
                            banHash.Remove(nameplayer.Key);
                        }
                        GUILayout.TextField("ID:" + nameplayer.Key.ToString() + " Name:" + nameplayer.Value.ToString(), GUI.skin.label);

                        GUILayout.EndHorizontal();
                    }
                    GUILayout.EndScrollView();
                }
            }
            else
            {
                GUILayout.Label(INC.la("only_master_client"));
            }
        }
        else
{
            GUILayout.Box(INC.la("ban_perm_m_player"));
            vectors2[0] = GUILayout.BeginScrollView(vectors2[0], GUILayout.Height(300f));
            if (INC.banlist.Count == 0)
            {
                GUILayout.Label(INC.la("no_players_in_p_banlist"));
            }
            else
            {
                for (int i = 0; i < INC.banlist.Count; i++)
                {
                    string nameplayer = INC.banlist[i];
                    GUILayout.BeginHorizontal(GUI.skin.box);
                    if (GUILayout.Button("x", GUILayout.Width(40f)))
                    {
                        INC.banlist.Remove(nameplayer);
                        to_read_banlist = -1;
                    }

                    if (to_read_banlist == i)
                    {
                        if (GUILayout.Button("apply", GUILayout.Width(60f)))
                        {
                            to_read_banlist = -1;
                        }
                        INC.banlist[i] = GUILayout.TextField(INC.banlist[i]);
                    }
                    else
                    {
                        if (GUILayout.Button("read", GUILayout.Width(60f)))
                        {
                            to_read_banlist = i;
                        }
                        GUILayout.TextField(nameplayer, GUI.skin.label);
                    }
                    GUILayout.EndHorizontal();
                }
            }
            GUILayout.EndScrollView();
            GUILayout.BeginHorizontal();
            AddOnPlayerBanlist = GUILayout.TextField(AddOnPlayerBanlist, GUILayout.Width(400f));
            if (GUILayout.Button(INC.la("add_on_player_to_p_ban")))
            {
                if (!INC.banlist.Contains(AddOnPlayerBanlist))
                {
                    INC.banlist.Add(AddOnPlayerBanlist);
                    AddOnPlayerBanlist = string.Empty;
                }
            }
            GUILayout.EndHorizontal();
            }
        }
        if (playered == 0)
        {
            if (selplayer.customProperties.Count > 0)
            {
                foreach (System.Collections.DictionaryEntry x in selplayer.customProperties)
                {
                    CheckPropertyL(selplayer, x.Key.ToString());
                }
            }
            GUILayout.Box(INC.la("information_and_set_player"));
            bool flagbanlist = INC.banlist.Contains(selplayer.name2);
            string onbanlistplayer = flagbanlist ? INC.la("player_in_banlist") + "\n" : string.Empty;
            string nomessage = selplayer.LockPlayer ? INC.la("no_message_on_player") + "\n" : string.Empty;
            string nicklocal = INC.la("full_nick") + selplayer.name2.Replace("\n", string.Empty).toHex() + "\n";
            string guildlocal = INC.la("full_guild") + selplayer.guildName.Replace("\n", string.Empty).toHex() + "\n";
            string statslocal = INC.la("stats_on_menu") + playerStats() + "\n";
            string characterslocal = INC.la("character_on_menu") + selplayer.character + " " + INC.la("skill_on_menu") + selplayer.statSKILL + "\n";
            string propertylocal = INC.la("player_mod") + Checkmod(selplayer) + " " + INC.la("player_property") + selplayer.Property + "\n";
            string testing = "";
            if ((int)FengGameManagerMKII.settings[384] == 1)
            {
                testing = "kb:" + (selplayer.bytes / 1024).ToString();
            }
            GUILayout.TextField(onbanlistplayer + nomessage + nicklocal + guildlocal + statslocal + characterslocal + propertylocal + testing, GUI.skin.label);
            GUILayout.BeginVertical(GUILayout.Width(400f));
            bool isMS = PhotonNetwork.isMasterClient;
            if (AddPl.Count <= 1)
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button(INC.la("kicked_player")))
                {
                    CommandOnPlayer.kick(selplayer);
                }
                if (isMS)
                {
                    if (GUILayout.Button(INC.la("baned_player")))
                    {
                        CommandOnPlayer.ban(selplayer);
                    }
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                if (GUILayout.Button(flagbanlist ? INC.la("remove_to_banlist_player") : INC.la("add_to_banlist_player")))
                {
                    Addbanlist(selplayer);
                }
                if (GUILayout.Button(selplayer.LockPlayer ? INC.la("unmute_player") : INC.la("mute_player")))
                {
                    selplayer.LockPlayer = !selplayer.LockPlayer;
                    cext.mess(selplayer.LockPlayer ? PhotonNetwork.player.name() + INC.la("added_to_mutlist") + selplayer.ishexname : PhotonNetwork.player.ishexname + INC.la("removed_to_mutlist") + selplayer.ishexname);
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                if (isMS)
                {
                    if (selplayer.dead && (selplayer.isTitan != 2))
                    {
                        if (GUILayout.Button(INC.la("revive_m_player")))
                        {
                            FengGameManagerMKII.instance.photonView.RPC("respawnHeroInNewRound", selplayer, new object[0]);
                            cext.mess(selplayer.ishexname + INC.la("rev_players"));
                        }
                    }
                    if (GUILayout.Button(INC.la("clear_stats_m_player")))
                    {
                        selplayer.kills = 0;
                        selplayer.deaths = 0;
                        selplayer.max_dmg = 0;
                        selplayer.total_dmg = 0;
                        cext.mess(INC.la("revived_stats") + selplayer.id);
                    }
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                pm_message = GUILayout.TextField(pm_message,GUILayout.Width(330f));
                if(GUILayout.Button(">>"))
                {
                    string to_s = pm_message.Trim();
                    if(to_s != "" && selplayer != null)
                    {
                        string text32 = InRoomChat.StyleMes(INC.la("priv_message_from") + PhotonNetwork.player.id + ":") + to_s;
                        base.photonView.RPC("Chat",selplayer,new object[] { text32,"" });
                        pm_message = "";
                        PanelInformer.instance.Add(INC.la("senden_pm") + selplayer.ishexname, PanelInformer.LOG_TYPE.INFORMAION);
                    }
                    else
                    {
                        PanelInformer.instance.Add(INC.la("error_send_message"), PanelInformer.LOG_TYPE.WARNING);
                    }
                }
                GUILayout.EndHorizontal();
                if(PhotonNetwork.isMasterClient)
                {
                    GUILayout.BeginHorizontal();
                    if(GUILayout.Button(INC.la("revive_all")))
                    {
                        InRoomChat.instance.reviveall();
                    }
                    if(GUILayout.Button(INC.la("clean_stats_all")))
                    {
                        InRoomChat.instance.resetkdall();
                    }
                    GUILayout.EndHorizontal();
                }
            }
            else
            {
                GUILayout.Label(INC.la("addeds_coun_player") + AddPl.Count);
                if (isMS)
                {
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button(INC.la("kicked_all_player")))
                    {
                        foreach (PhotonPlayer pl in AddPl)
                        {
                            if (pl != null)
                            {
                                CommandOnPlayer.kick(pl);
                            }
                        }
                        AddPl = new List<PhotonPlayer> { PhotonNetwork.player };
                        selplayer = PhotonNetwork.player;
                    }
                    if (GUILayout.Button(INC.la("baned_all_player")))
                    {
                        foreach (PhotonPlayer pl in AddPl)
                        {
                            if (pl != null)
                            {
                                CommandOnPlayer.ban(pl);
                            }
                        }
                        AddPl = new List<PhotonPlayer> { PhotonNetwork.player };
                        selplayer = PhotonNetwork.player;
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button(INC.la("rev_all_player")))
                    {
                        string str = "";
                        foreach (PhotonPlayer pl in AddPl)
                        {
                            if (pl.dead && (pl.isTitan != 2))
                            {
                                FengGameManagerMKII.instance.photonView.RPC("respawnHeroInNewRound", selplayer, new object[0]);
                                str = str + pl.id;
                            }
                        }
                        cext.mess(INC.la("rev_all_players") + str);
                        AddPl = new List<PhotonPlayer> { PhotonNetwork.player };
                        selplayer = PhotonNetwork.player;
                    }
                    if (GUILayout.Button(INC.la("clear_all_stats_m_player")))
                    {
                        string str = "";
                        foreach (PhotonPlayer pl in AddPl)
                        {
                            pl.kills = 0;
                            pl.deaths = 0;
                            pl.max_dmg = 0;
                            pl.total_dmg = 0;
                            str = str + pl.id;
                        }
                        cext.mess(INC.la("revived_all_stats") + str);
                        AddPl = new List<PhotonPlayer> { PhotonNetwork.player };
                        selplayer = PhotonNetwork.player;
                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.BeginHorizontal();
                if (GUILayout.Button(INC.la("add_to_banlist_all_player")))
                {
                    foreach (PhotonPlayer pl in AddPl)
                    {
                        Addbanlist(pl);
                    }
                    AddPl = new List<PhotonPlayer> { PhotonNetwork.player };
                    selplayer = PhotonNetwork.player;
                }
                if (GUILayout.Button(INC.la("mute_all_player")))
                {
                    foreach (PhotonPlayer pl in AddPl)
                    {
                        if (!pl.isLocal && !pl.LockPlayer)
                        {
                            pl.LockPlayer = false;
                            cext.mess(PhotonNetwork.player.ishexname + INC.la("added_to_mutlist") + pl.ishexname);
                        }
                    }
                    AddPl = new List<PhotonPlayer> { PhotonNetwork.player };
                    selplayer = PhotonNetwork.player;
                }
                GUILayout.EndHorizontal();

            }
            GUILayout.EndVertical();
        }
    }
    void Addbanlist(PhotonPlayer player)
    {
        string name = player.name2;
        if (name != string.Empty && !INC.banlist.Contains(name) && PhotonNetwork.player != player)
        {
            INC.banlist.Add(name);
            if (PhotonNetwork.player.isMasterClient)
            {
                kickPlayerRC(player, false, INC.la("added_to_banlist"));
            }
        }
        else if (INC.banlist.Contains(name))
        {
            INC.banlist.Remove(name);
        }
    }
    void playerList_1()
    {
        GUIStyle stylewwss = new GUIStyle(GUI.skin.button);
        stylewwss.alignment = TextAnchor.MiddleLeft;
        vectors2[21] = GUILayout.BeginScrollView(vectors2[21], GUILayout.Height(380f));
        GUILayout.BeginHorizontal(GUILayout.Width(214f));
        GUILayout.Label(INC.la("players_list_om"), GUILayout.Width(155f));
        if (GUILayout.Button("<size=18>☑</size>"))
        {
            foreach (PhotonPlayer player in PhotonNetwork.playerList)
            {
                if (!AddPl.Contains(player))
                {
                    AddPl.Add(player);
                }
            }
        }
        if (GUILayout.Button("<size=18>☐</size>"))
        {
            AddPl.Clear();
        }
        GUILayout.EndHorizontal();
        foreach (PhotonPlayer player in PhotonNetwork.playerList)
        {

            if (AddPl.Contains(player) || selplayer == player)
            {
                stylewwss.normal = GUI.skin.button.onNormal; 
              
            }
            else
            {
                stylewwss.normal = GUI.skin.button.normal; 
             
            }
            string fulPlayername = player.id;

            if ((player.dead))
            {
                fulPlayername = fulPlayername + "<color=#FF0000>D</color>";
            }
            else
            {
                if (player.isMasterClient)
                {
                    fulPlayername = fulPlayername + "M";
                }
                if ((player.isTitan) == 2)
                {
                    fulPlayername = fulPlayername + "T";
                }
                if ((player.isTitan) == 1 && (player.team) != 2)
                {
                    fulPlayername = fulPlayername + "H";
                }
                if ((player.team) == 2)
                {
                    fulPlayername = fulPlayername + "A";
                }
            }
            if (player.name() != string.Empty)
            {
                string nnsk = player.name2.toHex();

                fulPlayername = fulPlayername + "<color=#000000>|</color>" + nnsk;
            }
            else
            {
                fulPlayername = fulPlayername + "<color=#000000>|[No Name]</color>";
            }
            if (ignoreList.Contains(player.ID))
            {
                fulPlayername = "<color=#FF0000>[X]</color>" + fulPlayername;
            }
            GUILayout.BeginHorizontal(GUILayout.Width(214f));
            if (GUILayout.Button(fulPlayername, stylewwss, GUILayout.Width(185f), GUILayout.Height(24f)))
            {
                AddPl.Clear();
                selplayer = player;
                AddPl.Add(player);
            }
            string str4343 = "";
            if (AddPl.Contains(player))
            {
                str4343 = "<size=18>☑</size>";
                stylewwss.normal = GUI.skin.button.onNormal; 
            }
            else
            {
                str4343 = "<size=18>☐</size>";
                stylewwss.normal = GUI.skin.button.normal; 
            }
            if (GUILayout.Button(str4343, stylewwss, GUILayout.Width(24f), GUILayout.Height(24f)))
            {
                if (AddPl.Contains(player))
                {
                    AddPl.Remove(player);
                    return;
                }
                AddPl.Add(player);
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndScrollView();
    }
    void playerListOnMenu()
    {
        if (selplayer.ID > 0)
        {
            GUIStyle fdfdg = new GUIStyle(GUI.skin.label);
            fdfdg.fontSize = 160;
            fdfdg.alignment = TextAnchor.MiddleCenter;
            fdfdg.fontStyle = FontStyle.Bold;
            fdfdg.normal.textColor = new Color(fdfdg.normal.textColor.r, fdfdg.normal.textColor.g, fdfdg.normal.textColor.b, 0.1f);
            GUI.Label(new Rect(Screen.width / 2 - 300, Screen.height / 2 - 200, 600, 400), selplayer.ID.ToString(), fdfdg);
        }
        GUILayout.BeginArea(new Rect(Screen.width / 2 - 370, Screen.height / 2 - 250, 840, 450), GUI.skin.box);
        GUILayout.Label("<size=16>" + INC.la("b_player_list") + "</size>");
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
        {
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical(GUILayout.Width(240f));
            playerList_1();
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            infoPlayers();
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }
        else
        {
            GUILayout.Label(INC.la("no_multyplayer"));
        }
        if (GUILayout.Button(INC.la("back"), GUILayout.Width(150f)))
        {
            mymenu = 0;
        }
        GUILayout.EndArea();


    }
    IEnumerator screenShotInfo()
    {
        yield return new WaitForSeconds(0.2f);
        PanelInformer.instance.Add(INC.la("screen_shot"), PanelInformer.LOG_TYPE.INFORMAION);
        yield break;
    }
    void CustomButtonCyan()//LateUpdate
    {
        if (FengGameManagerMKII.inputRC.isInputCyanKeyDown(InputCode.screenshot))
        {
            string path = FengGameManagerMKII.ScreenshotsPath;
            string str2 = "screen_" + DateTime.Now.ToString("DD:MM:YY").Replace(":", "_") + "_TIME-" + DateTime.Now.ToString("HH:mm:ss").Replace(":", "_") + ".png";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            Application.CaptureScreenshot(path + str2);
            StartCoroutine(screenShotInfo());
        }
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
        {
            if ((int)FengGameManagerMKII.settings[384] == 1)
            {
                statusGUI.lateUpdate();
            }
            if ((RCSettings.AnnieSurvive > 0) && PhotonNetwork.isMasterClient)
            {
                isCheckingsFT();
            }
            if (  (int)FengGameManagerMKII.settings[300] == 1 && Nanimation.Count > 0)
            {
                timeUPD232 += (float)Time.deltaTime;
                if (timeUPD232 > 0.2f)
                {
                    timeUPD232 = 0;
                    if (counnnts > Nanimation.Count - 1)
                    {
                        counnnts = 0;
                    }
                    PhotonNetwork.player.name2 = Nanimation[counnnts];

                    counnnts++;
                }
            }
            bool flag_1 = inputRC.isInputCyanKey(InputCode.buttonX);
            if (PhotonNetwork.isMasterClient)
            {
                if (inputRC.isInputCyanKeyDown(InputCode.speed_rest) && flag_1)
                {
                    GoRestarting();
                    return;
                }
                if (isGoRestart)
                {
                    if (dds <= 0 && timerGoRestart > 0)
                    {
                        if ((int)FengGameManagerMKII.settings[329] == 0)
                        {
                            object[] param = new object[] { false, "Restart in", false, "second", timerGoRestart };

                            base.photonView.RPC("updateKillInfo", PhotonTargets.All, param);
                        }
                        else
                        {
                            cext.mess(INC.la("go_restarting_in") + (timerGoRestart) + INC.la("restarting_seconds"));
                        }

                    }
                    dds += (double)Time.deltaTime;
                    if (dds >= 1)
                    {
                        dds = 0;
                        timerGoRestart--;
                    }
                    if (timerGoRestart <= 0)
                    {
                        restartRC();
                        cext.mess(INC.la("go_restarting_game"));
                        isGoRestart = false;
                    }

                }
            }
            if (FengGameManagerMKII.inputRC.isInputCyanKeyDown(InputCode.message_0) && flag_1)
            {
                InRoomChat.SendMessageToChat((string)FengGameManagerMKII.settings[313]);
                return;
            }
            if (FengGameManagerMKII.inputRC.isInputCyanKeyDown(InputCode.message_1) && flag_1)
            {
                InRoomChat.SendMessageToChat((string)FengGameManagerMKII.settings[314]);
                return;
            }
            if (FengGameManagerMKII.inputRC.isInputCyanKeyDown(InputCode.message_2) && flag_1)
            {
                InRoomChat.SendMessageToChat((string)FengGameManagerMKII.settings[315]);
                return;
            }
            if (FengGameManagerMKII.inputRC.isInputCyanKeyDown(InputCode.message_3) && flag_1)
            {

                InRoomChat.SendMessageToChat((string)FengGameManagerMKII.settings[316]);
                return;
            }
            if (FengGameManagerMKII.inputRC.isInputCyanKeyDown(InputCode.message_4) && flag_1)
            {
                InRoomChat.SendMessageToChat((string)FengGameManagerMKII.settings[317]);
                return;
            }
            if (FengGameManagerMKII.inputRC.isInputCyanKeyDown(InputCode.message_5) && flag_1)
            {
                InRoomChat.SendMessageToChat((string)FengGameManagerMKII.settings[318]);
                return;
            }
            if (FengGameManagerMKII.inputRC.isInputCyanKeyDown(InputCode.message_6) && flag_1)
            {
                InRoomChat.SendMessageToChat((string)FengGameManagerMKII.settings[319]);
                return;
            }

        }
    }
   
    void newbinds()
    {
        GUILayout.BeginArea(new Rect(405, 0, 400, 450));
        GUILayout.Space(5f);
        vectors2[9] = GUILayout.BeginScrollView(vectors2[9]);
        GUILayout.Box(INC.la("new_binds_cm"));
        GUILayout.BeginHorizontal();
        if (GUILayout.Button((string)FengGameManagerMKII.settings[311], GUILayout.Width(100f)))
        {
            FengGameManagerMKII.settings[311] = "waiting...";
            FengGameManagerMKII.settings[100] = 30 + 7;
        }
        GUILayout.Label(INC.la("basic_binds") + (string)FengGameManagerMKII.settings[311] + " + " + (string)FengGameManagerMKII.settings[304]);

        GUILayout.EndHorizontal();
        GUILayout.Label(INC.la("speed_messages"));
        string[] sstt = new string[] { "stringMess_0", "stringMess_1", "stringMess_2", "stringMess_3", "stringMess_4", "stringMess_5", "stringMess_6" };

        for (int i = 0; i < 7; i++)
        {
            int num = 304 + i;
            GUILayout.BeginHorizontal();
            if (GUILayout.Button((string)FengGameManagerMKII.settings[num], GUILayout.Width(100f)))
            {
                FengGameManagerMKII.settings[num] = "waiting...";
                FengGameManagerMKII.settings[100] = i + 30;
            }
            if (GUILayout.Button(">", GUILayout.Width(22f)))
            {
                string[] dfsdfs = new string[] { "speedmessage1", "speedmessage2", "speedmessage3", "Message4", "Message5", "Message6", "Message7" };
                FengGameManagerMKII.settings[313 + i] = PlayerPrefs.GetString(dfsdfs[i], "");
            }

            FengGameManagerMKII.settings[313 + i] = GUILayout.TextField((string)FengGameManagerMKII.settings[313 + i]);

            GUILayout.EndHorizontal();
        }
        GUILayout.BeginHorizontal();
        GUICyan.OnRebind(INC.la("speed_restart"), 312, InputCode.speed_rest, 100f);

        GUILayout.Label(INC.la("timer_go_restart"), GUILayout.Width(100f));
        FengGameManagerMKII.settings[302] = GUILayout.TextField((string)FengGameManagerMKII.settings[302]);
        GUILayout.EndHorizontal();
        GUILayout.Label(INC.la("to_chat_l_info"));
        FengGameManagerMKII.settings[329] = GUILayout.SelectionGrid((int)FengGameManagerMKII.settings[329], new string[] { INC.la("to_kill_info_go"), INC.la("to_chat_info_go") }, 2);
        GUICyan.OnRebind(INC.la("scrensht_b"), 370, InputCode.screenshot, 100f);
        GUICyan.OnRebind(INC.la("debug_console_b"), 371, InputCode.debug_console, 100f);
        GUICyan.OnRebind(INC.la("object_list_b"), 372, InputCode.objects_list, 100f);
        GUICyan.OnRebind(INC.la("focused_player"), 404, InputCode.Focus_player, 100f);
        GUILayout.Label(INC.la("chraacters_b"));
        GUILayout.BeginHorizontal();
        if (textures_character == null)
        {
            textures_character = new Texture2D[] { (Texture2D)Statics.CMassets.Load("heart"), (Texture2D)Statics.CMassets.Load("what"), (Texture2D)Statics.CMassets.Load("class"), (Texture2D)Statics.CMassets.Load("class3"), (Texture2D)Statics.CMassets.Load("danger"), (Texture2D)Statics.CMassets.Load("fuck_gacground") };
        }
        for (int i = 0; i < textures_character.Length; i++)
        {
            Texture2D texte = textures_character[i];
            GUILayout.BeginVertical();
            GUILayout.Box(texte, GUILayout.Width(60), GUILayout.Height(60));
            int number = 377 + i;
            if (GUILayout.Button((string)FengGameManagerMKII.settings[number]))
            {
                FengGameManagerMKII.settings[number] = "waiting...";
                FengGameManagerMKII.settings[100] = 30 + 12 + i;
            }
            GUILayout.EndVertical();
        }
            GUILayout.EndHorizontal();
        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }

    void bindRC()
    {
        GUILayout.BeginArea(new Rect(0, 0, 400, 450));

        float labelwidth = 428 / 3;
        GUIStyle style12 = new GUIStyle(GUI.skin.label);
        style12.alignment = TextAnchor.MiddleRight;
        style12.fixedWidth = 180f;
        GUIStyle style1212 = new GUIStyle(GUI.skin.button);
        style1212.alignment = TextAnchor.MiddleLeft;

        GUIStyle binds = new GUIStyle(GUI.skin.button);
        binds.alignment = TextAnchor.UpperCenter;

        GUILayout.Space(5f);
        vectors2[8] = GUILayout.BeginScrollView(vectors2[8]);
        GUILayout.BeginHorizontal();
        string[] liste3w = new string[] { INC.la("human"), INC.la("titan"), INC.la("horse"), INC.la("cannon") };
        for (int i = 0; i < liste3w.Length; i++)
        {
            if (i == (int)FengGameManagerMKII.settings[190])
            {
                binds.normal = GUI.skin.button.onNormal; 
            }
            else
            {
                binds.normal = GUI.skin.button.normal; 
            }
            if (GUILayout.Button(liste3w[i], binds))
            {
                FengGameManagerMKII.settings[190] = i;
            }
        }
        GUILayout.EndHorizontal();
        if ((int)FengGameManagerMKII.settings[190] == 0)
        {
            GUICyan.OnRebindRC(INC.la("forward"), 442, style12, style1212);
            GUICyan.OnRebindRC(INC.la("backward"), 425, style12, style1212);
            GUICyan.OnRebindRC(INC.la("left"), 434, style12, style1212);
            GUICyan.OnRebindRC(INC.la("right"), 439, style12, style1212);
            GUICyan.OnRebindRC(INC.la("jump"), 433, style12, style1212);
            GUICyan.OnRebindRC(INC.la("dodge"), 424, style12, style1212);
            GUICyan.OnRebindRC(INC.la("left_hook"), 435, style12, style1212);
            GUICyan.OnRebindRC(INC.la("right_hook"), 440, style12, style1212);
            GUICyan.OnRebindRC(INC.la("both_hooks"), 422, style12, style1212);
            GUICyan.OnRebindRC(INC.la("lock"), 430, style12, style1212);
            GUICyan.OnRebindRC(INC.la("attack"), 420, style12, style1212);
            GUICyan.OnRebindRC(INC.la("special"), 421, style12, style1212);
            GUICyan.OnRebindRC(INC.la("salute"), 441, style12, style1212);
            GUICyan.OnRebindRC(INC.la("change_camera"), 423, style12, style1212);
            GUICyan.OnRebindRC(INC.la("reset"), 438, style12, style1212);
            GUICyan.OnRebindRC(INC.la("pause"), 436, style12, style1212);
            GUICyan.OnRebindRC(INC.la("show_hide_cursor"), 432, style12, style1212);
            GUICyan.OnRebindRC(INC.la("fullscreen"), 431, style12, style1212);
            GUICyan.OnRebindRC(INC.la("change_blade"), 437, style12, style1212);
            GUICyan.OnRebindRC(INC.la("flare_green"), 426, style12, style1212);
            GUICyan.OnRebindRC(INC.la("flare_red"), 427, style12, style1212);
            GUICyan.OnRebindRC(INC.la("flare_black"), 428, style12, style1212);

           
            GUICyan.OnRebindRC(INC.la("reel_in"), 98, style12, style1212);
            GUICyan.OnRebindRC(INC.la("reel_out"), 99, style12, style1212);
            GUICyan.OnToogleCyan(INC.la("on_gas_burst"), 181, 1, 0, labelwidth);
            if ((int)FengGameManagerMKII.settings[181] == 1)
            {
                GUICyan.OnRebindRC(INC.la("gas_burst_bind"), 182, style12, style1212);
            }
            GUICyan.OnRebindRC(INC.la("minimap_max"), 232, style12, style1212);
            GUICyan.OnRebindRC(INC.la("minimap_toggle"), 233, style12, style1212);
            GUICyan.OnRebindRC(INC.la("minimap_reset"), 234, style12, style1212);
            GUICyan.OnRebindRC(INC.la("open_chat"), 236, style12, style1212);
            GUICyan.OnRebindRC(INC.la("live_spectate"), 262, style12, style1212);
            RebindKey();
        }

        else if ((int)FengGameManagerMKII.settings[190] == 1)
        {
            string[] listrebind_t = new string[] {       INC.la( "forward"),	INC.la("backward"),	INC.la("left"),
							INC.la("right"),	INC.la("walk"),	INC.la("jump"),INC.la("punch"),INC.la("slam"),
							INC.la("grab_front"),	INC.la("grab_back"),	INC.la("grab_nape"),	INC.la("slap"),	INC.la("bite"),
							INC.la("cover_nape") };
            for (int i = 0; i < listrebind_t.Length; i++)
            {
                string namebind = listrebind_t[i];
                GUICyan.OnRebindRC(namebind, 101 + i, style12, style1212);
            }
            GUICyan.OnRebindCyanTitan(INC.la("sit_down_titan"), 405,15, style12, style1212);
            GUICyan.OnRebindCyanTitan(INC.la("laugh_titan"), 406,16, style12, style1212);
            GUICyan.OnRebindCyanTitan(INC.la("attack_down_titan"), 407, 17, style12, style1212);
            GUICyan.OnRebindCyanTitan(INC.la("attack_face_titan"), 408, 18, style12, style1212);
            RebindKeyTitan();
        }
        else if ((int)FengGameManagerMKII.settings[190] == 2)
        {
            string[] rebindhorse = new string[] {      INC.la("forward"),	INC.la("backward"),	INC.la("left"),
							INC.la("right"),	INC.la("walk"),	INC.la("jump"),	INC.la("mount_horse")};
            for (int i = 0; i < rebindhorse.Length; i++)
            {
                string namebind = rebindhorse[i];
                GUICyan.OnRebindRC(namebind, 237 + i, style12, style1212);
            }
            RebindKeyHorse();

        }
        else if ((int)FengGameManagerMKII.settings[190] == 3)
        {
            string[] rebindcanon = new string[] { 
                        INC.la("rotate_up"),   INC.la("rotate_down"),   INC.la("rotate_left"),
						   INC.la(	"rotate_right"),   INC.la(	"fire_cannon"),   INC.la("mount_cannon"),   INC.la("slow_rotate")};


            for (int i = 0; i < rebindcanon.Length; i++)
            {
                string namebind = rebindcanon[i];
                GUICyan.OnRebindRC(namebind, 254 + i, style12, style1212);
            }

            RebindCannon();
        }
        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }
  
    public void saves()
    {
        PrefersCyan.SetInt("int_hook_kill_enable", (int)FengGameManagerMKII.settings[443]);
        PrefersCyan.SetInt("int_camera_type", (int)IN_GAME_MAIN_CAMERA.cameraMode);
        PrefersCyan.SetString("string_custom_attack0", (string)FengGameManagerMKII.settings[420]);
        PrefersCyan.SetString("string_custom_attack1", (string)FengGameManagerMKII.settings[421]);
        PrefersCyan.SetString("string_custom_bothRope", (string)FengGameManagerMKII.settings[422]);
        PrefersCyan.SetString("string_custom_camera", (string)FengGameManagerMKII.settings[423]);
        PrefersCyan.SetString("string_custom_dodge", (string)FengGameManagerMKII.settings[424]);
        PrefersCyan.SetString("string_custom_down", (string)FengGameManagerMKII.settings[425]);
        PrefersCyan.SetString("string_custom_flare1", (string)FengGameManagerMKII.settings[426]);
        PrefersCyan.SetString("string_custom_flare2", (string)FengGameManagerMKII.settings[427]);
        PrefersCyan.SetString("string_custom_flare3", (string)FengGameManagerMKII.settings[428]);
        PrefersCyan.SetString("string_custom_focus", (string)FengGameManagerMKII.settings[430]);
        PrefersCyan.SetString("string_custom_fullscreen", (string)FengGameManagerMKII.settings[431]);
        PrefersCyan.SetString("string_custom_hideCursor", (string)FengGameManagerMKII.settings[432]);
        PrefersCyan.SetString("string_custom_jump", (string)FengGameManagerMKII.settings[433]);
        PrefersCyan.SetString("string_custom_left", (string)FengGameManagerMKII.settings[434]);
        PrefersCyan.SetString("string_custom_leftRope", (string)FengGameManagerMKII.settings[435]);
        PrefersCyan.SetString("string_custom_pause", (string)FengGameManagerMKII.settings[436]);
        PrefersCyan.SetString("string_custom_reload", (string)FengGameManagerMKII.settings[437]);
        PrefersCyan.SetString("string_custom_restart", (string)FengGameManagerMKII.settings[438]);
        PrefersCyan.SetString("string_custom_right", (string)FengGameManagerMKII.settings[439]);
        PrefersCyan.SetString("string_custom_rightRope", (string)FengGameManagerMKII.settings[440]);
        PrefersCyan.SetString("string_custom_salute", (string)FengGameManagerMKII.settings[441]);
        PrefersCyan.SetString("string_custom_up", (string)FengGameManagerMKII.settings[442]);

        PrefersCyan.SetFloat("float_fixed_fov", (float)FengGameManagerMKII.settings[419]);
        PrefersCyan.SetInt("int_fov_enable", (int)FengGameManagerMKII.settings[418]);
        PrefersCyan.SetFloat("float_grafix_lod", (float)FengGameManagerMKII.settings[413]);
        PrefersCyan.SetInt("int_grafix_shadow", (int)FengGameManagerMKII.settings[414]);
        PrefersCyan.SetFloat("float_grafix_shadow_distance", (float)FengGameManagerMKII.settings[415]);
        PrefersCyan.SetInt("int_fog_mode", (int)FengGameManagerMKII.settings[416]);
        PrefersCyan.SetFloat("float_view_distance", (float)FengGameManagerMKII.settings[417]);
        PrefersCyan.SetString("string_single_mapname", (string)FengGameManagerMKII.settings[410]);
        PrefersCyan.SetString("string_single_hero", IN_GAME_MAIN_CAMERA.singleCharacter);
        PrefersCyan.SetInt("int_single_daytime", (int)FengGameManagerMKII.settings[409]);
        PrefersCyan.SetInt("int_single_diffuculty", (int)FengGameManagerMKII.settings[411]);
        PrefersCyan.SetInt("int_costume_value", (int)FengGameManagerMKII.settings[412]);
        PrefersCyan.SetInt("int_panel_single_set", (int)FengGameManagerMKII.settings[365]);
        PrefersCyan.SetInt("int_cuality_settings", cuality_settings);
        PrefersCyan.SetString("string_attack_titan_f", (string)FengGameManagerMKII.settings[408]);
        PrefersCyan.SetString("string_attack_titan_d", (string)FengGameManagerMKII.settings[407]);
        PrefersCyan.SetString("string_sid_down_titan", (string)FengGameManagerMKII.settings[405]);
        PrefersCyan.SetString("string_laugh_titan", (string)FengGameManagerMKII.settings[406]);
        PrefersCyan.SetString("string_button_focus_p", (string)FengGameManagerMKII.settings[404]);
        PrefersCyan.SetInt("int_blend_weinth", (int)FengGameManagerMKII.settings[403]);
        PrefersCyan.SetInt("int_aniso_filter", (int)FengGameManagerMKII.settings[402]);
        PrefersCyan.SetInt("int_grafix_shad_project", (int)FengGameManagerMKII.settings[401]);
        PrefersCyan.SetInt("int_grafix_blur", (int)FengGameManagerMKII.settings[400]);
        PrefersCyan.SetInt("int_current_server", (int)FengGameManagerMKII.settings[399]);
        PrefersCyan.SetInt("int_auto_spam_stats",(int)FengGameManagerMKII.settings[398]);
        PrefersCyan.SetString("string_custom_w_urll", (string)FengGameManagerMKII.settings[394]);
        PrefersCyan.SetString("string_custom_w_urlr", (string)FengGameManagerMKII.settings[395]);
        PrefersCyan.SetInt("int_custom_w_pr", (int)FengGameManagerMKII.settings[396]);
        PrefersCyan.SetColor("color_custom_w_color", (Color)FengGameManagerMKII.settings[397]);
        PrefersCyan.SetInt("int_prop_anti_spam", (int)FengGameManagerMKII.settings[393]);
        PrefersCyan.SetInt("int_cyan_bot", (int)FengGameManagerMKII.settings[392]);
        PrefersCyan.SetColor("color_part_obj_2", (Color)FengGameManagerMKII.settings[391]);
        PrefersCyan.SetColor("color_part_obj_1", (Color)FengGameManagerMKII.settings[390]);
        PrefersCyan.SetColor("color_part_obj_0", (Color)FengGameManagerMKII.settings[389]);
        PrefersCyan.SetInt("int_part_obj_2", (int)FengGameManagerMKII.settings[388]);
        PrefersCyan.SetInt("int_part_obj_1", (int)FengGameManagerMKII.settings[387]);
        PrefersCyan.SetInt("int_part_obj_0", (int)FengGameManagerMKII.settings[386]);
        PrefersCyan.SetInt("int_diss_objects", (int)FengGameManagerMKII.settings[385]);
        PrefersCyan.SetInt("int_conection", (int)FengGameManagerMKII.settings[384]);
        PrefersCyan.SetInt("int_time_in_chat", (int)FengGameManagerMKII.settings[383]);
        PrefersCyan.SetString("string_char_fuck", (string)FengGameManagerMKII.settings[382]);
        PrefersCyan.SetString("string_char_danger", (string)FengGameManagerMKII.settings[381]);
        PrefersCyan.SetString("string_char_class_1", (string)FengGameManagerMKII.settings[380]);
        PrefersCyan.SetString("string_char_class", (string)FengGameManagerMKII.settings[379]);
        PrefersCyan.SetString("string_char_what", (string)FengGameManagerMKII.settings[378]);
        PrefersCyan.SetString("string_char_i_love", (string)FengGameManagerMKII.settings[377]);
        PrefersCyan.SetString("string_screenshot", (string)FengGameManagerMKII.settings[370]);
        PrefersCyan.SetString("string_debug_console", (string)FengGameManagerMKII.settings[371]);
        PrefersCyan.SetString("string_object_list", (string)FengGameManagerMKII.settings[372]);
        PrefersCyan.SetInt("int_clean_hex", (int)FengGameManagerMKII.settings[376]);
        PrefersCyan.SetString("string_first_num", (string)FengGameManagerMKII.settings[373]);
        PrefersCyan.SetString("string_ending_num", (string)FengGameManagerMKII.settings[374]);
        PrefersCyan.SetInt("int_style_system_mess", (int)FengGameManagerMKII.settings[375]);
        PrefersCyan.SetColor("color_system_message", (Color)FengGameManagerMKII.settings[369]);
        PrefersCyan.SetInt("int_private_server", (int)FengGameManagerMKII.settings[368]);
        PrefersCyan.SetString("string_guild_name_1", (string)FengGameManagerMKII.settings[361]);
        PrefersCyan.SetString("string_guild_name_2", (string)FengGameManagerMKII.settings[362]);
        PrefersCyan.SetString("string_chat_name", (string)FengGameManagerMKII.settings[364]);
        PrefersCyan.SetString("string_nick", (string)FengGameManagerMKII.settings[363]);
        PrefersCyan.SetInt("int_infinite_wp", (int)FengGameManagerMKII.settings[360]);
        PrefersCyan.SetVector2("vector2_pos_grafix_kills", (Vector2)FengGameManagerMKII.settings[358]);
        PrefersCyan.SetVector2("vector2_pos_grafix_damage", (Vector2)FengGameManagerMKII.settings[359]);
        PrefersCyan.SetColor("color_sprite_2", (Color)FengGameManagerMKII.settings[357]);
        PrefersCyan.SetColor("color_sprite_1", (Color)FengGameManagerMKII.settings[356]);
        PrefersCyan.SetInt("int_show_damage_in_min", (int)FengGameManagerMKII.settings[355]);
        PrefersCyan.SetInt("int_show_kills_in_min", (int)FengGameManagerMKII.settings[354]);
        PrefersCyan.SetColor("color_background_chat", (Color)FengGameManagerMKII.settings[353]);
        PrefersCyan.SetFloat("float_size_chat", (float)FengGameManagerMKII.settings[352]);
        PrefersCyan.SetString("string_color_multy", (string)FengGameManagerMKII.settings[351]);
        PrefersCyan.SetString("string_max_time_multy", (string)FengGameManagerMKII.settings[349]);
        PrefersCyan.SetString("string_max_player_multy", (string)FengGameManagerMKII.settings[350]);
        PrefersCyan.SetString("string_pass_multy", (string)FengGameManagerMKII.settings[348]);
        PrefersCyan.SetString("string_name_multy", (string)FengGameManagerMKII.settings[347]);
        PrefersCyan.SetInt("int_day_time_multy", (int)FengGameManagerMKII.settings[346]);
        PrefersCyan.SetInt("int_diff_multy", (int)FengGameManagerMKII.settings[345]);
        PrefersCyan.SetString("stringSaving_lvl_multy", (string)FengGameManagerMKII.settings[344]);
        PrefersCyan.SetInt("int_panel_creat_game_cm", (int)FengGameManagerMKII.settings[343]);
        PrefersCyan.SetInt("intCenzureFilter", (int)FengGameManagerMKII.settings[342]);
        PrefersCyan.SetInt("intShowInfoTitans", (int)FengGameManagerMKII.settings[341]);
        PrefersCyan.SetInt("intShowBackgrounds", (int)FengGameManagerMKII.settings[340]);
        PrefersCyan.SetInt("intDisTextOnChat", (int)FengGameManagerMKII.settings[339]);
        PrefersCyan.SetInt("intDisShowChat", (int)FengGameManagerMKII.settings[338]);
        PrefersCyan.SetString("stringMessageOfTheDay", (string)FengGameManagerMKII.settings[337]);
        PrefersCyan.SetInt("intShowStylish", (int)FengGameManagerMKII.settings[336]);
        PrefersCyan.SetInt("intKickTitanPlayer", (int)FengGameManagerMKII.settings[335]);
        PrefersCyan.SetInt("intShowConnectPlayers", (int)FengGameManagerMKII.settings[334]);
        PrefersCyan.SetInt("intShowArmor_FT", (int)FengGameManagerMKII.settings[333]);
        PrefersCyan.SetString("stringArmor_FT", (string)FengGameManagerMKII.settings[332]);
        PrefersCyan.SetInt("intSpawnTitan_FT", (int)FengGameManagerMKII.settings[331]);
        PrefersCyan.SetInt("intAudio_On", (int)FengGameManagerMKII.settings[330]);
        PrefersCyan.SetInt("intInfoToChatGoRest", (int)FengGameManagerMKII.settings[329]);
        PrefersCyan.SetInt("intShowMyNetworkName", (int)FengGameManagerMKII.settings[328]);
        PrefersCyan.SetInt("intShowoldTPSCamera", (int)FengGameManagerMKII.settings[327]);
        PrefersCyan.SetInt("intShowOriginalCamera", (int)FengGameManagerMKII.settings[324]);
        PrefersCyan.SetInt("intShowTPSCamera", (int)FengGameManagerMKII.settings[325]);
        PrefersCyan.SetInt("intShowWOWCamera", (int)FengGameManagerMKII.settings[326]);
        PrefersCyan.SetInt("intshowSSInGame", (int)FengGameManagerMKII.settings[320]);
        PrefersCyan.SetInt("intEnableSS", (int)FengGameManagerMKII.settings[320]);
        PrefersCyan.SetInt("intcameraTilt", (int)FengGameManagerMKII.settings[321]);
        PrefersCyan.SetInt("intinvertMouseY", (int)FengGameManagerMKII.settings[322]);
        PrefersCyan.SetFloat("floatcameraDistance", distanceSlider);
        PrefersCyan.SetFloat("floatMouseSensitivity", IN_GAME_MAIN_CAMERA.sensitivityMulti);
        PrefersCyan.SetFloat("floatvol", AudioListener.volume);
        PrefersCyan.SetInt("intskinQ", QualitySettings.masterTextureLimit);
        PrefersCyan.SetInt("inthuman", (int)FengGameManagerMKII.settings[0]);
        PrefersCyan.SetInt("inttitan", (int)FengGameManagerMKII.settings[1]);
        PrefersCyan.SetInt("intlevel", (int)FengGameManagerMKII.settings[2]);
        PrefersCyan.SetInt("intgasenable", (int)FengGameManagerMKII.settings[15]);
        PrefersCyan.SetInt("inttitantype1", (int)FengGameManagerMKII.settings[16]);
        PrefersCyan.SetInt("inttitantype2", (int)FengGameManagerMKII.settings[17]);
        PrefersCyan.SetInt("inttitantype3", (int)FengGameManagerMKII.settings[18]);
        PrefersCyan.SetInt("inttitantype4", (int)FengGameManagerMKII.settings[19]);
        PrefersCyan.SetInt("inttitantype5", (int)FengGameManagerMKII.settings[20]);
        PrefersCyan.SetString("stringtitanhair1", (string)FengGameManagerMKII.settings[21]);
        PrefersCyan.SetString("stringtitanhair2", (string)FengGameManagerMKII.settings[22]);
        PrefersCyan.SetString("stringtitanhair3", (string)FengGameManagerMKII.settings[23]);
        PrefersCyan.SetString("stringtitanhair4", (string)FengGameManagerMKII.settings[24]);
        PrefersCyan.SetString("stringtitanhair5", (string)FengGameManagerMKII.settings[25]);
        PrefersCyan.SetString("stringtitaneye1", (string)FengGameManagerMKII.settings[26]);
        PrefersCyan.SetString("stringtitaneye2", (string)FengGameManagerMKII.settings[27]);
        PrefersCyan.SetString("stringtitaneye3", (string)FengGameManagerMKII.settings[28]);
        PrefersCyan.SetString("stringtitaneye4", (string)FengGameManagerMKII.settings[29]);
        PrefersCyan.SetString("stringtitaneye5", (string)FengGameManagerMKII.settings[30]);
        PrefersCyan.SetInt("inttitanR", (int)FengGameManagerMKII.settings[32]);
        PrefersCyan.SetString("stringtree1", (string)FengGameManagerMKII.settings[33]);
        PrefersCyan.SetString("stringtree2", (string)FengGameManagerMKII.settings[34]);
        PrefersCyan.SetString("stringtree3", (string)FengGameManagerMKII.settings[35]);
        PrefersCyan.SetString("stringtree4", (string)FengGameManagerMKII.settings[36]);
        PrefersCyan.SetString("stringtree5", (string)FengGameManagerMKII.settings[37]);
        PrefersCyan.SetString("stringtree6", (string)FengGameManagerMKII.settings[38]);
        PrefersCyan.SetString("stringtree7", (string)FengGameManagerMKII.settings[39]);
        PrefersCyan.SetString("stringtree8", (string)FengGameManagerMKII.settings[40]);
        PrefersCyan.SetString("stringleaf1", (string)FengGameManagerMKII.settings[41]);
        PrefersCyan.SetString("stringleaf2", (string)FengGameManagerMKII.settings[42]);
        PrefersCyan.SetString("stringleaf3", (string)FengGameManagerMKII.settings[43]);
        PrefersCyan.SetString("stringleaf4", (string)FengGameManagerMKII.settings[44]);
        PrefersCyan.SetString("stringleaf5", (string)FengGameManagerMKII.settings[45]);
        PrefersCyan.SetString("stringleaf6", (string)FengGameManagerMKII.settings[46]);
        PrefersCyan.SetString("stringleaf7", (string)FengGameManagerMKII.settings[47]);
        PrefersCyan.SetString("stringleaf8", (string)FengGameManagerMKII.settings[48]);
        PrefersCyan.SetString("stringforestG", (string)FengGameManagerMKII.settings[49]);
        PrefersCyan.SetInt("intforestR", (int)FengGameManagerMKII.settings[50]);
        PrefersCyan.SetString("stringhouse1", (string)FengGameManagerMKII.settings[51]);
        PrefersCyan.SetString("stringhouse2", (string)FengGameManagerMKII.settings[52]);
        PrefersCyan.SetString("stringhouse3", (string)FengGameManagerMKII.settings[53]);
        PrefersCyan.SetString("stringhouse4", (string)FengGameManagerMKII.settings[54]);
        PrefersCyan.SetString("stringhouse5", (string)FengGameManagerMKII.settings[55]);
        PrefersCyan.SetString("stringhouse6", (string)FengGameManagerMKII.settings[56]);
        PrefersCyan.SetString("stringhouse7", (string)FengGameManagerMKII.settings[57]);
        PrefersCyan.SetString("stringhouse8", (string)FengGameManagerMKII.settings[58]);
        PrefersCyan.SetString("stringcityG", (string)FengGameManagerMKII.settings[59]);
        PrefersCyan.SetString("stringcityW", (string)FengGameManagerMKII.settings[60]);
        PrefersCyan.SetString("stringcityH", (string)FengGameManagerMKII.settings[61]);
        PrefersCyan.SetInt("intskinQL", (int)FengGameManagerMKII.settings[63]);
        PrefersCyan.SetString("stringeren", (string)FengGameManagerMKII.settings[65]);
        PrefersCyan.SetString("stringannie", (string)FengGameManagerMKII.settings[66]);
        PrefersCyan.SetString("stringcolossal", (string)FengGameManagerMKII.settings[67]);
        PrefersCyan.SetString("stringhoodie", (string)FengGameManagerMKII.settings[14]);
        PrefersCyan.SetString("stringcnumber", (string)FengGameManagerMKII.settings[82]);
        PrefersCyan.SetString("stringcmax", (string)FengGameManagerMKII.settings[85]);
        PrefersCyan.SetString("stringtitanbody1", (string)FengGameManagerMKII.settings[86]);
        PrefersCyan.SetString("stringtitanbody2", (string)FengGameManagerMKII.settings[87]);
        PrefersCyan.SetString("stringtitanbody3", (string)FengGameManagerMKII.settings[88]);
        PrefersCyan.SetString("stringtitanbody4", (string)FengGameManagerMKII.settings[89]);
        PrefersCyan.SetString("stringtitanbody5", (string)FengGameManagerMKII.settings[90]);
        PrefersCyan.SetInt("intcustomlevel", (int)FengGameManagerMKII.settings[91]);
        PrefersCyan.SetInt("inttraildisable", (int)FengGameManagerMKII.settings[92]);
        PrefersCyan.SetInt("intwind", (int)FengGameManagerMKII.settings[93]);
        PrefersCyan.SetString("stringsnapshot", (string)FengGameManagerMKII.settings[95]);
        PrefersCyan.SetInt("intreel", (int)FengGameManagerMKII.settings[97]);
        PrefersCyan.SetString("stringreelin", (string)FengGameManagerMKII.settings[98]);
        PrefersCyan.SetString("stringreelout", (string)FengGameManagerMKII.settings[99]);
        PrefersCyan.SetString("stringtforward", (string)FengGameManagerMKII.settings[101]);
        PrefersCyan.SetString("stringtback", (string)FengGameManagerMKII.settings[102]);
        PrefersCyan.SetString("stringtleft", (string)FengGameManagerMKII.settings[103]);
        PrefersCyan.SetString("stringtright", (string)FengGameManagerMKII.settings[104]);
        PrefersCyan.SetString("stringtwalk", (string)FengGameManagerMKII.settings[105]);
        PrefersCyan.SetString("stringtjump", (string)FengGameManagerMKII.settings[106]);
        PrefersCyan.SetString("stringtpunch", (string)FengGameManagerMKII.settings[107]);
        PrefersCyan.SetString("stringtslam", (string)FengGameManagerMKII.settings[108]);
        PrefersCyan.SetString("stringtgrabfront", (string)FengGameManagerMKII.settings[109]);
        PrefersCyan.SetString("stringtgrabback", (string)FengGameManagerMKII.settings[110]);
        PrefersCyan.SetString("stringtgrabnape", (string)FengGameManagerMKII.settings[111]);
        PrefersCyan.SetString("stringtantiae", (string)FengGameManagerMKII.settings[112]);
        PrefersCyan.SetString("stringtbite", (string)FengGameManagerMKII.settings[113]);
        PrefersCyan.SetString("stringtcover", (string)FengGameManagerMKII.settings[114]);
        PrefersCyan.SetString("stringtsit", (string)FengGameManagerMKII.settings[115]);
        PrefersCyan.SetInt("intreel2", (int)FengGameManagerMKII.settings[116]);
        PrefersCyan.SetInt("inthumangui", (int)FengGameManagerMKII.settings[133]);
        PrefersCyan.SetString("stringcustomGround", (string)FengGameManagerMKII.settings[162]);
        PrefersCyan.SetString("stringforestskyfront", (string)FengGameManagerMKII.settings[163]);
        PrefersCyan.SetString("stringforestskyback", (string)FengGameManagerMKII.settings[164]);
        PrefersCyan.SetString("stringforestskyleft", (string)FengGameManagerMKII.settings[165]);
        PrefersCyan.SetString("stringforestskyright", (string)FengGameManagerMKII.settings[166]);
        PrefersCyan.SetString("stringforestskyup", (string)FengGameManagerMKII.settings[167]);
        PrefersCyan.SetString("stringforestskydown", (string)FengGameManagerMKII.settings[168]);
        PrefersCyan.SetString("stringcityskyfront", (string)FengGameManagerMKII.settings[169]);
        PrefersCyan.SetString("stringcityskyback", (string)FengGameManagerMKII.settings[170]);
        PrefersCyan.SetString("stringcityskyleft", (string)FengGameManagerMKII.settings[171]);
        PrefersCyan.SetString("stringcityskyright", (string)FengGameManagerMKII.settings[172]);
        PrefersCyan.SetString("stringcityskyup", (string)FengGameManagerMKII.settings[173]);
        PrefersCyan.SetString("stringcityskydown", (string)FengGameManagerMKII.settings[174]);
        PrefersCyan.SetString("stringcustomskyfront", (string)FengGameManagerMKII.settings[175]);
        PrefersCyan.SetString("stringcustomskyback", (string)FengGameManagerMKII.settings[176]);
        PrefersCyan.SetString("stringcustomskyleft", (string)FengGameManagerMKII.settings[177]);
        PrefersCyan.SetString("stringcustomskyright", (string)FengGameManagerMKII.settings[178]);
        PrefersCyan.SetString("stringcustomskyup", (string)FengGameManagerMKII.settings[179]);
        PrefersCyan.SetString("stringcustomskydown", (string)FengGameManagerMKII.settings[180]);
        PrefersCyan.SetInt("intdashenable", (int)FengGameManagerMKII.settings[181]);
        PrefersCyan.SetString("stringdashkey", (string)FengGameManagerMKII.settings[182]);
        PrefersCyan.SetInt("intvsync", (int)FengGameManagerMKII.settings[183]);
        PrefersCyan.SetString("stringfpscap", (string)FengGameManagerMKII.settings[184]);
        PrefersCyan.SetInt("intspeedometer", (int)FengGameManagerMKII.settings[189]);
        PrefersCyan.SetInt("intbombMode", (int)FengGameManagerMKII.settings[192]);
        PrefersCyan.SetInt("intteamMode", (int)FengGameManagerMKII.settings[193]);
        PrefersCyan.SetInt("introckThrow", (int)FengGameManagerMKII.settings[194]);
        PrefersCyan.SetInt("intexplodeModeOn", (int)FengGameManagerMKII.settings[195]);
        PrefersCyan.SetString("stringexplodeModeNum", (string)FengGameManagerMKII.settings[196]);
        PrefersCyan.SetInt("inthealthMode", (int)FengGameManagerMKII.settings[197]);
        PrefersCyan.SetString("stringhealthLower", (string)FengGameManagerMKII.settings[198]);
        PrefersCyan.SetString("stringhealthUpper", (string)FengGameManagerMKII.settings[199]);
        PrefersCyan.SetInt("intinfectionModeOn", (int)FengGameManagerMKII.settings[200]);
        PrefersCyan.SetString("stringinfectionModeNum", (string)FengGameManagerMKII.settings[201]);
        PrefersCyan.SetInt("intbanEren", (int)FengGameManagerMKII.settings[202]);
        PrefersCyan.SetInt("intmoreTitanOn", (int)FengGameManagerMKII.settings[203]);
        PrefersCyan.SetString("stringmoreTitanNum", (string)FengGameManagerMKII.settings[204]);
        PrefersCyan.SetInt("intdamageModeOn", (int)FengGameManagerMKII.settings[205]);
        PrefersCyan.SetString("stringdamageModeNum", (string)FengGameManagerMKII.settings[206]);
        PrefersCyan.SetInt("intsizeMode", (int)FengGameManagerMKII.settings[207]);
        PrefersCyan.SetString("stringsizeLower", (string)FengGameManagerMKII.settings[208]);
        PrefersCyan.SetString("stringsizeUpper", (string)FengGameManagerMKII.settings[209]);
        PrefersCyan.SetInt("intspawnModeOn", (int)FengGameManagerMKII.settings[210]);
        PrefersCyan.SetString("stringnRate", (string)FengGameManagerMKII.settings[211]);
        PrefersCyan.SetString("stringaRate", (string)FengGameManagerMKII.settings[212]);
        PrefersCyan.SetString("stringjRate", (string)FengGameManagerMKII.settings[213]);
        PrefersCyan.SetString("stringcRate", (string)FengGameManagerMKII.settings[214]);
        PrefersCyan.SetString("stringpRate", (string)FengGameManagerMKII.settings[215]);
        PrefersCyan.SetInt("inthorseMode", (int)FengGameManagerMKII.settings[216]);
        PrefersCyan.SetInt("intwaveModeOn", (int)FengGameManagerMKII.settings[217]);
        PrefersCyan.SetString("stringwaveModeNum", (string)FengGameManagerMKII.settings[218]);
        PrefersCyan.SetInt("intfriendlyMode", (int)FengGameManagerMKII.settings[219]);
        PrefersCyan.SetInt("intpvpMode", (int)FengGameManagerMKII.settings[220]);
        PrefersCyan.SetInt("intmaxWaveOn", (int)FengGameManagerMKII.settings[221]);
        PrefersCyan.SetString("stringmaxWaveNum", (string)FengGameManagerMKII.settings[222]);
        PrefersCyan.SetInt("intendlessModeOn", (int)FengGameManagerMKII.settings[223]);
        PrefersCyan.SetString("stringendlessModeNum", (string)FengGameManagerMKII.settings[224]);
        PrefersCyan.SetString("stringmotd", (string)FengGameManagerMKII.settings[225]);
        PrefersCyan.SetInt("intpointModeOn", (int)FengGameManagerMKII.settings[226]);
        PrefersCyan.SetString("stringpointModeNum", (string)FengGameManagerMKII.settings[227]);
        PrefersCyan.SetInt("intahssReload", (int)FengGameManagerMKII.settings[228]);
        PrefersCyan.SetInt("intpunkWaves", (int)FengGameManagerMKII.settings[229]);
        PrefersCyan.SetInt("intmapOn", (int)FengGameManagerMKII.settings[231]);
        PrefersCyan.SetString("stringmapMaximize", (string)FengGameManagerMKII.settings[232]);
        PrefersCyan.SetString("stringmapToggle", (string)FengGameManagerMKII.settings[233]);
        PrefersCyan.SetString("stringmapReset", (string)FengGameManagerMKII.settings[234]);
        PrefersCyan.SetInt("intglobalDisableMinimap", (int)FengGameManagerMKII.settings[235]);
        PrefersCyan.SetString("stringchatRebind", (string)FengGameManagerMKII.settings[236]);
        PrefersCyan.SetString("stringhforward", (string)FengGameManagerMKII.settings[237]);
        PrefersCyan.SetString("stringhback", (string)FengGameManagerMKII.settings[238]);
        PrefersCyan.SetString("stringhleft", (string)FengGameManagerMKII.settings[239]);
        PrefersCyan.SetString("stringhright", (string)FengGameManagerMKII.settings[240]);
        PrefersCyan.SetString("stringhwalk", (string)FengGameManagerMKII.settings[241]);
        PrefersCyan.SetString("stringhjump", (string)FengGameManagerMKII.settings[242]);
        PrefersCyan.SetString("stringhmount", (string)FengGameManagerMKII.settings[243]);
        PrefersCyan.SetInt("intchatfeed", (int)FengGameManagerMKII.settings[244]);
        PrefersCyan.SetFloat("floatbombR", (float)FengGameManagerMKII.settings[246]);
        PrefersCyan.SetFloat("floatbombG", (float)FengGameManagerMKII.settings[247]);
        PrefersCyan.SetFloat("floatbombB", (float)FengGameManagerMKII.settings[248]);
        PrefersCyan.SetFloat("floatbombA", (float)FengGameManagerMKII.settings[249]);
        PrefersCyan.SetInt("intbombRadius", (int)FengGameManagerMKII.settings[250]);
        PrefersCyan.SetInt("intbombRange", (int)FengGameManagerMKII.settings[251]);
        PrefersCyan.SetInt("intbombSpeed", (int)FengGameManagerMKII.settings[252]);
        PrefersCyan.SetInt("intbombCD", (int)FengGameManagerMKII.settings[253]);
        PrefersCyan.SetString("stringcannonUp", (string)FengGameManagerMKII.settings[254]);
        PrefersCyan.SetString("stringcannonDown", (string)FengGameManagerMKII.settings[255]);
        PrefersCyan.SetString("stringcannonLeft", (string)FengGameManagerMKII.settings[256]);
        PrefersCyan.SetString("stringcannonRight", (string)FengGameManagerMKII.settings[257]);
        PrefersCyan.SetString("stringcannonFire", (string)FengGameManagerMKII.settings[258]);
        PrefersCyan.SetString("stringcannonMount", (string)FengGameManagerMKII.settings[259]);
        PrefersCyan.SetString("stringcannonSlow", (string)FengGameManagerMKII.settings[260]);
        PrefersCyan.SetInt("intdeadlyCannon", (int)FengGameManagerMKII.settings[261]);
        PrefersCyan.SetString("stringliveCam", (string)FengGameManagerMKII.settings[262]);
        PrefersCyan.SetInt("intOnStyleSetChat", (int)FengGameManagerMKII.settings[264]);
        PrefersCyan.SetString("stringSearhList", (string)FengGameManagerMKII.settings[265]);
        PrefersCyan.SetString("stringMySignInChat", (string)FengGameManagerMKII.settings[266]);
        PrefersCyan.SetInt("intCyanPanelServerList", (int)FengGameManagerMKII.settings[267]);
        PrefersCyan.SetInt("intBBStyleMyMess", (int)FengGameManagerMKII.settings[268]);
        PrefersCyan.SetFloat("floatSnapshotsQuality", (float)FengGameManagerMKII.settings[269]);
        PrefersCyan.SetString("stringFont", (string)FengGameManagerMKII.settings[270]);
        PrefersCyan.SetColor("color_MyColorInChat", (Color)FengGameManagerMKII.settings[271]);
        PrefersCyan.SetInt("intSortingServerList", (int)FengGameManagerMKII.settings[272]);
        PrefersCyan.SetString("stringNameMySkin", (string)FengGameManagerMKII.settings[273]);
        PrefersCyan.SetInt("intOnBackgroundChat", (int)FengGameManagerMKII.settings[274]);
        PrefersCyan.SetInt("intNoPasswordRoom", (int)FengGameManagerMKII.settings[275]);
        PrefersCyan.SetInt("intFullRoom", (int)FengGameManagerMKII.settings[276]);
        PrefersCyan.SetInt("intAutoKickVivid", (int)FengGameManagerMKII.settings[277]);
        PrefersCyan.SetInt("intAutoKickHyper", (int)FengGameManagerMKII.settings[278]);
        PrefersCyan.SetInt("intAutoKickTokyo", (int)FengGameManagerMKII.settings[279]);
        PrefersCyan.SetInt("intFPSCount", (int)FengGameManagerMKII.settings[280]);
        PrefersCyan.SetInt("intBodyLean", (int)FengGameManagerMKII.settings[281]);
        PrefersCyan.SetInt("intGasTrail", (int)FengGameManagerMKII.settings[282]);
        PrefersCyan.SetInt("intAllGasTrail", (int)FengGameManagerMKII.settings[283]);
        PrefersCyan.SetInt("intShowSlashEffect", (int)FengGameManagerMKII.settings[284]);
        PrefersCyan.SetInt("intShowTitanSmokeEffect", (int)FengGameManagerMKII.settings[285]);
        PrefersCyan.SetInt("intNapeMelonEffect", (int)FengGameManagerMKII.settings[286]);
        PrefersCyan.SetInt("intBloodEffect", (int)FengGameManagerMKII.settings[287]);
        PrefersCyan.SetInt("intSmokeTitan_Spawn", (int)FengGameManagerMKII.settings[288]);
        PrefersCyan.SetInt("intShowAIM", (int)FengGameManagerMKII.settings[289]);
        PrefersCyan.SetInt("intShowSprits", (int)FengGameManagerMKII.settings[290]);
        PrefersCyan.SetInt("intShowAllNetworkName", (int)FengGameManagerMKII.settings[291]);
        PrefersCyan.SetInt("intShowChat", (int)FengGameManagerMKII.settings[292]);
        PrefersCyan.SetInt("intShowLabelCenter", (int)FengGameManagerMKII.settings[293]);
        PrefersCyan.SetInt("intShowLabelTopCenter", (int)FengGameManagerMKII.settings[294]);
        PrefersCyan.SetInt("intShowLabelTopLeft", (int)FengGameManagerMKII.settings[295]);
        PrefersCyan.SetInt("intFirstLogin", (int)FengGameManagerMKII.settings[296]);
        PrefersCyan.SetInt("intAutoKickGuest", (int)FengGameManagerMKII.settings[297]);
        PrefersCyan.SetInt("intAutoKickShowGuest", (int)FengGameManagerMKII.settings[298]);
        PrefersCyan.SetInt("intAutoKickNoName", (int)FengGameManagerMKII.settings[299]);
        PrefersCyan.SetInt("intShowAnimationNick", (int)FengGameManagerMKII.settings[300]);

        PrefersCyan.SetString("stringScriptAnimation", (string)FengGameManagerMKII.settings[301]);
        PrefersCyan.SetString("stringTimerGoRestart", (string)FengGameManagerMKII.settings[302]);
        PrefersCyan.SetInt("intShowLabelTopRight", (int)FengGameManagerMKII.settings[303]);
        PrefersCyan.SetString("stringNumMessage_0", (string)FengGameManagerMKII.settings[304]);
        PrefersCyan.SetString("stringNumMessage_1", (string)FengGameManagerMKII.settings[305]);
        PrefersCyan.SetString("stringNumMessage_2", (string)FengGameManagerMKII.settings[306]);
        PrefersCyan.SetString("stringNumMessage_3", (string)FengGameManagerMKII.settings[307]);
        PrefersCyan.SetString("stringNumMessage_4", (string)FengGameManagerMKII.settings[308]);
        PrefersCyan.SetString("stringNumMessage_5", (string)FengGameManagerMKII.settings[309]);
        PrefersCyan.SetString("stringNumMessage_6", (string)FengGameManagerMKII.settings[310]);
        PrefersCyan.SetString("stringGeneralButton", (string)FengGameManagerMKII.settings[311]);
        PrefersCyan.SetString("stringNumSpeedRestart", (string)FengGameManagerMKII.settings[312]);
        PrefersCyan.SetString("stringMess_0", (string)FengGameManagerMKII.settings[313]);
        PrefersCyan.SetString("stringMess_1", (string)FengGameManagerMKII.settings[314]);
        PrefersCyan.SetString("stringMess_2", (string)FengGameManagerMKII.settings[315]);
        PrefersCyan.SetString("stringMess_3", (string)FengGameManagerMKII.settings[316]);
        PrefersCyan.SetString("stringMess_4", (string)FengGameManagerMKII.settings[317]);
        PrefersCyan.SetString("stringMess_5", (string)FengGameManagerMKII.settings[318]);
        PrefersCyan.SetString("stringMess_6", (string)FengGameManagerMKII.settings[319]);
        INC.Save();
    }
    void s_buttons()
    {
        GUILayout.BeginArea(new Rect(Screen.width / 2 - 475, Screen.height / 2 + 221, 950, 30));

        float s = 30;
        GUILayout.BeginHorizontal();
        GUILayout.Label("", GUILayout.Width(200f));
        if (GUILayout.Button(INC.la("save"), GUILayout.Height(s)))
        {
            if (mymenu == 6)
            {
                string streee = string.Empty;
                if (myanim.Count > 0)
                {
                    foreach (AnimN ssddee in myanim)
                    {
                       streee = streee + ssddee.ToString() +",";
                    }
                }
                else
                {
                    foreach (string str34 in Nanimation)
                    {
                        if (str34.Trim() != "")
                        {
                            streee = streee + str34 + ",";
                        }
                    }
                }
                FengGameManagerMKII.settings[301] = streee;
            }
            PanelInformer.instance.Add(INC.la("m_saveds"), PanelInformer.LOG_TYPE.INFORMAION);
            saves();
            ApplySettings();
        }
        if (GUILayout.Button(INC.la("load"), GUILayout.Height(s)))
        {
            INC.instance.LoadSettingMod();
            PanelInformer.instance.Add(INC.la("m_loades"), PanelInformer.LOG_TYPE.INFORMAION);
        }
        else if (GUILayout.Button(INC.la("default"), GUILayout.Height(s)))
        {
            mymenu = 18;
        }
        else if (GUILayout.Button(INC.la("continue"), GUILayout.Height(s)))
        {
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                Time.timeScale = 1f;
            }
            if (!IN_GAME_MAIN_CAMERA.instance.enabled)
            {
                Screen.showCursor = true;
                Screen.lockCursor = true;
                 FengGameManagerMKII.instance.MenuOn = false;
                IN_GAME_MAIN_CAMERA.instance.Smov.disable = false;
                IN_GAME_MAIN_CAMERA.instance.mouselook.disable = false;
            }
            else
            {
                IN_GAME_MAIN_CAMERA.isPausing = false;
                if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.TPS || IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.oldTPS)
                {
                    Screen.showCursor = false;
                    Screen.lockCursor = true;
                }
                else
                {
                    Screen.showCursor = false;
                    Screen.lockCursor = false;
                }
                 FengGameManagerMKII.instance.MenuOn = false;
            }

            ApplySettings();
        }
        else if (GUILayout.Button(INC.la("quit"), GUILayout.Height(s)))
        {
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                Time.timeScale = 1f;
            }
            else
            {
                PhotonNetwork.Disconnect();
            }
            Screen.lockCursor = false;
            Screen.showCursor = true;
            IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.STOP;
            this.gameStart = false;
             FengGameManagerMKII.instance.MenuOn = false;
            this.DestroyAllExistingCloths();
            UnityEngine.Object.Destroy(base.gameObject);
            Application.LoadLevel("menu");
        }

        GUILayout.EndHorizontal();
        GUILayout.EndArea();

    }
 
    void UpdateFilesMapScript()
    {
        fils = new List<FileInfo>();
        DirectoryInfo dee = new DirectoryInfo(path);
        if (!dee.Exists)
        {
            dee.Create();
        }
        foreach (FileInfo fil in dee.GetFiles())
        {
            if (fil.FullName.EndsWith(".txt"))
            {
                fils.Add(fil);
            }
        }
    }
    void UpdateFilesLogicScript()
    {
        logic = new List<FileInfo>();
        DirectoryInfo dee = new DirectoryInfo(pathLogic);
        if (!dee.Exists)
        {
            dee.Create();
        }
        foreach (FileInfo fil in dee.GetFiles())
        {
            if (fil.FullName.EndsWith(".txt"))
            {
                logic.Add(fil);
            }
        }
    }

    void map_settings()
    {
        if (size_font_scripts == 0)
        {
            size_font_scripts = GUI.skin.textArea.fontSize;
        }
        GUILayout.BeginArea(new Rect(Screen.width / 2 - 370, Screen.height / 2 - 250, 840, 450), GUI.skin.box);
        string[] countspanel = new string[] { INC.la("map_settings"), INC.la("logic_script") };
        int sss = countspanelcustom;
        countspanelcustom = GUILayout.SelectionGrid(countspanelcustom, countspanel, 2, GUILayout.Width(250f));
        if (sss != countspanelcustom)
        {
            is_file_exist = false;
        }
        GUILayout.BeginHorizontal();
        if (countspanelcustom == 0)
        {
            GUILayout.BeginVertical(GUILayout.Width(230f));
            GUILayout.Box(INC.la("settings_map"));
            vectors2[13] = GUILayout.BeginScrollView(vectors2[13]);
            GUICyan.OnTextFileCyan(INC.la("titan_spawn_cap"), 85, 150f);
            string[] texts = new string[] { INC.la("1_round"), INC.la("waves"), INC.la("pvp"), INC.la("racing"), INC.la("custom") };
            RCSettings.gameType = GUILayout.SelectionGrid(RCSettings.gameType, texts, 2, GUI.skin.button);
            GUILayout.Box(INC.la("textures_sky_box"));
            GUILayout.Label(INC.la("ground_skin"));
            FengGameManagerMKII.settings[162] = GUILayout.TextField((string)FengGameManagerMKII.settings[162]);
            GUILayout.Label(INC.la("skybox_front"));
            FengGameManagerMKII.settings[175] = GUILayout.TextField((string)FengGameManagerMKII.settings[175]);
            GUILayout.Label(INC.la("skybox_back"));
            FengGameManagerMKII.settings[176] = GUILayout.TextField((string)FengGameManagerMKII.settings[176]);
            GUILayout.Label(INC.la("skybox_left"));
            FengGameManagerMKII.settings[177] = GUILayout.TextField((string)FengGameManagerMKII.settings[177]);
            GUILayout.Label(INC.la("skybox_right"));
            FengGameManagerMKII.settings[178] = GUILayout.TextField((string)FengGameManagerMKII.settings[178]);
            GUILayout.Label(INC.la("skybox_up"));
            FengGameManagerMKII.settings[179] = GUILayout.TextField((string)FengGameManagerMKII.settings[179]);
            GUILayout.Label(INC.la("skybox_down"));
            FengGameManagerMKII.settings[180] = GUILayout.TextField((string)FengGameManagerMKII.settings[180]);
            GUILayout.EndScrollView();
            GUILayout.EndVertical();

            GUILayout.BeginVertical();
            GUILayout.Space(0);
            GUILayout.Box(INC.la("level_script"));
            vectors2[12] = GUILayout.BeginScrollView(vectors2[12]);
            GUIStyle style77 = new GUIStyle(GUI.skin.textArea);
            style77.fontSize = size_font_scripts;
            FengGameManagerMKII.currentScript = GUILayout.TextArea(FengGameManagerMKII.currentScript, style77);
            GUILayout.EndScrollView();
            GUILayout.EndVertical();

            GUILayout.BeginVertical(GUILayout.Width(230f));
            GUILayout.Space(0);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(INC.la("copy")))
            {
                TextEditor textEditor = new TextEditor();
                textEditor.content = new GUIContent(FengGameManagerMKII.currentScript);
                textEditor.SelectAll();
                textEditor.Copy();
                PanelInformer.instance.Add(INC.la("copyed") + FengGameManagerMKII.currentScript.Length, PanelInformer.LOG_TYPE.INFORMAION);
            }
            else if (GUILayout.Button(INC.la("clear")))
            {
                FengGameManagerMKII.currentScript = string.Empty;
                PanelInformer.instance.Add(INC.la("cleared"), PanelInformer.LOG_TYPE.INFORMAION);
            }
            GUILayout.EndHorizontal();
            size_font_scripts = (int)GUILayout.HorizontalSlider((float)size_font_scripts, 4, 25);
            if (fils == null)
            {
                UpdateFilesMapScript();
            }
            if (GUILayout.Button(INC.la("update_list_script")))
            {
                UpdateFilesMapScript();
                PanelInformer.instance.Add(INC.la("list_updated"), PanelInformer.LOG_TYPE.INFORMAION);
            }
            filtermapScript = GUILayout.TextField(filtermapScript);
            vectors2[24] = GUILayout.BeginScrollView(vectors2[24]);
            GUIStyle style = new GUIStyle(GUI.skin.button);
            style.alignment = TextAnchor.UpperLeft;
            if (fils.Count > 0)
            {
                foreach (FileInfo filed in fils)
                {
                    if (filtermapScript == "" || filed.Name.Trim().ToLower().Contains(filtermapScript.Trim().ToLower()))
                    {
                        GUILayout.BeginHorizontal();
                        if (GUILayout.Button(filed.Name + "\n" + ((double)filed.Length / (double)1024).ToString("F") + "kb" + " LW:" + filed.LastWriteTime.ToString("yyyy/MM/dd"), style))
                        {
                            if (filed.Exists)
                            {
                                FengGameManagerMKII.currentScript = File.ReadAllText(filed.FullName).Trim();
                                PanelInformer.instance.Add(INC.la("load_file") + filed.Name, PanelInformer.LOG_TYPE.INFORMAION);
                            }
                            else
                            {
                                PanelInformer.instance.Add(INC.la("file_not_found"), PanelInformer.LOG_TYPE.DANGER);

                            }
                        }
                        if (GUILayout.Button("x", GUILayout.Width(24f)))
                        {
                            PanelInformer.instance.Add(INC.la("file_dell_name") + filed.Name, PanelInformer.LOG_TYPE.INFORMAION);
                            if (filed.Exists)
                            {
                                filed.Delete();
                            }
                            UpdateFilesMapScript();
                            return;
                        }
                        GUILayout.EndHorizontal();
                    }
                }
            }
            else
            {
                GUILayout.Label(INC.la("no_files"));
            }
            GUILayout.EndScrollView();
            if (!is_file_exist)
            {
                script_name = GUILayout.TextField(script_name);
                if (GUILayout.Button(INC.la("save")))
                {
                    if (script_name.Trim() != "")
                    {
                        if (FengGameManagerMKII.currentScript.Trim() != "")
                        {
                            string path_local = path + script_name.Trim() + ".txt";
                            FileInfo info = new FileInfo(path_local);
                            if (info.Exists)
                            {
                                is_file_exist = true;
                                PanelInformer.instance.Add(INC.la("error_exisets_file"), PanelInformer.LOG_TYPE.DANGER);
                            }
                            else
                            {
                                File.WriteAllText(path_local, FengGameManagerMKII.currentScript.Trim(), Encoding.UTF8);
                                PanelInformer.instance.Add(INC.la("saved_file") + script_name.Trim() + ".txt", PanelInformer.LOG_TYPE.INFORMAION);
                            }
                        }
                        else
                        {
                            PanelInformer.instance.Add(INC.la("script_empty"), PanelInformer.LOG_TYPE.WARNING);
                        }
                    }
                    else
                    {
                        PanelInformer.instance.Add(INC.la("name_file_empty"), PanelInformer.LOG_TYPE.WARNING);
                    }
                    UpdateFilesMapScript();
                }
            }
            else
            {
                GUILayout.Label(INC.la("file_exiset"));
                GUILayout.BeginHorizontal();
                if (GUILayout.Button(INC.la("yes")))
                {
                    string path_local = path + script_name + ".txt";
                    FileInfo info = new FileInfo(path_local);
                    if (info.Exists)
                    {
                        info.Delete();
                    }
                    File.WriteAllText(path_local, FengGameManagerMKII.currentScript.Trim(), Encoding.UTF8);
                    PanelInformer.instance.Add(INC.la("atd_file"), PanelInformer.LOG_TYPE.INFORMAION);
                    is_file_exist = false;
                    UpdateFilesMapScript();
                }
                if (GUILayout.Button(INC.la("no")))
                {
                    is_file_exist = false;
                    UpdateFilesMapScript();
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();
        }
        else
        {
            logicscript();
        }

        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
    void logicscript()
    {
       
            GUILayout.BeginVertical();
            GUILayout.Box(INC.la("logic_script"));
            vectors2[26] = GUILayout.BeginScrollView(vectors2[26]);
            GUIStyle style77 = new GUIStyle(GUI.skin.textArea);
            style77.fontSize = size_font_scripts;
            FengGameManagerMKII.currentScriptLogic = GUILayout.TextArea(FengGameManagerMKII.currentScriptLogic, style77);
            GUILayout.EndScrollView();
            GUILayout.EndVertical();

            GUILayout.BeginVertical(GUILayout.Width(230f));
            GUILayout.Space(0);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(INC.la("copy")))
            {
                TextEditor textEditor = new TextEditor();
                textEditor.content = new GUIContent(FengGameManagerMKII.currentScriptLogic);
                textEditor.SelectAll();
                textEditor.Copy();
                PanelInformer.instance.Add(INC.la("copyed") + FengGameManagerMKII.currentScriptLogic.Length, PanelInformer.LOG_TYPE.INFORMAION);
            }
            else if (GUILayout.Button(INC.la("clear")))
            {
                FengGameManagerMKII.currentScriptLogic = string.Empty;
                PanelInformer.instance.Add(INC.la("cleared"), PanelInformer.LOG_TYPE.INFORMAION);
            }
            GUILayout.EndHorizontal();
            size_font_scripts = (int)GUILayout.HorizontalSlider((float)size_font_scripts, 4, 25);
            if (logic == null)
            {
                UpdateFilesLogicScript();
            }
            if (GUILayout.Button(INC.la("update_list_script")))
            {
                UpdateFilesLogicScript();
                PanelInformer.instance.Add(INC.la("list_updated"), PanelInformer.LOG_TYPE.INFORMAION);
            }
            filterlogicScript = GUILayout.TextField(filterlogicScript);
            vectors2[25] = GUILayout.BeginScrollView(vectors2[25]);
            GUIStyle style = new GUIStyle(GUI.skin.button);
            style.alignment = TextAnchor.UpperLeft;
            if (logic.Count > 0)
            {
                foreach (FileInfo filed in logic)
                {
                    if (filterlogicScript == "" || filed.Name.Trim().ToLower().Contains(filterlogicScript.Trim().ToLower()))
                    {
                        GUILayout.BeginHorizontal();
                        if (GUILayout.Button(filed.Name + "\n" + ((double)filed.Length / (double)1024).ToString("F") + "kb" + " LW:" + filed.LastWriteTime.ToString("yyyy/MM/dd"), style))
                        {
                            if (filed.Exists)
                            {
                                FengGameManagerMKII.currentScriptLogic = File.ReadAllText(filed.FullName).Trim();
                                PanelInformer.instance.Add(INC.la("load_file") + filed.Name, PanelInformer.LOG_TYPE.INFORMAION);
                            }
                            else
                            {
                                PanelInformer.instance.Add(INC.la("file_not_found"), PanelInformer.LOG_TYPE.WARNING);

                            }
                        }
                        if (GUILayout.Button("x", GUILayout.Width(24f)))
                        {
                            PanelInformer.instance.Add(INC.la("file_dell_name") + filed.Name, PanelInformer.LOG_TYPE.INFORMAION);
                            if (filed.Exists)
                            {
                                filed.Delete();
                            }
                            UpdateFilesLogicScript();
                            return;
                        }
                        GUILayout.EndHorizontal();
                    }
                }
            }
            else
            {
                GUILayout.Label(INC.la("no_files"));
            }
            GUILayout.EndScrollView();
            if (!is_file_exist)
            {
                logic_script_name = GUILayout.TextField(logic_script_name);
                if (GUILayout.Button(INC.la("save")))
                {
                    if (logic_script_name.Trim() != "")
                    {
                        if (FengGameManagerMKII.currentScriptLogic.Trim() != "")
                        {
                            string path_local = pathLogic + logic_script_name.Trim() + ".txt";
                            FileInfo info = new FileInfo(path_local);
                            if (info.Exists)
                            {
                                is_file_exist = true;
                                PanelInformer.instance.Add(INC.la("error_exisets_file"), PanelInformer.LOG_TYPE.DANGER);
                            }
                            else
                            {
                                File.WriteAllText(path_local, FengGameManagerMKII.currentScriptLogic.Trim(), Encoding.UTF8);
                                PanelInformer.instance.Add(INC.la("saved_file") + logic_script_name.Trim() + ".txt", PanelInformer.LOG_TYPE.INFORMAION);
                            }
                        }
                        else
                        {
                            PanelInformer.instance.Add(INC.la("script_empty"), PanelInformer.LOG_TYPE.WARNING);
                        }
                    }
                    else
                    {
                        PanelInformer.instance.Add(INC.la("name_file_empty"), PanelInformer.LOG_TYPE.WARNING);
                    }
                    UpdateFilesLogicScript();
                }
            }
            else
            {
                GUILayout.Label(INC.la("file_exiset"));
                GUILayout.BeginHorizontal();
                if (GUILayout.Button(INC.la("yes")))
                {
                    string path_local = pathLogic + logic_script_name + ".txt";
                    FileInfo info = new FileInfo(path_local);
                    if (info.Exists)
                    {
                        info.Delete();
                    }
                    File.WriteAllText(path_local, FengGameManagerMKII.currentScriptLogic.Trim(), Encoding.UTF8);
                    PanelInformer.instance.Add(INC.la("atd_file"), PanelInformer.LOG_TYPE.INFORMAION);
                    is_file_exist = false;
                    UpdateFilesLogicScript();
                }
                if (GUILayout.Button(INC.la("no")))
                {
                    is_file_exist = false;
                    UpdateFilesLogicScript();
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();
        
    }

    void reset()
    {
        FengGameManagerMKII.settings[192] = 0;
        FengGameManagerMKII.settings[193] = 0;
        FengGameManagerMKII.settings[194] = 0;
        FengGameManagerMKII.settings[195] = 0;
        FengGameManagerMKII.settings[196] = "30";
        FengGameManagerMKII.settings[197] = 0;
        FengGameManagerMKII.settings[198] = "100";
        FengGameManagerMKII.settings[199] = "200";
        FengGameManagerMKII.settings[200] = 0;
        FengGameManagerMKII.settings[201] = "1";
        FengGameManagerMKII.settings[202] = 0;
        FengGameManagerMKII.settings[203] = 0;
        FengGameManagerMKII.settings[204] = "1";
        FengGameManagerMKII.settings[205] = 0;
        FengGameManagerMKII.settings[206] = "1000";
        FengGameManagerMKII.settings[207] = 0;
        FengGameManagerMKII.settings[208] = "1.0";
        FengGameManagerMKII.settings[209] = "3.0";
        FengGameManagerMKII.settings[210] = 0;
        FengGameManagerMKII.settings[211] = "20.0";
        FengGameManagerMKII.settings[212] = "20.0";
        FengGameManagerMKII.settings[213] = "20.0";
        FengGameManagerMKII.settings[214] = "20.0";
        FengGameManagerMKII.settings[215] = "20.0";
        FengGameManagerMKII.settings[216] = 0;
        FengGameManagerMKII.settings[217] = 0;
        FengGameManagerMKII.settings[218] = "1";
        FengGameManagerMKII.settings[219] = 0;
        FengGameManagerMKII.settings[220] = 0;
        FengGameManagerMKII.settings[221] = 0;
        FengGameManagerMKII.settings[222] = "20";
        FengGameManagerMKII.settings[223] = 0;
        FengGameManagerMKII.settings[224] = "10";
        FengGameManagerMKII.settings[225] = string.Empty;
        FengGameManagerMKII.settings[226] = 0;
        FengGameManagerMKII.settings[227] = "50";
        FengGameManagerMKII.settings[228] = 0;
        FengGameManagerMKII.settings[229] = 0;
        FengGameManagerMKII.settings[235] = 0;
    }
    void RebindKey()
    {
        if ((int)FengGameManagerMKII.settings[100] != 0)
        {
            Event current = Event.current;
            bool flag3 = false;
            string text3 = "waiting...";
            if (current.type == EventType.KeyDown && current.keyCode != KeyCode.None)
            {
                flag3 = true;
                text3 = current.keyCode.ToString();
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                flag3 = true;
                text3 = KeyCode.LeftShift.ToString();
            }
            else if (Input.GetKey(KeyCode.RightShift))
            {
                flag3 = true;
                text3 = KeyCode.RightShift.ToString();
            }
            else if (Input.GetAxis("Mouse ScrollWheel") != 0f)
            {
                if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                {
                    flag3 = true;
                    text3 = "Scroll Up";
                }
                else
                {
                    flag3 = true;
                    text3 = "Scroll Down";
                }
            }
            else
            {
                for (int j = 0; j < 7; j++)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0 + j))
                    {
                        flag3 = true;
                        text3 = "Mouse" + Convert.ToString(j);
                    }
                }
            }
            if (flag3)
            {
                for (int s = 0; s < 22; s++)
                {
                    if ((int)FengGameManagerMKII.settings[100] == 420 + s)
                    {
                        FengGameManagerMKII.settings[420 + s] = text3;
                        FengGameManagerMKII.settings[100] = 0;
                        FengGameManagerMKII.inputRC.setInputCustom(s, text3);
                    }
                }
                    if ((int)FengGameManagerMKII.settings[100] == 98)
                    {
                        FengGameManagerMKII.settings[98] = text3;
                        FengGameManagerMKII.settings[100] = 0;
                        FengGameManagerMKII.inputRC.setInputHuman(InputCodeRC.reelin, text3);
                    }
                    else if ((int)FengGameManagerMKII.settings[100] == 99)
                    {
                        FengGameManagerMKII.settings[99] = text3;
                        FengGameManagerMKII.settings[100] = 0;
                        FengGameManagerMKII.inputRC.setInputHuman(InputCodeRC.reelout, text3);
                    }
                    else if ((int)FengGameManagerMKII.settings[100] == 182)
                    {
                        FengGameManagerMKII.settings[182] = text3;
                        FengGameManagerMKII.settings[100] = 0;
                        FengGameManagerMKII.inputRC.setInputHuman(InputCodeRC.dash, text3);
                    }
                    else if ((int)FengGameManagerMKII.settings[100] == 232)
                    {
                        FengGameManagerMKII.settings[232] = text3;
                        FengGameManagerMKII.settings[100] = 0;
                        FengGameManagerMKII.inputRC.setInputHuman(InputCodeRC.mapMaximize, text3);
                    }
                    else if ((int)FengGameManagerMKII.settings[100] == 233)
                    {
                        FengGameManagerMKII.settings[233] = text3;
                        FengGameManagerMKII.settings[100] = 0;
                        FengGameManagerMKII.inputRC.setInputHuman(InputCodeRC.mapToggle, text3);
                    }
                    else if ((int)FengGameManagerMKII.settings[100] == 234)
                    {
                        FengGameManagerMKII.settings[234] = text3;
                        FengGameManagerMKII.settings[100] = 0;
                        FengGameManagerMKII.inputRC.setInputHuman(InputCodeRC.mapReset, text3);
                    }
                    else if ((int)FengGameManagerMKII.settings[100] == 236)
                    {
                        FengGameManagerMKII.settings[236] = text3;
                        FengGameManagerMKII.settings[100] = 0;
                        FengGameManagerMKII.inputRC.setInputHuman(InputCodeRC.chat, text3);
                    }
                    else if ((int)FengGameManagerMKII.settings[100] == 262)
                    {
                        FengGameManagerMKII.settings[262] = text3;
                        FengGameManagerMKII.settings[100] = 0;
                        FengGameManagerMKII.inputRC.setInputHuman(InputCodeRC.liveCam, text3);
                    }
                    else if ((int)FengGameManagerMKII.settings[100] >= 30 && (int)FengGameManagerMKII.settings[100] <= 50)
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            if ((int)FengGameManagerMKII.settings[100] == i + 30)
                            {
                                FengGameManagerMKII.settings[304 + i] = text3;
                                FengGameManagerMKII.settings[100] = 0;
                                FengGameManagerMKII.inputRC.setInputCyan(i, text3);
                            }
                        }
                        if ((int)FengGameManagerMKII.settings[100] == 30 + InputCode.buttonX)
                        {
                            FengGameManagerMKII.settings[311] = text3;
                            FengGameManagerMKII.settings[100] = 0;
                            FengGameManagerMKII.inputRC.setInputCyan(InputCode.buttonX, text3);
                        }
                        if ((int)FengGameManagerMKII.settings[100] == 30 + InputCode.speed_rest)
                        {
                            FengGameManagerMKII.settings[312] = text3;
                            FengGameManagerMKII.settings[100] = 0;
                            FengGameManagerMKII.inputRC.setInputCyan(InputCode.speed_rest, text3);
                        }
                        if ((int)FengGameManagerMKII.settings[100] == 30 + InputCode.screenshot)
                        {
                            FengGameManagerMKII.settings[370] = text3;
                            FengGameManagerMKII.settings[100] = 0;
                            FengGameManagerMKII.inputRC.setInputCyan(InputCode.screenshot, text3);
                        }
                        if ((int)FengGameManagerMKII.settings[100] == 30 + InputCode.debug_console)
                        {
                            FengGameManagerMKII.settings[371] = text3;
                            FengGameManagerMKII.settings[100] = 0;
                            FengGameManagerMKII.inputRC.setInputCyan(InputCode.debug_console, text3);
                        }
                        if ((int)FengGameManagerMKII.settings[100] == 30 + InputCode.Focus_player)
                        {
                            FengGameManagerMKII.settings[404] = text3;
                            FengGameManagerMKII.settings[100] = 0;
                            FengGameManagerMKII.inputRC.setInputCyan(InputCode.Focus_player, text3);
                        }
                        if ((int)FengGameManagerMKII.settings[100] == 30 + InputCode.objects_list)
                        {
                            FengGameManagerMKII.settings[372] = text3;
                            FengGameManagerMKII.settings[100] = 0;
                            FengGameManagerMKII.inputRC.setInputCyan(InputCode.objects_list, text3);
                        }
                        for (int s = 0; s < 6; s++)
                        {
                            int num = 12 + s;
                            if ((int)FengGameManagerMKII.settings[100] == 30 + num)
                            {
                                FengGameManagerMKII.settings[377 + s] = text3;
                                FengGameManagerMKII.settings[100] = 0;
                                FengGameManagerMKII.inputRC.setInputCyan(num, text3);
                            }
                        }
                    }
                  
            }
        }
    }
    void RebindKeyTitan()
    {
        if ((int)FengGameManagerMKII.settings[100] != 0)
        {
            Event current = Event.current;
            bool flag3 = false;
            string text3 = "waiting...";
            if (current.type == EventType.KeyDown && current.keyCode != KeyCode.None)
            {
                flag3 = true;
                text3 = current.keyCode.ToString();
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                flag3 = true;
                text3 = KeyCode.LeftShift.ToString();
            }
            else if (Input.GetKey(KeyCode.RightShift))
            {
                flag3 = true;
                text3 = KeyCode.RightShift.ToString();
            }
            else if (Input.GetAxis("Mouse ScrollWheel") != 0f)
            {
                if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                {
                    flag3 = true;
                    text3 = "Scroll Up";
                }
                else
                {
                    flag3 = true;
                    text3 = "Scroll Down";
                }
            }
            else
            {
                for (int j = 0; j < 7; j++)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0 + j))
                    {
                        flag3 = true;
                        text3 = "Mouse" + Convert.ToString(j);
                    }
                }
            }
            if (flag3)
            {
                for (int j = 0; j < 14; j++)
                {
                    int num16 = 101 + j;
                    if ((int)FengGameManagerMKII.settings[100] == num16)
                    {
                        FengGameManagerMKII.settings[num16] = text3;
                        FengGameManagerMKII.settings[100] = 0;
                        FengGameManagerMKII.inputRC.setInputTitan(j, text3);
                    }
                }
                if ((int)FengGameManagerMKII.settings[100] == 15)
                {
                    FengGameManagerMKII.settings[405] = text3;
                    FengGameManagerMKII.settings[100] = 0;
                    FengGameManagerMKII.inputRC.setInputTitan(InputCodeRC.titanSitDown, text3);
                }
                if ((int)FengGameManagerMKII.settings[100] == 16)
                {
                    FengGameManagerMKII.settings[406] = text3;
                    FengGameManagerMKII.settings[100] = 0;
                    FengGameManagerMKII.inputRC.setInputTitan(InputCodeRC.titanlaugh, text3);
                }
                if ((int)FengGameManagerMKII.settings[100] == 17)
                {
                    FengGameManagerMKII.settings[407] = text3;
                    FengGameManagerMKII.settings[100] = 0;
                    FengGameManagerMKII.inputRC.setInputTitan(InputCodeRC.titanAttackDown, text3);
                }
                if ((int)FengGameManagerMKII.settings[100] == 18)
                {
                    FengGameManagerMKII.settings[408] = text3;
                    FengGameManagerMKII.settings[100] = 0;
                    FengGameManagerMKII.inputRC.setInputTitan(InputCodeRC.titanAttackFace, text3);
                }

            }
        }
    }
    void RebindKeyHorse()
    {
        if ((int)FengGameManagerMKII.settings[100] != 0)
        {
            Event current = Event.current;
            bool flag3 = false;
            string text3 = "waiting...";
            if (current.type == EventType.KeyDown && current.keyCode != KeyCode.None)
            {
                flag3 = true;
                text3 = current.keyCode.ToString();
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                flag3 = true;
                text3 = KeyCode.LeftShift.ToString();
            }
            else if (Input.GetKey(KeyCode.RightShift))
            {
                flag3 = true;
                text3 = KeyCode.RightShift.ToString();
            }
            else if (Input.GetAxis("Mouse ScrollWheel") != 0f)
            {
                if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                {
                    flag3 = true;
                    text3 = "Scroll Up";
                }
                else
                {
                    flag3 = true;
                    text3 = "Scroll Down";
                }
            }
            else
            {
                for (int j = 0; j < 7; j++)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0 + j))
                    {
                        flag3 = true;
                        text3 = "Mouse" + Convert.ToString(j);
                    }
                }
            }
            if (flag3)
            {
                for (int j = 0; j < 7; j++)
                {
                    int num16 = 237 + j;
                    if ((int)FengGameManagerMKII.settings[100] == num16)
                    {
                        FengGameManagerMKII.settings[num16] = text3;
                        FengGameManagerMKII.settings[100] = 0;
                        FengGameManagerMKII.inputRC.setInputHorse(j, text3);
                    }
                }
            }
        }
    }
    void RebindCannon()
    {
        if ((int)FengGameManagerMKII.settings[100] != 0)
        {
            Event current = Event.current;
            bool flag3 = false;
            string text3 = "waiting...";
            if (current.type == EventType.KeyDown && current.keyCode != KeyCode.None)
            {
                flag3 = true;
                text3 = current.keyCode.ToString();
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                flag3 = true;
                text3 = KeyCode.LeftShift.ToString();
            }
            else if (Input.GetKey(KeyCode.RightShift))
            {
                flag3 = true;
                text3 = KeyCode.RightShift.ToString();
            }
            else if (Input.GetAxis("Mouse ScrollWheel") != 0f)
            {
                if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                {
                    flag3 = true;
                    text3 = "Scroll Up";
                }
                else
                {
                    flag3 = true;
                    text3 = "Scroll Down";
                }
            }
            else
            {
                for (int j = 0; j < 6; j++)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0 + j))
                    {
                        flag3 = true;
                        text3 = "Mouse" + Convert.ToString(j);
                    }
                }
            }
            if (flag3)
            {
                for (int j = 0; j < 7; j++)
                {
                    int num16 = 254 + j;
                    if ((int)FengGameManagerMKII.settings[100] == num16)
                    {
                        FengGameManagerMKII.settings[num16] = text3;
                        FengGameManagerMKII.settings[100] = 0;
                        FengGameManagerMKII.inputRC.setInputCannon(j, text3);
                    }
                }
            }
        }
    }
    
    public float titanspawners(int num)
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

        if (num == 0)
        {
            return 100f - (t_aberrant + t_jumper + t_crawler + t_punk);
        }
        else if (num == 1)
        {
            return 100f - (t_normal + t_jumper + t_crawler + t_punk);
        }
        else if (num == 2)
        {
            return 100f - (t_normal + t_aberrant + t_crawler + t_punk);
        }
        else if (num == 3)
        {
            return 100f - (t_normal + t_aberrant + t_jumper + t_punk);
        }
        else
        {
            return 100f - (t_normal + t_aberrant + t_jumper + t_crawler);
        }
    }
  

    void CommandsInfo()
    {
        if (command == null)
        {
            command = new List<int>();
        }
        if (InRoomChat.instance.commands == null)
        {
            InRoomChat.instance.ComAdds();
        }
        GUILayout.BeginArea(new Rect(Screen.width / 2 - 370, Screen.height / 2 - 250, 840, 450), GUI.skin.box);
        GUILayout.Box(INC.la("command_list_m"));
        vectors2[3] = GUILayout.BeginScrollView(vectors2[3]);
        if (basic_version_cm)
        {
            GUILayout.Label(INC.la("command_info_copy_i"));
            string str32 = string.Empty;
            foreach (InRoomChat.Commands comm in InRoomChat.instance.commands)
            {
                str32 = str32 + comm.ToString("\n");
            }
            GUILayout.TextArea(str32);
        }
        else
        {
            GUIStyle ns = new GUIStyle();
            ns.alignment = TextAnchor.MiddleLeft;
            ns.normal.textColor = GUI.skin.button.normal.textColor;
            for (int i = 0; i < InRoomChat.instance.commands.Count; i++)
            {
                InRoomChat.Commands comm = InRoomChat.instance.commands[i];
                bool cnt = command.Contains(i);
                GUILayout.BeginVertical(GUI.skin.box);
                if (GUILayout.Button("<size=16>" + comm.command + (comm.m ? "  ONLY ADMIN" : "") + "</size>", ns, GUILayout.Height(26f)))
                {
                    if (cnt)
                    {
                        command.Remove(i);
                        return;
                    }
                    command.Add(i);
                }
                if (cnt)
                {
                    GUILayout.Label(INC.la("command_i") + comm.command);
                    GUILayout.Label(INC.la("descri_i") + comm.description);
                    GUILayout.BeginHorizontal();
                    GUILayout.Label(INC.la("example_i") + comm.example, GUILayout.Width(500f));
                    if (GUILayout.Button(INC.la("copy"), GUILayout.Width(100f)))
                    {
                        TextEditor textEditor = new TextEditor();
                        textEditor.content = new GUIContent(comm.example);
                        textEditor.SelectAll();
                        textEditor.Copy();
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.Label((comm.m ? INC.la("onismc_i") : INC.la("offismc_i")));
                }
                GUILayout.EndVertical();
            }
        }
        GUILayout.EndScrollView();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button(INC.la("back"), GUILayout.Width(100f)))
        {
            mymenu = 0;
        }
        if (GUILayout.Button(basic_version_cm ? INC.la("basic_version_co") : INC.la("copy_version_co"), GUILayout.Width(200f)))
        {
            basic_version_cm = !basic_version_cm;
        }
        if (!basic_version_cm)
        {
            if (GUILayout.Button(INC.la("show_all_mc"), GUILayout.Width(130f)))
            {
                command.Clear();
            }
            if (GUILayout.Button(INC.la("hide_all_mc"), GUILayout.Width(130f)))
            {
                for (int i = 0; i < InRoomChat.instance.commands.Count; i++)
                {
                    bool cnt = command.Contains(i);
                    if (!cnt)
                    {
                        command.Add(i);
                    }

                }
            }
        }
        GUILayout.EndHorizontal();
        GUILayout.EndArea();

    }
 
    void AnimateSetting()
    {
        GUILayout.BeginArea(new Rect(Screen.width / 2 - 370, Screen.height / 2 - 250, 840, 450), GUI.skin.box);
        GUICyan.OnToogleCyan(INC.la("show_anim_nick_name"), 300, 1, 0, 100);
        if ((int)FengGameManagerMKII.settings[300] == 1)
        {
            GUILayout.Label(INC.la("info_animationNick"));
        }
        else
        {
            if (tosettings == 0)
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button(INC.la("create_anim"), GUILayout.Width(140f)))
                {
                    tosettings = 1;
                }
                if (GUILayout.Button(INC.la("export_my_script"), GUILayout.Width(160f)))
                {
                    tosettings = 2;
                }
                if (Nanimation.Count > 0)
                {
                    if (GUILayout.Button(INC.la("edit_my_script"), GUILayout.Width(200f)))
                    {
                        string str = ((string)FengGameManagerMKII.settings[301]).Trim();
                        if (str.Length >= 3)
                        {
                            myanim = new List<AnimN>();
                            string[] srrre = str.Split(new char[] { ',' });
                            foreach (string str543 in srrre)
                            {
                                if (str543 != string.Empty)
                                {
                                    myanim.Add(new AnimN(str543, true));
                                }
                            }
                            tosettings = 10;
                            addnickName = myanim.First<AnimN>().ToString().Trim().StripHex();
                        }
                    }
                }
                GUILayout.EndHorizontal();
                if (GUILayout.Button(INC.la("back"), GUILayout.Width(140f)))
                {
                    mymenu = 0;
                }
            }
            else if (tosettings == 1)
            {
                GUILayout.Label(INC.la("add_nick_name_to_anim"));
                addnickName = GUILayout.TextField(addnickName, GUILayout.Height(30f));
                if (GUILayout.Button(INC.la("apply"), GUILayout.Width(140f)))
                {
                    addnickName = addnickName.StripHex().HexDell().Trim();
                    if (addnickName.Length >= 3)
                    {
                        tosettings = 10;
                        myanim = new List<AnimN>();
                        myanim.Add(new AnimN(addnickName));
                    }
                }
                if (GUILayout.Button(INC.la("back"), GUILayout.Width(140f)))
                {
                    tosettings = 0;
                    addnickName = string.Empty;
                }
            }
            if (tosettings == 2)
            {

                addnickName = GUILayout.TextField(addnickName, GUILayout.Height(30f));
                if (GUILayout.Button(INC.la("export_my_script"), GUILayout.Width(200f)))
                {
                    if (addnickName.Length >= 3)
                    {
                        myanim = new List<AnimN>();
                        string[] srrre = addnickName.Trim().Split(new char[] { ',' });
                        foreach (string str543 in srrre)
                        {
                            if (str543 != string.Empty)
                            {
                                myanim.Add(new AnimN(str543, true));
                            }
                        }
                        tosettings = 10;
                        addnickName = myanim.First<AnimN>().ToString().Trim().StripHex();
                    }
                }
                if (GUILayout.Button(INC.la("back"), GUILayout.Width(140f)))
                {
                    tosettings = 0;
                    addnickName = string.Empty;
                }

            }
            if (tosettings == 10)
            {

                GUILayout.BeginHorizontal();
                GUILayout.Label(INC.la("my_nick_an") + addnickName + "\n" + INC.la("caders_an"));
                if (GUILayout.Button(INC.la("back"), GUILayout.Width(140f)))
                {
                    tosettings = 0;
                    addnickName = string.Empty;
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.BeginVertical(GUILayout.Width(250f));
                vectors2[1] = GUILayout.BeginScrollView(vectors2[1]);
                {
                    foreach (AnimN counter in myanim)
                    {
                        string str = string.Empty;
                        if (counter == toR)
                        {
                            str = ">";
                        }
                        GUILayout.BeginHorizontal();
                        if (GUILayout.Button(str + " " + counter.ToString().toHex(), GUILayout.Width(200f)))
                        {
                            toR = counter;
                        }

                        if (GUILayout.Button("x", GUILayout.Width(20f)))
                        {
                            myanim.Remove(counter);
                        }
                        GUILayout.EndHorizontal();
                    }
                }
                GUILayout.EndScrollView();
                if (GUILayout.Button(INC.la("add_to_anim_c"), GUILayout.Width(200f)))
                {
                    myanim.Add(new AnimN(addnickName));
                    toR = myanim.Last<AnimN>();
                }
                GUILayout.EndVertical();

                if (toR == null)
                {
                    GUILayout.Label(INC.la("caders_anin_c"));
                }
                else
                {
                    GUILayout.BeginVertical();

                    vectors2[2] = GUILayout.BeginScrollView(vectors2[2], GUILayout.Height(100f));
                    {

                        GUILayout.BeginHorizontal();
                        foreach (AnimN.T butt in toR.Viev)
                        {
                            GUILayout.BeginVertical();
                            GUILayout.Label(butt.B.ToString(), GUI.skin.box, GUILayout.Width(70f));
                            if (GUILayout.Button("<color=#" + butt.color + ">" + butt.color + "</color>", GUILayout.Width(70f)))
                            {
                                currents = butt;
                            }
                            if (butt == currents)
                            {
                                GUILayout.Button("", GUILayout.Width(70f), GUILayout.Height(10f));
                            }
                            GUILayout.EndVertical();
                        }
                        GUILayout.EndHorizontal();
                    }
                    GUILayout.EndScrollView();


                    GUILayout.BeginHorizontal();
                    GUILayout.BeginVertical(GUILayout.Width(200f));
                    if (currents != null)
                    {
                        currents.color = col_for_anim.HexConverter();
                        col_for_anim = cext.color_toGUI(col_for_anim, "<color=#" + currents.color + ">" + currents.color + "</color>", false);
                    }
                    GUILayout.BeginHorizontal();
                    GUILayout.Label(INC.la("palitra_in_c_n"));
                    if (GUILayout.Button("+", GUILayout.Width(60), GUILayout.Height(25)))
                    {
                        if (!palitra.Contains(col_for_anim))
                        {
                            palitra.Add(col_for_anim);
                        }
                    }
                    GUILayout.EndHorizontal();
                    vectors2[4] = GUILayout.BeginScrollView(vectors2[4], GUILayout.Height(100f));

                    GUILayout.BeginHorizontal();
                    if (palitra.Count >= 1)
                    {
                        foreach (Color polll in palitra)
                        {

                            GUILayout.BeginVertical();
                            GUIStyle sssddd = new GUIStyle(GUI.skin.box);
                            sssddd.normal.textColor = polll;

                            if (GUILayout.Button("0", sssddd, GUILayout.Width(30), GUILayout.Height(30)))
                            {
                                col_for_anim = polll;
                            }
                            if (GUILayout.Button("x", GUILayout.Width(30), GUILayout.Height(25)))
                            {
                                palitra.Remove(polll);
                            }
                            GUILayout.EndVertical();
                        }
                    }

                    GUILayout.EndHorizontal();
                    GUILayout.EndScrollView();


                    GUILayout.EndVertical();
                    GUILayout.BeginVertical();
                    if (GUILayout.Button(!testing ? INC.la("sett_anim") : INC.la("stopedt_anim")))
                    {
                        if (!testing)
                        {
                            Nanimation = new List<string>();
                            foreach (AnimN ssddee in myanim)
                            {
                                Nanimation.Add(ssddee.ToString());
                            }
                            timeUPD232 = 0f;
                            counnnts = 0;
                            testing = true;
                        }
                        else
                        {
                            testing = false;
                        }
                    }
                    if (testing)
                    {
                        timeUPD232 += deltaTime;
                        if (timeUPD232 >= 0.2f)
                        {
                            if (counnnts > Nanimation.Count - 1)
                            {
                                counnnts = 0;
                            }
                            timeUPD232 = 0f;
                            cccurent = Nanimation[counnnts];
                            counnnts++;
                        }
                        GUILayout.Label(cccurent.toHex());
                    }
                    if (GUILayout.Button("Export code"))
                    {
                        code = "";
                        foreach (AnimN counter in myanim)
                        {
                            code = code + counter.ToString() + ",\n";
                        }
                    }
                    scroolPos = GUILayout.BeginScrollView(scroolPos);

                    GUILayout.TextArea(code);

                    GUILayout.EndScrollView();
                    GUILayout.EndVertical();
                    GUILayout.EndHorizontal();
                    GUILayout.EndVertical();

                }
                GUILayout.EndHorizontal();
            }
        }
        GUILayout.EndArea();
    }
  
    void SendSkin()
    {
        GUILayout.BeginArea(new Rect(Screen.width / 2 - 130, Screen.height / 2 - 250, 260, 450), GUI.skin.box);
        GUIStyle stylewwss = new GUIStyle(GUI.skin.button);
        stylewwss.alignment = TextAnchor.MiddleLeft;
        GUILayout.Label(INC.la("added_place_player"));
        vectors2[22] = GUILayout.BeginScrollView(vectors2[22]);
        foreach (PhotonPlayer player in PhotonNetwork.playerList)
        {

            if (selplayer == player)
            {
                stylewwss.normal = GUI.skin.button.onNormal; 
            }
            else
            {
                stylewwss.normal = GUI.skin.button.normal; 
            }
            string fulPlayername = player.id;

            if ((player.dead))
            {
                fulPlayername = fulPlayername + "<color=#FF0000>D</color>";
            }
            else
            {
                if (player.isMasterClient)
                {
                    fulPlayername = fulPlayername + "M";
                }
                if ((player.isTitan) == 2)
                {
                    fulPlayername = fulPlayername + "T";
                }
                if ((player.isTitan) == 1 && (player.team) != 2)
                {
                    fulPlayername = fulPlayername + "H";
                }
                if ((player.team) == 2)
                {
                    fulPlayername = fulPlayername + "A";
                }
            }
            if (player.name() != string.Empty)
            {
                string nnsk = player.name2.toHex();

                fulPlayername = fulPlayername + "<color=#000000>|</color>" + nnsk;
            }
            else
            {
                fulPlayername = fulPlayername + "<color=#000000>|[No Name]</color>";
            }
            if (ignoreList.Contains(player.ID))
            {
                fulPlayername = "<color=#FF0000>[X]</color>" + fulPlayername;
            }
            if (GUILayout.Button(fulPlayername, stylewwss, GUILayout.Height(24f)))
            {
                selplayer = player;
            }
        }
        GUILayout.EndScrollView();

        if (selplayer != null)
        {
            if (GUILayout.Button(INC.la("send_to_skin_player")))
            {
                if (selplayer.CM)
                {
                    string[] str32 = selplayer.version.Split(new char[] { '.' });
                    if (Convert.ToInt32(str32[0]) > 0 || Convert.ToInt32(str32[1]) >= 3)
                    {
                        string[] text14 = new string[] { myCyanSkin.horse, myCyanSkin.hair, myCyanSkin.eyes, myCyanSkin.glass, myCyanSkin.face, myCyanSkin.skin, myCyanSkin.costume, myCyanSkin.logo_and_cape, myCyanSkin.dmg_right, myCyanSkin.dmg_left, myCyanSkin.gas, myCyanSkin.hoodie, myCyanSkin.weapon_trail };
                        base.photonView.RPC("FriendSkin", selplayer, new object[] { text14 });
                        mymenu = 2;
                        PanelInformer.instance.Add(INC.la("to_send_skins"), PanelInformer.LOG_TYPE.INFORMAION);
                        return;
                    }

                }
                string str345 = string.Empty;
                if (myCyanSkin.horse != string.Empty)
                {
                    str345 = str345 + "horse:" + myCyanSkin.horse + "\n";
                }
                if (myCyanSkin.hair != string.Empty)
                {
                    str345 = str345 + "hair:" + myCyanSkin.hair + "\n";
                }
                if (myCyanSkin.eyes != string.Empty)
                {
                    str345 = str345 + "eyes:" + myCyanSkin.eyes + "\n";
                }
                if (myCyanSkin.glass != string.Empty)
                {
                    str345 = str345 + "glass:" + myCyanSkin.glass + "\n";
                }
                if (myCyanSkin.face != string.Empty)
                {
                    str345 = str345 + "face:" + myCyanSkin.face + "\n";
                }
                if (myCyanSkin.skin != string.Empty)
                {
                    str345 = str345 + "skin:" + myCyanSkin.skin + "\n";
                }
                if (myCyanSkin.hoodie != string.Empty)
                {
                    str345 = str345 + "hoodie:" + myCyanSkin.hoodie + "\n";
                }
                if (myCyanSkin.costume != string.Empty)
                {
                    str345 = str345 + "costume:" + myCyanSkin.costume + "\n";
                }
                if (myCyanSkin.logo_and_cape != string.Empty)
                {
                    str345 = str345 + "logo_and_cape:" + myCyanSkin.logo_and_cape + "\n";
                }
                if (myCyanSkin.dmg_right != string.Empty)
                {
                    str345 = str345 + "blade/gun_right:" + myCyanSkin.dmg_right + "\n";
                }
                if (myCyanSkin.dmg_left != string.Empty)
                {
                    str345 = str345 + "blade/gun_left:" + myCyanSkin.dmg_left + "\n";
                }
                if (myCyanSkin.gas != string.Empty)
                {
                    str345 = str345 + "gas:" + myCyanSkin.gas + "\n";
                }
                if (myCyanSkin.weapon_trail != string.Empty)
                {
                    str345 = str345 + "weapon_trail:" + myCyanSkin.weapon_trail + "\n";
                }
                if (str345 != string.Empty)
                {
                    cext.mess(str345, selplayer);
                }
                mymenu = 2;
                PanelInformer.instance.Add(INC.la("to_send_skins"), PanelInformer.LOG_TYPE.INFORMAION);
                return;
            }
        }
        if (GUILayout.Button(INC.la("back")))
        {
            mymenu = 2;
        }
        GUILayout.EndArea();
    }
 
    void ChatOnMenu()
    {
        GUILayout.BeginArea(new Rect(Screen.width / 2 - 370, Screen.height / 2 - 250, 840, 450), GUI.skin.box);
        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return)
        {
            if (text_chat_menu.Trim() == string.Empty && GUI.GetNameOfFocusedControl() == "ChatOnMenu")
            {
                text_chat_menu = "";
                GUI.FocusControl("");
            }
            else if (GUI.GetNameOfFocusedControl() != "ChatOnMenu")
            {
                GUI.FocusControl("ChatOnMenu");
            }
            if (GUI.GetNameOfFocusedControl() == "ChatOnMenu" && text_chat_menu.Trim() != string.Empty)
            {

                if (chatting == 0)
                {
                    InRoomChat.instance.Command(text_chat_menu.Trim());
                    text_chat_menu = "";
                    GUI.FocusControl("");
                }
                else if (chatting == 1)
                {
                    object[] pam = new object[] { text_chat_menu, INC.chatname };
                    foreach (PhotonPlayer player in PhotonNetwork.playerList)
                    {
                        if (player.CM || PhotonNetwork.offlineMode)
                        {
                            base.photonView.RPC("PrivteCyanRPC", player, pam);
                        }
                    }
                    text_chat_menu = "";
                    GUI.FocusControl("");
                }
                else if (chatting == 2)
                {
                    if (current_cnf != null)
                    {
                        confer.command(text_chat_menu, current_cnf);
                        text_chat_menu = "";
                        GUI.FocusControl("");
                    }
                }
            }
        }
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical(GUILayout.Width(550f));
        {
            GUILayout.Box(INC.la("chat_on_menu"));
            chatting = GUILayout.SelectionGrid(chatting, new string[] { INC.la("chat_ver_1"), INC.la("chat_ver_2"), INC.la("chat_ver_3") }, 3);
            if (chatting == 0)
            {
                vectors2[23] = GUILayout.BeginScrollView(vectors2[23]);
                if (InRoomChat.chat_cont != string.Empty)
                {
                    GUILayout.TextArea(InRoomChat.chat_cont.HexDell(), GUI.skin.label);
                }
                else
                {
                    GUILayout.Label(INC.la("no_mess_on_chat_men"));
                }
                GUILayout.EndScrollView();
            }
            else if (chatting == 1)
            {
                vectors2[23] = GUILayout.BeginScrollView(vectors2[23]);
                string str = string.Empty;
                foreach (string con in chat_Cyan)
                {
                    str = str + con + "\n";
                }
                if (str != string.Empty)
                {
                    GUILayout.TextArea(str, GUI.skin.label);
                }
                else
                {
                    GUILayout.Label(INC.la("no_mess_on_chat_men"));
                }
                GUILayout.EndScrollView();
            }
            else if (chatting == 2)
            {
                int con_lenght = confer.conference_list.Count;
                if (con_lenght > 0)
                {
                    GUILayout.BeginHorizontal();
                    if (c_conf > 0)
                    {
                        if (GUILayout.Button("<", GUILayout.Width(30f)))
                        {
                            c_conf = c_conf - 1;
                        }
                    }
                    int num = 0;
                    for (int i = c_conf; i < con_lenght; i++)
                    {
                        if (num < 4)
                        {
                            Conference.cnf con = confer.conference_list[i];
                            string str44 = con.name_conf;
                            if (current_cnf != null && current_cnf == con)
                            {
                                str44 = ">" + str44 + "<";
                            }

                            if (GUILayout.Button(str44, GUILayout.Width(100f)))
                            {
                                current_cnf = con;
                            }
                        }
                        num++;
                    }
                    if (con_lenght > 4 && c_conf < con_lenght - 4)
                    {
                        if (GUILayout.Button(">", GUILayout.Width(30f)))
                        {
                            c_conf = c_conf + 1;
                        }
                    }

                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    name_conf = GUILayout.TextField(name_conf, GUILayout.Width(430f));
                    if (GUILayout.Button(INC.la("create_conf")))
                    {
                        if (name_conf.Trim() == string.Empty)
                        {
                            name_conf = "CONF-" + ((int)UnityEngine.Random.Range(999, 99999)).ToString();
                        }
                        current_cnf = confer.CreateConference(name_conf);
                    }
                    GUILayout.EndHorizontal();

                    if (current_cnf != null)
                    {
                        GUILayout.Label(INC.la("general_p_conf") + current_cnf.master.id.ToString() + current_cnf.master.ishexname);
                    }
                    vectors2[23] = GUILayout.BeginScrollView(vectors2[23]);
                    if (current_cnf != null)
                    {
                        string sss = "";
                        foreach (int str43 in current_cnf.players_ID)
                        {
                            sss = sss + "[" + str43.ToString() + "]";
                        }
                        GUILayout.Label(INC.la("players_cnf") + sss);
        
                        string str = "";

                        foreach (string str43 in current_cnf.messages)
                        {
                            str = str + str43 + "\n";
                        }
                        if (str != string.Empty)
                        {
                            GUILayout.TextArea(str, GUI.skin.label);
                        }
                    }
                    else
                    {
                        GUILayout.Label(INC.la("no_aded_confers"));
                    }
                    GUILayout.EndScrollView();
                }
                else
                {
                    GUILayout.Label(INC.la("no_confers"));
                    name_conf = GUILayout.TextField(name_conf, GUILayout.Width(150f));
                    if (GUILayout.Button(INC.la("create_conf"), GUILayout.Width(150f)))
                    {
                        if (name_conf.Trim() == string.Empty)
                        {
                            name_conf = "CONF-" + ((int)UnityEngine.Random.Range(999, 99999)).ToString();
                        }
                        current_cnf = confer.CreateConference(name_conf);
                    }
                }
            }
            GUILayout.BeginHorizontal();
            GUI.SetNextControlName("ChatOnMenu");
            text_chat_menu = GUILayout.TextField(text_chat_menu, GUILayout.Width(430f));

            string str66 = "AutoScroll";
            if (auto_scroll_chat_on_menu)
            {
                vectors2[23].y = 99999;
                str66 = "<b>" + str66 + "</b>";
            }
            auto_scroll_chat_on_menu = GUILayout.Toggle(auto_scroll_chat_on_menu, str66, GUI.skin.button);
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
        GUILayout.BeginVertical();
        {
            GUILayout.Box(INC.la("dissalow_text_on_cahat_m"));
            GUICyan.OnToogleCyan(INC.la("dis_t_c_toogle_on"), 339, 1, 0, 50);
            if ((int)FengGameManagerMKII.settings[339] == 1)
            {
                vectors2[20] = GUILayout.BeginScrollView(vectors2[20]);
                if (dis_text.Count == 0)
                {
                    GUILayout.Label(INC.la("no_mess_on_dis_text"));
                }
                else
                {
                    foreach (string str in dis_text)
                    {
                        GUILayout.BeginHorizontal();
                        if (GUILayout.Button("x", GUILayout.Width(30)))
                        {
                            dis_text.Remove(str);
                            return;
                        }
                        GUILayout.Label(str);
                        GUILayout.EndHorizontal();
                    }
                }
                GUILayout.EndScrollView();
                GUILayout.BeginHorizontal();
                add_diss_text = GUILayout.TextField(add_diss_text);
                if (GUILayout.Button("+", GUILayout.Width(50)))
                {
                    if (add_diss_text.Contains(","))
                    {
                        string[] str55 = add_diss_text.Split(new char[] { ',' });
                        foreach (string text in str55)
                        {
                            string tt = text.Trim();
                            if (tt.Length > 2 && !dis_text.Contains(tt))
                            {
                                dis_text.Add(tt);
                            }
                            else if (tt.Length < 2)
                            {
                                info_diss_text = INC.la("no_add_info_cc") + tt;
                            }
                            else if (dis_text.Contains(tt))
                            {
                                info_diss_text = INC.la("is_tt_info_cc") + tt;
                            }
                        }
                        add_diss_text = "";
                    }
                    else
                    {
                        string line = add_diss_text.Trim();
                        if (line.Length > 2)
                        {
                            if (!dis_text.Contains(line))
                            {
                                dis_text.Add(line);
                                add_diss_text = "";
                                info_diss_text = "";
                            }
                            else
                            {
                                info_diss_text = INC.la("is_tt_info_dd");
                            }
                        }
                        else
                        {
                            info_diss_text = INC.la("is_tt_info_ss");
                        }

                    }
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginVertical(GUI.skin.box);
                if (info_diss_text != string.Empty)
                {
                    GUILayout.Label(info_diss_text);
                }
                else
                {
                    GUILayout.Label(INC.la("infor_diss_text"));
                }
                GUILayout.EndVertical();
            }
        }
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
        if (GUILayout.Button(INC.la("back"), GUILayout.Width(120)))
        {
            mymenu = 0;
        }
        GUILayout.EndArea();
    }
   
    void StyleSettings()
    {
        GUILayout.BeginArea(new Rect(Screen.width / 2 - 370, Screen.height / 2 - 250, 840, 450), GUI.skin.box);
        Dictionary<string, GUIStyle> nskin = new Dictionary<string, GUIStyle>();
        nskin.Add("BOX", GUI.skin.box);
        nskin.Add("LABEL", GUI.skin.label);
        nskin.Add("BUTTON", GUI.skin.button);
        nskin.Add("TEXT_FIELD", GUI.skin.textField);
        nskin.Add("TEXT_AREA", GUI.skin.textArea);
        nskin.Add("TOGGLE", GUI.skin.toggle);
        nskin.Add("WINDOW", GUI.skin.window);
        nskin.Add("SLIDER", GUI.skin.horizontalSlider);
        nskin.Add("SLIDER_T", GUI.skin.horizontalSliderThumb);
        nskin.Add("VERTICAL_S", GUI.skin.verticalScrollbar);
        nskin.Add("VERTICAL_ST", GUI.skin.verticalScrollbarThumb);
        nskin.Add("HORIZONTAL_S", GUI.skin.horizontalScrollbar);
        nskin.Add("HORIZONTAL_ST", GUI.skin.horizontalScrollbarThumb);
        if (styled_sets == null)
        {
            styled_sets = nskin.Values.First<GUIStyle>();
        }
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical(GUILayout.Width(200f));
        vectors2[18] = GUILayout.BeginScrollView(vectors2[18]);
        foreach (var s in nskin)
        {
            string str = "";
            if (styled_sets == s.Value)
            {
                str = ">>";
            }
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(str + s.Key, GUILayout.Width(120)))
            {
                styled_sets = s.Value;
                styled_statesed = null;
                styled_stateBack = null;
            }
            GUILayout.Label("Example", s.Value);
            GUILayout.EndHorizontal();
        }
        GUILayout.EndScrollView();
        if (GUILayout.Button(INC.la("back"), GUILayout.Width(120)))
        {
            mymenu = 15;
        }
        GUILayout.EndVertical();
        GUILayout.BeginVertical();
        if (styled_sets != null)
        {
            GUILayout.Button("---------Example---------", styled_sets, GUILayout.Height(30f));

            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical(GUILayout.Width(200f));
            GUILayout.Box(INC.la("colors_text_sts"));
            vectors2[19] = GUILayout.BeginScrollView(vectors2[19]);
            Dictionary<string, GUIStyleState> ssd = new Dictionary<string, GUIStyleState>();
            ssd.Add("Text_Normal", styled_sets.normal);
            ssd.Add("Text_Active", styled_sets.active);
            ssd.Add("Text_Hover", styled_sets.hover);
            ssd.Add("Text_Focused", styled_sets.focused);
            ssd.Add("Text_ONNormal", styled_sets.onNormal);
            ssd.Add("Text_ONActive", styled_sets.onActive);
            ssd.Add("Text_ONHover", styled_sets.onHover);
            ssd.Add("Text_ONFocused", styled_sets.onFocused);
            if (styled_statesed == null)
            {
                styled_statesed = ssd.Values.First<GUIStyleState>();
            }
            foreach (var b in ssd)
            {
                string str = "";
                if (styled_statesed == b.Value)
                {
                    str = ">>";
                }
                GUIStyle gs = new GUIStyle(GUI.skin.button);
                gs.normal.textColor = b.Value.textColor;
                if (GUILayout.Button(str + b.Key, gs))
                {
                    styled_statesed = b.Value;
                }
            }
            GUILayout.EndScrollView();
            if (styled_statesed != null)
            {
                styled_statesed.textColor = cext.color_toGUI(styled_statesed.textColor);
            }

            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            to_settings_sts = GUILayout.SelectionGrid(to_settings_sts, new string[] { INC.la("parametrs_sts"), INC.la("textures_sts") }, 2);
            if (to_settings_sts == 0)
            {
                GUILayout.Box(INC.la("parametrs_sts"));
                vectors2[17] = GUILayout.BeginScrollView(vectors2[17]);
                styled_sets.fontSize = cext.int_slider_toGUI(styled_sets.fontSize, 7, 20, INC.la("font_size_sts"));
                styled_sets.fontStyle = cext.font_style_toGUI(styled_sets.fontStyle, INC.la("font_style_sts"));
                styled_sets.alignment = cext.anchor__toGUI(styled_sets.alignment, INC.la("text_achor_sts"));
                styled_sets.border.bottom = cext.int_slider_toGUI(styled_sets.border.bottom, 0f, 50, "Border bottom:");
                styled_sets.border.top = cext.int_slider_toGUI(styled_sets.border.top, 0f, 50, "Border top:");
                styled_sets.border.left = cext.int_slider_toGUI(styled_sets.border.left, 0f, 50, "Border left:");
                styled_sets.border.right = cext.int_slider_toGUI(styled_sets.border.right, 0f, 50, "Border right:");
                styled_sets.margin.bottom = cext.int_slider_toGUI(styled_sets.margin.bottom, 0f, 50, "Margin bottom:");
                styled_sets.margin.top = cext.int_slider_toGUI(styled_sets.margin.top, 0f, 50, "Margin top:");
                styled_sets.margin.left = cext.int_slider_toGUI(styled_sets.margin.left, 0f, 50, "Margin left:");
                styled_sets.margin.right = cext.int_slider_toGUI(styled_sets.margin.right, 0f, 50, "Margin right:");
                styled_sets.padding.bottom = cext.int_slider_toGUI(styled_sets.padding.bottom, 0f, 50, "Padding bottom:");
                styled_sets.padding.top = cext.int_slider_toGUI(styled_sets.padding.top, 0f, 50, "Padding top:");
                styled_sets.padding.left = cext.int_slider_toGUI(styled_sets.padding.left, 0f, 50, "Padding left:");
                styled_sets.padding.right = cext.int_slider_toGUI(styled_sets.padding.right, 0f, 50, "Padding right:");

                styled_sets.overflow.bottom = cext.int_slider_toGUI(styled_sets.overflow.bottom, -25, 25, "Overflow bottom:");
                styled_sets.overflow.top = cext.int_slider_toGUI(styled_sets.overflow.top, -25, 25, "Overflow top:");
                styled_sets.overflow.left = cext.int_slider_toGUI(styled_sets.overflow.left, -25, 25, "Overflow left:");
                styled_sets.overflow.right = cext.int_slider_toGUI(styled_sets.overflow.right, -25, 25, "Overflow right:");
                int s = styled_sets.clipping == TextClipping.Overflow ? 1 : 0;
                s = GUILayout.SelectionGrid(s, new string[] { "Clip", "Overflow" }, 2);
                styled_sets.clipping = s == 0 ? TextClipping.Clip : TextClipping.Overflow;
                styled_sets.imagePosition = cext.imagePosition__toGUI(styled_sets.imagePosition, "Image Position:");
                GUILayout.EndScrollView();
            }
            else if (to_settings_sts == 1)
            {
                vectors2[17] = GUILayout.BeginScrollView(vectors2[17], GUILayout.Height(90f));
                GUILayout.BeginHorizontal();
                GUIStyle style = new GUIStyle(GUI.skin.box);
                if (styled_stateBack == null)
                {
                    styled_stateBack = ssd.Values.First<GUIStyleState>();
                }
                foreach (var b in ssd)
                {
                    string str = b.Key.Replace("Text_", "");
                    if (styled_stateBack == b.Value)
                    {
                        str = ">>" + str;
                    }
                    if (b.Value.background != null)
                    {
                        style.normal.background = b.Value.background;
                        str = str + "\n" + b.Value.background.width.ToString() + "x" + b.Value.background.height.ToString();
                        str = str + "\n" + b.Value.background.name;
                        if (GUILayout.Button(str, style, GUILayout.Width(80f)))
                        {
                            styled_stateBack = b.Value;
                        }
                    }


                }
                GUILayout.EndHorizontal();
                GUILayout.EndScrollView();
                if (styled_stateBack != null)
                {

                    if (FilesHelper.drivers == null)
                    {
                        FilesHelper.drivers = Directory.GetLogicalDrives();
                    }
                    if (FilesHelper.catalogs == null)
                    {
                        FilesHelper.getFilesAndDir(Application.dataPath);
                    }
                    GUILayout.Label(FilesHelper.current_catalog);
                    GUILayout.BeginHorizontal();
                    foreach (string log in FilesHelper.drivers)
                    {
                        if (GUILayout.Button(log, GUILayout.Width(40f)))
                        {
                            FilesHelper.getFilesAndDir(log);
                        }
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();

                    GUILayout.BeginVertical();
                    vectors2[16] = GUILayout.BeginScrollView(vectors2[16]);
                    GUIStyle styleb = new GUIStyle(GUI.skin.button);
                    styleb.alignment = TextAnchor.MiddleLeft;

                    if (GUILayout.Button(INC.la("uped_sts")))
                    {
                        FilesHelper.current_catalog = FilesHelper.current_catalog.Replace("//", @"\");
                        string str00 = string.Empty;
                        if (FilesHelper.current_catalog.EndsWith(@"\"))
                        {
                            str00 = FilesHelper.current_catalog.Substring(0, FilesHelper.current_catalog.Length - 1).Replace(@"\", "#");
                        }
                        else
                        {
                            str00 = FilesHelper.current_catalog.Replace(@"\", "#");
                        }
                        string[] ssdff = str00.Split(new char[] { '#' });

                        string str33 = string.Empty;
                        for (int i = 0; i < ssdff.Length - 1; i++)
                        {
                            str33 = str33 + ssdff[i] + @"\";
                        }
                        FilesHelper.getFilesAndDir(str33);
                    }
                    GUILayout.Label(INC.la("folders_sts"));

                    foreach (string dir in FilesHelper.catalogs)
                    {
                        string dirr = string.Empty;
                        if (FilesHelper.current_catalog.EndsWith(@"\"))
                        {
                            dirr = FilesHelper.current_catalog + dir;
                        }
                        else
                        {
                            dirr = FilesHelper.current_catalog + @"\" + dir;
                        }
                        if (my_File != null && my_File.path == dirr)
                        {
                            styleb.normal = GUI.skin.button.onNormal; 
                        }
                        else
                        {
                            styleb.normal = GUI.skin.button.normal; 
                        }
                        if (GUILayout.Button(dir, styleb))
                        {
                            my_File = new FilesHelper.MyFiles(dirr);
                        }
                    }
                    GUILayout.Label(INC.la("files_sts"));
                    foreach (string dir in FilesHelper.files)
                    {
                        string dirr = string.Empty;
                        if (FilesHelper.current_catalog.EndsWith(@"\"))
                        {
                            dirr = FilesHelper.current_catalog + dir;
                        }
                        else
                        {
                            dirr = FilesHelper.current_catalog + @"\" + dir;
                        }
                        if (my_File != null && my_File.path == dirr)
                        {
                            styleb.normal = GUI.skin.button.onNormal; 
                        }
                        else
                        {
                            styleb.normal = GUI.skin.button.normal; 
                        }
                        if (GUILayout.Button(dir, styleb))
                        {
                            my_File = new FilesHelper.MyFiles(dirr);
                        }

                    }
                    GUILayout.EndScrollView();
                    GUILayout.EndVertical();



                    if (FilesHelper.main_texture != null)
                    {
                        GUILayout.BeginVertical();
                        GUIStyle sstyle = new GUIStyle();
                        sstyle.normal.background = FilesHelper.main_texture;
                        GUILayout.Box("", sstyle, GUILayout.Width(140f), GUILayout.Height(140f));
                        if (GUILayout.Button(INC.la("close")))
                        {
                            Destroy(FilesHelper.main_texture);
                            return;
                        }
                        GUILayout.EndVertical();
                    }

                    GUILayout.EndHorizontal();
                    if (my_File != null)
                    {
                        GUILayout.BeginHorizontal();
                        if (my_File.isDirectory)
                        {
                            if (GUILayout.Button(INC.la("open_sts"), GUILayout.Width(120f)))
                            {
                                FilesHelper.getFilesAndDir(my_File.path);
                                vectors2[16].y = 0;
                                my_File = null;
                                return;
                            }
                        }
                        if (my_File.isFile && (my_File.path.ToLower().EndsWith("png") || my_File.path.ToLower().EndsWith("jpg") || my_File.path.ToLower().EndsWith("jpeg")))
                        {
                            if (GUILayout.Button(INC.la("select_sts"), GUILayout.Width(120f)))
                            {
                                string str = styled_stateBack.background.name;

                                byte[] bb = File.ReadAllBytes(my_File.path);
                                Texture2D tte = new Texture2D(1, 1, TextureFormat.RGBA32, false);
                                tte.LoadImage(bb);
                                tte.Apply();
                                if (styled_stateBack.background != null)
                                {
                                    Destroy(styled_stateBack.background);
                                }
                                styled_stateBack.background = tte;
                                styled_stateBack.background.name = str;
                                my_File = null;
                                return;

                            }
                            if (GUILayout.Button(INC.la("lock_sts"), GUILayout.Width(120f)))
                            {
                                if (FilesHelper.main_texture != null)
                                {
                                    Destroy(FilesHelper.main_texture);
                                }
                                byte[] bb = File.ReadAllBytes(my_File.path);
                                Texture2D tte = new Texture2D(1, 1, TextureFormat.RGBA32, false);
                                tte.LoadImage(bb);
                                tte.Apply();
                                FilesHelper.main_texture = tte;
                                return;
                            }
                            GUILayout.Label("size:" + my_File.bite.ToString() + "(" + my_File.kbite.ToString() + "kb.).");
                        }
                        GUILayout.EndHorizontal();
                    }
                }
            }
            GUILayout.EndVertical();

            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
   
    void updateListThame()
    {

        style_list = new List<FileInfo>();
        string[] listt = Directory.GetFiles(INC.thame_path);
        foreach (string ll in listt)
        {
            if (ll.EndsWith(".cyansthame"))
            {
                FileInfo info = new FileInfo(ll);
                style_list.Add(info);
            }
        }
    }
    void styled()
    {
        GUIStyle style = new GUIStyle(GUI.skin.button);
        style.alignment = TextAnchor.MiddleLeft;
        float labelwith = 50f;
        GUILayout.BeginArea(new Rect(Screen.width / 2 - 370, Screen.height / 2 - 250, 840, 450), GUI.skin.box);
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical(GUILayout.Width(250f));
        {
            scrollPos1 = GUILayout.BeginScrollView(scrollPos1);
            GUICyan.OnToogleCyan(INC.la("dis_backgounds"), 340, 1, 0, labelwith);
            if ((int)FengGameManagerMKII.settings[340] == 1)
            {
                INC.color_background = cext.color_toGUI(INC.color_background, INC.la("color_backgrounds_text"));
            }
            INC.gui_color = cext.color_toGUI(INC.gui_color, INC.la("color_backgrounds_GUI"));
            GUILayout.EndScrollView();
            theme_name = GUILayout.TextField(theme_name);
            if (GUILayout.Button(INC.la("create_my_style")))
            {
                theme_name = theme_name.Trim();
                if (theme_name.Length > 0)
                {
                    string ppath = INC.thame_path + theme_name + ".cyansthame";
                    FileInfo info = new FileInfo(ppath);
                    if (!info.Exists)
                    {
                        saves();
                        UIMainReferences.MAIN.CreateStyle(ppath);
                        PanelInformer.instance.Add(INC.la("file_created_to") + ppath, PanelInformer.LOG_TYPE.INFORMAION);
                        updateListThame();
                        return;
                    }
                    PanelInformer.instance.Add(INC.la("file_exiset2"), PanelInformer.LOG_TYPE.DANGER);
                    return;
                }
                PanelInformer.instance.Add(INC.la("name_file_empty"), PanelInformer.LOG_TYPE.WARNING);
                return;
            }
            if (GUILayout.Button(INC.la("advenced_settings_style")))
            {
                mymenu = 14;
            }
            if (GUILayout.Button(INC.la("back"), GUILayout.Width(120f)))
            {
                mymenu = 0;
            }
        }
        GUILayout.EndVertical();
        GUILayout.BeginVertical(GUILayout.Width(250f));
        {
            GUILayout.Label(INC.la("custom_fonts"));
            scrollPos2 = GUILayout.BeginScrollView(scrollPos2);

            if (font_list == null)
            {
                font_list = new List<Font>();
                string[] str = new string[] { "CENSCBK", "times", "tahoma", "segoesc", "pala", "MTCORSVA", "MISTRAL", "impact", "GOST_BU", "georgia", "ds_goose", "cou", "constan", "consola", "comic", "CENTURY" };
                foreach (string sss in str)
                {
                    Font fnt = null;
                    try
                    {
                        fnt = (Font)Statics.CMassets.Load(sss);
                    }
                    catch { };
                    if (fnt != null)
                    {
                        font_list.Add(fnt);
                    }
                }
            }
            foreach (Font ft in font_list)
            {
                if (GUILayout.Button(ft.name, style))
                {
                    FengGameManagerMKII.settings[270] = ft.name;
                    GUI.skin.label.font = ft;
                    GUI.skin.button.font = ft;
                    GUI.skin.textArea.font = ft;
                    GUI.skin.textField.font = ft;
                    GUI.skin.toggle.font = ft;
                    GUI.skin.box.font = ft;
                    GUI.skin.window.font = ft;
                }
            }
            GUILayout.EndScrollView();
        }
        GUILayout.EndVertical();

        GUILayout.BeginVertical();

        GUILayout.Label(INC.la("custom_thams"));
        if (GUILayout.Button(INC.la("update_list_script")))
        {
            updateListThame();
            PanelInformer.instance.Add(INC.la("list_thame_upd"), PanelInformer.LOG_TYPE.INFORMAION);
        }
        if (style_list == null)
        {
            updateListThame();
        }
        scrollPos3 = GUILayout.BeginScrollView(scrollPos3);
        foreach (FileInfo info in style_list)
        {
            GUILayout.BeginHorizontal();
         
            if (GUILayout.Button(info.Name, style))
            {
                UIMainReferences.MAIN.ChangeStyle(info.FullName);
                PanelInformer.instance.Add(INC.la("appled_thame") + info.Name, PanelInformer.LOG_TYPE.INFORMAION);
                return;
            }
            if (GUILayout.Button("x", GUILayout.Width(30f)))
            {
                PanelInformer.instance.Add(INC.la("dell_thame") + info.Name, PanelInformer.LOG_TYPE.INFORMAION);
                info.Delete();
                updateListThame();
                return;
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndScrollView();
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
  
    void UpdatedSnapScr()
    {
        list_screen = new List<FileInfo>();
        if (snap == 0)
        {
            string path = FengGameManagerMKII.ScreenshotsPath;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string[] sss = Directory.GetFiles(path);
            foreach (string s in sss)
            {
                if (s.EndsWith(".png"))
                {
                    list_screen.Add(new FileInfo(s));
                }
            }
        }
        else if (snap == 1)
        {
            string path = BTN_save_snapshot.path;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string[] sss = Directory.GetFiles(path);
            foreach (string s in sss)
            {
                if (s.EndsWith(".png"))
                {
                    list_screen.Add(new FileInfo(s));
                }
            }
        }
    }
    void snap_screen()
    {
        if (list_screen == null)
        {
            UpdatedSnapScr();
        }
        GUILayout.BeginArea(new Rect(Screen.width / 2 - 370, Screen.height / 2 - 250, 840, 450), GUI.skin.box);
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical(GUILayout.Width(200f));
        int s = snap;
        snap = GUILayout.SelectionGrid(snap, new string[] { INC.la("screenshots"), INC.la("snapshots") }, 2);
        if (s != snap)
        {
            UpdatedSnapScr();
            if (textured_snap != null)
            {
                Destroy(textured_snap);
            }
        }
        if (GUILayout.Button(INC.la("updated_snaps")))
        {
            UpdatedSnapScr();
            PanelInformer.instance.Add(INC.la("snaphots_updated"), PanelInformer.LOG_TYPE.INFORMAION);
        } 
        
        
        if (GUILayout.Button(full ? "<b>" + INC.la("full_image") + "</b>": INC.la("full_image")))
        {
            full = !full;
        }
        if (snap == 1)
        {
            local_snapshots = GUILayout.SelectionGrid(local_snapshots, new string[] { "Local", "Saveds" }, 2);
        }
        scroolPos999 = GUILayout.BeginScrollView(scroolPos999);

        if (snap == 0 || (snap == 1 && local_snapshots == 1))
        {
            if (list_screen.Count > 0)
            {
                foreach (FileInfo info in list_screen)
                {
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button(info.Name + "\n" + "size:" + (info.Length / 1024f).ToString("F") + "kb."))
                    {
                        if (textured_snap != null)
                        {
                            Destroy(textured_snap);
                        }
                        byte[] dd = info.ReadAllBytes();
                        Texture2D text = new Texture2D(1, 1, TextureFormat.ARGB32, false);
                        text.LoadImage(dd);
                        text.Apply();
                        textured_snap = text;
                    }
                    if (GUILayout.Button("x", GUILayout.Width(25f)))
                    {
                        PanelInformer.instance.Add(INC.la("dell_thame") + info.FullName, PanelInformer.LOG_TYPE.INFORMAION);
                        info.Delete();
                        UpdatedSnapScr();
                        return;
                    }
                    GUILayout.EndHorizontal();
                }
            }
            else
            {
                GUILayout.Label(INC.la("no_mess_on_dis_text"));
            }
        }
        else
        {
            if (SnapShotSaves.img != null && SnapShotSaves.img.Length > 0)
            {
                for (int i = 0; i < SnapShotSaves.img.Length; i++)
                {
                    Texture2D texture = SnapShotSaves.img[i];
                    if (texture != null)
                    {
                        if (GUILayout.Button("Damage_" + SnapShotSaves.dmg[i]))
                        {
                            if (textured_snap != null)
                            {
                                Destroy(textured_snap);
                            }
                            Texture2D text = new Texture2D(texture.width, texture.height, TextureFormat.ARGB32, false);
                            text.SetPixels(texture.GetPixels());
                            text.Apply();
                            textured_snap = text;
                        }
                    }
                }
            }
            else
            {
                GUILayout.Label(INC.la("no_mess_on_dis_text"));
            }
        }

        GUILayout.EndScrollView();
        if (GUILayout.Button(INC.la("back")))
        {
            mymenu = 0;
        }
        GUILayout.EndVertical();
        GUILayout.BeginVertical();
        try
        {
            if (textured_snap != null)
            {
                scroolPos998 = GUILayout.BeginScrollView(scroolPos998);
                if(full)
                {
                        GUILayout.Label(textured_snap);
                }
                else
                {
                        GUILayout.Label(textured_snap, GUILayout.Width(600));
                }
            
                GUILayout.EndScrollView();
            }
            else
            {
                GUILayout.Label(INC.la("image_not_found"));
            }
        }
        catch{}
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
 
    public void Menu()
    {
        Screen.showCursor = true;
        Screen.lockCursor = false;
        if ((int)FengGameManagerMKII.settings[64] != 6)
        {
            GUI.backgroundColor = INC.gui_color;
            bool is_textured = (int)FengGameManagerMKII.settings[340] == 0;
            Rect bacgrounds_texture = new Rect(Screen.width / 2 - 480, Screen.height / 2 - 255, 960, 510);
            if (!is_textured)
            {

                Texture2D texturwq = (INC.color_background).toTexture1();
                GUI.DrawTexture(bacgrounds_texture, texturwq);
                Destroy(texturwq);
            }
            if (mymenu == 0)
            {
                if (is_textured)
                {
                    GUI.DrawTexture(bacgrounds_texture, TexturesBackgrounds.back_general);
                }
                GUILayout.BeginArea(new Rect(Screen.width / 2 - 370, Screen.height / 2 - 250, 840, 450), GUI.skin.box);
                grafics();
                titansettings();
                newsettings();
                GUILayout.EndArea();
            }
            else if (mymenu == 1)
            {
                if (is_textured)
                {
                    GUI.DrawTexture(bacgrounds_texture, TexturesBackgrounds.back_rebinds);
                }
                GUILayout.BeginArea(new Rect(Screen.width / 2 - 370, Screen.height / 2 - 250, 840, 450), GUI.skin.box);
                bindRC();
                newbinds();
                GUILayout.EndArea();
            }
            else if (mymenu == 2)
            {
                if (is_textured)
                {
                    GUI.DrawTexture(bacgrounds_texture, TexturesBackgrounds.back_skins);
                }
                GUILayout.BeginArea(new Rect(Screen.width / 2 - 370, Screen.height / 2 - 250, 840, 450), GUI.skin.box);
                newpanelskins();
                skins_mototd();
                GUILayout.EndArea();
            }
            else if (mymenu == 3)
            {
                if (is_textured)
                {
                    GUI.DrawTexture(bacgrounds_texture, TexturesBackgrounds.back_custom);
                }
                map_settings();
            }
            else if (mymenu == 4)
            {
                if (is_textured)
                {
                    GUI.DrawTexture(bacgrounds_texture, TexturesBackgrounds.back_other);
                }
                if (texture == null)
                {
                    texture = cext.loadResTexture("tital_cyannod");
                    return;
                }
                string[] str44 = new string[] { CyanMod.INC.la("about"), "Formula" };
                GUILayout.BeginArea(new Rect(Screen.width / 2 - 370, Screen.height / 2 - 250, 840, 450), GUI.skin.box);
                GUILayout.BeginHorizontal();
                GUILayout.BeginVertical();
                GUIStyle tyle = new GUIStyle(GUI.skin.button);
                for (int i = 0; i < str44.Length; i++)
                {
                    if (current_panel == i)
                    {
                        tyle.normal = GUI.skin.button.onNormal;
                    }
                    else
                    {
                        tyle.normal = GUI.skin.button.normal;
                    }
                    if (GUILayout.Button(str44[i], tyle))
                    {
                        current_panel = i;
                    }
                }
                GUILayout.EndVertical();
                GUILayout.BeginVertical();
                if (current_panel == 1)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.BeginVertical(GUILayout.Width(200f));
                    GUILayout.Label("Kills:");
                    kills_f = GUILayout.TextField(kills_f);
                    GUILayout.Label("Death:");
                    death_f = GUILayout.TextField(death_f);
                    GUILayout.Label("Max Damage:");
                    max_dmg_f = GUILayout.TextField(max_dmg_f);
                    GUILayout.Label("Total Damage:");
                    total_f = GUILayout.TextField(total_f);
                    if (GUILayout.Button("Lets go"))
                    {
                        result_f = "";
                        int k = 0;
                        int d = 0;
                        int m = 0;
                        int t = 0;
                        if (!int.TryParse(kills_f, out k))
                        {
                            result_f = "Error 'Kills'";
                            return;
                        }
                        if (!int.TryParse(death_f, out d))
                        {
                            result_f = "Error 'Death'";
                            return;
                        }
                        if (!int.TryParse(max_dmg_f, out m))
                        {
                            result_f = "Error 'Max Damage'";
                            return;
                        }
                        if (!int.TryParse(total_f, out t))
                        {
                            result_f = "Error 'Total Damage'";
                            return;
                        }
                        result_f = "Kills:" + k + "\nDeath:" + d + "\nMax Damage:" + m +"\nTotal Damage:" + t + "\nResult:" +  cext.calculate_formuls(0, k, d, m, t).ToString("F");
                   
                     
                    }
                    GUILayout.EndVertical();
                    GUILayout.BeginVertical();
                    GUILayout.Label(result_f);

                    if (texture_formils == null)
                    {
                        texture_formils = cext.loadResTexture("F1");
                    }
                    GUILayout.Label(texture_formils,GUILayout.Width(400));
                    GUILayout.EndVertical();
                    GUILayout.EndHorizontal();
                }
                if (current_panel == 0)
                {
                    GUIStyle style = new GUIStyle(GUI.skin.label);
                    style.fontSize = GUI.skin.label.fontSize + 2;
                    style.alignment = TextAnchor.MiddleCenter;
                    GUIStyle url_style = new GUIStyle(style);
                    url_style.hover.textColor = Color.red;
                    url_style.hover.background = CyanMod.Coltext.transp;
                    url_style.alignment = TextAnchor.MiddleLeft;
                    GUILayout.Label(texture, style);
                    string text = "Разработчик: tap1k\n";
                    text = text + "Версия мода: " + UIMainReferences.CyanModVers;

                    GUILayout.Label(text, style);

                    GUILayout.BeginHorizontal();
                    style.alignment = TextAnchor.MiddleRight;
                    GUILayout.Label("Папблик мода:", style);
                    if (GUILayout.Button("https://vk.com/cyan_mod", url_style))
                    {
                        Process.Start("https://vk.com/cyan_mod");
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Сайт разработчика:", style);
                    if (GUILayout.Button("http://attackontitan.ucoz.ru", url_style))
                    {
                        Process.Start("http://attackontitan.ucoz.ru");
                    }
                    GUILayout.EndHorizontal();

                    style.alignment = TextAnchor.MiddleCenter;
                    string text2 = "Особая благодарность за идеи и помощь:\n";
                    text2 = text2 + "Asshurish,coloN,JustlPain,BABAIKA.\n";
                    text2 = text2 + "У вас есть идеи по улучшению мода? Если есть, то поделитесь ею.\nЕсли ваша идея попадет в мод, то вас обязательно включат в этот список.";
                    GUILayout.Label(text2, style);
                    url_style.alignment = TextAnchor.MiddleCenter;
                    if (GUILayout.Button("[ Поделиться идеей ]", url_style))
                    {
                        Process.Start("https://vk.com/topic-113070254_32934439");
                    }
                }
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();
                GUILayout.EndArea();
            }
            else if (mymenu == 5)
            {
                if (is_textured)
                {
                    GUI.DrawTexture(bacgrounds_texture, TexturesBackgrounds.back_general);
                }
                playerListOnMenu();
            }
            else if (mymenu == 6)
            {
                if (is_textured)
                {
                    GUI.DrawTexture(bacgrounds_texture, TexturesBackgrounds.back_general);
                }
                AnimateSetting();
            }
            else if (mymenu == 7)
            {
                if (is_textured)
                {
                    GUI.DrawTexture(bacgrounds_texture, TexturesBackgrounds.back_general);
                }
                CommandsInfo();
            }
            else if (mymenu == 11)
            {
                if (is_textured)
                {
                    GUI.DrawTexture(bacgrounds_texture, TexturesBackgrounds.back_skins);
                }
                SendSkin();
            }
            else if (mymenu == 13)
            {
                if (is_textured)
                {
                    GUI.DrawTexture(bacgrounds_texture, TexturesBackgrounds.back_general);
                }
                ChatOnMenu();
            }
            else if (mymenu == 14)
            {
                if (is_textured)
                {
                    GUI.DrawTexture(bacgrounds_texture, TexturesBackgrounds.back_general);
                }
                StyleSettings();
            }
            else if (mymenu == 15)
            {
                if (is_textured)
                {
                    GUI.DrawTexture(bacgrounds_texture, TexturesBackgrounds.back_general);
                }
                styled();
            }
            else if (mymenu == 16)
            {
                if (is_textured)
                {
                    GUI.DrawTexture(bacgrounds_texture, TexturesBackgrounds.back_general);
                }
                snap_screen();
            }
            else if (mymenu == 17)
            {
                if (is_textured)
                {
                    GUI.DrawTexture(bacgrounds_texture, TexturesBackgrounds.back_general);
                }
                EmogiList();
            }
            else if (mymenu == 18)
            {
                if (is_textured)
                {
                    GUI.DrawTexture(bacgrounds_texture, TexturesBackgrounds.back_general);
                }
                toDefoult();
            }
            else if (mymenu == 19)
            {
                if (is_textured)
                {
                    GUI.DrawTexture(bacgrounds_texture, TexturesBackgrounds.back_general);
                }
                Bot_Meneger();
            }
            s_buttons();
            buttons();
        }

    }
    int current_bot_meneger = 0;
    bool is_log_bot = false;
    string key_bot_permament = "";
    Vector2 scroolPos33;
    Vector2 scroolPos44;
    Vector2 scroolPos55;
    Vector2 scroolPos66;
    Vector2 scroolPos77;
    static string text_for_bot = "";
    static string text_for_bot_field1 = "";
    static string text_for_bot_field2 = "";
    void Bot_Meneger()
    {
        GUILayout.BeginArea(new Rect(Screen.width / 2 - 370, Screen.height / 2 - 250, 840, 450), GUI.skin.box);
        GUILayout.BeginHorizontal();
        GUICyan.OnToogleCyan(INC.la("cyan_mod_bot"), 392, 1, 0, 50f);
        if (my_bot != null)
        {
            if (GUILayout.Button(is_log_bot ? "Hide Log" : "Show Log", GUILayout.Width(150)))
            {
                is_log_bot = !is_log_bot;
            }
        }
        GUILayout.EndHorizontal();

        if (my_bot == null)
        {
            GUILayout.Label("Бот спит.");
            if (GUILayout.Button("Разбудить", GUILayout.Width(150f)))
            {
                my_bot = new GameObject().AddComponent<Cyan_mod_bot>();
            }
        }
        else
        {
            if (is_log_bot)
            {
                GUILayout.Label("CYAN MOD BOT LOG:");
                scroolPos33 = GUILayout.BeginScrollView(scroolPos33);
                if (Cyan_mod_bot.LogBot.listLogBot.Count > 0)
                {
                    foreach (Cyan_mod_bot.LogBot loged in Cyan_mod_bot.LogBot.listLogBot)
                    {
                        string str = "";
                        if (loged.logtype == LogType.Log)
                        {
                            str = "00FF00";
                        }
                        else if (loged.logtype == LogType.Warning)
                        {
                            str = "FFFF00";
                        }
                        else
                        {
                            str = "FF0000";
                        }
                        GUILayout.TextField("<color=#" + str + ">[" + loged.time + "]</color>" + loged.message, GUI.skin.label);
                    }
                }
                else
                {
                    GUILayout.Label("Log clean.");
                }
                GUILayout.EndScrollView();
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Clear", GUILayout.Width(120f)))
                {
                    Cyan_mod_bot.LogBot.listLogBot.Clear();
                }
                GUILayout.Label("Max logs:[" + Cyan_mod_bot.LogBot.maxlog + "]", GUILayout.Width(120f));
                Cyan_mod_bot.LogBot.maxlog = (int)GUILayout.HorizontalSlider((float)Cyan_mod_bot.LogBot.maxlog, 10, 120);
                GUILayout.EndHorizontal();
            }
            else
            {
                GUILayout.Label("Память и функции бота:");
                current_bot_meneger = GUILayout.SelectionGrid(current_bot_meneger, new string[] { "Неизменные ключи", "Кастомизированные" }, 2);
                if (current_bot_meneger == 0)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.BeginVertical(GUILayout.Width(150f));
                    if (Cyan_mod_bot.permament_memory != null && Cyan_mod_bot.permament_memory.Count > 0)
                    {
                        GUIStyle styleb = new GUIStyle(GUI.skin.button);
                        scroolPos44 = GUILayout.BeginScrollView(scroolPos44);
                        foreach (var ss in Cyan_mod_bot.permament_memory)
                        {
                            if (key_bot_permament == ss.Key)
                            {
                                styleb.normal = GUI.skin.button.onNormal;
                            }
                            else
                            {
                                styleb.normal = GUI.skin.button.normal;
                            }
                            if (GUILayout.Button(ss.Key, styleb))
                            {
                                key_bot_permament = ss.Key;
                            }
                        }
                        GUILayout.EndScrollView();
                    }
                    else
                    {
                        GUILayout.Label("Permament memory not found.");
                    }
                    GUILayout.EndVertical();
                    GUILayout.BeginVertical();

                    scroolPos55 = GUILayout.BeginScrollView(scroolPos55);
                    if (key_bot_permament == "" && Cyan_mod_bot.permament_memory != null && Cyan_mod_bot.permament_memory.Count > 0)
                    {
                        key_bot_permament = Cyan_mod_bot.permament_memory.Keys.First();
                    }
                    else if (Cyan_mod_bot.permament_memory != null && Cyan_mod_bot.permament_memory[key_bot_permament] != null)
                    {
                        for(int df = 0; df < Cyan_mod_bot.permament_memory[key_bot_permament].Count() ; df++)
                        {
                            GUILayout.BeginHorizontal();
                           Cyan_mod_bot.permament_memory[key_bot_permament][df] = GUILayout.TextField(Cyan_mod_bot.permament_memory[key_bot_permament][df]);
                           if (Cyan_mod_bot.permament_memory[key_bot_permament].Count() > 1)
                           {
                               if (GUILayout.Button("X", GUILayout.Width(50f)))
                               {
                                   Cyan_mod_bot.permament_memory[key_bot_permament].Remove(Cyan_mod_bot.permament_memory[key_bot_permament][df]);
                               }
                           }
                           GUILayout.EndHorizontal();
                        }
                    }
                    else
                    {
                        GUILayout.Label("Key no added.");
                    }
                    GUILayout.EndScrollView();
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Add new:", GUILayout.Width(140f));
                    text_for_bot = GUILayout.TextField(text_for_bot);
                    if(GUILayout.Button("+",GUILayout.Width(40f)))
                    {
                        if (text_for_bot.Trim() != "")
                        {
                            Cyan_mod_bot.permament_memory[key_bot_permament].Add(text_for_bot);
                            text_for_bot = "";
                        }
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.EndVertical();
                    GUILayout.EndHorizontal();
                }
                else if (current_bot_meneger == 1)
                {
                  
                        GUILayout.BeginHorizontal();
                        GUILayout.BeginVertical( GUI.skin.box,GUILayout.Width(170f));
                        GUIStyle styleb = new GUIStyle(GUI.skin.button);
                         scroolPos44 = GUILayout.BeginScrollView(scroolPos44);
                         if (my_bot.main_list.Count > 0)
                         {
                             for (int k = 0; k < my_bot.main_list.Count; k++)
                             {
                                 if (current_custom_bot == k)
                                 {
                                     styleb.normal = GUI.skin.button.onNormal;
                                 }
                                 else
                                 {
                                     styleb.normal = GUI.skin.button.normal;
                                 }
                                 GUILayout.BeginHorizontal();
                                 if (GUILayout.Button("Element:" + k, styleb))
                                 {
                                     current_custom_bot = k;
                                 }
                                 if (GUILayout.Button("X",GUILayout.Width(30f)))
                                 {
                                     my_bot.main_list.Remove(my_bot.main_list[k]);
                                 }
                                 GUILayout.EndHorizontal();
                             }
                         }
                        GUILayout.EndScrollView();
                        if (GUILayout.Button("Add"))
                        {
                            my_bot.main_list.Add(new Cyan_mod_bot.MyMemory());
                        }
                        GUILayout.EndVertical();
                        if (my_bot.main_list.Count > current_custom_bot)
                        {
                            GUILayout.BeginVertical(GUI.skin.box);
                            scroolPos66 = GUILayout.BeginScrollView(scroolPos66);
                            if (my_bot.main_list[current_custom_bot].vopros.Count > 0)
                            {
                                for (int i = 0; i < my_bot.main_list[current_custom_bot].vopros.Count; i++)
                                {
                                    GUILayout.BeginHorizontal();
                                    if (my_bot.main_list[current_custom_bot].vopros[i].Count > 0)
                                    {
                                        GUILayout.BeginHorizontal();
                                        for (int fg = 0; fg < my_bot.main_list[current_custom_bot].vopros[i].Count(); fg++)
                                        {
                                          
                                            my_bot.main_list[current_custom_bot].vopros[i][fg] = GUILayout.TextField(my_bot.main_list[current_custom_bot].vopros[i][fg]);
                                           
                                        
                                        }
                                        GUILayout.EndHorizontal();
                                        if (GUILayout.Button("X", GUILayout.Width(30f)))
                                        {
                                            my_bot.main_list[current_custom_bot].vopros.Remove(my_bot.main_list[current_custom_bot].vopros[i]);

                                        }
                                    }
                                    GUILayout.EndHorizontal();
                                }
                            }
                            GUILayout.EndScrollView();
                            text_for_bot_field1 = GUILayout.TextField(text_for_bot_field1);
                            if (GUILayout.Button("Add"))
                            {
                                string sr54 = text_for_bot_field1.Trim();
                                if (sr54 != "")
                                {
                                    my_bot.main_list[current_custom_bot].vopros.Add(sr54.Split(new char[] { ' ', ',' }).ToList());
                                    text_for_bot_field1 = "";
                                }
                            }
                            GUILayout.EndVertical();

                            GUILayout.BeginVertical(GUI.skin.box);
                            scroolPos77 = GUILayout.BeginScrollView(scroolPos77);
                            if (my_bot.main_list[current_custom_bot].ovets.Count > 0)
                            {
                              
                                for (int i = 0; i < my_bot.main_list[current_custom_bot].ovets.Count; i++)
                                {
                                    GUILayout.BeginHorizontal();
                                    my_bot.main_list[current_custom_bot].ovets[i] = GUILayout.TextField(my_bot.main_list[current_custom_bot].ovets[i]);
                                    if (GUILayout.Button("X", GUILayout.Width(30f)))
                                    {
                                        my_bot.main_list[current_custom_bot].ovets.Remove(my_bot.main_list[current_custom_bot].ovets[i]);
                                    }
                                    GUILayout.EndHorizontal();
                                }
                             
                            }
                            GUILayout.EndScrollView();

                            text_for_bot_field2 = GUILayout.TextField(text_for_bot_field2);
                            if (GUILayout.Button("Add"))
                            {
                                string sr54 = text_for_bot_field2.Trim();
                                if (sr54 != "")
                                {
                                    my_bot.main_list[current_custom_bot].ovets.Add(text_for_bot_field2);
                                    text_for_bot_field2 = "";
                                }
                            }
                            GUILayout.EndVertical();
                        }
                        GUILayout.EndHorizontal();
                    
                }
            }
        }
        GUILayout.EndArea();
    }
    int current_custom_bot = 0;
    bool[] isclean = new bool[]{false,false,false,false};
    void toDefoult()
    {
        GUILayout.BeginArea(new Rect(Screen.width / 2 - 370, Screen.height / 2 - 250, 840, 450), GUI.skin.box);
        isclean[0] = GUILayout.Toggle(isclean[0], INC.la("general_settings_deff"));
        isclean[1] = GUILayout.Toggle(isclean[1], INC.la("skins_deff"));
        isclean[2] = GUILayout.Toggle(isclean[2], INC.la("personal_deff"));

        GUILayout.BeginHorizontal();
        if (GUILayout.Button(INC.la("to_defff"), GUILayout.Width(150f)))
        {
            if (isclean[0])
            {
                FileInfo info = new FileInfo(INC.configPath);
                if (info.Exists)
                {
                    info.Delete();
                }
                PrefersCyan.Clean();
                loadconfig();
            }
            if (isclean[1])
            {
                INC.cSkins.Clear();
                File.WriteAllText(INC.skinsPath, "", Encoding.UTF8);
                FengGameManagerMKII.myCyanSkin = null;
            }
            if (isclean[2])
            {
                PlayerPrefs.DeleteAll();
            }
            PanelInformer.instance.Add(INC.la("m_defaults"), PanelInformer.LOG_TYPE.INFORMAION);
            mymenu = 0;
        }
        if (GUILayout.Button(INC.la("to_cancle_deff"), GUILayout.Width(150f)))
        {
            mymenu = 0;
        }
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
    Vector2 scrolPoss88 = new Vector2();
    string enojikey = "";
    string emojivalue = "";
    bool is_delite_emoji = false;
    bool is_deleted = false;
    void EmogiList()
    {
        GUILayout.BeginArea(new Rect(Screen.width / 2 - 370, Screen.height / 2 - 250, 840, 450), GUI.skin.box);
        GUILayout.Label(INC.la("info_emoji_1"));
        GUILayout.Label(INC.la("info_emoji"));
        scrolPoss88 = GUILayout.BeginScrollView(scrolPoss88);
        if (InRoomChat.emolis.Count > 0)
        {
            int sss = (InRoomChat.emolis.Count / 3) + 1;
         

            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            {
                for (int s = 0; s < sss; s++)
                {
                    if (s < InRoomChat.emolis.Count())
                    {
                        GUILayout.BeginHorizontal();
                        if (is_delite_emoji && GUILayout.Button("X", GUILayout.Width(30f)))
                        {
                            is_deleted = true;
                            InRoomChat.emolis.Remove(InRoomChat.emolis.ElementAt(s).Key);
                        }
                        GUILayout.TextField("#" + InRoomChat.emolis.ElementAt(s).Key, GUI.skin.label, GUILayout.Width(80));
                        GUILayout.TextField(InRoomChat.emolis.ElementAt(s).Value, GUI.skin.label, GUILayout.Width(140));

                        GUILayout.EndHorizontal();
                    }
                }
            }
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            {
                for (int s = sss; s < sss * 2; s++)
                {
                    if (s < InRoomChat.emolis.Count())
                    {
                        GUILayout.BeginHorizontal();
                        if (is_delite_emoji && GUILayout.Button("X", GUILayout.Width(30f)))
                        {
                            is_deleted = true;
                            InRoomChat.emolis.Remove(InRoomChat.emolis.ElementAt(s).Key);
                        }
                        GUILayout.TextField("#" + InRoomChat.emolis.ElementAt(s).Key, GUI.skin.label, GUILayout.Width(80));
                        GUILayout.TextField(InRoomChat.emolis.ElementAt(s).Value, GUI.skin.label, GUILayout.Width(140));
                        GUILayout.EndHorizontal();
                    }
                }
            }
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            {
                for (int s = sss * 2; s < sss * 3; s++)
                {
                    if (s < InRoomChat.emolis.Count())
                    {
                        GUILayout.BeginHorizontal();
                        if (is_delite_emoji && GUILayout.Button("X", GUILayout.Width(30f)))
                        {
                            is_deleted = true;
                            InRoomChat.emolis.Remove(InRoomChat.emolis.ElementAt(s).Key);
                        }
                        GUILayout.TextField("#" + InRoomChat.emolis.ElementAt(s).Key, GUI.skin.label, GUILayout.Width(80));
                        GUILayout.TextField(InRoomChat.emolis.ElementAt(s).Value, GUI.skin.label, GUILayout.Width(140));
                        GUILayout.EndHorizontal();
                    }
                }
            }
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }
            GUILayout.EndScrollView();
            GUILayout.BeginHorizontal();
            if (is_deleted)
            {
                if (GUILayout.Button(INC.la("saved_emoji"), GUILayout.Width(120f)))
                {
                    string str = "";
                    foreach (var ss in InRoomChat.emolis)
                    {
                        str = str + ss.Key + ":" + ss.Value + "\n";
                    }
                    File.WriteAllText(Application.dataPath + "/Emoji.txt", str, Encoding.UTF8);
                    is_deleted = false;
                    is_delite_emoji = false;
                }
                if (GUILayout.Button(INC.la("cansel_emoji"), GUILayout.Width(120f)))
                {
                    InRoomChat.Load_emoji(false);
                    is_deleted = false;
                    is_delite_emoji = false;
                }
            }
            else
            {
                GUILayout.Label(INC.la("key_emoji"), GUILayout.Width(80f));
                enojikey = GUILayout.TextField(enojikey, GUILayout.Width(80f));
                GUILayout.Label(INC.la("value_emoji"), GUILayout.Width(80f));
                emojivalue = GUILayout.TextField(emojivalue);
                if (GUILayout.Button(INC.la("add_emoji"), GUILayout.Width(120f)))
                {
                    if ((enojikey = enojikey.Replace("#", "")).Trim() != "" && (emojivalue = emojivalue.Replace(":", "")).Trim() != "" && !InRoomChat.emolis.ContainsKey(enojikey.Trim()))
                    {
                        InRoomChat.emolis.Add(enojikey, emojivalue);
                        string str = "";
                        foreach (var ss in InRoomChat.emolis)
                        {
                            str = str + ss.Key + ":" + ss.Value + "\n";
                        }
                        File.WriteAllText(Application.dataPath + "/Emoji.txt", str, Encoding.UTF8);
                        enojikey = ""; emojivalue = "";
                    }
                }
                is_delite_emoji = GUILayout.Toggle(is_delite_emoji, INC.la("delite_emoji"), GUILayout.Width(100f));
            }
            GUILayout.EndHorizontal();
        
        GUILayout.EndArea();
    }
    void ChageGrafix(int i = 0)
    {
        if ((int)FengGameManagerMKII.settings[354] == 1)
        {
            if (grafic == null)
            {
                grafic = new GameObject("panel_kills").AddComponent<Grafics_Cyan_mod>();
                grafic.name_graf = "kills/minute:";
                grafic.name_graf2 = "overall:";
                grafic.isValue2 = true;
                grafic.winID = 201;
                grafic.rect.x = ((Vector2)FengGameManagerMKII.settings[358]).x;
                grafic.rect.y = ((Vector2)FengGameManagerMKII.settings[358]).y;
            }
            else
            {
                if (i == 0)
                {
                    kills_last = 0;
                    timer_kills_min = 0;
                    kills_s = 0;
                    grafic.clean();
                    grafic.value = 0;
                    grafic.value2 = 0;
                }
            }
        }
        else
        {
            Destroy(grafic);
        }
        if ((int)FengGameManagerMKII.settings[355] == 1)
        {

            if (grafic_damage_in_min == null)
            {
                grafic_damage_in_min = new GameObject("panel_damage").AddComponent<Grafics_Cyan_mod>();
                grafic_damage_in_min.name_graf = "damage/minute:";
                grafic_damage_in_min.name_graf2 = "overall:";
                grafic_damage_in_min.isValue2 = true;
                grafic_damage_in_min.winID = 200;
                grafic_damage_in_min.rect.x = ((Vector2)FengGameManagerMKII.settings[359]).x;
                grafic_damage_in_min.rect.y = ((Vector2)FengGameManagerMKII.settings[359]).y;
            }
            else
            {
                if (i == 0)
                {
                    damage_last = 0;
                    timer_kills_min = 0;
                    damage_s = 0;
                    grafic_damage_in_min.clean();
                    grafic_damage_in_min.value = 0;
                    grafic_damage_in_min.value2 = 0;
                }
            }
        }
        else
        {
            Destroy(grafic_damage_in_min);
        }
    }
    public void OnJoinedLobby()
    {
        NGUITools.SetActive(UIMainReferences.instance.panelMultiStart, false);
        NGUITools.SetActive(UIMainReferences.instance.panelMultiROOM, true);
        NGUITools.SetActive(UIMainReferences.instance.PanelMultiJoinPrivate, false);
    }
 
    public void OnJoinedRoom()
    {
        InRoomChat.chat_cont = string.Empty;
        InRoomChat.ChatContentList = new List<InRoomChat.ChatContent>();
        selplayer = PhotonNetwork.player;
        AddPl = new List<PhotonPlayer> { PhotonNetwork.player };
        this.maxPlayers = PhotonNetwork.room.maxPlayers;
        this.playerList = string.Empty;
        FengGameManagerMKII.level = PhotonNetwork.room.MapName;
        lvlInfo = LevelInfo.getInfo(FengGameManagerMKII.level);
        UnityEngine.Debug.Log("OnJoinedRoom " + PhotonNetwork.room.name + "    >>>>   " + lvlInfo.mapName);
        this.gameTimesUp = false;
    
        if (PhotonNetwork.room.Difficulty.ToLower() == "normal")
        {
            IN_GAME_MAIN_CAMERA.difficulty = DIFFICULTY.NORMAL;
        }
        else if (PhotonNetwork.room.Difficulty.ToLower() == "hard")
        {
            IN_GAME_MAIN_CAMERA.difficulty = DIFFICULTY.HARD;
        }
        else if (PhotonNetwork.room.Difficulty.ToLower() == "abnormal")
        {
            IN_GAME_MAIN_CAMERA.difficulty = DIFFICULTY.ABNORMAL;
        }
        this.time = PhotonNetwork.room.FirstTime;
        this.time *= 60;
        if (PhotonNetwork.room.DayTime.ToLower() == "day")
        {
            IN_GAME_MAIN_CAMERA.dayLight = DayLight.Day;
        }
        else if (PhotonNetwork.room.DayTime.ToLower() == "dawn")
        {
            IN_GAME_MAIN_CAMERA.dayLight = DayLight.Dawn;
        }
        else if (PhotonNetwork.room.DayTime.ToLower() == "night")
        {
            IN_GAME_MAIN_CAMERA.dayLight = DayLight.Night;
        }

        IN_GAME_MAIN_CAMERA.gamemode = lvlInfo.type;
        PhotonNetwork.LoadLevel(lvlInfo.mapName);
        this.name = LoginFengKAI.player.name;
        if (FengGameManagerMKII.loginstate != 3)
        {
            this.name = (string)FengGameManagerMKII.settings[363];
            LoginFengKAI.player.name = this.name;
            string str3 = string.Empty;
            string g1trimg = ((string)FengGameManagerMKII.settings[361]).Trim();
            string g1trimg2 = ((string)FengGameManagerMKII.settings[362]).Trim();
            if (g1trimg != string.Empty && g1trimg2 != string.Empty)
            {
                str3 = g1trimg + "\n" + g1trimg2;
            }
            else if (g1trimg != string.Empty)
            {
                str3 = g1trimg;
            }
            else if (g1trimg2 != string.Empty)
            {
                str3 = g1trimg2;
            }
            LoginFengKAI.player.guildname = str3;
        }
        PhotonNetwork.player.name2 = LoginFengKAI.player.name;
        PhotonNetwork.player.guildName = LoginFengKAI.player.guildname;
        PhotonNetwork.player.kills = 0;
        PhotonNetwork.player.max_dmg = 0;
        PhotonNetwork.player.total_dmg = 0;
        PhotonNetwork.player.deaths = 0;
        PhotonNetwork.player.dead = true;
        PhotonNetwork.player.isTitan = 0;
        PhotonNetwork.player.RCteam = 0;
        PhotonNetwork.player.currentLevel = string.Empty;
        base.photonView.RPC("verifyPlayerHasLeft", PhotonTargets.All, new object[] { 0 - Convert.ToInt32(UIMainReferences.CyanModVers.Replace(".", "")) });
        foreach (PhotonPlayer pl in PhotonNetwork.playerList)
        {
            INC.add_pl(pl);
        }
        this.humanScore = 0;
        this.titanScore = 0;
        this.PVPtitanScore = 0;
        this.PVPhumanScore = 0;
        this.wave = 1;
        this.highestwave = 1;
        this.localRacingResult = string.Empty;
        this.needChooseSide = true;
        this.killInfoGO = new ArrayList();
        if (!PhotonNetwork.isMasterClient)
        {
            base.photonView.RPC("RequireStatus", PhotonTargets.MasterClient, new object[0]);
        }
        this.assetCacheTextures = new Dictionary<string, Texture2D>();
        this.isFirstLoad = true;
        if (FengGameManagerMKII.OnPrivateServer)
        {
            FengGameManagerMKII.ServerRequestAuthentication(FengGameManagerMKII.PrivateServerAuthPass);
        }
    }

    public void OnLeftLobby()
    {
        UnityEngine.Debug.Log("OnLeftLobby");
    }

    public void OnLeftRoom()
    {
        if (Application.loadedLevel != 0)
        {
            Time.timeScale = 1f;
            if (PhotonNetwork.connected)
            {
                PhotonNetwork.Disconnect();
            }
            this.resetSettings(true);
            this.loadconfig();
            IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.STOP;
            this.gameStart = false;
            Screen.lockCursor = false;
            Screen.showCursor = true;
            FengGameManagerMKII.instance.MenuOn = false;
            this.DestroyAllExistingCloths();
            UnityEngine.Object.Destroy(base.gameObject);
            Application.LoadLevel("menu");
        }
    }
    public void FamelTitanRandomSpawn(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Transform tran = CyanMod.CachingsGM.Find("titanRespawn").transform;
            GameObject ft = PhotonNetwork.Instantiate("FEMALE_TITAN", tran.position, tran.rotation, 0);
            GameObject[] respawn = GameObject.FindGameObjectsWithTag("titanRespawn");
            Vector3 pos = respawn[UnityEngine.Random.Range(0, respawn.Length)].transform.localPosition;
            ft.transform.localPosition = pos;
            GameObject obj2 = PhotonNetwork.Instantiate("FX/FXtitanSpawn", ft.transform.position, Quaternion.Euler(-90f, 0f, 0f), 0);

        }
    }
 
    private void OnLevelWasLoaded(int level)
    {
        if (((level != 0) && (Application.loadedLevelName != "characterCreation")) && (Application.loadedLevelName != "SnapShot"))
        {

            selplayer = PhotonNetwork.player;
            AddPl = new List<PhotonPlayer> { PhotonNetwork.player };
            ChangeQuality.setCurrentQuality();
            foreach (GameObject obj2 in alltitans)
            {
                if ((obj2.GetPhotonView() == null) || !obj2.GetPhotonView().owner.isMasterClient)
                {
                    UnityEngine.Object.Destroy(obj2);
                }
            }
            this.isWinning = false;
            this.gameStart = true;
            this.ShowHUDInfoCenter(string.Empty);
            GameObject cdp = CyanMod.CachingsGM.Find("cameraDefaultPosition");
            Transform trs = cdp.transform;
            GameObject obj3 = (GameObject)UnityEngine.Object.Instantiate(Cach.MainCamera_mono != null ? Cach.MainCamera_mono : Cach.MainCamera_mono = (GameObject)Resources.Load("MainCamera_mono"), trs.position, trs.rotation);
            UnityEngine.Object.Destroy(cdp);
            obj3.name = "MainCamera";

            Screen.lockCursor = true;
            Screen.showCursor = true;
            this.ui = (GameObject)UnityEngine.Object.Instantiate(Cach.UI_IN_GAME != null ? Cach.UI_IN_GAME : Cach.UI_IN_GAME = (GameObject)Resources.Load("UI_IN_GAME"));
            this.ui.name = "UI_IN_GAME";
            this.ui.SetActive(true);
            uiT = ui.GetComponent<UIReferArray>();
            NGUITools.SetActive(uiT.panels[0], true);
            NGUITools.SetActive(uiT.panels[1], false);
            NGUITools.SetActive(uiT.panels[2], false);
            NGUITools.SetActive(uiT.panels[3], false);
            lvlInfo = LevelInfo.getInfo(FengGameManagerMKII.level);
            this.cache();
            this.loadskin();
            IN_GAME_MAIN_CAMERA.instance.setHUDposition();
            IN_GAME_MAIN_CAMERA.instance.setDayLight(IN_GAME_MAIN_CAMERA.dayLight);
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                this.single_kills = 0;
                this.single_maxDamage = 0;
                this.single_totalDamage = 0;
                IN_GAME_MAIN_CAMERA.instance.enabled = true;
                IN_GAME_MAIN_CAMERA.instance.Smov.disable = true;
                IN_GAME_MAIN_CAMERA.instance.mouselook.disable = true;
                IN_GAME_MAIN_CAMERA.gamemode = lvlInfo.type;
                this.SpawnPlayer(IN_GAME_MAIN_CAMERA.singleCharacter.ToUpper(), "playerRespawn");
                if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.TPS || IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.oldTPS)
                {
                    Screen.lockCursor = true;
                }
                else
                {
                    Screen.lockCursor = false;
                }
                Screen.showCursor = false;
                int abnormal = 90;
                if (IN_GAME_MAIN_CAMERA.difficulty == DIFFICULTY.HARD)
                {
                    abnormal = 70;
                }
                this.spawnTitanCustom("titanRespawn", abnormal, lvlInfo.enemyNumber, false);
                ChageGrafix();
            }
            else
            {
                PVPcheckPoint.chkPts = new ArrayList();
                IN_GAME_MAIN_CAMERA.instance.enabled = false;
                Camera.main.GetComponent<CameraShake>().enabled = false;
                IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.MULTIPLAYER;

                if (lvlInfo.type == GAMEMODE.TROST)
                {
                    GameObject playerRespawn = CyanMod.CachingsGM.Find("playerRespawn");
                    playerRespawn.SetActive(false);
                    UnityEngine.Object.Destroy(playerRespawn);
                    CyanMod.CachingsGM.Find("rock").animation["lift"].speed = 0f;
                    CyanMod.CachingsGM.Find("door_fine").SetActive(false);
                    CyanMod.CachingsGM.Find("door_broke").SetActive(true);
                    UnityEngine.Object.Destroy(CyanMod.CachingsGM.Find("ppl"));
                }
                else if (lvlInfo.type == GAMEMODE.BOSS_FIGHT_CT)
                {
                    GameObject playerRespawn = CyanMod.CachingsGM.Find("playerRespawnTrost");
                    playerRespawn.SetActive(false);
                    UnityEngine.Object.Destroy(playerRespawn);
                }
                if (this.needChooseSide)
                {
                    this.ShowHUDInfoTopCenterADD("\n\nPRESS 1 TO ENTER GAME");
                }
                else if (((int)settings[0xf5]) == 0)
                {
                    if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.TPS || IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.oldTPS)
                    {
                        Screen.lockCursor = true;
                    }
                    else
                    {
                        Screen.lockCursor = false;
                    }
                    if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE)
                    {

                        if ((PhotonNetwork.player.isTitan) == 2)
                        {
                            this.checkpoint = CyanMod.CachingsGM.Find("PVPchkPtT");
                        }
                        else
                        {
                            this.checkpoint = CyanMod.CachingsGM.Find("PVPchkPtH");
                        }
                    }
                    if ((PhotonNetwork.player.isTitan) == 2)
                    {
                        this.SpawnNonAITitan2(this.myLastHero, "titanRespawn");
                    }
                    else
                    {
                        this.SpawnPlayer(this.myLastHero, this.myLastRespawnTag);
                    }
                }
                if (lvlInfo.type == GAMEMODE.BOSS_FIGHT_CT)
                {
                    UnityEngine.Object.Destroy(CyanMod.CachingsGM.Find("rock"));
                }
                if (PhotonNetwork.isMasterClient)
                {
                    if (Famel_Titan_mode_survive)
                    {
                        FamelTitanRandomSpawn(1);
                        isUpdateFT = false;
                    }
                    else
                    {
                        if (lvlInfo.type == GAMEMODE.TROST)
                        {
                            if (!this.isPlayerAllDead2())
                            {
                                PhotonNetwork.Instantiate("TITAN_EREN_trost", new Vector3(-200f, 0f, -194f), Quaternion.Euler(0f, 180f, 0f), 0).GetComponent<TITAN_EREN>().rockLift = true;
                                int rate = 90;
                                if (IN_GAME_MAIN_CAMERA.difficulty == DIFFICULTY.HARD)
                                {
                                    rate = 70;
                                }
                                GameObject[] objArray = GameObject.FindGameObjectsWithTag("titanRespawn");
                                GameObject obj4 = CyanMod.CachingsGM.Find("titanRespawnTrost");
                                if (obj4 != null)
                                {
                                    foreach (GameObject obj5 in objArray)
                                    {
                                        if (obj5.transform.parent.gameObject == obj4)
                                        {
                                            this.spawnTitan(rate, obj5.transform.position, obj5.transform.rotation, false);
                                        }
                                    }
                                }
                            }
                        }
                        else if (lvlInfo.type == GAMEMODE.BOSS_FIGHT_CT)
                        {
                            if (!this.isPlayerAllDead2())
                            {
                                PhotonNetwork.Instantiate("COLOSSAL_TITAN", (Vector3)(-Vector3.up * 10000f), Quaternion.Euler(0f, 180f, 0f), 0);
                            }
                        }
                        else if (((lvlInfo.type == GAMEMODE.KILL_TITAN) || (lvlInfo.type == GAMEMODE.ENDLESS_TITAN)) || (lvlInfo.type == GAMEMODE.SURVIVE_MODE))
                        {
                            if ((lvlInfo.name == "Annie") || (lvlInfo.name == "Annie II"))
                            {
                                GameObject tr = CyanMod.CachingsGM.Find("titanRespawn");
                                PhotonNetwork.Instantiate("FEMALE_TITAN", tr.transform.position, tr.transform.rotation, 0);
                            }
                            else
                            {
                                int num3 = 90;
                                if (IN_GAME_MAIN_CAMERA.difficulty == DIFFICULTY.HARD)
                                {
                                    num3 = 70;
                                }
                                this.spawnTitanCustom("titanRespawn", num3, lvlInfo.enemyNumber, false);
                            }
                        }
                        else if (((lvlInfo.type != GAMEMODE.TROST) && (lvlInfo.type == GAMEMODE.PVP_CAPTURE)) && (lvlInfo.mapName == "OutSide"))
                        {
                            GameObject[] objArray2 = GameObject.FindGameObjectsWithTag("titanRespawn");
                            if (objArray2.Length <= 0)
                            {
                                return;
                            }
                            for (int i = 0; i < objArray2.Length; i++)
                            {
                                this.spawnTitanRaw(objArray2[i].transform.position, objArray2[i].transform.rotation).setAbnormalType2(AbnormalType.TYPE_CRAWLER, true);
                            }
                        }
                    }
                }
                if (!lvlInfo.supply)
                {
                    UnityEngine.Object.Destroy(CyanMod.CachingsGM.Find("aot_supply"));
                }
                if (!PhotonNetwork.isMasterClient)
                {
                    base.photonView.RPC("RequireStatus", PhotonTargets.MasterClient, new object[0]);
                }
              
                if (((int)settings[0xf5]) == 1)
                {
                    this.EnterSpecMode(true);
                }
            }
            if (LavaMode || (lvlInfo.lavaMode))
            {
                GameObject aot_supply = CyanMod.CachingsGM.Find("aot_supply");
                GameObject lava_pos = CyanMod.CachingsGM.Find("aot_supply_lava_position");
                if (lava_pos == null)
                {
                    if (lvlInfo.mapName == "The City I")
                    {
                        aot_supply.transform.position = new Vector3(36, 54, -435);
                    }
                    else
                    {
                        aot_supply.transform.position = new Vector3(-48.5f, 80, -240.9f);
                    }
                }
                else
                {
                    Transform trans = lava_pos.transform;
                    aot_supply.transform.position = trans.position;
                    aot_supply.transform.rotation = trans.rotation;
                }
                UnityEngine.Object.Instantiate(Cach.levelBottom != null ? Cach.levelBottom : Cach.levelBottom = (GameObject)Resources.Load("levelBottom"), new Vector3(0f, -29.5f, 0f), Quaternion.Euler(0f, 0f, 0f));
            }
            this.RecompilePlayerList(0.1f);
            EnableSprite(false);
            ApplySettings();
            panelScore = new PanelScore(LoginFengKAI.player.name, lvlInfo);
        }
    }
    public PanelScore panelScore;
    public void OnMasterClientSwitched(PhotonPlayer newMasterClient)
    {
        if (!noRestart)
        {
            if (PhotonNetwork.isMasterClient)
            {
                this.restartingMC = true;
                if (RCSettings.infectionMode > 0)
                {
                    this.restartingTitan = true;
                }
                if (RCSettings.bombMode > 0)
                {
                    this.restartingBomb = true;
                }
                if (RCSettings.horseMode > 0)
                {
                    this.restartingHorse = true;
                }
                if (RCSettings.banEren == 0)
                {
                    this.restartingEren = true;
                }
            }
            this.resetSettings(false);
            if (!lvlInfo.teamTitan)
            {
                PhotonNetwork.player.isTitan = 1;
            }
            if (!this.gameTimesUp && PhotonNetwork.isMasterClient)
            {
                this.restartGame2(true);
                base.photonView.RPC("setMasterRC", PhotonTargets.All, new object[0]);
            }
        }
        noRestart = false;
    }

    public void OnPhotonCreateRoomFailed()
    {
        UnityEngine.Debug.Log("OnPhotonCreateRoomFailed");
    }

    public void OnPhotonCustomRoomPropertiesChanged()
    {
        if (PhotonNetwork.isMasterClient)
        {
            if (!PhotonNetwork.room.open)
            {
                PhotonNetwork.room.open = true;
            }
            if (!PhotonNetwork.room.visible)
            {
                PhotonNetwork.room.visible = true;
            }
            if (PhotonNetwork.room.maxPlayers != this.maxPlayers)
            {
                PhotonNetwork.room.maxPlayers = this.maxPlayers;
            }
        }
        else
        {
            this.maxPlayers = PhotonNetwork.room.maxPlayers;
        }
    }

    public void OnPhotonInstantiate()
    {
        UnityEngine.Debug.Log("OnPhotonInstantiate");
    }

    public void OnPhotonJoinRoomFailed()
    {
        UnityEngine.Debug.Log("OnPhotonJoinRoomFailed");
        PanelServerList.instance.conected = false;
    }

    public void OnPhotonMaxCccuReached()
    {
        UnityEngine.Debug.Log("OnPhotonMaxCccuReached");
    }

    bool CheckAutokicked(PhotonPlayer player)
    {
        string player_name = ((string)player.customProperties[PhotonPlayerProperty.name]).Trim();
        if ((int)settings[299] == 1 && player_name.StripHex() == string.Empty)
        {
            this.kickPlayerRC(player, false, INC.la("no_nick_name_kick"));
            return false;
        }
        else if ((int)settings[277] == 1 && player_name.StripHex().StartsWith("Vivid-Assassin"))
        {
            this.kickPlayerRC(player, false, "Vivid-Assassin");
            return false;
        }
        else if ((int)settings[278] == 1 && player_name.StartsWith("[ffd700]Hyper-MegaCannon"))
        {
            this.kickPlayerRC(player, false, "Hyper-MegaCannon");
            return false;
        }
        else if ((int)settings[279] == 1 && player_name.StartsWith("[FF0000]Tokyo Ghoul"))
        {
            this.kickPlayerRC(player, false, "Tokyo Ghoul");
            return false;
        }
        else if ((int)settings[297] == 1 && (int)settings[298] == 1 && player_name.StartsWith("GUEST"))
        {
            this.kickPlayerRC(player, false, "");
            return false;
        }
        else if ((int)settings[297] == 1 && player_name.StartsWith("GUEST"))
        {
            this.kickPlayerRC(player, false, INC.la("anti_guest"));
            return false;
        }
        else if (INC.banlist.Count > 0)
        {
            foreach (string str in INC.banlist)
            {
                string dde = str.Trim();
                if (player_name.Contains(dde))
                {
                    this.kickPlayerRC(player, false, INC.la("banned_banlist_player") + dde);
                    return false;
                }
            }
        }
        return true;
    }

    IEnumerator PlayerConected(PhotonPlayer player)
    {
        int count = 0;
        while (player.customProperties[PhotonPlayerProperty.name] == null)
        {
            yield return new WaitForSeconds(0.3f);
            count++;
            if (PhotonPlayer.Find(player.ID) == null)
            {
                yield break;
            }
            if (count > 30)
            {
                INC.add_pl(player);
                if (PhotonNetwork.isMasterClient)
                {
                    kickPlayerRC(player, false, INC.la("ghost_player"));
                }
                yield break;
            }
        }
        if (PhotonPlayer.Find(player.ID) == null)
        {
            yield break;
        }
        INC.add_pl(player);
        InRoomChat.Recompile();
        if (PhotonNetwork.isMasterClient)
        {
            string playernae = (string)player.customProperties[PhotonPlayerProperty.name];
            PhotonView photonView = base.photonView;
            if (FengGameManagerMKII.banHash.ContainsValue(playernae))
            {
                this.kickPlayerRC(player, false, INC.la("banned_player"));
                yield break;
            }
            yield return new WaitForSeconds(0.3f);
            if (!CheckAutokicked(player))
            {
                yield break;
            }
            foreach (System.Collections.DictionaryEntry x in player.customProperties)
            {
                CheckPropertyL(player, x.Key.ToString());
            }
            int num = (player.statACL);
            int num2 = (player.statBLA);
            int num3 = (player.statGAS);
            int num4 = (player.statSPD);
            if((int)FengGameManagerMKII.settings[398] == 1 && (num > 150 || num2 > 125 || num3 > 150 || num4 > 140))
            {
                this.kickPlayerRC(player, true, INC.la("excessive_stats_player"));
                yield break;
            }
            if (RCSettings.asoPreservekdr == 1)
            {
                base.StartCoroutine(this.WaitAndReloadKDR(player));
            }
            if (FengGameManagerMKII.level.StartsWith("Custom"))
            {
                base.StartCoroutine(this.customlevelE(new List<PhotonPlayer>
				{
					player
				}));
            }
            ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
            if (RCSettings.bombMode == 1)
            {
                hashtable.Add("bomb", 1);
            }
            if (RCSettings.globalDisableMinimap == 1)
            {
                hashtable.Add("globalDisableMinimap", 1);
            }
            if (RCSettings.teamMode > 0)
            {
                hashtable.Add("team", RCSettings.teamMode);
            }
            if (RCSettings.pointMode > 0)
            {
                hashtable.Add("point", RCSettings.pointMode);
            }
            if (RCSettings.disableRock > 0)
            {
                hashtable.Add("rock", RCSettings.disableRock);
            }
            if (RCSettings.explodeMode > 0)
            {
                hashtable.Add("explode", RCSettings.explodeMode);
            }
            if (RCSettings.healthMode > 0)
            {
                hashtable.Add("healthMode", RCSettings.healthMode);
                hashtable.Add("healthLower", RCSettings.healthLower);
                hashtable.Add("healthUpper", RCSettings.healthUpper);
            }
            if (RCSettings.infectionMode > 0)
            {
                hashtable.Add("infection", RCSettings.infectionMode);
            }
            if (RCSettings.banEren == 1)
            {
                hashtable.Add("eren", RCSettings.banEren);
            }
            if (RCSettings.moreTitans > 0)
            {
                hashtable.Add("titanc", RCSettings.moreTitans);
            }
            if (RCSettings.damageMode > 0)
            {
                hashtable.Add("damage", RCSettings.damageMode);
            }
            if (RCSettings.sizeMode > 0)
            {
                hashtable.Add("sizeMode", RCSettings.sizeMode);
                hashtable.Add("sizeLower", RCSettings.sizeLower);
                hashtable.Add("sizeUpper", RCSettings.sizeUpper);
            }
            if (RCSettings.spawnMode > 0)
            {
                hashtable.Add("spawnMode", RCSettings.spawnMode);
                hashtable.Add("nRate", RCSettings.nRate);
                hashtable.Add("aRate", RCSettings.aRate);
                hashtable.Add("jRate", RCSettings.jRate);
                hashtable.Add("cRate", RCSettings.cRate);
                hashtable.Add("pRate", RCSettings.pRate);
            }
            if (RCSettings.waveModeOn > 0)
            {
                hashtable.Add("waveModeOn", 1);
                hashtable.Add("waveModeNum", RCSettings.waveModeNum);
            }
            if (RCSettings.friendlyMode > 0)
            {
                hashtable.Add("friendly", 1);
            }
            if (RCSettings.pvpMode > 0)
            {
                hashtable.Add("pvp", RCSettings.pvpMode);
            }
            if (RCSettings.maxWave > 0)
            {
                hashtable.Add("maxwave", RCSettings.maxWave);
            }
            if (RCSettings.endlessMode > 0)
            {
                hashtable.Add("endless", RCSettings.endlessMode);
            }
            if (RCSettings.motd != string.Empty)
            {
                hashtable.Add("motd", RCSettings.motd);
            }
            if (RCSettings.horseMode > 0)
            {
                hashtable.Add("horse", RCSettings.horseMode);
            }
            if (RCSettings.ahssReload > 0)
            {
                hashtable.Add("ahssReload", RCSettings.ahssReload);
            }
            if (RCSettings.punkWaves > 0)
            {
                hashtable.Add("punkWaves", RCSettings.punkWaves);
            }
            if (RCSettings.deadlyCannons > 0)
            {
                hashtable.Add("deadlycannons", RCSettings.deadlyCannons);
            }
            if (RCSettings.racingStatic > 0)
            {
                hashtable.Add("asoracing", RCSettings.racingStatic);
            }
            if (RCSettings.AnnieSurvive > 0)
            {
                hashtable.Add("annie_mode", RCSettings.AnnieSurvive);
            }
            if (FengGameManagerMKII.ignoreList != null && FengGameManagerMKII.ignoreList.Count > 0)
            {
                photonView.RPC("ignorePlayerArray", player, new object[]
				{
					FengGameManagerMKII.ignoreList.ToArray()
				});
            }
            photonView.RPC("settingRPC", player, new object[]
			{
				hashtable
			});
            photonView.RPC("setMasterRC", player, new object[0]);
            if (Time.timeScale <= 0.1f && this.pauseWaitTime > 3f)
            {
                photonView.RPC("pauseRPC", player, new object[] { true });
                cext.mess(INC.la("to_paused_game"), player);
            }
            ChageSetingInfo(player);
        }
        else
        {
            yield return new WaitForSeconds(0.3f);
            foreach (System.Collections.DictionaryEntry x in player.customProperties)
            {
                CheckPropertyL(player, x.Key.ToString());
            }
        }
        this.RecompilePlayerList(0.1f);
        yield break;
    }
    public void OnPhotonPlayerConnected(PhotonPlayer player)
    {
        StartCoroutine(PlayerConected(player));
        if ((int)FengGameManagerMKII.settings[334] == 1)
        {
            if (InRoomChat.ChatContentList.Count > 15)
            {
                InRoomChat.ChatContentList.Remove(InRoomChat.ChatContentList[0]);
            }
            InRoomChat.ChatContentList.Add(new InRoomChat.ChatContent(INC.la("connected_player"), player));
            InRoomChat.Recompile();
        }
        base.photonView.RPC("verifyPlayerHasLeft", player, new object[] { 0 - Convert.ToInt32(UIMainReferences.CyanModVers.Replace(".","")) });
        this.RecompilePlayerList(0.1f);
       

    }

    public void OnPhotonPlayerDisconnected(PhotonPlayer player)
    {
        foreach (Conference.cnf con in confer.conference_list)
        {
            if (con.players_ID.Contains(player.ID))
            {
                con.players_ID.Remove(player.ID);
                con.messages.Add(INC.la("disc_conf") + player.iscleanname);
                if (!PhotonPlayer.InRoom(con.master))
                {
                    con.master = PhotonPlayer.Find(con.players_ID[0]);
                }
            }
        }
        if ((int)FengGameManagerMKII.settings[334] == 1)
        {
            if (InRoomChat.ChatContentList.Count > 15)
            {
                InRoomChat.ChatContentList.Remove(InRoomChat.ChatContentList[0]);
            }
            InRoomChat.ChatContentList.Add(new InRoomChat.ChatContent(INC.la("disc_player"), player));
           
        }
        InRoomChat.Recompile();
        if (!this.gameTimesUp)
        {
            this.oneTitanDown(string.Empty, true);
            this.someOneIsDead(0);
        }
        if (ignoreList.Contains(player.ID))
        {
            ignoreList.Remove(player.ID);
        }
        InstantiateTracker.instance.TryRemovePlayer(player.ID);
        if (PhotonNetwork.isMasterClient)
        {
            base.photonView.RPC("verifyPlayerHasLeft", PhotonTargets.All, new object[] { player.ID });
        }
        if (RCSettings.asoPreservekdr == 1)
        {
            string key = player.name2;
            if (this.PreservedPlayerKDR.ContainsKey(key))
            {
                this.PreservedPlayerKDR.Remove(key);
            }
            int[] numArray = new int[] { (player.kills), (player.deaths), (player.max_dmg), (player.total_dmg) };
            this.PreservedPlayerKDR.Add(key, numArray);
        }
        this.RecompilePlayerList(0.1f);
    }

    public void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
    {
        this.RecompilePlayerList(0.1f);
    }

    public void OnPhotonRandomJoinFailed()
    {
        UnityEngine.Debug.Log("OnPhotonRandomJoinFailed");
    }

    public void OnPhotonSerializeView()
    {
        UnityEngine.Debug.Log("OnPhotonSerializeView");
    }

    public void OnReceivedRoomListUpdate()
    {
    }

    public void OnUpdate()
    {
        if (RCEvents.ContainsKey("OnUpdate"))
        {
            if (this.updateTime > 0f)
            {
                this.updateTime -= Time.deltaTime;
            }
            else
            {
                ((RCEvent)RCEvents["OnUpdate"]).checkEvent();
                this.updateTime = 1f;
            }
        }
    }

    public void OnUpdatedFriendList()
    {
        UnityEngine.Debug.Log("OnUpdatedFriendList");
    }

    public int operantType(string str, int condition)
    {
        switch (condition)
        {
            case 0:
            case 3:
                if (str.StartsWith("Equals"))
                {
                    return 2;
                }
                if (!str.StartsWith("NotEquals"))
                {
                    if (!str.StartsWith("LessThan"))
                    {
                        if (str.StartsWith("LessThanOrEquals"))
                        {
                            return 1;
                        }
                        if (str.StartsWith("GreaterThanOrEquals"))
                        {
                            return 3;
                        }
                        if (str.StartsWith("GreaterThan"))
                        {
                            return 4;
                        }
                    }
                    return 0;
                }
                return 5;

            case 1:
            case 4:
            case 5:
                if (str.StartsWith("Equals"))
                {
                    return 2;
                }
                if (!str.StartsWith("NotEquals"))
                {
                    return 0;
                }
                return 5;

            case 2:
                if (str.StartsWith("Equals"))
                {
                    return 0;
                }
                if (!str.StartsWith("NotEquals"))
                {
                    if (str.StartsWith("Contains"))
                    {
                        return 2;
                    }
                    if (str.StartsWith("NotContains"))
                    {
                        return 3;
                    }
                    if (str.StartsWith("StartsWith"))
                    {
                        return 4;
                    }
                    if (str.StartsWith("NotStartsWith"))
                    {
                        return 5;
                    }
                    if (str.StartsWith("EndsWith"))
                    {
                        return 6;
                    }
                    if (str.StartsWith("NotEndsWith"))
                    {
                        return 7;
                    }
                    return 0;
                }
                return 1;
        }
        return 0;
    }

    public RCEvent parseBlock(string[] stringArray, int eventClass, int eventType, RCCondition condition)
    {
        List<RCAction> sentTrueActions = new List<RCAction>();
        RCEvent event2 = new RCEvent(null, null, 0, 0);
        for (int i = 0; i < stringArray.Length; i++)
        {
            int num2;
            int num3;
            int num4;
            int length;
            string[] strArray;
            int num6;
            int num7;
            int index;
            int num9;
            string str;
            int num10;
            int num11;
            int num12;
            string[] strArray2;
            RCCondition condition2;
            RCEvent event3;
            RCAction action;
            if (stringArray[i].StartsWith("If") && (stringArray[i + 1] == "{"))
            {
                num2 = i + 2;
                num3 = i + 2;
                num4 = 0;
                length = i + 2;
                while (length < stringArray.Length)
                {
                    if (stringArray[length] == "{")
                    {
                        num4++;
                    }
                    if (stringArray[length] == "}")
                    {
                        if (num4 > 0)
                        {
                            num4--;
                        }
                        else
                        {
                            num3 = length - 1;
                            length = stringArray.Length;
                        }
                    }
                    length++;
                }
                strArray = new string[(num3 - num2) + 1];
                num6 = 0;
                num7 = num2;
                while (num7 <= num3)
                {
                    strArray[num6] = stringArray[num7];
                    num6++;
                    num7++;
                }
                index = stringArray[i].IndexOf("(");
                num9 = stringArray[i].LastIndexOf(")");
                str = stringArray[i].Substring(index + 1, (num9 - index) - 1);
                num10 = this.conditionType(str);
                num11 = str.IndexOf('.');
                str = str.Substring(num11 + 1);
                num12 = this.operantType(str, num10);
                index = str.IndexOf('(');
                num9 = str.LastIndexOf(")");
                strArray2 = str.Substring(index + 1, (num9 - index) - 1).Split(new char[] { ',' });
                condition2 = new RCCondition(num12, num10, this.returnHelper(strArray2[0]), this.returnHelper(strArray2[1]));
                event3 = this.parseBlock(strArray, 1, 0, condition2);
                action = new RCAction(0, 0, event3, null);
                event2 = event3;
                sentTrueActions.Add(action);
                i = num3;
            }
            else if (stringArray[i].StartsWith("While") && (stringArray[i + 1] == "{"))
            {
                num2 = i + 2;
                num3 = i + 2;
                num4 = 0;
                length = i + 2;
                while (length < stringArray.Length)
                {
                    if (stringArray[length] == "{")
                    {
                        num4++;
                    }
                    if (stringArray[length] == "}")
                    {
                        if (num4 > 0)
                        {
                            num4--;
                        }
                        else
                        {
                            num3 = length - 1;
                            length = stringArray.Length;
                        }
                    }
                    length++;
                }
                strArray = new string[(num3 - num2) + 1];
                num6 = 0;
                num7 = num2;
                while (num7 <= num3)
                {
                    strArray[num6] = stringArray[num7];
                    num6++;
                    num7++;
                }
                index = stringArray[i].IndexOf("(");
                num9 = stringArray[i].LastIndexOf(")");
                str = stringArray[i].Substring(index + 1, (num9 - index) - 1);
                num10 = this.conditionType(str);
                num11 = str.IndexOf('.');
                str = str.Substring(num11 + 1);
                num12 = this.operantType(str, num10);
                index = str.IndexOf('(');
                num9 = str.LastIndexOf(")");
                strArray2 = str.Substring(index + 1, (num9 - index) - 1).Split(new char[] { ',' });
                condition2 = new RCCondition(num12, num10, this.returnHelper(strArray2[0]), this.returnHelper(strArray2[1]));
                event3 = this.parseBlock(strArray, 3, 0, condition2);
                action = new RCAction(0, 0, event3, null);
                sentTrueActions.Add(action);
                i = num3;
            }
            else if (stringArray[i].StartsWith("ForeachTitan") && (stringArray[i + 1] == "{"))
            {
                num2 = i + 2;
                num3 = i + 2;
                num4 = 0;
                length = i + 2;
                while (length < stringArray.Length)
                {
                    if (stringArray[length] == "{")
                    {
                        num4++;
                    }
                    if (stringArray[length] == "}")
                    {
                        if (num4 > 0)
                        {
                            num4--;
                        }
                        else
                        {
                            num3 = length - 1;
                            length = stringArray.Length;
                        }
                    }
                    length++;
                }
                strArray = new string[(num3 - num2) + 1];
                num6 = 0;
                num7 = num2;
                while (num7 <= num3)
                {
                    strArray[num6] = stringArray[num7];
                    num6++;
                    num7++;
                }
                index = stringArray[i].IndexOf("(");
                num9 = stringArray[i].LastIndexOf(")");
                str = stringArray[i].Substring(index + 2, (num9 - index) - 3);
                num10 = 0;
                event3 = this.parseBlock(strArray, 2, 0, null);
                event3.foreachVariableName = str;
                action = new RCAction(0, 0, event3, null);
                sentTrueActions.Add(action);
                i = num3;
            }
            else if (stringArray[i].StartsWith("ForeachPlayer") && (stringArray[i + 1] == "{"))
            {
                num2 = i + 2;
                num3 = i + 2;
                num4 = 0;
                length = i + 2;
                while (length < stringArray.Length)
                {
                    if (stringArray[length] == "{")
                    {
                        num4++;
                    }
                    if (stringArray[length] == "}")
                    {
                        if (num4 > 0)
                        {
                            num4--;
                        }
                        else
                        {
                            num3 = length - 1;
                            length = stringArray.Length;
                        }
                    }
                    length++;
                }
                strArray = new string[(num3 - num2) + 1];
                num6 = 0;
                num7 = num2;
                while (num7 <= num3)
                {
                    strArray[num6] = stringArray[num7];
                    num6++;
                    num7++;
                }
                index = stringArray[i].IndexOf("(");
                num9 = stringArray[i].LastIndexOf(")");
                str = stringArray[i].Substring(index + 2, (num9 - index) - 3);
                num10 = 1;
                event3 = this.parseBlock(strArray, 2, 1, null);
                event3.foreachVariableName = str;
                action = new RCAction(0, 0, event3, null);
                sentTrueActions.Add(action);
                i = num3;
            }
            else if (stringArray[i].StartsWith("Else") && (stringArray[i + 1] == "{"))
            {
                num2 = i + 2;
                num3 = i + 2;
                num4 = 0;
                length = i + 2;
                while (length < stringArray.Length)
                {
                    if (stringArray[length] == "{")
                    {
                        num4++;
                    }
                    if (stringArray[length] == "}")
                    {
                        if (num4 > 0)
                        {
                            num4--;
                        }
                        else
                        {
                            num3 = length - 1;
                            length = stringArray.Length;
                        }
                    }
                    length++;
                }
                strArray = new string[(num3 - num2) + 1];
                num6 = 0;
                for (num7 = num2; num7 <= num3; num7++)
                {
                    strArray[num6] = stringArray[num7];
                    num6++;
                }
                if (stringArray[i] == "Else")
                {
                    event3 = this.parseBlock(strArray, 0, 0, null);
                    action = new RCAction(0, 0, event3, null);
                    event2.setElse(action);
                    i = num3;
                }
                else if (stringArray[i].StartsWith("Else If"))
                {
                    index = stringArray[i].IndexOf("(");
                    num9 = stringArray[i].LastIndexOf(")");
                    str = stringArray[i].Substring(index + 1, (num9 - index) - 1);
                    num10 = this.conditionType(str);
                    num11 = str.IndexOf('.');
                    str = str.Substring(num11 + 1);
                    num12 = this.operantType(str, num10);
                    index = str.IndexOf('(');
                    num9 = str.LastIndexOf(")");
                    strArray2 = str.Substring(index + 1, (num9 - index) - 1).Split(new char[] { ',' });
                    condition2 = new RCCondition(num12, num10, this.returnHelper(strArray2[0]), this.returnHelper(strArray2[1]));
                    event3 = this.parseBlock(strArray, 1, 0, condition2);
                    action = new RCAction(0, 0, event3, null);
                    event2.setElse(action);
                    i = num3;
                }
            }
            else
            {
                int num13;
                int num14;
                int num15;
                int num16;
                string str2;
                string[] strArray3;
                RCActionHelper helper;
                RCActionHelper helper2;
                RCActionHelper helper3;
                if (stringArray[i].StartsWith("VariableInt"))
                {
                    num13 = 1;
                    num14 = stringArray[i].IndexOf('.');
                    num15 = stringArray[i].IndexOf('(');
                    num16 = stringArray[i].LastIndexOf(')');
                    str2 = stringArray[i].Substring(num14 + 1, (num15 - num14) - 1);
                    strArray3 = stringArray[i].Substring(num15 + 1, (num16 - num15) - 1).Split(new char[] { ',' });
                    if (str2.StartsWith("SetRandom"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        helper3 = this.returnHelper(strArray3[2]);
                        action = new RCAction(num13, 12, null, new RCActionHelper[] { helper, helper2, helper3 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Set"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        action = new RCAction(num13, 0, null, new RCActionHelper[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Add"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        action = new RCAction(num13, 1, null, new RCActionHelper[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Subtract"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        action = new RCAction(num13, 2, null, new RCActionHelper[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Multiply"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        action = new RCAction(num13, 3, null, new RCActionHelper[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Divide"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        action = new RCAction(num13, 4, null, new RCActionHelper[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Modulo"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        action = new RCAction(num13, 5, null, new RCActionHelper[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Power"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        action = new RCAction(num13, 6, null, new RCActionHelper[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                }
                else if (stringArray[i].StartsWith("VariableBool"))
                {
                    num13 = 2;
                    num14 = stringArray[i].IndexOf('.');
                    num15 = stringArray[i].IndexOf('(');
                    num16 = stringArray[i].LastIndexOf(')');
                    str2 = stringArray[i].Substring(num14 + 1, (num15 - num14) - 1);
                    strArray3 = stringArray[i].Substring(num15 + 1, (num16 - num15) - 1).Split(new char[] { ',' });
                    if (str2.StartsWith("SetToOpposite"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        action = new RCAction(num13, 11, null, new RCActionHelper[] { helper });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("SetRandom"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        action = new RCAction(num13, 12, null, new RCActionHelper[] { helper });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Set"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        action = new RCAction(num13, 0, null, new RCActionHelper[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                }
                else if (stringArray[i].StartsWith("VariableString"))
                {
                    num13 = 3;
                    num14 = stringArray[i].IndexOf('.');
                    num15 = stringArray[i].IndexOf('(');
                    num16 = stringArray[i].LastIndexOf(')');
                    str2 = stringArray[i].Substring(num14 + 1, (num15 - num14) - 1);
                    strArray3 = stringArray[i].Substring(num15 + 1, (num16 - num15) - 1).Split(new char[] { ',' });
                    if (str2.StartsWith("Set"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        action = new RCAction(num13, 0, null, new RCActionHelper[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Concat"))
                    {
                        RCActionHelper[] helpers = new RCActionHelper[strArray3.Length];
                        for (length = 0; length < strArray3.Length; length++)
                        {
                            helpers[length] = this.returnHelper(strArray3[length]);
                        }
                        action = new RCAction(num13, 7, null, helpers);
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Append"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        action = new RCAction(num13, 8, null, new RCActionHelper[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Replace"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        helper3 = this.returnHelper(strArray3[2]);
                        action = new RCAction(num13, 10, null, new RCActionHelper[] { helper, helper2, helper3 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Remove"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        action = new RCAction(num13, 9, null, new RCActionHelper[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                }
                else if (stringArray[i].StartsWith("VariableFloat"))
                {
                    num13 = 4;
                    num14 = stringArray[i].IndexOf('.');
                    num15 = stringArray[i].IndexOf('(');
                    num16 = stringArray[i].LastIndexOf(')');
                    str2 = stringArray[i].Substring(num14 + 1, (num15 - num14) - 1);
                    strArray3 = stringArray[i].Substring(num15 + 1, (num16 - num15) - 1).Split(new char[] { ',' });
                    if (str2.StartsWith("SetRandom"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        helper3 = this.returnHelper(strArray3[2]);
                        action = new RCAction(num13, 12, null, new RCActionHelper[] { helper, helper2, helper3 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Set"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        action = new RCAction(num13, 0, null, new RCActionHelper[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Add"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        action = new RCAction(num13, 1, null, new RCActionHelper[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Subtract"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        action = new RCAction(num13, 2, null, new RCActionHelper[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Multiply"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        action = new RCAction(num13, 3, null, new RCActionHelper[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Divide"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        action = new RCAction(num13, 4, null, new RCActionHelper[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Modulo"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        action = new RCAction(num13, 5, null, new RCActionHelper[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Power"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        action = new RCAction(num13, 6, null, new RCActionHelper[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                }
                else if (stringArray[i].StartsWith("VariablePlayer"))
                {
                    num13 = 5;
                    num14 = stringArray[i].IndexOf('.');
                    num15 = stringArray[i].IndexOf('(');
                    num16 = stringArray[i].LastIndexOf(')');
                    str2 = stringArray[i].Substring(num14 + 1, (num15 - num14) - 1);
                    strArray3 = stringArray[i].Substring(num15 + 1, (num16 - num15) - 1).Split(new char[] { ',' });
                    if (str2.StartsWith("Set"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        action = new RCAction(num13, 0, null, new RCActionHelper[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                }
                else if (stringArray[i].StartsWith("VariableTitan"))
                {
                    num13 = 6;
                    num14 = stringArray[i].IndexOf('.');
                    num15 = stringArray[i].IndexOf('(');
                    num16 = stringArray[i].LastIndexOf(')');
                    str2 = stringArray[i].Substring(num14 + 1, (num15 - num14) - 1);
                    strArray3 = stringArray[i].Substring(num15 + 1, (num16 - num15) - 1).Split(new char[] { ',' });
                    if (str2.StartsWith("Set"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        action = new RCAction(num13, 0, null, new RCActionHelper[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                }
                else
                {
                    RCActionHelper helper4;
                    if (stringArray[i].StartsWith("Player"))
                    {
                        num13 = 7;
                        num14 = stringArray[i].IndexOf('.');
                        num15 = stringArray[i].IndexOf('(');
                        num16 = stringArray[i].LastIndexOf(')');
                        str2 = stringArray[i].Substring(num14 + 1, (num15 - num14) - 1);
                        strArray3 = stringArray[i].Substring(num15 + 1, (num16 - num15) - 1).Split(new char[] { ',' });
                        if (str2.StartsWith("KillPlayer"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            helper2 = this.returnHelper(strArray3[1]);
                            action = new RCAction(num13, 0, null, new RCActionHelper[] { helper, helper2 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SpawnPlayerAt"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            helper2 = this.returnHelper(strArray3[1]);
                            helper3 = this.returnHelper(strArray3[2]);
                            helper4 = this.returnHelper(strArray3[3]);
                            action = new RCAction(num13, 2, null, new RCActionHelper[] { helper, helper2, helper3, helper4 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SpawnPlayer"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            action = new RCAction(num13, 1, null, new RCActionHelper[] { helper });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("MovePlayer"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            helper2 = this.returnHelper(strArray3[1]);
                            helper3 = this.returnHelper(strArray3[2]);
                            helper4 = this.returnHelper(strArray3[3]);
                            action = new RCAction(num13, 3, null, new RCActionHelper[] { helper, helper2, helper3, helper4 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SetKills"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            helper2 = this.returnHelper(strArray3[1]);
                            action = new RCAction(num13, 4, null, new RCActionHelper[] { helper, helper2 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SetDeaths"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            helper2 = this.returnHelper(strArray3[1]);
                            action = new RCAction(num13, 5, null, new RCActionHelper[] { helper, helper2 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SetMaxDmg"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            helper2 = this.returnHelper(strArray3[1]);
                            action = new RCAction(num13, 6, null, new RCActionHelper[] { helper, helper2 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SetTotalDmg"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            helper2 = this.returnHelper(strArray3[1]);
                            action = new RCAction(num13, 7, null, new RCActionHelper[] { helper, helper2 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SetName"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            helper2 = this.returnHelper(strArray3[1]);
                            action = new RCAction(num13, 8, null, new RCActionHelper[] { helper, helper2 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SetGuildName"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            helper2 = this.returnHelper(strArray3[1]);
                            action = new RCAction(num13, 9, null, new RCActionHelper[] { helper, helper2 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SetTeam"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            helper2 = this.returnHelper(strArray3[1]);
                            action = new RCAction(num13, 10, null, new RCActionHelper[] { helper, helper2 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SetCustomInt"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            helper2 = this.returnHelper(strArray3[1]);
                            action = new RCAction(num13, 11, null, new RCActionHelper[] { helper, helper2 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SetCustomBool"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            helper2 = this.returnHelper(strArray3[1]);
                            action = new RCAction(num13, 12, null, new RCActionHelper[] { helper, helper2 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SetCustomString"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            helper2 = this.returnHelper(strArray3[1]);
                            action = new RCAction(num13, 13, null, new RCActionHelper[] { helper, helper2 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SetCustomFloat"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            helper2 = this.returnHelper(strArray3[1]);
                            action = new RCAction(num13, 14, null, new RCActionHelper[] { helper, helper2 });
                            sentTrueActions.Add(action);
                        }
                    }
                    else if (stringArray[i].StartsWith("Titan"))
                    {
                        num13 = 8;
                        num14 = stringArray[i].IndexOf('.');
                        num15 = stringArray[i].IndexOf('(');
                        num16 = stringArray[i].LastIndexOf(')');
                        str2 = stringArray[i].Substring(num14 + 1, (num15 - num14) - 1);
                        strArray3 = stringArray[i].Substring(num15 + 1, (num16 - num15) - 1).Split(new char[] { ',' });
                        if (str2.StartsWith("KillTitan"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            helper2 = this.returnHelper(strArray3[1]);
                            helper3 = this.returnHelper(strArray3[2]);
                            action = new RCAction(num13, 0, null, new RCActionHelper[] { helper, helper2, helper3 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SpawnTitanAt"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            helper2 = this.returnHelper(strArray3[1]);
                            helper3 = this.returnHelper(strArray3[2]);
                            helper4 = this.returnHelper(strArray3[3]);
                            RCActionHelper helper5 = this.returnHelper(strArray3[4]);
                            RCActionHelper helper6 = this.returnHelper(strArray3[5]);
                            RCActionHelper helper7 = this.returnHelper(strArray3[6]);
                            action = new RCAction(num13, 2, null, new RCActionHelper[] { helper, helper2, helper3, helper4, helper5, helper6, helper7 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SpawnTitan"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            helper2 = this.returnHelper(strArray3[1]);
                            helper3 = this.returnHelper(strArray3[2]);
                            helper4 = this.returnHelper(strArray3[3]);
                            action = new RCAction(num13, 1, null, new RCActionHelper[] { helper, helper2, helper3, helper4 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SetHealth"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            helper2 = this.returnHelper(strArray3[1]);
                            action = new RCAction(num13, 3, null, new RCActionHelper[] { helper, helper2 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("MoveTitan"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            helper2 = this.returnHelper(strArray3[1]);
                            helper3 = this.returnHelper(strArray3[2]);
                            helper4 = this.returnHelper(strArray3[3]);
                            action = new RCAction(num13, 4, null, new RCActionHelper[] { helper, helper2, helper3, helper4 });
                            sentTrueActions.Add(action);
                        }
                    }
                    else if (stringArray[i].StartsWith("Game"))
                    {
                        num13 = 9;
                        num14 = stringArray[i].IndexOf('.');
                        num15 = stringArray[i].IndexOf('(');
                        num16 = stringArray[i].LastIndexOf(')');
                        str2 = stringArray[i].Substring(num14 + 1, (num15 - num14) - 1);
                        strArray3 = stringArray[i].Substring(num15 + 1, (num16 - num15) - 1).Split(new char[] { ',' });
                        if (str2.StartsWith("PrintMessage"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            action = new RCAction(num13, 0, null, new RCActionHelper[] { helper });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("LoseGame"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            action = new RCAction(num13, 2, null, new RCActionHelper[] { helper });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("WinGame"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            action = new RCAction(num13, 1, null, new RCActionHelper[] { helper });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("Restart"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            action = new RCAction(num13, 3, null, new RCActionHelper[] { helper });
                            sentTrueActions.Add(action);
                        }
                    }
                }
            }
        }
        return new RCEvent(condition, sentTrueActions, eventClass, eventType);
    }
    [RPC]
    public void pauseRPC(bool pause, PhotonMessageInfo info)
    {
        if (info.sender.isMasterClient)
        {
            if (pause)
            {
                this.pauseWaitTime = 100000f;
                Time.timeScale = 1E-06f;
            }
            else
            {
                this.pauseWaitTime = 3f;
            }
        }
    }

    public void playerKillInfoSingleUpdate(int dmg)
    {
        this.single_kills++;
        this.single_maxDamage = Mathf.Max(dmg, this.single_maxDamage);
        this.single_totalDamage += dmg;
    }

    public void playerKillInfoUpdate(PhotonPlayer player, int dmg)
    {
        player.kills = (player.kills) + 1;
        player.max_dmg = Mathf.Max(dmg, player.max_dmg);
        player.total_dmg = player.total_dmg + dmg;
    }

    public TITAN randomSpawnOneTitan(string place, int rate)
    {
        GameObject[] objArray = GameObject.FindGameObjectsWithTag(place);
        int index = UnityEngine.Random.Range(0, objArray.Length);
        GameObject obj2 = objArray[index];
        while (objArray[index] == null)
        {
            index = UnityEngine.Random.Range(0, objArray.Length);
            obj2 = objArray[index];
        }
        objArray[index] = null;
        return this.spawnTitan(rate, obj2.transform.position, obj2.transform.rotation, false);
    }

    public void randomSpawnTitan(string place, int rate, int num, bool punk = false)
    {
        if (num == -1)
        {
            num = 1;
        }
        GameObject[] objArray = GameObject.FindGameObjectsWithTag(place);
        if (objArray.Length > 0)
        {
            for (int i = 0; i < num; i++)
            {
                int index = UnityEngine.Random.Range(0, objArray.Length);
                GameObject obj2 = objArray[index];
                while (objArray[index] == null)
                {
                    index = UnityEngine.Random.Range(0, objArray.Length);
                    obj2 = objArray[index];
                }
                objArray[index] = null;
                this.spawnTitan(rate, obj2.transform.position, obj2.transform.rotation, punk);
            }
        }
    }

    public string randomString(int length)
    {
        int num;
        List<string> list = new List<string>();
        System.Random random = new System.Random();
        string str = "0123456789ABCDEFqwertzuiopasdghjklyxcvbn";
        StringBuilder builder = new StringBuilder(length);
        for (int i = 0; i < length; i = num + 1)
        {
            builder.Append(str[random.Next(str.Length)]);
            num = i;
        }
        if (!list.Contains(builder.ToString()))
        {
            list.Add(builder.ToString());
        }
        return builder.ToString();
    }

    public Texture2D RCLoadTexture(string tex)
    {
        if (this.assetCacheTextures == null)
        {
            this.assetCacheTextures = new Dictionary<string, Texture2D>();
        }
        if (this.assetCacheTextures.ContainsKey(tex))
        {
            return this.assetCacheTextures[tex];
        }
        Texture2D textured = (Texture2D)RCassets.Load(tex);
        this.assetCacheTextures.Add(tex, textured);
        return textured;
    }

    public void RecompilePlayerList(float time)
    {
        if (!this.isRecompiling)
        {
            this.isRecompiling = true;
            base.StartCoroutine(this.WaitAndRecompilePlayerList(time));
        }
    }

    [RPC]
    private void refreshPVPStatus(int score1, int score2)
    {
        this.PVPhumanScore = score1;
        this.PVPtitanScore = score2;
    }

    [RPC]
    private void refreshPVPStatus_AHSS(int[] score1)
    {
        this.teamScores = score1;
    }

    private void refreshRacingResult()
    {
        this.localRacingResult = "Result\n";
        IComparer comparer = new IComparerRacingResult();
        this.racingResult.Sort(comparer);
        int num = Mathf.Min(this.racingResult.Count, 6);
        for (int i = 0; i < num; i++)
        {
            string localRacingResult = this.localRacingResult;
            object[] objArray = new object[] { localRacingResult, "Rank ", i + 1, " : " };
            this.localRacingResult = string.Concat(objArray);
            this.localRacingResult = this.localRacingResult + (this.racingResult[i] as RacingResult).name;
            this.localRacingResult = this.localRacingResult + "   " + ((((int)((this.racingResult[i] as RacingResult).time * 100f)) * 0.01f)).ToString() + "s";
            this.localRacingResult = this.localRacingResult + "\n";
        }
        object[] parameters = new object[] { this.localRacingResult };
        base.photonView.RPC("netRefreshRacingResult", PhotonTargets.All, parameters);
    }

    private void refreshRacingResult2()
    {
        this.localRacingResult = "Result\n";
        IComparer comparer = new IComparerRacingResult();
        this.racingResult.Sort(comparer);
        int num = Mathf.Min(this.racingResult.Count, 10);
        for (int i = 0; i < num; i++)
        {
            string localRacingResult = this.localRacingResult;
            object[] objArray = new object[] { localRacingResult, "Rank ", i + 1, " : " };
            this.localRacingResult = string.Concat(objArray);
            this.localRacingResult = this.localRacingResult + "[FFFFFF]" + (this.racingResult[i] as RacingResult).name;
            this.localRacingResult = this.localRacingResult + "   " + ((((int)((this.racingResult[i] as RacingResult).time * 100f)) * 0.01f)).ToString() + "s";
            this.localRacingResult = this.localRacingResult + "\n";
        }
        object[] parameters = new object[] { this.localRacingResult };
        base.photonView.RPC("netRefreshRacingResult", PhotonTargets.All, parameters);
    }

    [RPC]
    private void refreshStatus(int score1, int score2, int wav, int highestWav, float time1, float time2, bool startRacin, bool endRacin)
    {
        this.humanScore = score1;
        this.titanScore = score2;
        this.wave = wav;
        this.highestwave = highestWav;
        this.roundTime = time1;
        this.timeTotalServer = time2;
        this.startRacing = startRacin;
        this.endRacing = endRacin;
        if (this.startRacing && (findDoorRacing != null))
        {
            findDoorRacing.SetActive(false);
        }
    }
   
    public IEnumerator reloadSky()
    {
        yield return new WaitForSeconds(0.5f);
        if ((skyMaterial != null) && (Camera.main.GetComponent<Skybox>().material != skyMaterial))
        {
            Camera.main.GetComponent<Skybox>().material = skyMaterial;
        }
        Screen.lockCursor = !Screen.lockCursor;
        Screen.lockCursor = !Screen.lockCursor;
        yield break;
    }

    public void removeCT(COLOSSAL_TITAN titan)
    {
        this.cT.Remove(titan);
        alltitans.Remove(titan.gameObject);
    }

    public void removeET(TITAN_EREN hero)
    {
        this.eT.Remove(hero);
        allheroes.Remove(hero.gameObject);
    }

    public void removeFT(FEMALE_TITAN titan)
    {
        this.fT.Remove(titan);
        alltitans.Remove(titan.gameObject);
    }

    public void removeHero(HERO hero)
    {
        this.heroes.Remove(hero);
        allheroes.Remove(hero.gameObject);
    }

    public void removeHook(Bullet h)
    {
        this.hooks.Remove(h);
    }

    public void removeTitan(TITAN titan)
    {
        this.titans.Remove(titan);
        alltitans.Remove(titan.gameObject);
    }

    [RPC]
    private void RequireStatus()
    {
        object[] parameters = new object[] { this.humanScore, this.titanScore, this.wave, this.highestwave, this.roundTime, this.timeTotalServer, this.startRacing, this.endRacing };
        base.photonView.RPC("refreshStatus", PhotonTargets.Others, parameters);
        object[] objArray2 = new object[] { this.PVPhumanScore, this.PVPtitanScore };
        base.photonView.RPC("refreshPVPStatus", PhotonTargets.Others, objArray2);
        object[] objArray3 = new object[] { this.teamScores };
        base.photonView.RPC("refreshPVPStatus_AHSS", PhotonTargets.Others, objArray3);
    }

    private void resetGameSettings()
    {
        RCSettings.bombMode = 0;
        RCSettings.teamMode = 0;
        RCSettings.pointMode = 0;
        RCSettings.disableRock = 0;
        RCSettings.explodeMode = 0;
        RCSettings.healthMode = 0;
        RCSettings.healthLower = 0;
        RCSettings.healthUpper = 0;
        RCSettings.infectionMode = 0;
        RCSettings.banEren = 0;
        RCSettings.moreTitans = 0;
        RCSettings.damageMode = 0;
        RCSettings.sizeMode = 0;
        RCSettings.sizeLower = 0f;
        RCSettings.sizeUpper = 0f;
        RCSettings.spawnMode = 0;
        RCSettings.nRate = 0f;
        RCSettings.aRate = 0f;
        RCSettings.jRate = 0f;
        RCSettings.cRate = 0f;
        RCSettings.pRate = 0f;
        RCSettings.horseMode = 0;
        RCSettings.waveModeOn = 0;
        RCSettings.waveModeNum = 0;
        RCSettings.friendlyMode = 0;
        RCSettings.pvpMode = 0;
        RCSettings.maxWave = 0;
        RCSettings.endlessMode = 0;
        RCSettings.ahssReload = 0;
        RCSettings.punkWaves = 0;
        RCSettings.globalDisableMinimap = 0;
        RCSettings.motd = string.Empty;
        RCSettings.deadlyCannons = 0;
        RCSettings.asoPreservekdr = 0;
        RCSettings.racingStatic = 0;
    }

    private void resetSettings(bool isLeave)
    {
        masterRC = false;
        SpeeFocusPlayer.isShow = false;
        PhotonNetwork.player.RCteam = 0;
        if (isLeave)
        {
            currentLevel = string.Empty;
            PhotonNetwork.player.currentLevel = string.Empty;
            this.levelCache = new List<string[]>();
            this.titanSpawns.Clear();
            this.playerSpawnsC.Clear();
            this.playerSpawnsM.Clear();
            this.titanSpawners.Clear();
            intVariables.Clear();
            boolVariables.Clear();
            stringVariables.Clear();
            floatVariables.Clear();
            RCRegions.Clear();
            RCEvents.Clear();
            RCVariableNames.Clear();
            playerVariables.Clear();
            titanVariables.Clear();
            RCRegionTriggers.Clear();
            currentScriptLogic = string.Empty;
            PhotonNetwork.player.statACL = 100;
            PhotonNetwork.player.statBLA = 100;
            PhotonNetwork.player.statGAS = 100;
            PhotonNetwork.player.statSPD = 100;
            this.restartingTitan = false;
            this.restartingMC = false;
            this.restartingHorse = false;
            this.restartingEren = false;
            this.restartingBomb = false;
        }

        this.resetGameSettings();
        banHash = new ExitGames.Client.Photon.Hashtable();
        imatitan = new ExitGames.Client.Photon.Hashtable();
        oldScript = string.Empty;
        ignoreList = new List<int>();
        this.restartCount = new List<float>();
        heroHash = new ExitGames.Client.Photon.Hashtable();
    }

    private IEnumerator respawnE(float seconds)
    {
        while (true)
        {
            yield return new WaitForSeconds(seconds);
            if (!this.isLosing && !this.isWinning)
            {
                for (int j = 0; j < PhotonNetwork.playerList.Length; j++)
                {
                    PhotonPlayer targetPlayer = PhotonNetwork.playerList[j];
                    if (targetPlayer.dead && (targetPlayer.isTitan != 2))
                    {
                        this.photonView.RPC("respawnHeroInNewRound", targetPlayer, new object[0]);
                    }
                }
            }
        }
    }

    [RPC]
    private void respawnHeroInNewRound()
    {
        if (!this.needChooseSide && IN_GAME_MAIN_CAMERA.instance.gameOver)
        {
            this.SpawnPlayer(this.myLastHero, this.myLastRespawnTag);
            IN_GAME_MAIN_CAMERA.instance.gameOver = false;
            this.ShowHUDInfoCenter(string.Empty);
        }
    }

    public IEnumerator restartE(float time)
    {
        yield return new WaitForSeconds(time);
        this.restartGame2(false);
    }

    public void restartGame(bool masterclientSwitched = false)
    {

    }

    void ChageSetingInfo(PhotonPlayer player = null)
    {
        string to_player_info = string.Empty;
        if (LavaMode)
        {
            to_player_info = to_player_info + INC.la("lava_mode_active") + "\n";
        }
        if (Famel_Titan_mode_survive)
        {
            to_player_info = to_player_info + INC.la("anni_mode_allow") + "\n";
        }
        string mess = ((string)FengGameManagerMKII.settings[337]).Trim();
        if (mess != string.Empty)
        {
            to_player_info = to_player_info + INC.la("motd_mess") + mess + "\n";
        }
        if ((int)FengGameManagerMKII.settings[297] == 1)
        {
            to_player_info = to_player_info + INC.la("anti_guest_active") + "\n";
        }
        if ((int)FengGameManagerMKII.settings[335] == 1)
        {
            to_player_info = to_player_info + INC.la("anti_player_titan_active") + "\n";
        }
        if ((int)FengGameManagerMKII.settings[299] == 1)
        {
            to_player_info = to_player_info + INC.la("anti_noname_pl_active") + "\n";
        }
        if ((int)FengGameManagerMKII.settings[331] == 1 && (fT.Count > 0) && !Famel_Titan_mode_survive)
        {
            to_player_info = to_player_info + INC.la("annie_15tit_spawn_dis") + "\n";
        }
        int num343 = 0;
        if (int.TryParse((string)FengGameManagerMKII.settings[332], out num343))
        {
            if (num343 > 0 && (fT.Count > 0 || Famel_Titan_mode_survive))
            {
                to_player_info = to_player_info + INC.la("annie_min_damage") + num343.ToString() + "\n";
            }
        }
        if ((int)FengGameManagerMKII.settings[333] == 1 && (fT.Count > 0 || Famel_Titan_mode_survive))
        {
            to_player_info = to_player_info + INC.la("annie_show_l_damage") + "\n";
        }

        string str = to_player_info.Trim();
        if (str != string.Empty)
        {
            if (player != null)
            {
                cext.mess(str, player);
            }
            else if (current_set != str)
            {
                cext.mess(str);
                current_set = str;
            }

        }
    }

    public void restartGame2(bool masterclientSwitched = false)
    {
        if (!this.gameTimesUp)
        {
            if (PanelSetHeroCustom.instance != null)
            {
                PanelSetHeroCustom.instance.enabled = false;
            }
            this.PVPtitanScore = 0;
            this.PVPhumanScore = 0;
            this.startRacing = false;
            this.endRacing = false;
            this.checkpoint = null;
            this.timeElapse = 0f;
            this.roundTime = 0f;
            isUpdateFT = false;
            this.isWinning = false;
            this.isLosing = false;
            this.isPlayer1Winning = false;
            this.isPlayer2Winning = false;
            this.wave = 1;
            this.myRespawnTime = 0f;
            this.kicklist = new ArrayList();
            playergrab_skin = null;
            number_grabed_skin = 0;
            this.killInfoGO = new ArrayList();
            this.racingResult = new ArrayList();
            killinfo = new List<KillInfoComponent>();
            this.ShowHUDInfoCenter(string.Empty);
            this.isRestarting = true;
            this.DestroyAllExistingCloths();
            PhotonNetwork.DestroyAll();
            ExitGames.Client.Photon.Hashtable hash = this.checkGameGUI();
            base.photonView.RPC("settingRPC", PhotonTargets.Others, new object[] { hash });
            base.photonView.RPC("RPCLoadLevel", PhotonTargets.All, new object[0]);
            this.setGameSettings(hash);
            ChageSetingInfo();
            if (masterclientSwitched)
            {
                cext.mess(INC.la("set_ms") + PhotonNetwork.player.ishexname);
            }
        }
    }

    [RPC]
    private void restartGameByClient()
    {
        if (PhotonNetwork.isMasterClient)
        {
            if (heroes.Count == 0)
            {
                restartGame2(false);
            }
        }
    }

    public void restartGameSingle()
    {
        this.startRacing = false;
        this.endRacing = false;
        this.checkpoint = null;
        this.single_kills = 0;
        this.single_maxDamage = 0;
        this.single_totalDamage = 0;
        this.timeElapse = 0f;
        this.roundTime = 0f;
        this.timeTotalServer = 0f;
        this.isWinning = false;
        this.isLosing = false;
        this.isPlayer1Winning = false;
        this.isPlayer2Winning = false;
        this.wave = 1;
        this.myRespawnTime = 0f;
        this.ShowHUDInfoCenter(string.Empty);
        Application.LoadLevel(Application.loadedLevel);
    }

    public void restartGameSingle2()
    {
        this.startRacing = false;
        this.endRacing = false;
        this.checkpoint = null;
        this.single_kills = 0;
        this.single_maxDamage = 0;
        this.single_totalDamage = 0;
        this.timeElapse = 0f;
        this.roundTime = 0f;
        this.timeTotalServer = 0f;
        this.isWinning = false;
        this.isLosing = false;
        this.isPlayer1Winning = false;
        this.isPlayer2Winning = false;
        this.wave = 1;
        this.myRespawnTime = 0f;
        this.ShowHUDInfoCenter(string.Empty);
        this.DestroyAllExistingCloths();
        Application.LoadLevel(Application.loadedLevel);
    }

    public void restartRC()
    {
        intVariables.Clear();
        boolVariables.Clear();
        stringVariables.Clear();
        floatVariables.Clear();
        playerVariables.Clear();
        titanVariables.Clear();
        if (RCSettings.infectionMode > 0)
        {
            this.endGameInfectionRC();
        }
        else
        {
            this.endGameRC();
        }
        this.restartGame2(false);
    }

    public RCActionHelper returnHelper(string str)
    {
        float num;
        int num2;
        string[] strArray = str.Split(new char[] { '.' });
        if (float.TryParse(str, out num))
        {
            strArray = new string[] { str };
        }
        List<RCActionHelper> list = new List<RCActionHelper>();
        int sentType = 0;
        for (num2 = 0; num2 < strArray.Length; num2++)
        {
            string str2;
            RCActionHelper helper;
            if (list.Count == 0)
            {
                str2 = strArray[num2];
                if (str2.StartsWith("\"") && str2.EndsWith("\""))
                {
                    helper = new RCActionHelper(0, 0, str2.Substring(1, str2.Length - 2));
                    list.Add(helper);
                    sentType = 2;
                }
                else
                {
                    int num4;
                    if (int.TryParse(str2, out num4))
                    {
                        helper = new RCActionHelper(0, 0, num4);
                        list.Add(helper);
                        sentType = 0;
                    }
                    else
                    {
                        float num5;
                        if (float.TryParse(str2, out num5))
                        {
                            helper = new RCActionHelper(0, 0, num5);
                            list.Add(helper);
                            sentType = 3;
                        }
                        else if ((str2.ToLower() == "true") || (str2.ToLower() == "false"))
                        {
                            helper = new RCActionHelper(0, 0, Convert.ToBoolean(str2.ToLower()));
                            list.Add(helper);
                            sentType = 1;
                        }
                        else
                        {
                            int index;
                            int num7;
                            if (str2.StartsWith("Variable"))
                            {
                                index = str2.IndexOf('(');
                                num7 = str2.LastIndexOf(')');
                                if (str2.StartsWith("VariableInt"))
                                {
                                    str2 = str2.Substring(index + 1, (num7 - index) - 1);
                                    helper = new RCActionHelper(1, 0, this.returnHelper(str2));
                                    list.Add(helper);
                                    sentType = 0;
                                }
                                else if (str2.StartsWith("VariableBool"))
                                {
                                    str2 = str2.Substring(index + 1, (num7 - index) - 1);
                                    helper = new RCActionHelper(1, 1, this.returnHelper(str2));
                                    list.Add(helper);
                                    sentType = 1;
                                }
                                else if (str2.StartsWith("VariableString"))
                                {
                                    str2 = str2.Substring(index + 1, (num7 - index) - 1);
                                    helper = new RCActionHelper(1, 2, this.returnHelper(str2));
                                    list.Add(helper);
                                    sentType = 2;
                                }
                                else if (str2.StartsWith("VariableFloat"))
                                {
                                    str2 = str2.Substring(index + 1, (num7 - index) - 1);
                                    helper = new RCActionHelper(1, 3, this.returnHelper(str2));
                                    list.Add(helper);
                                    sentType = 3;
                                }
                                else if (str2.StartsWith("VariablePlayer"))
                                {
                                    str2 = str2.Substring(index + 1, (num7 - index) - 1);
                                    helper = new RCActionHelper(1, 4, this.returnHelper(str2));
                                    list.Add(helper);
                                    sentType = 4;
                                }
                                else if (str2.StartsWith("VariableTitan"))
                                {
                                    str2 = str2.Substring(index + 1, (num7 - index) - 1);
                                    helper = new RCActionHelper(1, 5, this.returnHelper(str2));
                                    list.Add(helper);
                                    sentType = 5;
                                }
                            }
                            else if (str2.StartsWith("Region"))
                            {
                                index = str2.IndexOf('(');
                                num7 = str2.LastIndexOf(')');
                                if (str2.StartsWith("RegionRandomX"))
                                {
                                    str2 = str2.Substring(index + 1, (num7 - index) - 1);
                                    helper = new RCActionHelper(4, 0, this.returnHelper(str2));
                                    list.Add(helper);
                                    sentType = 3;
                                }
                                else if (str2.StartsWith("RegionRandomY"))
                                {
                                    str2 = str2.Substring(index + 1, (num7 - index) - 1);
                                    helper = new RCActionHelper(4, 1, this.returnHelper(str2));
                                    list.Add(helper);
                                    sentType = 3;
                                }
                                else if (str2.StartsWith("RegionRandomZ"))
                                {
                                    str2 = str2.Substring(index + 1, (num7 - index) - 1);
                                    helper = new RCActionHelper(4, 2, this.returnHelper(str2));
                                    list.Add(helper);
                                    sentType = 3;
                                }
                            }
                        }
                    }
                }
            }
            else if (list.Count > 0)
            {
                str2 = strArray[num2];
                if (list[list.Count - 1].helperClass != 1)
                {
                    if (str2.StartsWith("ConvertToInt()"))
                    {
                        helper = new RCActionHelper(5, sentType, null);
                        list.Add(helper);
                        sentType = 0;
                    }
                    else if (str2.StartsWith("ConvertToBool()"))
                    {
                        helper = new RCActionHelper(5, sentType, null);
                        list.Add(helper);
                        sentType = 1;
                    }
                    else if (str2.StartsWith("ConvertToString()"))
                    {
                        helper = new RCActionHelper(5, sentType, null);
                        list.Add(helper);
                        sentType = 2;
                    }
                    else if (str2.StartsWith("ConvertToFloat()"))
                    {
                        helper = new RCActionHelper(5, sentType, null);
                        list.Add(helper);
                        sentType = 3;
                    }
                }
                else
                {
                    switch (list[list.Count - 1].helperType)
                    {
                        case 4:
                            {
                                if (!str2.StartsWith("GetTeam()"))
                                {
                                    goto Label_05E2;
                                }
                                helper = new RCActionHelper(2, 1, null);
                                list.Add(helper);
                                sentType = 0;
                                continue;
                            }
                        case 5:
                            {
                                if (!str2.StartsWith("GetType()"))
                                {
                                    goto Label_0896;
                                }
                                helper = new RCActionHelper(3, 0, null);
                                list.Add(helper);
                                sentType = 0;
                                continue;
                            }
                    }
                    if (str2.StartsWith("ConvertToInt()"))
                    {
                        helper = new RCActionHelper(5, sentType, null);
                        list.Add(helper);
                        sentType = 0;
                    }
                    else if (str2.StartsWith("ConvertToBool()"))
                    {
                        helper = new RCActionHelper(5, sentType, null);
                        list.Add(helper);
                        sentType = 1;
                    }
                    else if (str2.StartsWith("ConvertToString()"))
                    {
                        helper = new RCActionHelper(5, sentType, null);
                        list.Add(helper);
                        sentType = 2;
                    }
                    else if (str2.StartsWith("ConvertToFloat()"))
                    {
                        helper = new RCActionHelper(5, sentType, null);
                        list.Add(helper);
                        sentType = 3;
                    }
                }
            }
            continue;
        Label_05E2:
            if (str2.StartsWith("GetType()"))
            {
                helper = new RCActionHelper(2, 0, null);
                list.Add(helper);
                sentType = 0;
            }
            else if (str2.StartsWith("GetIsAlive()"))
            {
                helper = new RCActionHelper(2, 2, null);
                list.Add(helper);
                sentType = 1;
            }
            else if (str2.StartsWith("GetTitan()"))
            {
                helper = new RCActionHelper(2, 3, null);
                list.Add(helper);
                sentType = 0;
            }
            else if (str2.StartsWith("GetKills()"))
            {
                helper = new RCActionHelper(2, 4, null);
                list.Add(helper);
                sentType = 0;
            }
            else if (str2.StartsWith("GetDeaths()"))
            {
                helper = new RCActionHelper(2, 5, null);
                list.Add(helper);
                sentType = 0;
            }
            else if (str2.StartsWith("GetMaxDmg()"))
            {
                helper = new RCActionHelper(2, 6, null);
                list.Add(helper);
                sentType = 0;
            }
            else if (str2.StartsWith("GetTotalDmg()"))
            {
                helper = new RCActionHelper(2, 7, null);
                list.Add(helper);
                sentType = 0;
            }
            else if (str2.StartsWith("GetCustomInt()"))
            {
                helper = new RCActionHelper(2, 8, null);
                list.Add(helper);
                sentType = 0;
            }
            else if (str2.StartsWith("GetCustomBool()"))
            {
                helper = new RCActionHelper(2, 9, null);
                list.Add(helper);
                sentType = 1;
            }
            else if (str2.StartsWith("GetCustomString()"))
            {
                helper = new RCActionHelper(2, 10, null);
                list.Add(helper);
                sentType = 2;
            }
            else if (str2.StartsWith("GetCustomFloat()"))
            {
                helper = new RCActionHelper(2, 11, null);
                list.Add(helper);
                sentType = 3;
            }
            else if (str2.StartsWith("GetPositionX()"))
            {
                helper = new RCActionHelper(2, 14, null);
                list.Add(helper);
                sentType = 3;
            }
            else if (str2.StartsWith("GetPositionY()"))
            {
                helper = new RCActionHelper(2, 15, null);
                list.Add(helper);
                sentType = 3;
            }
            else if (str2.StartsWith("GetPositionZ()"))
            {
                helper = new RCActionHelper(2, 0x10, null);
                list.Add(helper);
                sentType = 3;
            }
            else if (str2.StartsWith("GetName()"))
            {
                helper = new RCActionHelper(2, 12, null);
                list.Add(helper);
                sentType = 2;
            }
            else if (str2.StartsWith("GetGuildName()"))
            {
                helper = new RCActionHelper(2, 13, null);
                list.Add(helper);
                sentType = 2;
            }
            else if (str2.StartsWith("GetSpeed()"))
            {
                helper = new RCActionHelper(2, 0x11, null);
                list.Add(helper);
                sentType = 3;
            }
            continue;
        Label_0896:
            if (str2.StartsWith("GetSize()"))
            {
                helper = new RCActionHelper(3, 1, null);
                list.Add(helper);
                sentType = 3;
            }
            else if (str2.StartsWith("GetHealth()"))
            {
                helper = new RCActionHelper(3, 2, null);
                list.Add(helper);
                sentType = 0;
            }
            else if (str2.StartsWith("GetPositionX()"))
            {
                helper = new RCActionHelper(3, 3, null);
                list.Add(helper);
                sentType = 3;
            }
            else if (str2.StartsWith("GetPositionY()"))
            {
                helper = new RCActionHelper(3, 4, null);
                list.Add(helper);
                sentType = 3;
            }
            else if (str2.StartsWith("GetPositionZ()"))
            {
                helper = new RCActionHelper(3, 5, null);
                list.Add(helper);
                sentType = 3;
            }
        }
        for (num2 = list.Count - 1; num2 > 0; num2--)
        {
            list[num2 - 1].setNextHelper(list[num2]);
        }
        return list[0];
    }

    public static PeerStates returnPeerState(int peerstate)
    {
        switch (peerstate)
        {
            case 0:
                return PeerStates.Authenticated;

            case 1:
                return PeerStates.ConnectedToMaster;

            case 2:
                return PeerStates.DisconnectingFromMasterserver;

            case 3:
                return PeerStates.DisconnectingFromGameserver;

            case 4:
                return PeerStates.DisconnectingFromNameServer;
        }
        return PeerStates.ConnectingToMasterserver;
    }

    [RPC]
    private void RPCLoadLevel(PhotonMessageInfo info)
    {
        if (info.sender.isMasterClient)
        {
            this.DestroyAllExistingCloths();
            PhotonNetwork.LoadLevel(lvlInfo.mapName);
        }
        else if (PhotonNetwork.isMasterClient)
        {
            this.kickPlayerRC(info.sender, true, "false restart.");
        }
        else if (!masterRC)
        {
            this.restartCount.Add(Time.time);
            foreach (float num in this.restartCount)
            {
                if ((Time.time - num) > 60f)
                {
                    this.restartCount.Remove(num);
                }
            }
            if (this.restartCount.Count < 6)
            {
                this.DestroyAllExistingCloths();
                PhotonNetwork.LoadLevel(lvlInfo.mapName);
            }
        }
    }

    public void sendChatContentInfo(string content)
    {
        object[] parameters = new object[] { content, string.Empty };
        base.photonView.RPC("Chat", PhotonTargets.All, parameters);
    }

    public void sendKillInfo(bool t1, string killer, bool t2, string victim, int dmg = 0)
    {
        object[] parameters = new object[] { t1, killer, t2, victim, dmg };
        base.photonView.RPC("updateKillInfo", PhotonTargets.All, parameters);
    }

    public static void ServerCloseConnection(PhotonPlayer targetPlayer, bool requestIpBan, string inGameName = null)
    {
        RaiseEventOptions options = new RaiseEventOptions
        {
            TargetActors = new int[] { targetPlayer.ID }
        };
        if (requestIpBan)
        {
            ExitGames.Client.Photon.Hashtable eventContent = new ExitGames.Client.Photon.Hashtable();
            eventContent[(byte)0] = true;

            if ((inGameName != null) && (inGameName.Length > 0))
            {
                eventContent[(byte)1] = inGameName;
            }
            PhotonNetwork.RaiseEvent(0xcb, eventContent, true, options);
        }
        else
        {
            PhotonNetwork.RaiseEvent(0xcb, null, true, options);
        }
    }

    public static void ServerRequestAuthentication(string authPassword)
    {
        if (!string.IsNullOrEmpty(authPassword))
        {
            ExitGames.Client.Photon.Hashtable eventContent = new ExitGames.Client.Photon.Hashtable();
            eventContent[(byte)0] = authPassword;

            PhotonNetwork.RaiseEvent(0xc6, eventContent, true, new RaiseEventOptions());
        }
    }

    public static void ServerRequestUnban(string bannedAddress)
    {
        if (!string.IsNullOrEmpty(bannedAddress))
        {
            ExitGames.Client.Photon.Hashtable eventContent = new ExitGames.Client.Photon.Hashtable();
            eventContent[(byte)0] = bannedAddress;

            PhotonNetwork.RaiseEvent(0xc7, eventContent, true, new RaiseEventOptions());
        }
    }

    public static Texture2D background_image;
    public void setBackground()
    {
        if (isAssetLoaded)
        {
            if (background_image == null)
            {
                FileInfo info = new FileInfo(Application.dataPath + "/Style/Background_main.jpg");
                if (info.Exists)
                {
                    background_image = new Texture2D(1, 1, TextureFormat.ARGB32, false);
                    background_image.LoadImage(info.ReadAllBytes());
                    background_image.Apply();
                    UnityEngine.Debug.Log("Image loaded " + Application.dataPath + "/Style/Background_main.jpg");
                }
                else
                {
                    UnityEngine.Debug.LogError("File exist " + Application.dataPath + "/Style/Background_main.jpg");
                    return;
                }
            }

            UnityEngine.Object.Instantiate(RCassets.Load("backgroundCamera"));
            foreach (Image ren in GameObject.Find("Image").GetComponents<Image>())
            {
                ren.sprite = UnityEngine.Sprite.Create((Texture2D)FengGameManagerMKII.background_image, new Rect(0, 0, FengGameManagerMKII.background_image.width, FengGameManagerMKII.background_image.height), Vector2.zero);
            }
        }

    }

    private void setGameSettings(ExitGames.Client.Photon.Hashtable hash)
    {
        this.restartingEren = false;
        this.restartingBomb = false;
        this.restartingHorse = false;
        this.restartingTitan = false;
        if (hash.ContainsKey("annie_mode"))
        {
            if (RCSettings.AnnieSurvive != (int)hash["annie_mode"])
            {
                RCSettings.AnnieSurvive = (int)hash["annie_mode"];
                IN_GAME_MAIN_CAMERA.gamemode = GAMEMODE.SURVIVE_MODE;
            }
        }
        else if (RCSettings.AnnieSurvive != 0)
        {
            RCSettings.AnnieSurvive = 0;
            IN_GAME_MAIN_CAMERA.gamemode = lvlInfo.type;
        }
        if (hash.ContainsKey("bomb"))
        {
            if (RCSettings.bombMode != (int)hash["bomb"])
            {
                RCSettings.bombMode = (int)hash["bomb"];
                InRoomChat.instance.addLINE("PVP Bomb Mode enabled.");
            }
        }
        else if (RCSettings.bombMode != 0)
        {
            RCSettings.bombMode = 0;
            InRoomChat.instance.addLINE("PVP Bomb Mode disabled.");
            if (PhotonNetwork.isMasterClient)
            {
                this.restartingBomb = true;
            }
        }
        if (hash.ContainsKey("globalDisableMinimap"))
        {
            if (RCSettings.globalDisableMinimap != (int)hash["globalDisableMinimap"])
            {
                RCSettings.globalDisableMinimap = (int)hash["globalDisableMinimap"];
                InRoomChat.instance.addLINE("Minimaps are not allowed.");
            }
        }
        else if (RCSettings.globalDisableMinimap != 0)
        {
            RCSettings.globalDisableMinimap = 0;
            InRoomChat.instance.addLINE("Minimaps are allowed.");
        }
        if (hash.ContainsKey("horse"))
        {
            if (RCSettings.horseMode != (int)hash["horse"])
            {
                RCSettings.horseMode = (int)hash["horse"];
                InRoomChat.instance.addLINE("Horses enabled.");
            }
        }
        else if (RCSettings.horseMode != 0)
        {
            RCSettings.horseMode = 0;
            InRoomChat.instance.addLINE("Horses disabled.");
            if (PhotonNetwork.isMasterClient)
            {
                this.restartingHorse = true;
            }
        }
        if (hash.ContainsKey("punkWaves"))
        {
            if (RCSettings.punkWaves != (int)hash["punkWaves"])
            {
                RCSettings.punkWaves = (int)hash["punkWaves"];
                InRoomChat.instance.addLINE("Punk override every 5 waves enabled.");
            }
        }
        else if (RCSettings.punkWaves != 0)
        {
            RCSettings.punkWaves = 0;
            InRoomChat.instance.addLINE("Punk override every 5 waves disabled.");
        }
        if (hash.ContainsKey("ahssReload"))
        {
            if (RCSettings.ahssReload != (int)hash["ahssReload"])
            {
                RCSettings.ahssReload = (int)hash["ahssReload"];
                InRoomChat.instance.addLINE("AHSS Air-Reload disabled.");
            }
        }
        else if (RCSettings.ahssReload != 0)
        {
            RCSettings.ahssReload = 0;
            InRoomChat.instance.addLINE("AHSS Air-Reload allowed.");
        }
        if (hash.ContainsKey("team"))
        {
            if (RCSettings.teamMode != (int)hash["team"])
            {
                RCSettings.teamMode = (int)hash["team"];
                string text = string.Empty;
                if (RCSettings.teamMode == 1)
                {
                    text = "no sort";
                }
                else if (RCSettings.teamMode == 2)
                {
                    text = "locked by size";
                }
                else if (RCSettings.teamMode == 3)
                {
                    text = "locked by skill";
                }
                InRoomChat.instance.addLINE("Team Mode enabled (" + text + ").");
                if ((PhotonNetwork.player.RCteam) == 0)
                {
                    this.setTeam(3);
                }
            }
        }
        else if (RCSettings.teamMode != 0)
        {
            RCSettings.teamMode = 0;
            this.setTeam(0);
            InRoomChat.instance.addLINE("Team mode disabled.");
        }
        if (hash.ContainsKey("point"))
        {
            if (RCSettings.pointMode != (int)hash["point"])
            {
                RCSettings.pointMode = (int)hash["point"];
                InRoomChat.instance.addLINE("Point limit enabled (" + Convert.ToString(RCSettings.pointMode) + ").");
            }
        }
        else if (RCSettings.pointMode != 0)
        {
            RCSettings.pointMode = 0;
            InRoomChat.instance.addLINE("Point limit disabled.");
        }
        if (hash.ContainsKey("rock"))
        {
            if (RCSettings.disableRock != (int)hash["rock"])
            {
                RCSettings.disableRock = (int)hash["rock"];
                InRoomChat.instance.addLINE("Punk rock throwing disabled.");
            }
        }
        else if (RCSettings.disableRock != 0)
        {
            RCSettings.disableRock = 0;
            InRoomChat.instance.addLINE("Punk rock throwing enabled.");
        }
        if (hash.ContainsKey("explode"))
        {
            if (RCSettings.explodeMode != (int)hash["explode"])
            {
                RCSettings.explodeMode = (int)hash["explode"];
                InRoomChat.instance.addLINE("Titan Explode Mode enabled (Radius " + Convert.ToString(RCSettings.explodeMode) + ").");
            }
        }
        else if (RCSettings.explodeMode != 0)
        {
            RCSettings.explodeMode = 0;
            InRoomChat.instance.addLINE("Titan Explode Mode disabled.");
        }
        if (hash.ContainsKey("healthMode") && hash.ContainsKey("healthLower") && hash.ContainsKey("healthUpper"))
        {
            if (RCSettings.healthMode != (int)hash["healthMode"] || RCSettings.healthLower != (int)hash["healthLower"] || RCSettings.healthUpper != (int)hash["healthUpper"])
            {
                RCSettings.healthMode = (int)hash["healthMode"];
                RCSettings.healthLower = (int)hash["healthLower"];
                RCSettings.healthUpper = (int)hash["healthUpper"];
                string text = "Static";
                if (RCSettings.healthMode == 2)
                {
                    text = "Scaled";
                }
                InRoomChat.instance.addLINE(string.Concat(new string[]
			{
				"Titan Health (",
				text,
				", ",
				RCSettings.healthLower.ToString(),
				" to ",
				RCSettings.healthUpper.ToString(),
				") enabled."
			}));
            }
        }
        else if (RCSettings.healthMode != 0 || RCSettings.healthLower != 0 || RCSettings.healthUpper != 0)
        {
            RCSettings.healthMode = 0;
            RCSettings.healthLower = 0;
            RCSettings.healthUpper = 0;
            InRoomChat.instance.addLINE("Titan Health disabled.");
        }
        if (hash.ContainsKey("infection"))
        {
            if (RCSettings.infectionMode != (int)hash["infection"])
            {
                RCSettings.infectionMode = (int)hash["infection"];
                this.name = LoginFengKAI.player.name;
                PhotonNetwork.player.RCteam = 0;
                InRoomChat.instance.addLINE("Infection mode (" + Convert.ToString(RCSettings.infectionMode) + ") enabled. Make sure your first character is human.");
            }
        }
        else if (RCSettings.infectionMode != 0)
        {
            RCSettings.infectionMode = 0;
            PhotonNetwork.player.isTitan = 1;
            InRoomChat.instance.addLINE("Infection Mode disabled.");
            if (PhotonNetwork.isMasterClient)
            {
                this.restartingTitan = true;
            }
        }
        if (hash.ContainsKey("eren"))
        {
            if (RCSettings.banEren != (int)hash["eren"])
            {
                RCSettings.banEren = (int)hash["eren"];
                InRoomChat.instance.addLINE("Anti-Eren enabled. Using eren transform will get you kicked.");
                if (PhotonNetwork.isMasterClient)
                {
                    this.restartingEren = true;
                }
            }
        }
        else if (RCSettings.banEren != 0)
        {
            RCSettings.banEren = 0;
            InRoomChat.instance.addLINE("Anti-Eren disabled. Eren transform is allowed.");
        }
        if (hash.ContainsKey("titanc"))
        {
            if (RCSettings.moreTitans != (int)hash["titanc"])
            {
                RCSettings.moreTitans = (int)hash["titanc"];
                InRoomChat.instance.addLINE("" + Convert.ToString(RCSettings.moreTitans) + " titans will spawn each round.");
            }
        }
        else if (RCSettings.moreTitans != 0)
        {
            RCSettings.moreTitans = 0;
            InRoomChat.instance.addLINE("Default titans will spawn each round.");
        }
        if (hash.ContainsKey("damage"))
        {
            if (RCSettings.damageMode != (int)hash["damage"])
            {
                RCSettings.damageMode = (int)hash["damage"];
                InRoomChat.instance.addLINE("Nape minimum damage (" + Convert.ToString(RCSettings.damageMode) + ") enabled.");
            }
        }
        else if (RCSettings.damageMode != 0)
        {
            RCSettings.damageMode = 0;
            InRoomChat.instance.addLINE("Nape minimum damage disabled.");
        }
        if (hash.ContainsKey("sizeMode") && hash.ContainsKey("sizeLower") && hash.ContainsKey("sizeUpper"))
        {
            if (RCSettings.sizeMode != (int)hash["sizeMode"] || RCSettings.sizeLower != (float)hash["sizeLower"] || RCSettings.sizeUpper != (float)hash["sizeUpper"])
            {
                RCSettings.sizeMode = (int)hash["sizeMode"];
                RCSettings.sizeLower = (float)hash["sizeLower"];
                RCSettings.sizeUpper = (float)hash["sizeUpper"];
                InRoomChat.instance.addLINE(string.Concat(new string[]
			{
				"Custom titan size (",
				RCSettings.sizeLower.ToString("F2"),
				",",
				RCSettings.sizeUpper.ToString("F2"),
				") enabled."
			}));
            }
        }
        else if (RCSettings.sizeMode != 0 || RCSettings.sizeLower != 0f || RCSettings.sizeUpper != 0f)
        {
            RCSettings.sizeMode = 0;
            RCSettings.sizeLower = 0f;
            RCSettings.sizeUpper = 0f;
            InRoomChat.instance.addLINE("Custom titan size disabled.");
        }
        if (hash.ContainsKey("spawnMode") && hash.ContainsKey("nRate") && hash.ContainsKey("aRate") && hash.ContainsKey("jRate") && hash.ContainsKey("cRate") && hash.ContainsKey("pRate"))
        {
            if (RCSettings.spawnMode != (int)hash["spawnMode"] || RCSettings.nRate != (float)hash["nRate"] || RCSettings.aRate != (float)hash["aRate"] || RCSettings.jRate != (float)hash["jRate"] || RCSettings.cRate != (float)hash["cRate"] || RCSettings.pRate != (float)hash["pRate"])
            {
                RCSettings.spawnMode = (int)hash["spawnMode"];
                RCSettings.nRate = (float)hash["nRate"];
                RCSettings.aRate = (float)hash["aRate"];
                RCSettings.jRate = (float)hash["jRate"];
                RCSettings.cRate = (float)hash["cRate"];
                RCSettings.pRate = (float)hash["pRate"];
                InRoomChat.instance.addLINE(string.Concat(new string[]
			{
				"Custom spawn rate enabled (",
				RCSettings.nRate.ToString("F2"),
				"% Normal, ",
				RCSettings.aRate.ToString("F2"),
				"% Abnormal, ",
				RCSettings.jRate.ToString("F2"),
				"% Jumper, ",
				RCSettings.cRate.ToString("F2"),
				"% Crawler, ",
				RCSettings.pRate.ToString("F2"),
				"% Punk "
			}));
            }
        }
        else if (RCSettings.spawnMode != 0 || RCSettings.nRate != 0f || RCSettings.aRate != 0f || RCSettings.jRate != 0f || RCSettings.cRate != 0f || RCSettings.pRate != 0f)
        {
            RCSettings.spawnMode = 0;
            RCSettings.nRate = 0f;
            RCSettings.aRate = 0f;
            RCSettings.jRate = 0f;
            RCSettings.cRate = 0f;
            RCSettings.pRate = 0f;
            InRoomChat.instance.addLINE("Custom spawn rate disabled.");
        }
        if (hash.ContainsKey("waveModeOn") && hash.ContainsKey("waveModeNum"))
        {
            if (RCSettings.waveModeOn != (int)hash["waveModeOn"] || RCSettings.waveModeNum != (int)hash["waveModeNum"])
            {
                RCSettings.waveModeOn = (int)hash["waveModeOn"];
                RCSettings.waveModeNum = (int)hash["waveModeNum"];
                InRoomChat.instance.addLINE("Custom wave mode (" + RCSettings.waveModeNum.ToString() + ") enabled.");
            }
        }
        else if (RCSettings.waveModeOn != 0 || RCSettings.waveModeNum != 0)
        {
            RCSettings.waveModeOn = 0;
            RCSettings.waveModeNum = 0;
            InRoomChat.instance.addLINE("Custom wave mode disabled.");
        }
        if (hash.ContainsKey("friendly"))
        {
            if (RCSettings.friendlyMode != (int)hash["friendly"])
            {
                RCSettings.friendlyMode = (int)hash["friendly"];
                InRoomChat.instance.addLINE("PVP is prohibited.");
            }
        }
        else if (RCSettings.friendlyMode != 0)
        {
            RCSettings.friendlyMode = 0;
            InRoomChat.instance.addLINE("PVP is allowed.");
        }
        if (hash.ContainsKey("pvp"))
        {
            if (RCSettings.pvpMode != (int)hash["pvp"])
            {
                RCSettings.pvpMode = (int)hash["pvp"];
                string text = string.Empty;
                if (RCSettings.pvpMode == 1)
                {
                    text = "Team-Based";
                }
                else if (RCSettings.pvpMode == 2)
                {
                    text = "FFA";
                }
                InRoomChat.instance.addLINE("Blade/AHSS PVP enabled (" + text + ").");
            }
        }
        else if (RCSettings.pvpMode != 0)
        {
            RCSettings.pvpMode = 0;
            InRoomChat.instance.addLINE("Blade/AHSS PVP disabled.");
        }
        if (hash.ContainsKey("maxwave"))
        {
            if (RCSettings.maxWave != (int)hash["maxwave"])
            {
                RCSettings.maxWave = (int)hash["maxwave"];
                InRoomChat.instance.addLINE("Max wave is " + RCSettings.maxWave.ToString() + ".");
            }
        }
        else if (RCSettings.maxWave != 0)
        {
            RCSettings.maxWave = 0;
            InRoomChat.instance.addLINE("Max wave set to default.");
        }
        if (hash.ContainsKey("endless"))
        {
            if (RCSettings.endlessMode != (int)hash["endless"])
            {
                RCSettings.endlessMode = (int)hash["endless"];
                InRoomChat.instance.addLINE("Endless respawn enabled (" + RCSettings.endlessMode.ToString() + " seconds).");
            }
        }
        else if (RCSettings.endlessMode != 0)
        {
            RCSettings.endlessMode = 0;
            InRoomChat.instance.addLINE("Endless respawn disabled.");
        }
        if (hash.ContainsKey("motd"))
        {
            if (RCSettings.motd != (string)hash["motd"])
            {
                RCSettings.motd = (string)hash["motd"];
                InRoomChat.instance.addLINE("MOTD:" + RCSettings.motd + "");
            }
        }
        else if (RCSettings.motd != string.Empty)
        {
            RCSettings.motd = string.Empty;
        }
        if (hash.ContainsKey("deadlycannons"))
        {
            if (RCSettings.deadlyCannons != (int)hash["deadlycannons"])
            {
                RCSettings.deadlyCannons = (int)hash["deadlycannons"];
                InRoomChat.instance.addLINE("Cannons will now kill players.");
            }
        }
        else if (RCSettings.deadlyCannons != 0)
        {
            RCSettings.deadlyCannons = 0;
            InRoomChat.instance.addLINE("Cannons will no longer kill players.");
        }
        if (hash.ContainsKey("asoracing"))
        {
            if (RCSettings.racingStatic != (int)hash["asoracing"])
            {
                RCSettings.racingStatic = (int)hash["asoracing"];
                InRoomChat.instance.addLINE("Racing will not restart on win.");
            }
        }
        else if (RCSettings.racingStatic != 0)
        {
            RCSettings.racingStatic = 0;
            InRoomChat.instance.addLINE("Racing will restart on win.");
        }
    }

    public IEnumerator setGuildFeng()
    {
        WWW iteratorVariable0;
        WWWForm form = new WWWForm();
        form.AddField("name", LoginFengKAI.player.name);
        form.AddField("guildname", LoginFengKAI.player.guildname);
        if (Application.isWebPlayer)
        {
            iteratorVariable0 = new WWW("http://aotskins.com/version/guild.php", form);
        }
        else
        {
            iteratorVariable0 = new WWW("http://fenglee.com/game/aog/change_guild_name.php", form);
        }
        yield return iteratorVariable0;
        yield break;
    }

    [RPC]
    private void setMasterRC(PhotonMessageInfo info)
    {
        if (info.sender.isMasterClient)
        {
            masterRC = true;
        }
    }

    private void setTeam(int setting)
    {
        if (setting == 0)
        {
            PhotonNetwork.player.RCteam = 0;
            PhotonNetwork.player.name2 = LoginFengKAI.player.name;
        }
        else if (setting == 1)
        {
            string name = LoginFengKAI.player.name;
            while (name.Contains("[") && (name.Length >= (name.IndexOf("[") + 8)))
            {
                int index = name.IndexOf("[");
                name = name.Remove(index, 8);
            }
            if (!name.StartsWith("[00FFFF]"))
            {
                name = "[00FFFF]" + name;
            }
            PhotonNetwork.player.RCteam = 1;
            PhotonNetwork.player.name2 = name;
        }
        else if (setting == 2)
        {
            string str2 = LoginFengKAI.player.name;
            while (str2.Contains("[") && (str2.Length >= (str2.IndexOf("[") + 8)))
            {
                int startIndex = str2.IndexOf("[");
                str2 = str2.Remove(startIndex, 8);
            }
            if (!str2.StartsWith("[FF00FF]"))
            {
                str2 = "[FF00FF]" + str2;
            }
            this.name = str2;
            PhotonNetwork.player.RCteam = 2;
            PhotonNetwork.player.name2 = name;
        }
        else if (setting == 3)
        {
            int num3 = 0;
            int num4 = 0;
            int num5 = 1;
            foreach (PhotonPlayer player in PhotonNetwork.playerList)
            {
                int num6 = (player.RCteam);
                if (num6 > 0)
                {
                    switch (num6)
                    {
                        case 1:
                            num3++;
                            break;

                        case 2:
                            num4++;
                            break;
                    }
                }
            }
            if (num3 > num4)
            {
                num5 = 2;
            }
            this.setTeam(num5);
        }
        if (((setting == 0) || (setting == 1)) || (setting == 2))
        {
            foreach (HERO hero in heroes)
            {
                if (hero.photonView.isMine)
                {
                    base.photonView.RPC("labelRPC", PhotonTargets.All, new object[] { hero.photonView.viewID });
                }
            }
        }
    }

    [RPC]
    private void setTeamRPC(int setting, PhotonMessageInfo info)
    {
        if (info.sender.isMasterClient || info.sender.isLocal)
        {
            this.setTeam(setting);
        }
    }

    [RPC]
    private void settingRPC(ExitGames.Client.Photon.Hashtable hash, PhotonMessageInfo info)
    {
        if (info.sender.isMasterClient)
        {
            this.setGameSettings(hash);
        }
    }

    public void ShowHUDInfoCenter(string content)
    {
        if (LabelInfoCenter != null)
        {
            LabelInfoCenter.text = content;
        }
    }

    public void ShowHUDInfoCenterADD(string content)
    {
        if (LabelInfoCenter != null)
        {
            LabelInfoCenter.text = LabelInfoCenter.text + content;
        }
    }

    private void ShowHUDInfoTopCenter(string content)
    {
        if (LabelInfoTopCenter != null)
        {
            LabelInfoTopCenter.text = content;
        }
    }

    private void ShowHUDInfoTopCenterADD(string content)
    {
        if (LabelInfoTopCenter != null)
        {
            LabelInfoTopCenter.text = LabelInfoTopCenter.text + content;
        }

    }

    private void ShowHUDInfoTopLeft(string content)
    {
        if (LabelInfoTopLeft != null)
        {
            LabelInfoTopLeft.text = content;
        }
    }

    private void ShowHUDInfoTopRight(string content)
    {
        if (LabelInfoTopRight != null)
        {
            LabelInfoTopRight.text = content;
        }
    }

    private void ShowHUDInfoTopRightMAPNAME(string content)
    {
        if (LabelInfoTopRight != null)
        {
            LabelInfoTopRight.text = LabelInfoTopRight.text + content;
        }
    }

    [RPC]
    private void showResult(string text0, string text1, string text2, string text3, string text4, string text6, PhotonMessageInfo t)
    {
        if (!this.gameTimesUp && (t.sender.isMasterClient || t.sender.isLocal))
        {
            this.gameTimesUp = true;
            NGUITools.SetActive(uiT.panels[0], false);
            NGUITools.SetActive(uiT.panels[1], false);
            NGUITools.SetActive(uiT.panels[2], true);
            NGUITools.SetActive(uiT.panels[3], false);
            CyanMod.CachingsGM.Find("LabelName").GetComponent<UILabel>().text = text0;
            CyanMod.CachingsGM.Find("LabelKill").GetComponent<UILabel>().text = text1;
            CyanMod.CachingsGM.Find("LabelDead").GetComponent<UILabel>().text = text2;
            CyanMod.CachingsGM.Find("LabelMaxDmg").GetComponent<UILabel>().text = text3;
            CyanMod.CachingsGM.Find("LabelTotalDmg").GetComponent<UILabel>().text = text4;
            CyanMod.CachingsGM.Find("LabelResultTitle").GetComponent<UILabel>().text = text6;
            Screen.lockCursor = false;
            Screen.showCursor = true;
            IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.STOP;
            this.gameStart = false;
        }
        else if (!t.sender.isMasterClient && PhotonNetwork.player.isMasterClient)
        {
            this.kickPlayerRC(t.sender, true, "false game end.");
        }

    }

    private void SingleShowHUDInfoTopCenter(string content)
    {

        if (LabelInfoTopLeft != null)
        {
            LabelInfoTopLeft.text = content;
        }
    }

    private void SingleShowHUDInfoTopLeft(string content)
    {

        if (LabelInfoTopLeft != null)
        {
            content = content.Replace("[0]", "[*^_^*]");
            LabelInfoTopLeft.text = content;
        }
    }

    [RPC]
    public void someOneIsDead(int id = -1)
    {
        if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE)
        {
            if (id != 0)
            {
                this.PVPtitanScore += 2;
            }
            this.checkPVPpts();
            object[] parameters = new object[] { this.PVPhumanScore, this.PVPtitanScore };
            base.photonView.RPC("refreshPVPStatus", PhotonTargets.Others, parameters);
        }
        else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.ENDLESS_TITAN)
        {
            this.titanScore++;
        }
        else if (((IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.KILL_TITAN) || (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)) || ((IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.BOSS_FIGHT_CT) || (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.TROST)))
        {
            if (this.isPlayerAllDead2())
            {
                this.gameLose2();
            }
        }
        else if (((IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_AHSS) && (RCSettings.pvpMode == 0)) && (RCSettings.bombMode == 0))
        {
            if (this.isPlayerAllDead2())
            {
                this.gameLose2();
                this.teamWinner = 0;
            }
            if (this.isTeamAllDead2(1))
            {
                this.teamWinner = 2;
                this.gameWin2();
            }
            if (this.isTeamAllDead2(2))
            {
                this.teamWinner = 1;
                this.gameWin2();
            }
        }
    }

    public void SpawnNonAITitan(string id, string tag = "titanRespawn")
    {
        GameObject obj2;
        GameObject[] objArray = GameObject.FindGameObjectsWithTag(tag);
        GameObject obj3 = objArray[UnityEngine.Random.Range(0, objArray.Length)];
        this.myLastHero = id.ToUpper();
        if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE)
        {
            obj2 = PhotonNetwork.Instantiate("TITAN_VER3.1", this.checkpoint.transform.position + new Vector3((float)UnityEngine.Random.Range(-20, 20), 2f, (float)UnityEngine.Random.Range(-20, 20)), this.checkpoint.transform.rotation, 0);
        }
        else
        {
            obj2 = PhotonNetwork.Instantiate("TITAN_VER3.1", obj3.transform.position, obj3.transform.rotation, 0);
        }
        IN_GAME_MAIN_CAMERA.instance.setMainObjectASTITAN(obj2);
        TITAN tit = obj2.GetComponent<TITAN>();
        tit.nonAI = true;
        tit.speed = 30f;
        obj2.GetComponent<TITAN_CONTROLLER>().enabled = true;
        if ((id == "RANDOM") && (UnityEngine.Random.Range(0, 100) < 7))
        {
            tit.setAbnormalType2(AbnormalType.TYPE_CRAWLER, true);
        }
        IN_GAME_MAIN_CAMERA.instance.enabled = true;
        IN_GAME_MAIN_CAMERA.instance.Smov.disable = true;
        IN_GAME_MAIN_CAMERA.instance.mouselook.disable = true;
        IN_GAME_MAIN_CAMERA.instance.gameOver = false;
        PhotonNetwork.player.dead = false;
        PhotonNetwork.player.isTitan = 2;
        if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.TPS || IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.oldTPS)
        {
            Screen.lockCursor = true;
        }
        else
        {
            Screen.lockCursor = false;
        }
        Screen.showCursor = true;
        this.ShowHUDInfoCenter(string.Empty);
    }

    public void SpawnNonAITitan2(string id, string tag = "titanRespawn")
    {
        if (logicLoaded && customLevelLoaded)
        {
            GameObject obj2;
            GameObject[] objArray = GameObject.FindGameObjectsWithTag(tag);
            GameObject obj3 = objArray[UnityEngine.Random.Range(0, objArray.Length)];
            Vector3 position = obj3.transform.position;
            if (level.StartsWith("Custom") && (this.titanSpawns.Count > 0))
            {
                position = this.titanSpawns[UnityEngine.Random.Range(0, this.titanSpawns.Count)];
            }
            this.myLastHero = id.ToUpper();
            if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE)
            {
                obj2 = PhotonNetwork.Instantiate("TITAN_VER3.1", this.checkpoint.transform.position + new Vector3((float)UnityEngine.Random.Range(-20, 20), 2f, (float)UnityEngine.Random.Range(-20, 20)), this.checkpoint.transform.rotation, 0);
            }
            else
            {
                obj2 = PhotonNetwork.Instantiate("TITAN_VER3.1", position, obj3.transform.rotation, 0);
            }
            IN_GAME_MAIN_CAMERA.instance.setMainObjectASTITAN(obj2);
            TITAN tit = obj2.GetComponent<TITAN>();
            tit.nonAI = true;
            tit.speed = 30f;
            obj2.GetComponent<TITAN_CONTROLLER>().enabled = true;
            if ((id == "RANDOM") && (UnityEngine.Random.Range(0, 100) < 7))
            {
                tit.setAbnormalType2(AbnormalType.TYPE_CRAWLER, true);
            }
            IN_GAME_MAIN_CAMERA.instance.enabled = true;
            IN_GAME_MAIN_CAMERA.instance.Smov.disable = true;
            IN_GAME_MAIN_CAMERA.instance.mouselook.disable = true;
            IN_GAME_MAIN_CAMERA.instance.gameOver = false;
            PhotonNetwork.player.dead = false;
            PhotonNetwork.player.isTitan = 2;
            if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.TPS || IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.oldTPS)
            {
                Screen.lockCursor = true;
            }
            else
            {
                Screen.lockCursor = false;
            }
            Screen.showCursor = true;
            this.ShowHUDInfoCenter(string.Empty);
        }
        else
        {
            this.NOTSpawnNonAITitanRC(id);
        }
    }

    public void SpawnPlayer(string id, string tag = "playerRespawn")
    {
        if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE)
        {
            this.SpawnPlayerAt2(id, this.checkpoint);
        }
        else
        {
            this.myLastRespawnTag = tag;
            GameObject[] objArray = GameObject.FindGameObjectsWithTag(tag);
            GameObject pos = objArray[UnityEngine.Random.Range(0, objArray.Length)];
            this.SpawnPlayerAt2(id, pos);
        }
    }

    public void SpawnPlayerAt(string id, GameObject pos)
    {

    }

    public void SpawnPlayerAt2(string id, GameObject pos)
    {
        if (!logicLoaded || !customLevelLoaded)
        {
            this.NOTSpawnPlayerRC(id);
        }
        else
        {
            Vector3 position = pos.transform.position;
            if (this.racingSpawnPointSet)
            {
                position = this.racingSpawnPoint;
            }
            else if (level.StartsWith("Custom"))
            {
                if (cext.returnIntFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.RCteam]) == 0)
                {
                    List<Vector3> list = new List<Vector3>();
                    foreach (Vector3 vector2 in this.playerSpawnsC)
                    {
                        list.Add(vector2);
                    }
                    foreach (Vector3 vector2 in this.playerSpawnsM)
                    {
                        list.Add(vector2);
                    }
                    if (list.Count > 0)
                    {
                        position = list[UnityEngine.Random.Range(0, list.Count)];
                    }
                }
                else if (cext.returnIntFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.RCteam]) == 1)
                {
                    if (this.playerSpawnsC.Count > 0)
                    {
                        position = this.playerSpawnsC[UnityEngine.Random.Range(0, this.playerSpawnsC.Count)];
                    }
                }
                else if ((cext.returnIntFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.RCteam]) == 2) && (this.playerSpawnsM.Count > 0))
                {
                    position = this.playerSpawnsM[UnityEngine.Random.Range(0, this.playerSpawnsM.Count)];
                }
            }
            IN_GAME_MAIN_CAMERA component = IN_GAME_MAIN_CAMERA.instance;
            this.myLastHero = id.ToUpper();
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                if (IN_GAME_MAIN_CAMERA.singleCharacter == "TITAN_EREN")
                {
                    component.setMainObject((GameObject)UnityEngine.Object.Instantiate(Resources.Load("TITAN_EREN"), pos.transform.position, pos.transform.rotation), true, false);
                }
                else
                {
                    component.setMainObject((GameObject)UnityEngine.Object.Instantiate(Resources.Load("AOTTG_HERO 1"), pos.transform.position, pos.transform.rotation), true, false);
                  
                    HERO hero = component.main_object.GetComponent<HERO>();
                    if (((IN_GAME_MAIN_CAMERA.singleCharacter == "SET 1") || (IN_GAME_MAIN_CAMERA.singleCharacter == "SET 2")) || (IN_GAME_MAIN_CAMERA.singleCharacter == "SET 3"))
                    {
                        HeroCostume costume = CostumeConeveter.LocalDataToHeroCostume(IN_GAME_MAIN_CAMERA.singleCharacter);
                        costume.checkstat();
                        CostumeConeveter.HeroCostumeToLocalData(costume, IN_GAME_MAIN_CAMERA.singleCharacter);
                        hero.setup.init();

                        if (costume != null)
                        {
                            hero.setup.myCostume = costume;
                            hero.setup.myCostume.stat = costume.stat;
                        }
                        else
                        {
                            costume = HeroCostume.costumeOption[3];
                            hero.setup.myCostume = costume;
                            hero.setup.myCostume.stat = HeroStat.getInfo(costume.name.ToUpper());
                        }
                        hero.setup.setCharacterComponent();
                        hero.setStat2();
                        hero.setSkillHUDPosition2();
                    }
                    else
                    {
                        for (int i = 0; i < HeroCostume.costume.Length; i++)
                        {
                            if (HeroCostume.costume[i].name.ToUpper() == IN_GAME_MAIN_CAMERA.singleCharacter.ToUpper())
                            {
                                int index = (HeroCostume.costume[i].id + CheckBoxCostume.costumeSet) - 1;
                                if (HeroCostume.costume[index].name != HeroCostume.costume[i].name)
                                {
                                    index = HeroCostume.costume[i].id + 1;
                                }
                                hero.setup.init();
                                hero.setup.myCostume = HeroCostume.costume[index];
                                hero.setup.myCostume.stat = HeroStat.getInfo(HeroCostume.costume[index].name.ToUpper());
                                hero.setup.setCharacterComponent();
                                hero.setStat2();
                                hero.setSkillHUDPosition2();
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                component.setMainObject(PhotonNetwork.Instantiate("AOTTG_HERO 1", position, pos.transform.rotation, 0), true, false);
                HERO hero = component.main_object.GetComponent<HERO>();
                id = id.ToUpper();
                if (((id == "SET 1") || (id == "SET 2")) || (id == "SET 3"))
                {
                    HeroCostume costume2 = CostumeConeveter.LocalDataToHeroCostume(id);
                    costume2.checkstat();
                    CostumeConeveter.HeroCostumeToLocalData(costume2, id);
                    hero.setup.init();
                    if (costume2 != null)
                    {
                        hero.setup.myCostume = costume2;
                        hero.setup.myCostume.stat = costume2.stat;
                    }
                    else
                    {
                        costume2 = HeroCostume.costumeOption[3];
                        hero.setup.myCostume = costume2;
                        hero.setup.myCostume.stat = HeroStat.getInfo(costume2.name.ToUpper());
                    }
                    hero.setup.setCharacterComponent();
                    hero.setStat2();
                    hero.setSkillHUDPosition2();
                }
                else
                {
                    for (int j = 0; j < HeroCostume.costume.Length; j++)
                    {
                        if (HeroCostume.costume[j].name.ToUpper() == id.ToUpper())
                        {
                            int num4 = HeroCostume.costume[j].id;
                            if (id.ToUpper() != "AHSS")
                            {
                                num4 += CheckBoxCostume.costumeSet - 1;
                            }
                            if (HeroCostume.costume[num4].name != HeroCostume.costume[j].name)
                            {
                                num4 = HeroCostume.costume[j].id + 1;
                            }
                            hero.setup.init();
                            hero.setup.myCostume = HeroCostume.costume[num4];
                            hero.setup.myCostume.stat = HeroStat.getInfo(HeroCostume.costume[num4].name.ToUpper());
                            hero.setup.setCharacterComponent();
                            hero.setStat2();
                            hero.setSkillHUDPosition2();
                            break;
                        }
                    }
                }
                CostumeConeveter.HeroCostumeToPhotonData2(hero.setup.myCostume, PhotonNetwork.player);
                if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE)
                {
                    Transform transform = component.main_object.transform;
                    transform.position += new Vector3((float)UnityEngine.Random.Range(-20, 20), 2f, (float)UnityEngine.Random.Range(-20, 20));
                }
                ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
                hashtable.Add("dead", false);
                hashtable.Add(PhotonPlayerProperty.isTitan, 1);
                PhotonNetwork.player.SetCustomProperties(hashtable);
            }
            component.enabled = true;
            component.setHUDposition();
            component.Smov.disable = true;
            component.mouselook.disable = true;
            component.gameOver = false;
            if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.TPS || IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.oldTPS)
            {
                Screen.lockCursor = true;
            }
            else
            {
                Screen.lockCursor = false;
            }
            Screen.showCursor = false;
            this.isLosing = false;
            this.ShowHUDInfoCenter(string.Empty);
        }
    }

    [RPC]
    public void spawnPlayerAtRPC(float posX, float posY, float posZ, PhotonMessageInfo info)
    {
        if (((info.sender.isMasterClient && logicLoaded) && (customLevelLoaded && !this.needChooseSide)) && IN_GAME_MAIN_CAMERA.instance.gameOver)
        {
            string str2;
            Vector3 position = new Vector3(posX, posY, posZ);
            IN_GAME_MAIN_CAMERA mcam = IN_GAME_MAIN_CAMERA.instance;
            mcam.setMainObject(PhotonNetwork.Instantiate("AOTTG_HERO 1", position, new Quaternion(0f, 0f, 0f, 1f), 0), true, false);
            string slot = this.myLastHero.ToUpper();
            if (((str2 = slot) != null) && (((str2 == "SET 1") || (str2 == "SET 2")) || (str2 == "SET 3")))
            {
                HeroCostume costume = CostumeConeveter.LocalDataToHeroCostume(slot);
                costume.checkstat();
                CostumeConeveter.HeroCostumeToLocalData(costume, slot);
                HERO_SETUP hs = mcam.main_objectH.GetComponent<HERO_SETUP>();
                hs.init();
                if (costume != null)
                {
                    hs.myCostume = costume;
                    hs.myCostume.stat = costume.stat;
                }
                else
                {
                    costume = HeroCostume.costumeOption[3];
                    hs.myCostume = costume;
                    hs.myCostume.stat = HeroStat.getInfo(costume.name.ToUpper());
                }
                hs.setCharacterComponent();
                mcam.main_objectH.setStat2();
                mcam.main_objectH.setSkillHUDPosition2();
            }
            else
            {
                for (int i = 0; i < HeroCostume.costume.Length; i++)
                {
                    if (HeroCostume.costume[i].name.ToUpper() == slot.ToUpper())
                    {
                        int id = HeroCostume.costume[i].id;
                        if (slot.ToUpper() != "AHSS")
                        {
                            id += CheckBoxCostume.costumeSet - 1;
                        }
                        if (HeroCostume.costume[id].name != HeroCostume.costume[i].name)
                        {
                            id = HeroCostume.costume[i].id + 1;
                        }
                        HERO_SETUP hs = mcam.main_objectH.GetComponent<HERO_SETUP>();
                        hs.init();
                        hs.myCostume = HeroCostume.costume[id];
                        hs.myCostume.stat = HeroStat.getInfo(HeroCostume.costume[id].name.ToUpper());
                        hs.setCharacterComponent();
                        mcam.main_objectH.setStat2();
                        mcam.main_objectH.setSkillHUDPosition2();
                        break;
                    }
                }
            }
            CostumeConeveter.HeroCostumeToPhotonData2(mcam.main_objectH.GetComponent<HERO_SETUP>().myCostume, PhotonNetwork.player);
            if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE)
            {

                mcam.main_objectT.position += new Vector3((float)UnityEngine.Random.Range(-20, 20), 2f, (float)UnityEngine.Random.Range(-20, 20));
            }
            PhotonNetwork.player.isTitan = 1;
            PhotonNetwork.player.dead = false;
            mcam.enabled = true;
            mcam.setHUDposition();
            mcam.Smov.disable = true;
            mcam.mouselook.disable = true;
            mcam.gameOver = false;
            if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.TPS || IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.oldTPS)
            {
                Screen.lockCursor = true;
            }
            else
            {
                Screen.lockCursor = false;
            }
            Screen.showCursor = false;
            this.isLosing = false;
            this.ShowHUDInfoCenter(string.Empty);
        }
    }

    private void spawnPlayerCustomMap()
    {
        if (!this.needChooseSide && IN_GAME_MAIN_CAMERA.instance.gameOver)
        {
            IN_GAME_MAIN_CAMERA.instance.gameOver = false;
            if ((PhotonNetwork.player.isTitan) == 2)
            {
                this.SpawnNonAITitan2(this.myLastHero, "titanRespawn");
            }
            else
            {
                this.SpawnPlayer(this.myLastHero, this.myLastRespawnTag);
            }
            this.ShowHUDInfoCenter(string.Empty);
        }
    }

    public TITAN spawnTitan(int rate, Vector3 position, Quaternion rotation, bool punk = false)
    {

        TITAN titan = this.spawnTitanRaw(position, rotation);
        if (punk)
        {
            titan.setAbnormalType2(AbnormalType.TYPE_PUNK, false);
        }
        else if (UnityEngine.Random.Range(0, 100) < rate)
        {
            if (IN_GAME_MAIN_CAMERA.difficulty ==  DIFFICULTY.ABNORMAL)
            {
                if ((UnityEngine.Random.Range((float)0f, (float)1f) >= 0.7f) && !lvlInfo.noCrawler)
                {
                    titan.setAbnormalType2(AbnormalType.TYPE_CRAWLER, false);
                }
                else
                {
                    titan.setAbnormalType2(AbnormalType.TYPE_JUMPER, false);
                }
            }
        }
        else if (IN_GAME_MAIN_CAMERA.difficulty == DIFFICULTY.ABNORMAL)
        {
            if ((UnityEngine.Random.Range((float)0f, (float)1f) >= 0.7f) && !lvlInfo.noCrawler)
            {
                titan.setAbnormalType2(AbnormalType.TYPE_CRAWLER, false);
            }
            else
            {
                titan.setAbnormalType2(AbnormalType.TYPE_JUMPER, false);
            }
        }
        else if (UnityEngine.Random.Range(0, 100) < rate)
        {
            if ((UnityEngine.Random.Range((float)0f, (float)1f) >= 0.8f) && !lvlInfo.noCrawler)
            {
                titan.setAbnormalType2(AbnormalType.TYPE_CRAWLER, false);
            }
            else
            {
                titan.setAbnormalType2(AbnormalType.TYPE_I, false);
            }
        }
        else if ((UnityEngine.Random.Range((float)0f, (float)1f) >= 0.8f) && !lvlInfo.noCrawler)
        {
            titan.setAbnormalType2(AbnormalType.TYPE_CRAWLER, false);
        }
        else
        {
            titan.setAbnormalType2(AbnormalType.TYPE_JUMPER, false);
        }
        if ((int)FengGameManagerMKII.settings[288] == 0)
        {
            GameObject obj2;
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                obj2 = (GameObject)UnityEngine.Object.Instantiate(Cach.FXtitanSpawn != null ? Cach.FXtitanSpawn : Cach.FXtitanSpawn = (GameObject)Resources.Load("FX/FXtitanSpawn"), titan.transform.position, Quaternion.Euler(-90f, 0f, 0f));
            }
            else
            {
                obj2 = PhotonNetwork.Instantiate("FX/FXtitanSpawn", titan.transform.position, Quaternion.Euler(-90f, 0f, 0f), 0);
            }
            obj2.transform.localScale = titan.transform.localScale;
        }
        return titan;
    }

    public void spawnTitanAction(int type, float size, int health, int number)
    {
        Vector3 position = new Vector3(UnityEngine.Random.Range((float)-400f, (float)400f), 0f, UnityEngine.Random.Range((float)-400f, (float)400f));
        Quaternion rotation = new Quaternion(0f, 0f, 0f, 1f);
        if (this.titanSpawns.Count > 0)
        {
            position = this.titanSpawns[UnityEngine.Random.Range(0, this.titanSpawns.Count)];
        }
        else
        {
            GameObject[] objArray = GameObject.FindGameObjectsWithTag("titanRespawn");
            if (objArray.Length > 0)
            {
                int index = UnityEngine.Random.Range(0, objArray.Length);
                GameObject obj2 = objArray[index];
                while (objArray[index] == null)
                {
                    index = UnityEngine.Random.Range(0, objArray.Length);
                    obj2 = objArray[index];
                }
                objArray[index] = null;
                position = obj2.transform.position;
                rotation = obj2.transform.rotation;
            }
        }
        for (int i = 0; i < number; i++)
        {
            TITAN titan1 = this.spawnTitanRaw(position, rotation);
            titan1.resetLevel(size);
            titan1.hasSetLevel = true;
            if (health > 0f)
            {
                titan1.currentHealth = health;
                titan1.maxHealth = health;
            }
            switch (type)
            {
                case 0:
                    titan1.setAbnormalType2(AbnormalType.NORMAL, false);
                    break;

                case 1:
                    titan1.setAbnormalType2(AbnormalType.TYPE_I, false);
                    break;

                case 2:
                    titan1.setAbnormalType2(AbnormalType.TYPE_JUMPER, false);
                    break;

                case 3:
                    titan1.setAbnormalType2(AbnormalType.TYPE_CRAWLER, true);
                    break;

                case 4:
                    titan1.setAbnormalType2(AbnormalType.TYPE_PUNK, false);
                    break;
            }
        }
    }

    public void spawnTitanAtAction(int type, float size, int health, int number, float posX, float posY, float posZ)
    {
        Vector3 position = new Vector3(posX, posY, posZ);
        Quaternion rotation = new Quaternion(0f, 0f, 0f, 1f);
        for (int i = 0; i < number; i++)
        {
            TITAN titan = this.spawnTitanRaw(position, rotation);
            titan.resetLevel(size);
            titan.hasSetLevel = true;
            if (health > 0f)
            {
                titan.currentHealth = health;
                titan.maxHealth = health;
            }
            switch (type)
            {
                case 0:
                    titan.setAbnormalType2(AbnormalType.NORMAL, false);
                    break;

                case 1:
                    titan.setAbnormalType2(AbnormalType.TYPE_I, false);
                    break;

                case 2:
                    titan.setAbnormalType2(AbnormalType.TYPE_JUMPER, false);
                    break;

                case 3:
                    titan.setAbnormalType2(AbnormalType.TYPE_CRAWLER, true);
                    break;

                case 4:
                    titan.setAbnormalType2(AbnormalType.TYPE_PUNK, false);
                    break;
            }
        }
    }

    public void spawnTitanCustom(string type, int abnormal, int rate, bool punk)
    {
        int num;
        Vector3 position;
        Quaternion rotation;
        GameObject[] objArray;
        int num2;
        GameObject obj2;
        int moreTitans = rate;
        if (level.StartsWith("Custom"))
        {
            moreTitans = 5;
            if (RCSettings.gameType == 1)
            {
                moreTitans = 3;
            }
            else if ((RCSettings.gameType == 2) || (RCSettings.gameType == 3))
            {
                moreTitans = 0;
            }
        }
        if ((RCSettings.moreTitans > 0) || (((RCSettings.moreTitans == 0) && level.StartsWith("Custom")) && (RCSettings.gameType >= 2)))
        {
            moreTitans = RCSettings.moreTitans;
        }
        if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)
        {
            if (punk)
            {
                moreTitans = rate;
            }
            else
            {
                int waveModeNum;
                if (RCSettings.moreTitans == 0)
                {
                    waveModeNum = 1;
                    if (RCSettings.waveModeOn == 1)
                    {
                        waveModeNum = RCSettings.waveModeNum;
                    }
                    moreTitans += (this.wave - 1) * (waveModeNum - 1);
                }
                else if (RCSettings.moreTitans > 0)
                {
                    waveModeNum = 1;
                    if (RCSettings.waveModeOn == 1)
                    {
                        waveModeNum = RCSettings.waveModeNum;
                    }
                    moreTitans += (this.wave - 1) * waveModeNum;
                }
            }
        }
        moreTitans = Math.Min(50, moreTitans);
        if (RCSettings.spawnMode == 1)
        {
            float nRate = RCSettings.nRate;
            float aRate = RCSettings.aRate;
            float jRate = RCSettings.jRate;
            float cRate = RCSettings.cRate;
            float pRate = RCSettings.pRate;
            if (punk && (RCSettings.punkWaves == 1))
            {
                nRate = 0f;
                aRate = 0f;
                jRate = 0f;
                cRate = 0f;
                pRate = 100f;
                moreTitans = rate;
            }
            for (num = 0; num < moreTitans; num++)
            {
                position = new Vector3(UnityEngine.Random.Range((float)-400f, (float)400f), 0f, UnityEngine.Random.Range((float)-400f, (float)400f));
                rotation = new Quaternion(0f, 0f, 0f, 1f);
                if (this.titanSpawns.Count > 0)
                {
                    position = this.titanSpawns[UnityEngine.Random.Range(0, this.titanSpawns.Count)];
                }
                else
                {
                    objArray = GameObject.FindGameObjectsWithTag("titanRespawn");
                    if (objArray.Length > 0)
                    {
                        num2 = UnityEngine.Random.Range(0, objArray.Length);
                        obj2 = objArray[num2];
                        while (objArray[num2] == null)
                        {
                            num2 = UnityEngine.Random.Range(0, objArray.Length);
                            obj2 = objArray[num2];
                        }
                        objArray[num2] = null;
                        position = obj2.transform.position;
                        rotation = obj2.transform.rotation;
                    }
                }
                float num10 = UnityEngine.Random.Range((float)0f, (float)100f);
                if (num10 <= ((((nRate + aRate) + jRate) + cRate) + pRate))
                {
                    TITAN titan = this.spawnTitanRaw(position, rotation);
                    if (num10 < nRate)
                    {
                        titan.setAbnormalType2(AbnormalType.NORMAL, false);
                    }
                    else if ((num10 >= nRate) && (num10 < (nRate + aRate)))
                    {
                        titan.setAbnormalType2(AbnormalType.TYPE_I, false);
                    }
                    else if ((num10 >= (nRate + aRate)) && (num10 < ((nRate + aRate) + jRate)))
                    {
                        titan.setAbnormalType2(AbnormalType.TYPE_JUMPER, false);
                    }
                    else if ((num10 >= ((nRate + aRate) + jRate)) && (num10 < (((nRate + aRate) + jRate) + cRate)))
                    {
                        titan.setAbnormalType2(AbnormalType.TYPE_CRAWLER, true);
                    }
                    else if ((num10 >= (((nRate + aRate) + jRate) + cRate)) && (num10 < ((((nRate + aRate) + jRate) + cRate) + pRate)))
                    {
                        titan.setAbnormalType2(AbnormalType.TYPE_PUNK, false);
                    }
                    else
                    {
                        titan.setAbnormalType2(AbnormalType.NORMAL, false);
                    }
                }
                else
                {
                    this.spawnTitan(abnormal, position, rotation, punk);
                }
            }
        }
        else if (level.StartsWith("Custom"))
        {
            for (num = 0; num < moreTitans; num++)
            {
                position = new Vector3(UnityEngine.Random.Range((float)-400f, (float)400f), 0f, UnityEngine.Random.Range((float)-400f, (float)400f));
                rotation = new Quaternion(0f, 0f, 0f, 1f);
                if (this.titanSpawns.Count > 0)
                {
                    position = this.titanSpawns[UnityEngine.Random.Range(0, this.titanSpawns.Count)];
                }
                else
                {
                    objArray = GameObject.FindGameObjectsWithTag("titanRespawn");
                    if (objArray.Length > 0)
                    {
                        num2 = UnityEngine.Random.Range(0, objArray.Length);
                        obj2 = objArray[num2];
                        while (objArray[num2] == null)
                        {
                            num2 = UnityEngine.Random.Range(0, objArray.Length);
                            obj2 = objArray[num2];
                        }
                        objArray[num2] = null;
                        position = obj2.transform.position;
                        rotation = obj2.transform.rotation;
                    }
                }
                this.spawnTitan(abnormal, position, rotation, punk);
            }
        }
        else
        {
            this.randomSpawnTitan("titanRespawn", abnormal, moreTitans, punk);
        }
    }

    private TITAN spawnTitanRaw(Vector3 position, Quaternion rotation)
    {
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
        {
            return ((GameObject)UnityEngine.Object.Instantiate(Cach.TITAN_VER3_1 != null ? Cach.TITAN_VER3_1 : Cach.TITAN_VER3_1 = (GameObject)Resources.Load("TITAN_VER3.1"), position, rotation)).GetComponent<TITAN>();
        }
        return (PhotonNetwork.Instantiate("TITAN_VER3.1", position, rotation, 0)).GetComponent<TITAN>();
    }
    [RPC]
    private void spawnTitanRPC(PhotonMessageInfo info)
    {
        if (info.sender.isMasterClient)
        {
            foreach (TITAN titan in this.titans)
            {
                if (titan.photonView.isMine && (!PhotonNetwork.isMasterClient || titan.nonAI))
                {
                    PhotonNetwork.Destroy(titan.gameObject);
                }
            }
            this.SpawnNonAITitan2(this.myLastHero, "titanRespawn");
        }
    }

    private void Start()
    {
        //string[] str54 = File.ReadAllLines(Application.dataPath + "/new2.txt");
        //string str55 = string.Empty;
        //foreach (string str2 in str54)
        //{
        //    string str = str2.Trim();
        //    str55 = str55 + "public string " + str + "Filed" + ";" + "\n" + " public string " + str + "\n{\n get{ return " + str + "Filed;} \n set\n{ \n " + str + "Filed = value;" + "}\n}";
        //}
        //File.WriteAllText(Application.dataPath + "/mew2fix.txt", str55);
        InRoomChat.IsPausedFlayer = false;
        FengGameManagerMKII.instance = this;
        statusGUI = base.gameObject.AddComponent<PhotonStatsGui>();

        base.gameObject.AddComponent<SpeeFocusPlayer>();
        confer = base.gameObject.AddComponent<Conference>();
        killinfo = new List<KillInfoComponent>();
        base.gameObject.name = "MultiplayerManager";
        HeroCostume.init2();
        CharacterMaterials.init();
        UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
        isPlayng = false;
        sizeImage = 30f;
        this.heroes = new ArrayList();
        this.eT = new ArrayList();
        this.titans = new List<TITAN>();
        this.fT = new ArrayList();
        this.cT = new ArrayList();
        allheroes = new List<GameObject>();
        alltitans = new List<GameObject>();
        this.hooks = new ArrayList();
        this.name = string.Empty;
        if (FengGameManagerMKII.privateServerField == null)
        {
            FengGameManagerMKII.privateServerField = string.Empty;
        }
        Famel_Titan_mode_survive = false;
        FengGameManagerMKII.usernameField = string.Empty;
        FengGameManagerMKII.passwordField = string.Empty;
        this.resetGameSettings();
        FengGameManagerMKII.banHash = new ExitGames.Client.Photon.Hashtable();
        FengGameManagerMKII.imatitan = new ExitGames.Client.Photon.Hashtable();
        FengGameManagerMKII.oldScript = string.Empty;
        FengGameManagerMKII.currentLevel = string.Empty;
        if (FengGameManagerMKII.currentScript == null)
        {
            FengGameManagerMKII.currentScript = string.Empty;
        }
        this.titanSpawns = new List<Vector3>();
        this.playerSpawnsC = new List<Vector3>();
        this.playerSpawnsM = new List<Vector3>();
        this.playersRPC = new List<PhotonPlayer>();
        this.levelCache = new List<string[]>();
        this.titanSpawners = new List<TitanSpawner>();
        this.restartCount = new List<float>();
        FengGameManagerMKII.ignoreList = new List<int>();
        this.groundList = new List<GameObject>();
        FengGameManagerMKII.noRestart = false;
        FengGameManagerMKII.masterRC = false;
        this.isSpawning = false;
        FengGameManagerMKII.intVariables = new ExitGames.Client.Photon.Hashtable();
        FengGameManagerMKII.heroHash = new ExitGames.Client.Photon.Hashtable();
        FengGameManagerMKII.boolVariables = new ExitGames.Client.Photon.Hashtable();
        FengGameManagerMKII.stringVariables = new ExitGames.Client.Photon.Hashtable();
        FengGameManagerMKII.floatVariables = new ExitGames.Client.Photon.Hashtable();
        FengGameManagerMKII.RCRegions = new ExitGames.Client.Photon.Hashtable();
        FengGameManagerMKII.RCEvents = new ExitGames.Client.Photon.Hashtable();
        FengGameManagerMKII.RCVariableNames = new ExitGames.Client.Photon.Hashtable();
        FengGameManagerMKII.RCRegionTriggers = new ExitGames.Client.Photon.Hashtable();
        FengGameManagerMKII.playerVariables = new ExitGames.Client.Photon.Hashtable();
        FengGameManagerMKII.titanVariables = new ExitGames.Client.Photon.Hashtable();
        FengGameManagerMKII.logicLoaded = false;
        FengGameManagerMKII.customLevelLoaded = false;
        FengGameManagerMKII.oldScriptLogic = string.Empty;
        FengGameManagerMKII.currentScriptLogic = string.Empty;
        this.retryTime = 0f;
        this.playerList = string.Empty;
        this.updateTime = 0f;
        if (this.textureBackgroundBlack == null)
        {
            this.textureBackgroundBlack = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            this.textureBackgroundBlack.SetPixel(0, 0, new Color(0f, 0f, 0f, 1f));
            this.textureBackgroundBlack.Apply();
        }
        if (this.textureBackgroundBlue == null)
        {
            this.textureBackgroundBlue = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            this.textureBackgroundBlue.SetPixel(0, 0, new Color(0.08f, 0.3f, 0.4f, 1f));
            this.textureBackgroundBlue.Apply();
        }
        this.loadconfig();
        List<string> list = new List<string> { "AOTTG_HERO", "Colossal", "Icosphere", "Cube", "colossal", "CITY", "city", "rock", "PanelLogin", "LOGIN", "VERSION" };
        UnityEngine.Object[] array = UnityEngine.Object.FindObjectsOfType(typeof(GameObject));
        for (int i = 0; i < array.Length; i++)
        {
            GameObject gameObject = (GameObject)array[i];
            foreach (string current in list)
            {
                if (gameObject.name.Contains(current) || gameObject.name == "Button" || gameObject.name == "PopupListLang" || (gameObject.name == "Label" && gameObject.GetComponent<UILabel>().text.Contains("Snap")))
                {
                    UnityEngine.Object.Destroy(gameObject);
                }
                else if (gameObject.name == "Checkbox")
                {
                    UnityEngine.Object.Destroy(gameObject);
                }
            }
        }
        this.setBackground();
        ChangeQuality.setCurrentQuality();
        fpsinterval = 0.5;
        fpsfps = 0;
        fpstime = fpsinterval;
        fpsaccum = 0;
        fpsframes = 0;
        selplayer = PhotonNetwork.player;
        AddPl = new List<PhotonPlayer> { PhotonNetwork.player };
    
        currents = null;
        palitra = new List<Color>();
        myanim = new List<AnimN>();
        addnickName = string.Empty;
        timerGoRestart = 0;
        isGoRestart = false;
        showFPS = false;
        fpsinterval = 0.5;
        fpsfps = 0;
        fpstime = fpsinterval;
        fpsaccum = 0;
        fpsframes = 0;
        AddOnPlayerBanlist = string.Empty;
        playergrab_skin = null;
        number_grabed_skin = 0;
        ApplySettings();
    }

    public void titanGetKill(PhotonPlayer player, int Damage, string name)
    {
        Damage = Mathf.Max(10, Damage);
        object[] parameters = new object[] { Damage };
        base.photonView.RPC("netShowDamage", player, parameters);
        object[] objArray2 = new object[] { name, false };
        base.photonView.RPC("oneTitanDown", PhotonTargets.MasterClient, objArray2);
        this.sendKillInfo(false, (string)player.name2, true, name, Damage);
        this.playerKillInfoUpdate(player, Damage);
    }

    public void titanGetKillbyServer(int Damage, string name)
    {
        Damage = Mathf.Max(10, Damage);
        this.sendKillInfo(false, LoginFengKAI.player.name, true, name, Damage);
        this.netShowDamage(Damage);
        this.oneTitanDown(name, false);
        this.playerKillInfoUpdate(PhotonNetwork.player, Damage);
    }

    private void tryKick(KickState tmp)
    {
        cext.mess(string.Concat(new object[] { "kicking #", tmp.name, ", ", tmp.getKickCount(), "/", (int)(PhotonNetwork.playerList.Length * 0.5f), "vote" }));
        if (tmp.getKickCount() >= ((int)(PhotonNetwork.playerList.Length * 0.5f)))
        {
            this.kickPhotonPlayer(tmp.name.ToString());
        }
    }

    public void unloadAssets()
    {
        if (!this.isUnloading)
        {
            this.isUnloading = true;
            base.StartCoroutine(this.unloadAssetsE(10f));
        }
    }

    public IEnumerator unloadAssetsE(float time)
    {
        yield return new WaitForSeconds(time);
        Resources.UnloadUnusedAssets();
        this.isUnloading = false;
        yield break;
    }
  
    public void unloadAssetsEditor()
    {
        if (!this.isUnloading)
        {
            this.isUnloading = true;
            base.StartCoroutine(this.unloadAssetsE(30f));
        }
    }

    private void Update()
    {
            deltaTime = (float)((DateTime.Now.Ticks - last_longT) * 1E-7);
            last_longT = DateTime.Now.Ticks;
        if (INC.isLogined)
        {
            if (LabelNetworkStatus != null)
            {
                if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE && PhotonNetwork.connected)
                {
                    string str = string.Empty;
                    if (LabelTopLeft)
                    {
                        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                        {
                            str = LangCore.lang.ping + PhotonNetwork.GetPing();
                        }
                        else
                        {
                            str = PhotonNetwork.connectionStatesDetailed.ToString() + " " + LangCore.lang.ping + PhotonNetwork.GetPing();
                        }
                        LabelNetworkStatus.text = (PhotonNetwork.offlineMode ? LangCore.lang.offline_mode : str) + fps;
                    }
                    else
                    {
                         LabelNetworkStatus.text = str;
                    }
                  

                }
                else if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                {
                    if (LabelTopLeft)
                    {
                        LabelNetworkStatus.text = fps;
                    }
                    else
                    {
                        LabelNetworkStatus.text = "";
                    }
                }
            }
            if (this.gameStart)
            {

                if (IN_GAME_MAIN_CAMERA.instance != null)
                {
                    IN_GAME_MAIN_CAMERA.instance.snapShotUpdate();
                }
                foreach (HERO hero in this.heroes)
                {
                    hero.update2();
                    if (LavaMode && PhotonNetwork.isMasterClient && !hero.isInvincible() && !hero.HasDied())
                    {
                        if (hero.transform.position.y < 0.5f)
                        {
                            hero.photonView.RPC("netDie2", PhotonTargets.All, new object[] { -1, "LAVA MODE" });
                        }
                    }
                }
                foreach (Bullet bullet in this.hooks)
                {
                    bullet.update();
                }
                foreach (TITAN_EREN titan_eren in this.eT)
                {
                    titan_eren.update();
                }
                foreach (TITAN titan in this.titans)
                {
                    titan.update2();
                }
                foreach (FEMALE_TITAN female_titan in this.fT)
                {
                    female_titan.update();
                }
                foreach (COLOSSAL_TITAN colossal_titan in this.cT)
                {
                    colossal_titan.update2();
                }
                if (IN_GAME_MAIN_CAMERA.instance.isOn)
                {
                    IN_GAME_MAIN_CAMERA.instance.update2();
                }
            }
            FPSCount();
        }
    }

    private void FPSCount()
    {
        if (showFPS)
        {
            fpstime -= (double)Time.deltaTime;
            fpsaccum += (double)(Time.timeScale / Time.deltaTime);
            fpsframes++;
            if (fpstime <= 0.0)
            {
                fpsfps = fpsaccum / (double)fpsframes;
                fpstime = fpsinterval;
                fpsaccum = 0.0;
                fpsframes = 0;
            }

        }
        else
        {
            fpstime = fpsinterval;
        }
    }
    [RPC]
    private void updateKillInfo(bool t1, string killer, bool t2, string victim, int dmg)
    {
        KillInfoComponent killin1;

        KillInfoComponent obj4 = (KillInfoComponent)UnityEngine.Object.Instantiate(Cach.UI_KillInfo != null ? Cach.UI_KillInfo : Cach.UI_KillInfo = ((GameObject)Resources.Load("UI/KillInfo")).GetComponent<KillInfoComponent>());
        for (int i = 0; i < this.killinfo.Count; i++)
        {
            killin1 = this.killinfo[i];
            if (killin1 != null)
            {
                killin1.moveOn();
            }
        }
        if (this.killinfo.Count > 4)
        {
            killin1 = this.killinfo[0];
            if (killin1 != null)
            {
                killin1.destory();
            }
            this.killinfo.RemoveAt(0);
        }
        obj4.transform.parent = uiT.panels[0].transform;
        obj4.show(t1, killer, t2, victim, dmg);
        this.killinfo.Add(obj4);
        if (((int)settings[0xf4]) == 1)
        {
            string str = "(" + this.roundTime.ToString("F2") + ") " + killer.toHex() + " killed ";
            string newLine = str + victim.toHex() + " for " + dmg.ToString() + " damage.";
            InRoomChat.instance.addLINE(newLine);
        }
    }

    [RPC]
    public void verifyPlayerHasLeft(int ID, PhotonMessageInfo info)
    {
        if (ID < 0)
        {
            info.sender.CM = true;
            string ver = ID.ToString().Replace("-","");
            UnityEngine.Debug.Log(ver);
            info.sender.version = "0."+  ver[0] + "." + ver[1] + "." + ver[2];
        }
        if (info.sender.isMasterClient && (PhotonPlayer.Find(ID) != null))
        {
            PhotonPlayer player = PhotonPlayer.Find(ID);
            string str = string.Empty;
            str = (player.name2);
            banHash.Add(ID, str);
        }
    }

    public IEnumerator WaitAndRecompilePlayerList(float time)
    {
        yield return new WaitForSeconds(time);
        if (RCSettings.teamMode != 0)
        {
            int num;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            int num6 = 0;
            int num7 = 0;
            int num8 = 0;
            int num9 = 0;
            Dictionary<int, PhotonPlayer> dictionary = new Dictionary<int, PhotonPlayer>();
            Dictionary<int, PhotonPlayer> dictionary2 = new Dictionary<int, PhotonPlayer>();
            Dictionary<int, PhotonPlayer> dictionary3 = new Dictionary<int, PhotonPlayer>();
            foreach (PhotonPlayer player2 in PhotonNetwork.playerList)
            {
                if (!ignoreList.Contains(player2.ID))
                {
                    switch ((player2.RCteam))
                    {
                        case 0:
                            dictionary3.Add(player2.ID, player2);
                            break;

                        case 1:
                            dictionary.Add(player2.ID, player2);
                            num2 += (player2.kills);
                            num4 += (player2.deaths);
                            num6 += (player2.max_dmg);
                            num8 += (player2.total_dmg);
                            break;

                        case 2:
                            dictionary2.Add(player2.ID, player2);
                            num3 += (player2.kills);
                            num5 += (player2.deaths);
                            num7 += (player2.max_dmg);
                            num9 += (player2.total_dmg);
                            break;
                    }
                }
            }
            this.cyanKills = num2;
            this.magentaKills = num3;
            if (PhotonNetwork.isMasterClient)
            {
                if (RCSettings.teamMode == 2)
                {
                    foreach (PhotonPlayer player3 in PhotonNetwork.playerList)
                    {
                        int num11 = 0;
                        if (dictionary.Count > (dictionary2.Count + 1))
                        {
                            num11 = 2;
                            if (dictionary.ContainsKey(player3.ID))
                            {
                                dictionary.Remove(player3.ID);
                            }
                            if (!dictionary2.ContainsKey(player3.ID))
                            {
                                dictionary2.Add(player3.ID, player3);
                            }
                        }
                        else if (dictionary2.Count > (dictionary.Count + 1))
                        {
                            num11 = 1;
                            if (!dictionary.ContainsKey(player3.ID))
                            {
                                dictionary.Add(player3.ID, player3);
                            }
                            if (dictionary2.ContainsKey(player3.ID))
                            {
                                dictionary2.Remove(player3.ID);
                            }
                        }
                        if (num11 > 0)
                        {
                            this.photonView.RPC("setTeamRPC", player3, new object[] { num11 });
                        }
                    }
                }
                else if (RCSettings.teamMode == 3)
                {
                    foreach (PhotonPlayer player4 in PhotonNetwork.playerList)
                    {
                        int num12 = 0;
                        num = (player4.RCteam);
                        if (num > 0)
                        {
                            switch (num)
                            {
                                case 1:
                                    {
                                        int num13 = 0;
                                        num13 = (player4.kills);
                                        if (((num3 + num13) + 7) < (num2 - num13))
                                        {
                                            num12 = 2;
                                            num3 += num13;
                                            num2 -= num13;
                                        }
                                        break;
                                    }
                                case 2:
                                    {
                                        int num14 = 0;
                                        num14 = (player4.kills);
                                        if (((num2 + num14) + 7) < (num3 - num14))
                                        {
                                            num12 = 1;
                                            num2 += num14;
                                            num3 -= num14;
                                        }
                                        break;
                                    }
                            }
                            if (num12 > 0)
                            {
                                this.photonView.RPC("setTeamRPC", player4, new object[] { num12 });
                            }
                        }
                    }
                }
            }

            string team_cyan = "[00FFFF]TEAM CYAN[ffffff]:" + this.cyanKills + "/" + num4 + "/" + num6 + "/" + num8 + "\n";
            string team_magneta = " \n[FF00FF]TEAM MAGENTA[ffffff]:" + this.magentaKills + "/" + num5 + "/" + num7 + "/" + num9 + "\n";
            string team_invidual = " \n[00FF00]INDIVIDUAL\n";
            foreach (PhotonPlayer player5 in PhotonNetwork.playerList)
            {
                int num342 = player5.RCteam;
                if (num342 == 1)
                {
                    team_cyan = team_cyan + i_player_1(player5);
                }
                if (num342 == 2)
                {
                    team_magneta = team_magneta + i_player_1(player5);
                }
                if (num342 == 0)
                {
                    team_invidual = team_invidual + i_player_1(player5);
                }
            }
            this.playerList = team_cyan + team_magneta + team_invidual;
        }
        else
        {
            if (LabelTopLeft)
            {
                string Pliist = string.Empty;
                foreach (PhotonPlayer player in PhotonNetwork.playerList)
                {
                    Pliist = Pliist + i_player_1(player);
                }
                this.playerList = Pliist;
            }
        }


        if ((PhotonNetwork.isMasterClient && !this.isWinning) && (!this.isLosing && (this.roundTime >= 5f)))
        {
            int num15;
            if (RCSettings.infectionMode > 0)
            {
                int num16 = 0;
                for (num15 = 0; num15 < PhotonNetwork.playerList.Length; num15++)
                {
                    PhotonPlayer targetPlayer = PhotonNetwork.playerList[num15];
                    if (!ignoreList.Contains(targetPlayer.ID))
                    {
                        if ((targetPlayer.isTitan) == 1)
                        {
                            if ((targetPlayer.dead) && ((targetPlayer.deaths) > 0))
                            {
                                if (!imatitan.ContainsKey(targetPlayer.ID))
                                {
                                    imatitan.Add(targetPlayer.ID, 2);
                                }
                                targetPlayer.isTitan = 2;
                                this.photonView.RPC("spawnTitanRPC", targetPlayer, new object[0]);
                            }
                            else if (imatitan.ContainsKey(targetPlayer.ID))
                            {
                                for (int j = 0; j < this.heroes.Count; j++)
                                {
                                    HERO hero = (HERO)this.heroes[j];
                                    if (hero.photonView.owner == targetPlayer)
                                    {
                                        hero.markDie();
                                        hero.photonView.RPC("netDie2", PhotonTargets.All, new object[] { -1, "noswitchingfagt" });
                                    }
                                }
                            }
                        }
                        else if (((targetPlayer.isTitan) == 2) && !(targetPlayer.dead))
                        {
                            num16++;
                        }
                    }
                }
                if ((num16 <= 0) && (IN_GAME_MAIN_CAMERA.gamemode != GAMEMODE.KILL_TITAN))
                {
                    this.gameWin();
                }
            }
            else if (RCSettings.pointMode > 0)
            {
                if (RCSettings.teamMode > 0)
                {
                    if (this.cyanKills >= RCSettings.pointMode)
                    {
                        TeamCyanWins();
                    }
                    else if (this.magentaKills >= RCSettings.pointMode)
                    {
                        TeamMagnetaWins();
                    }
                }
                else if (RCSettings.teamMode == 0)
                {
                    for (num15 = 0; num15 < PhotonNetwork.playerList.Length; num15++)
                    {
                        PhotonPlayer player9 = PhotonNetwork.playerList[num15];
                        if ((player9.kills) >= RCSettings.pointMode)
                        {
                            cext.mess(player9.ishexname + INC.la("winner_player"));
                            this.gameWin();
                        }
                    }
                }
            }
            else if ((RCSettings.pointMode <= 0) && ((RCSettings.bombMode == 1) || (RCSettings.pvpMode > 0)))
            {
                if ((RCSettings.teamMode > 0) && (PhotonNetwork.playerList.Length > 1))
                {
                    int num18 = 0;
                    int num19 = 0;
                    int num20 = 0;
                    int num21 = 0;
                    for (num15 = 0; num15 < PhotonNetwork.playerList.Length; num15++)
                    {
                        PhotonPlayer player10 = PhotonNetwork.playerList[num15];
                        if (!ignoreList.Contains(player10.ID))
                        {
                            if ((player10.RCteam) == 1)
                            {
                                num20++;
                                if (!(player10.dead))
                                {
                                    num18++;
                                }
                            }
                            else if ((player10.RCteam) == 2)
                            {
                                num21++;
                                if (!(player10.dead))
                                {
                                    num19++;
                                }
                            }
                        }
                    }
                    if ((num20 > 0) && (num21 > 0))
                    {
                        if (num18 == 0)
                        {
                            TeamMagnetaWins();
                        }
                        else if (num19 == 0)
                        {
                            TeamCyanWins();

                        }
                    }
                }
                else if ((RCSettings.teamMode == 0) && (PhotonNetwork.playerList.Length > 1))
                {
                    int num22 = 0;
                    PhotonPlayer player11 = null;
                    for (num15 = 0; num15 < PhotonNetwork.playerList.Length; num15++)
                    {
                        PhotonPlayer player12 = PhotonNetwork.playerList[num15];
                        if (!(player12.dead))
                        {
                            player11 = player12;
                            num22++;
                        }
                    }
                    if (num22 <= 1)
                    {

                        if (player11 != null)
                        {
                            for (num15 = 0; num15 < 5; num15++)
                            {
                                this.playerKillInfoUpdate(player11, 0);
                            }
                        }
                        TeamPlayerWins(player11);
                    }
                }
            }
        }
        if (LabelTopLeft)
        {
            this.ShowHUDInfoTopLeft(playerList.toHex());
        }
        this.isRecompiling = false;
        yield break;
    }

    void TeamPlayerWins(PhotonPlayer player)
    {
        if (player != null)
        {
            cext.mess(player.ishexname + INC.la("5_points_added"));
            this.gameWin();
            return;
        }
        cext.mess(INC.la("nobody_wins"));
        this.gameWin();
    }

    void TeamCyanWins()
    {
        cext.mess(INC.la("team_cyan_wins"));
        this.gameWin();
    }

    void TeamMagnetaWins()
    {
        cext.mess(INC.la("team_magenta_wins"));
        this.gameWin();
    }

    private string i_player_1(PhotonPlayer player5)
    {
        string text = string.Empty;
        int kills = player5.kills;
        int death = player5.deaths;
        int total = player5.total_dmg;
        int max_dmg = player5.max_dmg;
        string name = player5.ishexname;
        bool isDead = player5.dead;
        bool isTitan = player5.isTitan == 2;
        bool isAhss = player5.team == 2;

        if (ignoreList.Contains(player5.ID))
        {
            text = text + "[FF0000][X]";
        }
        if (player5.isLocal)
        {
            text = text + "[00FF00]";
        }
        else
        {
            text = text + "[00CC00]";
        }
        text = text + "[" + Convert.ToString(player5.ID) + "]";
        if (player5.isMasterClient)
        {
            text = text + "[M]";
        }
        if (isDead)
        {
            text = text + "[" + ColorSet.color_red + "]*dead*[-]";
        }
        if (isTitan)
        {
            text = text + "[" + ColorSet.color_titan_player + "]<T>";
        }
        else
        {
            if (isAhss)
            {
                text = text + "[" + ColorSet.color_human_1 + "]<A>";
            }
            else
            {
                text = text + "[" + ColorSet.color_human + "]<H>";
            }
        }
        return text + name + "[FFFFFF]:" + kills + "/" + death + "/" + max_dmg + "/" + total + "\n";
    }

    public IEnumerator WaitAndReloadKDR(PhotonPlayer player)
    {
        yield return new WaitForSeconds(5f);
        string key = (player.name2);
        if (this.PreservedPlayerKDR.ContainsKey(key))
        {
            int[] numArray = this.PreservedPlayerKDR[key];
            this.PreservedPlayerKDR.Remove(key);
            player.kills = numArray[0];
            player.deaths = numArray[1];
            player.max_dmg = numArray[2];
            player.total_dmg = numArray[3];
        }
    }

    public IEnumerator WaitAndResetRestarts()
    {
        yield return new WaitForSeconds(10f);
        this.restartingBomb = false;
        this.restartingEren = false;
        this.restartingHorse = false;
        this.restartingMC = false;
        this.restartingTitan = false;
    }

    public IEnumerator WaitAndRespawn1(float time, string str)
    {
        yield return new WaitForSeconds(time);
        this.SpawnPlayer(this.myLastHero, str);
    }

    public IEnumerator WaitAndRespawn2(float time, GameObject pos)
    {
        yield return new WaitForSeconds(time);
        this.SpawnPlayerAt2(this.myLastHero, pos);
    }

    public new string name { get; set; }
    private enum LoginStates
    {
        notlogged,
        loggingin,
        loginfailed,
        loggedin
    }
    public enum BuildType : byte
    {
        Dev = 1,
        Beta = 2,
        Relase = 3,
        Stable = 4,
        Other = 255
    }
}

