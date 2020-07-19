using System;
using UnityEngine;

public class BTN_PAUSE_MENU_QUIT : MonoBehaviour
{
    private void OnClick()
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
        FengGameManagerMKII.instance.gameStart = false;
         FengGameManagerMKII.instance.MenuOn = false;
        UnityEngine.Object.Destroy(CyanMod.CachingsGM.Find("MultiplayerManager"));
        Application.LoadLevel("menu");
    }
}

