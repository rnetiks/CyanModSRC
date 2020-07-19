using ExitGames.Client.Photon;
using System;
using UnityEngine;

public class BTN_choose_human : MonoBehaviour
{
    public bool isPlayerAllDead()
    {
        int num = 0;
        int num2 = 0;
        foreach (PhotonPlayer player in PhotonNetwork.playerList)
        {
            if (((int) player.isTitan) == 1)
            {
                num++;
                if ((bool) player.dead)
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

    private void OnClick()
    {
        string selection = CyanMod.CachingsGM.Find<UIPopupList>("PopupListCharacterHUMAN").selection;
        NGUITools.SetActive(FengGameManagerMKII.instance.uiT.panels[0], true);
        FengGameManagerMKII.instance.needChooseSide = false;
        if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE)
        {
            FengGameManagerMKII.instance.checkpoint = CyanMod.CachingsGM.Find("PVPchkPtH");
        }
        if (!PhotonNetwork.isMasterClient && (FengGameManagerMKII.instance.roundTime > 60f))
        {
            if (!this.isPlayerAllDead2())
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
            if (this.isPlayerAllDead2())
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
        NGUITools.SetActive(FengGameManagerMKII.instance.uiT.panels[3], false);
        IN_GAME_MAIN_CAMERA.usingTitan = false;
         IN_GAME_MAIN_CAMERA.instance.setHUDposition();
        PhotonNetwork.player.character = selection;
    }
}

