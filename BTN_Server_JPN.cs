using System;
using UnityEngine;

public class BTN_Server_JPN : MonoBehaviour
{
    private void OnClick()
    {
        FengGameManagerMKII.settings[399] = 3;
        CyanMod.INC.Conected();
    }
}

