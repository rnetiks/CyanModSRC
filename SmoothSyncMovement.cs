using Photon;
using System;
using UnityEngine;

public class SmoothSyncMovement : Photon.MonoBehaviour
{
    public Vector3 correctCameraPos;
    public Quaternion correctCameraRot;
    private Vector3 correctPlayerPos = Vector3.zero;
    private Quaternion correctPlayerRot = Quaternion.identity;
    private Vector3 correctPlayerVelocity = Vector3.zero;
    Transform BaseT;

    public bool disabled;
    public bool noVelocity;
    public bool PhotonCamera;
    public float SmoothingDelay = 5f;

    public void Awake()
    {
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
        {
            base.enabled = false;
        }
        BaseT = base.transform;
        this.correctPlayerPos = BaseT.position;
        this.correctPlayerRot = BaseT.rotation;
        if (base.rigidbody == null)
        {
            this.noVelocity = true;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(BaseT.position);
            stream.SendNext(BaseT.rotation);
            if (!this.noVelocity)
            {
                stream.SendNext(base.rigidbody.velocity);
            }
            if (this.PhotonCamera)
            {
                stream.SendNext(Camera.main.transform.rotation);
            }
        }
        else
        {
            this.correctPlayerPos = (Vector3) stream.ReceiveNext();
            this.correctPlayerRot = (Quaternion) stream.ReceiveNext();
            if (!this.noVelocity)
            {
                this.correctPlayerVelocity = (Vector3) stream.ReceiveNext();
            }
            if (this.PhotonCamera)
            {
                this.correctCameraRot = (Quaternion) stream.ReceiveNext();
            }
        }
    }

    public void Update()
    {
        if (!this.disabled && !base.photonView.isMine)
        {
            BaseT.position = Vector3.Lerp(BaseT.position, this.correctPlayerPos, Time.deltaTime * this.SmoothingDelay);
            BaseT.rotation = Quaternion.Lerp(BaseT.rotation, this.correctPlayerRot, Time.deltaTime * this.SmoothingDelay);
            if (!this.noVelocity)
            {
                base.rigidbody.velocity = this.correctPlayerVelocity;
            }
        }
    }
}

