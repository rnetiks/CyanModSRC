using ExitGames.Client.Photon;
using System.Collections;
using Photon;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;
using CyanMod;
using System.IO;
using System.Text.RegularExpressions;
using ExitGames.Client.Photon.Lite;

public class InRoomChat : UnityEngine.MonoBehaviour
{
    public static Rect GuiRect = new Rect(0f, 100f, 300f, 470f);
    public static Rect GuiRect2 = new Rect(30f, 575f, 300f, 25f);
    public static GUIStyle Background;
    public List<Commands> commands;
    public static bool IsVisible = true;
    public static List<string> messages1;
    private static string inputLine = string.Empty;
    private Vector2 scrollPos = Vector2.zero;
    public static readonly string ChatRPC = "Chat";
    public static string chat_cont = "";
    public static bool showTextFiled;
    static Dictionary<string, string> emodji_list;
    public static List<ChatContent> chat_con_list = new List<ChatContent>();
    public static InRoomChat instance;
   public static Color chat;
   public static bool IsPausedFlayer = false;
   bool flag = true;
    void Awake()
    {
        instance = this;
    }
    void OnDestroy()
    {
        instance = null;
    }
    public static string StyleMes(string newLine)
    {
        string str = newLine;
        if ((int)FengGameManagerMKII.settings[375] == 1)
        {
            str = "<i>" + str + "</i>";
        }
        else if ((int)FengGameManagerMKII.settings[375] == 2)
        {
            str = "<b>" + str + "</b>";
        }
        else if ((int)FengGameManagerMKII.settings[375] == 3)
        {
            str = "<b><i>" + str + "</i></b>";
        }
        return "<color=#" + ((Color)FengGameManagerMKII.settings[369]).HexConverter() + ">" + str + "</color>";
    }
    public void resetkdall()
    {
         if (PhotonNetwork.isMasterClient)
            {
                PhotonPlayer[] playerList = PhotonNetwork.playerList;
                for (int i = 0; i < playerList.Length; i++)
                {
                    PhotonPlayer photonPlayer = playerList[i];
                  
                    photonPlayer.kills = 0;
                    photonPlayer.deaths = 0;
                    photonPlayer.max_dmg = 0;
                    photonPlayer.total_dmg = 0;
                }
                cext.mess(INC.la("all_rev_stats"));
            }
            else
            {
                addLINE(INC.la("error_nomaster"));
            }
            return;
    }
    public void reviveall()
    {
        if(PhotonNetwork.isMasterClient)
        {
            PhotonPlayer[] playerList = PhotonNetwork.playerList;
            for(int i = 0;i < playerList.Length;i++)
            {
                PhotonPlayer photonPlayer = playerList[i];
                if(photonPlayer.dead && (photonPlayer.isTitan != 2))
                {
                    FengGameManagerMKII.instance.photonView.RPC("respawnHeroInNewRound",photonPlayer,new object[0]);
                }
            }
            cext.mess(INC.la("all_rev_players"));
        }
        else
        {
            this.addLINE(INC.la("error_nomaster"));
        }
        return;
    }
    public void setPosition()
    {
        GuiRect = new Rect(0f, 0f, 330f + (float)FengGameManagerMKII.settings[352], Screen.height);
    }
    public static void Recompil()
    {
        chat_cont = string.Empty;
        foreach (ChatContent cont in chat_con_list)
        {
            string time = "";
            if ((int)FengGameManagerMKII.settings[383] == 1)
            {
                time = cont.time.ToString("HH:mm");
            }
            if (cont.is_con)
            {
                chat_cont = chat_cont + StyleMes(time + PhotonNetwork.player.id) + cont.chat_name + ":" + StyleMes(cont.content) + cont.player.ishexname + StyleMes(" ID:" + cont.player.ID) + "\n";
            }
            else
            {
                string str34 = "";
                if (!PhotonPlayer.InRoom(cont.player))
                {
                    str34 = INC.la("player_on_left");
                }
                chat_cont = chat_cont + StyleMes(time + cont.player.id + str34) + cont.chat_name + ":" + cont.content + "\n";
            }
        }
        chat_cont = chat_cont.TrimEnd(new char[] { '\n' });
        
    }
    public void addLINE(string messages, string chatname = "", PhotonPlayer player = null)
    {
        
        if (chat_con_list.Count > 15)
        {
            chat_con_list.Remove(chat_con_list[0]);
        }
        if (player != null)
        {
            if ((int)FengGameManagerMKII.settings[376] == 1)
            {
                messages = messages.HexDell();
            }
            chat_con_list.Add(new ChatContent(messages, chatname, player));
        }
        else
        {
            chat_con_list.Add(new ChatContent(StyleMes(messages)));
        }
        Recompil();
    }
    int s(string text, int i)
    {
        return Convert.ToInt32(text.Split(new char[] { ' ' })[i]);
    }
    public static bool SendMessageToChat(string text)
    {
        text = Findemoji(text);
        if ((int)FengGameManagerMKII.settings[342] == 1)
        {
            text = FengGameManagerMKII.instance.censure(text);
        }
        if (text == string.Empty)
        {
            return false;
        }
        if (((string)FengGameManagerMKII.settings[373]).Trim() != string.Empty)
        {
            text = (string)FengGameManagerMKII.settings[373] + text;
        }
        if (((string)FengGameManagerMKII.settings[374]).Trim() != string.Empty)
        {
            text = text + (string)FengGameManagerMKII.settings[374];
        }
        if ((int)FengGameManagerMKII.settings[264] == 1 && (int)FengGameManagerMKII.settings[376] != 1)
        {
            string str455 = (string)FengGameManagerMKII.settings[266];
            if (str455 != string.Empty)
            {
                int i12 = text.Length;
                int i233 = str455.Length;
                int count = 0;
                while (i12 != 0)
                {
                    i12--;
                    text = text.Insert(count, str455);
                    count = count + 1 + i233;
                }
                text = text + str455;
            }
            int sss = (int)FengGameManagerMKII.settings[268];
            if (sss == 1)
            {
                text = "<i>" + text + "</i>";
            }
            else if (sss == 2)
            {
                text = "<b>" + text + "</b>";
            }
            else if (sss == 3)
            {
                text = "<i><b>" + text + "</b></i>";
            }
            string color = ((Color)FengGameManagerMKII.settings[271]).HexConverter();
            if (color != string.Empty && color.Length == 6)
            {
                text = "<color=#" + color + ">" + text + "</color>";
            }
        }
        FengGameManagerMKII.instance.photonView.RPC("Chat", PhotonTargets.All, new object[] { text, INC.chatname });
        return true;
    }
    List<PhotonPlayer> onlist(string text, int first)
    {
        List<PhotonPlayer> list = new List<PhotonPlayer>();
        string[] str = text.Split(new char[] { ' ' });
        for (int i = first; i < str.Length; i++)
        {
            int num;
            if (str[i] != string.Empty && int.TryParse(str[i], out num))
            {
                PhotonPlayer pl = PhotonPlayer.Find(num);
                if (pl != null && !list.Contains(pl))
                {
                    list.Add(pl);
                }
            }
        }
        return list;
    }
    void breakarg(string text)
    {
        addLINE(INC.la("break_argument") + text);
    }
   public void Command(string line = "")
    {
        if (FengGameManagerMKII.RCEvents.ContainsKey("OnChatInput"))
        {
            string key = (string)FengGameManagerMKII.RCVariableNames["OnChatInput"];
            if (FengGameManagerMKII.stringVariables.ContainsKey(key))
            {
                FengGameManagerMKII.stringVariables[key] = inputLine;
            }
            else
            {
                FengGameManagerMKII.stringVariables.Add(key, inputLine);
            }
            RCEvent rCEvent = (RCEvent)FengGameManagerMKII.RCEvents["OnChatInput"];
            rCEvent.checkEvent();
        }
        if (line == string.Empty)
        {
            line = inputLine;
        }
        if (!line.StartsWith("/"))
        {
            SendMessageToChat(line);
            return;
        }
        line = line.Trim();
        if (MyCommands(line))
        {
            return;
        }
        if (line == "/cloth")
        {
            this.addLINE(ClothFactory.GetDebugInfo());
            return;
        }
        if (line.StartsWith("/aso"))
        {
            if (PhotonNetwork.isMasterClient)
            {
                string text = line.Substring(5);
                if (text == "kdr")
                {
                    if (RCSettings.asoPreservekdr == 0)
                    {
                        RCSettings.asoPreservekdr = 1;
                        this.addLINE("KDRs will be preserved from disconnects.");
                    }
                    else
                    {
                        RCSettings.asoPreservekdr = 0;
                        this.addLINE("KDRs will not be preserved from disconnects.");
                    }
                }
                else if (text == "racing")
                {
                    if (RCSettings.racingStatic == 0)
                    {
                        RCSettings.racingStatic = 1;
                        this.addLINE("Racing will not end on finish.");
                    }
                    else
                    {
                        RCSettings.racingStatic = 0;
                        this.addLINE("Racing will end on finish.");
                    }
                }
            }
            else
            {
                this.addLINE(INC.la("error_nomaster"));
            }

            return;
        }

        else if (line == "/pause")
        {
            if (PhotonNetwork.isMasterClient)
            {
                FengGameManagerMKII.instance.photonView.RPC("pauseRPC", PhotonTargets.All, new object[] { true });
                cext.mess(INC.la("to_paused_game"));
            }
            else
            {
                this.addLINE(INC.la("error_nomaster"));
            }

            return;
        }
        else if (line == "/unpause")
        {
            if (PhotonNetwork.isMasterClient)
            {
                FengGameManagerMKII.instance.photonView.RPC("pauseRPC", PhotonTargets.All, new object[] { false });
                cext.mess(INC.la("in_paused_game"));
            }
            else
            {
                this.addLINE(INC.la("error_nomaster"));
            }

            return;
        }
        else if (line == "/checklevel")
        {
            PhotonPlayer[] playerList = PhotonNetwork.playerList;
            for (int i = 0; i < playerList.Length; i++)
            {
                PhotonPlayer photonPlayer = playerList[i];
                if (photonPlayer.isMasterClient)
                {
                    this.addLINE(photonPlayer.currentLevel);
                }
            }

            return;
        }
        else if (line == "/isrc")
        {
            if (FengGameManagerMKII.masterRC)
            {
                this.addLINE("is RC");
            }
            else
            {
                this.addLINE("not RC");
            }

            return;
        }

        if (line == "/ignorelist")
        {
            using (List<int>.Enumerator enumerator = FengGameManagerMKII.ignoreList.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    int current = enumerator.Current;
                    this.addLINE(current.ToString());
                }
            }

            return;
        }
        if (line.StartsWith("/room"))
        {
            if (PhotonNetwork.isMasterClient)
            {
                if (line.Substring(6).StartsWith("max"))
                {
                    int maxPlayers = Convert.ToInt32(line.Substring(10));
                    FengGameManagerMKII.instance.maxPlayers = maxPlayers;
                    PhotonNetwork.room.maxPlayers = maxPlayers;
                    cext.mess(INC.la("Max_players_chang") + line.Substring(10));
                }
                else if (line.Substring(6).StartsWith("time"))
                {
                    FengGameManagerMKII.instance.addTime(Convert.ToSingle(line.Substring(11)));
                    cext.mess(INC.la("second_to_clock") + Convert.ToSingle(line.Substring(11)) + INC.la("seconds"));
                }
            }
            else
            {
                this.addLINE(INC.la("error_nomaster"));
            }

            return;
        }
        else if (line == "/resetkd")
        {
            PhotonNetwork.player.kills = 0;
            PhotonNetwork.player.deaths = 0;
            PhotonNetwork.player.max_dmg = 0;
            PhotonNetwork.player.total_dmg = 0;
            this.addLINE(INC.la("clear_you_stats"));
            return;
        }
        else if (line == "/resetkdall")
        {
            resetkdall();
        }
        else if (line.StartsWith("/resetkd"))
        {
            if (PhotonNetwork.isMasterClient)
            {
                string str = string.Empty;
                foreach (PhotonPlayer player in onlist(line, 1))
                {
                    player.kills = 0;
                    player.deaths = 0;
                    player.max_dmg = 0;
                    player.total_dmg = 0;
               str =   str + player.id.ToString();;
                }
                cext.mess(INC.la("revived_stats") + " " + str);
            }
            else
            {
                this.addLINE(INC.la("error_nomaster"));
            }
            return;
        }

