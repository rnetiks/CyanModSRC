using System;
using UnityEngine;

public class DaylightChange : MonoBehaviour
{
    private void OnSelectionChange()
    {
        UIPopupList uilist = base.GetComponent<UIPopupList>();
        if (uilist.selection == "DAY")
        {
            IN_GAME_MAIN_CAMERA.dayLight = DayLight.Day;
        }
        if (uilist.selection == "DAWN")
        {
            IN_GAME_MAIN_CAMERA.dayLight = DayLight.Dawn;
        }
        if (uilist.selection == "NIGHT")
        {
            IN_GAME_MAIN_CAMERA.dayLight = DayLight.Night;
        }
    }
}

