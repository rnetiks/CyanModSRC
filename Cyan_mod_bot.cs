using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CyanMod;
using UnityEngine;
using System.IO;
using System.Collections;

public class Cyan_mod_bot : UnityEngine.MonoBehaviour
{
    public List<MyMemory> main_list;
    readonly string MemPath = Application.dataPath + "/Bot_Memory.txt";
    public static string myName
    {
        get
        {
            if (permament_memory != null)
            {
                return permament_memory["name"][0];
            }
            return "";
        }
        set
        {
            permament_memory["name"] = new List<string>() { value };
        }
    }

    string[] keys;

  public static Dictionary<string, List<string>> permament_memory;
    List<int> my_players;
    List<int> prioritet_player;
    void Awake()
    {
        LogBot.AddLog("Start Bot.", LogType.Log);
        DontDestroyOnLoad(base.gameObject);
        my_players = new List<int>();
        prioritet_player = new List<int>();
    }
    string list(string keys)
    {
        if (permament_memory.ContainsKey(keys) )
        {
            if (permament_memory[keys].Count > 0)
            {
                return permament_memory[keys][UnityEngine.Random.Range(0, permament_memory[keys].Count)];
            }
            else
            {
                LogBot.AddLog("List Clean Key:" + keys, LogType.Error);
                return "";
            }
        }
        else
        {
            LogBot.AddLog("Key Not Found! Key:" + keys, LogType.Error);
            return "";
        }
    }
    void Clean()
    {
        prioritet_player.Clear();
        my_players.Clear();
    }
    void SendMessage(string text, PhotonPlayer player = null)
    {
        FengGameManagerMKII.instance.photonView.RPC("Chat", PhotonTargets.All, new object[] { text, myName });
        LogBot.AddLog("Send Message " + text, LogType.Log);
    }

