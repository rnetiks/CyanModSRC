using Photon;
using System;
using UnityEngine;

public class BombExplode : Photon.MonoBehaviour
{
    public GameObject myExplosion;

    public void Start()
    {
        float num;
        float num2;
        float num3;
        float num4;
        float num6;
        if (base.photonView == null)
        {
            return;
        }
        PhotonPlayer owner = base.photonView.owner;
        ParticleSystem pattcolor = base.GetComponent<ParticleSystem>();
        if (RCSettings.teamMode > 0)
        {
            switch ((owner.RCteam))
            {
                case 1:
                    pattcolor.startColor = Color.cyan;
                    goto Label_016F;

                case 2:
                    pattcolor.startColor = Color.magenta;
                    goto Label_016F;
            }
            num = (owner.RCBombR);
            num2 = (owner.RCBombG);
            num3 = (owner.RCBombB);
            num4 = (owner.RCBombA);
            num4 = Mathf.Max(0.5f, num4);
            pattcolor.startColor = new Color(num, num2, num3, num4);
        }
        else
        {
            num = (owner.RCBombR);
            num2 = (owner.RCBombG);
            num3 = (owner.RCBombB);
            num4 = (owner.RCBombA);
            num4 = Mathf.Max(0.5f, num4);
            pattcolor.startColor = new Color(num, num2, num3, num4);
        }
    Label_016F:
        num6 = (owner.RCBombRadius) * 2f;
        num6 = Mathf.Clamp(num6, 40f, 120f);
        pattcolor.startSize = num6;
    }
}

