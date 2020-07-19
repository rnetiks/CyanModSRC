using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using CyanMod;

	public class Conference: Photon.MonoBehaviour
	{
       public List<cnf> conference_list;
        void Start()
        {
            conference_list = new List<cnf>();
        }
        cnf Find(int ID)
        {
            foreach (cnf c in conference_list)
            {
                if (c.ID_conf == ID)
                {
                    return c;
                }
            }
            return null;
        }
        public void command(string line, cnf room)
        {
            PhotonPlayer isMaster = room.master;
            line = line.Trim();
            if (!line.StartsWith("/"))
            {
                if (room.players_ID.Contains(PhotonNetwork.player.ID))
                {
                    object[] pam = new object[] { room.ID_conf, line };
                    foreach (PhotonPlayer player in PhotonNetwork.playerList)
                    {
                        if ((player.CM || PhotonNetwork.offlineMode) && room.players_ID.Contains(player.ID))
                        {
                            base.photonView.RPC("ConfChat", player, pam);
                        }
                    }
                }
                else
                {
                    room.messages.Add(INC.la("you_kicked_in_room"));
                }
            }
            else
            {
                if (line.StartsWith("/kick"))
                {
                    PhotonPlayer player = PhotonPlayer.Find(Convert.ToInt32(line.Split(new char[] { ' ' })[1]));
                    if (player == null)
                    {
                        room.messages.Add("ERROR: Player not found.");
                        return;
                    }
                    else if (  !player.CM )
                    {
                        room.messages.Add("ERROR: Only Cyan_mod user.");
                        return;
                    }
                    else if(player == PhotonNetwork.player)
                   {
                       room.messages.Add("ERROR: This is You.");
                       return;
                   }
                    else if (player != room.master)
                    {
                        room.messages.Add("ERROR: No Master.");
                        return;
                    }
                    else
                    {
                        foreach (PhotonPlayer player2 in PhotonNetwork.playerList)
                        {
                            if ((player2.CM || PhotonNetwork.offlineMode) && room.players_ID.Contains(player2.ID))
                            {
                                base.photonView.RPC("KickedConference", player2, new object[] { room.ID_conf, player.ID });
                            }
                        }
                    }
                }
                if (line.StartsWith("/add"))
                {
                    PhotonPlayer player = PhotonPlayer.Find(Convert.ToInt32(line.Split(new char[] { ' ' })[1]));
                    if (player == null)
                    {
                        room.messages.Add("ERROR: Player not found.");
                        return;
                    }
                    else if (!player.CM)
                    {
                        room.messages.Add("ERROR: Only Cyan_mod user.");
                        return;
                    }
                    else if (player == PhotonNetwork.player)
                    {
                        room.messages.Add("ERROR: This is You.");
                        return;
                    }
                    else if (PhotonNetwork.player != room.master)
                    {
                        room.messages.Add("ERROR: No Master.");
                        return;
                    }
                    else
                    {
                        if (!room.players_ID.Contains(player.ID))
                        {
                            foreach (PhotonPlayer player2 in PhotonNetwork.playerList)
                            {
                                if ((player2.CM || PhotonNetwork.offlineMode))
                                {
                                    base.photonView.RPC("AddConference", player2, new object[] { room.ID_conf, player.ID, room.players_ID.ToArray(), room.name_conf });
                                }
                            }
                        }
                        else
                        {
                            room.messages.Add("ERROR: Player Adedded.");
                            return;
                        }
                    }
                }
                if (line.StartsWith("/dell"))
                {
                    if (room.master == PhotonNetwork.player)
                    {
                        foreach (PhotonPlayer player2 in PhotonNetwork.playerList)
                        {
                            if ((player2.CM || PhotonNetwork.offlineMode) && room.players_ID.Contains(player2.ID) )
                            {
                                base.photonView.RPC("DeliteConference", player2, new object[] { room.ID_conf });
                            }
                        }
                    }
                }
                if (line.StartsWith("/exit"))
                {
                    foreach (PhotonPlayer player2 in PhotonNetwork.playerList)
                    {
                        if ((player2.CM || PhotonNetwork.offlineMode) && room.players_ID.Contains(player2.ID))
                        {
                            base.photonView.RPC("ExitConference", player2, new object[] { room.ID_conf });
                        }
                    }
                }
            }
        }
        [RPC]
        public void ConfChat(int ID,string text , PhotonMessageInfo info)
        {
             cnf c = Find(ID);
             if (c != null && c.players_ID.Contains(info.sender.ID))
             {
                 if (!PhotonPlayer.InRoom(c.master))
                 {
                     int i = 0;
                     PhotonPlayer player = null;
                     while ((player = PhotonPlayer.Find(c.players_ID[i])) == null)
                     {
                         i++;
                         if(i>c.players_ID.Count)
                         {
                             break;
                         }
                     }
                     c.master = player;
                     c.messages.Add("Master Conference switched to " + player.ishexname);
                 }
                 if (c.messages.Count > 30)
                 {
                     c.messages.Remove(c.messages[0]);
                 }
                 c.messages.Add(info.sender.id + info.sender.ishexname.HexDell() + ":" + text.HexDell());
             }
        }
        [RPC]
        public void ExitConference(int ID,  PhotonMessageInfo info)
        {
            cnf c = Find(ID);
            PhotonPlayer player = info.sender;
            if (c != null && c.players_ID.Contains(player.ID))
            {
                if (player == PhotonNetwork.player)
                {
                    conference_list.Remove(c);
                }
                else
                {
                    c.players_ID.Remove(player.ID);
                    c.messages.Add(INC.la("out_conference") + player.iscleanname);
                }
            }
        }
        public cnf CreateConference( string name)
        {
            cnf c = new cnf((int)UnityEngine.Random.Range(999, 99999), name, PhotonNetwork.player, new int[]{PhotonNetwork.player.ID});
            conference_list.Add(c);
       Debug.Log("Беседа с именем " +name + " создана.");
            string comlist = "~~~Command List~~~\n/add ID - added player on Conference. \n/kick ID - kicked player on Conference. \n/dell - dellite Conference.\n/exit - out Conference.\n~~~~~~~~~~~~~~~~";
            c.messages.Add(comlist);
            return c;
        }
        [RPC]
        public void KickedConference(int ID_room,int ID_player, PhotonMessageInfo info)
        {
            cnf con = Find(ID_room);
            if (con != null && con.master == info.sender)
            {
                con.players_ID.Remove(ID_player);
                con.messages.Add(INC.la("kicked_confer") + PhotonPlayer.Find(ID_player).iscleanname);
            }
        }
        [RPC]
        public void AddConference(int ID_room, int ID_player,int[] ids,string name, PhotonMessageInfo info)
        {
            if (ID_player == PhotonNetwork.player.ID)
            {
                cnf c = new cnf(ID_room, name, info.sender, ids);
                conference_list.Add(c);
                c.messages.Add("~~~~Commands~~~~\n/exit - out Conference.\n~~~~~~~~~~~~~");
                InRoomChat.instance.addLINE(INC.la("you_aded_to_conf") + name);
            }
            else
            {
                cnf con = Find(ID_room);
                if (con != null && con.master == info.sender)
                {
                    con.players_ID.Add(ID_player);
                    con.messages.Add(INC.la("added_new_player") + PhotonPlayer.Find(ID_player).iscleanname);
                }
            }
        }
        [RPC]
        public void DeliteConference(int ID, PhotonMessageInfo info)
        {
            cnf c = Find(ID);
            if (c != null)
            {
                PhotonPlayer player = info.sender;
                if (player == c.master)
                {
                    conference_list.Remove(c);
                }
            }
        }
        public class cnf
        {
            public List<int> players_ID;
            public List<int> exist_ID;
            public int ID_conf;
            public string name_conf;
            public PhotonPlayer master;
            public List<string> messages;

            public cnf(int ID,string name,PhotonPlayer is_master,int[] ids)
            {
                players_ID = new List<int>(ids);
                exist_ID = new List<int>();
                ID_conf = ID;
                name_conf = name;
                master = is_master;
                messages = new List<string>();
            }
        }
	}

