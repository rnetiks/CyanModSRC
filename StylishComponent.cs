using System;
using UnityEngine;

public class StylishComponent : MonoBehaviour
{
    public GameObject bar;
    private int chainKillRank;
    private float[] chainRankMultiplier = new float[] { 1f, 1.1f, 1.2f, 1.3f, 1.5f, 1.7f, 2f, 2.3f, 2.5f };
    private float chainTime;
    private float duration;
    private Vector3 exitPosition;
    private bool flip;
    private bool hasLostRank;
    public GameObject labelChain;
    public GameObject labelHits;
    public GameObject labelS;
    public GameObject labelS1;
    public GameObject labelS2;
    public GameObject labelsub;
    public GameObject labelTotal;
    private Vector3 originalPosition;
    private float R;
    private int styleHits;
    private float stylePoints;
    private int styleRank;
    private int[] styleRankDepletions = new int[] { 1, 2, 5, 10, 15, 20, 0x19, 0x19 };
    private int[] styleRankPoints = new int[] { 350, 950, 0x992, 0x11c6, 0x1b58, 0x3a98, 0x186a0 };
    private string[,] styleRankText = new string[,] { { "D", "eja Vu" }, { "C", "asual" }, { "B", "oppin!" }, { "A", "mazing!" }, { "S", "ensational!" }, { "S", "pectacular!!" }, { "S", "tylish!!!" }, { "X", "TREEME!!!" } };
    private int styleTotalDamage;
     UILabel labelChainT;
     UILabel labelHitsT;
     UILabel labelST;
     UILabel labelS1T;
     UILabel labelS2T;
     UILabel labelsubT;
     UILabel labelTotalT;
     UISprite barT;
    Transform baseT;
    GameObject baseG;
    public static StylishComponent instance;

    void Awake()
    {
        instance = this;
        baseT = base.transform;
        baseG = base.gameObject;
    }
    void OnDestroy()
    {
        instance = null;
    }
    private int GetRankPercentage()
    {
        if ((this.styleRank > 0) && (this.styleRank < this.styleRankPoints.Length))
        {
            return (int) (((this.stylePoints - this.styleRankPoints[this.styleRank - 1]) * 100f) / ((float) (this.styleRankPoints[this.styleRank] - this.styleRankPoints[this.styleRank - 1])));
        }
        if (this.styleRank == 0)
        {
            return (((int) (this.stylePoints * 100f)) / this.styleRankPoints[this.styleRank]);
        }
        return 100;
    }

    private int GetStyleDepletionRate()
    {
        return this.styleRankDepletions[this.styleRank];
    }

    public void reset()
    {
        this.styleTotalDamage = 0;
        this.chainKillRank = 0;
        this.chainTime = 0f;
        this.styleRank = 0;
        this.stylePoints = 0f;
        this.styleHits = 0;
    }

    private void setPosition()
    {
        this.originalPosition = new Vector3((float) ((int) ((Screen.width * 0.5f) - 2f)), (float) ((int) ((Screen.height * 0.5f) - 200f)), 0f);
        this.exitPosition = new Vector3((float) Screen.width, this.originalPosition.y, this.originalPosition.z);
    }

    private void SetRank()
    {
        int styleRank = this.styleRank;
        int index = 0;
        while (index < this.styleRankPoints.Length)
        {
            if (this.stylePoints <= this.styleRankPoints[index])
            {
                break;
            }
            index++;
        }
        if (index < this.styleRankPoints.Length)
        {
            this.styleRank = index;
        }
        else
        {
            this.styleRank = this.styleRankPoints.Length;
        }
        if (this.styleRank < styleRank)
        {
            if (this.hasLostRank)
            {
                this.stylePoints = 0f;
                this.styleHits = 0;
                this.styleTotalDamage = 0;
                this.styleRank = 0;
            }
            else
            {
                this.hasLostRank = true;
            }
        }
        else if (this.styleRank > styleRank)
        {
            this.hasLostRank = false;
        }
    }

