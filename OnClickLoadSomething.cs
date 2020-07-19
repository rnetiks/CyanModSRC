using System;
using UnityEngine;

public class OnClickLoadSomething : MonoBehaviour
{
    public string ResourceToLoad;
    public ResourceTypeOption ResourceTypeToLoad;

    public void OnClick()
    {
        switch (this.ResourceTypeToLoad)
        {
            case ResourceTypeOption.Scene:
                Application.LoadLevel(this.ResourceToLoad);
                return;

            case ResourceTypeOption.Web:
                Application.OpenURL(this.ResourceToLoad);
                return;
        }
    }

    public enum ResourceTypeOption : byte
    {
        Scene = 0,
        Web = 1
    }
}

