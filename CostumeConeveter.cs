using ExitGames.Client.Photon;
using System;
using UnityEngine;

public class CostumeConeveter
{
    private static int DivisionToInt(DIVISION id)
    {
        if (id == DIVISION.TheGarrison)
        {
            return 0;
        }
        if (id == DIVISION.TheMilitaryPolice)
        {
            return 1;
        }
        if ((id != DIVISION.TheSurveryCorps) && (id == DIVISION.TraineesSquad))
        {
            return 3;
        }
        return 2;
    }

    public static void HeroCostumeToLocalData(HeroCostume costume, string slot)
    {
        slot = slot.ToUpper();
        PlayerPrefs.SetInt(slot + PhotonPlayerProperty.sex, SexToInt(costume.sex));
        PlayerPrefs.SetInt(slot + PhotonPlayerProperty.costumeId, costume.costumeId);
        PlayerPrefs.SetInt(slot + PhotonPlayerProperty.heroCostumeId, costume.id);
        PlayerPrefs.SetInt(slot + PhotonPlayerProperty.cape, !costume.cape ? 0 : 1);
        PlayerPrefs.SetInt(slot + PhotonPlayerProperty.hairInfo, costume.hairInfo.id);
        PlayerPrefs.SetInt(slot + PhotonPlayerProperty.eye_texture_id, costume.eye_texture_id);
        PlayerPrefs.SetInt(slot + PhotonPlayerProperty.beard_texture_id, costume.beard_texture_id);
        PlayerPrefs.SetInt(slot + PhotonPlayerProperty.glass_texture_id, costume.glass_texture_id);
        PlayerPrefs.SetInt(slot + PhotonPlayerProperty.skin_color, costume.skin_color);
        PlayerPrefs.SetFloat(slot + PhotonPlayerProperty.hair_color1, costume.hair_color.r);
        PlayerPrefs.SetFloat(slot + PhotonPlayerProperty.hair_color2, costume.hair_color.g);
        PlayerPrefs.SetFloat(slot + PhotonPlayerProperty.hair_color3, costume.hair_color.b);
        PlayerPrefs.SetInt(slot + PhotonPlayerProperty.division, DivisionToInt(costume.division));
        PlayerPrefs.SetInt(slot + PhotonPlayerProperty.statSPD, costume.stat.SPD);
        PlayerPrefs.SetInt(slot + PhotonPlayerProperty.statGAS, costume.stat.GAS);
        PlayerPrefs.SetInt(slot + PhotonPlayerProperty.statBLA, costume.stat.BLA);
        PlayerPrefs.SetInt(slot + PhotonPlayerProperty.statACL, costume.stat.ACL);
        PlayerPrefs.SetString(slot + PhotonPlayerProperty.statSKILL, costume.stat.skillId);
    }

    public static void HeroCostumeToPhotonData(HeroCostume costume, PhotonPlayer player)
    {

    }

    public static void HeroCostumeToPhotonData2(HeroCostume costume, PhotonPlayer player)
    {
        player.sex = SexToInt(costume.sex);
        int costumeId = costume.costumeId;
        if (costumeId == 0x1a)
        {
            costumeId = 0x19;
        }
        player.costumeId = costumeId;
        player.heroCostumeId = costume.id;
        player.cape = costume.cape;
        player.hairInfo = costume.hairInfo.id;
        player.eye_texture_id = costume.eye_texture_id;
        player.beard_texture_id = costume.beard_texture_id;
        player.glass_texture_id = costume.glass_texture_id;
        player.skin_color = costume.skin_color;
        player.hair_color1 = costume.hair_color.r;
        player.hair_color2 = costume.hair_color.g;
        player.hair_color3 = costume.hair_color.b;
        player.division = DivisionToInt(costume.division);
        player.statSPD = costume.stat.SPD;
        player.statGAS = costume.stat.GAS;
        player.statBLA = costume.stat.BLA;
        player.statACL = costume.stat.ACL;
        player.statSKILL = costume.stat.skillId;
    }