    private void setRankText()
    {
        labelST.text = this.styleRankText[this.styleRank, 0];
        if (this.styleRank == 5)
        {
            labelS2T.text = "[" + ColorSet.color_SS + "]S";
        }
        else
        {
            labelS2T.text = string.Empty;
        }
        if (this.styleRank == 6)
        {
            labelS2T.text = "[" + ColorSet.color_SSS + "]S";
            labelS1T.text = "[" + ColorSet.color_SSS + "]S";
        }
        else
        {
            labelS1T.text = string.Empty;
        }
        if (this.styleRank == 0)
        {
            labelST.text = "[" + ColorSet.color_D + "]D";
        }
        if (this.styleRank == 1)
        {
            labelST.text = "[" + ColorSet.color_C + "]C";
        }
        if (this.styleRank == 2)
        {
            labelST.text = "[" + ColorSet.color_B + "]B";
        }
        if (this.styleRank == 3)
        {
            labelST.text = "[" + ColorSet.color_A + "]A";
        }
        if (this.styleRank == 4)
        {
            labelST.text = "[" + ColorSet.color_S + "]S";
        }
        if (this.styleRank == 5)
        {
            labelST.text = "[" + ColorSet.color_SS + "]S";
        }
        if (this.styleRank == 6)
        {
            labelST.text = "[" + ColorSet.color_SSS + "]S";
        }
        if (this.styleRank == 7)
        {
            labelST.text = "[" + ColorSet.color_X + "]X";
        }
        labelsubT.text = this.styleRankText[this.styleRank, 1];
    }

    private void shakeUpdate()
    {
        if (this.duration > 0f)
        {
            this.duration -= Time.deltaTime;
            if (this.flip)
            {
                baseG.transform.localPosition = this.originalPosition + ((Vector3) (Vector3.up * this.R));
            }
            else
            {
                baseG.transform.localPosition = this.originalPosition - ((Vector3) (Vector3.up * this.R));
            }
            this.flip = !this.flip;
            if (this.duration <= 0f)
            {
                baseG.transform.localPosition = this.originalPosition;
            }
        }
    }

    private void Start()
    {
        instance = this;
        this.setPosition();
        baseT.localPosition = this.exitPosition;
        labelChainT = labelChain.GetComponent<UILabel>();
        labelHitsT = labelHits.GetComponent<UILabel>();
        labelST = labelS.GetComponent<UILabel>();
        labelS1T = labelS1.GetComponent<UILabel>();
        labelS2T = labelS2.GetComponent<UILabel>();
        labelsubT = labelsub.GetComponent<UILabel>();
        labelTotalT = labelTotal.GetComponent<UILabel>();
        barT = bar.GetComponent<UISprite>();
    }

    public void startShake(int R, float duration)
    {
        if (this.duration < duration)
        {
            this.R = R;
            this.duration = duration;
        }
    }

    public void Style(int damage)
    {
        if (damage != -1)
        {
            this.stylePoints += (int) ((damage + 200) * this.chainRankMultiplier[this.chainKillRank]);
            this.styleTotalDamage += damage;
            this.chainKillRank = (this.chainKillRank >= (this.chainRankMultiplier.Length - 1)) ? this.chainKillRank : (this.chainKillRank + 1);
            this.chainTime = 5f;
            this.styleHits++;
            this.SetRank();
        }
        else if (this.stylePoints == 0f)
        {
            this.stylePoints++;
            this.SetRank();
        }
        this.startShake(5, 0.3f);
        this.setPosition();
        labelTotalT.text = ((int)this.stylePoints).ToString();
        labelHitsT.text = this.styleHits + ((this.styleHits <= 1) ? "Hit" : "Hits");
        if (this.chainKillRank == 0)
        {
            labelChainT.text = string.Empty;
        }
        else
        {
            labelChainT.text = "x" + this.chainRankMultiplier[this.chainKillRank] + "!";
        }
    }

    private void Update()
    {
        if (!IN_GAME_MAIN_CAMERA.isPausing)
        {
            if (this.stylePoints > 0f)
            {
                this.setRankText();
                barT.fillAmount = this.GetRankPercentage() * 0.01f;
                this.stylePoints -= (this.GetStyleDepletionRate() * Time.deltaTime) * 10f;
                this.SetRank();
            }
            else
            {
                baseT.localPosition = Vector3.Lerp(baseT.localPosition, this.exitPosition, Time.deltaTime * 3f);
            }
            if (this.chainTime > 0f)
            {
                this.chainTime -= Time.deltaTime;
            }
            else
            {
                this.chainTime = 0f;
                this.chainKillRank = 0;
            }
            this.shakeUpdate();
        }
    }
}

