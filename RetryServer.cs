using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using CyanMod;
using System.Collections;

public class RetryServer : UnityEngine.MonoBehaviour
{
    int id_server;
    string name_server;
    string map_server;
    string diff_server;
    string day_server;
    string password;
    string info_s = "";
    void Start()
    {
        info_s = "Reconnect...";
        RoomInfo info = PhotonNetwork.room;
        id_server = info.IDRoom;
        name_server = info.RoomName;
        map_server = info.MapName;
        diff_server = info.Difficulty;
        day_server = info.DayTime;
        password = info.Password;
        DontDestroyOnLoad(base.gameObject);
        Leave();
        StartCoroutine(waitlvl());
    }
    void Leave()
    {
        Time.timeScale = 1f;
        if (PhotonNetwork.connected)
        {
            PhotonNetwork.Disconnect();
        }
        IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.STOP;
        FengGameManagerMKII.instance.gameStart = false;
        Screen.lockCursor = false;
        Screen.showCursor = true;
         FengGameManagerMKII.instance.MenuOn = false;
        UnityEngine.Object.Destroy(CyanMod.CachingsGM.Find("MultiplayerManager"));
        Application.LoadLevel("menu");
    }
    IEnumerator waitlvl()
    {
        yield return info_s =  INC.la("conected_photon_server");
        INC.Connected();
        yield return new WaitForSeconds(0.2f);
        RoomInfo[] info = PhotonNetwork.GetRoomList();
        int count_con = 0;
        while (info.Length == 0)
        {
            if (count_con > 20)
            {
                yield return info_s = INC.la("time_out_conected_photon_server");
                yield return new WaitForSeconds(2f);
                Destroy(base.gameObject);
            }
            yield return new WaitForSeconds(0.1f);
            count_con = count_con + 1;
            info = PhotonNetwork.GetRoomList();
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        yield return info_s = INC.la("find_server") + name_server.toHex().Resize(15) + "...";
        int count = 0;
        while (true)
        {
            if (count > 10)
            {
                yield return info_s = INC.la("server_not_found");
                yield return new WaitForSeconds(2f);
                Destroy(base.gameObject);
            }
            foreach (RoomInfo inf in info)
            {

                if (id_server == inf.IDRoom && name_server == inf.RoomName && map_server == inf.MapName && diff_server == inf.Difficulty && day_server == inf.DayTime && password == inf.Password)
                {
                    if (inf.playerCount < inf.maxPlayers)
                    {
                        PhotonNetwork.JoinRoom(inf.name);
                        yield return info_s = INC.la("conected_to_retry_server") + name_server.toHex().Resize(15) + "...";
                        yield return new WaitForSeconds(2f);
                        Destroy(base.gameObject);
                    }
                    else
                    {
                        yield return info_s =INC.la("retry_server_is_full");
                        yield return new WaitForSeconds(2f);
                        Destroy(base.gameObject);
                    }
                }
            }
            yield return new WaitForSeconds(0.1f);
            count = count + 1;
            info = PhotonNetwork.GetRoomList();
            yield return null;
        }
    }
    void OnGUI()
    {
        GUI.Box(new Rect(Screen.width / 2 - 80, Screen.height / 2 - 25, 160, 50), info_s);
    }
}

public class WitServer : UnityEngine.MonoBehaviour
{
    int id_server;
    string name_server;
    string map_server;
    string diff_server;
    string day_server;
    string password;
    string info_s = "";

    public void to_wait(RoomInfo info)
    {
       
        id_server = info.IDRoom;
        name_server = info.RoomName;
        map_server = info.MapName;
        diff_server = info.Difficulty;
        day_server = info.DayTime;
        password = info.Password;
        info_s = INC.la("waiting_server") + name_server.toHex().Resize(15) + "...";
        StartCoroutine(waitlvl());
    }

    IEnumerator waitlvl()
    {
        RoomInfo[] info = PhotonNetwork.GetRoomList();
        int count = 0;
        while (true)
        {
            if (count > 1000)
            {
                yield return info_s = INC.la("wait_server_time_out");
                yield return new WaitForSeconds(2f);
                Destroy(base.gameObject);
            }
            foreach (RoomInfo inf in info)
            {
                if (id_server == inf.IDRoom && name_server == inf.RoomName && map_server == inf.MapName && diff_server == inf.Difficulty && day_server == inf.DayTime && password == inf.Password)
                {
                    if (inf.playerCount < inf.maxPlayers)
                    {
                        PhotonNetwork.JoinRoom(inf.name);
                        yield return info_s = INC.la("conected_to_server") + name_server.toHex().Resize(15) + "...";
                        yield return new WaitForSeconds(2f);
                        Destroy(base.gameObject);
                    }
                }
            }
            yield return new WaitForSeconds(0.2f);
            info = PhotonNetwork.GetRoomList();
            yield return info_s = INC.la("waiting_server") + name_server.toHex().Resize(15) + "..." + "\n" + (1000 - count);
            count = count + 1;
            yield return null;

        }
    }
    Vector2 scrollPos;
    void OnGUI()
    {
        GUI.depth= -120;
        GUI.backgroundColor = INC.gui_color;
        GUILayout.BeginArea(new Rect(Screen.width / 2 - 120, Screen.height / 2 - 50, 240, 100),GUI.skin.box);
        scrollPos = GUILayout.BeginScrollView(scrollPos);
        GUILayout.Label(info_s, new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter});
        GUILayout.EndScrollView();
        if (GUILayout.Button(INC.la("wait_server_stop"),GUILayout.Width(100f)))
        {
            Destroy(base.gameObject);
        }
        GUILayout.EndArea();
    }
}

