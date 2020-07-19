﻿using System;
using UnityEngine;

public class BTN_START_MULTI_SERVER : MonoBehaviour
{
    private void OnClick()
    {
        string text = CyanMod.CachingsGM.Find("InputServerName").GetComponent<UIInput>().label.text;
        int maxPlayers = int.Parse(CyanMod.CachingsGM.Find("InputMaxPlayer").GetComponent<UIInput>().label.text);
        int num2 = int.Parse(CyanMod.CachingsGM.Find("InputMaxTime").GetComponent<UIInput>().label.text);
        string selection = CyanMod.CachingsGM.Find("PopupListMap").GetComponent<UIPopupList>().selection;
        string str3 = !CyanMod.CachingsGM.Find("CheckboxHard").GetComponent<UICheckbox>().isChecked ? (!CyanMod.CachingsGM.Find("CheckboxAbnormal").GetComponent<UICheckbox>().isChecked ? "normal" : "abnormal") : "hard";
        string str4 = string.Empty;
        if (IN_GAME_MAIN_CAMERA.dayLight == DayLight.Day)
        {
            str4 = "day";
        }
        if (IN_GAME_MAIN_CAMERA.dayLight == DayLight.Dawn)
        {
            str4 = "dawn";
        }
        if (IN_GAME_MAIN_CAMERA.dayLight == DayLight.Night)
        {
            str4 = "night";
        }
        string unencrypted = CyanMod.CachingsGM.Find("InputStartServerPWD").GetComponent<UIInput>().label.text;
        if (unencrypted.Length > 0)
        {
            unencrypted = new SimpleAES().Encrypt(unencrypted);
        }
        PhotonNetwork.CreateRoom(string.Concat(new object[] { text, "`", selection, "`", str3, "`", num2, "`", str4, "`", unencrypted, "`", UnityEngine.Random.Range(0, 0xc350) }), true, true, maxPlayers);
    }
}

