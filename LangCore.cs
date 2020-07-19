using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using CyanMod;

public class LangCore : UnityEngine.MonoBehaviour
{
    public static LangCore lang;
    public string ping { get; set; }
    public string offline_mode { get; set; }
    public string press { get; set; }
    public string next_player { get; set; }
    public string prev_player { get; set; }
    public string to_spectator { get; set; }
    public string wave { get; set; }
    public string survive { get; set; }
    public string to_rest { get; set; }
    public string humanity_fail { get; set; }
    public string game_rest_in { get; set; }
    public string second { get; set; }
    public string survive_all_waves { get; set; }
    public string human_win { get; set; }
    public string team { get; set; }
    public string win { get; set; }
    public string round_end { get; set; }
    public string respawn_in { get; set; }
    public string current_speed { get; set; }
    public string max_sped { get; set; }
    public string kills { get; set; }
    public string max_damage { get; set; }
    public string total_dmg { get; set; }
    public string time { get; set; }
    public string race_start_in { get; set; }
    public string wating { get; set; }
    public string titan_left { get; set; }
    public string current_wave { get; set; }
    public string human { get; set; }
    public string camera_hud { get; set; }
    public string room { get; set; }
    public string open { get; set; }
    public string closed { get; set; }
    public string entered_1 { get; set; }
    public string last_round { get; set; }
    public string use_cannon { get; set; }
    
    public void Start()
    {
        lang = this;
        last_round = INC.la("last_round_hud");
        entered_1 = INC.la("entered_1hud");
        closed = INC.la("closed_hud");
        open = INC.la("open_hud");
        room = INC.la("room_hud");
        camera_hud = INC.la("camera_hud");
        human = INC.la("human_hud");
        current_wave = INC.la("current_wave_hud");
        titan_left = INC.la("titan_left_hud");
        wating = INC.la("wating_hud");
        race_start_in = INC.la("race_start_in_hud");
        time = INC.la("time_hud");
        total_dmg = INC.la("total_dmg_hud");
        max_damage = INC.la("max_damage_hud");
        kills = INC.la("kills_hud");
        max_sped = INC.la("max_sped_hud");
        current_speed = INC.la("current_speed_hud");
        respawn_in = INC.la("respawn_in_hud");
        round_end = INC.la("round_end_hud");
        win = INC.la("win_hud");
        team = INC.la("team_hud");
        human_win = INC.la("human_win_in_hud");
        survive_all_waves = INC.la("survive_all_waves_in_hud");
        second = INC.la("second_in_hud");
        game_rest_in = INC.la("game_rest_in_hud");
        humanity_fail = INC.la("humanity_fail_hud");
        to_rest = INC.la("to_rest_hud");
        survive = INC.la("survive_hud");
        wave = INC.la("wave_hud");
        ping = INC.la("ping_hud");
        offline_mode = INC.la("offline_mode_hud");
        press = INC.la("press_hud");
        next_player = INC.la("next_player_hud");
        prev_player = INC.la("prev_player_hud");
        to_spectator = INC.la("to_spectator_hud");
        use_cannon = INC.la("to_use_cannon_hud");
    }
}