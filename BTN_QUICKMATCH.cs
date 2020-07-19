using System;
using UnityEngine;

public class BTN_QUICKMATCH : MonoBehaviour
{
    private void OnClick()
    {
        NGUITools.SetActive(base.transform.parent.gameObject, false);
        NGUITools.SetActive(UIMainReferences.instance.panelMultiSet, true);
        PhotonNetwork.offlineMode = true;
    }

    private void Start()
    {


    }
}

