using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ChekNameObject : UnityEngine.MonoBehaviour
{
    GameObject target;
    TextMesh text;
    Font font;
    void Start()
    {
        target = base.gameObject;
        if (this.font == null)
        {
            this.font = (Font)Resources.FindObjectsOfTypeAll(typeof(Font))[0];
        }
        target.AddComponent<MeshRenderer>().material = this.font.material;
        text = target.AddComponent<TextMesh>();
        text.font = this.font;
        text.fontSize = 0;
        text.anchor = TextAnchor.MiddleCenter;
        text.text = base.gameObject.name;
    }
}

