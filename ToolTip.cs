using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ToolTip
{
    public static void tip(Vector2 r, Texture2D tx = null)
    {
        Tooltip(GUILayoutUtility.GetLastRect(), tx, r);
    }
    public static bool tip()
    {
        if (GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition))
        {
            return true;
        }
        return false;
    }
   
    static void Tooltip(Rect rect, Texture2D tx, Vector2 r)
    {
        if (rect.Contains(Event.current.mousePosition))
        {
            if (tx != null)
            {
                GUI.DrawTexture(new Rect(r.x, r.y, tx.width, tx.height), tx);
            }
        }
    }
}

