using System;
using UnityEngine;
using System.IO;
using System.Text;

public class PanelCredits : MonoBehaviour
{
    public GameObject label_back;
    public GameObject label_title;
    private int lang = -1;
    public static PanelCredits instance;
    void Awake()
    {
        instance = this;
    }
    void OnDestroy()
    {
        instance = null;
    }
    public void showTxt()
    {
       
            this.label_title.GetComponent<UILabel>().text =CyanMod.INC.la("btn_credits");
            this.label_back.GetComponent<UILabel>().text = CyanMod.INC.la("btn_back");
            UILabel[] gm = base.gameObject.GetComponentsInChildren<UILabel>();
            string sr = "";
            foreach (UILabel lab in gm)
            {
                sr = sr + lab.text + "\n\n";
                lab.color = CyanMod.INC.color_UI;
            }
            File.WriteAllText(Application.dataPath + "/sss.txt", sr, Encoding.UTF8);
        
    }
    private void Start()
    {
        this.showTxt();
    }
}

