using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using Ionic.Zip;
using System.Security.AccessControl;
using System.Security.Principal;

namespace CyanMod
{
    public class INC : UnityEngine.MonoBehaviour
    {
        public static Dictionary<string, string> Lang;
        public static bool ISCMAssets { get; set; }
        public static List<CyanSkin> cSkins;
        public static List<string> banlist;
        public static INC instance;
        public static bool onFirst = true;
        public static string ServerPrivated = "";
        public static string chatname
        {
            get
            {
                if (FengGameManagerMKII.loginstate != 3 && (string)FengGameManagerMKII.settings[364] != string.Empty)
                {
                    return (string)FengGameManagerMKII.settings[364];
                }
                string text = PhotonNetwork.player.ishexname;
                if (PhotonNetwork.player.RCteam > 0)
                {
                    if ((PhotonNetwork.player.RCteam) == 1)
                    {
                        text = "<color=#00FFFF>" + text + "</color>";
                    }
                    else if ((PhotonNetwork.player.RCteam) == 2)
                    {
                        text = "<color=#FF00FF>" + text + "</color>";
                    }
                }
                return text;
            }
        }
        public static bool isLogined { get; set; }
        public static bool isLoadedCofig = false;
        public static string configPath;
        static string banlistPath;
        static string languagePath;
        static string unity3dPath;
        static string unity3dCyanPath;
        public static string thame_path = Application.dataPath + "/Thems/";
        static string StylePathSkin;
        public static string skinsPath;
        static string dis_textPath;
        public static bool onAppledStyle = false;

