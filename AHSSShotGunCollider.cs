using System;
using System.Collections;
using UnityEngine;

public class AHSSShotGunCollider : MonoBehaviour
{
    public bool active_me;
    private int count;
    public GameObject currentCamera;
    public IN_GAME_MAIN_CAMERA currentCameraT;
    public ArrayList currentHits = new ArrayList();
    public int dmg = 1;
    private int myTeam = 1;
    private string ownerName = string.Empty;
    public float scoreMulti;
    private int viewID = -1;
    Transform baseT;
    GameObject baseG;
    void AddStats(string n, int dmg, float s = 0f)
    {
        FengGameManagerMKII.instance.panelScore.AddStats(n, dmg, s);
    }
    void Awake()
    {
        baseT = base.transform;
        baseG = baseT.root.gameObject;
    }
    private bool checkIfBehind(GameObject titan)
    {
        Transform transform = titan.transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck/head");
        Vector3 to = baseT.position - transform.transform.position;
        Debug.DrawRay(transform.transform.position, (Vector3)(-transform.transform.forward * 10f), Color.white, 5f);
        Debug.DrawRay(transform.transform.position, (Vector3)(to * 10f), Color.green, 5f);
        return (Vector3.Angle(-transform.transform.forward, to) < 100f);
    }

