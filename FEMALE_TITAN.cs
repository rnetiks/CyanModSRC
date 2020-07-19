using Photon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using CyanMod;

public class FEMALE_TITAN : Photon.MonoBehaviour
{
    private Vector3 abnorma_jump_bite_horizon_v;
    public int AnkleLHP = 200;
    private int AnkleLHPMAX = 200;
    public int AnkleRHP = 200;
    private int AnkleRHPMAX = 200;
    private string attackAnimation;
    private float attackCheckTime;
    private float attackCheckTimeA;
    private float attackCheckTimeB;
    private bool attackChkOnce;
    public float attackDistance = 13f;
    private bool attacked;
    public float attackWait = 1f;
    private float attention = 10f;
    public GameObject bottomObject;
    public float chaseDistance = 80f;
    private Transform checkHitCapsuleEnd;
    private Vector3 checkHitCapsuleEndOld;
    private float checkHitCapsuleR;
    private Transform checkHitCapsuleStart;
    public GameObject currentCamera;
    private Transform currentGrabHand;
    private float desDeg;
    private float dieTime;
    private GameObject eren;
    [CompilerGenerated]
    public static Dictionary<string, int> f__switchsmap1;
    [CompilerGenerated]
    public static Dictionary<string, int> f__switchSmap1;
    [CompilerGenerated]
    public static Dictionary<string, int> f__switchsmap2;
    [CompilerGenerated]
    public static Dictionary<string, int> f__switchSmap2;
    [CompilerGenerated]
    public static Dictionary<string, int> f__switchsmap3;
    [CompilerGenerated]
    public static Dictionary<string, int> f__switchSmap3;
    private string fxName;
    private Vector3 fxPosition;
    private Quaternion fxRotation;
    private GameObject grabbedTarget;
    public GameObject grabTF;
    private float gravity = 120f;
    private bool grounded;
    public bool hasDie;
    private bool hasDieSteam;
    public bool hasspawn;
    public GameObject healthLabel;
    public float healthTime;
    public string hitAnimation;
    private bool isAttackMoveByCore;
    private bool isGrabHandLeft;
    public float lagMax;
    public int maxHealth;
    public float maxVelocityChange = 80f;
    public static float minusDistance = 99999f;
    public static GameObject minusDistanceEnemy;
    public float myDistance;
    public GameObject myHero;
    public int NapeArmor = 0x3e8;
    private bool needFreshCorePosition;
    private string nextAttackAnimation;
    private Vector3 oldCorePosition;
    private float sbtime;
    public float size;
    public float speed = 80f;
    private bool startJump;
    private string state = "idle";
    private int stepSoundPhase = 2;
    private float tauntTime;
    private string turnAnimation;
    private float turnDeg;
    private GameObject whoHasTauntMe;
    AudioSource snd_titan_foot;
    public int damage;
    Transform baseT;
    Animation baseA;
    GameObject baseG;
    Rigidbody baseR;
    UILabel labeld;
   HERO grabbedTargetT;
   IN_GAME_MAIN_CAMERA currentCameraT;
   Dictionary<string, Transform> cacheTrasform;
   Transform grabTFT;

   Transform tfind(string transform_name)
   {
       if(cacheTrasform.ContainsKey(transform_name) && cacheTrasform[transform_name] != null)
       {
           return cacheTrasform[transform_name];
       }
       Transform trss = baseT.Find(transform_name);
       cacheTrasform.Add(transform_name,trss);
       return trss;
   }
    private void attack(string type)
    {
        this.state = "attack";
        this.attacked = false;
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
        this.startJump = false;
        this.attackChkOnce = false;
        this.nextAttackAnimation = null;
        this.fxName = null;
        this.isAttackMoveByCore = false;
        this.attackCheckTime = 0f;
        this.attackCheckTimeA = 0f;
        this.attackCheckTimeB = 0f;
        this.fxRotation = Quaternion.Euler(270f, 0f, 0f);
        string key = type;
        if (key != null)
        {
            int num;
            if (f__switchSmap2 == null)
            {
                Dictionary<string, int> dictionary = new Dictionary<string, int>(0x11) {
                    { 
                        "combo_1",
                        0
                    },
                    { 
                        "combo_2",
                        1
                    },
                    { 
                        "combo_3",
                        2
                    },
                    { 
                        "combo_blind_1",
                        3
                    },
                    { 
                        "combo_blind_2",
                        4
                    },
                    { 
                        "combo_blind_3",
                        5
                    },
                    { 
                        "front",
                        6
                    },
                    { 
                        "jumpCombo_1",
                        7
                    },
                    { 
                        "jumpCombo_2",
                        8
                    },
                    { 
                        "jumpCombo_3",
                        9
                    },
                    { 
                        "jumpCombo_4",
                        10
                    },
                    { 
                        "sweep",
                        11
                    },
                    { 
                        "sweep_back",
                        12
                    },
                    { 
                        "sweep_front_left",
                        13
                    },
                    { 
                        "sweep_front_right",
                        14
                    },
                    { 
                        "sweep_head_b_l",
                        15
                    },
                    { 
                        "sweep_head_b_r",
                        0x10
                    }
                };
                f__switchSmap2 = dictionary;
            }
            if (f__switchSmap2.TryGetValue(key, out num))
            {
                switch (num)
                {
                    case 0:
                        this.attackCheckTimeA = 0.63f;
                        this.attackCheckTimeB = 0.8f;
                        this.checkHitCapsuleEnd = tfind("Amarture/Core/Controller_Body/hip/thigh_R/shin_R/foot_R");
                        this.checkHitCapsuleStart = tfind("Amarture/Core/Controller_Body/hip/thigh_R");
                        this.checkHitCapsuleR = 5f;
                        this.isAttackMoveByCore = true;
                        this.nextAttackAnimation = "combo_2";
                        break;

                    case 1:
                        this.attackCheckTimeA = 0.27f;
                        this.attackCheckTimeB = 0.43f;
                        this.checkHitCapsuleEnd = tfind("Amarture/Core/Controller_Body/hip/thigh_L/shin_L/foot_L");
                        this.checkHitCapsuleStart = tfind("Amarture/Core/Controller_Body/hip/thigh_L");
                        this.checkHitCapsuleR = 5f;
                        this.isAttackMoveByCore = true;
                        this.nextAttackAnimation = "combo_3";
                        break;

                    case 2:
                        this.attackCheckTimeA = 0.15f;
                        this.attackCheckTimeB = 0.3f;
                        this.checkHitCapsuleEnd = tfind("Amarture/Core/Controller_Body/hip/thigh_R/shin_R/foot_R");
                        this.checkHitCapsuleStart = tfind("Amarture/Core/Controller_Body/hip/thigh_R");
                        this.checkHitCapsuleR = 5f;
                        this.isAttackMoveByCore = true;
                        break;

                    case 3:
                        this.isAttackMoveByCore = true;
                        this.attackCheckTimeA = 0.72f;
                        this.attackCheckTimeB = 0.83f;
                        this.checkHitCapsuleStart = tfind("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R");
                        this.checkHitCapsuleEnd = tfind("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R/hand_R_001");
                        this.checkHitCapsuleR = 4f;
                        this.nextAttackAnimation = "combo_blind_2";
                        break;

                    case 4:
                        this.isAttackMoveByCore = true;
                        this.attackCheckTimeA = 0.5f;
                        this.attackCheckTimeB = 0.6f;
                        this.checkHitCapsuleStart = tfind("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R");
                        this.checkHitCapsuleEnd = tfind("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R/hand_R_001");
                        this.checkHitCapsuleR = 4f;
                        this.nextAttackAnimation = "combo_blind_3";
                        break;

                    case 5:
                        this.isAttackMoveByCore = true;
                        this.attackCheckTimeA = 0.2f;
                        this.attackCheckTimeB = 0.28f;
                        this.checkHitCapsuleStart = tfind("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R");
                        this.checkHitCapsuleEnd = tfind("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R/hand_R_001");
                        this.checkHitCapsuleR = 4f;
                        break;

                    case 6:
                        this.isAttackMoveByCore = true;
                        this.attackCheckTimeA = 0.44f;
                        this.attackCheckTimeB = 0.55f;
                        this.checkHitCapsuleStart = tfind("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R");
                        this.checkHitCapsuleEnd = tfind("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R/hand_R_001");
                        this.checkHitCapsuleR = 4f;
                        break;

                    case 7:
                        this.isAttackMoveByCore = false;
                        this.nextAttackAnimation = "jumpCombo_2";
                        this.abnorma_jump_bite_horizon_v = Vector3.zero;
                        break;

                    case 8:
                        this.isAttackMoveByCore = false;
                        this.attackCheckTimeA = 0.48f;
                        this.attackCheckTimeB = 0.7f;
                        this.checkHitCapsuleStart = tfind("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R");
                        this.checkHitCapsuleEnd = tfind("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R/hand_R_001");
                        this.checkHitCapsuleR = 4f;
                        this.nextAttackAnimation = "jumpCombo_3";
                        break;

                    case 9:
                        this.isAttackMoveByCore = false;
                        this.checkHitCapsuleEnd = tfind("Amarture/Core/Controller_Body/hip/thigh_L/shin_L/foot_L");
                        this.checkHitCapsuleStart = tfind("Amarture/Core/Controller_Body/hip/thigh_L");
                        this.checkHitCapsuleR = 5f;
                        this.attackCheckTimeA = 0.22f;
                        this.attackCheckTimeB = 0.42f;
                        break;

                    case 10:
                        this.isAttackMoveByCore = false;
                        break;

                    case 11:
                        this.isAttackMoveByCore = true;
                        this.attackCheckTimeA = 0.39f;
                        this.attackCheckTimeB = 0.6f;
                        this.checkHitCapsuleEnd = tfind("Amarture/Core/Controller_Body/hip/thigh_R/shin_R/foot_R");
                        this.checkHitCapsuleStart = tfind("Amarture/Core/Controller_Body/hip/thigh_R");
                        this.checkHitCapsuleR = 5f;
                        break;

                    case 12:
                        this.isAttackMoveByCore = true;
                        this.attackCheckTimeA = 0.41f;
                        this.attackCheckTimeB = 0.48f;
                        this.checkHitCapsuleStart = tfind("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R");
                        this.checkHitCapsuleEnd = tfind("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R/hand_R_001");
                        this.checkHitCapsuleR = 4f;
                        break;

                    case 13:
                        this.isAttackMoveByCore = true;
                        this.attackCheckTimeA = 0.53f;
                        this.attackCheckTimeB = 0.63f;
                        this.checkHitCapsuleStart = tfind("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R");
                        this.checkHitCapsuleEnd = tfind("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R/hand_R_001");
                        this.checkHitCapsuleR = 4f;
                        break;

                    case 14:
                        this.isAttackMoveByCore = true;
                        this.attackCheckTimeA = 0.5f;
                        this.attackCheckTimeB = 0.62f;
                        this.checkHitCapsuleStart = tfind("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_L/upper_arm_L");
                        this.checkHitCapsuleEnd = tfind("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_L/upper_arm_L/forearm_L/hand_L/hand_L_001");
                        this.checkHitCapsuleR = 4f;
                        break;

                    case 15:
                        this.isAttackMoveByCore = true;
                        this.attackCheckTimeA = 0.4f;
                        this.attackCheckTimeB = 0.51f;
                        this.checkHitCapsuleStart = tfind("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_L/upper_arm_L");
                        this.checkHitCapsuleEnd = tfind("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_L/upper_arm_L/forearm_L/hand_L/hand_L_001");
                        this.checkHitCapsuleR = 4f;
                        break;

                    case 0x10:
                        this.isAttackMoveByCore = true;
                        this.attackCheckTimeA = 0.4f;
                        this.attackCheckTimeB = 0.51f;
                        this.checkHitCapsuleStart = tfind("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R");
                        this.checkHitCapsuleEnd = tfind("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R/hand_R_001");
                        this.checkHitCapsuleR = 4f;
                        break;
                }
            }
        }
        this.checkHitCapsuleEndOld = this.checkHitCapsuleEnd.transform.position;
        this.needFreshCorePosition = true;
    }

