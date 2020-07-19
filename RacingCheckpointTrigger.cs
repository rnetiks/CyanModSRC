using System;
using UnityEngine;

public class RacingCheckpointTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject gameObject = other.gameObject;
        if (gameObject.layer == 8)
        {
            HERO hero = gameObject.transform.root.gameObject.GetComponent<HERO>();
            if (((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && (hero.photonView != null)) && (hero.photonView.isMine && (hero != null)))
            {
                InRoomChat.instance.addLINE("Checkpoint set.");
                hero.fillGas();
                FengGameManagerMKII.instance.racingSpawnPoint = base.gameObject.transform.position;
                FengGameManagerMKII.instance.racingSpawnPointSet = true;
            }
        }
    }
}

