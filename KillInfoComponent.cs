using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class KillInfoComponent : MonoBehaviour
{
    private float alpha = 1f;
    private GameObject baseG;
    private Transform baseT;
    private int col;
    public GameObject groupBig;
    public GameObject groupSmall;
    public GameObject labelNameLeft;
    public GameObject labelNameRight;
    public GameObject labelScore;
    public GameObject leftTitan;
    private float lifeTime = 8f;
    private UISprite LTitanUISprite;
    private float maxScale = 1.5f;
    private UILabel NameLeftUILabel;
    private UILabel NameRightUILabel;
    private int offset = 0x18;
    public GameObject rightTitan;
    private UISprite RTitanUISprite;
    private UILabel ScoreUILabel;
    private UISprite SkeletonUISprite;
    public GameObject slabelNameLeft;
    public GameObject slabelNameRight;
    public GameObject slabelScore;
    public GameObject sleftTitan;
    private UISprite sLTitanUISprite;
    private UILabel sNameLeftUILabel;
    private UILabel sNameRightUILabel;
    public GameObject spriteSkeleton;
    public GameObject spriteSword;
    public GameObject srightTitan;
    private UISprite sRTitanUISprite;
    private UILabel sScoreUILabel;
    private UISprite sSkeletonUISprite;
    public GameObject sspriteSkeleton;
    public GameObject sspriteSword;
    private UISprite sSwordUISprite;
    private bool start;
    private UISprite SwordUISprite;
    private float timeElapsed;

    private void Awake()
    {
        this.baseT = base.transform;
        this.baseG = base.gameObject;
        this.NameLeftUILabel = this.labelNameLeft.GetComponent<UILabel>();
        this.NameRightUILabel = this.labelNameRight.GetComponent<UILabel>();
        this.ScoreUILabel = this.labelScore.GetComponent<UILabel>();
        this.LTitanUISprite = this.leftTitan.GetComponent<UISprite>();
        this.RTitanUISprite = this.rightTitan.GetComponent<UISprite>();
        this.SkeletonUISprite = this.spriteSkeleton.GetComponent<UISprite>();
        this.SwordUISprite = this.spriteSword.GetComponent<UISprite>();
        this.sNameLeftUILabel = this.slabelNameLeft.GetComponent<UILabel>();
        this.sNameRightUILabel = this.slabelNameRight.GetComponent<UILabel>();
        this.sScoreUILabel = this.slabelScore.GetComponent<UILabel>();
        this.sLTitanUISprite = this.sleftTitan.GetComponent<UISprite>();
        this.sRTitanUISprite = this.srightTitan.GetComponent<UISprite>();
        this.sSkeletonUISprite = this.sspriteSkeleton.GetComponent<UISprite>();
        this.sSwordUISprite = this.sspriteSword.GetComponent<UISprite>();
    }

    public void destory()
    {
        this.timeElapsed = this.lifeTime;
    }

    protected internal void moveOn()
    {
        this.col++;
        if (this.col > 4)
        {
            this.timeElapsed = this.lifeTime;
        }
        this.groupBig.SetActive(false);
        this.groupSmall.SetActive(true);
    }

    private void setAlpha(float alpha)
    {
        if (this.groupBig.activeInHierarchy)
        {
            this.ScoreUILabel.color = new Color(this.ScoreUILabel.color.r, this.ScoreUILabel.color.g, this.ScoreUILabel.color.b, alpha);
            this.LTitanUISprite.color = new Color(1f, 1f, 1f, alpha);
            this.RTitanUISprite.color = new Color(1f, 1f, 1f, alpha);
            this.NameLeftUILabel.color = new Color(1f, 1f, 1f, alpha);
            this.NameRightUILabel.color = new Color(1f, 1f, 1f, alpha);
            this.SkeletonUISprite.color = new Color(1f, 1f, 1f, alpha);
            this.SwordUISprite.color = new Color(1f, 1f, 1f, alpha);
        }
        if (this.groupSmall.activeInHierarchy)
        {
            this.sScoreUILabel.color = new Color(this.ScoreUILabel.color.r, this.ScoreUILabel.color.g, this.ScoreUILabel.color.b, alpha);
            this.sLTitanUISprite.color = new Color(1f, 1f, 1f, alpha);
            this.sRTitanUISprite.color = new Color(1f, 1f, 1f, alpha);
            this.sNameLeftUILabel.color = new Color(1f, 1f, 1f, alpha);
            this.sNameRightUILabel.color = new Color(1f, 1f, 1f, alpha);
            this.sSkeletonUISprite.color = new Color(1f, 1f, 1f, alpha);
            this.sSwordUISprite.color = new Color(1f, 1f, 1f, alpha);
        }
    }

    protected internal void show(bool isTitan1, string name1, bool isTitan2, string name2, int dmg = 0)
    {
        this.groupBig.SetActive(true);
        this.groupSmall.SetActive(true);
        if (!isTitan1)
        {
            this.leftTitan.SetActive(false);
            this.spriteSkeleton.SetActive(false);
            this.sleftTitan.SetActive(false);
            this.sspriteSkeleton.SetActive(false);
            Transform transform = this.labelNameLeft.transform;
            transform.position += new Vector3(18f, 0f, 0f);
            Transform transform2 = this.slabelNameLeft.transform;
            transform2.position += new Vector3(16f, 0f, 0f);
        }
        else
        {
            this.spriteSword.SetActive(false);
            this.sspriteSword.SetActive(false);
            Transform transform3 = this.labelNameRight.transform;
            transform3.position -= new Vector3(18f, 0f, 0f);
            Transform transform4 = this.slabelNameRight.transform;
            transform4.position -= new Vector3(16f, 0f, 0f);
        }
        if (!isTitan2)
        {
            this.rightTitan.SetActive(false);
            this.srightTitan.SetActive(false);
        }
        this.NameLeftUILabel.text = name1;
        this.NameRightUILabel.text = name2;
        this.sNameLeftUILabel.text = name1;
        this.sNameRightUILabel.text = name2;
        if (dmg == 0)
        {
            this.ScoreUILabel.text = string.Empty;
            this.sScoreUILabel.text = string.Empty;
        }
        else
        {
            this.ScoreUILabel.text = dmg.ToString();
            this.sScoreUILabel.text = dmg.ToString();
            if (dmg > 0x3e8)
            {
                this.ScoreUILabel.color = Color.red;
                this.sScoreUILabel.color = Color.red;
            }
        }
        this.groupSmall.SetActive(false);
    }

    private void Start()
    {
        this.start = true;
        this.baseT.localScale = new Vector3(0.85f, 0.85f, 0.85f);
        this.baseT.localPosition = new Vector3(0f, -100f + (Screen.height * 0.5f), 0f);
    }

    private void Update()
    {
        if (this.start)
        {
            this.timeElapsed += Time.deltaTime;
            if (this.timeElapsed < 0.2f)
            {
                this.baseT.localScale = Vector3.Lerp(this.baseT.localScale, (Vector3)(Vector3.one * this.maxScale), Time.deltaTime * 10f);
            }
            else if (this.timeElapsed < 1f)
            {
                this.baseT.localScale = Vector3.Lerp(this.baseT.localScale, Vector3.one, Time.deltaTime * 10f);
            }
            if (this.timeElapsed > this.lifeTime)
            {
                this.baseT.position += new Vector3(0f, Time.deltaTime * 0.15f, 0f);
                this.alpha = ((1f - (Time.deltaTime * 45f)) + this.lifeTime) - this.timeElapsed;
                this.setAlpha(this.alpha);
            }
            else
            {
                float num = ((int)(100f - (Screen.height * 0.5f))) + (this.col * this.offset);
                this.baseT.localPosition = Vector3.Lerp(this.baseT.localPosition, new Vector3(0f, -num, 0f), Time.deltaTime * 10f);
            }
            if (this.timeElapsed > (this.lifeTime + 0.5f))
            {
                UnityEngine.Object.Destroy(this.baseG);
            }
        }
    }
}

