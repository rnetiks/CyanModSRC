using Photon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

public class COLOSSAL_TITAN : Photon.MonoBehaviour
{
    private string actionName;
    private string attackAnimation;
    private float attackCheckTime;
    private float attackCheckTimeA;
    private float attackCheckTimeB;
    private bool attackChkOnce;
    private int attackCount;
    private int attackPattern = -1;
    public GameObject bottomObject;
    private Transform checkHitCapsuleEnd;
    private Vector3 checkHitCapsuleEndOld;
    private float checkHitCapsuleR;
    private Transform checkHitCapsuleStart;
    public GameObject door_broken;
    public GameObject door_closed;
    public bool hasDie;
    public bool hasspawn;
    public GameObject healthLabel;
    public float healthTime;
    private bool isSteamNeed;
    public float lagMax;
    public int maxHealth;
    public static float minusDistance = 99999f;
    public static GameObject minusDistanceEnemy;
    public float myDistance;
    public GameObject myHero;
    public int NapeArmor = 0x2710;
    public int NapeArmorTotal = 0x2710;
    public GameObject neckSteamObject;
    public float size;
    private string state = "idle";
    public GameObject sweepSmokeObject;
    public float tauntTime;
    private float waitTime = 2f;
    Transform baseT;
    Rigidbody baseR;
    GameObject baseG;
    UILabel healthLabelT;

    private void attack_sweep(string type = "")
    {
        this.callTitanHAHA();
        this.state = "attack_sweep";
        this.attackAnimation = "sweep" + type;
        this.attackCheckTimeA = 0.4f;
        this.attackCheckTimeB = 0.57f;
        this.checkHitCapsuleStart = baseT.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R");
        this.checkHitCapsuleEnd = baseT.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R/hand_R_001");
        this.checkHitCapsuleR = 20f;
        this.crossFade("attack_" + this.attackAnimation, 0.1f);
        this.attackChkOnce = false;
        ParticleSystem pd = this.sweepSmokeObject.GetComponent<ParticleSystem>();
        pd.enableEmission = true;
        pd.Play();
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
        {
            if (FengGameManagerMKII.LAN)
            {
                if (Network.peerType == NetworkPeerType.Server)
                {
                }
            }
            else if (PhotonNetwork.isMasterClient)
            {
                base.photonView.RPC("startSweepSmoke", PhotonTargets.Others, new object[0]);
            }
        }
    }

    private void Awake()
    {
         baseT = base.transform;
         baseR = base.rigidbody;
         baseG = base.gameObject;
        baseR.freezeRotation = true;
        baseR.useGravity = false;
        baseR.isKinematic = true;
    }
    bool isLevlCT
    {
        get
        {
            return FengGameManagerMKII.lvlInfo.type == GAMEMODE.BOSS_FIGHT_CT;
        }
    }
    public void beTauntedBy(GameObject target, float tauntTime)
    {
    }

