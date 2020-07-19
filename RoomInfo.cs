using ExitGames.Client.Photon;
using System;
using System.Runtime.CompilerServices;
using CyanMod;

public class RoomInfo
{
    protected bool autoCleanUpField = PhotonNetwork.autoCleanUpPlayerObjects;
    private Hashtable customPropertiesField = new Hashtable();
    protected byte maxPlayersField;
    protected string nameField;
    protected bool openField = true;
    protected bool visibleField = true;
    protected string RoomName_to_Core_F;
    protected string RoomNameField;
    protected string MapNameField;
    protected string PasswordField;
    protected string DifficultyField;
    protected string DayTimeField;
    protected int FirstTimeFiled;
    protected int IDRoomFiled;


    protected internal RoomInfo(string roomName, Hashtable properties)
    {
        this.CacheProperties(properties);
        this.nameField = roomName;
        string[] name = roomName.Split(new char[] { '`' });
        RoomNameField = name[0].Resize(15);

        MapNameField = name[1];
        DifficultyField = name[2].ToUpper();
        FirstTimeFiled = Convert.ToInt32(name[3]);
        DayTimeField = name[4].ToUpper();
        if (name[5] != string.Empty)
        {
            PasswordField = name[5].Cript();
        }
        else
        {
            PasswordField = string.Empty; ;
        }
        IDRoomFiled = Convert.ToInt32(name[6]);
        string str33 = RoomNameField;
        if (str33.StripHex().Length > 20)
        {
            str33 = str33.StripHex().Substring(0, 19);
        }
        else
        {
            str33 = str33.toHex();
        }
        RoomName_to_Core_F = str33;
    }

    protected internal void CacheProperties(Hashtable propertiesToCache)
    {
        if (((propertiesToCache != null) && (propertiesToCache.Count != 0)) && !this.customPropertiesField.Equals(propertiesToCache))
        {
            if (propertiesToCache.ContainsKey((byte)0xfb))
            {
                this.removedFromList = (bool)propertiesToCache[(byte)0xfb];
                if (this.removedFromList)
                {
                    return;
                }
            }
            if (propertiesToCache.ContainsKey((byte)0xff))
            {
                this.maxPlayersField = (byte)propertiesToCache[(byte)0xff];
            }
            if (propertiesToCache.ContainsKey((byte)0xfd))
            {
                this.openField = (bool)propertiesToCache[(byte)0xfd];
            }
            if (propertiesToCache.ContainsKey((byte)0xfe))
            {
                this.visibleField = (bool)propertiesToCache[(byte)0xfe];
            }
            if (propertiesToCache.ContainsKey((byte)0xfc))
            {
                this.playerCount = (byte)propertiesToCache[(byte)0xfc];
            }
            if (propertiesToCache.ContainsKey((byte)0xf9))
            {
                this.autoCleanUpField = (bool)propertiesToCache[(byte)0xf9];
            }
            this.customPropertiesField.MergeStringKeys(propertiesToCache);
        }
    }

    public override bool Equals(object p)
    {
        Room room = p as Room;
        return ((room != null) && this.nameField.Equals(room.nameField));
    }

    public override int GetHashCode()
    {
        return this.nameField.GetHashCode();
    }

    public override string ToString()
    {
        object[] args = new object[] { this.nameField, !this.visibleField ? "hidden" : "visible", !this.openField ? "closed" : "open", this.maxPlayersField, this.playerCount };
        return string.Format("Room: '{0}' {1},{2} {4}/{3} players.", args);
    }

    public string ToStringFull()
    {
        object[] args = new object[] { this.nameField, !this.visibleField ? "hidden" : "visible", !this.openField ? "closed" : "open", this.maxPlayersField, this.playerCount, this.customPropertiesField.ToStringFull() };
        return string.Format("Room: '{0}' {1},{2} {4}/{3} players.\ncustomProps: {5}", args);
    }

    public Hashtable customProperties
    {
        get
        {
            return this.customPropertiesField;
        }
    }

    public bool isLocalClientInside { get; set; }

    public byte maxPlayers
    {
        get
        {
            return this.maxPlayersField;
        }
    }

    public string name
    {
        get
        {
            return this.nameField;
        }
    }

    public bool open
    {
        get
        {
            return this.openField;
        }
    }

    public int playerCount { get; private set; }

    public bool removedFromList { get; internal set; }

    public bool visible
    {
        get
        {
            return this.visibleField;
        }
    }
    public string RoomName_to_core
    {
        get
        {
            return RoomName_to_Core_F;
        }
    }
    public string RoomName
    {
        get
        {
            return RoomNameField;
        }
    }
    public string MapName
    {
        get
        {
            return MapNameField;
        }
    }
    public string Password
    {
        get
        {
            return PasswordField;
        }
    }
    public string Difficulty
    {
        get
        {
            return DifficultyField;
        }
    }
    public string DayTime
    {
        get
        {
            return DayTimeField;
        }
    }
    public int FirstTime
    {
        get
        {
            return FirstTimeFiled;
        }
    }
    public int IDRoom
    {
        get
        {
            return IDRoomFiled;
        }
    }
}

