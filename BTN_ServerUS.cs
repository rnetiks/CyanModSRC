using System;
using UnityEngine;

public class BTN_ServerUS : MonoBehaviour
{
    private void OnClick()
    {
        FengGameManagerMKII.settings[399] = 1;
        CyanMod.INC.Connected();
    }
}

