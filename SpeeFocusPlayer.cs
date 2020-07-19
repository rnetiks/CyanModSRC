using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SpeeFocusPlayer : UnityEngine.MonoBehaviour
{
   public static bool isShow;
    public static SpeeFocusPlayer instance;
    HERO current_Hero;
    Vector2 scroolPos;
    void Start()
    {
        instance = this;
        isShow = false;
    }
    void OnDestroy()
    {
        isShow = false;
    }
    void OnDisable()
    {
        isShow = false;
    }
    void OnGUI()
    {
        if (isShow)
        {
            GUIStyle style = new GUIStyle(GUI.skin.button);
            style.alignment = TextAnchor.MiddleLeft;

            GUILayout.BeginArea(new Rect(Screen.width - 200, 30, 200, 400),GUI.skin.box);
            scroolPos = GUILayout.BeginScrollView(scroolPos);
            foreach (PhotonPlayer player in PhotonNetwork.playerList)
            {
                string str = player.id + " " + player.ishexname;
                if (player.dead)
                {
                    str = "<color=red>X</color>" + str;
                }
                if (GUILayout.Button(str, style))
                {
                    FocusedPlayer(player);
                }
            }
            GUILayout.EndScrollView();
            string str4 = "null";
            if (current_Hero != null)
            {
                str4 = "POS:" + current_Hero.transform.position.ToString();
                str4 = str4 +  "\nSPD:" + current_Hero.transform.rigidbody.velocity.magnitude.ToString("F");
            }
            GUILayout.Label(str4);
            GUILayout.EndArea();
        }
    }
    void FocusedPlayer(PhotonPlayer player)
    {
        foreach (HERO hero in FengGameManagerMKII.instance.heroes)
        {
            if (player.ID == hero.id)
            {
                current_Hero = hero;
                IN_GAME_MAIN_CAMERA.instance.setMainObject(hero.gameObject, true, false);
                IN_GAME_MAIN_CAMERA.instance.setSpectorMode(false);
                return;
            }
        }
    }
    public void Disable()
    {
        isShow = false;
        FocusedPlayer(PhotonNetwork.player);
        if (FengGameManagerMKII.instance.MenuOn)
        {
            Screen.showCursor = true;
        }
        else
        {
            Screen.showCursor = false;
        }
    }
    public void Enable()
    {
        if (!FengGameManagerMKII.instance.MenuOn)
        {
            FocusedPlayer(PhotonNetwork.player);
            isShow = true;
            Screen.lockCursor = false;
            Screen.showCursor = true;
            IN_GAME_MAIN_CAMERA.cameraMode = CAMERA_TYPE.TPS;
        }
    }
    void Update()
    {
        if (isShow)
        {
            if (FengGameManagerMKII.instance.MenuOn || RCSettings.globalDisableMinimap == 1)
            {
                Disable();
            }
        }
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
        {
            if (FengGameManagerMKII.inputRC.isInputCyanKeyDown(InputCode.Focus_player))
            {
                if (isShow)
                {
                    Disable();
                }
                else
                {
                    Enable();
                }
            }
        }
    }
}

