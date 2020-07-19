using System;
using UnityEngine;

public class supplyCheck : MonoBehaviour
{
    private float elapsedTime;
    private float stepTime = 1f;

    private void Start()
    {
        if (Minimap.instance != null)
        {
            Minimap.instance.TrackGameObjectOnMinimap(base.gameObject, Color.white, false, true, Minimap.IconStyle.SUPPLY);
        }
    }

    private void Update()
    {
        this.elapsedTime += Time.deltaTime;
        if (this.elapsedTime > this.stepTime)
        {
            this.elapsedTime -= this.stepTime;
            foreach (HERO hero in FengGameManagerMKII.instance.heroes)
            {
                if (hero != null)
                {
                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                    {
                        if (Vector3.Distance(hero.transform.position, base.transform.position) < 1.5f)
                        {
                            hero.getSupply();
                        }
                    }
                    else if (hero.photonView.isMine && (Vector3.Distance(hero.transform.position, base.transform.position) < 1.5f))
                    {
                        hero.getSupply();
                    }
                }
            }
        }
    }
}

