using System;
using System.Collections.Generic;

using CyanMod;

public static class RCSettings
{
    static int ahssReloadFiled;
    static float aRateFiled;
    static int asoPreservekdrFiled;
    static int banErenFiled;
    static int bombModeFiled;
    static float cRateFiled;
    static int damageModeFiled;
    static int deadlyCannonsFiled;
    static int disableRockFiled;
    static int endlessModeFiled;
    static int explodeModeFiled;
    static int friendlyModeFiled;
    static int gameTypeFiled;
    static int globalDisableMinimapFiled;
    static int healthLowerFiled;
    static int healthModeFiled;
    static int healthUpperFiled;
    static int horseModeFiled;
    static int infectionModeFiled;
    static float jRateFiled;
    static int maxWaveFiled;
    static int moreTitansFiled;
    static string motdFiled;
    static float nRateFiled;
    static int pointModeFiled;
    static float pRateFiled;
    static int punkWavesFiled;
    static int pvpModeFiled;
    static int racingStaticFiled;
    static float sizeLowerFiled;
    static int sizeModeFiled;
    static float sizeUpperFiled;
    static int spawnModeFiled;
    static int teamModeFiled;
    static int titanCapFiled;
    static int waveModeNumFiled;
    static int waveModeOnFiled;
    static int anniesurvivemodeFiled;
    public static List<string> rules
    {
        get
        {
            List<string> lines = new List<string>();
            if (RCSettings.bombMode > 0)
            {
                lines.Add(INC.la("bomb_mode_is_on"));
            }
            if (RCSettings.teamMode > 0)
            {
                if (RCSettings.teamMode == 1)
                {
                    lines.Add(INC.la("team_mode_no_sort"));
                }
                else if (RCSettings.teamMode == 2)
                {
                    lines.Add(INC.la("team_mode_by_size"));
                }
                else if (RCSettings.teamMode == 3)
                {
                    lines.Add(INC.la("team_mode_by_skill"));
                }
            }
            if (RCSettings.pointMode > 0)
            {
                lines.Add(INC.la("point_mode_is_on") + "(" + Convert.ToString(RCSettings.pointMode) + ").");
            }
            if (RCSettings.disableRock > 0)
            {
                lines.Add(INC.la("punk_rock_disa"));
            }
            if (RCSettings.spawnMode > 0)
            {
                lines.Add(INC.la("custom_spawn_rate"));
                lines.Add(INC.la("t_nornal") + RCSettings.nRate.ToString("F2") + "%");
                lines.Add(INC.la("t_abnormal") + RCSettings.aRate.ToString("F2") + "%");
                lines.Add(INC.la("t_jumper") + RCSettings.jRate.ToString("F2") + "%");
                lines.Add(INC.la("t_crawler") + RCSettings.cRate.ToString("F2") + "%");
                lines.Add(INC.la("t_punk") + RCSettings.pRate.ToString("F2") + "%");
            }
            if (RCSettings.explodeMode > 0)
            {
                lines.Add(INC.la("titan_explode_mode") + Convert.ToString(RCSettings.explodeMode));
            }
            if (RCSettings.healthMode > 0)
            {
                lines.Add(INC.la("titan_health_mode") + "(" + Convert.ToString(RCSettings.healthLower) + "-" + Convert.ToString(RCSettings.healthUpper) + ")");

            }
            if (RCSettings.infectionMode > 0)
            {
                lines.Add(INC.la("infection_mode_on") + Convert.ToString(RCSettings.infectionMode) + ".");
            }
            if (RCSettings.damageMode > 0)
            {
                lines.Add(INC.la("minimum_nape_damage_is_on") + Convert.ToString(RCSettings.damageMode) + ".");
            }
            if (RCSettings.moreTitans > 0)
            {
                lines.Add(INC.la("custom_titan_is_on") + Convert.ToString(RCSettings.moreTitans) + ".");
            }
            if (RCSettings.sizeMode > 0)
            {
                lines.Add(INC.la("custom_titan_size_is_on") + "(" + RCSettings.sizeLower.ToString("F2") + "," + RCSettings.sizeUpper.ToString("F2") + ").");
            }
            if (RCSettings.banEren > 0)
            {
                lines.Add(INC.la("anti-eren_is_on"));
            }
            if (RCSettings.waveModeOn == 1)
            {
                lines.Add(INC.la("custom_wave_mode_is_on") + Convert.ToString(RCSettings.waveModeNum) + ".");
            }
            if (RCSettings.friendlyMode > 0)
            {
                lines.Add(INC.la("anti_friendly_fire_disabled"));
            }
            if (RCSettings.pvpMode > 0 && RCSettings.friendlyMode == 0)
            {
                if (RCSettings.pvpMode == 1)
                {
                    lines.Add(INC.la("pvp_is_on_team_based"));
                }
                else if (RCSettings.pvpMode == 2)
                {
                    lines.Add(INC.la("pvp_is_on_team_ffa"));
                }
            }
            if (RCSettings.maxWave > 0)
            {
                lines.Add(INC.la("max_wave_set_to") + RCSettings.maxWave.ToString());
            }
            if (RCSettings.horseMode > 0)
            {
                lines.Add(INC.la("horses_are_enabled"));
            }
            if (RCSettings.ahssReload > 0)
            {
                lines.Add(INC.la("ahhs_air_reload_disabled"));
            }
            if (RCSettings.punkWaves > 0)
            {
                lines.Add(INC.la("punk_override_every"));
            }
            if (RCSettings.endlessMode > 0)
            {
                lines.Add(INC.la("endless_respawn_is_enabled") + RCSettings.endlessMode.ToString() + INC.la("seconds"));
            }
            if (RCSettings.globalDisableMinimap > 0)
            {
                lines.Add(INC.la("minimaps_are_disabled"));
            }
            if (RCSettings.motd != string.Empty)
            {
                lines.Add(INC.la("motd") + RCSettings.motd);
            }
            if (RCSettings.deadlyCannons > 0)
            {
                lines.Add(INC.la("cannons_will_kill_humans"));
            }
            return lines;
        }
    }

