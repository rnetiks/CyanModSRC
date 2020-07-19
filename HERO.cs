using ExitGames.Client.Photon;
using Photon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using Xft;
using CyanMod;

public class HERO : Photon.MonoBehaviour
{
    
    private HERO_STATE _state;
    private bool almostSingleHook;
    private string attackAnimation;
    private int attackLoop;
    private bool attackMove;
    private bool attackReleased;
    public AudioSource audio_ally;
    public AudioSource audio_hitwall;
    private GameObject badGuy;
    public Animation baseAnimation;
    public Rigidbody baseRigidBody;
    public Transform baseTransform;
    public bool bigLean;
    public float bombCD;
    public bool bombImmune;
    public float bombRadius;
    public float bombSpeed;
    public float bombTime;
    public float bombTimeMax;
    private float buffTime;
    public int bulletMAX = 7;
    public GameObject bulletLeft;
    public GameObject bulletRight;
    private bool buttonAttackRelease;
    public Dictionary<string, UISprite> cachedSprites;
    public float CameraMultiplier;
    public bool canJump = true;
    public GameObject checkBoxLeft;
    public GameObject checkBoxRight;
    public GameObject cross1;
    public GameObject cross2;
    public GameObject crossL1;
    public GameObject crossL2;
    public GameObject crossR1;
    public GameObject crossR2;
    public string currentAnimation;
    public int currentBladeNum = 5;
    public float currentBladeSta = 100f;
    private BUFF currentBuff;
    public Camera currentCamera;
    public IN_GAME_MAIN_CAMERA currentCameraT;
    public float currentGas = 100f;
    public float currentSpeed;
    public Vector3 currentV;
    private bool dashD;
    private Vector3 dashDirection;
    private bool dashL;
    private bool dashR;
    private float dashTime;
    private bool dashU;
    private Vector3 dashV;
    public bool detonate;
    private float dTapTime = -1f;
    private bool EHold;
    private GameObject eren_titan;
    private int escapeTimes = 1;
    private float facingDirection;
    private float flare1CD;
    private float flare2CD;
    private float flare3CD;
    private float flareTotalCD = 30f;
    private Transform forearmL;
    private Transform forearmR;
    public float gravity = 20f;
    private bool grounded;
    private GameObject gunDummy;
    private Vector3 gunTarget;
    private Transform handL;
    private Transform handR;
    private bool hasDied;
    public bool hasspawn;
    private bool hookBySomeOne = true;
    public GameObject hookRefL1;
    public GameObject hookRefL2;
    public GameObject hookRefR1;
    public GameObject hookRefR2;
    private bool hookSomeOne;
    private GameObject hookTarget;
    private float invincible = 3f;
    public bool isCannon;
    private bool isLaunchLeft;
    private bool isLaunchRight;
    private bool isLeftHandHooked;
    private bool isMounted;
    public bool isPhotonCamera;
    private bool isRightHandHooked;
    public float jumpHeight = 2f;
    private bool justGrounded;
    public GameObject LabelDistance;
    public Transform lastHook;
    private float launchElapsedTimeL;
    private float launchElapsedTimeR;
    private Vector3 launchForce;
    private Vector3 launchPointLeft;
    private Vector3 launchPointRight;
    private bool leanLeft;
    private bool leftArmAim;
    public XWeaponTrail leftbladetrail;
    public XWeaponTrail leftbladetrail2;
    public int leftBulletLeft = 7;
    public bool leftGunHasBullet = true;
    private float lTapTime = -1f;
    public GameObject maincamera;
    public float maxVelocityChange = 10f;
    public AudioSource meatDie;
    public Bomb myBomb;
    public GameObject myCannon;
    public Transform myCannonBase;
    public Transform myCannonPlayer;
    public CannonPropRegion myCannonRegion;
    public GROUP myGroup;
    private GameObject myHorse;
    public GameObject myNetWorkName;
    public float myScale = 1f;
    public int myTeam = 1;
    public List<TITAN> myTitans;
    private bool needLean;
    private Quaternion oldHeadRotation;
    private float originVM;
    private bool QHold;
    private string reloadAnimation = string.Empty;
    private bool rightArmAim;
    public XWeaponTrail rightbladetrail;
    public XWeaponTrail rightbladetrail2;
    public int rightBulletLeft = 7;
    public bool rightGunHasBullet = true;
    public AudioSource rope;
    private float rTapTime = -1f;
    public HERO_SETUP setup;
    public GameObject skillCD;
    public float skillCDDuration;
    public float skillCDLast;
    public float skillCDLastCannon;
    public string skillId;
    public string skillIDHUD;
    public AudioSource slash;
    public AudioSource slashHit;
    private ParticleSystem smoke_3dmg;
    private ParticleSystem sparks;
    public float speed = 10f;
    public GameObject speedFX;
    public GameObject speedFX1;
    private ParticleSystem speedFXPS;
    public bool spinning;
    private string standAnimation = "stand";
    private Quaternion targetHeadRotation;
    private Quaternion targetRotation;
    public Vector3 targetV;
    private bool throwedBlades;
    public bool titanForm;
    private GameObject titanWhoGrabMe;
    private int titanWhoGrabMeID;
    public int totalBladeNum = 5;
    public float totalBladeSta = 100f;
    public float totalGas = 100f;
    private Transform upperarmL;
    private Transform upperarmR;
    private float useGasSpeed = 0.2f;
    public bool useGun;
    private float uTapTime = -1f;
    private bool wallJump;
    private float wallRunTime;
    public int id;
    Transform maincameraT;
    public bool gastrail = true;
    public bool aim = true;
    public TextMesh myNetWorkNameT;
    public bool showsprites = true;
    public SmoothSyncMovement smoothSyncMovement;
    UILabel labeldist;
    Transform cross1T;
    Transform cross2T;
    Transform crossL1T;
    Transform crossL2T;
    Transform crossR1T;
    Transform crossR2T;
    Transform LabelDistanceT;
    Bullet bulletLeftT;
    Bullet bulletRightT;
    Transform bulletLeftTT;
    Transform bulletRightTT;
    PhotonView pviev;
    GameObject baseG;
    Transform baseGT;
    bool isSingle = IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE;
    bool isMulty = IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER;
    public static HERO myHero;
    bool isCheckedGas = false;

    bool bodyLEAN
    {
        get
        {
            return (int)FengGameManagerMKII.settings[281] == 0;
        }
    }
    private void applyForceToBody(GameObject GO, Vector3 v)
    {
        GO.rigidbody.AddForce(v);
        GO.rigidbody.AddTorque(UnityEngine.Random.Range((float)-10f, (float)10f), UnityEngine.Random.Range((float)-10f, (float)10f), UnityEngine.Random.Range((float)-10f, (float)10f));
    }

    public void attackAccordingToMouse()
    {
        if (Input.mousePosition.x < (Screen.width * 0.5))
        {
            this.attackAnimation = "attack2";
        }
        else
        {
            this.attackAnimation = "attack1";
        }
    }

    public void attackAccordingToTarget(Transform a)
    {
        Vector3 vector = a.position - this.baseTransform.position;
        float current = -Mathf.Atan2(vector.z, vector.x) * 57.29578f;
        float f = -Mathf.DeltaAngle(current, this.baseTransform.rotation.eulerAngles.y - 90f);
        if (((Mathf.Abs(f) < 90f) && (vector.magnitude < 6f)) && ((a.position.y <= (this.baseTransform.position.y + 2f)) && (a.position.y >= (this.baseTransform.position.y - 5f))))
        {
            this.attackAnimation = "attack4";
        }
        else if (f > 0f)
        {
            this.attackAnimation = "attack1";
        }
        else
        {
            this.attackAnimation = "attack2";
        }
    }

    private void Awake()
    {
        this.cache();
        this.setup = baseG.GetComponent<HERO_SETUP>();
        this.baseRigidBody.freezeRotation = true;
        this.baseRigidBody.useGravity = false;
        this.handL = this.baseTransform.Find("Amarture/Controller_Body/hip/spine/chest/shoulder_L/upper_arm_L/forearm_L/hand_L");
        this.handR = this.baseTransform.Find("Amarture/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R");
        this.forearmL = this.baseTransform.Find("Amarture/Controller_Body/hip/spine/chest/shoulder_L/upper_arm_L/forearm_L");
        this.forearmR = this.baseTransform.Find("Amarture/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R");
        this.upperarmL = this.baseTransform.Find("Amarture/Controller_Body/hip/spine/chest/shoulder_L/upper_arm_L");
        this.upperarmR = this.baseTransform.Find("Amarture/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R");

    }

    public void cache()
    {
        baseG = base.gameObject;
        baseGT = baseG.transform;
        this.maincamera = CyanMod.CachingsGM.Find("MainCamera");
        this.baseAnimation = base.animation;
        maincameraT = maincamera.transform;
        pviev = base.photonView;
        if (this.pviev != null)
        {
            this.baseTransform = this.pviev.transform;
            this.baseRigidBody = this.pviev.rigidbody;
        }
        else
        {
            this.baseTransform = base.transform;
            this.baseRigidBody = base.rigidbody;
        }
        smoothSyncMovement = baseG.GetComponent<SmoothSyncMovement>();
        if ((isSingle) || pviev.isMine)
        {
            this.cross1 = CyanMod.CachingsGM.Find("cross1");
            this.cross2 = CyanMod.CachingsGM.Find("cross2");
            cross1T = cross1.transform;
            cross2T = cross2.transform;
            if ((int)FengGameManagerMKII.settings[289] == 0)
            {
                this.crossL1 = CyanMod.CachingsGM.Find("crossL1");
                this.crossL2 = CyanMod.CachingsGM.Find("crossL2");
                this.crossR1 = CyanMod.CachingsGM.Find("crossR1");
                this.crossR2 = CyanMod.CachingsGM.Find("crossR2");
                crossL1T = crossL1.transform;
                crossL2T = crossL2.transform;
                crossR1T = crossR1.transform;
                crossR2T = crossR2.transform;
            }
            this.LabelDistance = CyanMod.CachingsGM.Find("LabelDistance");
            LabelDistanceT = LabelDistance.transform;
            labeldist = LabelDistance.GetComponent<UILabel>();
            this.cachedSprites = new Dictionary<string, UISprite>();
            foreach (GameObject obj2 in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
            {
                UISprite sprite = obj2.GetComponent<UISprite>();
                if ((sprite != null) && obj2.activeInHierarchy)
                {
                    string name = obj2.name;
                    if (!((((name.Contains("blade") || name.Contains("bullet")) || (name.Contains("gas") || name.Contains("flare"))) || name.Contains("skill_cd")) ? this.cachedSprites.ContainsKey(name) : true))
                    {
                        this.cachedSprites.Add(name, sprite);
                    }
                }
            }
            ColorSprite();
        }
        head = this.baseTransform.Find("Amarture/Controller_Body/hip/spine/chest/neck/head");
    }
    public void ColorSprite()
    {
        Color color_blue = (Color)FengGameManagerMKII.settings[356];
        Color black = (Color)FengGameManagerMKII.settings[357];
        foreach (var s in cachedSprites)
        {
            string strs = s.Key;
            if ((strs.StartsWith("skill_cd") && strs != "skill_cd_bottom") || strs.StartsWith("blader") || strs.StartsWith("bladel") || strs == ("gasL") || strs == ("gasR") || strs.StartsWith("flareg") || strs.StartsWith("bullet"))
            {
                s.Value.color = color_blue;
            }
            else if (strs == "skill_cd_bottom" || strs == ("gasL1") || strs == ("gasR1") || strs == "bladeCL" || strs == "bladeCR")
            {
                s.Value.color = black;
            }
        }
    }
    private void OnDestroy()
    {
        if (isSingle || base.photonView.isMine)
        {
            myHero = null;
        }
        if (this.myNetWorkName != null)
        {
            UnityEngine.Object.Destroy(this.myNetWorkName);
        }
        if (this.gunDummy != null)
        {
            UnityEngine.Object.Destroy(this.gunDummy);
        }
        if (isMulty)
        {
            this.releaseIfIHookSb();
        }
        if (FengGameManagerMKII.instance != null)
        {
            FengGameManagerMKII.instance.removeHero(this);
        }
        if ((isMulty) && pviev.isMine)
        {
            Vector3 vector = (Vector3)(Vector3.up * 5000f);
            this.cross1T.localPosition = vector;
            this.cross2T.localPosition = vector;
            if (crossL1T != null)
            {
                this.crossL1T.localPosition = vector;
                this.crossL2T.localPosition = vector;
                this.crossR1T.localPosition = vector;
                this.crossR2T.localPosition = vector;
            }
            this.LabelDistance.transform.localPosition = vector;
        }
        if (this.setup.part_cape != null)
        {
            ClothFactory.DisposeObject(this.setup.part_cape);
        }
        if (this.setup.part_hair_1 != null)
        {
            ClothFactory.DisposeObject(this.setup.part_hair_1);
        }
        if (this.setup.part_hair_2 != null)
        {
            ClothFactory.DisposeObject(this.setup.part_hair_2);
        }
    }
    public void backToHuman()
    {
        smoothSyncMovement.disabled = false;
        this.baseRigidBody.velocity = Vector3.zero;
        this.titanForm = false;
        this.ungrabbed();
        this.falseAttack();
        this.skillCDDuration = this.skillCDLast;
        currentCameraT.setMainObject(baseG, true, false);
        if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)
        {
            pviev.RPC("backToHumanRPC", PhotonTargets.Others, new object[0]);
        }
    }
    public void name_remove()
    {
        if (myNetWorkName != null)
        {
            Destroy(myNetWorkName);
        }
    }
    [RPC]
    private void backToHumanRPC()
    {
        this.titanForm = false;
        this.eren_titan = null;
        smoothSyncMovement.disabled = false;
    }

    [RPC]
    public void badGuyReleaseMe()
    {
        this.hookBySomeOne = false;
        this.badGuy = null;
    }

    [RPC]
    public void blowAway(Vector3 force)
    {
        if ((isSingle) || pviev.isMine)
        {
            this.baseRigidBody.AddForce(force, ForceMode.Impulse);
            this.baseTransform.LookAt(this.baseTransform.position);
        }
    }

    private void bodyLean()
    {
        if ((isSingle) || pviev.isMine)
        {
            float z = 0f;
            this.needLean = false;
            if ((!this.useGun && (this.state == HERO_STATE.Attack)) && ((this.attackAnimation != "attack3_1") && (this.attackAnimation != "attack3_2")))
            {
                float y = this.baseRigidBody.velocity.y;
                float x = this.baseRigidBody.velocity.x;
                float num4 = this.baseRigidBody.velocity.z;
                float num5 = Mathf.Sqrt((x * x) + (num4 * num4));
                float num6 = Mathf.Atan2(y, num5) * 57.29578f;
                this.targetRotation = Quaternion.Euler(-num6 * (1f - (Vector3.Angle(this.baseRigidBody.velocity, this.baseTransform.forward) / 90f)), this.facingDirection, 0f);
                if ((this.isLeftHandHooked && (this.bulletLeft != null)) || (this.isRightHandHooked && (this.bulletRight != null)))
                {
                    this.baseTransform.rotation = this.targetRotation;
                }
            }
            else
            {
                if ((this.isLeftHandHooked && (this.bulletLeft != null)) && (this.isRightHandHooked && (this.bulletRight != null)))
                {
                    if (this.almostSingleHook)
                    {
                        this.needLean = true;
                        z = this.getLeanAngle(bulletRightTT.position, true);
                    }
                }
                else if (this.isLeftHandHooked && (this.bulletLeft != null))
                {
                    this.needLean = true;
                    z = this.getLeanAngle(bulletLeftTT.position, true);
                }
                else if (this.isRightHandHooked && (this.bulletRight != null))
                {
                    this.needLean = true;
                    z = this.getLeanAngle(bulletRightTT.position, false);
                }
                if (this.needLean)
                {
                    float a = 0f;
                    if (!this.useGun && (this.state != HERO_STATE.Attack))
                    {
                        a = this.currentSpeed * 0.1f;
                        a = Mathf.Min(a, 20f);
                    }
                    this.targetRotation = Quaternion.Euler(-a, this.facingDirection, z);
                }
                else if (this.state != HERO_STATE.Attack)
                {
                    this.targetRotation = Quaternion.Euler(0f, this.facingDirection, 0f);
                }
            }
        }
    }

    public void bombInit()
    {
        this.skillIDHUD = this.skillId;
        this.skillCDDuration = this.skillCDLast;
        if (RCSettings.bombMode == 1)
        {
            int num = (int)FengGameManagerMKII.settings[250];
            int num2 = (int)FengGameManagerMKII.settings[0xfb];
            int num3 = (int)FengGameManagerMKII.settings[0xfc];
            int num4 = (int)FengGameManagerMKII.settings[0xfd];
            if ((num < 0) || (num > 10))
            {
                num = 5;
                FengGameManagerMKII.settings[250] = 5;
            }
            if ((num2 < 0) || (num2 > 10))
            {
                num2 = 5;
                FengGameManagerMKII.settings[0xfb] = 5;
            }
            if ((num3 < 0) || (num3 > 10))
            {
                num3 = 5;
                FengGameManagerMKII.settings[0xfc] = 5;
            }
            if ((num4 < 0) || (num4 > 10))
            {
                num4 = 5;
                FengGameManagerMKII.settings[0xfd] = 5;
            }
            if ((((num + num2) + num3) + num4) > 20)
            {
                num = 5;
                num2 = 5;
                num3 = 5;
                num4 = 5;
                FengGameManagerMKII.settings[250] = 5;
                FengGameManagerMKII.settings[0xfb] = 5;
                FengGameManagerMKII.settings[0xfc] = 5;
                FengGameManagerMKII.settings[0xfd] = 5;
            }
            this.bombTimeMax = ((num2 * 60f) + 200f) / ((num3 * 60f) + 200f);
            this.bombRadius = (num * 4f) + 20f;
            this.bombCD = (num4 * -0.4f) + 5f;
            this.bombSpeed = (num3 * 60f) + 200f;
            PhotonNetwork.player.RCBombR = (float)FengGameManagerMKII.settings[0xf6];
            PhotonNetwork.player.RCBombG = (float)FengGameManagerMKII.settings[0xf7];
            PhotonNetwork.player.RCBombB = (float)FengGameManagerMKII.settings[0xf8];
            PhotonNetwork.player.RCBombA = (float)FengGameManagerMKII.settings[0xf9];
            PhotonNetwork.player.RCBombRadius = this.bombRadius;

            this.skillId = "bomb";
            this.skillIDHUD = "armin";
            this.skillCDLast = this.bombCD;
            this.skillCDDuration = 10f;
            if (FengGameManagerMKII.instance.roundTime > 10f)
            {
                this.skillCDDuration = 5f;
            }
        }
    }

    private void breakApart(Vector3 v, bool isBite)
    {

    }

    private void breakApart2(Vector3 v, bool isBite)
    {
        GameObject obj2;
        GameObject obj3;
        GameObject obj4;
        GameObject obj5;
        GameObject obj6;
        GameObject IRlll = (Cach.Character_parts_AOTTG_HERO_body != null ? Cach.Character_parts_AOTTG_HERO_body : Cach.Character_parts_AOTTG_HERO_body = (GameObject)Resources.Load("Character_parts/AOTTG_HERO_body"));
        GameObject obj7 = (GameObject)UnityEngine.Object.Instantiate(IRlll, base.transform.position, base.transform.rotation);
        obj7.gameObject.GetComponent<HERO_SETUP>().myCostume = this.setup.myCostume;
        obj7.GetComponent<HERO_SETUP>().isDeadBody = true;
        obj7.GetComponent<HERO_DEAD_BODY_SETUP>().init(this.currentAnimation, baseAnimation[this.currentAnimation].normalizedTime, BODY_PARTS.ARM_R);
        if (!isBite)
        {
            GameObject gO = (GameObject)UnityEngine.Object.Instantiate(IRlll, base.transform.position, base.transform.rotation);
            GameObject obj9 = (GameObject)UnityEngine.Object.Instantiate(IRlll, base.transform.position, base.transform.rotation);
            GameObject obj10 = (GameObject)UnityEngine.Object.Instantiate(IRlll, base.transform.position, base.transform.rotation);
            gO.gameObject.GetComponent<HERO_SETUP>().myCostume = this.setup.myCostume;
            obj9.gameObject.GetComponent<HERO_SETUP>().myCostume = this.setup.myCostume;
            obj10.gameObject.GetComponent<HERO_SETUP>().myCostume = this.setup.myCostume;
            gO.GetComponent<HERO_SETUP>().isDeadBody = true;
            obj9.GetComponent<HERO_SETUP>().isDeadBody = true;
            obj10.GetComponent<HERO_SETUP>().isDeadBody = true;
            gO.GetComponent<HERO_DEAD_BODY_SETUP>().init(this.currentAnimation, baseAnimation[this.currentAnimation].normalizedTime, BODY_PARTS.UPPER);
            obj9.GetComponent<HERO_DEAD_BODY_SETUP>().init(this.currentAnimation, baseAnimation[this.currentAnimation].normalizedTime, BODY_PARTS.LOWER);
            obj10.GetComponent<HERO_DEAD_BODY_SETUP>().init(this.currentAnimation, baseAnimation[this.currentAnimation].normalizedTime, BODY_PARTS.ARM_L);
            this.applyForceToBody(gO, v);
            this.applyForceToBody(obj9, v);
            this.applyForceToBody(obj10, v);
            if ((isSingle) || base.photonView.isMine)
            {
                this.currentCameraT.setMainObject(gO, false, false);
            }
        }
        else if ((isSingle) || base.photonView.isMine)
        {
            this.currentCameraT.setMainObject(obj7, false, false);
        }
        this.applyForceToBody(obj7, v);
        Transform transform = handL.transform;
        Transform transform2 = handR.transform;
        if (this.useGun)
        {
            obj2 = (GameObject)UnityEngine.Object.Instantiate(Cach.Character_parts_character_gun_l != null ? Cach.Character_parts_character_gun_l : Cach.Character_parts_character_gun_l = (GameObject)Resources.Load("Character_parts/character_gun_l"), transform.position, transform.rotation);
            obj3 = (GameObject)UnityEngine.Object.Instantiate(Cach.character_gun_r != null ? Cach.character_gun_r : Cach.character_gun_r = (GameObject)Resources.Load("Character_parts/character_gun_r"), transform2.position, transform2.rotation);
            obj4 = (GameObject)UnityEngine.Object.Instantiate(Cach.character_3dmg_2 != null ? Cach.character_3dmg_2 : Cach.character_3dmg_2 = (GameObject)Resources.Load("Character_parts/character_3dmg_2"), base.transform.position, base.transform.rotation);
            obj5 = (GameObject)UnityEngine.Object.Instantiate(Cach.character_gun_mag_l != null ? Cach.character_gun_mag_l : Cach.character_gun_mag_l = (GameObject)Resources.Load("Character_parts/character_gun_mag_l"), base.transform.position, base.transform.rotation);
            obj6 = (GameObject)UnityEngine.Object.Instantiate(Cach.character_gun_mag_r != null ? Cach.character_gun_mag_r : Cach.character_gun_mag_r = (GameObject)Resources.Load("Character_parts/character_gun_mag_r"), base.transform.position, base.transform.rotation);

        }
        else
        {
            obj2 = (GameObject)UnityEngine.Object.Instantiate(Cach.character_blade_l != null ? Cach.character_blade_l : Cach.character_blade_l = (GameObject)Resources.Load("Character_parts/character_blade_l"), transform.position, transform.rotation);
            obj3 = (GameObject)UnityEngine.Object.Instantiate(Cach.character_blade_r != null ? Cach.character_blade_r : Cach.character_blade_r = (GameObject)Resources.Load("Character_parts/character_blade_r"), transform2.position, transform2.rotation);
            obj4 = (GameObject)UnityEngine.Object.Instantiate(Cach.character_3dmg != null ? Cach.character_3dmg : Cach.character_3dmg = (GameObject)Resources.Load("Character_parts/character_3dmg"), base.transform.position, base.transform.rotation);
            obj5 = (GameObject)UnityEngine.Object.Instantiate(Cach.character_3dmg_gas_l != null ? Cach.character_3dmg_gas_l : Cach.character_3dmg_gas_l = (GameObject)Resources.Load("Character_parts/character_3dmg_gas_l"), base.transform.position, base.transform.rotation);
            obj6 = (GameObject)UnityEngine.Object.Instantiate(Cach.character_3dmg_gas_r != null ? Cach.character_3dmg_gas_r : Cach.character_3dmg_gas_r = (GameObject)Resources.Load("Character_parts/character_3dmg_gas_r"), base.transform.position, base.transform.rotation);
        }
        obj2.renderer.material = CharacterMaterials.materials[this.setup.myCostume._3dmg_texture];
        obj3.renderer.material = CharacterMaterials.materials[this.setup.myCostume._3dmg_texture];
        obj4.renderer.material = CharacterMaterials.materials[this.setup.myCostume._3dmg_texture];
        obj5.renderer.material = CharacterMaterials.materials[this.setup.myCostume._3dmg_texture];
        obj6.renderer.material = CharacterMaterials.materials[this.setup.myCostume._3dmg_texture];
        this.applyForceToBody(obj2, v);
        this.applyForceToBody(obj3, v);
        this.applyForceToBody(obj4, v);
        this.applyForceToBody(obj5, v);
        this.applyForceToBody(obj6, v);
    }

    private void bufferUpdate()
    {
        if (this.buffTime > 0f)
        {
            this.buffTime -= Time.deltaTime;
            if (this.buffTime <= 0f)
            {
                this.buffTime = 0f;
                if ((this.currentBuff == BUFF.SpeedUp) && baseAnimation.IsPlaying("run_sasha"))
                {
                    this.crossFade("run", 0.1f);
                }
                this.currentBuff = BUFF.NoBuff;
            }
        }
    }

    private void calcFlareCD()
    {
        if (this.flare1CD > 0f)
        {
            this.flare1CD -= Time.deltaTime;
            if (this.flare1CD < 0f)
            {
                this.flare1CD = 0f;
            }
        }
        if (this.flare2CD > 0f)
        {
            this.flare2CD -= Time.deltaTime;
            if (this.flare2CD < 0f)
            {
                this.flare2CD = 0f;
            }
        }
        if (this.flare3CD > 0f)
        {
            this.flare3CD -= Time.deltaTime;
            if (this.flare3CD < 0f)
            {
                this.flare3CD = 0f;
            }
        }
    }

    private void calcSkillCD()
    {
        if (this.skillCDDuration > 0f)
        {
            this.skillCDDuration -= Time.deltaTime;
            if (this.skillCDDuration < 0f)
            {
                this.skillCDDuration = 0f;
            }
        }
    }

    private float CalculateJumpVerticalSpeed()
    {
        return Mathf.Sqrt((2f * this.jumpHeight) * this.gravity);
    }

    private void changeBlade()
    {
        if ((!this.useGun || this.grounded) || (FengGameManagerMKII.lvlInfo.type != GAMEMODE.PVP_AHSS))
        {
            this.state = HERO_STATE.ChangeBlade;
            this.throwedBlades = false;
            if (this.useGun)
            {
                if (!this.leftGunHasBullet && !this.rightGunHasBullet)
                {
                    if (this.grounded)
                    {
                        this.reloadAnimation = "AHSS_gun_reload_both";
                    }
                    else
                    {
                        this.reloadAnimation = "AHSS_gun_reload_both_air";
                    }
                }
                else if (!this.leftGunHasBullet)
                {
                    if (this.grounded)
                    {
                        this.reloadAnimation = "AHSS_gun_reload_l";
                    }
                    else
                    {
                        this.reloadAnimation = "AHSS_gun_reload_l_air";
                    }
                }
                else if (!this.rightGunHasBullet)
                {
                    if (this.grounded)
                    {
                        this.reloadAnimation = "AHSS_gun_reload_r";
                    }
                    else
                    {
                        this.reloadAnimation = "AHSS_gun_reload_r_air";
                    }
                }
                else
                {
                    if (this.grounded)
                    {
                        this.reloadAnimation = "AHSS_gun_reload_both";
                    }
                    else
                    {
                        this.reloadAnimation = "AHSS_gun_reload_both_air";
                    }
                    this.rightGunHasBullet = false;
                    this.leftGunHasBullet = false;
                }
                this.crossFade(this.reloadAnimation, 0.05f);
            }
            else
            {
                if (!this.grounded)
                {
                    this.reloadAnimation = "changeBlade_air";
                }
                else
                {
                    this.reloadAnimation = "changeBlade";
                }
                this.crossFade(this.reloadAnimation, 0.1f);
            }
        }
    }

    private void checkDashDoubleTap()
    {
        if (this.uTapTime >= 0f)
        {
            this.uTapTime += Time.deltaTime;
            if (this.uTapTime > 0.2f)
            {
                this.uTapTime = -1f;
            }
        }
        if (this.dTapTime >= 0f)
        {
            this.dTapTime += Time.deltaTime;
            if (this.dTapTime > 0.2f)
            {
                this.dTapTime = -1f;
            }
        }
        if (this.lTapTime >= 0f)
        {
            this.lTapTime += Time.deltaTime;
            if (this.lTapTime > 0.2f)
            {
                this.lTapTime = -1f;
            }
        }
        if (this.rTapTime >= 0f)
        {
            this.rTapTime += Time.deltaTime;
            if (this.rTapTime > 0.2f)
            {
                this.rTapTime = -1f;
            }
        }
        if (FengGameManagerMKII.inputRC.isInputCustomKeyDown(InputCode.custom_up))
        {
            if (this.uTapTime == -1f)
            {
                this.uTapTime = 0f;
            }
            if (this.uTapTime != 0f)
            {
                this.dashU = true;
            }
        }
        if (FengGameManagerMKII.inputRC.isInputCustomKeyDown(InputCode.custom_down))
        {
            if (this.dTapTime == -1f)
            {
                this.dTapTime = 0f;
            }
            if (this.dTapTime != 0f)
            {
                this.dashD = true;
            }
        }
        if (FengGameManagerMKII.inputRC.isInputCustomKeyDown(InputCode.custom_left))
        {
            if (this.lTapTime == -1f)
            {
                this.lTapTime = 0f;
            }
            if (this.lTapTime != 0f)
            {
                this.dashL = true;
            }
        }
        if (FengGameManagerMKII.inputRC.isInputCustomKeyDown(InputCode.custom_right))
        {
            if (this.rTapTime == -1f)
            {
                this.rTapTime = 0f;
            }
            if (this.rTapTime != 0f)
            {
                this.dashR = true;
            }
        }
    }

