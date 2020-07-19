using Photon;
using System;
using System.Collections;
using UnityEngine;

public class PVPcheckPoint : Photon.MonoBehaviour
{
    private bool annie;
    public GameObject[] chkPtNextArr;
    public GameObject[] chkPtPreviousArr;
    public static ArrayList chkPts;
    private float getPtsInterval = 20f;
    private float getPtsTimer;
    public bool hasAnnie;
    private float hitTestR = 15f;
    public GameObject humanCyc;
    public float humanPt;
    public float humanPtMax = 40f;
    public int id;
    public bool isBase;
    public int normalTitanRate = 70;
    private bool playerOn;
    public float size = 1f;
    private float spawnTitanTimer;
    public CheckPointState state;
    private GameObject supply;
    private float syncInterval = 0.6f;
    private float syncTimer;
    public GameObject titanCyc;
    public float titanInterval = 30f;
    private bool titanOn;
    public float titanPt;
    public float titanPtMax = 40f;

    [RPC]
    private void changeHumanPt(float pt)
    {
        this.humanPt = pt;
    }

    [RPC]
    private void changeState(int num)
    {
        if (num == 0)
        {
            this.state = CheckPointState.Non;
        }
        if (num == 1)
        {
            this.state = CheckPointState.Human;
        }
        if (num == 2)
        {
            this.state = CheckPointState.Titan;
        }
    }

    [RPC]
    private void changeTitanPt(float pt)
    {
        this.titanPt = pt;
    }

    private void checkIfBeingCapture()
    {
        this.playerOn = false;
        this.titanOn = false;

        foreach (GameObject hero in FengGameManagerMKII.instance.allheroes)
        {
            if (Vector3.Distance(hero.transform.position, base.transform.position) < this.hitTestR)
            {
                this.playerOn = true;
                if ((this.state == CheckPointState.Human) && hero.GetPhotonView().isMine)
                {
                    if (FengGameManagerMKII.instance.checkpoint != base.gameObject)
                    {
                        FengGameManagerMKII.instance.checkpoint = base.gameObject;
                        InRoomChat.instance.addLINE(CyanMod.INC.la("checkpoint") + this.id);
                    }
                    break;
                }
            }
        }
        foreach (TITAN titan in FengGameManagerMKII.instance.titans)
        {
            if ((Vector3.Distance(titan.transform.position, base.transform.position) < (this.hitTestR + 5f)) && ((titan == null) || !titan.hasDie))
            {
                this.titanOn = true;
                if (((this.state == CheckPointState.Titan) && titan.photonView.isMine) && ((titan != null) && titan.nonAI))
                {
                    if (FengGameManagerMKII.instance.checkpoint != base.gameObject)
                        FengGameManagerMKII.instance.checkpoint = base.gameObject;
                    InRoomChat.instance.addLINE(CyanMod.INC.la("checkpoint") + this.id);
                    return;
                }
                break;

            }
            }
        }


    private bool checkIfHumanWins()
    {
        for (int i = 0; i < chkPts.Count; i++)
        {
            if ((chkPts[i] as PVPcheckPoint).state != CheckPointState.Human)
            {
                return false;
            }
        }
        return true;
    }

    private bool checkIfTitanWins()
    {
        for (int i = 0; i < chkPts.Count; i++)
        {
            if ((chkPts[i] as PVPcheckPoint).state != CheckPointState.Titan)
            {
                return false;
            }
        }
        return true;
    }

    private float getHeight(Vector3 pt)
    {
        RaycastHit hit;
        LayerMask mask = ((int) 1) << LayerMask.NameToLayer("Ground");
        if (Physics.Raycast(pt, -Vector3.up, out hit, 1000f, mask.value))
        {
            return hit.point.y;
        }
        return 0f;
    }

    public string getStateString()
    {
        if (this.state == CheckPointState.Human)
        {
            return ("<color=#" + ColorSet.color_human + ">H</color>");
        }
        if (this.state == CheckPointState.Titan)
        {
            return ("<color=#" + ColorSet.color_titan_player + ">T</color>");
        }
        return ("<color=#" + ColorSet.color_D + ">_</color>");
    }

    private void humanGetsPoint()
    {
        if (this.humanPt >= this.humanPtMax)
        {
            this.humanPt = this.humanPtMax;
            this.titanPt = 0f;
            this.syncPts();
            this.state = CheckPointState.Human;
            object[] parameters = new object[] { 1 };
            base.photonView.RPC("changeState", PhotonTargets.All, parameters);
            if (FengGameManagerMKII.lvlInfo.mapName != "The City I")
            {
                this.supply = PhotonNetwork.Instantiate("aot_supply", base.transform.position - ((Vector3) (Vector3.up * (base.transform.position.y - this.getHeight(base.transform.position)))), base.transform.rotation, 0);
            }
            FengGameManagerMKII component = FengGameManagerMKII.instance;
            component.PVPhumanScore += 2;
            FengGameManagerMKII.instance.checkPVPpts();
            if (this.checkIfHumanWins())
            {
                FengGameManagerMKII.instance.gameWin2();
            }
        }
        else
        {
            this.humanPt += Time.deltaTime;
        }
    }

