using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using CyanMod;
using System.Net.NetworkInformation;
using System.IO;
using System.Net;

public class TCPConnects : UnityEngine.MonoBehaviour
{
    Rect rect = new Rect(100, 20, 300, 400);
    Vector2 vector;
    List<string> tcp_list = new List<string>();

    void OnGUI()
    {
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
        {
            rect = GUILayout.Window(228, rect, win, "",GUI.skin.box);
        }
    }
    void win(int id)
    {
        GUILayout.Label("------GetActiveTcpListeners-----");
        vector = GUILayout.BeginScrollView(vector);
        foreach (IPEndPoint tss in IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners())
        {
                GUILayout.TextField(tss.ToString());
            
        }
        GUILayout.Label("------GetActiveUdpListeners-----");
        foreach (IPEndPoint tss in IPGlobalProperties.GetIPGlobalProperties().GetActiveUdpListeners())
        {
            GUILayout.TextField(tss.ToString());
        }
        GUILayout.Label("------GetIPv6GlobalStatistics-----");
   
        GUILayout.EndScrollView();
        if (GUILayout.Button("Save"))
        {
            string stee = "";
            foreach (string str in tcp_list)
            {
                stee = stee + str + "\n";
            }
            File.WriteAllText(Application.dataPath + "/connects.txt",stee, Encoding.UTF8);
        }
        GUI.DragWindow();
    }
}

