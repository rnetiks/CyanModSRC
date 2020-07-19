﻿using Photon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Bomb : Photon.MonoBehaviour
{
    private Vector3 correctPlayerPos = Vector3.zero;
    private Quaternion correctPlayerRot = Quaternion.identity;
    private Vector3 correctPlayerVelocity = Vector3.zero;
    public bool disabled;
    public GameObject myExplosion;
    public float SmoothingDelay = 10f;

    public void Awake()
    {
        if (base.photonView != null)
        {
            float num;
            float num2;
            float num3;
            base.photonView.observed = this;
            this.correctPlayerPos = base.transform.position;
            this.correctPlayerRot = Quaternion.identity;
            PhotonPlayer owner = base.photonView.owner;
            if (RCSettings.teamMode > 0)
            {
                switch ((owner.RCteam))
                {
                    case 1:
                        base.GetComponent<ParticleSystem>().startColor = Color.cyan;
                        return;

                    case 2:
                        base.GetComponent<ParticleSystem>().startColor = Color.magenta;
                        return;
                }
                num = (owner.RCBombR);
                num2 = (owner.RCBombG);
                num3 = (owner.RCBombB);
                base.GetComponent<ParticleSystem>().startColor = new Color(num, num2, num3, 1f);
            }
            else
            {
                num = (owner.RCBombR);
                num2 = (owner.RCBombG);
                num3 = (owner.RCBombB);
                base.GetComponent<ParticleSystem>().startColor = new Color(num, num2, num3, 1f);
            }
        }
    }

    public void destroyMe()
    {
        if (base.photonView.isMine)
        {
            if (this.myExplosion != null)
            {
                PhotonNetwork.Destroy(this.myExplosion);
            }
            PhotonNetwork.Destroy(base.gameObject);
        }
    }

    public void Explode(float radius)
    {
        this.disabled = true;
        base.rigidbody.velocity = Vector3.zero;
        Vector3 position = base.transform.position;
        this.myExplosion = PhotonNetwork.Instantiate("RCAsset/BombExplodeMain", position, Quaternion.Euler(0f, 0f, 0f), 0);
        foreach (HERO hero in FengGameManagerMKII.instance.heroes)
        {
            GameObject gameObject = hero.gameObject;
            if (((Vector3.Distance(gameObject.transform.position, position) < radius) && !gameObject.GetPhotonView().isMine) && !hero.bombImmune)
            {
                PhotonPlayer owner = gameObject.GetPhotonView().owner;
                if (RCSettings.teamMode > 0)
                {
                    int num = (PhotonNetwork.player.RCteam);
                    int num2 = (owner.RCteam);
                    if ((num == 0) || (num != num2))
                    {
                        hero.markDie();
                        hero.photonView.RPC("netDie2", PhotonTargets.All, new object[] { -1, (PhotonNetwork.player.name2) + " " });
                        FengGameManagerMKII.instance.playerKillInfoUpdate(PhotonNetwork.player, 0);
                    }
                }
                else
                {
                    hero.markDie();
                    hero.photonView.RPC("netDie2", PhotonTargets.All, new object[] { -1, (PhotonNetwork.player.name2) + " " });
                    FengGameManagerMKII.instance.playerKillInfoUpdate(PhotonNetwork.player, 0);
                }
            }
        }
        base.StartCoroutine(this.WaitAndFade(1.5f));
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(base.transform.position);
            stream.SendNext(base.transform.rotation);
            stream.SendNext(base.rigidbody.velocity);
        }
        else
        {
            this.correctPlayerPos = (Vector3) stream.ReceiveNext();
            this.correctPlayerRot = (Quaternion) stream.ReceiveNext();
            this.correctPlayerVelocity = (Vector3) stream.ReceiveNext();
        }
    }

    public void Update()
    {
        if (!this.disabled && !base.photonView.isMine)
        {
            base.transform.position = Vector3.Lerp(base.transform.position, this.correctPlayerPos, Time.deltaTime * this.SmoothingDelay);
            base.transform.rotation = Quaternion.Lerp(base.transform.rotation, this.correctPlayerRot, Time.deltaTime * this.SmoothingDelay);
            base.rigidbody.velocity = this.correctPlayerVelocity;
        }
    }

    private IEnumerator WaitAndFade(float time)
    {
        yield return new WaitForSeconds(time);
        PhotonNetwork.Destroy(this.myExplosion);
        PhotonNetwork.Destroy(this.gameObject);
    }

}

