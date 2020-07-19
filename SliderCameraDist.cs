using System;
using UnityEngine;

public class SliderCameraDist : MonoBehaviour
{
    private bool init;

    private void OnSliderChange(float value)
    {
     
        if (!this.init)
        {
            UISlider uis = base.gameObject.GetComponent<UISlider>();
            this.init = true;
            uis.sliderValue = FengGameManagerMKII.instance.distanceSlider;
               
        }
        else
            
        {
            PrefersCyan.SetFloat("floatcameraDistance", FengGameManagerMKII.instance.distanceSlider);
        }
        IN_GAME_MAIN_CAMERA.cameraDistance = 0.3f + FengGameManagerMKII.instance.distanceSlider;
    }
}

