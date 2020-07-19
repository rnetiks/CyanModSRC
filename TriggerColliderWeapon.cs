using System;
using System.Collections;
using UnityEngine;

public class TriggerColliderWeapon : MonoBehaviour
{
    public bool active_me;
    public GameObject currentCamera;
    public ArrayList currentHits = new ArrayList();
    public ArrayList currentHitsII = new ArrayList();
    public AudioSource meatDie;
    public int myTeam = 1;
    public float scoreMulti = 1f;
    public IN_GAME_MAIN_CAMERA currentCameraT;
    Transform BaseTR;
    Transform BaseT;

    void AddStats(string n,int dmg,float s =0f)
    {
        FengGameManagerMKII.instance.panelScore.AddStats(n, dmg, s);
    }
    void Awake()
    {
        BaseT = base.transform;
        BaseTR = BaseT.root;
    }

    private bool checkIfBehind(GameObject titan)
    {
        Transform transform = titan.transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck/head");
        Vector3 to = BaseT.position - transform.transform.position;
        return (Vector3.Angle(-transform.transform.forward, to) < 70f);
    }

    public void clearHits()
    {
        this.currentHitsII = new ArrayList();
        this.currentHits = new ArrayList();
    }

    private void napeMeat(Vector3 vkill, Transform titan)
    {
        if ((int)FengGameManagerMKII.settings[286] == 0)
        {
            Transform transform = titan.transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck");
            GameObject obj2 = (GameObject)UnityEngine.Object.Instantiate(Cach.titanNapeMeat != null ? Cach.titanNapeMeat : Cach.titanNapeMeat = (GameObject)Resources.Load("titanNapeMeat"), transform.position, transform.rotation);
            obj2.transform.localScale = titan.localScale;
            obj2.rigidbody.AddForce((Vector3)(vkill.normalized * 15f), ForceMode.Impulse);
            obj2.rigidbody.AddForce((Vector3)(-titan.forward * 10f), ForceMode.Impulse);
            obj2.rigidbody.AddTorque(new Vector3((float)UnityEngine.Random.Range(-100, 100), (float)UnityEngine.Random.Range(-100, 100), (float)UnityEngine.Random.Range(-100, 100)), ForceMode.Impulse);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (this.active_me)
        {
            if (!this.currentHitsII.Contains(other.gameObject))
            {
                this.currentHitsII.Add(other.gameObject);
                currentCameraT.startShake(0.1f, 0.1f, 0.95f);
                if (other.gameObject.transform.root.gameObject.tag == "titan")
                {
                    if ((int)FengGameManagerMKII.settings[330] == 0)
                    {
                        currentCameraT.main_objectH.slashHit.Play();
                    }
                    if ((int)FengGameManagerMKII.settings[287] == 0)
                    {
                        GameObject obj2;
                        if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)
                        {
                            obj2 = PhotonNetwork.Instantiate("hitMeat", BaseT.position, Quaternion.Euler(270f, 0f, 0f), 0);
                        }
                        else
                        {
                            obj2 = (GameObject)UnityEngine.Object.Instantiate(Cach.hitMeat != null ? Cach.hitMeat : Cach.hitMeat = (GameObject)Resources.Load("hitMeat"));
                        }
                        obj2.transform.position = BaseT.position;
                    }
                    BaseTR.GetComponent<HERO>().useBlade(0);
                }
            }
            if (other.gameObject.tag == "playerHitbox")
            {
                if (FengGameManagerMKII.lvlInfo.pvp)
                {
                    float b = 1f - (Vector3.Distance(other.gameObject.transform.position, BaseT.position) * 0.05f);
                    b = Mathf.Min(1f, b);
                    HitBox component = other.gameObject.GetComponent<HitBox>();
                    HERO hero = null;
                    if ((((component != null) && (component.transform.root != null)) && ((hero = component.transform.root.GetComponent<HERO>()).myTeam != this.myTeam)) && !hero.isInvincible())
                    {
                        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                        {
                            if (!hero.isGrabbed)
                            {
                                Vector3 vector = component.transform.root.transform.position - BaseT.position;
                                hero.die((Vector3)(((vector.normalized * b) * 1000f) + (Vector3.up * 50f)), false);
                            }
                        }
                        else if (((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && !hero.HasDied()) && !hero.isGrabbed)
                        {
                            hero.markDie();
                            int viewID = BaseTR.gameObject.GetPhotonView().viewID;
                            string plName = PhotonView.Find(viewID).owner.name2;
                            object[] parameters = new object[5];
                            Vector3 vector2 = component.transform.root.position - BaseT.position;
                            parameters[0] = (Vector3)(((vector2.normalized * b) * 1000f) + (Vector3.up * 50f));
                            parameters[1] = false;
                            parameters[2] = viewID;
                            parameters[3] = plName;
                            parameters[4] = false;
                            hero.photonView.RPC("netDie", PhotonTargets.All, parameters);
                            AddStats(plName,0);
                        }
                    }
                }
            }
            else if (other.gameObject.tag == "titanneck")
            {
                HitBox item = other.gameObject.GetComponent<HitBox>();
                if (((item != null) && this.checkIfBehind(item.transform.root.gameObject)) && !this.currentHits.Contains(item))
                {
                    item.hitPosition = (Vector3)((BaseT.position + item.transform.position) * 0.5f);
                    this.currentHits.Add(item);
                    if ((int)FengGameManagerMKII.settings[330] == 0)
                    {
                        this.meatDie.Play();
                    }
                    TITAN titn = item.transform.root.GetComponent<TITAN>();
                    FEMALE_TITAN ft = null;
                    COLOSSAL_TITAN ct = null;
                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                    {
                        if ((titn != null) && !titn.hasDie)
                        {
                            Vector3 vector3 = this.currentCameraT.main_objectR.velocity - item.transform.root.rigidbody.velocity;
                            int num2 = (int)((vector3.magnitude * 10f) * this.scoreMulti);
                            num2 = Mathf.Max(10, num2);
                            if ((int)FengGameManagerMKII.settings[320] == 1)
                            {
                                IN_GAME_MAIN_CAMERA.instance.startSnapShot(item.transform.position, num2, item.transform.root.gameObject, 0.02f);
                            }
                            AddStats(titn.name, num2, titn.myLevel);
                            titn.die();
                            this.napeMeat(this.currentCameraT.main_objectR.velocity, item.transform.root);
                            FengGameManagerMKII.instance.netShowDamage(num2);
                            FengGameManagerMKII.instance.playerKillInfoSingleUpdate(num2);
                        }
                    }
                    else if (!PhotonNetwork.isMasterClient)
                    {
                        if (titn != null)
                        {
                            if (!titn.hasDie)
                            {
                                Vector3 vector4 = this.currentCameraT.main_objectR.velocity - item.transform.root.rigidbody.velocity;
                                int num3 = (int)((vector4.magnitude * 10f) * this.scoreMulti);
                                num3 = Mathf.Max(10, num3);
                                if ((int)FengGameManagerMKII.settings[320] == 1)
                                {
                                    IN_GAME_MAIN_CAMERA.instance.startSnapShot(item.transform.position, num3, item.transform.root.gameObject, 0.02f);
                                    titn.asClientLookTarget = false;
                                }
                                AddStats(titn.name, num3, titn.myLevel);
                                object[] objArray2 = new object[] { BaseTR.gameObject.GetPhotonView().viewID, num3 };
                                titn.photonView.RPC("titanGetHit", titn.photonView.owner, objArray2);
                            }
                        }
                        else if ((ft = item.transform.root.GetComponent<FEMALE_TITAN>()) != null)
                        {
                            BaseTR.GetComponent<HERO>().useBlade(0x7fffffff);
                            Vector3 vector5 = this.currentCameraT.main_objectR.velocity - item.transform.root.rigidbody.velocity;
                            int num4 = (int)((vector5.magnitude * 10f) * this.scoreMulti);
                            num4 = Mathf.Max(10, num4);
                            if (!ft.hasDie)
                            {
                                AddStats(ft.name, num4, ft.size);
                                object[] objArray3 = new object[] { BaseTR.gameObject.GetPhotonView().viewID, num4 };
                                ft.photonView.RPC("titanGetHit", ft.photonView.owner, objArray3);
                            }
                        }
                        else if ((ct = item.transform.root.GetComponent<COLOSSAL_TITAN>()) != null)
                        {
                            BaseTR.GetComponent<HERO>().useBlade(0x7fffffff);
                            if (!ct.hasDie)
                            {
                             
                                Vector3 vector6 = this.currentCameraT.main_objectR.velocity - item.transform.root.rigidbody.velocity;
                                int num5 = (int)((vector6.magnitude * 10f) * this.scoreMulti);
                                num5 = Mathf.Max(10, num5);
                                AddStats(ct.name, num5, ct.size);
                                object[] objArray4 = new object[] { BaseTR.gameObject.GetPhotonView().viewID, num5 };
                                ct.photonView.RPC("titanGetHit", ct.photonView.owner, objArray4);
                            }
                        }
                    }
                    else if (titn != null)
                    {
                        if (!titn.hasDie)
                        {
                            Vector3 vector7 = this.currentCameraT.main_objectR.velocity - item.transform.root.rigidbody.velocity;
                            int num6 = (int)((vector7.magnitude * 10f) * this.scoreMulti);
                            num6 = Mathf.Max(10, num6);
                            AddStats(titn.name, num6, titn.myLevel);
                            if ((int)FengGameManagerMKII.settings[320] == 1)
                            {
                                IN_GAME_MAIN_CAMERA.instance.startSnapShot(item.transform.position, num6, item.transform.root.gameObject, 0.02f);
                            }
                            titn.titanGetHit(BaseTR.gameObject.GetPhotonView().viewID, num6);
                        }
                    }
                    else if ((ft = item.transform.root.GetComponent<FEMALE_TITAN>()) != null)
                    {
                        BaseTR.GetComponent<HERO>().useBlade(0x7fffffff);
                        if (!ft.hasDie)
                        {
                            Vector3 vector8 = this.currentCameraT.main_objectR.velocity - item.transform.root.rigidbody.velocity;
                            int num7 = (int)((vector8.magnitude * 10f) * this.scoreMulti);
                            num7 = Mathf.Max(10, num7);
                            AddStats(ft.name, num7, ft.size);
                            if ((int)FengGameManagerMKII.settings[320] == 1)
                            {
                                IN_GAME_MAIN_CAMERA.instance.startSnapShot(item.transform.position, num7, null, 0.02f);
                            }
                            ft.titanGetHit(BaseTR.gameObject.GetPhotonView().viewID, num7);
                        }
                    }
                    else if ((ct = item.transform.root.GetComponent<COLOSSAL_TITAN>()) != null)
                    {
                        BaseTR.GetComponent<HERO>().useBlade(0x7fffffff);
                        if (!ct.hasDie)
                        {
                            Vector3 vector9 = this.currentCameraT.main_objectR.velocity - item.transform.root.rigidbody.velocity;
                            int num8 = (int)((vector9.magnitude * 10f) * this.scoreMulti);
                            num8 = Mathf.Max(10, num8);
                            AddStats(ct.name, num8, ct.size);
                            if ((int)FengGameManagerMKII.settings[320] == 1)
                            {
                                IN_GAME_MAIN_CAMERA.instance.startSnapShot(item.transform.position, num8, null, 0.02f);
                            }
                            ct.titanGetHit(BaseTR.gameObject.GetPhotonView().viewID, num8);
                        }
                    }
                    this.showCriticalHitFX();
                }
            }
            else if (other.gameObject.tag == "titaneye")
            {
                if (!this.currentHits.Contains(other.gameObject))
                {
                    this.currentHits.Add(other.gameObject);
                    GameObject gameObject = other.gameObject.transform.root.gameObject;
                    TITAN tit = null;
                    FEMALE_TITAN ft = gameObject.GetComponent<FEMALE_TITAN>();
                    if (ft != null)
                    {
                        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                        {
                            if (!ft.hasDie)
                            {
                                ft.hitEye();
                            }
                        }
                        else if (!PhotonNetwork.isMasterClient)
                        {
                            if (!ft.hasDie)
                            {
                                object[] objArray5 = new object[] { BaseTR.gameObject.GetPhotonView().viewID };
                                ft.photonView.RPC("hitEyeRPC", PhotonTargets.MasterClient, objArray5);
                            }
                        }
                        else if (!ft.hasDie)
                        {
                            ft.hitEyeRPC(BaseTR.gameObject.GetPhotonView().viewID);
                        }
                    }
                    else if ((tit = gameObject.GetComponent<TITAN>()).abnormalType != AbnormalType.TYPE_CRAWLER)
                    {
                        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                        {
                            if (!tit.hasDie)
                            {
                                tit.hitEye();
                            }
                        }
                        else if (!PhotonNetwork.isMasterClient)
                        {
                            if (!tit.hasDie)
                            {
                                object[] objArray6 = new object[] { BaseTR.gameObject.GetPhotonView().viewID };
                                tit.photonView.RPC("hitEyeRPC", PhotonTargets.MasterClient, objArray6);
                            }
                        }
                        else if (!tit.hasDie)
                        {
                            tit.hitEyeRPC(BaseTR.gameObject.GetPhotonView().viewID);
                        }
                        this.showCriticalHitFX();
                    }
                }
            }
            else if ((other.gameObject.tag == "titanankle") && !this.currentHits.Contains(other.gameObject))
            {
                this.currentHits.Add(other.gameObject);
                GameObject obj4 = other.gameObject.transform.root.gameObject;
                Vector3 vector10 = this.currentCameraT.main_objectR.velocity - obj4.rigidbody.velocity;
                int num9 = (int)((vector10.magnitude * 10f) * this.scoreMulti);
                num9 = Mathf.Max(10, num9);
                TITAN tit = obj4.GetComponent<TITAN>();
                FEMALE_TITAN ft = null;
                if ((tit != null) && (tit.abnormalType != AbnormalType.TYPE_CRAWLER))
                {
                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                    {
                        if (!tit.hasDie)
                        {
                            tit.hitAnkle();
                        }
                    }
                    else
                    {
                        if (!PhotonNetwork.isMasterClient)
                        {
                            if (!tit.hasDie)
                            {
                                object[] objArray7 = new object[] { BaseTR.gameObject.GetPhotonView().viewID };
                                tit.photonView.RPC("hitAnkleRPC", PhotonTargets.MasterClient, objArray7);
                            }
                        }
                        else if (!tit.hasDie)
                        {
                            tit.hitAnkle();
                        }
                        this.showCriticalHitFX();
                    }
                }
                else if ((ft = obj4.GetComponent<FEMALE_TITAN>()) != null)
                {
                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                    {
                        if (other.gameObject.name == "ankleR")
                        {
                            if (!ft.hasDie)
                            {
                                ft.hitAnkleR(num9);
                            }
                        }
                        else if (!ft.hasDie)
                        {
                            ft.hitAnkleL(num9);
                        }
                    }
                    else if (other.gameObject.name == "ankleR")
                    {
                        if (!PhotonNetwork.isMasterClient)
                        {
                            if (!ft.hasDie)
                            {
                                object[] objArray8 = new object[] { BaseTR.gameObject.GetPhotonView().viewID, num9 };
                                ft.photonView.RPC("hitAnkleRRPC", PhotonTargets.MasterClient, objArray8);
                            }
                        }
                        else if (!ft.hasDie)
                        {
                            ft.hitAnkleRRPC(BaseTR.gameObject.GetPhotonView().viewID, num9);
                        }
                    }
                    else if (!PhotonNetwork.isMasterClient)
                    {
                        if (!ft.hasDie)
                        {
                            object[] objArray9 = new object[] { BaseTR.gameObject.GetPhotonView().viewID, num9 };
                            ft.photonView.RPC("hitAnkleLRPC", PhotonTargets.MasterClient, objArray9);
                        }
                    }
                    else if (!ft.hasDie)
                    {
                        ft.hitAnkleLRPC(BaseTR.gameObject.GetPhotonView().viewID, num9);
                    }
                    this.showCriticalHitFX();
                }
            }
        }
    }

    private void showCriticalHitFX()
    {
        currentCameraT.startShake(0.2f, 0.3f, 0.95f);
        if ((int)FengGameManagerMKII.settings[284] == 0)
        {
            GameObject obj2;
            if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)
            {
                obj2 = PhotonNetwork.Instantiate("redCross", BaseT.position, Quaternion.Euler(270f, 0f, 0f), 0);
            }
            else
            {
                obj2 = (GameObject)UnityEngine.Object.Instantiate(Cach.redCross != null ? Cach.redCross : Cach.redCross = (GameObject)Resources.Load("redCross"));
            }
            obj2.transform.position = BaseT.position;
        }
    }

    private void Start()
    {
        this.currentCamera = GameObject.Find("MainCamera");
        currentCameraT = currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>();
        base.gameObject.renderer.material.mainTexture = Color.cyan.toTexture1();
    }
}

