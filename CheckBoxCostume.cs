using System;
using UnityEngine;

public class CheckBoxCostume : MonoBehaviour
{
    public static int costumeSet = 1;
    public int set = 1;

    private void OnActivate(bool yes)
    {
    }
    void Start()
    {
        CheckBoxCostume.costumeSet = PrefersCyan.GetInt("int_costume_value", 1);
    }
}

