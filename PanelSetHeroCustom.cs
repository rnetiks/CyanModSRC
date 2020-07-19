using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using CyanMod;

public class PanelSetHeroCustom : UnityEngine.MonoBehaviour
{
    public static PanelSetHeroCustom instance;
    void Awake()
    {
        instance = this;
    }
    void OnDestroy()
    {
        instance = null;
    }
    string[] CameraType = new string[] { "ORIGINAL", "WOW", "TPS", "oldTPS" };
    public static bool activet
    {
        get
        {
            if (instance != null && instance.enabled)
            {
                return true;
            }
            return false;
        }
    }
    int isTitan = 0;
    HeroStat stat;
    string id;
    void OnGUI()
    {
        GUI.backgroundColor = INC.gui_color;
        GUILayout.BeginArea(new Rect(0, 0, 300, Screen.height));
        GUILayout.BeginVertical(GUI.skin.box);
        int ist = isTitan;
        isTitan = GUILayout.SelectionGrid(isTitan, new string[] { "HUMAN", id }, 2);
        if (ist != isTitan)
        {
            if (!LevelInfo.getInfo(FengGameManagerMKII.level).teamTitan)
            {
                isTitan = 0;
            }
        }
        if (isTitan == 0)
        {
            GUIStyle style = new GUIStyle(GUI.skin.button);
            foreach (var stats in HeroStat.stats)
            {
                if (stats.Key != "CUSTOM_DEFAULT")
                {
                    if (stat != null)
                    {
                        if (stat.name == stats.Key)
                        {
                            style.normal = GUI.skin.button.active;
                        }
                        else
                        {
                            style.normal = GUI.skin.button.normal;
                        }
                    }
                    if (GUILayout.Button(stats.Key, style))
                    {
                        IN_GAME_MAIN_CAMERA.singleCharacter = stats.Key;
                        if (((stats.Key != "Set 1") && (stats.Key != "Set 2")) && (stats.Key != "Set 3"))
                        {
                            stat = HeroStat.getInfo(stats.Key);
                        }
                        else
                        {
                            HeroCostume costume = CostumeConeveter.LocalDataToHeroCostume(stats.Key);
                            if (costume == null)
                            {
                                stat = new HeroStat();
                            }
                            else
                            {
                                stat = costume.stat;
                            }
                        }
                        if (PanelEnterGame.instance != null)
                        {
                            PanelEnterGame.instance.to_InfoHero(stat);
                        }
                        stat.name = stats.Key;
                    }
                }
            }
            if (stat != null)
            {
                GUILayout.Label(stat.name);
                GUILayout.BeginHorizontal();
                GUILayout.Label("SPD:" + stat.SPD);
                GUILayout.Label("ACL:" + stat.ACL);
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                GUILayout.Label("BLA:" + stat.BLA);
                GUILayout.Label("GAS:" + stat.GAS);
                GUILayout.EndHorizontal();
            }
            string[] cos = new string[] { "Cos 1", "Cos 2", "Cos 3" };
            GUILayout.BeginHorizontal();
            GUIStyle style3 = new GUIStyle(GUI.skin.button);
            for (int s = 0; s < cos.Length; s++)
            {
                string de = cos[s];
                if (CheckBoxCostume.costumeSet - 1 == s)
                {
                    style3.normal = GUI.skin.button.active;
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


        GUILayout.Label(CyanMod.INC.la("camera_type"));

        IN_GAME_MAIN_CAMERA.cameraMode = (CAMERA_TYPE)GUILayout.SelectionGrid((int)IN_GAME_MAIN_CAMERA.cameraMode, CameraType, 4);

        if (GUILayout.Button(INC.la("lets_go")))
        {
            if (isTitan == 0)
            {
                string selection = stat.name;
                NGUITools.SetActive(FengGameManagerMKII.instance.uiT.panels[0], true);
                FengGameManagerMKII.instance.needChooseSide = false;
                if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE)
                {
                    FengGameManagerMKII.instance.checkpoint = CyanMod.CachingsGM.Find("PVPchkPtH");
                }
                if (!PhotonNetwork.isMasterClient && (FengGameManagerMKII.instance.roundTime > 60f))
                {
                    if (FengGameManagerMKII.instance.heroes.Count != 0)
                    {
                        FengGameManagerMKII.instance.NOTSpawnPlayer(selection);
                    }
                    else
                    {
                        FengGameManagerMKII.instance.NOTSpawnPlayer(selection);
                        FengGameManagerMKII.instance.photonView.RPC("restartGameByClient", PhotonTargets.MasterClient, new object[0]);
                    }
                }
                else if (((IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.BOSS_FIGHT_CT) || (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.TROST)) || (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE))
                {
                    if (FengGameManagerMKII.instance.heroes.Count != 0)
                    {
                        FengGameManagerMKII.instance.NOTSpawnPlayer(selection);
                        FengGameManagerMKII.instance.photonView.RPC("restartGameByClient", PhotonTargets.MasterClient, new object[0]);
                    }
                    else
                    {
                        FengGameManagerMKII.instance.SpawnPlayer(selection, "playerRespawn");
                    }
                }
                else
                {
                    FengGameManagerMKII.instance.SpawnPlayer(selection, "playerRespawn");
                }
                NGUITools.SetActive(FengGameManagerMKII.instance.uiT.panels[1], false);
                NGUITools.SetActive(FengGameManagerMKII.instance.uiT.panels[2], false);
                instance.enabled = false;

                IN_GAME_MAIN_CAMERA.usingTitan = false;
                IN_GAME_MAIN_CAMERA.instance.setHUDposition();
                PhotonNetwork.player.character = selection;
            }
            else
            {
                if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_AHSS)
                {
                    id = "AHSS";
                    NGUITools.SetActive(FengGameManagerMKII.instance.uiT.panels[0], true);
                    FengGameManagerMKII.instance.needChooseSide = false;
                    if (!PhotonNetwork.isMasterClient && (FengGameManagerMKII.instance.roundTime > 60f))
                    {
                        FengGameManagerMKII.instance.NOTSpawnPlayer(id);
                        FengGameManagerMKII.instance.photonView.RPC("restartGameByClient", PhotonTargets.MasterClient, new object[0]);
                    }
                    else
                    {
                        FengGameManagerMKII.instance.SpawnPlayer(id, "playerRespawn2");
                    }
                    NGUITools.SetActive(FengGameManagerMKII.instance.uiT.panels[1], false);
                    NGUITools.SetActive(FengGameManagerMKII.instance.uiT.panels[2], false);
                    instance.enabled = false;
                    IN_GAME_MAIN_CAMERA.usingTitan = false;
                    IN_GAME_MAIN_CAMERA.instance.setHUDposition();

                    PhotonNetwork.player.character = id;
                }
                else
                {
                    if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE)
                    {
                        FengGameManagerMKII.instance.checkpoint = CyanMod.CachingsGM.Find("PVPchkPtT");
                    }
                    string selection = "RANDOM";
                    NGUITools.SetActive(FengGameManagerMKII.instance.uiT.panels[0], true);
                    if ((!PhotonNetwork.isMasterClient && (FengGameManagerMKII.instance.roundTime > 60f)) || FengGameManagerMKII.instance.justSuicide)
                    {
                        FengGameManagerMKII.instance.justSuicide = false;
                        FengGameManagerMKII.instance.NOTSpawnNonAITitan(selection);
                    }
                    else
                    {
                        FengGameManagerMKII.instance.SpawnNonAITitan2(selection, "titanRespawn");
                    }
                    FengGameManagerMKII.instance.needChooseSide = false;
                    NGUITools.SetActive(FengGameManagerMKII.instance.uiT.panels[1], false);
                    NGUITools.SetActive(FengGameManagerMKII.instance.uiT.panels[2], false);
                    instance.enabled = false;
                    IN_GAME_MAIN_CAMERA.usingTitan = true;
                    IN_GAME_MAIN_CAMERA.instance.setHUDposition();
                }

            }
            FengGameManagerMKII.instance.saves();
        }
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
    void Start()
    {
        CheckBoxCostume.costumeSet = (int)FengGameManagerMKII.settings[412];
        id = "TITAN";
        if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_AHSS)
        {
            id = "AHSS";
        }
        HeroStat.initDATA();
        if (((IN_GAME_MAIN_CAMERA.singleCharacter != "Set 1") && (IN_GAME_MAIN_CAMERA.singleCharacter != "Set 2")) && (IN_GAME_MAIN_CAMERA.singleCharacter != "Set 3"))
        {
            stat = HeroStat.getInfo(IN_GAME_MAIN_CAMERA.singleCharacter);
        }
        else
        {
            HeroCostume costume = CostumeConeveter.LocalDataToHeroCostume(IN_GAME_MAIN_CAMERA.singleCharacter);
            if (costume == null)
            {
                stat = new HeroStat();
            }
            else
            {
                stat = costume.stat;
            }
            stat.name = IN_GAME_MAIN_CAMERA.singleCharacter;
        }
        if (PanelEnterGame.instance != null)
        {
            PanelEnterGame.instance.to_InfoHero(stat);
        }
    }
}