    private void FixedUpdate()
    {
        if (this.count > 1)
        {
            this.active_me = false;
        }
        else
        {
            this.count++;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (((IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.MULTIPLAYER) || baseG.GetPhotonView().isMine) && this.active_me)
        {
            GameObject otherG = other.gameObject;
            if (otherG.tag == "playerHitbox")
            {
                if (FengGameManagerMKII.lvlInfo.pvp)
                {
                    float b = 1f - (Vector3.Distance(otherG.transform.position, baseT.position) * 0.05f);
                    b = Mathf.Min(1f, b);
                    HitBox component = otherG.GetComponent<HitBox>();
                    HERO hero = component.transform.root.GetComponent<HERO>();
                    if ((((component != null) && (component.transform.root != null)) && (hero.myTeam != this.myTeam)) && !hero.isInvincible())
                    {
                        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                        {
                            if (!hero.isGrabbed)
                            {
                                Vector3 vector = component.transform.root.transform.position - baseT.position;
                                hero.die((Vector3)(((vector.normalized * b) * 1000f) + (Vector3.up * 50f)), false);
                            }
                        }
                        else if (((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && !hero.HasDied()) && !hero.isGrabbed)
                        {
                            hero.markDie();
                            object[] parameters = new object[5];
                            Vector3 vector2 = component.transform.root.position - baseT.position;
                            parameters[0] = (Vector3)(((vector2.normalized * b) * 1000f) + (Vector3.up * 50f));
                            parameters[1] = false;
                            parameters[2] = this.viewID;
                            parameters[3] = this.ownerName;
                            parameters[4] = false;
                            AddStats(ownerName, 0);
                            hero.photonView.RPC("netDie", PhotonTargets.All, parameters);
                        }
                    }
                }
            }
            else if (otherG.tag == "erenHitbox")
            {
                if ((this.dmg > 0) && !otherG.transform.root.gameObject.GetComponent<TITAN_EREN>().isHit)
                {
                    otherG.transform.root.gameObject.GetComponent<TITAN_EREN>().hitByTitan();
                }
            }
            else if (otherG.tag == "titanneck")
            {
                HitBox item = otherG.GetComponent<HitBox>();
                if (((item != null) && this.checkIfBehind(item.transform.root.gameObject)) && !this.currentHits.Contains(item))
                {
                    item.hitPosition = (Vector3)((baseT.position + item.transform.position) * 0.5f);
                    this.currentHits.Add(item);
                    TITAN tit = item.transform.root.GetComponent<TITAN>();
                    FEMALE_TITAN ft = null;
                    COLOSSAL_TITAN ct = null;
                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                    {
                        if ((tit != null) && !tit.hasDie)
                        {
                            Vector3 vector3 = currentCameraT.main_object.rigidbody.velocity - item.transform.root.rigidbody.velocity;
                            int num2 = (int)((vector3.magnitude * 10f) * this.scoreMulti);
                            num2 = Mathf.Max(10, num2);
                            FengGameManagerMKII.instance.netShowDamage(num2);
                            if (num2 > (tit.myLevel * 100f))
                            {
                                AddStats(tit.name, num2, tit.myLevel);
                                tit.die();
                                if ((int)FengGameManagerMKII.settings[320] == 1)
                                {
                                    IN_GAME_MAIN_CAMERA.instance.startSnapShot(item.transform.position, num2, item.transform.root.gameObject, 0.02f);
                                }
                                FengGameManagerMKII.instance.playerKillInfoSingleUpdate(num2);
                            }
                        }
                    }
                    else if (!PhotonNetwork.isMasterClient)
                    {
                        if (tit != null)
                        {
                            if (!tit.hasDie)
                            {
                                Vector3 vector4 = currentCameraT.main_object.rigidbody.velocity - item.transform.root.rigidbody.velocity;
                                int num3 = (int)((vector4.magnitude * 10f) * this.scoreMulti);
                                num3 = Mathf.Max(10, num3);
                                if (num3 > (tit.myLevel * 100f))
                                {
                                    AddStats(tit.name, num3, tit.myLevel);
                                    if ((int)FengGameManagerMKII.settings[320] == 1)
                                    {
                                        IN_GAME_MAIN_CAMERA.instance.startSnapShot(item.transform.position, num3, item.transform.root.gameObject, 0.02f);
                                        tit.asClientLookTarget = false;
                                    }
                                    object[] objArray2 = new object[] { baseG.GetPhotonView().viewID, num3 };
                                    tit.photonView.RPC("titanGetHit", tit.photonView.owner, objArray2);
                                }
                            }
                        }
                        else if ((ft = item.transform.root.GetComponent<FEMALE_TITAN>()) != null)
                        {
                            Vector3 vector5 = currentCameraT.main_object.rigidbody.velocity - item.transform.root.rigidbody.velocity;
                            int num4 = (int)((vector5.magnitude * 10f) * this.scoreMulti);
                            num4 = Mathf.Max(10, num4);
                            if (!ft.hasDie)
                            {
                                AddStats(ft.name, num4, ft.size);
                                object[] objArray3 = new object[] { baseG.GetPhotonView().viewID, num4 };
                                ft.photonView.RPC("titanGetHit", ft.photonView.owner, objArray3);
                            }
                        }
                        else if (((ct = item.transform.root.GetComponent<COLOSSAL_TITAN>()) != null) && !ct.hasDie)
                        {
                            Vector3 vector6 = currentCameraT.main_object.rigidbody.velocity - item.transform.root.rigidbody.velocity;
                            int num5 = (int)((vector6.magnitude * 10f) * this.scoreMulti);
                            num5 = Mathf.Max(10, num5);
                            AddStats(ct.name, num5, ct.size);
                            object[] objArray4 = new object[] { baseG.GetPhotonView().viewID, num5 };
                            ct.photonView.RPC("titanGetHit", ct.photonView.owner, objArray4);
                        }
                    }
                    else if (tit != null)
                    {
                        if (!tit.hasDie)
                        {
                            Vector3 vector7 = currentCameraT.main_object.rigidbody.velocity - item.transform.root.rigidbody.velocity;
                            int num6 = (int)((vector7.magnitude * 10f) * this.scoreMulti);
                            num6 = Mathf.Max(10, num6);
                            if (num6 > (tit.myLevel * 100f))
                            {
                                AddStats(tit.name, num6, tit.myLevel);
                                if ((int)FengGameManagerMKII.settings[320] == 1)
                                {
                                    IN_GAME_MAIN_CAMERA.instance.startSnapShot(item.transform.position, num6, item.transform.root.gameObject, 0.02f);
                                }
                                tit.titanGetHit(baseG.GetPhotonView().viewID, num6);
                            }
                        }
                    }
                    else if ((ft = item.transform.root.GetComponent<FEMALE_TITAN>()) != null)
                    {
                        if (!ft.hasDie)
                        {
                            Vector3 vector8 = currentCameraT.main_object.rigidbody.velocity - item.transform.root.rigidbody.velocity;
                            int num7 = (int)((vector8.magnitude * 10f) * this.scoreMulti);
                            num7 = Mathf.Max(10, num7);
                            AddStats(ft.name, num7, ft.size);
                            if ((int)FengGameManagerMKII.settings[320] == 1)
                            {
                                IN_GAME_MAIN_CAMERA.instance.startSnapShot(item.transform.position, num7, null, 0.02f);
                            }
                            ft.titanGetHit(baseG.GetPhotonView().viewID, num7);
                        }
                    }
                    else if (((ct = item.transform.root.GetComponent<COLOSSAL_TITAN>()) != null) && !ct.hasDie)
                    {
                        Vector3 vector9 = currentCameraT.main_object.rigidbody.velocity - item.transform.root.rigidbody.velocity;
                        int num8 = (int)((vector9.magnitude * 10f) * this.scoreMulti);
                        num8 = Mathf.Max(10, num8);
                        AddStats(ct.name, num8, ct.size);
                        if ((int)FengGameManagerMKII.settings[320] == 1)
                        {
                            IN_GAME_MAIN_CAMERA.instance.startSnapShot(item.transform.position, num8, null, 0.02f);
                        }
                        ct.titanGetHit(baseG.GetPhotonView().viewID, num8);
                    }
                    this.showCriticalHitFX(otherG.transform.position);
                }
            }
            else if (otherG.tag == "titaneye")
            {
                if (!this.currentHits.Contains(otherG))
                {
                    this.currentHits.Add(otherG);
                    GameObject gameObject = otherG.transform.root.gameObject;
                    FEMALE_TITAN ft = gameObject.GetComponent<FEMALE_TITAN>();
                    TITAN tit = null;
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
                                object[] objArray5 = new object[] { baseG.GetPhotonView().viewID };
                                ft.photonView.RPC("hitEyeRPC", PhotonTargets.MasterClient, objArray5);
                            }
                        }
                        else if (!ft.hasDie)
                        {
                            ft.hitEyeRPC(baseG.GetPhotonView().viewID);
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
                                object[] objArray6 = new object[] { baseG.GetPhotonView().viewID };
                                tit.photonView.RPC("hitEyeRPC", PhotonTargets.MasterClient, objArray6);
                            }
                        }
                        else if (!tit.hasDie)
                        {
                            tit.hitEyeRPC(baseG.GetPhotonView().viewID);
                        }
                        this.showCriticalHitFX(otherG.transform.position);
                    }
                }
            }
            else if ((otherG.tag == "titanankle") && !this.currentHits.Contains(otherG))
            {
                this.currentHits.Add(otherG);
                GameObject obj3 = otherG.transform.root.gameObject;
                Vector3 vector10 = currentCameraT.main_object.rigidbody.velocity - obj3.rigidbody.velocity;
                int num9 = (int)((vector10.magnitude * 10f) * this.scoreMulti);
                num9 = Mathf.Max(10, num9);
                TITAN tit = obj3.GetComponent<TITAN>();
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
                                object[] objArray7 = new object[] { baseG.GetPhotonView().viewID };
                                tit.photonView.RPC("hitAnkleRPC", PhotonTargets.MasterClient, objArray7);
                            }
                        }
                        else if (!tit.hasDie)
                        {
                            tit.hitAnkle();
                        }
                        this.showCriticalHitFX(otherG.transform.position);
                    }
                }
                else if ((ft = obj3.GetComponent<FEMALE_TITAN>()) != null)
                {
                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                    {
                        if (otherG.name == "ankleR")
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
                    else if (otherG.name == "ankleR")
                    {
                        if (!PhotonNetwork.isMasterClient)
                        {
                            if (!ft.hasDie)
                            {
                                object[] objArray8 = new object[] { baseG.GetPhotonView().viewID, num9 };
                                ft.photonView.RPC("hitAnkleRRPC", PhotonTargets.MasterClient, objArray8);
                            }
                        }
                        else if (!ft.hasDie)
                        {
                            ft.hitAnkleRRPC(baseG.GetPhotonView().viewID, num9);
                        }
                    }
                    else if (!PhotonNetwork.isMasterClient)
                    {
                        if (!ft.hasDie)
                        {
                            object[] objArray9 = new object[] { baseG.GetPhotonView().viewID, num9 };
                            ft.photonView.RPC("hitAnkleLRPC", PhotonTargets.MasterClient, objArray9);
                        }
                    }
                    else if (!ft.hasDie)
                    {
                        ft.hitAnkleLRPC(baseG.GetPhotonView().viewID, num9);
                    }
                    this.showCriticalHitFX(otherG.transform.position);
                }
            }
        }
    }

    private void showCriticalHitFX(Vector3 position)
    {
        GameObject obj2;
        currentCameraT.startShake(0.2f, 0.3f, 0.95f);
        if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)
        {
            obj2 = PhotonNetwork.Instantiate("redCross1", baseT.position, Quaternion.Euler(270f, 0f, 0f), 0);
        }
        else
        {
            obj2 = (GameObject)UnityEngine.Object.Instantiate(Cach.redCross1 != null ? Cach.redCross1 : Cach.redCross1 = (GameObject)Resources.Load("redCross1"));
        }
        obj2.transform.position = position;
    }

    private void Start()
    {
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
        {
            if (!baseG.GetPhotonView().isMine)
            {
                base.enabled = false;
                return;
            }
            EnemyfxIDcontainer xf = baseG.GetComponent<EnemyfxIDcontainer>();
            if (xf != null)
            {
                this.viewID = xf.myOwnerViewID;
                this.ownerName = xf.titanName;
                this.myTeam = PhotonView.Find(this.viewID).gameObject.GetComponent<HERO>().myTeam;
            }
        }
        else
        {
            this.myTeam = IN_GAME_MAIN_CAMERA.instance.main_objectH.myTeam;
        }
        this.active_me = true;
        this.count = 0;
        this.currentCamera = CyanMod.CachingsGM.Find("MainCamera");
        currentCameraT = currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>();
    }
}

