using System;
using System.Collections.Generic;
using Chat = InRoomChat;

namespace CyanMod {
	public class Commands {
		static InRoomChat Chat = InRoomChat.instance;
		[Command("/banlist")]
		public static void BanList() {
			InRoomChat.instance.addLINE(INC.la("list_of_banned_players"));
			using (Dictionary<object, object>.KeyCollection.Enumerator enumerator3 = FengGameManagerMKII.banHash.Keys.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					if (enumerator3.Current == null) continue;
					int num5 = (int)enumerator3.Current;
					Chat.addLINE(string.Concat("", Convert.ToString(num5), ":", (string)FengGameManagerMKII.banHash[num5], ""));
				}
			}
		}
		[Command("/ban")]
		public static void Ban(int id) {
			CommandOnPlayer.ban(PhotonPlayer.Find(id));
		}
		[Command("/kick")]
		public static void Kick(int id) {
			CommandOnPlayer.kick(PhotonPlayer.Find(id));
		}
		[Command("/rules")]
		public static void ShowRules() {
			Chat.addLINE(INC.la("current_gamemodes"));
			foreach (var lines in RCSettings.rules)
			{
				Chat.addLINE(lines);
			}
		}
		[Command("/unban")]
		public static void Unban(string line) {
			if (FengGameManagerMKII.OnPrivateServer)
			{
				FengGameManagerMKII.ServerRequestUnban(line);
			}
			else if (PhotonNetwork.isMasterClient)
			{
				int num4 = Convert.ToInt32(line);
				if (FengGameManagerMKII.banHash.ContainsKey(num4))
				{
					FengGameManagerMKII.banHash.Remove(num4);
					cext.mess((string)FengGameManagerMKII.banHash[num4] + INC.la("unbanned_server"));
				}
				else
				{
					Chat.addLINE(INC.la("player_not_fond"));
				}
			}
			else
			{
				Chat.addLINE(INC.la("error_nomaster"));
			}
		}
		
	}
}