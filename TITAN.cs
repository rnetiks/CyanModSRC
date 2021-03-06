﻿using ExitGames.Client.Photon;
using Photon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

public class TITAN : Photon.MonoBehaviour
{
    private Vector3 abnorma_jump_bite_horizon_v;
    public AbnormalType abnormalType;
    public int activeRad = 0x7fffffff;
    private float angle;
    public bool asClientLookTarget;
    private string attackAnimation;
    private float attackCheckTime;
    private float attackCheckTimeA;
    private float attackCheckTimeB;
    private int attackCount;
    public float attackDistance = 13f;
    private bool attacked;
    private float attackEndWait;
    public float attackWait = 1f;
    public Animation baseAnimation;
    public AudioSource baseAudioSource;
    public List<Collider> baseColliders;
    public Transform baseGameObjectTransform;
    public Rigidbody baseRigidBody;
    public Transform baseTransform;
    private float between2;
    public float chaseDistance = 80f;
    public ArrayList checkPoints = new ArrayList();
    public bool colliderEnabled;
    public TITAN_CONTROLLER controller;
    public GameObject currentCamera;
    private Transform currentGrabHand;
    public int currentHealth;
    private float desDeg;
    private float dieTime;
    public bool eye;
    [CompilerGenerated]
    private static Dictionary<string, int> f__switchSmap5;
    [CompilerGenerated]
    private static Dictionary<string, int> f__switchSmap6;
    [CompilerGenerated]
    private static Dictionary<string, int> f__switchSmap7;
    [CompilerGenerated]
    public static Dictionary<string, int> fswitchmap5;
    [CompilerGenerated]
    public static Dictionary<string, int> fswitchmap6;
    [CompilerGenerated]
    public static Dictionary<string, int> fswitchmap7;
    private string fxName;
    private Vector3 fxPosition;
    private Quaternion fxRotation;
    private float getdownTime;
    private GameObject grabbedTarget;
    public GameObject grabTF;
    private float gravity = 120f;
    private bool grounded;
    public bool hasDie;
    private bool hasDieSteam;
    public bool hasExplode;
    public bool hasload;
    public bool hasSetLevel;
    public bool hasSpawn;
    private Transform head;
    private Vector3 headscale = Vector3.one;
    public GameObject healthLabel;
    public bool healthLabelEnabled;
    public float healthTime;
    private string hitAnimation;
    private float hitPause;
    public bool isAlarm;
    private bool isAttackMoveByCore;
    private bool isGrabHandLeft;
    public bool isHooked;
    public bool isLook;
    public float lagMax;
    private bool leftHandAttack;
    public GameObject mainMaterial;
    public int maxHealth;
    private float maxStamina = 320f;
    public float maxVelocityChange = 10f;
    public static float minusDistance = 99999f;
    public static GameObject minusDistanceEnemy;
    public FengGameManagerMKII MultiplayerManager;
    public int myDifficulty;
    public float myDistance;
    public GROUP myGroup = GROUP.T;
    public GameObject myHero;
    public float myLevel = 1f;
    public TitanTrigger myTitanTrigger;
    private Transform neck;
    private bool needFreshCorePosition;
    private string nextAttackAnimation;
    public bool nonAI;
    private bool nonAIcombo;
    private Vector3 oldCorePosition;
    private Quaternion oldHeadRotation;
    public PVPcheckPoint PVPfromCheckPt;
    private float random_run_time;
    private float rockInterval;
    public bool rockthrow;
    private string runAnimation;
    private float sbtime;
    public int skin;
    private Vector3 spawnPt;
    public float speed = 7f;
    private float stamina = 320f;
    private TitanState state;
    private int stepSoundPhase = 2;
    private bool stuck;
    private float stuckTime;
    private float stuckTurnAngle;
    private Vector3 targetCheckPt;
    private Quaternion targetHeadRotation;
    private float targetR;
    private float tauntTime;
    private GameObject throwRock;
    private string turnAnimation;
    private float turnDeg;
    private GameObject whoHasTauntMe;

    IN_GAME_MAIN_CAMERA currentCameraT;
    UILabel healthLabelT;
    HERO grabbedTargetT;
    RockThrow throwRockT;
    Transform throwRockTT;
   public Transform grabTFT;
     Dictionary<string, Transform> cacheTrasform;
     Transform Amarture_Core;
    Transform hand_L;
    Transform hand_R;
      Transform chest;
    Transform colider_AABB;
    Transform headT;
 
     Transform tfind(string transform_name)
     {
         if (cacheTrasform.ContainsKey(transform_name) && cacheTrasform[transform_name] != null)
         {
             return cacheTrasform[transform_name];
         }
         Transform trss = baseTransform.Find(transform_name);
         cacheTrasform.Add(transform_name, trss);
         return trss;
     }

    private void attack(string type)
    {
    }

    private void attack2(string type)
    {
        this.state = TitanState.attack;
        this.attacked = false;
        this.isAlarm = true;
        if (this.attackAnimation == type)
        {
            this.attackAnimation = type;
            this.playAnimationAt("attack_" + type, 0f);
        }
        else
        {
            this.attackAnimation = type;
            this.playAnimationAt("attack_" + type, 0f);
        }
        this.nextAttackAnimation = null;
        this.fxName = null;
        this.isAttackMoveByCore = false;
        this.attackCheckTime = 0f;
        this.attackCheckTimeA = 0f;
        this.attackCheckTimeB = 0f;
        this.attackEndWait = 0f;
        this.fxRotation = Quaternion.Euler(270f, 0f, 0f);
        string key = type;
        if (key != null)
        {
            int num;
            if (fswitchmap6 == null)
            {
                Dictionary<string, int> dictionary = new Dictionary<string, int>(0x16) {
                    { 
                        "abnormal_getup",
                        0
                    },
                    { 
                        "abnormal_jump",
                        1
                    },
                    { 
                        "combo_1",
                        2
                    },
                    { 
                        "combo_2",
                        3
                    },
                    { 
                        "combo_3",
                        4
                    },
                    { 
                        "front_ground",
                        5
                    },
                    { 
                        "kick",
                        6
                    },
                    { 
                        "slap_back",
                        7
                    },
                    { 
                        "slap_face",
                        8
                    },
                    { 
                        "stomp",
                        9
                    },
                    { 
                        "bite",
                        10
                    },
                    { 
                        "bite_l",
                        11
                    },
                    { 
                        "bite_r",
                        12
                    },
                    { 
                        "jumper_0",
                        13
                    },
                    { 
                        "crawler_jump_0",
                        14
                    },
                    { 
                        "anti_AE_l",
                        15
                    },
                    { 
                        "anti_AE_r",
                        0x10
                    },
                    { 
                        "anti_AE_low_l",
                        0x11
                    },
                    { 
                        "anti_AE_low_r",
                        0x12
                    },
                    { 
                        "quick_turn_l",
                        0x13
                    },
                    { 
                        "quick_turn_r",
                        20
                    },
                    { 
                        "throw",
                        0x15
                    }
                };
                fswitchmap6 = dictionary;
            }
            if (fswitchmap6.TryGetValue(key, out num))
            {
                switch (num)
                {
                    case 0:
                        this.attackCheckTime = 0f;
                        this.fxName = string.Empty;
                        break;

                    case 1:
                        this.nextAttackAnimation = "abnormal_getup";
                        if (!this.nonAI)
                        {
                            this.attackEndWait = (this.myDifficulty <= 0) ? UnityEngine.Random.Range((float) 1f, (float) 4f) : UnityEngine.Random.Range((float) 0f, (float) 1f);
                        }
                        else
                        {
                            this.attackEndWait = 0f;
                        }
                        this.attackCheckTime = 0.75f;
                        this.fxName = "boom4";
                        this.fxRotation = Quaternion.Euler(270f, this.baseTransform.rotation.eulerAngles.y, 0f);
                        break;

                    case 2:
                        this.nextAttackAnimation = "combo_2";
                        this.attackCheckTimeA = 0.54f;
                        this.attackCheckTimeB = 0.76f;
                        this.nonAIcombo = false;
                        this.isAttackMoveByCore = true;
                        this.leftHandAttack = false;
                        break;

                    case 3:
                        if ((this.abnormalType != AbnormalType.TYPE_PUNK) && !this.nonAI)
                        {
                            this.nextAttackAnimation = "combo_3";
                        }
                        this.attackCheckTimeA = 0.37f;
                        this.attackCheckTimeB = 0.57f;
                        this.nonAIcombo = false;
                        this.isAttackMoveByCore = true;
                        this.leftHandAttack = true;
                        break;

                    case 4:
                        this.nonAIcombo = false;
                        this.isAttackMoveByCore = true;
                        this.attackCheckTime = 0.21f;
                        this.fxName = "boom1";
                        break;

                    case 5:
                        this.fxName = "boom1";
                        this.attackCheckTime = 0.45f;
                        break;

                    case 6:
                        this.fxName = "boom5";
                        this.fxRotation = this.baseTransform.rotation;
                        this.attackCheckTime = 0.43f;
                        break;

                    case 7:
                        this.fxName = "boom3";
                        this.attackCheckTime = 0.66f;
                        break;

                    case 8:
                        this.fxName = "boom3";
                        this.attackCheckTime = 0.655f;
                        break;

                    case 9:
                        this.fxName = "boom2";
                        this.attackCheckTime = 0.42f;
                        break;

                    case 10:
                        this.fxName = "bite";
                        this.attackCheckTime = 0.6f;
                        break;

                    case 11:
                        this.fxName = "bite";
                        this.attackCheckTime = 0.4f;
                        break;

                    case 12:
                        this.fxName = "bite";
                        this.attackCheckTime = 0.4f;
                        break;

                    case 13:
                        this.abnorma_jump_bite_horizon_v = Vector3.zero;
                        break;

                    case 14:
                        this.abnorma_jump_bite_horizon_v = Vector3.zero;
                        break;

                    case 15:
                        this.attackCheckTimeA = 0.31f;
                        this.attackCheckTimeB = 0.4f;
                        this.leftHandAttack = true;
                        break;

                    case 0x10:
                        this.attackCheckTimeA = 0.31f;
                        this.attackCheckTimeB = 0.4f;
                        this.leftHandAttack = false;
                        break;

                    case 0x11:
                        this.attackCheckTimeA = 0.31f;
                        this.attackCheckTimeB = 0.4f;
                        this.leftHandAttack = true;
                        break;

                    case 0x12:
                        this.attackCheckTimeA = 0.31f;
                        this.attackCheckTimeB = 0.4f;
                        this.leftHandAttack = false;
                        break;

                    case 0x13:
                        this.attackCheckTimeA = 2f;
                        this.attackCheckTimeB = 2f;
                        this.isAttackMoveByCore = true;
                        break;

                    case 20:
                        this.attackCheckTimeA = 2f;
                        this.attackCheckTimeB = 2f;
                        this.isAttackMoveByCore = true;
                        break;

                    case 0x15:
                        this.isAlarm = true;
                        this.chaseDistance = 99999f;
                        break;
                }
            }
        }
        this.needFreshCorePosition = true;
    }

    private void Awake()
    {
      
        this.cache();
        this.baseRigidBody.freezeRotation = true;
        this.baseRigidBody.useGravity = false;
    }

    public void beLaughAttacked()
    {
        if (!this.hasDie && (this.abnormalType != AbnormalType.TYPE_CRAWLER))
        {
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
            {
                object[] parameters = new object[] { 0f };
                base.photonView.RPC("laugh", PhotonTargets.All, parameters);
            }
            else if (((this.state == TitanState.idle) || (this.state == TitanState.turn)) || (this.state == TitanState.chase))
            {
                this.laugh(0f);
            }
        }
    }

    public void beTauntedBy(GameObject target, float tauntTime)
    {
        this.whoHasTauntMe = target;
        this.tauntTime = tauntTime;
        this.isAlarm = true;
    }