    private bool attackTarget(GameObject target)
    {
        int num;
        float current = 0f;
        float f = 0f;
        Vector3 vector = target.transform.position - baseT.position;
        current = -Mathf.Atan2(vector.z, vector.x) * 57.29578f;
        f = -Mathf.DeltaAngle(current, baseG.transform.rotation.eulerAngles.y - 90f);
        if ((this.eren != null) && (this.myDistance < 35f))
        {
            this.attack("combo_1");
            return true;
        }
        int num4 = 0;
        string str = string.Empty;
        ArrayList list = new ArrayList();
        if (this.myDistance < 40f)
        {
            if (Mathf.Abs(f) < 90f)
            {
                if (f > 0f)
                {
                    num4 = 1;
                }
                else
                {
                    num4 = 2;
                }
            }
            else if (f > 0f)
            {
                num4 = 4;
            }
            else
            {
                num4 = 3;
            }
            float num5 = target.transform.position.y - baseT.position.y;
            if (Mathf.Abs(f) < 90f)
            {
                if (((num5 > 0f) && (num5 < 12f)) && (this.myDistance < 22f))
                {
                    list.Add("attack_sweep");
                }
                if ((num5 >= 55f) && (num5 < 90f))
                {
                    list.Add("attack_jumpCombo_1");
                }
            }
            if (((Mathf.Abs(f) < 90f) && (num5 > 12f)) && (num5 < 40f))
            {
                list.Add("attack_combo_1");
            }
            if (Mathf.Abs(f) < 30f)
            {
                if (((num5 > 0f) && (num5 < 12f)) && ((this.myDistance > 20f) && (this.myDistance < 30f)))
                {
                    list.Add("attack_front");
                }
                if (((this.myDistance < 12f) && (num5 > 33f)) && (num5 < 51f))
                {
                    list.Add("grab_up");
                }
            }
            if (((Mathf.Abs(f) > 100f) && (this.myDistance < 11f)) && ((num5 >= 15f) && (num5 < 32f)))
            {
                list.Add("attack_sweep_back");
            }
            num = num4;
            switch (num)
            {
                case 1:
                    if (this.myDistance < 11f)
                    {
                        if ((num5 >= 21f) && (num5 < 32f))
                        {
                            list.Add("attack_sweep_front_right");
                        }
                        break;
                    }
                    if (this.myDistance < 20f)
                    {
                        if ((num5 < 12f) || (num5 >= 21f))
                        {
                            if ((num5 >= 21f) && (num5 < 32f))
                            {
                                list.Add("grab_mid_right");
                            }
                            else if ((num5 >= 32f) && (num5 < 47f))
                            {
                                list.Add("grab_up_right");
                            }
                            break;
                        }
                        list.Add("grab_bottom_right");
                    }
                    break;

                case 2:
                    if (this.myDistance < 11f)
                    {
                        if ((num5 >= 21f) && (num5 < 32f))
                        {
                            list.Add("attack_sweep_front_left");
                        }
                        break;
                    }
                    if (this.myDistance < 20f)
                    {
                        if ((num5 < 12f) || (num5 >= 21f))
                        {
                            if ((num5 >= 21f) && (num5 < 32f))
                            {
                                list.Add("grab_mid_left");
                            }
                            else if ((num5 >= 32f) && (num5 < 47f))
                            {
                                list.Add("grab_up_left");
                            }
                            break;
                        }
                        list.Add("grab_bottom_left");
                    }
                    break;

                case 3:
                    if (this.myDistance < 11f)
                    {
                        if ((num5 >= 33f) && (num5 < 51f))
                        {
                            list.Add("attack_sweep_head_b_l");
                        }
                        break;
                    }
                    list.Add("turn180");
                    break;

                case 4:
                    if (this.myDistance < 11f)
                    {
                        if ((num5 >= 33f) && (num5 < 51f))
                        {
                            list.Add("attack_sweep_head_b_r");
                        }
                        break;
                    }
                    list.Add("turn180");
                    break;
            }
        }
        if (list.Count > 0)
        {
            str = (string) list[UnityEngine.Random.Range(0, list.Count)];
        }
        else if (UnityEngine.Random.Range(0, 100) < 10)
        {
            if (net_searh)
            {
                List<GameObject> nlist = FengGameManagerMKII.instance.allheroes;
                this.myHero = nlist[UnityEngine.Random.Range(0, nlist.Count)];
                this.attention = UnityEngine.Random.Range((float)5f, (float)10f);
                return true;
            }
        }
        string key = str;
        if (key != null)
        {
            if (f__switchSmap1 == null)
            {
                Dictionary<string, int> dictionary = new Dictionary<string, int>(0x11) {
                    { 
                        "grab_bottom_left",
                        0
                    },
                    { 
                        "grab_bottom_right",
                        1
                    },
                    { 
                        "grab_mid_left",
                        2
                    },
                    { 
                        "grab_mid_right",
                        3
                    },
                    { 
                        "grab_up",
                        4
                    },
                    { 
                        "grab_up_left",
                        5
                    },
                    { 
                        "grab_up_right",
                        6
                    },
                    { 
                        "attack_combo_1",
                        7
                    },
                    { 
                        "attack_front",
                        8
                    },
                    { 
                        "attack_jumpCombo_1",
                        9
                    },
                    { 
                        "attack_sweep",
                        10
                    },
                    { 
                        "attack_sweep_back",
                        11
                    },
                    { 
                        "attack_sweep_front_left",
                        12
                    },
                    { 
                        "attack_sweep_front_right",
                        13
                    },
                    { 
                        "attack_sweep_head_b_l",
                        14
                    },
                    { 
                        "attack_sweep_head_b_r",
                        15
                    },
                    { 
                        "turn180",
                        0x10
                    }
                };
                f__switchSmap1 = dictionary;
            }
            if (f__switchSmap1.TryGetValue(key, out num))
            {
                switch (num)
                {
                    case 0:
                        this.grab("bottom_left");
                        return true;

                    case 1:
                        this.grab("bottom_right");
                        return true;

                    case 2:
                        this.grab("mid_left");
                        return true;

                    case 3:
                        this.grab("mid_right");
                        return true;

                    case 4:
                        this.grab("up");
                        return true;

                    case 5:
                        this.grab("up_left");
                        return true;

                    case 6:
                        this.grab("up_right");
                        return true;

                    case 7:
                        this.attack("combo_1");
                        return true;

                    case 8:
                        this.attack("front");
                        return true;

                    case 9:
                        this.attack("jumpCombo_1");
                        return true;

                    case 10:
                        this.attack("sweep");
                        return true;

                    case 11:
                        this.attack("sweep_back");
                        return true;

                    case 12:
                        this.attack("sweep_front_left");
                        return true;

                    case 13:
                        this.attack("sweep_front_right");
                        return true;

                    case 14:
                        this.attack("sweep_head_b_l");
                        return true;

                    case 15:
                        this.attack("sweep_head_b_r");
                        return true;

                    case 0x10:
                        this.turn180();
                        return true;
                }
            }
        }
        return false;
    }

