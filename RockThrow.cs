using Photon;
using System;
using UnityEngine;

public class RockThrow : Photon.MonoBehaviour
{
    private bool launched;
    private Vector3 oldP;
    private Vector3 r;
    private Vector3 v;
    Transform baseT;
    GameObject cam;

    void Awake()
    {
        baseT = base.transform;
    }
    private void explore()
    {
        GameObject obj2;
        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && PhotonNetwork.isMasterClient)
        {
            obj2 = PhotonNetwork.Instantiate("FX/boom6", baseT.position, baseT.rotation, 0);
            EnemyfxIDcontainer fx = obj2.GetComponent<EnemyfxIDcontainer>();
            if (fx != null)
            {
                EnemyfxIDcontainer basefx = baseT.root.gameObject.GetComponent<EnemyfxIDcontainer>();
                fx.myOwnerViewID = basefx.myOwnerViewID;
                fx.titanName = basefx.titanName;
            }
        }
        else
        {
            obj2 = (GameObject)UnityEngine.Object.Instantiate(Cach.boom6 != null ? Cach.boom6 : Cach.boom6 = (GameObject)Resources.Load("FX/boom6"), baseT.position, baseT.rotation);
        }
        obj2.transform.localScale = baseT.localScale;
        float b = 1f - (Vector3.Distance(cam.transform.position, obj2.transform.position) * 0.05f);
        b = Mathf.Min(1f, b);
         IN_GAME_MAIN_CAMERA.instance.startShake(b, b, 0.95f);
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
        {
            UnityEngine.Object.Destroy(base.gameObject);
        }
        else
        {
            PhotonNetwork.Destroy(base.photonView);
        }
    }

    private void hitPlayer(HERO heros)
    {
        if ((!heros.HasDied()) && !heros.isInvincible())
        {
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                if (!heros.isGrabbed)
                {
                    heros.die((Vector3)((this.v.normalized * 1000f) + (Vector3.up * 50f)), false);
                }
            }
            else if (((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && !heros.HasDied()) && !heros.isGrabbed)
            {
                heros.markDie();
                int myOwnerViewID = -1;
                string titanName = string.Empty;
                EnemyfxIDcontainer fx = baseT.root.gameObject.GetComponent<EnemyfxIDcontainer>();
                if (fx != null)
                {
                    myOwnerViewID = fx.myOwnerViewID;
                    titanName = fx.titanName;
                }
                Debug.Log("rock hit player " + titanName);
                object[] parameters = new object[] { (Vector3) ((this.v.normalized * 1000f) + (Vector3.up * 50f)), false, myOwnerViewID, titanName, true };
                heros.photonView.RPC("netDie", PhotonTargets.All, parameters);
            }
        }
    }

    [RPC]
    private void initRPC(int viewID, Vector3 scale, Vector3 pos, float level)
    {
        GameObject gameObject = PhotonView.Find(viewID).gameObject;
        Transform transform = gameObject.transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R/hand_R_001");
        baseT.localScale = gameObject.transform.localScale;
        baseT.parent = transform;
        baseT.localPosition = pos;
    }

    public void launch(Vector3 v1)
    {
        this.launched = true;
        this.oldP = baseT.position;
        this.v = v1;
        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && PhotonNetwork.isMasterClient)
        {
            object[] parameters = new object[] { this.v, this.oldP };
            base.photonView.RPC("launchRPC", PhotonTargets.Others, parameters);
        }
    }

    [RPC]
    private void launchRPC(Vector3 v, Vector3 p)
    {
        this.launched = true;
        Vector3 vector = p;
        baseT.position = vector;
        this.oldP = vector;
        baseT.parent = null;
        this.launch(v);
    }

    private void Start()
    {
        this.r = new Vector3(UnityEngine.Random.Range((float) -5f, (float) 5f), UnityEngine.Random.Range((float) -5f, (float) 5f), UnityEngine.Random.Range((float) -5f, (float) 5f));
        cam = CyanMod.CachingsGM.Find("MainCamera");
    }

    private void Update()
    {
        if (this.launched)
        {
            baseT.Rotate(this.r);
            this.v -= (Vector3) ((20f * Vector3.up) * Time.deltaTime);
       
       baseT.position += (Vector3)(this.v * Time.deltaTime);
            if ((IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.MULTIPLAYER) || PhotonNetwork.isMasterClient)
            {
                LayerMask mask = ((int) 1) << LayerMask.NameToLayer("Ground");
                LayerMask mask2 = ((int) 1) << LayerMask.NameToLayer("Players");
                LayerMask mask3 = ((int) 1) << LayerMask.NameToLayer("EnemyAABB");
                LayerMask mask4 = (mask2 | mask) | mask3;
                foreach (RaycastHit hit in Physics.SphereCastAll(baseT.position, 2.5f * baseT.lossyScale.x, baseT.position - this.oldP, Vector3.Distance(baseT.position, this.oldP), (int) mask4))
                {
                    string namelayer = LayerMask.LayerToName(hit.collider.gameObject.layer);
                    if (namelayer == "EnemyAABB")
                    {
                        GameObject gameObject = hit.collider.gameObject.transform.root.gameObject;
                        TITAN titan = gameObject.GetComponent<TITAN>();
                        if ((titan != null) && !titan.hasDie)
                        {
                            titan.hitAnkle();
                            Vector3 position = baseT.position;
                            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                            {
                                titan.hitAnkle();
                            }
                            else
                            {
                                EnemyfxIDcontainer fx = baseT.root.gameObject.GetComponent<EnemyfxIDcontainer>();
                                PhotonView viev = PhotonView.Find(fx.myOwnerViewID);
                                if ((fx != null) && (viev != null))
                                {
                                    Vector3 vector2 = viev.transform.position;
                                }
                                gameObject.GetComponent<HERO>().photonView.RPC("hitAnkleRPC", PhotonTargets.All, new object[0]);
                            }
                        }
                        this.explore();
                    }
                    else if (namelayer == "Players")
                    {
                        GameObject hero = hit.collider.gameObject.transform.root.gameObject;
                        TITAN_EREN te = hero.GetComponent<TITAN_EREN>();
                        HERO hr = null;
                        if (te != null)
                        {
                            if (!te.isHit)
                            {
                                te.hitByTitan();
                            }
                        }
                        else if (((hr = hero.GetComponent<HERO>()) != null) && !hr.isInvincible())
                        {
                            this.hitPlayer(hr);
                        }
                    }
                    else if (namelayer != "Leave")
                    {
                        this.explore();
                    }
                }
                this.oldP = baseT.position;
            }
        }
    }
}

