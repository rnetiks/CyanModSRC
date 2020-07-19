using System;
using UnityEngine;

public class BTN_toPublicServer : MonoBehaviour
{
    private void OnClick()
    {
        NGUITools.SetActive(base.transform.parent.gameObject, false);
        NGUITools.SetActive(UIMainReferences.instance.panelMultiROOM, true);
    }
}