    public static HeroCostume HeroToAHSS(string slot)
    {
        slot = slot.ToUpper();
        HeroCostume costume = null;
        string str = slot.Substring(5).ToUpper();
        int index = 0;
        while (index < HeroCostume.costume.Length)
        {
            if (HeroCostume.costume[index].name.ToUpper() != str)
            {
                index++;
            }
            else
            {
                int id = HeroCostume.costume[index].id;
                if (slot != "AHSS")
                {
                    id += CheckBoxCostume.costumeSet - 1;
                }
                if (HeroCostume.costume[id].name != HeroCostume.costume[index].name)
                {
                    id = HeroCostume.costume[index].id + 1;
                }
                costume = HeroCostume.costume[id];
                break;
            }
        }
        if (costume == null)
        {
            return HeroCostume.costume[0];
        }
        HeroCostume costume3 = new HeroCostume
        {
            sex = costume.sex,
            id = HeroCostume.costume[0x25].costumeId,
            costumeId = HeroCostume.costume[0x25].costumeId,
            cape = costume.cape,
            hairInfo = costume.hairInfo,
            eye_texture_id = costume.eye_texture_id,
            beard_texture_id = costume.beard_texture_id,
            glass_texture_id = costume.glass_texture_id,
            skin_color = costume.skin_color,
            hair_color = costume.hair_color,
            division = costume.division,
            stat = costume.stat
        };
        costume3.setBodyByCostumeId(-1);
        costume3.setMesh();
        costume3.setTexture();
        return costume3;
    }

    private static DIVISION IntToDivision(int id)
    {
        if (id == 0)
        {
            return DIVISION.TheGarrison;
        }
        if (id == 1)
        {
            return DIVISION.TheMilitaryPolice;
        }
        if ((id != 2) && (id == 3))
        {
            return DIVISION.TraineesSquad;
        }
        return DIVISION.TheSurveryCorps;
    }

    private static SEX IntToSex(int id)
    {
        if (id == 0)
        {
            return SEX.FEMALE;
        }
        if (id == 1)
        {
            return SEX.MALE;
        }
        return SEX.MALE;
    }

    private static UNIFORM_TYPE IntToUniformType(int id)
    {
        if (id == 0)
        {
            return UNIFORM_TYPE.CasualA;
        }
        if (id == 1)
        {
            return UNIFORM_TYPE.CasualB;
        }
        if (id != 2)
        {
            if (id == 3)
            {
                return UNIFORM_TYPE.UniformB;
            }
            if (id == 4)
            {
                return UNIFORM_TYPE.CasualAHSS;
            }
        }
        return UNIFORM_TYPE.UniformA;
    }