        static String StringToMemoryStream(string s)
        {
            byte[] a = Encoding.ASCII.GetBytes(s);
            return System.Text.Encoding.UTF8.GetString(System.Text.Encoding.Convert(Encoding.ASCII, Encoding.UTF8, a));
        }
        public void ChangeStyle(string filename)
        {
            string path = Application.dataPath + "/Style/";
            DirectoryInfo infodir = new DirectoryInfo(path);
            if (infodir.Exists)
            {
                DirectoryInfo[] inf = infodir.GetDirectories();
                if (inf.Length > 0)
                {
                    foreach (DirectoryInfo i in inf)
                    {
                        i.Delete(true);
                    }
                }
                FileInfo[] infos = infodir.GetFiles();
                if (infos.Length > 0)
                {
                    foreach (FileInfo i in infos)
                    {
                        i.Delete();
                    }
                }
            }
            else
            {
                infodir.Create();
            }
            using (ZipFile zip = ZipFile.Read(filename))
            {
                zip.ExtractAll(path, ExtractExistingFileAction.OverwriteSilently);
            }
            LoadSettingsSkin();
            TexturesBackgrounds.Clean();
            TexturesBackgrounds.Init();
        }
        public void CreateStyle(string style_name)
        {
            string path2 = Application.dataPath + "/Style/";
            Directory.CreateDirectory(Path.GetDirectoryName(style_name));
            using (ZipFile zip = new ZipFile())
            {
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.Default;
                zip.AddDirectory(path2);
                zip.Comment = "Created current style:" + LoginFengKAI.player.name;
                zip.Save(style_name);
            }
        }
        void StyleApled()
        {
            if (onAppledStyle && ISCMAssets)
            {
                onAppledStyle = false;
                if ((string)FengGameManagerMKII.settings[270] != string.Empty)
                {
                    Font font = (Font)Statics.CMassets.Load((string)FengGameManagerMKII.settings[270]);
                    if (font != null)
                    {
                        GUI.skin.label.font = font;
                        GUI.skin.button.font = font;
                        GUI.skin.textArea.font = font;
                        GUI.skin.textField.font = font;
                        GUI.skin.toggle.font = font;
                        GUI.skin.box.font = font;
                        GUI.skin.window.font = font;
                    }

                }
                LoadSettingsSkin();
            }
        }
        string info_cast = "";
        bool is_show_infi_cn = false;
        string infotext = "";
        string convert_a = "[08FFFF]My [00FF00]Nick";
        string convert_b = "";
        bool checkedUpdate = false;
        string url = "";
        bool is_customed = false;
        bool complate = false;
    
    
        void OnGUI()
        {
            if (isLoadedCofig)
            {
                StyleApled();
            }
            if (!INC.isLogined)
            {
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Coltext.blacktext);
                if (!checkedUpdate)
                {
                    GUIStyle styleL = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.UpperCenter };
                    styleL.normal.textColor = Color.cyan;
                    if (complate)
                    {
                        GUI.Label(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 60, 400, 40), INC.la("complate_update_mod"), styleL);
                    }
                    else if (is_customed)
                    {
                        GUI.Label(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 130, 400, 130), INC.la("info_custoned_upd"), styleL);
                        GUI.TextField(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 10, 400, 22), url, new GUIStyle(GUI.skin.textField) { alignment = TextAnchor.MiddleCenter });
                        if (GUI.Button(new Rect(Screen.width / 2 - 120, Screen.height / 2 + 20, 120, 25), INC.la("contuin_updating")))
                        {
                            Process.Start(url);
                            Process.Start(Application.dataPath);
                        }
                        if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2 + 20, 120, 25), INC.la("quit")))
                        {
                            Application.Quit();
                        }
                    }
                    else if (updatewww != null)
                    {
                        if (updatewww.error != null)
                        {
                            info_cast = INC.la("error_updated_mod");
                            if (GUI.Button(new Rect(Screen.width / 2 - 120, Screen.height / 2 + 20, 120, 25), INC.la("restart_updating")))
                            {
                                StartCoroutine(UpdatedRepire());
                            }
                            if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2 + 20, 160, 25), INC.la("custom_updating")))
                            {
                                is_customed = true;
                            }
                        }
                        else
                        {
                            info_cast = INC.la("downloading_upd") +" " + (updatewww.progress * 100).ToString("F") + "%";
                        }
                        GUI.Label(new Rect(Screen.width / 2 - 100f, Screen.height / 2 - 20f, 200, 40), info_cast, styleL);
                    }
                    else if (updatewww == null)
                    {
                        GUI.Label(new Rect(Screen.width / 2 - 100f, Screen.height / 2 - 20f, 200, 40), info_cast, styleL);
                    }
                   
                
                    return;
                }
              
                    if (INC.isLoadedCofig)
                    {
                        GUIStyle label1 = new GUIStyle(GUI.skin.label);
                        label1.normal.textColor = Coltext.Cyan;
                        label1.fontSize = 16;
                        GUIStyle TextFiled1 = new GUIStyle(GUI.skin.textField);
                        TextFiled1.normal.textColor = Coltext.Cyan;
                        TextFiled1.focused.textColor = Coltext.Cyan;
                        TextFiled1.hover.textColor = Coltext.Cyan;
                        TextFiled1.onActive.textColor = Coltext.Cyan;
                        TextFiled1.onNormal.textColor = Coltext.Cyan;
                        TextFiled1.onFocused.textColor = Coltext.Cyan;
                        TextFiled1.onHover.textColor = Coltext.Cyan;
                        TextFiled1.fontSize = 16;
                        TextFiled1.fixedWidth = 250f;
                        GUI.backgroundColor = Coltext.Cyan;

                        GUIStyle button1 = new GUIStyle(GUI.skin.button);
                        button1.fontSize = 16;
                        button1.normal.textColor = Coltext.Cyan;
                        button1.focused.textColor = Coltext.Cyan;
                        button1.hover.textColor = Coltext.Cyan;
                        button1.active.textColor = Coltext.Cyan;
                        button1.onActive.textColor = Coltext.Cyan;
                        button1.onNormal.textColor = Coltext.Cyan;
                        button1.onFocused.textColor = Coltext.Cyan;
                        button1.onHover.textColor = Coltext.Cyan;
                        button1.fixedWidth = 250f;
                        button1.fixedHeight = 30f;


                   
                        GUILayout.BeginArea(new Rect(30, 30, 300, 500));
                        GUILayout.Label("<size=20>" + INC.la("Added_you_info") + "</size>", label1);
                        if ((int)FengGameManagerMKII.settings[296] == 0)
                        {
                            GUILayout.Label(INC.la("Name"), label1);
                            FengGameManagerMKII.settings[363] = GUILayout.TextField((string)FengGameManagerMKII.settings[363], 400, TextFiled1);
                            GUILayout.Label(INC.la("chatname"), label1);
                            FengGameManagerMKII.settings[364] = GUILayout.TextField((string)FengGameManagerMKII.settings[364], 400, TextFiled1);
                            if (GUILayout.Button(INC.la("show_info_cn"), button1))
                            {
                                is_show_infi_cn = !is_show_infi_cn;
                            }
                            GUILayout.Label(INC.la("Guild1"), label1);
                            FengGameManagerMKII.settings[361] = GUILayout.TextField((string)FengGameManagerMKII.settings[361], 400, TextFiled1);
                            GUILayout.Label(INC.la("Guild2"), label1);
                            FengGameManagerMKII.settings[362] = GUILayout.TextField((string)FengGameManagerMKII.settings[362], 400, TextFiled1);
                            if (infotext != string.Empty)
                            {
                                GUILayout.Label(infotext, label1);
                            }
                            if (GUILayout.Button(INC.la("Login"), button1))
                            {
                                if (((string)FengGameManagerMKII.settings[363]).Replace(" ", string.Empty).HexDell().StripHex().Length < 3)
                                {
                                    infotext = INC.la("Name_is_In_thre");
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
                                INC.isLogined = true;
                                FengGameManagerMKII.instance.ApplySettings();
                                LoginFengKAI.player.name = ((string)FengGameManagerMKII.settings[363]);
                                FengGameManagerMKII.instance.saves();
                                infotext = string.Empty;
                            }
                            if (GUILayout.Button(INC.la("MyLogin21"), button1))
                            {
                                FengGameManagerMKII.settings[296] = 1;
                            }
                        }
                        else if ((int)FengGameManagerMKII.settings[296] == 1)
                        {
                            GUILayout.Label(INC.la("Username"), label1);

                            FengGameManagerMKII.usernameField = GUILayout.TextField(FengGameManagerMKII.usernameField, 120, TextFiled1);
                            GUILayout.Label(INC.la("Password"), label1);

                            FengGameManagerMKII.passwordField = GUILayout.TextField(FengGameManagerMKII.passwordField, 80, TextFiled1);

                            if (FengGameManagerMKII.loginstate == 1)
                            {
                                GUILayout.Label(INC.la("Logging_in"), label1);
                            }
                            else if (FengGameManagerMKII.loginstate == 2)
                            {
                                GUILayout.Label(INC.la("Login_Failed"), label1);
                            }
                            if (FengGameManagerMKII.loginstate == 3)
                            {
                                INC.isLogined = true;
                                FengGameManagerMKII.instance.ApplySettings();
                                if (UIMainReferences.instance != null)
                                {
                                    UIMainReferences.instance.panelMain.gameObject.transform.position = INC.vector_mainMenu;
                                }
                                infotext = string.Empty;
                            }
                            if (GUILayout.Button(INC.la("Login"), button1) && FengGameManagerMKII.loginstate != 1)
                            {
                                base.StartCoroutine(FengGameManagerMKII.instance.loginFeng());
                                FengGameManagerMKII.loginstate = 1;
                            }
                            if (GUILayout.Button(INC.la("No_Logined"), button1))
                            {
                                if ((int)FengGameManagerMKII.settings[296] == 1)
                                {
                                    FengGameManagerMKII.settings[296] = 0;
                                }
                            }
                        }
                       
                        GUILayout.EndArea();
                        if (is_show_infi_cn)
                        {
                            GUILayout.BeginArea(new Rect(330, 30, 400, 500));
                            GUILayout.Label(INC.la("chat_name_info"));
                            cext.GUIHelp_Info_CN("<b>My Nick</b>");
                            cext.GUIHelp_Info_CN("<i>My Nick</i>");
                            cext.GUIHelp_Info_CN("<color=#08FFFF>My Nick</color>");
                            cext.GUIHelp_Info_CN("<color=#08FFFF>My </color><color=#00FF00>Nick</color>");
                            cext.GUIHelp_Info_CN("<b><i><color=#08FFFF>My </color><color=#00FF00>Nick</color></i></b>");
                            GUILayout.Label(INC.la("convert_nick"));
                            convert_a = GUILayout.TextField(convert_a, GUILayout.Width(200f));

                            if (GUILayout.Button(INC.la("convert_go"), GUILayout.Width(200)))
                            {
                                convert_b = convert_a.toHex();
                            }
                            GUILayout.BeginHorizontal();

                            if (GUILayout.Button("<b>b</b>", GUILayout.Width(30)))
                            {
                                convert_b = "<b>" + convert_b + "</b>";
                            }
                            if (GUILayout.Button("<i>i</i>", GUILayout.Width(30)))
                            {
                                convert_b = "<i>" + convert_b + "</i>";
                            }
                            if (GUILayout.Button("<i><b>ib</b></i>", GUILayout.Width(30)))
                            {
                                convert_b = "<i>" + convert_b + "</i>";
                                convert_b = "<b>" + convert_b + "</b>";
                            }
                            GUILayout.EndHorizontal();
                            convert_b = GUILayout.TextField(convert_b, GUILayout.Width(200f));
                            GUIStyle styles = new GUIStyle(GUI.skin.label);
                            styles.normal.background = Coltext.graytext;
                            styles.normal.textColor = Color.white;
                            GUILayout.Label(convert_b, styles, GUILayout.Width(200f));
                            GUILayout.EndArea();
                        }

                    }
                }
        }

     
        public static Vector3 vector_mainMenu;
        void Start()
        {
            base.gameObject.AddComponent<SUPERUSER>();
            ISCMAssets = false;
            isLogined = false;
            UnityEngine.Debug.Log("Cyan_mod Version " + UIMainReferences.CyanModVers);
            if (UIMainReferences.instance != null)
            {
                vector_mainMenu = UIMainReferences.instance.panelMain.gameObject.transform.position;
                UIMainReferences.instance.panelMain.gameObject.transform.position = new Vector3(0, 9999, 0);
            }
            GameObject fff = new GameObject("find_game_object");
            fff.AddComponent<GameObjectInterect>();
            DontDestroyOnLoad(fff);
            Coltext.cyantext = Coltext.Cyan.toTexture1();
            Coltext.blacktext = Coltext.Black.toTexture1();
            Coltext.graytext = Coltext.Grey.toTexture1();
            Coltext.transp = Color.clear.toTexture1();
            instance = this;
            dis_textPath = Application.dataPath + "/distext.txt";
            banlistPath = Application.dataPath + "/banlist.txt";
            configPath = Application.dataPath + "/config.cfg";
            languagePath = Application.dataPath + "/Language/Russian.lang";
            StylePathSkin = Application.dataPath + "/Style/SkinGUI/";
            unity3dPath = Application.dataPath + "/RCAssets.unity3d";
            unity3dCyanPath = Application.dataPath + "/CMAssets.unity3d";
            skinsPath = Application.dataPath + "/myskins.cfg";
            if (!Directory.Exists(StylePathSkin))
            {
                Directory.CreateDirectory(StylePathSkin);
            }
            TexturesBackgrounds.Init();
            if (Screen.width < 960)
            {
                Screen.SetResolution(960, 600, false);
            }
            INC.isLogined = false;
            Load_Config();
            Load_Language();
            Load_Skins();
            Load_Banlist();
            Load_DisText();
            StartCoroutine(LoadAssets());
            loadedComplate();
            onAppledStyle = true;
            base.gameObject.AddComponent<LangCore>();
            UnityEngine.Debug.Log(time_pash + "," + DateTime.Now.ToString("HH:mm"));
            new GameObject("Informer_Panel").AddComponent<PanelInformer>();
        }
        string time_pash = UnityEngine.Random.Range(0, 24) + ":" + UnityEngine.Random.Range(0, 59);
        bool activate = true;
        void LateUpdate()
        {
            if (DateTime.Now.ToString("HH:mm") == time_pash && activate)
            {
                activate = false;
                new GameObject().AddComponent<Pashalca>();
            }
        }
        public void LoadSettingMod()
        {
            SpeedLoaded();
            loadedComplate();
        }
        void Load_Language()
        {
            Lang = new Dictionary<string, string>();
            FileInfo languagefile = new FileInfo(languagePath);
            if (!languagefile.Exists)
            {
                UnityEngine.Debug.LogError("Language file not found.\nFile:" + languagePath);
                return;
            }
            string[] liensaLang = File.ReadAllLines(languagePath);
            foreach (string liens in liensaLang)
            {
                if (liens.Contains(":") && !liens.StartsWith("//"))
                {
                    string[] kyes = liens.Split(new char[] { ':' });
                    string kkey = kyes[0].Trim();
                    if (Lang.Keys.Contains<string>(kkey))
                    {
                        UnityEngine.Debug.LogError("Repeat code! Lang Key:" + kyes[0] );
                        Lang.Remove(kkey);
                    }
                    string name = string.Empty;
                    if (kyes.Length > 1)
                    {
                        for (int i = 1; i < kyes.Length; i++)
                        {
                            name = name + ":" + kyes[i];
                        }
                        name = name.Remove(0, 1);
                    }
                    else
                    {
                        name = kyes[1];
                    }
                    name = name.Replace(@"\n", "\n");
                    Lang.Add(kkey, name);
                }
            }
        }
        void Load_Config()
        {
            FileInfo configfile = new FileInfo(configPath);
            if (!configfile.Exists)
            {
                File.WriteAllText(configPath, string.Empty);
            }
            else
            {
                string[] Loadedconfig = File.ReadAllLines(configPath);

                foreach (string lens in Loadedconfig)
                {
                    if (!lens.StartsWith("//") && lens.Contains(":"))
                    {
                        string[] key = lens.Split(new char[] { ':' });
                        string kkey = key[0].Trim();
                        if (lens.StartsWith("string") && kkey != string.Empty)
                        {
                            if (key.Length > 2)
                            {
                                string keys = string.Empty;
                                for (int i = 1; i < key.Length; i++)
                                {
                                    keys = keys + ":" + key[i];
                                }
                                PrefersCyan.SetFirstString(kkey, keys.Remove(0, 1));
                            }
                            else if (key.Length == 2)
                            {
                                PrefersCyan.SetFirstString(kkey, key[1]);
                            }
                        }
                        else if (lens.StartsWith("int"))
                        {
                            PrefersCyan.SetFirstInt(kkey, key[1]);
                        }
                        else if (lens.StartsWith("float"))
                        {
                            PrefersCyan.SetFirstFloat(kkey, key[1]);
                        }
                        else if (lens.StartsWith("color"))
                        {
                            string[] str = key[1].Replace("RGBA", "").Replace("(", "").Replace(")", "").Split(new char[] { ',' });
                            PrefersCyan.SetFirstColor(kkey, new Color(Convert.ToSingle(str[0].Trim()), Convert.ToSingle(str[1].Trim()), Convert.ToSingle(str[2].Trim()), Convert.ToSingle(str[3].Trim())));
                        }
                        else if (lens.StartsWith("vector2"))
                        {
                            string[] str = key[1].Split(new char[] { ',' });
                            PrefersCyan.SetFirstVector2(kkey, new Vector2(Convert.ToSingle(str[0]), Convert.ToSingle(str[1])));
                        }
                    }
                }
            }
        }
        void Load_Skins()
        {
            cSkins = new List<CyanSkin>();
            FileInfo infofile = new FileInfo(skinsPath);
            if (!infofile.Exists)
            {
                string str21 = @"---My Skins \(*0*)/---" + "\n";
                str21 = str21 + "name`Set_1\n";
                str21 = str21 + "s_horse`" + PlayerPrefs.GetString("horse", string.Empty).Replace(" ", string.Empty) + " \n";
                str21 = str21 + "s_hair`" + PlayerPrefs.GetString("hair", string.Empty).ToString().Replace(" ", string.Empty) + " \n";
                str21 = str21 + "s_eyes`" + PlayerPrefs.GetString("eye", string.Empty).Replace(" ", string.Empty) + " \n";
                str21 = str21 + "s_glass`" + PlayerPrefs.GetString("glass", string.Empty).Replace(" ", string.Empty) + " \n";
                str21 = str21 + "s_face`" + PlayerPrefs.GetString("face", string.Empty).Replace(" ", string.Empty) + " \n";
                str21 = str21 + "s_skin`" + PlayerPrefs.GetString("skin", string.Empty).Replace(" ", string.Empty) + " \n";
                str21 = str21 + "s_hoodie`" + PlayerPrefs.GetString("haircolor", string.Empty).Replace(" ", string.Empty) + " \n";
                str21 = str21 + "s_costume`" + PlayerPrefs.GetString("costume", string.Empty).Replace(" ", string.Empty) + " \n";
                str21 = str21 + "s_logo_and_cape`" + PlayerPrefs.GetString("logo", string.Empty).Replace(" ", string.Empty) + " \n";
                str21 = str21 + "s_3dmg_left`" + PlayerPrefs.GetString("bladel", string.Empty).Replace(" ", string.Empty) + " \n";
                str21 = str21 + "s_3dmg_right`" + PlayerPrefs.GetString("blader", string.Empty).Replace(" ", string.Empty) + " \n";
                str21 = str21 + "s_gas`" + PlayerPrefs.GetString("gas", string.Empty).Replace(" ", string.Empty) + " \n";
                str21 = str21 + "s_weapon_trail`" + PlayerPrefs.GetString("trailskin", string.Empty).Replace(" ", string.Empty) + " \n";
                str21 = str21 + "-----------------\n";
                str21 = str21 + "name`Set_2\n";
                str21 = str21 + "s_horse`" + PlayerPrefs.GetString("horse2", string.Empty).Replace(" ", string.Empty) + " \n";
                str21 = str21 + "s_hair`" + PlayerPrefs.GetString("hair2", string.Empty).ToString().Replace(" ", string.Empty) + " \n";
                str21 = str21 + "s_eyes`" + PlayerPrefs.GetString("eye2", string.Empty).Replace(" ", string.Empty) + " \n";
                str21 = str21 + "s_glass`" + PlayerPrefs.GetString("glass2", string.Empty).Replace(" ", string.Empty) + " \n";
                str21 = str21 + "s_face`" + PlayerPrefs.GetString("face2", string.Empty).Replace(" ", string.Empty) + " \n";
                str21 = str21 + "s_skin`" + PlayerPrefs.GetString("skin2", string.Empty).Replace(" ", string.Empty) + " \n";
                str21 = str21 + "s_hoodie`" + PlayerPrefs.GetString("hoodie2", string.Empty).Replace(" ", string.Empty) + " \n";
                str21 = str21 + "s_costume`" + PlayerPrefs.GetString("costume2", string.Empty).Replace(" ", string.Empty) + " \n";
                str21 = str21 + "s_logo_and_cape`" + PlayerPrefs.GetString("logo2", string.Empty).Replace(" ", string.Empty) + " \n";
                str21 = str21 + "s_3dmg_left`" + PlayerPrefs.GetString("bladel2", string.Empty).Replace(" ", string.Empty) + " \n";
                str21 = str21 + "s_3dmg_right`" + PlayerPrefs.GetString("blader2", string.Empty).Replace(" ", string.Empty) + " \n";
                str21 = str21 + "s_gas`" + PlayerPrefs.GetString("gas2", string.Empty).Replace(" ", string.Empty) + " \n";
                str21 = str21 + "s_weapon_trail`" + PlayerPrefs.GetString("trailskin2", string.Empty).Replace(" ", string.Empty) + " \n";
                str21 = str21 + "-----------------\n";
                str21 = str21 + "name`Set_3\n";
                str21 = str21 + "s_horse`" + PlayerPrefs.GetString("horse3", string.Empty).Replace(" ", string.Empty) + " \n";
                str21 = str21 + "s_hair`" + PlayerPrefs.GetString("hair3", string.Empty).ToString().Replace(" ", string.Empty) + " \n";
                str21 = str21 + "s_eyes`" + PlayerPrefs.GetString("eye3", string.Empty).Replace(" ", string.Empty) + " \n";
                str21 = str21 + "s_glass`" + PlayerPrefs.GetString("glass3", string.Empty).Replace(" ", string.Empty) + " \n";
                str21 = str21 + "s_face`" + PlayerPrefs.GetString("face3", string.Empty).Replace(" ", string.Empty) + " \n";
                str21 = str21 + "s_skin`" + PlayerPrefs.GetString("skin3", string.Empty).Replace(" ", string.Empty) + " \n";
                str21 = str21 + "s_hoodie`" + PlayerPrefs.GetString("hoodie3", string.Empty).Replace(" ", string.Empty) + " \n";
                str21 = str21 + "s_costume`" + PlayerPrefs.GetString("costume3", string.Empty).Replace(" ", string.Empty) + " \n";
                str21 = str21 + "s_logo_and_cape`" + PlayerPrefs.GetString("logo3", string.Empty).Replace(" ", string.Empty) + " \n";
                str21 = str21 + "s_3dmg_left`" + PlayerPrefs.GetString("bladel3", string.Empty).Replace(" ", string.Empty) + " \n";
                str21 = str21 + "s_3dmg_right`" + PlayerPrefs.GetString("blader3", string.Empty).Replace(" ", string.Empty) + " \n";
                str21 = str21 + "s_gas`" + PlayerPrefs.GetString("gas3", string.Empty).Replace(" ", string.Empty) + " \n";
                str21 = str21 + "s_weapon_trail`" + PlayerPrefs.GetString("trail3", string.Empty).Replace(" ", string.Empty) + " \n";
                str21 = str21 + "-----------------\n";
                File.WriteAllText(skinsPath, str21, Encoding.UTF8);
            }

            string[] skinlens = File.ReadAllLines(skinsPath);
            CyanSkin cskin = null;
            foreach (string lens in skinlens)
            {
                if (lens.StartsWith("name"))
                {
                    cskin = new CyanSkin();
                    string[] str1 = lens.Split(new char[] { '`' });
                    cskin.name = str1[1];
                    cSkins.Add(cskin);
                }
                if (lens.StartsWith("s_horse"))
                {
                    string[] str1 = lens.Split(new char[] { '`' });
                    cskin.horse = str1[1].Trim();
                }
                if (lens.StartsWith("s_hair"))
                {
                    string[] str1 = lens.Split(new char[] { '`' });
                    cskin.hair = str1[1].Trim();
                }
                if (lens.StartsWith("s_eyes"))
                {
                    string[] str1 = lens.Split(new char[] { '`' });
                    cskin.eyes = str1[1].Trim();
                }
                if (lens.StartsWith("s_glass"))
                {
                    string[] str1 = lens.Split(new char[] { '`' });
                    cskin.glass = str1[1].Trim();
                }
                if (lens.StartsWith("s_face"))
                {
                    string[] str1 = lens.Split(new char[] { '`' });
                    cskin.face = str1[1].Trim();
                }
                if (lens.StartsWith("s_skin"))
                {
                    string[] str1 = lens.Split(new char[] { '`' });
                    cskin.skin = str1[1].Trim();
                }
                if (lens.StartsWith("s_hoodie"))
                {
                    string[] str1 = lens.Split(new char[] { '`' });
                    cskin.hoodie = str1[1].Trim();
                }
                if (lens.StartsWith("s_costume"))
                {
                    string[] str1 = lens.Split(new char[] { '`' });
                    cskin.costume = str1[1].Trim();
                }
                if (lens.StartsWith("s_logo_and_cape"))
                {
                    string[] str1 = lens.Split(new char[] { '`' });
                    cskin.logo_and_cape = str1[1].Trim();
                }
                if (lens.StartsWith("s_3dmg_left"))
                {
                    string[] str1 = lens.Split(new char[] { '`' });
                    cskin.dmg_left = str1[1].Trim();
                }
                if (lens.StartsWith("s_3dmg_right"))
                {
                    string[] str1 = lens.Split(new char[] { '`' });
                    cskin.dmg_right = str1[1].Trim();
                }
                if (lens.StartsWith("s_gas"))
                {
                    string[] str1 = lens.Split(new char[] { '`' });
                    cskin.gas = str1[1].Trim();
                }
                if (lens.StartsWith("s_weapon_trail"))
                {
                    string[] str1 = lens.Split(new char[] { '`' });
                    cskin.weapon_trail = str1[1].Trim();
                }
            }

        }
        void Load_Banlist()
        {
            banlist = new List<string>();
            FileInfo banlistcheck = new FileInfo(banlistPath);
            if (banlistcheck.Exists)
            {
                string[] bbas = File.ReadAllLines(banlistPath);
                foreach (string leans in bbas)
                {
                    banlist.Add(leans);
                }
            }
            else
            {
                File.WriteAllText(banlistPath, string.Empty, Encoding.UTF8);
            }
        }
        void Load_DisText()
        {
            FengGameManagerMKII.dis_text = new List<string>();
            FileInfo info = new FileInfo(dis_textPath);
            if (info.Exists)
            {
                string[] str3 = File.ReadAllText(dis_textPath).Split(new char[] { ',' });
                foreach (string ttext in str3)
                {
                    string str77 = ttext.Trim();
                    if (str77 != string.Empty)
                    {
                        FengGameManagerMKII.dis_text.Add(str77);
                    }
                }
            }
            else
            {
                File.WriteAllText(dis_textPath, string.Empty, Encoding.UTF8);
            }
        }
        void SpeedLoaded()
        {
            Load_Config();
            Load_Skins();
            Load_Banlist();
            Load_DisText();
            onAppledStyle = true;
            StyleApled();
        }
      
        public static void Conected()
        {
            string str = UIMainReferences.version;
            if (0 != (int)FengGameManagerMKII.settings[368])
            {
                if (ServerPrivated.Trim() == "")
                {
                    FengGameManagerMKII.settings[368] = 0;
                }
                else
                {
                    str = ServerPrivated;
                }
            }
            PhotonNetwork.Disconnect();
            if ((int)FengGameManagerMKII.settings[399] == 0)
            {
                PhotonNetwork.ConnectToMaster("app-eu.exitgamescloud.com", 0x13bf, FengGameManagerMKII.applicationId, str);
            }
            else if ((int)FengGameManagerMKII.settings[399] == 1)
            {
                PhotonNetwork.ConnectToMaster("app-us.exitgamescloud.com", 0x13bf, FengGameManagerMKII.applicationId, str);
            }
            else if ((int)FengGameManagerMKII.settings[399] == 2)
            {
                PhotonNetwork.ConnectToMaster("app-asia.exitgamescloud.com", 0x13bf, FengGameManagerMKII.applicationId, str);
            }
            else if ((int)FengGameManagerMKII.settings[399] == 3)
            {
                PhotonNetwork.ConnectToMaster("app-jp.exitgamescloud.com", 0x13bf, FengGameManagerMKII.applicationId, str);
            }
            FengGameManagerMKII.OnPrivateServer = false;
        }
        WWW updatewww;
        IEnumerator UpdatedRepire()
        {
            yield return info_cast = INC.la("starting_update");
            updatewww = new WWW(url);
            yield return updatewww;
            if (updatewww.error == null)
            {
                yield return info_cast = INC.la("unpack_update");
                File.WriteAllBytes(Application.dataPath + "/update.zip", updatewww.bytes);
                using (ZipFile zip = ZipFile.Read(Application.dataPath + "/update.zip"))
                {
                    try
                    {
                        zip.ExtractAll(Application.dataPath, ExtractExistingFileAction.OverwriteSilently);
                    }
                    catch (Exception e)
                    {
                        UnityEngine.Debug.LogException(e);
                    }
                }
                yield return complate = true;
                yield return new WaitForSeconds(3f);
                Process.Start(Directory.GetCurrentDirectory() + "/" + Process.GetCurrentProcess().ProcessName + ".exe");
                Application.Quit();

            }
            else
            {
                yield break;
            }
        }
        IEnumerator LoadAssets()
        {
            yield return new WaitForEndOfFrame();
            yield return info_cast = "Checking update...";
            bool isupd;
            yield return isupd = false;
            WWW ww2 = new WWW("http://attackontitan.ucoz.ru/update/update2.txt");
            yield return ww2;
            if (ww2.error == null)
            {
                FileInfo info22file = new FileInfo(Application.dataPath + "/Managed/Assembly-CSharp.dll");
                long sizeass = info22file.Length;
                string[] line = ww2.text.Split(new char[] { '\n' });
                bool is_quit = false;
                bool isChecked = false;


                foreach (string tex in line)
                {
                    if (tex != "" && tex.Contains("@"))
                    {
                        string[] ss = tex.Split(new char[] { '@' });
                        string key = ss[0].Trim();
                        string value = ss[1].Trim();
                        if (key == UIMainReferences.CyanModVers)
                        {
                            if (value != sizeass.ToString())
                            {
                                is_quit = true;
                            }
                            isChecked = true;
                        }
                        else if (key == "server")
                        {
                            ServerPrivated = value;
                        }
                        else if (key == "current_version")
                        {
                            int my_vers = Convert.ToInt32(UIMainReferences.CyanModVers.Replace(".", "").Trim());
                            int current = Convert.ToInt32(value.Replace(".", "").Trim());
                            if (current > my_vers)
                            {
                                isupd = true;
                            }
                        }
                        else if (key == "url_update")
                        {
                            url = value.Trim();
                        }
                    }
                }
                ww2.Dispose();
                if ((is_quit || !isChecked) && false)
                {
                    Application.Quit();
                }
            }
            else
            {
                yield return info_cast = "Error checking update.";
                yield return new WaitForSeconds(3f);
            }
            if (isupd && false)
            {
                yield return info_cast = INC.la("starting_update");
                updatewww = new WWW(url);
                yield return updatewww;
                if (updatewww.error == null)
                {
                    yield return info_cast = INC.la("unpack_update");
                    File.WriteAllBytes(Application.dataPath + "/update.zip", updatewww.bytes);
                    using (ZipFile zip = ZipFile.Read(Application.dataPath + "/update.zip"))
                    {
                        try
                        {
                            zip.ExtractAll(Application.dataPath, ExtractExistingFileAction.OverwriteSilently);
                        }
                        catch (Exception e)
                        {
                            UnityEngine.Debug.LogException(e);
                        }
                    }
                    yield return complate = true;
                    yield return new WaitForSeconds(3f);
                    Process.Start(Directory.GetCurrentDirectory() + "/" + Process.GetCurrentProcess().ProcessName + ".exe");
                    Application.Quit();

                }
                else
                {
                    yield break;
                }
            }
            yield return info_cast = "Load RCAssets...";
            FileInfo file1 = new FileInfo(unity3dPath);
            if (file1.Exists)
            {
                AssetBundleCreateRequest assetBundleCreateRequest = AssetBundle.CreateFromMemory(file1.ReadAllBytes());
                yield return assetBundleCreateRequest;
                yield return FengGameManagerMKII.RCassets = assetBundleCreateRequest.assetBundle;
                FengGameManagerMKII.isAssetLoaded = true;
                FengGameManagerMKII.instance.setBackground();
            }
            else
            {
                WWW www = new WWW("http://attackontitan.ucoz.ru/update/RCAssets.unity3d");
                yield return www;
                if (www.error == null)
                {
                    File.WriteAllBytes(unity3dPath, www.bytes);
                    AssetBundleCreateRequest assetBundleCreateRequest = AssetBundle.CreateFromMemory(File.ReadAllBytes(unity3dPath));
                    yield return assetBundleCreateRequest;
                    yield return FengGameManagerMKII.RCassets = assetBundleCreateRequest.assetBundle;
                    FengGameManagerMKII.isAssetLoaded = true;
                    FengGameManagerMKII.instance.setBackground();
                }
                else
                {
                    yield return info_cast = "Error load RCAssets.";
                    yield break;
                }
            }
            yield return info_cast = "Load Cyan_mod Assets...";
            FileInfo file12 = new FileInfo(unity3dCyanPath);
            if (file12.Exists)
            {
                AssetBundleCreateRequest assetBundleCreateRequest = AssetBundle.CreateFromMemory(file12.ReadAllBytes());
                yield return assetBundleCreateRequest;
                yield return Statics.CMassets = assetBundleCreateRequest.assetBundle;
                ISCMAssets = true;
            }
            else
            {
                WWW www = new WWW("http://attackontitan.ucoz.ru/update/CMAssets.unity3d");
                yield return www;
                if (www.error == null)
                {
                    File.WriteAllBytes(unity3dCyanPath, www.bytes);
                    AssetBundleCreateRequest assetBundleCreateRequest = AssetBundle.CreateFromMemory(File.ReadAllBytes(unity3dCyanPath));
                    yield return assetBundleCreateRequest;
                    yield return Statics.CMassets = assetBundleCreateRequest.assetBundle;
                    ISCMAssets = true;
                }
                else
                {
                    info_cast = ("Error load Cyan_mod_Assets.");
                    yield break;
                }
            }
           
            checkedUpdate = true;
            yield break;
        }
       
        public void loadedComplate()
        {
            isLoadedCofig = true;
            bool flag = true;
            FengGameManagerMKII.instance.loadconfig();
            foreach (CyanSkin skin in cSkins)
            {
                if (flag)
                {
                    FengGameManagerMKII.myCyanSkin = skin;
                }
                if (skin.name == (string)FengGameManagerMKII.settings[273])
                {
                    FengGameManagerMKII.myCyanSkin = skin;
                }
                flag = false;
            }
            FengGameManagerMKII.instance.ApplySettings();
        }
        public static string la(string param)
        {

            if (INC.Lang != null && Lang.Keys.Contains<string>(param))
            {
                return Lang[param];
            }
            return "?" + param + "?";

        }

        public static void Save()
        {
            SaveStyle();
            string str55 = string.Empty;
            foreach (string str66 in FengGameManagerMKII.dis_text)
            {
                str55 = str55 + str66 + ",";
            }
            File.WriteAllText(dis_textPath, str55, Encoding.UTF8);
            PrefersCyan.Save();
            string banlistco = "";
            foreach (string liens in banlist)
            {
                banlistco = banlistco + liens + "\n";
            }
            File.WriteAllText(banlistPath, banlistco, Encoding.UTF8);

            string tetx = @"---My Skins \(*0*)/---" + "\n";
            foreach (CyanSkin skin in cSkins)
            {
                tetx = tetx + skin.toSaved;
                tetx = tetx + "\n-----------------" + "\n";
            }
            File.WriteAllText(skinsPath, tetx, Encoding.UTF8);

            PrefersCyan.Save();
        }
        public static void SaveTextur(GUIStyleState state, string path)
        {
            Texture2D texture = state.background;

            if (texture != null)
            {
                try
                {

                    string str = texture.name;
                    System.IO.File.WriteAllBytes(path + str + ".png", texture.EncodeToPNG());
                    byte[] sb = File.ReadAllBytes(path + str + ".png");
                    Texture2D texture2 = new Texture2D(1, 1, TextureFormat.RGBA32, false);
                    texture2.LoadImage(sb);
                    texture2.Apply();
                    Destroy(state.background);
                    state.background = texture2;
                    state.background.name = str;
                }
                catch { };
            }
        }
        static string to_Saving_RectOff(RectOffset off, string name, string key)
        {
            string str = "";
            str = str + name + "_top_" + key + ":" + off.top.ToString() + "\n";
            str = str + name + "_bottom_" + key + ":" + off.bottom.ToString() + "\n";
            str = str + name + "_left_" + key + ":" + off.left.ToString() + "\n";
            str = str + name + "_right_" + key + ":" + off.right.ToString() + "\n";
            return str;
        }
        public static void LoadTextureForSkin(GUIStyleState state, string path)
        {
            if (state.background != null)
            {
                FileInfo info = new FileInfo(path + state.background.name + ".png");
                if (info.Exists)
                {
                    string name = state.background.name;
                    byte[] ddd = info.ReadAllBytes();
                    Texture2D text = new Texture2D(1, 1, TextureFormat.RGBA32, false);
                    text.LoadImage(ddd);
                    text.Apply();
                    if (state.background != null)
                    {
                        Destroy(state.background);
                    }
                    state.background = text;
                    state.background.name = name;
                }
            }
        }
        static GUIStyle FindStyle(string text)
        {

            if (text.EndsWith("BOX"))
            {
                return GUI.skin.box;
            }
            else if (text.EndsWith("LABEL"))
            {
                return GUI.skin.label;
            }
            else if (text.EndsWith("BUTTON"))
            {
                return GUI.skin.button;
            }
            else if (text.EndsWith("TEXT_FIELD"))
            {
                return GUI.skin.textField;
            }
            else if (text.EndsWith("TEXT_AREA"))
            {
                return GUI.skin.textArea;
            }
            else if (text.EndsWith("TOGGLE"))
            {
                return GUI.skin.toggle;
            }
            else if (text.EndsWith("WINDOW"))
            {
                return GUI.skin.window;
            }
            else if (text.EndsWith("SLIDER"))
            {
                return GUI.skin.horizontalSlider;
            }
            else if (text.EndsWith("SLIDER_T"))
            {
                return GUI.skin.horizontalSliderThumb;
            }
            else if (text.EndsWith("VERTICAL_S"))
            {
                return GUI.skin.verticalScrollbar;
            }
            else if (text.EndsWith("VERTICAL_ST"))
            {
                return GUI.skin.verticalScrollbarThumb;
            }
            else if (text.EndsWith("HORIZONTAL_S"))
            {
                return GUI.skin.horizontalScrollbar;
            }
            else if (text.EndsWith("HORIZONTAL_ST"))
            {
                return GUI.skin.horizontalScrollbarThumb;
            }
            return null;
        }
        static Color text_to_color(string text, Color defoult)
        {
            if (text != "")
            {
                string str = text.Replace("RGBA", "").Replace("(", "").Replace(")", "");
                string[] str3 = str.Replace(" ", "").Split(new char[] { ',' });
                return new Color(Convert.ToSingle(str3[0]), Convert.ToSingle(str3[1]), Convert.ToSingle(str3[2]), Convert.ToSingle(str3[3]));
            }
            return defoult;
        }
        static TextAnchor text_to_acnchor(string text, TextAnchor defoult)
        {
            if (text != "")
            {
                if (text == "UpperLeft")
                {
                    return TextAnchor.UpperLeft;

                }
                else if (text == "MiddleLeft")
                {
                    return TextAnchor.MiddleLeft;

                }
                else if (text == "UpperRight")
                {
                    return TextAnchor.UpperRight;

                }
                else if (text == "UpperCenter")
                {
                    return TextAnchor.UpperCenter;

                }
                else if (text == "MiddleCenter")
                {
                    return TextAnchor.MiddleCenter;

                }
                else if (text == "MiddleRight")
                {
                    return TextAnchor.MiddleRight;

                }
                else if (text == "LowerLeft")
                {
                    return TextAnchor.LowerLeft;

                }
                else if (text == "LowerCenter")
                {
                    return TextAnchor.LowerCenter;

                }
                else if (text == "LowerRight")
                {
                    return TextAnchor.LowerRight;

                }
            }
            return defoult;
        }
        static FontStyle text_to_fontStyle(string text, FontStyle defoult)
        {
            if (text != "")
            {
                if (text == "Bold")
                {
                    return FontStyle.Bold;
                }
                else if (text == "BoldAndItalic")
                {
                    return FontStyle.BoldAndItalic;
                }
                else if (text == "Italic")
                {
                    return FontStyle.Italic;
                }
                else if (text == "Normal")
                {
                    return FontStyle.Normal;
                }
            }
            return defoult;
        }
        static ImagePosition text_to_image_pos(string text, ImagePosition defoult)
        {
            if (text != "")
            {
                if (text == "ImageAbove")
                {
                    return ImagePosition.ImageAbove;
                }
                else if (text == "ImageLeft")
                {
                    return ImagePosition.ImageLeft;
                }
                else if (text == "ImageOnly")
                {
                    return ImagePosition.ImageOnly;
                }
                else if (text == "TextOnly")
                {
                    return ImagePosition.TextOnly;
                }
            }
            return defoult;
        }

       static Font findFont(string fontname)
        {
            Font font = (Font)Statics.CMassets.Load(fontname);
            if (font != null)
            {
                return font;
            }
            return (Font)Resources.FindObjectsOfTypeAll(typeof(Font))[0];
        }
        static Color color_backgroundFiled;
        static Color color_UiLabelFiled;
        static Color gui_colorFiled;
        public static Color color_background
        {
            get
            {
                if (color_backgroundFiled != null)
                {
                    return color_backgroundFiled;
                }
                return Color.cyan;
            }
            set
            {
                color_backgroundFiled = value;
            }
        }
        public static Color color_UI
        {
            get
            {
                if (color_UiLabelFiled != null)
                {
                    return color_UiLabelFiled;
                }
                return Color.cyan;
            }
            set
            {
                color_UiLabelFiled = value;
            }
        }
        public static Color gui_color
        {
            get
            {
                if (gui_colorFiled != null)
                {
                    return gui_colorFiled;
                }
                return Color.cyan;
            }
            set
            {
                gui_colorFiled = value;
            }
        }
        public static void LoadSettingsSkin()
        {
            FileInfo info = new FileInfo(StylePathSkin + "config.cyanstyle");
            if (info.Exists)
            {
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
                foreach (var s in nskin)
                {
                    LoadTextureForSkin(s.Value.normal, StylePathSkin);
                    LoadTextureForSkin(s.Value.active, StylePathSkin);
                    LoadTextureForSkin(s.Value.hover, StylePathSkin);
                    LoadTextureForSkin(s.Value.focused, StylePathSkin);
                    LoadTextureForSkin(s.Value.onNormal, StylePathSkin);
                    LoadTextureForSkin(s.Value.onHover, StylePathSkin);
                    LoadTextureForSkin(s.Value.onFocused, StylePathSkin);
                    LoadTextureForSkin(s.Value.onActive, StylePathSkin);
                }
                string[] slin = File.ReadAllLines(StylePathSkin + "config.cyanstyle");
                foreach (string line in slin)
                {
                    if (line.Contains(":"))
                    {
                        string[] s = line.Split(new char[] { ':' });
                        string key = s[0].Trim();
                        string value = s[1].Trim();

                        if (key.StartsWith("color_gui_panels"))
                        {
                            gui_color = text_to_color(value, new Color(0f, 1f, 1f, 1f));
                        }
                        else if(key.StartsWith("color_background"))
                        {
                            color_background = text_to_color(value, new Color(0f, 0f, 0f, 0.2f));
                        }
                        else if (key.StartsWith("color_ui_panels"))
                        {
                            color_UI = text_to_color(value, new Color(0f, 1f, 1f, 1f));
                        }
                        GUIStyle style = FindStyle(key);
                        if (style != null)
                        {
                            if (key.StartsWith("string"))
                            {
                                if (key.StartsWith("string_font_name"))
                                {
                                    style.font = findFont(value);
                                    style.font.name = value;
                                }
                            }
                            if (key.StartsWith("int"))
                            {
                                if (key.StartsWith("int_margin"))
                                {
                                    if (key.StartsWith("int_margin_bottom"))
                                    {
                                        style.margin.bottom = Convert.ToInt32(value);
                                    }
                                    else if (key.StartsWith("int_margin_top"))
                                    {
                                        style.margin.top = Convert.ToInt32(value);
                                    }
                                    else if (key.StartsWith("int_margin_left"))
                                    {
                                        style.margin.left = Convert.ToInt32(value);
                                    }
                                    else if (key.StartsWith("int_margin_right"))
                                    {
                                        style.margin.right = Convert.ToInt32(value);
                                    }
                                }
                                else if (key.StartsWith("int_padding"))
                                {
                                    if (key.StartsWith("int_padding_bottom"))
                                    {
                                        style.padding.bottom = Convert.ToInt32(value);
                                    }
                                    else if (key.StartsWith("int_padding_top"))
                                    {
                                        style.padding.top = Convert.ToInt32(value);
                                    }
                                    else if (key.StartsWith("int_padding_left"))
                                    {
                                        style.padding.left = Convert.ToInt32(value);
                                    }
                                    else if (key.StartsWith("int_padding_right"))
                                    {
                                        style.padding.right = Convert.ToInt32(value);
                                    }
                                }
                                else if (key.StartsWith("int_overflow"))
                                {
                                    if (key.StartsWith("int_overflow_bottom"))
                                    {
                                        style.overflow.bottom = Convert.ToInt32(value);
                                    }
                                    else if (key.StartsWith("int_overflow_top"))
                                    {
                                        style.overflow.top = Convert.ToInt32(value);
                                    }
                                    else if (key.StartsWith("int_overflow_left"))
                                    {
                                        style.overflow.left = Convert.ToInt32(value);
                                    }
                                    else if (key.StartsWith("int_overflow_right"))
                                    {
                                        style.overflow.right = Convert.ToInt32(value);
                                    }
                                }
                                else if (key.StartsWith("int_border"))
                                {
                                    if (key.StartsWith("int_border_bottom"))
                                    {
                                        style.border.bottom = Convert.ToInt32(value);
                                    }
                                    else if (key.StartsWith("int_border_top"))
                                    {
                                        style.border.top = Convert.ToInt32(value);
                                    }
                                    else if (key.StartsWith("int_border_left"))
                                    {
                                        style.border.left = Convert.ToInt32(value);
                                    }
                                    else if (key.StartsWith("int_border_right"))
                                    {
                                        style.border.right = Convert.ToInt32(value);
                                    }
                                }
                            }
                            if (key.StartsWith("int_font_size_"))
                            {
                                style.fontSize = Convert.ToInt32(value);
                            }
                            if (key.StartsWith("font_style_"))
                            {
                                style.fontStyle = text_to_fontStyle(value, style.fontStyle);
                            }
                            if (key.StartsWith("image_pos"))
                            {
                                style.imagePosition = text_to_image_pos(value, style.imagePosition);
                            }
                            if (key.StartsWith("alignment_"))
                            {
                                style.alignment = text_to_acnchor(value, style.alignment);
                            }
                            if (key.StartsWith("color_normal_"))
                            {
                                style.normal.textColor = text_to_color(value, style.normal.textColor);
                            }
                            if (key.StartsWith("color_active_"))
                            {
                                style.active.textColor = text_to_color(value, style.active.textColor);
                            }
                            if (key.StartsWith("color_hover_"))
                            {
                                style.hover.textColor = text_to_color(value, style.hover.textColor);
                            }
                            if (key.StartsWith("color_focused_"))
                            {
                                style.focused.textColor = text_to_color(value, style.focused.textColor);
                            }
                            if (key.StartsWith("color_onNormal_"))
                            {
                                style.onNormal.textColor = text_to_color(value, style.onNormal.textColor);
                            }
                            if (key.StartsWith("color_onHover_"))
                            {
                                style.onHover.textColor = text_to_color(value, style.onHover.textColor);
                            }
                            if (key.StartsWith("color_onFocused_"))
                            {
                                style.onFocused.textColor = text_to_color(value, style.onFocused.textColor);
                            }
                            if (key.StartsWith("color_onActive_"))
                            {
                                style.onActive.textColor = text_to_color(value, style.onActive.textColor);
                            }
                        }
                    }
                }
              
            }
            else
            {
                File.WriteAllText(StylePathSkin + "config.cyanstyle", "", Encoding.UTF8);
            }
        }
        public static void SaveStyle()
        {
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
            string str = "GUI_SKIN_Cyan_mod\n";
            str = str + "color_background:" + color_background.ToString() + "\n";
            str = str + "color_ui_panels:" + color_UI.ToString() + "\n";
            str = str + "color_gui_panels:" + gui_color.ToString() + "\n";
            foreach (var s in nskin)
            {
                s.Value.wordWrap = true;
                SaveTextur(s.Value.normal, StylePathSkin);
                SaveTextur(s.Value.active, StylePathSkin);
                SaveTextur(s.Value.hover, StylePathSkin);
                SaveTextur(s.Value.focused, StylePathSkin);
                SaveTextur(s.Value.onNormal, StylePathSkin);
                SaveTextur(s.Value.onHover, StylePathSkin);
                SaveTextur(s.Value.onFocused, StylePathSkin);
                SaveTextur(s.Value.onActive, StylePathSkin);
                if (s.Value.font != null && s.Value.font.name != null && s.Value.font.name != "")
                {
                    str = str + "string_font_name_" + s.Key + ":" + s.Value.font.name + "\n";
                }
                str = str + "color_normal_" + s.Key + ":" + s.Value.normal.textColor.ToString() + "\n";
                str = str + "color_active_" + s.Key + ":" + s.Value.active.textColor.ToString() + "\n";
                str = str + "color_hover_" + s.Key + ":" + s.Value.hover.textColor.ToString() + "\n";
                str = str + "color_focused_" + s.Key + ":" + s.Value.focused.textColor.ToString() + "\n";
                str = str + "color_onNormal_" + s.Key + ":" + s.Value.onNormal.textColor.ToString() + "\n";
                str = str + "color_onHover_" + s.Key + ":" + s.Value.onHover.textColor.ToString() + "\n";
                str = str + "color_onFocused_" + s.Key + ":" + s.Value.onFocused.textColor.ToString() + "\n";
                str = str + "color_onActive_" + s.Key + ":" + s.Value.onActive.textColor.ToString() + "\n";
                str = str + "alignment_" + s.Key + ":" + s.Value.alignment.ToString() + "\n";
                str = str + "int_font_size_" + s.Key + ":" + s.Value.fontSize.ToString() + "\n";
                str = str + "font_style_" + s.Key + ":" + s.Value.fontStyle.ToString() + "\n";
                str = str + "image_pos_" + s.Key + ":" + s.Value.imagePosition.ToString() + "\n";
                str = str + to_Saving_RectOff(s.Value.border, "int_border", s.Key);
                str = str + to_Saving_RectOff(s.Value.margin, "int_margin", s.Key);
                str = str + to_Saving_RectOff(s.Value.overflow, "int_overflow", s.Key);
                str = str + to_Saving_RectOff(s.Value.padding, "int_padding", s.Key);

            }
            foreach (var s in nskin)
            {
                LoadTextureForSkin(s.Value.normal, StylePathSkin);
                LoadTextureForSkin(s.Value.active, StylePathSkin);
                LoadTextureForSkin(s.Value.hover, StylePathSkin);
                LoadTextureForSkin(s.Value.focused, StylePathSkin);
                LoadTextureForSkin(s.Value.onNormal, StylePathSkin);
                LoadTextureForSkin(s.Value.onHover, StylePathSkin);
                LoadTextureForSkin(s.Value.onFocused, StylePathSkin);
                LoadTextureForSkin(s.Value.onActive, StylePathSkin);
            }
            File.WriteAllText(StylePathSkin + "config.cyanstyle", str, Encoding.UTF8);

        }
        public static List<PlayerCheck> ConnectPlList = new List<PlayerCheck>();

        public static void add_pl(PhotonPlayer player)
        {
            if (player.name == "")
            {
                ConnectPlList.Add(new PlayerCheck(player, PhotonNetwork.room));
                return;
            }
            if (ConnectPlList.Count > 0)
            {
            
                    for (int i = 0; i < ConnectPlList.Count; i++ )
                    {
                        PlayerCheck pc = ConnectPlList[i];
                        if (player.iscleanname == pc.name && pc.server_id == PhotonNetwork.room.IDRoom && pc.server_name == PhotonNetwork.room.RoomName)
                        {
                            return;
                        }
                        if (player.ID == pc.ID && player.iscleanname != pc.name && pc.server_id == PhotonNetwork.room.IDRoom && pc.server_name == PhotonNetwork.room.RoomName)
                        {
                            ConnectPlList[i].ID = player.ID;
                            ConnectPlList[i].ishexname = player.ishexname;
                            ConnectPlList[i].hash = player.customProperties;
                            ConnectPlList[i].name = player.name2;
                        }
                    }
                ConnectPlList.Add(new PlayerCheck(player, PhotonNetwork.room));
                return;
            }
            else
            {
                ConnectPlList.Add(new PlayerCheck(player, PhotonNetwork.room));
            }
        }
        public class PlayerCheck
        {
            public int ID;
            public ExitGames.Client.Photon.Hashtable hash;
            public string time;
            public string name;
            public string server_name;
            public int server_id;
            public string ishexname;
            public bool show = false;
            public bool show_hash = false;

            public PlayerCheck(PhotonPlayer player,Room room)
            {
                ID = player.ID;
                hash = player.customProperties;
                time = DateTime.Now.ToString("HH:mm:ss");
                name = player.iscleanname;
                server_name = room.RoomName;
                server_id = room.IDRoom;
                ishexname = player.ishexname;
            }
        }

    }
    public class Cyanmesh : UnityEngine.MonoBehaviour
    {
        public static bool isInited = true;
        public static GameObject Neko_L;
        public static GameObject Neko_R;
        public static GameObject Bunny_L;
        public static GameObject Bunny_R;
        public static GameObject Horn_Objects;
        public static GameObject Devil_W_L;
        public static GameObject Devil_W_R;
        public static GameObject Angel_W_L;
        public static GameObject Angel_W_R;
        public static GameObject Cat_Objects;
        public static GameObject arch_Objects;
        public static GameObject bat_1;
        public static GameObject bat_2;
        public static GameObject Butterfly;
        public static GameObject Dove_Objects;
        public static GameObject Heart_3;
        public static GameObject Heart_2;
        public static GameObject Heart_1;
        public static GameObject Skull_2;
        public static GameObject Skull_1;
        public static void Init()
        {
            if (isInited)
            {
                Neko_L = (GameObject)Statics.CMassets.Load("Neko_mi_mi_L");
                Neko_R = (GameObject)Statics.CMassets.Load("Neko_mi_mi_R");
                Bunny_L = (GameObject)Statics.CMassets.Load("Rabbit");
                Bunny_R = (GameObject)Statics.CMassets.Load("Rabbit");
                Horn_Objects = (GameObject)Statics.CMassets.Load("Horn4");
                Devil_W_L = (GameObject)Statics.CMassets.Load("Wings_Devil_Right");
                Devil_W_R = (GameObject)Statics.CMassets.Load("Wings_Devil_Left");
                Angel_W_L = (GameObject)Statics.CMassets.Load("Wings_Left");
                Angel_W_R = (GameObject)Statics.CMassets.Load("Wings_Right");
                Cat_Objects = (GameObject)Statics.CMassets.Load("Cat");
                arch_Objects = (GameObject)Statics.CMassets.Load("arch");
                bat_1 = (GameObject)Statics.CMassets.Load("bat");
                bat_2 = (GameObject)Statics.CMassets.Load("bat2");
                Butterfly = (GameObject)Statics.CMassets.Load("Butterfly");
                Dove_Objects = (GameObject)Statics.CMassets.Load("dove");
                Heart_3 = (GameObject)Statics.CMassets.Load("Heart999");
                Heart_2 = (GameObject)Statics.CMassets.Load("Heart2");
                Heart_1 = (GameObject)Statics.CMassets.Load("Heart3");
                Skull_2 = (GameObject)Statics.CMassets.Load("Skull");
                Skull_1 = (GameObject)Statics.CMassets.Load("Skull3");
                isInited = false;
            }
        }
    }
    public class DebugConsoleCyan : UnityEngine.MonoBehaviour
    {
        Rect rect;
        Vector2 pos;
        Vector2 pos2;
        List<Log> logslist;
        Log log;
        bool OnDebugCansole;
        bool show;
        bool Stoped;
        bool AllError;
        bool Autoscrool;
        PhotonVieverMeneger pviev;
        public static DebugConsoleCyan instance;
        int selected;
        void Awake()
        {
            selected = 0;
            Autoscrool = true;
            Stoped = false;
            AllError = false;
            OnDebugCansole = PlayerPrefs.GetInt("On_Debug_Console_Cyan", 0) == 1;
            DontDestroyOnLoad(base.gameObject);
            logslist = new List<Log>();
            Application.RegisterLogCallback(new Application.LogCallback(this.AddLogged));
        }
        public void AddLogged(string mess, string stack, LogType type)
        {
            if (OnDebugCansole && !Stoped)
            {
                if (AllError)
                {
                    if (type == LogType.Exception || type == LogType.Error || type == LogType.Assert)
                    {
                        Log loged = new Log(mess, stack, type);

                        logslist.Add(loged);
                        if (logslist.Count > 120)
                        {
                            logslist.Remove(logslist.First<Log>());
                        }
                    }
                }
                else
                {
                    if (!mess.Contains("The referenced script on this Behaviour is missing"))
                    {
                        Log loged = new Log(mess, stack, type);
                        logslist.Add(loged);
                        if (logslist.Count > 120)
                        {
                            logslist.Remove(logslist.First<Log>());
                        }
                    }
                }
            }
        }
        Color clbut(LogType logtype)
        {
            if (logtype == LogType.Assert)
            {
                return Color.red;
            }
            else if (logtype == LogType.Error)
            {
                return Color.red;
            }
            else if (logtype == LogType.Exception)
            {
                return Color.red;
            }
            else if (logtype == LogType.Warning)
            {
                return Color.yellow;
            }
            else
            {
                return colorlabel;
            }
        }
        void OnGUI()
        {
            if (this.show)
            {
                this.rect = GUILayout.Window(IDwindows.id3, this.rect, new GUI.WindowFunction(this.LogPanel), "", style);
            }
        }
        void Logg()
        {
            GUIStyle stylelabel = new GUIStyle(GUI.skin.label);
            stylelabel.normal.textColor = colorlabel;

            GUILayout.BeginVertical(style);
            GUILayout.Label("<b>Debug Log:</b>", stylelabel);
            GUILayout.EndVertical();
            if (OnDebugCansole)
            {
                float posss = log.message == string.Empty ? 500 : 350;
                GUILayout.Space(5f);
                GUILayout.BeginVertical(style);
                pos = GUILayout.BeginScrollView(pos, GUILayout.Height(posss));
                if (logslist.Count >= 1)
                {
                    if (Autoscrool)
                    {
                        pos.y = 9999;
                    }
                    foreach (Log loged in logslist)
                    {
                        GUIStyle stylebutton = new GUIStyle(GUI.skin.label);
                        stylebutton.alignment = TextAnchor.MiddleLeft;
                        stylebutton.normal.textColor = clbut(loged.logtype);
                        stylebutton.hover.textColor = clbut(loged.logtype);
                        stylebutton.active.textColor = colorlabel;
                        if (log == loged)
                        {
                            stylebutton.normal.background = buttonH;
                            stylebutton.active.background = buttonH;
                            stylebutton.hover.background = buttonH;
                        }
                        else
                        {
                            stylebutton.normal.background = buttonN;
                            stylebutton.active.background = buttonH;
                            stylebutton.hover.background = buttonH;
                        }
                        if (GUILayout.Button(loged.logtype + "|" + loged.time.ToString("HH:mm:ss") + "|" + Convert.ToString(loged.message), stylebutton))
                        {
                            log = loged;
                        }
                    }
                }
                else
                {
                    GUILayout.Label("Console clean.", stylelabel);
                }
                GUILayout.EndScrollView();
                GUILayout.EndVertical();
                if (log.message != string.Empty)
                {
                    GUILayout.Space(5f);
                    GUILayout.BeginVertical(style);
                    pos2 = GUILayout.BeginScrollView(pos2, GUILayout.Height(150));
                    GUILayout.TextArea("<color=#007700>LogType:</color>" + log.logtype + " <color=#007700>Time:</color>" + log.time.ToString("HH:mm:ss") + "\n<color=#007700>Message:</color>" + Convert.ToString(log.message) + "\n<color=#007700>StackTrace:</color>" + Convert.ToString(log.stackTrace), stylelabel);

                    GUILayout.EndScrollView();
                    GUILayout.EndVertical();
                }
                GUILayout.Space(5f);
                GUILayout.BeginHorizontal(style);
                if (GUILayout.Button("Clear", GUILayout.Width(120f)))
                {
                    logslist.Clear();
                }
                if (GUILayout.Button("Off", GUILayout.Width(50f)))
                {
                    OnDebugCansole = false;
                    PlayerPrefs.SetInt("On_Debug_Console_Cyan", 0);
                }
                AllError = GUILayout.Toggle(AllError, "<color=#00FF00>Only Error.</color>");
                Stoped = GUILayout.Toggle(Stoped, "<color=#00FF00>Stop.</color>");
                Autoscrool = GUILayout.Toggle(Autoscrool, "<color=#00FF00>AutoScroll.</color>");
                GUILayout.EndHorizontal();
            }
            else
            {
                GUILayout.Label("Debug Console Disabled.", stylelabel);
                if (GUILayout.Button("Enable", GUILayout.Width(120f)))
                {
                    OnDebugCansole = true;
                    PlayerPrefs.SetInt("On_Debug_Console_Cyan", 1);
                }
            }
        }
        void LogPanel(int winID)
        {
            selected = GUILayout.SelectionGrid(selected, new string[] { "Log", "PhotonView" }, 2, GUILayout.Width(250f));
            if (selected == 0)
            {
                Logg();
            }
            else if (selected == 1)
            {
                pviev.menu();
            }
            rect = rect.maxrect();
            GUI.DragWindow();
        }
        void SetPos()
        {
            rect = new Rect(10, 10, 700, 0);
        }
        void Start()
        {
            instance = this;
            colorlabel = new Color(0f, 1f, 0f, 1f);
            show = false;
            SetPos();
            pos = Vector2.zero;
            log = new Log();

            background = (new Color(0f, 0f, 0f, 0.6f)).toTexture1();
            buttonN = (new Color(0f, 0f, 0f, 1f)).toTexture1();
            buttonH = (new Color(0.2f, 0.2f, 0.2f, 1f)).toTexture1();
            style = new GUIStyle();
            style.normal.background = background;
            style.active.background = background;
            style.hover.background = background;
            style.focused.background = background;
            style.onFocused.background = background;
            style.onHover.background = background;
            style.onNormal.background = background;
            style.onActive.background = background;
            pviev = base.gameObject.AddComponent<PhotonVieverMeneger>();
        }
        void LateUpdate()
        {
            if (FengGameManagerMKII.inputRC.isInputCyanKeyDown(InputCode.debug_console))
            {

                show = !show;
                SetPos();
            }
            if (show && selected == 1)
            {
                pviev.update();
            }
        }
        Color colorlabel;
        GUIStyle style;
        Texture2D background;
        Texture2D buttonN;
        Texture2D buttonH;
        public class Log
        {
            public readonly DateTime time;
            public readonly string message;
            public readonly string stackTrace;
            public readonly LogType logtype;
            public Log(string mess, string stack, LogType logt)
            {
                time = System.DateTime.Now;
                message = mess;
                stackTrace = stack;
                logtype = logt;
            }
            public Log()
            {
                time = System.DateTime.Now;
                message = string.Empty;
                stackTrace = string.Empty;
                logtype = LogType.Log;
            }
        }
    }
    public class PhotonVieverMeneger : UnityEngine.MonoBehaviour
    {
        Vector2 scrolpos;
        PhotonView myPviv;
        void Awake()
        {
            myPviv = null;
            scrolpos = Vector2.zero;
        }
        List<PhotonView> PvievList = new List<PhotonView>();
        public void menu()
        {
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical(GUILayout.Width(200f));
            scrolpos = GUILayout.BeginScrollView(scrolpos);
            {
                foreach (PhotonView piv in PvievList)
                {
                    string str = string.Empty;
                    if (myPviv == piv)
                    {
                        str = ">>";
                    }
                    if (GUILayout.Button(str + piv.name))
                    {
                        myPviv = piv;
                    }
                }
            }
            GUILayout.EndScrollView();
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            if (myPviv != null)
            {
                GUILayout.TextField(myPviv.ToString(), GUI.skin.label);
                GUILayout.TextField("player id:" + (myPviv.owner != null ? myPviv.owner.ID.ToString() : "none"), GUI.skin.label);

            }
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }
        public void update()
        {
            PvievList = new List<PhotonView>();
            PhotonView[] viewArray = UnityEngine.Object.FindObjectsOfType(typeof(PhotonView)) as PhotonView[];
            foreach (PhotonView p in viewArray)
            {
                PvievList.Add(p);
            }

        }
    }

    public class OnLeave : UnityEngine.MonoBehaviour
    {
        long t1 = DateTime.Now.Ticks;
        void OnGUI()
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Coltext.blacktext);
            string srew = string.Empty;
            if (FengGameManagerMKII.ignoreList.Contains(PhotonNetwork.player.ID))
            {
                srew = INC.la("you_banned_from_the_server");
            }
            else
            {
                srew = INC.la("you_kicked_from_the_server");
            }
            double s = (DateTime.Now.Ticks - t1) * 1E-7;
            GUI.Label(new Rect(Screen.width / 2 - 400, Screen.height / 2 - 100, 800, 200), "<color=cyan><size=20>" + srew + "</size>\n" + INC.la("leave_to_time") + (3 - (float)s).ToString("F") + ".</color>", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleLeft });
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 40, 100, 22), INC.la("quit")))
            {
                Leave();
            }
        }
        void Leave()
        {
            Time.timeScale = 1f;
            if (PhotonNetwork.connected)
            {
                PhotonNetwork.Disconnect();
            }
            IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.STOP;
            FengGameManagerMKII.instance.gameStart = false;
            Screen.lockCursor = false;
            Screen.showCursor = true;
             FengGameManagerMKII.instance.MenuOn = false;
            UnityEngine.Object.Destroy(CyanMod.CachingsGM.Find("MultiplayerManager"));
            Application.LoadLevel("menu");
        }
        void Update()
        {
            double s = (DateTime.Now.Ticks - t1) * 1E-7;
            if (s > 3)
            {
                Leave();
            }
        }
    }

    internal class FunctionComparer<T> : IComparer<T>
    {
        private Comparer<T> c;
        private Comparison<T> comparison;

        public FunctionComparer(Comparison<T> comparison)
        {
            this.c = Comparer<T>.Default;
            this.comparison = comparison;
        }

        public int Compare(T x, T y)
        {
            return this.comparison(x, y);
        }
    }
    public class GUICyan
    {
        public static void OnToogleCyan(string name, int num, int on, int off, GUIStyle label, GUIStyle toggle)
        {
            GUILayout.BeginHorizontal();
            GUILayout.TextField(name, label);
            bool flag34 = false;
            if ((int)FengGameManagerMKII.settings[num] == on)
            {
                flag34 = true;
            }
            bool flag35 = GUILayout.Toggle(flag34, flag34 ? INC.la("On") : INC.la("Off"), toggle);
            if (flag34 != flag35)
            {
                if (flag35)
                {
                    FengGameManagerMKII.settings[num] = on;
                }
                else
                {
                    FengGameManagerMKII.settings[num] = off;
                }
            }
            GUILayout.EndHorizontal();
        }
        public static void OnToogleCyan(string name, int num, int on, int off, float labelwidth)
        {
            GUIStyle labelstyle = new GUIStyle(GUI.skin.label);
            GUILayout.BeginHorizontal();
            bool flag34 = false; 
            if ((int)FengGameManagerMKII.settings[num] == on)
            {
                flag34 = true;
                labelstyle.normal.textColor = GUI.skin.label.onNormal.textColor;
            }
            else
            {
                labelstyle.normal.textColor = GUI.skin.label.normal.textColor;
            }
            GUILayout.TextField(name, labelstyle);
            bool flag35 = GUILayout.Toggle(flag34, flag34 ? INC.la("On") : INC.la("Off"), GUI.skin.button, GUILayout.Width(labelwidth));
            if (flag34 != flag35)
            {
                if (flag35)
                {
                    FengGameManagerMKII.settings[num] = on;
                }
                else
                {
                    FengGameManagerMKII.settings[num] = off;
                }
            }
            GUILayout.EndHorizontal();
           
            if (num == 63)
            {
                FengGameManagerMKII.linkHash[0].Clear();
                FengGameManagerMKII.linkHash[1].Clear();
                FengGameManagerMKII.linkHash[2].Clear();
            }
        }
        public static void OnTextFileCyan(string name, int num, float textfiledwidth)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(name);
            FengGameManagerMKII.settings[num] = GUILayout.TextField((string)FengGameManagerMKII.settings[num], GUILayout.Width(textfiledwidth));
            GUILayout.EndHorizontal();
        }
        public static void OnRebindRC(string name, int val, GUIStyle onlabel, GUIStyle onbutton)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(name, onlabel);
            if (GUILayout.Button((string)FengGameManagerMKII.settings[val], onbutton))
            {
                FengGameManagerMKII.settings[val] = "waiting...";
                FengGameManagerMKII.settings[100] = val;
            }
            GUILayout.EndHorizontal();
        }
        public static void OnRebindCyanTitan(string name, int settings_val, int code, GUIStyle onlabel, GUIStyle onbutton)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(name, onlabel);
            if (GUILayout.Button((string)FengGameManagerMKII.settings[settings_val], onbutton))
            {
                FengGameManagerMKII.settings[settings_val] = "waiting...";
                FengGameManagerMKII.settings[100] = code;
            }
            GUILayout.EndHorizontal();
        }
        public static void OnRebind(string name, int setting_code, int input_code, float width)
        {
            GUILayout.BeginHorizontal();

            if (GUILayout.Button((string)FengGameManagerMKII.settings[setting_code], GUILayout.Width(width)))
            {
                FengGameManagerMKII.settings[setting_code] = "waiting...";
                FengGameManagerMKII.settings[100] = 30 + input_code;
            }
            GUILayout.Label(name);
            GUILayout.EndHorizontal();
        }
    }
    public struct IDwindows
    {
        static int id1Filed = 155;
        static int id3Filed = 157;
        static int id2Filed = 156;
        public static int id1
        {
            get
            {
                return id1Filed;
            }
        }
        public static int id2
        {
            get
            {
                return id2Filed;
            }
        }
        public static int id3
        {
            get
            {
                return id3Filed;
            }
        }
        public static int id4
        {
            get { return 158; }
        }
        public static int id5
        {
            get { return 160; }
        }
    }
    public struct Coltext
    {
        static Texture2D CyanFiled;
        static Texture2D BlackFiled;
        static Texture2D GrayFiled;
        public static Texture2D transp
        {
            get
           ;
            set
            ;
        }
        public static Texture2D cyantext
        {
            get
            {
                return CyanFiled;
            }
            set
            {
                CyanFiled = value;
            }
        }
        public static Texture2D blacktext
        {
            get
            {
                return BlackFiled;
            }
            set
            {
                BlackFiled = value;
            }
        }
        public static Texture2D graytext
        {
            get
            {
                return GrayFiled;
            }
            set
            {
                GrayFiled = value;
            }
        }
        public static Color Red
        {
            get
            {
                return new Color(255, 0, 0, 1);
            }
        }
        public static Color Green
        {
            get
            {
                return new Color(0f, 1f, 0f, 1f);
            }
        }
        public static Color Blue
        {
            get
            {
                return new Color(0f, 0f, 1f, 1f);
            }
        }
        public static Color White
        {
            get
            {
                return new Color(1f, 1f, 1f, 1f);
            }
        }
        public static Color Black
        {
            get
            {
                return new Color(0f, 0f, 0f, 1f);
            }
        }
        public static Color Yellow
        {
            get
            {
                return new Color(255, 255, 0, 1);
            }
        }
        public static Color Cyan
        {
            get
            {
                return new Color(0f, 1f, 1f, 1f);
            }
        }
        public static Color Magenta
        {
            get
            {
                return new Color(1f, 0f, 1f, 1f);
            }
        }
        public static Color Grey
        {
            get
            {
                return new Color(0.5f, 0.5f, 0.5f, 1f);
            }
        }
        public static Color Clear
        {
            get
            {
                return new Color(0f, 0f, 0f, 0f);
            }
        }
        public static Color Fuchsia
        {
            get
            {
                return new Color(255, 0, 255, 1);
            }
        }
        public static Color Maroon
        {
            get
            {
                return new Color(128, 0, 0, 1);
            }
        }
        public static Color Lime
        {
            get
            {
                return new Color(0, 255, 0, 1);
            }
        }
        public static Color Navy
        {
            get
            {
                return new Color(0, 0, 128, 1);
            }
        }
        public static Color Olive
        {
            get
            {
                return new Color(128, 128, 0, 1);
            }
        }
        public static Color Purple
        {
            get
            {
                return new Color(128, 0, 128, 1);
            }
        }
        public static Color Silver
        {
            get
            {
                return new Color(192, 192, 192, 1);
            }
        }
        public static Color Teal
        {
            get
            {
                return new Color(0, 128, 128, 1);
            }
        }
    }
    public static class CachingsGM
    {
        private static Dictionary<string, GameObject> cache = new Dictionary<string, GameObject>();
        private static Dictionary<string, Component> cacheType = new Dictionary<string, Component>();

        public static GameObject Find(string name)
        {
            GameObject obj2;
            string str = name.ToLower().Trim();
            switch (str)
            {
                case "maincamera":
                    if (!cache.ContainsKey(name) || (cache[name] == null))
                    {
                        GameObject obj3;
                        cache[name] = obj3 = GameObject.Find(name);
                        return obj3;
                    }
                    return cache[name];

                case "aottg_hero1":
                case "aottg_hero1(clone)":
                case "colossal_titan":
                case "femaletitan":
                case "female_titan":
                case "crawler":
                case "punk":
                case "abberant":
                case "jumper":
                case "titan":
                case "tree":
                case "cube001":
                    return GameObject.Find(name);
            }
            if (((!cache.ContainsKey(name) || ((obj2 = cache[name]) == null)) || ((!obj2.activeInHierarchy && !str.StartsWith("ui")) && (!str.StartsWith("label") && !str.StartsWith("ngui")))) && ((obj2 = GameObject.Find(name)) != null))
            {
                GameObject obj4;
                cache[name] = obj4 = obj2;
                return obj4;
            }
            return obj2;
        }

        public static T Find<T>(string name) where T : Component
        {
            string key = name + typeof(T).FullName;
            if (cacheType.ContainsKey(key))
            {
                Component component = cacheType[key];
                if (component != null)
                {
                    Component component2;
                    T local = component as T;
                    if (local != null)
                    {
                        return local;
                    }
                    cacheType[key] = component2 = component.GetComponent<T>();
                    return (T)component2;
                }
            }
            GameObject obj2 = Find(name);
            if (obj2 != null)
            {
                Component component3;
                cacheType[key] = component3 = obj2.GetComponent<T>();
                return (T)component3;
            }
            obj2 = GameObject.Find(name);
            if (obj2 != null)
            {
                Component component4;
                cacheType[key] = component4 = obj2.GetComponent<T>();
                return (T)component4;
            }
            return default(T);
        }
    }
    internal static class CacheGameResources
    {
        private static System.Collections.Generic.Dictionary<string, UnityEngine.Object> cacheLocal = new System.Collections.Generic.Dictionary<string, UnityEngine.Object>();

        private static System.Collections.Generic.Dictionary<string, UnityEngine.Object> cacheGO = new System.Collections.Generic.Dictionary<string, UnityEngine.Object>();


        internal static UnityEngine.Object Load(string path)
        {
            path = path.ToUpper();
            if (CacheGameResources.cacheLocal.ContainsKey(path) && CacheGameResources.cacheLocal[path] != null)
            {
                return CacheGameResources.cacheLocal[path];
            }
            return CacheGameResources.cacheLocal[path] = Resources.Load(path);
        }

        internal static UnityEngine.Object Load(string path, System.Type type)
        {
            string key = path.ToUpper() + type.FullName;
            if (CacheGameResources.cacheGO.ContainsKey(key) && CacheGameResources.cacheGO[key] != null)
            {
                return CacheGameResources.cacheGO[key];
            }
            return CacheGameResources.cacheGO[key] = Resources.Load(path, type);
        }

        internal static T Load<T>(string path) where T : UnityEngine.Object
        {
            string key = path.ToUpper() + typeof(T).FullName;
            if (CacheGameResources.cacheGO.ContainsKey(key) && CacheGameResources.cacheGO[key] != null && CacheGameResources.cacheGO[key] is T)
            {
                return (T)((object)CacheGameResources.cacheGO[key]);
            }
            return (T)((object)(CacheGameResources.cacheGO[key] = Resources.Load<T>(path)));
        }

        internal static bool Load<T>(string path, out T value) where T : UnityEngine.Object
        {
            string key = path.ToUpper() + typeof(T).FullName;
            if (CacheGameResources.cacheGO.ContainsKey(key) && CacheGameResources.cacheGO[key] != null && CacheGameResources.cacheGO[key] is T)
            {
                value = (T)((object)CacheGameResources.cacheGO[key]);
                return value != null;
            }
            CacheGameResources.cacheGO[key] = (value = Resources.Load<T>(path));
            return value != null;
        }

        internal static UnityEngine.Object RCLoad(string path)
        {
            string key = "RC" + path.ToUpper();
            if (CacheGameResources.cacheLocal.ContainsKey(key) && CacheGameResources.cacheLocal[key] != null)
            {
                return CacheGameResources.cacheLocal[key];
            }
            return CacheGameResources.cacheLocal[key] = global::FengGameManagerMKII.RCassets.Load(path);
        }

        internal static UnityEngine.Object RCLoad(string path, System.Type type)
        {
            string key = "RC" + path.ToUpper() + type.FullName;
            if (CacheGameResources.cacheGO.ContainsKey(key) && CacheGameResources.cacheGO[key] != null)
            {
                return CacheGameResources.cacheGO[key];
            }
            return CacheGameResources.cacheGO[key] = global::FengGameManagerMKII.RCassets.Load(path, type);
        }

        internal static T RCLoad<T>(string path) where T : UnityEngine.Object
        {
            string key = "RC" + path.ToUpper() + typeof(T).FullName;
            if (CacheGameResources.cacheGO.ContainsKey(key) && CacheGameResources.cacheGO[key] != null && CacheGameResources.cacheGO[key] is T)
            {
                return (T)((object)CacheGameResources.cacheGO[key]);
            }
            return (T)((object)(CacheGameResources.cacheGO[key] = (T)((object)global::FengGameManagerMKII.RCassets.Load(path, typeof(T)))));
        }

        internal static bool RCLoad<T>(string path, out T value) where T : UnityEngine.Object
        {
            string key = "RC" + path.ToUpper() + typeof(T).FullName;
            if (CacheGameResources.cacheGO.ContainsKey(key) && CacheGameResources.cacheGO[key] != null && CacheGameResources.cacheGO[key] is T)
            {
                value = (T)((object)CacheGameResources.cacheGO[key]);
                return value != null;
            }
            CacheGameResources.cacheGO[key] = (value = (T)((object)global::FengGameManagerMKII.RCassets.Load(path, typeof(T))));
            return value != null;
        }
    }
}

