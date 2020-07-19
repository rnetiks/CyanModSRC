using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using System.Collections;

public class PhotonPlayer
{
    private bool CMFiled;
    private int bytesFiled;
    private bool SLBFiled;
    private bool LockPlayerFiled;
    private string versionFiled;
    private string propertyFiled;
    private bool RSFiled;
    private bool RCFiled;
    private string versionRSFiled;
    private int actorID;
    public readonly bool isLocal;
    private string nameField;
    public object TagObject;
    public string[] url_customObject;
    public Texture2D[] texture_customObject;
    public Eve eve;
    protected internal PhotonPlayer(bool isLocal, int actorID, ExitGames.Client.Photon.Hashtable properties)
    {
        texture_customObject = new Texture2D[2];
        url_customObject = new string[2];
        pausedFlayer = false;
        bytesFiled = 0;
        SLBFiled = false;
        CMFiled = false;
        versionFiled = string.Empty;
        propertyFiled = string.Empty;
        RSFiled = false;
        RCFiled = false;
        LockPlayerFiled = false;
        versionRSFiled = string.Empty;
        this.actorID = -1;
        this.nameField = string.Empty;
        this.customProperties = new ExitGames.Client.Photon.Hashtable();
        this.isLocal = isLocal;
        this.actorID = actorID;
        this.InternalCacheProperties(properties);
        skin = new CyanSkin();
        eve = new Eve();
    }

    public PhotonPlayer(bool isLocal, int actorID, string name)
    {
        texture_customObject = new Texture2D[2];
        url_customObject = new string[2];
        pausedFlayer = false;
        bytesFiled = 0;
        SLBFiled = false;
        CMFiled = false;
        versionFiled = string.Empty;
        propertyFiled = string.Empty;
        RSFiled = false;
        RCFiled = false;
        versionRSFiled = string.Empty;
        LockPlayerFiled = false;
        this.actorID = -1;
        this.nameField = string.Empty;
        this.customProperties = new ExitGames.Client.Photon.Hashtable();
        this.isLocal = isLocal;
        this.actorID = actorID;
        this.nameField = name;
        skin = new CyanSkin();
        eve = new Eve();
    }
    public CyanSkin skin{get;set;}
    public int bytes
    {
        get
        {
            return bytesFiled;
        }
        set
        {
            bytesFiled = value;
        }
    }
    public bool LockPlayer
    {
        get
        {
            return LockPlayerFiled;
        }
        set
        {
            LockPlayerFiled = value;
        }
    }
    public bool RS
    {
        get
        {
            return RSFiled;
        }
        set
        {
            RSFiled = value;
        }
    }
    public bool RC
    {
        get
        {
            return RCFiled;
        }
        set
        {
            RCFiled = value;
        }
    }
    public string Property
    {
        get
        {
            return propertyFiled;
        }
        set
        {
            propertyFiled = value;
        }
    }
    public string versionRS
    {
        get
        {
            return versionRSFiled;
        }
        set
        {
            versionRSFiled = value;
        }
    }
    public bool CM
    {
        get
        {
            return CMFiled;
        }
        set
        {
            CMFiled = value;
        }
    }
    public bool SLB
    {
        get
        {
            return SLBFiled;
        }
        set
        {
            SLBFiled = value;
        }
    }
    public string version
    {
        get
        {
            return versionFiled;
        }
        set
        {
            versionFiled = value;
        }
    }
    public override bool Equals(object p)
    {
        PhotonPlayer player = p as PhotonPlayer;
        return ((player != null) && (this.GetHashCode() == player.GetHashCode()));
    }

