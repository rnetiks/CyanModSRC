using System;
using UnityEngine;

public class BTN_ToServer : MonoBehaviour
{
    private void OnClick()
    {
        NGUITools.SetActive(base.transform.parent.gameObject, false);
        NGUITools.SetActive(UIMainReferences.instance.panelMultiSet, true);
    }
}