public static class PrefersCyan
{
    public static Dictionary<string, string> configstring;
    public static Dictionary<string, float> configfloat;
    public static Dictionary<string, int> configint;
    public static Dictionary<string, Color> configcolor;
    public static Dictionary<string, Vector2> configvector;
    public static void Clean()
    {
        configstring.Clear();
        configfloat.Clear();
        configint.Clear();
        configcolor.Clear();
        configvector.Clear();
    }
    public static void SetFirstVector2(string key, Vector2 value)
    {
        if (configvector == null)
        {
            configvector = new Dictionary<string, Vector2>();
        }
        if (configvector.ContainsKey(key))
        {
            configvector[key] = value;
        }
        else
        {
            configvector.Add(key, value);
        }
    }
    public static void SetFirstColor(string key, Color value)
    {
        if (configcolor == null)
        {
            configcolor = new Dictionary<string, Color>();
        }
        if (configcolor.ContainsKey(key))
        {
            configcolor[key] = value;
        }
        else
        {
            configcolor.Add(key, value);
        }
    }
    public static void SetFirstString(string key, string value)
    {
        if (configstring == null)
        {
            configstring = new Dictionary<string, string>();
        }
        if (configstring.ContainsKey(key))
        {
            configstring[key] = value;
        }
        else
        {
            configstring.Add(key, value);
        }
    }
    public static void SetFirstInt(string key, string value)
    {
        if (configint == null)
        {
            configint = new Dictionary<string, int>();
        }
        if (configint.ContainsKey(key))
        {
            int num;
            if (int.TryParse(value, out num))
            {
                configint[key]= num;
                return;
            }
            configint[key] = 0;
        }
        else
        {
            int num;
            if (int.TryParse(value, out num))
            {
                configint.Add(key, num);
                return;
            }
            configint.Add(key, 0);
        }
    }
    public static void SetFirstFloat(string key, string value)
    {
        if (configfloat == null)
        {
            configfloat = new Dictionary<string, float>();
        }
        if (configfloat.ContainsKey(key))
        {
            float num;
            if (float.TryParse(value, out num))
            {
                configfloat[key]=  num;
                return;
            }
            configfloat[key] = 0f;
            return;
        }
        else
        {
            float num;
            if (float.TryParse(value, out num))
            {
                configfloat.Add(key, num);
                return;
            }
            configfloat.Add(key, 0f);
        }
    }
    public static void Save()
    {
        if (configstring != null && configfloat != null && configint != null && configcolor != null && configvector != null)
        {
            string text = "************************\nConfig_File_Cyan_mod\n************************\n";


            foreach (var t1 in configstring)
            {
                text = text + t1.Key + ":" + t1.Value + "\n";
            }
            foreach (var t1 in configfloat)
            {
                text = text + t1.Key + ":" + t1.Value.ToString() + "\n";
            }
            foreach (var t1 in configint)
            {
                text = text + t1.Key + ":" + t1.Value.ToString() + "\n";
            }
            foreach (var t1 in configcolor)
            {
                text = text + t1.Key + ":" + t1.Value.ToString() + "\n";
            }
            foreach (var t1 in configvector)
            {
                text = text + t1.Key + ":" + t1.Value.x + "," + t1.Value.y + "\n";
            }
            File.WriteAllText(CyanMod.INC.configPath, text, Encoding.UTF8);
        }
    }
    public static void SetString(string key, string value)
    {
        configstring[key] = value;
    }
    public static void SetInt(string key, int value)
    {
        configint[key] = value;
    }
    public static void SetFloat(string key, float value)
    {
        configfloat[key] = value;
    }
    public static void SetColor(string key, Color value)
    {
        configcolor[key] = value;
    }
    public static void SetVector2(string key, Vector2 value)
    {
        configvector[key] = value;
    }
    public static Vector2 GetVector2(string key, Vector2 defoult)
    {
        if (configvector == null)
        {
            configvector = new Dictionary<string, Vector2>();
        }
        if (configvector.ContainsKey(key) && configvector[key] != null)
        {
            return configvector[key];
        }
        configvector.Add(key, defoult);
        return defoult;
    }
    public static string GetString(string key, string defoult)
    {
        if (configstring == null)
        {
            configstring = new Dictionary<string, string>();
        }
        if (configstring.ContainsKey(key) && configstring[key] != null)
        {
            return configstring[key];
        }
        configstring.Add(key, defoult);
        return defoult;
    }
    public static int GetInt(string key, int defoult)
    {
        if (configint == null)
        {
            configint = new Dictionary<string, int>();
        }
        if (configint.ContainsKey(key))
        {
            return configint[key];
        }
        configint.Add(key, defoult);
        return defoult;
    }
    public static float GetFloat(string key, float defoult)
    {
        if (configfloat == null)
        {
            configfloat = new Dictionary<string, float>();
        }
        if (configfloat.ContainsKey(key))
        {
            return configfloat[key];
        }
        configfloat.Add(key, defoult);
        return defoult;
    }
    public static Color GetColor(string key, Color defoult)
    {
        if (configcolor == null)
        {
            configcolor = new Dictionary<string, Color>();
        }
        if (configcolor.ContainsKey(key))
        {
            return configcolor[key];
        }
        configcolor.Add(key, defoult);
        return defoult;
    }
   
}

