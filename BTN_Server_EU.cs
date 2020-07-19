using System;
using UnityEngine;

public class BTN_Server_EU : MonoBehaviour
{
    private void OnClick()
    {
        FengGameManagerMKII.settings[399] = 0;
        CyanMod.INC.Conected();
    }
}