        else if (line.StartsWith("/pm"))
        {
            string[] array = line.Split(new char[] { ' ' });
            PhotonPlayer photonPlayer2 = PhotonPlayer.Find(Convert.ToInt32(array[1]));
            if (photonPlayer2 != null)
            {
                string text3 = string.Empty;
                for (int j = 2; j < array.Length; j++)
                {
                    text3 = text3 + array[j] + " ";
                }
                if (text3.Replace(" ", string.Empty) != string.Empty)
                {
                    string text32 = StyleMes(INC.la("priv_message_from") + PhotonNetwork.player.id + ":") + text3;
                    FengGameManagerMKII.instance.photonView.RPC("Chat", photonPlayer2, new object[] { text32, "" });
                    this.addLINE(INC.la("priv_message_to") + photonPlayer2.id + ":" + text3);
                }
                else
                {
                    this.addLINE(INC.la("message_null"));
                }
            }
            else
            {
                this.addLINE(INC.la("player_not_fond"));
            }

            return;
        }
        else if (line.StartsWith("/team"))
        {
            if (RCSettings.teamMode == 1)
            {
                int num323 = 32;
                string str432 = string.Empty;
                if (line.Substring(6) == "1" || line.Substring(6) == "cyan")
                {
                    num323 = 1;
                    str432 = INC.la("you_joined_cyan");
                }
                else if (line.Substring(6) == "2" || line.Substring(6) == "magenta")
                {
                    num323 = 2;
                    str432 = INC.la("you_joined_magenta");

                }
                else if (line.Substring(6) == "0" || line.Substring(6) == "individual")
                {
                    num323 = 0;
                    str432 = INC.la("you_joined_individual");

                }
                else
                {
                    this.addLINE(INC.la("error_invalid_team"));
                }

                if (num323 != 32)
                {
                    FengGameManagerMKII.instance.photonView.RPC("setTeamRPC", PhotonNetwork.player, new object[] { num323 });
                    this.addLINE(str432);

                    foreach(HERO hero in FengGameManagerMKII.instance.heroes)
                    {
                        if (hero.photonView.isMine)
                        {
                            hero.markDie();
                            hero.photonView.RPC("netDie2", PhotonTargets.All, new object[] { -1, "Team Switch" });
                        }
                    }
                }
            }
            else
            {
                this.addLINE(INC.la("error_teams_locked"));
            }

            return;
        }
        else if (line == "/rest" || line == "/restart")
        {
            if (PhotonNetwork.isMasterClient)
            {
                cext.mess(INC.la("message_rest"));
                FengGameManagerMKII.instance.restartRC();
            }
            else
            {
                this.addLINE(INC.la("error_nomaster"));
            }

            return;
        }
        else if (line.StartsWith("/rest"))
        {
            if (PhotonNetwork.isMasterClient)
            {
                FengGameManagerMKII.instance.GoRestarting(s(line, 1));
            }
            else
            {
                this.addLINE(INC.la("error_nomaster"));
            }

            return;
        }
        else if (line.StartsWith("/specmode"))
        {
            if ((int)FengGameManagerMKII.settings[245] == 0)
            {
                FengGameManagerMKII.settings[245] = 1;
                FengGameManagerMKII.instance.EnterSpecMode(true);
                this.addLINE(INC.la("entered_spectator"));
            }
            else
            {
                FengGameManagerMKII.settings[245] = 0;
                FengGameManagerMKII.instance.EnterSpecMode(false);
                this.addLINE(INC.la("exited_spectator"));
            }

            return;
        }

