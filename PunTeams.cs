using System;
using System.Collections.Generic;
using UnityEngine;

public class PunTeams : MonoBehaviour
{
    public static Dictionary<Team, List<PhotonPlayer>> PlayersPerTeam;
    public const string TeamPlayerProp = "team";

    public void OnJoinedRoom()
    {
        this.UpdateTeams();
    }

    public void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
    {
        this.UpdateTeams();
    }

    public void Start()
    {
        PlayersPerTeam = new Dictionary<Team, List<PhotonPlayer>>();
        foreach (object obj2 in Enum.GetValues(typeof(Team)))
        {
            PlayersPerTeam[(Team) ((byte) obj2)] = new List<PhotonPlayer>();
        }
    }

    public void UpdateTeams()
    {
        foreach (object obj2 in Enum.GetValues(typeof(Team)))
        {
            PlayersPerTeam[(Team) ((byte) obj2)].Clear();
        }
        for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            PhotonPlayer player = PhotonNetwork.playerList[i];
            Team team = player.GetTeam();
            PlayersPerTeam[team].Add(player);
        }
    }

    public enum Team : byte
    {
        blue = 2,
        none = 0,
        red = 1
    }
}