    private void humanLosePoint()
    {
        if (this.humanPt > 0f)
        {
            this.humanPt -= Time.deltaTime * 3f;
            if (this.humanPt <= 0f)
            {
                this.humanPt = 0f;
                this.syncPts();
                if (this.state != CheckPointState.Titan)
                {
                    this.state = CheckPointState.Non;
                    object[] parameters = new object[] { 0 };
                    base.photonView.RPC("changeState", PhotonTargets.Others, parameters);
                }
            }
        }
    }

    private void newTitan()
    {
        TITAN titan = FengGameManagerMKII.instance.spawnTitan(this.normalTitanRate, base.transform.position - ((Vector3) (Vector3.up * (base.transform.position.y - this.getHeight(base.transform.position)))), base.transform.rotation, false);
        if (FengGameManagerMKII.lvlInfo.mapName == "The City I")
        {
            titan.chaseDistance = 120f;
        }
        else
        {
            titan.chaseDistance = 200f;
        }
        titan.PVPfromCheckPt = this;
    }

    private void Start()
    {
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
        {
            UnityEngine.Object.Destroy(base.gameObject);
        }
        else if (IN_GAME_MAIN_CAMERA.gamemode != GAMEMODE.PVP_CAPTURE)
        {
            if (base.photonView.isMine)
            {
                UnityEngine.Object.Destroy(base.gameObject);
            }
            UnityEngine.Object.Destroy(base.gameObject);
        }
        else
        {
            chkPts.Add(this);
            IComparer comparer = new IComparerPVPchkPtID();
            chkPts.Sort(comparer);
            if (this.humanPt == this.humanPtMax)
            {
                this.state = CheckPointState.Human;
                if (base.photonView.isMine && (FengGameManagerMKII.lvlInfo.mapName != "The City I"))
                {
                    this.supply = PhotonNetwork.Instantiate("aot_supply", base.transform.position - ((Vector3) (Vector3.up * (base.transform.position.y - this.getHeight(base.transform.position)))), base.transform.rotation, 0);
                }
            }
            else if (base.photonView.isMine && !this.hasAnnie)
            {
                if (RCSettings.AnnieSurvive == 0)
                {
                    if (UnityEngine.Random.Range(0, 100) < 50)
                    {
                        int num = UnityEngine.Random.Range(1, 2);
                        for (int i = 0; i < num; i++)
                        {
                            this.newTitan();
                        }
                    }
                    if (this.isBase)
                    {
                        this.newTitan();
                    }
                }
            }
            if (this.titanPt == this.titanPtMax)
            {
                this.state = CheckPointState.Titan;
            }
            this.hitTestR = 15f * this.size;
            base.transform.localScale = new Vector3(this.size, this.size, this.size);
        }
    }

    private void syncPts()
    {
        object[] parameters = new object[] { this.titanPt };
        base.photonView.RPC("changeTitanPt", PhotonTargets.Others, parameters);
        object[] objArray2 = new object[] { this.humanPt };
        base.photonView.RPC("changeHumanPt", PhotonTargets.Others, objArray2);
    }

    private void titanGetsPoint()
    {
        if (this.titanPt >= this.titanPtMax)
        {
            this.titanPt = this.titanPtMax;
            this.humanPt = 0f;
            this.syncPts();
            if ((this.state == CheckPointState.Human) && (this.supply != null))
            {
                PhotonNetwork.Destroy(this.supply);
            }
            this.state = CheckPointState.Titan;
            object[] parameters = new object[] { 2 };
            base.photonView.RPC("changeState", PhotonTargets.All, parameters);
            FengGameManagerMKII component = FengGameManagerMKII.instance;
            component.PVPtitanScore += 2;
            FengGameManagerMKII.instance.checkPVPpts();
            if (this.checkIfTitanWins())
            {
                FengGameManagerMKII.instance.gameLose2();
            }
            if (RCSettings.AnnieSurvive == 0)
            {
                if (this.hasAnnie)
                {
                    if (!this.annie)
                    {
                        this.annie = true;
                        PhotonNetwork.Instantiate("FEMALE_TITAN", base.transform.position - ((Vector3)(Vector3.up * (base.transform.position.y - this.getHeight(base.transform.position)))), base.transform.rotation, 0);
                    }
                    else
                    {
                        this.newTitan();
                    }
                }
                else
                {
                    this.newTitan();
                }
            }
        }
        else
        {
            this.titanPt += Time.deltaTime;
        }
    }