public class CyanSkin
{
    public string name;
    public string horse;
    public string hair;
    public string eyes;
    public string glass;
    public string face;
    public string skin;
    public string hoodie;
    public string costume;
    public string logo_and_cape;
    public string dmg_right;
    public string dmg_left;
    public string gas;
    public string weapon_trail;
    public bool isRead = false;

    public CyanSkin()
    {
        name = "";
        horse = "";
        hair = "";
        eyes = "";
        glass = "";
        face = "";
        skin = "";
        hoodie = "";
        costume = "";
        logo_and_cape = "";
        dmg_right = "";
        dmg_left = "";
        gas = "";
        weapon_trail = "";
    }
    public CyanSkin(CyanSkin newskin)
    {
        name = newskin.name;
        horse = newskin.horse;
        hair = newskin.hair;
        eyes = newskin.eyes;
        glass = newskin.glass;
        face = newskin.face;
        skin = newskin.skin;
        hoodie = newskin.hoodie;
        costume = newskin.costume;
        logo_and_cape = newskin.logo_and_cape;
        dmg_right = newskin.dmg_right;
        dmg_left = newskin.dmg_left;
        gas = newskin.gas;
        weapon_trail = newskin.weapon_trail;
    }
    public bool isCleaned
    {
        get
        {
            if (horse == "" &&hair == "" && eyes == "" && glass == "" && face == "" && skin == "" && hoodie == "" && costume == "" && logo_and_cape == "" && dmg_right == "" && dmg_left == "" && gas == "" && weapon_trail == "")
            {
                return true;
            }
            return false;
        }
    }
    public string toSaved
    {
        get 
        {
            string str = string.Empty;
            str = str + "name`" + name.Trim() + "\n";
            str = str + "s_horse`" + horse.Trim() + "\n";
            str = str + "s_hair`" + hair.Trim() + "\n";
            str = str + "s_eyes`" + eyes.Trim() + "\n";
            str = str + "s_glass`" + glass.Trim() + "\n";
            str = str + "s_face`" + face.Trim() + "\n";
            str = str + "s_skin`" + skin.Trim() + "\n";
            str = str + "s_hoodie`" + hoodie.Trim() + "\n";
            str = str + "s_costume`" + costume.Trim() + "\n";
            str = str + "s_logo_and_cape`" + logo_and_cape.Trim() + "\n";
            str = str + "s_3dmg_right`" + dmg_right.Trim() + "\n";
            str = str + "s_3dmg_left`" + dmg_left.Trim() + "\n";
            str = str + "s_gas`" + gas.Trim() + "\n";
            str = str + "s_weapon_trail`" + weapon_trail.Trim() + "\n";
            return str.Trim();
        }
    }
}
public class AnimN
{
    public List<T> list;
    public AnimN(string text)
    {
        list = new List<T>();
        for (int i = 0; i < text.Length; i++)
        {
            list.Add(new T(text[i], "FFFFFF"));
        }
    }
    public AnimN(string text, bool isexporting)
    {
        string[] tttes = text.Split(new char[] { '[' });

        list = new List<T>();
        for (int i = 0; i < tttes.Length; i++)
        {
            string Text = tttes[i];
            string ttser = Text.Replace("]", string.Empty);
            if (ttser.Length >= 5)
            {
                list.Add(new T(ttser[6], ttser.Substring(0, 6)));
            }
        }
    }
    public string ToString()
    {
        string str = string.Empty;
        foreach (T dd in list)
        {
            str = str + "[" + dd.color + "]" + dd.B;
        }
        return str;
    }
    public List<T> Viev
    {
        get
        {
            return list;
        }
    }
    public T Find(T bb)
    {
        foreach (T dd in list)
        {
            if (dd == bb)
            {
                return dd;
            }
        }
        return null;
    }
    public class T
    {
        public char B;
        public string color;
        public T(char s12, string ccd)
        {
            B = s12;
            color = ccd;
        }
    }
}
public static class Cach
{
    public static GameObject redCross1 { get; set; }
    public static GameObject rope { get; set; }
    public static GameObject ThunderCT { get; set; }
    public static GameObject colossal_steam { get; set; }
    public static GameObject colossal_steam_dmg { get; set; }
    public static GameObject FX_boom1 { get; set; }
    public static GameObject FX_boom1_CT_KICK { get; set; }
    public static GameObject TITAN_VER3_1 { get; set; }
    public static GameObject UI_LabelNameOverHead { get; set; }
    public static GameObject rock { get; set; }
    public static UnityEngine.Object FX_ThunderCT { get; set; }
    public static GameObject FX_FXtitanDie1 { get; set; }
    public static GameObject FX_FXtitanDie { get; set; }
    public static GameObject TITAN_EREN { get; set; }
    public static GameObject AOTTG_HERO_1 { get; set; }
    public static KillInfoComponent UI_KillInfo { get; set; }
    public static GameObject UI_IN_GAME { get; set; }
    public static GameObject MainCamera_mono { get; set; }
    public static UnityEngine.Object levelBottom { get; set; }
    public static UnityEngine.GameObject hitMeat2 { get; set; }
    public static UnityEngine.GameObject hook { get; set; }

