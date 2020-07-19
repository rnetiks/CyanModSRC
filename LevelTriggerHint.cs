using System;
using UnityEngine;

public class LevelTriggerHint : MonoBehaviour
{
    public string content;
    public HintType myhint;
    private bool on;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.on = true;
        }
    }

    private void Start()
    {
        if (!FengGameManagerMKII.lvlInfo.hint)
        {
            base.enabled = false;
        }
        if (this.content == string.Empty)
        {
            switch (this.myhint)
            {
                case HintType.MOVE:
                {
                    string[] strArray = new string[] { "Hello soldier!\nWelcome to Attack On Titan Tribute Game!\n Press <color=#00FF00>", (string)FengGameManagerMKII.settings[442], (string)FengGameManagerMKII.settings[434], (string)FengGameManagerMKII.settings[425], (string)FengGameManagerMKII.settings[439], "</color> to Move." };
                    this.content = string.Concat(strArray);
                    return;
                }
                case HintType.TELE:
                this.content = "Move to <color=#00FF00>green warp point</color> to proceed.";
                    return;

                case HintType.CAMA:
                {
                    string[] strArray2 = new string[] { "Press <color=#00FF00>", (string)FengGameManagerMKII.settings[423], "</color> to change camera mode\nPress <color=#00FF00>", (string)FengGameManagerMKII.settings[432], "</color> to hide or show the cursor." };
                    this.content = string.Concat(strArray2);
                    return;
                }
                case HintType.JUMP:
                this.content = "Press <color=#00FF00>" + (string)FengGameManagerMKII.settings[433] + "</color> to Jump.";
                    return;

                case HintType.JUMP2:
                    this.content = "Press <color=#00FF00>" + (string)FengGameManagerMKII.settings[442] + "</color> towards a wall to perform a wall-run.";
                    return;

                case HintType.HOOK:
                {
                    string[] strArray3 = new string[] { "Press and Hold<color=#00FF00> ", (string)FengGameManagerMKII.settings[435], "</color> or <color=#00FF00>", (string)FengGameManagerMKII.settings[440], "</color> to launch your grapple.\nNow Try hooking to the [>3<] box. " };
                    this.content = string.Concat(strArray3);
                    return;
                }
                case HintType.HOOK2:
                {
                    string[] strArray4 = new string[] { "Press and Hold<color=#00FF00> ", (string)FengGameManagerMKII.settings[422], "</color> to launch both of your grapples at the same Time.\n\nNow aim between the two black blocks. \nYou will see the mark '<' and '>' appearing on the blocks. \nThen press ", (string)FengGameManagerMKII.settings[422], " to hook the blocks." };
                    this.content = string.Concat(strArray4);
                    return;
                }
                case HintType.SUPPLY:
                this.content = "Press <color=#00FF00>" + (string)FengGameManagerMKII.settings[437] + "</color> to reload your blades.\n Move to the supply station to refill your gas and blades.";
                    return;

                case HintType.DODGE:
                    this.content = "Press <color=#00FF00>" + (string)FengGameManagerMKII.settings[424] + "</color> to Dodge.";
                    return;

                case HintType.ATTACK:
                {
                    string[] strArray5 = new string[] { "Press <color=#00FF00>", (string)FengGameManagerMKII.settings[420], "</color> to Attack. \nPress <color=#00FF00>", (string)FengGameManagerMKII.settings[421], "</color> to use special attack.\n***You can only kill a titan by slashing his <color=#00FF00>NAPE</color>.***\n\n" };
                    this.content = string.Concat(strArray5);
                    break;
                }
                default:
                    return;
            }
        }
    }

    private void Update()
    {
        if (this.on)
        {
            FengGameManagerMKII.instance.ShowHUDInfoCenter(this.content + "\n\n\n\n\n");
            this.on = false;
        }
    }
}

