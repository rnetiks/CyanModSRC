using CyanMod;
using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class IN_GAME_MAIN_CAMERA : MonoBehaviour
{
    public RotationAxes axes;
    public Rigidbody baseR;
    public Transform baseT;
    public AudioSource bgmusic;
    public static float cameraDistance = 0.6f;
    public static CAMERA_TYPE cameraMode { get; set; }
    public static int cameraTilt = 1;
    public Light camLght;
    public static int character = 1;
    private float closestDistance;
    private int currentPeekPlayerIndex;
    public static DayLight dayLight = DayLight.Dawn;
    private float decay;
    public static DIFFICULTY difficulty { get; set; }
    private float distance = 10f;
    private float distanceMulti;
    private float distanceOffsetMulti;
    private float duration;
    private float flashDuration;
    private bool flip;
    public static GAMEMODE gamemode { get; set; }
    public bool gameOver;
    public static GAMETYPE gametype{get;set;}
    private bool hasSnapShot;
    private Transform head;
    public float height = 5f;
    public float heightDamping = 2f;
    private float heightMulti;
    public static IN_GAME_MAIN_CAMERA instance{get;set;}
    public static int invertY { get; set; }
    public bool isOn;
    public static bool isPausing;
    public static bool isTyping;
    public float justHit;
    public int lastScore;
    public static int level;
    private bool lockAngle;
    private Vector3 lockCameraPosition;
    private GameObject locker;
    private GameObject lockTarget;
    public GameObject main_object;
    public HERO main_objectH;
    public Rigidbody main_objectR;
    public Transform main_objectT;
    public float maximumX = 360f;
    public float maximumY = 60f;
    public float minimumX = -360f;
    public float minimumY = -60f;
    public MouseLook mouselook;
    public bool needSetHUD;
    private float R;
    public float rotationY;
    public int score;
    public static float sensitivityMulti { get; set; }
    public static string singleCharacter { get; set; }
    public Skybox skybox;
    public Material skyBoxDAWN;
    public Material skyBoxDAY;
    public Material skyBoxNIGHT;
    public SpectatorMovement Smov;
    private Texture2D snapshot1;
    private Texture2D snapshot2;
    private Texture2D snapshot3;
    public GameObject snapShotCamera;
    private int snapShotCount;
    private float snapShotCountDown;
    private int snapShotDmg;
    private float snapShotInterval = 0.02f;
    private float snapShotStartCountDownTime;
    private GameObject snapShotTarget;
    private Vector3 snapShotTargetPosition;
    public bool spectatorMode;
    private bool startSnapShotFrameCount;
    public static STEREO_3D_TYPE stereoType;
    public Transform target;
    public Texture texture;
    public TiltShift tiltShift;
    public float timer;
    public static bool triggerAutoLock;
    public static bool usingTitan{get;set;}
    private Vector3 verticalHeightOffset = Vector3.zero;
    public float verticalRotationOffset;
    public float xSpeed = -3f;
    public float ySpeed = -0.8f;
    private void Awake()
    {
        isTyping = false;
        isPausing = false;
        base.name = "MainCamera";
        instance = this;
        this.baseT = Camera.main.transform;
        this.baseR = Camera.main.rigidbody;
        this.Smov = base.GetComponent<SpectatorMovement>();
        this.mouselook = base.GetComponent<MouseLook>();
        this.camLght = CachingsGM.Find("mainLight").GetComponent<Light>();
        this.skybox = base.GetComponent<Skybox>();
        this.tiltShift = base.GetComponent<TiltShift>();
        this.tiltShift.enabled = false; 
        this.CreateMinimap();
    }

    private void camareMovement()
    {
        if ((int)FengGameManagerMKII.settings[418] == 1)
        {
            this.distanceOffsetMulti = cameraDistance * 60 / 150f;
        }
        else if ((int)FengGameManagerMKII.settings[418] == 2)
        {
            this.distanceOffsetMulti = (cameraDistance * (180f - base.camera.fieldOfView)) / 150f;
        }
        else
        {
            this.distanceOffsetMulti = (cameraDistance * (200f - base.camera.fieldOfView)) / 150f;
        }
        this.baseT.position = (this.head == null) ? this.main_objectT.position : this.head.transform.position;
        this.baseT.position += (Vector3)(Vector3.up * this.heightMulti);
        this.baseT.position -= (Vector3)((Vector3.up * (0.6f - cameraDistance)) * 2f);
        float y = this.baseT.eulerAngles.y;
        float z = this.baseT.eulerAngles.z;
        Quaternion quaternion = Quaternion.Euler(0f, y, 0f);
        if (cameraMode == CAMERA_TYPE.WOW)
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                float angle = (Input.GetAxis("Mouse X") * 10f) * IN_GAME_MAIN_CAMERA.sensitivityMulti;
                float num4 = ((-Input.GetAxis("Mouse Y") * 10f) * IN_GAME_MAIN_CAMERA.sensitivityMulti) * invertY;
                this.baseT.RotateAround(this.baseT.position, Vector3.up, angle);
                this.baseT.RotateAround(this.baseT.position, this.baseT.right, num4);
            }
            this.baseT.position -= (Vector3)(((this.baseT.forward * this.distance) * this.distanceMulti) * this.distanceOffsetMulti);
        }
        else if (cameraMode == CAMERA_TYPE.ORIGINAL)
        {
            float num5 = 0f;
            if (Input.mousePosition.x < (Screen.width * 0.4f))
            {
                num5 = (-((((Screen.width * 0.4f) - Input.mousePosition.x) / ((float)Screen.width)) * 0.4f) * this.getSensitivityMultiWithDeltaTime) * 150f;
                this.baseT.RotateAround(this.baseT.position, Vector3.up, num5);
            }
            else if (Input.mousePosition.x > (Screen.width * 0.6f))
            {
                num5 = ((((Input.mousePosition.x - (Screen.width * 0.6f)) / ((float)Screen.width)) * 0.4f) * this.getSensitivityMultiWithDeltaTime) * 150f;
                this.baseT.RotateAround(this.baseT.position, Vector3.up, num5);
            }
            float x = ((140f * ((Screen.height * 0.6f) - Input.mousePosition.y)) / ((float)Screen.height)) * 0.5f;
            this.baseT.rotation = Quaternion.Euler(x, this.baseT.rotation.eulerAngles.y, this.baseT.rotation.eulerAngles.z);
            this.baseT.position -= (Vector3)(((this.baseT.forward * this.distance) * this.distanceMulti) * this.distanceOffsetMulti);
        }
        else if (cameraMode == CAMERA_TYPE.TPS)
        {
            if (!FengGameManagerMKII.instance.MenuOn && !SpeeFocusPlayer.isShow && !(PanelSetHeroCustom.instance != null && PanelSetHeroCustom.instance.enabled))
            {
                Screen.lockCursor = true;
            }
            float num7 = (Input.GetAxis("Mouse X") * 10f) * sensitivityMulti;
            float num8 = ((-Input.GetAxis("Mouse Y") * 10f) * sensitivityMulti) * invertY;
            this.baseT.RotateAround(this.baseT.position, Vector3.up, num7);
            float num9 = this.baseT.rotation.eulerAngles.x % 360f;
            float num10 = num9 + num8;
            if (((num8 <= 0f) || (((num9 >= 260f) || (num10 <= 260f)) && ((num9 >= 80f) || (num10 <= 80f)))) && ((num8 >= 0f) || (((num9 <= 280f) || (num10 >= 280f)) && ((num9 <= 100f) || (num10 >= 100f)))))
            {
                this.baseT.RotateAround(this.baseT.position, this.baseT.right, num8);
            }
            this.baseT.position -= (Vector3)(((this.baseT.forward * this.distance) * this.distanceMulti) * this.distanceOffsetMulti);
        }
        else if (cameraMode == CAMERA_TYPE.oldTPS)
        {
            if (!FengGameManagerMKII.instance.MenuOn && !SpeeFocusPlayer.isShow && !(PanelSetHeroCustom.instance != null && PanelSetHeroCustom.instance.enabled))
            {
                Screen.lockCursor = true;
            }
            this.rotationY += ((Input.GetAxis("Mouse Y") * 2.5f) * (sensitivityMulti * 3f)) * invertY;
            this.rotationY = Mathf.Clamp(this.rotationY, -60f, 60f);
            this.rotationY = Mathf.Max(this.rotationY, -999f + (this.heightMulti * 2f));
            this.rotationY = Mathf.Min(this.rotationY, 999f);
            this.baseT.localEulerAngles = new Vector3(-this.rotationY, this.baseT.localEulerAngles.y + ((Input.GetAxis("Mouse X") * 2.5f) * (sensitivityMulti * 3f)), z);
            quaternion = Quaternion.Euler(0f, this.baseT.eulerAngles.y, 0f);
            this.baseT.position -= (Vector3)((((quaternion * Vector3.forward) * 10f) * this.distanceMulti) * this.distanceOffsetMulti);
            this.baseT.position += (Vector3)((((-Vector3.up * this.rotationY) * 0.1f) * ((float)Math.Pow((double)this.heightMulti, 1.1))) * this.distanceOffsetMulti);
        }
        if (cameraDistance < 0.65f)
        {
            this.baseT.position += (Vector3)(this.baseT.right * Mathf.Max((float)((0.6f - cameraDistance) * 2f), (float)0.65f));
        }
    }

    public void CameraMovementLive(HERO hero)
    {
        float magnitude = hero.rigidbody.velocity.magnitude;
        if (magnitude > 10f)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, Mathf.Min((float)100f, (float)(magnitude + 40f)), 0.1f);
        }
        else
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 50f, 0.1f);
        }
        float num2 = (hero.CameraMultiplier * (200f - Camera.main.fieldOfView)) / 150f;
        this.baseT.position = (Vector3)((this.head.transform.position + (Vector3.up * this.heightMulti)) - ((Vector3.up * (0.6f - cameraDistance)) * 2f));
        this.baseT.position -= (Vector3)(((this.baseT.forward * this.distance) * this.distanceMulti) * num2);
        if (hero.CameraMultiplier < 0.65f)
        {
            this.baseT.position += (Vector3)(this.baseT.right * Mathf.Max((float)((0.6f - hero.CameraMultiplier) * 2f), (float)0.65f));
        }
        this.baseT.rotation = Quaternion.Lerp(Camera.main.transform.rotation, hero.smoothSyncMovement.correctCameraRot, Time.deltaTime * 5f);
    }

    public CAMERA_TYPE chageCamera()
    {
        bool flag = false;
        CAMERA_TYPE cameraMode = IN_GAME_MAIN_CAMERA.cameraMode;
        int num = 0;
        while (!flag)
        {
            num++;
            switch (cameraMode)
            {
                case CAMERA_TYPE.ORIGINAL:
                    cameraMode = CAMERA_TYPE.WOW;
                    Screen.lockCursor = false;
                    if (((int)FengGameManagerMKII.settings[0x146]) == 0)
                    {
                        flag = true;
                    }
                    break;

                case CAMERA_TYPE.WOW:
                    cameraMode = CAMERA_TYPE.TPS;
                    Screen.lockCursor = true;
                    if (((int)FengGameManagerMKII.settings[0x145]) == 0)
                    {
                        flag = true;
                    }
                    break;

                case CAMERA_TYPE.TPS:
                    cameraMode = CAMERA_TYPE.oldTPS;
                    Screen.lockCursor = true;
                    if (((int)FengGameManagerMKII.settings[0x147]) == 0)
                    {
                        flag = true;
                    }
                    break;

                case CAMERA_TYPE.oldTPS:
                    cameraMode = CAMERA_TYPE.ORIGINAL;
                    Screen.lockCursor = false;
                    if (((int)FengGameManagerMKII.settings[0x144]) == 0)
                    {
                        flag = true;
                    }
                    break;
            }
            if (num > 10)
            {
                cameraMode = CAMERA_TYPE.ORIGINAL;
                Screen.lockCursor = false;
                flag = true;
            }
        }
        PrefersCyan.SetString("string_cameraType", cameraMode.ToString());
        return cameraMode;
    }

    private void CreateMinimap()
    {
        if (FengGameManagerMKII.lvlInfo != null)
        {
            Minimap minimap = base.gameObject.AddComponent<Minimap>();
            if (Minimap.instance.myCam == null)
            {
                Minimap.instance.myCam = new GameObject().AddComponent<Camera>();
                Minimap.instance.myCam.nearClipPlane = 0.3f;
                Minimap.instance.myCam.farClipPlane = 1000f;
                Minimap.instance.myCam.enabled = false;
            }
            minimap.CreateMinimap(Minimap.instance.myCam, 0x200, 0.3f, FengGameManagerMKII.lvlInfo.minimapPreset);
            if ((((int)FengGameManagerMKII.settings[0xe7]) == 0) || (RCSettings.globalDisableMinimap == 1))
            {
                minimap.SetEnabled(false);
            }
        }
    }

    public void createSnapShotRT()
    {
    }

    public void createSnapShotRT2()
    {
        Camera component = this.snapShotCamera.GetComponent<Camera>();
        if (component.targetTexture != null)
        {
            component.targetTexture.Release();
        }
        float num = (float)FengGameManagerMKII.settings[0x10d];
        if (num > 0f)
        {
            component.targetTexture = new RenderTexture((int)(Screen.width * num), (int)(Screen.height * num), 0x18);
        }
        else
        {
            component.targetTexture = new RenderTexture((int)(Screen.width * 0.4f), (int)(Screen.height * 0.4f), 0x18);
        }
    }

    private GameObject findNearestTitan()
    {
        GameObject obj2 = null;
        float positiveInfinity = float.PositiveInfinity;
        this.closestDistance = float.PositiveInfinity;
        float num2 = positiveInfinity;
        Vector3 position = this.main_objectT.position;
        foreach (GameObject obj3 in FengGameManagerMKII.instance.alltitans)
        {
            Vector3 vector2 = obj3.transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck").position - position;
            float magnitude = vector2.magnitude;
            TITAN component = obj3.GetComponent<TITAN>();
            if ((magnitude < num2) && ((component == null) || !component.hasDie))
            {
                obj2 = obj3;
                num2 = magnitude;
                this.closestDistance = num2;
            }
        }
        return obj2;
    }

    public void flashBlind()
    {
        CachingsGM.Find("flash").GetComponent<UISprite>().alpha = 1f;
        this.flashDuration = 2f;
    }

    private float getSensitivityMultiWithDeltaTime
    {
        get { return ((sensitivityMulti * Time.deltaTime) * 62f); }
    }

    private void OnDestroy()
    {
        instance = null;
    }

    private void reset()
    {
        if (gametype == GAMETYPE.SINGLE)
        {
            FengGameManagerMKII.instance.restartGameSingle2();
        }
    }

    private Texture2D RTImage(Camera cam)
    {
        RenderTexture active = RenderTexture.active;
        RenderTexture.active = cam.targetTexture;
        cam.Render();
        Texture2D textured = new Texture2D(cam.targetTexture.width, cam.targetTexture.height);
        int num = (int)(cam.targetTexture.width * 0.04f);
        int destX = (int)(cam.targetTexture.width * 0.02f);
        textured.ReadPixels(new Rect((float)num, (float)num, (float)(cam.targetTexture.width - num), (float)(cam.targetTexture.height - num)), destX, destX);
        textured.Apply();
        RenderTexture.active = active;
        return textured;
    }

    private Texture2D RTImage2(Camera cam)
    {
        RenderTexture active = RenderTexture.active;
        RenderTexture.active = cam.targetTexture;
        cam.Render();
        Texture2D textured = new Texture2D(cam.targetTexture.width, cam.targetTexture.height);
        int num = (int)(cam.targetTexture.width * 0.04f);
        int destX = (int)(cam.targetTexture.width * 0.02f);
        textured.SetPixel(0, 0, Color.white);
        textured.ReadPixels(new Rect((float)num, (float)num, (float)(cam.targetTexture.width - num), (float)(cam.targetTexture.height - num)), destX, destX);
        textured.Apply();
        RenderTexture.active = active;
        return textured;
    }

    public void setDayLight(DayLight val)
    {
        dayLight = val;
        if (dayLight == DayLight.Night)
        {
            GameObject obj2 = (GameObject)UnityEngine.Object.Instantiate((Cach.flashlight != null) ? Cach.flashlight : ((GameObject)Resources.Load("flashlight")));
            obj2.transform.parent = this.baseT;
            obj2.transform.position = this.baseT.position;
            obj2.transform.rotation = Quaternion.Euler(353f, 0f, 0f);
            RenderSettings.ambientLight = FengColor.nightAmbientLight;
            this.camLght.color = FengColor.nightLight;
            this.skybox.material = this.skyBoxNIGHT;
        }
        if (dayLight == DayLight.Day)
        {
            RenderSettings.ambientLight = FengColor.dayAmbientLight;
            this.camLght.color = FengColor.dayLight;
            this.skybox.material = this.skyBoxDAY;
        }
        if (dayLight == DayLight.Dawn)
        {
            RenderSettings.ambientLight = FengColor.dawnAmbientLight;
            this.camLght.color = FengColor.dawnAmbientLight;
            this.skybox.material = this.skyBoxDAWN;
        }
        this.snapShotCamera.gameObject.GetComponent<Skybox>().material = this.skybox.material;
    }

    public void setHUDposition()
    {
        CachingsGM.Find<Transform>("Flare").localPosition = new Vector3((float)(((int)(-Screen.width * 0.5f)) + 14), (float)((int)(-Screen.height * 0.5f)), 0f);
        CachingsGM.Find<Transform>("LabelInfoBottomRight").localPosition = new Vector3((float)((int)(Screen.width * 0.5f)), (float)((int)(-Screen.height * 0.5f)), 0f);
        CachingsGM.Find<Transform>("LabelInfoTopCenter").localPosition = new Vector3(0f, (float)((int)(Screen.height * 0.5f)), 0f);
        CachingsGM.Find<Transform>("LabelInfoTopRight").localPosition = new Vector3((float)((int)(Screen.width * 0.5f)), (float)((int)(Screen.height * 0.5f)), 0f);
        CachingsGM.Find<Transform>("LabelNetworkStatus").localPosition = new Vector3((float)((int)(-Screen.width * 0.5f)), (float)((int)(Screen.height * 0.5f)), 0f);
        CachingsGM.Find<Transform>("LabelInfoTopLeft").localPosition = new Vector3((float)((int)(-Screen.width * 0.5f)), (float)((int)((Screen.height * 0.5f) - 20f)), 0f);
        CachingsGM.Find<Transform>("Chatroom").localPosition = new Vector3((float)((int)(-Screen.width * 0.5f)), (float)((int)(-Screen.height * 0.5f)), 0f);
        if (InRoomChat.instance != null)
        {
            InRoomChat.instance.setPosition();
        }
        if (usingTitan && (gametype != GAMETYPE.SINGLE))
        {
            Vector3 vector = new Vector3(0f, 9999f, 0f);
            CachingsGM.Find<Transform>("skill_cd_bottom").localPosition = vector;
            CachingsGM.Find<Transform>("skill_cd_armin").localPosition = vector;
            CachingsGM.Find<Transform>("skill_cd_eren").localPosition = vector;
            CachingsGM.Find<Transform>("skill_cd_jean").localPosition = vector;
            CachingsGM.Find<Transform>("skill_cd_levi").localPosition = vector;
            CachingsGM.Find<Transform>("skill_cd_marco").localPosition = vector;
            CachingsGM.Find<Transform>("skill_cd_mikasa").localPosition = vector;
            CachingsGM.Find<Transform>("skill_cd_petra").localPosition = vector;
            CachingsGM.Find<Transform>("skill_cd_sasha").localPosition = vector;
            CachingsGM.Find<Transform>("GasUI").localPosition = vector;
            CachingsGM.Find<Transform>("stamina_titan").localPosition = new Vector3(-160f, (float)((int)((-Screen.height * 0.5f) + 15f)), 0f);
            CachingsGM.Find<Transform>("stamina_titan_bottom").localPosition = new Vector3(-160f, (float)((int)((-Screen.height * 0.5f) + 15f)), 0f);
        }
        else
        {
            CachingsGM.Find<Transform>("skill_cd_bottom").localPosition = new Vector3(0f, (float)((int)((-Screen.height * 0.5f) + 5f)), 0f);
            CachingsGM.Find<Transform>("GasUI").localPosition = CachingsGM.Find("skill_cd_bottom").transform.localPosition;
            CachingsGM.Find<Transform>("stamina_titan").localPosition = new Vector3(0f, 9999f, 0f);
            CachingsGM.Find<Transform>("stamina_titan_bottom").localPosition = new Vector3(0f, 9999f, 0f);
        }
        if ((this.main_object != null) && (this.main_objectH != null))
        {
            if (gametype == GAMETYPE.SINGLE)
            {
                this.main_objectH.setSkillHUDPosition2();
            }
            else if ((this.main_objectH.photonView != null) && this.main_objectH.photonView.isMine)
            {
                this.main_objectH.setSkillHUDPosition2();
            }
        }
        if (stereoType == STEREO_3D_TYPE.SIDE_BY_SIDE)
        {
            base.gameObject.GetComponent<Camera>().aspect = Screen.width / Screen.height;
        }
        this.createSnapShotRT2();
    }

    public GameObject setMainObject(GameObject obj, bool resetRotation = true, bool lockAngle = false)
    {
        float num;
        this.main_object = obj;
        if (obj == null)
        {
            this.head = null;
            num = 1f;
            this.heightMulti = 1f;
            this.distanceMulti = num;
            this.main_objectR = null;
            this.main_objectT = null;
        }
        else if (this.main_object.transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck/head") != null)
        {
            this.main_objectR = this.main_object.rigidbody;
            this.main_objectT = this.main_object.transform;
            this.main_objectH = this.main_object.GetComponent<HERO>();
            this.head = this.main_objectT.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck/head");
            this.distanceMulti = (this.head != null) ? (Vector3.Distance(this.head.transform.position, this.main_objectT.position) * 0.2f) : 1f;
            this.heightMulti = (this.head != null) ? (Vector3.Distance(this.head.transform.position, this.main_objectT.position) * 0.33f) : 1f;
            if (resetRotation)
            {
                this.baseT.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
        else if (this.main_object.transform.Find("Amarture/Controller_Body/hip/spine/chest/neck/head") != null)
        {
            this.head = this.main_object.transform.Find("Amarture/Controller_Body/hip/spine/chest/neck/head");
            num = 0.64f;
            this.heightMulti = 0.64f;
            this.distanceMulti = num;
            if (resetRotation)
            {
                this.baseT.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            this.main_objectR = this.main_object.rigidbody;
            this.main_objectT = this.main_object.transform;
            this.main_objectH = this.main_object.GetComponent<HERO>();
        }
        else
        {
            this.main_objectR = this.main_object.rigidbody;
            this.main_objectT = this.main_object.transform;
            this.main_objectH = null;
            this.head = null;
            num = 1f;
            this.heightMulti = 1f;
            this.distanceMulti = num;
            if (resetRotation)
            {
                this.baseT.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
        this.lockAngle = lockAngle;
        return obj;
    }

    public GameObject setMainObjectASTITAN(GameObject obj)
    {
        this.main_object = obj;
        if (this.main_object.transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck/head") != null)
        {
            this.main_objectR = this.main_object.rigidbody;
            this.main_objectT = this.main_object.transform;
            this.head = this.main_objectT.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck/head");
            this.distanceMulti = (this.head != null) ? (Vector3.Distance(this.head.transform.position, this.main_objectT.position) * 0.4f) : 1f;
            this.heightMulti = (this.head != null) ? (Vector3.Distance(this.head.transform.position, this.main_objectT.position) * 0.45f) : 1f;
            this.baseT.rotation = Quaternion.Euler(0f, 0f, 0f);
            this.main_objectH = null;
        }
        return obj;
    }

    public void setSpectorMode(bool val)
    {
        this.spectatorMode = val;
        this.Smov.disable = !val;
        this.mouselook.disable = !val;
    }

    private void shakeUpdate()
    {
        if (this.duration > 0f)
        {
            this.duration -= Time.deltaTime;
            if (this.flip)
            {
                this.baseT.position += (Vector3)(Vector3.up * this.R);
            }
            else
            {
                this.baseT.position -= (Vector3)(Vector3.up * this.R);
            }
            this.flip = !this.flip;
            this.R *= this.decay;
        }
    }

    UITexture uiTextSnapLocal;
    UITexture uiTextSnap
    {
        get
        {
          return  uiTextSnapLocal != null ? uiTextSnapLocal : uiTextSnapLocal = FengGameManagerMKII.instance.uiT.panels[0].transform.Find("snapshot1").GetComponent<UITexture>();
        }
    }

    public void snapShot2(int index)
    {
        uiTextSnap.enabled = false;
        Vector3 vector;
        RaycastHit hit;
        Transform transform = this.snapShotCamera.transform;
        Transform transform2 = this.main_objectT;
        Transform transform3 = this.head.transform;
        transform.position = (this.head == null) ? transform2.position : transform3.position;
        transform.position += (Vector3)(Vector3.up * this.heightMulti);
        transform.position -= (Vector3)(Vector3.up * 1.1f);
        Vector3 worldPosition = vector = transform.position;
        Vector3 vector3 = (Vector3)((worldPosition + this.snapShotTargetPosition) * 0.5f);
        transform.position = vector3;
        worldPosition = vector3;
        transform.LookAt(this.snapShotTargetPosition);
        if (index == 3)
        {
            transform.RotateAround(this.baseT.position, Vector3.up, UnityEngine.Random.Range((float)-180f, (float)180f));
        }
        else
        {
            transform.RotateAround(this.baseT.position, Vector3.up, UnityEngine.Random.Range((float)-20f, (float)20f));
        }
        transform.LookAt(worldPosition);
        transform.RotateAround(worldPosition, this.baseT.right, UnityEngine.Random.Range((float)-20f, (float)20f));
        float num = Vector3.Distance(this.snapShotTargetPosition, vector);
        if ((this.snapShotTarget != null) && (this.snapShotTarget.GetComponent<TITAN>() != null))
        {
            num += ((index - 1) * transform.localScale.x) * 10f;
        }
        transform.position -= (Vector3)(transform.forward * UnityEngine.Random.Range((float)(num + 3f), (float)(num + 10f)));
        transform.LookAt(worldPosition);
        transform.RotateAround(worldPosition, this.baseT.forward, UnityEngine.Random.Range((float)-30f, (float)30f));
        Vector3 end = (this.head == null) ? transform2.position : transform3.position;
        Vector3 vector5 = ((this.head == null) ? transform2.position : transform3.position) - transform.position;
        end -= vector5;
        LayerMask mask = ((int)1) << LayerMask.NameToLayer("Ground");
        LayerMask mask2 = ((int)1) << LayerMask.NameToLayer("EnemyBox");
        LayerMask mask3 = mask | mask2;
        if (this.head != null)
        {
            if (Physics.Linecast(transform3.position, end, out hit, (int)mask))
            {
                transform.position = hit.point;
            }
            else if (Physics.Linecast(transform3.position - ((Vector3)((vector5 * this.distanceMulti) * 3f)), end, out hit, (int)mask3))
            {
                transform.position = hit.point;
            }
        }
        else if (Physics.Linecast(transform2.position + Vector3.up, end, out hit, (int)mask3))
        {
            transform.position = hit.point;
        }
        Camera component = this.snapShotCamera.GetComponent<Camera>();
        switch (index)
        {
            case 1:
                this.snapshot1 = this.RTImage2(component);
                SnapShotSaves.addIMG(this.snapshot1, this.snapShotDmg);
                break;

            case 2:
                this.snapshot2 = this.RTImage2(component);
                SnapShotSaves.addIMG(this.snapshot2, this.snapShotDmg);
                break;

            case 3:
                this.snapshot3 = this.RTImage2(component);
                SnapShotSaves.addIMG(this.snapshot3, this.snapShotDmg);
                break;
        }
        this.snapShotCount = index;
        this.hasSnapShot = true;
        this.snapShotCountDown = 2f;
        if (index == 1)
        {
            uiTextSnap.mainTexture = this.snapshot1;
            uiTextSnap.transform.localScale = new Vector3(Screen.width * 0.4f, Screen.height * 0.4f, 1f);
            uiTextSnap.transform.localPosition = new Vector3(-Screen.width * 0.225f, Screen.height * 0.225f, 0f);
            uiTextSnap.transform.rotation = Quaternion.Euler(0f, 0f, 10f);
        }
    }

    public void snapShotUpdate()
    {
        if (this.startSnapShotFrameCount)
        {
            this.snapShotStartCountDownTime -= Time.deltaTime;
            if (this.snapShotStartCountDownTime <= 0f)
            {
                this.snapShot2(1);
                uiTextSnap.enabled = false;
                this.startSnapShotFrameCount = false;
            }
        }
        if (this.hasSnapShot)
        {
            this.snapShotCountDown -= Time.deltaTime;
            if (this.snapShotCountDown <= 0f)
            {
                uiTextSnap.enabled = false;
                this.hasSnapShot = false;
                this.snapShotCountDown = 0f;
            }
            else if (this.snapShotCountDown < 1f)
            {
                uiTextSnap.mainTexture = this.snapshot3;
                uiTextSnap.enabled = (((int)FengGameManagerMKII.settings[0x143]) == 1);
            }
            else if (this.snapShotCountDown < 1.5f)
            {
                uiTextSnap.mainTexture = this.snapshot2;
                uiTextSnap.enabled = (((int)FengGameManagerMKII.settings[0x143]) == 1);
            }
              else if (this.snapShotCountDown < 2f)
            {
                uiTextSnap.mainTexture = this.snapshot1;
                uiTextSnap.enabled = (((int)FengGameManagerMKII.settings[0x143]) == 1);
            }
            if (this.snapShotCount < 3)
            {
                this.snapShotInterval -= Time.deltaTime;
                if (this.snapShotInterval <= 0f)
                {
                    this.snapShotInterval = 0.05f;
                    this.snapShotCount++;
                    this.snapShot2(this.snapShotCount);
                }
            }
        }
    }

    private void Start()
    {
        instance = this;
        isPausing = false;
        invertY = (int)FengGameManagerMKII.settings[0x142];
        this.setDayLight(dayLight);
        this.locker = CachingsGM.Find("locker");
        cameraTilt = (int)FengGameManagerMKII.settings[0x141];
        cameraDistance = FengGameManagerMKII.instance.distanceSlider + 0.3f;
        this.createSnapShotRT2();
        this.isOn = true;
    }

    public void startShake(float R, float duration, float decay = 0.95f)
    {
        if (this.duration < duration)
        {
            this.R = R;
            this.duration = duration;
            this.decay = decay;
        }
    }

    public void startSnapShot(Vector3 p, int dmg, GameObject target, float startTime)
    {
        int num;
        if (int.TryParse((string)FengGameManagerMKII.settings[0x5f], out num))
        {
            if (dmg >= num)
            {
                this.snapShotCount = 1;
                this.startSnapShotFrameCount = true;
                this.snapShotTargetPosition = p;
                this.snapShotTarget = target;
                this.snapShotStartCountDownTime = startTime;
                this.snapShotInterval = 0.05f + UnityEngine.Random.Range((float)0f, (float)0.03f);
                this.snapShotDmg = dmg;
            }
        }
        else
        {
            this.snapShotCount = 1;
            this.startSnapShotFrameCount = true;
            this.snapShotTargetPosition = p;
            this.snapShotTarget = target;
            this.snapShotStartCountDownTime = startTime;
            this.snapShotInterval = 0.05f + UnityEngine.Random.Range((float)0f, (float)0.03f);
            this.snapShotDmg = dmg;
        }
    }

    public void update()
    {
    }
    UISprite flash;
    public void update2()
    {
        if (this.flashDuration > 0f)
        {
            this.flashDuration -= Time.deltaTime;
            if (this.flashDuration <= 0f)
            {
                this.flashDuration = 0f;
            }
            (flash != null ? flash : flash = CachingsGM.Find("flash").GetComponent<UISprite>()).alpha = this.flashDuration * 0.5f;
        }
        if (gametype == GAMETYPE.STOP)
        {
            Screen.showCursor = true;
            Screen.lockCursor = false;
        }
        else
        {
            if ((gametype != GAMETYPE.SINGLE) && this.gameOver)
            {
                if (FengGameManagerMKII.inputRC.isInputCustomKeyDown(InputCode.custom_attack1))
                {
                    if (this.spectatorMode)
                    {
                        this.setSpectorMode(false);
                    }
                    else
                    {
                        this.setSpectorMode(true);
                    }
                }
                if (FengGameManagerMKII.inputRC.isInputCustomKeyDown(InputCode.custom_flare1))
                {
                    
                        int num = FengGameManagerMKII.instance.allheroes.Count;
                        if (num > 0)
                        {

                            this.currentPeekPlayerIndex = currentPeekPlayerIndex + 1;

                            if (this.currentPeekPlayerIndex >= num)
                            {
                                this.currentPeekPlayerIndex = 0;
                            }
                            this.setMainObject(FengGameManagerMKII.instance.allheroes[this.currentPeekPlayerIndex], true, false);

                            this.setSpectorMode(false);
                            this.lockAngle = false;
                        }
                  
                }
                if (FengGameManagerMKII.inputRC.isInputCustomKeyDown(InputCode.custom_flare2))
                {
                    int num2 = FengGameManagerMKII.instance.allheroes.Count;
                    if (num2 > 0)
                    {
                        this.currentPeekPlayerIndex = currentPeekPlayerIndex - 1;
                        if (this.currentPeekPlayerIndex < 0)
                        {
                            this.currentPeekPlayerIndex = num2 - 1;
                        }
                        this.setMainObject(FengGameManagerMKII.instance.allheroes[this.currentPeekPlayerIndex], true, false);
                        this.setSpectorMode(false);
                        this.lockAngle = false;
                    }
                }
                if (this.spectatorMode)
                {
                    return;
                }
            }
            if (FengGameManagerMKII.inputRC.isInputCustomKeyDown(InputCode.custom_pause))
            {
                if (isPausing)
                {
                    if (this.main_object != null)
                    {
                        Vector3 position = this.baseT.position;
                        position = (this.head == null) ? this.main_objectT.position : this.head.transform.position;
                        position += (Vector3)(Vector3.up * this.heightMulti);
                        this.baseT.position = Vector3.Lerp(this.baseT.position, position - ((Vector3)(this.baseT.forward * 5f)), 0.2f);
                    }
                    return;
                }
                isPausing = !isPausing;
                if (isPausing)
                {
                    if (gametype == GAMETYPE.SINGLE)
                    {
                        Time.timeScale = 0f;
                    }
                    FengGameManagerMKII.instance.MenuOn = true;
                    Screen.showCursor = true;
                    Screen.lockCursor = false;
                }
            }
            if (this.needSetHUD)
            {
                this.needSetHUD = false;
                this.setHUDposition();
                Screen.lockCursor = !Screen.lockCursor;
                Screen.lockCursor = !Screen.lockCursor;
            }
            if (FengGameManagerMKII.inputRC.isInputCustomKeyDown(InputCode.custom_fullscreen))
            {
                Screen.fullScreen = !Screen.fullScreen;
                if (Screen.fullScreen)
                {
                    Screen.SetResolution(960, 600, false);
                }
                else
                {
                    Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
                }
                this.needSetHUD = true;
                Minimap.OnScreenResolutionChanged();
            }
            if (FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_restart))
            {
                this.reset();
            }
            if (FengGameManagerMKII.inputRC.isInputCustomKeyDown(InputCode.custom_hideCursor))
            {
                Screen.showCursor = !Screen.showCursor;
            }
            if (this.main_object != null)
            {
                RaycastHit hit;
                if (FengGameManagerMKII.inputRC.isInputCustomKeyDown(InputCode.custom_camera))
                {
                    cameraMode = this.chageCamera();
                    this.verticalRotationOffset = 0f;
                    if ((((int)FengGameManagerMKII.settings[0xf5]) == 1) || (this.main_object.GetComponent<HERO>() == null))
                    {
                        Screen.showCursor = false;
                    }
                }
         
                if (FengGameManagerMKII.inputRC.isInputCustomKeyDown(InputCode.custom_focus))
                {
                    triggerAutoLock = !triggerAutoLock;
                    if (triggerAutoLock)
                    {
                        this.lockTarget = this.findNearestTitan();
                        if (this.closestDistance >= 150f)
                        {
                            this.lockTarget = null;
                            triggerAutoLock = false;
                        }
                    }
                }
                if (this.gameOver && (this.main_object != null))
                {
                    if (this.main_objectT == null)
                    {
                        this.main_objectT = this.main_object.transform;
                    }
                    if (FengGameManagerMKII.inputRC.isInputHumanDown(InputCodeRC.liveCam))
                    {
                        if (((int)FengGameManagerMKII.settings[0x107]) == 0)
                        {
                            FengGameManagerMKII.settings[0x107] = 1;
                        }
                        else
                        {
                            FengGameManagerMKII.settings[0x107] = 0;
                        }
                    }

                    if (((main_objectH != null) && (((int)FengGameManagerMKII.settings[0x107]) == 1)) && (main_objectH.smoothSyncMovement.enabled && main_objectH.isPhotonCamera))
                    {
                        this.CameraMovementLive(main_objectH);
                    }
                    else if (this.lockAngle)
                    {
                        this.baseT.rotation = Quaternion.Lerp(this.baseT.rotation, this.main_objectT.rotation, 0.2f);
                        this.baseT.position = Vector3.Lerp(this.baseT.position, this.main_objectT.position - ((Vector3)(this.main_objectT.forward * 5f)), 0.2f);
                    }
                    else
                    {
                        this.camareMovement();
                    }
                }
                else
                {
                    this.camareMovement();
                }
                if (triggerAutoLock && (this.lockTarget != null))
                {
                    float z = this.baseT.eulerAngles.z;
                    Transform transform = this.lockTarget.transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck");
                    Vector3 vector2 = transform.position - ((this.head == null) ? this.main_objectT.position : this.head.transform.position);
                    vector2.Normalize();
                    this.lockCameraPosition = (this.head == null) ? this.main_objectT.position : this.head.transform.position;
                    this.lockCameraPosition -= (Vector3)(((vector2 * this.distance) * this.distanceMulti) * this.distanceOffsetMulti);
                    this.lockCameraPosition += (Vector3)(((Vector3.up * 3f) * this.heightMulti) * this.distanceOffsetMulti);
                    this.baseT.position = Vector3.Lerp(this.baseT.position, this.lockCameraPosition, Time.deltaTime * 4f);
                    if (this.head != null)
                    {
                        this.baseT.LookAt((Vector3)((this.head.transform.position * 0.8f) + (transform.position * 0.2f)));
                    }
                    else
                    {
                        this.baseT.LookAt((Vector3)((this.main_objectT.position * 0.8f) + (transform.position * 0.2f)));
                    }
                    this.baseT.localEulerAngles = new Vector3(this.baseT.eulerAngles.x, this.baseT.eulerAngles.y, z);
                    Vector2 vector3 = base.camera.WorldToScreenPoint(transform.position - ((Vector3)(transform.forward * this.lockTarget.transform.localScale.x)));
                    this.locker.transform.localPosition = new Vector3(vector3.x - (Screen.width * 0.5f), vector3.y - (Screen.height * 0.5f), 0f);
                    if ((this.lockTarget.GetComponent<TITAN>() != null) && this.lockTarget.GetComponent<TITAN>().hasDie)
                    {
                        this.lockTarget = null;
                    }
                }
                else
                {
                    this.locker.transform.localPosition = new Vector3(0f, (-Screen.height * 0.5f) - 50f, 0f);
                }
                Vector3 end = (this.head == null) ? this.main_objectT.position : this.head.transform.position;
                Vector3 vector5 = ((this.head == null) ? this.main_objectT.position : this.head.transform.position) - this.baseT.position;
                Vector3 normalized = vector5.normalized;
                end -= (Vector3)((this.distance * normalized) * this.distanceMulti);
                LayerMask mask = ((int)1) << LayerMask.NameToLayer("Ground");
                LayerMask mask2 = ((int)1) << LayerMask.NameToLayer("EnemyBox");
                LayerMask mask3 = mask | mask2;
                if (this.head != null)
                {
                    if (Physics.Linecast(this.head.transform.position, end, out hit, (int)mask))
                    {
                        this.baseT.position = hit.point;
                    }
                    else if (Physics.Linecast(this.head.transform.position - ((Vector3)((normalized * this.distanceMulti) * 3f)), end, out hit, (int)mask2))
                    {
                        this.baseT.position = hit.point;
                    }
                    Debug.DrawLine(this.head.transform.position - ((Vector3)((normalized * this.distanceMulti) * 3f)), end, Color.red);
                }
                else if (Physics.Linecast(this.main_objectT.position + Vector3.up, end, out hit, (int)mask3))
                {
                    this.baseT.position = hit.point;
                }
                this.shakeUpdate();
            }
        }
    }

    public enum RotationAxes
    {
        MouseXAndY,
        MouseX,
        MouseY
    }
}
