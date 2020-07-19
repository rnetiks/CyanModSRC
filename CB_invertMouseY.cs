using System;
using UnityEngine;

public class CB_invertMouseY : MonoBehaviour
{
    private bool init;

    private void OnActivate(bool result)
    {
        if (!this.init)
        {
            this.init = true;
            if ((int)FengGameManagerMKII.settings[322] == 1)
            {
                base.gameObject.GetComponent<UICheckbox>().isChecked = (int)FengGameManagerMKII.settings[322] == -1;
            }
            else
            {
                FengGameManagerMKII.settings[322] = 1;
            }
        }
        else
        {
            if (!result)
            {
                FengGameManagerMKII.settings[322] = 1;
            }
            else
            {
                FengGameManagerMKII.settings[322] = -1;
            }
        }
        IN_GAME_MAIN_CAMERA.invertY = (int)FengGameManagerMKII.settings[322];
    }
}