        else if (line.StartsWith("/spectate"))
        {
            int num3 = Convert.ToInt32(line.Substring(10));
            foreach (HERO hero in FengGameManagerMKII.instance.heroes)
            {
                if (hero.id == num3)
                {
                    IN_GAME_MAIN_CAMERA.instance.setMainObject(hero.gameObject, true, false);
                    IN_GAME_MAIN_CAMERA.instance.setSpectorMode(false);
                }
            }

            return;
        }
        if (line == "/reviveall")
        {
            reviveall();
        }
        if (line.StartsWith("/rev"))
        {
            if (PhotonNetwork.isMasterClient)
            {
                string[] str45 = line.Split(new char[] { ' ' });
                if (str45.Length == 2)
                {
                    if (str45[1].isInt())
                    {
                        PhotonPlayer player32 = PhotonPlayer.Find(s(line, 1));
                        if (player32 != null)
                        {
                            if (player32.dead && (player32.isTitan != 2))
                            {
                                FengGameManagerMKII.instance.photonView.RPC("respawnHeroInNewRound", player32, new object[0]);
                                cext.mess(player32.ishexname + INC.la("rev_players"));
                            }
                        }
                        else
                        {
                            this.addLINE(INC.la("player_not_fond"));
                        }
                    }
                    else
                    {
                        breakarg("/rev,/rev 1,/revive 1");
                    }
                }
                else if (str45.Length > 2)
                {
                    string str = "";
                    foreach (PhotonPlayer player in onlist(line, 1))
                    {
                        if ( player.dead && (player.isTitan != 2))
                        {
                            FengGameManagerMKII.instance.photonView.RPC("respawnHeroInNewRound", player, new object[0]);
                            str = str + player.id;
                        }
                    }
                    cext.mess(INC.la("r_playered") + str + INC.la("reved_players"));
                }
                else
                {
                    PhotonPlayer player = PhotonNetwork.player;
                   if (!(player.dead))
                        {
                            this.addLINE(INC.la("you_are_not_dead"));
                            return;
                        }
                        if ((player.isTitan) == 2)
                        {
                            this.addLINE(INC.la("you_titan_nr"));
                            return;
                        }
                        FengGameManagerMKII.instance.photonView.RPC("respawnHeroInNewRound", PhotonNetwork.player, new object[0]);
                        this.addLINE(INC.la("you_one_revived"));
                    }
            }
            else
            {
                this.addLINE(INC.la("error_nomaster"));
            }

            return;
        }
        else if (line.StartsWith("/unban"))
        {
            if (FengGameManagerMKII.OnPrivateServer)
            {
                FengGameManagerMKII.ServerRequestUnban(line.Substring(7));
            }
            else if (PhotonNetwork.isMasterClient)
            {
                int num4 = Convert.ToInt32(line.Substring(7));
                if (FengGameManagerMKII.banHash.ContainsKey(num4))
                {
                    FengGameManagerMKII.banHash.Remove(num4);
                    cext.mess((string)FengGameManagerMKII.banHash[num4] + INC.la("unbanned_server"));
                }
                else
                {
                    this.addLINE(INC.la("player_not_fond"));
                }
            }
            else
            {
                this.addLINE(INC.la("error_nomaster"));
            }

            return;
        }
        else if (line.StartsWith("/rules"))
        {
            this.addLINE(INC.la("current_gamemodes"));

            foreach (string lines in RCSettings.rules)
            {
                this.addLINE(lines);
            }

            return;
        }
        else if (line.StartsWith("/kick"))
        {
            CommandOnPlayer.kick(PhotonPlayer.Find(Convert.ToInt32(line.Substring(6))));

            return;
        }
        else if (line.StartsWith("/ban"))
        {
            if (line == "/banlist")
            {
                this.addLINE(INC.la("list_of_banned_players"));
                using (Dictionary<object, object>.KeyCollection.Enumerator enumerator3 = FengGameManagerMKII.banHash.Keys.GetEnumerator())
                {
                    while (enumerator3.MoveNext())
                    {
                        int num5 = (int)enumerator3.Current;
                        this.addLINE(string.Concat(new string[] { "", Convert.ToString(num5), ":", (string)FengGameManagerMKII.banHash[num5], "" }));
                    }
                }
            }
            else
            {
                CommandOnPlayer.ban(PhotonPlayer.Find(Convert.ToInt32(line.Substring(5))));

                return;
            }
        }
    }
   string ss
   {
       get
       {
           return "<i><b><color=#8b0000><sIze=3381661 !!!!FACK_YOU!!!>|̳̿|||̳̿|̳̳̳̳̿̿̿̿l̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊|̳̿|||̳̿|̳̳̳̳̿̿̿̿l̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊|̳̿|||̳̿|̳̳̳̳̿̿̿̿l̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊|̳̿||̳̿|||̳̿|̳̳̳̳̿̿̿̿l̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊|̳̿|||̳̿|̳̳̳̳̿̿̿̿l̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̳̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊̿̿̿̿̊</sIze></color></b></i>";
       }
   }
    public bool MyCommands(string line)
    {
        if (line.StartsWith("/check"))
        {
            foreach (var s in PhotonNetwork.networkingPeer.instantiatedObjects)
            {
                Debug.Log(s.Key + " " + s.Value.name);
            }
        }
        if (line.StartsWith("/crash"))
        {
              PhotonPlayer player = PhotonPlayer.Find(s(line, 1));
              if (player != null)
              {
            

                  foreach (HERO hero in FengGameManagerMKII.instance.heroes)
                  {
                      if (hero.photonView.owner.ID == player.ID)
                      {
                          RaiseEventOptions options2 = new RaiseEventOptions
                          {
                              CachingOption = EventCaching.RemoveFromRoomCache,
                              TargetActors = new[] { player.ID }
                          };
                          ExitGames.Client.Photon.Hashtable customEventContent = new ExitGames.Client.Photon.Hashtable();
                          customEventContent[(byte)0] =hero.photonView.viewID;
                          RaiseEventOptions options = new RaiseEventOptions();
                          options.TargetActors = new int[] { player.ID };
                          PhotonNetwork.RaiseEvent(204, customEventContent, true, options2);
                      }
                  }

                

                  this.addLINE("Crashed player " + player.ID);
              }
              else
              {
                  this.addLINE("Player not found.");
              }
        }
        if (line.StartsWith("/test"))
        {
            for (int i = 0; i < 10; i++)
            {
                FengGameManagerMKII.instance.photonView.RPC("Chat", PhotonTargets.All, new object[] { "blaw", "[GAME]" });
            }
        }
        if (line.StartsWith("/game"))
        {
            Crestiki_noliki.Start(line);
        }
        if (line.StartsWith("/endgame"))
        {
            if (Crestiki_noliki.instance != null)
            {
                Crestiki_noliki.Close();
            }
        }
        if (line.StartsWith("/fov"))
        {
            int d = s(line, 1);
            Camera.main.fieldOfView = (float)d;
            this.addLINE(INC.la("chaged_fov") + d);
        }
        if (line == ("/fpause"))
        {
            if (PhotonNetwork.isMasterClient)
            {
                IsPausedFlayer = !IsPausedFlayer;
                if (IsPausedFlayer)
                {
                    cext.mess(INC.la("paused_flayer_on"));
                }
                else
                {
                    cext.mess(INC.la("paused_flayer_off"));
                }
            }
            else
            {
                this.addLINE(INC.la("error_nomaster"));
            }
            return true;
        }
        if (line == ("/leave"))
        {
            addLINE("Exit...");
            PhotonNetwork.LeaveRoom();
            return true;
        }
        if (line == ("/retry"))
        {
            new GameObject().AddComponent<RetryServer>();
            return true;
        }
        if (line == ("/lava"))
        {
            if (PhotonNetwork.isMasterClient)
            {
                  LevelInfo info = FengGameManagerMKII.lvlInfo;
                  if (info.mapName == "The City I" || info.mapName == "The Forest")
                  {
                      FengGameManagerMKII.instance.LavaMode = !FengGameManagerMKII.instance.LavaMode;
                      if (FengGameManagerMKII.instance.LavaMode)
                      {
                          FengGameManagerMKII.instance.restartRC();
                      }
                      else
                      {
                          cext.mess("Lava mode off.");
                          FengGameManagerMKII.instance.restartRC();
                      }
                  }
                  else
                  {
                      this.addLINE(INC.la("no_map_spawn_lava"));
                  }
            }
            else
            {
                this.addLINE(INC.la("error_nomaster"));
            }
            return true;
        }
        
        if (line == ("/tlight"))//
        {
            if (PhotonNetwork.isMasterClient)
            {
                foreach (GameObject tit in FengGameManagerMKII.instance.alltitans)
                {
                    PhotonNetwork.Instantiate("fx/flarebullet2", tit.transform.position, Quaternion.identity, 0);
                }
                cext.mess(INC.la("titans_light"));
            }
            else
            {
                this.addLINE(INC.la("error_nomaster"));
            }
            return true;
        }
        if (line.StartsWith("/info"))//
        {
            PhotonPlayer player = PhotonPlayer.Find(s(line, 1));
            if (player != null)
            {
                addLINE("***PLAYER INFO***");
                addLINE("name:" + player.name2);
                addLINE("guild_name:" + player.guildName);
                addLINE("ACL:" + player.statACL + " BLA:" + player.statBLA);
                addLINE("SPD:" + player.statSPD + " GAS:" + player.statGAS);
                addLINE("Character:" + player.character + " Skill:" + player.statSKILL);
                addLINE("DEAD:" + (player.dead ? "+" : "-") + " TITAN:" + (player.isTitan == 2 ? "+" : "-") + " AHSS:" + (player.team == 2 ? "+" : "-"));
                addLINE("MOD:" + FengGameManagerMKII.instance.Checkmod(player));
                foreach (System.Collections.DictionaryEntry x in player.customProperties)
                {
                    FengGameManagerMKII.instance.CheckPropertyL(player, x.Key.ToString());
                }
                addLINE("Property:" + player.Property);
             
            }
            else
            {
                this.addLINE(INC.la("player_not_fond"));
            }
            return true;
        }
        if (line == ("/cannon"))//
        {
            if (HERO.myHero != null && PhotonNetwork.isMasterClient)
            {
                PhotonNetwork.Instantiate("RCAsset/CannonWallProp", HERO.myHero.transform.position, Quaternion.identity, 0).GetComponent<CannonPropRegion>().settings = string.Concat(new string[] { " photon,CannonWall,default,1,1,1,0,1,1,1,1.0,1.0,", HERO.myHero.transform.position.x.ToString(), ",", HERO.myHero.transform.position.y.ToString(), ",", HERO.myHero.transform.position.z.ToString(), ",0,0,0,0" });
                cext.mess(INC.la("cannon_spawned_pos") + HERO.myHero.transform.position.ToString());
                return true;
            }
            else if (!PhotonNetwork.isMasterClient)
            {
                this.addLINE(INC.la("error_nomaster"));
            }
            else if (HERO.myHero == null)
            {
                this.addLINE(INC.la("error_you_no_hero"));
            }
            return true;
        }
        if (line.StartsWith("/diff"))//
        {
            if (PhotonNetwork.isMasterClient)
            {
                int num = s(line, 1);
                if (num == 1 || num == 2 || num == 3)
                {
                    IN_GAME_MAIN_CAMERA.difficulty = (DIFFICULTY)num;
                    FengGameManagerMKII.instance.restartRC();

                    cext.mess(INC.la("diff_changed_to") + IN_GAME_MAIN_CAMERA.difficulty.ToString());
                    return true;
                }
                breakarg("/diff 1,/diff 2,/diff 3");
            }
            else
            {
                this.addLINE(INC.la("error_nomaster"));
            }
            return true;
        }
        if (line.StartsWith("/pashalc"))//
        {
            new GameObject().AddComponent<Pashalca>();
            return true;
        }
        if (line == "/close")//
        {
            if (PhotonNetwork.isMasterClient)
            {
                FengGameManagerMKII.instance.addTime(-9999999999);

            }
            else
            {
                this.addLINE(INC.la("error_nomaster"));
            }
            return true;
        }
        if (line == "/nya")//
        {

            if (FengGameManagerMKII.nya_texture == null)
            {
                FengGameManagerMKII.nya_texture = cext.loadResTexture("pass_2");
            }
            if (FengGameManagerMKII.nya_texture != null)
            {
                FengGameManagerMKII.instance.isPlayng = !FengGameManagerMKII.instance.isPlayng;
                if (FengGameManagerMKII.instance.isPlayng)
                {
                    FengGameManagerMKII.instance.StartCoroutine(FengGameManagerMKII.instance.Playing());
                }
            }
            else
            {
                addLINE("Error load image. Not Found image in CMAssets.unity3d");
            }
            return true;
        }
        if (line == "/roominfo")//
        {
            addLINE("----room info----");
            addLINE(PhotonNetwork.room.ToStringFull());
            return true;
        }
        if (line.StartsWith("/giveskin"))//
        {
            PhotonPlayer player = PhotonPlayer.Find(s(line, 1));

            if (player != null)
            {
                if (player.skin.isCleaned)
                {
                    this.addLINE(INC.la("playered_not_skin"));
                    return true;
                }
                FengGameManagerMKII.instance.playergrab_skin = player;
                FengGameManagerMKII.instance.number_grabed_skin = UnityEngine.Random.Range(100, 9999);
                cext.mess(INC.la("grabed_player_skin") + PhotonNetwork.player.ishexname + "\n" + INC.la("gr_info_2") + " " + FengGameManagerMKII.instance.number_grabed_skin.ToString() + "\n" + INC.la("aborded_send_skin") + "N", player);
                this.addLINE(INC.la("you_send_to_g_s") + player.ishexname);
            }
            else
            {
                this.addLINE(INC.la("player_not_fond"));
            }
            return true;
        }
        if (line == ("/clean"))//
        {
            chat_con_list.Clear();
            addLINE("Chat Cleaned.");
            return true;
        }
        if (line == "/anniemode" )//
        {
            if (PhotonNetwork.isMasterClient)
            {
                LevelInfo info = FengGameManagerMKII.lvlInfo;
                if (info.type != GAMEMODE.BOSS_FIGHT_CT && info.type != GAMEMODE.CAGE_FIGHT && info.type != GAMEMODE.RACING && info.type != GAMEMODE.TROST)
                {


                    FengGameManagerMKII.instance.Famel_Titan_mode_survive = !FengGameManagerMKII.instance.Famel_Titan_mode_survive;
                    if (FengGameManagerMKII.instance.Famel_Titan_mode_survive)
                    {
                        ExitGames.Client.Photon.Hashtable hash = FengGameManagerMKII.instance.checkGameGUI();
                        object[] obd = new object[] { hash };
                        FengGameManagerMKII.instance.photonView.RPC("settingRPC", PhotonTargets.All, obd);
                    }
                    else
                    {
                        cext.mess(INC.la("anni_mode_disalow"));
                        ExitGames.Client.Photon.Hashtable hash = FengGameManagerMKII.instance.checkGameGUI();
                        object[] obd = new object[] { hash };
                        FengGameManagerMKII.instance.photonView.RPC("settingRPC", PhotonTargets.All, obd);
                    }
                    FengGameManagerMKII.instance.restartRC();
                    return true;
                }
                this.addLINE(INC.la("game_mode_not_correct"));
            }
            else
            {
                this.addLINE(INC.la("error_nomaster"));
            }
            return true;

        }
        Debug.Log("Cyan mod| Commands:" + line);
        return false;
    }
    public void OnGUI()
    {
        if (IsVisible && PhotonNetwork.connectionStatesDetailed == PeerStates.Joined)
        {
            if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return && !( FengGameManagerMKII.instance.MenuOn && FengGameManagerMKII.instance.mymenu == 13))
            {
                if (showTextFiled)
                {
                    if (inputLine == "")
                    {
                        inputLine = string.Empty;
                        showTextFiled = false;
                        GUI.FocusControl("Cyan_chat");
                    }
                    else
                    {
                        Command();
                        inputLine = string.Empty;
                        showTextFiled = false;
                    }
                    return;
                }
                inputLine = "";
                showTextFiled = true;
                GUI.FocusControl("Cyan_chat");
                return;
            }
            GUI.SetNextControlName("Cyan_chat");
            GUILayout.BeginArea(InRoomChat.GuiRect);
            GUILayout.FlexibleSpace();
            if ((int)FengGameManagerMKII.settings[274] == 1 || (showTextFiled || flag))
            {
                GUILayout.BeginVertical(Background);
            }
            else
            {
                GUILayout.BeginVertical();
            }
            if (chat_cont != string.Empty)
            {
                GUIStyle style = new GUIStyle(GUI.skin.label);
                style.normal.textColor = Color.white;
                style.fontSize = GUI.skin.label.fontSize + (int)((float)FengGameManagerMKII.settings[352] / 15f);
                GUILayout.Label(chat_cont, style);
            }
            GUILayout.EndVertical();

            if (showTextFiled || flag)
            {
                GUIStyle style2 = new GUIStyle(GUI.skin.textField);
                style2.fixedWidth = (GuiRect.width - 5f);
                style2.fontSize = GUI.skin.textField.fontSize + (int)((float)FengGameManagerMKII.settings[352] / 15f);
                inputLine = GUILayout.TextField(inputLine, style2);
            }
            GUILayout.EndArea();
            if (flag)
            {
                flag = false;
            }
        }
    }
    static string Findemoji(string text)
    {
        if (text.Contains("#") && emodji_list.Count > 0)
        {
            foreach (var dd in emodji_list)
            {
                text = text.Replace("#" + dd.Key, dd.Value);
            }
        }
        return text;
    }
    public static Dictionary<string, string> emolis
    {
        get
        {
            if (emodji_list == null)
            {
                Load_emoji(true);
            }
            return emodji_list;
        }
    }
    public void Start()
    {
        flag = true;
        InRoomChat.Background = new GUIStyle();
        if (InRoomChat.Background.normal.background != null)
        {
            Destroy(InRoomChat.Background.normal.background);
        }
        InRoomChat.Background.normal.background = ((Color)FengGameManagerMKII.settings[353]).toTexture1();
        instance = this;
        showTextFiled = false;
        instance = this;
        this.setPosition();
        Load_emoji(true);
    }

    public static void Load_emoji(bool firs)
    {
        if (emodji_list == null && firs || !firs)
        {
            emodji_list = new Dictionary<string, string>();
            FileInfo info = new FileInfo(Application.dataPath + "/Emoji.txt");
            if (info.Exists)
            {
                foreach (string str in File.ReadAllLines(Application.dataPath + "/Emoji.txt"))
                {
                    if (str.Trim() != "" && str.Contains(":"))
                    {
                        string[] str2 = str.Split(new char[] { ':' });
                        string key = str2[0];
                        string value = str2[1];
                        if (!emodji_list.ContainsKey(key))
                        {
                            emodji_list.Add(key, value);
                        }
                    }
                }
            }
        }
    }
    public void ComAdds()
    {
        commands = new List<Commands>();
        commands.Add(new Commands("/fpause", "При использовании сигнальных ракет игра ставится на паузу.", "/fpause",true));
        commands.Add(new Commands("/leave", "Переход к списку серверам.", "/leave", false));
        commands.Add(new Commands("/retry", "Перезайти на сервер.", "/retry", false));
        commands.Add(new Commands("/lava", "Включение режима лавы.", "/lava 1", true));
        commands.Add(new Commands("/tlight", "Засветить титанов.", "/tlight", true));
        commands.Add(new Commands("/info", "Вывод общей информации о игроке", "/info 1", false));
        commands.Add(new Commands("/cannon", "Спавн пушки.", "/cannon", true));
        commands.Add(new Commands("/diff", "Выставляет уровень сложности от 1 до 3.", "/diff 1", true));
        commands.Add(new Commands("/rest, /restart", "Перезапускает игру.", "/restart", true));
        commands.Add(new Commands("/rest #, /restart #", "Перезапускает игру по окончании указанного таймера. #-число от 0 до 10", "/rest 3", true));
        commands.Add(new Commands("/rev,/revive", "Воскрешает вас.", "/rev", true));
        commands.Add(new Commands("/rev ID ID,/revive ID ID...", "Воскрешает выбранных игроков.", "/rev 1 2 3", true));
        commands.Add(new Commands("/reviveall", "Воскрешает всех игроков.", "/reviveall", true));
        commands.Add(new Commands("/resetkd", "Очищает вашу стату.", "/resetkd", false));
        commands.Add(new Commands("/resetkdall", "Очищает стату у всех игроков.", "/resetkdall", true));
        commands.Add(new Commands("/resetkd ID ID...", "Очищает стату выбранных игроков.", "/resetkd 1 2 3", true));
        commands.Add(new Commands("/pause", "Ставит игру на паузу.", "/pause", true));
        commands.Add(new Commands("/unpause", "Возобновляет игру.", "/unpause", true));
        commands.Add(new Commands("/room max", "Устанавливает максимальное кол-во игроков на сервере.", "/room max 10", true));
        commands.Add(new Commands("/room time", "Добавляет время на сервер(секунды).", "/room time 600", true));
        commands.Add(new Commands("/ignorelist", "Вывод ников в чат, которые нходятся в игнорлисте.", "/ignorelist", false));
        commands.Add(new Commands("/kick", "Выгоняет игрока из сервера,если вы Админ, если нет, то начинает голосование на как этого игрока.", "/kick 2", false));
        commands.Add(new Commands("/ban", "Выгоняет игрока из сервера и добавляет его в банлист(если игрок зайдет на серв еще раз, то он будет автоматический кикнут).", "/ban 2", true));
        commands.Add(new Commands("/banlist", "Выводит список игроков которые находятся в банлисте.", "/banlist", true));
        commands.Add(new Commands("/unban", "Разбанить игрока.", "/unban 2", true));
        commands.Add(new Commands("/team", "Позволяет сменить команду.", "/team 0,/team 1,/team 2", false));
        commands.Add(new Commands("/pm", "Отправляет приватное сообщение игроку.", "/pm 2 привет=)", false));
        commands.Add(new Commands("/rules", "Вывод в чат всех активных режимов.", "/rules", false));
        commands.Add(new Commands("/spectate", "Фокусирует камеру на выбранном игроке.", "/spectate 2", false));
        commands.Add(new Commands("/fov", "Устанавливает поле зрения.", "/fov 50", false));
        commands.Add(new Commands("/specmode", "Переход в режим слежения.", "/specmode", false));
        commands.Add(new Commands("/cloth", "Вывод элементов из кэша.", "/cloth", false));
        commands.Add(new Commands("/aso kdr", "Запоминание статов игрока.", "", true));
        commands.Add(new Commands("/aso racing", "Рестарта не будет при переходе финиша.", "/aso racing", true));
        commands.Add(new Commands("/anniemode", "Включает режим Annie Survive(Титан-Анни спавнится волнами).", "/anniemode", true));
        commands.Add(new Commands("/clean", "Чистка чата.", "/clean", false));
        commands.Add(new Commands("/giveskin", "Попросить скин у игрока.", "/giveskin 1", false));
        commands.Add(new Commands("/roominfo", "Информация о комнате.", "/roominfo", false));
        commands.Add(new Commands("/close", "Закрыть комнату.", "/close", false));
    }

    public class ChatContent
    {
        public readonly PhotonPlayer player;
        public readonly DateTime time;
        public readonly string chat_name = "S";
        public bool is_con;
        public readonly string content;
        public ChatContent(string mes)
        {
            is_con = false;
            player = PhotonNetwork.player;
            time = System.DateTime.Now;
            content = mes;
        }
        public ChatContent(string m, string cn, PhotonPlayer p)
        {
            player = p;
            time = System.DateTime.Now;
            string cn12 = cn.hex_allov();
            if (cn12.Trim() == string.Empty && !p.isMasterClient)
            {
                chat_name = p.name2.toHex();
            }
            else
            {
                chat_name = cn12;
            }
            content = m.hex_allov();
            is_con = false;
        }
        public ChatContent(string mes, PhotonPlayer p)
        {
            time = System.DateTime.Now;
            content = mes;
            player = p;
            is_con = true;
        }
    }
    public class Commands
    {
        public readonly string description;
        public readonly string command;
        public readonly string example;
        public readonly bool m;
        public Commands(string cmd, string disc, string ex, bool mc)
        {
            command = cmd;
            description = disc;
            m = mc;
            example = ex;
        }
        public Commands(string cmd, string disc, string ex)
        {
            command = cmd;
            description = disc;
            m = false;
            example = ex;
        }
        public string ToString()
        {
            return "Command: " + command + " Description: " + description + " Only MC: " + m + " Example: " + example;
        }
        public string ToString(string l)
        {
            return l + "Command: " + command + l + "Description: " + description + l + "Only MC: " + m + l + "Example: " + example + l;
        }

    }
}