    public static UnityEngine.GameObject glass { get; set; }
    public static UnityEngine.GameObject character_face { get; set; }
    public static UnityEngine.GameObject Character_parts_AOTTG_HERO_body { get; set; }
    public static UnityEngine.GameObject Character_parts_character_gun_l { get; set; }
    public static UnityEngine.GameObject character_gun_r { get; set; }
    public static UnityEngine.GameObject character_3dmg_2 { get; set; }
    public static UnityEngine.GameObject character_gun_mag_l { get; set; }
    public static UnityEngine.GameObject character_gun_mag_r { get; set; }
    public static UnityEngine.GameObject character_blade_l { get; set; }
    public static UnityEngine.GameObject character_blade_r { get; set; }
    public static UnityEngine.GameObject character_3dmg { get; set; }
    public static UnityEngine.GameObject character_3dmg_gas_l { get; set; }
    public static UnityEngine.GameObject character_3dmg_gas_r { get; set; }
    public static UnityEngine.GameObject flashlight { get; set; }
    public static UnityEngine.GameObject shotGun_1 { get; set; }
    public static UnityEngine.GameObject shotGun { get; set; }
    public static UnityEngine.GameObject boost_smoke { get; set; }
    public static UnityEngine.GameObject rockThrow { get; set; }
    public static UnityEngine.GameObject boom2 { get; set; }
    public static UnityEngine.GameObject bloodExplore { get; set; }
    public static UnityEngine.GameObject bloodsplatter { get; set; }
    public static UnityEngine.GameObject justSmoke { get; set; }
    public static UnityEngine.GameObject flareBullet1 { get; set; }
    public static UnityEngine.GameObject flareBullet2 { get; set; }
    public static UnityEngine.GameObject flareBullet3 { get; set; }
    public static UnityEngine.GameObject Thunder { get; set; }
    public static UnityEngine.GameObject boom2_eren { get; set; }
    public static UnityEngine.GameObject hitMeatBIG { get; set; }
    public static UnityEngine.GameObject boom6 { get; set; }
    public static UnityEngine.GameObject FXtitanSpawn { get; set; }
    public static UnityEngine.GameObject hitMeat { get; set; }
    public static UnityEngine.GameObject redCross { get; set; }
    public static UnityEngine.GameObject titanNapeMeat { get; set; }
}

