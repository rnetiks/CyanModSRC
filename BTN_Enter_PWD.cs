using System;
using UnityEngine;

public class BTN_Enter_PWD : MonoBehaviour
{
    private void OnClick()
    {
        string text = CyanMod.CachingsGM.Find("InputEnterPWD").GetComponent<UIInput>().label.text;
        SimpleAES eaes = new SimpleAES();
        if (text != eaes.Decrypt(PanelMultiJoinPWD.Password))
        {
            PhotonNetwork.JoinRoom(PanelMultiJoinPWD.roomName);
        }
        else
        {
            NGUITools.SetActive(UIMainReferences.instance.PanelMultiPWD, false);
            NGUITools.SetActive(UIMainReferences.instance.panelMultiROOM, true);
            CyanMod.CachingsGM.Find("PanelMultiROOM").GetComponent<PanelMultiJoin>().refresh();
        }
    }
}