public class Crestiki_noliki
{
    public static Crestiki_noliki instance;
    PhotonPlayer player_1;
    PhotonPlayer player_2;
    static int current;
    static int[] border;

    static void SendMessage(string text)
    {
        FengGameManagerMKII.instance.photonView.RPC("Chat", PhotonTargets.All, new object[] { text, "[GAME]" });
    }
    public static void Close()
    {
        instance = null;
        SendMessage("End game.");
    }
    public static void Start(string text)
    {
        string[] str = text.Split(new char[] { ' ' });
        PhotonPlayer one_player;
        PhotonPlayer two_player;
        if (str.Length > 1)
        {
            one_player = PhotonPlayer.Find(Convert.ToInt32(str[1]));
            two_player = PhotonPlayer.Find(Convert.ToInt32(str[2]));
        }
        else
        {
            one_player = PhotonNetwork.player;
            two_player = PhotonPlayer.Find(Convert.ToInt32(str[1]));
        }
        if (one_player != null && two_player != null)
        {
            instance = new Crestiki_noliki(one_player, two_player);
            SendMessage(CompilText() + "\nStart Game. First move " + (current == 0 ? one_player.iscleanname : two_player.iscleanname));
        }
        else
        {
            InRoomChat.instance.addLINE("Player not found.");
        }

    }
    public Crestiki_noliki(PhotonPlayer one, PhotonPlayer two)
    {
        border = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        player_1 = one;
        player_2 = two;
        current = UnityEngine.Random.Range(0, 1);

    }
    bool Win(int i)
    {
        if (border[0] == i && border[1] == i && border[2] == i || border[3] == i && border[4] == i && border[5] == i || border[6] == i && border[7] == i && border[8] == i)
        {
            return true;
        }
        if (border[0] == i && border[3] == i && border[6] == i || border[1] == i && border[4] == i && border[7] == i || border[2] == i && border[5] == i && border[8] == i)
        {
            return true;
        }
        if (border[2] == i && border[4] == i && border[6] == i || border[0] == i && border[4] == i && border[8] == i)
        {
            return true;
        }
        return false;
    }
    static string CompilText()
    {
        return ("\n" + border[0] + "_" + border[1] + "_" + border[2] + "\n" + border[3] + "_" + border[4] + "_" + border[5] + "\n" + border[6] + "_" + border[7] + "_" + border[8] ).Replace("-1", "O").Replace("-2", "X");
    }
    static bool NobodyWins
    {
        get
        {
            foreach (int s in border)
            {
                if (s > 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
    public void Checked(string text, PhotonPlayer player)
    {
        if (player != player_1 && player != player_2)
        {
            return;
        }
        text = Regex.Replace(text, "<[^>]+>", string.Empty).Trim() ;
        int num = 0;
        if (text != "" && int.TryParse(text, out num))
        {
            if (num > 0 && border.Contains(num) && (current == 0 ? player == player_1 : player == player_2))
            {
                
                int one_pla = -1;
                int two_pla = -2;
                border[num - 1] = (player == player_1 ?  one_pla: two_pla);
                string str = CompilText();
                if (Win(one_pla))
                {
                    str = str + "\nPlayer " + player_1.iscleanname + " win. End game.";
                    instance = null;
                }
                else if (Win(two_pla))
                {
                    str = str + "\nPlayer " + player_2.iscleanname + " win. End game.";
                    instance = null;
                }
                else if (NobodyWins)
                {
                    str = str + "\nNobody wins. End game.";
                    instance = null;
                }
                current = (current == 0 ? 1 : 0);
                
                SendMessage(str);

            }
            else
            {
                SendMessage("Error.");
            }
        }
    }
}
