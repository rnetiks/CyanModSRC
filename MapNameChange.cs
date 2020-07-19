using System;
using UnityEngine;

public class MapNameChange : MonoBehaviour
{
    private void OnSelectionChange()
    {
        UIPopupList list = base.GetComponent<UIPopupList>();
        LevelInfo info = LevelInfo.getInfo(list.selection);
        if (info != null)
        {
            CyanMod.CachingsGM.Find("LabelLevelInfo").GetComponent<UILabel>().text = info.desc;
        }
        if (!list.items.Contains("Custom"))
        {
            list.items.Add("Custom");
            list.textScale *= 0.8f;
        }
        if (!list.items.Contains("Custom (No PT)"))
        {
            list.items.Add("Custom (No PT)");
        }
    }
}

