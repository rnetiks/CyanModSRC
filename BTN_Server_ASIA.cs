using System;
using UnityEngine;

public class BTN_Server_ASIA : MonoBehaviour
{
    private void OnClick()
    {
        FengGameManagerMKII.settings[399] = 2;
        CyanMod.INC.Connected();
    }
}

