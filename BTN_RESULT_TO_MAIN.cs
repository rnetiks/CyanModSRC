﻿using System;
using UnityEngine;

public class BTN_RESULT_TO_MAIN : MonoBehaviour
{
    private void OnClick()
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
}