    public static int AnnieSurvive
    {
        get
        {
            return anniesurvivemodeFiled;
        }
        set
        {
            anniesurvivemodeFiled = value;
        }

    }
    public static int ahssReload
    {
        get
        {
            return ahssReloadFiled;
        }
        set
        {
            ahssReloadFiled = value;
        }
    }
    public static float aRate
    {
        get
        {
            return aRateFiled;
        }
        set
        {
            aRateFiled = value;
        }
    }
    public static int asoPreservekdr
    {
        get
        {
            return asoPreservekdrFiled;
        }
        set
        {
            asoPreservekdrFiled = value;
        }
    }
    public static int banEren
    {
        get
        {
            return banErenFiled;
        }
        set
        {
            banErenFiled = value;
        }
    }
    public static int bombMode
    {
        get
        {
            return bombModeFiled;
        }
        set
        {
            bombModeFiled = value;
        }
    }
    public static float cRate
    {
        get
        {
            return cRateFiled;
        }
        set
        {
            cRateFiled = value;
        }
    }
    public static int damageMode
    {
        get
        {
            return damageModeFiled;
        }
        set
        {
            damageModeFiled = value;
        }
    }
    public static int deadlyCannons
    {
        get
        {
            return deadlyCannonsFiled;
        }
        set
        {
            deadlyCannonsFiled = value;
        }
    }
    public static int disableRock
    {
        get
        {
            return disableRockFiled;
        }
        set
        {
            disableRockFiled = value;
        }
    }
    public static int endlessMode
    {
        get
        {
            return endlessModeFiled;
        }
        set
        {
            endlessModeFiled = value;
        }
    }
    public static int explodeMode
    {
        get
        {
            return explodeModeFiled;
        }
        set
        {
            explodeModeFiled = value;
        }
    }
    public static int friendlyMode
    {
        get
        {
            return friendlyModeFiled;
        }
        set
        {
            friendlyModeFiled = value;
        }
    }
    public static int gameType
    {
        get
        {
            return gameTypeFiled;
        }
        set
        {
            gameTypeFiled = value;
        }
    }
    public static int globalDisableMinimap
    {
        get
        {
            return globalDisableMinimapFiled;
        }
        set
        {
            globalDisableMinimapFiled = value;
        }
    }
    public static int healthLower
    {
        get
        {
            return healthLowerFiled;
        }
        set
        {
            healthLowerFiled = value;
        }
    }
    public static int healthMode
    {
        get
        {
            return healthModeFiled;
        }
        set
        {
            healthModeFiled = value;
        }
    }
    public static int healthUpper
    {
        get
        {
            return healthUpperFiled;
        }
        set
        {
            healthUpperFiled = value;
        }
    }
    public static int horseMode
    {
        get
        {
            return horseModeFiled;
        }
        set
        {
            horseModeFiled = value;
        }
    }
    public static int infectionMode
    {
        get
        {
            return infectionModeFiled;
        }
        set
        {
            infectionModeFiled = value;
        }
    }
    public static float jRate
    {
        get
        {
            return jRateFiled;
        }
        set
        {
            jRateFiled = value;
        }
    }
    public static int maxWave
    {
        get
        {
            return maxWaveFiled;
        }
        set
        {
            maxWaveFiled = value;
        }
    }
    public static int moreTitans
    {
        get
        {
            return moreTitansFiled;
        }
        set
        {
            moreTitansFiled = value;
        }
    }
    public static string motd
    {
        get
        {
            return motdFiled;
        }
        set
        {
            motdFiled = value;
        }
    }
    public static float nRate
    {
        get
        {
            return nRateFiled;
        }
        set
        {
            nRateFiled = value;
        }
    }
    public static int pointMode
    {
        get
        {
            return pointModeFiled;
        }
        set
        {
            pointModeFiled = value;
        }
    }
    public static float pRate
    {
        get
        {
            return pRateFiled;
        }
        set
        {
            pRateFiled = value;
        }
    }
    public static int punkWaves
    {
        get
        {
            return punkWavesFiled;
        }
        set
        {
            punkWavesFiled = value;
        }
    }
    public static int pvpMode
    {
        get
        {
            return pvpModeFiled;
        }
        set
        {
            pvpModeFiled = value;
        }
    }
    public static int racingStatic
    {
        get
        {
            return racingStaticFiled;
        }
        set
        {
            racingStaticFiled = value;
        }
    }
    public static float sizeLower
    {
        get
        {
            return sizeLowerFiled;
        }
        set
        {
            sizeLowerFiled = value;
        }
    }
    public static int sizeMode
    {
        get
        {
            return sizeModeFiled;
        }
        set
        {
            sizeModeFiled = value;
        }
    }
    public static float sizeUpper
    {
        get
        {
            return sizeUpperFiled;
        }
        set
        {
            sizeUpperFiled = value;
        }
    }
    public static int spawnMode
    {
        get
        {
            return spawnModeFiled;
        }
        set
        {
            spawnModeFiled = value;
        }
    }
    public static int teamMode
    {
        get
        {
            return teamModeFiled;
        }
        set
        {
            teamModeFiled = value;
        }
    }
    public static int titanCap
    {
        get
        {
            return titanCapFiled;
        }
        set
        {
            titanCapFiled = value;
        }
    }
    public static int waveModeNum
    {
        get
        {
            return waveModeNumFiled;
        }
        set
        {
            waveModeNumFiled = value;
        }
    }
    public static int waveModeOn
    {
        get
        {
            return waveModeOnFiled;
        }
        set
        {
            waveModeOnFiled = value;
        }
    }
}

