using System;
using UnityEngine;

public class BTN_PAUSE_MENU_CONTINUE : MonoBehaviour
{
    private void OnClick()
    {
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
        {
            Time.timeScale = 1f;
        }
        GameObject obj2 = CyanMod.CachingsGM.Find("UI_IN_GAME");
        NGUITools.SetActive(obj2.GetComponent<UIReferArray>().panels[0], true);
        NGUITools.SetActive(obj2.GetComponent<UIReferArray>().panels[1], false);
        NGUITools.SetActive(obj2.GetComponent<UIReferArray>().panels[2], false);
        NGUITools.SetActive(obj2.GetComponent<UIReferArray>().panels[3], false);
        if (! IN_GAME_MAIN_CAMERA.instance.enabled)
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
    }
}

