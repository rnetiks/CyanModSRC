using Photon;
using System;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class MoveByKeys : Photon.MonoBehaviour
{
    public float speed = 10f;

    private void Start()
    {
        base.enabled = base.photonView.isMine;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Transform transform = base.transform;
            transform.position += (Vector3) (Vector3.left * (this.speed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.D))
        {
            Transform transform5 = base.transform;
            transform5.position += (Vector3) (Vector3.right * (this.speed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.W))
        {
            Transform transform6 = base.transform;
            transform6.position += (Vector3) (Vector3.forward * (this.speed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.S))
        {
            Transform transform7 = base.transform;
            transform7.position += (Vector3) (Vector3.back * (this.speed * Time.deltaTime));
        }
    }
}

