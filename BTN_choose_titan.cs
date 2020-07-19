using ExitGames.Client.Photon;
using System;
using UnityEngine;

public class BTN_choose_titan : MonoBehaviour
{
    private void OnClick()
    {
        if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_AHSS)
        {
            string id = "AHSS";
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
            NGUITools.SetActive(FengGameManagerMKII.instance.uiT.panels[3], false);
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
            string selection = CyanMod.CachingsGM.Find<UIPopupList>("PopupListCharacterTITAN").selection;
            NGUITools.SetActive(base.transform.parent.gameObject, false);
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
            NGUITools.SetActive(FengGameManagerMKII.instance.uiT.panels[3], false);
            IN_GAME_MAIN_CAMERA.usingTitan = true;
             IN_GAME_MAIN_CAMERA.instance.setHUDposition();
        }
    }

    private void Start()
    {
        if (!LevelInfo.getInfo(FengGameManagerMKII.level).teamTitan)
        {
            base.gameObject.GetComponent<UIButton>().isEnabled = false;
        }
    }
}