    bool PrioritetPlayer(PhotonPlayer player)
    {
        return (player.isMasterClient || prioritet_player.Contains(player.ID));
    }
    void AddFocusedPlayer(int ID)
    {
        if (!my_players.Contains(ID))
        {
            my_players.Add(ID);
            LogBot.AddLog("Focused player " + ID, LogType.Log);
        }
        else
        {
            LogBot.AddLog("Contains player in Focused player " + ID, LogType.Warning);
        }
    }
    void RemoveFocusedPlayer(int ID)
    {
        if (!my_players.Contains(ID))
        {
            my_players.Remove(ID);
            LogBot.AddLog("Remove Focused player " + ID, LogType.Log);
        }
        else
        {
            LogBot.AddLog("Contains Remove Focused player " + ID, LogType.Warning);
        }
    }
   public void FocusedPlayerInRoom()
    {
        foreach (int s in my_players)
        {
            if (PhotonPlayer.Find(s) == null)
            {
                my_players.Remove(s);
            }
        }
        foreach (int s in prioritet_player)
        {
            if (PhotonPlayer.Find(s) == null)
            {
                prioritet_player.Remove(s);
            }
        }
    }
    bool MasterCommand(string text, PhotonPlayer player)
    {
        if (PrioritetPlayer(player))
        {

        }
        return false;
    }
    bool OtherCommands(string text, PhotonPlayer player)
    {
        return false;
    }
    public void Command(string text, PhotonPlayer player)
    {
        string nne = text;
        text = text.ToLower().Trim();

        if (text.StartsWith(myName.ToLower()))
        {
            if (text == myName.ToLower())
            {
                AddFocusedPlayer(player.ID);
                SendMessage(player.iscleanname + "," + list("ya_sluhau"));
            }
            else
            {
                string ll = FindOtvet(text);
                if (!MasterCommand(ll, player) && !OtherCommands(ll, player))
                {
                    SendMessage(player.iscleanname + "," + ll);
                }
            }
        }
        else if (my_players.Contains(player.ID))
        {
            string ll = FindOtvet(text);
            if (!MasterCommand(ll, player) && !OtherCommands(ll, player))
            {
                SendMessage(player.iscleanname + "," + ll);
            }
        }
    }
    string FindOtvet(string text)
    {
        string[] str = text.Split(new char[] { ' ',',' });
        foreach (MyMemory mm in main_list)
        {
            if (mm.vopros.Count > 0)
            {
                foreach (List<string> str2 in mm.vopros)
                {
                    if (str2.Count > 0)
                    {
                        bool[] bl = new bool[str2.Count];
                        for (int s = 0; s < bl.Length; s++)
                        {
                            bl[s] = false;
                        }
                        for (int d = 0; d < str2.Count; d++)
                        {
                            for (int g = 0; g < str.Length; g++)
                            {
                                if (str[g].Contains(str2[d]))
                                {
                                    bl[d] = true;
                                }
                            }
                        }
                        bool flag = false;
                        for (int s = 0; s < bl.Length; s++)
                        {
                            if (!bl[s])
                            {
                                flag = true;
                            }
                        }
                        if (!flag)
                        {
                            return mm.ovets[UnityEngine.Random.Range(0, mm.ovets.Count)];
                        }
                    }
                }
            }
        }
        return list("not_found");
    }
    void ApledEngine()
    {
        keys = new string[20];
        keys[0] = "$kill_titan$";
        keys[1] = "$lol$";
        keys[2] = "$kill_all_titans$";
        keys[3] = "$close_server$";
        keys[4] = "$spawn_titan$";
        keys[5] = "$cansel$";//
    }
    void Start()
    {
        permament_memory = new Dictionary<string, List<string>>();
        ApledEngine();
        main_list = new List<MyMemory>();
        FileInfo info = new FileInfo(MemPath);
        if (info.Exists)
        {
            string[] text = File.ReadAllLines(MemPath);
            bool glag45 = false;
            foreach (string line in text)
            {
                if (line.Trim() != "")
                {
                    if (line.StartsWith("#permament_memory#"))
                    {
                        glag45 = true;
                    }
                    else if (line.StartsWith("#end_permament_memory#"))
                    {
                        glag45 = false;
                    }
                    if (glag45 && line.Contains("$-$"))
                    {

                        string[] dtt = line.Split(new string[] { "$-$" }, StringSplitOptions.RemoveEmptyEntries);
                        string key = dtt[0].Trim();
                        string value = dtt[1].Trim();
                        if (!permament_memory.ContainsKey(key))
                        {
                            permament_memory.Add(key, value.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries).ToList());
                        }
                        else
                        {
                            LogBot.AddLog("Repait Keys " + key, LogType.Error);
                        }

                    }
                    else if (line.Contains("-||-"))
                    {
                        string[] str1 = line.Split(new string[] { "-||-" }, StringSplitOptions.RemoveEmptyEntries);
                        MyMemory mm = new MyMemory();
                        foreach (string vopr in str1[0].Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            mm.Addvopros(vopr.Trim().ToLower());
                        }
                        foreach (string vopr in str1[1].Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            mm.Addotvet(vopr.Trim());
                        }
                        main_list.Add(mm);
                    }
                }
            }
        }
        else
        {
            File.WriteAllText(MemPath, "", Encoding.UTF8);
        }
    }
  
    public class MyMemory
    {
        public List<List<string>> vopros;
        public List<string> ovets;
        public MyMemory()
        {
            vopros = new List<List<string>>();
            ovets = new List<string>();
        }
        public void Addotvet(string text)
        {
            if (!ovets.Contains(text))
            {
                ovets.Add(text.Replace("\n", "\n"));
            }
        }
        public void Addvopros(string text)
        {
            List<string> list = text.Split(new char[] { ' ' }).ToList();
            if (!vopros.Contains(list))
            {
                vopros.Add(list);
            }
        }
    }
    public class LogBot
    {
        public static List<LogBot> listLogBot;
        public static int maxlog = 50;
        public readonly LogType logtype;
        public readonly string message;
        public readonly string time;
        public LogBot(string mes, LogType log)
        {
            time = DateTime.Now.ToString("HH:mm:ss");
            message = mes;
            logtype = log;
        }
        public static void AddLog(string text, LogType log)
        {
            if (listLogBot == null)
            {
                listLogBot = new List<LogBot>();
            }
            listLogBot.Add(new LogBot(text, log));
            if (listLogBot.Count > maxlog)
            {
                listLogBot.Remove(listLogBot[0]);
            }
        }
    }
  
}