    private void titanLosePoint()
    {
        if (this.titanPt > 0f)
        {
            this.titanPt -= Time.deltaTime * 3f;
            if (this.titanPt <= 0f)
            {
                this.titanPt = 0f;
                this.syncPts();
                if (this.state != CheckPointState.Human)
                {
                    this.state = CheckPointState.Non;
                    object[] parameters = new object[] { 0 };
                    base.photonView.RPC("changeState", PhotonTargets.All, parameters);
                }
            }
        }
    }

    private void Update()
    {
        float x = this.humanPt / this.humanPtMax;
        float num2 = this.titanPt / this.titanPtMax;
        if (!base.photonView.isMine)
        {
            x = this.humanPt / this.humanPtMax;
            num2 = this.titanPt / this.titanPtMax;
            this.humanCyc.transform.localScale = new Vector3(x, x, 1f);
            this.titanCyc.transform.localScale = new Vector3(num2, num2, 1f);
            this.syncTimer += Time.deltaTime;
            if (this.syncTimer > this.syncInterval)
            {
                this.syncTimer = 0f;
                this.checkIfBeingCapture();
            }
        }
        else
        {
            if (this.state == CheckPointState.Non)
            {
                if (this.playerOn && !this.titanOn)
                {
                    this.humanGetsPoint();
                    this.titanLosePoint();
                }
                else if (this.titanOn && !this.playerOn)
                {
                    this.titanGetsPoint();
                    this.humanLosePoint();
                }
                else
                {
                    this.humanLosePoint();
                    this.titanLosePoint();
                }
            }
            else if (this.state == CheckPointState.Human)
            {
                if (this.titanOn && !this.playerOn)
                {
                    this.titanGetsPoint();
                }
                else
                {
                    this.titanLosePoint();
                }
                this.getPtsTimer += Time.deltaTime;
                if (this.getPtsTimer > this.getPtsInterval)
                {
                    this.getPtsTimer = 0f;
                    if (!this.isBase)
                    {
                        FengGameManagerMKII component = FengGameManagerMKII.instance;
                        component.PVPhumanScore++;
                    }
                    FengGameManagerMKII.instance.checkPVPpts();
                }
            }
            else if (this.state == CheckPointState.Titan)
            {
                if (this.playerOn && !this.titanOn)
                {
                    this.humanGetsPoint();
                }
                else
                {
                    this.humanLosePoint();
                }
                this.getPtsTimer += Time.deltaTime;
                if (this.getPtsTimer > this.getPtsInterval)
                {
                    this.getPtsTimer = 0f;
                    if (!this.isBase)
                    {
                        FengGameManagerMKII.instance.PVPtitanScore++;
                    }
                    FengGameManagerMKII.instance.checkPVPpts();
                }
                this.spawnTitanTimer += Time.deltaTime;
                if (this.spawnTitanTimer > this.titanInterval)
                {
                    this.spawnTitanTimer = 0f;
                    if (RCSettings.AnnieSurvive == 0)
                    {
                        if (FengGameManagerMKII.lvlInfo.mapName == "The City I")
                        {
                            if (FengGameManagerMKII.instance.alltitans.Count < 12)
                            {
                                this.newTitan();
                            }
                        }
                        else if (FengGameManagerMKII.instance.alltitans.Count < 20)
                        {
                            this.newTitan();
                        }
                    }
                }
            }
            this.syncTimer += Time.deltaTime;
            if (this.syncTimer > this.syncInterval)
            {
                this.syncTimer = 0f;
                this.checkIfBeingCapture();
                this.syncPts();
            }
            x = this.humanPt / this.humanPtMax;
            num2 = this.titanPt / this.titanPtMax;
            this.humanCyc.transform.localScale = new Vector3(x, x, 1f);
            this.titanCyc.transform.localScale = new Vector3(num2, num2, 1f);
        }
    }

    public GameObject chkPtNext
    {
        get
        {
            if (this.chkPtNextArr.Length <= 0)
            {
                return null;
            }
            return this.chkPtNextArr[UnityEngine.Random.Range(0, this.chkPtNextArr.Length)];
        }
    }

    public GameObject chkPtPrevious
    {
        get
        {
            if (this.chkPtPreviousArr.Length <= 0)
            {
                return null;
            }
            return this.chkPtPreviousArr[UnityEngine.Random.Range(0, this.chkPtPreviousArr.Length)];
        }
    }
}

