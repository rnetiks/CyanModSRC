using System;
using UnityEngine;

public class PopListCamera : MonoBehaviour
{
    UIPopupList uilist;
    private void Awake()
    {
        uilist =  base.GetComponent<UIPopupList>();
        uilist.selection = PrefersCyan.GetString("string_cameraType", "TPS");
        
    }

    private void OnSelectionChange()
    {
    
        if (uilist.selection == "ORIGINAL")
        {
            IN_GAME_MAIN_CAMERA.cameraMode = CAMERA_TYPE.ORIGINAL;
        }
        if (uilist.selection == "WOW")
        {
            IN_GAME_MAIN_CAMERA.cameraMode = CAMERA_TYPE.WOW;
        }
        if (uilist.selection == "TPS")
        {
            IN_GAME_MAIN_CAMERA.cameraMode = CAMERA_TYPE.TPS;
        }
        if (uilist.selection == "oldTPS")
        {
            IN_GAME_MAIN_CAMERA.cameraMode = CAMERA_TYPE.oldTPS;
        }
        PrefersCyan.SetString("string_cameraType", uilist.selection);
    }
    void Start()
    {
        if (!uilist.items.Contains("oldTPS"))
        {
            uilist.items.Add("oldTPS");
        }
    }
}

