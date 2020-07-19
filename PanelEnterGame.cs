using System;
using UnityEngine;

public class PanelEnterGame : MonoBehaviour
{
    public GameObject label_camera_info;
    public GameObject label_camera_type;
    public GameObject label_human;
    public GameObject label_select_character;
    public GameObject label_select_titan;
    public GameObject label_titan;
    private int lang = -1;
    public static PanelEnterGame instance;
    void Awake()
    {
        instance = this;
    }
    void OnDestroy()
    {
        instance = null;
    }
    private void OnEnable()
    {
    }
    public void to_InfoHero(HeroStat state)
    {
        foreach (UILabel label in base.gameObject.GetComponentsInChildren<UILabel>())
        {
            if (label.text.ToLower().Contains("spd") && label.text.ToLower().Contains("acl") && label.text.ToLower().Contains("gas"))
            {
                string str = "SPD\nGAS\nBLA\nACL\n\nspd:" + state.SPD + " gas:" + state.GAS + " bla:" + state.BLA + " acl:" + state.ACL + "\n";
                str = str + "name:" + state.name + " skill:" + state.skillId;
                label.text = str;
            }
        }
    }
    public void showTxt()
    {
        
    }
    private void Update()
    {
        this.showTxt();
    }
}

