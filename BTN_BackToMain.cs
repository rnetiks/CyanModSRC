using System;
using UnityEngine;

public class BTN_BackToMain : MonoBehaviour
{
    private void OnClick()
    {
        NGUITools.SetActive(base.transform.parent.gameObject, false);
        NGUITools.SetActive(UIMainReferences.instance.panelMain.gameObject, true);
         FengGameManagerMKII.instance.MenuOn = false;
        PhotonNetwork.Disconnect();
        PhotonNetwork.offlineMode = false;
    }
}