public static class Statics
{
    public static AssetBundle CMassets;
}

public class CommandOnPlayer
{
    public static void kick(PhotonPlayer player)
    {
        if (player != null)
        {
            if (player == PhotonNetwork.player)
            {
                 InRoomChat.instance.addLINE(CyanMod.INC.la("error_you_no_kiked"));
            }
            else if (!FengGameManagerMKII.OnPrivateServer && !PhotonNetwork.isMasterClient)
            {
                object[] parameters4 = new object[] { "/kick #" + Convert.ToString(player.ID), LoginFengKAI.player.name };
                FengGameManagerMKII.instance.photonView.RPC("Chat", PhotonTargets.All, parameters4);
            }
            else
            {
                if (FengGameManagerMKII.OnPrivateServer)
                {
                    FengGameManagerMKII.instance.kickPlayerRC(player, false, "");
                }
                else if (PhotonNetwork.isMasterClient)
                {
                    FengGameManagerMKII.instance.kickPlayerRC(player, false, "");
                    cext.mess(player.ishexname + CyanMod.INC.la("kicked_from_the_serv"));
                }
            }
        }
        else
        {
             InRoomChat.instance.addLINE(CyanMod.INC.la("player_not_fond"));
        }
    }

    public static void ban(PhotonPlayer player)
    {
        if (player != null)
        {
            if (player == PhotonNetwork.player)
            {
                 InRoomChat.instance.addLINE(CyanMod.INC.la("error_you_no_kiked"));
            }
            else if (FengGameManagerMKII.OnPrivateServer)
            {
                FengGameManagerMKII.instance.kickPlayerRC(player, true, "");
            }
            else if (PhotonNetwork.isMasterClient)
            {
                FengGameManagerMKII.instance.kickPlayerRC(player, true, "");
                cext.mess(player.ishexname + player.id + CyanMod.INC.la("has_banned"));
            }
        }
        else
        {
             InRoomChat.instance.addLINE(CyanMod.INC.la("player_not_fond"));
        }
    }
}

