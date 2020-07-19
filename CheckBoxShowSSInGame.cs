using System;
using UnityEngine;

public class CheckBoxShowSSInGame : MonoBehaviour
{
    private bool init;

    private void OnActivate(bool yes)
    {
        if (this.init)
        {
            if (yes)
            {
                FengGameManagerMKII.settings[323] = 1;
            }
            else
            {
                FengGameManagerMKII.settings[323] = 0;
            }
        }
    }

    private void Start()
    {
        this.init = true;
        UICheckbox cb = base.GetComponent<UICheckbox>();
        if ((int)FengGameManagerMKII.settings[323]  == 1)
            {
                cb.isChecked = true;
            }
            else
            {
                cb.isChecked = false;
            }
   
    }
}

