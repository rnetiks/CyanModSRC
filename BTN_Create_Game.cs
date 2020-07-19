using System;
using UnityEngine;

public class BTN_Create_Game : MonoBehaviour
{
    private void OnClick()
    {
        NGUITools.SetActive(base.transform.parent.gameObject, false);
        NGUITools.SetActive(UIMainReferences.instance.panelMultiSet, true);
    }
}

