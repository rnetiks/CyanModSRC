using System.Diagnostics;
using UnityEngine;
using CyanMod;

public class UIMainReferences : MonoBehaviour
{
    public GameObject panelMain;
    public GameObject panelOption;
    public GameObject panelMultiROOM;
    public GameObject PanelMultiJoinPrivate;
    public GameObject PanelMultiWait;
    public GameObject PanelDisconnect;
    public GameObject panelMultiSet;
    public GameObject panelMultiStart;
    public GameObject panelCredits;
    public GameObject panelSingleSet;
    public GameObject PanelMultiPWD;
    public GameObject PanelSnapShot;
    public GameObject testpanel;
    public static bool isGAMEFirstLaunch = true;
    public static string version = "01042015";
    public static string fengVersion;
    public static string CyanModVers = "";
    public static UIMainReferences instance;
    public static INC MAIN;
    public static Material sky_Night;
    void Awake()
    {
        instance = this;
    }
    void OnDestroy()
    {
        instance = null;
    }
    private void Start()
    {
        instance = this;
        if (sky_Night == null)
        {
            sky_Night = Resources.Load<IN_GAME_MAIN_CAMERA>("MainCamera_mono").skyBoxNIGHT;
        }
        Camera.main.GetComponent<Skybox>().material = sky_Night;
        UIMainReferences.fengVersion = "01042015";
        NGUITools.SetActive(this.panelMain, true);
        FileVersionInfo VersionInfo = FileVersionInfo.GetVersionInfo(Application.dataPath + "/Managed/Assembly-CSharp.dll");
        UIMainReferences.CyanModVers = System.Convert.ToString(VersionInfo.ProductVersion);
        if (UIMainReferences.isGAMEFirstLaunch)
        {
            IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.STOP;
            IN_GAME_MAIN_CAMERA.invertY = 1;
            UnityEngine.Object.DontDestroyOnLoad(global::UIMainReferences.MAIN = new GameObject("Cyan_mod").AddComponent<INC>());
            UIMainReferences.version = UIMainReferences.fengVersion;
            UIMainReferences.isGAMEFirstLaunch = false;
            GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("InputManagerController"));
            gameObject.name = "InputManagerController";
            UnityEngine.Object.DontDestroyOnLoad(gameObject);
            FengGameManagerMKII.s = new string[]{"verified343","hair","character_eye","glass","character_face","character_head","character_hand","character_body","character_arm","character_leg","character_chest","character_cape","character_brand","character_3dmg","r","character_blade_l","character_3dmg_gas_r","character_blade_r","3dmg_smoke","HORSE","hair","body_001","Cube","Plane_031","mikasa_asset","character_cap_","character_gun"};
			
            FengGameManagerMKII.loginstate = 0;

        }
    }
}