    public void cache()
    {
        cacheTrasform = new Dictionary<string, Transform>();
        this.baseAnimation = base.animation;
        this.baseTransform = base.transform;
        this.baseRigidBody = base.rigidbody;
        this.baseColliders = new List<Collider>();
        this.baseAudioSource = this.baseTransform.Find("snd_titan_foot").GetComponent<AudioSource>();
        Amarture_Core = this.baseTransform.Find("Amarture/Core");
        hand_L = this.baseTransform.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_L/upper_arm_L/forearm_L/hand_L/hand_L_001");
        hand_R = this.baseTransform.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R/hand_R_001");
     chest = this.baseTransform.Find("Amarture/Core/Controller_Body/hip/spine/chest");
        colider_AABB = this.baseTransform.Find("AABB");
        headT = this.baseTransform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck/head");
        foreach (Collider collider in base.GetComponentsInChildren<Collider>())
        {
            if (collider.name != "AABB")
            {
                this.baseColliders.Add(collider);
            }
        }
        GameObject obj2 = new GameObject {
            name = "PlayerDetectorRC"
        };
        CapsuleCollider collider2 = obj2.AddComponent<CapsuleCollider>();
        CapsuleCollider component = colider_AABB.GetComponent<CapsuleCollider>();
        collider2.center = component.center;
        collider2.radius = Math.Abs((float) (this.headT.position.y - this.baseTransform.position.y));
        collider2.height = component.height * 1.2f;
        collider2.material = component.material;
        collider2.isTrigger = true;
        collider2.name = "PlayerDetectorRC";
        this.myTitanTrigger = obj2.AddComponent<TitanTrigger>();
        this.myTitanTrigger.isCollide = false;
        obj2.layer = 0x10;
        obj2.transform.parent = colider_AABB;
        obj2.transform.localPosition = new Vector3(0f, 0f, 0f);
        this.MultiplayerManager = FengGameManagerMKII.instance;
        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE) || base.photonView.isMine)
        {
            this.baseGameObjectTransform = base.gameObject.transform;
        }
    }

    private void chase()
    {
        this.state = TitanState.chase;
        this.isAlarm = true;
        this.crossFade(this.runAnimation, 0.5f);
    }

    private HERO checkIfHitCrawlerMouthhero(Transform head, float rad)
    {
        float num = rad * this.myLevel;
        foreach (HERO hero in FengGameManagerMKII.instance.heroes)
        {
            if (!hero.isInvincible())
            {
                float num2 = hero.GetComponent<CapsuleCollider>().height * 0.5f;
                if (Vector3.Distance(hero.transform.position + ((Vector3)(Vector3.up * num2)), head.position - ((Vector3)((Vector3.up * 1.5f) * this.myLevel))) < (num + num2))
                {
                    return hero;
                }
            }
        }
        return null;
    }

    private GameObject checkIfHitCrawlerMouth(Transform head, float rad)
    {
        float num = rad * this.myLevel;
        foreach (GameObject obj2 in FengGameManagerMKII.instance.allheroes)
        {
            if ((obj2.GetComponent<TITAN_EREN>() == null) && ((obj2.GetComponent<HERO>() == null) || !obj2.GetComponent<HERO>().isInvincible()))
            {
                float num2 = obj2.GetComponent<CapsuleCollider>().height * 0.5f;
                if (Vector3.Distance(obj2.transform.position + ((Vector3) (Vector3.up * num2)), head.position - ((Vector3) ((Vector3.up * 1.5f) * this.myLevel))) < (num + num2))
                {
                    return obj2;
                }
            }
        }
        return null;
    }

    private HERO checkIfHitHand(Transform hand)
    {
        float num = 2.4f * this.myLevel;
        foreach (Collider collider in Physics.OverlapSphere(hand.GetComponent<SphereCollider>().transform.position, num + 1f))
        {
            if (collider.transform.root.tag == "Player")
            {
                GameObject gameObject = collider.transform.root.gameObject;
                TITAN_EREN te = gameObject.GetComponent<TITAN_EREN>();
                HERO hr = null;
                if (te != null)
                {
                    if (!te.isHit)
                    {
                        te.hitByTitan();
                    }
                }
                else if (((hr = gameObject.GetComponent<HERO>()) != null) && !hr.isInvincible())
                {
                    return hr;
                }
            }
        }
        return null;
    }

    private HERO checkIfHitHead(Transform head, float rad)
    {
        float num = rad * this.myLevel;
        foreach (HERO hero in FengGameManagerMKII.instance.heroes)
        {
            if ( !hero.isInvincible())
            {
                float num2 = hero.gameObject.GetComponent<CapsuleCollider>().height * 0.5f;
                if (Vector3.Distance(hero.transform.position + ((Vector3)(Vector3.up * num2)), head.position + ((Vector3)((Vector3.up * 1.5f) * this.myLevel))) < (num + num2))
                {
                    return hero;
                }
            }
        }
        return null;
    }

    private void crossFade(string aniName, float time)
    {
        this.baseAnimation.CrossFade(aniName, time);
        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && base.photonView.isMine)
        {
            object[] parameters = new object[] { aniName, time };
            base.photonView.RPC("netCrossFade", PhotonTargets.Others, parameters);
        }
    }

    public bool die()
    {
        if (this.hasDie)
        {
            return false;
        }
        this.hasDie = true;
        FengGameManagerMKII.instance.oneTitanDown(string.Empty, false);
        this.dieAnimation();
        return true;
    }

    private void dieAnimation()
    {
        if (!this.baseAnimation.IsPlaying("sit_idle") && !this.baseAnimation.IsPlaying("sit_hit_eye"))
        {
            if (this.abnormalType == AbnormalType.TYPE_CRAWLER)
            {
                this.crossFade("crawler_die", 0.2f);
            }
            else if (this.abnormalType == AbnormalType.NORMAL)
            {
                this.crossFade("die_front", 0.05f);
            }
            else if (((this.baseAnimation.IsPlaying("attack_abnormal_jump") && (this.baseAnimation["attack_abnormal_jump"].normalizedTime > 0.7f)) || (this.baseAnimation.IsPlaying("attack_abnormal_getup") && (this.baseAnimation["attack_abnormal_getup"].normalizedTime < 0.7f))) || this.baseAnimation.IsPlaying("tired"))
            {
                this.crossFade("die_ground", 0.2f);
            }
            else
            {
                this.crossFade("die_back", 0.05f);
            }
        }
        else
        {
            this.crossFade("sit_die", 0.1f);
        }
    }

    public void dieBlow(Vector3 attacker, float hitPauseTime)
    {
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
        {
            this.dieBlowFunc(attacker, hitPauseTime);
            if (FengGameManagerMKII.instance.alltitans.Count <= 1)
            {
                 IN_GAME_MAIN_CAMERA.instance.gameOver = true;
            }
        }
        else
        {
            object[] parameters = new object[] { attacker, hitPauseTime };
            base.photonView.RPC("dieBlowRPC", PhotonTargets.All, parameters);
        }
    }

    public void dieBlowFunc(Vector3 attacker, float hitPauseTime)
    {
        if (!this.hasDie)
        {
            this.baseTransform.rotation = Quaternion.Euler(0f, Quaternion.LookRotation(attacker - this.baseTransform.position).eulerAngles.y, 0f);
            this.hasDie = true;
            this.hitAnimation = "die_blow";
            this.hitPause = hitPauseTime;
            this.playAnimation(this.hitAnimation);
            this.baseAnimation[this.hitAnimation].time = 0f;
            this.baseAnimation[this.hitAnimation].speed = 0f;
            this.needFreshCorePosition = true;
            FengGameManagerMKII.instance.oneTitanDown(string.Empty, false);
            if (base.photonView.isMine)
            {
                if (this.grabbedTargetT != null)
                {
                    this.grabbedTargetT.photonView.RPC("netUngrabbed", PhotonTargets.All, new object[0]);
                }
                if (this.nonAI)
                {
                    this.currentCameraT.setMainObject(null, true, false);
                    this.currentCameraT.setSpectorMode(true);
                    this.currentCameraT.gameOver = true;
                    PhotonNetwork.player.dead = true;
                    PhotonNetwork.player.deaths = (PhotonNetwork.player.deaths) + 1;
                }
            }
        }
    }

    [RPC]
    private void dieBlowRPC(Vector3 attacker, float hitPauseTime)
    {
        if (base.photonView.isMine)
        {
            Vector3 vector = attacker - this.baseTransform.position;
            if (vector.magnitude < 80f)
            {
                this.dieBlowFunc(attacker, hitPauseTime);
            }
        }
    }

    [RPC]
    public void DieByCannon(int viewID)
    {
        PhotonView view = PhotonView.Find(viewID);
        if (view != null)
        {
            int damage = 0;
            if (PhotonNetwork.isMasterClient)
            {
                this.OnTitanDie(view);
            }
            if (this.nonAI)
            {
                FengGameManagerMKII.instance.titanGetKill(view.owner, damage, (string) PhotonNetwork.player.name2);
            }
            else
            {
                FengGameManagerMKII.instance.titanGetKill(view.owner, damage, base.name);
            }
        }
        else
        {
            FengGameManagerMKII.instance.photonView.RPC("netShowDamage", view.owner, new object[] { this.speed });
        }
    }

    public void dieHeadBlow(Vector3 attacker, float hitPauseTime)
    {
        if (this.abnormalType != AbnormalType.TYPE_CRAWLER)
        {
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                this.dieHeadBlowFunc(attacker, hitPauseTime);
                if (FengGameManagerMKII.instance.alltitans.Count <= 1)
                {
                     IN_GAME_MAIN_CAMERA.instance.gameOver = true;
                }
            }
            else
            {
                object[] parameters = new object[] { attacker, hitPauseTime };
                base.photonView.RPC("dieHeadBlowRPC", PhotonTargets.All, parameters);
            }
        }
    }

    public void dieHeadBlowFunc(Vector3 attacker, float hitPauseTime)
    {
        if (!this.hasDie)
        {
            GameObject obj2;
            this.playSound("snd_titan_head_blow");
            this.baseTransform.rotation = Quaternion.Euler(0f, Quaternion.LookRotation(attacker - this.baseTransform.position).eulerAngles.y, 0f);
            this.hasDie = true;
            this.hitAnimation = "die_headOff";
            this.hitPause = hitPauseTime;
            this.playAnimation(this.hitAnimation);
            this.baseAnimation[this.hitAnimation].time = 0f;
            this.baseAnimation[this.hitAnimation].speed = 0f;
            FengGameManagerMKII.instance.oneTitanDown(string.Empty, false);
            this.needFreshCorePosition = true;
            if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && base.photonView.isMine)
            {
                obj2 = PhotonNetwork.Instantiate("bloodExplore", this.head.position + ((Vector3) ((Vector3.up * 1f) * this.myLevel)), Quaternion.Euler(270f, 0f, 0f), 0);
            }
            else
            {
                obj2 = (GameObject)UnityEngine.Object.Instantiate(Cach.bloodExplore != null ? Cach.bloodExplore : Cach.bloodExplore = (GameObject)Resources.Load("bloodExplore"), this.head.position + ((Vector3)((Vector3.up * 1f) * this.myLevel)), Quaternion.Euler(270f, 0f, 0f));
            }
            obj2.transform.localScale = this.baseTransform.localScale;
            if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && base.photonView.isMine)
            {
                obj2 = PhotonNetwork.Instantiate("bloodsplatter", this.head.position, Quaternion.Euler(270f + this.neck.rotation.eulerAngles.x, this.neck.rotation.eulerAngles.y, this.neck.rotation.eulerAngles.z), 0);
            }
            else
            {
                obj2 = (GameObject)UnityEngine.Object.Instantiate(Cach.bloodsplatter != null ? Cach.bloodsplatter : Cach.bloodsplatter = (GameObject)Resources.Load("bloodsplatter"), this.head.position, Quaternion.Euler(270f + this.neck.rotation.eulerAngles.x, this.neck.rotation.eulerAngles.y, this.neck.rotation.eulerAngles.z));
            }
            obj2.transform.localScale = this.baseTransform.localScale;
            obj2.transform.parent = this.neck;
           
                if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && base.photonView.isMine)
                {
                    obj2 = PhotonNetwork.Instantiate("FX/justSmoke", this.neck.position, Quaternion.Euler(270f, 0f, 0f), 0);
                }
                else
                {
                    obj2 = (GameObject)UnityEngine.Object.Instantiate(Cach.justSmoke != null ? Cach.justSmoke : Cach.justSmoke = (GameObject)Resources.Load("FX/justSmoke"), this.neck.position, Quaternion.Euler(270f, 0f, 0f));
                }
                obj2.transform.parent = this.neck;
            
            if (base.photonView.isMine)
            {
                if (this.grabbedTargetT != null)
                {
                    grabbedTargetT.photonView.RPC("netUngrabbed", PhotonTargets.All, new object[0]);
                }
                if (this.nonAI)
                {
                    this.currentCameraT.setMainObject(null, true, false);
                    this.currentCameraT.setSpectorMode(true);
                    this.currentCameraT.gameOver = true;
                    PhotonNetwork.player.dead = true;
                    PhotonNetwork.player.deaths = (PhotonNetwork.player.deaths) + 1;
                }
            }
        }
    }

    [RPC]
    private void dieHeadBlowRPC(Vector3 attacker, float hitPauseTime)
    {
        if (base.photonView.isMine)
        {
            Vector3 vector = attacker - this.neck.position;
            if (vector.magnitude < this.lagMax)
            {
                this.dieHeadBlowFunc(attacker, hitPauseTime);
            }
        }
    }

    private void eat()
    {
        this.state = TitanState.eat;
        this.attacked = false;
        if (this.isGrabHandLeft)
        {
            this.attackAnimation = "eat_l";
            this.crossFade("eat_l", 0.1f);
        }
        else
        {
            this.attackAnimation = "eat_r";
            this.crossFade("eat_r", 0.1f);
        }
    }

    private void eatSet(HERO hero)
    {
        if (((IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE) && ((IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.MULTIPLAYER) || !base.photonView.isMine)) || !hero.isGrabbed)
        {
            this.grabToRight();
            if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && base.photonView.isMine)
            {
                base.photonView.RPC("grabToRight", PhotonTargets.Others, new object[0]);
                object[] parameters = new object[] { "grabbed" };
                hero.photonView.RPC("netPlayAnimation", PhotonTargets.All, parameters);
                object[] objArray2 = new object[] { base.photonView.viewID, false };
                hero.photonView.RPC("netGrabbed", PhotonTargets.All, objArray2);
            }
            else
            {
                hero.grabbed(base.gameObject, false);
                hero.animation.Play("grabbed");
            }
        }
    }

    private void eatSetL(HERO hero)
    {
        if (((IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE) && ((IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.MULTIPLAYER) || !base.photonView.isMine)) || !hero.isGrabbed)
        {
            this.grabToLeft();
            if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && base.photonView.isMine)
            {
                base.photonView.RPC("grabToLeft", PhotonTargets.Others, new object[0]);
                object[] parameters = new object[] { "grabbed" };
                hero.photonView.RPC("netPlayAnimation", PhotonTargets.All, parameters);
                object[] objArray2 = new object[] { base.photonView.viewID, true };
                hero.photonView.RPC("netGrabbed", PhotonTargets.All, objArray2);
            }
            else
            {
                hero.grabbed(base.gameObject, true);
                hero.animation.Play("grabbed");
            }
        }
    }

    private bool executeAttack(string decidedAction)
    {
        return false;
    }

    private bool executeAttack2(string decidedAction)
    {
        string key = decidedAction;
        if (key != null)
        {
            int num;
            if (fswitchmap5 == null)
            {
                Dictionary<string, int> dictionary = new Dictionary<string, int>(0x12) {
                    { 
                        "grab_ground_front_l",
                        0
                    },
                    { 
                        "grab_ground_front_r",
                        1
                    },
                    { 
                        "grab_ground_back_l",
                        2
                    },
                    { 
                        "grab_ground_back_r",
                        3
                    },
                    { 
                        "grab_head_front_l",
                        4
                    },
                    { 
                        "grab_head_front_r",
                        5
                    },
                    { 
                        "grab_head_back_l",
                        6
                    },
                    { 
                        "grab_head_back_r",
                        7
                    },
                    { 
                        "attack_abnormal_jump",
                        8
                    },
                    { 
                        "attack_combo",
                        9
                    },
                    { 
                        "attack_front_ground",
                        10
                    },
                    { 
                        "attack_kick",
                        11
                    },
                    { 
                        "attack_slap_back",
                        12
                    },
                    { 
                        "attack_slap_face",
                        13
                    },
                    { 
                        "attack_stomp",
                        14
                    },
                    { 
                        "attack_bite",
                        15
                    },
                    { 
                        "attack_bite_l",
                        0x10
                    },
                    { 
                        "attack_bite_r",
                        0x11
                    }
                };
                fswitchmap5 = dictionary;
            }
            if (fswitchmap5.TryGetValue(key, out num))
            {
                switch (num)
                {
                    case 0:
                        this.grab("ground_front_l");
                        return true;

                    case 1:
                        this.grab("ground_front_r");
                        return true;

                    case 2:
                        this.grab("ground_back_l");
                        return true;

                    case 3:
                        this.grab("ground_back_r");
                        return true;

                    case 4:
                        this.grab("head_front_l");
                        return true;

                    case 5:
                        this.grab("head_front_r");
                        return true;

                    case 6:
                        this.grab("head_back_l");
                        return true;

                    case 7:
                        this.grab("head_back_r");
                        return true;

                    case 8:
                        this.attack2("abnormal_jump");
                        return true;

                    case 9:
                        this.attack2("combo_1");
                        return true;

                    case 10:
                        this.attack2("front_ground");
                        return true;

                    case 11:
                        this.attack2("kick");
                        return true;

                    case 12:
                        this.attack2("slap_back");
                        return true;

                    case 13:
                        this.attack2("slap_face");
                        return true;

                    case 14:
                        this.attack2("stomp");
                        return true;

                    case 15:
                        this.attack2("bite");
                        return true;

                    case 0x10:
                        this.attack2("bite_l");
                        return true;

                    case 0x11:
                        this.attack2("bite_r");
                        return true;
                }
            }
        }
        return false;
    }

    public void explode()
    {
        if (((RCSettings.explodeMode > 0) && this.hasDie) && ((this.dieTime >= 1f) && !this.hasExplode))
        {
            int num = 0;
            float num2 = this.myLevel * 10f;
            if (this.abnormalType == AbnormalType.TYPE_CRAWLER)
            {
                if (this.dieTime >= 2f)
                {
                    this.hasExplode = true;
                    num2 = 0f;
                    num = 1;
                }
            }
            else
            {
                num = 1;
                this.hasExplode = true;
            }
            if (num == 1)
            {
                Vector3 position = this.baseTransform.position + ((Vector3) (Vector3.up * num2));
                PhotonNetwork.Instantiate("FX/Thunder", position, Quaternion.Euler(270f, 0f, 0f), 0);
                PhotonNetwork.Instantiate("FX/boom1", position, Quaternion.Euler(270f, 0f, 0f), 0);
                foreach (HERO hero in FengGameManagerMKII.instance.heroes)
                {
                    if (Vector3.Distance(hero.transform.position, position) < RCSettings.explodeMode)
                    {
                        hero.markDie();
                        hero.photonView.RPC("netDie2", PhotonTargets.All, new object[] { -1, "Server " });
                    }
                }
            }
        }
    }

    private void findNearestFacingHero()
    {
      
    }

    private void findNearestFacingHero2()
    {
        if (net_searh)
        {
            GameObject obj2 = null;
            float positiveInfinity = float.PositiveInfinity;
            Vector3 position = this.baseTransform.position;
            float current = 0f;
            float num3 = (this.abnormalType != AbnormalType.NORMAL) ? 180f : 100f;
            float f = 0f;
            foreach (GameObject gameObject in this.MultiplayerManager.allheroes)
            {
                float num5 = Vector3.Distance(gameObject.transform.position, position);
                if (num5 < positiveInfinity)
                {
                    Vector3 vector2 = gameObject.transform.position - this.baseTransform.position;
                    current = -Mathf.Atan2(vector2.z, vector2.x) * 57.29578f;
                    f = -Mathf.DeltaAngle(current, this.baseGameObjectTransform.rotation.eulerAngles.y - 90f);
                    if (Mathf.Abs(f) < num3)
                    {
                        obj2 = gameObject;
                        positiveInfinity = num5;
                    }
                }
            }
            if (obj2 != null)
            {
                GameObject myHero = this.myHero;
                this.myHero = obj2;
                if (((myHero != this.myHero) && (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)) && PhotonNetwork.isMasterClient)
                {
                    if (this.myHero == null)
                    {
                        object[] parameters = new object[] { -1 };
                        base.photonView.RPC("setMyTarget", PhotonTargets.Others, parameters);
                    }
                    else
                    {
                        object[] objArray2 = new object[] { this.myHero.GetPhotonView().viewID };
                        base.photonView.RPC("setMyTarget", PhotonTargets.Others, objArray2);
                    }
                }
                this.tauntTime = 5f;
            }
        }
    }

    private void findNearestHero()
    {
      
    }
    public bool net_searh = true;
    private void findNearestHero2()
    {
        if (net_searh)
        {
            GameObject myHero = this.myHero;
            this.myHero = this.getNearestHero2();
            if (((this.myHero != myHero) && (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)) && PhotonNetwork.isMasterClient)
            {
                if (this.myHero == null)
                {
                    object[] parameters = new object[] { -1 };
                    base.photonView.RPC("setMyTarget", PhotonTargets.Others, parameters);
                }
                else
                {
                    object[] objArray2 = new object[] { this.myHero.GetPhotonView().viewID };
                    base.photonView.RPC("setMyTarget", PhotonTargets.Others, objArray2);
                }
            }
            this.oldHeadRotation = this.head.rotation;
        }
    }

    private void FixedUpdate()
    {
        if (!((!IN_GAME_MAIN_CAMERA.isPausing || (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)) ? ((IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE) && !base.photonView.isMine) : true))
        {
            this.baseRigidBody.AddForce(new Vector3(0f, -this.gravity * this.baseRigidBody.mass, 0f));
            if (this.needFreshCorePosition)
            {
                this.oldCorePosition = this.baseTransform.position - this.Amarture_Core.position;
                this.needFreshCorePosition = false;
            }
            if (this.hasDie)
            {
                if ((this.hitPause <= 0f) && this.baseAnimation.IsPlaying("die_headOff"))
                {
                    Vector3 vector = (this.baseTransform.position - this.Amarture_Core.position) - this.oldCorePosition;
                    this.baseRigidBody.velocity = (Vector3) ((vector / Time.deltaTime) + (Vector3.up * this.baseRigidBody.velocity.y));
                }
                this.oldCorePosition = this.baseTransform.position - this.Amarture_Core.position;
            }
            else if (!(((this.state != TitanState.attack) || !this.isAttackMoveByCore) ? (this.state != TitanState.hit) : false))
            {
                Vector3 vector2 = (this.baseTransform.position - this.Amarture_Core.position) - this.oldCorePosition;
                this.baseRigidBody.velocity = (Vector3) ((vector2 / Time.deltaTime) + (Vector3.up * this.baseRigidBody.velocity.y));
                this.oldCorePosition = this.baseTransform.position - this.Amarture_Core.position;
            }
            if (this.hasDie)
            {
                if (this.hitPause <= 0f)
                {
                    if (this.baseAnimation.IsPlaying("die_blow"))
                    {
                        if (this.baseAnimation["die_blow"].normalizedTime < 0.55f)
                        {
                            this.baseRigidBody.velocity = (Vector3) ((-this.baseTransform.forward * 300f) + (Vector3.up * this.baseRigidBody.velocity.y));
                        }
                        else if (this.baseAnimation["die_blow"].normalizedTime < 0.83f)
                        {
                            this.baseRigidBody.velocity = (Vector3) ((-this.baseTransform.forward * 100f) + (Vector3.up * this.baseRigidBody.velocity.y));
                        }
                        else
                        {
                            this.baseRigidBody.velocity = (Vector3) (Vector3.up * this.baseRigidBody.velocity.y);
                        }
                    }
                }
                else
                {
                    this.hitPause -= Time.deltaTime;
                    if (this.hitPause <= 0f)
                    {
                        this.baseAnimation[this.hitAnimation].speed = 1f;
                        this.hitPause = 0f;
                    }
                }
            }
            else
            {
                if ((this.nonAI && !IN_GAME_MAIN_CAMERA.isPausing) && ((this.state == TitanState.idle) || ((this.state == TitanState.attack) && (this.attackAnimation == "jumper_1"))))
                {
                    Vector3 zero = Vector3.zero;
                    if (this.controller.targetDirection != -874f)
                    {
                        bool flag = false;
                        if (this.stamina < 5f)
                        {
                            flag = true;
                        }
                        else if (((this.stamina < 40f) && !this.baseAnimation.IsPlaying("run_abnormal")) && !this.baseAnimation.IsPlaying("crawler_run"))
                        {
                            flag = true;
                        }
                        if (this.controller.isWALKDown || flag)
                        {
                            zero = (Vector3) (((this.baseTransform.forward * this.speed) * Mathf.Sqrt(this.myLevel)) * 0.2f);
                        }
                        else
                        {
                            zero = (Vector3) ((this.baseTransform.forward * this.speed) * Mathf.Sqrt(this.myLevel));
                        }
                        this.baseGameObjectTransform.rotation = Quaternion.Lerp(this.baseGameObjectTransform.rotation, Quaternion.Euler(0f, this.controller.targetDirection, 0f), (this.speed * 0.15f) * Time.deltaTime);
                        if (this.state == TitanState.idle)
                        {
                            if (this.controller.isWALKDown || flag)
                            {
                                if (this.abnormalType == AbnormalType.TYPE_CRAWLER)
                                {
                                    if (!this.baseAnimation.IsPlaying("crawler_run"))
                                    {
                                        this.crossFade("crawler_run", 0.1f);
                                    }
                                }
                                else if (!this.baseAnimation.IsPlaying("run_walk"))
                                {
                                    this.crossFade("run_walk", 0.1f);
                                }
                            }
                            else if (this.abnormalType == AbnormalType.TYPE_CRAWLER)
                            {
                                if (!this.baseAnimation.IsPlaying("crawler_run"))
                                {
                                    this.crossFade("crawler_run", 0.1f);
                                }
                                HERO hero = this.checkIfHitCrawlerMouthhero(this.head, 2.2f);
                                if (hero != null)
                                {
                                    Vector3 position = this.chest.position;
                                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                                    {
                                        hero.die((Vector3)(((hero.transform.position - position) * 15f) * this.myLevel), false);
                                    }
                                    else if (((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && base.photonView.isMine) && !hero.HasDied())
                                    {
                                        hero.markDie();
                                        object[] parameters = new object[] { (Vector3)(((hero.transform.position - position) * 15f) * this.myLevel), true, !this.nonAI ? -1 : base.photonView.viewID, base.name, true };
                                        hero.photonView.RPC("netDie", PhotonTargets.All, parameters);
                                    }
                                }
                            }
                            else if (!this.baseAnimation.IsPlaying("run_abnormal"))
                            {
                                this.crossFade("run_abnormal", 0.1f);
                            }
                        }
                    }
                    else if (this.state == TitanState.idle)
                    {
                        if (this.abnormalType == AbnormalType.TYPE_CRAWLER)
                        {
                            if (!this.baseAnimation.IsPlaying("crawler_idle"))
                            {
                                this.crossFade("crawler_idle", 0.1f);
                            }
                        }
                        else if (!this.baseAnimation.IsPlaying("idle"))
                        {
                            this.crossFade("idle", 0.1f);
                        }
                        zero = Vector3.zero;
                    }
                    if (this.state == TitanState.idle)
                    {
                        Vector3 velocity = this.baseRigidBody.velocity;
                        Vector3 force = zero - velocity;
                        force.x = Mathf.Clamp(force.x, -this.maxVelocityChange, this.maxVelocityChange);
                        force.z = Mathf.Clamp(force.z, -this.maxVelocityChange, this.maxVelocityChange);
                        force.y = 0f;
                        this.baseRigidBody.AddForce(force, ForceMode.VelocityChange);
                    }
                    else if ((this.state == TitanState.attack) && (this.attackAnimation == "jumper_0"))
                    {
                        Vector3 vector7 = this.baseRigidBody.velocity;
                        Vector3 vector8 = ((Vector3) (zero * 0.8f)) - vector7;
                        vector8.x = Mathf.Clamp(vector8.x, -this.maxVelocityChange, this.maxVelocityChange);
                        vector8.z = Mathf.Clamp(vector8.z, -this.maxVelocityChange, this.maxVelocityChange);
                        vector8.y = 0f;
                        this.baseRigidBody.AddForce(vector8, ForceMode.VelocityChange);
                    }
                }
                if (!(((this.abnormalType == AbnormalType.TYPE_I) || (this.abnormalType == AbnormalType.TYPE_JUMPER)) ? ((this.nonAI || (this.state != TitanState.attack)) || (this.attackAnimation != "jumper_0")) : true))
                {
                    Vector3 vector9 = (Vector3) (((this.baseTransform.forward * this.speed) * this.myLevel) * 0.5f);
                    Vector3 vector10 = this.baseRigidBody.velocity;
                    if ((this.baseAnimation["attack_jumper_0"].normalizedTime <= 0.28f) || (this.baseAnimation["attack_jumper_0"].normalizedTime >= 0.8f))
                    {
                        vector9 = Vector3.zero;
                    }
                    Vector3 vector11 = vector9 - vector10;
                    vector11.x = Mathf.Clamp(vector11.x, -this.maxVelocityChange, this.maxVelocityChange);
                    vector11.z = Mathf.Clamp(vector11.z, -this.maxVelocityChange, this.maxVelocityChange);
                    vector11.y = 0f;
                    this.baseRigidBody.AddForce(vector11, ForceMode.VelocityChange);
                }
                if (((this.state == TitanState.chase) || (this.state == TitanState.wander)) || (((this.state == TitanState.to_check_point) || (this.state == TitanState.to_pvp_pt)) || (this.state == TitanState.random_run)))
                {
                    Vector3 vector12 = (Vector3) (this.baseTransform.forward * this.speed);
                    Vector3 vector13 = this.baseRigidBody.velocity;
                    Vector3 vector14 = vector12 - vector13;
                    vector14.x = Mathf.Clamp(vector14.x, -this.maxVelocityChange, this.maxVelocityChange);
                    vector14.z = Mathf.Clamp(vector14.z, -this.maxVelocityChange, this.maxVelocityChange);
                    vector14.y = 0f;
                    this.baseRigidBody.AddForce(vector14, ForceMode.VelocityChange);
                    if ((!this.stuck && (this.abnormalType != AbnormalType.TYPE_CRAWLER)) && !this.nonAI)
                    {
                        if (this.baseAnimation.IsPlaying(this.runAnimation) && (this.baseRigidBody.velocity.magnitude < (this.speed * 0.5f)))
                        {
                            this.stuck = true;
                            this.stuckTime = 2f;
                            this.stuckTurnAngle = (UnityEngine.Random.Range(0, 2) * 140f) - 70f;
                        }
                        if (((this.state == TitanState.chase) && (this.myHero != null)) && ((this.myDistance > this.attackDistance) && (this.myDistance < 150f)))
                        {
                            float num = 0.05f;
                            if (this.myDifficulty > 1)
                            {
                                num += 0.05f;
                            }
                            if (this.abnormalType != AbnormalType.NORMAL)
                            {
                                num += 0.1f;
                            }
                            if (UnityEngine.Random.Range((float) 0f, (float) 1f) < num)
                            {
                                this.stuck = true;
                                this.stuckTime = 1f;
                                float num2 = UnityEngine.Random.Range((float) 20f, (float) 50f);
                                this.stuckTurnAngle = ((UnityEngine.Random.Range(0, 2) * num2) * 2f) - num2;
                            }
                        }
                    }
                    float current = 0f;
                    if (this.state == TitanState.wander)
                    {
                        current = this.baseTransform.rotation.eulerAngles.y - 90f;
                    }
                    else if (((this.state == TitanState.to_check_point) || (this.state == TitanState.to_pvp_pt)) || (this.state == TitanState.random_run))
                    {
                        Vector3 vector15 = this.targetCheckPt - this.baseTransform.position;
                        current = -Mathf.Atan2(vector15.z, vector15.x) * 57.29578f;
                    }
                    else
                    {
                        if (this.myHero == null)
                        {
                            return;
                        }
                        Vector3 vector16 = this.myHero.transform.position - this.baseTransform.position;
                        current = -Mathf.Atan2(vector16.z, vector16.x) * 57.29578f;
                    }
                    if (this.stuck)
                    {
                        this.stuckTime -= Time.deltaTime;
                        if (this.stuckTime < 0f)
                        {
                            this.stuck = false;
                        }
                        if (this.stuckTurnAngle > 0f)
                        {
                            this.stuckTurnAngle -= Time.deltaTime * 10f;
                        }
                        else
                        {
                            this.stuckTurnAngle += Time.deltaTime * 10f;
                        }
                        current += this.stuckTurnAngle;
                    }
                    float num4 = -Mathf.DeltaAngle(current, this.baseGameObjectTransform.rotation.eulerAngles.y - 90f);
                    if (this.abnormalType == AbnormalType.TYPE_CRAWLER)
                    {
                        this.baseGameObjectTransform.rotation = Quaternion.Lerp(this.baseGameObjectTransform.rotation, Quaternion.Euler(0f, this.baseGameObjectTransform.rotation.eulerAngles.y + num4, 0f), ((this.speed * 0.3f) * Time.deltaTime) / this.myLevel);
                    }
                    else
                    {
                        this.baseGameObjectTransform.rotation = Quaternion.Lerp(this.baseGameObjectTransform.rotation, Quaternion.Euler(0f, this.baseGameObjectTransform.rotation.eulerAngles.y + num4, 0f), ((this.speed * 0.5f) * Time.deltaTime) / this.myLevel);
                    }
                }
            }
        }
    }

    Transform chkOverHead;
    Transform chkFront;
    Transform chkFrontRight;
    Transform chkFrontLeft;
    Transform chkBackRight;
    Transform chkBackLeft;
    private string[] GetAttackStrategy()
    {
        string[] strArray = null;
        if (!this.isAlarm && ((this.myHero.transform.position.y + 3f) > (this.neck.position.y + (10f * this.myLevel))))
        {
            return strArray;
        }
        if (this.myHero.transform.position.y > (this.neck.position.y - (3f * this.myLevel)))
        {
            if (this.myDistance < (this.attackDistance * 0.5f))
            {
                if (Vector3.Distance(this.myHero.transform.position, (chkOverHead != null ? chkOverHead : chkOverHead = this.tfind("chkOverHead")).position) < (3.6f * this.myLevel))
                {
                    if (this.between2 > 0f)
                    {
                        strArray = new string[] { "grab_head_front_r" };
                    }
                    else
                    {
                        strArray = new string[] { "grab_head_front_l" };
                    }
                }
                else if (Mathf.Abs(this.between2) < 90f)
                {
                    if (Mathf.Abs(this.between2) < 30f)
                    {
                        if (Vector3.Distance(this.myHero.transform.position, (chkFront != null ? chkFront : chkFront = this.tfind("chkFront")).position) < (2.5f * this.myLevel))
                        {
                            strArray = new string[] { "attack_bite", "attack_bite", "attack_slap_face" };
                        }
                    }
                    else if (this.between2 > 0f)
                    {
                        if (Vector3.Distance(this.myHero.transform.position, (chkFrontRight != null ? chkFrontRight : chkFrontRight = this.tfind("chkFrontRight")).position) < (2.5f * this.myLevel))
                        {
                            strArray = new string[] { "attack_bite_r" };
                        }
                    }
                    else if (Vector3.Distance(this.myHero.transform.position, (chkFrontLeft != null ? chkFrontLeft : chkFrontLeft = this.tfind("chkFrontLeft")).position) < (2.5f * this.myLevel))
                    {
                        strArray = new string[] { "attack_bite_l" };
                    }
                }
                else if (this.between2 > 0f)
                {
                    if (Vector3.Distance(this.myHero.transform.position, (chkBackRight != null ? chkBackRight : chkBackRight = this.tfind("chkBackRight")).position) < (2.8f * this.myLevel))
                    {
                        strArray = new string[] { "grab_head_back_r", "grab_head_back_r", "attack_slap_back" };
                    }
                }
                else if (Vector3.Distance(this.myHero.transform.position, (chkBackLeft != null ? chkBackLeft : chkBackLeft = this.tfind("chkBackLeft")).position) < (2.8f * this.myLevel))
                {
                    strArray = new string[] { "grab_head_back_l", "grab_head_back_l", "attack_slap_back" };
                }
            }
            if (strArray == null)
            {
                if ((this.abnormalType != AbnormalType.NORMAL) && (this.abnormalType != AbnormalType.TYPE_PUNK))
                {
                    if ((this.abnormalType != AbnormalType.TYPE_I) && (this.abnormalType != AbnormalType.TYPE_JUMPER))
                    {
                        return strArray;
                    }
                    if ((this.myDifficulty <= 0) && (UnityEngine.Random.Range(0, 100) >= 50))
                    {
                        return strArray;
                    }
                    return new string[] { "attack_abnormal_jump" };
                }
                if ((this.myDifficulty <= 0) && (UnityEngine.Random.Range(0, 0x3e8) >= 3))
                {
                    return strArray;
                }
                if (Mathf.Abs(this.between2) < 60f)
                {
                    strArray = new string[] { "attack_combo" };
                }
            }
            return strArray;
        }
        if (Mathf.Abs(this.between2) < 90f)
        {
            if (this.between2 > 0f)
            {
                if (this.myDistance < (this.attackDistance * 0.25f))
                {
                    if (this.abnormalType == AbnormalType.TYPE_PUNK)
                    {
                        return new string[] { "attack_kick", "attack_stomp" };
                    }
                    if (this.abnormalType == AbnormalType.NORMAL)
                    {
                        return new string[] { "attack_front_ground", "attack_stomp" };
                    }
                    return new string[] { "attack_kick" };
                }
                if (this.myDistance < (this.attackDistance * 0.5f))
                {
                    if ((this.abnormalType != AbnormalType.TYPE_PUNK) && (this.abnormalType == AbnormalType.NORMAL))
                    {
                        return new string[] { "grab_ground_front_r", "grab_ground_front_r", "attack_stomp" };
                    }
                    return new string[] { "grab_ground_front_r", "grab_ground_front_r", "attack_abnormal_jump" };
                }
                if (this.abnormalType == AbnormalType.TYPE_PUNK)
                {
                    return new string[] { "attack_combo", "attack_combo", "attack_abnormal_jump" };
                }
                if (this.abnormalType == AbnormalType.NORMAL)
                {
                    if (this.myDifficulty > 0)
                    {
                        return new string[] { "attack_front_ground", "attack_combo", "attack_combo" };
                    }
                    return new string[] { "attack_front_ground", "attack_front_ground", "attack_front_ground", "attack_front_ground", "attack_combo" };
                }
                return new string[] { "attack_abnormal_jump" };
            }
            if (this.myDistance < (this.attackDistance * 0.25f))
            {
                if (this.abnormalType == AbnormalType.TYPE_PUNK)
                {
                    return new string[] { "attack_kick", "attack_stomp" };
                }
                if (this.abnormalType == AbnormalType.NORMAL)
                {
                    return new string[] { "attack_front_ground", "attack_stomp" };
                }
                return new string[] { "attack_kick" };
            }
            if (this.myDistance < (this.attackDistance * 0.5f))
            {
                if ((this.abnormalType != AbnormalType.TYPE_PUNK) && (this.abnormalType == AbnormalType.NORMAL))
                {
                    return new string[] { "grab_ground_front_l", "grab_ground_front_l", "attack_stomp" };
                }
                return new string[] { "grab_ground_front_l", "grab_ground_front_l", "attack_abnormal_jump" };
            }
            if (this.abnormalType == AbnormalType.TYPE_PUNK)
            {
                return new string[] { "attack_combo", "attack_combo", "attack_abnormal_jump" };
            }
            if (this.abnormalType == AbnormalType.NORMAL)
            {
                if (this.myDifficulty > 0)
                {
                    return new string[] { "attack_front_ground", "attack_combo", "attack_combo" };
                }
                return new string[] { "attack_front_ground", "attack_front_ground", "attack_front_ground", "attack_front_ground", "attack_combo" };
            }
            return new string[] { "attack_abnormal_jump" };
        }
        if (this.between2 > 0f)
        {
            if (this.myDistance >= (this.attackDistance * 0.5f))
            {
                return strArray;
            }
            if (this.abnormalType == AbnormalType.NORMAL)
            {
                return new string[] { "grab_ground_back_r" };
            }
            return new string[] { "grab_ground_back_r" };
        }
        if (this.myDistance >= (this.attackDistance * 0.5f))
        {
            return strArray;
        }
        if (this.abnormalType == AbnormalType.NORMAL)
        {
            return new string[] { "grab_ground_back_l" };
        }
        return new string[] { "grab_ground_back_l" };
    }

    private void getDown()
    {
        this.state = TitanState.down;
        this.isAlarm = true;
        this.playAnimation("sit_hunt_down");
        this.getdownTime = UnityEngine.Random.Range((float) 3f, (float) 5f);
    }

    private GameObject getNearestHero()
    {
       
        GameObject obj2 = null;
        float positiveInfinity = float.PositiveInfinity;
        Vector3 position = this.baseTransform.position;
        foreach (GameObject obj3 in FengGameManagerMKII.instance.allheroes)
        {
            Vector3 vector2 = obj3.transform.position - position;
            float sqrMagnitude = vector2.sqrMagnitude;
            if (sqrMagnitude < positiveInfinity)
            {
                obj2 = obj3;
                positiveInfinity = sqrMagnitude;
            }
        }
        return obj2;
    }

    private GameObject getNearestHero2()
    {
        GameObject obj2 = null;
        float positiveInfinity = float.PositiveInfinity;
        Vector3 position = this.baseTransform.position;
        foreach (GameObject gameObject in this.MultiplayerManager.allheroes)
        {
            float num2 = Vector3.Distance(gameObject.transform.position, position);
            if (num2 < positiveInfinity)
            {
                obj2 = gameObject;
                positiveInfinity = num2;
            }
        }
        return obj2;
    }

    private int getPunkNumber()
    {
        int num = 0;
        foreach (TITAN titan in FengGameManagerMKII.instance.titans)
        {
            if ((titan.name == "Punk"))
            {
                num++;
            }
        }
        return num;
    }

    private void grab(string type)
    {
        this.state = TitanState.grab;
        this.attacked = false;
        this.isAlarm = true;
        this.attackAnimation = type;
        this.crossFade("grab_" + type, 0.1f);
        this.isGrabHandLeft = true;
        this.grabbedTargetT = null;
        string key = type;
        if (key != null)
        {
            int num;
            if (f__switchSmap7 == null)
            {
                Dictionary<string, int> dictionary = new Dictionary<string, int>(8) {
                    { 
                        "ground_back_l",
                        0
                    },
                    { 
                        "ground_back_r",
                        1
                    },
                    { 
                        "ground_front_l",
                        2
                    },
                    { 
                        "ground_front_r",
                        3
                    },
                    { 
                        "head_back_l",
                        4
                    },
                    { 
                        "head_back_r",
                        5
                    },
                    { 
                        "head_front_l",
                        6
                    },
                    { 
                        "head_front_r",
                        7
                    }
                };
                f__switchSmap7 = dictionary;
            }
            if (f__switchSmap7.TryGetValue(key, out num))
            {
                switch (num)
                {
                    case 0:
                        this.attackCheckTimeA = 0.34f;
                        this.attackCheckTimeB = 0.49f;
                        break;

                    case 1:
                        this.attackCheckTimeA = 0.34f;
                        this.attackCheckTimeB = 0.49f;
                        this.isGrabHandLeft = false;
                        break;

                    case 2:
                        this.attackCheckTimeA = 0.37f;
                        this.attackCheckTimeB = 0.6f;
                        break;

                    case 3:
                        this.attackCheckTimeA = 0.37f;
                        this.attackCheckTimeB = 0.6f;
                        this.isGrabHandLeft = false;
                        break;

                    case 4:
                        this.attackCheckTimeA = 0.45f;
                        this.attackCheckTimeB = 0.5f;
                        this.isGrabHandLeft = false;
                        break;

                    case 5:
                        this.attackCheckTimeA = 0.45f;
                        this.attackCheckTimeB = 0.5f;
                        break;

                    case 6:
                        this.attackCheckTimeA = 0.38f;
                        this.attackCheckTimeB = 0.55f;
                        break;

                    case 7:
                        this.attackCheckTimeA = 0.38f;
                        this.attackCheckTimeB = 0.55f;
                        this.isGrabHandLeft = false;
                        break;
                }
            }
        }
        if (this.isGrabHandLeft)
        {
            this.currentGrabHand = this.hand_L;
        }
        else
        {
            this.currentGrabHand = this.hand_R;
        }
    }

    [RPC]
    public void grabbedTargetEscape()
    {
        this.grabbedTargetT = null;
    }

    [RPC]
    public void grabToLeft()
    {
        Transform transform = this.hand_L;
        grabTFT.parent = transform;
        SphereCollider cs =  transform.GetComponent<SphereCollider>();
        this.grabTFT.position = cs.transform.position;
        this.grabTFT.rotation = cs.transform.rotation;
        this.grabTFT.localPosition -= (Vector3)((Vector3.right * cs.radius) * 0.3f);
        this.grabTFT.localPosition -= (Vector3)((Vector3.up * cs.radius) * 0.51f);
        this.grabTFT.localPosition -= (Vector3)((Vector3.forward * cs.radius) * 0.3f);
        this.grabTFT.localRotation = Quaternion.Euler(this.grabTFT.localRotation.eulerAngles.x, this.grabTFT.localRotation.eulerAngles.y + 180f, this.grabTFT.localRotation.eulerAngles.z + 180f);
    }

    [RPC]
    public void grabToRight()
    {
        Transform transform = this.hand_R;
        grabTFT.parent = transform;
        SphereCollider cs = transform.GetComponent<SphereCollider>();
        this.grabTFT.position = cs.transform.position;
        this.grabTFT.rotation = cs.transform.rotation;
        this.grabTFT.localPosition -= (Vector3)((Vector3.right * cs.radius) * 0.3f);
        this.grabTFT.localPosition += (Vector3)((Vector3.up * cs.radius) * 0.51f);
        this.grabTFT.localPosition -= (Vector3)((Vector3.forward * cs.radius) * 0.3f);
        this.grabTFT.localRotation = Quaternion.Euler(this.grabTFT.localRotation.eulerAngles.x, this.grabTFT.localRotation.eulerAngles.y + 180f, this.grabTFT.localRotation.eulerAngles.z);
    }

    public void headMovement()
    {
       
    }

    public void headMovement2()
    {
        if (!this.hasDie)
        {
            if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)
            {
                if (base.photonView.isMine)
                {
                    this.targetHeadRotation = this.head.rotation;
                    bool flag = false;
                    if (((((this.abnormalType != AbnormalType.TYPE_CRAWLER) && (this.state != TitanState.attack)) && ((this.state != TitanState.down) && (this.state != TitanState.hit))) && (((this.state != TitanState.recover) && (this.state != TitanState.eat)) && ((this.state != TitanState.hit_eye) && !this.hasDie))) && ((this.myDistance < 100f) && (this.myHero != null)))
                    {
                        Vector3 vector = this.myHero.transform.position - this.baseTransform.position;
                        this.angle = -Mathf.Atan2(vector.z, vector.x) * 57.29578f;
                        float num = -Mathf.DeltaAngle(this.angle, this.baseTransform.rotation.eulerAngles.y - 90f);
                        num = Mathf.Clamp(num, -40f, 40f);
                        float y = (this.neck.position.y + (this.myLevel * 2f)) - this.myHero.transform.position.y;
                        float num3 = Mathf.Atan2(y, this.myDistance) * 57.29578f;
                        num3 = Mathf.Clamp(num3, -40f, 30f);
                        this.targetHeadRotation = Quaternion.Euler(this.head.rotation.eulerAngles.x + num3, this.head.rotation.eulerAngles.y + num, this.head.rotation.eulerAngles.z);
                        if (!this.asClientLookTarget)
                        {
                            this.asClientLookTarget = true;
                            object[] parameters = new object[] { true };
                            base.photonView.RPC("setIfLookTarget", PhotonTargets.Others, parameters);
                        }
                        flag = true;
                    }
                    if (!flag && this.asClientLookTarget)
                    {
                        this.asClientLookTarget = false;
                        object[] objArray2 = new object[] { false };
                        base.photonView.RPC("setIfLookTarget", PhotonTargets.Others, objArray2);
                    }
                    if (((this.state == TitanState.attack) || (this.state == TitanState.hit)) || (this.state == TitanState.hit_eye))
                    {
                        this.oldHeadRotation = Quaternion.Lerp(this.oldHeadRotation, this.targetHeadRotation, Time.deltaTime * 20f);
                    }
                    else
                    {
                        this.oldHeadRotation = Quaternion.Lerp(this.oldHeadRotation, this.targetHeadRotation, Time.deltaTime * 10f);
                    }
                }
                else
                {
                    bool flag2;
                    if (flag2 = this.myHero != null)
                    {
                        this.myDistance = Mathf.Sqrt(((this.myHero.transform.position.x - this.baseTransform.position.x) * (this.myHero.transform.position.x - this.baseTransform.position.x)) + ((this.myHero.transform.position.z - this.baseTransform.position.z) * (this.myHero.transform.position.z - this.baseTransform.position.z)));
                    }
                    else
                    {
                        this.myDistance = float.MaxValue;
                    }
                    this.targetHeadRotation = this.head.rotation;
                    if ((this.asClientLookTarget && flag2) && (this.myDistance < 100f))
                    {
                        Vector3 vector2 = this.myHero.transform.position - this.baseTransform.position;
                        this.angle = -Mathf.Atan2(vector2.z, vector2.x) * 57.29578f;
                        float num4 = -Mathf.DeltaAngle(this.angle, this.baseTransform.rotation.eulerAngles.y - 90f);
                        num4 = Mathf.Clamp(num4, -40f, 40f);
                        float num5 = (this.neck.position.y + (this.myLevel * 2f)) - this.myHero.transform.position.y;
                        float num6 = Mathf.Atan2(num5, this.myDistance) * 57.29578f;
                        num6 = Mathf.Clamp(num6, -40f, 30f);
                        this.targetHeadRotation = Quaternion.Euler(this.head.rotation.eulerAngles.x + num6, this.head.rotation.eulerAngles.y + num4, this.head.rotation.eulerAngles.z);
                    }
                    if (!this.hasDie)
                    {
                        this.oldHeadRotation = Quaternion.Slerp(this.oldHeadRotation, this.targetHeadRotation, Time.deltaTime * 10f);
                    }
                }
            }
            else
            {
                this.targetHeadRotation = this.head.rotation;
                if (((((this.abnormalType != AbnormalType.TYPE_CRAWLER) && (this.state != TitanState.attack)) && ((this.state != TitanState.down) && (this.state != TitanState.hit))) && (((this.state != TitanState.recover) && (this.state != TitanState.hit_eye)) && (!this.hasDie && (this.myDistance < 100f)))) && (this.myHero != null))
                {
                    Vector3 vector3 = this.myHero.transform.position - this.baseTransform.position;
                    this.angle = -Mathf.Atan2(vector3.z, vector3.x) * 57.29578f;
                    float num7 = -Mathf.DeltaAngle(this.angle, this.baseTransform.rotation.eulerAngles.y - 90f);
                    num7 = Mathf.Clamp(num7, -40f, 40f);
                    float num8 = (this.neck.position.y + (this.myLevel * 2f)) - this.myHero.transform.position.y;
                    float num9 = Mathf.Atan2(num8, this.myDistance) * 57.29578f;
                    num9 = Mathf.Clamp(num9, -40f, 30f);
                    this.targetHeadRotation = Quaternion.Euler(this.head.rotation.eulerAngles.x + num9, this.head.rotation.eulerAngles.y + num7, this.head.rotation.eulerAngles.z);
                }
                if (((this.state == TitanState.attack) || (this.state == TitanState.hit)) || (this.state == TitanState.hit_eye))
                {
                    this.oldHeadRotation = Quaternion.Lerp(this.oldHeadRotation, this.targetHeadRotation, Time.deltaTime * 20f);
                }
                else
                {
                    this.oldHeadRotation = Quaternion.Lerp(this.oldHeadRotation, this.targetHeadRotation, Time.deltaTime * 10f);
                }
            }
            this.head.rotation = this.oldHeadRotation;
        }
        if (!this.baseAnimation.IsPlaying("die_headOff"))
        {
            this.head.localScale = this.headscale;
        }
    }

    private void hit(string animationName, Vector3 attacker, float hitPauseTime)
    {
        this.state = TitanState.hit;
        this.hitAnimation = animationName;
        this.hitPause = hitPauseTime;
        this.playAnimation(this.hitAnimation);
        this.baseAnimation[this.hitAnimation].time = 0f;
        this.baseAnimation[this.hitAnimation].speed = 0f;
        this.baseTransform.rotation = Quaternion.Euler(0f, Quaternion.LookRotation(attacker - this.baseTransform.position).eulerAngles.y, 0f);
        this.needFreshCorePosition = true;
        if (base.photonView.isMine && (this.grabbedTargetT != null))
        {
            grabbedTargetT.photonView.RPC("netUngrabbed", PhotonTargets.All, new object[0]);
        }
    }

    public void hitAnkle()
    {
        if (!this.hasDie && (this.state != TitanState.down))
        {
            if (this.grabbedTargetT != null)
            {
                grabbedTargetT.photonView.RPC("netUngrabbed", PhotonTargets.All, new object[0]);
            }
            this.getDown();
        }
    }

    [RPC]
    public void hitAnkleRPC(int viewID)
    {
        if (!this.hasDie && (this.state != TitanState.down))
        {
            PhotonView view = PhotonView.Find(viewID);
            if (view != null)
            {
                Vector3 vector = view.gameObject.transform.position - this.baseTransform.position;
                if (vector.magnitude < 20f)
                {
                    if (base.photonView.isMine && (this.grabbedTargetT != null))
                    {
                        this.grabbedTargetT.photonView.RPC("netUngrabbed", PhotonTargets.All, new object[0]);
                    }
                    this.getDown();
                }
            }
        }
    }

    public void hitEye()
    {
        if (!this.hasDie)
        {
            this.justHitEye();
        }
    }

    [RPC]
    public void hitEyeRPC(int viewID)
    {
        if (!this.hasDie)
        {
            Vector3 vector = PhotonView.Find(viewID).gameObject.transform.position - this.neck.position;
            if (vector.magnitude < 20f)
            {
                if (base.photonView.isMine && (this.grabbedTargetT != null))
                {
                    this.grabbedTargetT.photonView.RPC("netUngrabbed", PhotonTargets.All, new object[0]);
                }
                if (!this.hasDie)
                {
                    this.justHitEye();
                }
            }
        }
    }

    public void hitL(Vector3 attacker, float hitPauseTime)
    {
        if (this.abnormalType != AbnormalType.TYPE_CRAWLER)
        {
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                this.hit("hit_eren_L", attacker, hitPauseTime);
            }
            else
            {
                object[] parameters = new object[] { attacker, hitPauseTime };
                base.photonView.RPC("hitLRPC", PhotonTargets.All, parameters);
            }
        }
    }

    [RPC]
    private void hitLRPC(Vector3 attacker, float hitPauseTime)
    {
        if (base.photonView.isMine)
        {
            Vector3 vector = attacker - this.baseTransform.position;
            if (vector.magnitude < 80f)
            {
                this.hit("hit_eren_L", attacker, hitPauseTime);
            }
        }
    }

    public void hitR(Vector3 attacker, float hitPauseTime)
    {
        if (this.abnormalType != AbnormalType.TYPE_CRAWLER)
        {
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                this.hit("hit_eren_R", attacker, hitPauseTime);
            }
            else
            {
                object[] parameters = new object[] { attacker, hitPauseTime };
                base.photonView.RPC("hitRRPC", PhotonTargets.All, parameters);
            }
        }
    }

    [RPC]
    private void hitRRPC(Vector3 attacker, float hitPauseTime)
    {
        if (base.photonView.isMine && !this.hasDie)
        {
            Vector3 vector = attacker - this.baseTransform.position;
            if (vector.magnitude < 80f)
            {
                this.hit("hit_eren_R", attacker, hitPauseTime);
            }
        }
    }

    private void idle(float sbtime = 0f)
    {
        this.stuck = false;
        this.sbtime = sbtime;
        if ((this.myDifficulty == 2) && ((this.abnormalType == AbnormalType.TYPE_JUMPER) || (this.abnormalType == AbnormalType.TYPE_I)))
        {
            this.sbtime = UnityEngine.Random.Range((float) 0f, (float) 1.5f);
        }
        else if (this.myDifficulty >= 1)
        {
            this.sbtime = 0f;
        }
        this.sbtime = Mathf.Max(0.5f, this.sbtime);
        if (this.abnormalType == AbnormalType.TYPE_PUNK)
        {
            this.sbtime = 0.1f;
            if (this.myDifficulty == 1)
            {
                this.sbtime += 0.4f;
            }
        }
        this.state = TitanState.idle;
        if (this.abnormalType == AbnormalType.TYPE_CRAWLER)
        {
            this.crossFade("crawler_idle", 0.2f);
        }
        else
        {
            this.crossFade("idle", 0.2f);
        }
    }

    public bool IsGrounded()
    {
        LayerMask mask = ((int) 1) << LayerMask.NameToLayer("Ground");
        LayerMask mask2 = ((int) 1) << LayerMask.NameToLayer("EnemyAABB");
        LayerMask mask3 = mask2 | mask;
        return Physics.Raycast(base.gameObject.transform.position + ((Vector3) (Vector3.up * 0.1f)), -Vector3.up, (float) 0.3f, mask3.value);
    }

    private void justEatHero(HERO hero, Transform hand)
    {
        if (hero != null)
        {
            if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && base.photonView.isMine)
            {
                if (!hero.HasDied())
                {
                    hero.markDie();
                    if (this.nonAI)
                    {
                        object[] parameters = new object[] { base.photonView.viewID, base.name };
                        hero.photonView.RPC("netDie2", PhotonTargets.All, parameters);
                    }
                    else
                    {
                        object[] objArray2 = new object[] { -1, base.name };
                        hero.photonView.RPC("netDie2", PhotonTargets.All, objArray2);
                    }
                }
            }
            else if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                hero.die2(hand);
            }
        }
    }

    private void justHitEye()
    {
        if (this.state != TitanState.hit_eye)
        {
            if ((this.state != TitanState.down) && (this.state != TitanState.sit))
            {
                this.playAnimation("hit_eye");
            }
            else
            {
                this.playAnimation("sit_hit_eye");
            }
            this.state = TitanState.hit_eye;
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
                this.healthLabel = (GameObject) UnityEngine.Object.Instantiate(Cach.UI_LabelNameOverHead != null ? Cach.UI_LabelNameOverHead:Cach.UI_LabelNameOverHead = (GameObject) Resources.Load("UI/LabelNameOverHead"));
                this.healthLabel.name = "LabelNameOverHead";
                this.healthLabel.transform.parent = this.baseTransform;
                this.healthLabel.transform.localPosition = new Vector3(0f, 20f + (1f / this.myLevel), 0f);
                if (this.abnormalType == AbnormalType.TYPE_CRAWLER)
                {
                    this.healthLabel.transform.localPosition = new Vector3(0f, 10f + (1f / this.myLevel), 0f);
                }
                float x = 1f;
                if (this.myLevel < 1f)
                {
                    x = 1f / this.myLevel;
                }
                this.healthLabel.transform.localScale = new Vector3(x, x, x);
                this.healthLabelEnabled = true;
                healthLabelT = this.healthLabel.GetComponent<UILabel>();
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

    public void lateUpdate()
    {
        
    }

    public void lateUpdate2()
    {
        if (!IN_GAME_MAIN_CAMERA.isPausing || (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE))
        {
            if (this.baseAnimation.IsPlaying("run_walk"))
            {
                if ((((this.baseAnimation["run_walk"].normalizedTime % 1f) > 0.1f) && ((this.baseAnimation["run_walk"].normalizedTime % 1f) < 0.6f)) && (this.stepSoundPhase == 2))
                {
                    this.stepSoundPhase = 1;
                    if ((int)FengGameManagerMKII.settings[330] == 0)
                    {
                        this.baseAudioSource.Stop();
                        this.baseAudioSource.Play();
                    }
                }
                else if (((this.baseAnimation["run_walk"].normalizedTime % 1f) > 0.6f) && (this.stepSoundPhase == 1))
                {
                    this.stepSoundPhase = 2;
                    if ((int)FengGameManagerMKII.settings[330] == 0)
                    {
                        this.baseAudioSource.Stop();
                        this.baseAudioSource.Play();
                    }
                }
            }
            else if (this.baseAnimation.IsPlaying("crawler_run"))
            {
                if ((((this.baseAnimation["crawler_run"].normalizedTime % 1f) > 0.1f) && ((this.baseAnimation["crawler_run"].normalizedTime % 1f) < 0.56f)) && (this.stepSoundPhase == 2))
                {
                    this.stepSoundPhase = 1;
                    if ((int)FengGameManagerMKII.settings[330] == 0)
                    {
                        this.baseAudioSource.Stop();
                        this.baseAudioSource.Play();
                    }
                }
                else if (((this.baseAnimation["crawler_run"].normalizedTime % 1f) > 0.56f) && (this.stepSoundPhase == 1))
                {
                    this.stepSoundPhase = 2;
                    if ((int)FengGameManagerMKII.settings[330] == 0)
                    {
                        this.baseAudioSource.Stop();
                        this.baseAudioSource.Play();
                    }
                }
            }
            else if (this.baseAnimation.IsPlaying("run_abnormal"))
            {
                if ((((this.baseAnimation["run_abnormal"].normalizedTime % 1f) > 0.47f) && ((this.baseAnimation["run_abnormal"].normalizedTime % 1f) < 0.95f)) && (this.stepSoundPhase == 2))
                {
                    this.stepSoundPhase = 1;
                    if ((int)FengGameManagerMKII.settings[330] == 0)
                    {
                        this.baseAudioSource.Stop();
                        this.baseAudioSource.Play();
                    }
                }
                else if (!((((this.baseAnimation["run_abnormal"].normalizedTime % 1f) > 0.95f) || ((this.baseAnimation["run_abnormal"].normalizedTime % 1f) < 0.47f)) ? (this.stepSoundPhase != 1) : true))
                {
                    this.stepSoundPhase = 2;
                    if ((int)FengGameManagerMKII.settings[330] == 0)
                    {
                        this.baseAudioSource.Stop();
                        this.baseAudioSource.Play();
                    }
                }
            }
            this.headMovement2();
            this.grounded = false;
            this.updateLabel();
            this.updateCollider();
        }
    }

    [RPC]
    private void laugh(float sbtime = 0f)
    {
        if (((this.state == TitanState.idle) || (this.state == TitanState.turn)) || (this.state == TitanState.chase))
        {
            this.sbtime = sbtime;
            this.state = TitanState.laugh;
            this.crossFade("laugh", 0.2f);
        }
    }

    public void loadskin()
    {
        this.skin = 0x56;
        this.eye = false;
        if (!(((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE) || base.photonView.isMine) ? (((int) FengGameManagerMKII.settings[1]) != 1) : true))
        {
            int index = (int) UnityEngine.Random.Range((float) 86f, (float) 90f);
            int num2 = index - 60;
            if (((int) FengGameManagerMKII.settings[0x20]) == 1)
            {
                num2 = UnityEngine.Random.Range(0x1a, 30);
            }
            string body = (string) FengGameManagerMKII.settings[index];
            string eye = (string) FengGameManagerMKII.settings[num2];
            this.skin = index;
            if ((eye.EndsWith(".jpg") || eye.EndsWith(".png")) || eye.EndsWith(".jpeg"))
            {
                this.eye = true;
            }
            base.GetComponent<TITAN_SETUP>().setVar(this.skin, this.eye);
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                base.StartCoroutine(this.loadskinE(body, eye));
            }
            else
            {
                base.photonView.RPC("loadskinRPC", PhotonTargets.AllBuffered, new object[] { body, eye });
            }
        }
    }

    public IEnumerator loadskinE(string body, string eye)
    {
        while (!this.hasSpawn)
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
            if (iteratorVariable2.name.Contains("eye"))
            {
                if (eye.ToLower() == "transparent")
                {
                    iteratorVariable2.enabled = false;
                }
                else if ((eye.EndsWith(".jpg") || eye.EndsWith(".png")) || eye.EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(eye))
                    {
                        WWW link = new WWW(eye);
                        yield return link;
                        if (link.error == null)
                        {
                            Texture2D iteratorVariable4 = cext.loadimage(link, mipmap, 0x30d40);
                            link.Dispose();
                            if (!FengGameManagerMKII.linkHash[0].ContainsKey(eye))
                            {
                                iteratorVariable1 = true;
                                iteratorVariable2.material.mainTextureScale = new Vector2(iteratorVariable2.material.mainTextureScale.x * 4f, iteratorVariable2.material.mainTextureScale.y * 8f);
                                iteratorVariable2.material.mainTextureOffset = new Vector2(0f, 0f);
                                iteratorVariable2.material.mainTexture = iteratorVariable4;
                                FengGameManagerMKII.linkHash[0].Add(eye, iteratorVariable2.material);
                                iteratorVariable2.material = (Material)FengGameManagerMKII.linkHash[0][eye];
                            }
                            else
                            {
                                iteratorVariable2.material = (Material)FengGameManagerMKII.linkHash[0][eye];
                            }
                        }
                        else
                        {
                            PanelInformer.instance.Add("Error load skins " + base.name + ":" + link.url, PanelInformer.LOG_TYPE.DANGER);
                        }
                    }
                    else
                    {
                        iteratorVariable2.material = (Material) FengGameManagerMKII.linkHash[0][eye];
                    }
                }
            }
            else if ((iteratorVariable2.name == "hair") && ((body.EndsWith(".jpg") || body.EndsWith(".png")) || body.EndsWith(".jpeg")))
            {
                if (!FengGameManagerMKII.linkHash[2].ContainsKey(body))
                {
                    WWW iteratorVariable5 = new WWW(body);
                    yield return iteratorVariable5;
                    if (iteratorVariable5.error == null)
                    {
                        Texture2D iteratorVariable6 = cext.loadimage(iteratorVariable5, mipmap, 0xf4240);
                        iteratorVariable5.Dispose();
                        if (!FengGameManagerMKII.linkHash[2].ContainsKey(body))
                        {
                            iteratorVariable1 = true;
                            iteratorVariable2.material = this.mainMaterial.GetComponent<SkinnedMeshRenderer>().material;
                            iteratorVariable2.material.mainTexture = iteratorVariable6;
                            FengGameManagerMKII.linkHash[2].Add(body, iteratorVariable2.material);
                            iteratorVariable2.material = (Material)FengGameManagerMKII.linkHash[2][body];
                        }
                        else
                        {
                            iteratorVariable2.material = (Material)FengGameManagerMKII.linkHash[2][body];
                        }
                    }
                    else
                    {
                        PanelInformer.instance.Add("Error load skins " + base.name + ":" + iteratorVariable5.url, PanelInformer.LOG_TYPE.DANGER);
                    }
                }
                else
                {
                    iteratorVariable2.material = (Material) FengGameManagerMKII.linkHash[2][body];
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
    public void loadskinRPC(string body, string eye)
    {
        if (((int) FengGameManagerMKII.settings[1]) == 1)
        {
            base.StartCoroutine(this.loadskinE(body, eye));
        }
    }

    private bool longRangeAttackCheck()
    {
        return false;
    }
    Transform chkAeLeft;
    Transform chkAeLLeft;
    Transform chkAeRight;
    Transform chkAeLRight;
    private bool longRangeAttackCheck2()
    {
        if ((this.abnormalType == AbnormalType.TYPE_PUNK) && (this.myHero != null))
        {
            Vector3 line = (Vector3) ((this.myHero.rigidbody.velocity * Time.deltaTime) * 30f);
            if (line.sqrMagnitude > 10f)
            {
                if (this.simpleHitTestLineAndBall(line, (chkAeLeft != null ?chkAeLeft: chkAeLeft= this.baseTransform.Find("chkAeLeft")).position - this.myHero.transform.position, 5f * this.myLevel))
                {
                    this.attack2("anti_AE_l");
                    return true;
                }
                if (this.simpleHitTestLineAndBall(line, (chkAeLLeft != null ? chkAeLLeft : chkAeLLeft = this.baseTransform.Find("chkAeLLeft")).position - this.myHero.transform.position, 5f * this.myLevel))
                {
                    this.attack2("anti_AE_low_l");
                    return true;
                }
                if (this.simpleHitTestLineAndBall(line, (chkAeRight != null ? chkAeRight : chkAeRight = this.baseTransform.Find("chkAeRight")).position - this.myHero.transform.position, 5f * this.myLevel))
                {
                    this.attack2("anti_AE_r");
                    return true;
                }
                if (this.simpleHitTestLineAndBall(line, (chkAeLRight != null ? chkAeLRight : chkAeLRight = this.baseTransform.Find("chkAeLRight")).position - this.myHero.transform.position, 5f * this.myLevel))
                {
                    this.attack2("anti_AE_low_r");
                    return true;
                }
            }
            Vector3 vector2 = this.myHero.transform.position - this.baseTransform.position;
            float current = -Mathf.Atan2(vector2.z, vector2.x) * 57.29578f;
            float f = -Mathf.DeltaAngle(current, this.baseGameObjectTransform.rotation.eulerAngles.y - 90f);
            if (this.rockInterval > 0f)
            {
                this.rockInterval -= Time.deltaTime;
            }
            else if (Mathf.Abs(f) < 5f)
            {
                Vector3 vector3 = this.myHero.transform.position + line;
                Vector3 vector4 = vector3 - this.baseTransform.position;
                float sqrMagnitude = vector4.sqrMagnitude;
                if (((sqrMagnitude > 8000f) && (sqrMagnitude < 90000f)) && (RCSettings.disableRock == 0))
                {
                    this.attack2("throw");
                    this.rockInterval = 2f;
                    return true;
                }
            }
        }
        return false;
    }

    public void moveTo(float posX, float posY, float posZ)
    {
        this.baseTransform.position = new Vector3(posX, posY, posZ);
    }

    [RPC]
    public void moveToRPC(float posX, float posY, float posZ, PhotonMessageInfo info)
    {
        if (info.sender.isMasterClient)
        {
            this.baseTransform.position = new Vector3(posX, posY, posZ);
        }
    }

    [RPC]
    private void netCrossFade(string aniName, float time)
    {
        this.baseAnimation.CrossFade(aniName, time);
    }

    [RPC]
    private void netDie()
    {
        this.asClientLookTarget = false;
        if (!this.hasDie)
        {
            this.hasDie = true;
            if (this.nonAI)
            {
                this.currentCameraT.setMainObject(null, true, false);
                this.currentCameraT.setSpectorMode(true);
                this.currentCameraT.gameOver = true;
                PhotonNetwork.player.dead = true;
                PhotonNetwork.player.deaths = (PhotonNetwork.player.deaths) + 1;
            }
            this.dieAnimation();
        }
    }

    [RPC]
    private void netPlayAnimation(string aniName)
    {
        this.baseAnimation.Play(aniName);
    }

    [RPC]
    private void netPlayAnimationAt(string aniName, float normalizedTime)
    {
        this.baseAnimation.Play(aniName);
        this.baseAnimation[aniName].normalizedTime = normalizedTime;
    }

    [RPC]
    private void netSetAbnormalType(int type,PhotonMessageInfo info = null)
    {
        if (isAllow(info))
        {
            if (!this.hasload)
            {
                this.hasload = true;
                this.loadskin();
            }
            if (type == 0)
            {
                this.abnormalType = AbnormalType.NORMAL;
                base.name = "Titan";
                this.runAnimation = "run_walk";
                base.GetComponent<TITAN_SETUP>().setHair2();
            }
            else if (type == 1)
            {
                this.abnormalType = AbnormalType.TYPE_I;
                base.name = "Aberrant";
                this.runAnimation = "run_abnormal";
                base.GetComponent<TITAN_SETUP>().setHair2();
            }
            else if (type == 2)
            {
                this.abnormalType = AbnormalType.TYPE_JUMPER;
                base.name = "Jumper";
                this.runAnimation = "run_abnormal";
                base.GetComponent<TITAN_SETUP>().setHair2();
            }
            else if (type == 3)
            {
                this.abnormalType = AbnormalType.TYPE_CRAWLER;
                base.name = "Crawler";
                this.runAnimation = "crawler_run";
                base.GetComponent<TITAN_SETUP>().setHair2();
            }
            else if (type == 4)
            {
                this.abnormalType = AbnormalType.TYPE_PUNK;
                base.name = "Punk";
                this.runAnimation = "run_abnormal_1";
                base.GetComponent<TITAN_SETUP>().setHair2();
            }
            if (((this.abnormalType == AbnormalType.TYPE_I) || (this.abnormalType == AbnormalType.TYPE_JUMPER)) || (this.abnormalType == AbnormalType.TYPE_PUNK))
            {
                this.speed = 18f;
                if (this.myLevel > 1f)
                {
                    this.speed *= Mathf.Sqrt(this.myLevel);
                }
                if (this.myDifficulty == 1)
                {
                    this.speed *= 1.4f;
                }
                if (this.myDifficulty == 2)
                {
                    this.speed *= 1.6f;
                }
                this.baseAnimation["turnaround1"].speed = 2f;
                this.baseAnimation["turnaround2"].speed = 2f;
            }
            if (this.abnormalType == AbnormalType.TYPE_CRAWLER)
            {
                this.chaseDistance += 50f;
                this.speed = 25f;
                if (this.myLevel > 1f)
                {
                    this.speed *= Mathf.Sqrt(this.myLevel);
                }
                if (this.myDifficulty == 1)
                {
                    this.speed *= 2f;
                }
                if (this.myDifficulty == 2)
                {
                    this.speed *= 2.2f;
                }
                CapsuleCollider foo = this.colider_AABB.gameObject.GetComponent<CapsuleCollider>();
                foo.height = 10f;
                foo.radius = 5f;
                foo.center = new Vector3(0f, 5.05f, 0f);
            }
            if (this.nonAI)
            {
                if (this.abnormalType == AbnormalType.TYPE_CRAWLER)
                {
                    this.speed = Mathf.Min(70f, this.speed);
                }
                else
                {
                    this.speed = Mathf.Min(60f, this.speed);
                }
                this.baseAnimation["attack_jumper_0"].speed = 7f;
                this.baseAnimation["attack_crawler_jump_0"].speed = 4f;
            }
            this.baseAnimation["attack_combo_1"].speed = 1f;
            this.baseAnimation["attack_combo_2"].speed = 1f;
            this.baseAnimation["attack_combo_3"].speed = 1f;
            this.baseAnimation["attack_quick_turn_l"].speed = 1f;
            this.baseAnimation["attack_quick_turn_r"].speed = 1f;
            this.baseAnimation["attack_anti_AE_l"].speed = 1.1f;
            this.baseAnimation["attack_anti_AE_low_l"].speed = 1.1f;
            this.baseAnimation["attack_anti_AE_r"].speed = 1.1f;
            this.baseAnimation["attack_anti_AE_low_r"].speed = 1.1f;
            this.idle(0f);
        }
    }

    bool isAllow(PhotonMessageInfo info)
    {
        if (info == null)
        {
            return true;
        }
        else
        {
            PhotonPlayer player = info.sender;
            if (player.isMasterClient)
            {
                return true;
            }
            else
            {
                return base.gameObject.GetComponent<SmoothSyncMovement>().photonView.owner.ID == info.sender.ID;
            }
        }
    }
    [RPC]
    private void netSetLevel(float level, int AI, int skinColor,PhotonMessageInfo info = null)
    {
        if (isAllow(info))
        {
            this.setLevel2(level, AI, skinColor);
            if (level > 5f)
            {
                this.headscale = new Vector3(1f, 1f, 1f);
            }
            else if ((level < 1f) && FengGameManagerMKII.level.StartsWith("Custom"))
            {
                CapsuleCollider component = this.myTitanTrigger.GetComponent<CapsuleCollider>();
                component.radius *= 2.5f - level;
            }
        }
    }

    private void OnCollisionStay()
    {
        this.grounded = true;
    }

    private void OnDestroy()
    {
        if (CyanMod.CachingsGM.Find("MultiplayerManager") != null)
        {
            FengGameManagerMKII.instance.removeTitan(this);
        }
    }

    public void OnTitanDie(PhotonView view)
    {
        if (FengGameManagerMKII.logicLoaded && FengGameManagerMKII.RCEvents.ContainsKey("OnTitanDie"))
        {
            RCEvent event2 = (RCEvent) FengGameManagerMKII.RCEvents["OnTitanDie"];
            string[] strArray = (string[]) FengGameManagerMKII.RCVariableNames["OnTitanDie"];
            if (FengGameManagerMKII.titanVariables.ContainsKey(strArray[0]))
            {
                FengGameManagerMKII.titanVariables[strArray[0]] = this;
            }
            else
            {
                FengGameManagerMKII.titanVariables.Add(strArray[0], this);
            }
            if (FengGameManagerMKII.playerVariables.ContainsKey(strArray[1]))
            {
                FengGameManagerMKII.playerVariables[strArray[1]] = view.owner;
            }
            else
            {
                FengGameManagerMKII.playerVariables.Add(strArray[1], view.owner);
            }
            event2.checkEvent();
        }
    }

    private void playAnimation(string aniName)
    {
        this.baseAnimation.Play(aniName);
        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && base.photonView.isMine)
        {
            object[] parameters = new object[] { aniName };
            base.photonView.RPC("netPlayAnimation", PhotonTargets.Others, parameters);
        }
    }

    private void playAnimationAt(string aniName, float normalizedTime)
    {
        this.baseAnimation.Play(aniName);
        this.baseAnimation[aniName].normalizedTime = normalizedTime;
        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && base.photonView.isMine)
        {
            object[] parameters = new object[] { aniName, normalizedTime };
            base.photonView.RPC("netPlayAnimationAt", PhotonTargets.Others, parameters);
        }
    }

    private void playSound(string sndname)
    {
        this.playsoundRPC(sndname);
        if (base.photonView.isMine)
        {
            object[] parameters = new object[] { sndname };
            base.photonView.RPC("playsoundRPC", PhotonTargets.Others, parameters);
        }
    }

    [RPC]
    private void playsoundRPC(string sndname)
    {
        if ((int)FengGameManagerMKII.settings[330] == 0)
        {
            this.tfind(sndname).GetComponent<AudioSource>().Play();
        }
    }

    public void pt()
    {
        if (this.controller.bite)
        {
            this.attack2("bite");
        }
        if (this.controller.bitel)
        {
            this.attack2("bite_l");

        }
        if (this.controller.biter)
        {
            this.attack2("bite_r");

        }
        if (this.controller.chopl)
        {
            this.attack2("anti_AE_low_l");

        }
        if (this.controller.chopr)
        {
            this.attack2("anti_AE_low_r");

        }
        if (this.controller.choptl)
        {
            this.attack2("anti_AE_l");

        }
        if (this.controller.choptr)
        {
            this.attack2("anti_AE_r");

        }
        if (this.controller.cover && (this.stamina > 75f))
        {
            this.recoverpt();
            this.stamina -= 75f;

        }
        if (this.controller.grabbackl)
        {
            this.grab("ground_back_l");

        }
        if (this.controller.grabbackr)
        {
            this.grab("ground_back_r");

        }
        if (this.controller.grabfrontl)
        {
            this.grab("ground_front_l");

        }
        if (this.controller.grabfrontr)
        {
            this.grab("ground_front_r");
        }
        if (this.controller.grabnapel)
        {
            this.grab("head_back_l");

        }
        if (this.controller.grabnaper)
        {
            this.grab("head_back_r");

        }
        if (this.controller.laugh)
        {
            object[] parameters = new object[] { 0f };
            base.photonView.RPC("laugh", PhotonTargets.All, parameters);

        }
        if (this.controller.sitdown)
        {
            sitdown(false);
        }
        if (this.controller.attackDown)
        {
            this.attack2("slap_back");
        }
        if (this.controller.attackFace)
        {
            this.attack2("slap_face");
        }
    }

    public void randomRun(Vector3 targetPt, float r)
    {
        this.state = TitanState.random_run;
        this.targetCheckPt = targetPt;
        this.targetR = r;
        this.random_run_time = UnityEngine.Random.Range((float) 1f, (float) 2f);
        this.crossFade(this.runAnimation, 0.5f);
    }

    private void recover()
    {
        this.state = TitanState.recover;
        this.playAnimation("idle_recovery");
        this.getdownTime = UnityEngine.Random.Range((float) 2f, (float) 5f);
    }

    private void recoverpt()
    {
        this.state = TitanState.recover;
        this.playAnimation("idle_recovery");
        this.getdownTime = UnityEngine.Random.Range((float) 1.8f, (float) 2.5f);
    }

    public IEnumerator reloadSky()
    {
        yield return new WaitForSeconds(0.5f);
        if ((FengGameManagerMKII.skyMaterial != null) && (Camera.main.GetComponent<Skybox>().material != FengGameManagerMKII.skyMaterial))
        {
            Camera.main.GetComponent<Skybox>().material = FengGameManagerMKII.skyMaterial;
        }
        yield break;
    }

    private void remainSitdown()
    {
        this.state = TitanState.sit;
        this.playAnimation("sit_idle");
        this.getdownTime = UnityEngine.Random.Range((float) 10f, (float) 30f);
    }

    public void resetLevel(float level)
    {
        this.myLevel = level;
        this.setmyLevel();
    }

    public void setAbnormalType(AbnormalType type, bool forceCrawler = false)
    {
        int num = 0;
        float num2 = 0.02f * ((int)IN_GAME_MAIN_CAMERA.difficulty + 1);
        if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_AHSS)
        {
            num2 = 100f;
        }
        if (type == AbnormalType.NORMAL)
        {
            if (UnityEngine.Random.Range((float) 0f, (float) 1f) < num2)
            {
                num = 4;
            }
            else
            {
                num = 0;
            }
        }
        else if (type == AbnormalType.TYPE_I)
        {
            if (UnityEngine.Random.Range((float) 0f, (float) 1f) < num2)
            {
                num = 4;
            }
            else
            {
                num = 1;
            }
        }
        else if (type == AbnormalType.TYPE_JUMPER)
        {
            if (UnityEngine.Random.Range((float) 0f, (float) 1f) < num2)
            {
                num = 4;
            }
            else
            {
                num = 2;
            }
        }
        else if (type == AbnormalType.TYPE_CRAWLER)
        {
            num = 3;
            if ((CyanMod.CachingsGM.Find("Crawler") != null) && (UnityEngine.Random.Range(0, 0x3e8) > 5))
            {
                num = 2;
            }
        }
        else if (type == AbnormalType.TYPE_PUNK)
        {
            num = 4;
        }
        if (forceCrawler)
        {
            num = 3;
        }
        if (num == 4)
        {
            if (!FengGameManagerMKII.lvlInfo.punk)
            {
                num = 1;
            }
            else
            {
                if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE) && (this.getPunkNumber() >= 3))
                {
                    num = 1;
                }
                if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)
                {
                    int wave = FengGameManagerMKII.instance.wave;
                    if (((wave != 5) && (wave != 10)) && ((wave != 15) && (wave != 20)))
                    {
                        num = 1;
                    }
                }
            }
        }
        if ((IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE) && base.photonView.isMine)
        {
            object[] parameters = new object[] { num };
            base.photonView.RPC("netSetAbnormalType", PhotonTargets.AllBuffered, parameters);
        }
        else if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
        {
            this.netSetAbnormalType(num);
        }
    }

    public void setAbnormalType2(AbnormalType type, bool forceCrawler)
    {
        bool flag = false;
        if ((RCSettings.spawnMode > 0) || (((((int) FengGameManagerMKII.settings[0x5b]) == 1) && (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)) && PhotonNetwork.isMasterClient))
        {
            flag = true;
        }
        if (FengGameManagerMKII.level.StartsWith("Custom"))
        {
            flag = true;
        }
        int num = 0;
        float num2 = 0.02f * ((int)IN_GAME_MAIN_CAMERA.difficulty + 1);
        if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_AHSS)
        {
            num2 = 100f;
        }
        if (type == AbnormalType.NORMAL)
        {
            if (UnityEngine.Random.Range((float) 0f, (float) 1f) < num2)
            {
                num = 4;
            }
            else
            {
                num = 0;
            }
            if (flag)
            {
                num = 0;
            }
        }
        else if (type == AbnormalType.TYPE_I)
        {
            if (UnityEngine.Random.Range((float) 0f, (float) 1f) < num2)
            {
                num = 4;
            }
            else
            {
                num = 1;
            }
            if (flag)
            {
                num = 1;
            }
        }
        else if (type == AbnormalType.TYPE_JUMPER)
        {
            if (UnityEngine.Random.Range((float) 0f, (float) 1f) < num2)
            {
                num = 4;
            }
            else
            {
                num = 2;
            }
            if (flag)
            {
                num = 2;
            }
        }
        else if (type == AbnormalType.TYPE_CRAWLER)
        {
            num = 3;
            if ((CyanMod.CachingsGM.Find("Crawler") != null) && (UnityEngine.Random.Range(0, 0x3e8) > 5))
            {
                num = 2;
            }
            if (flag)
            {
                num = 3;
            }
        }
        else if (type == AbnormalType.TYPE_PUNK)
        {
            num = 4;
        }
        if (forceCrawler)
        {
            num = 3;
        }
        if (num == 4)
        {
            if (!FengGameManagerMKII.lvlInfo.punk)
            {
                num = 1;
            }
            else
            {
                if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE) && (this.getPunkNumber() >= 3))
                {
                    num = 1;
                }
                if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)
                {
                    int wave = FengGameManagerMKII.instance.wave;
                    if (((wave != 5) && (wave != 10)) && ((wave != 15) && (wave != 20)))
                    {
                        num = 1;
                    }
                }
            }
            if (flag)
            {
                num = 4;
            }
        }
        if ((IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE) && base.photonView.isMine)
        {
            object[] parameters = new object[] { num };
            base.photonView.RPC("netSetAbnormalType", PhotonTargets.AllBuffered, parameters);
        }
        else if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
        {
            this.netSetAbnormalType(num);
        }
    }

    [RPC]
    private void setIfLookTarget(bool bo)
    {
        this.asClientLookTarget = bo;
    }

    private void setLevel(float level, int AI, int skinColor)
    {
       
    }

    TextMesh label_size;

    private void setLevel2(float level, int AI, int skinColor)
    {
        this.myLevel = level;
        this.myLevel = Mathf.Clamp(this.myLevel, 0.1f, 50f);
        this.attackWait += UnityEngine.Random.Range((float) 0f, (float) 2f);
        this.chaseDistance += this.myLevel * 10f;
        this.baseTransform.localScale = new Vector3(this.myLevel, this.myLevel, this.myLevel);
        float x = Mathf.Min(Mathf.Pow(2f / this.myLevel, 0.35f), 1.25f);
        this.headscale = new Vector3(x, x, x);
        this.head = this.headT;
        this.head.localScale = this.headscale;
        if (skinColor != 0)
        {
            this.mainMaterial.GetComponent<SkinnedMeshRenderer>().material.color = (skinColor != 1) ? ((skinColor != 2) ? FengColor.titanSkin3 : FengColor.titanSkin2) : FengColor.titanSkin1;
        }
        float num2 = 1.4f - ((this.myLevel - 0.7f) * 0.15f);
        num2 = Mathf.Clamp(num2, 0.9f, 1.5f);
        foreach (AnimationState state in this.baseAnimation)
        {
            state.speed = num2;
        }
        Rigidbody rigidbody = base.rigidbody;
        rigidbody.mass *= this.myLevel;
        base.rigidbody.rotation = Quaternion.Euler(0f, (float) UnityEngine.Random.Range(0, 360), 0f);
        if (this.myLevel > 1f)
        {
            this.speed *= Mathf.Sqrt(this.myLevel);
        }
        this.myDifficulty = AI;
        if ((this.myDifficulty == 1) || (this.myDifficulty == 2))
        {
            foreach (AnimationState state2 in this.baseAnimation)
            {
                state2.speed = num2 * 1.05f;
            }
            if (this.nonAI)
            {
                this.speed *= 1.1f;
            }
            else
            {
                this.speed *= 1.4f;
            }
            this.chaseDistance *= 1.15f;
        }
        if (this.myDifficulty == 2)
        {
            foreach (AnimationState state3 in this.baseAnimation)
            {
                state3.speed = num2 * 1.05f;
            }
            if (this.nonAI)
            {
                this.speed *= 1.1f;
            }
            else
            {
                this.speed *= 1.5f;
            }
            this.chaseDistance *= 1.3f;
        }
        if ((IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.ENDLESS_TITAN) || (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE))
        {
            this.chaseDistance = 999999f;
        }
        if (this.nonAI)
        {
            if (this.abnormalType == AbnormalType.TYPE_CRAWLER)
            {
                this.speed = Mathf.Min(70f, this.speed);
            }
            else
            {
                this.speed = Mathf.Min(60f, this.speed);
            }
        }
        if (label_size == null && (int)FengGameManagerMKII.settings[341] == 1)                                              
        {
            label_size = cext.LabelObjext( this);
        }
        this.attackDistance = Vector3.Distance(this.baseTransform.position, this.tfind("ap_front_ground").position) * 1.65f;
    }

    private void setmyLevel()
    {
        this.baseAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
        if ((IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE) && base.photonView.isMine)
        {
            object[] parameters = new object[] { this.myLevel, (int)IN_GAME_MAIN_CAMERA.difficulty, UnityEngine.Random.Range(0, 4) };
            base.photonView.RPC("netSetLevel", PhotonTargets.AllBuffered, parameters);
            this.baseAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
        }
        else if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
        {
            this.setLevel2(this.myLevel, (int)IN_GAME_MAIN_CAMERA.difficulty, UnityEngine.Random.Range(0, 4));
        }
    }

    [RPC]
    private void setMyTarget(int ID)
    {
        if (ID == -1)
        {
            this.myHero = null;
        }
        PhotonView view = PhotonView.Find(ID);
        if (view != null)
        {
            this.myHero = view.gameObject;
        }
    }

    public void setRoute(GameObject route)
    {
        this.checkPoints = new ArrayList();
        for (int i = 1; i <= 10; i++)
        {
            this.checkPoints.Add(route.transform.Find("r" + i).position);
        }
        this.checkPoints.Add("end");
    }

    private bool simpleHitTestLineAndBall(Vector3 line, Vector3 ball, float R)
    {
        Vector3 rhs = Vector3.Project(ball, line);
        Vector3 vector2 = ball - rhs;
        if (vector2.magnitude > R)
        {
            return false;
        }
        if (Vector3.Dot(line, rhs) < 0f)
        {
            return false;
        }
        if (rhs.sqrMagnitude > line.sqrMagnitude)
        {
            return false;
        }
        return true;
    }

    private void sitdown(bool b = true)
    {
        this.state = TitanState.sit;
        this.playAnimation("sit_down");
        if (b)
        {
            this.getdownTime = UnityEngine.Random.Range((float)10f, (float)30f);
        }
    }

    private void Start()
    {
        this.MultiplayerManager.addTitan(this);
        if (Minimap.instance != null)
        {
            Minimap.instance.TrackGameObjectOnMinimap(base.gameObject, Color.yellow, false, true, Minimap.IconStyle.CIRCLE);
        }
        this.currentCamera = CyanMod.CachingsGM.Find("MainCamera");
        currentCameraT = currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>();
        this.runAnimation = "run_walk";
        this.grabTF = new GameObject();
        this.grabTF.name = "titansTmpGrabTF";
        grabTFT = grabTF.transform;
        this.head = this.headT;
        this.neck = this.tfind("Amarture/Core/Controller_Body/hip/spine/chest/neck");
        this.oldHeadRotation = this.head.rotation;
        if ((IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.MULTIPLAYER) || base.photonView.isMine)
        {
            if (!this.hasSetLevel)
            {
                this.myLevel = UnityEngine.Random.Range((float) 0.7f, (float) 3f);
                if (RCSettings.sizeMode > 0)
                {
                    float sizeLower = RCSettings.sizeLower;
                    float sizeUpper = RCSettings.sizeUpper;
                    this.myLevel = UnityEngine.Random.Range(sizeLower, sizeUpper);
                }
                this.hasSetLevel = true;
            }
            this.spawnPt = this.baseTransform.position;
            this.setmyLevel();
            this.setAbnormalType2(this.abnormalType, false);
            if (this.myHero == null)
            {
                this.findNearestHero2();
            }
            this.controller = base.gameObject.GetComponent<TITAN_CONTROLLER>();
            if (this.nonAI)
            {
                base.StartCoroutine(this.reloadSky());
            }
        }
        if ((this.maxHealth == 0) && (RCSettings.healthMode > 0))
        {
            if (RCSettings.healthMode == 1)
            {
                this.maxHealth = this.currentHealth = UnityEngine.Random.Range(RCSettings.healthLower, RCSettings.healthUpper + 1);
            }
            else if (RCSettings.healthMode == 2)
            {
                this.maxHealth = this.currentHealth = Mathf.Clamp(Mathf.RoundToInt((this.myLevel / 4f) * UnityEngine.Random.Range(RCSettings.healthLower, RCSettings.healthUpper + 1)), RCSettings.healthLower, RCSettings.healthUpper);
            }
        }
        this.lagMax = 150f + (this.myLevel * 3f);
        this.healthTime = Time.time;
        if ((this.currentHealth > 0) && base.photonView.isMine)
        {
            base.photonView.RPC("labelRPC", PhotonTargets.AllBuffered, new object[] { this.currentHealth, this.maxHealth });
        }
        this.hasExplode = false;
        this.colliderEnabled = true;
        this.isHooked = false;
        this.isLook = false;
        this.hasSpawn = true;
    }

    public void suicide()
    {
        this.netDie();
        if (this.nonAI)
        {
            FengGameManagerMKII.instance.sendKillInfo(false, string.Empty, true, (string) PhotonNetwork.player.name2, 0);
        }
        FengGameManagerMKII.instance.needChooseSide = true;
        FengGameManagerMKII.instance.justSuicide = true;
    }

    public void testVisual(bool setCollider)
    {
        if (setCollider)
        {
            foreach (Renderer renderer in base.GetComponentsInChildren<Renderer>())
            {
                renderer.material.color = Color.white;
            }
        }
        else
        {
            foreach (Renderer renderer2 in base.GetComponentsInChildren<Renderer>())
            {
                renderer2.material.color = Color.black;
            }
        }
    }

    [RPC]
    public void titanGetHit(int viewID, int speed)
    {
        PhotonView view = PhotonView.Find(viewID);
        if (view != null)
        {
            Vector3 vector = view.gameObject.transform.position - this.neck.position;
            if (((vector.magnitude < this.lagMax) && !this.hasDie) && ((Time.time - this.healthTime) > 0.2f))
            {
                this.healthTime = Time.time;
                if ((speed >= RCSettings.damageMode) || (this.abnormalType == AbnormalType.TYPE_CRAWLER))
                {
                    this.currentHealth -= speed;
                }
                if (this.maxHealth > 0f)
                {
                    base.photonView.RPC("labelRPC", PhotonTargets.AllBuffered, new object[] { this.currentHealth, this.maxHealth });
                }
                if (this.currentHealth < 0f)
                {
                    if (PhotonNetwork.isMasterClient)
                    {
                        this.OnTitanDie(view);
                    }
                    base.photonView.RPC("netDie", PhotonTargets.OthersBuffered, new object[0]);
                    if (this.grabbedTargetT != null)
                    {
                        this.grabbedTargetT.photonView.RPC("netUngrabbed", PhotonTargets.All, new object[0]);
                    }
                    this.netDie();
                    if (this.nonAI)
                    {
                        FengGameManagerMKII.instance.titanGetKill(view.owner, speed, (string) PhotonNetwork.player.name2);
                    }
                    else
                    {
                        FengGameManagerMKII.instance.titanGetKill(view.owner, speed, base.name);
                    }
                }
                else
                {
                    FengGameManagerMKII.instance.photonView.RPC("netShowDamage", view.owner, new object[] { speed });
                }
            }
        }
    }

    public void toCheckPoint(Vector3 targetPt, float r)
    {
        this.state = TitanState.to_check_point;
        this.targetCheckPt = targetPt;
        this.targetR = r;
        this.crossFade(this.runAnimation, 0.5f);
    }

    public void toPVPCheckPoint(Vector3 targetPt, float r)
    {
        this.state = TitanState.to_pvp_pt;
        this.targetCheckPt = targetPt;
        this.targetR = r;
        this.crossFade(this.runAnimation, 0.5f);
    }

    private void turn(float d)
    {
        if (this.abnormalType == AbnormalType.TYPE_CRAWLER)
        {
            if (d > 0f)
            {
                this.turnAnimation = "crawler_turnaround_R";
            }
            else
            {
                this.turnAnimation = "crawler_turnaround_L";
            }
        }
        else if (d > 0f)
        {
            this.turnAnimation = "turnaround2";
        }
        else
        {
            this.turnAnimation = "turnaround1";
        }
        this.playAnimation(this.turnAnimation);
        this.baseAnimation[this.turnAnimation].time = 0f;
        d = Mathf.Clamp(d, -120f, 120f);
        this.turnDeg = d;
        this.desDeg = base.gameObject.transform.rotation.eulerAngles.y + this.turnDeg;
        this.state = TitanState.turn;
    }

    public void update()
    {
        
    }
    Transform hip;
    GameObject stamina_titan;
    public void update2()
    {
        if (((!IN_GAME_MAIN_CAMERA.isPausing || (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)) && (this.myDifficulty >= 0)) && ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE) || base.photonView.isMine))
        {
            this.explode();
            if (!this.nonAI)
            {
                if ((this.activeRad < 0x7fffffff) && (((this.state == TitanState.idle) || (this.state == TitanState.wander)) || (this.state == TitanState.chase)))
                {
                    if (this.checkPoints.Count > 1)
                    {
                        if (Vector3.Distance((Vector3) this.checkPoints[0], this.baseTransform.position) > this.activeRad)
                        {
                            this.toCheckPoint((Vector3) this.checkPoints[0], 10f);
                        }
                    }
                    else if (Vector3.Distance(this.spawnPt, this.baseTransform.position) > this.activeRad)
                    {
                        this.toCheckPoint(this.spawnPt, 10f);
                    }
                }
                if (this.whoHasTauntMe != null)
                {
                    this.tauntTime -= Time.deltaTime;
                    if (this.tauntTime <= 0f)
                    {
                        this.whoHasTauntMe = null;
                    }
                    this.myHero = this.whoHasTauntMe;
                    if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && PhotonNetwork.isMasterClient)
                    {
                        object[] parameters = new object[] { this.myHero.GetPhotonView().viewID };
                        base.photonView.RPC("setMyTarget", PhotonTargets.Others, parameters);
                    }
                }
            }
            if (this.hasDie)
            {
                this.dieTime += Time.deltaTime;
                if ((this.dieTime > 2f) && !this.hasDieSteam)
                {
                    this.hasDieSteam = true;
                    if ((int)FengGameManagerMKII.settings[285] == 0)
                    {
                        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                        {
                            GameObject obj2 = (GameObject)UnityEngine.Object.Instantiate(Cach.FX_FXtitanDie1 != null ? Cach.FX_FXtitanDie1 : Cach.FX_FXtitanDie1 = (GameObject)Resources.Load("FX/FXtitanDie1"));
                            obj2.transform.position =(hip != null ? hip : hip =  this.baseTransform.Find("Amarture/Core/Controller_Body/hip")).position;
                            obj2.transform.localScale = this.baseTransform.localScale;
                        }
                        else if (base.photonView.isMine)
                        {
                            PhotonNetwork.Instantiate("FX/FXtitanDie1", (hip != null ? hip : hip = this.baseTransform.Find("Amarture/Core/Controller_Body/hip")).position, Quaternion.Euler(-90f, 0f, 0f), 0).transform.localScale = this.baseTransform.localScale;
                        }
                    }
                }
                if (this.dieTime > 5f)
                {

                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                    {
                        if ((int)FengGameManagerMKII.settings[285] == 0)
                        {
                            GameObject obj3 = (GameObject)UnityEngine.Object.Instantiate(Cach.FX_FXtitanDie != null ? Cach.FX_FXtitanDie : Cach.FX_FXtitanDie = (GameObject)Resources.Load("FX/FXtitanDie"));
                            obj3.transform.position = (hip != null ? hip : hip = this.baseTransform.Find("Amarture/Core/Controller_Body/hip")).position;
                            obj3.transform.localScale = this.baseTransform.localScale;
                        }
                        UnityEngine.Object.Destroy(base.gameObject);
                    }
                    else if (base.photonView.isMine)
                    {
                        if ((int)FengGameManagerMKII.settings[285] == 0)
                        {
                            PhotonNetwork.Instantiate("FX/FXtitanDie", (hip != null ? hip : hip = this.baseTransform.Find("Amarture/Core/Controller_Body/hip")).position, Quaternion.Euler(-90f, 0f, 0f), 0).transform.localScale = this.baseTransform.localScale;
                        }
                             PhotonNetwork.Destroy(base.gameObject);
                        this.myDifficulty = -1;
                    }
                }
            }
            else
            {
                if (this.state == TitanState.hit)
                {
                    if (this.hitPause > 0f)
                    {
                        this.hitPause -= Time.deltaTime;
                        if (this.hitPause <= 0f)
                        {
                            this.baseAnimation[this.hitAnimation].speed = 1f;
                            this.hitPause = 0f;
                        }
                    }
                    if (this.baseAnimation[this.hitAnimation].normalizedTime >= 1f)
                    {
                        this.idle(0f);
                    }
                }
                if (!this.nonAI)
                {
                    if (this.myHero == null)
                    {
                        this.findNearestHero2();
                    }
                    if (!((((this.state == TitanState.idle) || (this.state == TitanState.chase)) || (this.state == TitanState.wander)) ? ((this.whoHasTauntMe != null) || (UnityEngine.Random.Range(0, 100) >= 10)) : true))
                    {
                        this.findNearestFacingHero2();
                    }
                    if (this.myHero == null)
                    {
                        this.myDistance = float.MaxValue;
                    }
                    else
                    {
                        this.myDistance = Mathf.Sqrt(((this.myHero.transform.position.x - this.baseTransform.position.x) * (this.myHero.transform.position.x - this.baseTransform.position.x)) + ((this.myHero.transform.position.z - this.baseTransform.position.z) * (this.myHero.transform.position.z - this.baseTransform.position.z)));
                    }
                }
                else
                {
                    if (this.stamina < this.maxStamina)
                    {
                        if (this.baseAnimation.IsPlaying("idle"))
                        {
                            this.stamina += Time.deltaTime * 30f;
                        }
                        if (this.baseAnimation.IsPlaying("crawler_idle"))
                        {
                            this.stamina += Time.deltaTime * 35f;
                        }
                        if (this.baseAnimation.IsPlaying("run_walk"))
                        {
                            this.stamina += Time.deltaTime * 10f;
                        }
                    }
                    if (this.baseAnimation.IsPlaying("run_abnormal_1"))
                    {
                        this.stamina -= Time.deltaTime * 5f;
                    }
                    if (this.baseAnimation.IsPlaying("crawler_run"))
                    {
                        this.stamina -= Time.deltaTime * 15f;
                    }
                    if (this.stamina < 0f)
                    {
                        this.stamina = 0f;
                    }
                    if (!IN_GAME_MAIN_CAMERA.isPausing)
                    {
                        (stamina_titan != null ? stamina_titan : stamina_titan = GameObject.Find("stamina_titan")).transform.localScale = new Vector3(this.stamina, 16f);
                    }
                }
                if (this.state == TitanState.laugh)
                {
                    if (this.baseAnimation["laugh"].normalizedTime >= 1f)
                    {
                        this.idle(2f);
                    }
                }
                else if (this.state != TitanState.idle)
                {
                    if (this.state != TitanState.attack)
                    {
                        if (this.state != TitanState.grab)
                        {
                            if (this.state != TitanState.eat)
                            {
                                if (this.state != TitanState.chase)
                                {
                                    if (this.state != TitanState.wander)
                                    {
                                        if (this.state != TitanState.turn)
                                        {
                                            if (this.state != TitanState.hit_eye)
                                            {
                                                if (this.state != TitanState.to_check_point)
                                                {
                                                    if (this.state != TitanState.to_pvp_pt)
                                                    {
                                                        if (this.state != TitanState.random_run)
                                                        {
                                                            if (this.state != TitanState.down)
                                                            {
                                                                if (this.state != TitanState.sit)
                                                                {
                                                                    if (this.state == TitanState.recover)
                                                                    {
                                                                        this.getdownTime -= Time.deltaTime;
                                                                        if (this.getdownTime <= 0f)
                                                                        {
                                                                            this.idle(0f);
                                                                        }
                                                                        if (this.baseAnimation.IsPlaying("idle_recovery") && (this.baseAnimation["idle_recovery"].normalizedTime >= 1f))
                                                                        {
                                                                            this.idle(0f);
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    this.getdownTime -= Time.deltaTime;
                                                                    this.angle = 0f;
                                                                    this.between2 = 0f;
                                                                    if ((this.myDistance < this.chaseDistance) || (this.whoHasTauntMe != null))
                                                                    {
                                                                        if (this.myDistance < 50f)
                                                                        {
                                                                            this.isAlarm = true;
                                                                        }
                                                                        else
                                                                        {
                                                                            Vector3 vector17 = this.myHero.transform.position - this.baseTransform.position;
                                                                            this.angle = -Mathf.Atan2(vector17.z, vector17.x) * 57.29578f;
                                                                            this.between2 = -Mathf.DeltaAngle(this.angle, this.baseGameObjectTransform.rotation.eulerAngles.y - 90f);
                                                                            if (Mathf.Abs(this.between2) < 100f)
                                                                            {
                                                                                this.isAlarm = true;
                                                                            }
                                                                        }
                                                                    }
                                                                    if (this.baseAnimation.IsPlaying("sit_down") && (this.baseAnimation["sit_down"].normalizedTime >= 1f))
                                                                    {
                                                                        this.playAnimation("sit_idle");
                                                                    }
                                                                    if ((!(((this.getdownTime <= 0f) || this.isAlarm) ? !this.baseAnimation.IsPlaying("sit_idle") : true)) )
                                                                    {
                                                                        if (nonAI)
                                                                        {
                                                                            if (controller.sitdown)
                                                                            {
                                                                                this.crossFade("sit_getup", 0.1f);
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            this.crossFade("sit_getup", 0.1f);
                                                                        }
                                                                    }
                                                                    if (this.baseAnimation.IsPlaying("sit_getup") && (this.baseAnimation["sit_getup"].normalizedTime >= 1f))
                                                                    {
                                                                        this.idle(0f);
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                this.getdownTime -= Time.deltaTime;
                                                                if (this.baseAnimation.IsPlaying("sit_hunt_down") && (this.baseAnimation["sit_hunt_down"].normalizedTime >= 1f))
                                                                {
                                                                    this.playAnimation("sit_idle");
                                                                }
                                                                if (this.getdownTime <= 0f)
                                                                {
                                                                    if (nonAI)
                                                                    {
                                                                        if (controller.sitdown)
                                                                        {
                                                                            this.crossFade("sit_getup", 0.1f);
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        this.crossFade("sit_getup", 0.1f);
                                                                    }
                                                                }
                                                                if (this.baseAnimation.IsPlaying("sit_getup") && (this.baseAnimation["sit_getup"].normalizedTime >= 1f))
                                                                {
                                                                    this.idle(0f);
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            this.random_run_time -= Time.deltaTime;
                                                            if ((Vector3.Distance(this.baseTransform.position, this.targetCheckPt) < this.targetR) || (this.random_run_time <= 0f))
                                                            {
                                                                this.idle(0f);
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (this.myDistance < (this.chaseDistance * 0.7f))
                                                        {
                                                            this.chase();
                                                        }
                                                        if (Vector3.Distance(this.baseTransform.position, this.targetCheckPt) < this.targetR)
                                                        {
                                                            this.idle(0f);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if ((this.checkPoints.Count <= 0) && (this.myDistance < this.attackDistance))
                                                    {
                                                        string decidedAction = string.Empty;
                                                        string[] attackStrategy = this.GetAttackStrategy();
                                                        if (attackStrategy != null)
                                                        {
                                                            decidedAction = attackStrategy[UnityEngine.Random.Range(0, attackStrategy.Length)];
                                                        }
                                                        if (this.executeAttack2(decidedAction))
                                                        {
                                                            return;
                                                        }
                                                    }
                                                    if (Vector3.Distance(this.baseTransform.position, this.targetCheckPt) < this.targetR)
                                                    {
                                                        if (this.checkPoints.Count <= 0)
                                                        {
                                                            this.idle(0f);
                                                        }
                                                        else if (this.checkPoints.Count != 1)
                                                        {
                                                            if (this.checkPoints.Count == 4)
                                                            {
                                                              cext.mess("*WARNING!* An abnormal titan is approaching the north gate!");
                                                            }
                                                            Vector3 vector16 = (Vector3) this.checkPoints[0];
                                                            this.targetCheckPt = vector16;
                                                            this.checkPoints.RemoveAt(0);
                                                        }
                                                        else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.BOSS_FIGHT_CT)
                                                        {
                                                            this.MultiplayerManager.gameLose2();
                                                            this.checkPoints = new ArrayList();
                                                            this.idle(0f);
                                                        }
                                                    }
                                                }
                                            }
                                            else if (this.baseAnimation.IsPlaying("sit_hit_eye") && (this.baseAnimation["sit_hit_eye"].normalizedTime >= 1f))
                                            {
                                                this.remainSitdown();
                                            }
                                            else if (this.baseAnimation.IsPlaying("hit_eye") && (this.baseAnimation["hit_eye"].normalizedTime >= 1f))
                                            {
                                                if (this.nonAI)
                                                {
                                                    this.idle(0f);
                                                }
                                                else
                                                {
                                                    this.attack2("combo_1");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            this.baseGameObjectTransform.rotation = Quaternion.Lerp(this.baseGameObjectTransform.rotation, Quaternion.Euler(0f, this.desDeg, 0f), (Time.deltaTime * Mathf.Abs(this.turnDeg)) * 0.015f);
                                            if (this.baseAnimation[this.turnAnimation].normalizedTime >= 1f)
                                            {
                                                this.idle(0f);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        float current = 0f;
                                        float f = 0f;
                                        if ((this.myDistance < this.chaseDistance) || (this.whoHasTauntMe != null))
                                        {
                                            Vector3 vector15 = this.myHero.transform.position - this.baseTransform.position;
                                            current = -Mathf.Atan2(vector15.z, vector15.x) * 57.29578f;
                                            f = -Mathf.DeltaAngle(current, this.baseGameObjectTransform.rotation.eulerAngles.y - 90f);
                                            if (this.isAlarm || (Mathf.Abs(f) < 90f))
                                            {
                                                this.chase();
                                                return;
                                            }
                                            if (!this.isAlarm && (this.myDistance < (this.chaseDistance * 0.1f)))
                                            {
                                                this.chase();
                                                return;
                                            }
                                        }
                                        if (UnityEngine.Random.Range((float) 0f, (float) 1f) < 0.01f)
                                        {
                                            this.idle(0f);
                                        }
                                    }
                                }
                                else if (this.myHero == null)
                                {
                                    this.idle(0f);
                                }
                                else if (!this.longRangeAttackCheck2())
                                {
                                    if (((IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE) && (this.PVPfromCheckPt != null)) && (this.myDistance > this.chaseDistance))
                                    {
                                        this.idle(0f);
                                    }
                                    else if (this.abnormalType != AbnormalType.TYPE_CRAWLER)
                                    {
                                        if (((this.abnormalType == AbnormalType.TYPE_JUMPER) && (((this.myDistance > this.attackDistance) && (this.myHero.transform.position.y > (this.head.position.y + (4f * this.myLevel)))) || (this.myHero.transform.position.y > (this.head.position.y + (4f * this.myLevel))))) && (Vector3.Distance(this.baseTransform.position, this.myHero.transform.position) < (1.5f * this.myHero.transform.position.y)))
                                        {
                                            this.attack2("jumper_0");
                                        }
                                        else if (this.myDistance < this.attackDistance)
                                        {
                                            this.idle(UnityEngine.Random.Range((float) 0.05f, (float) 0.2f));
                                        }
                                    }
                                    else
                                    {
                                        Vector3 vector13 = this.myHero.transform.position - this.baseTransform.position;
                                        float num24 = -Mathf.Atan2(vector13.z, vector13.x) * 57.29578f;
                                        float num25 = -Mathf.DeltaAngle(num24, this.baseGameObjectTransform.rotation.eulerAngles.y - 90f);
                                        if ((((this.myDistance < (this.attackDistance * 3f)) && (UnityEngine.Random.Range((float) 0f, (float) 1f) < 0.1f)) && ((Mathf.Abs(num25) < 90f) && (this.myHero.transform.position.y < (this.neck.position.y + (30f * this.myLevel))))) && (this.myHero.transform.position.y > (this.neck.position.y + (10f * this.myLevel))))
                                        {
                                            this.attack2("crawler_jump_0");
                                        }
                                        else
                                        {
                                            GameObject obj = this.checkIfHitCrawlerMouth(this.head, 2.2f);
                                            if (obj != null)
                                            {
                                                Vector3 position = this.chest.position;
                                                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                                                {
                                                    obj.GetComponent<HERO>().die((Vector3)(((obj.transform.position - position) * 15f) * this.myLevel), false);
                                                }
                                                else if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && base.photonView.isMine)
                                                {
                                                    TITAN_EREN te = obj.GetComponent<TITAN_EREN>();
                                                    HERO her = null;
                                                    if (te != null)
                                                    {
                                                        te.hitByTitan();
                                                    }
                                                    else if (!(her = obj.GetComponent<HERO>()).HasDied())
                                                    {
                                                        her.markDie();
                                                        object[] objArray8 = new object[] { (Vector3)(((her.transform.position - position) * 15f) * this.myLevel), true, !this.nonAI ? -1 : base.photonView.viewID, base.name, true };
                                                       her.photonView.RPC("netDie", PhotonTargets.All, objArray8);
                                                    }
                                                }
                                            }
                                            if ((this.myDistance < this.attackDistance) && (UnityEngine.Random.Range((float) 0f, (float) 1f) < 0.02f))
                                            {
                                                this.idle(UnityEngine.Random.Range((float) 0.05f, (float) 0.2f));
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (!this.attacked && (this.baseAnimation[this.attackAnimation].normalizedTime >= 0.48f))
                                {
                                    this.attacked = true;
                                    this.justEatHero(this.grabbedTargetT, this.currentGrabHand);
                                }
                                if (this.baseAnimation[this.attackAnimation].normalizedTime >= 1f)
                                {
                                    this.idle(0f);
                                }
                            }
                        }
                        else
                        {
                            if (((this.baseAnimation["grab_" + this.attackAnimation].normalizedTime >= this.attackCheckTimeA) && (this.baseAnimation["grab_" + this.attackAnimation].normalizedTime <= this.attackCheckTimeB)) && (this.grabbedTargetT == null))
                            {
                                HERO hero = this.checkIfHitHand(this.currentGrabHand);

                                if (hero != null)
                                {
                                    if (this.isGrabHandLeft)
                                    {
                                        this.eatSetL(hero);
                                        this.grabbedTargetT = hero;
                                    }
                                    else
                                    {
                                        this.eatSet(hero);
                                        this.grabbedTargetT = hero;
                                    }
                                }
                            }
                            if (this.baseAnimation["grab_" + this.attackAnimation].normalizedTime >= 1f)
                            {
                                if (this.grabbedTargetT != null)
                                {
                                    this.eat();
                                }
                                else
                                {
                                    this.idle(UnityEngine.Random.Range((float) (this.attackWait - 1f), (float) 2f));
                                }
                            }
                        }
                    }
                    else
                    {
                        if (this.attackAnimation == "combo")
                        {
                            if (this.nonAI)
                            {
                                if (this.controller.isAttackDown)
                                {
                                    this.nonAIcombo = true;
                                }
                                if (!this.nonAIcombo && (this.baseAnimation["attack_" + this.attackAnimation].normalizedTime >= 0.385f))
                                {
                                    this.idle(0f);
                                    return;
                                }
                            }
                            if ((this.baseAnimation["attack_" + this.attackAnimation].normalizedTime >= 0.11f) && (this.baseAnimation["attack_" + this.attackAnimation].normalizedTime <= 0.16f))
                            {
                                HERO hero = this.checkIfHitHand(this.hand_R);
                                if (hero != null)
                                {
                                    Vector3 vector = this.chest.position;
                                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                                    {
                                        hero.die((Vector3)(((hero.transform.position - vector) * 15f) * this.myLevel), false);
                                    }
                                    else if (((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && base.photonView.isMine) && !hero.HasDied())
                                    {
                                        hero.markDie();
                                        object[] objArray2 = new object[] { (Vector3)(((hero.transform.position - vector) * 15f) * this.myLevel), false, !this.nonAI ? -1 : base.photonView.viewID, base.name, true };
                                        hero.photonView.RPC("netDie", PhotonTargets.All, objArray2);
                                    }
                                }
                            }
                            if ((this.baseAnimation["attack_" + this.attackAnimation].normalizedTime >= 0.27f) && (this.baseAnimation["attack_" + this.attackAnimation].normalizedTime <= 0.32f))
                            {
                                HERO hero = this.checkIfHitHand(this.hand_L);
                                if (hero != null)
                                {
                                    Vector3 vector2 = this.chest.position;
                                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                                    {
                                        hero.die((Vector3)(((hero.transform.position - vector2) * 15f) * this.myLevel), false);
                                    }
                                    else if (((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && base.photonView.isMine) && !hero.HasDied())
                                    {
                                        hero.markDie();
                                        object[] objArray3 = new object[] { (Vector3)(((hero.transform.position - vector2) * 15f) * this.myLevel), false, !this.nonAI ? -1 : base.photonView.viewID, base.name, true };
                                        hero.photonView.RPC("netDie", PhotonTargets.All, objArray3);
                                    }
                                }
                            }
                        }
                        if (((this.attackCheckTimeA != 0f) && (this.baseAnimation["attack_" + this.attackAnimation].normalizedTime >= this.attackCheckTimeA)) && (this.baseAnimation["attack_" + this.attackAnimation].normalizedTime <= this.attackCheckTimeB))
                        {
                            if (this.leftHandAttack)
                            {
                                HERO hero = this.checkIfHitHand(this.hand_L);
                                if (hero != null)
                                {
                                    Vector3 vector3 = this.chest.position;
                                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                                    {
                                        hero.die((Vector3)(((hero.transform.position - vector3) * 15f) * this.myLevel), false);
                                    }
                                    else if (((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && base.photonView.isMine) && !hero.HasDied())
                                    {
                                        hero.markDie();
                                        object[] objArray4 = new object[] { (Vector3)(((hero.transform.position - vector3) * 15f) * this.myLevel), false, !this.nonAI ? -1 : base.photonView.viewID, base.name, true };
                                        hero.photonView.RPC("netDie", PhotonTargets.All, objArray4);
                                    }
                                }
                            }
                            else
                            {
                                HERO hero = this.checkIfHitHand(this.hand_R);
                                if (hero != null)
                                {
                                    Vector3 vector4 = this.chest.position;
                                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                                    {
                                        hero.die((Vector3)(((hero.transform.position - vector4) * 15f) * this.myLevel), false);
                                    }
                                    else if (((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && base.photonView.isMine) && !hero.HasDied())
                                    {
                                        hero.markDie();
                                        object[] objArray5 = new object[] { (Vector3)(((hero.transform.position - vector4) * 15f) * this.myLevel), false, !this.nonAI ? -1 : base.photonView.viewID, base.name, true };
                                        hero.photonView.RPC("netDie", PhotonTargets.All, objArray5);
                                    }
                                }
                            }
                        }
                        if ((!this.attacked && (this.attackCheckTime != 0f)) && (this.baseAnimation["attack_" + this.attackAnimation].normalizedTime >= this.attackCheckTime))
                        {
                            GameObject obj8;
                            this.attacked = true;
                            this.fxPosition = this.tfind("ap_" + this.attackAnimation).position;
                            if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && base.photonView.isMine)
                            {
                                obj8 = PhotonNetwork.Instantiate("FX/" + this.fxName, this.fxPosition, this.fxRotation, 0);
                            }
                            else
                            {
                                obj8 = (GameObject) UnityEngine.Object.Instantiate(Resources.Load("FX/" + this.fxName), this.fxPosition, this.fxRotation);
                            }
                            EnemyfxIDcontainer fx = obj8.GetComponent<EnemyfxIDcontainer>();
                            if (this.nonAI)
                            {
                                obj8.transform.localScale = (Vector3) (this.baseTransform.localScale * 1.5f);
                                if (fx != null)
                                {
                                    fx.myOwnerViewID = base.photonView.viewID;
                                }
                            }
                            else
                            {
                                obj8.transform.localScale = this.baseTransform.localScale;
                            }
                            if (fx != null)
                            {
                                fx.titanName = base.name;
                            }
                            float b = 1f - (Vector3.Distance(this.currentCamera.transform.position, obj8.transform.position) * 0.05f);
                            b = Mathf.Min(1f, b);
                            this.currentCameraT.startShake(b, b, 0.95f);
                        }
                        if (this.attackAnimation == "throw")
                        {
                            if (!this.attacked && (this.baseAnimation["attack_" + this.attackAnimation].normalizedTime >= 0.11f))
                            {
                                this.attacked = true;
                                Transform transform = this.hand_R;
                                if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && base.photonView.isMine)
                                {
                                    this.throwRock = PhotonNetwork.Instantiate("FX/rockThrow", transform.position, transform.rotation, 0);
                                }
                                else
                                {
                                    this.throwRock = (GameObject)UnityEngine.Object.Instantiate(Cach.rockThrow != null ? Cach.rockThrow : Cach.rockThrow = (GameObject)Resources.Load("FX/rockThrow"), transform.position, transform.rotation);
                                }
                                throwRockTT = throwRock.transform;
                                throwRockTT.localScale = this.baseTransform.localScale;
                                throwRockTT.position -= (Vector3)((throwRockTT.forward * 2.5f) * this.myLevel);
                                EnemyfxIDcontainer fx = this.throwRock.GetComponent<EnemyfxIDcontainer>();
                                if(fx  != null)
                                {
                                    if (this.nonAI)
                                    {
                                        fx.myOwnerViewID = base.photonView.viewID;
                                    }
                                  fx.titanName = base.name;
                                }
                                throwRockTT.parent = transform;
                                throwRockT = throwRock.GetComponent<RockThrow>();
                                if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && base.photonView.isMine)
                                {
                                    object[] objArray6 = new object[] { base.photonView.viewID, this.baseTransform.localScale, throwRockTT.localPosition, this.myLevel };
                                    throwRockT.photonView.RPC("initRPC", PhotonTargets.Others, objArray6);
                                }
                            }
                            if (this.baseAnimation["attack_" + this.attackAnimation].normalizedTime >= 0.11f)
                            {
                                float y = Mathf.Atan2(this.myHero.transform.position.x - this.baseTransform.position.x, this.myHero.transform.position.z - this.baseTransform.position.z) * 57.29578f;
                                this.baseGameObjectTransform.rotation = Quaternion.Euler(0f, y, 0f);
                            }
                            if ((this.throwRock != null) && (this.baseAnimation["attack_" + this.attackAnimation].normalizedTime >= 0.62f))
                            {
                                Vector3 vector5;
                                float num3 = 1f;
                                float num4 = -20f;
                                if (this.myHero != null)
                                {
                                    vector5 = ((Vector3)((this.myHero.transform.position - throwRockTT.position) / num3)) + this.myHero.rigidbody.velocity;
                                    float num5 = this.myHero.transform.position.y + (2f * this.myLevel);
                                    float num6 = num5 - throwRockTT.position.y;
                                    vector5 = new Vector3(vector5.x, (num6 / num3) - ((0.5f * num4) * num3), vector5.z);
                                }
                                else
                                {
                                    vector5 = (Vector3) ((this.baseTransform.forward * 60f) + (Vector3.up * 10f));
                                }
                                throwRockT.launch(vector5);
                                throwRockTT.parent = null;
                                this.throwRock = null;
                            }
                        }
                        if ((this.attackAnimation != "jumper_0") && (this.attackAnimation != "crawler_jump_0"))
                        {
                            if ((this.attackAnimation != "jumper_1") && (this.attackAnimation != "crawler_jump_1"))
                            {
                                if ((this.attackAnimation != "jumper_2") && (this.attackAnimation != "crawler_jump_2"))
                                {
                                    if (!this.baseAnimation.IsPlaying("tired"))
                                    {
                                        if (this.baseAnimation["attack_" + this.attackAnimation].normalizedTime >= (1f + this.attackEndWait))
                                        {
                                            if (this.nextAttackAnimation != null)
                                            {
                                                this.attack2(this.nextAttackAnimation);
                                            }
                                            else if ((this.attackAnimation == "quick_turn_l") || (this.attackAnimation == "quick_turn_r"))
                                            {
                                                this.baseTransform.rotation = Quaternion.Euler(this.baseTransform.rotation.eulerAngles.x, this.baseTransform.rotation.eulerAngles.y + 180f, this.baseTransform.rotation.eulerAngles.z);
                                                this.idle(UnityEngine.Random.Range((float) 0.5f, (float) 1f));
                                                this.playAnimation("idle");
                                            }
                                            else if ((this.abnormalType == AbnormalType.TYPE_I) || (this.abnormalType == AbnormalType.TYPE_JUMPER))
                                            {
                                                this.attackCount++;
                                                if ((this.attackCount > 3) && (this.attackAnimation == "abnormal_getup"))
                                                {
                                                    this.attackCount = 0;
                                                    this.crossFade("tired", 0.5f);
                                                }
                                                else
                                                {
                                                    this.idle(UnityEngine.Random.Range((float) (this.attackWait - 1f), (float) 3f));
                                                }
                                            }
                                            else
                                            {
                                                this.idle(UnityEngine.Random.Range((float) (this.attackWait - 1f), (float) 3f));
                                            }
                                        }
                                    }
                                    else if (this.baseAnimation["tired"].normalizedTime >= (1f + Mathf.Max((float) (this.attackEndWait * 2f), (float) 3f)))
                                    {
                                        this.idle(UnityEngine.Random.Range((float) (this.attackWait - 1f), (float) 3f));
                                    }
                                }
                                else if (this.baseAnimation["attack_" + this.attackAnimation].normalizedTime >= 1f)
                                {
                                    this.idle(0f);
                                }
                            }
                            else if ((this.baseAnimation["attack_" + this.attackAnimation].normalizedTime >= 1f) && this.grounded)
                            {
                                GameObject obj10;
                                if (this.abnormalType == AbnormalType.TYPE_CRAWLER)
                                {
                                    this.attackAnimation = "crawler_jump_2";
                                }
                                else
                                {
                                    this.attackAnimation = "jumper_2";
                                }
                                this.crossFade("attack_" + this.attackAnimation, 0.1f);
                                this.fxPosition = this.baseTransform.position;
                                if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && base.photonView.isMine)
                                {
                                    obj10 = PhotonNetwork.Instantiate("FX/boom2", this.fxPosition, this.fxRotation, 0);
                                }
                                else
                                {
                                    obj10 = (GameObject)UnityEngine.Object.Instantiate(Cach.boom2 != null ? Cach.boom2 : Cach.boom2 = (GameObject)Resources.Load("FX/boom2"), this.fxPosition, this.fxRotation);
                                }
                                obj10.transform.localScale = (Vector3) (this.baseTransform.localScale * 1.6f);
                                float num23 = 1f - (Vector3.Distance(this.currentCamera.transform.position, obj10.transform.position) * 0.05f);
                                num23 = Mathf.Min(1f, num23);
                                this.currentCameraT.startShake(num23, num23, 0.95f);
                            }
                        }
                        else
                        {
                            if (!this.attacked)
                            {
                                if (this.baseAnimation["attack_" + this.attackAnimation].normalizedTime >= 0.68f)
                                {
                                    this.attacked = true;
                                    if ((this.myHero == null) || this.nonAI)
                                    {
                                        float num7 = 120f;
                                        Vector3 vector6 = (Vector3) ((this.baseTransform.forward * this.speed) + (Vector3.up * num7));
                                        if (this.nonAI && (this.abnormalType == AbnormalType.TYPE_CRAWLER))
                                        {
                                            num7 = 100f;
                                            float a = this.speed * 2.5f;
                                            a = Mathf.Min(a, 100f);
                                            vector6 = (Vector3) ((this.baseTransform.forward * a) + (Vector3.up * num7));
                                        }
                                        this.baseRigidBody.velocity = vector6;
                                    }
                                    else
                                    {
                                        float num9;
                                        float num10 = this.myHero.rigidbody.velocity.y;
                                        float num11 = -20f;
                                        float gravity = this.gravity;
                                        float num13 = this.neck.position.y;
                                        float num14 = (num11 - gravity) * 0.5f;
                                        float num15 = num10;
                                        float num16 = this.myHero.transform.position.y - num13;
                                        float num17 = Mathf.Abs((float) ((Mathf.Sqrt((num15 * num15) - ((4f * num14) * num16)) - num15) / (2f * num14)));
                                        Vector3 vector7 = (Vector3) ((this.myHero.transform.position + (this.myHero.rigidbody.velocity * num17)) + ((((Vector3.up * 0.5f) * num11) * num17) * num17));
                                        float num18 = vector7.y;
                                        if ((num16 < 0f) || ((num18 - num13) < 0f))
                                        {
                                            num9 = 60f;
                                            float num19 = this.speed * 2.5f;
                                            num19 = Mathf.Min(num19, 100f);
                                            Vector3 vector8 = (Vector3) ((this.baseTransform.forward * num19) + (Vector3.up * num9));
                                            this.baseRigidBody.velocity = vector8;
                                            return;
                                        }
                                        float num20 = num18 - num13;
                                        float num21 = Mathf.Sqrt((2f * num20) / this.gravity);
                                        num9 = this.gravity * num21;
                                        num9 = Mathf.Max(30f, num9);
                                        Vector3 vector9 = (Vector3) ((vector7 - this.baseTransform.position) / num17);
                                        this.abnorma_jump_bite_horizon_v = new Vector3(vector9.x, 0f, vector9.z);
                                        Vector3 velocity = this.baseRigidBody.velocity;
                                        Vector3 force = new Vector3(this.abnorma_jump_bite_horizon_v.x, velocity.y, this.abnorma_jump_bite_horizon_v.z) - velocity;
                                        this.baseRigidBody.AddForce(force, ForceMode.VelocityChange);
                                        this.baseRigidBody.AddForce((Vector3) (Vector3.up * num9), ForceMode.VelocityChange);
                                        float num22 = Vector2.Angle(new Vector2(this.baseTransform.position.x, this.baseTransform.position.z), new Vector2(this.myHero.transform.position.x, this.myHero.transform.position.z));
                                        num22 = Mathf.Atan2(this.myHero.transform.position.x - this.baseTransform.position.x, this.myHero.transform.position.z - this.baseTransform.position.z) * 57.29578f;
                                        this.baseGameObjectTransform.rotation = Quaternion.Euler(0f, num22, 0f);
                                    }
                                }
                                else
                                {
                                    this.baseRigidBody.velocity = Vector3.zero;
                                }
                            }
                            if (this.baseAnimation["attack_" + this.attackAnimation].normalizedTime >= 1f)
                            {
                                HERO hero = this.checkIfHitHead(this.head, 3f);
                                if (hero != null)
                                {
                                    Vector3 vector12 = this.chest.position;
                                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                                    {
                                        hero.die((Vector3)(((hero.transform.position - vector12) * 15f) * this.myLevel), false);
                                    }
                                    else if (((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && base.photonView.isMine) && !hero.HasDied())
                                    {
                                        hero.markDie();
                                        object[] objArray7 = new object[] { (Vector3)(((hero.transform.position - vector12) * 15f) * this.myLevel), true, !this.nonAI ? -1 : base.photonView.viewID, base.name, true };
                                        hero.photonView.RPC("netDie", PhotonTargets.All, objArray7);
                                    }
                                    if (this.abnormalType == AbnormalType.TYPE_CRAWLER)
                                    {
                                        this.attackAnimation = "crawler_jump_1";
                                    }
                                    else
                                    {
                                        this.attackAnimation = "jumper_1";
                                    }
                                    this.playAnimation("attack_" + this.attackAnimation);
                                }
                                if (((Mathf.Abs(this.baseRigidBody.velocity.y) < 0.5f) || (this.baseRigidBody.velocity.y < 0f)) || this.IsGrounded())
                                {
                                    if (this.abnormalType == AbnormalType.TYPE_CRAWLER)
                                    {
                                        this.attackAnimation = "crawler_jump_1";
                                    }
                                    else
                                    {
                                        this.attackAnimation = "jumper_1";
                                    }
                                    this.playAnimation("attack_" + this.attackAnimation);
                                }
                            }
                        }
                    }
                }
                else if (this.nonAI)
                {
                    if (!IN_GAME_MAIN_CAMERA.isPausing)
                    {
                        this.pt();
                        if (this.abnormalType != AbnormalType.TYPE_CRAWLER)
                        {
                            if (this.controller.isAttackDown && (this.stamina > 25f))
                            {
                                this.stamina -= 25f;
                                this.attack2("combo_1");
                            }
                            else if (this.controller.isAttackIIDown && (this.stamina > 50f))
                            {
                                this.stamina -= 50f;
                                this.attack2("abnormal_jump");
                            }
                            else if (this.controller.isJumpDown && (this.stamina > 15f))
                            {
                                this.stamina -= 15f;
                                this.attack2("jumper_0");
                            }
                        }
                        else if (this.controller.isAttackDown && (this.stamina > 40f))
                        {
                            this.stamina -= 40f;
                            this.attack2("crawler_jump_0");
                        }
                        if (this.controller.isSuicide)
                        {
                            this.suicide();
                        }
                    }
                }
                else if (this.sbtime > 0f)
                {
                    this.sbtime -= Time.deltaTime;
                }
                else
                {
                    if (!this.isAlarm)
                    {
                        if (((this.abnormalType != AbnormalType.TYPE_PUNK) && (this.abnormalType != AbnormalType.TYPE_CRAWLER)) && (UnityEngine.Random.Range((float) 0f, (float) 1f) < 0.005f))
                        {
                            this.sitdown();
                            return;
                        }
                        if (UnityEngine.Random.Range((float) 0f, (float) 1f) < 0.02f)
                        {
                            this.wander(0f);
                            return;
                        }
                        if (UnityEngine.Random.Range((float) 0f, (float) 1f) < 0.01f)
                        {
                            this.turn((float) UnityEngine.Random.Range(30, 120));
                            return;
                        }
                        if (UnityEngine.Random.Range((float) 0f, (float) 1f) < 0.01f)
                        {
                            this.turn((float) UnityEngine.Random.Range(-30, -120));
                            return;
                        }
                    }
                    this.angle = 0f;
                    this.between2 = 0f;
                    if ((this.myDistance < this.chaseDistance) || (this.whoHasTauntMe != null))
                    {
                        Vector3 vector18 = this.myHero.transform.position - this.baseTransform.position;
                        this.angle = -Mathf.Atan2(vector18.z, vector18.x) * 57.29578f;
                        this.between2 = -Mathf.DeltaAngle(this.angle, this.baseGameObjectTransform.rotation.eulerAngles.y - 90f);
                        if (this.myDistance >= this.attackDistance)
                        {
                            if (this.isAlarm || (Mathf.Abs(this.between2) < 90f))
                            {
                                this.chase();
                                return;
                            }
                            if (!this.isAlarm && (this.myDistance < (this.chaseDistance * 0.1f)))
                            {
                                this.chase();
                                return;
                            }
                        }
                    }
                    if (!this.longRangeAttackCheck2())
                    {
                        if (this.myDistance < this.chaseDistance)
                        {
                            if (((this.abnormalType == AbnormalType.TYPE_JUMPER) && ((this.myDistance > this.attackDistance) || (this.myHero.transform.position.y > (this.head.position.y + (4f * this.myLevel))))) && ((Mathf.Abs(this.between2) < 120f) && (Vector3.Distance(this.baseTransform.position, this.myHero.transform.position) < (1.5f * this.myHero.transform.position.y))))
                            {
                                this.attack2("jumper_0");
                                return;
                            }
                            if ((((this.abnormalType == AbnormalType.TYPE_CRAWLER) && (this.myDistance < (this.attackDistance * 3f))) && ((Mathf.Abs(this.between2) < 90f) && (this.myHero.transform.position.y < (this.neck.position.y + (30f * this.myLevel))))) && (this.myHero.transform.position.y > (this.neck.position.y + (10f * this.myLevel))))
                            {
                                this.attack2("crawler_jump_0");
                                return;
                            }
                        }
                        if (((this.abnormalType == AbnormalType.TYPE_PUNK) && (this.myDistance < 90f)) && (Mathf.Abs(this.between2) > 90f))
                        {
                            if (UnityEngine.Random.Range((float) 0f, (float) 1f) < 0.4f)
                            {
                                this.randomRun(this.baseTransform.position + new Vector3(UnityEngine.Random.Range((float) -50f, (float) 50f), UnityEngine.Random.Range((float) -50f, (float) 50f), UnityEngine.Random.Range((float) -50f, (float) 50f)), 10f);
                            }
                            if (UnityEngine.Random.Range((float) 0f, (float) 1f) < 0.2f)
                            {
                                this.recover();
                            }
                            else if (UnityEngine.Random.Range(0, 2) == 0)
                            {
                                this.attack2("quick_turn_l");
                            }
                            else
                            {
                                this.attack2("quick_turn_r");
                            }
                        }
                        else
                        {
                            if (this.myDistance < this.attackDistance)
                            {
                                if (this.abnormalType == AbnormalType.TYPE_CRAWLER)
                                {
                                    if (((this.myHero.transform.position.y + 3f) <= (this.neck.position.y + (20f * this.myLevel))) && (UnityEngine.Random.Range((float) 0f, (float) 1f) < 0.1f))
                                    {
                                        this.chase();
                                    }
                                    return;
                                }
                                string str2 = string.Empty;
                                string[] strArray2 = this.GetAttackStrategy();
                                if (strArray2 != null)
                                {
                                    str2 = strArray2[UnityEngine.Random.Range(0, strArray2.Length)];
                                }
                                if (!(((this.abnormalType == AbnormalType.TYPE_JUMPER) || (this.abnormalType == AbnormalType.TYPE_I)) ? (Mathf.Abs(this.between2) <= 40f) : true))
                                {
                                    if ((str2.Contains("grab") || str2.Contains("kick")) || (str2.Contains("slap") || str2.Contains("bite")))
                                    {
                                        if (UnityEngine.Random.Range(0, 100) < 30)
                                        {
                                            this.turn(this.between2);
                                            return;
                                        }
                                    }
                                    else if (UnityEngine.Random.Range(0, 100) < 90)
                                    {
                                        this.turn(this.between2);
                                        return;
                                    }
                                }
                                if (this.executeAttack2(str2))
                                {
                                    return;
                                }
                                if (this.abnormalType == AbnormalType.NORMAL)
                                {
                                    if ((UnityEngine.Random.Range(0, 100) < 30) && (Mathf.Abs(this.between2) > 45f))
                                    {
                                        this.turn(this.between2);
                                        return;
                                    }
                                }
                                else if (Mathf.Abs(this.between2) > 45f)
                                {
                                    this.turn(this.between2);
                                    return;
                                }
                            }
                            if (this.PVPfromCheckPt != null)
                            {
                                if (this.PVPfromCheckPt.state == CheckPointState.Titan)
                                {
                                    GameObject chkPtPrevious;
                                    if (UnityEngine.Random.Range(0, 100) <= 0x30)
                                    {
                                        chkPtPrevious = this.PVPfromCheckPt.chkPtPrevious;
                                        if ((chkPtPrevious != null) && ((chkPtPrevious.GetComponent<PVPcheckPoint>().state != CheckPointState.Titan) || (UnityEngine.Random.Range(0, 100) < 5)))
                                        {
                                            this.toPVPCheckPoint(chkPtPrevious.transform.position, (float) (5 + UnityEngine.Random.Range(0, 10)));
                                            this.PVPfromCheckPt = chkPtPrevious.GetComponent<PVPcheckPoint>();
                                        }
                                    }
                                    else
                                    {
                                        chkPtPrevious = this.PVPfromCheckPt.chkPtNext;
                                        if ((chkPtPrevious != null) && ((chkPtPrevious.GetComponent<PVPcheckPoint>().state != CheckPointState.Titan) || (UnityEngine.Random.Range(0, 100) < 20)))
                                        {
                                            this.toPVPCheckPoint(chkPtPrevious.transform.position, (float) (5 + UnityEngine.Random.Range(0, 10)));
                                            this.PVPfromCheckPt = chkPtPrevious.GetComponent<PVPcheckPoint>();
                                        }
                                    }
                                }
                                else
                                {
                                    this.toPVPCheckPoint(this.PVPfromCheckPt.transform.position, (float) (5 + UnityEngine.Random.Range(0, 10)));
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void updateCollider()
    {
        if (this.colliderEnabled)
        {
            if ((!this.isHooked && !this.myTitanTrigger.isCollide) && !this.isLook)
            {
                foreach (Collider collider in this.baseColliders)
                {
                    if (collider != null)
                    {
                        collider.enabled = false;
                    }
                }
                this.colliderEnabled = false;
            }
        }
        else if ((this.isHooked || this.myTitanTrigger.isCollide) || this.isLook)
        {
            foreach (Collider collider2 in this.baseColliders)
            {
                if (collider2 != null)
                {
                    collider2.enabled = true;
                }
            }
            this.colliderEnabled = true;
        }
    }

    public void updateLabel()
    {
        if ((this.healthLabel != null) && healthLabelT.isVisible)
        {
            this.healthLabel.transform.LookAt(((Vector3) (2f * this.healthLabel.transform.position)) - Camera.main.transform.position);
        }
        if (label_size != null) 
        {
            label_size.transform.LookAt(((Vector3)(2f * label_size.transform.position)) - Camera.main.transform.position);
            label_size.text = "size:" + myLevel.ToString("F") + "\n" + "speed:" +speed.ToString("F") + "\n" + abnormalType;
        }
    }

    private void wander(float sbtime = 0f)
    {
        this.state = TitanState.wander;
        this.crossFade(this.runAnimation, 0.5f);
    }


}

