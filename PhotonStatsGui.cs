using ExitGames.Client.Photon;
using System;
using UnityEngine;
using CyanMod;

public class PhotonStatsGui : MonoBehaviour
{
    public bool buttonsOn;
    public bool healthStatsVisible;
    public bool statsOn = true;
    public Rect statsRect = new Rect(0f, 100f, 200f, 50f);
    public bool statsWindowOn = true;
    public bool trafficStatsOn;
    public int WindowId = 100;

    public void newSettin(float labelwidth)
    {
        GUICyan.OnToogleCyan(INC.la("show_connection"), 384, 1, 0, labelwidth);
        try
        {
            if ((int)FengGameManagerMKII.settings[384] == 1)
            {
                PhotonNetwork.networkingPeer.TrafficStatsEnabled = true;
            }
            else
            {
                PhotonNetwork.networkingPeer.TrafficStatsEnabled = false;
            }
        }
        catch{};
    }

    public void lateUpdate()
    {
        ExitGames.Client.Photon.TrafficStatsGameLevel trafficStatsGameLevel = PhotonNetwork.networkingPeer.TrafficStatsGameLevel;
        string text = string.Format("Out|In|Sum:{0,4}|{1,4}|{2,4}", trafficStatsGameLevel.TotalOutgoingMessageCount, trafficStatsGameLevel.TotalIncomingMessageCount, trafficStatsGameLevel.TotalMessageCount);
        text = text + "\n(in)Total KBytes|Packs\n" + PhotonNetwork.networkingPeer.TrafficStatsIncoming.TotalPacketBytes / 1024 + "|" + PhotonNetwork.networkingPeer.TrafficStatsIncoming.TotalPacketCount;
        text = text + "\n(out)Total KBytes|Packs\n" + PhotonNetwork.networkingPeer.TrafficStatsOutgoing.TotalPacketBytes / 1024 + "|" + PhotonNetwork.networkingPeer.TrafficStatsOutgoing.TotalPacketCount;
        text = text + "\n" + string.Format("ping: {6}[+/-{7}]ms\nlongest delta between\nsend: {0,4}ms disp: {1,4}ms\nlongest time for:\nev({3}):{2,3}ms op({5}):{4,3}ms", new object[] { trafficStatsGameLevel.LongestDeltaBetweenSending, trafficStatsGameLevel.LongestDeltaBetweenDispatching, trafficStatsGameLevel.LongestEventCallback, trafficStatsGameLevel.LongestEventCallbackCode, trafficStatsGameLevel.LongestOpResponseCallback, trafficStatsGameLevel.LongestOpResponseCallbackOpCode, PhotonNetwork.networkingPeer.RoundTripTime, PhotonNetwork.networkingPeer.RoundTripTimeVariance });
        text = text + "\n" + FengGameManagerMKII.instance.text_pause;
        FengGameManagerMKII.instance.PauseLabel(text);
    }
}

