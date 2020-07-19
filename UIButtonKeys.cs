using System;
using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Button Keys"), RequireComponent(typeof(Collider))]
public class UIButtonKeys : MonoBehaviour
{
    public UIButtonKeys selectOnClick;
    public UIButtonKeys selectOnDown;
    public UIButtonKeys selectOnLeft;
    public UIButtonKeys selectOnRight;
    public UIButtonKeys selectOnUp;
    public bool startsSelected;

    private void OnClick()
    {
        if (base.enabled && (this.selectOnClick != null))
        {
            UICamera.selectedObject = this.selectOnClick.gameObject;
        }
    }

    private void OnKey(KeyCode key)
    {
        if (base.enabled && NGUITools.GetActive(base.gameObject))
        {
            switch (key)
            {
                case KeyCode.UpArrow:
                    if (this.selectOnUp == null)
                    {
                        break;
                    }
                    UICamera.selectedObject = this.selectOnUp.gameObject;
                    return;

                case KeyCode.DownArrow:
                    if (this.selectOnDown == null)
                    {
                        break;
                    }
                    UICamera.selectedObject = this.selectOnDown.gameObject;
                    return;

                case KeyCode.RightArrow:
                    if (this.selectOnRight == null)
                    {
                        break;
                    }
                    UICamera.selectedObject = this.selectOnRight.gameObject;
                    return;

                case KeyCode.LeftArrow:
                    if (this.selectOnLeft == null)
                    {
                        break;
                    }
                    UICamera.selectedObject = this.selectOnLeft.gameObject;
                    return;

                case KeyCode.Tab:
                    if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                    {
                        if (this.selectOnLeft != null)
                        {
                            UICamera.selectedObject = this.selectOnLeft.gameObject;
                            return;
                        }
                        if (this.selectOnUp != null)
                        {
                            UICamera.selectedObject = this.selectOnUp.gameObject;
                            return;
                        }
                        if (this.selectOnDown != null)
                        {
                            UICamera.selectedObject = this.selectOnDown.gameObject;
                            return;
                        }
                        if (this.selectOnRight != null)
                        {
                            UICamera.selectedObject = this.selectOnRight.gameObject;
                        }
                        break;
                    }
                    if (this.selectOnRight != null)
                    {
                        UICamera.selectedObject = this.selectOnRight.gameObject;
                        return;
                    }
                    if (this.selectOnDown != null)
                    {
                        UICamera.selectedObject = this.selectOnDown.gameObject;
                        return;
                    }
                    if (this.selectOnUp != null)
                    {
                        UICamera.selectedObject = this.selectOnUp.gameObject;
                        return;
                    }
                    if (this.selectOnLeft == null)
                    {
                        break;
                    }
                    UICamera.selectedObject = this.selectOnLeft.gameObject;
                    return;

                default:
                    return;
            }
        }
    }

    private void Start()
    {
        if (this.startsSelected && ((UICamera.selectedObject == null) || !NGUITools.GetActive(UICamera.selectedObject)))
        {
            UICamera.selectedObject = base.gameObject;
        }
    }
}

