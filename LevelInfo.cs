using System;
using System.Collections.Generic;
using UnityEngine;
using CyanMod;

public class LevelInfo
{
    public string desc;
    public int enemyNumber;
    public bool hint;
    public bool horse;
    private static bool init;
    public bool lavaMode;
    public static List<LevelInfo> levels;
    public string mapName;
    public Minimap.Preset minimapPreset;
    public string name;
    public bool noCrawler;
    public bool punk = true;
    public bool pvp;
    public RespawnMode respawnMode;
    public bool supply = true;
    public bool teamTitan;
    public GAMEMODE type;
    public bool fog_mode = true;

    public static LevelInfo getInfo(string name)
    {
        initData2();
        foreach (LevelInfo info in levels)
        {
            if (info.name == name)
            {
                return info;
            }
        }
        return levels[0];
    }

    private static void initData()
    {

    }

    public static void initData2()
    {
        if (!init)
        {
            init = true;
            levels = new List<LevelInfo>();
            levels.Add(new LevelInfo()
            {
                name = "The City",
                mapName = "The City I",
                desc = INC.la("info_the_city_i"),
                enemyNumber = 10,
                type = GAMEMODE.KILL_TITAN,
                respawnMode = RespawnMode.NEVER,
                supply = true,
                teamTitan = true,
                pvp = true
            });

            levels.Add(new LevelInfo()
            {
                name = "The City II",
                mapName = "The City I",
                desc = INC.la("info_the_city_2"),
                enemyNumber = 10,
                type = GAMEMODE.KILL_TITAN,
                respawnMode = RespawnMode.DEATHMATCH,
                supply = true,
                teamTitan = true,
                pvp = true
            });

            levels.Add(new LevelInfo()
            {
                name = "Cage Fighting",
                mapName = "Cage Fighting",
                desc = "2 players in different cages. when you kill a titan,  one or more titan will spawn to your opponent's cage.",
                enemyNumber = 1,
                type = GAMEMODE.CAGE_FIGHT,
                respawnMode = RespawnMode.NEVER
            });

            levels.Add(new LevelInfo()
            {
                name = "The Forest",
                mapName = "The Forest",
                desc = INC.la("info_the_forest"),
                enemyNumber = 5,
                type = GAMEMODE.KILL_TITAN,
                respawnMode = RespawnMode.NEVER,
                supply = true,
                teamTitan = true,
                pvp = true
            });

            levels.Add(new LevelInfo()
            {
                name = "The Forest II",
                mapName = "The Forest",
                desc = INC.la("info_the_forest_2"),
                enemyNumber = 3,
                type = GAMEMODE.SURVIVE_MODE,
                respawnMode = RespawnMode.NEVER,
                supply = true
            });
            levels.Add(new LevelInfo()
            {
                name = "The Forest III",
                mapName = "The Forest",
                desc = INC.la("info_the_forest_3"),
                enemyNumber = 3,
                type = GAMEMODE.SURVIVE_MODE,
                respawnMode = RespawnMode.NEWROUND,
                supply = true
            });
            levels.Add(new LevelInfo()
            {
                name = "Annie",
                mapName = "The Forest",
                desc = INC.la("info_annie"),
                enemyNumber = 15,
                type = GAMEMODE.KILL_TITAN,
                respawnMode = RespawnMode.NEVER,
                punk = false,
                pvp = true
            });
            levels.Add(new LevelInfo()
            {
                name = "Annie II",
                mapName = "The Forest",
                desc = INC.la("info_annie_2"),
                enemyNumber = 15,
                type = GAMEMODE.KILL_TITAN,
                respawnMode = RespawnMode.DEATHMATCH,
                punk = false,
                pvp = true
            });
            levels.Add(new LevelInfo()
            {
                name = "Colossal Titan",
                mapName = "Colossal Titan",
                desc = INC.la("info_colossal_titan"),
                enemyNumber = 2,
                type = GAMEMODE.BOSS_FIGHT_CT,
                respawnMode = RespawnMode.NEVER,
                fog_mode = false
            });
            levels.Add(new LevelInfo()
            {
                name = "Colossal Titan II",
                mapName = "Colossal Titan",
                desc = INC.la("info_colossal_titan_2"),
                enemyNumber = 2,
                type = GAMEMODE.BOSS_FIGHT_CT,
                respawnMode = RespawnMode.DEATHMATCH
                ,
                fog_mode = false
            });

            levels.Add(new LevelInfo()
            {
                name = "Trost",
                mapName = "Colossal Titan",
                desc = INC.la("info_titan_eren"),
                enemyNumber = 2,
                type = GAMEMODE.TROST,
                respawnMode = RespawnMode.NEVER,
                punk = false,
                fog_mode = false
            });
            levels.Add(new LevelInfo()
            {
                name = "Trost II",
                mapName = "Colossal Titan",
                desc = INC.la("info_titan_eren_2"),
                enemyNumber = 2,
                type = GAMEMODE.TROST,
                respawnMode = RespawnMode.DEATHMATCH,
                punk = false,
                fog_mode = false
            });
            levels.Add(new LevelInfo()
            {
                name = "[S]City",
                mapName = "The City I",
                desc = INC.la("info_city_single"),
                enemyNumber = 15,
                type = GAMEMODE.KILL_TITAN,
                respawnMode = RespawnMode.NEVER,
                supply = true
            });
            levels.Add(new LevelInfo()
            {
                name = "[S]Forest",
                mapName = "The Forest",
                desc = string.Empty,
                enemyNumber = 15,
                type = GAMEMODE.KILL_TITAN,
                respawnMode = RespawnMode.NEVER,
                supply = true
            });
            levels.Add(new LevelInfo()
            {
                name = "[S]Forest Survive(no crawler)",
                mapName = "The Forest",
                desc = string.Empty,
                enemyNumber = 3,
                type = GAMEMODE.SURVIVE_MODE,
                respawnMode = RespawnMode.NEVER,
                supply = true,
                noCrawler = true,
                punk = true
            });
            levels.Add(new LevelInfo()
            {
                name = "[S]Tutorial",
                mapName = "tutorial",
                desc = string.Empty,
                enemyNumber = 1,
                type = GAMEMODE.KILL_TITAN,
                respawnMode = RespawnMode.NEVER,
                supply = true,
                hint = true,
                punk = false
            });
            levels.Add(new LevelInfo()
            {
                name = "[S]Battle training",
                mapName = "tutorial 1",
                desc = string.Empty,
                enemyNumber = 7,
                type = GAMEMODE.KILL_TITAN,
                respawnMode = RespawnMode.NEVER,
                supply = true,
                punk = false
            });
            levels.Add(new LevelInfo()
            {
                name = "The Forest IV  - LAVA",
                mapName = "The Forest",
                desc = INC.la("info_forest_4_lava"),
                enemyNumber = 3,
                type = GAMEMODE.SURVIVE_MODE,
                respawnMode = RespawnMode.NEWROUND,
                supply = true,
                noCrawler = true,
                lavaMode = true
            });
            levels.Add(new LevelInfo()
            {
                name = "The Forest IV  - LAVA",
                mapName = "The Forest",
                desc = INC.la("info_forest_4_lava"),
                enemyNumber = 3,
                type = GAMEMODE.SURVIVE_MODE,
                respawnMode = RespawnMode.NEWROUND,
                supply = true,
                noCrawler = true,
                lavaMode = true
            });
            levels.Add(new LevelInfo()
            {
                name = "[S]Racing - Akina",
                mapName = "track - akina",
                desc = string.Empty,
                enemyNumber = 0,
                type = GAMEMODE.RACING,
                respawnMode = RespawnMode.NEVER,
                supply = false
            });
            levels.Add(new LevelInfo()
            {
                name = "Racing - Akina",
                mapName = "track - akina",
                desc = INC.la("info_racing_akina"),
                enemyNumber = 0,
                type = GAMEMODE.RACING,
                respawnMode = RespawnMode.NEVER,
                supply = false,
                pvp = true
            });
            levels.Add(new LevelInfo()
            {
                name = "Outside The Walls",
                mapName = "OutSide",
                desc = INC.la("info_out_side"),
                enemyNumber = 0,
                type = GAMEMODE.PVP_CAPTURE,
                respawnMode = RespawnMode.DEATHMATCH,
                supply = true,
                horse = true,
                teamTitan = true,
                fog_mode = false
            });
            levels.Add(new LevelInfo()
            {
                name = "The City III",
                mapName = "The City I",
                desc = INC.la("info_the_city_3"),
                enemyNumber = 0,
                type = GAMEMODE.PVP_CAPTURE,
                respawnMode = RespawnMode.DEATHMATCH,
                supply = true,
                horse = false,
                teamTitan = true
            });
            levels.Add(new LevelInfo()
            {
                name = "Cave Fight",
                mapName = "CaveFight",
                desc = INC.la("info_cave_fight"),
                enemyNumber = -1,
                type = GAMEMODE.PVP_AHSS,
                respawnMode = RespawnMode.NEVER,
                supply = true,
                horse = false,
                teamTitan = true,
                pvp = true
            });
            levels.Add(new LevelInfo()
            {
                name = "House Fight",
                mapName = "HouseFight",
                desc = INC.la("info_house_fight"),
                enemyNumber = -1,
                type = GAMEMODE.PVP_AHSS,
                respawnMode = RespawnMode.NEVER,
                supply = true,
                horse = false,
                teamTitan = true,
                pvp = true
            });
            levels.Add(new LevelInfo()
            {
                name = "[S]Forest Survive(no crawler no punk)",
                mapName = "The Forest",
                desc = string.Empty,
                enemyNumber = 3,
                type = GAMEMODE.SURVIVE_MODE,
                respawnMode = RespawnMode.NEVER,
                supply = true,
                noCrawler = true,
                punk = false
            });
            levels.Add(new LevelInfo()
            {
                name = "Custom",
                mapName = "The Forest",
                desc = INC.la("info_custom"),
                enemyNumber = 1,
                type = GAMEMODE.KILL_TITAN,
                respawnMode = RespawnMode.NEVER,
                supply = true,
                teamTitan = true,
                pvp = true,
                punk = true
            });
            levels.Add(new LevelInfo()
            {
                name = "Custom (No PT)",
                mapName = "The Forest",
                desc = INC.la("info_custom_nopt"),
                enemyNumber = 1,
                type = GAMEMODE.KILL_TITAN,
                respawnMode = RespawnMode.NEVER,
                pvp = true,
                punk = true,
                supply = true,
                teamTitan = false
            });
            levels.Add(new LevelInfo()
            {
                name = "[S]The Forest IV - LAVA",
                mapName = "The Forest",
                desc = INC.la("info_forest_4_lava"),
                enemyNumber = 3,
                type = GAMEMODE.SURVIVE_MODE,
                respawnMode = RespawnMode.NEWROUND,
                supply = true,
                noCrawler = true,
                lavaMode = true
            });

            levels[0].minimapPreset = new Minimap.Preset(new Vector3(22.6f, 0f, 13f), 731.9738f);
            levels[8].minimapPreset = new Minimap.Preset(new Vector3(8.8f, 0f, 65f), 765.5751f);
            levels[9].minimapPreset = new Minimap.Preset(new Vector3(8.8f, 0f, 65f), 765.5751f);
            levels[0x12].minimapPreset = new Minimap.Preset(new Vector3(443.2f, 0f, 1912.6f), 1929.042f);
            levels[0x13].minimapPreset = new Minimap.Preset(new Vector3(443.2f, 0f, 1912.6f), 1929.042f);
            levels[20].minimapPreset = new Minimap.Preset(new Vector3(2549.4f, 0f, 3042.4f), 3697.16f);
            levels[0x15].minimapPreset = new Minimap.Preset(new Vector3(22.6f, 0f, 13f), 734.9738f);
        }
    }
}

