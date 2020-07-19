using System;
using UnityEngine;

public class BTN_leave_lobby : MonoBehaviour
{
    private void OnClick()
    {
        NGUITools.SetActive(base.transform.parent.gameObject, false);
        NGUITools.SetActive(UIMainReferences.instance.panelMultiStart, true);
        PhotonNetwork.Disconnect();
    }
}

