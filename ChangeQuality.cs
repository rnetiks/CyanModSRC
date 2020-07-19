using System;
using UnityEngine;

public class ChangeQuality : MonoBehaviour
{
    private bool init;
    public static bool isTiltShiftOn;

    private void OnSliderChange()
    {
        if (!this.init)
        {
            this.init = true;
            base.gameObject.GetComponent<UISlider>().sliderValue = FengGameManagerMKII.instance.qualitySlider; 
        }
    }

    public static void setCurrentQuality()
    {

        setQuality(FengGameManagerMKII.instance.qualitySlider);
        
    }

    private static void setQuality(float val)
    {
        if (val < 0.167f)
        {
            QualitySettings.SetQualityLevel(0, true);
        }
        else if (val < 0.33f)
        {
            QualitySettings.SetQualityLevel(1, true);
        }
        else if (val < 0.5f)
        {
            QualitySettings.SetQualityLevel(2, true);
        }
        else if (val < 0.67f)
        {
            QualitySettings.SetQualityLevel(3, true);
        }
        else if (val < 0.83f)
        {
            QualitySettings.SetQualityLevel(4, true);
        }
        else if (val <= 1f)
        {
            QualitySettings.SetQualityLevel(5, true);
        }
        if (val < 0.9f)
        {
            turnOffTiltShift();
        }
        else
        {
            turnOnTiltShift();
        }
    }

    public static void turnOffTiltShift()
    {
        isTiltShiftOn = false;
        if (IN_GAME_MAIN_CAMERA.instance != null)
        {
            IN_GAME_MAIN_CAMERA.instance.tiltShift.enabled = false;
        }
    }

    public static void turnOnTiltShift()
    {
        isTiltShiftOn = true;
        if (IN_GAME_MAIN_CAMERA.instance != null)
        {
            IN_GAME_MAIN_CAMERA.instance.tiltShift.enabled = true;
        }
    }
}

