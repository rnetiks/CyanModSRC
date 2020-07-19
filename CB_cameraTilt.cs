using System;
using UnityEngine;

public class CB_cameraTilt : MonoBehaviour
{
    private bool init;

    private void OnActivate(bool result)
    {
        if (!this.init)
        {
            this.init = true;
            if ((int)FengGameManagerMKII.settings[321] == 0)
            {
                base.gameObject.GetComponent<UICheckbox>().isChecked = (int)FengGameManagerMKII.settings[321] == 1;
            }
            else
            {
                FengGameManagerMKII.settings[321] = 1;
            }
        }
        else
        {
            if (!result)
            {
                FengGameManagerMKII.settings[321] = 0;
            }
            else
            {
                FengGameManagerMKII.settings[321] = 1;
            }
        }
        IN_GAME_MAIN_CAMERA.cameraTilt = (int)FengGameManagerMKII.settings[321];
    }
}