    public static PhotonPlayer Find(int ID)
    {
        for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            PhotonPlayer player = PhotonNetwork.playerList[i];
            if (player.ID == ID)
            {
                return player;
            }
        }
        return null;
    }
    public static bool InRoom(PhotonPlayer player)
    {
       foreach(PhotonPlayer pl in  PhotonNetwork.playerList)
        {
            if (player == pl)
            {
                return true;
            }
        }
       return false;
    }
    public PhotonPlayer Get(int id)
    {
        return Find(id);
    }

    public override int GetHashCode()
    {
        return this.ID;
    }
    public bool pausedFlayer { get; set; }
    public PhotonPlayer GetNext()
    {
        return this.GetNextFor(this.ID);
    }

    public PhotonPlayer GetNextFor(PhotonPlayer currentPlayer)
    {
        if (currentPlayer == null)
        {
            return null;
        }
        return this.GetNextFor(currentPlayer.ID);
    }

    public PhotonPlayer GetNextFor(int currentPlayerId)
    {
        if (((PhotonNetwork.networkingPeer == null) || (PhotonNetwork.networkingPeer.mActors == null)) || (PhotonNetwork.networkingPeer.mActors.Count < 2))
        {
            return null;
        }
        Dictionary<int, PhotonPlayer> mActors = PhotonNetwork.networkingPeer.mActors;
        int num = 0x7fffffff;
        int num2 = currentPlayerId;
        foreach (int num3 in mActors.Keys)
        {
            if (num3 < num2)
            {
                num2 = num3;
            }
            else if ((num3 > currentPlayerId) && (num3 < num))
            {
                num = num3;
            }
        }
        if (num != 0x7fffffff)
        {
            return mActors[num];
        }
        return mActors[num2];
    }

    internal void InternalCacheProperties(ExitGames.Client.Photon.Hashtable properties)
    {
        if (((properties != null) && (properties.Count != 0)) && !this.customProperties.Equals(properties))
        {
            if (properties.ContainsKey((byte)0xff))
            {
                this.nameField = (string)properties[(byte)0xff];
            }
            this.customProperties.MergeStringKeys(properties);
            this.customProperties.StripKeysWithNullValues();
            toChage();
        }
    }

    internal void InternalChangeLocalID(int newID)
    {
        if (!this.isLocal)
        {
            Debug.LogError("ERROR You should never change PhotonPlayer IDs!");
        }
        else
        {
            this.actorID = newID;
        }
    }

    public void SetCustomProperties(ExitGames.Client.Photon.Hashtable propertiesToSet)
    {
        if (propertiesToSet != null)
        {
            this.customProperties.MergeStringKeys(propertiesToSet);
            this.customProperties.StripKeysWithNullValues();

            ExitGames.Client.Photon.Hashtable actorProperties = propertiesToSet.StripToStringKeys();
            if ((this.actorID > 0) && !PhotonNetwork.offlineMode)
            {
                PhotonNetwork.networkingPeer.OpSetCustomPropertiesOfActor(this.actorID, actorProperties, true, 0);
            }
            object[] parameters = new object[] { this, propertiesToSet };
            NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonPlayerPropertiesChanged, parameters);
            toChage();
        }
    }
    public void toChage()
    {
        if (customProperties["beard_texture_id"] != null)
        {
            this.beard_texture_idFiled = cext.returnIntFromObject(customProperties[PhotonPlayerProperty.beard_texture_id]);
        }
        if (customProperties["cape"] != null)
        {
            this.capeFiled = cext.returnBoolFromObject(customProperties[PhotonPlayerProperty.cape]);
        }
        if (customProperties["character"] != null)
        {
            this.characterFiled = cext.returnStringFromObject(customProperties[PhotonPlayerProperty.character]);
        }
        if (customProperties["costumeId"] != null)
        {
            this.costumeIdFiled = cext.returnIntFromObject(customProperties[PhotonPlayerProperty.costumeId]);
        }
        if (customProperties["currentLevel"] != null)
        {
            this.currentLevelFiled = cext.returnStringFromObject(customProperties[PhotonPlayerProperty.currentLevel]);
        }
        if (customProperties["customBool"] != null)
        {
            this.customBoolFiled = cext.returnBoolFromObject(customProperties[PhotonPlayerProperty.customBool]);
        }
        if (customProperties["customFloat"] != null)
        {
            this.customFloatFiled = cext.returnFloatFromObject(customProperties[PhotonPlayerProperty.customFloat]);
        }
        if (customProperties["customInt"] != null)
        {
            this.customIntFiled = cext.returnIntFromObject(customProperties[PhotonPlayerProperty.customInt]);
        }
        if (customProperties["customString"] != null)
        {
            this.customStringFiled = cext.returnStringFromObject(customProperties[PhotonPlayerProperty.customString]);
        }
        if (customProperties["dead"] != null)
        {
            this.deadFiled = cext.returnBoolFromObject(customProperties[PhotonPlayerProperty.dead]);
        }
        if (customProperties["deaths"] != null)
        {
            this.deathsFiled = cext.returnIntFromObject(customProperties[PhotonPlayerProperty.deaths]);
        }
        if (customProperties["division"] != null)
        {
            this.divisionFiled = cext.returnIntFromObject(customProperties[PhotonPlayerProperty.division]);
        }
        if (customProperties["eye_texture_id"] != null)
        {
            this.eye_texture_idFiled = cext.returnIntFromObject(customProperties[PhotonPlayerProperty.eye_texture_id]);
        }
        if (customProperties["glass_texture_id"] != null)
        {
            this.glass_texture_idFiled = cext.returnIntFromObject(customProperties[PhotonPlayerProperty.glass_texture_id]);
        }
        if (customProperties["guildName"] != null)
        {
            this.guildNameFiled = cext.returnStringFromObject(customProperties[PhotonPlayerProperty.guildName]);
        }
        if (customProperties["hair_color1"] != null)
        {
            this.hair_color1Filed = cext.returnFloatFromObject(customProperties[PhotonPlayerProperty.hair_color1]);
        }
        if (customProperties["hair_color2"] != null)
        {
            this.hair_color2Filed = cext.returnFloatFromObject(customProperties[PhotonPlayerProperty.hair_color2]);
        }
        if (customProperties["hair_color3"] != null)
        {
            this.hair_color3Filed = cext.returnFloatFromObject(customProperties[PhotonPlayerProperty.hair_color3]);
        }
        if (customProperties["hairInfo"] != null)
        {
            this.hairInfoFiled = cext.returnIntFromObject(customProperties[PhotonPlayerProperty.hairInfo]);
        }
        if (customProperties["heroCostumeId"] != null)
        {
            this.heroCostumeIdFiled = cext.returnIntFromObject(customProperties[PhotonPlayerProperty.heroCostumeId]);
        }
        if (customProperties["isTitan"] != null)
        {
            this.isTitanFiled = cext.returnIntFromObject(customProperties[PhotonPlayerProperty.isTitan]);
        }
        if (customProperties["kills"] != null)
        {
            this.killsFiled = cext.returnIntFromObject(customProperties[PhotonPlayerProperty.kills]);
        }
        if (customProperties["max_dmg"] != null)
        {
            this.max_dmgFiled = cext.returnIntFromObject(customProperties[PhotonPlayerProperty.max_dmg]);
        }
        if (customProperties["name"] != null)
        {
            this.nameFiled2 = cext.returnStringFromObject(customProperties[PhotonPlayerProperty.name]);
            string str = nameFiled2;

            if (str.Length > 30 && str.StripHex().Length > 30)
            {
                str.StripHex().Substring(0, 29);
            }
            is_name2 = "<color=#FFFFFF>" + str.toHex().Resize(15) + "</color>";
        }
        if (customProperties["RCBombA"] != null)
        {
            this.RCBombAFiled = cext.returnFloatFromObject(customProperties[PhotonPlayerProperty.RCBombA]);
        }
        if (customProperties["RCBombB"] != null)
        {
            this.RCBombBFiled = cext.returnFloatFromObject(customProperties[PhotonPlayerProperty.RCBombB]);
        }
        if (customProperties["RCBombG"] != null)
        {
            this.RCBombGFiled = cext.returnFloatFromObject(customProperties[PhotonPlayerProperty.RCBombG]);
        }
        if (customProperties["RCBombR"] != null)
        {
            this.RCBombRFiled = cext.returnFloatFromObject(customProperties[PhotonPlayerProperty.RCBombR]);
        }
        if (customProperties["RCBombRadius"] != null)
        {
            this.RCBombRadiusFiled = cext.returnFloatFromObject(customProperties[PhotonPlayerProperty.RCBombRadius]);
        }
        if (customProperties["RCteam"] != null)
        {
            this.RCteamFiled = cext.returnIntFromObject(customProperties[PhotonPlayerProperty.RCteam]);
        }
        if (customProperties["sex"] != null)
        {
            this.sexFiled = cext.returnIntFromObject(customProperties[PhotonPlayerProperty.sex]);
        }
        if (customProperties["skin_color"] != null)
        {
            this.skin_colorFiled = cext.returnIntFromObject(customProperties[PhotonPlayerProperty.skin_color]);
        }
        if (customProperties["statACL"] != null)
        {
            this.statACLFiled = cext.returnIntFromObject(customProperties[PhotonPlayerProperty.statACL]);
        }
        if (customProperties["statBLA"] != null)
        {
            this.statBLAFiled = cext.returnIntFromObject(customProperties[PhotonPlayerProperty.statBLA]);
        }
        if (customProperties["statGAS"] != null)
        {
            this.statGASFiled = cext.returnIntFromObject(customProperties[PhotonPlayerProperty.statGAS]);
        }
        if (customProperties["statSKILL"] != null)
        {
            this.statSKILLFiled = cext.returnStringFromObject(customProperties[PhotonPlayerProperty.statSKILL]);
        }
        if (customProperties["statSPD"] != null)
        {
            this.statSPDFiled = cext.returnIntFromObject(customProperties[PhotonPlayerProperty.statSPD]);
        }
        if (customProperties["team"] != null)
        {
            this.teamFiled = cext.returnIntFromObject(customProperties[PhotonPlayerProperty.team]);
        }
        if (customProperties["total_dmg"] != null)
        {
            this.total_dmgFiled = cext.returnIntFromObject(customProperties[PhotonPlayerProperty.total_dmg]);
        }
    }
    public override string ToString()
    {
        if (string.IsNullOrEmpty(this.name))
        {
            return String.Format("#{0:00}{1}", this.ID, !this.isMasterClient ? string.Empty : "(master)");
        }
        return String.Format("'{0}'{1}", this.name, !this.isMasterClient ? string.Empty : "(master)");
    }

    public string ToStringFull()
    {
        return String.Format("#{0:00} '{1}' {2}", this.ID, this.name, this.customProperties.ToStringFull());
    }

    public ExitGames.Client.Photon.Hashtable allProperties
    {
        get
        {
            ExitGames.Client.Photon.Hashtable target = new ExitGames.Client.Photon.Hashtable();
            target.Merge(this.customProperties);
            target[(byte)0xff] = this.name;
            return target;
        }
    }

    public ExitGames.Client.Photon.Hashtable customProperties { get; private set; }

    public int ID
    {
        get { return this.actorID; }
    }

    public string id
    {
        get { return "[" + this.actorID.ToString() + "]"; }
    }

    public bool isMasterClient
    {
        get { return (PhotonNetwork.networkingPeer.mMasterClient == this); }
    }
    public string name
    {
        get { return this.nameField; }
        set
        {
            if (!this.isLocal)
            {
                Debug.LogError("Error: Cannot change the name of a remote player!");
            }
            else
            {
                this.nameField = value;
            }
        }
    }
    string is_name2 = "";
    public string ishexname
    {
        get
        {
            return is_name2;
        }
    }
    public string iscleanname
    {
        get
        {
            return nameFiled2.StripHex() ;
        }
    }
    int beard_texture_idFiled = 0;
    public int beard_texture_id
    {
        get { return beard_texture_idFiled; }
        set
        {
            SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "beard_texture_id", value } });
        }
    }
    bool capeFiled = false;
    public bool cape
    {
        get { return capeFiled; }
        set
        {
            SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "cape", value } });
        }
    }
    string characterFiled = string.Empty;
    public string character
    {
        get { return characterFiled; }
        set
        {
            SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "character", value } });
        }
    }
    int costumeIdFiled = 0;
    public int costumeId
    {
        get { return costumeIdFiled; }
        set
        {
            SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "costumeId", value } });
        }
    }
    string currentLevelFiled = string.Empty;
    public string currentLevel
    {
        get { return currentLevelFiled; }
        set
        {
            SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "currentLevel", value } });
        }
    }
    bool customBoolFiled = false;
    public bool customBool
    {
        get { return customBoolFiled; }
        set
        {
            SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "customBool", value } });
        }
    }
    float customFloatFiled = 0;
    public float customFloat
    {
        get { return customFloatFiled; }
        set
        {
            SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "customFloat", value } });
        }
    }
    int customIntFiled = 0;
    public int customInt
    {
        get { return customIntFiled; }
        set
        {
            SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "customInt", value } });
        }
    }
    string customStringFiled = string.Empty;
    public string customString
    {
        get { return customStringFiled; }
        set
        {
            SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "customString", value } });
        }
    }
    bool deadFiled = true;
    public bool dead
    {
        get { return deadFiled; }
        set
        {
            SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "dead", value } });
        }
    }
    int deathsFiled = 0;
    public int deaths
    {
        get { return deathsFiled; }
        set
        {
            SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "deaths", value } });
        }
    }
    int divisionFiled = 0;
    public int division
    {
        get { return divisionFiled; }
        set
        {
            SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "division", value } });
        }
    }
    int eye_texture_idFiled = 0;
    public int eye_texture_id
    {
        get { return eye_texture_idFiled; }
        set
        {
            SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "eye_texture_id", value } });
        }
    }
    int glass_texture_idFiled = 0;
    public int glass_texture_id
    {
        get { return glass_texture_idFiled; }
        set
        {
            SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "glass_texture_id", value } });
        }
    }
    public string guildNameFiled = string.Empty;
    public string guildName
    {
        get { return guildNameFiled; }
        set
        {
            SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "guildName", value } });
        }
    }
    float hair_color1Filed = 0;
    public float hair_color1
    {
        get { return hair_color1Filed; }
        set
        {
            SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "hair_color1", value } });
        }
    }
    float hair_color2Filed = 0;
    public float hair_color2
    {
        get { return hair_color2Filed; }
        set
        {
            SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "hair_color2", value } });
        }
    }
    float hair_color3Filed = 0;
    public float hair_color3
    {
        get { return hair_color3Filed; }
        set
        {
            SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "hair_color3", value } });
        }
    }
    int hairInfoFiled = 0;
    public int hairInfo
    {
        get { return hairInfoFiled; }
        set
        {
            SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "hairInfo", value } });
        }
    }
    int heroCostumeIdFiled = 0;
    public int heroCostumeId
    {
        get { return heroCostumeIdFiled; }
        set
        {
            SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "heroCostumeId", value } });
        }
    }
    int isTitanFiled = 0;
    public int isTitan
    {
        get { return isTitanFiled; }
        set
        {
            SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "isTitan", value } });
        }
    }
    int killsFiled = 0;
    public int kills
    {
        get { return killsFiled; }
        set
        {
            ExitGames.Client.Photon.Hashtable Has = new ExitGames.Client.Photon.Hashtable();
            Has.Add("kills", value);
            SetCustomProperties(Has);
        }
    }
    int max_dmgFiled = 0;
    public int max_dmg
    {
        get { return max_dmgFiled; }
        set
        {
            SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "max_dmg", value } });
        }
    }
    string nameFiled2 = string.Empty;
    public string name2
    {
        get { return nameFiled2; }
        set
        {
            ExitGames.Client.Photon.Hashtable has = new ExitGames.Client.Photon.Hashtable();
            has.Add("name", value);
            this.SetCustomProperties(has);
        }
    }
    float RCBombAFiled = 0;
    public float RCBombA
    {
        get { return RCBombAFiled; }
        set
        {
            SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "RCBombA", value } });
        }
    }
    float RCBombBFiled = 0;
    public float RCBombB
    {
        get { return RCBombBFiled; }
        set
        {
            SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "RCBombB", value } });
        }
    }
    float RCBombGFiled = 0;
    public float RCBombG
    {
        get { return RCBombGFiled; }
        set
        {
            SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "RCBombG", value } });
        }
    }
    float RCBombRFiled = 0;
    public float RCBombR
    {
        get { return RCBombRFiled; }
        set
        {
            SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "RCBombR", value } });
        }
    }
    float RCBombRadiusFiled = 0;
    public float RCBombRadius
    {
        get { return RCBombRadiusFiled; }
        set
        {
            SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "RCBombRadius", value } });
        }
    }
    int RCteamFiled = 0;
    public int RCteam
    {
        get { return RCteamFiled; }
        set
        {
            SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "RCteam", value } });
        }
    }
    int sexFiled = 0;
    public int sex
    {
        get { return sexFiled; }
        set
        {
            SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "sex", value } });
        }
    }
    int skin_colorFiled = 0;
    public int skin_color
    {
        get { return skin_colorFiled; }
        set
        {
            SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "skin_color", value } });
        }
    }
    int statACLFiled = 0;
    public int statACL
    {
        get { return statACLFiled; }
        set
        {
            SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "statACL", value } });
        }
    }
    int statBLAFiled = 0;
    public int statBLA
    {
        get { return statBLAFiled; }
        set
        {
            SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "statBLA", value } });
        }
    }
    int statGASFiled = 0;
    public int statGAS
    {
        get { return statGASFiled; }
        set
        {
            SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "statGAS", value } });
        }
    }
    string statSKILLFiled = string.Empty;
    public string statSKILL
    {
        get { return statSKILLFiled; }
        set
        {
            SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "statSKILL", value } });
        }
    }
    int statSPDFiled = 0;
    public int statSPD
    {
        get { return statSPDFiled; }
        set
        {
            SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "statSPD", value } });
        }
    }
    int teamFiled = 0;
    public int team
    {
        get { return teamFiled; }
        set
        {
            SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "team", value } });
        }
    }
    int total_dmgFiled = 0;
    public int total_dmg
    {
        get { return total_dmgFiled; }
        set
        {
            SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "total_dmg", value } });
        }
    }

  
}

public class Eve
{
    public Dictionary<byte, int> codelist;
    public Dictionary<string, int> rpclist;
    public Eve()
    {
        codelist = new Dictionary<byte, int>();
        rpclist = new Dictionary<string, int>();
    }
    public void addcode(byte c)
    {
        if (codelist.ContainsKey(c))
        {
            codelist[c] = codelist[c] + 1;
        }
        else
        {
            codelist.Add(c, 0);
        }
    }
    public void addrpc(string c)
    {
        if (rpclist.ContainsKey(c))
        {
            rpclist[c] = rpclist[c] + 1;
        }
        else
        {
            rpclist.Add(c, 0);
        }
    }
}

