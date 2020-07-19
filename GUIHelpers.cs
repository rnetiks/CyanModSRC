using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class GUIHelpers
{
    public static Rect AlignRect(float width, float height, Alignment alignment)
    {
      return  AlignRect(width, height, new Rect(0f, 0f, (float) Screen.width, (float) Screen.height), alignment, 0f, 0f);
    }

    public static Rect AlignRect(float width, float height, Rect parentRect, Alignment alignment)
    {
     return    AlignRect(width, height, parentRect, alignment, 0f, 0f);
    }

    public static Rect AlignRect(float width, float height, Alignment alignment, float xOffset, float yOffset)
    {
    return   AlignRect(width, height, new Rect(0f, 0f, Screen.width,  Screen.height), alignment, xOffset,yOffset);
    }

    public static Rect AlignRect(float width, float height, Rect parentRect, Alignment alignment, float xOffset, float yOffset)
    {
        Rect rect;
        switch (alignment)
        {
            case Alignment.TOPLEFT:
                rect = new Rect(0f, 0f, width, height);
                break;

            case Alignment.TOPCENTER:
                rect = new Rect((parentRect.width * 0.5f) - (width * 0.5f), 0f, width, height);
                break;

            case Alignment.TOPRIGHT:
                rect = new Rect(parentRect.width - width, 0f, width, height);
                break;

            case Alignment.RIGHT:
                rect = new Rect(parentRect.width - width, (parentRect.height * 0.5f) - (height * 0.5f), width, height);
                break;

            case Alignment.BOTTOMRIGHT:
                rect = new Rect(parentRect.width - width, parentRect.height - height, width, height);
                break;

            case Alignment.BOTTOMCENTER:
                rect = new Rect((parentRect.width * 0.5f) - (width * 0.5f), parentRect.height - height, width, height);
                break;

            case Alignment.BOTTOMLEFT:
                rect = new Rect(0f, (parentRect.y + parentRect.height) - height, width, height);
                break;

            case Alignment.LEFT:
                rect = new Rect(0f, (parentRect.height * 0.5f) - (height * 0.5f), width, height);
                break;

            case Alignment.CENTER:
                rect = new Rect((parentRect.width * 0.5f) - (width * 0.5f), (parentRect.height * 0.5f) - (height * 0.5f), width, height);
                break;

            default:
                rect = new Rect(0f, 0f, width, height);
                break;
        }
        rect.x += parentRect.x + xOffset;
        rect.y += parentRect.y + yOffset;
        return rect;
    }

    public static Rect ClampPosition(this Rect r, Rect borderRect)
    {
     return    new Rect(Mathf.Clamp(r.x, borderRect.x, (borderRect.x + borderRect.width) - r.width),
            Mathf.Clamp(r.y, borderRect.y, (borderRect.y + borderRect.height) - r.height), r.width, r.height);
    }

    public static Vector2 FixedTouchDelta(this Touch aTouch)
    {
        float f = Time.deltaTime / aTouch.deltaTime;
        if (((f == 0f) || float.IsNaN(f)) || float.IsInfinity(f))
        {
            f = 1f;
        }
        return (Vector2) (aTouch.deltaPosition * f);
    }

    public static Vector2 FlipY(Vector2 inPos)
    {
        inPos.y = Screen.height - inPos.y;
        return inPos;
    }

    public static Vector2 GetGUIPosition(this Touch aTouch)
    {
        Vector2 position = aTouch.position;
        position.y = Screen.height - position.y;
        return position;
    }

    public static bool GetKeyDown(this Event aEvent, KeyCode aKey)
    {
        return ((aEvent.type == EventType.KeyDown) && (aEvent.keyCode == aKey));
    }

    public static bool GetKeyUp(this Event aEvent, KeyCode aKey)
    {
        return ((aEvent.type == EventType.KeyUp) && (aEvent.keyCode == aKey));
    }

    public static bool GetMouseDown(this Event aEvent, int aButton)
    {
        return ((aEvent.type == EventType.MouseDown) && (aEvent.button == aButton));
    }

    public static bool GetMouseDown(this Event aEvent, int aButton, Rect aRect)
    {
        return (((aEvent.type == EventType.MouseDown) && (aEvent.button == aButton)) &&
                aRect.Contains(aEvent.mousePosition));
    }

    public static bool GetMouseUp(this Event aEvent, int aButton)
    {
        return ((aEvent.type == EventType.MouseUp) && (aEvent.button == aButton));
    }

    public static bool GetMouseUp(this Event aEvent, int aButton, Rect aRect)
    {
        return (((aEvent.type == EventType.MouseUp) && (aEvent.button == aButton)) &&
                aRect.Contains(aEvent.mousePosition));
    }

    public static Rect Grow(this Rect r, float nbPixels)
    {
        return r.Shrink(-nbPixels, -nbPixels);
    }

    public static Rect Grow(this Rect r, float nbPixelX, float nbPixelY)
    {
        return r.Shrink(-nbPixelX, -nbPixelY);
    }

    public static Rect InverseTransform(this Rect r, Rect from)
    {
        return new Rect(r.x + @from.x, r.y + @from.y, r.width, r.height);
    }

    public static Vector2 InverseTransformPoint(Rect rect, Vector3 inPos)
    {
        return new Vector2(rect.x + inPos.x, rect.y + inPos.y);
    }

    public static Vector2 MouseRelativePos(Rect rect)
    {
        return RelativePos(rect, mousePos.x, mousePos.y);
    }

    public static Rect Move(this Rect r, Vector2 movement)
    {
        return r.Move(movement.x, movement.y);
    }

    public static Rect Move(this Rect r, float xMovement, float yMovement)
    {
        return new Rect(r.x + xMovement, r.y + yMovement, r.width, r.height);
    }

    public static Rect MoveX(this Rect r, float xMovement)
    {
        return r.Move(xMovement, 0f);
    }

    public static Rect MoveY(this Rect r, float yMovement)
    {
        return r.Move(0f, yMovement);
    }

    public static Vector2 RelativePos(Rect rect, Vector2 inPos)
    {
        return RelativePos(rect, inPos.x, inPos.y);
    }

    public static Vector2 RelativePos(Rect rect, Vector3 inPos)
    {
        return RelativePos(rect, inPos.x, inPos.y);
    }

    public static Vector2 RelativePos(Rect rect, float x, float y)
    {
        return new Vector2(x - rect.x, y - rect.y);
    }

    public static Rect RelativeTo(this Rect r, Rect to)
    {
        return new Rect(r.x - to.x, r.y - to.y, r.width, r.height);
    }

    public static Rect Shrink(this Rect r, float nbPixels)
    {
        return r.Shrink(nbPixels, nbPixels);
    }

    public static Rect Shrink(this Rect r, float nbPixelX, float nbPixelY)
    {
        return new Rect(r.x + nbPixelX, r.y + nbPixelY, r.width - (nbPixelX*2f), r.height - (nbPixelY*2f));
    }

    public static Vector2 mousePos
    {
        get { return Event.current.mousePosition; }
    }

    public static Vector2 mousePosInvertY
    {
        get { return FlipY(mousePos); }
    }

    public static Rect screenRect
    {
        get { return new Rect(0f, 0f, (float) Screen.width, (float) Screen.height); }
    }

    public enum Alignment
    {
        TOPLEFT,
        TOPCENTER,
        TOPRIGHT,
        RIGHT,
        BOTTOMRIGHT,
        BOTTOMCENTER,
        BOTTOMLEFT,
        LEFT,
        CENTER
    }
}