    private void Awake()
    {
        cacheTrasform = new Dictionary<string, Transform>();
        baseT = base.transform;
        baseA = base.animation;
        baseG = base.gameObject;
        baseR = base.rigidbody;
        baseR.freezeRotation = true;
        baseR.useGravity = false;
    }

    public void beTauntedBy(GameObject target, float tauntTime)
    {
        this.whoHasTauntMe = target;
        this.tauntTime = tauntTime;
    }

    private void chase()
    {
        this.state = "chase";
        this.crossFade("run", 0.5f);
    }

    private RaycastHit[] checkHitCapsule(Vector3 start, Vector3 end, float r)
    {
        return Physics.SphereCastAll(start, r, end - start, Vector3.Distance(start, end));
    }

    private HERO checkIfHitHand(Transform hand)
    {
        foreach (Collider collider in Physics.OverlapSphere(hand.GetComponent<SphereCollider>().transform.position, 10.6f))
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
                }
                HERO hr = gameObject.GetComponent<HERO>();
                if ((hr != null) && !hr.isInvincible())
                {
                    return hr;
                }
            }
        }
        return null;
    }

    private GameObject checkIfHitHead(Transform head, float rad)
    {
        float num = rad * 4f;
        foreach (GameObject obj2 in  FengGameManagerMKII.instance.allheroes)
        {
            if ((obj2.GetComponent<TITAN_EREN>() == null) && !obj2.GetComponent<HERO>().isInvincible())
            {
                float num2 = obj2.GetComponent<CapsuleCollider>().height * 0.5f;
                if (Vector3.Distance(obj2.transform.position + ((Vector3) (Vector3.up * num2)), head.transform.position + ((Vector3) ((Vector3.up * 1.5f) * 4f))) < (num + num2))
                {
                    return obj2;
                }
            }
        }
        return null;
    }

    private void crossFade(string aniName, float time)
    {
        baseA.CrossFade(aniName, time);
        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && PhotonNetwork.isMasterClient)
        {
            object[] parameters = new object[] { aniName, time };
            base.photonView.RPC("netCrossFade", PhotonTargets.Others, parameters);
        }
    }

    private void eatSet(HERO hero)
    {
        if (!hero.isGrabbed)
        {
            this.grabToRight();
            if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && PhotonNetwork.isMasterClient)
            {
                object[] parameters = new object[] { base.photonView.viewID, false };
                hero.photonView.RPC("netGrabbed", PhotonTargets.All, parameters);
                object[] objArray2 = new object[] { "grabbed" };
                hero.photonView.RPC("netPlayAnimation", PhotonTargets.All, objArray2);
                base.photonView.RPC("grabToRight", PhotonTargets.Others, new object[0]);
            }
            else
            {
                hero.grabbed(baseG, false);
                hero.animation.Play("grabbed");
            }
        }
    }

    private void eatSetL(HERO hero)
    {
        if (!hero.isGrabbed)
        {
            this.grabToLeft();
            if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && PhotonNetwork.isMasterClient)
            {
                object[] parameters = new object[] { base.photonView.viewID, true };
                hero.photonView.RPC("netGrabbed", PhotonTargets.All, parameters);
                object[] objArray2 = new object[] { "grabbed" };
                hero.photonView.RPC("netPlayAnimation", PhotonTargets.All, objArray2);
                base.photonView.RPC("grabToLeft", PhotonTargets.Others, new object[0]);
            }
            else
            {
                hero.grabbed(baseG, true);
                hero.animation.Play("grabbed");
            }
        }
    }

    public void erenIsHere(GameObject target)
    {
        if (net_searh)
        {
            this.myHero = this.eren = target;
        }
    }

    private void findNearestFacingHero()
    {
        if (net_searh)
        {
            GameObject obj2 = null;
            float positiveInfinity = float.PositiveInfinity;
            Vector3 position = baseT.position;
            float current = 0f;
            float num3 = 180f;
            float f = 0f;
            foreach (HERO hero in FengGameManagerMKII.instance.heroes)
            {
                GameObject obj3 = hero.gameObject;
                Vector3 vector2 = obj3.transform.position - position;
                float sqrMagnitude = vector2.sqrMagnitude;
                if (sqrMagnitude < positiveInfinity)
                {
                    Vector3 vector3 = obj3.transform.position - baseT.position;
                    current = -Mathf.Atan2(vector3.z, vector3.x) * 57.29578f;
                    f = -Mathf.DeltaAngle(current, baseG.transform.rotation.eulerAngles.y - 90f);
                    if (Mathf.Abs(f) < num3)
                    {
                        obj2 = obj3;
                        positiveInfinity = sqrMagnitude;
                    }
                }
            }
            if (obj2 != null)
            {
                this.myHero = obj2;
                this.tauntTime = 5f;
            }
        }
    }
    public bool net_searh = true;
    private void findNearestHero()
    {
        if (net_searh)
        {
            this.myHero = this.getNearestHero();
            this.attention = UnityEngine.Random.Range((float)5f, (float)10f);
        }
    }

    private void FixedUpdate()
    {
        if ((!IN_GAME_MAIN_CAMERA.isPausing || (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)) && ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE) || base.photonView.isMine))
        {
            CheckHitGround chg= this.bottomObject.GetComponent<CheckHitGround>();
            if (chg.isGrounded)
            {
                this.grounded = true;
                chg.isGrounded = false;
            }
            else
            {
                this.grounded = false;
            }
            if (this.needFreshCorePosition)
            {
                this.oldCorePosition = baseT.position - tfind("Amarture/Core").position;
                this.needFreshCorePosition = false;
            }
            if (((this.state != "attack") || !this.isAttackMoveByCore) && (((this.state != "hit") && (this.state != "turn180")) && (this.state != "anklehurt")))
            {
                if (this.state == "chase")
                {
                    if (this.myHero == null)
                    {
                        return;
                    }
                    Vector3 vector = (Vector3) (baseT.forward * this.speed);
                    Vector3 velocity = baseR.velocity;
                    Vector3 force = vector - velocity;
                    force.y = 0f;
                    baseR.AddForce(force, ForceMode.VelocityChange);
                    float current = 0f;
                    Vector3 vector4 = this.myHero.transform.position - baseT.position;
                    current = -Mathf.Atan2(vector4.z, vector4.x) * 57.29578f;
                    float num2 = -Mathf.DeltaAngle(current, baseG.transform.rotation.eulerAngles.y - 90f);
                    baseG.transform.rotation = Quaternion.Lerp(baseG.transform.rotation, Quaternion.Euler(0f, baseG.transform.rotation.eulerAngles.y + num2, 0f), this.speed * Time.deltaTime);
                }
                else if (this.grounded && !baseA.IsPlaying("attack_jumpCombo_1"))
                {
                    baseR.AddForce(new Vector3(-baseR.velocity.x, 0f, -baseR.velocity.z), ForceMode.VelocityChange);
                }
            }
            else
            {
                Vector3 vector5 = (baseT.position - tfind("Amarture/Core").position) - this.oldCorePosition;
                baseR.velocity = (Vector3) ((vector5 / Time.deltaTime) + (Vector3.up * baseR.velocity.y));
                this.oldCorePosition = baseT.position - tfind("Amarture/Core").position;
            }
            baseR.AddForce(new Vector3(0f, -this.gravity * baseR.mass, 0f));
        }
    }

    private void getDown()
    {
        this.state = "anklehurt";
        this.playAnimation("legHurt");
        this.AnkleRHP = this.AnkleRHPMAX;
        this.AnkleLHP = this.AnkleLHPMAX;
        this.needFreshCorePosition = true;
    }

    private GameObject getNearestHero()
    {
    
        GameObject obj2 = null;
        float positiveInfinity = float.PositiveInfinity;
        Vector3 position = baseT.position;
        foreach (GameObject obj3 in FengGameManagerMKII.instance.allheroes)
        {
            HERO hero = obj3.GetComponent<HERO>();
            TITAN_EREN te = null;
            if (((hero == null) || !hero.HasDied()) && (((te = obj3.GetComponent<TITAN_EREN>()) == null) || !te.hasDied))
            {
                Vector3 vector2 = obj3.transform.position - position;
                float sqrMagnitude = vector2.sqrMagnitude;
                if (sqrMagnitude < positiveInfinity)
                {
                    obj2 = obj3;
                    positiveInfinity = sqrMagnitude;
                }
            }
        }
        return obj2;
    }

    private float getNearestHeroDistance()
    {
        float positiveInfinity = float.PositiveInfinity;
        Vector3 position = baseT.position;
        foreach (GameObject obj2 in FengGameManagerMKII.instance.allheroes)
        {
            Vector3 vector2 = obj2.transform.position - position;
            float magnitude = vector2.magnitude;
            if (magnitude < positiveInfinity)
            {
                positiveInfinity = magnitude;
            }
        }
        return positiveInfinity;
    }

    private void grab(string type)
    {
        this.state = "grab";
        this.attacked = false;
        this.attackAnimation = type;
        if (baseA.IsPlaying("attack_grab_" + type))
        {
            baseA["attack_grab_" + type].normalizedTime = 0f;
            this.playAnimation("attack_grab_" + type);
        }
        else
        {
            this.crossFade("attack_grab_" + type, 0.1f);
        }
        this.isGrabHandLeft = true;
        grabbedTargetT = null;
        this.attackCheckTime = 0f;
        string key = type;
        if (key != null)
        {
            int num;
            if (f__switchSmap3 == null)
            {
                Dictionary<string, int> dictionary = new Dictionary<string, int>(7) {
                    { 
                        "bottom_left",
                        0
                    },
                    { 
                        "bottom_right",
                        1
                    },
                    { 
                        "mid_left",
                        2
                    },
                    { 
                        "mid_right",
                        3
                    },
                    { 
                        "up",
                        4
                    },
                    { 
                        "up_left",
                        5
                    },
                    { 
                        "up_right",
                        6
                    }
                };
                f__switchSmap3 = dictionary;
            }
            if (f__switchSmap3.TryGetValue(key, out num))
            {
                switch (num)
                {
                    case 0:
                        this.attackCheckTimeA = 0.28f;
                        this.attackCheckTimeB = 0.38f;
                        this.attackCheckTime = 0.65f;
                        this.isGrabHandLeft = false;
                        break;

                    case 1:
                        this.attackCheckTimeA = 0.27f;
                        this.attackCheckTimeB = 0.37f;
                        this.attackCheckTime = 0.65f;
                        break;

                    case 2:
                        this.attackCheckTimeA = 0.27f;
                        this.attackCheckTimeB = 0.37f;
                        this.attackCheckTime = 0.65f;
                        this.isGrabHandLeft = false;
                        break;

                    case 3:
                        this.attackCheckTimeA = 0.27f;
                        this.attackCheckTimeB = 0.36f;
                        this.attackCheckTime = 0.66f;
                        break;

                    case 4:
                        this.attackCheckTimeA = 0.25f;
                        this.attackCheckTimeB = 0.32f;
                        this.attackCheckTime = 0.67f;
                        break;

                    case 5:
                        this.attackCheckTimeA = 0.26f;
                        this.attackCheckTimeB = 0.4f;
                        this.attackCheckTime = 0.66f;
                        break;

                    case 6:
                        this.attackCheckTimeA = 0.26f;
                        this.attackCheckTimeB = 0.4f;
                        this.attackCheckTime = 0.66f;
                        this.isGrabHandLeft = false;
                        break;
                }
            }
        }
        if (this.isGrabHandLeft)
        {
            this.currentGrabHand = tfind("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_L/upper_arm_L/forearm_L/hand_L/hand_L_001");
        }
        else
        {
            this.currentGrabHand = tfind("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R/hand_R_001");
        }
    }

    [RPC]
    public void grabbedTargetEscape()
    {
        grabbedTargetT = null;
    }

    [RPC]
    public void grabToLeft()
    {
        Transform transform = tfind("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_L/upper_arm_L/forearm_L/hand_L/hand_L_001");
        this.grabTFT.parent = transform;
        this.grabTFT.parent = transform;
        this.grabTFT.position = transform.GetComponent<SphereCollider>().transform.position;
        this.grabTFT.rotation = transform.GetComponent<SphereCollider>().transform.rotation;
        SphereCollider cs = transform.GetComponent<SphereCollider>();
        this.grabTFT.localPosition -= (Vector3)((Vector3.right * cs.radius) * 0.3f);
        this.grabTFT.localPosition -= (Vector3)((Vector3.up * cs.radius) * 0.51f);
        this.grabTFT.localPosition -= (Vector3)((Vector3.forward * cs.radius) * 0.3f);
        this.grabTFT.localRotation = Quaternion.Euler(this.grabTFT.localRotation.eulerAngles.x, this.grabTFT.localRotation.eulerAngles.y + 180f, this.grabTFT.localRotation.eulerAngles.z + 180f);
    }

    [RPC]
    public void grabToRight()
    {
        Transform transform = tfind("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R/hand_R_001");
        this.grabTFT.parent = transform;
        SphereCollider sc  = transform.GetComponent<SphereCollider>();
        this.grabTFT.position = sc.transform.position;
        this.grabTFT.rotation = sc.transform.rotation;
        SphereCollider cs = transform.GetComponent<SphereCollider>();
        this.grabTFT.localPosition -= (Vector3)((Vector3.right * cs.radius) * 0.3f);
        this.grabTFT.localPosition += (Vector3)((Vector3.up * cs.radius) * 0.51f);
        this.grabTFT.localPosition -= (Vector3)((Vector3.forward * cs.radius) * 0.3f);
        this.grabTFT.localRotation = Quaternion.Euler(this.grabTFT.localRotation.eulerAngles.x, this.grabTFT.localRotation.eulerAngles.y + 180f, this.grabTFT.localRotation.eulerAngles.z);
    }

    public void hit(int dmg)
    {
        this.NapeArmor -= dmg;
        if (this.NapeArmor <= 0)
        {
            this.NapeArmor = 0;
        }
    }

    public void hitAnkleL(int dmg)
    {
        if (!this.hasDie && (this.state != "anklehurt"))
        {
            this.AnkleLHP -= dmg;
            if (this.AnkleLHP <= 0)
            {
                this.getDown();
            }
        }
    }

    [RPC]
    public void hitAnkleLRPC(int viewID, int dmg)
    {
        if (!this.hasDie && (this.state != "anklehurt"))
        {
            PhotonView view = PhotonView.Find(viewID);
            if (view != null)
            {
                if (grabbedTargetT != null)
                {
                    grabbedTargetT.photonView.RPC("netUngrabbed", PhotonTargets.All, new object[0]);
                }
                Vector3 vector = view.gameObject.transform.position - tfind("Amarture/Core/Controller_Body").transform.position;
                if (vector.magnitude < 20f)
                {
                    this.AnkleLHP -= dmg;
                    if (this.AnkleLHP <= 0)
                    {
                        this.getDown();
                    }
                    FengGameManagerMKII.instance.sendKillInfo(false, (string) view.owner.name2, true, "Female Titan's ankle", dmg);

                    FengGameManagerMKII.instance.photonView.RPC("netShowDamage", view.owner, new object[] { dmg });
                }
            }
        }
    }

    public void hitAnkleR(int dmg)
    {
        if (!this.hasDie && (this.state != "anklehurt"))
        {
            this.AnkleRHP -= dmg;
            if (this.AnkleRHP <= 0)
            {
                this.getDown();
            }
        }
    }

    [RPC]
    public void hitAnkleRRPC(int viewID, int dmg)
    {
        if (!this.hasDie && (this.state != "anklehurt"))
        {
            PhotonView view = PhotonView.Find(viewID);
            if (view != null)
            {
                if (grabbedTargetT != null)
                {
                    grabbedTargetT.photonView.RPC("netUngrabbed", PhotonTargets.All, new object[0]);
                }
                Vector3 vector = view.gameObject.transform.position - tfind("Amarture/Core/Controller_Body").transform.position;
                if (vector.magnitude < 20f)
                {
                    this.AnkleRHP -= dmg;
                    if (this.AnkleRHP <= 0)
                    {
                        this.getDown();
                    }
                    FengGameManagerMKII.instance.sendKillInfo(false, (string) view.owner.name2, true, "Female Titan's ankle", dmg);

                    FengGameManagerMKII.instance.photonView.RPC("netShowDamage", view.owner, new object[] { dmg });
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
            if (grabbedTargetT != null)
            {
                grabbedTargetT.photonView.RPC("netUngrabbed", PhotonTargets.All, new object[0]);
            }
            Transform transform = tfind("Amarture/Core/Controller_Body/hip/spine/chest/neck");
            PhotonView view = PhotonView.Find(viewID);
            if (view != null)
            {
                Vector3 vector = view.gameObject.transform.position - transform.transform.position;
                if (vector.magnitude < 20f)
                {
                    this.justHitEye();
                }
            }
        }
    }

    private void idle(float sbtime = 0f)
    {
        this.sbtime = sbtime;
        this.sbtime = Mathf.Max(0.5f, this.sbtime);
        this.state = "idle";
        this.crossFade("idle", 0.2f);
    }

    public bool IsGrounded()
    {
        return this.bottomObject.GetComponent<CheckHitGround>().isGrounded;
    }

    private void justEatHero(HERO hero, Transform hand)
    {
        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && PhotonNetwork.isMasterClient)
        {
            if (!hero.HasDied())
            {
                hero.markDie();
                hero.photonView.RPC("netDie2", PhotonTargets.All, new object[] { -1, "Female Titan" });
            }
        }
        else if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
        {
            hero.die2(hand);
        }
    }

    private void justHitEye()
    {
        this.attack("combo_blind_1");
    }

    private void killPlayer(HERO hero)
    {
        if (hero != null)
        {
            Vector3 position = tfind("Amarture/Core/Controller_Body/hip/spine/chest").position;
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                if (!hero.HasDied())
                {
                    hero.die((Vector3)(((hero.transform.position - position) * 15f) * 4f), false);
                }
            }
            else if (((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && PhotonNetwork.isMasterClient) && !hero.HasDied())
            {
                hero.markDie();
                object[] parameters = new object[] { (Vector3)(((hero.transform.position - position) * 15f) * 4f), false, -1, "Female Titan", true };
                hero.photonView.RPC("netDie", PhotonTargets.All, parameters);
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
            if ((int)FengGameManagerMKII.settings[333] == 0)
            {
                if (this.healthLabel == null)
                {
                    this.healthLabel = (GameObject)UnityEngine.Object.Instantiate(Cach.UI_LabelNameOverHead != null ? Cach.UI_LabelNameOverHead : Cach.UI_LabelNameOverHead = (GameObject)Resources.Load("UI/LabelNameOverHead"));
                    this.healthLabel.name = "LabelNameOverHead";
                    this.healthLabel.transform.parent = baseT;
                    this.healthLabel.transform.localPosition = new Vector3(0f, 52f, 0f);
                    float a = 4f;
                    if ((this.size > 0f) && (this.size < 1f))
                    {
                        a = 4f / this.size;
                        a = Mathf.Min(a, 15f);
                    }
                    this.healthLabel.transform.localScale = new Vector3(a, a, a);
                    labeld =  this.healthLabel.GetComponent<UILabel>();
                }
                string str = "[7FFF00]";
                float num2 = ((float)health) / ((float)maxHealth);
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
                labeld.text = str + Convert.ToString(health);
            }
        }
    }

    public void lateUpdate()
    {
      
    }

    public void lateUpdate2()        
    {
        if (!IN_GAME_MAIN_CAMERA.isPausing || (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE))
        {
            if (baseA.IsPlaying("run"))
            {
                if ((((baseA["run"].normalizedTime % 1f) > 0.1f) && ((baseA["run"].normalizedTime % 1f) < 0.6f)) && (this.stepSoundPhase == 2))
                {
                    this.stepSoundPhase = 1;
                    if ((int)FengGameManagerMKII.settings[330] == 0)
                    {
                        snd_titan_foot.Stop();
                        snd_titan_foot.Play();
                    }
                }
                if (((baseA["run"].normalizedTime % 1f) > 0.6f) && (this.stepSoundPhase == 1))
                {
                    this.stepSoundPhase = 2;
                    if ((int)FengGameManagerMKII.settings[330] == 0)
                    {
                        snd_titan_foot.Stop();
                        snd_titan_foot.Play();
                    }
                }
            }
            this.updateLabel();
            this.healthTime -= Time.deltaTime;
        }
    }

    public void loadskin()
    {
        if (((int) FengGameManagerMKII.settings[1]) == 1)
        {
            base.photonView.RPC("loadskinRPC", PhotonTargets.AllBuffered, new object[] { (string) FengGameManagerMKII.settings[0x42] });
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

    [RPC]
    private void netCrossFade(string aniName, float time)
    {
        baseA.CrossFade(aniName, time);
    }

    [RPC]
    public void netDie()
    {
        if (!this.hasDie)
        {
            this.hasDie = true;
            FengGameManagerMKII.instance.isUpdateFT = true;
            this.crossFade("die", 0.05f);
        }
    }

    [RPC]
    private void netPlayAnimation(string aniName)
    {
        baseA.Play(aniName);
    }

    [RPC]
    private void netPlayAnimationAt(string aniName, float normalizedTime)
    {
        baseA.Play(aniName);
        baseA[aniName].normalizedTime = normalizedTime;
    }

    private void OnDestroy()
    {
        FengGameManagerMKII mk = FengGameManagerMKII.instance;
        if (mk != null)
        {
            mk.removeFT(this);
        }
        
    }

    private void playAnimation(string aniName)
    {
        baseA.Play(aniName);
        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && PhotonNetwork.isMasterClient)
        {
            object[] parameters = new object[] { aniName };
            base.photonView.RPC("netPlayAnimation", PhotonTargets.Others, parameters);
        }
    }

    private void playAnimationAt(string aniName, float normalizedTime)
    {
        baseA.Play(aniName);
        baseA[aniName].normalizedTime = normalizedTime;
        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && PhotonNetwork.isMasterClient)
        {
            object[] parameters = new object[] { aniName, normalizedTime };
            base.photonView.RPC("netPlayAnimationAt", PhotonTargets.Others, parameters);
        }
    }

    private void playSound(string sndname)
    {
        this.playsoundRPC(sndname);
        if (Network.peerType == NetworkPeerType.Server)
        {
            object[] parameters = new object[] { sndname };
            base.photonView.RPC("playsoundRPC", PhotonTargets.Others, parameters);
        }
    }
    AudioSource snd_eren_swing1;
    AudioSource snd_eren_swing2;
    AudioSource snd_eren_swing3;
    [RPC]
    private void playsoundRPC(string sndname)
    {
        if ((int)FengGameManagerMKII.settings[330] == 0)
        {
            if (sndname == "snd_eren_swing1")
            {
                if (snd_eren_swing1 != null)
                {
                    snd_eren_swing1.Play();
                }
                else
                {
                    (snd_eren_swing1 = tfind(sndname).GetComponent<AudioSource>()).Play();
                }
                
            }
            if (sndname == "snd_eren_swing2")
            {
                if (snd_eren_swing2 != null)
                {
                    snd_eren_swing2.Play();
                }
                else
                {
                    (snd_eren_swing2 = tfind(sndname).GetComponent<AudioSource>()).Play();
                }

            }
            if (sndname == "snd_eren_swing3")
            {
                if (snd_eren_swing3 != null)
                {
                    snd_eren_swing3.Play();
                }
                else
                {
                    (snd_eren_swing3 = tfind(sndname).GetComponent<AudioSource>()).Play();
                }

            }
        }
    }

    [RPC]
    public void setSize(float size, PhotonMessageInfo info)
    {
        size = Mathf.Clamp(size, 0.2f, 30f);
        if (info.sender.isMasterClient)
        {
            baseT.localScale = (Vector3) (transform.localScale * (size * 0.25f));
            this.size = size;
        }
    }

    private void Start()
    {
        snd_titan_foot = tfind("snd_titan_foot").GetComponent<AudioSource>();
        this.startMain();
        this.size = 4f;
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
                damage = 0;
            int.TryParse((string)FengGameManagerMKII.settings[332], out damage);
            if (RCSettings.healthMode > 0)
            {
                this.maxHealth = this.NapeArmor = UnityEngine.Random.Range(RCSettings.healthLower, RCSettings.healthUpper);
            }
            else if (damage > 0)
            {
                this.maxHealth = this.NapeArmor = damage;
            }
            if ((int)FengGameManagerMKII.settings[333] == 0)
            {
                if (this.NapeArmor > 0)
                {
                    base.photonView.RPC("labelRPC", PhotonTargets.AllBuffered, new object[] { this.NapeArmor, this.maxHealth });
                }
            }
            this.loadskin();
        }
        this.hasspawn = true;
    }

    private void startMain()
    {
        FengGameManagerMKII.instance.addFT(this);
        base.name = "Female Titan";
        this.grabTF = new GameObject();
        this.grabTF.name = "titansTmpGrabTF";
        grabTFT = grabTF.transform;
        this.currentCamera = CyanMod.CachingsGM.Find("MainCamera");
        this.currentCameraT = currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>();
        this.oldCorePosition = baseT.position - tfind("Amarture/Core").position;
        if (this.myHero == null)
        {
            this.findNearestHero();
        }
        foreach (AnimationState state in baseA)
        {
            if (state != null)
            {
                state.speed = 0.7f;
            }
        }
        baseA["turn180"].speed = 0.5f;
        this.NapeArmor = 0x3e8;
        this.AnkleLHP = 50;
        this.AnkleRHP = 50;
        this.AnkleLHPMAX = 50;
        this.AnkleRHPMAX = 50;
        bool flag = false;
        if (FengGameManagerMKII.lvlInfo.respawnMode == RespawnMode.NEVER)
        {
            flag = true;
        }
        if (IN_GAME_MAIN_CAMERA.difficulty == DIFFICULTY.NORMAL)
        {
            this.NapeArmor = !flag ? 0x3e8 : 0x3e8;
            this.AnkleLHP = this.AnkleLHPMAX = !flag ? 50 : 50;
            this.AnkleRHP = this.AnkleRHPMAX = !flag ? 50 : 50;
        }
        else if (IN_GAME_MAIN_CAMERA.difficulty == DIFFICULTY.HARD)
        {
            this.NapeArmor = !flag ? 0xbb8 : 0x9c4;
            this.AnkleLHP = this.AnkleLHPMAX = !flag ? 200 : 100;
            this.AnkleRHP = this.AnkleRHPMAX = !flag ? 200 : 100;
            foreach (AnimationState state2 in baseA)
            {
                if (state2 != null)
                {
                    state2.speed = 0.7f;
                }
            }
            baseA["turn180"].speed = 0.7f;
        }
        else if (IN_GAME_MAIN_CAMERA.difficulty == DIFFICULTY.ABNORMAL)
        {
            this.NapeArmor = !flag ? 0x1770 : 0xfa0;
            this.AnkleLHP = this.AnkleLHPMAX = !flag ? 0x3e8 : 200;
            this.AnkleRHP = this.AnkleRHPMAX = !flag ? 0x3e8 : 200;
            foreach (AnimationState state3 in baseA)
            {
                if (state3 != null)
                {
                    state3.speed = 1f;
                }
            }
            baseA["turn180"].speed = 0.9f;
        }
        if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE)
        {
            this.NapeArmor = (int) (this.NapeArmor * 0.8f);
        }
        baseA["legHurt"].speed = 1f;
        baseA["legHurt_loop"].speed = 1f;
        baseA["legHurt_getup"].speed = 1f;
    }

    [RPC]
    public void titanGetHit(int viewID, int speed)
    {
        Transform transform = tfind("Amarture/Core/Controller_Body/hip/spine/chest/neck");
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
                if (this.NapeArmor <= 0)
                {
                    this.NapeArmor = 0;
                    if (!this.hasDie)
                    {
                        base.photonView.RPC("netDie", PhotonTargets.OthersBuffered, new object[0]);
                        if (grabbedTargetT != null)
                        {
                            grabbedTargetT.photonView.RPC("netUngrabbed", PhotonTargets.All, new object[0]);
                        }
                        this.netDie();
                        FengGameManagerMKII.instance.titanGetKill(view.owner, speed, base.name);
                    }
                }
                else
                {
                    FengGameManagerMKII.instance.sendKillInfo(false, (string) view.owner.name2, true, "Female Titan's neck", speed);

                    FengGameManagerMKII.instance.photonView.RPC("netShowDamage", view.owner, new object[] { speed });
                }
                this.healthTime = 0.2f;
            }
        }
    }

    private void turn(float d)
    {
        if (d > 0f)
        {
            this.turnAnimation = "turnaround1";
        }
        else
        {
            this.turnAnimation = "turnaround2";
        }
        this.playAnimation(this.turnAnimation);
        baseA[this.turnAnimation].time = 0f;
        d = Mathf.Clamp(d, -120f, 120f);
        this.turnDeg = d;
        this.desDeg = baseG.transform.rotation.eulerAngles.y + this.turnDeg;
        this.state = "turn";
    }

    private void turn180()
    {
        this.turnAnimation = "turn180";
        this.playAnimation(this.turnAnimation);
        baseA[this.turnAnimation].time = 0f;
        this.state = "turn180";
        this.needFreshCorePosition = true;
    }

    public void update()
    {
        if ((!IN_GAME_MAIN_CAMERA.isPausing || (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)) && ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE) || base.photonView.isMine))
        {
            if (this.hasDie)
            {
                this.dieTime += Time.deltaTime;
                if (baseA["die"].normalizedTime >= 1f)
                {
                    this.playAnimation("die_cry");
                    if (IN_GAME_MAIN_CAMERA.gamemode != GAMEMODE.PVP_CAPTURE && ((int)FengGameManagerMKII.settings[331] == 0 && (RCSettings.AnnieSurvive == 0)))
                    {
                        for (int i = 0; i < 15; i++)
                        {
                           FengGameManagerMKII.instance.randomSpawnOneTitan("titanRespawn", 50).beTauntedBy(baseG, 20f);
                        }
                    }
                }
                if ((this.dieTime > 2f) && !this.hasDieSteam)
                {
                    this.hasDieSteam = true;
                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                    {
                        GameObject obj2 = (GameObject)UnityEngine.Object.Instantiate(Cach.FX_FXtitanDie1 != null ? Cach.FX_FXtitanDie1 : Cach.FX_FXtitanDie1 = (GameObject)Resources.Load("FX/FXtitanDie1"));
                        obj2.transform.position = tfind("Amarture/Core/Controller_Body/hip").position;
                        obj2.transform.localScale = baseT.localScale;
                    }
                    else if (base.photonView.isMine)
                    {
                        PhotonNetwork.Instantiate("FX/FXtitanDie1", tfind("Amarture/Core/Controller_Body/hip").position, Quaternion.Euler(-90f, 0f, 0f), 0).transform.localScale = baseT.localScale;
                    }
                }
                if (this.dieTime > ((IN_GAME_MAIN_CAMERA.gamemode != GAMEMODE.PVP_CAPTURE && (RCSettings.AnnieSurvive == 0)) ? 20f : 5f))
                {
                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                    {
                        GameObject obj3 = (GameObject) UnityEngine.Object.Instantiate(Cach.FX_FXtitanDie != null ? Cach.FX_FXtitanDie : Cach.FX_FXtitanDie = (GameObject)Resources.Load("FX/FXtitanDie"));
                        obj3.transform.position = tfind("Amarture/Core/Controller_Body/hip").position;
                        obj3.transform.localScale = baseT.localScale;
                        UnityEngine.Object.Destroy(baseG);
                    }
                    else if (base.photonView.isMine)
                    {
                        PhotonNetwork.Instantiate("FX/FXtitanDie", tfind("Amarture/Core/Controller_Body/hip").position, Quaternion.Euler(-90f, 0f, 0f), 0).transform.localScale = baseT.localScale;
                        PhotonNetwork.Destroy(baseG);
                    }
                }
            }
            else
            {
                if (net_searh)
                {
                    if (this.attention > 0f)
                    {
                        this.attention -= Time.deltaTime;
                        if (this.attention < 0f)
                        {

                            this.attention = 0f;
                            List<GameObject> nlist = FengGameManagerMKII.instance.allheroes;
                            this.myHero = nlist[UnityEngine.Random.Range(0, nlist.Count)];
                            this.attention = UnityEngine.Random.Range((float)5f, (float)10f);

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
                    }
                    if (this.eren != null)
                    {
                        if (!this.eren.GetComponent<TITAN_EREN>().hasDied)
                        {
                            this.myHero = this.eren;
                        }
                        else
                        {
                            this.eren = null;
                            this.myHero = null;
                        }
                    }
                    if (this.myHero == null)
                    {
                        this.findNearestHero();
                        if (this.myHero != null)
                        {
                            return;
                        }
                    }
                    if (this.myHero == null)
                    {
                        this.myDistance = float.MaxValue;
                    }
                    else
                    {
                        this.myDistance = Mathf.Sqrt(((this.myHero.transform.position.x - baseT.position.x) * (this.myHero.transform.position.x - baseT.position.x)) + ((this.myHero.transform.position.z - baseT.position.z) * (this.myHero.transform.position.z - baseT.position.z)));
                    }
                }
                if (this.state == "idle")
                {
                    if (this.myHero != null)
                    {
                        float current = 0f;
                        float f = 0f;
                        Vector3 vector = this.myHero.transform.position - baseT.position;
                        current = -Mathf.Atan2(vector.z, vector.x) * 57.29578f;
                        f = -Mathf.DeltaAngle(current, baseG.transform.rotation.eulerAngles.y - 90f);
                        if (!this.attackTarget(this.myHero))
                        {
                            if (Mathf.Abs(f) < 90f)
                            {
                                this.chase();
                            }
                            else if (UnityEngine.Random.Range(0, 100) < 1)
                            {
                                this.turn180();
                            }
                            else if (Mathf.Abs(f) > 100f)
                            {
                                if (UnityEngine.Random.Range(0, 100) < 10)
                                {
                                    this.turn180();
                                }
                            }
                            else if ((Mathf.Abs(f) > 45f) && (UnityEngine.Random.Range(0, 100) < 30))
                            {
                                this.turn(f);
                            }
                        }
                    }
                }
                else if (this.state == "attack")
                {
                    if ((!this.attacked && (this.attackCheckTime != 0f)) && (baseA["attack_" + this.attackAnimation].normalizedTime >= this.attackCheckTime))
                    {
                        GameObject obj4;
                        this.attacked = true;
                        this.fxPosition = tfind("ap_" + this.attackAnimation).position;
                        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && PhotonNetwork.isMasterClient)
                        {
                            obj4 = PhotonNetwork.Instantiate("FX/" + this.fxName, this.fxPosition, this.fxRotation, 0);
                        }
                        else
                        {
                            obj4 = (GameObject) UnityEngine.Object.Instantiate(Resources.Load("FX/" + this.fxName), this.fxPosition, this.fxRotation);
                        }
                        obj4.transform.localScale = baseT.localScale;
                        float b = 1f - (Vector3.Distance(this.currentCamera.transform.position, obj4.transform.position) * 0.05f);
                        b = Mathf.Min(1f, b);
                        currentCameraT.startShake(b, b, 0.95f);
                    }
                    if ((this.attackCheckTimeA != 0f) && (((baseA["attack_" + this.attackAnimation].normalizedTime >= this.attackCheckTimeA) && (baseA["attack_" + this.attackAnimation].normalizedTime <= this.attackCheckTimeB)) || (!this.attackChkOnce && (baseA["attack_" + this.attackAnimation].normalizedTime >= this.attackCheckTimeA))))
                    {
                        if (!this.attackChkOnce)
                        {
                            this.attackChkOnce = true;
                            this.playSound("snd_eren_swing" + UnityEngine.Random.Range(1, 3));
                        }
                        foreach (RaycastHit hit in this.checkHitCapsule(this.checkHitCapsuleStart.position, this.checkHitCapsuleEnd.position, this.checkHitCapsuleR))
                        {
                            GameObject gameObject = hit.collider.gameObject;
                            if (gameObject.tag == "Player")
                            {
                                this.killPlayer(gameObject.GetComponent<HERO>());
                            }
                            if (gameObject.tag == "erenHitbox")
                            {
                                TITAN_EREN te = gameObject.transform.root.gameObject.GetComponent<TITAN_EREN>();
                                if (this.attackAnimation == "combo_1")
                                {
                                    if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && PhotonNetwork.isMasterClient)
                                    {
                                        te.hitByFTByServer(1);
                                    }
                                }
                                else if (this.attackAnimation == "combo_2")
                                {
                                    if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && PhotonNetwork.isMasterClient)
                                    {
                                        te.hitByFTByServer(2);
                                    }
                                }
                                else if (((this.attackAnimation == "combo_3") && (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)) && PhotonNetwork.isMasterClient)
                                {
                                    te.hitByFTByServer(3);
                                }
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
                    if (((this.attackAnimation == "jumpCombo_1") && (baseA["attack_" + this.attackAnimation].normalizedTime >= 0.65f)) && (!this.startJump && (this.myHero != null)))
                    {
                        this.startJump = true;
                        float y = this.myHero.rigidbody.velocity.y;
                        float num6 = -20f;
                        float gravity = this.gravity;
                        float num8 = tfind("Amarture/Core/Controller_Body/hip/spine/chest/neck").position.y;
                        float num9 = (num6 - gravity) * 0.5f;
                        float num10 = y;
                        float num11 = this.myHero.transform.position.y - num8;
                        float num12 = Mathf.Abs((float) ((Mathf.Sqrt((num10 * num10) - ((4f * num9) * num11)) - num10) / (2f * num9)));
                        Vector3 vector2 = (Vector3) ((this.myHero.transform.position + (this.myHero.rigidbody.velocity * num12)) + ((((Vector3.up * 0.5f) * num6) * num12) * num12));
                        float num13 = vector2.y;
                        if ((num11 < 0f) || ((num13 - num8) < 0f))
                        {
                            this.idle(0f);
                            num12 = 0.5f;
                            vector2 = baseT.position + ((Vector3) ((num8 + 5f) * Vector3.up));
                            num13 = vector2.y;
                        }
                        float num14 = num13 - num8;
                        float num15 = Mathf.Sqrt((2f * num14) / this.gravity);
                        float num16 = (this.gravity * num15) + 20f;
                        num16 = Mathf.Clamp(num16, 20f, 90f);
                        Vector3 vector3 = (Vector3) ((vector2 - baseT.position) / num12);
                        this.abnorma_jump_bite_horizon_v = new Vector3(vector3.x, 0f, vector3.z);
                        Vector3 velocity = baseR.velocity;
                        Vector3 vector5 = new Vector3(this.abnorma_jump_bite_horizon_v.x, num16, this.abnorma_jump_bite_horizon_v.z);
                        if (vector5.magnitude > 90f)
                        {
                            vector5 = (Vector3) (vector5.normalized * 90f);
                        }
                        Vector3 force = vector5 - velocity;
                        baseR.AddForce(force, ForceMode.VelocityChange);
                        float num17 = Vector2.Angle(new Vector2(baseT.position.x, baseT.position.z), new Vector2(this.myHero.transform.position.x, this.myHero.transform.position.z));
                        num17 = Mathf.Atan2(this.myHero.transform.position.x - baseT.position.x, this.myHero.transform.position.z - baseT.position.z) * 57.29578f;
                        baseG.transform.rotation = Quaternion.Euler(0f, num17, 0f);
                    }
                    if (this.attackAnimation != "jumpCombo_3")
                    {
                        if (baseA["attack_" + this.attackAnimation].normalizedTime >= 1f)
                        {
                            if (this.nextAttackAnimation == null)
                            {
                                this.findNearestHero();
                                this.idle(0f);
                            }
                            else
                            {
                                this.attack(this.nextAttackAnimation);
                                if (this.eren != null)
                                {
                                    baseG.transform.rotation = Quaternion.Euler(0f, Quaternion.LookRotation(this.eren.transform.position - baseT.position).eulerAngles.y, 0f);
                                }
                            }
                        }
                    }
                    else if ((baseA["attack_" + this.attackAnimation].normalizedTime >= 1f) && this.IsGrounded())
                    {
                        this.attack("jumpCombo_4");
                    }
                }
                else if (this.state == "grab")
                {
                    if (((baseA["attack_grab_" + this.attackAnimation].normalizedTime >= this.attackCheckTimeA) && (baseA["attack_grab_" + this.attackAnimation].normalizedTime <= this.attackCheckTimeB)) && (grabbedTargetT == null))
                    {
                      HERO hero = this.checkIfHitHand(this.currentGrabHand);
                      if (hero != null)
                        {
                            if (this.isGrabHandLeft)
                            {
                                this.eatSetL(hero);
                                grabbedTargetT = hero;
                            }
                            else
                            {
                                this.eatSet(hero);
                                grabbedTargetT = hero;
                            }
                        }
                    }
                    if ((baseA["attack_grab_" + this.attackAnimation].normalizedTime > this.attackCheckTime) && (grabbedTargetT != null))
                    {
                        this.justEatHero(grabbedTargetT, this.currentGrabHand);
                        grabbedTargetT = null;
                    }
                    if (baseA["attack_grab_" + this.attackAnimation].normalizedTime >= 1f)
                    {
                        this.idle(0f);
                    }
                }
                else if (this.state == "turn")
                {
                    baseG.transform.rotation = Quaternion.Lerp(baseG.transform.rotation, Quaternion.Euler(0f, this.desDeg, 0f), (Time.deltaTime * Mathf.Abs(this.turnDeg)) * 0.1f);
                    if (baseA[this.turnAnimation].normalizedTime >= 1f)
                    {
                        this.idle(0f);
                    }
                }
                else if (this.state == "chase")
                {
                    if (((((this.eren == null) || (this.myDistance >= 35f)) || !this.attackTarget(this.myHero)) && (((this.getNearestHeroDistance() >= 50f) || (UnityEngine.Random.Range(0, 100) >= 20)) || !this.attackTarget(this.getNearestHero()))) && (this.myDistance < (this.attackDistance - 15f)))
                    {
                        this.idle(UnityEngine.Random.Range((float) 0.05f, (float) 0.2f));
                    }
                }
                else if (this.state == "turn180")
                {
                    if (baseA[this.turnAnimation].normalizedTime >= 1f)
                    {
                        baseG.transform.rotation = Quaternion.Euler(baseG.transform.rotation.eulerAngles.x, baseG.transform.rotation.eulerAngles.y + 180f, baseG.transform.rotation.eulerAngles.z);
                        this.idle(0f);
                        this.playAnimation("idle");
                    }
                }
                else if (this.state == "anklehurt")
                {
                    if (baseA["legHurt"].normalizedTime >= 1f)
                    {
                        this.crossFade("legHurt_loop", 0.2f);
                    }
                    if (baseA["legHurt_loop"].normalizedTime >= 3f)
                    {
                        this.crossFade("legHurt_getup", 0.2f);
                    }
                    if (baseA["legHurt_getup"].normalizedTime >= 1f)
                    {
                        this.idle(0f);
                        this.playAnimation("idle");
                    }
                }
            }
        }
    }

    public void updateLabel()
    {
        if ((this.healthLabel != null) && labeld.isVisible)
        {
            this.healthLabel.transform.LookAt(((Vector3) (2f * this.healthLabel.transform.position)) - Camera.main.transform.position);
        }
    }

}

