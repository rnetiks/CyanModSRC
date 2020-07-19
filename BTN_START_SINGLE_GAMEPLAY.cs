using System;
using UnityEngine;

public class BTN_START_SINGLE_GAMEPLAY : MonoBehaviour
{
    private void OnClick()
    {
        string selection = CyanMod.CachingsGM.Find("PopupListMap").GetComponent<UIPopupList>().selection;
        string str2 = CyanMod.CachingsGM.Find("PopupListCharacter").GetComponent<UIPopupList>().selection;
        int num = !CyanMod.CachingsGM.Find("CheckboxHard").GetComponent<UICheckbox>().isChecked ? (!CyanMod.CachingsGM.Find("CheckboxAbnormal").GetComponent<UICheckbox>().isChecked ? 1 : 3) : 2;
        IN_GAME_MAIN_CAMERA.difficulty = (DIFFICULTY)num;
        IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.SINGLE;
        IN_GAME_MAIN_CAMERA.singleCharacter = str2.ToUpper();
        if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.TPS || IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.oldTPS)
        {
            Screen.lockCursor = true;
        }
        Screen.showCursor = false;
        FengGameManagerMKII.level = selection;

        FengGameManagerMKII.settings[410] = selection;
        FengGameManagerMKII.settings[411] = num;
        FengGameManagerMKII.instance.saves();
        Application.LoadLevel(LevelInfo.getInfo(selection).mapName);
    }
    
}

