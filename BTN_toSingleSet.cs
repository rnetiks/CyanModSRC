using System;
using UnityEngine;

public class BTN_toSingleSet : MonoBehaviour
{
    private void OnClick()
    {
        NGUITools.SetActive(base.transform.parent.gameObject, false);
        NGUITools.SetActive(UIMainReferences.instance.panelSingleSet, true);
    }

    private void Start()
    {
    }
}