    public static HeroCostume LocalDataToHeroCostume(string slot)
    {
        slot = slot.ToUpper();
        if (!PlayerPrefs.HasKey(slot + PhotonPlayerProperty.sex))
        {
            return HeroCostume.costume[0];
        }
        HeroCostume costume = new HeroCostume();
        costume = new HeroCostume
        {
            sex = IntToSex(PlayerPrefs.GetInt(slot + PhotonPlayerProperty.sex)),
            id = PlayerPrefs.GetInt(slot + PhotonPlayerProperty.heroCostumeId),
            costumeId = PlayerPrefs.GetInt(slot + PhotonPlayerProperty.costumeId),
            cape = (PlayerPrefs.GetInt(slot + PhotonPlayerProperty.cape) != 1) ? false : true,
            hairInfo = (IntToSex(PlayerPrefs.GetInt(slot + PhotonPlayerProperty.sex)) == SEX.FEMALE) ? CostumeHair.hairsF[PlayerPrefs.GetInt(slot + PhotonPlayerProperty.hairInfo)] : CostumeHair.hairsM[PlayerPrefs.GetInt(slot + PhotonPlayerProperty.hairInfo)],
            eye_texture_id = PlayerPrefs.GetInt(slot + PhotonPlayerProperty.eye_texture_id),
            beard_texture_id = PlayerPrefs.GetInt(slot + PhotonPlayerProperty.beard_texture_id),
            glass_texture_id = PlayerPrefs.GetInt(slot + PhotonPlayerProperty.glass_texture_id),
            skin_color = PlayerPrefs.GetInt(slot + PhotonPlayerProperty.skin_color),
            hair_color = new Color(PlayerPrefs.GetFloat(slot + PhotonPlayerProperty.hair_color1), PlayerPrefs.GetFloat(slot + PhotonPlayerProperty.hair_color2), PlayerPrefs.GetFloat(slot + PhotonPlayerProperty.hair_color3)),
            division = IntToDivision(PlayerPrefs.GetInt(slot + PhotonPlayerProperty.division)),
            stat = new HeroStat()
        };
        costume.stat.SPD = PlayerPrefs.GetInt(slot + PhotonPlayerProperty.statSPD);
        costume.stat.GAS = PlayerPrefs.GetInt(slot + PhotonPlayerProperty.statGAS);
        costume.stat.BLA = PlayerPrefs.GetInt(slot + PhotonPlayerProperty.statBLA);
        costume.stat.ACL = PlayerPrefs.GetInt(slot + PhotonPlayerProperty.statACL);
        costume.stat.skillId = PlayerPrefs.GetString(slot + PhotonPlayerProperty.statSKILL);
        costume.setBodyByCostumeId(-1);
        costume.setMesh();
        costume.setTexture();
        return costume;
    }

    public static HeroCostume PhotonDataToHeroCostume(PhotonPlayer player)
    {
        return null;
    }

    public static HeroCostume PhotonDataToHeroCostume2(PhotonPlayer player)
    {
        HeroCostume costume = new HeroCostume();
        SEX sex = IntToSex(player.sex);
        costume = new HeroCostume
        {
            sex = sex,
            costumeId = player.costumeId,
            id = player.heroCostumeId,
            cape = player.cape,
            hairInfo = (sex != SEX.MALE) ? CostumeHair.hairsF[player.hairInfo] : CostumeHair.hairsM[player.hairInfo],
            eye_texture_id = player.eye_texture_id,
            beard_texture_id = player.beard_texture_id,
            glass_texture_id = player.glass_texture_id,
            skin_color = player.skin_color,
            hair_color = new Color(player.hair_color1, player.hair_color2, player.hair_color3),
            division = IntToDivision(player.division),
            stat = new HeroStat()
        };
        costume.stat.SPD = player.statSPD;
        costume.stat.GAS = player.statGAS;
        costume.stat.BLA = player.statBLA;
        costume.stat.ACL = player.statACL;
        costume.stat.skillId = player.statSKILL;
        if ((costume.costumeId == 0x19) && (costume.sex == SEX.FEMALE))
        {
            costume.costumeId = 0x1a;
        }
        costume.setBodyByCostumeId(-1);
        costume.setMesh2();
        costume.setTexture();
        return costume;
    }

    private static int SexToInt(SEX id)
    {
        if (id == SEX.FEMALE)
        {
            return 0;
        }
        if (id == SEX.MALE)
        {
            return 1;
        }
        return 1;
    }

    private static int UniformTypeToInt(UNIFORM_TYPE id)
    {
        if (id == UNIFORM_TYPE.CasualA)
        {
            return 0;
        }
        if (id == UNIFORM_TYPE.CasualB)
        {
            return 1;
        }
        if (id != UNIFORM_TYPE.UniformA)
        {
            if (id == UNIFORM_TYPE.UniformB)
            {
                return 3;
            }
            if (id == UNIFORM_TYPE.CasualAHSS)
            {
                return 4;
            }
        }
        return 2;
    }
}

