using System;
using UnityEngine;

public class CheckBoxCamera : MonoBehaviour
{
    public new CAMERA_TYPE camera;

    private void OnSelectionChange(bool yes)
    {
        if (yes)
        {
            IN_GAME_MAIN_CAMERA.cameraMode = this.camera;
            PrefersCyan.SetInt("int_camera_type", (int)IN_GAME_MAIN_CAMERA.cameraMode);
        }
    }

    private void Start()
    {
      

        if (this.camera.ToString().ToUpper() == ((CAMERA_TYPE)PrefersCyan.GetInt("int_camera_type",0)).ToString().ToUpper())
            {
                base.GetComponent<UICheckbox>().isChecked = true;
            }
            else
            {
                base.GetComponent<UICheckbox>().isChecked = false;
            }
        
    }
}

