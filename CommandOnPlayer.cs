using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CyanMod;


	public class CommandOnPlayer
	{
        public static void kick(PhotonPlayer player)
        {
            if (player != null)
            {
                if (player == PhotonNetwork.player)
                {
                    FengGameManagerMKII.instance.chatRoom.addLINE(INC.la("error_you_no_kiked"));
                }
                else if (!FengGameManagerMKII.OnPrivateServer && !PhotonNetwork.isMasterClient)
                {
                    object[] parameters4 = new object[] { "/kick #" + Convert.ToString(player.ID), LoginFengKAI.player.name };
                    FengGameManagerMKII.instance.photonView.RPC("Chat", PhotonTargets.All, parameters4);
                }
                else
                {
                    if (FengGameManagerMKII.OnPrivateServer)
                    {
                        FengGameManagerMKII.instance.kickPlayerRC(player, false, "");
                    }
                    else if (PhotonNetwork.isMasterClient)
                    {
                        FengGameManagerMKII.instance.kickPlayerRC(player, false, "");
                        cext.mess(player.name() + INC.la("kicked_from_the_serv"));
                    }
                }
            }
            else
            {
                FengGameManagerMKII.instance.chatRoom.addLINE(INC.la("player_not_fond"));
            }
        }

        public static void ban(PhotonPlayer player)
        {
            if (player != null)
            {
                if (player == PhotonNetwork.player)
                {
                    FengGameManagerMKII.instance.chatRoom.addLINE(INC.la("error_you_no_kiked"));
                }
                else if (FengGameManagerMKII.OnPrivateServer)
                {
                    FengGameManagerMKII.instance.kickPlayerRC(player, true, "");
                }
                else if (PhotonNetwork.isMasterClient)
                {
                    FengGameManagerMKII.instance.kickPlayerRC(player, true, "");
                    cext.mess(player.name() + player.id() + INC.la("has_banned"));
                }
            }
            else
            {
                FengGameManagerMKII.instance.chatRoom.addLINE(INC.la("player_not_fond"));
            }
        }
	}