    private void checkDashRebind()
    {
        if (FengGameManagerMKII.inputRC.isInputHuman(InputCodeRC.dash))
        {
            if (FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_up))
            {
                this.dashU = true;
            }
            else if (FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_down))
            {
                this.dashD = true;
            }
            else if (FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_left))
            {
                this.dashL = true;
            }
            else if (FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_right))
            {
                this.dashR = true;
            }
        }
    }

    public void checkTitan()
    {
        int count;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        LayerMask mask = ((int)1) << LayerMask.NameToLayer("PlayerAttackBox");
        LayerMask mask2 = ((int)1) << LayerMask.NameToLayer("Ground");
        LayerMask mask3 = ((int)1) << LayerMask.NameToLayer("EnemyBox");
        LayerMask mask4 = (mask | mask2) | mask3;
        RaycastHit[] hitArray = Physics.RaycastAll(ray, 180f, mask4.value);
        List<RaycastHit> list = new List<RaycastHit>();
        List<TITAN> list2 = new List<TITAN>();
        for (count = 0; count < hitArray.Length; count++)
        {
            RaycastHit item = hitArray[count];
            list.Add(item);
        }
        list.Sort((x, y) => x.distance.CompareTo(y.distance));
        float num2 = 180f;
        for (count = 0; count < list.Count; count++)
        {
            RaycastHit hit2 = list[count];
            GameObject gameObject = hit2.collider.gameObject;
            if (gameObject.layer == 0x10)
            {
                if (gameObject.name.Contains("PlayerDetectorRC"))
                {
                    RaycastHit hit3 = hit2 = list[count];
                    if (hit3.distance < num2)
                    {
                        num2 -= 60f;
                        if (num2 <= 60f)
                        {
                            count = list.Count;
                        }
                        TITAN component = gameObject.transform.root.gameObject.GetComponent<TITAN>();
                        if (component != null)
                        {
                            list2.Add(component);
                        }
                    }
                }
            }
            else
            {
                count = list.Count;
            }
        }
        for (count = 0; count < this.myTitans.Count; count++)
        {
            TITAN titan2 = this.myTitans[count];
            if (!list2.Contains(titan2))
            {
                titan2.isLook = false;
            }
        }
        for (count = 0; count < list2.Count; count++)
        {
            TITAN titan3 = list2[count];
            titan3.isLook = true;
        }
        this.myTitans = list2;
    }

    public void ClearPopup()
    {
        FengGameManagerMKII.instance.ShowHUDInfoCenter(string.Empty);
    }

    public void continueAnimation()
    {
        foreach (AnimationState state in baseAnimation)
        {
            if ((state != null) && (state.speed == 1f))
            {
                return;
            }
            state.speed = 1f;
        }
        this.customAnimationSpeed();
        this.playAnimation(this.currentPlayingClipName());
        if ((IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE) && pviev.isMine)
        {
            pviev.RPC("netContinueAnimation", PhotonTargets.Others, new object[0]);
        }
    }

    public void crossFade(string aniName, float time)
    {
        this.currentAnimation = aniName;
        baseAnimation.CrossFade(aniName, time);
        if (PhotonNetwork.connected && pviev.isMine)
        {
            object[] parameters = new object[] { aniName, time };
            pviev.RPC("netCrossFade", PhotonTargets.Others, parameters);
        }
    }

    public string currentPlayingClipName()
    {
        foreach (AnimationState state in baseAnimation)
        {
            if ((state != null) && baseAnimation.IsPlaying(state.name))
            {
                return state.name;
            }
        }
        return string.Empty;
    }

    private void customAnimationSpeed()
    {
        baseAnimation["attack5"].speed = 1.85f;
        baseAnimation["changeBlade"].speed = 1.2f;
        baseAnimation["air_release"].speed = 0.6f;
        baseAnimation["changeBlade_air"].speed = 0.8f;
        baseAnimation["AHSS_gun_reload_both"].speed = 0.38f;
        baseAnimation["AHSS_gun_reload_both_air"].speed = 0.5f;
        baseAnimation["AHSS_gun_reload_l"].speed = 0.4f;
        baseAnimation["AHSS_gun_reload_l_air"].speed = 0.5f;
        baseAnimation["AHSS_gun_reload_r"].speed = 0.4f;
        baseAnimation["AHSS_gun_reload_r_air"].speed = 0.5f;
    }

    private void dash(float horizontal, float vertical)
    {
        if (((this.dashTime <= 0f) && (this.currentGas > 0f)) && !this.isMounted)
        {
            this.useGas(this.totalGas * 0.04f);
            this.facingDirection = this.getGlobalFacingDirection(horizontal, vertical);
            this.dashV = this.getGlobaleFacingVector3(this.facingDirection);
            this.originVM = this.currentSpeed;
            Quaternion quaternion = Quaternion.Euler(0f, this.facingDirection, 0f);
            this.baseRigidBody.rotation = quaternion;
            this.targetRotation = quaternion;
            if (isSingle)
            {
                UnityEngine.Object.Instantiate(Cach.boost_smoke != null ? Cach.boost_smoke : Cach.boost_smoke = (GameObject)Resources.Load("FX/boost_smoke"), this.baseTransform.position, this.baseTransform.rotation);
            }
            else
            {
                PhotonNetwork.Instantiate("FX/boost_smoke", this.baseTransform.position, this.baseTransform.rotation, 0);
            }
            this.dashTime = 0.5f;
            this.crossFade("dash", 0.1f);
            baseAnimation["dash"].time = 0.1f;
            this.state = HERO_STATE.AirDodge;
            this.falseAttack();
            this.baseRigidBody.AddForce((Vector3)(this.dashV * 40f), ForceMode.VelocityChange);
        }
    }

    public void die(Vector3 v, bool isBite)
    {
        if (this.invincible <= 0f)
        {
            FengGameManagerMKII.instance.panelScore.AddDeath();
            if (this.titanForm && (this.eren_titan != null))
            {
                this.eren_titan.GetComponent<TITAN_EREN>().lifeTime = 0.1f;
            }
            if (this.bulletLeft != null)
            {
                bulletLeftT.removeMe();
            }
            if (this.bulletRight != null)
            {
                bulletRightT.removeMe();
            }

            if (((isSingle) || base.photonView.isMine) && !this.useGun)
            {
                DiactivateTrail(true);
            }
            this.breakApart2(v, isBite);
            this.currentCameraT.gameOver = true;
            FengGameManagerMKII.instance.gameLose2();
            this.falseAttack();
            this.hasDied = true;
            if ((int)FengGameManagerMKII.settings[330] == 0)
            {
                this.meatDie.Play();
                Transform transform = audio_die != null ? audio_die : audio_die = base.transform.Find("audio_die");
                transform.parent = null;
                transform.GetComponent<AudioSource>().Play();
            }

            if ((int)FengGameManagerMKII.settings[323] == 1)
            {
                IN_GAME_MAIN_CAMERA.instance.startSnapShot(base.transform.position, 0, null, 0.02f);
            }
            UnityEngine.Object.Destroy(base.gameObject);
        }
    }
    Transform audio_die;
    public void die2(Transform tf)
    {
        if (this.invincible <= 0f)
        {
            if (this.titanForm && (this.eren_titan != null))
            {
                this.eren_titan.GetComponent<TITAN_EREN>().lifeTime = 0.1f;
            }
            if (this.bulletLeft != null)
            {
                bulletLeftT.removeMe();
            }
            if (this.bulletRight != null)
            {
                bulletRightT.removeMe();
            }
            if ((int)FengGameManagerMKII.settings[330] == 0)
            {
                Transform transform = audio_die != null ? audio_die : audio_die = base.transform.Find("audio_die");
                transform.parent = null;
                transform.GetComponent<AudioSource>().Play();

                this.meatDie.Play();
            }
            this.currentCameraT.setMainObject(null, true, false);
            this.currentCameraT.gameOver = true;
            FengGameManagerMKII.instance.gameLose2();
            this.falseAttack();
            this.hasDied = true;
            GameObject obj2 = (GameObject)UnityEngine.Object.Instantiate(Cach.hitMeat2 != null ? Cach.hitMeat2 : Cach.hitMeat2 = (GameObject)Resources.Load("hitMeat2"));
            obj2.transform.position = this.baseTransform.position;
            UnityEngine.Object.Destroy(baseG);
        }
    }

    private void dodge(bool offTheWall = false)
    {
        if (((this.myHorse != null) && !this.isMounted) && (Vector3.Distance(this.myHorse.transform.position, this.baseTransform.position) < 15f))
        {
            this.getOnHorse();
        }
        else
        {
            this.state = HERO_STATE.GroundDodge;
            if (!offTheWall)
            {
                float num;
                float num2;
                if (FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_up))
                {
                    num = 1f;
                }
                else if (FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_down))
                {
                    num = -1f;
                }
                else
                {
                    num = 0f;
                }
                if (FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_left))
                {
                    num2 = -1f;
                }
                else if (FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_right))
                {
                    num2 = 1f;
                }
                else
                {
                    num2 = 0f;
                }
                float num3 = this.getGlobalFacingDirection(num2, num);
                if ((num2 != 0f) || (num != 0f))
                {
                    this.facingDirection = num3 + 180f;
                    this.targetRotation = Quaternion.Euler(0f, this.facingDirection, 0f);
                }
                this.crossFade("dodge", 0.1f);
            }
            else
            {
                this.playAnimation("dodge");
                this.playAnimationAt("dodge", 0.2f);
            }
            this.sparks.enableEmission = false;
        }
    }

    private void dodge2(bool offTheWall = false)
    {
        if ((!FengGameManagerMKII.inputRC.isInputHorse(InputCodeRC.horseMount) || (this.myHorse == null)) || (this.isMounted || (Vector3.Distance(this.myHorse.transform.position, this.baseTransform.position) >= 15f)))
        {
            this.state = HERO_STATE.GroundDodge;
            if (!offTheWall)
            {
                float num;
                float num2;
                if (FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_up))
                {
                    num = 1f;
                }
                else if (FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_down))
                {
                    num = -1f;
                }
                else
                {
                    num = 0f;
                }
                if (FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_left))
                {
                    num2 = -1f;
                }
                else if (FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_right))
                {
                    num2 = 1f;
                }
                else
                {
                    num2 = 0f;
                }
                float num3 = this.getGlobalFacingDirection(num2, num);
                if ((num2 != 0f) || (num != 0f))
                {
                    this.facingDirection = num3 + 180f;
                    this.targetRotation = Quaternion.Euler(0f, this.facingDirection, 0f);
                }
                this.crossFade("dodge", 0.1f);
            }
            else
            {
                this.playAnimation("dodge");
                this.playAnimationAt("dodge", 0.2f);
            }
            this.sparks.enableEmission = false;
        }
    }

    private void erenTransform()
    {
        this.skillCDDuration = this.skillCDLast;
        if (this.bulletLeft != null)
        {
            bulletLeftT.removeMe();
        }
        if (this.bulletRight != null)
        {
            bulletRightT.removeMe();
        }
        if (isSingle)
        {
            this.eren_titan = (GameObject)UnityEngine.Object.Instantiate(Cach.TITAN_EREN != null ? Cach.TITAN_EREN : Cach.TITAN_EREN = (GameObject)Resources.Load("TITAN_EREN"), this.baseTransform.position, this.baseTransform.rotation);
        }
        else
        {
            this.eren_titan = PhotonNetwork.Instantiate("TITAN_EREN", this.baseTransform.position, this.baseTransform.rotation, 0);
        }
        this.eren_titan.GetComponent<TITAN_EREN>().realBody = baseG;
        this.currentCameraT.flashBlind();
        this.currentCameraT.setMainObject(this.eren_titan, true, false);
        this.eren_titan.GetComponent<TITAN_EREN>().born();
        this.eren_titan.rigidbody.velocity = this.baseRigidBody.velocity;
        this.baseRigidBody.velocity = Vector3.zero;
        this.baseTransform.position = this.eren_titan.transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck").position;
        this.titanForm = true;
        if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)
        {
            object[] parameters = new object[] { this.eren_titan.GetPhotonView().viewID };
            pviev.RPC("whoIsMyErenTitan", PhotonTargets.Others, parameters);
        }
        if ((this.smoke_3dmg.enableEmission && (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)) && pviev.isMine)
        {
            object[] objArray2 = new object[] { false };
            pviev.RPC("net3DMGSMOKE", PhotonTargets.Others, objArray2);
        }
        this.smoke_3dmg.enableEmission = false;
    }

    private void escapeFromGrab()
    {
    }

    public void falseAttack()
    {
        this.attackMove = false;
        if (this.useGun)
        {
            if (!this.attackReleased)
            {
                this.continueAnimation();
                this.attackReleased = true;
            }
        }
        else
        {
            if ((isSingle) || pviev.isMine)
            {
                this.checkBoxLeft.GetComponent<TriggerColliderWeapon>().active_me = false;
                this.checkBoxRight.GetComponent<TriggerColliderWeapon>().active_me = false;
                this.checkBoxLeft.GetComponent<TriggerColliderWeapon>().clearHits();
                this.checkBoxRight.GetComponent<TriggerColliderWeapon>().clearHits();
                this.leftbladetrail.StopSmoothly(0.2f);
                this.rightbladetrail.StopSmoothly(0.2f);
                this.leftbladetrail2.StopSmoothly(0.2f);
                this.rightbladetrail2.StopSmoothly(0.2f);
            }
            this.attackLoop = 0;
            if (!this.attackReleased)
            {
                this.continueAnimation();
                this.attackReleased = true;
            }
        }
    }

    public void fillGas()
    {
        this.currentGas = this.totalGas;
    }

    private GameObject findNearestTitan()
    {
        GameObject obj2 = null;
        float positiveInfinity = float.PositiveInfinity;
        Vector3 position = this.baseTransform.position;
        foreach (GameObject obj3 in FengGameManagerMKII.instance.alltitans)
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

    private void FixedUpdate()
    {
        if ((!this.titanForm && !this.isCannon) && (!IN_GAME_MAIN_CAMERA.isPausing || (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)))
        {
            this.currentSpeed = this.baseRigidBody.velocity.magnitude;
            if ((isSingle) || pviev.isMine)
            {
                if ((!this.baseAnimation.IsPlaying("attack3_2") && !this.baseAnimation.IsPlaying("attack5")) && !this.baseAnimation.IsPlaying("special_petra"))
                {
                    this.baseRigidBody.rotation = Quaternion.Lerp(baseGT.rotation, this.targetRotation, Time.deltaTime * 6f);
                }
                if (this.state == HERO_STATE.Grab)
                {
                    this.baseRigidBody.AddForce(-this.baseRigidBody.velocity, ForceMode.VelocityChange);
                }
                else
                {
                    if (this.IsGrounded())
                    {
                        if (!this.grounded)
                        {
                            this.justGrounded = true;
                        }
                        this.grounded = true;
                    }
                    else
                    {
                        this.grounded = false;
                    }
                    if (this.hookSomeOne)
                    {
                        if (this.hookTarget != null)
                        {
                            Vector3 vector = this.hookTarget.transform.position - this.baseTransform.position;
                            float magnitude = vector.magnitude;
                            if (magnitude > 2f)
                            {
                                this.baseRigidBody.AddForce((Vector3)(((vector.normalized * Mathf.Pow(magnitude, 0.15f)) * 30f) - (this.baseRigidBody.velocity * 0.95f)), ForceMode.VelocityChange);
                            }
                        }
                        else
                        {
                            this.hookSomeOne = false;
                        }
                    }
                    else if (this.hookBySomeOne && (this.badGuy != null))
                    {
                        if (this.badGuy != null)
                        {
                            Vector3 vector2 = this.badGuy.transform.position - this.baseTransform.position;
                            float f = vector2.magnitude;
                            if (f > 5f)
                            {
                                this.baseRigidBody.AddForce((Vector3)((vector2.normalized * Mathf.Pow(f, 0.15f)) * 0.2f), ForceMode.Impulse);
                            }
                        }
                        else
                        {
                            this.hookBySomeOne = false;
                        }
                    }
                    float x = 0f;
                    float z = 0f;
                    if (!IN_GAME_MAIN_CAMERA.isTyping)
                    {
                        if (FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_up))
                        {
                            z = 1f;
                        }
                        else if (FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_down))
                        {
                            z = -1f;
                        }
                        else
                        {
                            z = 0f;
                        }
                        if (FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_left))
                        {
                            x = -1f;
                        }
                        else if (FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_right))
                        {
                            x = 1f;
                        }
                        else
                        {
                            x = 0f;
                        }
                    }
                    bool flag = false;
                    bool flag2 = false;
                    bool flag3 = false;
                    this.isLeftHandHooked = false;
                    this.isRightHandHooked = false;
                    if (this.isLaunchLeft)
                    {
                        if ((this.bulletLeft != null) && bulletLeftT.isHooked())
                        {
                            this.isLeftHandHooked = true;
                            Vector3 to = bulletLeftTT.position - this.baseTransform.position;
                            to.Normalize();
                            to = (Vector3)(to * 10f);
                            if (!this.isLaunchRight)
                            {
                                to = (Vector3)(to * 2f);
                            }
                            if ((Vector3.Angle(this.baseRigidBody.velocity, to) > 90f) && FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_jump))
                            {
                                flag2 = true;
                                flag = true;
                            }
                            if (!flag2)
                            {
                                this.baseRigidBody.AddForce(to);
                                if (Vector3.Angle(this.baseRigidBody.velocity, to) > 90f)
                                {
                                    this.baseRigidBody.AddForce((Vector3)(-this.baseRigidBody.velocity * 2f), ForceMode.Acceleration);
                                }
                            }
                            if (!bodyLEAN && !this.useGun)
                            {
                                this.facingDirection = Mathf.Atan2(to.x, to.z) * 57.29578f;
                                Quaternion quaternion = Quaternion.Euler(0f, this.facingDirection, 0f);
                                this.baseTransform.rotation = quaternion;
                                this.baseRigidBody.rotation = quaternion;
                            }
                        }
                        this.launchElapsedTimeL += Time.deltaTime;
                        if (this.QHold && (this.currentGas > 0f))
                        {
                            this.useGas(this.useGasSpeed * Time.deltaTime);
                        }
                        else if (this.launchElapsedTimeL > 0.3f)
                        {
                            this.isLaunchLeft = false;
                            if (this.bulletLeft != null)
                            {
                                bulletLeftT.disable();
                                this.releaseIfIHookSb();
                                this.bulletLeft = null;
                                flag2 = false;
                            }
                        }
                    }
                    if (this.isLaunchRight)
                    {
                        if ((this.bulletRight != null) && bulletRightT.isHooked())
                        {
                            this.isRightHandHooked = true;
                            Vector3 vector4 = bulletRightTT.position - this.baseTransform.position;
                            vector4.Normalize();
                            vector4 = (Vector3)(vector4 * 10f);
                            if (!this.isLaunchLeft)
                            {
                                vector4 = (Vector3)(vector4 * 2f);
                            }
                            if ((Vector3.Angle(this.baseRigidBody.velocity, vector4) > 90f) && FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_jump))
                            {
                                flag3 = true;
                                flag = true;
                            }
                            if (!flag3)
                            {
                                this.baseRigidBody.AddForce(vector4);
                                if (Vector3.Angle(this.baseRigidBody.velocity, vector4) > 90f)
                                {
                                    this.baseRigidBody.AddForce((Vector3)(-this.baseRigidBody.velocity * 2f), ForceMode.Acceleration);
                                }
                            }
                            if (!bodyLEAN && !this.useGun)
                            {
                                this.facingDirection = Mathf.Atan2(vector4.x, vector4.z) * 57.29578f;
                                Quaternion quaternion = Quaternion.Euler(0f, this.facingDirection, 0f);
                                this.baseTransform.rotation = quaternion;
                                this.baseRigidBody.rotation = quaternion;
                            }
                        }
                        this.launchElapsedTimeR += Time.deltaTime;
                        if (this.EHold && (this.currentGas > 0f))
                        {
                            this.useGas(this.useGasSpeed * Time.deltaTime);
                        }
                        else if (this.launchElapsedTimeR > 0.3f)
                        {
                            this.isLaunchRight = false;
                            if (this.bulletRight != null)
                            {
                                bulletRightT.disable();
                                this.releaseIfIHookSb();
                                this.bulletRight = null;
                                flag3 = false;
                            }
                        }
                    }
                    if (this.grounded)
                    {
                        Vector3 vector5;
                        Vector3 zero = Vector3.zero;
                        if (this.state == HERO_STATE.Attack)
                        {
                            if (this.attackAnimation == "attack5")
                            {
                                if ((this.baseAnimation[this.attackAnimation].normalizedTime > 0.4f) && (this.baseAnimation[this.attackAnimation].normalizedTime < 0.61f))
                                {
                                    this.baseRigidBody.AddForce((Vector3)(baseGT.forward * 200f));
                                }
                            }
                            else if (this.attackAnimation == "special_petra")
                            {
                                if ((this.baseAnimation[this.attackAnimation].normalizedTime > 0.35f) && (this.baseAnimation[this.attackAnimation].normalizedTime < 0.48f))
                                {
                                    this.baseRigidBody.AddForce((Vector3)(baseGT.forward * 200f));
                                }
                            }
                            else if (this.baseAnimation.IsPlaying("attack3_2"))
                            {
                                zero = Vector3.zero;
                            }
                            else if (this.baseAnimation.IsPlaying("attack1") || this.baseAnimation.IsPlaying("attack2"))
                            {
                                this.baseRigidBody.AddForce((Vector3)(baseGT.forward * 200f));
                            }
                            if (this.baseAnimation.IsPlaying("attack3_2"))
                            {
                                zero = Vector3.zero;
                            }
                        }
                        if (this.justGrounded)
                        {
                            if ((this.state != HERO_STATE.Attack) || (((this.attackAnimation != "attack3_1") && (this.attackAnimation != "attack5")) && (this.attackAnimation != "special_petra")))
                            {
                                if ((((this.state != HERO_STATE.Attack) && (x == 0f)) && ((z == 0f) && (this.bulletLeft == null))) && ((this.bulletRight == null) && (this.state != HERO_STATE.FillGas)))
                                {
                                    this.state = HERO_STATE.Land;
                                    this.crossFade("dash_land", 0.01f);
                                }
                                else
                                {
                                    this.buttonAttackRelease = true;
                                    if (((this.state != HERO_STATE.Attack) && (((this.baseRigidBody.velocity.x * this.baseRigidBody.velocity.x) + (this.baseRigidBody.velocity.z * this.baseRigidBody.velocity.z)) > ((this.speed * this.speed) * 1.5f))) && (this.state != HERO_STATE.FillGas))
                                    {
                                        this.state = HERO_STATE.Slide;
                                        this.crossFade("slide", 0.05f);
                                        this.facingDirection = Mathf.Atan2(this.baseRigidBody.velocity.x, this.baseRigidBody.velocity.z) * 57.29578f;
                                        this.targetRotation = Quaternion.Euler(0f, this.facingDirection, 0f);
                                        this.sparks.enableEmission = true;
                                    }
                                }
                            }
                            this.justGrounded = false;
                            zero = this.baseRigidBody.velocity;
                        }
                        if (((this.state == HERO_STATE.Attack) && (this.attackAnimation == "attack3_1")) && (this.baseAnimation[this.attackAnimation].normalizedTime >= 1f))
                        {
                            this.playAnimation("attack3_2");
                            this.resetAnimationSpeed();
                            vector5 = Vector3.zero;
                            this.baseRigidBody.velocity = vector5;
                            zero = vector5;
                            this.currentCameraT.startShake(0.2f, 0.3f, 0.95f);
                        }
                        if (this.state == HERO_STATE.GroundDodge)
                        {
                            if ((this.baseAnimation["dodge"].normalizedTime >= 0.2f) && (this.baseAnimation["dodge"].normalizedTime < 0.8f))
                            {
                                zero = (Vector3)((-this.baseTransform.forward * 2.4f) * this.speed);
                            }
                            if (this.baseAnimation["dodge"].normalizedTime > 0.8f)
                            {
                                zero = (Vector3)(this.baseRigidBody.velocity * 0.9f);
                            }
                        }
                        else if (this.state == HERO_STATE.Idle)
                        {
                            Vector3 vector7 = new Vector3(x, 0f, z);
                            float resultAngle = this.getGlobalFacingDirection(x, z);
                            zero = this.getGlobaleFacingVector3(resultAngle);
                            float num6 = (vector7.magnitude <= 0.95f) ? ((vector7.magnitude >= 0.25f) ? vector7.magnitude : 0f) : 1f;
                            zero = (Vector3)(zero * num6);
                            zero = (Vector3)(zero * this.speed);
                            if ((this.buffTime > 0f) && (this.currentBuff == BUFF.SpeedUp))
                            {
                                zero = (Vector3)(zero * 4f);
                            }
                            if ((x != 0f) || (z != 0f))
                            {
                                if (((!this.baseAnimation.IsPlaying("run") && !this.baseAnimation.IsPlaying("jump")) && !this.baseAnimation.IsPlaying("run_sasha")) && (!this.baseAnimation.IsPlaying("horse_geton") || (this.baseAnimation["horse_geton"].normalizedTime >= 0.5f)))
                                {
                                    if ((this.buffTime > 0f) && (this.currentBuff == BUFF.SpeedUp))
                                    {
                                        this.crossFade("run_sasha", 0.1f);
                                    }
                                    else
                                    {
                                        this.crossFade("run", 0.1f);
                                    }
                                }
                            }
                            else
                            {
                                if (((!this.baseAnimation.IsPlaying(this.standAnimation) && (this.state != HERO_STATE.Land)) && (!this.baseAnimation.IsPlaying("jump") && !this.baseAnimation.IsPlaying("horse_geton"))) && !this.baseAnimation.IsPlaying("grabbed"))
                                {
                                    this.crossFade(this.standAnimation, 0.1f);
                                    zero = (Vector3)(zero * 0f);
                                }
                                resultAngle = -874f;
                            }
                            if (resultAngle != -874f)
                            {
                                this.facingDirection = resultAngle;
                                this.targetRotation = Quaternion.Euler(0f, this.facingDirection, 0f);
                            }
                        }
                        else if (this.state == HERO_STATE.Land)
                        {
                            zero = (Vector3)(this.baseRigidBody.velocity * 0.96f);
                        }
                        else if (this.state == HERO_STATE.Slide)
                        {
                            zero = (Vector3)(this.baseRigidBody.velocity * 0.99f);
                            if (this.currentSpeed < (this.speed * 1.2f))
                            {
                                this.idle();
                                this.sparks.enableEmission = false;
                            }
                        }
                        Vector3 velocity = this.baseRigidBody.velocity;
                        Vector3 force = zero - velocity;
                        force.x = Mathf.Clamp(force.x, -this.maxVelocityChange, this.maxVelocityChange);
                        force.z = Mathf.Clamp(force.z, -this.maxVelocityChange, this.maxVelocityChange);
                        force.y = 0f;
                        if (this.baseAnimation.IsPlaying("jump") && (this.baseAnimation["jump"].normalizedTime > 0.18f))
                        {
                            force.y += 8f;
                        }
                        if ((this.baseAnimation.IsPlaying("horse_geton") && (this.baseAnimation["horse_geton"].normalizedTime > 0.18f)) && (this.baseAnimation["horse_geton"].normalizedTime < 1f))
                        {
                            float num7 = 6f;
                            force = -this.baseRigidBody.velocity;
                            force.y = num7;
                            float num8 = Vector3.Distance(this.myHorse.transform.position, this.baseTransform.position);
                            float num9 = ((0.6f * this.gravity) * num8) / 12f;
                            vector5 = this.myHorse.transform.position - this.baseTransform.position;
                            force += (Vector3)(num9 * vector5.normalized);
                        }
                        if ((this.state != HERO_STATE.Attack) || !this.useGun)
                        {
                            this.baseRigidBody.AddForce(force, ForceMode.VelocityChange);
                            this.baseRigidBody.rotation = Quaternion.Lerp(baseGT.rotation, Quaternion.Euler(0f, this.facingDirection, 0f), Time.deltaTime * 10f);
                        }
                    }
                    else
                    {
                        if (this.sparks.enableEmission)
                        {
                            this.sparks.enableEmission = false;
                        }
                        if (((this.myHorse != null) && (this.baseAnimation.IsPlaying("horse_geton") || this.baseAnimation.IsPlaying("air_fall"))) && ((this.baseRigidBody.velocity.y < 0f) && (Vector3.Distance(this.myHorse.transform.position + ((Vector3)(Vector3.up * 1.65f)), this.baseTransform.position) < 0.5f)))
                        {
                            this.baseTransform.position = this.myHorse.transform.position + ((Vector3)(Vector3.up * 1.65f));
                            this.baseTransform.rotation = this.myHorse.transform.rotation;
                            this.isMounted = true;
                            this.crossFade("horse_idle", 0.1f);
                            this.myHorse.GetComponent<Horse>().mounted();
                        }
                        if (!((((((this.state != HERO_STATE.Idle) || this.baseAnimation.IsPlaying("dash")) || (this.baseAnimation.IsPlaying("wallrun") || this.baseAnimation.IsPlaying("toRoof"))) || ((this.baseAnimation.IsPlaying("horse_geton") || this.baseAnimation.IsPlaying("horse_getoff")) || (this.baseAnimation.IsPlaying("air_release") || this.isMounted))) || ((this.baseAnimation.IsPlaying("air_hook_l_just") && (this.baseAnimation["air_hook_l_just"].normalizedTime < 1f)) || (this.baseAnimation.IsPlaying("air_hook_r_just") && (this.baseAnimation["air_hook_r_just"].normalizedTime < 1f)))) ? (this.baseAnimation["dash"].normalizedTime < 0.99f) : false))
                        {
                            if (((!this.isLeftHandHooked && !this.isRightHandHooked) && ((this.baseAnimation.IsPlaying("air_hook_l") || this.baseAnimation.IsPlaying("air_hook_r")) || this.baseAnimation.IsPlaying("air_hook"))) && (this.baseRigidBody.velocity.y > 20f))
                            {
                                this.baseAnimation.CrossFade("air_release");
                            }
                            else
                            {
                                bool flag4 = (Mathf.Abs(this.baseRigidBody.velocity.x) + Mathf.Abs(this.baseRigidBody.velocity.z)) > 25f;
                                bool flag5 = this.baseRigidBody.velocity.y < 0f;
                                if (!flag4)
                                {
                                    if (flag5)
                                    {
                                        if (!this.baseAnimation.IsPlaying("air_fall"))
                                        {
                                            this.crossFade("air_fall", 0.2f);
                                        }
                                    }
                                    else if (!this.baseAnimation.IsPlaying("air_rise"))
                                    {
                                        this.crossFade("air_rise", 0.2f);
                                    }
                                }
                                else if (!this.isLeftHandHooked && !this.isRightHandHooked)
                                {
                                    float current = -Mathf.Atan2(this.baseRigidBody.velocity.z, this.baseRigidBody.velocity.x) * 57.29578f;
                                    float num11 = -Mathf.DeltaAngle(current, this.baseTransform.rotation.eulerAngles.y - 90f);
                                    if (Mathf.Abs(num11) < 45f)
                                    {
                                        if (!this.baseAnimation.IsPlaying("air2"))
                                        {
                                            this.crossFade("air2", 0.2f);
                                        }
                                    }
                                    else if ((num11 < 135f) && (num11 > 0f))
                                    {
                                        if (!this.baseAnimation.IsPlaying("air2_right"))
                                        {
                                            this.crossFade("air2_right", 0.2f);
                                        }
                                    }
                                    else if ((num11 > -135f) && (num11 < 0f))
                                    {
                                        if (!this.baseAnimation.IsPlaying("air2_left"))
                                        {
                                            this.crossFade("air2_left", 0.2f);
                                        }
                                    }
                                    else if (!this.baseAnimation.IsPlaying("air2_backward"))
                                    {
                                        this.crossFade("air2_backward", 0.2f);
                                    }
                                }
                                else if (this.useGun)
                                {
                                    if (!this.isRightHandHooked)
                                    {
                                        if (!this.baseAnimation.IsPlaying("AHSS_hook_forward_l"))
                                        {
                                            this.crossFade("AHSS_hook_forward_l", 0.1f);
                                        }
                                    }
                                    else if (!this.isLeftHandHooked)
                                    {
                                        if (!this.baseAnimation.IsPlaying("AHSS_hook_forward_r"))
                                        {
                                            this.crossFade("AHSS_hook_forward_r", 0.1f);
                                        }
                                    }
                                    else if (!this.baseAnimation.IsPlaying("AHSS_hook_forward_both"))
                                    {
                                        this.crossFade("AHSS_hook_forward_both", 0.1f);
                                    }
                                }
                                else if (!this.isRightHandHooked)
                                {
                                    if (!this.baseAnimation.IsPlaying("air_hook_l"))
                                    {
                                        this.crossFade("air_hook_l", 0.1f);
                                    }
                                }
                                else if (!this.isLeftHandHooked)
                                {
                                    if (!this.baseAnimation.IsPlaying("air_hook_r"))
                                    {
                                        this.crossFade("air_hook_r", 0.1f);
                                    }
                                }
                                else if (!this.baseAnimation.IsPlaying("air_hook"))
                                {
                                    this.crossFade("air_hook", 0.1f);
                                }
                            }
                        }
                        if (((this.state == HERO_STATE.Idle) && this.baseAnimation.IsPlaying("air_release")) && (this.baseAnimation["air_release"].normalizedTime >= 1f))
                        {
                            this.crossFade("air_rise", 0.2f);
                        }
                        if (this.baseAnimation.IsPlaying("horse_getoff") && (this.baseAnimation["horse_getoff"].normalizedTime >= 1f))
                        {
                            this.crossFade("air_rise", 0.2f);
                        }
                        if (this.baseAnimation.IsPlaying("toRoof"))
                        {
                            if (this.baseAnimation["toRoof"].normalizedTime < 0.22f)
                            {
                                this.baseRigidBody.velocity = Vector3.zero;
                                this.baseRigidBody.AddForce(new Vector3(0f, this.gravity * this.baseRigidBody.mass, 0f));
                            }
                            else
                            {
                                if (!this.wallJump)
                                {
                                    this.wallJump = true;
                                    this.baseRigidBody.AddForce((Vector3)(Vector3.up * 8f), ForceMode.Impulse);
                                }
                                this.baseRigidBody.AddForce((Vector3)(this.baseTransform.forward * 0.05f), ForceMode.Impulse);
                            }
                            if (this.baseAnimation["toRoof"].normalizedTime >= 1f)
                            {
                                this.playAnimation("air_rise");
                            }
                        }
                        else if (((((this.state == HERO_STATE.Idle) && this.isPressDirectionTowardsHero(x, z)) && (!FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_jump) && !FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_leftRope))) && ((!FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_rightRope) && !FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_bothRope)) && (this.IsFrontGrounded() && !this.baseAnimation.IsPlaying("wallrun")))) && !this.baseAnimation.IsPlaying("dodge"))
                        {
                            this.crossFade("wallrun", 0.1f);
                            this.wallRunTime = 0f;
                        }
                        else if (this.baseAnimation.IsPlaying("wallrun"))
                        {
                            this.baseRigidBody.AddForce(((Vector3)(Vector3.up * this.speed)) - this.baseRigidBody.velocity, ForceMode.VelocityChange);
                            this.wallRunTime += Time.deltaTime;
                            if ((this.wallRunTime > 1f) || ((z == 0f) && (x == 0f)))
                            {
                                this.baseRigidBody.AddForce((Vector3)((-this.baseTransform.forward * this.speed) * 0.75f), ForceMode.Impulse);
                                this.dodge2(true);
                            }
                            else if (!this.IsUpFrontGrounded())
                            {
                                this.wallJump = false;
                                this.crossFade("toRoof", 0.1f);
                            }
                            else if (!this.IsFrontGrounded())
                            {
                                this.crossFade("air_fall", 0.1f);
                            }
                        }
                        else if ((!this.baseAnimation.IsPlaying("attack5") && !this.baseAnimation.IsPlaying("special_petra")) && (!this.baseAnimation.IsPlaying("dash") && !this.baseAnimation.IsPlaying("jump")))
                        {
                            Vector3 vector10 = new Vector3(x, 0f, z);
                            float num12 = this.getGlobalFacingDirection(x, z);
                            Vector3 vector11 = this.getGlobaleFacingVector3(num12);
                            float num13 = (vector10.magnitude <= 0.95f) ? ((vector10.magnitude >= 0.25f) ? vector10.magnitude : 0f) : 1f;
                            vector11 = (Vector3)(vector11 * num13);
                            vector11 = (Vector3)(vector11 * ((((float)this.setup.myCostume.stat.ACL) / 10f) * 2f));
                            if ((x == 0f) && (z == 0f))
                            {
                                if (this.state == HERO_STATE.Attack)
                                {
                                    vector11 = (Vector3)(vector11 * 0f);
                                }
                                num12 = -874f;
                            }
                            if (num12 != -874f)
                            {
                                this.facingDirection = num12;
                                this.targetRotation = Quaternion.Euler(0f, this.facingDirection, 0f);
                            }
                            if (((!flag2 && !flag3) && (!this.isMounted && FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_jump))) && (this.currentGas > 0f))
                            {
                                if ((x != 0f) || (z != 0f))
                                {
                                    this.baseRigidBody.AddForce(vector11, ForceMode.Acceleration);
                                }
                                else
                                {
                                    this.baseRigidBody.AddForce((Vector3)(this.baseTransform.forward * vector11.magnitude), ForceMode.Acceleration);
                                }
                                flag = true;
                            }
                        }
                        if ((this.baseAnimation.IsPlaying("air_fall") && (this.currentSpeed < 0.2f)) && this.IsFrontGrounded())
                        {
                            this.crossFade("onWall", 0.3f);
                        }
                    }
                    this.spinning = false;

                    if (flag2 && flag3)
                    {
                        float num14 = this.currentSpeed + 0.1f;
                        this.baseRigidBody.AddForce(-this.baseRigidBody.velocity, ForceMode.VelocityChange);
                        Vector3 vector13 = ((Vector3)((this.bulletRight.transform.position + this.bulletLeft.transform.position) * 0.5f)) - this.baseTransform.position;
                        float num15 = 0f;
                        if ( FengGameManagerMKII.inputRC.isInputHuman(InputCodeRC.reelin))
                        {
                            num15 = -1f;
                        }
                        else if ( FengGameManagerMKII.inputRC.isInputHuman(InputCodeRC.reelout))
                        {
                            num15 = 1f;
                        }
                        else
                        {
                            num15 = Input.GetAxis("Mouse ScrollWheel") * 5555f;
                        }
                        num15 = Mathf.Clamp(num15, -0.8f, 0.8f);
                        float num16 = 1f + num15;
                        Vector3 vector14 = Vector3.RotateTowards(vector13, this.baseRigidBody.velocity, 1.53938f * num16, 1.53938f * num16);
                        vector14.Normalize();
                        this.spinning = true;
                        this.baseRigidBody.velocity = (Vector3)(vector14 * num14);

                    }
                    else if (flag2)
                    {
                        float num17 = this.currentSpeed + 0.1f;
                        this.baseRigidBody.AddForce(-this.baseRigidBody.velocity, ForceMode.VelocityChange);
                        Vector3 vector15 = this.bulletLeft.transform.position - this.baseTransform.position;
                        float num18 = 0f;
                        if (FengGameManagerMKII.inputRC.isInputHuman(InputCodeRC.reelin))
                        {
                            num18 = -1f;
                        }
                        else if (FengGameManagerMKII.inputRC.isInputHuman(InputCodeRC.reelout))
                        {
                            num18 = 1f;
                        }
                        else
                        {
                            num18 = Input.GetAxis("Mouse ScrollWheel") * 5555f;
                        }
                        num18 = Mathf.Clamp(num18, -0.8f, 0.8f);
                        float num19 = 1f + num18;
                        Vector3 vector16 = Vector3.RotateTowards(vector15, this.baseRigidBody.velocity, 1.53938f * num19, 1.53938f * num19);
                        vector16.Normalize();
                        this.spinning = true;
                        this.baseRigidBody.velocity = (Vector3)(vector16 * num17);

                    }
                    else if (flag3)
                    {
                        float num20 = this.currentSpeed + 0.1f;
                        this.baseRigidBody.AddForce(-this.baseRigidBody.velocity, ForceMode.VelocityChange);
                        Vector3 vector17 = this.bulletRight.transform.position - this.baseTransform.position;
                        float num21 = 0f;
                        if (FengGameManagerMKII.inputRC.isInputHuman(InputCodeRC.reelin))
                        {
                            num21 = -1f;
                        }
                        else if (FengGameManagerMKII.inputRC.isInputHuman(InputCodeRC.reelout))
                        {
                            num21 = 1f;
                        }
                        else
                        {
                            num21 = Input.GetAxis("Mouse ScrollWheel") * 5555f;
                        }
                        num21 = Mathf.Clamp(num21, -0.8f, 0.8f);
                        float num22 = 1f + num21;
                        Vector3 vector18 = Vector3.RotateTowards(vector17, this.baseRigidBody.velocity, 1.53938f * num22, 1.53938f * num22);
                        vector18.Normalize();
                        this.spinning = true;
                        this.baseRigidBody.velocity = (Vector3)(vector18 * num20);

                    }



                    if (((this.state == HERO_STATE.Attack) && ((this.attackAnimation == "attack5") || (this.attackAnimation == "special_petra"))) && ((this.baseAnimation[this.attackAnimation].normalizedTime > 0.4f) && !this.attackMove))
                    {
                        this.attackMove = true;
                        if (this.launchPointRight.magnitude > 0f)
                        {
                            Vector3 vector18 = this.launchPointRight - this.baseTransform.position;
                            vector18.Normalize();
                            vector18 = (Vector3)(vector18 * 13f);
                            this.baseRigidBody.AddForce(vector18, ForceMode.Impulse);
                        }
                        if ((this.attackAnimation == "special_petra") && (this.launchPointLeft.magnitude > 0f))
                        {
                            Vector3 vector19 = this.launchPointLeft - this.baseTransform.position;
                            vector19.Normalize();
                            vector19 = (Vector3)(vector19 * 13f);
                            this.baseRigidBody.AddForce(vector19, ForceMode.Impulse);
                            if (this.bulletRight != null)
                            {
                                bulletRightT.disable();
                                this.releaseIfIHookSb();
                            }
                            if (this.bulletLeft != null)
                            {
                                bulletLeftT.disable();
                                this.releaseIfIHookSb();
                            }
                        }
                        this.baseRigidBody.AddForce((Vector3)(Vector3.up * 2f), ForceMode.Impulse);
                    }
                    bool flag6 = false;
                    if ((this.bulletLeft != null) || (this.bulletRight != null))
                    {
                        if (((this.bulletLeft != null) && (bulletLeftTT.position.y > baseGT.position.y)) && (this.isLaunchLeft && bulletLeftT.isHooked()))
                        {
                            flag6 = true;
                        }
                        if (((this.bulletRight != null) && (bulletRightTT.position.y > baseGT.position.y)) && (this.isLaunchRight && bulletRightT.isHooked()))
                        {
                            flag6 = true;
                        }
                    }
                    if (flag6)
                    {
                        this.baseRigidBody.AddForce(new Vector3(0f, -10f * this.baseRigidBody.mass, 0f));
                    }
                    else
                    {
                        this.baseRigidBody.AddForce(new Vector3(0f, -this.gravity * this.baseRigidBody.mass, 0f));
                    }
                    if ((int)FengGameManagerMKII.settings[418] == 0)
                    {
                        if (this.currentSpeed > 10f)
                        {
                            this.currentCamera.fieldOfView = Mathf.Lerp(this.currentCamera.fieldOfView, Mathf.Min((float)100f, (float)(this.currentSpeed + 40f)), 0.1f);
                        }
                        else
                        {
                            this.currentCamera.fieldOfView = Mathf.Lerp(this.currentCamera.fieldOfView, 50f, 0.1f);
                        }
                    }
                    else if ((int)FengGameManagerMKII.settings[418] == 2)
                    {
                        if (this.currentSpeed > 10f)
                        {
                            this.currentCamera.fieldOfView = Mathf.Lerp(this.currentCamera.fieldOfView, Mathf.Min((float)FengGameManagerMKII.settings[419] , ((float)(this.currentSpeed) + (float)FengGameManagerMKII.settings[419] / 2f)), 0.1f);
                        }
                        else
                        {
                            this.currentCamera.fieldOfView = Mathf.Lerp(this.currentCamera.fieldOfView, (float)FengGameManagerMKII.settings[419] /2f, 0.1f);
                        }
                    }
                    else
                    {
                        this.currentCamera.fieldOfView = (float)FengGameManagerMKII.settings[419];
                    }
                    if (flag)
                    {
                        this.useGas(this.useGasSpeed * Time.deltaTime);
                        if (gastrail)
                        {
                            if ((!this.smoke_3dmg.enableEmission && (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)) && pviev.isMine)
                            {
                                object[] parameters = new object[] { true };
                                pviev.RPC("net3DMGSMOKE", PhotonTargets.Others, parameters);
                            }

                            this.smoke_3dmg.enableEmission = true;
                        }
                    }
                    else
                    {
                        if ((this.smoke_3dmg.enableEmission && (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)) && pviev.isMine)
                        {
                            object[] objArray2 = new object[] { false };
                            pviev.RPC("net3DMGSMOKE", PhotonTargets.Others, objArray2);
                        }
                        this.smoke_3dmg.enableEmission = false;
                    }
                    if (this.currentSpeed > 80f)
                    {
                        if (!this.speedFXPS.enableEmission)
                        {
                            this.speedFXPS.enableEmission = true;
                        }
                        this.speedFXPS.startSpeed = this.currentSpeed;
                        this.speedFX.transform.LookAt(this.baseTransform.position + this.baseRigidBody.velocity);
                    }
                    else if (this.speedFXPS.enableEmission)
                    {
                        this.speedFXPS.enableEmission = false;
                    }
                }
            }
        }
    }

    public string getDebugInfo()
    {
        string str = "\n";
        str = "Left:" + this.isLeftHandHooked + " ";
        if (this.isLeftHandHooked && (this.bulletLeft != null))
        {
            Vector3 vector = bulletLeftTT.position - this.baseTransform.position;
            str = str + ((int)(Mathf.Atan2(vector.x, vector.z) * 57.29578f));
        }
        string str2 = str;
        str = string.Concat(new object[] { str2, "\nRight:", this.isRightHandHooked, " " });
        if (this.isRightHandHooked && (this.bulletRight != null))
        {
            Vector3 vector2 = bulletRightTT.position - this.baseTransform.position;
            str = str + ((int)(Mathf.Atan2(vector2.x, vector2.z) * 57.29578f));
        }
        str = string.Concat(new object[] { str, "\nfacingDirection:", (int)this.facingDirection, "\nActual facingDirection:", (int)this.baseTransform.rotation.eulerAngles.y, "\nState:", this.state.ToString(), "\n\n\n\n\n" });
        if (this.state == HERO_STATE.Attack)
        {
            this.targetRotation = Quaternion.Euler(0f, this.facingDirection, 0f);
        }
        return str;
    }

    private Vector3 getGlobaleFacingVector3(float resultAngle)
    {
        float num = -resultAngle + 90f;
        return new Vector3(Mathf.Cos(num * 0.01745329f), 0f, Mathf.Sin(num * 0.01745329f));
    }

    private Vector3 getGlobaleFacingVector3(float horizontal, float vertical)
    {
        float num = -this.getGlobalFacingDirection(horizontal, vertical) + 90f;
        return new Vector3(Mathf.Cos(num * 0.01745329f), 0f, Mathf.Sin(num * 0.01745329f));
    }

    private float getGlobalFacingDirection(float horizontal, float vertical)
    {
        if ((vertical == 0f) && (horizontal == 0f))
        {
            return this.baseTransform.rotation.eulerAngles.y;
        }
        float y = this.currentCamera.transform.rotation.eulerAngles.y;
        float num2 = Mathf.Atan2(vertical, horizontal) * 57.29578f;
        num2 = -num2 + 90f;
        return (y + num2);
    }

    private float getLeanAngle(Vector3 p, bool left)
    {
        if (!this.useGun && (this.state == HERO_STATE.Attack))
        {
            return 0f;
        }
        float num = p.y - this.baseTransform.position.y;
        float num2 = Vector3.Distance(p, this.baseTransform.position);
        float a = Mathf.Acos(num / num2) * 57.29578f;
        a *= 0.1f;
        a *= 1f + Mathf.Pow(this.baseRigidBody.velocity.magnitude, 0.2f);
        Vector3 vector = p - this.baseTransform.position;
        float current = Mathf.Atan2(vector.x, vector.z) * 57.29578f;
        float target = Mathf.Atan2(this.baseRigidBody.velocity.x, this.baseRigidBody.velocity.z) * 57.29578f;
        float num6 = Mathf.DeltaAngle(current, target);
        a += Mathf.Abs((float)(num6 * 0.5f));
        if (this.state != HERO_STATE.Attack)
        {
            a = Mathf.Min(a, 80f);
        }
        if (num6 > 0f)
        {
            this.leanLeft = true;
        }
        else
        {
            this.leanLeft = false;
        }
        if (this.useGun)
        {
            return (a * ((num6 >= 0f) ? 1f : -1f));
        }
        float num7 = 0f;
        if ((left && (num6 < 0f)) || (!left && (num6 > 0f)))
        {
            num7 = 0.1f;
        }
        else
        {
            num7 = 0.5f;
        }
        return (a * ((num6 >= 0f) ? num7 : -num7));
    }

    private void getOffHorse()
    {
        this.playAnimation("horse_getoff");
        this.baseRigidBody.AddForce((Vector3)(((Vector3.up * 10f) - (this.baseTransform.forward * 2f)) - (this.baseTransform.right * 1f)), ForceMode.VelocityChange);
        this.unmounted();
    }

    private void getOnHorse()
    {
        this.playAnimation("horse_geton");
        this.facingDirection = this.myHorse.transform.rotation.eulerAngles.y;
        this.targetRotation = Quaternion.Euler(0f, this.facingDirection, 0f);
    }

    public void getSupply()
    {
        if (((baseAnimation.IsPlaying(this.standAnimation) || baseAnimation.IsPlaying("run")) || baseAnimation.IsPlaying("run_sasha")) && (((this.currentBladeSta != this.totalBladeSta) || (this.currentBladeNum != this.totalBladeNum)) || (((this.currentGas != this.totalGas) || (this.leftBulletLeft != this.bulletMAX)) || (this.rightBulletLeft != this.bulletMAX))))
        {
            this.state = HERO_STATE.FillGas;
            this.crossFade("supply", 0.1f);
        }
    }

    public void grabbed(GameObject titan, bool leftHand)
    {
        if (this.isMounted)
        {
            this.unmounted();
        }
        this.state = HERO_STATE.Grab;
        base.GetComponent<CapsuleCollider>().isTrigger = true;
        this.falseAttack();
        this.titanWhoGrabMe = titan;
        if (this.titanForm && (this.eren_titan != null))
        {
            this.eren_titan.GetComponent<TITAN_EREN>().lifeTime = 0.1f;
        }
        if (!this.useGun && ((isSingle) || pviev.isMine))
        {
            DiactivateTrail(true);
        }
        this.smoke_3dmg.enableEmission = false;
        this.sparks.enableEmission = false;
    }

    public bool HasDied()
    {
        if (!this.hasDied)
        {
            return this.isInvincible();
        }
        return true;
    }
    Transform head;
    Transform neck;
    private void headMovement()
    {
        Transform transform = (head != null ? head : head = this.baseTransform.Find("Amarture/Controller_Body/hip/spine/chest/neck/head"));
        Transform transform2 = neck != null ? neck : neck = this.baseTransform.Find("Amarture/Controller_Body/hip/spine/chest/neck");
        float x = Mathf.Sqrt(((this.gunTarget.x - this.baseTransform.position.x) * (this.gunTarget.x - this.baseTransform.position.x)) + ((this.gunTarget.z - this.baseTransform.position.z) * (this.gunTarget.z - this.baseTransform.position.z)));
        this.targetHeadRotation = transform.rotation;
        Vector3 vector = this.gunTarget - this.baseTransform.position;
        float current = -Mathf.Atan2(vector.z, vector.x) * 57.29578f;
        float num3 = -Mathf.DeltaAngle(current, this.baseTransform.rotation.eulerAngles.y - 90f);
        num3 = Mathf.Clamp(num3, -40f, 40f);
        float y = transform2.position.y - this.gunTarget.y;
        float num5 = Mathf.Atan2(y, x) * 57.29578f;
        num5 = Mathf.Clamp(num5, -40f, 30f);
        this.targetHeadRotation = Quaternion.Euler(transform.rotation.eulerAngles.x + num5, transform.rotation.eulerAngles.y + num3, transform.rotation.eulerAngles.z);
        this.oldHeadRotation = Quaternion.Lerp(this.oldHeadRotation, this.targetHeadRotation, Time.deltaTime * 60f);
        transform.rotation = this.oldHeadRotation;
    }

    public void hookedByHuman(int hooker, Vector3 hookPosition)
    {
       
            object[] parameters = new object[] { hooker, hookPosition };
            pviev.RPC("RPCHookedByHuman", pviev.owner, parameters);
        
    }

    [RPC]
    public void hookFail()
    {
        this.hookTarget = null;
        this.hookSomeOne = false;
    }

    public void hookToHuman(GameObject target, Vector3 hookPosition)
    {
        this.releaseIfIHookSb();
        this.hookTarget = target;
        this.hookSomeOne = true;
        HERO hero = target.GetComponent<HERO>();
        if (hero != null)
        {
            if ((int)FengGameManagerMKII.settings[443] == 1)
            {
                hero.photonView.RPC("netDie2", PhotonTargets.All, new object[] { -1, "Hook Kill" });
            }
            else
            {
                hero.hookedByHuman(pviev.viewID, hookPosition);
            }
        }

        this.launchForce = hookPosition - this.baseTransform.position;
        float num = Mathf.Pow(this.launchForce.magnitude, 0.1f);
        if ((int)FengGameManagerMKII.settings[443] == 0)
        {
            if (this.grounded)
            {
                this.baseRigidBody.AddForce((Vector3)(Vector3.up * Mathf.Min((float)(this.launchForce.magnitude * 0.2f), (float)10f)), ForceMode.Impulse);
            }
            this.baseRigidBody.AddForce((Vector3)((this.launchForce * num) * 0.1f), ForceMode.Impulse);
        }
    }

    public void idle()
    {
        if (this.state == HERO_STATE.Attack)
        {
            this.falseAttack();
        }
        this.state = HERO_STATE.Idle;
        this.crossFade(this.standAnimation, 0.1f);
    }

    private bool IsFrontGrounded()
    {
        LayerMask mask = ((int)1) << LayerMask.NameToLayer("Ground");
        LayerMask mask2 = ((int)1) << LayerMask.NameToLayer("EnemyBox");
        LayerMask mask3 = mask2 | mask;
        return Physics.Raycast(baseGT.position + ((Vector3)(baseGT.up * 1f)), baseGT.forward, (float)1f, mask3.value);
    }

    public bool IsGrounded()
    {
        LayerMask mask = ((int)1) << LayerMask.NameToLayer("Ground");
        LayerMask mask2 = ((int)1) << LayerMask.NameToLayer("EnemyBox");
        LayerMask mask3 = mask2 | mask;
        return Physics.Raycast(baseGT.position + ((Vector3)(Vector3.up * 0.1f)), -Vector3.up, (float)0.3f, mask3.value);
    }

    public bool isInvincible()
    {
        return (this.invincible > 0f);
    }

    private bool isPressDirectionTowardsHero(float h, float v)
    {
        if ((h == 0f) && (v == 0f))
        {
            return false;
        }
        return (Mathf.Abs(Mathf.DeltaAngle(this.getGlobalFacingDirection(h, v), this.baseTransform.rotation.eulerAngles.y)) < 45f);
    }

    private bool IsUpFrontGrounded()
    {
        LayerMask mask = ((int)1) << LayerMask.NameToLayer("Ground");
        LayerMask mask2 = ((int)1) << LayerMask.NameToLayer("EnemyBox");
        LayerMask mask3 = mask2 | mask;
        return Physics.Raycast(baseGT.position + ((Vector3)(baseGT.up * 3f)), baseGT.forward, (float)1.2f, mask3.value);
    }

    [RPC]
    private void killObject(PhotonMessageInfo info)
    {
        PhotonPlayer player = info.sender;
        if (info != null && (player.isMasterClient || player.isLocal))
        {
            UnityEngine.Object.Destroy(baseG);
        }
    }

    public void lateUpdate()
    {

    }

    public void lateUpdate2()
    {
        if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE && this.myNetWorkName != null)
        {
            if (this.titanForm && (this.eren_titan != null))
            {
                this.myNetWorkNameT.transform.localPosition = (Vector3)((Vector3.up * Screen.height) * 2f);
            }
            Vector3 start = new Vector3(this.baseTransform.position.x, this.baseTransform.position.y + 2f, this.baseTransform.position.z);
            GameObject maincamera = this.maincamera;
            LayerMask mask = ((int)1) << LayerMask.NameToLayer("Ground");
            LayerMask mask2 = ((int)1) << LayerMask.NameToLayer("EnemyBox");
            LayerMask mask3 = mask2 | mask;
            if ((Vector3.Angle(maincamera.transform.forward, start - maincamera.transform.position) > 90f) || Physics.Linecast(start, maincamera.transform.position, (int)mask3))
            {
                this.myNetWorkNameT.transform.localPosition = (Vector3)((Vector3.up * Screen.height) * 2f);
            }
            else
            {
                Vector2 vector2 = this.maincamera.GetComponent<Camera>().WorldToScreenPoint(start);
                this.myNetWorkNameT.transform.localPosition = new Vector3((float)((int)(vector2.x - (Screen.width * 0.5f))), (float)((int)(vector2.y - (Screen.height * 0.5f))), 0f);
            }
        }
        if (!this.titanForm && !this.isCannon)
        {
            if ((IN_GAME_MAIN_CAMERA.cameraTilt == 1) && ((isSingle) || pviev.isMine))
            {
                Quaternion quaternion;
                Vector3 zero = Vector3.zero;
                Vector3 position = Vector3.zero;
                if ((this.isLaunchLeft && (this.bulletLeft != null)) && bulletLeftT.isHooked())
                {
                    zero = bulletLeftTT.position;
                }
                if ((this.isLaunchRight && (this.bulletRight != null)) && bulletRightT.isHooked())
                {
                    position = bulletRightTT.position;
                }
                Vector3 vector5 = Vector3.zero;
                if ((zero.magnitude != 0f) && (position.magnitude == 0f))
                {
                    vector5 = zero;
                }
                else if ((zero.magnitude == 0f) && (position.magnitude != 0f))
                {
                    vector5 = position;
                }
                else if ((zero.magnitude != 0f) && (position.magnitude != 0f))
                {
                    vector5 = (Vector3)((zero + position) * 0.5f);
                }
                Vector3 from = Vector3.Project(vector5 - this.baseTransform.position, this.maincameraT.up);
                Vector3 vector7 = Vector3.Project(vector5 - this.baseTransform.position, this.maincameraT.right);
                if (vector5.magnitude > 0f)
                {
                    Vector3 to = from + vector7;
                    float num = Vector3.Angle(vector5 - this.baseTransform.position, this.baseRigidBody.velocity) * 0.005f;
                    Vector3 vector9 = this.maincameraT.right + vector7.normalized;
                    quaternion = Quaternion.Euler(this.maincameraT.rotation.eulerAngles.x, this.maincameraT.rotation.eulerAngles.y, (vector9.magnitude >= 1f) ? (-Vector3.Angle(from, to) * num) : (Vector3.Angle(from, to) * num));
                }
                else
                {
                    quaternion = Quaternion.Euler(this.maincameraT.rotation.eulerAngles.x, this.maincameraT.rotation.eulerAngles.y, 0f);
                }
                this.maincameraT.rotation = Quaternion.Lerp(this.maincameraT.rotation, quaternion, Time.deltaTime * 2f);
            }
            if ((this.state == HERO_STATE.Grab) && (this.titanWhoGrabMe != null))
            {
                TITAN tit = this.titanWhoGrabMe.GetComponent<TITAN>();
                FEMALE_TITAN ft = null;
                if (tit != null)
                {
                    this.baseTransform.position = tit.grabTFT.position;
                    this.baseTransform.rotation = tit.grabTFT.rotation;
                }
                else if ((ft = this.titanWhoGrabMe.GetComponent<FEMALE_TITAN>()) != null)
                {
                    this.baseTransform.position = ft.grabTF.transform.position;
                    this.baseTransform.rotation = ft.grabTF.transform.rotation;
                }
            }
            if (this.useGun)
            {
                if (this.leftArmAim || this.rightArmAim)
                {
                    Vector3 vector10 = this.gunTarget - this.baseTransform.position;
                    float current = -Mathf.Atan2(vector10.z, vector10.x) * 57.29578f;
                    float num3 = -Mathf.DeltaAngle(current, this.baseTransform.rotation.eulerAngles.y - 90f);
                    this.headMovement();
                    if ((!this.isLeftHandHooked && this.leftArmAim) && ((num3 < 40f) && (num3 > -90f)))
                    {
                        this.leftArmAimTo(this.gunTarget);
                    }
                    if ((!this.isRightHandHooked && this.rightArmAim) && ((num3 > -40f) && (num3 < 90f)))
                    {
                        this.rightArmAimTo(this.gunTarget);
                    }
                }
                else if (!this.grounded)
                {
                    this.handL.localRotation = Quaternion.Euler(90f, 0f, 0f);
                    this.handR.localRotation = Quaternion.Euler(-90f, 0f, 0f);
                }
                if (this.isLeftHandHooked && (this.bulletLeft != null))
                {
                    this.leftArmAimTo(bulletLeftTT.position);
                }
                if (this.isRightHandHooked && (this.bulletRight != null))
                {
                    this.rightArmAimTo(bulletRightTT.position);
                }
            }
            this.setHookedPplDirection();
            if (bodyLEAN || this.useGun)
            {
                this.bodyLean();
            }
        }
    }

    public void launch(Vector3 des, bool left = true, bool leviMode = false)
    {
        if (this.isMounted)
        {
            this.unmounted();
        }
        if (this.state != HERO_STATE.Attack)
        {
            this.idle();
        }
        Vector3 vector = des - this.baseTransform.position;
        if (left)
        {
            this.launchPointLeft = des;
        }
        else
        {
            this.launchPointRight = des;
        }
        vector.Normalize();
        vector = (Vector3)(vector * 20f);
        if (((this.bulletLeft != null) && (this.bulletRight != null)) && (bulletLeftT.isHooked() && bulletRightT.isHooked()))
        {
            vector = (Vector3)(vector * 0.8f);
        }
        if (!baseAnimation.IsPlaying("attack5") && !baseAnimation.IsPlaying("special_petra"))
        {
            leviMode = false;
        }
        else
        {
            leviMode = true;
        }
        if (!leviMode)
        {
            this.falseAttack();
            this.idle();
            if (this.useGun)
            {
                this.crossFade("AHSS_hook_forward_both", 0.1f);
            }
            else if (bodyLEAN)
            {
                if (left && !this.isRightHandHooked)
                {
                    this.crossFade("air_hook_l_just", 0.1f);
                }
                else if (!left && !this.isLeftHandHooked)
                {
                    this.crossFade("air_hook_r_just", 0.1f);
                }
                else
                {
                    this.crossFade("dash", 0.1f);
                    baseAnimation["dash"].time = 0f;
                }
            }
        }
        if (left)
        {
            this.isLaunchLeft = true;
        }
        if (!left)
        {
            this.isLaunchRight = true;
        }
        this.launchForce = vector;
        if (!leviMode)
        {
            if (vector.y < 30f)
            {
                this.launchForce += (Vector3)(Vector3.up * (30f - vector.y));
            }
            if (des.y >= this.baseTransform.position.y)
            {
                this.launchForce += (Vector3)((Vector3.up * (des.y - this.baseTransform.position.y)) * 10f);
            }
            this.baseRigidBody.AddForce(this.launchForce);
        }
        this.facingDirection = Mathf.Atan2(this.launchForce.x, this.launchForce.z) * 57.29578f;
        Quaternion quaternion = Quaternion.Euler(0f, this.facingDirection, 0f);
        baseGT.rotation = quaternion;
        this.baseRigidBody.rotation = quaternion;
        this.targetRotation = quaternion;
        if (left)
        {
            this.launchElapsedTimeL = 0f;
        }
        else
        {
            this.launchElapsedTimeR = 0f;
        }
        if (leviMode)
        {
            this.launchElapsedTimeR = -100f;
        }
        if (baseAnimation.IsPlaying("special_petra"))
        {
            this.launchElapsedTimeR = -100f;
            this.launchElapsedTimeL = -100f;
            if (this.bulletRight != null)
            {
                bulletRightT.disable();
                this.releaseIfIHookSb();
            }
            if (this.bulletLeft != null)
            {
                bulletLeftT.disable();
                this.releaseIfIHookSb();
            }
        }
        this.sparks.enableEmission = false;
    }

    private void launchLeftRope(RaycastHit hit, bool single, int mode = 0)
    {
        if (this.currentGas != 0f)
        {
            this.useGas(0f);
            if (isSingle)
            {
                this.bulletLeft = (GameObject)UnityEngine.Object.Instantiate(Cach.hook != null ? Cach.hook : Cach.hook = (GameObject)Resources.Load("hook"));
            }
            else if (pviev.isMine)
            {
                this.bulletLeft = PhotonNetwork.Instantiate("hook", this.baseTransform.position, this.baseTransform.rotation, 0);
            }
            GameObject obj2 = !this.useGun ? this.hookRefL1 : this.hookRefL2;
            string str = !this.useGun ? "hookRefL1" : "hookRefL2";
            bulletLeftTT = bulletLeft.transform;
            bulletLeftTT.position = obj2.transform.position;
            bulletLeftT = this.bulletLeft.GetComponent<Bullet>();

            float num = !single ? ((hit.distance <= 50f) ? (hit.distance * 0.05f) : (hit.distance * 0.3f)) : 0f;
            Vector3 vector = (hit.point - ((Vector3)(this.baseTransform.right * num))) - bulletLeftTT.position;
            vector.Normalize();
            if (mode == 1)
            {
                bulletLeftT.launch((Vector3)(vector * 3f), this.baseRigidBody.velocity, str, true, this, true);
            }
            else
            {
                bulletLeftT.launch((Vector3)(vector * 3f), this.baseRigidBody.velocity, str, true, this, false);
            }
            this.launchPointLeft = Vector3.zero;
        }
    }

    private void launchRightRope(RaycastHit hit, bool single, int mode = 0)
    {
        if (this.currentGas != 0f)
        {
            this.useGas(0f);
            if (isSingle)
            {
                this.bulletRight = (GameObject)UnityEngine.Object.Instantiate(Cach.hook != null ? Cach.hook : Cach.hook = (GameObject)Resources.Load("hook"));
            }
            else if (pviev.isMine)
            {
                this.bulletRight = PhotonNetwork.Instantiate("hook", this.baseTransform.position, this.baseTransform.rotation, 0);
            }
            GameObject obj2 = !this.useGun ? this.hookRefR1 : this.hookRefR2;
            string str = !this.useGun ? "hookRefR1" : "hookRefR2";
            bulletRightTT = this.bulletRight.transform;
            bulletRightTT.position = obj2.transform.position;
            bulletRightT = this.bulletRight.GetComponent<Bullet>();
            float num = !single ? ((hit.distance <= 50f) ? (hit.distance * 0.05f) : (hit.distance * 0.3f)) : 0f;
            Vector3 vector = (hit.point + ((Vector3)(this.baseTransform.right * num))) - bulletRightTT.position;
            vector.Normalize();
            if (mode == 1)
            {
                bulletRightT.launch((Vector3)(vector * 5f), this.baseRigidBody.velocity, str, false, this, true);
            }
            else
            {
                bulletRightT.launch((Vector3)(vector * 3f), this.baseRigidBody.velocity, str, false, this, false);
            }
            this.launchPointRight = Vector3.zero;
        }
    }

    private void leftArmAimTo(Vector3 target)
    {
        float y = target.x - this.upperarmL.transform.position.x;
        float num2 = target.y - this.upperarmL.transform.position.y;
        float x = target.z - this.upperarmL.transform.position.z;
        float num4 = Mathf.Sqrt((y * y) + (x * x));
        this.handL.localRotation = Quaternion.Euler(90f, 0f, 0f);
        this.forearmL.localRotation = Quaternion.Euler(-90f, 0f, 0f);
        this.upperarmL.rotation = Quaternion.Euler(0f, 90f + (Mathf.Atan2(y, x) * 57.29578f), -Mathf.Atan2(num2, num4) * 57.29578f);
    }

    public void loadskin()
    {
        if (isSingle || base.photonView.isMine)
        {
            if ((int)FengGameManagerMKII.settings[93] == 1)
            {
                Renderer[] componentsInChildren = base.GetComponentsInChildren<Renderer>();
                for (int i = 0; i < componentsInChildren.Length; i++)
                {
                    Renderer renderer = componentsInChildren[i];
                    if (renderer.name.Contains("speed"))
                    {
                        renderer.enabled = false;
                    }
                }
            }
            if ((int)FengGameManagerMKII.settings[0] == 1 && FengGameManagerMKII.myCyanSkin != null)
            {
                CyanSkin skin = FengGameManagerMKII.myCyanSkin;
                string text14 = skin.horse + "," + skin.hair + "," + skin.eyes + "," + skin.glass + "," + skin.face + "," + skin.skin + "," + skin.costume + "," + skin.logo_and_cape + "," + skin.dmg_right + "," + skin.dmg_left + "," + skin.gas + "," + skin.hoodie + "," + skin.weapon_trail;
                if (isSingle)
                {
                    base.StartCoroutine(this.loadskinE(-1, text14));
                }
                else
                {
                    int num14 = -1;
                    if (this.myHorse != null)
                    {
                        num14 = this.myHorse.GetPhotonView().viewID;
                    }
                    base.photonView.RPC("loadskinRPC", PhotonTargets.AllBuffered, new object[] { num14, text14 });
                }
            }
        }
        else if (Application.loadedLevelName.Contains("characterCreation"))
        {
            CyanSkin skin = FengGameManagerMKII.myCyanSkin;
            string text14 = skin.horse + "," + skin.hair + "," + skin.eyes + "," + skin.glass + "," + skin.face + "," + skin.skin + "," + skin.costume + "," + skin.logo_and_cape + "," + skin.dmg_right + "," + skin.dmg_left + "," + skin.gas + "," + skin.hoodie + "," + skin.weapon_trail;

            base.StartCoroutine(this.loadskinE(-1, text14));
        }
    }

    public IEnumerator loadskinE(int horse, string url)
    {
        while (!this.hasspawn)
        {
            yield return null;
        }
        bool mipmap = true;
        bool iteratorVariable1 = false;
        if (((int)FengGameManagerMKII.settings[0x3f]) == 1)
        {
            mipmap = false;
        }
        string[] iteratorVariable2 = url.Split(new char[] { ',' });
        bool iteratorVariable3 = false;
        if (((int)FengGameManagerMKII.settings[15]) == 0)
        {
            iteratorVariable3 = true;
        }
        bool iteratorVariable4 = false;
        if (FengGameManagerMKII.lvlInfo.horse || (RCSettings.horseMode == 1))
        {
            iteratorVariable4 = true;
        }
        bool iteratorVariable5 = false;
        if ((isSingle) || this.photonView.isMine)
        {
            iteratorVariable5 = true;
        }
        if (this.setup.part_hair_1 != null)
        {
            Renderer renderer = this.setup.part_hair_1.renderer;
            if ((iteratorVariable2[1].EndsWith(".jpg") || iteratorVariable2[1].EndsWith(".png")) || iteratorVariable2[1].EndsWith(".jpeg"))
            {
                if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[1]))
                {
                    WWW link = new WWW(iteratorVariable2[1]);
                    yield return link;
                    if (link.error == null)
                    {
                        Texture2D iteratorVariable8 = cext.loadimage(link, mipmap, 0x30d40);
                        link.Dispose();
                        if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[1]))
                        {
                            iteratorVariable1 = true;
                            if (this.setup.myCostume.hairInfo.id >= 0)
                            {
                                renderer.material = CharacterMaterials.materials[this.setup.myCostume.hairInfo.texture];
                            }
                            renderer.material.mainTexture = iteratorVariable8;
                            FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[1], renderer.material);
                            renderer.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[1]];
                        }
                        else
                        {
                            renderer.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[1]];
                        }
                    }
                    else
                    {
                        PanelInformer.instance.Add("Error load skins " + base.name + ":" + link.url, PanelInformer.LOG_TYPE.DANGER);
                    }
                }
                else
                {
                    renderer.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[1]];
                }
            }
            else if (iteratorVariable2[1].ToLower() == "transparent")
            {
                renderer.enabled = false;
            }
        }
        if (this.setup.part_cape != null)
        {
            Renderer iteratorVariable9 = this.setup.part_cape.renderer;
            if ((iteratorVariable2[7].EndsWith(".jpg") || iteratorVariable2[7].EndsWith(".png")) || iteratorVariable2[7].EndsWith(".jpeg"))
            {
                if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[7]))
                {
                    WWW iteratorVariable10 = new WWW(iteratorVariable2[7]);
                    yield return iteratorVariable10;
                    if (iteratorVariable10.error == null)
                    {
                        Texture2D iteratorVariable11 = cext.loadimage(iteratorVariable10, mipmap, 0x30d40);
                        iteratorVariable10.Dispose();
                        if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[7]))
                        {
                            iteratorVariable1 = true;
                            iteratorVariable9.material.mainTexture = iteratorVariable11;
                            FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[7], iteratorVariable9.material);
                            iteratorVariable9.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[7]];
                        }
                        else
                        {
                            iteratorVariable9.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[7]];
                        }
                    }
                    else
                    {
                        PanelInformer.instance.Add("Error load skins " + base.name + ":" + iteratorVariable10.url, PanelInformer.LOG_TYPE.DANGER);
                    }
                }
                else
                {
                    iteratorVariable9.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[7]];
                }
            }
            else if (iteratorVariable2[7].ToLower() == "transparent")
            {
                iteratorVariable9.enabled = false;
            }
        }
        if (this.setup.part_chest_3 != null)
        {
            Renderer iteratorVariable12 = this.setup.part_chest_3.renderer;
            if ((iteratorVariable2[6].EndsWith(".jpg") || iteratorVariable2[6].EndsWith(".png")) || iteratorVariable2[6].EndsWith(".jpeg"))
            {
                if (!FengGameManagerMKII.linkHash[1].ContainsKey(iteratorVariable2[6]))
                {
                    WWW iteratorVariable13 = new WWW(iteratorVariable2[6]);
                    yield return iteratorVariable13;
                    if (iteratorVariable13.error == null)
                    {
                        Texture2D iteratorVariable14 = cext.loadimage(iteratorVariable13, mipmap, 0x7a120);
                        iteratorVariable13.Dispose();
                        if (!FengGameManagerMKII.linkHash[1].ContainsKey(iteratorVariable2[6]))
                        {
                            iteratorVariable1 = true;
                            iteratorVariable12.material.mainTexture = iteratorVariable14;
                            FengGameManagerMKII.linkHash[1].Add(iteratorVariable2[6], iteratorVariable12.material);
                            iteratorVariable12.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[6]];
                        }
                        else
                        {
                            iteratorVariable12.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[6]];
                        }
                    }
                    else
                    {
                        PanelInformer.instance.Add("Error load skins " + base.name + ":" + iteratorVariable13.url, PanelInformer.LOG_TYPE.DANGER);
                    }
                }
                else
                {
                    iteratorVariable12.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[6]];
                }
            }
            else if (iteratorVariable2[6].ToLower() == "transparent")
            {
                iteratorVariable12.enabled = false;
            }
        }
        foreach (Renderer iteratorVariable15 in this.GetComponentsInChildren<Renderer>())
        {
            if (iteratorVariable15.name.Contains(FengGameManagerMKII.s[1]))
            {
                if ((iteratorVariable2[1].EndsWith(".jpg") || iteratorVariable2[1].EndsWith(".png")) || iteratorVariable2[1].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[1]))
                    {
                        WWW iteratorVariable16 = new WWW(iteratorVariable2[1]);
                        yield return iteratorVariable16;
                        if (iteratorVariable16.error == null)
                        {
                            Texture2D iteratorVariable17 = cext.loadimage(iteratorVariable16, mipmap, 0x30d40);
                            iteratorVariable16.Dispose();
                            if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[1]))
                            {
                                iteratorVariable1 = true;
                                if (this.setup.myCostume.hairInfo.id >= 0)
                                {
                                    iteratorVariable15.material = CharacterMaterials.materials[this.setup.myCostume.hairInfo.texture];
                                }
                                iteratorVariable15.material.mainTexture = iteratorVariable17;
                                FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[1], iteratorVariable15.material);
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[1]];
                            }
                            else
                            {
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[1]];
                            }
                        }
                        else
                        {
                            PanelInformer.instance.Add("Error load skins " + base.name + ":" + iteratorVariable16.url, PanelInformer.LOG_TYPE.DANGER);
                        }
                    }
                    else
                    {
                        iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[1]];
                    }
                }
                else if (iteratorVariable2[1].ToLower() == "transparent")
                {
                    iteratorVariable15.enabled = false;
                }
            }
            else if (iteratorVariable15.name.Contains(FengGameManagerMKII.s[2]))
            {
                if ((iteratorVariable2[2].EndsWith(".jpg") || iteratorVariable2[2].EndsWith(".png")) || iteratorVariable2[2].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[2]))
                    {
                        WWW iteratorVariable18 = new WWW(iteratorVariable2[2]);
                        yield return iteratorVariable18;
                        if (iteratorVariable18.error == null)
                        {
                            Texture2D iteratorVariable19 = cext.loadimage(iteratorVariable18, mipmap, 0x30d40);
                            iteratorVariable18.Dispose();
                            if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[2]))
                            {
                                iteratorVariable1 = true;
                                iteratorVariable15.material.mainTextureScale = (Vector2)(iteratorVariable15.material.mainTextureScale * 8f);
                                iteratorVariable15.material.mainTextureOffset = new Vector2(0f, 0f);
                                iteratorVariable15.material.mainTexture = iteratorVariable19;
                                FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[2], iteratorVariable15.material);
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[2]];
                            }
                            else
                            {
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[2]];
                            }
                        }
                        else
                        {
                            PanelInformer.instance.Add("Error load skins " + base.name + ":" + iteratorVariable18.url, PanelInformer.LOG_TYPE.DANGER);
                        }
                    }
                    else
                    {
                        iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[2]];
                    }
                }
                else if (iteratorVariable2[2].ToLower() == "transparent")
                {
                    iteratorVariable15.enabled = false;
                }
            }
            else if (iteratorVariable15.name.Contains(FengGameManagerMKII.s[3]))
            {
                if ((iteratorVariable2[3].EndsWith(".jpg") || iteratorVariable2[3].EndsWith(".png")) || iteratorVariable2[3].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[3]))
                    {
                        WWW iteratorVariable20 = new WWW(iteratorVariable2[3]);
                        yield return iteratorVariable20;
                        if (iteratorVariable20.error == null)
                        {
                            Texture2D iteratorVariable21 = cext.loadimage(iteratorVariable20, mipmap, 0x30d40);
                            iteratorVariable20.Dispose();
                            if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[3]))
                            {
                                iteratorVariable1 = true;
                                iteratorVariable15.material.mainTextureScale = (Vector2)(iteratorVariable15.material.mainTextureScale * 8f);
                                iteratorVariable15.material.mainTextureOffset = new Vector2(0f, 0f);
                                iteratorVariable15.material.mainTexture = iteratorVariable21;
                                FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[3], iteratorVariable15.material);
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[3]];
                            }
                            else
                            {
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[3]];
                            }
                        }
                        else
                        {
                            PanelInformer.instance.Add("Error load skins " + base.name + ":" + iteratorVariable20.url, PanelInformer.LOG_TYPE.DANGER);
                        }
                    }
                    else
                    {
                        iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[3]];
                    }
                }
                else if (iteratorVariable2[3].ToLower() == "transparent")
                {
                    iteratorVariable15.enabled = false;
                }
            }
            else if (iteratorVariable15.name.Contains(FengGameManagerMKII.s[4]))
            {
                if ((iteratorVariable2[4].EndsWith(".jpg") || iteratorVariable2[4].EndsWith(".png")) || iteratorVariable2[4].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[4]))
                    {
                        WWW iteratorVariable22 = new WWW(iteratorVariable2[4]);
                        yield return iteratorVariable22;
                        if (iteratorVariable22.error == null)
                        {
                            Texture2D iteratorVariable23 = cext.loadimage(iteratorVariable22, mipmap, 0x30d40);
                            iteratorVariable22.Dispose();
                            if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[4]))
                            {
                                iteratorVariable1 = true;
                                iteratorVariable15.material.mainTextureScale = (Vector2)(iteratorVariable15.material.mainTextureScale * 8f);
                                iteratorVariable15.material.mainTextureOffset = new Vector2(0f, 0f);
                                iteratorVariable15.material.mainTexture = iteratorVariable23;
                                FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[4], iteratorVariable15.material);
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[4]];
                            }
                            else
                            {
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[4]];
                            }
                        }
                        else
                        {
                            PanelInformer.instance.Add("Error load skins " + base.name + ":" + iteratorVariable22.url, PanelInformer.LOG_TYPE.DANGER);
                        }
                    }
                    else
                    {
                        iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[4]];
                    }
                }
                else if (iteratorVariable2[4].ToLower() == "transparent")
                {
                    iteratorVariable15.enabled = false;
                }
            }
            else if ((iteratorVariable15.name.Contains(FengGameManagerMKII.s[5]) || iteratorVariable15.name.Contains(FengGameManagerMKII.s[6])) || iteratorVariable15.name.Contains(FengGameManagerMKII.s[10]))
            {
                if ((iteratorVariable2[5].EndsWith(".jpg") || iteratorVariable2[5].EndsWith(".png")) || iteratorVariable2[5].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[5]))
                    {
                        WWW iteratorVariable24 = new WWW(iteratorVariable2[5]);
                        yield return iteratorVariable24;
                        if (iteratorVariable24.error == null)
                        {
                            Texture2D iteratorVariable25 = cext.loadimage(iteratorVariable24, mipmap, 0x30d40);
                            iteratorVariable24.Dispose();
                            if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[5]))
                            {
                                iteratorVariable1 = true;
                                iteratorVariable15.material.mainTexture = iteratorVariable25;
                                FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[5], iteratorVariable15.material);
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[5]];
                            }
                            else
                            {
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[5]];
                            }
                        }
                        else
                        {
                            PanelInformer.instance.Add("Error load skins " + base.name + ":" + iteratorVariable24.url, PanelInformer.LOG_TYPE.DANGER);
                        }
                    }
                    else
                    {
                        iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[5]];
                    }
                }
                else if (iteratorVariable2[5].ToLower() == "transparent")
                {
                    iteratorVariable15.enabled = false;
                }
            }
            else if ((iteratorVariable15.name.Contains(FengGameManagerMKII.s[7]) || iteratorVariable15.name.Contains(FengGameManagerMKII.s[8])) || (iteratorVariable15.name.Contains(FengGameManagerMKII.s[9]) || iteratorVariable15.name.Contains(FengGameManagerMKII.s[0x18])))
            {
                if ((iteratorVariable2[6].EndsWith(".jpg") || iteratorVariable2[6].EndsWith(".png")) || iteratorVariable2[6].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[1].ContainsKey(iteratorVariable2[6]))
                    {
                        WWW iteratorVariable26 = new WWW(iteratorVariable2[6]);
                        yield return iteratorVariable26;
                        if (iteratorVariable26.error == null)
                        {
                            Texture2D iteratorVariable27 = cext.loadimage(iteratorVariable26, mipmap, 0x7a120);
                            iteratorVariable26.Dispose();
                            if (!FengGameManagerMKII.linkHash[1].ContainsKey(iteratorVariable2[6]))
                            {
                                iteratorVariable1 = true;
                                iteratorVariable15.material.mainTexture = iteratorVariable27;
                                FengGameManagerMKII.linkHash[1].Add(iteratorVariable2[6], iteratorVariable15.material);
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[6]];
                            }
                            else
                            {
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[6]];
                            }
                        }
                        else
                        {
                            PanelInformer.instance.Add("Error load skins " + base.name + ":" + iteratorVariable26.url, PanelInformer.LOG_TYPE.DANGER);
                        }
                    }
                    else
                    {
                        iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[6]];
                    }
                }
                else if (iteratorVariable2[6].ToLower() == "transparent")
                {
                    iteratorVariable15.enabled = false;
                }
            }
            else if (iteratorVariable15.name.Contains(FengGameManagerMKII.s[11]) || iteratorVariable15.name.Contains(FengGameManagerMKII.s[12]))
            {
                if ((iteratorVariable2[7].EndsWith(".jpg") || iteratorVariable2[7].EndsWith(".png")) || iteratorVariable2[7].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[7]))
                    {
                        WWW iteratorVariable28 = new WWW(iteratorVariable2[7]);
                        yield return iteratorVariable28;
                        if (iteratorVariable28.error == null)
                        {
                            Texture2D iteratorVariable29 = cext.loadimage(iteratorVariable28, mipmap, 0x30d40);
                            iteratorVariable28.Dispose();
                            if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[7]))
                            {
                                iteratorVariable1 = true;
                                iteratorVariable15.material.mainTexture = iteratorVariable29;
                                FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[7], iteratorVariable15.material);
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[7]];
                            }
                            else
                            {
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[7]];
                            }
                        }
                        else
                        {
                            PanelInformer.instance.Add("Error load skins " + base.name + ":" + iteratorVariable28.url, PanelInformer.LOG_TYPE.DANGER);
                        }
                    }
                    else
                    {
                        iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[7]];
                    }
                }
                else if (iteratorVariable2[7].ToLower() == "transparent")
                {
                    iteratorVariable15.enabled = false;
                }
            }
            else if (iteratorVariable15.name.Contains(FengGameManagerMKII.s[15]) || ((iteratorVariable15.name.Contains(FengGameManagerMKII.s[13]) || iteratorVariable15.name.Contains(FengGameManagerMKII.s[0x1a])) && !iteratorVariable15.name.Contains("_r")))
            {
                if ((iteratorVariable2[8].EndsWith(".jpg") || iteratorVariable2[8].EndsWith(".png")) || iteratorVariable2[8].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[1].ContainsKey(iteratorVariable2[8]))
                    {
                        WWW iteratorVariable30 = new WWW(iteratorVariable2[8]);
                        yield return iteratorVariable30;
                        if (iteratorVariable30.error == null)
                        {
                            Texture2D iteratorVariable31 = cext.loadimage(iteratorVariable30, mipmap, 0x7a120);
                            iteratorVariable30.Dispose();
                            if (!FengGameManagerMKII.linkHash[1].ContainsKey(iteratorVariable2[8]))
                            {
                                iteratorVariable1 = true;
                                iteratorVariable15.material.mainTexture = iteratorVariable31;
                                FengGameManagerMKII.linkHash[1].Add(iteratorVariable2[8], iteratorVariable15.material);
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[8]];
                            }
                            else
                            {
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[8]];
                            }
                        }
                        else
                        {
                            PanelInformer.instance.Add("Error load skins " + base.name + ":" + iteratorVariable30.url, PanelInformer.LOG_TYPE.DANGER);
                        }
                    }
                    else
                    {
                        iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[8]];
                    }
                }
                else if (iteratorVariable2[8].ToLower() == "transparent")
                {
                    iteratorVariable15.enabled = false;
                }
            }
            else if ((iteratorVariable15.name.Contains(FengGameManagerMKII.s[0x11]) || iteratorVariable15.name.Contains(FengGameManagerMKII.s[0x10])) || (iteratorVariable15.name.Contains(FengGameManagerMKII.s[0x1a]) && iteratorVariable15.name.Contains("_r")))
            {
                if ((iteratorVariable2[9].EndsWith(".jpg") || iteratorVariable2[9].EndsWith(".png")) || iteratorVariable2[9].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[1].ContainsKey(iteratorVariable2[9]))
                    {
                        WWW iteratorVariable32 = new WWW(iteratorVariable2[9]);
                        yield return iteratorVariable32;
                        if (iteratorVariable32.error == null)
                        {
                            Texture2D iteratorVariable33 = cext.loadimage(iteratorVariable32, mipmap, 0x7a120);
                            iteratorVariable32.Dispose();
                            if (!FengGameManagerMKII.linkHash[1].ContainsKey(iteratorVariable2[9]))
                            {
                                iteratorVariable1 = true;
                                iteratorVariable15.material.mainTexture = iteratorVariable33;
                                FengGameManagerMKII.linkHash[1].Add(iteratorVariable2[9], iteratorVariable15.material);
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[9]];
                            }
                            else
                            {
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[9]];
                            }
                        }
                        else
                        {
                            PanelInformer.instance.Add("Error load skins " + base.name + ":" + iteratorVariable32.url, PanelInformer.LOG_TYPE.DANGER);
                        }
                    }
                    else
                    {
                        iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[9]];
                    }
                }
                else if (iteratorVariable2[9].ToLower() == "transparent")
                {
                    iteratorVariable15.enabled = false;
                }
            }
            else if ((iteratorVariable15.name == FengGameManagerMKII.s[0x12]) && iteratorVariable3)
            {
                if ((iteratorVariable2[10].EndsWith(".jpg") || iteratorVariable2[10].EndsWith(".png")) || iteratorVariable2[10].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[10]))
                    {
                        WWW iteratorVariable34 = new WWW(iteratorVariable2[10]);
                        yield return iteratorVariable34;
                        if (iteratorVariable34.error == null)
                        {
                            Texture2D iteratorVariable35 = cext.loadimage(iteratorVariable34, mipmap, 0x30d40);
                            iteratorVariable34.Dispose();
                            if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[10]))
                            {
                                iteratorVariable1 = true;
                                iteratorVariable15.material.mainTexture = iteratorVariable35;
                                FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[10], iteratorVariable15.material);
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[10]];
                            }
                            else
                            {
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[10]];
                            }
                        }
                        else
                        {
                            PanelInformer.instance.Add("Error load skins " + base.name + ":" + iteratorVariable34.url, PanelInformer.LOG_TYPE.DANGER);
                        }
                    }
                    else
                    {
                        iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[10]];
                    }
                }
                else if (iteratorVariable2[10].ToLower() == "transparent")
                {
                    iteratorVariable15.enabled = false;
                }
            }
            else if (iteratorVariable15.name.Contains(FengGameManagerMKII.s[0x19]))
            {
                if ((iteratorVariable2[11].EndsWith(".jpg") || iteratorVariable2[11].EndsWith(".png")) || iteratorVariable2[11].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[11]))
                    {
                        WWW iteratorVariable36 = new WWW(iteratorVariable2[11]);
                        yield return iteratorVariable36;
                        if (iteratorVariable36.error == null)
                        {
                            Texture2D iteratorVariable37 = cext.loadimage(iteratorVariable36, mipmap, 0x30d40);
                            iteratorVariable36.Dispose();
                            if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[11]))
                            {
                                iteratorVariable1 = true;
                                iteratorVariable15.material.mainTexture = iteratorVariable37;
                                FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[11], iteratorVariable15.material);
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[11]];
                            }
                            else
                            {
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[11]];
                            }
                        }
                        else
                        {
                            PanelInformer.instance.Add("Error load skins " + base.name + ":" + iteratorVariable36.url, PanelInformer.LOG_TYPE.DANGER);
                        }
                    }
                    else
                    {
                        iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[11]];
                    }
                }
                else if (iteratorVariable2[11].ToLower() == "transparent")
                {
                    iteratorVariable15.enabled = false;
                }
            }
        }
        if (iteratorVariable4 && (horse >= 0))
        {
            GameObject gameObject = PhotonView.Find(horse).gameObject;
            if (gameObject != null)
            {
                foreach (Renderer iteratorVariable39 in gameObject.GetComponentsInChildren<Renderer>())
                {
                    if (iteratorVariable39.name.Contains(FengGameManagerMKII.s[0x13]))
                    {
                        if ((iteratorVariable2[0].EndsWith(".jpg") || iteratorVariable2[0].EndsWith(".png")) || iteratorVariable2[0].EndsWith(".jpeg"))
                        {
                            if (!FengGameManagerMKII.linkHash[1].ContainsKey(iteratorVariable2[0]))
                            {
                                WWW iteratorVariable40 = new WWW(iteratorVariable2[0]);
                                yield return iteratorVariable40;
                                if (iteratorVariable40.error == null)
                                {
                                    Texture2D iteratorVariable41 = cext.loadimage(iteratorVariable40, mipmap, 0x7a120);
                                    iteratorVariable40.Dispose();
                                    if (!FengGameManagerMKII.linkHash[1].ContainsKey(iteratorVariable2[0]))
                                    {
                                        iteratorVariable1 = true;
                                        iteratorVariable39.material.mainTexture = iteratorVariable41;
                                        FengGameManagerMKII.linkHash[1].Add(iteratorVariable2[0], iteratorVariable39.material);
                                        iteratorVariable39.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[0]];
                                    }
                                    else
                                    {
                                        iteratorVariable39.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[0]];
                                    }
                                }
                                else
                                {
                                    PanelInformer.instance.Add("Error load skins " + base.name + ":" + iteratorVariable40.url, PanelInformer.LOG_TYPE.DANGER);
                                }
                            }
                            else
                            {
                                iteratorVariable39.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[0]];
                            }
                        }
                        else if (iteratorVariable2[0].ToLower() == "transparent")
                        {
                            iteratorVariable39.enabled = false;
                        }
                    }
                }
            }
        }
        if (iteratorVariable5 && ((iteratorVariable2[12].EndsWith(".jpg") || iteratorVariable2[12].EndsWith(".png")) || iteratorVariable2[12].EndsWith(".jpeg")))
        {
            if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[12]))
            {
                WWW iteratorVariable42 = new WWW(iteratorVariable2[12]);
                yield return iteratorVariable42;
                if (iteratorVariable42.error == null)
                {
                    Texture2D iteratorVariable43 = cext.loadimage(iteratorVariable42, mipmap, 0x30d40);
                    iteratorVariable42.Dispose();
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[12]))
                    {
                        iteratorVariable1 = true;
                        this.leftbladetrail.MyMaterial.mainTexture = iteratorVariable43;
                        this.rightbladetrail.MyMaterial.mainTexture = iteratorVariable43;
                        FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[12], this.leftbladetrail.MyMaterial);
                        this.leftbladetrail.MyMaterial = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[12]];
                        this.rightbladetrail.MyMaterial = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[12]];
                        this.leftbladetrail2.MyMaterial = this.leftbladetrail.MyMaterial;
                        this.rightbladetrail2.MyMaterial = this.leftbladetrail.MyMaterial;
                    }
                    else
                    {
                        this.leftbladetrail2.MyMaterial = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[12]];
                        this.rightbladetrail2.MyMaterial = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[12]];
                        this.leftbladetrail.MyMaterial = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[12]];
                        this.rightbladetrail.MyMaterial = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[12]];
                    }
                }
                else
                {
                    PanelInformer.instance.Add("Error load skins " + base.name + ":" + iteratorVariable42.url, PanelInformer.LOG_TYPE.DANGER);
                }
            }
            else
            {
                this.leftbladetrail2.MyMaterial = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[12]];
                this.rightbladetrail2.MyMaterial = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[12]];
                this.leftbladetrail.MyMaterial = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[12]];
                this.rightbladetrail.MyMaterial = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[12]];
            }
        }
        if (iteratorVariable1)
        {
            FengGameManagerMKII.instance.unloadAssets();
        }
        yield break;
    }

    [RPC]
    public void loadskinRPC(int horse, string url, PhotonMessageInfo info)
    {
        if (((int)FengGameManagerMKII.settings[0]) == 1)
        {
            base.StartCoroutine(this.loadskinE(horse, url));
        }
        string[] str23 = url.Split(new char[] { ',' });
        PhotonPlayer player = info.sender;
        player.skin.horse = str23[0];
        player.skin.hair = str23[1];
        player.skin.eyes = str23[2];
        player.skin.glass = str23[3];
        player.skin.face = str23[4];
        player.skin.skin = str23[5];
        player.skin.costume = str23[6];
        player.skin.logo_and_cape = str23[7];
        player.skin.dmg_right = str23[8];
        player.skin.dmg_left = str23[9];
        player.skin.gas = str23[10];
        player.skin.hoodie = str23[11];
        player.skin.weapon_trail = str23[12];
    }

    public void markDie()
    {
        this.hasDied = true;
        this.state = HERO_STATE.Die;
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
    private void net3DMGSMOKE(bool ifON)
    {
        if (this.smoke_3dmg != null && ((int)FengGameManagerMKII.settings[283] == 0))
        {
            this.smoke_3dmg.enableEmission = ifON;
        }
    }

    [RPC]
    private void netContinueAnimation()
    {
        foreach (AnimationState state in baseAnimation)
        {
            if ((state != null) && (state.speed == 1f))
            {
                return;
            }
            state.speed = 1f;
        }
        this.playAnimation(this.currentPlayingClipName());
    }

    [RPC]
    private void netCrossFade(string aniName, float time)
    {
        this.currentAnimation = aniName;
        if (baseAnimation != null)
        {
            baseAnimation.CrossFade(aniName, time);
        }
    }

    [RPC]
    public void netDie(Vector3 v, bool isBite, int viewID = -1, string titanName = "", bool killByTitan = true, PhotonMessageInfo info = null)
    {
        PhotonPlayer player = info.sender;
        if ((pviev.isMine && (info != null)) && (IN_GAME_MAIN_CAMERA.gamemode != GAMEMODE.BOSS_FIGHT_CT))
        {
            if (FengGameManagerMKII.ignoreList.Contains(player.ID))
            {
                pviev.RPC("backToHumanRPC", PhotonTargets.Others, new object[0]);
                return;
            }
            if (!player.isLocal && !player.isMasterClient)
            {
                if (viewID < 0)
                {
                    if (titanName == "")
                    {
                        InRoomChat.instance.addLINE("Unusual Kill from ID " + player.ID.ToString() + " (possibly valid).");
                    }
                    else
                    {
                        InRoomChat.instance.addLINE("Unusual Kill from ID " + player.ID.ToString());
                    }
                }
                else if (PhotonView.Find(viewID) == null)
                {
                    InRoomChat.instance.addLINE("Unusual Kill from ID " + player.ID.ToString());
                }
                else if (PhotonView.Find(viewID).owner.ID != player.ID)
                {
                    InRoomChat.instance.addLINE("Unusual Kill from ID " + player.ID.ToString());
                }
            }
        }
        if (PhotonNetwork.isMasterClient)
        {
            this.onDeathEvent(viewID, killByTitan);
            int iD = pviev.owner.ID;
            if (FengGameManagerMKII.heroHash.ContainsKey(iD))
            {
                FengGameManagerMKII.heroHash.Remove(iD);
            }
        }
        if (pviev.isMine)
        {
            Vector3 vector = (Vector3)(Vector3.up * 5000f);
            if (this.myBomb != null)
            {
                this.myBomb.destroyMe();
            }
            if (this.myCannon != null)
            {
                PhotonNetwork.Destroy(this.myCannon);
            }
            if (this.titanForm && (this.eren_titan != null))
            {
                this.eren_titan.GetComponent<TITAN_EREN>().lifeTime = 0.1f;
            }
            if (this.skillCD != null)
            {
                this.skillCD.transform.localPosition = vector;
            }
        }
        if (this.bulletLeft != null)
        {
            bulletLeftT.removeMe();
        }
        if (this.bulletRight != null)
        {
            bulletRightT.removeMe();
        }

        if (!this.useGun && ((isSingle) || pviev.isMine))
        {
            DiactivateTrail(true);
        }
        this.falseAttack();
        this.breakApart2(v, isBite);
        if (pviev.isMine)
        {
            this.currentCameraT.setSpectorMode(false);
            this.currentCameraT.gameOver = true;
            FengGameManagerMKII.instance.myRespawnTime = 0f;
        }
        this.hasDied = true;
        if ((int)FengGameManagerMKII.settings[330] == 0)
        {
            Transform transform = audio_die != null ? audio_die : audio_die = base.transform.Find("audio_die");
            if (transform != null)
            {
                transform.parent = null;
                transform.GetComponent<AudioSource>().Play();
            }
            this.meatDie.Play();
        }
        smoothSyncMovement.disabled = true;
        if (pviev.isMine)
        {
            PhotonNetwork.RemoveRPCs(pviev);
            FengGameManagerMKII.instance.panelScore.AddDeath();
            PhotonNetwork.player.dead = true;
            PhotonNetwork.player.deaths = (PhotonNetwork.player.deaths) + 1;
            object[] parameters = new object[] { (titanName != string.Empty) ? 1 : 0 };
            FengGameManagerMKII.instance.photonView.RPC("someOneIsDead", PhotonTargets.MasterClient, parameters);
            if (viewID != -1)
            {
                PhotonView view = PhotonView.Find(viewID);
                if (view != null)
                {
                    FengGameManagerMKII.instance.sendKillInfo(killByTitan, "[" + player.ID.ToString() + "][FFFFFF]" + (view.owner.name2), false, (PhotonNetwork.player.name2), 0);
                    player.kills = (view.owner.kills) + 1;

                }
                else
                {
                    FengGameManagerMKII.instance.sendKillInfo(killByTitan, "[" + player.ID.ToString() + "][FFFFFF]" + (player.name2), false, (PhotonNetwork.player.name2), 0);
                    player.kills = (player.kills) + 1;
                }
            }
            else
            {
                FengGameManagerMKII.instance.sendKillInfo(titanName != string.Empty, "[" + player.ID.ToString() + "][FFFFFF]" + titanName, false, (PhotonNetwork.player.name2), 0);
            }
        }
        if (pviev.isMine)
        {
            PhotonNetwork.Destroy(pviev);
        }
    }

    [RPC]
    private void netDie2(int viewID = -1, string titanName = "", PhotonMessageInfo info = null)
    {
        GameObject obj2;
        PhotonPlayer player = info.sender;
        if ((pviev.isMine && (info != null)) && (IN_GAME_MAIN_CAMERA.gamemode != GAMEMODE.BOSS_FIGHT_CT))
        {
            if (FengGameManagerMKII.ignoreList.Contains(player.ID))
            {
                pviev.RPC("backToHumanRPC", PhotonTargets.Others, new object[0]);
                return;
            }
            if (!player.isLocal && !player.isMasterClient)
            {
                if (viewID < 0)
                {
                    if (titanName == "")
                    {
                        InRoomChat.instance.addLINE("Unusual Kill from ID " + player.ID.ToString() + " (possibly valid).");
                    }
                    else if ((RCSettings.bombMode == 0) && (RCSettings.deadlyCannons == 0))
                    {
                        InRoomChat.instance.addLINE("Unusual Kill from ID " + player.ID.ToString());
                    }
                }
                else if (PhotonView.Find(viewID) == null)
                {
                    InRoomChat.instance.addLINE("Unusual Kill from ID " + player.ID.ToString());
                }
                else if (PhotonView.Find(viewID).owner.ID != player.ID)
                {
                    InRoomChat.instance.addLINE("Unusual Kill from ID " + player.ID.ToString());
                }
            }
        }
        if (pviev.isMine)
        {
            Vector3 vector = (Vector3)(Vector3.up * 5000f);
            if (this.myBomb != null)
            {
                this.myBomb.destroyMe();
            }
            if (this.myCannon != null)
            {
                PhotonNetwork.Destroy(this.myCannon);
            }
            PhotonNetwork.RemoveRPCs(pviev);
            if (this.titanForm && (this.eren_titan != null))
            {
                this.eren_titan.GetComponent<TITAN_EREN>().lifeTime = 0.1f;
            }
            if (this.skillCD != null)
            {
                this.skillCD.transform.localPosition = vector;
            }
        }

        if (this.bulletLeft != null)
        {
            bulletLeftT.removeMe();
        }
        if (this.bulletRight != null)
        {
            bulletRightT.removeMe();
        }
        if ((int)FengGameManagerMKII.settings[330] == 0)
        {
            this.meatDie.Play();
            Transform transform = audio_die != null ? audio_die : audio_die = base.transform.Find("audio_die");
            transform.parent = null;
            transform.GetComponent<AudioSource>().Play();
        }
        if (pviev.isMine)
        {
            this.currentCameraT.setMainObject(null, true, false);
            this.currentCameraT.setSpectorMode(true);
            this.currentCameraT.gameOver = true;
            FengGameManagerMKII.instance.myRespawnTime = 0f;
        }
        this.falseAttack();
        this.hasDied = true;
        smoothSyncMovement.disabled = true;
        if ((isMulty) && pviev.isMine)
        {
            FengGameManagerMKII.instance.panelScore.AddDeath();
            PhotonNetwork.RemoveRPCs(pviev);
            PhotonNetwork.player.dead = true;
            PhotonNetwork.player.deaths = (PhotonNetwork.player.deaths) + 1;
            if (viewID != -1)
            {
                PhotonView view = PhotonView.Find(viewID);
                if (view != null)
                {
                    FengGameManagerMKII.instance.sendKillInfo(true, "[FFC000][" + player.ID.ToString() + "][FFFFFF]" + (view.owner.name2), false, (PhotonNetwork.player.name2), 0);
                    view.owner.kills = (view.owner.kills) + 1;
                }
                else
                {
                    FengGameManagerMKII.instance.sendKillInfo(true, "[FFC000][" + player.ID.ToString() + "][FFFFFF]" + (player.name2), false, (PhotonNetwork.player.name2), 0);

                    player.kills = (player.kills) + 1;
                }
            }
            else
            {
                FengGameManagerMKII.instance.sendKillInfo(true, "[FFC000][" + player.ID.ToString() + "][FFFFFF]" + titanName, false, (PhotonNetwork.player.name2), 0);
            }
            object[] parameters = new object[] { (titanName != string.Empty) ? 1 : 0 };
            FengGameManagerMKII.instance.photonView.RPC("someOneIsDead", PhotonTargets.MasterClient, parameters);
        }
        if ((IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE) && pviev.isMine)
        {
            obj2 = PhotonNetwork.Instantiate("hitMeat2", this.baseTransform.position, Quaternion.Euler(270f, 0f, 0f), 0);
        }
        else
        {
            obj2 = (GameObject)UnityEngine.Object.Instantiate(Cach.hitMeat2 != null ? Cach.hitMeat2 : Cach.hitMeat2 = (GameObject)Resources.Load("hitMeat2"));
        }
        obj2.transform.position = this.baseTransform.position;
        if (pviev.isMine)
        {
            PhotonNetwork.Destroy(pviev);
        }
        if (PhotonNetwork.isMasterClient)
        {
            this.onDeathEvent(viewID, true);
            int iD = pviev.owner.ID;
            if (FengGameManagerMKII.heroHash.ContainsKey(iD))
            {
                FengGameManagerMKII.heroHash.Remove(iD);
            }
        }
    }
    void DiactivateTrail(bool flag)
    {
        this.leftbladetrail.Deactivate(flag);
        this.rightbladetrail.Deactivate(flag);
        this.leftbladetrail2.Deactivate(flag);
        this.rightbladetrail2.Deactivate(flag);
    }
    public void netDieLocal(Vector3 v, bool isBite, int viewID = -1, string titanName = "", bool killByTitan = true)
    {
        if (pviev.isMine)
        {
            Vector3 vector = (Vector3)(Vector3.up * 5000f);
            if (this.titanForm && (this.eren_titan != null))
            {
                this.eren_titan.GetComponent<TITAN_EREN>().lifeTime = 0.1f;
            }
            if (this.myBomb != null)
            {
                this.myBomb.destroyMe();
            }
            if (this.myCannon != null)
            {
                PhotonNetwork.Destroy(this.myCannon);
            }
            if (this.skillCD != null)
            {
                this.skillCD.transform.localPosition = vector;
            }
        }
        if (this.bulletLeft != null)
        {
            bulletLeftT.removeMe();
        }
        if (this.bulletRight != null)
        {
            bulletRightT.removeMe();
        }

        if (!this.useGun && ((isSingle) || pviev.isMine))
        {
            DiactivateTrail(true);
        }
        this.falseAttack();
        this.breakApart2(v, isBite);
        if (pviev.isMine)
        {
            this.currentCameraT.setSpectorMode(false);
            this.currentCameraT.gameOver = true;
            FengGameManagerMKII.instance.myRespawnTime = 0f;
        }
        this.hasDied = true;
        if ((int)FengGameManagerMKII.settings[330] == 0)
        {
            this.meatDie.Play();
            Transform transform = audio_die != null ? audio_die : audio_die = base.transform.Find("audio_die");
            transform.parent = null;
            transform.GetComponent<AudioSource>().Play();
        }
        smoothSyncMovement.disabled = true;
        if (pviev.isMine)
        {
            PhotonNetwork.RemoveRPCs(pviev);
            PhotonNetwork.player.dead = true;
            PhotonNetwork.player.deaths = (PhotonNetwork.player.deaths) + 1;
            object[] parameters = new object[] { (titanName != string.Empty) ? 1 : 0 };
            FengGameManagerMKII.instance.photonView.RPC("someOneIsDead", PhotonTargets.MasterClient, parameters);
            if (viewID != -1)
            {
                PhotonView view = PhotonView.Find(viewID);
                if (view != null)
                {
                    FengGameManagerMKII.instance.sendKillInfo(killByTitan, (view.owner.name2), false, (PhotonNetwork.player.name2), 0);
                    view.owner.kills = (view.owner.kills) + 1;
                }
            }
            else
            {
                FengGameManagerMKII.instance.sendKillInfo(titanName != string.Empty, titanName, false, (PhotonNetwork.player.name2), 0);
            }
        }
        if (pviev.isMine)
        {
            PhotonNetwork.Destroy(pviev);
        }
        if (PhotonNetwork.isMasterClient)
        {
            this.onDeathEvent(viewID, killByTitan);
            int iD = pviev.owner.ID;
            if (FengGameManagerMKII.heroHash.ContainsKey(iD))
            {
                FengGameManagerMKII.heroHash.Remove(iD);
            }
        }
    }

    [RPC]
    private void netGrabbed(int id, bool leftHand)
    {
        this.titanWhoGrabMeID = id;
        this.grabbed(PhotonView.Find(id).gameObject, leftHand);
    }

    [RPC]
    private void netlaughAttack()
    {
        foreach (TITAN titan in FengGameManagerMKII.instance.titans)
        {
            if (((Vector3.Distance(titan.transform.position, this.baseTransform.position) < 50f) && (Vector3.Angle(titan.transform.forward, this.baseTransform.position - titan.transform.position) < 90f)))
            {
                titan.beLaughAttacked();
            }
        }
    }

    [RPC]
    private void netPauseAnimation()
    {
        foreach (AnimationState state in baseAnimation)
        {
            state.speed = 0f;
        }
    }

    [RPC]
    private void netPlayAnimation(string aniName)
    {
        this.currentAnimation = aniName;
        if (baseAnimation != null)
        {
            baseAnimation.Play(aniName);
        }
    }

    [RPC]
    private void netPlayAnimationAt(string aniName, float normalizedTime)
    {
        this.currentAnimation = aniName;
        if (baseAnimation != null)
        {
            baseAnimation.Play(aniName);
            baseAnimation[aniName].normalizedTime = normalizedTime;
        }
    }

    [RPC]
    private void netSetIsGrabbedFalse()
    {
        this.state = HERO_STATE.Idle;
    }

    [RPC]
    private void netTauntAttack(float tauntTime, float distance = 100f)
    {
        foreach (TITAN titan in FengGameManagerMKII.instance.titans)
        {
            if ((Vector3.Distance(titan.transform.position, this.baseTransform.position) < distance))
            {
                titan.beTauntedBy(baseG, tauntTime);
            }
        }
    }

    [RPC]
    private void netUngrabbed()
    {
        this.ungrabbed();
        this.netPlayAnimation(this.standAnimation);
        this.falseAttack();
    }

    public void onDeathEvent(int viewID, bool isTitan)
    {
        RCEvent event2;
        string[] strArray;
        if (isTitan)
        {
            if (FengGameManagerMKII.RCEvents.ContainsKey("OnPlayerDieByTitan"))
            {
                event2 = (RCEvent)FengGameManagerMKII.RCEvents["OnPlayerDieByTitan"];
                strArray = (string[])FengGameManagerMKII.RCVariableNames["OnPlayerDieByTitan"];
                if (FengGameManagerMKII.playerVariables.ContainsKey(strArray[0]))
                {
                    FengGameManagerMKII.playerVariables[strArray[0]] = pviev.owner;
                }
                else
                {
                    FengGameManagerMKII.playerVariables.Add(strArray[0], pviev.owner);
                }
                if (FengGameManagerMKII.titanVariables.ContainsKey(strArray[1]))
                {
                    FengGameManagerMKII.titanVariables[strArray[1]] = PhotonView.Find(viewID).gameObject.GetComponent<TITAN>();
                }
                else
                {
                    FengGameManagerMKII.titanVariables.Add(strArray[1], PhotonView.Find(viewID).gameObject.GetComponent<TITAN>());
                }
                event2.checkEvent();
            }
        }
        else if (FengGameManagerMKII.RCEvents.ContainsKey("OnPlayerDieByPlayer"))
        {
            event2 = (RCEvent)FengGameManagerMKII.RCEvents["OnPlayerDieByPlayer"];
            strArray = (string[])FengGameManagerMKII.RCVariableNames["OnPlayerDieByPlayer"];
            if (FengGameManagerMKII.playerVariables.ContainsKey(strArray[0]))
            {
                FengGameManagerMKII.playerVariables[strArray[0]] = pviev.owner;
            }
            else
            {
                FengGameManagerMKII.playerVariables.Add(strArray[0], pviev.owner);
            }
            if (FengGameManagerMKII.playerVariables.ContainsKey(strArray[1]))
            {
                FengGameManagerMKII.playerVariables[strArray[1]] = PhotonView.Find(viewID).owner;
            }
            else
            {
                FengGameManagerMKII.playerVariables.Add(strArray[1], PhotonView.Find(viewID).owner);
            }
            event2.checkEvent();
        }
    }

    public void pauseAnimation()
    {
        foreach (AnimationState state in baseAnimation)
        {
            if (state != null)
            {
                state.speed = 0f;
            }
        }
        if ((IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE) && pviev.isMine)
        {
            pviev.RPC("netPauseAnimation", PhotonTargets.Others, new object[0]);
        }
    }

    public void playAnimation(string aniName)
    {
        this.currentAnimation = aniName;
        baseAnimation.Play(aniName);
        if (PhotonNetwork.connected && pviev.isMine)
        {
            object[] parameters = new object[] { aniName };
            pviev.RPC("netPlayAnimation", PhotonTargets.Others, parameters);
        }
    }

    private void playAnimationAt(string aniName, float normalizedTime)
    {
        this.currentAnimation = aniName;
        baseAnimation.Play(aniName);
        baseAnimation[aniName].normalizedTime = normalizedTime;
        if (PhotonNetwork.connected && pviev.isMine)
        {
            object[] parameters = new object[] { aniName, normalizedTime };
            pviev.RPC("netPlayAnimationAt", PhotonTargets.Others, parameters);
        }
    }

    private void releaseIfIHookSb()
    {
        if (this.hookSomeOne && (this.hookTarget != null))
        {
            this.hookTarget.GetPhotonView().RPC("badGuyReleaseMe", this.hookTarget.GetPhotonView().owner, new object[0]);
            this.hookTarget = null;
            this.hookSomeOne = false;
        }
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

    public void resetAnimationSpeed()
    {
        foreach (AnimationState state in baseAnimation)
        {
            if (state != null)
            {
                state.speed = 1f;
            }
        }
        this.customAnimationSpeed();
    }

    [RPC]
    public void ReturnFromCannon(PhotonMessageInfo info)
    {
        if (info.sender == pviev.owner)
        {
            this.isCannon = false;
            smoothSyncMovement.disabled = false;
        }
    }

    private void rightArmAimTo(Vector3 target)
    {
        float y = target.x - this.upperarmR.transform.position.x;
        float num2 = target.y - this.upperarmR.transform.position.y;
        float x = target.z - this.upperarmR.transform.position.z;
        float num4 = Mathf.Sqrt((y * y) + (x * x));
        this.handR.localRotation = Quaternion.Euler(-90f, 0f, 0f);
        this.forearmR.localRotation = Quaternion.Euler(90f, 0f, 0f);
        this.upperarmR.rotation = Quaternion.Euler(180f, 90f + (Mathf.Atan2(y, x) * 57.29578f), Mathf.Atan2(num2, num4) * 57.29578f);
    }

    [RPC]
    private void RPCHookedByHuman(int hooker, Vector3 hookPosition)
    {
        this.hookBySomeOne = true;
        this.badGuy = PhotonView.Find(hooker).gameObject;
        if (Vector3.Distance(hookPosition, this.baseTransform.position) < 15f)
        {
            this.launchForce = PhotonView.Find(hooker).gameObject.transform.position - this.baseTransform.position;
            this.baseRigidBody.AddForce((Vector3)(-this.baseRigidBody.velocity * 0.9f), ForceMode.VelocityChange);
            float num = Mathf.Pow(this.launchForce.magnitude, 0.1f);
            if (this.grounded)
            {
                this.baseRigidBody.AddForce((Vector3)(Vector3.up * Mathf.Min((float)(this.launchForce.magnitude * 0.2f), (float)10f)), ForceMode.Impulse);

            }
            this.baseRigidBody.AddForce((Vector3)((this.launchForce * num) * 0.1f), ForceMode.Impulse);
            if (this.state != HERO_STATE.Grab)
            {
                this.dashTime = 1f;
                this.crossFade("dash", 0.05f);
                baseAnimation["dash"].time = 0.1f;
                this.state = HERO_STATE.AirDodge;
                this.falseAttack();
                this.facingDirection = Mathf.Atan2(this.launchForce.x, this.launchForce.z) * 57.29578f;
                Quaternion quaternion = Quaternion.Euler(0f, this.facingDirection, 0f);
                baseGT.rotation = quaternion;
                this.baseRigidBody.rotation = quaternion;
                this.targetRotation = quaternion;
            }
        }
        else
        {
            this.hookBySomeOne = false;
            this.badGuy = null;
            PhotonView.Find(hooker).RPC("hookFail", PhotonView.Find(hooker).owner, new object[0]);
        }
    }

    private void salute()
    {
        this.state = HERO_STATE.Salute;
        this.crossFade("salute", 0.1f);
    }

    private void setHookedPplDirection()
    {
        this.almostSingleHook = false;
        if (this.isRightHandHooked && this.isLeftHandHooked)
        {
            if ((this.bulletLeft != null) && (this.bulletRight != null))
            {
                Vector3 normal = bulletLeftTT.position - bulletRightTT.position;
                if (normal.sqrMagnitude < 4f)
                {
                    Vector3 vector2 = ((Vector3)((bulletLeftTT.position + bulletRightTT.position) * 0.5f)) - this.baseTransform.position;
                    this.facingDirection = Mathf.Atan2(vector2.x, vector2.z) * 57.29578f;
                    if (this.useGun && (this.state != HERO_STATE.Attack))
                    {
                        float current = -Mathf.Atan2(this.baseRigidBody.velocity.z, this.baseRigidBody.velocity.x) * 57.29578f;
                        float target = -Mathf.Atan2(vector2.z, vector2.x) * 57.29578f;
                        float num3 = -Mathf.DeltaAngle(current, target);
                        this.facingDirection += num3;
                    }
                    this.almostSingleHook = true;
                }
                else
                {
                    Vector3 to = this.baseTransform.position - bulletLeftTT.position;
                    Vector3 vector4 = this.baseTransform.position - bulletRightTT.position;
                    Vector3 vector5 = (Vector3)((bulletLeftTT.position + bulletRightTT.position) * 0.5f);
                    Vector3 from = this.baseTransform.position - vector5;
                    if ((Vector3.Angle(from, to) < 30f) && (Vector3.Angle(from, vector4) < 30f))
                    {
                        this.almostSingleHook = true;
                        Vector3 vector7 = vector5 - this.baseTransform.position;
                        this.facingDirection = Mathf.Atan2(vector7.x, vector7.z) * 57.29578f;
                    }
                    else
                    {
                        this.almostSingleHook = false;
                        Vector3 forward = this.baseTransform.forward;
                        Vector3.OrthoNormalize(ref normal, ref forward);
                        this.facingDirection = Mathf.Atan2(forward.x, forward.z) * 57.29578f;
                        float num4 = Mathf.Atan2(to.x, to.z) * 57.29578f;
                        if (Mathf.DeltaAngle(num4, this.facingDirection) > 0f)
                        {
                            this.facingDirection += 180f;
                        }
                    }
                }
            }
        }
        else
        {
            this.almostSingleHook = true;
            Vector3 zero = Vector3.zero;
            if (this.isRightHandHooked && (this.bulletRight != null))
            {
                zero = bulletRightTT.position - this.baseTransform.position;
            }
            else
            {
                if (!this.isLeftHandHooked || (this.bulletLeft == null))
                {
                    return;
                }
                zero = bulletLeftTT.position - this.baseTransform.position;
            }
            this.facingDirection = Mathf.Atan2(zero.x, zero.z) * 57.29578f;
            if (this.state != HERO_STATE.Attack)
            {
                float num5 = -Mathf.Atan2(this.baseRigidBody.velocity.z, this.baseRigidBody.velocity.x) * 57.29578f;
                float num6 = -Mathf.Atan2(zero.z, zero.x) * 57.29578f;
                float num7 = -Mathf.DeltaAngle(num5, num6);
                if (this.useGun)
                {
                    this.facingDirection += num7;
                }
                else
                {
                    float num8 = 0f;
                    if ((this.isLeftHandHooked && (num7 < 0f)) || (this.isRightHandHooked && (num7 > 0f)))
                    {
                        num8 = -0.1f;
                    }
                    else
                    {
                        num8 = 0.1f;
                    }
                    this.facingDirection += num7 * num8;
                }
            }
        }
    }

    [RPC]
    public void SetMyCannon(int viewID, PhotonMessageInfo info)
    {
        if (info.sender == pviev.owner)
        {
            PhotonView view = PhotonView.Find(viewID);
            if (view != null)
            {
                this.myCannon = view.gameObject;
                if (this.myCannon != null)
                {
                    this.myCannonBase = this.myCannon.transform;
                    this.myCannonPlayer = this.myCannonBase.Find("PlayerPoint");
                    this.isCannon = true;
                }
            }
        }
    }

    [RPC]
    public void SetMyPhotonCamera(float offset, PhotonMessageInfo info)
    {
        if (pviev.owner == info.sender)
        {
            this.CameraMultiplier = offset;
            smoothSyncMovement.PhotonCamera = true;
            this.isPhotonCamera = true;
        }
    }

    [RPC]
    private void setMyTeam(int val)
    {
        this.myTeam = val;
        this.checkBoxLeft.GetComponent<TriggerColliderWeapon>().myTeam = val;
        this.checkBoxRight.GetComponent<TriggerColliderWeapon>().myTeam = val;
        if ((isMulty) && PhotonNetwork.isMasterClient)
        {
            object[] objArray;
            if (RCSettings.friendlyMode > 0)
            {
                if (val != 1)
                {
                    objArray = new object[] { 1 };
                    pviev.RPC("setMyTeam", PhotonTargets.AllBuffered, objArray);
                }
            }
            else if (RCSettings.pvpMode == 1)
            {
                int num = pviev.owner.RCteam;
                if (val != num)
                {
                    objArray = new object[] { num };
                    pviev.RPC("setMyTeam", PhotonTargets.AllBuffered, objArray);
                }
            }
            else if ((RCSettings.pvpMode == 2) && (val != pviev.owner.ID))
            {
                objArray = new object[] { pviev.owner.ID };
                pviev.RPC("setMyTeam", PhotonTargets.AllBuffered, objArray);
            }
        }
    }

    public void setSkillHUDPosition()
    {

    }

    public void setSkillHUDPosition2()
    {
        this.skillCD = CyanMod.CachingsGM.Find("skill_cd_" + this.skillIDHUD);
        if (this.skillCD != null)
        {
            GameObject obj = CyanMod.CachingsGM.Find("skill_cd_bottom");
            this.skillCD.transform.localPosition = obj.transform.localPosition;
        }

    }
    public void setStat()
    {

    }
    public void setStat2()
    {
        this.skillCDLast = 1.5f;
        
        this.skillId = this.setup.myCostume.stat.skillId;
        if (this.skillId == "levi")
        {
            this.skillCDLast = 3.5f;
        }
        this.customAnimationSpeed();
        if (this.skillId == "armin")
        {
            this.skillCDLast = 5f;
        }
        if (this.skillId == "marco")
        {
            this.skillCDLast = 10f;
        }
        if (this.skillId == "jean")
        {
            this.skillCDLast = 0.001f;
        }
        if (this.skillId == "eren")
        {
            this.skillCDLast = 120f;
            if (isMulty)
            {
                if ((FengGameManagerMKII.lvlInfo.teamTitan || (FengGameManagerMKII.lvlInfo.type == GAMEMODE.RACING)) || ((FengGameManagerMKII.lvlInfo.type == GAMEMODE.PVP_CAPTURE) || (FengGameManagerMKII.lvlInfo.type == GAMEMODE.TROST)))
                {
                    this.skillId = "petra";
                    this.skillCDLast = 1f;
                }
                else
                {
                    int num = 0;
                    foreach (PhotonPlayer player in PhotonNetwork.playerList)
                    {
                        if ((player.isTitan == 1) && (player.character.ToUpper() == "EREN"))
                        {
                            num++;
                        }
                    }
                    if (num > 1)
                    {
                        this.skillId = "petra";
                        this.skillCDLast = 1f;
                    }
                }
            }
        }
        if (this.skillId == "sasha")
        {
            this.skillCDLast = 20f;
        }
        if (this.skillId == "petra")
        {
            this.skillCDLast = 3.5f;
        }
        if (SUPERUSER.isLogeds == "Logged")
        {
            skillCDLast = SUPERUSER.speed_dc;
        }
        this.bombInit();
        this.speed = ((float)this.setup.myCostume.stat.SPD) / 10f;
        this.totalGas = this.currentGas = this.setup.myCostume.stat.GAS;
        this.totalBladeSta = this.currentBladeSta = this.setup.myCostume.stat.BLA;
        this.baseRigidBody.mass = 0.5f - ((this.setup.myCostume.stat.ACL - 100) * 0.001f);
        GameObject.Find("skill_cd_bottom").transform.localPosition = new Vector3(0f, (-Screen.height * 0.5f) + 5f, 0f);
        this.skillCD = GameObject.Find("skill_cd_" + this.skillIDHUD);
        this.skillCD.transform.localPosition = GameObject.Find("skill_cd_bottom").transform.localPosition;
        GameObject.Find("GasUI").transform.localPosition = GameObject.Find("skill_cd_bottom").transform.localPosition;
        if ((isSingle) || base.photonView.isMine)
        {
            GameObject.Find("bulletL").GetComponent<UISprite>().enabled = false;
            GameObject.Find("bulletR").GetComponent<UISprite>().enabled = false;
            GameObject.Find("bulletL1").GetComponent<UISprite>().enabled = false;
            GameObject.Find("bulletR1").GetComponent<UISprite>().enabled = false;
            GameObject.Find("bulletL2").GetComponent<UISprite>().enabled = false;
            GameObject.Find("bulletR2").GetComponent<UISprite>().enabled = false;
            GameObject.Find("bulletL3").GetComponent<UISprite>().enabled = false;
            GameObject.Find("bulletR3").GetComponent<UISprite>().enabled = false;
            GameObject.Find("bulletL4").GetComponent<UISprite>().enabled = false;
            GameObject.Find("bulletR4").GetComponent<UISprite>().enabled = false;
            GameObject.Find("bulletL5").GetComponent<UISprite>().enabled = false;
            GameObject.Find("bulletR5").GetComponent<UISprite>().enabled = false;
            GameObject.Find("bulletL6").GetComponent<UISprite>().enabled = false;
            GameObject.Find("bulletR6").GetComponent<UISprite>().enabled = false;
            GameObject.Find("bulletL7").GetComponent<UISprite>().enabled = false;
            GameObject.Find("bulletR7").GetComponent<UISprite>().enabled = false;
        }
        if (this.setup.myCostume.uniform_type == UNIFORM_TYPE.CasualAHSS)
        {
            this.standAnimation = "AHSS_stand_gun";
            this.useGun = true;
            this.gunDummy = new GameObject();
            this.gunDummy.name = "gunDummy";
            this.gunDummy.transform.position = this.baseTransform.position;
            this.gunDummy.transform.rotation = this.baseTransform.rotation;
            this.myGroup = GROUP.A;
            this.setTeam2(2);
            if ((isSingle) || base.photonView.isMine)
            {
                GameObject.Find("bladeCL").GetComponent<UISprite>().enabled = false;
                GameObject.Find("bladeCR").GetComponent<UISprite>().enabled = false;
                GameObject.Find("bladel1").GetComponent<UISprite>().enabled = false;
                GameObject.Find("blader1").GetComponent<UISprite>().enabled = false;
                GameObject.Find("bladel2").GetComponent<UISprite>().enabled = false;
                GameObject.Find("blader2").GetComponent<UISprite>().enabled = false;
                GameObject.Find("bladel3").GetComponent<UISprite>().enabled = false;
                GameObject.Find("blader3").GetComponent<UISprite>().enabled = false;
                GameObject.Find("bladel4").GetComponent<UISprite>().enabled = false;
                GameObject.Find("blader4").GetComponent<UISprite>().enabled = false;
                GameObject.Find("bladel5").GetComponent<UISprite>().enabled = false;
                GameObject.Find("blader5").GetComponent<UISprite>().enabled = false;
                GameObject.Find("bulletL").GetComponent<UISprite>().enabled = true;
                GameObject.Find("bulletR").GetComponent<UISprite>().enabled = true;
                GameObject.Find("bulletL1").GetComponent<UISprite>().enabled = true;
                GameObject.Find("bulletR1").GetComponent<UISprite>().enabled = true;
                GameObject.Find("bulletL2").GetComponent<UISprite>().enabled = true;
                GameObject.Find("bulletR2").GetComponent<UISprite>().enabled = true;
                GameObject.Find("bulletL3").GetComponent<UISprite>().enabled = true;
                GameObject.Find("bulletR3").GetComponent<UISprite>().enabled = true;
                GameObject.Find("bulletL4").GetComponent<UISprite>().enabled = true;
                GameObject.Find("bulletR4").GetComponent<UISprite>().enabled = true;
                GameObject.Find("bulletL5").GetComponent<UISprite>().enabled = true;
                GameObject.Find("bulletR5").GetComponent<UISprite>().enabled = true;
                GameObject.Find("bulletL6").GetComponent<UISprite>().enabled = true;
                GameObject.Find("bulletR6").GetComponent<UISprite>().enabled = true;
                GameObject.Find("bulletL7").GetComponent<UISprite>().enabled = true;
                GameObject.Find("bulletR7").GetComponent<UISprite>().enabled = true;
                if (this.skillId != "bomb")
                {
                    this.skillCD.transform.localPosition = (Vector3)(Vector3.up * 5000f);
                }
            }
        }
        else if (this.setup.myCostume.sex == SEX.FEMALE)
        {
            this.standAnimation = "stand";
            this.setTeam2(1);
        }
        else
        {
            this.standAnimation = "stand_levi";
            this.setTeam2(1);
        }
      
    }

    public void setTeam(int team)
    {
        this.setMyTeam(team);
        if ((isMulty) && pviev.isMine)
        {
            object[] parameters = new object[] { team };
            pviev.RPC("setMyTeam", PhotonTargets.OthersBuffered, parameters);
            PhotonNetwork.player.team = team;
        }
    }

    public void setTeam2(int team)
    {
        if ((isMulty) && pviev.isMine)
        {
            object[] parameters = new object[] { team };
            pviev.RPC("setMyTeam", PhotonTargets.AllBuffered, parameters);
            PhotonNetwork.player.team = team;
        }
        else
        {
            this.setMyTeam(team);
        }
    }

    public void shootFlare(int type)
    {
        bool flag = false;
        GameObject gm = null;
        bool issp = isSingle;
        if ((type == 1) && (this.flare1CD == 0f))
        {
            this.flare1CD = this.flareTotalCD;
            flag = true;
            if (issp)
            {
                gm = Cach.flareBullet1 != null ? Cach.flareBullet1 : Cach.flareBullet1 = (GameObject)Resources.Load("FX/flareBullet1");
            }
        }
        if ((type == 2) && (this.flare2CD == 0f))
        {
            this.flare2CD = this.flareTotalCD;
            flag = true;
            if (issp)
            {
                gm = Cach.flareBullet2 != null ? Cach.flareBullet2 : Cach.flareBullet2 = (GameObject)Resources.Load("FX/flareBullet2");
            }
        }
        if ((type == 3) && (this.flare3CD == 0f))
        {
            this.flare3CD = this.flareTotalCD;
            flag = true;
            if (issp)
            {
                gm = Cach.flareBullet3 != null ? Cach.flareBullet3 : Cach.flareBullet3 = (GameObject)Resources.Load("FX/flareBullet3");
            }
        }
        if (flag)
        {
            if (issp)
            {
                GameObject obj2 = (GameObject)UnityEngine.Object.Instantiate(gm, base.transform.position, base.transform.rotation);
                obj2.GetComponent<FlareMovement>().dontShowHint();
                UnityEngine.Object.Destroy(obj2, 25f);
            }
            else
            {
                PhotonNetwork.Instantiate("FX/flareBullet" + type, base.transform.position, base.transform.rotation, 0).GetComponent<FlareMovement>().dontShowHint();
            }
        }
    }

    private void showAimUI()
    {

    }

    public void showSprites(bool flag)
    {
        if (cachedSprites.Count >= 1)
        {
            foreach (string sprite in this.cachedSprites.Keys)
            {
                if (cachedSprites[sprite] != null)
                {
                    if (flag)
                    {

                        if (sprite.StartsWith("skill_cd_") && sprite != "skill_cd_" + this.skillIDHUD && sprite != "skill_cd_bottom")
                        {
                            cachedSprites[sprite].enabled = false;

                        }
                        else
                        {

                            cachedSprites[sprite].enabled = true;
                        }
                        if (useGun)
                        {
                            if (sprite.StartsWith("blade"))
                            {
                                cachedSprites[sprite].enabled = false;
                            }
                        }
                        else
                        {
                            if (sprite.StartsWith("bullet"))
                            {
                                cachedSprites[sprite].enabled = false;
                            }
                        }
                    }
                    else
                    {
                        cachedSprites[sprite].enabled = false;
                    }
                 
                   
                }
            }
            showsprites = flag;
        }
    }

    private void showAimUI2()
    {

        Vector3 vector;
        this.checkTitan();
        if (Screen.showCursor)
        {
            vector = (Vector3)(Vector3.up * 10000f);
            if (this.crossR2 != null)
            {
                this.crossR2T.localPosition = vector;
                this.crossR1T.localPosition = vector;
                this.crossL2T.localPosition = vector;
                this.crossL1T.localPosition = vector;
            }
            this.LabelDistanceT.localPosition = vector;
            this.cross2T.localPosition = vector;
            this.cross1T.localPosition = vector;
        }
        else
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            LayerMask mask = ((int)1) << LayerMask.NameToLayer("Ground");
            LayerMask mask2 = ((int)1) << LayerMask.NameToLayer("EnemyBox");
            LayerMask mask3 = mask2 | mask;
            if (Physics.Raycast(ray, out hit, 1E+07f, mask3.value))
            {
                RaycastHit hit2;
                this.cross1T.localPosition = Input.mousePosition;
                this.cross1T.localPosition -= new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);
                this.cross2T.localPosition = this.cross1T.localPosition;
                vector = hit.point - this.baseTransform.position;
                float magnitude = vector.magnitude;

                string str = string.Empty;
                if (((int)FengGameManagerMKII.settings[0xbd]) == 1)
                {
                    str = this.currentSpeed.ToString("F1") + " u/s";
                }
                else if (((int)FengGameManagerMKII.settings[0xbd]) == 2)
                {
                    str = ((this.currentSpeed / 100f)).ToString("F1") + "K";
                }
                labeldist.text = ((magnitude <= 1000f) ? ((int)magnitude).ToString() : "???") + "\n" + str;
                if (magnitude > 120f)
                {

                    cross1T.localPosition += (Vector3)(Vector3.up * 10000f);
                    this.LabelDistanceT.localPosition = cross2T.localPosition;
                }
                else
                {

                    cross2T.localPosition += (Vector3)(Vector3.up * 10000f);
                    this.LabelDistanceT.localPosition = cross1T.localPosition;
                }

                this.LabelDistanceT.localPosition -= new Vector3(0f, 15f, 0f);
                if (aim && this.crossR2 != null)
                {
                    Vector3 vector2 = new Vector3(0f, 0.4f, 0f);
                    vector2 -= (Vector3)(this.baseTransform.right * 0.3f);
                    Vector3 vector3 = new Vector3(0f, 0.4f, 0f);
                    vector3 += (Vector3)(this.baseTransform.right * 0.3f);
                    float num2 = (hit.distance <= 50f) ? (hit.distance * 0.05f) : (hit.distance * 0.3f);
                    Vector3 vector4 = (hit.point - ((Vector3)(this.baseTransform.right * num2))) - (this.baseTransform.position + vector2);
                    Vector3 vector5 = (hit.point + ((Vector3)(this.baseTransform.right * num2))) - (this.baseTransform.position + vector3);
                    vector4.Normalize();
                    vector5.Normalize();
                    vector4 = (Vector3)(vector4 * 1000000f);
                    vector5 = (Vector3)(vector5 * 1000000f);

                    if (Physics.Linecast(this.baseTransform.position + vector2, (this.baseTransform.position + vector2) + vector4, out hit2, mask3.value))
                    {

                        crossL1T.localPosition = currentCamera.WorldToScreenPoint(hit2.point);
                        crossL1T.localPosition -= new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);
                        crossL1T.localRotation = Quaternion.Euler(0f, 0f, (Mathf.Atan2(crossL1T.localPosition.y - (Input.mousePosition.y - (Screen.height * 0.5f)), crossL1T.localPosition.x - (Input.mousePosition.x - (Screen.width * 0.5f))) * 57.29578f) + 180f);

                        crossL2T.localPosition = crossL1T.localPosition;
                        crossL2T.localRotation = crossL1T.localRotation;
                        if (hit2.distance > 120f)
                        {
                            crossL1T.localPosition += (Vector3)(Vector3.up * 10000f);
                        }
                        else
                        {

                            crossL2T.localPosition += (Vector3)(Vector3.up * 10000f);
                        }
                    }
                    if (Physics.Linecast(this.baseTransform.position + vector3, (this.baseTransform.position + vector3) + vector5, out hit2, mask3.value))
                    {

                        crossR1T.localPosition = currentCamera.WorldToScreenPoint(hit2.point);

                        crossR1T.localPosition -= new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);
                        crossR1T.localRotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(crossR1T.localPosition.y - (Input.mousePosition.y - (Screen.height * 0.5f)), crossR1T.localPosition.x - (Input.mousePosition.x - (Screen.width * 0.5f))) * 57.29578f);

                        crossR2T.localPosition = crossR1T.localPosition;
                        crossR2T.localRotation = crossR1T.localRotation;
                        if (hit2.distance > 120f)
                        {

                            crossR1T.localPosition += (Vector3)(Vector3.up * 10000f);
                        }
                        else
                        {

                            crossR2T.localPosition += (Vector3)(Vector3.up * 10000f);
                        }
                    }
                }
            }
        }
    }

    private void showFlareCD()
    {

    }

    private void showFlareCD2()
    {
        if (this.cachedSprites["UIflare1"] != null && showsprites)
        {
            this.cachedSprites["UIflare1"].fillAmount = (this.flareTotalCD - this.flare1CD) / this.flareTotalCD;
            this.cachedSprites["UIflare2"].fillAmount = (this.flareTotalCD - this.flare2CD) / this.flareTotalCD;
            this.cachedSprites["UIflare3"].fillAmount = (this.flareTotalCD - this.flare3CD) / this.flareTotalCD;
        }
    }

    private void showGas()
    {

    }

    private void showGas2()
    {
        if (showsprites)
        {
            float num = this.currentGas / this.totalGas;
            float num2 = this.currentBladeSta / this.totalBladeSta;
            this.cachedSprites["gasL1"].fillAmount = this.cachedSprites["gasR1"].fillAmount = this.currentGas / this.totalGas;

            if (!this.useGun)
            {
                this.cachedSprites["bladeCL"].fillAmount = this.cachedSprites["bladeCR"].fillAmount = this.currentBladeSta / this.totalBladeSta;

                if (num <= 0f)
                {
                    this.cachedSprites["gasL"].color = this.cachedSprites["gasR"].color = Color.red;
                }
                else if (num < 0.3f)
                {
                    this.cachedSprites["gasL"].color = this.cachedSprites["gasR"].color = Color.yellow;
                }
                else
                {
                    this.cachedSprites["gasL"].color = this.cachedSprites["gasR"].color = (Color)FengGameManagerMKII.settings[356];
                }
                if (num2 <= 0f)
                {
                    this.cachedSprites["bladel1"].color = this.cachedSprites["blader1"].color = Color.red;

                }
                else if (num2 < 0.3f)
                {
                    this.cachedSprites["bladel1"].color = this.cachedSprites["blader1"].color = Color.yellow;

                }
                else
                {
                    this.cachedSprites["bladel1"].color = this.cachedSprites["blader1"].color = (Color)FengGameManagerMKII.settings[356];
                }
                if (this.currentBladeNum <= 4)
                {
                    this.cachedSprites["bladel5"].enabled = this.cachedSprites["blader5"].enabled = false;
                }
                else
                {
                    this.cachedSprites["bladel5"].enabled = this.cachedSprites["blader5"].enabled = true;
                }
                if (this.currentBladeNum <= 3)
                {
                    this.cachedSprites["bladel4"].enabled = this.cachedSprites["blader4"].enabled = false;
                }
                else
                {
                    this.cachedSprites["bladel4"].enabled = this.cachedSprites["blader4"].enabled = true;
                }
                if (this.currentBladeNum <= 2)
                {
                    this.cachedSprites["bladel3"].enabled = this.cachedSprites["blader3"].enabled = false;
                }
                else
                {
                    this.cachedSprites["bladel3"].enabled = this.cachedSprites["blader3"].enabled = true;
                }
                if (this.currentBladeNum <= 1)
                {
                    this.cachedSprites["bladel2"].enabled = this.cachedSprites["blader2"].enabled = false;
                }
                else
                {
                    this.cachedSprites["bladel2"].enabled = this.cachedSprites["blader2"].enabled = true;
                }
                if (this.currentBladeNum <= 0)
                {
                    this.cachedSprites["bladel1"].enabled = this.cachedSprites["blader1"].enabled = false;
                }
                else
                {
                    this.cachedSprites["bladel1"].enabled = this.cachedSprites["blader1"].enabled = true;
                }
            }
            else
            {
                if (this.leftGunHasBullet)
                {
                    this.cachedSprites["bulletL"].enabled = true;
                }
                else
                {
                    this.cachedSprites["bulletL"].enabled = false;
                }
                if (this.rightGunHasBullet)
                {
                    this.cachedSprites["bulletR"].enabled = true;
                }
                else
                {
                    this.cachedSprites["bulletR"].enabled = false;
                }
            }
        }
    }

    [RPC]
    private void showHitDamage()
    {
        GameObject target = CyanMod.CachingsGM.Find("LabelScore");
        if (target != null)
        {
            this.speed = Mathf.Max(10f, this.speed);
            target.GetComponent<UILabel>().text = this.speed.ToString();
            target.transform.localScale = Vector3.zero;
            this.speed = (int)(this.speed * 0.1f);
            this.speed = Mathf.Clamp(this.speed, 40f, 150f);
            iTween.Stop(target);
            object[] args = new object[] { "x", this.speed, "y", this.speed, "z", this.speed, "easetype", iTween.EaseType.easeOutElastic, "time", 1f };
            iTween.ScaleTo(target, iTween.Hash(args));
            object[] objArray2 = new object[] { "x", 0, "y", 0, "z", 0, "easetype", iTween.EaseType.easeInBounce, "time", 0.5f, "delay", 2f };
            iTween.ScaleTo(target, iTween.Hash(objArray2));
        }
    }

    UISprite skillCDSprite;
    private void showSkillCD()
    {
        if (this.skillCD != null && showsprites)
        {
            (skillCDSprite != null ? skillCDSprite : skillCDSprite = this.skillCD.GetComponent<UISprite>()).fillAmount = (this.skillCDLast - this.skillCDDuration) / this.skillCDLast;
        }
    }

    [RPC]
    public void SpawnCannonRPC(string settings, PhotonMessageInfo info)
    {
        if ((info.sender.isMasterClient && pviev.isMine) && (this.myCannon == null))
        {
            if ((this.myHorse != null) && this.isMounted)
            {
                this.getOffHorse();
            }
            this.idle();
            if (this.bulletLeft != null)
            {
                bulletLeftT.removeMe();
            }
            if (this.bulletRight != null)
            {
                bulletRightT.removeMe();
            }
            if ((this.smoke_3dmg.enableEmission && (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)) && pviev.isMine)
            {
                object[] parameters = new object[] { false };
                pviev.RPC("net3DMGSMOKE", PhotonTargets.Others, parameters);
            }
            this.smoke_3dmg.enableEmission = false;
            this.baseRigidBody.velocity = Vector3.zero;
            string[] strArray = settings.Split(new char[] { ',' });
            if (strArray.Length > 15)
            {
                this.myCannon = PhotonNetwork.Instantiate("RCAsset/" + strArray[1], new Vector3(Convert.ToSingle(strArray[12]), Convert.ToSingle(strArray[13]), Convert.ToSingle(strArray[14])), new Quaternion(Convert.ToSingle(strArray[15]), Convert.ToSingle(strArray[0x10]), Convert.ToSingle(strArray[0x11]), Convert.ToSingle(strArray[0x12])), 0);
            }
            else
            {
                this.myCannon = PhotonNetwork.Instantiate("RCAsset/" + strArray[1], new Vector3(Convert.ToSingle(strArray[2]), Convert.ToSingle(strArray[3]), Convert.ToSingle(strArray[4])), new Quaternion(Convert.ToSingle(strArray[5]), Convert.ToSingle(strArray[6]), Convert.ToSingle(strArray[7]), Convert.ToSingle(strArray[8])), 0);
            }

            this.myCannonBase = this.myCannon.transform;
            this.myCannonPlayer = this.myCannon.transform.Find("PlayerPoint");
            this.isCannon = true;
            this.myCannon.GetComponent<Cannon>().myHero = this;
            this.myCannonRegion = null;
            Camera.main.GetComponent<IN_GAME_MAIN_CAMERA>().setMainObject(this.myCannon.transform.Find("Barrel").Find("FiringPoint").gameObject, true, false);
            Camera.main.fieldOfView = 55f;
            pviev.RPC("SetMyCannon", PhotonTargets.OthersBuffered, new object[] { this.myCannon.GetPhotonView().viewID });
            this.skillCDLastCannon = this.skillCDLast;
            this.skillCDLast = 3.5f;
            this.skillCDDuration = 3.5f;
        }
    }
    void CheckStats()
    {
        if (PhotonNetwork.isMasterClient)
        {
            PhotonPlayer player = base.photonView.owner;
            if (player.character.StartsWith("Set"))
            {
                if (player.statACL > 125 || player.statBLA > 125 || player.statGAS > 125 || player.statSPD > 125)
                {
                    FengGameManagerMKII.instance.kickPlayerRC(player, true, INC.la("excessive_stats_player"));
                    return;
                }
            }
            else
            {
                int num = (player.statACL);
                int num2 = (player.statBLA);
                int num3 = (player.statGAS);
                int num4 = (player.statSPD);
                if (num > 150 || num2 > 125 || num3 > 150 || num4 > 140)
                {
                    FengGameManagerMKII.instance.kickPlayerRC(player, true, INC.la("excessive_stats_player"));
                    return;
                }
            }
        }
    }
    private void Start()
    {
        FengGameManagerMKII.instance.addHero(this);
        if ((FengGameManagerMKII.lvlInfo.horse || (RCSettings.horseMode == 1)) && ((isMulty) && pviev.isMine))
        {
            this.myHorse = PhotonNetwork.Instantiate("horse", this.baseTransform.position + ((Vector3)(Vector3.up * 5f)), this.baseTransform.rotation, 0);
            this.myHorse.GetComponent<Horse>().myHero = baseG;
            this.myHorse.GetComponent<TITAN_CONTROLLER>().isHorse = true;
        }
        this.sparks = this.baseTransform.Find("slideSparks").GetComponent<ParticleSystem>();
        this.smoke_3dmg = this.baseTransform.Find("3dmg_smoke").GetComponent<ParticleSystem>();
        this.baseTransform.localScale = new Vector3(this.myScale, this.myScale, this.myScale);
        this.facingDirection = this.baseTransform.rotation.eulerAngles.y;
        this.targetRotation = Quaternion.Euler(0f, this.facingDirection, 0f);
        this.smoke_3dmg.enableEmission = false;
        this.sparks.enableEmission = false;
        this.speedFXPS = this.speedFX1.GetComponent<ParticleSystem>();
        this.speedFXPS.enableEmission = false;
        if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.MULTIPLAYER)
        {
            if (Minimap.instance != null)
            {
                Minimap.instance.TrackGameObjectOnMinimap(baseG, Color.green, false, true, Minimap.IconStyle.CIRCLE);
            }
        }
        else
        {
            if (PhotonNetwork.isMasterClient)
            {
                int iD = pviev.owner.ID;
                if (FengGameManagerMKII.heroHash.ContainsKey(iD))
                {
                    FengGameManagerMKII.heroHash[iD] = this;
                }
                else
                {
                    FengGameManagerMKII.heroHash.Add(iD, this);
                }
            }

            this.myNetWorkName = (GameObject)UnityEngine.Object.Instantiate(Cach.UI_LabelNameOverHead != null ? Cach.UI_LabelNameOverHead : Cach.UI_LabelNameOverHead = (GameObject)Resources.Load("UI/LabelNameOverHead"));
            this.myNetWorkName.name = "LabelNameOverHead";
            this.myNetWorkName.transform.parent = FengGameManagerMKII.instance.uiT.panels[0].transform;
            this.myNetWorkName.transform.localScale = new Vector3(6f, 6f, 6f);
            myNetWorkName.GetComponent<UILabel>().enabled = false;
            TextMesh tm = myNetWorkName.GetComponent<TextMesh>();
            if (tm == null)
            {
                tm = myNetWorkName.AddComponent<TextMesh>();
            }
            MeshRenderer mr = myNetWorkName.GetComponent<MeshRenderer>();
            if (mr == null)
            {
                mr = myNetWorkName.AddComponent<MeshRenderer>();
            }
            mr.material = FengGameManagerMKII.fotHUD.material;
            tm.font = FengGameManagerMKII.fotHUD;

            tm.fontSize = 20;
            tm.anchor = TextAnchor.MiddleCenter;
            tm.alignment = TextAlignment.Center;
            this.myNetWorkNameT = tm;
            myNetWorkNameT.text = string.Empty;

            if (pviev.isMine)
            {
                if (Minimap.instance != null)
                {
                    Minimap.instance.TrackGameObjectOnMinimap(baseG, Color.green, false, true, Minimap.IconStyle.CIRCLE);
                }
                smoothSyncMovement.PhotonCamera = true;
                pviev.RPC("SetMyPhotonCamera", PhotonTargets.OthersBuffered, new object[] { FengGameManagerMKII.instance.distanceSlider + 0.3f });
            }
            else
            {
                bool flag = false;
                switch ((pviev.owner.RCteam))
                {
                    case 1:
                        flag = true;
                        if (Minimap.instance != null)
                        {
                            Minimap.instance.TrackGameObjectOnMinimap(baseG, Color.cyan, false, true, Minimap.IconStyle.CIRCLE);
                        }
                        break;

                    case 2:
                        flag = true;
                        if (Minimap.instance != null)
                        {
                            Minimap.instance.TrackGameObjectOnMinimap(baseG, Color.magenta, false, true, Minimap.IconStyle.CIRCLE);
                        }
                        break;
                }

                if ((pviev.owner.team) == 2)
                {

                    this.myNetWorkNameT.text = "<color=#FF0000>AHSS\n</color>";
                    if (!flag && (Minimap.instance != null))
                    {
                        Minimap.instance.TrackGameObjectOnMinimap(baseG, Color.red, false, true, Minimap.IconStyle.CIRCLE);
                    }
                }
                else if (!flag && (Minimap.instance != null))
                {
                    Minimap.instance.TrackGameObjectOnMinimap(baseG, Color.blue, false, true, Minimap.IconStyle.CIRCLE);
                }
            }
            string str = (pviev.owner.guildName).toHex();
            if (str != string.Empty)
            {

                string text = myNetWorkNameT.text + "<color=#FFFF00>" + str + "</color>\n" + pviev.owner.ishexname;

                myNetWorkNameT.text = string.Concat(text);
            }
            else
            {
                myNetWorkNameT.text = myNetWorkNameT.text + (pviev.owner.ishexname);
            }
            id = base.photonView.owner.ID;
        }
        if ((IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE) && !pviev.isMine)
        {
            baseG.layer = LayerMask.NameToLayer("NetworkObject");
            if (IN_GAME_MAIN_CAMERA.dayLight == DayLight.Night)
            {
                GameObject obj3 = (GameObject)UnityEngine.Object.Instantiate(Cach.flashlight != null ? Cach.flashlight : Cach.flashlight = (GameObject)Resources.Load("flashlight"));
                obj3.transform.parent = this.baseTransform;
                obj3.transform.position = this.baseTransform.position + Vector3.up;
                obj3.transform.rotation = Quaternion.Euler(353f, 0f, 0f);
            }
            this.setup.init();
            this.setup.myCostume = new HeroCostume();
            this.setup.myCostume = CostumeConeveter.PhotonDataToHeroCostume2(pviev.owner);
            this.setup.setCharacterComponent();
            UnityEngine.Object.Destroy(this.checkBoxLeft);
            UnityEngine.Object.Destroy(this.checkBoxRight);
            UnityEngine.Object.Destroy(this.leftbladetrail);
            UnityEngine.Object.Destroy(this.rightbladetrail);
            UnityEngine.Object.Destroy(this.leftbladetrail2);
            UnityEngine.Object.Destroy(this.rightbladetrail2);
            this.hasspawn = true;
        }
        else
        {

            this.currentCamera = maincamera.GetComponent<Camera>();
            this.currentCameraT = maincamera.GetComponent<IN_GAME_MAIN_CAMERA>();
            this.loadskin();
            this.hasspawn = true;
            base.StartCoroutine(this.reloadSky());
        }

        this.bombImmune = false;
        if (RCSettings.bombMode == 1)
        {
            this.bombImmune = true;
            base.StartCoroutine(this.stopImmunity());
        }
        if (isMulty)
        {
            myPhotonplayer = base.gameObject.GetComponent<SmoothSyncMovement>().photonView.owner;
        }
        if (isSingle || pviev.isMine)
        {
            myHero = this;
            FengGameManagerMKII.instance.ApplySettings();
        }
        else
        {
            if ((int)FengGameManagerMKII.settings[291] == 1)
            {
                name_remove();
            }
        }
        if ((int)FengGameManagerMKII.settings[398] == 1)
        {
            CheckStats();
        }

        if ((int)FengGameManagerMKII.settings[385] == 1)
        {
            if ((int)FengGameManagerMKII.settings[386] != 0)
            {
                Color cl = (Color)FengGameManagerMKII.settings[389];
                if (isMulty)
                {
                    object[] parameters = new object[] { (int)FengGameManagerMKII.settings[386], 0, 0, cl.r, cl.g, cl.b, cl.a };
                    base.photonView.CyanRPC("LoadObjects", parameters);
                }
                if (isSingle)
                {
                    LoadObjects((int)FengGameManagerMKII.settings[386], 0, 0, cl.r, cl.g, cl.b, cl.a);
                }

            }
            if ((int)FengGameManagerMKII.settings[387] != 0)
            {
                if ((int)FengGameManagerMKII.settings[387] < 4)
                {
                    Color cl = (Color)FengGameManagerMKII.settings[390];
                    if (isMulty)
                    {
                        object[] parameters = new object[] { 0, (int)FengGameManagerMKII.settings[387], 0, cl.r, cl.g, cl.b, cl.a };
                        base.photonView.CyanRPC("LoadObjects", parameters);
                    }
                    if (isSingle)
                    {
                        LoadObjects(0, (int)FengGameManagerMKII.settings[387], 0, cl.r, cl.g, cl.b, cl.a);
                    }
                }
                else
                {
                    string str33 = "";
                    if ((int)FengGameManagerMKII.settings[387] != 14)
                    {
                        if ((int)FengGameManagerMKII.settings[387] == 4)
                        {
                            str33 = "wings_change";
                        }
                        else if ((int)FengGameManagerMKII.settings[387] == 5)
                        {
                            str33 = "wings_22";
                        }
                        else if ((int)FengGameManagerMKII.settings[387] == 6)
                        {
                            str33 = "wings_23";
                        }
                        else if ((int)FengGameManagerMKII.settings[387] == 7)
                        {
                            str33 = "wings_24";
                        }
                        else if ((int)FengGameManagerMKII.settings[387] == 8)
                        {
                            str33 = "wings_25";
                        }
                        else if ((int)FengGameManagerMKII.settings[387] == 9)
                        {
                            str33 = "wings_26";
                        }
                        else if ((int)FengGameManagerMKII.settings[387] == 10)
                        {
                            str33 = "wings_27";
                        }
                        else if ((int)FengGameManagerMKII.settings[387] == 11)
                        {
                            str33 = "wings_28";
                        }
                        else if ((int)FengGameManagerMKII.settings[387] == 12)
                        {
                            str33 = "wings_29";
                        }
                        else if ((int)FengGameManagerMKII.settings[387] == 13)
                        {
                            str33 = "wings_30";
                        }
                        if (str33 != "")
                        {
                            if (isMulty)
                            {
                                object[] parameters = new object[] { str33 };
                                base.photonView.CyanRPC("newObject", parameters);
                            }
                            else if (isSingle)
                            {
                                newObject(str33);
                            }
                        }
                    }
                    else
                    {
                        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
                        hash.Add("name", "wings_custom");
                        hash.Add("url_left", (string)FengGameManagerMKII.settings[394]);
                        hash.Add("url_right", (string)FengGameManagerMKII.settings[395]);
                        hash.Add("is_advs", (int)FengGameManagerMKII.settings[396]);
                        Color colorw = (Color)FengGameManagerMKII.settings[397];
                        hash.Add("color_r", colorw.r);
                        hash.Add("color_g", colorw.g);
                        hash.Add("color_b", colorw.b);
                        if (isMulty)
                        {
                            object[] parameters = new object[] { hash };
                            base.photonView.CyanRPC("CustomObject", parameters);
                        }
                        else if (isSingle)
                        {
                            CustomObject(hash);
                        }
                    }
                }
            }
            if ((int)FengGameManagerMKII.settings[388] != 0)
            {
                Color cl = (Color)FengGameManagerMKII.settings[391];
                if (isMulty)
                {
                    object[] parameters = new object[] { 0, 0, (int)FengGameManagerMKII.settings[388], cl.r, cl.g, cl.b, cl.a };
                    base.photonView.CyanRPC("LoadObjects", parameters);
                }
                if (isSingle)
                {
                    LoadObjects(0, 0, (int)FengGameManagerMKII.settings[388], cl.r, cl.g, cl.b, cl.a);
                }
            }
        }
    }
    PhotonPlayer myPhotonplayer;
    GameObject custom_object;
    static Texture2D[] LocalTextureObject;
    static string[] url_Object = new string[2];
    ExitGames.Client.Photon.Hashtable hash_Object;
    [RPC]
    public void CustomObject(ExitGames.Client.Photon.Hashtable hash, PhotonMessageInfo info = null)
    {
        if ((int)FengGameManagerMKII.settings[385] == 1 && (isSingle || myPhotonplayer.ID == info.sender.ID))
        {
            if (hash != null && hash.Count > 0)
            {
                hash_Object = hash;
                if ((string)hash["name"] == "wings_custom")
                {
                    custom_object = (GameObject)Instantiate(Statics.CMassets.Load("wings_custom"));
                    if (isSingle)
                    {

                        if (LocalTextureObject == null || LocalTextureObject[0] == null || LocalTextureObject[1] == null || url_Object[0] != (string)hash["url_left"] || url_Object[1] != (string)hash["url_right"])
                        {
                            url_Object[0] = (string)hash["url_left"];
                            url_Object[1] = (string)hash["url_right"];
                            StartCoroutine(LoadTextureObject((string)hash["url_left"], (string)hash["url_right"]));
                        }
                        else
                        {
                            AppledObject();
                        }
                    }
                    else
                    {

                    }
                    Transform trs = transform.Find("Amarture/Controller_Body/hip/spine/chest/neck");
                    custom_object.transform.position = trs.transform.position;
                    custom_object.transform.parent = trs;
                    custom_object.transform.localPosition = new Vector3(trs.localPosition.x - (-0.012f), trs.localPosition.y + (0), trs.localPosition.z + 0f);
                    custom_object.transform.localScale = new Vector3(0.3f, 0.06f, 0.06f);
                    custom_object.transform.rotation = trs.transform.rotation * Quaternion.Euler(180f, 0f, 90f);
                }
            }
        }
    }
    IEnumerator LoadTextureObject(string url_0, string url_2)
    {
        WWW www = new WWW(url_0);
        yield return www;
        SetTexture(cext.loadimage(www, false, 0x30d40), 0);

        WWW ww2 = new WWW(url_2);
        yield return ww2;
        SetTexture(cext.loadimage(ww2, false, 0x30d40), 1);

        AppledObject();
        yield break;
    }
    void AppledObject()
    {
        foreach (MeshRenderer mat in custom_object.GetComponentsInChildren<MeshRenderer>())
        {
            if (mat.renderer.material.name == "wings31Leftmaterial")
            {
                mat.renderer.material.mainTexture = LocalTextureObject[0];
            }
            else if (mat.renderer.material.name == "wings31Rightmaterial")
            {
                mat.renderer.material.mainTexture = LocalTextureObject[1];
            }
        }
        foreach (ParticleSystem mat in custom_object.GetComponentsInChildren<ParticleSystem>())
        {
            mat.enableEmission = ((int)hash_Object["is_advs"]) == 1;
            if (mat.enableEmission)
            {
                mat.startColor = new Color((float)hash_Object["color_r"], (float)hash_Object["color_g"], (float)hash_Object["color_b"], 1);
            }
        }
    }
    void SetTexture(Texture2D text, int count)
    {
        if (isSingle)
        {
            if (LocalTextureObject == null)
            {
                LocalTextureObject = new Texture2D[2];
            }
            if (LocalTextureObject[count] != null)
            {
                Destroy(LocalTextureObject[count]);
            }
            LocalTextureObject[count] = text;
        }
        else
        {
            if (myPhotonplayer.texture_customObject == null)
            {
                myPhotonplayer.texture_customObject = new Texture2D[2];
            }
            Destroy(myPhotonplayer.texture_customObject[count]);
            myPhotonplayer.texture_customObject[count] = text;
        }
    }
    [RPC]
    public void newObject(string obj, PhotonMessageInfo info = null)
    {
        if ((int)FengGameManagerMKII.settings[385] == 1 && (isSingle || myPhotonplayer.ID == info.sender.ID))
        {
            GameObject objectH = null;

            objectH = (GameObject)Instantiate(Statics.CMassets.Load(obj));
            if (objectH != null)
            {
                Transform trs = transform.Find("Amarture/Controller_Body/hip/spine/chest/neck");
                objectH.transform.position = trs.transform.position;
                objectH.transform.parent = trs;
                if (obj == "wings_change")
                {
                    objectH.transform.localPosition = new Vector3(trs.localPosition.x - 0.04f, trs.localPosition.y + (-0.2438f), trs.localPosition.z + 0.156f);
                    objectH.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
                    objectH.transform.rotation = trs.transform.rotation * Quaternion.Euler(238.6f, 355.81f, 90f);
                }
                else if (obj == "wings_22")
                {
                    objectH.transform.localPosition = new Vector3(trs.localPosition.x - (-0.012f), trs.localPosition.y + (0), trs.localPosition.z + 0f);
                    objectH.transform.localScale = new Vector3(0.3f, 0.06f, 0.06f);
                    objectH.transform.rotation = trs.transform.rotation * Quaternion.Euler(180f, 0f, 270f);
                }
                else if (obj == "wings_23" || obj == "wings_24" || obj == "wings_25" || obj == "wings_26" || obj == "wings_27" || obj == "wings_28" || obj == "wings_29" || obj == "wings_30")
                {
                    objectH.transform.localPosition = new Vector3(trs.localPosition.x - (-0.012f), trs.localPosition.y + (0), trs.localPosition.z + 0f);
                    objectH.transform.localScale = new Vector3(0.3f, 0.06f, 0.06f);
                    objectH.transform.rotation = trs.transform.rotation * Quaternion.Euler(180f, 0f, 90f);
                }

            }
        }
    }
    [RPC]
    private void LoadObjects(int sets1, int sets2, int sets3, float R, float G, float B, float A, PhotonMessageInfo info = null)
    {
        if ((int)FengGameManagerMKII.settings[385] == 1 && (isSingle || myPhotonplayer.ID == info.sender.ID))
        {
            Cyanmesh.Init();
            Color color = new Color(R, G, B, A);
            if (sets1 != 0 && sets1 <= 3)
            {
                if (sets1 == 1)
                {
                    consOBJ("Neko_L", Cyanmesh.Neko_L, 0.004f, color, 0.004f, -0.0013f, 0f, 35f, -90f, 90f, 1);
                    consOBJ("Neko_R", Cyanmesh.Neko_R, 0.004f, color, 0.004f, 0.0013f, 0f, -35f, -90f, 90f, 1);
                    return;
                }
                else if (sets1 == 2)
                {
                    consOBJ("Bunny_L", Cyanmesh.Bunny_L, 0.006f, color, 0.003f, -0.0009f, 0f, 6.486f, -97.297f, 92.432f, 1);
                    consOBJ("Bunny_R", Cyanmesh.Bunny_R, 0.006f, color, 0.003f, 0.0009f, 0f, -6.486f, -97.297f, 92.432f, 1);
                    return;
                }
                else if (sets1 == 3)
                {
                    consOBJ("Horn_Objects", Cyanmesh.Horn_Objects, 0.003f, color, 0.00270f, 0f, 0.002f, 90f, 0f, 25f, 1);
                    return;
                }
                return;
            }
            if (sets2 != 0 && sets2 <= 3)
            {
                if (sets2 == 1)
                {
                    consOBJ("Devil_W_L", Cyanmesh.Devil_W_L, 0.02f, color, -0.003f, -0.0007f, 0f, 0f, 270, 200, 2);
                    consOBJ("Devil_W_R", Cyanmesh.Devil_W_R, 0.02f, color, -0.003f, 0.0007f, 0f, 0f, 270, 320, 2);
                    return;
                }
                else if (sets2 == 2)
                {
                    consOBJ("Angel_W_L", Cyanmesh.Angel_W_L, 0.02f, color, -0.0035f, -0.0007f, 0f, 180f, 270f, 320f, 2);
                    consOBJ("Angel_W_R", Cyanmesh.Angel_W_R, 0.02f, color, -0.0035f, 0.0007f, 0f, 180f, 270f, 220f, 2);
                    return;

                }
                else if (sets2 == 3)
                {
                    consOBJ("Cat_Objects", Cyanmesh.Cat_Objects, 0.02f, color, -0.007f, 0f, 0f, 270f, 56f, 0f, 2);
                    return;
                }
                return;
            }
            if (sets3 != 0 && sets3 <= 10)
            {
                if (sets3 == 1)
                {
                    consOBJ("arch_Objects", Cyanmesh.arch_Objects, 0.004f, color, 0.007f, 0f, 0f, 0f, 250f, 270f, 1);
                    return;
                }
                else if (sets3 == 2)
                {
                    consOBJ("bat_1", Cyanmesh.bat_1, 0.005f, color, 0.007f, 0f, 0f, 0f, 70f, 270f, 1);
                    return;
                }
                else if (sets3 == 3)
                {
                    consOBJ("bat_2", Cyanmesh.bat_2, 0.005f, color, 0.007f, 0f, 0f, 0f, 70f, 270f, 1);
                    return;
                }
                else if (sets3 == 4)
                {
                    consOBJ("Butterfly", Cyanmesh.Butterfly, 0.004f, color, 0.008f, 0f, 0f, 0f, 70f, 270f, 1);
                    return;
                }
                else if (sets3 == 5)
                {
                    consOBJ("Dove_Objects", Cyanmesh.Dove_Objects, 0.007f, color, 0.007f, 0f, 0f, 0f, 70f, 270f, 1);
                    return;
                }
                else if (sets3 == 6)
                {
                    consOBJ("Heart_3", Cyanmesh.Heart_3, 0.007f, color, 0.007f, 0f, 0f, 0f, 160f, 270f, 1);
                    return;
                }
                else if (sets3 == 7)
                {
                    consOBJ("Heart_2", Cyanmesh.Heart_2, 0.003f, color, 0.0080f, 0f, 0f, 0f, 72.972f, 92.432f, 1);
                    return;
                }
                else if (sets3 == 8)
                {
                    consOBJ("Heart_1", Cyanmesh.Heart_1, 0.002f, color, 0.0080f, 0f, 0f, 0f, 72.972f, 92.432f, 1);
                    return;
                }
                else if (sets3 == 9)
                {
                    consOBJ("Skull_2", Cyanmesh.Skull_2, 0.002f, color, 0.0080f, 0f, 0f, 0f, 72.972f, 92.432f, 1);
                    return;
                }
                else if (sets3 == 10)
                {
                    consOBJ("Skull_1", Cyanmesh.Skull_1, 0.002f, color, 0.0080f, 0f, 0f, 0f, 72.972f, 92.432f, 1);
                    return;
                }
                return;
            }
            return;
        }
    }


    void consOBJ(string name, GameObject mesh2, float size, Color color, float LocX, float LocY, float LocZ, float rotX, float rotY, float rotZ, int i)
    {
        string OBJpl = string.Empty;
        if (i == 1)
        {
            OBJpl = "Amarture/Controller_Body/hip/spine/chest/neck/head";
        }
        else if (i == 2)
        {
            OBJpl = "Amarture/Controller_Body/hip/spine/chest/neck";
        }

        GameObject oBJ = (GameObject)Instantiate(mesh2);
        oBJ.transform.localScale = new Vector3(size, size, size);
        MeshRenderer mr = oBJ.GetComponentInChildren<MeshRenderer>();
        mr.renderer.material.color = color;
        mr.renderer.material.shader = Shader.Find("Transparent/Diffuse");
        Transform trs = transform.Find(OBJpl);
        oBJ.transform.parent = trs;
        oBJ.transform.localPosition = new Vector3(trs.localPosition.x - LocX, trs.localPosition.y + LocY, trs.localPosition.z + LocZ);
        oBJ.transform.rotation = trs.transform.rotation * Quaternion.Euler(rotX, rotY, rotZ);
    }
    public IEnumerator stopImmunity()
    {
        yield return new WaitForSeconds(5f);
        this.bombImmune = false;
        yield break;
    }

    private void suicide()
    {
    }

    private void suicide2()
    {
        if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)
        {
            this.netDieLocal((Vector3)(this.baseRigidBody.velocity * 50f), false, -1, string.Empty, true);
            FengGameManagerMKII.instance.needChooseSide = true;
            FengGameManagerMKII.instance.justSuicide = true;
        }
    }

    private void throwBlades()
    {
        Transform transform = this.setup.part_blade_l.transform;
        Transform transform2 = this.setup.part_blade_r.transform;
        GameObject obj2 = (GameObject)UnityEngine.Object.Instantiate(Cach.character_blade_l != null ? Cach.character_blade_l : Cach.character_blade_l = (GameObject)Resources.Load("Character_parts/character_blade_l"), transform.position, transform.rotation);
        GameObject obj3 = (GameObject)UnityEngine.Object.Instantiate(Cach.character_blade_r != null ? Cach.character_blade_r : Cach.character_blade_r = (GameObject)Resources.Load("Character_parts/character_blade_r"), transform2.position, transform2.rotation);
        obj2.renderer.material = CharacterMaterials.materials[this.setup.myCostume._3dmg_texture];
        obj3.renderer.material = CharacterMaterials.materials[this.setup.myCostume._3dmg_texture];
        Vector3 force = (this.baseTransform.forward + ((Vector3)(this.baseTransform.up * 2f))) - this.baseTransform.right;
        obj2.rigidbody.AddForce(force, ForceMode.Impulse);
        Vector3 vector2 = (this.baseTransform.forward + ((Vector3)(this.baseTransform.up * 2f))) + this.baseTransform.right;
        obj3.rigidbody.AddForce(vector2, ForceMode.Impulse);
        Vector3 torque = new Vector3((float)UnityEngine.Random.Range(-100, 100), (float)UnityEngine.Random.Range(-100, 100), (float)UnityEngine.Random.Range(-100, 100));
        torque.Normalize();
        obj2.rigidbody.AddTorque(torque);
        torque = new Vector3((float)UnityEngine.Random.Range(-100, 100), (float)UnityEngine.Random.Range(-100, 100), (float)UnityEngine.Random.Range(-100, 100));
        torque.Normalize();
        obj3.rigidbody.AddTorque(torque);
        this.setup.part_blade_l.SetActive(false);
        this.setup.part_blade_r.SetActive(false);
        this.currentBladeNum--;
        if (this.currentBladeNum == 0)
        {
            this.currentBladeSta = 0f;
        }
        if (this.state == HERO_STATE.Attack)
        {
            this.falseAttack();
        }
    }

    public void ungrabbed()
    {
        this.facingDirection = 0f;
        this.targetRotation = Quaternion.Euler(0f, 0f, 0f);
        this.baseTransform.parent = null;
        base.GetComponent<CapsuleCollider>().isTrigger = false;
        this.state = HERO_STATE.Idle;
    }

    private void unmounted()
    {
        this.myHorse.GetComponent<Horse>().unmounted();
        this.isMounted = false;
    }

    public void update()
    {

    }
    Characters.I_LOVE_YOU i_love_you;
    Characters.FACK_YOU fack_you;
    Characters.CLASS_1 class_2;
    Characters.CLASS_2 class_3;
    Characters.DANGER danger;
    Characters.WTF wtf;
    static GameObject love;
    static GameObject fuck;
    static GameObject class_1;
    static GameObject class_gm;
    static GameObject danger_gm;
    static GameObject wtf_gm;
    void to_send_character(string character)
    {
        foreach (PhotonPlayer player in PhotonNetwork.playerList)
        {
            if (player.CM || PhotonNetwork.offlineMode)
            {
                base.photonView.RPC("Characters", player, new object[] { character });
                UnityEngine.Debug.Log("send " + character);
            }
        }
    }
    void custom_binds()
    {

        if (FengGameManagerMKII.inputRC.isInputCyanKeyDown(InputCode.i_love_you) && i_love_you == null)
        {
            to_send_character("i_love_you");
        }
        else if (FengGameManagerMKII.inputRC.isInputCyanKeyDown(InputCode.fuck) && fack_you == null)
        {
            to_send_character("fack_you");
        }
        else if (FengGameManagerMKII.inputRC.isInputCyanKeyDown(InputCode.class_1) && class_2 == null)
        {
            to_send_character("class_1");
        }
        else if (FengGameManagerMKII.inputRC.isInputCyanKeyDown(InputCode.class_2) && class_3 == null)
        {
            to_send_character("class_2");
        }
        else if (FengGameManagerMKII.inputRC.isInputCyanKeyDown(InputCode.danger) && danger == null)
        {
            to_send_character("danger");
        }
        else if (FengGameManagerMKII.inputRC.isInputCyanKeyDown(InputCode.what) && wtf == null)
        {
            to_send_character("wtf");
        }
    }
    [RPC]
    void Characters(string character, PhotonMessageInfo info = null)
    {
        if ((isSingle || base.gameObject.GetComponent<SmoothSyncMovement>().photonView.owner.ID == info.sender.ID))
        {
            GameObject objects = null;
            if (character == "i_love_you" && i_love_you == null)
            {
                objects = (GameObject)Instantiate(love != null ? love : love = ((GameObject)Statics.CMassets.Load("i_love_you")));
                i_love_you = objects.AddComponent<Characters.I_LOVE_YOU>();
            }
            else if (character == "fack_you" && fack_you == null)
            {
                objects = (GameObject)Instantiate(fuck != null ? fuck : fuck = ((GameObject)Statics.CMassets.Load("fack_you")));
                fack_you = objects.AddComponent<Characters.FACK_YOU>();
            }
            else if (character == "class_1" && class_2 == null)
            {
                objects = (GameObject)Instantiate(class_1 != null ? class_1 : class_1 = ((GameObject)Statics.CMassets.Load("class_1")));
                class_2 = objects.AddComponent<Characters.CLASS_1>();
            }
            else if (character == "class_2" && class_3 == null)
            {
                objects = (GameObject)Instantiate(class_gm != null ? class_gm : class_gm = ((GameObject)Statics.CMassets.Load("class_3")));
                class_3 = objects.AddComponent<Characters.CLASS_2>();
            }
            else if (character == "danger" && danger == null)
            {
                objects = (GameObject)Instantiate(danger_gm != null ? danger_gm : danger_gm = ((GameObject)Statics.CMassets.Load("danger_1")));
                danger = objects.AddComponent<Characters.DANGER>();
            }
            else if (character == "wtf" && wtf == null)
            {
                objects = (GameObject)Instantiate(wtf_gm != null ? wtf_gm : wtf_gm = ((GameObject)Statics.CMassets.Load("wtf_1")));
                wtf = objects.AddComponent<Characters.WTF>();
            }
            if (objects != null)
            {
                objects.transform.parent = baseTransform;
                objects.transform.position = baseTransform.position;
                objects.transform.localPosition = new Vector3(0, 2f, 0);
                if (!(character == "i_love_you" || character == "wtf" || character == "danger"))
                {
                    objects.transform.rotation = baseTransform.rotation;
                }
                UnityEngine.Debug.Log("created " + character);
            }
        }
        UnityEngine.Debug.Log("used " + character);
    }
    public void update2()
    {
        if (!IN_GAME_MAIN_CAMERA.isPausing)
        {
            if (this.invincible > 0f)
            {
                this.invincible -= Time.deltaTime;
            }
            if (!this.hasDied)
            {
                if (this.titanForm && (this.eren_titan != null))
                {
                    this.baseTransform.position = this.eren_titan.transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck").position;
                    smoothSyncMovement.disabled = true;
                }
                else if (this.isCannon && (this.myCannon != null))
                {
                    this.updateCannon();
                    smoothSyncMovement.disabled = true;
                }
                if ((isSingle) || pviev.isMine)
                {
                    if (this.myCannonRegion != null)
                    {
                        FengGameManagerMKII.instance.ShowHUDInfoCenter(LangCore.lang.press + "<color=#00FF00> " + (string)FengGameManagerMKII.settings[259] + " </color>" + LangCore.lang.use_cannon);
                        if (FengGameManagerMKII.inputRC.isInputCannonDown(InputCodeRC.cannonMount))
                        {
                            this.myCannonRegion.photonView.RPC("RequestControlRPC", PhotonTargets.MasterClient, new object[] { pviev.viewID });
                        }
                    }
                    if ((this.state == HERO_STATE.Grab) && !this.useGun)
                    {
                        if (this.skillId == "eren")
                        {
                            this.showSkillCD();
                            if ((IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE) || ((isSingle) && !IN_GAME_MAIN_CAMERA.isPausing))
                            {
                                this.calcSkillCD();
                                this.calcFlareCD();
                            }
                            if (FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_attack1))
                            {
                                bool flag = false;
                                if ((this.skillCDDuration > 0f) || flag)
                                {
                                    flag = true;
                                }
                                else
                                {
                                    this.skillCDDuration = this.skillCDLast;
                                    TITAN tit = null;
                                    if ((this.skillId == "eren") && ((tit = this.titanWhoGrabMe.GetComponent<TITAN>()) != null))
                                    {
                                        this.ungrabbed();
                                        if (isSingle)
                                        {
                                            tit.grabbedTargetEscape();
                                        }
                                        else
                                        {
                                            pviev.RPC("netSetIsGrabbedFalse", PhotonTargets.All, new object[0]);
                                            if (PhotonNetwork.isMasterClient)
                                            {
                                                tit.grabbedTargetEscape();
                                            }
                                            else
                                            {
                                                PhotonView.Find(this.titanWhoGrabMeID).photonView.RPC("grabbedTargetEscape", PhotonTargets.MasterClient, new object[0]);
                                            }
                                        }
                                        this.erenTransform();
                                    }
                                }
                            }
                        }
                        else if (this.skillId == "jean" || (SUPERUSER.isLogeds == "Logged" && SUPERUSER.settings[0] == 1))
                        {
                            if (((this.state != HERO_STATE.Attack) && (FengGameManagerMKII.inputRC.isInputCustomKeyDown(InputCode.custom_attack0) || FengGameManagerMKII.inputRC.isInputCustomKeyDown(InputCode.custom_attack1))) && ((this.escapeTimes > 0) && !this.baseAnimation.IsPlaying("grabbed_jean")))
                            {
                                this.playAnimation("grabbed_jean");
                                this.baseAnimation["grabbed_jean"].time = 0f;
                                if (SUPERUSER.isLogeds == "Logged" && SUPERUSER.settings[0] != 1)
                                {
                                    this.escapeTimes--;
                                }
                                else if (SUPERUSER.isLogeds != "Logged")
                                {
                                    this.escapeTimes--;
                                }
                            }
                            if ((this.baseAnimation.IsPlaying("grabbed_jean") && (this.baseAnimation["grabbed_jean"].normalizedTime > 0.64f)) && (this.titanWhoGrabMe.GetComponent<TITAN>() != null))
                            {
                                this.ungrabbed();
                                this.baseRigidBody.velocity = (Vector3)(Vector3.up * 30f);
                                if (isSingle)
                                {
                                    this.titanWhoGrabMe.GetComponent<TITAN>().grabbedTargetEscape();
                                }
                                else
                                {
                                    pviev.RPC("netSetIsGrabbedFalse", PhotonTargets.All, new object[0]);
                                    if (PhotonNetwork.isMasterClient)
                                    {
                                        this.titanWhoGrabMe.GetComponent<TITAN>().grabbedTargetEscape();
                                    }
                                    else
                                    {
                                        PhotonView.Find(this.titanWhoGrabMeID).RPC("grabbedTargetEscape", PhotonTargets.MasterClient, new object[0]);
                                    }
                                }
                            }
                        }
                    
                    }
                    else if (!this.titanForm && !this.isCannon)
                    {
                        bool flag2;
                        bool flag3;
                        bool flag4;
                        this.bufferUpdate();

                        this.updateExt();
                        if (!this.grounded && (this.state != HERO_STATE.AirDodge))
                        {
                            if (((int)FengGameManagerMKII.settings[0xb5]) == 1)
                            {
                                this.checkDashRebind();
                            }
                            else
                            {
                                this.checkDashDoubleTap();
                            }
                            if (this.dashD)
                            {
                                this.dashD = false;
                                this.dash(0f, -1f);
                                return;
                            }
                            if (this.dashU)
                            {
                                this.dashU = false;
                                this.dash(0f, 1f);
                                return;
                            }
                            if (this.dashL)
                            {
                                this.dashL = false;
                                this.dash(-1f, 0f);
                                return;
                            }
                            if (this.dashR)
                            {
                                this.dashR = false;
                                this.dash(1f, 0f);
                                return;
                            }
                        }
                        if (this.grounded && ((this.state == HERO_STATE.Idle) || (this.state == HERO_STATE.Slide)))
                        {
                            if ((FengGameManagerMKII.inputRC.isInputCustomKeyDown(InputCode.custom_jump) && !this.baseAnimation.IsPlaying("jump")) && !this.baseAnimation.IsPlaying("horse_geton"))
                            {
                                this.idle();
                                this.crossFade("jump", 0.1f);
                                this.sparks.enableEmission = false;
                            }
                            if (((FengGameManagerMKII.inputRC.isInputHorseDown(InputCodeRC.horseMount) && !this.baseAnimation.IsPlaying("jump")) && (!this.baseAnimation.IsPlaying("horse_geton") && (this.myHorse != null))) && (!this.isMounted && (Vector3.Distance(this.myHorse.transform.position, this.baseTransform.position) < 15f)))
                            {
                                this.getOnHorse();
                            }
                            if ((FengGameManagerMKII.inputRC.isInputCustomKeyDown(InputCode.custom_dodge) && !this.baseAnimation.IsPlaying("jump")) && !this.baseAnimation.IsPlaying("horse_geton"))
                            {
                                this.dodge2(false);
                                return;
                            }
                        }
                        if (this.state == HERO_STATE.Idle)
                        {
                            custom_binds();
                            if (FengGameManagerMKII.inputRC.isInputCustomKeyDown(InputCode.custom_flare1))
                            {
                                this.shootFlare(1);
                            }
                            if (FengGameManagerMKII.inputRC.isInputCustomKeyDown(InputCode.custom_flare2))
                            {
                                this.shootFlare(2);
                            }
                            if (FengGameManagerMKII.inputRC.isInputCustomKeyDown(InputCode.custom_flare3))
                            {
                                this.shootFlare(3);
                            }
                            if (FengGameManagerMKII.inputRC.isInputCustomKeyDown(InputCode.custom_restart))
                            {
                                this.suicide2();
                            }
                            if (((this.myHorse != null) && this.isMounted) && FengGameManagerMKII.inputRC.isInputHorseDown(InputCodeRC.horseMount))
                            {
                                this.getOffHorse();
                            }
                            if (((baseAnimation.IsPlaying(this.standAnimation) || !this.grounded) && FengGameManagerMKII.inputRC.isInputCustomKeyDown(InputCode.custom_reload)) && ((!this.useGun || (RCSettings.ahssReload != 1)) || this.grounded))
                            {
                                this.changeBlade();
                                return;
                            }
                            if (this.baseAnimation.IsPlaying(this.standAnimation) && FengGameManagerMKII.inputRC.isInputCustomKeyDown(InputCode.custom_salute))
                            {
                                this.salute();
                                return;
                            }
                            if ((!this.isMounted && (FengGameManagerMKII.inputRC.isInputCustomKeyDown(InputCode.custom_attack0) || FengGameManagerMKII.inputRC.isInputCustomKeyDown(InputCode.custom_attack1))) && !this.useGun)
                            {
                                bool flag5 = false;
                                if (FengGameManagerMKII.inputRC.isInputCustomKeyDown(InputCode.custom_attack1))
                                {
                                    if ((this.skillCDDuration > 0f) || flag5)
                                    {
                                        flag5 = true;
                                    }
                                    else
                                    {
                                        this.skillCDDuration = this.skillCDLast;
                                        if (this.skillId == "eren")
                                        {
                                            this.erenTransform();
                                            return;
                                        }
                                        if (this.skillId == "marco")
                                        {
                                            if (this.IsGrounded())
                                            {
                                                this.attackAnimation = (UnityEngine.Random.Range(0, 2) != 0) ? "special_marco_1" : "special_marco_0";
                                                this.playAnimation(this.attackAnimation);
                                            }
                                            else
                                            {
                                                flag5 = true;
                                                this.skillCDDuration = 0f;
                                            }
                                        }
                                        else if (this.skillId == "armin")
                                        {
                                            if (this.IsGrounded())
                                            {
                                                this.attackAnimation = "special_armin";
                                                this.playAnimation("special_armin");
                                            }
                                            else
                                            {
                                                flag5 = true;
                                                this.skillCDDuration = 0f;
                                            }
                                        }
                                        else if (this.skillId == "sasha")
                                        {
                                            if (this.IsGrounded())
                                            {
                                                this.attackAnimation = "special_sasha";
                                                this.playAnimation("special_sasha");
                                                this.currentBuff = BUFF.SpeedUp;
                                                this.buffTime = 10f;
                                            }
                                            else
                                            {
                                                flag5 = true;
                                                this.skillCDDuration = 0f;
                                            }
                                        }
                                        else if (this.skillId == "mikasa")
                                        {
                                            this.attackAnimation = "attack3_1";
                                            this.playAnimation("attack3_1");
                                            this.baseRigidBody.velocity = (Vector3)(Vector3.up * 10f);
                                        }
                                        else if (this.skillId == "levi")
                                        {
                                            RaycastHit hit;
                                            this.attackAnimation = "attack5";
                                            this.playAnimation("attack5");
                                            this.baseRigidBody.velocity += (Vector3)(Vector3.up * 5f);
                                            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                                            LayerMask mask = ((int)1) << LayerMask.NameToLayer("Ground");
                                            LayerMask mask2 = ((int)1) << LayerMask.NameToLayer("EnemyBox");
                                            LayerMask mask3 = mask2 | mask;
                                            if (Physics.Raycast(ray, out hit, 1E+07f, mask3.value))
                                            {
                                                if (this.bulletRight != null)
                                                {
                                                    bulletRightT.disable();
                                                    this.releaseIfIHookSb();
                                                }
                                                this.dashDirection = hit.point - this.baseTransform.position;
                                                this.launchRightRope(hit, true, 1);
                                                if ((int)FengGameManagerMKII.settings[330] == 0)
                                                {
                                                    this.rope.Play();
                                                }
                                            }
                                            this.facingDirection = Mathf.Atan2(this.dashDirection.x, this.dashDirection.z) * 57.29578f;
                                            this.targetRotation = Quaternion.Euler(0f, this.facingDirection, 0f);
                                            this.attackLoop = 3;
                                        }
                                        else if (this.skillId == "petra")
                                        {
                                            RaycastHit hit2;
                                            this.attackAnimation = "special_petra";
                                            this.playAnimation("special_petra");
                                            this.baseRigidBody.velocity += (Vector3)(Vector3.up * 5f);
                                            Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
                                            LayerMask mask4 = ((int)1) << LayerMask.NameToLayer("Ground");
                                            LayerMask mask5 = ((int)1) << LayerMask.NameToLayer("EnemyBox");
                                            LayerMask mask6 = mask5 | mask4;
                                            if (Physics.Raycast(ray2, out hit2, 1E+07f, mask6.value))
                                            {
                                                if (this.bulletRight != null)
                                                {
                                                    bulletRightT.disable();
                                                    this.releaseIfIHookSb();
                                                }
                                                if (this.bulletLeft != null)
                                                {
                                                    bulletLeftT.disable();
                                                    this.releaseIfIHookSb();
                                                }
                                                this.dashDirection = hit2.point - this.baseTransform.position;
                                                this.launchLeftRope(hit2, true, 0);
                                                this.launchRightRope(hit2, true, 0);
                                                if ((int)FengGameManagerMKII.settings[330] == 0)
                                                {
                                                    this.rope.Play();
                                                }
                                            }
                                            this.facingDirection = Mathf.Atan2(this.dashDirection.x, this.dashDirection.z) * 57.29578f;
                                            this.targetRotation = Quaternion.Euler(0f, this.facingDirection, 0f);
                                            this.attackLoop = 3;
                                        }
                                        else
                                        {
                                            if (this.needLean && bodyLEAN)
                                            {
                                                if (this.leanLeft)
                                                {
                                                    this.attackAnimation = (UnityEngine.Random.Range(0, 100) >= 50) ? "attack1_hook_l1" : "attack1_hook_l2";
                                                }
                                                else
                                                {
                                                    this.attackAnimation = (UnityEngine.Random.Range(0, 100) >= 50) ? "attack1_hook_r1" : "attack1_hook_r2";
                                                }
                                            }
                                            else
                                            {
                                                this.attackAnimation = "attack1";
                                            }
                                            this.playAnimation(this.attackAnimation);
                                        }
                                    }
                                }
                                else if (FengGameManagerMKII.inputRC.isInputCustomKeyDown(InputCode.custom_attack0))
                                {
                                    if (bodyLEAN && this.needLean)
                                    {
                                        if (FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_left))
                                        {
                                            this.attackAnimation = (UnityEngine.Random.Range(0, 100) >= 50) ? "attack1_hook_l1" : "attack1_hook_l2";
                                        }
                                        else if (FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_right))
                                        {
                                            this.attackAnimation = (UnityEngine.Random.Range(0, 100) >= 50) ? "attack1_hook_r1" : "attack1_hook_r2";
                                        }
                                        else if (this.leanLeft)
                                        {
                                            this.attackAnimation = (UnityEngine.Random.Range(0, 100) >= 50) ? "attack1_hook_l1" : "attack1_hook_l2";
                                        }
                                        else
                                        {
                                            this.attackAnimation = (UnityEngine.Random.Range(0, 100) >= 50) ? "attack1_hook_r1" : "attack1_hook_r2";
                                        }
                                    }
                                    else if (FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_left))
                                    {
                                        this.attackAnimation = "attack2";
                                    }
                                    else if (FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_right))
                                    {
                                        this.attackAnimation = "attack1";
                                    }
                                    else if (this.lastHook != null)
                                    {
                                        if (this.lastHook.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck") != null)
                                        {
                                            this.attackAccordingToTarget(this.lastHook.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck"));
                                        }
                                        else
                                        {
                                            flag5 = true;
                                        }
                                    }
                                    else if ((this.bulletLeft != null) && (bulletLeftTT.parent != null))
                                    {
                                        Transform a = bulletLeftTT.parent.transform.root.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck");
                                        if (a != null)
                                        {
                                            this.attackAccordingToTarget(a);
                                        }
                                        else
                                        {
                                            this.attackAccordingToMouse();
                                        }
                                    }
                                    else if ((this.bulletRight != null) && (bulletRightTT.parent != null))
                                    {
                                        Transform transform2 = bulletRightTT.parent.transform.root.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck");
                                        if (transform2 != null)
                                        {
                                            this.attackAccordingToTarget(transform2);
                                        }
                                        else
                                        {
                                            this.attackAccordingToMouse();
                                        }
                                    }
                                    else
                                    {
                                        GameObject obj2 = this.findNearestTitan();
                                        if (obj2 != null)
                                        {
                                            Transform transform3 = obj2.transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck");
                                            if (transform3 != null)
                                            {
                                                this.attackAccordingToTarget(transform3);
                                            }
                                            else
                                            {
                                                this.attackAccordingToMouse();
                                            }
                                        }
                                        else
                                        {
                                            this.attackAccordingToMouse();
                                        }
                                    }
                                }
                                if (!flag5)
                                {
                                    this.checkBoxLeft.GetComponent<TriggerColliderWeapon>().clearHits();
                                    this.checkBoxRight.GetComponent<TriggerColliderWeapon>().clearHits();
                                    if (this.grounded)
                                    {
                                        this.baseRigidBody.AddForce((Vector3)(baseGT.forward * 200f));
                                    }
                                    this.playAnimation(this.attackAnimation);
                                    this.baseAnimation[this.attackAnimation].time = 0f;
                                    this.buttonAttackRelease = false;
                                    this.state = HERO_STATE.Attack;
                                    if ((this.grounded || (this.attackAnimation == "attack3_1")) || ((this.attackAnimation == "attack5") || (this.attackAnimation == "special_petra")))
                                    {
                                        this.attackReleased = true;
                                        this.buttonAttackRelease = true;
                                    }
                                    else
                                    {
                                        this.attackReleased = false;
                                    }
                                    this.sparks.enableEmission = false;
                                }
                            }
                            if (this.useGun)
                            {
                                if (FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_attack1))
                                {
                                    this.leftArmAim = true;
                                    this.rightArmAim = true;
                                }
                                else if (FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_attack0))
                                {
                                    if (this.leftGunHasBullet)
                                    {
                                        this.leftArmAim = true;
                                        this.rightArmAim = false;
                                    }
                                    else
                                    {
                                        this.leftArmAim = false;
                                        if (this.rightGunHasBullet)
                                        {
                                            this.rightArmAim = true;
                                        }
                                        else
                                        {
                                            this.rightArmAim = false;
                                        }
                                    }
                                }
                                else
                                {
                                    this.leftArmAim = false;
                                    this.rightArmAim = false;
                                }
                                if (this.leftArmAim || this.rightArmAim)
                                {
                                    RaycastHit hit3;
                                    Ray ray3 = Camera.main.ScreenPointToRay(Input.mousePosition);
                                    LayerMask mask7 = ((int)1) << LayerMask.NameToLayer("Ground");
                                    LayerMask mask8 = ((int)1) << LayerMask.NameToLayer("EnemyBox");
                                    LayerMask mask9 = mask8 | mask7;
                                    if (Physics.Raycast(ray3, out hit3, 1E+07f, mask9.value))
                                    {
                                        this.gunTarget = hit3.point;
                                    }
                                }
                                bool flag6 = false;
                                bool flag7 = false;
                                bool flag8 = false;
                                if (FengGameManagerMKII.inputRC.isInputCustomKeyUp(InputCode.custom_attack1) && (this.skillId != "bomb"))
                                {
                                    if (this.leftGunHasBullet && this.rightGunHasBullet)
                                    {
                                        if (this.grounded)
                                        {
                                            this.attackAnimation = "AHSS_shoot_both";
                                        }
                                        else
                                        {
                                            this.attackAnimation = "AHSS_shoot_both_air";
                                        }
                                        flag6 = true;
                                    }
                                    else if (!this.leftGunHasBullet && !this.rightGunHasBullet)
                                    {
                                        flag7 = true;
                                    }
                                    else
                                    {
                                        flag8 = true;
                                    }
                                }
                                if (flag8 || FengGameManagerMKII.inputRC.isInputCustomKeyUp(InputCode.custom_attack0))
                                {
                                    if (this.grounded)
                                    {
                                        if (this.leftGunHasBullet && this.rightGunHasBullet)
                                        {
                                            if (this.isLeftHandHooked)
                                            {
                                                this.attackAnimation = "AHSS_shoot_r";
                                            }
                                            else
                                            {
                                                this.attackAnimation = "AHSS_shoot_l";
                                            }
                                        }
                                        else if (this.leftGunHasBullet)
                                        {
                                            this.attackAnimation = "AHSS_shoot_l";
                                        }
                                        else if (this.rightGunHasBullet)
                                        {
                                            this.attackAnimation = "AHSS_shoot_r";
                                        }
                                    }
                                    else if (this.leftGunHasBullet && this.rightGunHasBullet)
                                    {
                                        if (this.isLeftHandHooked)
                                        {
                                            this.attackAnimation = "AHSS_shoot_r_air";
                                        }
                                        else
                                        {
                                            this.attackAnimation = "AHSS_shoot_l_air";
                                        }
                                    }
                                    else if (this.leftGunHasBullet)
                                    {
                                        this.attackAnimation = "AHSS_shoot_l_air";
                                    }
                                    else if (this.rightGunHasBullet)
                                    {
                                        this.attackAnimation = "AHSS_shoot_r_air";
                                    }
                                    if (this.leftGunHasBullet || this.rightGunHasBullet)
                                    {
                                        flag6 = true;
                                    }
                                    else
                                    {
                                        flag7 = true;
                                    }
                                }
                                if (flag6)
                                {
                                    this.state = HERO_STATE.Attack;
                                    this.crossFade(this.attackAnimation, 0.05f);
                                    this.gunDummy.transform.position = this.baseTransform.position;
                                    this.gunDummy.transform.rotation = this.baseTransform.rotation;
                                    this.gunDummy.transform.LookAt(this.gunTarget);
                                    this.attackReleased = false;
                                    this.facingDirection = this.gunDummy.transform.rotation.eulerAngles.y;
                                    this.targetRotation = Quaternion.Euler(0f, this.facingDirection, 0f);
                                }
                                else if (flag7 && (this.grounded || ((FengGameManagerMKII.lvlInfo.type != GAMEMODE.PVP_AHSS) && (RCSettings.ahssReload == 0))))
                                {
                                    this.changeBlade();
                                }
                            }
                        }
                        else if (this.state == HERO_STATE.Attack)
                        {
                            if (!this.useGun)
                            {
                                if (!FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_attack0))
                                {
                                    this.buttonAttackRelease = true;
                                }
                                if (!this.attackReleased)
                                {
                                    if (this.buttonAttackRelease)
                                    {
                                        this.continueAnimation();
                                        this.attackReleased = true;
                                    }
                                    else if (this.baseAnimation[this.attackAnimation].normalizedTime >= 0.32f)
                                    {
                                        this.pauseAnimation();
                                    }
                                }
                                if ((this.attackAnimation == "attack3_1") && (this.currentBladeSta > 0f))
                                {
                                    if (this.baseAnimation[this.attackAnimation].normalizedTime >= 0.8f)
                                    {
                                        if (!this.checkBoxLeft.GetComponent<TriggerColliderWeapon>().active_me)
                                        {
                                            this.checkBoxLeft.GetComponent<TriggerColliderWeapon>().active_me = true;
                                            if (((int)FengGameManagerMKII.settings[0x5c]) == 0)
                                            {
                                                this.leftbladetrail2.Activate();
                                                this.rightbladetrail2.Activate();
                                                this.leftbladetrail.Activate();
                                                this.rightbladetrail.Activate();
                                            }
                                            this.baseRigidBody.velocity = (Vector3)(-Vector3.up * 30f);
                                        }
                                        if (!this.checkBoxRight.GetComponent<TriggerColliderWeapon>().active_me)
                                        {
                                            this.checkBoxRight.GetComponent<TriggerColliderWeapon>().active_me = true;
                                            if ((int)FengGameManagerMKII.settings[330] == 0)
                                            {
                                                this.slash.Play();
                                            }
                                        }
                                    }
                                    else if (this.checkBoxLeft.GetComponent<TriggerColliderWeapon>().active_me)
                                    {
                                        this.checkBoxLeft.GetComponent<TriggerColliderWeapon>().active_me = false;
                                        this.checkBoxRight.GetComponent<TriggerColliderWeapon>().active_me = false;
                                        this.checkBoxLeft.GetComponent<TriggerColliderWeapon>().clearHits();
                                        this.checkBoxRight.GetComponent<TriggerColliderWeapon>().clearHits();
                                        this.leftbladetrail.StopSmoothly(0.1f);
                                        this.rightbladetrail.StopSmoothly(0.1f);
                                        this.leftbladetrail2.StopSmoothly(0.1f);
                                        this.rightbladetrail2.StopSmoothly(0.1f);
                                    }
                                }
                                else
                                {
                                    float num;
                                    float num2;
                                    if (this.currentBladeSta == 0f)
                                    {
                                        num = -1f;
                                        num2 = -1f;
                                    }
                                    else if (this.attackAnimation == "attack5")
                                    {
                                        num2 = 0.35f;
                                        num = 0.5f;
                                    }
                                    else if (this.attackAnimation == "special_petra")
                                    {
                                        num2 = 0.35f;
                                        num = 0.48f;
                                    }
                                    else if (this.attackAnimation == "special_armin")
                                    {
                                        num2 = 0.25f;
                                        num = 0.35f;
                                    }
                                    else if (this.attackAnimation == "attack4")
                                    {
                                        num2 = 0.6f;
                                        num = 0.9f;
                                    }
                                    else if (this.attackAnimation == "special_sasha")
                                    {
                                        num = -1f;
                                        num2 = -1f;
                                    }
                                    else
                                    {
                                        num2 = 0.5f;
                                        num = 0.85f;
                                    }
                                    if ((this.baseAnimation[this.attackAnimation].normalizedTime > num2) && (this.baseAnimation[this.attackAnimation].normalizedTime < num))
                                    {
                                        if (!this.checkBoxLeft.GetComponent<TriggerColliderWeapon>().active_me)
                                        {
                                            this.checkBoxLeft.GetComponent<TriggerColliderWeapon>().active_me = true;
                                            if ((int)FengGameManagerMKII.settings[330] == 0)
                                            {
                                                this.slash.Play();
                                            }
                                            if (((int)FengGameManagerMKII.settings[0x5c]) == 0)
                                            {
                                                this.leftbladetrail2.Activate();
                                                this.rightbladetrail2.Activate();
                                                this.leftbladetrail.Activate();
                                                this.rightbladetrail.Activate();
                                            }
                                        }
                                        if (!this.checkBoxRight.GetComponent<TriggerColliderWeapon>().active_me)
                                        {
                                            this.checkBoxRight.GetComponent<TriggerColliderWeapon>().active_me = true;
                                        }
                                    }
                                    else if (this.checkBoxLeft.GetComponent<TriggerColliderWeapon>().active_me)
                                    {
                                        this.checkBoxLeft.GetComponent<TriggerColliderWeapon>().active_me = false;
                                        this.checkBoxRight.GetComponent<TriggerColliderWeapon>().active_me = false;
                                        this.checkBoxLeft.GetComponent<TriggerColliderWeapon>().clearHits();
                                        this.checkBoxRight.GetComponent<TriggerColliderWeapon>().clearHits();
                                        this.leftbladetrail2.StopSmoothly(0.1f);
                                        this.rightbladetrail2.StopSmoothly(0.1f);
                                        this.leftbladetrail.StopSmoothly(0.1f);
                                        this.rightbladetrail.StopSmoothly(0.1f);
                                    }
                                    if ((this.attackLoop > 0) && (this.baseAnimation[this.attackAnimation].normalizedTime > num))
                                    {
                                        this.attackLoop--;
                                        this.playAnimationAt(this.attackAnimation, num2);
                                    }
                                }
                                if (this.baseAnimation[this.attackAnimation].normalizedTime >= 1f)
                                {
                                    if ((this.attackAnimation == "special_marco_0") || (this.attackAnimation == "special_marco_1"))
                                    {
                                        if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)
                                        {
                                            if (!PhotonNetwork.isMasterClient)
                                            {
                                                object[] parameters = new object[] { 5f, 100f };
                                                pviev.RPC("netTauntAttack", PhotonTargets.MasterClient, parameters);
                                            }
                                            else
                                            {
                                                this.netTauntAttack(5f, 100f);
                                            }
                                        }
                                        else
                                        {
                                            this.netTauntAttack(5f, 100f);
                                        }
                                        this.falseAttack();
                                        this.idle();
                                    }
                                    else if (this.attackAnimation == "special_armin")
                                    {
                                        if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)
                                        {
                                            if (!PhotonNetwork.isMasterClient)
                                            {
                                                pviev.RPC("netlaughAttack", PhotonTargets.MasterClient, new object[0]);
                                            }
                                            else
                                            {
                                                this.netlaughAttack();
                                            }
                                        }
                                        else
                                        {
                                            foreach (TITAN titan in FengGameManagerMKII.instance.titans)
                                            {
                                                if (((Vector3.Distance(titan.transform.position, this.baseTransform.position) < 50f) && (Vector3.Angle(titan.transform.forward, this.baseTransform.position - titan.transform.position) < 90f)))
                                                {
                                                    titan.beLaughAttacked();
                                                }
                                            }
                                        }
                                        this.falseAttack();
                                        this.idle();
                                    }
                                    else if (this.attackAnimation == "attack3_1")
                                    {
                                        this.baseRigidBody.velocity -= (Vector3)((Vector3.up * Time.deltaTime) * 30f);
                                    }
                                    else
                                    {
                                        this.falseAttack();
                                        this.idle();
                                    }
                                }
                                if (this.baseAnimation.IsPlaying("attack3_2") && (this.baseAnimation["attack3_2"].normalizedTime >= 1f))
                                {
                                    this.falseAttack();
                                    this.idle();
                                }
                            }
                            else
                            {
                                this.baseTransform.rotation = Quaternion.Lerp(this.baseTransform.rotation, this.gunDummy.transform.rotation, Time.deltaTime * 30f);
                                if (!this.attackReleased && (this.baseAnimation[this.attackAnimation].normalizedTime > 0.167f))
                                {
                                    GameObject obj4;
                                    this.attackReleased = true;
                                    bool flag9 = false;
                                    if ((this.attackAnimation == "AHSS_shoot_both") || (this.attackAnimation == "AHSS_shoot_both_air"))
                                    {
                                        flag9 = true;
                                        this.leftGunHasBullet = false;
                                        this.rightGunHasBullet = false;
                                        this.baseRigidBody.AddForce((Vector3)(-this.baseTransform.forward * 1000f), ForceMode.Acceleration);
                                    }
                                    else
                                    {
                                        if ((this.attackAnimation == "AHSS_shoot_l") || (this.attackAnimation == "AHSS_shoot_l_air"))
                                        {
                                            this.leftGunHasBullet = false;
                                        }
                                        else
                                        {
                                            this.rightGunHasBullet = false;
                                        }
                                        this.baseRigidBody.AddForce((Vector3)(-this.baseTransform.forward * 600f), ForceMode.Acceleration);
                                    }
                                    this.baseRigidBody.AddForce((Vector3)(Vector3.up * 200f), ForceMode.Acceleration);
                                    string prefabName = "FX/shotGun";
                                    if (flag9)
                                    {
                                        prefabName = "FX/shotGun 1";
                                    }
                                    if ((isMulty) && pviev.isMine)
                                    {
                                        obj4 = PhotonNetwork.Instantiate(prefabName, (Vector3)((this.baseTransform.position + (this.baseTransform.up * 0.8f)) - (this.baseTransform.right * 0.1f)), this.baseTransform.rotation, 0);
                                        if (obj4.GetComponent<EnemyfxIDcontainer>() != null)
                                        {
                                            obj4.GetComponent<EnemyfxIDcontainer>().myOwnerViewID = pviev.viewID;
                                        }
                                    }
                                    else
                                    {
                                        obj4 = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(prefabName), (Vector3)((this.baseTransform.position + (this.baseTransform.up * 0.8f)) - (this.baseTransform.right * 0.1f)), this.baseTransform.rotation);

                                    }
                                }
                                if (this.baseAnimation[this.attackAnimation].normalizedTime >= 1f)
                                {
                                    this.falseAttack();
                                    this.idle();
                                }
                                if (!this.baseAnimation.IsPlaying(this.attackAnimation))
                                {
                                    this.falseAttack();
                                    this.idle();
                                }
                            }
                        }
                        else if (this.state == HERO_STATE.ChangeBlade)
                        {
                            if (this.useGun)
                            {
                                if (this.baseAnimation[this.reloadAnimation].normalizedTime > 0.22f)
                                {
                                    if (!this.leftGunHasBullet && this.setup.part_blade_l.activeSelf)
                                    {
                                        this.setup.part_blade_l.SetActive(false);
                                        Transform transform = this.setup.part_blade_l.transform;
                                        GameObject obj5 = (GameObject)UnityEngine.Object.Instantiate(Cach.Character_parts_character_gun_l != null ? Cach.Character_parts_character_gun_l : Cach.Character_parts_character_gun_l = (GameObject)Resources.Load("Character_parts/character_gun_l"), transform.position, transform.rotation);
                                        obj5.renderer.material = CharacterMaterials.materials[this.setup.myCostume._3dmg_texture];
                                        Vector3 force = ((Vector3)((-this.baseTransform.forward * 10f) + (this.baseTransform.up * 5f))) - this.baseTransform.right;
                                        obj5.rigidbody.AddForce(force, ForceMode.Impulse);
                                        Vector3 torque = new Vector3((float)UnityEngine.Random.Range(-100, 100), (float)UnityEngine.Random.Range(-100, 100), (float)UnityEngine.Random.Range(-100, 100));
                                        obj5.rigidbody.AddTorque(torque, ForceMode.Acceleration);
                                    }
                                    if (!this.rightGunHasBullet && this.setup.part_blade_r.activeSelf)
                                    {
                                        this.setup.part_blade_r.SetActive(false);
                                        Transform transform5 = this.setup.part_blade_r.transform;
                                        GameObject obj6 = (GameObject)UnityEngine.Object.Instantiate(Cach.character_gun_r != null ? Cach.character_gun_r : Cach.character_gun_r = (GameObject)Resources.Load("Character_parts/character_gun_r"), transform5.position, transform5.rotation);
                                        obj6.renderer.material = CharacterMaterials.materials[this.setup.myCostume._3dmg_texture];
                                        Vector3 vector3 = ((Vector3)((-this.baseTransform.forward * 10f) + (this.baseTransform.up * 5f))) + this.baseTransform.right;
                                        obj6.rigidbody.AddForce(vector3, ForceMode.Impulse);
                                        Vector3 vector4 = new Vector3((float)UnityEngine.Random.Range(-300, 300), (float)UnityEngine.Random.Range(-300, 300), (float)UnityEngine.Random.Range(-300, 300));
                                        obj6.rigidbody.AddTorque(vector4, ForceMode.Acceleration);
                                    }
                                }
                                if ((this.baseAnimation[this.reloadAnimation].normalizedTime > 0.62f) && !this.throwedBlades)
                                {
                                    this.throwedBlades = true;
                                    if ((this.leftBulletLeft > 0) && !this.leftGunHasBullet)
                                    {
                                        this.leftBulletLeft--;
                                        this.setup.part_blade_l.SetActive(true);
                                        this.leftGunHasBullet = true;
                                    }
                                    if ((this.rightBulletLeft > 0) && !this.rightGunHasBullet)
                                    {
                                        this.setup.part_blade_r.SetActive(true);
                                        this.rightBulletLeft--;
                                        this.rightGunHasBullet = true;
                                    }
                                    this.updateRightMagUI();
                                    this.updateLeftMagUI();
                                }
                                if (this.baseAnimation[this.reloadAnimation].normalizedTime > 1f)
                                {
                                    this.idle();
                                }
                            }
                            else
                            {
                                if (!this.grounded)
                                {
                                    if ((baseAnimation[this.reloadAnimation].normalizedTime >= 0.2f) && !this.throwedBlades)
                                    {
                                        this.throwedBlades = true;
                                        if (this.setup.part_blade_l.activeSelf)
                                        {
                                            this.throwBlades();
                                        }
                                    }
                                    if ((baseAnimation[this.reloadAnimation].normalizedTime >= 0.56f) && (this.currentBladeNum > 0))
                                    {
                                        this.setup.part_blade_l.SetActive(true);
                                        this.setup.part_blade_r.SetActive(true);
                                        this.currentBladeSta = this.totalBladeSta;
                                    }
                                }
                                else
                                {
                                    if ((this.baseAnimation[this.reloadAnimation].normalizedTime >= 0.13f) && !this.throwedBlades)
                                    {
                                        this.throwedBlades = true;
                                        if (this.setup.part_blade_l.activeSelf)
                                        {
                                            this.throwBlades();
                                        }
                                    }
                                    if ((this.baseAnimation[this.reloadAnimation].normalizedTime >= 0.37f) && (this.currentBladeNum > 0))
                                    {
                                        this.setup.part_blade_l.SetActive(true);
                                        this.setup.part_blade_r.SetActive(true);
                                        this.currentBladeSta = this.totalBladeSta;
                                    }
                                }
                                if (this.baseAnimation[this.reloadAnimation].normalizedTime >= 1f)
                                {
                                    this.idle();
                                }
                            }
                        }
                        else if (this.state == HERO_STATE.Salute)
                        {
                            if (this.baseAnimation["salute"].normalizedTime >= 1f)
                            {
                                this.idle();
                            }
                        }
                        else if (this.state == HERO_STATE.GroundDodge)
                        {
                            if (this.baseAnimation.IsPlaying("dodge"))
                            {
                                if (!this.grounded && (this.baseAnimation["dodge"].normalizedTime > 0.6f))
                                {
                                    this.idle();
                                }
                                if (this.baseAnimation["dodge"].normalizedTime >= 1f)
                                {
                                    this.idle();
                                }
                            }
                        }
                        else if (this.state == HERO_STATE.Land)
                        {
                            if (this.baseAnimation.IsPlaying("dash_land") && (this.baseAnimation["dash_land"].normalizedTime >= 1f))
                            {
                                this.idle();
                            }
                        }
                        else if (this.state == HERO_STATE.FillGas)
                        {
                            if (this.baseAnimation.IsPlaying("supply") && (this.baseAnimation["supply"].normalizedTime >= 1f))
                            {
                                isCheckedGas = false;
                                this.currentBladeSta = this.totalBladeSta;
                                this.currentBladeNum = this.totalBladeNum;
                                this.currentGas = this.totalGas;
                                if (!this.useGun)
                                {
                                    this.setup.part_blade_l.SetActive(true);
                                    this.setup.part_blade_r.SetActive(true);
                                }
                                else
                                {
                                    this.leftBulletLeft = this.rightBulletLeft = this.bulletMAX;
                                    this.rightGunHasBullet = true;
                                    this.leftGunHasBullet = true;
                                    this.setup.part_blade_l.SetActive(true);
                                    this.setup.part_blade_r.SetActive(true);
                                    this.updateRightMagUI();
                                    this.updateLeftMagUI();
                                }
                                this.idle();
                            }
                        }
                        else if (this.state == HERO_STATE.Slide)
                        {
                            if (!this.grounded)
                            {
                                this.idle();
                            }
                        }
                        else if (this.state == HERO_STATE.AirDodge)
                        {
                            if (this.dashTime > 0f)
                            {
                                this.dashTime -= Time.deltaTime;
                                if (this.currentSpeed > this.originVM)
                                {
                                    this.baseRigidBody.AddForce((Vector3)((-this.baseRigidBody.velocity * Time.deltaTime) * 1.7f), ForceMode.VelocityChange);
                                }
                            }
                            else
                            {
                                this.dashTime = 0f;
                                this.idle();
                            }
                        }
                        if (FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_leftRope))
                        {
                            flag4 = true;
                        }
                        else
                        {
                            flag4 = false;
                        }
                        if (!(flag4 ? (((this.baseAnimation.IsPlaying("attack3_1") || this.baseAnimation.IsPlaying("attack5")) || (this.baseAnimation.IsPlaying("special_petra") || (this.state == HERO_STATE.Grab))) ? (this.state != HERO_STATE.Idle) : false) : true))
                        {
                            if (this.bulletLeft != null)
                            {
                                this.QHold = true;
                            }
                            else
                            {
                                RaycastHit hit4;
                                Ray ray4 = Camera.main.ScreenPointToRay(Input.mousePosition);
                                LayerMask mask10 = ((int)1) << LayerMask.NameToLayer("Ground");
                                LayerMask mask11 = ((int)1) << LayerMask.NameToLayer("EnemyBox");
                                LayerMask mask12 = mask11 | mask10;
                                if (Physics.Raycast(ray4, out hit4, 10000f, mask12.value))
                                {
                                    this.launchLeftRope(hit4, true, 0);
                                    if ((int)FengGameManagerMKII.settings[330] == 0)
                                    {
                                        this.rope.Play();
                                    }
                                }
                            }
                        }
                        else
                        {
                            this.QHold = false;
                        }
                        if (FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_rightRope))
                        {
                            flag3 = true;
                        }
                        else
                        {
                            flag3 = false;
                        }
                        if (!(flag3 ? (((this.baseAnimation.IsPlaying("attack3_1") || this.baseAnimation.IsPlaying("attack5")) || (this.baseAnimation.IsPlaying("special_petra") || (this.state == HERO_STATE.Grab))) ? (this.state != HERO_STATE.Idle) : false) : true))
                        {
                            if (this.bulletRight != null)
                            {
                                this.EHold = true;
                            }
                            else
                            {
                                RaycastHit hit5;
                                Ray ray5 = Camera.main.ScreenPointToRay(Input.mousePosition);
                                LayerMask mask13 = ((int)1) << LayerMask.NameToLayer("Ground");
                                LayerMask mask14 = ((int)1) << LayerMask.NameToLayer("EnemyBox");
                                LayerMask mask15 = mask14 | mask13;
                                if (Physics.Raycast(ray5, out hit5, 10000f, mask15.value))
                                {
                                    this.launchRightRope(hit5, true, 0);
                                    if ((int)FengGameManagerMKII.settings[330] == 0)
                                    {
                                        this.rope.Play();
                                    }
                                }
                            }
                        }
                        else
                        {
                            this.EHold = false;
                        }
                        if (FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_bothRope))
                        {
                            flag2 = true;
                        }
                        else
                        {
                            flag2 = false;
                        }
                        if (!(flag2 ? (((this.baseAnimation.IsPlaying("attack3_1") || this.baseAnimation.IsPlaying("attack5")) || (this.baseAnimation.IsPlaying("special_petra") || (this.state == HERO_STATE.Grab))) ? (this.state != HERO_STATE.Idle) : false) : true))
                        {
                            this.QHold = true;
                            this.EHold = true;
                            if ((this.bulletLeft == null) && (this.bulletRight == null))
                            {
                                RaycastHit hit6;
                                Ray ray6 = Camera.main.ScreenPointToRay(Input.mousePosition);
                                LayerMask mask16 = ((int)1) << LayerMask.NameToLayer("Ground");
                                LayerMask mask17 = ((int)1) << LayerMask.NameToLayer("EnemyBox");
                                LayerMask mask18 = mask17 | mask16;
                                if (Physics.Raycast(ray6, out hit6, 1000000f, mask18.value))
                                {
                                    this.launchLeftRope(hit6, false, 0);
                                    this.launchRightRope(hit6, false, 0);
                                    if ((int)FengGameManagerMKII.settings[330] == 0)
                                    {
                                        this.rope.Play();
                                    }
                                }
                            }
                        }
                        if ((IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE) || ((isSingle) && !IN_GAME_MAIN_CAMERA.isPausing))
                        {
                            this.calcSkillCD();
                            this.calcFlareCD();
                        }
                        if (!this.useGun)
                        {
                            if (this.leftbladetrail.gameObject.GetActive())
                            {
                                this.leftbladetrail.update();
                                this.rightbladetrail.update();
                            }
                            if (this.leftbladetrail2.gameObject.GetActive())
                            {
                                this.leftbladetrail2.update();
                                this.rightbladetrail2.update();
                            }
                            if (this.leftbladetrail.gameObject.GetActive())
                            {
                                this.leftbladetrail.lateUpdate();
                                this.rightbladetrail.lateUpdate();
                            }
                            if (this.leftbladetrail2.gameObject.GetActive())
                            {
                                this.leftbladetrail2.lateUpdate();
                                this.rightbladetrail2.lateUpdate();
                            }
                        }
                        if (!IN_GAME_MAIN_CAMERA.isPausing)
                        {
                            this.showSkillCD();
                            this.showFlareCD2();
                            this.showGas2();
                            this.showAimUI2();
                        }
                    }
                    else if (this.isCannon && !IN_GAME_MAIN_CAMERA.isPausing)
                    {
                        this.showAimUI2();
                        this.calcSkillCD();
                        this.showSkillCD();
                    }

                    int num3 = this.isReeling();
                    if (num3 > 0)
                    {
                        bool flag7 = false;
                        bool flag8 = false;
                        if (this.isLaunchLeft && ((this.bulletLeft != null) && bulletLeftT.isHooked()))
                        {
                            this.isLeftHandHooked = true;
                            Vector3 to = bulletLeftTT.position - baseTransform.position;
                            to.Normalize();
                            to = (Vector3)(to * 10f);
                            if (!this.isLaunchRight)
                            {
                                to = (Vector3)(to * 2f);
                            }
                            if ((Vector3.Angle(this.baseRigidBody.velocity, to) > 90f) && FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_jump) && isRealiged)
                            {
                                flag7 = true;
                            }
                        }
                        if (this.isLaunchRight && ((this.bulletRight != null) && bulletRightT.isHooked()))
                        {
                            this.isRightHandHooked = true;
                            Vector3 vector4 = bulletRightTT.position - baseTransform.position;
                            vector4.Normalize();
                            vector4 = (Vector3)(vector4 * 10f);
                            if (!this.isLaunchLeft)
                            {
                                vector4 = (Vector3)(vector4 * 2f);
                            }
                            if ((Vector3.Angle(this.baseRigidBody.velocity, vector4) > 90f) && FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_jump) && isRealiged)
                            {
                                flag8 = true;
                            }
                        }
                        if (flag8 && flag7)
                        {
                            float num4 = this.currentSpeed + 0.1f;
                            this.baseRigidBody.AddForce(-this.baseRigidBody.velocity, ForceMode.VelocityChange);
                            Vector3 current = ((Vector3)((bulletRightTT.position + bulletLeftTT.position) * 0.5f)) - baseTransform.position;
                            float num5 = 0f;
                            switch (num3)
                            {
                                case 2:
                                    num5 = -1f;
                                    break;

                                case 1:
                                    num5 = 1f;
                                    break;
                            }
                            num5 = Mathf.Clamp(num5, -0.8f, 0.8f);
                            float num6 = 1f + num5;
                            Vector3 vector7 = Vector3.RotateTowards(current, this.baseRigidBody.velocity, 1.53938f * num6, 1.53938f * num6);
                            vector7.Normalize();
                            this.spinning = true;
                            this.baseRigidBody.velocity = (Vector3)(vector7 * num4);
                        }
                        else if (flag7)
                        {
                            float num7 = this.currentSpeed + 0.1f;
                            this.baseRigidBody.AddForce(-this.baseRigidBody.velocity, ForceMode.VelocityChange);
                            Vector3 vector8 = bulletLeftTT.position - baseTransform.position;
                            float num8 = 0f;
                            switch (num3)
                            {
                                case 2:
                                    num8 = -1f;
                                    break;

                                case 1:
                                    num8 = 1f;
                                    break;
                            }
                            num8 = Mathf.Clamp(num8, -0.8f, 0.8f);
                            float num9 = 1f + num8;
                            Vector3 vector9 = Vector3.RotateTowards(vector8, this.baseRigidBody.velocity, 1.53938f * num9, 1.53938f * num9);
                            vector9.Normalize();
                            this.spinning = true;
                            this.baseRigidBody.velocity = (Vector3)(vector9 * num7);
                        }
                        else if (flag8)
                        {
                            float num10 = this.currentSpeed + 0.1f;
                            this.baseRigidBody.AddForce(-this.baseRigidBody.velocity, ForceMode.VelocityChange);
                            Vector3 vector10 = bulletRightTT.position - baseTransform.position;
                            float num11 = 0f;
                            switch (num3)
                            {
                                case 2:
                                    num11 = -1f;
                                    break;

                                case 1:
                                    num11 = 1f;
                                    break;
                            }
                            num11 = Mathf.Clamp(num11, -0.8f, 0.8f);
                            float num12 = 1f + num11;
                            Vector3 vector11 = Vector3.RotateTowards(vector10, this.baseRigidBody.velocity, 1.53938f * num12, 1.53938f * num12);
                            vector11.Normalize();
                            this.spinning = true;
                            this.baseRigidBody.velocity = (Vector3)(vector11 * num10);
                        }
                    }
                    isRealiged = true;
                }
            }
        }
    }

    bool isRealiged = true;
    public void updateCannon()
    {
        this.baseTransform.position = this.myCannonPlayer.position;
        this.baseTransform.rotation = this.myCannonBase.rotation;
    }

    public void updateExt()
    {
        if (this.skillId == "bomb")
        {
            if (FengGameManagerMKII.inputRC.isInputCustomKeyDown(InputCode.custom_attack1) && (this.skillCDDuration <= 0f))
            {
                if ((this.myBomb != null) && !this.myBomb.disabled)
                {
                    this.myBomb.Explode(this.bombRadius);
                }
                this.detonate = false;
                this.skillCDDuration = this.bombCD;
                RaycastHit hitInfo = new RaycastHit();
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                LayerMask mask = ((int)1) << LayerMask.NameToLayer("Ground");
                LayerMask mask2 = ((int)1) << LayerMask.NameToLayer("EnemyBox");
                LayerMask mask3 = mask2 | mask;
                this.currentV = this.baseTransform.position;
                this.targetV = this.currentV + ((Vector3)(Vector3.forward * 200f));
                if (Physics.Raycast(ray, out hitInfo, 1000000f, mask3.value))
                {
                    this.targetV = hitInfo.point;
                }
                Vector3 vector = Vector3.Normalize(this.targetV - this.currentV);
                GameObject obj2 = PhotonNetwork.Instantiate("RCAsset/BombMain", this.currentV + ((Vector3)(vector * 4f)), new Quaternion(0f, 0f, 0f, 1f), 0);
                obj2.rigidbody.velocity = (Vector3)(vector * this.bombSpeed);
                this.myBomb = obj2.GetComponent<Bomb>();
                this.bombTime = 0f;
            }
            else if ((this.myBomb != null) && !this.myBomb.disabled)
            {
                this.bombTime += Time.deltaTime;
                bool flag = false;
                if (FengGameManagerMKII.inputRC.isInputCustomKeyUp(InputCode.custom_attack1))
                {
                    this.detonate = true;
                }
                else if (FengGameManagerMKII.inputRC.isInputCustomKeyDown(InputCode.custom_attack1) && this.detonate)
                {
                    this.detonate = false;
                    flag = true;
                }
                if (this.bombTime >= this.bombTimeMax)
                {
                    flag = true;
                }
                if (flag)
                {
                    this.myBomb.Explode(this.bombRadius);
                    this.detonate = false;
                }
            }
        }
    }

    public void updateLeftMagUI()
    {

    }

    public void updateRightMagUI()
    {
        if (showsprites)
        {
            for (int i = 1; i <= this.bulletMAX; i++)
            {
                if (i < leftBulletLeft)
                {
                    cachedSprites["bulletL" + i].enabled = true;
                }
                else
                {
                    cachedSprites["bulletL" + i].enabled = false;
                }
                if (i < rightBulletLeft)
                {
                    cachedSprites["bulletR" + i].enabled = true;
                }
                else
                {
                    cachedSprites["bulletR" + i].enabled = false;
                }

            }
        }
    }

    public void useBlade(int amount = 0)
    {
        if (amount == 0)
        {
            amount = 1;
        }
        amount *= 2;
        if (this.currentBladeSta > 0f)
        {
            this.currentBladeSta -= amount;
            if (this.currentBladeSta <= 0f)
            {
                if ((isSingle) || pviev.isMine)
                {
                    DiactivateTrail(false);
                    this.checkBoxLeft.GetComponent<TriggerColliderWeapon>().active_me = false;
                    this.checkBoxRight.GetComponent<TriggerColliderWeapon>().active_me = false;
                }
                this.currentBladeSta = 0f;
                this.throwBlades();
            }
        }
    }

    private void useGas(float amount = 0f)
    {
        if (amount == 0f)
        {
            amount = this.useGasSpeed;
        }
        if (this.currentGas > 0f)
        {
            if (!isCheckedGas && currentGas < 10f)
            {
                isCheckedGas = true;
                PanelInformer.instance.Add(INC.la("no_gas"), PanelInformer.LOG_TYPE.DANGER);
            }
            this.currentGas -= amount;
            if (this.currentGas < 0f)
            {
                this.currentGas = 0f;
            }
        }
    }

    [RPC]
    private void whoIsMyErenTitan(int id)
    {
        this.eren_titan = PhotonView.Find(id).gameObject;
        this.titanForm = true;
    }

    public bool isGrabbed
    {
        get { return (this.state == HERO_STATE.Grab); }
    }

    private HERO_STATE state
    {
        get { return this._state; }
        set
        {
            if ((this._state == HERO_STATE.AirDodge) || (this._state == HERO_STATE.GroundDodge))
            {
                this.dashTime = 0f;
            }
            this._state = value;
        }
    }

    public int isReeling()
    {
        int result;
        if (Input.GetAxis("Mouse ScrollWheel") > 0 || Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                result = 2;
            }
            else
            {
                result = 1;
            }
        }
        else
        {
            result = 0;
        }
        return result;
    }
}

