using System;
using UnityEngine;

public class SupportLogger : MonoBehaviour
{
    public bool LogTrafficStats = true;

    public void Start()
    {
        GameObject gm = CyanMod.CachingsGM.Find("PunSupportLogger");
        if (gm == null)
        {
            gm = new GameObject("PunSupportLogger");
            UnityEngine.Object.DontDestroyOnLoad(gm);
            gm.AddComponent<SupportLogging>().LogTrafficStats = this.LogTrafficStats;
        }
    }
}