public class LoadChanglog : UnityEngine.MonoBehaviour
{
    WWW www;
    public static string chengelog = "";
    IEnumerator down()
    {
        www = new WWW("http://attackontitan.ucoz.ru/RCyan_mod/ChanglogCyanMod.txt");
        yield return www;
        if (www.error != null)
        {
            yield break;
        }
        else
        {
            chengelog = www.text;
        }
        yield break;
    }
    void Start()
    {
        if (chengelog == "")
        {
            StartCoroutine(down());
        }
    }
    public void Updated()
    {
        if (chengelog != "")
        {
            GUILayout.Label(chengelog);
            return;
        }
        else
        {
            if (www != null)
            {
                if (www.error != null)
                {
                    GUILayout.Label(CyanMod.INC.la("error_load_change"));
                    if (GUILayout.Button(CyanMod.INC.la("updated_again_change"), GUILayout.Width(150f)))
                    {
                        PanelMain.instance.loadChange = new GameObject().AddComponent<LoadChanglog>();
                        Destroy(base.gameObject);
                    }
                }
                else
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label(LoadCyanModAnim.onlyT, new GUIStyle(), GUILayout.Width(30f), GUILayout.Height(30f));
                    GUILayout.Label(CyanMod.INC.la("download_change"));
                    GUILayout.EndHorizontal();
                    if (GUILayout.Button(CyanMod.INC.la("stoped_download_change"), GUILayout.Width(130f)))
                    {
                        PanelMain.instance.loadChange = new GameObject().AddComponent<LoadChanglog>();
                        Destroy(base.gameObject);
                    }
                }
            }
            else
            {
                GUILayout.Label(CyanMod.INC.la("no_load_change"));
                if (GUILayout.Button(CyanMod.INC.la("load_change"), GUILayout.Width(120f)))
                {
                    PanelMain.instance.loadChange = new GameObject().AddComponent<LoadChanglog>();
                    Destroy(base.gameObject);
                }
            }
        }
    }

}
public class loadedNews : UnityEngine.MonoBehaviour
{
    WWW www;
    public static string chengelog = "";
    IEnumerator down()
    {
        www = new WWW("http://attackontitan.ucoz.ru/RCyan_mod/CyanModNews.txt");
     
        yield return www;

        if (www.error != null)
        {
            yield break;
        }
        else
        {
            chengelog = www.text;
        }
        yield break;
    }
    void Start()
    {
        if (chengelog == "")
        {
            StartCoroutine(down());
        }
    }
    public void Updated()
    {
        if (chengelog != "")
        {
            GUILayout.Label(chengelog);
            return;
        }
        else
        {
            if (www != null)
            {
                if (www.error != null)
                {
                    GUILayout.Label(CyanMod.INC.la("error_load_change"));
                    if (GUILayout.Button(CyanMod.INC.la("updated_again_change"), GUILayout.Width(150f)))
                    {
                        PanelMain.instance.loadChange = new GameObject().AddComponent<LoadChanglog>();
                        Destroy(base.gameObject);
                    }
                }
                else
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label(LoadCyanModAnim.onlyT, new GUIStyle(), GUILayout.Width(30f), GUILayout.Height(30f));
                    GUILayout.Label(CyanMod.INC.la("download_change"));
                    GUILayout.EndHorizontal();
                    if (GUILayout.Button(CyanMod.INC.la("stoped_download_change"), GUILayout.Width(130f)))
                    {
                        PanelMain.instance.loadChange = new GameObject().AddComponent<LoadChanglog>();
                        Destroy(base.gameObject);
                    }
                }

            }
            else
            {
                GUILayout.Label(CyanMod.INC.la("no_load_change"));
                if (GUILayout.Button(CyanMod.INC.la("load_change"), GUILayout.Width(120f)))
                {
                    PanelMain.instance.loadChange = new GameObject().AddComponent<LoadChanglog>();
                    Destroy(base.gameObject);
                }
            }
        }
    }
}