    public void blowPlayer(HERO hero, Transform neck)
    {
        Vector3 vector = (new Vector3()) - ((neck.position + (baseT.forward * 50f)) - hero.transform.position);
        float num = 20f;
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
        {
            hero.blowAway((Vector3)((vector.normalized * num) + (Vector3.up * 1f)));
        }
        else if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && PhotonNetwork.isMasterClient)
        {
            object[] parameters = new object[] { (Vector3) ((vector.normalized * num) + (Vector3.up * 1f)) };
            hero.photonView.RPC("blowAway", PhotonTargets.All, parameters);
        }
    }


    private void callTitan(bool special = false)
    {
        if (special || (FengGameManagerMKII.instance.alltitans.Count <= 6))
        {
          
            Vector3 vector12 = Vector3.zero;
            bool flag1 = isLevlCT;
            if (flag1)
            {
                GameObject[] objArray = GameObject.FindGameObjectsWithTag("titanRespawn");
                ArrayList list = new ArrayList();
                foreach (GameObject obj3 in objArray)
                {
                    if (obj3.transform.parent.name == "titanRespawnCT")
                    {
                        list.Add(obj3);
                    }
                }
                vector12 = ((GameObject)list[UnityEngine.Random.Range(0, list.Count)]).transform.position;
            }
            else
            {
                Vector3 tit = baseG.transform.position;
                vector12 = new Vector3(UnityEngine.Random.Range(tit.x - 10, 20), tit.y, UnityEngine.Random.Range(tit.z - 10, 20));
            }
         

         
            TITAN titan;
            if (FengGameManagerMKII.LAN)
            {
                titan = ((GameObject)Network.Instantiate(Cach.TITAN_VER3_1 != null ? Cach.TITAN_VER3_1 : Cach.TITAN_VER3_1 = (GameObject)Resources.Load("TITAN_VER3.1"), vector12, Quaternion.identity, 0)).GetComponent<TITAN>();
            }
            else
            {
                titan = (PhotonNetwork.Instantiate("TITAN_VER3.1", vector12, Quaternion.identity, 0)).GetComponent<TITAN>();
            }

            if (special && flag1)
            {
                GameObject[] objArray2 = GameObject.FindGameObjectsWithTag("route");
                GameObject route = objArray2[UnityEngine.Random.Range(0, objArray2.Length)];
                while (route.name != "routeCT")
                {
                    route = objArray2[UnityEngine.Random.Range(0, objArray2.Length)];
                }
                titan.setRoute(route);
                titan.setAbnormalType2(AbnormalType.TYPE_I, false);
                titan.activeRad = 0;
                titan.toCheckPoint((Vector3)titan.checkPoints[0], 10f);
            }
            else
            {
                float num = 0.7f;
                float num2 = 0.7f;
                if (IN_GAME_MAIN_CAMERA.difficulty != DIFFICULTY.NORMAL)
                {
                    if (IN_GAME_MAIN_CAMERA.difficulty == DIFFICULTY.HARD)
                    {
                        num = 0.4f;
                        num2 = 0.7f;
                    }
                    else if (IN_GAME_MAIN_CAMERA.difficulty == DIFFICULTY.ABNORMAL)
                    {
                        num = -1f;
                        num2 = 0.7f;
                    }
                }
                if (FengGameManagerMKII.instance.alltitans.Count == 5)
                {
                    titan.setAbnormalType2(AbnormalType.TYPE_JUMPER, false);
                }
                else if (UnityEngine.Random.Range((float) 0f, (float) 1f) >= num)
                {
                    if (UnityEngine.Random.Range((float) 0f, (float) 1f) < num2)
                    {
                        titan.setAbnormalType2(AbnormalType.TYPE_JUMPER, false);
                    }
                    else
                    {
                        titan.setAbnormalType2(AbnormalType.TYPE_CRAWLER, false);
                    }
                }
                titan.activeRad = 200;
            }
            Transform titanTransform = titan.transform;
            if (FengGameManagerMKII.LAN)
            {
                GameObject obj6 = (GameObject)Network.Instantiate(Resources.Load("FX/FXtitanSpawn"), titanTransform.position, Quaternion.Euler(-90f, 0f, 0f), 0);
                obj6.transform.localScale = titanTransform.localScale;
            }
            else
            {
                PhotonNetwork.Instantiate("FX/FXtitanSpawn", titanTransform.position, Quaternion.Euler(-90f, 0f, 0f), 0).transform.localScale = titanTransform.localScale;
            }
        }
    }

    private void callTitanHAHA()
    {
        this.attackCount++;
        int num = 4;
        int num2 = 7;
        if (IN_GAME_MAIN_CAMERA.difficulty != DIFFICULTY.NORMAL)
        {
            if (IN_GAME_MAIN_CAMERA.difficulty == DIFFICULTY.HARD)
            {
                num = 4;
                num2 = 6;
            }
            else if (IN_GAME_MAIN_CAMERA.difficulty == DIFFICULTY.ABNORMAL)
            {
                num = 3;
                num2 = 5;
            }
        }
        if ((this.attackCount % num) == 0)
        {
            this.callTitan(false);
        }
        if (this.NapeArmor < (this.NapeArmorTotal * 0.3))
        {
            if ((this.attackCount % ((int) (num2 * 0.5f))) == 0)
            {
                this.callTitan(true);
            }
        }
        else if ((this.attackCount % num2) == 0)
        {
            this.callTitan(true);
        }
    }

    [RPC]
    private void changeDoor()
    {
        this.door_broken.SetActive(true);
        this.door_closed.SetActive(false);
    }

    private RaycastHit[] checkHitCapsule(Vector3 start, Vector3 end, float r)
    {
        return Physics.SphereCastAll(start, r, end - start, Vector3.Distance(start, end));
    }

    private GameObject checkIfHitHand(Transform hand)
    {
        foreach (Collider collider in Physics.OverlapSphere(hand.GetComponent<SphereCollider>().transform.position, 31f))
        {
            if (collider.transform.root.tag == "Player")
            {
                GameObject gameObject = collider.transform.root.gameObject;
                TITAN_EREN te = gameObject.GetComponent<TITAN_EREN>();
                if (te != null)
                {
                    if (!te.isHit)
                    {
                        te.hitByTitan();
                    }
                    return gameObject;
                }
                HERO he = gameObject.GetComponent<HERO>();
                if ((he != null) && !he.isInvincible())
                {
                    return gameObject;
                }
            }
        }
        return null;
    }

    private void crossFade(string aniName, float time)
    {
        base.animation.CrossFade(aniName, time);
        if ((!FengGameManagerMKII.LAN && (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)) && PhotonNetwork.isMasterClient)
        {
            object[] parameters = new object[] { aniName, time };
            base.photonView.RPC("netCrossFade", PhotonTargets.Others, parameters);
        }
    }
    public bool net_searh = true;
    private void findNearestHero()
    {
        if (net_searh)
        {
            this.myHero = this.getNearestHero();
        }
    }

    private GameObject getNearestHero()
    {
      
        GameObject obj2 = null;
        float positiveInfinity = float.PositiveInfinity;
        foreach (GameObject obj3 in FengGameManagerMKII.instance.allheroes)
        {
            if (((obj3.GetComponent<HERO>() == null) || !obj3.GetComponent<HERO>().HasDied()) && ((obj3.GetComponent<TITAN_EREN>() == null) || !obj3.GetComponent<TITAN_EREN>().hasDied))
            {
                float num2 = Mathf.Sqrt(((obj3.transform.position.x - baseT.position.x) * (obj3.transform.position.x - baseT.position.x)) + ((obj3.transform.position.z - baseT.position.z) * (obj3.transform.position.z - baseT.position.z)));
                if (((obj3.transform.position.y - baseT.position.y) < 450f) && (num2 < positiveInfinity))
                {
                    obj2 = obj3;
                    positiveInfinity = num2;
                }
            }
        }
        return obj2;
    }

    private void idle()
    {
        this.state = "idle";
        this.crossFade("idle", 0.2f);
    }

    private void kick()
    {
        this.state = "kick";
        this.actionName = "attack_kick_wall";
        this.attackCheckTime = 0.64f;
        this.attackChkOnce = false;
        this.crossFade(this.actionName, 0.1f);
    }

    private void killPlayer(HERO hero)
    {
        if (hero != null)
        {
            Vector3 position = baseT.Find("Amarture/Core/Controller_Body/hip/spine/chest").position;
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                if (!hero.HasDied())
                {
                    hero.die((Vector3)(((hero.transform.position - position) * 15f) * 4f), false);
                }
            }
            else if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
            {
                if (FengGameManagerMKII.LAN)
                {
                    if (!hero.HasDied())
                    {
                        hero.markDie();
                    }
                }
                else if (!hero.HasDied())
                {
                    hero.markDie();
                    object[] parameters = new object[] { (Vector3)(((hero.transform.position - position) * 15f) * 4f), false, -1, "Colossal Titan", true };
                    hero.photonView.RPC("netDie", PhotonTargets.All, parameters);
                }
            }
        }
    }

    [RPC]
    public void labelRPC(int health, int maxHealth)
    {
        if (health < 0)
        {
            if (this.healthLabel != null)
            {
                UnityEngine.Object.Destroy(this.healthLabel);
            }
        }
        else
        {
            if (this.healthLabel == null)
            {
                this.healthLabel = (GameObject)UnityEngine.Object.Instantiate(Cach.UI_LabelNameOverHead != null ? Cach.UI_LabelNameOverHead:Cach.UI_LabelNameOverHead = (GameObject) Cach.UI_LabelNameOverHead != null ? Cach.UI_LabelNameOverHead:Cach.UI_LabelNameOverHead = (GameObject) Resources.Load("UI/LabelNameOverHead"));
                this.healthLabel.name = "LabelNameOverHead";
                this.healthLabel.transform.parent = baseT;
                this.healthLabel.transform.localPosition = new Vector3(0f, 430f, 0f);
                float a = 15f;
                if ((this.size > 0f) && (this.size < 1f))
                {
                    a = 15f / this.size;
                    a = Mathf.Min(a, 100f);
                }
                this.healthLabel.transform.localScale = new Vector3(a, a, a);
                healthLabelT = healthLabel.GetComponent<UILabel>();
            }
            string str = "[7FFF00]";
            float num2 = ((float) health) / ((float) maxHealth);
            if ((num2 < 0.75f) && (num2 >= 0.5f))
            {
                str = "[f2b50f]";
            }
            else if ((num2 < 0.5f) && (num2 >= 0.25f))
            {
                str = "[ff8100]";
            }
            else if (num2 < 0.25f)
            {
                str = "[ff3333]";
            }
            healthLabelT.text = str + Convert.ToString(health);
        }
    }

    public void loadskin()
    {
        if (PhotonNetwork.isMasterClient && (((int) FengGameManagerMKII.settings[1]) == 1))
        {
            base.photonView.RPC("loadskinRPC", PhotonTargets.AllBuffered, new object[] { (string) FengGameManagerMKII.settings[0x43] });
        }
    }

    public IEnumerator loadskinE(string url)
    {
        while (!this.hasspawn)
        {
            yield return null;
        }
        bool mipmap = true;
        bool iteratorVariable1 = false;
        if (((int) FengGameManagerMKII.settings[0x3f]) == 1)
        {
            mipmap = false;
        }
        foreach (Renderer iteratorVariable2 in this.GetComponentsInChildren<Renderer>())
        {
            if (iteratorVariable2.name.Contains("hair"))
            {
                if (!FengGameManagerMKII.linkHash[2].ContainsKey(url))
                {
                    WWW link = new WWW(url);
                    yield return link;
                    Texture2D iteratorVariable4 = cext.loadimage(link, mipmap, 0xf4240);
                    link.Dispose();
                    if (!FengGameManagerMKII.linkHash[2].ContainsKey(url))
                    {
                        iteratorVariable1 = true;
                        iteratorVariable2.material.mainTexture = iteratorVariable4;
                        FengGameManagerMKII.linkHash[2].Add(url, iteratorVariable2.material);
                        iteratorVariable2.material = (Material) FengGameManagerMKII.linkHash[2][url];
                    }
                    else
                    {
                        iteratorVariable2.material = (Material) FengGameManagerMKII.linkHash[2][url];
                    }
                }
                else
                {
                    iteratorVariable2.material = (Material) FengGameManagerMKII.linkHash[2][url];
                }
            }
        }
        if (iteratorVariable1)
        {
            FengGameManagerMKII.instance.unloadAssets();
        }
        yield break;
    }

    [RPC]
    public void loadskinRPC(string url)
    {
        if ((((int) FengGameManagerMKII.settings[1]) == 1) && ((url.EndsWith(".jpg") || url.EndsWith(".png")) || url.EndsWith(".jpeg")))
        {
            base.StartCoroutine(this.loadskinE(url));
        }
    }

    private void neckSteam()
    {
        this.neckSteamObject.GetComponent<ParticleSystem>().Stop();
        this.neckSteamObject.GetComponent<ParticleSystem>().Play();
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
        {
            if (FengGameManagerMKII.LAN)
            {
                if (Network.peerType == NetworkPeerType.Server)
                {
                }
            }
            else if (PhotonNetwork.isMasterClient)
            {
                base.photonView.RPC("startNeckSteam", PhotonTargets.Others, new object[0]);
            }
        }
        this.isSteamNeed = true;
        Transform neck = baseT.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck");
        float radius = 30f;
        foreach (Collider collider in Physics.OverlapSphere(neck.transform.position - ((Vector3) (baseT.forward * 10f)), radius))
        {
            if (collider.transform.root.tag == "Player")
            {
                GameObject gameObject = collider.transform.root.gameObject;
                if ((gameObject.GetComponent<TITAN_EREN>() == null) && (gameObject.GetComponent<HERO>() != null))
                {
                    this.blowPlayer(gameObject.GetComponent<HERO>(), neck);
                }
            }
        }
    }

    [RPC]
    private void netCrossFade(string aniName, float time)
    {
        base.animation.CrossFade(aniName, time);
    }

    [RPC]
    public void netDie()
    {
        if (!this.hasDie)
        {
            this.hasDie = true;
        }
    }

    [RPC]
    private void netPlayAnimation(string aniName)
    {
        base.animation.Play(aniName);
    }

    [RPC]
    private void netPlayAnimationAt(string aniName, float normalizedTime)
    {
        base.animation.Play(aniName);
        base.animation[aniName].normalizedTime = normalizedTime;
    }

    private void OnDestroy()
    {
        if (FengGameManagerMKII.instance != null)
        {
            FengGameManagerMKII.instance.removeCT(this);
        }
    }

    private void playAnimation(string aniName)
    {
        base.animation.Play(aniName);
        if ((!FengGameManagerMKII.LAN && (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)) && PhotonNetwork.isMasterClient)
        {
            object[] parameters = new object[] { aniName };
            base.photonView.RPC("netPlayAnimation", PhotonTargets.Others, parameters);
        }
    }

    private void playAnimationAt(string aniName, float normalizedTime)
    {
        base.animation.Play(aniName);
        base.animation[aniName].normalizedTime = normalizedTime;
        if ((!FengGameManagerMKII.LAN && (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)) && PhotonNetwork.isMasterClient)
        {
            object[] parameters = new object[] { aniName, normalizedTime };
            base.photonView.RPC("netPlayAnimationAt", PhotonTargets.Others, parameters);
        }
    }

    private void playSound(string sndname)
    {
        this.playsoundRPC(sndname);
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
        {
            if (FengGameManagerMKII.LAN)
            {
                if (Network.peerType == NetworkPeerType.Server)
                {
                }
            }
            else if (PhotonNetwork.isMasterClient)
            {
                object[] parameters = new object[] { sndname };
                base.photonView.RPC("playsoundRPC", PhotonTargets.Others, parameters);
            }
        }
    }

    [RPC]
    private void playsoundRPC(string sndname)
    {
        if ((int)FengGameManagerMKII.settings[330] == 0)
        {
            baseT.Find(sndname).GetComponent<AudioSource>().Play();
        }
    }

    [RPC]
    private void removeMe()
    {
        UnityEngine.Object.Destroy(baseG);
    }

    [RPC]
    public void setSize(float size, PhotonMessageInfo info)
    {
        size = Mathf.Clamp(size, 0.1f, 50f);
        if (info.sender.isMasterClient)
        {
            Transform transform = baseT;
            transform.localScale = (Vector3) (transform.localScale * (size * 0.05f));
            this.size = size;
        }
    }

    private void slap(string type)
    {
        this.callTitanHAHA();
        this.state = "slap";
        this.attackAnimation = type;
        if ((type == "r1") || (type == "r2"))
        {
            this.checkHitCapsuleStart = baseT.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R/hand_R_001");
        }
        if ((type == "l1") || (type == "l2"))
        {
            this.checkHitCapsuleStart = baseT.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_L/upper_arm_L/forearm_L/hand_L/hand_L_001");
        }
        this.attackCheckTime = 0.57f;
        this.attackChkOnce = false;
        this.crossFade("attack_slap_" + this.attackAnimation, 0.1f);
    }

    private void Start()
    {
        this.startMain();
        this.size = 20f;
        if (Minimap.instance != null)
        {
            Minimap.instance.TrackGameObjectOnMinimap(baseG, Color.black, false, true, Minimap.IconStyle.CIRCLE);
        }
        if (base.photonView.isMine)
        {
            if (RCSettings.sizeMode > 0)
            {
                float sizeLower = RCSettings.sizeLower;
                float sizeUpper = RCSettings.sizeUpper;
                this.size = UnityEngine.Random.Range(sizeLower, sizeUpper);
                base.photonView.RPC("setSize", PhotonTargets.AllBuffered, new object[] { this.size });
            }
            this.lagMax = 150f + (this.size * 3f);
            this.healthTime = 0f;
            this.maxHealth = this.NapeArmor;
            if (RCSettings.healthMode > 0)
            {
                this.maxHealth = this.NapeArmor = UnityEngine.Random.Range(RCSettings.healthLower, RCSettings.healthUpper);
            }
            if (this.NapeArmor > 0)
            {
                base.photonView.RPC("labelRPC", PhotonTargets.AllBuffered, new object[] { this.NapeArmor, this.maxHealth });
            }
            this.loadskin();
        }
        this.hasspawn = true;
    }

    private void startMain()
    {
        FengGameManagerMKII.instance.addCT(this);
        if (this.myHero == null)
        {
            this.findNearestHero();
        }
        base.name = "COLOSSAL_TITAN";
        this.NapeArmor = 0x3e8;
        bool flag = false;
        if (FengGameManagerMKII.lvlInfo.respawnMode == RespawnMode.NEVER)
        {
            flag = true;
        }
        if (IN_GAME_MAIN_CAMERA.difficulty == DIFFICULTY.NORMAL)
        {
            this.NapeArmor = !flag ? 0x1388 : 0x7d0;
        }
        else if (IN_GAME_MAIN_CAMERA.difficulty == DIFFICULTY.HARD)
        {
            this.NapeArmor = !flag ? 0x1f40 : 0xdac;
            foreach (AnimationState state in base.animation)
            {
                if (state != null)
                {
                    state.speed = 1.02f;
                }
            }
        }
        else if (IN_GAME_MAIN_CAMERA.difficulty == DIFFICULTY.ABNORMAL)
        {
            this.NapeArmor = !flag ? 0x2ee0 : 0x1388;
            foreach (AnimationState state2 in base.animation)
            {
                if (state2 != null)
                {
                    state2.speed = 1.05f;
                }
            }
        }
        this.NapeArmorTotal = this.NapeArmor;
        this.state = "wait";
        baseT.position += (Vector3) (-Vector3.up * 10000f);
        if (FengGameManagerMKII.LAN)
        {
            base.GetComponent<PhotonView>().enabled = false;
        }
        else
        {
            base.GetComponent<NetworkView>().enabled = false;
        }
        if (isLevlCT)
        {
            this.door_broken = CyanMod.CachingsGM.Find("door_broke");
            this.door_closed = CyanMod.CachingsGM.Find("door_fine");
            this.door_broken.SetActive(false);
            this.door_closed.SetActive(true);
        }
    }

    [RPC]
    private void startNeckSteam()
    {
        this.neckSteamObject.GetComponent<ParticleSystem>().Stop();
        this.neckSteamObject.GetComponent<ParticleSystem>().Play();
    }

    [RPC]
    private void startSweepSmoke()
    {
        ParticleSystem pd = this.sweepSmokeObject.GetComponent<ParticleSystem>();
        pd.enableEmission = true;
        pd.Play();
    }

    private void steam()
    {
        this.callTitanHAHA();
        this.state = "steam";
        this.actionName = "attack_steam";
        this.attackCheckTime = 0.45f;
        this.crossFade(this.actionName, 0.1f);
        this.attackChkOnce = false;
    }

    [RPC]
    private void stopSweepSmoke()
    {
        ParticleSystem pd = this.sweepSmokeObject.GetComponent<ParticleSystem>();
        pd.enableEmission = false;
        pd.Stop();
    }

    [RPC]
    public void titanGetHit(int viewID, int speed)
    {
        Transform transform = baseT.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck");
        PhotonView view = PhotonView.Find(viewID);
        if (view != null)
        {
            Vector3 vector = view.gameObject.transform.position - transform.transform.position;
            if ((vector.magnitude < this.lagMax) && (this.healthTime <= 0f))
            {
                if (speed >= RCSettings.damageMode)
                {
                    this.NapeArmor -= speed;
                }
                if (this.maxHealth > 0f)
                {
                    base.photonView.RPC("labelRPC", PhotonTargets.AllBuffered, new object[] { this.NapeArmor, this.maxHealth });
                }
                this.neckSteam();
                if (this.NapeArmor <= 0)
                {
                    this.NapeArmor = 0;
                    if (!this.hasDie)
                    {
                        if (FengGameManagerMKII.LAN)
                        {
                            this.netDie();
                        }
                        else
                        {
                            base.photonView.RPC("netDie", PhotonTargets.OthersBuffered, new object[0]);
                            this.netDie();
                            FengGameManagerMKII.instance.titanGetKill(view.owner, speed, base.name);
                        }
                    }
                }
                else
                {
                    FengGameManagerMKII.instance.sendKillInfo(false, (string) view.owner.name2, true, "Colossal Titan's neck", speed);
                    object[] parameters = new object[] { speed };
                    FengGameManagerMKII.instance.photonView.RPC("netShowDamage", view.owner, parameters);
                }
                this.healthTime = 0.2f;
            }
        }
    }

    public void update()
    {
      
    }

    public void update2()
    {
        this.healthTime -= Time.deltaTime;
        this.updateLabel();
        if (this.state != "null")
        {
            if (this.state == "wait")
            {
                this.waitTime -= Time.deltaTime;
                if (this.waitTime <= 0f)
                {
                    baseT.position = new Vector3(30f, 0f, 784f);
                    UnityEngine.Object.Instantiate(Cach.ThunderCT != null ? Cach.ThunderCT : Cach.ThunderCT = (GameObject)Resources.Load("FX/ThunderCT"), baseT.position + ((Vector3)(Vector3.up * 350f)), Quaternion.Euler(270f, 0f, 0f));
                    IN_GAME_MAIN_CAMERA.instance.flashBlind();
                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                    {
                        this.idle();
                    }
                    else if (!FengGameManagerMKII.LAN ? base.photonView.isMine : base.networkView.isMine)
                    {
                        this.idle();
                    }
                    else
                    {
                        this.state = "null";
                    }
                }
            }
            else if (this.state != "idle")
            {
                if (this.state != "attack_sweep")
                {
                    if (this.state != "kick")
                    {
                        if (this.state != "slap")
                        {
                            if (this.state != "steam")
                            {
                                if (this.state != string.Empty)
                                {
                                }
                            }
                            else
                            {
                                if (!this.attackChkOnce && (base.animation[this.actionName].normalizedTime >= this.attackCheckTime))
                                {
                                    this.attackChkOnce = true;
                                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                                    {
                                        if (FengGameManagerMKII.LAN)
                                        {
                                            Network.Instantiate(Cach.colossal_steam != null ? Cach.colossal_steam : Cach.colossal_steam = (GameObject)Resources.Load("FX/colossal_steam"), baseT.position + ((Vector3)(baseT.up * 185f)), Quaternion.Euler(270f, 0f, 0f), 0);
                                            Network.Instantiate(Cach.colossal_steam != null ? Cach.colossal_steam : Cach.colossal_steam = (GameObject)Resources.Load("FX/colossal_steam"), baseT.position + ((Vector3) (baseT.up * 303f)), Quaternion.Euler(270f, 0f, 0f), 0);
                                            Network.Instantiate(Cach.colossal_steam != null ? Cach.colossal_steam : Cach.colossal_steam = (GameObject)Resources.Load("FX/colossal_steam"), baseT.position + ((Vector3) (baseT.up * 50f)), Quaternion.Euler(270f, 0f, 0f), 0);
                                        }
                                        else
                                        {
                                            PhotonNetwork.Instantiate("FX/colossal_steam", baseT.position + ((Vector3) (baseT.up * 185f)), Quaternion.Euler(270f, 0f, 0f), 0);
                                            PhotonNetwork.Instantiate("FX/colossal_steam", baseT.position + ((Vector3) (baseT.up * 303f)), Quaternion.Euler(270f, 0f, 0f), 0);
                                            PhotonNetwork.Instantiate("FX/colossal_steam", baseT.position + ((Vector3) (baseT.up * 50f)), Quaternion.Euler(270f, 0f, 0f), 0);
                                        }
                                    }
                                    else
                                    {
                                        UnityEngine.Object.Instantiate(Cach.colossal_steam != null ? Cach.colossal_steam : Cach.colossal_steam = (GameObject)Resources.Load("FX/colossal_steam"), baseT.position + ((Vector3) (baseT.forward * 185f)), Quaternion.Euler(270f, 0f, 0f));
                                        UnityEngine.Object.Instantiate(Cach.colossal_steam != null ? Cach.colossal_steam : Cach.colossal_steam = (GameObject)Resources.Load("FX/colossal_steam"), baseT.position + ((Vector3) (baseT.forward * 303f)), Quaternion.Euler(270f, 0f, 0f));
                                        UnityEngine.Object.Instantiate(Cach.colossal_steam != null ? Cach.colossal_steam : Cach.colossal_steam = (GameObject)Resources.Load("FX/colossal_steam"), baseT.position + ((Vector3) (baseT.forward * 50f)), Quaternion.Euler(270f, 0f, 0f));
                                    }
                                }
                                if (base.animation[this.actionName].normalizedTime >= 1f)
                                {
                                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                                    {
                                        if (FengGameManagerMKII.LAN)
                                        {
                                            Network.Instantiate(Cach.colossal_steam_dmg != null ? Cach.colossal_steam_dmg : Cach.colossal_steam_dmg = (GameObject)Resources.Load("FX/colossal_steam_dmg"), baseT.position + ((Vector3)(baseT.up * 185f)), Quaternion.Euler(270f, 0f, 0f), 0);
                                            Network.Instantiate(Cach.colossal_steam_dmg != null ? Cach.colossal_steam_dmg : Cach.colossal_steam_dmg = (GameObject)Resources.Load("FX/colossal_steam_dmg"), baseT.position + ((Vector3)(baseT.up * 303f)), Quaternion.Euler(270f, 0f, 0f), 0);
                                            Network.Instantiate(Cach.colossal_steam_dmg != null ? Cach.colossal_steam_dmg : Cach.colossal_steam_dmg = (GameObject)Resources.Load("FX/colossal_steam_dmg"), baseT.position + ((Vector3)(baseT.up * 50f)), Quaternion.Euler(270f, 0f, 0f), 0);
                                        }
                                        else
                                        {
                                            GameObject obj5 = PhotonNetwork.Instantiate("FX/colossal_steam_dmg", baseT.position + ((Vector3) (baseT.up * 185f)), Quaternion.Euler(270f, 0f, 0f), 0);
                                       EnemyfxIDcontainer fx = obj5.GetComponent<EnemyfxIDcontainer>();
                                       if (fx != null)
                                            {
                                                fx.titanName = base.name;
                                            }
                                            obj5 = PhotonNetwork.Instantiate("FX/colossal_steam_dmg", baseT.position + ((Vector3) (baseT.up * 303f)), Quaternion.Euler(270f, 0f, 0f), 0);
                                           fx = obj5.GetComponent<EnemyfxIDcontainer>();
                                            if (fx != null)
                                            {
                                                fx.titanName = base.name;
                                            }
                                            obj5 = PhotonNetwork.Instantiate("FX/colossal_steam_dmg", baseT.position + ((Vector3) (baseT.up * 50f)), Quaternion.Euler(270f, 0f, 0f), 0);
                                            fx = obj5.GetComponent<EnemyfxIDcontainer>();
                                            if (fx != null)
                                            {
                                                fx.titanName = base.name;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        UnityEngine.Object.Instantiate(Cach.colossal_steam_dmg != null ? Cach.colossal_steam_dmg : Cach.colossal_steam_dmg = (GameObject)Resources.Load("FX/colossal_steam_dmg"), baseT.position + ((Vector3)(baseT.forward * 185f)), Quaternion.Euler(270f, 0f, 0f));
                                        UnityEngine.Object.Instantiate(Cach.colossal_steam_dmg != null ? Cach.colossal_steam_dmg : Cach.colossal_steam_dmg = (GameObject)Resources.Load("FX/colossal_steam_dmg"), baseT.position + ((Vector3)(baseT.forward * 303f)), Quaternion.Euler(270f, 0f, 0f));
                                        UnityEngine.Object.Instantiate(Cach.colossal_steam_dmg != null ? Cach.colossal_steam_dmg : Cach.colossal_steam_dmg = (GameObject)Resources.Load("FX/colossal_steam_dmg"), baseT.position + ((Vector3)(baseT.forward * 50f)), Quaternion.Euler(270f, 0f, 0f));
                                    }
                                    if (this.hasDie)
                                    {
                                        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                                        {
                                            UnityEngine.Object.Destroy(baseG);
                                        }
                                        else if (FengGameManagerMKII.LAN)
                                        {
                                            if (!base.networkView.isMine)
                                            {
                                            }
                                        }
                                        else if (PhotonNetwork.isMasterClient)
                                        {
                                            PhotonNetwork.Destroy(base.photonView);
                                        }
                                        FengGameManagerMKII.instance.gameWin2();
                                    }
                                    this.findNearestHero();
                                    this.idle();
                                    this.playAnimation("idle");
                                }
                            }
                        }
                        else
                        {
                            if (!this.attackChkOnce && (base.animation["attack_slap_" + this.attackAnimation].normalizedTime >= this.attackCheckTime))
                            {
                                GameObject obj4;
                                this.attackChkOnce = true;
                                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                                {
                                    if (FengGameManagerMKII.LAN)
                                    {
                                        obj4 = (GameObject)Network.Instantiate(Cach.FX_boom1 != null ? Cach.FX_boom1 : Cach.FX_boom1 = (GameObject)Resources.Load("FX/boom1"), this.checkHitCapsuleStart.position, Quaternion.Euler(270f, 0f, 0f), 0);
                                    }
                                    else
                                    {
                                        obj4 = PhotonNetwork.Instantiate("FX/boom1", this.checkHitCapsuleStart.position, Quaternion.Euler(270f, 0f, 0f), 0);
                                    }
                                    EnemyfxIDcontainer fx = obj4.GetComponent<EnemyfxIDcontainer>();
                                    if (fx != null)
                                    {
                                        fx.titanName = base.name;
                                    }
                                }
                                else
                                {
                                    obj4 = (GameObject)UnityEngine.Object.Instantiate(Cach.FX_boom1 != null ? Cach.FX_boom1 : Cach.FX_boom1 = (GameObject)Resources.Load("FX/boom1"), this.checkHitCapsuleStart.position, Quaternion.Euler(270f, 0f, 0f));
                                }
                                obj4.transform.localScale = new Vector3(5f, 5f, 5f);
                            }
                            if (base.animation["attack_slap_" + this.attackAnimation].normalizedTime >= 1f)
                            {
                                this.findNearestHero();
                                this.idle();
                                this.playAnimation("idle");
                            }
                        }
                    }
                    else
                    {
                        if (!this.attackChkOnce && (base.animation[this.actionName].normalizedTime >= this.attackCheckTime))
                        {
                            this.attackChkOnce = true;
                            bool flag = isLevlCT;
                            if (flag)
                            {
                                this.door_broken.SetActive(true);
                                this.door_closed.SetActive(false);
                            }
                            if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && !FengGameManagerMKII.LAN && flag)
                            {
                                base.photonView.RPC("changeDoor", PhotonTargets.OthersBuffered, new object[0]);
                            }
                            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                            {
                                if (FengGameManagerMKII.LAN)
                                {
                                    Network.Instantiate(Cach.FX_boom1_CT_KICK != null ? Cach.FX_boom1_CT_KICK : Cach.FX_boom1_CT_KICK = (GameObject)Resources.Load("FX/boom1_CT_KICK"), (Vector3)((baseT.position + (baseT.forward * 120f)) + (baseT.right * 30f)), Quaternion.Euler(270f, 0f, 0f), 0);
                                    Network.Instantiate(Cach.rock != null ? Cach.rock : Cach.rock = (GameObject)Resources.Load("rock"), (Vector3)((baseT.position + (baseT.forward * 120f)) + (baseT.right * 30f)), Quaternion.Euler(0f, 0f, 0f), 0);
                                }
                                else
                                {
                                    PhotonNetwork.Instantiate("FX/boom1_CT_KICK", (Vector3) ((baseT.position + (baseT.forward * 120f)) + (baseT.right * 30f)), Quaternion.Euler(270f, 0f, 0f), 0);
                                    PhotonNetwork.Instantiate("rock", (Vector3) ((baseT.position + (baseT.forward * 120f)) + (baseT.right * 30f)), Quaternion.Euler(0f, 0f, 0f), 0);
                                }
                            }
                            else
                            {
                                UnityEngine.Object.Instantiate(Cach.FX_boom1_CT_KICK != null ? Cach.FX_boom1_CT_KICK : Cach.FX_boom1_CT_KICK = (GameObject)Resources.Load("FX/boom1_CT_KICK"), (Vector3)((baseT.position + (baseT.forward * 120f)) + (baseT.right * 30f)), Quaternion.Euler(270f, 0f, 0f));
                                UnityEngine.Object.Instantiate(Cach.rock != null ? Cach.rock : Cach.rock = (GameObject)Resources.Load("rock"), (Vector3)((baseT.position + (baseT.forward * 120f)) + (baseT.right * 30f)), Quaternion.Euler(0f, 0f, 0f));
                            }
                        }
                        if (base.animation[this.actionName].normalizedTime >= 1f)
                        {
                            this.findNearestHero();
                            this.idle();
                            this.playAnimation("idle");
                        }
                    }
                }
                else
                {
                    if ((this.attackCheckTimeA != 0f) && !(((base.animation["attack_" + this.attackAnimation].normalizedTime < this.attackCheckTimeA) || (base.animation["attack_" + this.attackAnimation].normalizedTime > this.attackCheckTimeB)) ? (this.attackChkOnce || (base.animation["attack_" + this.attackAnimation].normalizedTime < this.attackCheckTimeA)) : false))
                    {
                        if (!this.attackChkOnce)
                        {
                            this.attackChkOnce = true;
                        }
                        foreach (RaycastHit hit in this.checkHitCapsule(this.checkHitCapsuleStart.position, this.checkHitCapsuleEnd.position, this.checkHitCapsuleR))
                        {
                            GameObject gameObject = hit.collider.gameObject;
                            if (gameObject.tag == "Player")
                            {
                                this.killPlayer(gameObject.GetComponent<HERO>());
                            }
                            if ((((gameObject.tag == "erenHitbox") && (this.attackAnimation == "combo_3")) && (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)) && (!FengGameManagerMKII.LAN ? PhotonNetwork.isMasterClient : Network.isServer))
                            {
                                gameObject.transform.root.gameObject.GetComponent<TITAN_EREN>().hitByFTByServer(3);
                            }
                        }
                        foreach (RaycastHit hit2 in this.checkHitCapsule(this.checkHitCapsuleEndOld, this.checkHitCapsuleEnd.position, this.checkHitCapsuleR))
                        {
                            GameObject hitHero = hit2.collider.gameObject;
                            if (hitHero.tag == "Player")
                            {
                                this.killPlayer(hitHero.GetComponent<HERO>());
                            }
                        }
                        this.checkHitCapsuleEndOld = this.checkHitCapsuleEnd.position;
                    }
                    if (base.animation["attack_" + this.attackAnimation].normalizedTime >= 1f)
                    {
                        ParticleSystem ps = this.sweepSmokeObject.GetComponent<ParticleSystem>();
                        ps.enableEmission = false;
                        ps.Stop();
                        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && !FengGameManagerMKII.LAN)
                        {
                            base.photonView.RPC("stopSweepSmoke", PhotonTargets.Others, new object[0]);
                        }
                        this.findNearestHero();
                        this.idle();
                        this.playAnimation("idle");
                    }
                }
            }
            else if (this.attackPattern == -1)
            {
                this.slap("r1");
                this.attackPattern++;
            }
            else if (this.attackPattern == 0)
            {
                this.attack_sweep(string.Empty);
                this.attackPattern++;
            }
            else if (this.attackPattern == 1)
            {
                this.steam();
                this.attackPattern++;
            }
            else if (this.attackPattern == 2)
            {
                this.kick();
                this.attackPattern++;
            }
            else if (this.isSteamNeed || this.hasDie)
            {
                this.steam();
                this.isSteamNeed = false;
            }
            else if (this.myHero == null)
            {
                this.findNearestHero();
            }
            else
            {
                Vector3 vector = this.myHero.transform.position - baseT.position;
                float current = -Mathf.Atan2(vector.z, vector.x) * 57.29578f;
                float f = -Mathf.DeltaAngle(current, baseG.transform.rotation.eulerAngles.y - 90f);
                this.myDistance = Mathf.Sqrt(((this.myHero.transform.position.x - baseT.position.x) * (this.myHero.transform.position.x - baseT.position.x)) + ((this.myHero.transform.position.z - baseT.position.z) * (this.myHero.transform.position.z - baseT.position.z)));
                float num3 = this.myHero.transform.position.y - baseT.position.y;
                if ((this.myDistance < 85f) && (UnityEngine.Random.Range(0, 100) < 5))
                {
                    this.steam();
                }
                else
                {
                    if ((num3 > 310f) && (num3 < 350f))
                    {
                        if (Vector3.Distance(this.myHero.transform.position, baseT.Find("APL1").position) < 40f)
                        {
                            this.slap("l1");
                            return;
                        }
                        if (Vector3.Distance(this.myHero.transform.position, baseT.Find("APL2").position) < 40f)
                        {
                            this.slap("l2");
                            return;
                        }
                        if (Vector3.Distance(this.myHero.transform.position, baseT.Find("APR1").position) < 40f)
                        {
                            this.slap("r1");
                            return;
                        }
                        if (Vector3.Distance(this.myHero.transform.position, baseT.Find("APR2").position) < 40f)
                        {
                            this.slap("r2");
                            return;
                        }
                        if ((this.myDistance < 150f) && (Mathf.Abs(f) < 80f))
                        {
                            this.attack_sweep(string.Empty);
                            return;
                        }
                    }
                    if (((num3 < 300f) && (Mathf.Abs(f) < 80f)) && (this.myDistance < 85f))
                    {
                        this.attack_sweep("_vertical");
                    }
                    else
                    {
                        switch (UnityEngine.Random.Range(0, 7))
                        {
                            case 0:
                                this.slap("l1");
                                return;

                            case 1:
                                this.slap("l2");
                                return;

                            case 2:
                                this.slap("r1");
                                return;

                            case 3:
                                this.slap("r2");
                                return;

                            case 4:
                                this.attack_sweep(string.Empty);
                                return;

                            case 5:
                                this.attack_sweep("_vertical");
                                return;

                            case 6:
                                this.steam();
                                break;

                            default:
                                return;
                        }
                    }
                }
            }
        }
    }

    public void updateLabel()
    {
        if ((this.healthLabel != null) && healthLabelT.isVisible)
        {
            this.healthLabel.transform.LookAt(((Vector3) (2f * this.healthLabel.transform.position)) - Camera.main.transform.position);
        }
    }

}

