using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[ExecuteInEditMode, RequireComponent(typeof(UIWidget)), AddComponentMenu("NGUI/Examples/Set Color on Selection")]
public class SetColorOnSelection : MonoBehaviour
{
    [CompilerGenerated]
    private static Dictionary<string, int> f__switchSmap4;
    private UIWidget mWidget;

    private void OnSelectionChange(string val)
    {
        if (this.mWidget == null)
        {
            this.mWidget = base.GetComponent<UIWidget>();
        }
        string key = val;
        if (key != null)
        {
            int num;
            if (f__switchSmap4 == null)
            {
                Dictionary<string, int> dictionary = new Dictionary<string, int>(7) {
                    { 
                        "White",
                        0
                    },
                    { 
                        "Red",
                        1
                    },
                    { 
                        "Green",
                        2
                    },
                    { 
                        "Blue",
                        3
                    },
                    { 
                        "Yellow",
                        4
                    },
                    { 
                        "Cyan",
                        5
                    },
                    { 
                        "Magenta",
                        6
                    }
                };
                f__switchSmap4 = dictionary;
            }
            if (f__switchSmap4.TryGetValue(key, out num))
            {
                switch (num)
                {
                    case 0:
                        this.mWidget.color = Color.white;
                        return;

                    case 1:
                        this.mWidget.color = Color.red;
                        return;

                    case 2:
                        this.mWidget.color = Color.green;
                        return;

                    case 3:
                        this.mWidget.color = Color.blue;
                        return;

                    case 4:
                        this.mWidget.color = Color.yellow;
                        return;

                    case 5:
                        this.mWidget.color = Color.cyan;
                        return;

                    case 6:
                        this.mWidget.color = Color.magenta;
                        break;

                    default:
                        return;
                }
            }
        }
    }
}

